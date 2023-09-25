using System;
using System.Collections.Generic;
using System.Text;
using Static;
using UnityEngine;

namespace Simulation
{
	public abstract class Challenge
	{
		public double GetTotalGoldOfWave()
		{
			double num = 0.0;
			foreach (Enemy enemy in this.enemies)
			{
				num += enemy.GetTotalLootToDrop();
			}
			return num;
		}

		public virtual void Init(World world)
		{
			this.world = world;
			this.enemies = new List<Enemy>();
			this.totalGainedUpgrades = new ChallengeUpgradesTotal();
			this.totalGainedUpgrades.Init();
			this.SetEnvironments();
		}

		protected abstract void SetEnvironments();

		public virtual void Reset()
		{
			this.state = Challenge.State.SETUP;
		}

		public virtual void OnSetupComplete(TotemDataBase totem, List<Rune> wornRunes, HeroDataBase[] heroesData, List<Gear> gears)
		{
			this.state = Challenge.State.ACTION;
			this.world.visualEffects.Clear();
		}

		public abstract void Update(float dt, float unwarpedDt);

		public bool HasWon()
		{
			return this.state == Challenge.State.WON;
		}

		public bool HasLost()
		{
			return this.state == Challenge.State.LOST;
		}

		public virtual bool ShouldWaitToFinish()
		{
			return false;
		}

		public virtual bool AreRepeatedHeroesAllowed()
		{
			return false;
		}

		public abstract bool HasWaveProgression();

		public virtual int GetTotWave()
		{
			return -1;
		}

		public virtual int GetStageNumber()
		{
			return -1;
		}

		public virtual int GetWaveNumber()
		{
			return -1;
		}

		public virtual string GetWaveName()
		{
			return "$NONE$";
		}

		public virtual void LoadTotWave(int newTotWave)
		{
		}

		public abstract bool CanGoToBoss();

		public virtual void GoToBoss()
		{
		}

		public abstract bool CanLeaveBoss();

		public virtual void LeaveBoss()
		{
		}

		public virtual float GetBossTimeRatio()
		{
			return -1f;
		}

		public virtual float GetBossTimePassed()
		{
			return -1f;
		}

		public abstract bool HasTargetTotWave();

		public virtual int GetTargetTotWave()
		{
			return -1;
		}

		public abstract bool CanPrestigeNowExceptRainingGlory();

		public virtual int GetPrestigeReqStageNo()
		{
			return -1;
		}

		public virtual double GetNumMythstonesOnPrestige()
		{
			return -1.0;
		}

		public virtual double GetNumMythstonesOnPrestigePure()
		{
			return -1.0;
		}

		public virtual double GetNumMythstonesOnPrestigeArtifactBonus()
		{
			return -1.0;
		}

		public string GetWaveProgress()
		{
			return this.GetWaveProgress(StringExtension.StringBuilder).ToString();
		}

		public abstract StringBuilder GetWaveProgress(StringBuilder stringBuilder);

		public virtual void Prestige()
		{
			this.state = Challenge.State.SETUP;
			this.totalGainedUpgrades.Init();
			TutorialManager.OnPrestige();
		}

		public void SpawnOneMinion(EnemyDataBase minionData, int targetMinionsCount, int minionIndex, BuffData spawnBuff = null)
		{
			double enemyPower = this.GetEnemyPower(this.GetTotWave());
			Enemy enemy = new Enemy(minionData, enemyPower, this.world);
			this.enemies.Add(enemy);
			Vector3[] array = this.world.enemyPoses[targetMinionsCount];
			enemy.extraSpawnSpeed = 2.2f;
			enemy.pos = array[minionIndex];
			Vector2 vector = GameMath.GetRandomPointInUnitCircle() * 0.05f;
			Enemy enemy2 = enemy;
			enemy2.pos.x = enemy2.pos.x + vector.x;
			Enemy enemy3 = enemy;
			enemy3.pos.y = enemy3.pos.y + vector.y;
			enemy.posSpawnStart = enemy.pos;
			enemy.posSpawnEnd = enemy.pos;
			enemy.pos = enemy.posSpawnStart;
			enemy.durSpawnNonexistent = 0f;
			if (spawnBuff != null)
			{
				enemy.AddBuff(spawnBuff, 0, false);
			}
			this.OnNewEnemies();
		}

		protected abstract double GetEnemyPower(int totalWave);

		public virtual void DEBUGreset()
		{
		}

		public virtual void DEBUGchangeStage(int numStageChange)
		{
		}

		protected void UpdateEnemies(float dt)
		{
			int num = this.enemies.Count;
			for (int i = num - 1; i >= 0; i--)
			{
				Enemy enemy = this.enemies[i];
				enemy.Update(dt, 0);
				if (enemy.IsToBeRemoved())
				{
					this.enemies[i] = this.enemies[--num];
					this.enemies.RemoveAt(num);
				}
			}
		}

		protected bool IsThereAliveEnemy()
		{
			foreach (Enemy enemy in this.enemies)
			{
				if (enemy.IsAlive())
				{
					return true;
				}
			}
			return false;
		}

		protected void OnNewEnemies()
		{
			this.world.UpdateUnitStats(0f);
			this.waveTotHealthMax = 0.0;
			foreach (Enemy enemy in this.enemies)
			{
				if (!enemy.IsDead())
				{
					this.waveTotHealthMax += enemy.GetHealthMax();
				}
			}
			if (this.world.totem != null)
			{
				this.world.totem.OnNewEnemies();
			}
			foreach (Hero hero in this.world.heroes)
			{
				hero.OnNewEnemies();
			}
			if (this.world.blizzardTimeLeft > 0f)
			{
				this.world.AddBlizzardBuffToEnemies(this.world.blizzardTimeLeft);
			}
		}

		protected virtual void OnNewWave()
		{
			if (this.world.totem != null)
			{
				this.world.totem.OnNewWave();
			}
			foreach (Hero hero in this.world.heroes)
			{
				hero.OnNewWave();
			}
		}

		protected void OnNewStage()
		{
			this.world.dailyQuestPassStageCounter++;
			if (this.world.totem != null)
			{
				this.world.totem.OnNewStage();
			}
			foreach (Hero hero in this.world.heroes)
			{
				hero.OnNewStage();
			}
		}

		public double GetSkipGoldBounty(int currentStage, int skipCount)
		{
			return 0.0;
		}

		public double GetEpicSkipMythstoneBounty(int skippedWave)
		{
			return 0.0;
		}

		protected const float FAST_SPAWN_RATIO = 3.2f;

		protected const float NORMAL_SPAWN_RATIO = 1.95f;

		public Challenge.State state;

		public World world;

		public bool doesUpdateInBg;

		public bool hasRing;

		public int numHeroesMin;

		public int numHeroesMax;

		public bool heroesCanRevive;

		public BuffDataInvulnerability buffDataReviveInvulnerability;

		public List<Environment> allEnvironments;

		public Environment activeEnv;

		public List<Enemy> enemies;

		public double waveTotHealthMax;

		public List<ChallengeUpgrade> allUpgrades;

		public ChallengeUpgradesTotal totalGainedUpgrades;

		public enum State
		{
			SETUP,
			ACTION,
			WON,
			LOST
		}
	}
}
