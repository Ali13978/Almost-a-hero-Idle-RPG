using System;

namespace Simulation
{
	public class MerchantItemShield : MerchantItem
	{
		public override string GetId()
		{
			return "SHIELD";
		}

		public override string GetTitleString()
		{
			return LM.Get("MERCHANT_ITEM_SHIELD");
		}

		public override string GetDescriptionString(Simulator sim, int count)
		{
			float num = sim.GetActiveWorld().universalBonus.shieldDurationAdd * (float)count;
			string str = string.Empty;
			if (num > 0f)
			{
				str = AM.csl(" (+" + GameMath.GetTimeDetailedString((double)num, true) + ")");
			}
			return string.Format(LM.Get("MERCHANT_ITEM_DESC_SHIELD"), AM.csm(GameMath.GetPercentString(10f, true)), AM.csm(GameMath.GetTimeDetailedString((double)(30f * (float)count), true)) + str);
		}

		public override string GetSecondaryDescriptionString(Simulator sim)
		{
			return LM.Get("MERCHANT_ITEM_WARNING_TIMEWARP");
		}

		public override void Apply(Simulator sim)
		{
			base.Apply(sim);
			sim.GetActiveWorld().StartShield(30f + sim.GetActiveWorld().universalBonus.shieldDurationAdd);
		}

		public const float DURATION = 30f;

		public const float REVIVE_DUR_SPEED_FACTOR = 10f;

		public static int BASE_COUNT = 5;

		public static int BASE_COST = 30;
	}
}
