using System;
using Ui;

namespace Simulation
{
	public class MerchantItemAutoTap : MerchantItem
	{
		public override string GetId()
		{
			return "AUTO_TAP";
		}

		public override string GetTitleString()
		{
			return LM.Get("MERCHANT_ITEM_AUTOTAP");
		}

		public override string GetDescriptionString(Simulator sim, int count)
		{
			string str = string.Empty;
			float num = sim.GetActiveWorld().universalBonus.autoTapDurationAdd * (float)count;
			if (num > 0f)
			{
				str = AM.csl(" (+" + GameMath.GetTimeDetailedString((double)num, true) + ")");
			}
			return string.Format(LM.Get("MERCHANT_ITEM_DESC_AUTOTAP0"), AM.csm(GameMath.GetTimeDetailedString((double)(300f * (float)count), true))) + " " + System.Environment.NewLine + str;
		}

		public override string GetSecondaryDescriptionString(Simulator sim)
		{
			return LM.Get("MERCHANT_ITEM_WARNING_TIMEWARP");
		}

		public override void Apply(Simulator sim)
		{
			base.Apply(sim);
			World activeWorld = sim.GetActiveWorld();
			activeWorld.StartAutoTap(this.GetDuration(sim));
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.merchantItemAutoTap, 1f));
		}

		public float GetDuration(Simulator sim)
		{
			return 300f + sim.GetActiveWorld().universalBonus.autoTapDurationAdd;
		}

		public const float DUR = 300f;

		public static int BASE_COUNT = 5;

		public static int BASE_COST = 5;
	}
}
