using System;
using System.Collections.Generic;

namespace Simulation
{
	public class TimeChallenge11 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 10;
			this.numHeroesMin = 2;
			this.numHeroesMax = 2;
			this.hasRing = true;
			this.ratPower = 1.0;
			this.minPower = 22.0;
			this.maxPower = 22.0;
			this.dur = (float)(3 * TimeChallenge.MINUTE);
			this.numWaves = 1;
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateRegularMagolies();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.healthMax *= 25.0;
			enemyDataBase.damage *= 0.12;
			enemyDataBase.goldToDrop = 0.0;
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
			return 6f;
		}
	}
}
