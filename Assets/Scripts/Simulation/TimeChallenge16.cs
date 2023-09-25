using System;
using System.Collections.Generic;

namespace Simulation
{
	public class TimeChallenge16 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 15;
			this.numHeroesMin = 4;
			this.numHeroesMax = 4;
			this.hasRing = true;
			this.ratPower = 0.25;
			this.minPower = -70.0;
			this.maxPower = 110.0;
			this.numWaves = 10;
			this.dur = (float)(10 * TimeChallenge.MINUTE);
			this.allUpgrades = new List<ChallengeUpgrade>();
			for (int i = 0; i < 30; i++)
			{
				ChallengeUpgradeHealth challengeUpgradeHealth = new ChallengeUpgradeHealth(1.3);
				challengeUpgradeHealth.waveReq = 1;
				if (i > 0)
				{
					challengeUpgradeHealth.cost = this.allUpgrades[i - 1].cost * 2.5;
				}
				else
				{
					challengeUpgradeHealth.cost = 3000.0;
				}
				this.allUpgrades.Add(challengeUpgradeHealth);
			}
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateHumanSemiCorrupted();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.healthMax *= 1.0;
			enemyDataBase.damage *= 0.5;
			enemyDataBase.goldToDrop *= 0.01;
			enemyDataBase.durSpawnTranslate = 5f;
			Environment item = new Environment(Bg.TRAINING_GROUND, new List<EnemyDataBase>
			{
				enemyDataBase
			}, null, null, null, false);
			this.allEnvironments = new List<Environment>
			{
				item
			};
			this.activeEnv = this.allEnvironments[0];
		}

		protected override float GetSpawnPoints(int totWave)
		{
			return 1f + (float)(totWave % 8) * 0.6f;
		}
	}
}
