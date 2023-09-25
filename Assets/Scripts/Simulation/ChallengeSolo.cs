using System;
using System.Collections.Generic;

namespace Simulation
{
	public class ChallengeSolo : ChallengeStandard
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.numHeroesMin = 1;
			this.numHeroesMax = 1;
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase item = EnemyFactory.CreateRegularBandit();
			EnemyDataBase item2 = EnemyFactory.CreateRegularWolf();
			EnemyDataBase item3 = EnemyFactory.CreateDwarfSemiCorrupted();
			EnemyDataBase item4 = EnemyFactory.CreateDwarfCorrupted();
			EnemyDataBase item5 = EnemyFactory.CreateBossDwarf();
			EnemyDataBase item6 = EnemyFactory.CreateElfSemiCorrupted();
			EnemyDataBase item7 = EnemyFactory.CreateElfCorrupted();
			EnemyDataBase item8 = EnemyFactory.CreateBossElf();
			EnemyDataBase item9 = EnemyFactory.CreateEpicBossMagolis();
			EnemyDataBase item10 = EnemyFactory.CreateEpicBossOrc();
			Environment item11 = new Environment(Bg.DESERT, new List<EnemyDataBase>
			{
				item,
				item2,
				item6,
				item3
			}, new List<EnemyDataBase>
			{
				item5,
				item8
			}, new List<EnemyDataBase>
			{
				item10,
				item9
			}, null, false);
			Environment item12 = new Environment(Bg.DUNGEON, new List<EnemyDataBase>
			{
				item4,
				item7,
				item6,
				item3
			}, new List<EnemyDataBase>
			{
				item5,
				item8
			}, new List<EnemyDataBase>
			{
				item10,
				item9
			}, null, false);
			this.allEnvironments = new List<Environment>
			{
				item11,
				item12
			};
			this.activeEnv = this.allEnvironments[0];
		}

		public override void Reset()
		{
			base.Reset();
		}
	}
}
