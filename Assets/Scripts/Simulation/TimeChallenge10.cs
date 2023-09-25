using System;
using System.Collections.Generic;

namespace Simulation
{
	public class TimeChallenge10 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 9;
			this.numHeroesMin = 3;
			this.numHeroesMax = 3;
			this.hasRing = false;
			this.ratPower = 0.35;
			this.minPower = 0.0;
			this.maxPower = 60.0;
			this.dur = (float)(10 * TimeChallenge.MINUTE);
			this.numWaves = 15;
			ChallengeUpgradeReduceSkillLevelReq challengeUpgradeReduceSkillLevelReq = new ChallengeUpgradeReduceSkillLevelReq(15);
			challengeUpgradeReduceSkillLevelReq.waveReq = 1;
			challengeUpgradeReduceSkillLevelReq.cost = 0.0;
			ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints = new ChallengeUpgradeSkillPoints(20);
			challengeUpgradeSkillPoints.waveReq = 1;
			challengeUpgradeSkillPoints.cost = 0.0;
			this.allUpgrades = new List<ChallengeUpgrade>
			{
				challengeUpgradeReduceSkillLevelReq,
				challengeUpgradeSkillPoints
			};
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateDwarfCorrupted();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.goldToDrop *= 0.001;
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
			return 8f;
		}
	}
}
