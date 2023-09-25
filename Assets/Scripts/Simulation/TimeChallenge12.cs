using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class TimeChallenge12 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 11;
			this.numHeroesMin = 1;
			this.numHeroesMax = 1;
			this.hasRing = false;
			this.ratPower = 0.45;
			this.minPower = -25.0;
			this.maxPower = 110.0;
			this.numWaves = 10;
			this.dur = (float)(3 * TimeChallenge.MINUTE);
			for (int i = 0; i < 10; i++)
			{
				ChallengeUpgradeDamage challengeUpgradeDamage = new ChallengeUpgradeDamage(1.5 - (double)i * 0.05);
				challengeUpgradeDamage.waveReq = 1;
				challengeUpgradeDamage.cost = 1000.0 * Math.Pow(2.0, (double)i);
				this.allUpgrades.Add(challengeUpgradeDamage);
				if ((i + 1) % 3 == 0)
				{
					ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints = new ChallengeUpgradeSkillPoints(2);
					challengeUpgradeSkillPoints.waveReq = 1;
					challengeUpgradeSkillPoints.cost = 1000.0 * Math.Pow(2.0, (double)i);
					this.allUpgrades.Add(challengeUpgradeSkillPoints);
				}
			}
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateRegularBandit();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.goldToDrop *= 1000.0;
			enemyDataBase.damage *= 0.01;
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
			return Mathf.Max(1f, (float)totWave * 0.5f + 1f);
		}
	}
}
