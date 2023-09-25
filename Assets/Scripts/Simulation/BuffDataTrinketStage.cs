using System;

namespace Simulation
{
	public class BuffDataTrinketStage : BuffData
	{
		public override void OnNewStage(Buff buff)
		{
			Hero hero = buff.GetBy() as Hero;
			Trinket trinket = hero.trinket;
			trinket.req.IncreaseProgress(1);
		}
	}
}
