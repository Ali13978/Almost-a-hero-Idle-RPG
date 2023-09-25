using System;

namespace Simulation
{
	public class DropPowerupCooldown : DropPowerup
	{
		public override void Apply(World world)
		{
			world.AddPowerupCooldown();
		}
	}
}
