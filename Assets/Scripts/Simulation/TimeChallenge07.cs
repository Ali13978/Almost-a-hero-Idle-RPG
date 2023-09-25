using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class TimeChallenge07 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 6;
			this.numHeroesMin = 0;
			this.numHeroesMax = 0;
			this.hasRing = true;
			this.ratPower = 0.7;
			this.minPower = 0.0;
			this.maxPower = 95.0;
			this.numWaves = 50;
			this.dur = (float)(5 * TimeChallenge.MINUTE);
			for (int i = 0; i < 60; i++)
			{
				ChallengeUpgradeDamageTotem challengeUpgradeDamageTotem = new ChallengeUpgradeDamageTotem(1.02 + 0.02 * (double)(i % 15));
				challengeUpgradeDamageTotem.waveReq = 10;
				challengeUpgradeDamageTotem.cost = 10000.0 * GameMath.PowInt(1.29, i);
				this.allUpgrades.Add(challengeUpgradeDamageTotem);
			}
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateHumanCorrupted();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.damage *= 0.2;
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
			return Mathf.Max(1f, ((float)totWave % 10f + 1f) * 0.2f + 1f);
		}
	}
}
