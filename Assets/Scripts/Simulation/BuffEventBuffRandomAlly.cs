using System;

namespace Simulation
{
	public class BuffEventBuffRandomAlly : BuffEvent
	{
		public override void Apply(Unit by, World world)
		{
			Unit unit;
			if (by is Enemy)
			{
				unit = world.GetRandomAliveEnemy();
			}
			else
			{
				unit = world.GetRandomAliveHero();
			}
			if (unit != null)
			{
				unit.AddBuff(this.effect, 0, false);
			}
		}

		public BuffData effect;
	}
}
