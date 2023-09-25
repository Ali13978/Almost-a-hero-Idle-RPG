using System;
using Ui;

namespace Simulation
{
	public class MerchantItemEmergencyCharm : MerchantItem
	{
		public override string GetId()
		{
			return "EMERGENCY_CHARM";
		}

		public override string GetTitleString()
		{
			return LM.Get("MERCHANT_ITEM_EMERGENCY_CHARM");
		}

		public override string GetDescriptionString(Simulator sim, int count)
		{
			return LM.Get("MERCHANT_ITEM_DESC_EMERGENCY_CHARM");
		}

		public override void Apply(Simulator sim)
		{
			World activeWorld = sim.GetActiveWorld();
			activeWorld.StartEmergencyCharm();
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.merchantItemPowerUp, 1f));
		}

		public static int BASE_COUNT = 5;

		public static int BASE_COST = 50;
	}
}
