using System;
using System.Collections.Generic;

namespace Simulation
{
	public class TimeChallenge20 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 19;
			this.numHeroesMin = 3;
			this.numHeroesMax = 3;
			this.hasRing = true;
			this.ratPower = 0.28;
			this.minPower = -20.0;
			this.maxPower = 160.0;
			this.numWaves = 60;
			this.dur = (float)(15 * TimeChallenge.MINUTE);
			for (int i = 0; i < 15; i++)
			{
				ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints = new ChallengeUpgradeSkillPoints(2);
				challengeUpgradeSkillPoints.waveReq = i * 2;
				if (i == 0)
				{
					challengeUpgradeSkillPoints.cost = 100000.0;
				}
				else
				{
					challengeUpgradeSkillPoints.cost = this.allUpgrades[i - 1].cost * 6.0;
				}
				this.allUpgrades.Add(challengeUpgradeSkillPoints);
			}
			for (int j = 15; j < 25; j++)
			{
				if (j % 2 == 0)
				{
					ChallengeUpgradeDamage challengeUpgradeDamage = new ChallengeUpgradeDamage(1.3);
					challengeUpgradeDamage.waveReq = j * 2;
					challengeUpgradeDamage.cost = this.allUpgrades[j - 1].cost * 6.0;
					this.allUpgrades.Add(challengeUpgradeDamage);
				}
				else
				{
					ChallengeUpgradeHealth challengeUpgradeHealth = new ChallengeUpgradeHealth(1.65);
					challengeUpgradeHealth.waveReq = j * 2;
					challengeUpgradeHealth.cost = this.allUpgrades[j - 1].cost * 6.0;
					this.allUpgrades.Add(challengeUpgradeHealth);
				}
			}
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateRegularMagolies();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.damage *= 0.1;
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
			return 2f + (float)(totWave % 10) * 0.7f;
		}
	}
}
