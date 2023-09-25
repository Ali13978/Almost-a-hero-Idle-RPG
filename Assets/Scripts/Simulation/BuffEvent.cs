using System;

namespace Simulation
{
	public abstract class BuffEvent
	{
		public abstract void Apply(Unit by, World world);

		public float time;
	}
}
