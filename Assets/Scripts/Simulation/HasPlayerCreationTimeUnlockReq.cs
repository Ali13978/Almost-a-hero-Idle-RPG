using System;
using Static;

namespace Simulation
{
	public class HasPlayerCreationTimeUnlockReq : StatUnlockReq
	{
		public override bool IsUnlocked(Simulator sim)
		{
			return PlayerStats.playfabCreationDate != null && PlayerStats.playfabCreationDate.Value > 0L;
		}
	}
}
