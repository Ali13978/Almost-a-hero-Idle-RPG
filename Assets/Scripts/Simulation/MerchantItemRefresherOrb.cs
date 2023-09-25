using System;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class MerchantItemRefresherOrb : MerchantItem
	{
		public override string GetId()
		{
			return "REFRESHER_ORB";
		}

		public override string GetTitleString()
		{
			return LM.Get("MERCHANT_ITEM_REFRESHERORB");
		}

		public override string GetDescriptionString(Simulator sim, int count)
		{
			return string.Format(LM.Get("MERCHANT_ITEM_DESC_REFRESHERORB"), AM.csm(Mathf.RoundToInt(20f).ToString()), AM.csm(GameMath.GetTimeDetailedString((double)(10f * (float)count), true)));
		}

		public override void Apply(Simulator sim)
		{
			base.Apply(sim);
			World activeWorld = sim.GetActiveWorld();
			activeWorld.StartRefresherOrb(10f, 20f);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.merchantItemRefresherOrb, 1f));
		}

		public const float DUR = 10f;

		public const float REFRESH_MULT = 20f;

		public static int BASE_COUNT = 3;

		public static int BASE_COST = 10;
	}
}
