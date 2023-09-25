using System;

namespace Simulation
{
	public class UnlockReqHeroLevel : UnlockReq
	{
		public UnlockReqHeroLevel(string heroId, int reqLevel)
		{
			this.reqLevel = reqLevel;
			this.heroId = heroId;
		}

		public override bool IsSatisfied(Simulator sim, World world)
		{
			HeroDataBase heroDataBase = sim.GetAllHeroes().Find((HeroDataBase h) => h.id == this.heroId);
			return heroDataBase.evolveLevel >= this.reqLevel;
		}

		public override string GetReqString()
		{
			return string.Format("#Evolve hero to {0}", this.reqLevel);
		}

		public override string GetReqStringLessDetail()
		{
			return "DADADA";
		}

		public override string GetReqStringEvenLessDetail()
		{
			return "DADADA";
		}

		public override int GetReqInt()
		{
			return this.reqLevel;
		}

		public override string GetReqSatisfiedString()
		{
			return string.Format("#Evolve hero to {0}", this.reqLevel);
		}

		public string heroId;

		public int reqLevel;
	}
}
