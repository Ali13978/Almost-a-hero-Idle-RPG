using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class TimeChallenge13 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 12;
			this.numHeroesMin = 3;
			this.numHeroesMax = 3;
			this.hasRing = true;
			this.ratPower = 0.3;
			this.minPower = -5.0;
			this.maxPower = 115.0;
			this.numWaves = 450;
			this.dur = (float)(20 * TimeChallenge.MINUTE);
			for (int i = 0; i < 19; i++)
			{
				int waveReq = (i + 1) * 25;
				ChallengeUpgradeGold challengeUpgradeGold = new ChallengeUpgradeGold(1.35);
				double num = 0.5;
				double num2 = 0.5;
				if (i / 20 % 2 == 0)
				{
					num = 0.15;
				}
				else
				{
					num2 = 0.15;
				}
				ChallengeUpgradeDamage challengeUpgradeDamage = new ChallengeUpgradeDamage(1.0 + num);
				ChallengeUpgradeHealth challengeUpgradeHealth = new ChallengeUpgradeHealth(1.35);
				ChallengeUpgradeDamageTotem challengeUpgradeDamageTotem = new ChallengeUpgradeDamageTotem(1.0 + num2);
				ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints = new ChallengeUpgradeSkillPoints(1);
				challengeUpgradeGold.waveReq = waveReq;
				challengeUpgradeDamage.waveReq = waveReq;
				challengeUpgradeHealth.waveReq = waveReq;
				challengeUpgradeDamageTotem.waveReq = waveReq;
				challengeUpgradeSkillPoints.waveReq = waveReq;
				this.allUpgrades.Add(challengeUpgradeGold);
				this.allUpgrades.Add(challengeUpgradeDamage);
				this.allUpgrades.Add(challengeUpgradeHealth);
				this.allUpgrades.Add(challengeUpgradeDamageTotem);
				this.allUpgrades.Add(challengeUpgradeSkillPoints);
				if (i < 8)
				{
					ChallengeUpgradeReduceSkillLevelReq challengeUpgradeReduceSkillLevelReq = new ChallengeUpgradeReduceSkillLevelReq(1);
					challengeUpgradeReduceSkillLevelReq.waveReq = waveReq;
					this.allUpgrades.Add(challengeUpgradeReduceSkillLevelReq);
				}
			}
			this.allUpgrades[0].cost = 10000.0;
			int j = 1;
			int count = this.allUpgrades.Count;
			while (j < count)
			{
				this.allUpgrades[j].cost = this.allUpgrades[j - 1].cost * (4.0 + (double)j * 0.02);
				j++;
			}
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateElfSemiCorrupted();
			EnemyDataBase enemyDataBase2 = EnemyFactory.CreateElfCorrupted();
			EnemyDataBase enemyDataBase3 = EnemyFactory.CreateDwarfSemiCorrupted();
			EnemyDataBase enemyDataBase4 = EnemyFactory.CreateDwarfCorrupted();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase2.spawnWeight = 1f;
			enemyDataBase3.spawnWeight = 1f;
			enemyDataBase4.spawnWeight = 1f;
			enemyDataBase.goldToDrop *= 0.5;
			enemyDataBase2.goldToDrop *= 0.5;
			enemyDataBase3.goldToDrop *= 0.5;
			enemyDataBase4.goldToDrop *= 0.5;
			enemyDataBase.damage *= 0.025;
			enemyDataBase2.damage *= 0.025;
			enemyDataBase3.damage *= 0.025;
			enemyDataBase4.damage *= 0.025;
			Environment item = new Environment(Bg.TRAINING_GROUND, new List<EnemyDataBase>
			{
				enemyDataBase,
				enemyDataBase2,
				enemyDataBase3,
				enemyDataBase4
			}, null, null, null, false);
			this.allEnvironments = new List<Environment>
			{
				item
			};
			this.activeEnv = this.allEnvironments[0];
		}

		protected override float GetSpawnPoints(int totWave)
		{
			return Mathf.Max(1f, (float)(totWave % 50) * 0.1f + 1f);
		}
	}
}
