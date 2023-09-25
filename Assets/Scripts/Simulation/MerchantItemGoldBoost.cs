using System;
using Ui;

namespace Simulation
{
	public class MerchantItemGoldBoost : MerchantItem
	{
		public override string GetId()
		{
			return "GOLD_BOOST";
		}

		public override string GetTitleString()
		{
			return LM.Get("MERCHANT_ITEM_GOLD_BOOST");
		}

		public override string GetDescriptionString(Simulator sim, int count)
		{
			float horseshoeValueFactor = sim.GetActiveWorld().universalBonus.horseshoeValueFactor;
			float horseshoeDurationAdd = sim.GetActiveWorld().universalBonus.horseshoeDurationAdd;
			string str = string.Empty;
			string str2 = string.Empty;
			if (horseshoeValueFactor > 1f)
			{
				str = AM.csl(" (+" + GameMath.GetPercentString(horseshoeValueFactor - 1f, false) + ")");
			}
			if (horseshoeDurationAdd > 0f)
			{
				str2 = AM.csl(" (+" + GameMath.GetTimeDetailedString((double)(horseshoeDurationAdd * (float)count), true) + ")");
			}
			return string.Format(LM.Get("MERCHANT_ITEM_DESC_GOLD_BOOST"), AM.csm(GameMath.GetPercentString(2.0, true)) + str, AM.csm(GameMath.GetTimeDetailedString((double)(300f * (float)count), true)) + str2);
		}

		public override string GetSecondaryDescriptionString(Simulator sim)
		{
			return LM.Get("MERCHANT_ITEM_WARNING_TIMEWARP");
		}

		public override void Apply(Simulator sim)
		{
			base.Apply(sim);
			World activeWorld = sim.GetActiveWorld();
			activeWorld.StartGoldBoost(300f + activeWorld.universalBonus.horseshoeDurationAdd, 2.0 + (double)(activeWorld.universalBonus.horseshoeValueFactor - 1f));
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.merchantItemPowerUp, 1f));
		}

		public const float DUR = 300f;

		private const double GOLD_FACTOR_ADD = 2.0;

		public static int BASE_COUNT = 3;

		public static int BASE_COST = 30;
	}
}
