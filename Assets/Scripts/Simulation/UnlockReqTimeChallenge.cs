using System;

namespace Simulation
{
	public class UnlockReqTimeChallenge : UnlockReq
	{
		public UnlockReqTimeChallenge(ChallengeWithTime challenge)
		{
			this.challenge = challenge;
		}

		public override bool IsSatisfied(Simulator sim, World world)
		{
			return this.challenge.state == Challenge.State.WON;
		}

		public override string GetReqString()
		{
			if (this.challenge is TimeChallenge)
			{
				TimeChallenge timeChallenge = (TimeChallenge)this.challenge;
				return string.Format(LM.Get("UNLOCK_REQ_TIME_CHALLENGE"), timeChallenge.numWaves, GameMath.GetTimeLessDetailedString((double)timeChallenge.dur, true));
			}
			return LM.Get("UNLOCK_REQ_LESS_TIME_CHALLENGE");
		}

		public override string GetReqStringLessDetail()
		{
			return LM.Get("UNLOCK_REQ_LESS_TIME_CHALLENGE");
		}

		public override string GetReqStringEvenLessDetail()
		{
			return LM.Get("UNLOCK_REQ_LEAST_TIME_CHALLENGE");
		}

		public override int GetReqInt()
		{
			return -1;
		}

		public override string GetReqSatisfiedString()
		{
			if (this.challenge is TimeChallenge)
			{
				TimeChallenge timeChallenge = (TimeChallenge)this.challenge;
				return string.Format(LM.Get("UNLOCK_REQ_SATISFIED_TIME_CHALLENGE"), timeChallenge.numWaves, GameMath.GetTimeLessDetailedString((double)timeChallenge.dur, false));
			}
			return LM.Get("UNLOCK_REQ_LEAST_TIME_CHALLENGE");
		}

		public ChallengeWithTime challenge;
	}
}
