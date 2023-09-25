using System;
using Simulation;

namespace Ui
{
	public class CommandToggleArtifactUpgradeMultiplier : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.artifactMultiUpgradeIndex = this.value;
		}

		public int value;
	}
}
