using System;
using Simulation;
using Simulation.ArtifactSystem;

namespace Ui
{
	public class UiCommandArtifactOverhaul : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			OldArtifactsConverter.Convert(sim);
		}
	}
}
