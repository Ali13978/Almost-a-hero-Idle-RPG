using System;
using Simulation;
using Simulation.ArtifactSystem;

namespace Ui
{
	public class UiCommandArtifactEvolve : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			ArtifactsManager artifactsManager = sim.artifactsManager;
			artifactsManager.TryToEvolve(this.artifact, sim, true);
		}

		public Simulation.ArtifactSystem.Artifact artifact;
	}
}
