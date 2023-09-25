using System;
using Simulation;

namespace Ui
{
	public class UiCommandArtifactsUnlockSlot : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.artifactsManager.TryToPurchaseASlot(sim);
			UiManager.stateJustChanged = true;
		}
	}
}
