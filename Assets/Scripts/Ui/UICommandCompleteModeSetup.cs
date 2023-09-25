using System;
using Simulation;

namespace Ui
{
	public class UICommandCompleteModeSetup : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TrySetupChallenge(this.mode, this.totemDatabase, this.heroDatabases);
			sim.TrySwitchGameMode(this.mode);
		}

		public GameMode mode;

		public TotemDataBase totemDatabase;

		public HeroDataBase[] heroDatabases;
	}
}
