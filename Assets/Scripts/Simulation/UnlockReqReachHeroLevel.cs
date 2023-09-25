using System;

namespace Simulation
{
	public class UnlockReqReachHeroLevel : UnlockReq
	{
		public UnlockReqReachHeroLevel(int level)
		{
			this.level = level;
		}

		public int GetLevel()
		{
			return this.level;
		}

		public override bool IsSatisfied(Simulator sim, World world)
		{
			foreach (Hero hero in world.heroes)
			{
				if (hero.GetLevel() >= this.level)
				{
					return true;
				}
			}
			return false;
		}

		public override string GetReqString()
		{
			return string.Format(LM.Get("UNLOCK_REQ_HERO_LEVEL"), (this.level + 1).ToString());
		}

		public override string GetReqStringLessDetail()
		{
			return LM.Get("UNLOCK_REQ_LESS_HERO_LEVEL");
		}

		public override string GetReqStringEvenLessDetail()
		{
			return LM.Get("UNLOCK_REQ_LEAST_HERO_LEVEL");
		}

		public override int GetReqInt()
		{
			return this.level + 1;
		}

		public override string GetReqSatisfiedString()
		{
			return string.Format(LM.Get("UNLOCK_REQ_SATISFIED_HERO_LEVEL"), (this.level + 1).ToString());
		}

		private int level;
	}
}
