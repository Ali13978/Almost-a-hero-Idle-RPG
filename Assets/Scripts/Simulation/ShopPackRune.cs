using System;
using System.Collections.Generic;
using Static;

namespace Simulation
{
	public class ShopPackRune : ShopPack
	{
		public ShopPackRune()
		{
			this.tags = ShopPack.Tags.STANDARD;
			this.isOffer = true;
			this.name = "SHOP_PACK_RUNE";
			this.isIAP = false;
			this.currency = CurrencyType.GEM;
			this.cost = 1000.0;
			this.runeChance = 1f;
			this.numRunes = 3;
			this.totalTime = 43200.0;
		}

		public override bool CanAppear(Simulator sim)
		{
			List<Rune> allBuyableRunes = sim.GetAllBuyableRunes();
			return allBuyableRunes.Count >= 3 && ShopPackStarter.appeared;
		}

		public override void OnAppeared()
		{
			base.OnAppeared();
			PlayerStats.OnOfferOffered(OfferId.RUNE_PACK);
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.RUNE_PACK);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			PlayerStats.OnOfferAccepted(OfferId.RUNE_PACK);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			int count = sim.GetAllBuyableRunes().Count;
			if (count > 20)
			{
				return 15f;
			}
			if (count > 15)
			{
				return 10f;
			}
			if (count > 8)
			{
				return 5f;
			}
			return 1f;
		}
	}
}
