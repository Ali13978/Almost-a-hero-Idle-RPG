using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class TimeChallenge08 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 7;
			this.numHeroesMin = 2;
			this.numHeroesMax = 2;
			this.hasRing = true;
			this.ratPower = 0.8;
			this.minPower = 10.0;
			this.maxPower = 90.0;
			this.numWaves = 5;
			this.dur = (float)(5 * TimeChallenge.MINUTE);
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateElfCorrupted();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.damage *= 0.1;
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
			return Mathf.Max(1f, (float)totWave * 0.75f + 1f);
		}
	}
}
