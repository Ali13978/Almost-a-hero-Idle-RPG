using System;
using Ui;

namespace Simulation
{
	public class MerchantItemTimeWarp : MerchantItem
	{
		public override string GetId()
		{
			return "WARP_TIME";
		}

		public override string GetTitleString()
		{
			return LM.Get("MERCHANT_ITEM_TIMEWARP");
		}

		public override string GetDescriptionString(Simulator sim, int count)
		{
			float timeWarpSpeedFactor = sim.GetActiveWorld().universalBonus.timeWarpSpeedFactor;
			float num = sim.GetActiveWorld().universalBonus.timeWarpDurationAdd * (float)count;
			string str = string.Empty;
			string str2 = string.Empty;
			if (timeWarpSpeedFactor > 1f)
			{
				str = AM.csl(" (+" + GameMath.GetPercentString((timeWarpSpeedFactor - 1f) * 2.5f, true) + ")");
			}
			if (num > 0f)
			{
				str2 = AM.csl(" (+" + GameMath.GetTimeDetailedString((double)num, true) + ")");
			}
			return string.Format(LM.Get("MERCHANT_ITEM_DESC_TIMEWARP"), AM.csm(GameMath.GetPercentString(2.5f, false)) + str, AM.csm(GameMath.GetTimeDetailedString((double)(150f * (float)count), true)) + str2);
		}

		public override void Apply(Simulator sim)
		{
			base.Apply(sim);
			World activeWorld = sim.GetActiveWorld();
			activeWorld.StartTimeWarp(150f + activeWorld.universalBonus.timeWarpDurationAdd, 2.5f * activeWorld.universalBonus.timeWarpSpeedFactor);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.merchantItemTimeWarp, 1f));
		}

		public const float TIME_WARP_DUR = 150f;

		public const float TIME_WARP_SPEED = 2.5f;

		public static int BASE_COUNT = 3;

		public static int BASE_COST = 20;
	}
}
