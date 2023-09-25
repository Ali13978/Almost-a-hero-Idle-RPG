using System;

namespace Simulation
{
	public abstract class UnlockReq
	{
		public abstract bool IsSatisfied(Simulator sim, World world);

		public abstract string GetReqString();

		public abstract string GetReqStringLessDetail();

		public abstract string GetReqStringEvenLessDetail();

		public abstract int GetReqInt();

		public abstract string GetReqSatisfiedString();
	}
}
