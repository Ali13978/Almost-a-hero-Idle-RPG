using System;

namespace Simulation
{
	public class BuffEventHealRandom : BuffEvent
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
				unitHealthy.Heal(this.healRatio);
			}
		}

		public double healRatio;
	}
}
