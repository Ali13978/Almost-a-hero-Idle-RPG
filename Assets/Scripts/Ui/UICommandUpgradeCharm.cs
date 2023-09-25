using System;
using Simulation;

namespace Ui
{
	public class UICommandUpgradeCharm : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			CharmEffectData charm = sim.allCharmEffects[this.charmId];
			sim.TryUpgradeCharm(charm);
		}

		public int charmId;
	}
}
