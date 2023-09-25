using System;
using Simulation;

namespace Ui
{
	public class UiCommandArtifactUpgrade : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.artifactsManager.TryUpgradeMythicalArtifact(this.index, sim, this.jumpCount);
		}

		public int index;

		public int jumpCount;
	}
}
