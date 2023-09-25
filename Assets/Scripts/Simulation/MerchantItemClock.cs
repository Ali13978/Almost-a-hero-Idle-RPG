using System;

namespace Simulation
{
	public class MerchantItemClock : MerchantItem
	{
		public override string GetId()
		{
			return "CLOCK";
		}

		public override string GetTitleString()
		{
			return LM.Get("MERCHANT_ITEM_CLOCK");
		}

		public override string GetDescriptionString(Simulator sim, int count)
		{
			return string.Format(LM.Get("MERCHANT_ITEM_DESC_CLOCK"), AM.csm(GameMath.GetPercentString(0.25f * (float)count, false)));
		}

		public override void Apply(Simulator sim)
		{
			base.Apply(sim);
			World activeWorld = sim.GetActiveWorld();
			ChallengeWithTime challengeWithTime = (ChallengeWithTime)activeWorld.activeChallenge;
			challengeWithTime.AddTimeShield(0.25f);
		}

		private const float TIME_ADD_RATIO = 0.25f;

		public const float DUR = 600f;

		public static int BASE_COUNT = 1;

		public static int BASE_COST = 25;
	}
}
