using System;
using System.Collections.Generic;

namespace Simulation
{
	public class TimeChallenge27 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 26;
			this.numDuplicatesTotal = 0;
			this.numHeroesMin = 5;
			this.numHeroesMax = 5;
			this.hasRing = false;
			this.ratPower = 0.4;
			this.minPower = 40.0;
			this.maxPower = 65.0;
			this.numWaves = 40;
			this.dur = (float)(15 * TimeChallenge.MINUTE);
			ChallengeUpgradeReduceSkillLevelReq challengeUpgradeReduceSkillLevelReq = new ChallengeUpgradeReduceSkillLevelReq(15);
			challengeUpgradeReduceSkillLevelReq.waveReq = 0;
			challengeUpgradeReduceSkillLevelReq.cost = 0.0;
			this.allUpgrades.Add(challengeUpgradeReduceSkillLevelReq);
			for (int i = 0; i < 15; i++)
			{
				ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints = new ChallengeUpgradeSkillPoints(3);
				challengeUpgradeSkillPoints.waveReq = i * 2;
				challengeUpgradeSkillPoints.cost = 0.0;
				this.allUpgrades.Add(challengeUpgradeSkillPoints);
			}
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateRegularMagolies();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.damage *= 0.3;
			enemyDataBase.healthMax *= 5.0;
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
