using System;
using System.Collections.Generic;

namespace Simulation
{
	public class TimeChallenge14 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 13;
			this.numHeroesMin = 0;
			this.numHeroesMax = 0;
			this.hasRing = true;
			this.ratPower = 1.2;
			this.minPower = 20.0;
			this.maxPower = 30.0;
			this.numWaves = 3;
			this.dur = (float)(2 * TimeChallenge.MINUTE);
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateDwarfSemiCorrupted();
			enemyDataBase.spawnWeight = 1f;
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
			return 1f + 2f * (float)totWave;
		}
	}
}
