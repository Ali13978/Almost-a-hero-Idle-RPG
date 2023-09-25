using System;

namespace Simulation
{
	public class BuffEventBuffSelf : BuffEvent
	{
		public override void Apply(Unit by, World world)
		{
			by.AddBuff(this.effect, 0, false);
		}

		public BuffData effect;
	}
}
