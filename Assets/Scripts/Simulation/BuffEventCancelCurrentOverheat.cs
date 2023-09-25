using System;

namespace Simulation
{
	public class BuffEventCancelCurrentOverheat : BuffEvent
	{
		public override void Apply(Unit by, World world)
		{
			by.CancelCurrentOverheat();
		}
	}
}
