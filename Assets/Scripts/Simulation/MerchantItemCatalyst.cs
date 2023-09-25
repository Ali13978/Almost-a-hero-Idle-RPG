using System;
using Ui;

namespace Simulation
{
	public class MerchantItemCatalyst : MerchantItem
	{
		public override string GetId()
		{
			return "CHARM_CATALYST";
		}

		public override string GetTitleString()
		{
			return LM.Get("MERCHANT_ITEM_CATALYST");
		}

		public override string GetDescriptionString(Simulator sim, int count)
		{
			return string.Format(LM.Get("MERCHANT_ITEM_DESC_CATALYST"), AM.csm(GameMath.GetPercentString(0.5f, false)), AM.csm(GameMath.GetTimeInSecondsString(15f * (float)count)));
		}

		public override void Apply(Simulator sim)
		{
			base.Apply(sim);
			World activeWorld = sim.GetActiveWorld();
			activeWorld.StartCatalyst(15f, 0.5f);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.merchantItemPowerUp, 1f));
		}

		public const float PROGRESS_PER_SECOND = 0.5f;

		public const float DURATION = 15f;

		public static int BASE_COUNT = 4;

		public static int BASE_COST = 75;
	}
}
