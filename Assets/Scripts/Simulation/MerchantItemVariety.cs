using System;
using Ui;

namespace Simulation
{
	public class MerchantItemVariety : MerchantItem
	{
		public override string GetId()
		{
			return "CHARM_VARIETY";
		}

		public override string GetTitleString()
		{
			return LM.Get("MERCHANT_ITEM_VARIETY");
		}

		public override string GetDescriptionString(Simulator sim, int count)
		{
			return string.Format(LM.Get("MERCHANT_ITEM_DESC_VARIETY"), AM.csm("4"), AM.csm("3"));
		}

		public override void Apply(Simulator sim)
		{
			base.Apply(sim);
			World activeWorld = sim.GetActiveWorld();
			activeWorld.StartVariety();
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.merchantItemPowerUp, 1f));
		}

		public static int BASE_COUNT = 1;

		public static int BASE_COST = 100;
	}
}
