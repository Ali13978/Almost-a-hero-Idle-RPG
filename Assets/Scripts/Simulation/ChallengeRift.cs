using System;
using System.Collections.Generic;
using System.Text;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class ChallengeRift : ChallengeWithTime
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.InitData();
		}

		public override void Reset()
		{
			base.Reset();
			this.charmBuffs.Clear();
			this.curseBuffs.Clear();
			this.allEnchantments.Clear();
			this.activeCharmEffects.Clear();
			this.activeCurseEffects.Clear();
			this.totalGainedUpgrades.Init();
			this.discardedCharms.Clear();
			this.totWave = 0;
		}

		public override void Abandon()
		{
			base.Abandon();
			this.world.OnRiftNoCompleted(this.id);
		}

		private void InitData()
		{
			this.heroesCanRevive = true;
			this.buffDataReviveInvulnerability = new BuffDataInvulnerability();
			this.buffDataReviveInvulnerability.id = 110;
			this.buffDataReviveInvulnerability.dur = 3f;
			this.buffDataReviveInvulnerability.visuals |= 128;
			this.totWave = 0;
			this.activeCharmEffects = new List<CharmEffectData>();
			this.discardedCharms = new List<int>();
			this.charmBuffs = new List<CharmBuff>();
			this.activeCurseEffects = new List<CurseEffectData>();
			this.curseBuffs = new List<CurseBuff>();
			this.allEnchantments = new List<EnchantmentBuff>();
		}

		protected override void SetEnvironments()
		{
			this.activeEnv = this.allEnvironments[0];
		}

		public override void OnSetupComplete(TotemDataBase totem, List<Rune> wornRunes, HeroDataBase[] heroesData, List<Gear> gears)
		{
			foreach (RiftEffect riftEffect in this.riftEffects)
			{
				riftEffect.OnRiftSetup(this.world, this, totem, wornRunes, heroesData, gears);
			}
			this.charmSelectionAddTimer = this.firstTimeCharmSelectionDuration;
			this.numCharmSelection = 0;
			if (this.IsCursed())
			{
				foreach (int key in this.world.currentSim.currentCurses)
				{
					CurseEffectData curseEffectData = this.world.currentSim.allCurseEffects[key];
					curseEffectData.isLoading = true;
					curseEffectData.level = 0;
					this.AddCurseEffect(curseEffectData);
					curseEffectData.isLoading = false;
				}
				this.curseSpawnIndex = 0;
				this.GetNextCurseSpawnIndex();
			}
			base.OnSetupComplete(totem, wornRunes, heroesData, gears);
		}

		private void GetNextCurseSpawnIndex()
		{
			this.curseSpawnIndex++;
			if (this.curseSpawnIndex >= this.world.currentSim.currentCurses.Count)
			{
				this.curseSpawnIndex = 0;
			}
			this.curseSpawnIndex = this.world.currentSim.currentCurses.FindIndex(this.curseSpawnIndex, (int c) => !(this.world.currentSim.allCurseEffects[c] is CurseEffectDataPermanent));
			if (this.curseSpawnIndex == -1)
			{
				this.curseSpawnIndex = this.world.currentSim.currentCurses.FindIndex(0, (int c) => !(this.world.currentSim.allCurseEffects[c] is CurseEffectDataPermanent));
			}
		}

		public override bool AreRepeatedHeroesAllowed()
		{
			return this.world.cursedChallenges.Contains(this) && this.world.currentSim.currentCurses.Contains(1019);
		}

		public override void Update(float dt, float unwarpedDt)
		{
			if (this.totWave < this.targetTotWaveNo - 1)
			{
				this.timerPaused = false;
			}
			else
			{
				Enemy enemy2 = this.enemies.Find((Enemy e) => e.GetName() == "BOSS WISE SNAKE");
				this.timerPaused = (enemy2 != null && enemy2.IsDead());
			}
			base.Update(dt, unwarpedDt);
			if (this.state != Challenge.State.ACTION)
			{
				if (this.state == Challenge.State.WON)
				{
					if (this.enemies.Find((Enemy enemy) => enemy.GetName() == "BOSS WISE SNAKE") != null)
					{
						base.UpdateEnemies(dt);
						return;
					}
				}
				if (this.state == Challenge.State.WON && !this.rewardGiven)
				{
					this.world.OnRiftWon();
					this.rewardGiven = true;
				}
				else if (this.state == Challenge.State.WON && this.enemies.Count > 0)
				{
					this.enemies.RemoveAll((Enemy enemy) => enemy.IsAlive());
				}
				return;
			}
			if (this.IsCursed() && this.curseSpawnIndex != -1)
			{
				this.curseProgress += this.riftData.cursesSetup.progressPerMinute * dt * 0.01666f;
				if (this.curseProgress >= 1f)
				{
					this.world.currentSim.allCurseEffects[this.world.currentSim.currentCurses[this.curseSpawnIndex]].IncrementLevel(this);
					this.GetNextCurseSpawnIndex();
					this.world.AddSoundEvent(new SoundEventSound(SoundType.GAMEPLAY, "curses", false, 0f, new SoundSimple(SoundArchieve.inst.curseAppear, 1f, float.MaxValue)));
					this.curseProgress -= 1f;
				}
			}
			int num = 0;
			using (List<CurseBuff>.Enumerator enumerator = this.curseBuffs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					CurseBuff curse = enumerator.Current;
					int index = this.activeCurseEffects.FindIndex((CurseEffectData c) => curse.enchantmentData == c);
					CurseEffectData curseEffectData = this.activeCurseEffects[index];
					if (curse.state == EnchantmentBuffState.INACTIVE && curseEffectData.level >= 0)
					{
						curseEffectData.DecrementLevel(this);
						num++;
					}
					if (curseEffectData.level >= 0)
					{
						curse.state = EnchantmentBuffState.ACTIVE;
					}
				}
			}
			for (int i = 0; i < num; i++)
			{
				foreach (CurseBuff curseBuff in this.curseBuffs)
				{
					if (curseBuff.state != EnchantmentBuffState.INACTIVE)
					{
						curseBuff.OnCurseDispelled();
					}
				}
			}
			if (num > 0)
			{
				this.world.AddSoundEvent(new SoundEventSound(SoundType.GAMEPLAY, "curses", false, 0f, new SoundSimple(SoundArchieve.inst.curseDispel, 1f, float.MaxValue)));
			}
			if (!base.IsThereAliveEnemy() && this.state != Challenge.State.WON)
			{
				if (this.enemies.Find((Enemy e) => e.GetName() == "BOSS WISE SNAKE") == null)
				{
					this.totWave++;
					this.OnNewWave();
					if (this.totWave <= this.targetTotWaveNo)
					{
						this.CreateNewEnemies();
					}
					if (this.firstTimeCharmForceWave == this.totWave && !this.earnedCharmSelectionBefore)
					{
						this.charmSelectionAddTimer = 0f;
					}
				}
			}
			if (this.totWave > this.targetTotWaveNo)
			{
				this.state = Challenge.State.WON;
				this.rewardGiven = false;
				if ((double)this.timeCounter < this.finishingRecord)
				{
					this.finishingRecord = (double)this.timeCounter;
				}
			}
			this.charmSelectionAddTimer -= dt * this.world.buffTotalEffect.charmSelectionTimerFactor;
			if (this.charmSelectionAddTimer <= 0f)
			{
				this.numCharmSelection++;
				this.charmSelectionAddTimer = 45f;
				this.earnedCharmSelectionBefore = true;
				this.world.AddSoundEvent(new SoundEventSound(SoundType.GAMEPLAY, string.Empty, false, 0f, new SoundSimple(SoundArchieve.inst.charmSelectionAvailable, 1f, float.MaxValue)));
			}
			if (this.world.pickRandomCharms && this.numCharmSelection > 0)
			{
				while (this.numCharmSelection > 0)
				{
					this.nextCharmDraftEffects = this.world.currentSim.GetNextCharmEffects(this.world.GetCharmSelectionNum());
					this.world.TryBuyWorldUpgrade();
					this.world.currentSim.TryToClaimCharm(GameMath.GetRandomInt(0, this.world.GetCharmSelectionNum(), GameMath.RandType.NoSeed));
					UiManager.newCharmIdAdded = this.activeCharmEffects[this.activeCharmEffects.Count - 1].BaseData.id;
				}
			}
		}

		public override bool ShouldWaitToFinish()
		{
			return this.enemies.Count > 0;
		}

		protected override void OnChallengeLost()
		{
			this.world.OnRiftLost();
			this.world.OnRiftNoCompleted(this.id);
		}

		public override bool HasTargetTotWave()
		{
			return true;
		}

		public override int GetTargetTotWave()
		{
			return this.targetTotWaveNo;
		}

		protected virtual int GetNumEnemies(int totWave)
		{
			return this.numEnemiesMin + totWave / 5 % (this.numEnemiesMax - this.numEnemiesMin);
		}

		protected override double GetEnemyPower(int totWave)
		{
			return this.baseEnemyPower + (double)totWave * this.incEnemyPowerPerWave;
		}

		private void CreateNewEnemies()
		{
			bool flag = this.isBossRift && this.totWave % 10 == 0;
			bool flag2 = flag && this.activeEnv.dontSpawnRegularEnemiesWithBoss;
			int i = (!flag2) ? this.GetNumEnemies(this.totWave) : 0;
			double num = this.GetEnemyPower(this.totWave) * 2.0;
			List<Enemy> list = new List<Enemy>();
			if (flag)
			{
				EnemyDataBase dataBase = this.activeEnv.enemiesBoss[GameMath.GetRandomInt(0, this.activeEnv.enemiesBoss.Count, GameMath.RandType.NoSeed)];
				i--;
				Enemy item = new Enemy(dataBase, num + 0.3, this.world);
				list.Add(item);
			}
			while (i > 0)
			{
				EnemyDataBase dataBase2 = this.activeEnv.enemiesRegular[GameMath.GetRandomInt(0, this.activeEnv.enemiesRegular.Count, GameMath.RandType.NoSeed)];
				i--;
				Enemy item2 = new Enemy(dataBase2, num, this.world);
				list.Add(item2);
			}
			int num2 = list.Count - 1;
			Vector3[] array = this.world.enemyPoses[num2];
			for (int j = list.Count - 1; j >= 0; j--)
			{
				Enemy enemy = list[j];
				enemy.extraSpawnSpeed = 2.2f;
				enemy.pos = ((!flag2) ? array[j] : this.world.enemyPoses[4][0]);
				Vector2 vector = GameMath.GetRandomPointInUnitCircle() * 0.05f;
				Enemy enemy2 = enemy;
				enemy2.pos.x = enemy2.pos.x + vector.x;
				Enemy enemy3 = enemy;
				enemy3.pos.y = enemy3.pos.y + vector.y;
				if (!enemy.IsBoss())
				{
					enemy.posSpawnStart = enemy.pos + new Vector3(0.75f, 0f);
				}
				else
				{
					enemy.posSpawnStart = enemy.pos;
				}
				enemy.posSpawnEnd = enemy.pos;
				enemy.pos = enemy.posSpawnStart;
				enemy.durSpawnNonexistent = 0.1f * (float)j;
			}
			foreach (Enemy item3 in list)
			{
				this.enemies.Add(item3);
			}
			base.OnNewEnemies();
		}

		public override bool HasWaveProgression()
		{
			return true;
		}

		public override int GetTotWave()
		{
			return this.totWave;
		}

		public override int GetStageNumber()
		{
			return -1;
		}

		public override int GetWaveNumber()
		{
			return this.totWave;
		}

		public override string GetWaveName()
		{
			return "Time Challenging Wave";
		}

		public override StringBuilder GetWaveProgress(StringBuilder stringBuilder)
		{
			return stringBuilder.Append(GameMath.GetMinInt(this.totWave, this.targetTotWaveNo).ToString()).Append("/").Append(this.targetTotWaveNo.ToString());
		}

		public override void LoadTotWave(int newTotWave)
		{
			this.totWave = GameMath.GetMaxInt(1, newTotWave);
			this.enemies.Clear();
			this.CreateNewEnemies();
		}

		public override void DEBUGchangeStage(int numStageChange)
		{
			this.enemies.Clear();
			this.totWave += numStageChange;
			if (this.totWave < 0)
			{
				this.totWave = 0;
			}
			this.CreateNewEnemies();
			for (int i = 0; i < numStageChange; i++)
			{
				this.OnNewWave();
			}
		}

		public bool HasEffectType<T>() where T : RiftEffect
		{
			foreach (RiftEffect riftEffect in this.riftEffects)
			{
				if (riftEffect is T)
				{
					return true;
				}
			}
			return false;
		}

		public void LoadCharm(CharmEffectData ceData)
		{
			ceData.isLoading = true;
			this.activeCharmEffects.Add(ceData);
		}

		public void LoadCurse(CurseEffectData curseEffectData)
		{
			curseEffectData.isLoading = true;
			this.activeCurseEffects.Add(curseEffectData);
		}

		public void ApplyAllEnchantmentEffects(List<int> loadSequencer)
		{
			int num = 0;
			int num2 = 0;
			foreach (int num3 in loadSequencer)
			{
				if (num3 == 0)
				{
					this.activeCharmEffects[num++].Apply(this);
				}
				else
				{
					if (num3 != 1)
					{
						throw new Exception();
					}
					this.activeCurseEffects[num2++].Apply(this);
				}
			}
			num = 0;
			num2 = 0;
			foreach (int num4 in loadSequencer)
			{
				if (num4 == 0)
				{
					this.activeCharmEffects[num++].isLoading = false;
				}
				else
				{
					if (num4 != 1)
					{
						throw new Exception();
					}
					this.activeCurseEffects[num2++].isLoading = false;
				}
			}
		}

		public void AddCharmEffect(CharmEffectData ceData)
		{
			ceData.Apply(this);
			this.activeCharmEffects.Add(ceData);
		}

		public void AddCharmBuff(CharmBuff buff)
		{
			buff.isLoaded = buff.enchantmentData.isLoading;
			buff.world = this.world;
			this.charmBuffs.Add(buff);
			this.allEnchantments.Add(buff);
		}

		public void AddCurseEffect(CurseEffectData curseEffectData)
		{
			curseEffectData.Apply(this);
			this.activeCurseEffects.Add(curseEffectData);
		}

		public void AddCurseBuff(CurseBuff curseBuff)
		{
			curseBuff.isLoaded = curseBuff.enchantmentData.isLoading;
			curseBuff.world = this.world;
			int num = this.curseBuffs.FindIndex((CurseBuff c) => c.enchantmentData == curseBuff.enchantmentData);
			if (num != -1)
			{
				curseBuff.progress = this.curseBuffs[num].progress;
				this.curseBuffs[num] = curseBuff;
			}
			else
			{
				this.curseBuffs.Add(curseBuff);
			}
			int num2 = this.allEnchantments.FindIndex((EnchantmentBuff c) => c.enchantmentData == curseBuff.enchantmentData);
			if (num2 != -1)
			{
				this.allEnchantments[num2] = curseBuff;
			}
			else
			{
				this.allEnchantments.Add(curseBuff);
			}
		}

		public void RemoveCurse(CurseBuff curseBuff, CurseEffectData curseEffectData)
		{
			this.curseBuffs.Remove(curseBuff);
			this.allEnchantments.Remove(curseBuff);
			this.activeCurseEffects.Remove(curseEffectData);
		}

		public void RemoveCurse(int index)
		{
			this.curseBuffs[index].state = EnchantmentBuffState.INACTIVE;
		}

		public void AddCharmMilestone(int wave)
		{
			this.allUpgrades.Add(new ChallangeChoseCharm(wave));
		}

		public void RecalculateCharmBuffStats()
		{
			List<float> list = new List<float>();
			foreach (CharmBuff charmBuff in this.charmBuffs)
			{
				list.Add(charmBuff.progress);
			}
			this.charmBuffs.Clear();
			this.allEnchantments.Clear();
			foreach (CurseBuff item in this.curseBuffs)
			{
				this.allEnchantments.Add(item);
			}
			foreach (CharmEffectData charmEffectData in this.activeCharmEffects)
			{
				charmEffectData.Apply(this);
			}
			for (int i = 0; i < this.charmBuffs.Count; i++)
			{
				this.charmBuffs[i].progress = list[i];
			}
		}

		public void ClaimCharmEffect(int index)
		{
			if (this.nextCharmDraftEffects != null)
			{
				int i = 0;
				int count = this.nextCharmDraftEffects.Count;
				while (i < count)
				{
					CharmEffectData charmEffectData = this.nextCharmDraftEffects[i];
					if (i == index)
					{
						this.AddCharmEffect(charmEffectData);
					}
					else
					{
						this.AddDiscardedCharm(charmEffectData.BaseData.id);
					}
					i++;
				}
				this.nextCharmDraftEffects = null;
			}
		}

		private void AddDiscardedCharm(int id)
		{
			this.discardedCharms.Add(id);
		}

		public void RemoveActiveCharmsAndBuffs()
		{
			this.activeCharmEffects.Clear();
			this.charmBuffs.Clear();
			foreach (CurseBuff item in this.curseBuffs)
			{
				this.allEnchantments.Add(item);
			}
		}

		protected override void OnNewWave()
		{
			base.OnNewWave();
			foreach (EnchantmentBuff enchantmentBuff in this.allEnchantments)
			{
				enchantmentBuff.OnWavePassed();
			}
			if (this.IsCursed())
			{
				this.curseProgress += this.riftData.cursesSetup.progressPerWave;
			}
		}

		public bool IsCursed()
		{
			return this.riftData.cursesSetup != null;
		}

		public void RefreshCurseLevels()
		{
			foreach (CurseEffectData curseEffectData in this.activeCurseEffects)
			{
				curseEffectData.Recalculate(this);
			}
		}

		public List<CharmBuff> GetCharmsThatAreNotAlwaysActive()
		{
			List<CharmBuff> list = new List<CharmBuff>();
			foreach (CharmBuff charmBuff in this.charmBuffs)
			{
				if (!(charmBuff is CharmBuffPermanent))
				{
					list.Add(charmBuff);
				}
			}
			return list;
		}

		public static float DISCARDED_CARD_SHOWING_UP_REDUCE_FACTOR = 0.3f;

		public static float CHOSEN_CARD_SHOWING_UP_REDUCE_FACTOR = 0.15f;

		public int targetTotWaveNo;

		public List<EnemyDataBase> regularEnemies;

		public List<RiftEffect> riftEffects;

		public int id;

		public RiftData riftData;

		public List<CharmEffectData> activeCharmEffects;

		public List<CurseEffectData> activeCurseEffects;

		public List<int> discardedCharms;

		public List<CharmEffectData> nextCharmDraftEffects;

		public List<CharmBuff> charmBuffs;

		public List<CurseBuff> curseBuffs;

		public List<EnchantmentBuff> allEnchantments;

		public double finishingRecord = double.PositiveInfinity;

		public int numCharmSelection;

		public int curseSpawnIndex;

		public double riftPointReward;

		public static int MINUTE = 60;

		public const float DUR_CHARM_SELECTION_ADD = 45f;

		public int totWave;

		public double baseEnemyPower = 500.0;

		public double incEnemyPowerPerWave = 0.10000000149011612;

		public int numEnemiesMin = 3;

		public int numEnemiesMax = 5;

		public int heroStartLevel = 20;

		public float charmSelectionAddTimer;

		public float firstTimeCharmSelectionDuration = 45f;

		public int firstTimeCharmForceWave = -1;

		public int discoveryIndex;

		public bool isBossRift;

		private bool earnedCharmSelectionBefore;

		private bool rewardGiven;

		public float curseProgress;
	}
}
