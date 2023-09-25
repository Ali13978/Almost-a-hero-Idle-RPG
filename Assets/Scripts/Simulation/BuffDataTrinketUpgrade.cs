using System;

namespace Simulation
{
	public class BuffDataTrinketUpgrade : BuffData
	{
		public override void OnUpgradedSelf(Buff buff)
		{
			Hero hero = buff.GetBy() as Hero;
			Trinket trinket = hero.trinket;
			trinket.req.IncreaseProgress(1);
		}
	}
}
