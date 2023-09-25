using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class TimeChallenge30 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 29;
			this.numDuplicatesTotal = 5;
			this.numHeroesMin = 3;
			this.numHeroesMax = 3;
			this.hasRing = false;
			this.ratPower = 0.4;
			this.minPower = 0.0;
			this.maxPower = 295.0;
			this.numWaves = 50;
			this.dur = (float)(20 * TimeChallenge.MINUTE);
			for (int i = 0; i < 15; i++)
			{
				ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints = new ChallengeUpgradeSkillPoints(2);
				challengeUpgradeSkillPoints.waveReq = i * 2;
				challengeUpgradeSkillPoints.cost = 0.0;
				this.allUpgrades.Add(challengeUpgradeSkillPoints);
			}
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateRegularMagolies();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.damage *= 0.25;
			enemyDataBase.goldToDrop *= 10000.0;
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
			return Mathf.Max(2f, (float)(totWave % 5 + 1));
		}
	}
}
