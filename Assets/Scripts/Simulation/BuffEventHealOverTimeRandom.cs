using System;

namespace Simulation
{
	public class BuffEventHealOverTimeRandom : BuffEvent
	{
		public override void Apply(Unit by, World world)
		{
			UnitHealthy unitHealthy;
			if (by is Enemy)
			{
				unitHealthy = world.GetRandomEnemy();
			}
			else
			{
				unitHealthy = world.GetRandomHero();
			}
			if (unitHealthy != null)
			{
				unitHealthy.AddBuff(this.effect, 0, false);
			}
		}

		public BuffData effect;
	}
}
