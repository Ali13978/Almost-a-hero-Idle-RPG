using System;
using Simulation;

namespace Ui
{
	public class UiCommandArtifactCraft : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			if (this.isMythical)
			{
				sim.artifactsManager.TryCraftNewMythicalArtifact(sim);
			}
			else
			{
				sim.artifactsManager.TryCraftNewArtifact(sim, true);
			}
		}

		public bool isMythical;
	}
}
