using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class TimeChallenge01 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 0;
			this.numHeroesMin = 1;
			this.numHeroesMax = 1;
			this.hasRing = true;
			this.ratPower = 0.4;
			this.minPower = 0.0;
			this.maxPower = 8.0;
			this.numWaves = 50;
			this.dur = (float)(3 * TimeChallenge.MINUTE);
			this.numDuplicatesTotal = 0;
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateRegularWolf();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.damage *= 0.5;
			enemyDataBase.healthMax *= 2.0;
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
