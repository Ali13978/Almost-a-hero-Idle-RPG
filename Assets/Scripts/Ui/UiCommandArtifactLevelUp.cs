using System;
using Simulation;
using Simulation.ArtifactSystem;

namespace Ui
{
	public class UiCommandArtifactLevelUp : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			if (this.jumpCount < 1)
			{
				return;
			}
			ArtifactsManager artifactsManager = sim.artifactsManager;
			int maxLevelJump = artifactsManager.GetMaxLevelJump(this.artifact);
			this.jumpCount = GameMath.Clamp(this.jumpCount, 1, maxLevelJump);
			int levelCountForPrice = artifactsManager.GetLevelCountForPrice(this.artifact, sim.GetMythstones().GetAmount(), sim.GetUniversalBonusAll());
			this.jumpCount = GameMath.Clamp(this.jumpCount, 1, levelCountForPrice);
			artifactsManager.TryToUpgrade(this.artifact, this.jumpCount, sim);
		}

		public int jumpCount = 1;

		public Simulation.ArtifactSystem.Artifact artifact;
	}
}
