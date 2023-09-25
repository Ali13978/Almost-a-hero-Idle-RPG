using System;

namespace Simulation
{
	public class OutfitsOwnedUnlockReq : StatUnlockReq
	{
		public override bool IsUnlocked(Simulator sim)
		{
			return sim.IsThereAnySkinBought();
		}
	}
}
