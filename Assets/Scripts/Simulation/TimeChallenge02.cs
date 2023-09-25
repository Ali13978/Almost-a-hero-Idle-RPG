using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class TimeChallenge02 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 1;
			this.numHeroesMin = 1;
			this.numHeroesMax = 1;
			this.hasRing = true;
			this.ratPower = 0.5;
			this.minPower = 3.0;
			this.maxPower = 20.0;
			this.numWaves = 25;
			this.dur = (float)(5 * TimeChallenge.MINUTE);
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateRegularBandit();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.damage *= 0.5;
			enemyDataBase.healthMax *= 3.0;
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
			return Mathf.Max(1f, (float)totWave / (float)this.numWaves * 6f);
		}
	}
}
