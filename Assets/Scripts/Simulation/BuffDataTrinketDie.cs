using System;

namespace Simulation
{
	public class BuffDataTrinketDie : BuffData
	{
		public override void OnDeathSelf(Buff buff)
		{
			Hero hero = buff.GetBy() as Hero;
			Trinket trinket = hero.trinket;
			trinket.req.IncreaseProgress(1);
		}
	}
}
