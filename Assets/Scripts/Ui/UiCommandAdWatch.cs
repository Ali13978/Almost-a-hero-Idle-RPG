using System;
using Simulation;
using Static;

namespace Ui
{
	public class UiCommandAdWatch : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.GetActiveWorld().shouldSave = true;
			RewardedAdManager.inst.PrepareToShowRewardedVideo(this.targetFlashOffer);
			if (this.targetFlashOffer == null)
			{
				PlayerStats.OnAdAccept();
			}
		}

		public FlashOffer targetFlashOffer;
	}
}
