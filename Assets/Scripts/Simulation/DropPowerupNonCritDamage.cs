using System;

namespace Simulation
{
	public class DropPowerupNonCritDamage : DropPowerup
	{
		public override void Apply(World world)
		{
			world.AddPowerupNonCritDamage();
		}
	}
}
