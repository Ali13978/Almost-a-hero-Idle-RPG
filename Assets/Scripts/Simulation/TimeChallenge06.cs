using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class TimeChallenge06 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 5;
			this.numHeroesMin = 3;
			this.numHeroesMax = 3;
			this.hasRing = false;
			this.ratPower = 0.3;
			this.minPower = 0.0;
			this.maxPower = 80.0;
			this.numWaves = 100;
			this.dur = (float)(12 * TimeChallenge.MINUTE);
			ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints = new ChallengeUpgradeSkillPoints(2);
			challengeUpgradeSkillPoints.waveReq = 6;
			ChallengeUpgradeReduceSkillLevelReq challengeUpgradeReduceSkillLevelReq = new ChallengeUpgradeReduceSkillLevelReq(2);
			challengeUpgradeReduceSkillLevelReq.waveReq = 12;
			ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints2 = new ChallengeUpgradeSkillPoints(2);
			challengeUpgradeSkillPoints2.waveReq = 18;
			ChallengeUpgradeReduceSkillLevelReq challengeUpgradeReduceSkillLevelReq2 = new ChallengeUpgradeReduceSkillLevelReq(2);
			challengeUpgradeReduceSkillLevelReq2.waveReq = 24;
			ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints3 = new ChallengeUpgradeSkillPoints(2);
			challengeUpgradeSkillPoints3.waveReq = 30;
			ChallengeUpgradeReduceSkillLevelReq challengeUpgradeReduceSkillLevelReq3 = new ChallengeUpgradeReduceSkillLevelReq(2);
			challengeUpgradeReduceSkillLevelReq3.waveReq = 36;
			this.allUpgrades = new List<ChallengeUpgrade>
			{
				challengeUpgradeSkillPoints,
				challengeUpgradeReduceSkillLevelReq,
				challengeUpgradeSkillPoints2,
				challengeUpgradeReduceSkillLevelReq2,
				challengeUpgradeSkillPoints3,
				challengeUpgradeReduceSkillLevelReq3
			};
			for (int i = 0; i < 5; i++)
			{
				ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints4 = new ChallengeUpgradeSkillPoints(2);
				challengeUpgradeSkillPoints4.waveReq = 42 + i * 9;
				this.allUpgrades.Add(challengeUpgradeSkillPoints4);
			}
			int j = 0;
			int count = this.allUpgrades.Count;
			while (j < count)
			{
				ChallengeUpgrade challengeUpgrade = this.allUpgrades[j];
				challengeUpgrade.cost = 50.0 * GameMath.PowInt(1.27, challengeUpgrade.waveReq);
				j++;
			}
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateHumanSemiCorrupted();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.goldToDrop *= 1.0;
			enemyDataBase.damage *= 0.15;
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
			return Mathf.Max(1f, ((float)totWave % 20f + 1f) * 0.2f + 1f);
		}
	}
}
