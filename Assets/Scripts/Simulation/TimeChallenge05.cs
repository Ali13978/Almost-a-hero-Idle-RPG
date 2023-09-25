using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class TimeChallenge05 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 4;
			this.numHeroesMin = 2;
			this.numHeroesMax = 2;
			this.hasRing = true;
			this.ratPower = 0.6;
			this.minPower = 0.0;
			this.maxPower = 75.0;
			this.numWaves = 40;
			this.dur = (float)(4 * TimeChallenge.MINUTE);
			this.allUpgrades = new List<ChallengeUpgrade>();
			for (int i = 0; i < 18; i++)
			{
				ChallengeUpgradeGold challengeUpgradeGold = new ChallengeUpgradeGold(1.2 + (double)i * 0.1);
				challengeUpgradeGold.waveReq = i * 2 + 2;
				challengeUpgradeGold.cost = 1000.0 * GameMath.PowDouble(1.8 + (double)i * 0.02, (double)(i + 1));
				this.allUpgrades.Add(challengeUpgradeGold);
			}
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateDwarfSemiCorrupted();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.healthMax *= 8.0;
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
			return Mathf.Max(1f, ((float)totWave % 10f + 1f) * 0.5f + 1f);
		}
	}
}
