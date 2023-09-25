using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class TimeChallenge03 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 2;
			this.numHeroesMin = 2;
			this.numHeroesMax = 2;
			this.hasRing = false;
			this.ratPower = 0.25;
			this.minPower = 0.0;
			this.maxPower = 30.0;
			this.numWaves = 10;
			this.dur = (float)(8 * TimeChallenge.MINUTE);
			ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints = new ChallengeUpgradeSkillPoints(2);
			challengeUpgradeSkillPoints.cost = 100.0;
			challengeUpgradeSkillPoints.waveReq = 1;
			ChallengeUpgradeReduceSkillLevelReq challengeUpgradeReduceSkillLevelReq = new ChallengeUpgradeReduceSkillLevelReq(3);
			challengeUpgradeReduceSkillLevelReq.cost = 1000.0;
			challengeUpgradeReduceSkillLevelReq.waveReq = 2;
			ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints2 = new ChallengeUpgradeSkillPoints(3);
			challengeUpgradeSkillPoints2.cost = 5000.0;
			challengeUpgradeSkillPoints2.waveReq = 4;
			ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints3 = new ChallengeUpgradeSkillPoints(4);
			challengeUpgradeSkillPoints3.cost = 15000.0;
			challengeUpgradeSkillPoints3.waveReq = 6;
			ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints4 = new ChallengeUpgradeSkillPoints(5);
			challengeUpgradeSkillPoints4.cost = 50000.0;
			challengeUpgradeSkillPoints4.waveReq = 8;
			this.allUpgrades = new List<ChallengeUpgrade>
			{
				challengeUpgradeSkillPoints,
				challengeUpgradeReduceSkillLevelReq,
				challengeUpgradeSkillPoints2,
				challengeUpgradeSkillPoints3,
				challengeUpgradeSkillPoints4
			};
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateElfSemiCorrupted();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.damage *= 0.1;
			enemyDataBase.healthMax *= 2.2;
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
			return Mathf.Max(2f, 1f + (float)totWave / (float)this.numWaves * 6f);
		}
	}
}
