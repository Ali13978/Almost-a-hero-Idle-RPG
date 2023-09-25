using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class TimeChallenge09 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 8;
			this.numHeroesMin = 3;
			this.numHeroesMax = 3;
			this.hasRing = true;
			this.ratPower = 0.8;
			this.minPower = 8.0;
			this.maxPower = 95.0;
			this.numWaves = 250;
			this.dur = (float)(12 * TimeChallenge.MINUTE);
			for (int i = 0; i < 50; i++)
			{
				ChallengeUpgradeGold challengeUpgradeGold = new ChallengeUpgradeGold(1.03);
				challengeUpgradeGold.waveReq = i * 5;
				challengeUpgradeGold.cost = 1.0;
				ChallengeUpgradeDamage challengeUpgradeDamage = new ChallengeUpgradeDamage(1.03);
				challengeUpgradeDamage.waveReq = i * 5;
				challengeUpgradeDamage.cost = 1.0;
				ChallengeUpgradeHealth challengeUpgradeHealth = new ChallengeUpgradeHealth(1.03);
				challengeUpgradeHealth.waveReq = i * 5;
				challengeUpgradeHealth.cost = 1.0;
				ChallengeUpgradeDamageTotem challengeUpgradeDamageTotem = new ChallengeUpgradeDamageTotem(1.03);
				challengeUpgradeDamageTotem.waveReq = i * 5;
				challengeUpgradeDamageTotem.cost = 1.0;
				this.allUpgrades.Add(challengeUpgradeGold);
				this.allUpgrades.Add(challengeUpgradeDamage);
				this.allUpgrades.Add(challengeUpgradeHealth);
				this.allUpgrades.Add(challengeUpgradeDamageTotem);
			}
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateRegularWolf();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.goldToDrop *= 1.0;
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
			return Mathf.Max(1f, (float)(totWave % 25 + 1) * 0.18f + 1f);
		}
	}
}
