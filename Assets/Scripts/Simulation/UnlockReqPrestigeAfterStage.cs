using System;

namespace Simulation
{
	public class UnlockReqPrestigeAfterStage : UnlockReq
	{
		public UnlockReqPrestigeAfterStage(int stageNo)
		{
			this.stageNo = stageNo;
		}

		public override bool IsSatisfied(Simulator sim, World world)
		{
			int num = 0;
			if (sim.lastPrestigeRunstats != null)
			{
				num = sim.lastPrestigeRunstats.stage;
			}
			return num >= this.stageNo;
		}

		public override string GetReqString()
		{
			return string.Format(LM.Get("UNLOCK_REQ_PRESTIGE_STAGE"), this.stageNo);
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
			return this.stageNo;
		}

		public override string GetReqSatisfiedString()
		{
			return string.Format(LM.Get("UNLOCK_REQ_PRESTIGE_STAGE"), this.stageNo);
		}

		private int stageNo;
	}
}
