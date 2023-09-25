using System;

namespace Simulation
{
	public class BuffDataTrinketEpicBoss : BuffData
	{
		public override void OnKilled(Buff buff, UnitHealthy killed)
		{
			if (killed is Enemy)
			{
				Enemy enemy = killed as Enemy;
				if (enemy.IsEpic())
				{
					Hero hero = buff.GetBy() as Hero;
					Trinket trinket = hero.trinket;
					trinket.req.IncreaseProgress(1);
				}
			}
		}
	}
}
