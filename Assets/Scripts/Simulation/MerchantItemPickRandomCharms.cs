using System;
using Ui;

namespace Simulation
{
	public class MerchantItemPickRandomCharms : MerchantItem
	{
		public override string GetId()
		{
			return "PICK_RANDOM_CHARMS";
		}

		public override string GetTitleString()
		{
			return LM.Get("MERCHANT_ITEM_PICK_RANDOM_CHARMS");
		}

		public override string GetDescriptionString(Simulator sim, int count)
		{
			return LM.Get("MERCHANT_ITEM_DESC_PICK_RANDOM_CHARMS");
		}

		public override string GetSecondaryDescriptionString(Simulator sim)
		{
			return LM.Get("MERCHANT_ITEM_DESC_PICK_RANDOM_CHARMS_2");
		}

		public override void Apply(Simulator sim)
		{
			base.Apply(sim);
			World activeWorld = sim.GetActiveWorld();
			activeWorld.EnableRandomCharmsPicker();
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.merchantItemPowerUp, 1f));
		}

		public static int BASE_COUNT = 1;

		public static int BASE_COST = 25;
	}
}
