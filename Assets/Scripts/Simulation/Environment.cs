using System;
using System.Collections.Generic;

namespace Simulation
{
	public class Environment
	{
		public Environment(Bg bg, List<EnemyDataBase> enemiesRegular, List<EnemyDataBase> enemiesBoss = null, List<EnemyDataBase> enemiesEpic = null, EnemyDataBase enemyChest = null, bool dontSpawnRegularEnemiesWithBoss = false)
		{
			this.bg = bg;
			this.enemiesRegular = enemiesRegular;
			this.enemiesBoss = enemiesBoss;
			this.enemiesEpic = enemiesEpic;
			this.enemyChest = enemyChest;
			this.dontSpawnRegularEnemiesWithBoss = dontSpawnRegularEnemiesWithBoss;
		}

		public Bg bg;

		public bool dontSpawnRegularEnemiesWithBoss;

		public List<EnemyDataBase> enemiesRegular;

		public List<EnemyDataBase> enemiesBoss;

		public List<EnemyDataBase> enemiesEpic;

		public EnemyDataBase enemyChest;
	}
}
