using System;

namespace Simulation
{
	public class BuffDataTrinketKill : BuffData
	{
		public override void OnKilled(Buff buff, UnitHealthy killed)
		{
			Hero hero = buff.GetBy() as Hero;
			Trinket trinket = hero.trinket;
			trinket.req.IncreaseProgress(1);
		}
	}
}
