using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Simulation
{
	public class ChallengeStandard : Challenge
	{
		public static int GetStageNo(int totWave)
		{
			return totWave / 11;
		}

		public static int GetWaveNo(int totWave)
		{
			return totWave % 11;
		}

		public static bool IsBossWave(int totWave)
		{
			return ChallengeStandard.GetWaveNo(totWave) == 0;
		}

		public static bool IsBossEpicWave(int totWave)
		{
			return ChallengeStandard.IsBossWave(totWave) && ChallengeStandard.GetStageNo(totWave) % 5 == 0;
		}

		public static bool IsLastWaveBeforeBoss(int totWave)
		{
			return ChallengeStandard.IsBossWave(totWave + 1);
		}

		public static bool IsFirstWaveAfterBoss(int totWave)
		{
			return ChallengeStandard.IsBossWave(totWave - 1);
		}

		public override void Init(World world)
		{
			base.Init(world);
			this.doesUpdateInBg = true;
			this.hasRing = true;
			this.numHeroesMin = 0;
			this.numHeroesMax = 5;
			this.heroesCanRevive = true;
			this.buffDataReviveInvulnerability = new BuffDataInvulnerability();
			this.buffDataReviveInvulnerability.id = 114;
			this.buffDataReviveInvulnerability.dur = 1f;
			this.buffDataReviveInvulnerability.visuals |= 128;
			this.minTotWaveForPrestige = 881;
			this.durBossBattle = 30f;
			this.InitData();
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase item = EnemyFactory.CreateRegularBandit();
			EnemyDataBase item2 = EnemyFactory.CreateHumanCorrupted();
			EnemyDataBase item3 = EnemyFactory.CreateHumanSemiCorrupted();
			EnemyDataBase item4 = EnemyFactory.CreateBossHuman();
			EnemyDataBase item5 = EnemyFactory.CreateEpicBossOrc();
			EnemyDataBase item6 = EnemyFactory.CreateRegularWolf();
			EnemyDataBase item7 = EnemyFactory.CreateElfSemiCorrupted();
			EnemyDataBase item8 = EnemyFactory.CreateElfCorrupted();
			EnemyDataBase item9 = EnemyFactory.CreateBossElf();
			EnemyDataBase item10 = EnemyFactory.CreateDwarfSemiCorrupted();
			EnemyDataBase item11 = EnemyFactory.CreateDwarfCorrupted();
			EnemyDataBase item12 = EnemyFactory.CreateBossDwarf();
			EnemyDataBase item13 = EnemyFactory.CreateRegularSpider();
			EnemyDataBase item14 = EnemyFactory.CreateRegularBat();
			EnemyDataBase item15 = EnemyFactory.CreateRegularMagolies();
			EnemyDataBase item16 = EnemyFactory.CreateBossOrc();
			EnemyDataBase item17 = EnemyFactory.CreateEpicBossMagolis();
			EnemyDataBase enemyChest = EnemyFactory.CreateChest();
			Environment item18 = new Environment(Bg.VILLAGE, new List<EnemyDataBase>
			{
				item3,
				item2,
				item
			}, new List<EnemyDataBase>
			{
				item4
			}, new List<EnemyDataBase>
			{
				item5
			}, enemyChest, false);
			Environment item19 = new Environment(Bg.FOREST, new List<EnemyDataBase>
			{
				item8,
				item7,
				item6
			}, new List<EnemyDataBase>
			{
				item9
			}, new List<EnemyDataBase>
			{
				item5
			}, enemyChest, false);
			Environment item20 = new Environment(Bg.DEAD_LAND, new List<EnemyDataBase>
			{
				item10,
				item11
			}, new List<EnemyDataBase>
			{
				item12
			}, new List<EnemyDataBase>
			{
				item17
			}, enemyChest, false);
			Environment item21 = new Environment(Bg.LAVA, new List<EnemyDataBase>
			{
				item13,
				item14,
				item15
			}, new List<EnemyDataBase>
			{
				item16,
				item4,
				item12,
				item9
			}, new List<EnemyDataBase>
			{
				item17
			}, enemyChest, false);
			Environment item22 = new Environment(Bg.DESERT, new List<EnemyDataBase>
			{
				item3,
				item2,
				item
			}, new List<EnemyDataBase>
			{
				item4
			}, new List<EnemyDataBase>
			{
				item5
			}, enemyChest, false);
			Environment item23 = new Environment(Bg.DUNGEON, new List<EnemyDataBase>
			{
				item10,
				item11
			}, new List<EnemyDataBase>
			{
				item12
			}, new List<EnemyDataBase>
			{
				item17
			}, enemyChest, false);
			this.allEnvironments = new List<Environment>
			{
				item18,
				item19,
				item20,
				item23,
				item22,
				item21
			};
			this.activeEnv = this.allEnvironments[0];
		}

		public override void Reset()
		{
			base.Reset();
			this.totWave = 0;
			this.timeCounterWithoutAliveEnemy = 0f;
			this.timeLeftBoss = 0f;
			this.hasComeFromBoss = false;
			this.waveTotHealthMax = 0.0;
			this.enemies.Clear();
			int index = ChallengeStandard.GetStageNo(this.totWave - 1) / 30 % this.allEnvironments.Count;
			this.activeEnv = this.allEnvironments[index];
		}

		private void InitData()
		{
			this.buffDataReviveInvulnerability = new BuffDataInvulnerability();
			this.buffDataReviveInvulnerability.id = 115;
			this.buffDataReviveInvulnerability.dur = 5f;
			this.buffDataReviveInvulnerability.visuals |= 128;
			int num = 7;
			this.allUpgrades = new List<ChallengeUpgrade>();
			for (int i = 0; i < 100; i++)
			{
				this.allUpgrades.Add(new ChallengeUpgradeDamageTotem(2.0));
				this.allUpgrades.Add(new ChallengeUpgradeGold(2.0));
				this.allUpgrades.Add(new ChallengeUpgradeDamage(2.0));
				this.allUpgrades.Add(new ChallengeUpgradeHealth(2.0));
				if (i < num)
				{
					this.allUpgrades.Add(new ChallengeUpgradeSkillPoints(2));
				}
			}
			int j = 0;
			int count = this.allUpgrades.Count;
			while (j < count)
			{
				ChallengeUpgrade challengeUpgrade = this.allUpgrades[j];
				challengeUpgrade.stageReq = (j + 1) * 10;
				challengeUpgrade.cost = 50.0 * GameMath.PowInt(1.185, challengeUpgrade.stageReq);
				j++;
			}
		}

		public override void OnSetupComplete(TotemDataBase totem, List<Rune> wornRunes, HeroDataBase[] heroesData, List<Gear> gears)
		{
			base.OnSetupComplete(totem, wornRunes, heroesData, gears);
		}

		public override void Update(float dt, float dtUnwarp)
		{
			this.stageFinishTimer += dt;
			if (this.state == Challenge.State.ACTION && this.world.universalBonus.stagesToJumpInAdventure > this.GetStageNumber())
			{
				double totalGoldOfWave;
				while (this.world.universalBonus.stagesToJumpInAdventure > this.GetStageNumber())
				{
					totalGoldOfWave = base.GetTotalGoldOfWave();
					this.enemies.Clear();
					this.AdvanceWave(false);
				}
				totalGoldOfWave = base.GetTotalGoldOfWave();
				this.enemies.Clear();
				this.AdvanceWave(false);
				this.world.RainGold(totalGoldOfWave);
			}
			base.UpdateEnemies(dt);
			this.timeSinceLastSnowmanBoss += dtUnwarp;
			this.goToBossCooldown -= dt;
			if (this.chestCandyDropTimer < 10f)
			{
				this.chestCandyDropTimer += dtUnwarp;
				if (this.chestCandyDropTimer >= 10f)
				{
					this.chestsThatHadDroppedLoot = 0;
				}
			}
			if (base.IsThereAliveEnemy())
			{
				if (this.IsThereAliveNonleavingBoss())
				{
					this.timeLeftBoss -= dt;
					if (this.timeLeftBoss <= 0f)
					{
						this.LeaveBoss();
					}
				}
				else
				{
					this.timeLeftBoss = 0f;
				}
			}
			else
			{
				this.timeLeftBoss = 0f;
				if (!this.world.isRainingGlory && !TutorialManager.IsPaused())
				{
					if (this.world.currentSim.numPrestiges > 2)
					{
						this.timeCounterWithoutAliveEnemy += 3.2f * dt;
					}
					else
					{
						this.timeCounterWithoutAliveEnemy += 1.95f * dt;
					}
					if (ChallengeStandard.IsBossWave(this.totWave))
					{
						if (this.timeCounterWithoutAliveEnemy >= 0.6f || TutorialManager.ShouldEnemiesSpawnWithoutWaiting())
						{
							this.timeCounterWithoutAliveEnemy = 0f;
							this.justFinishedBossWave = true;
							this.AdvanceWave(true);
							this.justFinishedBossWave = false;
						}
					}
					else if (this.timeCounterWithoutAliveEnemy >= 0.1f || TutorialManager.ShouldEnemiesSpawnWithoutWaiting())
					{
						this.timeCounterWithoutAliveEnemy = 0f;
						this.AdvanceWave(true);
						if (!TutorialManager.ShouldIncrementTotWave())
						{
							this.totWave = 1;
						}
						if (TutorialManager.first == TutorialManager.First.FIGHT_HERO && this.totWave < 10)
						{
							this.totWave = 10;
						}
					}
				}
			}
		}

		private void AdvanceWave(bool triggerEvents)
		{
			if (!this.hasComeFromBoss)
			{
				int num = this.totWave;
				if (this.world.canProgressStage)
				{
					this.totWave++;
					if (triggerEvents)
					{
						if (ChallengeStandard.IsBossWave(this.totWave - 1))
						{
							LevelSkipPairs levelSkipPairs = this.world.GetLevelSkipPairs();
							if (levelSkipPairs != null)
							{
								int skipCountForRecord = levelSkipPairs.GetSkipCountForRecord(this.stageFinishTimer, this.GetStageNumber());
								if (skipCountForRecord > 0)
								{
									double num2 = 0.0;
									for (int i = 0; i < skipCountForRecord; i++)
									{
										int skippedWave = this.totWave - 1 + 11 * (i + 1);
										if (ChallengeStandard.IsBossEpicWave(skippedWave))
										{
											num2 += base.GetEpicSkipMythstoneBounty(skippedWave);
										}
									}
									double skipGoldBounty = base.GetSkipGoldBounty(ChallengeStandard.GetStageNo(this.totWave), skipCountForRecord);
									this.world.SetSkippedStage(skipCountForRecord);
									this.totWave += 11 * skipCountForRecord;
									this.world.dailyQuestPassStageCounter += skipCountForRecord;
									if (skipGoldBounty > 0.0)
									{
										this.world.RainGold(skipGoldBounty);
									}
									if (num2 > 0.0)
									{
										this.world.RainMythstone(num2);
									}
								}
							}
						}
						if (!ChallengeStandard.IsBossWave(this.totWave) && GameMath.GetProbabilityOutcome(this.world.GetWaveSkipChance(), GameMath.RandType.NoSeed))
						{
							this.totWave++;
						}
					}
				}
				if (this.allEnvironments.Count > 1)
				{
					int num3 = ChallengeStandard.GetStageNo(num - 1) / 30 % this.allEnvironments.Count;
					int num4 = ChallengeStandard.GetStageNo(this.totWave - 1) / 30 % this.allEnvironments.Count;
					if (num4 != num3)
					{
						this.activeEnv = this.allEnvironments[num4];
						this.world.OnEnvironmentIsToChange();
					}
				}
			}
			this.CreateNewEnemies();
			if (triggerEvents)
			{
				this.OnNewWave();
				if (this.justFinishedBossWave)
				{
					base.OnNewStage();
					this.stageFinishTimer = 0f;
					int stageNumber = this.GetStageNumber();
					if (stageNumber > this.world.currentSim.maxStagePrestigedAt && (stageNumber == 20 || stageNumber == 30 || stageNumber == 50 || stageNumber == 70 || stageNumber == 100))
					{
						UnityEngine.Debug.Log("Stage reached " + stageNumber);
						AdjustTracker.TrackStageReachedEvent(stageNumber);
						
					}
				}
				if (ChallengeStandard.IsFirstWaveAfterBoss(this.totWave))
				{
					this.world.shouldSoftSave = true;
				}
			}
		}

		private void CreateNewEnemies()
		{
			if (this.totWave <= 0)
			{
				this.totWave = 1;
			}
			int stageNo = ChallengeStandard.GetStageNo(this.totWave);
			float num = 1.8f + (float)(stageNo % 20) * 0.5f;
			List<Enemy> list = new List<Enemy>();
			if (ChallengeStandard.IsBossWave(this.totWave))
			{
				this.timeLeftBoss = this.GetBossDuration();
				double mythstone = 0.0;
				EnemyDataBase enemyDataBase;
				if (ChallengeStandard.IsBossEpicWave(this.totWave))
				{
					int randomInt = GameMath.GetRandomInt(0, this.activeEnv.enemiesEpic.Count, GameMath.RandType.NoSeed);
					enemyDataBase = this.activeEnv.enemiesEpic[randomInt];
					if (this.world.currentSim.numPrestiges > 0)
					{
						mythstone = ((TutorialManager.artifactsTab != TutorialManager.ArtifactsTab.BEFORE_BEGIN) ? this.world.GetEpicBossMythstoneDrop(stageNo) : this.world.currentSim.artifactsManager.GetCraftCost());
					}
				}
				else
				{
					this.cachedBossCandidates.Clear();
					for (int i = this.activeEnv.enemiesBoss.Count - 1; i >= 0; i--)
					{
						if (this.activeEnv.enemiesBoss[i].name != "BOSS SNOWMAN" || this.timeSinceLastSnowmanBoss >= 180f)
						{
							this.cachedBossCandidates.Add(this.activeEnv.enemiesBoss[i]);
						}
					}
					enemyDataBase = this.cachedBossCandidates.GetRandomListElement<EnemyDataBase>();
					if (enemyDataBase.name == "BOSS SNOWMAN")
					{
						this.timeSinceLastSnowmanBoss = 0f;
					}
				}
				num -= enemyDataBase.spawnWeight;
				this.bossName = enemyDataBase.name;
				int num2 = stageNo;
				Enemy enemy = new Enemy(enemyDataBase, (double)num2, this.world);
				enemy.mythstone = mythstone;
				list.Add(enemy);
				enemy.pos = new Vector3(0.7f, 0f, 0f);
			}
			else if (this.activeEnv.enemyChest != null && this.justFinishedBossWave && stageNo > 1 && GameMath.GetProbabilityOutcome(this.GetRaidChestChance(), GameMath.RandType.NoSeed) && TutorialManager.first == TutorialManager.First.FIN)
			{
				double power = (double)stageNo;
				while (list.Count < 5)
				{
					list.Add(new Enemy(this.activeEnv.enemyChest, power, this.world)
					{
						isChestRaid = true,
						canDropCandies = (this.chestsThatHadDroppedLoot++ < 5),
						doNotPlaySpawnSound = (list.Count < 2)
					});
				}
				num = 0f;
			}
			else if (this.activeEnv.enemyChest != null && GameMath.GetProbabilityOutcome(this.GetChestChance(), GameMath.RandType.NoSeed) && TutorialManager.first == TutorialManager.First.FIN)
			{
				double power2 = (double)stageNo;
				Enemy enemy2 = new Enemy(this.activeEnv.enemyChest, power2, this.world);
				list.Add(enemy2);
				num = 0f;
				enemy2.canDropCandies = (this.chestsThatHadDroppedLoot++ < 5);
			}
			List<EnemyDataBase> list2 = new List<EnemyDataBase>();
			foreach (EnemyDataBase item in this.activeEnv.enemiesRegular)
			{
				list2.Add(item);
			}
			float num3 = 0f;
			foreach (EnemyDataBase enemyDataBase2 in list2)
			{
				num3 += enemyDataBase2.spawnProb;
			}
			double power3 = (double)stageNo;
			while (list.Count < 5 && list2.Count > 0)
			{
				float num4 = GameMath.GetRandomFloat(0f, num3, GameMath.RandType.NoSeed);
				int j;
				for (j = 0; j < list2.Count; j++)
				{
					num4 -= list2[j].spawnProb;
					if (num4 < 0f)
					{
						break;
					}
				}
				if (j >= list2.Count)
				{
					j = list2.Count - 1;
				}
				EnemyDataBase enemyDataBase3 = list2[j];
				if (num < enemyDataBase3.spawnWeight)
				{
					list2.RemoveAt(j);
					num3 -= enemyDataBase3.spawnProb;
				}
				else
				{
					num -= enemyDataBase3.spawnWeight;
					Enemy item2 = new Enemy(enemyDataBase3, power3, this.world);
					list.Add(item2);
				}
			}
			int num5 = list.Count - 1;
			Vector3[] array = (num5 < 0) ? null : this.world.enemyPoses[num5];
			for (int k = list.Count - 1; k >= 0; k--)
			{
				Enemy enemy3 = list[k];
				enemy3.pos = array[k];
				Vector2 vector = GameMath.GetRandomPointInUnitCircle() * 0.05f;
				Enemy enemy4 = enemy3;
				enemy4.pos.x = enemy4.pos.x + vector.x;
				Enemy enemy5 = enemy3;
				enemy5.pos.y = enemy5.pos.y + vector.y;
				if (this.world.currentSim.numPrestiges > 2)
				{
					enemy3.extraSpawnSpeed = 3.2f;
				}
				else
				{
					enemy3.extraSpawnSpeed = 1.95f;
				}
				if (!enemy3.IsBoss())
				{
					enemy3.posSpawnStart = enemy3.pos + new Vector3(0.75f, 0f);
				}
				else
				{
					enemy3.posSpawnStart = enemy3.pos;
				}
				enemy3.posSpawnEnd = enemy3.pos;
				enemy3.pos = enemy3.posSpawnStart;
				enemy3.durSpawnNonexistent = 0.1f * (float)k;
			}
			foreach (Enemy item3 in list)
			{
				this.enemies.Add(item3);
			}
			base.OnNewEnemies();
		}

		public bool IsThereAliveNonleavingBoss()
		{
			foreach (Enemy enemy in this.enemies)
			{
				if (enemy.IsBoss() && enemy.IsAlive() && !enemy.IsLeaving())
				{
					return true;
				}
			}
			return false;
		}

		public bool IsThereAnyBoss()
		{
			foreach (Enemy enemy in this.enemies)
			{
				if (enemy.IsBoss())
				{
					return true;
				}
			}
			return false;
		}

		private Enemy GetBoss()
		{
			foreach (Enemy enemy in this.enemies)
			{
				if (enemy.IsBoss())
				{
					return enemy;
				}
			}
			throw new EntryPointNotFoundException();
		}

		public bool HasBoss()
		{
			foreach (Enemy enemy in this.enemies)
			{
				if (enemy.IsBoss())
				{
					return true;
				}
			}
			return false;
		}

		public bool HasEpicBoss()
		{
			if (!ChallengeStandard.IsBossEpicWave(this.totWave))
			{
				return false;
			}
			foreach (Enemy enemy in this.enemies)
			{
				if (enemy.IsBoss())
				{
					return true;
				}
			}
			return false;
		}

		public float GetBossDuration()
		{
			return this.durBossBattle + this.world.universalBonus.bossTimeAdd;
		}

		public override float GetBossTimePassed()
		{
			return this.GetBossDuration() - this.timeLeftBoss;
		}

		public override float GetBossTimeRatio()
		{
			return this.timeLeftBoss / this.GetBossDuration();
		}

		public float GetRaidChestChance()
		{
			if (this.world.raidChestDisabled)
			{
				return 0f;
			}
			return this.world.universalBonus.treasureRaidChance;
		}

		public float GetChestChance()
		{
			float num = PlayfabManager.titleData.goblinChestAppear;
			num *= this.world.universalBonus.chestChanceFactor;
			return num * (1f + this.world.buffTotalEffect.chestChanceAdd);
		}

		public override bool CanPrestigeNowExceptRainingGlory()
		{
			return this.totWave >= this.minTotWaveForPrestige;
		}

		public override double GetNumMythstonesOnPrestige()
		{
			return this.GetNumMythstonesOnPrestigePure() * this.world.universalBonus.prestigeMythFactor;
		}

		public override double GetNumMythstonesOnPrestigePure()
		{
			int num = ChallengeStandard.GetStageNo(this.totWave);
			if (ChallengeStandard.IsBossWave(this.totWave))
			{
				num--;
			}
			return Math.Floor((double)num * GameMath.PowDouble(1.03, (double)(num - 80) * 0.5));
		}

		public override double GetNumMythstonesOnPrestigeArtifactBonus()
		{
			return this.GetNumMythstonesOnPrestige() - this.GetNumMythstonesOnPrestigePure();
		}

		public override void Prestige()
		{
			base.Prestige();
			this.enemies.Clear();
			this.totWave = 0;
		}

		public override bool CanGoToBoss()
		{
			return !this.IsThereAliveNonleavingBoss() && this.hasComeFromBoss && this.goToBossCooldown <= 0f;
		}

		public override void GoToBoss()
		{
			this.enemies.Clear();
			this.hasComeFromBoss = false;
			this.goToBossCooldown = 10f;
			this.totWave++;
			this.CreateNewEnemies();
			TutorialManager.PressedFightBossButton();
		}

		public override bool CanLeaveBoss()
		{
			return ChallengeStandard.IsBossWave(this.totWave);
		}

		public override void LeaveBoss()
		{
			Enemy boss = this.GetBoss();
			boss.Leave();
			this.totWave--;
			this.hasComeFromBoss = true;
			SoundEventSound e = new SoundEventSound(SoundType.UI, string.Empty, false, 0f, new SoundDelayed(0.1f, SoundArchieve.inst.uiLeaveBoss, 1f));
			this.world.AddSoundEvent(e);
		}

		public override bool HasTargetTotWave()
		{
			return false;
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
			return ChallengeStandard.GetStageNo(this.totWave);
		}

		public override int GetWaveNumber()
		{
			return ChallengeStandard.GetWaveNo(this.totWave);
		}

		public override StringBuilder GetWaveProgress(StringBuilder stringBuilder)
		{
			if (ChallengeStandard.IsBossWave(this.totWave) || ChallengeStandard.IsBossEpicWave(this.totWave))
			{
				return stringBuilder.Append(LM.Get("UI_BOSS"));
			}
			return stringBuilder.Append(ChallengeStandard.GetWaveNo(this.totWave).ToString()).Append("/").Append(10.ToString());
		}

		public override string GetWaveName()
		{
			if (ChallengeStandard.IsBossEpicWave(this.totWave))
			{
				return LM.Get("UI_BOSS_EPIC");
			}
			if (ChallengeStandard.IsBossWave(this.totWave))
			{
				return LM.Get("UI_BOSS");
			}
			int waveNo = ChallengeStandard.GetWaveNo(this.totWave);
			return string.Format(LM.Get("UI_WAVE_X"), waveNo.ToString(), 10);
		}

		public override void LoadTotWave(int newTotWave)
		{
			this.enemies.Clear();
			this.totWave = newTotWave;
			int index = ChallengeStandard.GetStageNo(this.totWave - 1) / 30 % this.allEnvironments.Count;
			this.activeEnv = this.allEnvironments[index];
			this.timeLeftBoss = 0f;
			this.hasComeFromBoss = false;
			if (!TutorialManager.IsPaused())
			{
				this.CreateNewEnemies();
			}
		}

		public override void DEBUGreset()
		{
			this.enemies.Clear();
			this.totWave = 0;
			this.timeCounterWithoutAliveEnemy = 0f;
			this.timeLeftBoss = 0f;
			this.hasComeFromBoss = false;
		}

		public override void DEBUGchangeStage(int numStageChange)
		{
			this.enemies.Clear();
			this.totWave += numStageChange;
			if (this.totWave < 0)
			{
				this.totWave = 0;
			}
			int index = ChallengeStandard.GetStageNo(this.totWave - 1) / 30 % this.allEnvironments.Count;
			this.activeEnv = this.allEnvironments[index];
			this.timeLeftBoss = 0f;
			this.hasComeFromBoss = false;
			this.CreateNewEnemies();
		}

		public override int GetPrestigeReqStageNo()
		{
			return ChallengeStandard.GetStageNo(this.minTotWaveForPrestige);
		}

		public void DecreaseTimeLeftBoss(float timeDecrement)
		{
			this.timeLeftBoss -= timeDecrement;
		}

		protected override double GetEnemyPower(int totalWave)
		{
			return (double)ChallengeStandard.GetStageNo(totalWave);
		}

		public const int NUM_STAGES_REQUIRED_FOR_PRESTIGE = 80;

		public const int NUM_STAGES_PER_ENV_CHANGE = 30;

		private const float BOSS_SNOWMAN_PERIOD = 180f;

		public const float CHEST_CANDY_DROP_PERIOD = 10f;

		private const int CHEST_CANDY_DROP_AMOUNT = 5;

		public const int NUM_WAVES_PER_STAGE = 11;

		private const float DUR_BEFORE_ENEMY_SPAWN_WAVE = 0.1f;

		private const float DUR_BEFORE_ENEMY_SPAWN_BOSS = 0.5f;

		private const float GO_TO_BOSS_COOLDOWN_MAX = 10f;

		public int minTotWaveForPrestige;

		public float durBossBattle;

		public int totWave;

		private float timeCounterWithoutAliveEnemy;

		private float timeLeftBoss;

		private bool hasComeFromBoss;

		private float goToBossCooldown;

		private float timeSinceLastSnowmanBoss;

		public string bossName;

		private bool justFinishedBossWave;

		private int chestsThatHadDroppedLoot = 5;

		public float chestCandyDropTimer;

		private float stageFinishTimer;

		private List<EnemyDataBase> cachedBossCandidates = new List<EnemyDataBase>();
	}
}
