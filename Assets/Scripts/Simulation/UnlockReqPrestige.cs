using System;

namespace Simulation
{
	public class UnlockReqPrestige : UnlockReq
	{
		public UnlockReqPrestige(int numPrestiges)
		{
			this.numPrestiges = numPrestiges;
		}

		public override bool IsSatisfied(Simulator sim, World world)
		{
			return sim.numPrestiges >= this.numPrestiges;
		}

		public override string GetReqString()
		{
			return string.Format(LM.Get("UNLOCK_REQ_PRESTIGE"), this.numPrestiges.ToString());
		}

		public override string GetReqStringLessDetail()
		{
			return LM.Get("UNLOCK_REQ_LESS_PRESTIGE");
		}

		public override string GetReqStringEvenLessDetail()
		{
			return LM.Get("UNLOCK_REQ_LESS_PRESTIGE");
		}

		public override int GetReqInt()
		{
			return this.numPrestiges;
		}

		public override string GetReqSatisfiedString()
		{
			return string.Format(LM.Get("UNLOCK_REQ_PRESTIGE"), this.numPrestiges.ToString());
		}

		private int numPrestiges;
	}
}
