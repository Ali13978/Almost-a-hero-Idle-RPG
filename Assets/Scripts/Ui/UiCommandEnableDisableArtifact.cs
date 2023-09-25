using System;
using Simulation;

namespace Ui
{
	public class UiCommandEnableDisableArtifact : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			if (this.artifact.IsEnabled())
			{
				sim.artifactsManager.TryDisableArtifact(this.artifact, sim);
			}
			else
			{
				sim.artifactsManager.TryEnableArtifact(this.artifact, sim);
			}
		}

		public Artifact artifact;
	}
}
