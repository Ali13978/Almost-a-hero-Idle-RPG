using System;

namespace Simulation
{
	public class UnlockReqRiftChallenge : UnlockReq
	{
		public UnlockReqRiftChallenge(ChallengeRift challenge)
		{
			this.challenge = challenge;
		}

		public override bool IsSatisfied(Simulator sim, World world)
		{
			return this.challenge.state == Challenge.State.WON;
		}

		public override string GetReqString()
		{
			if (this.challenge != null)
			{
				ChallengeRift challengeRift = this.challenge;
				return string.Format(LM.Get("UNLOCK_REQ_TIME_CHALLENGE"), challengeRift.targetTotWaveNo, GameMath.GetTimeLessDetailedString((double)challengeRift.dur, true));
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
			if (this.challenge != null)
			{
				ChallengeRift challengeRift = this.challenge;
				return string.Format(LM.Get("UNLOCK_REQ_SATISFIED_TIME_CHALLENGE"), challengeRift.targetTotWaveNo, GameMath.GetTimeLessDetailedString((double)challengeRift.dur, false));
			}
			return LM.Get("UNLOCK_REQ_LEAST_TIME_CHALLENGE");
		}

		public ChallengeRift challenge;
	}
}
