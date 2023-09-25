using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class TimeChallenge29 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 28;
			this.numDuplicatesTotal = 5;
			this.numHeroesMin = 2;
			this.numHeroesMax = 2;
			this.hasRing = true;
			this.ratPower = 0.4;
			this.minPower = 0.0;
			this.maxPower = 190.0;
			this.numWaves = 10;
			this.dur = (float)(10 * TimeChallenge.MINUTE);
			ChallengeUpgradeReduceSkillLevelReq challengeUpgradeReduceSkillLevelReq = new ChallengeUpgradeReduceSkillLevelReq(5);
			challengeUpgradeReduceSkillLevelReq.waveReq = 0;
			challengeUpgradeReduceSkillLevelReq.cost = 0.0;
			this.allUpgrades.Add(challengeUpgradeReduceSkillLevelReq);
			for (int i = 0; i < 5; i++)
			{
				ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints = new ChallengeUpgradeSkillPoints(3);
				challengeUpgradeSkillPoints.waveReq = i * 2;
				challengeUpgradeSkillPoints.cost = 1000000.0 * GameMath.PowInt(4.0, i * 3);
				this.allUpgrades.Add(challengeUpgradeSkillPoints);
			}
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateDwarfCorrupted();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.damage *= 9.9999997473787516E-05;
			enemyDataBase.goldToDrop *= 0.0001;
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
			return Mathf.Max(2f, (float)(totWave % 5));
		}
	}
}
