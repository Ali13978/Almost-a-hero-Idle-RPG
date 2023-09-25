using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class TimeChallenge25 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 24;
			this.numDuplicatesTotal = 0;
			this.numHeroesMin = 4;
			this.numHeroesMax = 4;
			this.hasRing = true;
			this.ratPower = 0.5;
			this.minPower = -40.0;
			this.maxPower = 340.0;
			this.numWaves = 200;
			this.dur = (float)(25 * TimeChallenge.MINUTE);
			for (int i = 0; i < 20; i++)
			{
				int amount = (i < 10) ? 1 : 2;
				ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints = new ChallengeUpgradeSkillPoints(amount);
				challengeUpgradeSkillPoints.waveReq = i * 8;
				challengeUpgradeSkillPoints.cost = 100000000.0 * GameMath.PowInt(2.5, i * 4 + 10);
				this.allUpgrades.Add(challengeUpgradeSkillPoints);
				ChallengeUpgradeHealth challengeUpgradeHealth = new ChallengeUpgradeHealth(3.0);
				challengeUpgradeHealth.waveReq = i * 8 + 2;
				challengeUpgradeHealth.cost = 500000000.0 * GameMath.PowInt(2.5, i * 4 + 10);
				this.allUpgrades.Add(challengeUpgradeHealth);
				ChallengeUpgradeDamage challengeUpgradeDamage = new ChallengeUpgradeDamage(2.0);
				challengeUpgradeDamage.waveReq = i * 8 + 4;
				challengeUpgradeDamage.cost = 2500000000.0 * GameMath.PowInt(2.5, i * 4 + 10);
				this.allUpgrades.Add(challengeUpgradeDamage);
				ChallengeUpgradeDamageTotem challengeUpgradeDamageTotem = new ChallengeUpgradeDamageTotem(2.0);
				challengeUpgradeDamageTotem.waveReq = i * 8 + 6;
				challengeUpgradeDamageTotem.cost = 12500000000.0 * GameMath.PowInt(2.5, i * 4 + 10);
				this.allUpgrades.Add(challengeUpgradeDamageTotem);
			}
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateRegularWolf();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.healthMax *= 0.10000000149011612;
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
			return Mathf.Max(2f, (float)totWave / (float)this.numWaves * 6f);
		}
	}
}
