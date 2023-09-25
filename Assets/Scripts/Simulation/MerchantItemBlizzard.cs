using System;
using Ui;

namespace Simulation
{
	public class MerchantItemBlizzard : MerchantItem
	{
		public MerchantItemBlizzard()
		{
			this.level = 0;
		}

		public override string GetId()
		{
			return "BLIZZARD";
		}

		public override string GetTitleString()
		{
			return LM.Get("MERCHANT_ITEM_BLIZZARD_NAME");
		}

		public override string GetDescriptionString(Simulator sim, int count)
		{
			return string.Format(LM.Get("MERCHANT_ITEM_BLIZZARD_DESC"), AM.csm(GameMath.GetTimeLessDetailedString((double)(900f * (float)count), true)), AM.csm(GameMath.GetPercentString(0.5f, false)));
		}

		public override void Apply(Simulator sim)
		{
			base.Apply(sim);
			sim.GetActiveWorld().StartBlizzard(900f, 0.5f);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.merchantItemPowerUp, 1f));
		}

		public float GetDuration(Simulator sim)
		{
			return 900f;
		}

		public const float DUR = 900f;

		public const float SLOW_FACTOR = 0.5f;
	}
}
