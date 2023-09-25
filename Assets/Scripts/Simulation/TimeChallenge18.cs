using System;
using System.Collections.Generic;

namespace Simulation
{
	public class TimeChallenge18 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 17;
			this.numHeroesMin = 4;
			this.numHeroesMax = 4;
			this.hasRing = true;
			this.ratPower = 0.5;
			this.minPower = 15.0;
			this.maxPower = 125.0;
			this.numWaves = 100;
			this.dur = (float)(5 * TimeChallenge.MINUTE);
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateRegularMagolies();
			enemyDataBase.spawnWeight = 1f;
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
			return 2f + (float)(totWave % 20) * 0.7f;
		}
	}
}
