using System;

namespace Simulation
{
	public class UnlockReqReachStage : UnlockReq
	{
		public UnlockReqReachStage(int stageNo)
		{
			this.stageNo = stageNo;
		}

		public int GetStageNo()
		{
			return this.stageNo;
		}

		public override bool IsSatisfied(Simulator sim, World world)
		{
			int maxStageReached = world.GetMaxStageReached();
			return maxStageReached >= this.stageNo;
		}

		public override string GetReqString()
		{
			return string.Format(LM.Get("UNLOCK_REQ_STAGE"), this.stageNo.ToString());
		}

		public override string GetReqStringLessDetail()
		{
			return LM.Get("UNLOCK_REQ_LESS_STAGE");
		}

		public override string GetReqStringEvenLessDetail()
		{
			return LM.Get("UNLOCK_REQ_LEAST_STAGE");
		}

		public override int GetReqInt()
		{
			return this.stageNo;
		}

		public override string GetReqSatisfiedString()
		{
			return string.Format(LM.Get("UNLOCK_REQ_SATISFIED_STAGE"), this.stageNo.ToString());
		}

		private int stageNo;
	}
}
