using System;
using System.Collections.Generic;

namespace Simulation
{
	public class TimeChallenge26 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 25;
			this.numDuplicatesTotal = 5;
			this.numHeroesMin = 1;
			this.numHeroesMax = 1;
			this.hasRing = false;
			this.ratPower = 0.75;
			this.minPower = 40.0;
			this.maxPower = 60.0;
			this.numWaves = 100;
			this.dur = (float)(15 * TimeChallenge.MINUTE);
			ChallengeUpgradeReduceSkillLevelReq challengeUpgradeReduceSkillLevelReq = new ChallengeUpgradeReduceSkillLevelReq(10);
			challengeUpgradeReduceSkillLevelReq.waveReq = 0;
			challengeUpgradeReduceSkillLevelReq.cost = 0.0;
			this.allUpgrades.Add(challengeUpgradeReduceSkillLevelReq);
			for (int i = 0; i < 5; i++)
			{
				ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints = new ChallengeUpgradeSkillPoints(5);
				challengeUpgradeSkillPoints.waveReq = i * 10;
				challengeUpgradeSkillPoints.cost = 0.0;
				this.allUpgrades.Add(challengeUpgradeSkillPoints);
			}
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateRegularMagolies();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.damage *= 0.03;
			enemyDataBase.healthMax *= 2.5;
			enemyDataBase.goldToDrop = 0.0;
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
			return 3f + (float)((totWave + 1) % 10) * 0.3f;
		}
	}
}
