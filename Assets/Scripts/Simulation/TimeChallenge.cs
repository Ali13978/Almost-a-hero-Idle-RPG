using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Simulation
{
	public class TimeChallenge : ChallengeWithTime
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.hasRing = true;
			this.InitData();
		}

		public override void Reset()
		{
			base.Reset();
			this.totWave = 0;
		}

		private void InitData()
		{
			this.numHeroesMin = 2;
			this.numHeroesMax = 2;
			this.heroesCanRevive = true;
			this.buffDataReviveInvulnerability = new BuffDataInvulnerability();
			this.buffDataReviveInvulnerability.id = 110;
			this.buffDataReviveInvulnerability.dur = 3f;
			this.buffDataReviveInvulnerability.visuals |= 128;
			this.dur = 180f;
			this.numWaves = 15;
			this.totWave = 0;
			this.allUpgrades = new List<ChallengeUpgrade>();
		}

		protected override void SetEnvironments()
		{
			throw new NotSupportedException();
		}

		public override void OnSetupComplete(TotemDataBase totem, List<Rune> wornRunes, HeroDataBase[] heroesData, List<Gear> gears)
		{
			base.OnSetupComplete(totem, wornRunes, heroesData, gears);
		}

		public override void Update(float dt, float unwarpedDt)
		{
			base.Update(dt, unwarpedDt);
			if (this.state != Challenge.State.ACTION)
			{
				return;
			}
			if (!base.IsThereAliveEnemy())
			{
				this.totWave++;
				if (this.totWave <= this.numWaves)
				{
					this.CreateNewEnemies();
				}
			}
			if (this.totWave > this.numWaves)
			{
				this.state = Challenge.State.WON;
				this.world.OnChallengeWon();
			}
		}

		public override void Abandon()
		{
			base.Abandon();
			this.world.OnTimeChallengeNotCompleted(this.id);
		}

		protected override void OnChallengeLost()
		{
			base.OnChallengeLost();
			this.world.OnTimeChallengeNotCompleted(this.id);
		}

		public override bool HasTargetTotWave()
		{
			return true;
		}

		public override int GetTargetTotWave()
		{
			return this.numWaves;
		}

		protected virtual float GetSpawnPoints(int totWave)
		{
			return 1f + (float)totWave / 3f;
		}

		protected override double GetEnemyPower(int totWave)
		{
			double x = (double)totWave / (double)this.numWaves;
			double num = GameMath.PowDouble(x, this.ratPower);
			return this.minPower + (this.maxPower - this.minPower) * num;
		}

		private void CreateNewEnemies()
		{
			float num = this.GetSpawnPoints(this.totWave);
			double power = this.GetEnemyPower(this.totWave) * 2.0;
			List<EnemyDataBase> list = new List<EnemyDataBase>();
			foreach (EnemyDataBase item in this.activeEnv.enemiesRegular)
			{
				list.Add(item);
			}
			float num2 = 0f;
			foreach (EnemyDataBase enemyDataBase in list)
			{
				num2 += enemyDataBase.spawnProb;
			}
			List<Enemy> list2 = new List<Enemy>();
			while (list2.Count < 5 && list.Count > 0)
			{
				float num3 = GameMath.GetRandomFloat(0f, num2, GameMath.RandType.NoSeed);
				int i;
				for (i = 0; i < list.Count; i++)
				{
					num3 -= list[i].spawnProb;
					if (num3 < 0f)
					{
						break;
					}
				}
				EnemyDataBase enemyDataBase2 = list[i];
				if (num < enemyDataBase2.spawnWeight)
				{
					list.RemoveAt(i);
					num2 -= enemyDataBase2.spawnProb;
				}
				else
				{
					num -= enemyDataBase2.spawnWeight;
					Enemy item2 = new Enemy(enemyDataBase2, power, this.world);
					list2.Add(item2);
				}
			}
			int num4 = list2.Count - 1;
			Vector3[] array = this.world.enemyPoses[num4];
			for (int j = list2.Count - 1; j >= 0; j--)
			{
				Enemy enemy = list2[j];
				enemy.pos = array[j];
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
			foreach (Enemy item3 in list2)
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
			return stringBuilder.Append(GameMath.GetMinInt(this.totWave, this.numWaves).ToString()).Append("/").Append(this.numWaves.ToString());
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
		}

		public int numWaves;

		public int id;

		public static int MINUTE = 60;

		public double ratPower = 0.25;

		public double minPower;

		public double maxPower;

		public int totWave;
	}
}
