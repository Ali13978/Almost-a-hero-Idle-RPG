using System;

namespace Simulation
{
	public class BuffEventZeroBuffGenericCounter : BuffEvent
	{
		public override void Apply(Unit by, World world)
		{
			by.ZeroBuffGenericCounter(this.buffType);
		}

		public Type buffType;
	}
}
