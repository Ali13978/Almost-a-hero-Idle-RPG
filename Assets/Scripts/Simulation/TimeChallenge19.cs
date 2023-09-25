using System;
using System.Collections.Generic;

namespace Simulation
{
	public class TimeChallenge19 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 18;
			this.numHeroesMin = 5;
			this.numHeroesMax = 5;
			this.hasRing = false;
			this.ratPower = 1.1;
			this.minPower = 20.0;
			this.maxPower = 43.0;
			this.numWaves = 40;
			this.dur = (float)(15 * TimeChallenge.MINUTE);
			ChallengeUpgradeReduceSkillLevelReq challengeUpgradeReduceSkillLevelReq = new ChallengeUpgradeReduceSkillLevelReq(5);
			challengeUpgradeReduceSkillLevelReq.waveReq = 0;
			challengeUpgradeReduceSkillLevelReq.cost = 0.0;
			this.allUpgrades.Add(challengeUpgradeReduceSkillLevelReq);
			for (int i = 0; i < 20; i++)
			{
				ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints = new ChallengeUpgradeSkillPoints(1);
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
