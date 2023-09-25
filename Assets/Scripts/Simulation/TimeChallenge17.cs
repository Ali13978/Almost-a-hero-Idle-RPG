using System;
using System.Collections.Generic;

namespace Simulation
{
	public class TimeChallenge17 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 16;
			this.numHeroesMin = 1;
			this.numHeroesMax = 1;
			this.hasRing = false;
			this.ratPower = 0.75;
			this.minPower = 22.0;
			this.maxPower = 35.0;
			this.numWaves = 20;
			this.dur = (float)(10 * TimeChallenge.MINUTE);
			ChallengeUpgradeReduceSkillLevelReq challengeUpgradeReduceSkillLevelReq = new ChallengeUpgradeReduceSkillLevelReq(10);
			challengeUpgradeReduceSkillLevelReq.waveReq = 0;
			challengeUpgradeReduceSkillLevelReq.cost = 0.0;
			this.allUpgrades.Add(challengeUpgradeReduceSkillLevelReq);
			for (int i = 0; i < 19; i++)
			{
				ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints = new ChallengeUpgradeSkillPoints(1);
				challengeUpgradeSkillPoints.waveReq = i;
				challengeUpgradeSkillPoints.cost = 0.0;
				this.allUpgrades.Add(challengeUpgradeSkillPoints);
			}
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateHumanCorrupted();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.goldToDrop = 0.0;
			enemyDataBase.damage *= 2.0;
			enemyDataBase.healthMax *= 1.25;
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
			return 2f + (float)(totWave % 5) * 0.6f;
		}
	}
}
