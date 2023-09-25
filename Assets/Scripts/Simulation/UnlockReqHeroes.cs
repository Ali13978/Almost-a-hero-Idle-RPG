using System;

namespace Simulation
{
	public class UnlockReqHeroes : UnlockReq
	{
		public UnlockReqHeroes(int numHeroes)
		{
			this.numHeroes = numHeroes;
		}

		public override bool IsSatisfied(Simulator sim, World world)
		{
			return sim.GetUnlockedHeroIds().Count >= this.numHeroes;
		}

		public override string GetReqString()
		{
			return string.Format(LM.Get("UNLOCK_REQ_HEROES"), this.numHeroes.ToString());
		}

		public override string GetReqStringLessDetail()
		{
			return LM.Get("UNLOCK_REQ_LESS_HEROES");
		}

		public override string GetReqStringEvenLessDetail()
		{
			return LM.Get("UNLOCK_REQ_LEAST_HEROES");
		}

		public override int GetReqInt()
		{
			return this.numHeroes;
		}

		public override string GetReqSatisfiedString()
		{
			return string.Format(LM.Get("UNLOCK_REQ_SATISFIED_HEROES"), this.numHeroes.ToString());
		}

		public int numHeroes;
	}
}
