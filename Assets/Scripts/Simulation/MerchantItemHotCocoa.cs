using System;
using Ui;

namespace Simulation
{
	public class MerchantItemHotCocoa : MerchantItem
	{
		public MerchantItemHotCocoa()
		{
			this.level = 0;
		}

		public override string GetId()
		{
			return "HOT_COCOA";
		}

		public override string GetTitleString()
		{
			return LM.Get("MERCHANT_ITEM_HOT_COCOA_NAME");
		}

		public override string GetDescriptionString(Simulator sim, int count)
		{
			return string.Format(LM.Get("MERCHANT_ITEM_HOT_COCOA_DESC"), AM.csm(GameMath.GetPercentString(1f, false)), AM.csm(GameMath.GetPercentString(2f, false)), AM.csm(GameMath.GetTimeLessDetailedString((double)(360f * (float)count), true)));
		}

		public override void Apply(Simulator sim)
		{
			base.Apply(sim);
			sim.GetActiveWorld().StartHotCocoa(360f, 1f, 2f);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.merchantItemPowerUp, 1f));
		}

		public float GetDuration(Simulator sim)
		{
			return 360f;
		}

		public const float DUR = 360f;

		public const float COOLDOWN_FACTOR = 1f;

		public const float DAMAGE_FACTOR = 2f;
	}
}
