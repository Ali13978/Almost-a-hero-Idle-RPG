using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class TimeChallenge15 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 14;
			this.numHeroesMin = 4;
			this.numHeroesMax = 4;
			this.hasRing = false;
			this.ratPower = 0.6;
			this.minPower = 5.0;
			this.maxPower = 110.0;
			this.numWaves = 50;
			this.dur = (float)(20 * TimeChallenge.MINUTE);
			ChallengeUpgradeReduceSkillLevelReq challengeUpgradeReduceSkillLevelReq = new ChallengeUpgradeReduceSkillLevelReq(15);
			challengeUpgradeReduceSkillLevelReq.waveReq = 1;
			challengeUpgradeReduceSkillLevelReq.cost = 1.0;
			ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints = new ChallengeUpgradeSkillPoints(10);
			challengeUpgradeSkillPoints.waveReq = 1;
			challengeUpgradeSkillPoints.cost = 10000000.0;
			this.allUpgrades = new List<ChallengeUpgrade>
			{
				challengeUpgradeReduceSkillLevelReq,
				challengeUpgradeSkillPoints
			};
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateRegularSpider();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.goldToDrop *= 0.01;
			enemyDataBase.damage *= 0.1;
			enemyDataBase.healthMax *= 5.0;
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
			return Mathf.Max(1f, (float)totWave * 0.2f + 1f);
		}
	}
}
