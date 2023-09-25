using System;

namespace Simulation
{
	public abstract class SkinUnlockReq
	{
		public abstract bool IsSatisfied(Simulator sim, SkinData skinData);

		public abstract string GetReqString(bool reveal);
	}
}
