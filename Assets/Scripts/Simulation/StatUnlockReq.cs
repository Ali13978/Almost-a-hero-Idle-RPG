using System;

namespace Simulation
{
	public abstract class StatUnlockReq
	{
		public abstract bool IsUnlocked(Simulator sim);
	}
}
