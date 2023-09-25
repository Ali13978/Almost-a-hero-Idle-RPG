using System;
using System.Collections.Generic;

namespace Simulation
{
	public class TimeChallenge28 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 27;
			this.numDuplicatesTotal = 0;
			this.numHeroesMin = 1;
			this.numHeroesMax = 1;
			this.hasRing = false;
			this.ratPower = 0.4;
			this.minPower = 40.0;
			this.maxPower = 60.0;
			this.numWaves = 60;
			this.dur = (float)(8 * TimeChallenge.MINUTE);
			ChallengeUpgradeReduceSkillLevelReq challengeUpgradeReduceSkillLevelReq = new ChallengeUpgradeReduceSkillLevelReq(15);
			challengeUpgradeReduceSkillLevelReq.waveReq = 0;
			challengeUpgradeReduceSkillLevelReq.cost = 0.0;
			this.allUpgrades.Add(challengeUpgradeReduceSkillLevelReq);
			ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints = new ChallengeUpgradeSkillPoints(40);
			challengeUpgradeSkillPoints.waveReq = 0;
			challengeUpgradeSkillPoints.cost = 0.0;
			this.allUpgrades.Add(challengeUpgradeSkillPoints);
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateRegularBandit();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.damage *= 0.0099999997764825821;
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
			return 5f;
		}
	}
}
