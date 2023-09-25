using System;
using Simulation;
using Simulation.ArtifactSystem;

namespace Ui
{
	public class UiCommandArtifactRerollUniqueEffect : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			ArtifactsManager artifactsManager = sim.artifactsManager;
			artifactsManager.TryToReroll(this.artifact, this.effectIndex, sim);
		}

		public Simulation.ArtifactSystem.Artifact artifact;

		public int effectIndex;
	}
}
