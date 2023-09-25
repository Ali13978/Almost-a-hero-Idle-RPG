using System;

namespace Simulation
{
	public class DropPowerupRevive : DropPowerup
	{
		public override void Apply(World world)
		{
			world.AddPowerupRevive();
		}
	}
}
