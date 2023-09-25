using System;
using Ui;

namespace Simulation
{
	public class MerchantItemPowerUp : MerchantItem
	{
		public override string GetId()
		{
			return "POWER_UP";
		}

		public override string GetTitleString()
		{
			return LM.Get("MERCHANT_ITEM_POWERUP");
		}

		public override string GetDescriptionString(Simulator sim, int count)
		{
			return string.Format(LM.Get("MERCHANT_ITEM_DESC_POWERUP"), AM.csm(GameMath.GetPercentString(1.5, true)), AM.csm(GameMath.GetTimeDetailedString((double)(60f * (float)count), true)));
		}

		public override void Apply(Simulator sim)
		{
			base.Apply(sim);
			World activeWorld = sim.GetActiveWorld();
			activeWorld.StartPowerUp(60f, 1.5);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.merchantItemPowerUp, 1f));
		}

		public const float DUR = 60f;

		private const double DAMAGE_FACTOR_ADD = 1.5;

		public static int BASE_COUNT = 2;

		public static int BASE_COST = 10;
	}
}
