using System;

namespace Simulation
{
	public class SlotUnlockCondition
	{
		public bool HasSatisfied(World world)
		{
			return world.IsRiftBeaten(this.riftIdToBeat);
		}

		public int riftIdToBeat;
	}
}
