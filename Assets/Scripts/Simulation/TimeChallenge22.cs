using System;
using System.Collections.Generic;

namespace Simulation
{
	public class TimeChallenge22 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 21;
			this.numDuplicatesTotal = 4;
			this.numHeroesMin = 2;
			this.numHeroesMax = 2;
			this.hasRing = false;
			this.ratPower = 0.3;
			this.minPower = -70.0;
			this.maxPower = 160.0;
			this.numWaves = 10;
			this.dur = (float)(20 * TimeChallenge.MINUTE);
			for (int i = 0; i < 5; i++)
			{
				ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints = new ChallengeUpgradeSkillPoints(3);
				challengeUpgradeSkillPoints.waveReq = i * 2;
				challengeUpgradeSkillPoints.cost = 1000000.0 * GameMath.PowInt(3.0, i * 3);
				this.allUpgrades.Add(challengeUpgradeSkillPoints);
			}
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateElfSemiCorrupted();
			EnemyDataBase enemyDataBase2 = EnemyFactory.CreateElfCorrupted();
			EnemyDataBase enemyDataBase3 = EnemyFactory.CreateDwarfSemiCorrupted();
			EnemyDataBase enemyDataBase4 = EnemyFactory.CreateDwarfCorrupted();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase2.spawnWeight = 1f;
			enemyDataBase3.spawnWeight = 1f;
			enemyDataBase4.spawnWeight = 1f;
			enemyDataBase.damage *= 1E-06;
			enemyDataBase2.damage *= 1E-06;
			enemyDataBase3.damage *= 1E-06;
			enemyDataBase4.damage *= 1E-06;
			Environment item = new Environment(Bg.TRAINING_GROUND, new List<EnemyDataBase>
			{
				enemyDataBase,
				enemyDataBase2,
				enemyDataBase3,
				enemyDataBase4
			}, null, null, null, false);
			this.allEnvironments = new List<Environment>
			{
				item
			};
			this.activeEnv = this.allEnvironments[0];
		}

		protected override float GetSpawnPoints(int totWave)
		{
			return 3f;
		}
	}
}
