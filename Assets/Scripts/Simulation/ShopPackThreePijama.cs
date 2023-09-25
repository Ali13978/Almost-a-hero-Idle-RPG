using System;
using System.Collections.Generic;
using Static;

namespace Simulation
{
	public class ShopPackThreePijama : ShopPack
	{
		public ShopPackThreePijama()
		{
			this.isOffer = true;
			this.name = "SHOP_PACK_THREE_PIJAMAS";
			this.isIAP = false;
			this.totalTime = 43200.0;
			this.currency = CurrencyType.GEM;
			this.cost = 1000.0;
			this.numSkins = 3;
		}

		public override bool CanAppear(Simulator sim)
		{
			bool flag = false;
			bool flag2 = sim.IsHeroUnlocked("THOUR") && sim.IsHeroUnlocked("IDA") && sim.IsHeroUnlocked("HORATIO");
			bool flag3 = sim.IsSkinBought(HeroSkins.BELLY_PIJAMA) || sim.IsSkinBought(HeroSkins.VEXX_PIJAMA) || sim.IsSkinBought(HeroSkins.HILT_PIJAMA);
			bool flag4 = sim.GetHeroDataBase("THOUR").evolveLevel >= 1 && sim.GetHeroDataBase("IDA").evolveLevel >= 1 && sim.GetHeroDataBase("HORATIO").evolveLevel >= 1;
			if (flag3)
			{
				return false;
			}
			foreach (HeroDataBase heroDataBase in sim.GetAllHeroes())
			{
				if (heroDataBase.evolveLevel >= this.appearLevel)
				{
					flag = true;
				}
			}
			return ShopPackStarter.appeared && flag && flag2 && flag4;
		}

		public override bool ShouldDiscard(Simulator sim)
		{
			return sim.IsSkinBought(HeroSkins.BELLY_PIJAMA) || sim.IsSkinBought(HeroSkins.VEXX_PIJAMA) || sim.IsSkinBought(HeroSkins.HILT_PIJAMA);
		}

		public override void OnAppeared()
		{
			base.OnAppeared();
			ShopPackThreePijama.appeared = true;
			PlayerStats.OnOfferOffered(OfferId.THREE_PIJAMA_PACK);
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.THREE_PIJAMA_PACK);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			PlayerStats.OnOfferAccepted(OfferId.THREE_PIJAMA_PACK);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return ShopPackThreePijama.appeared ? 5f : float.PositiveInfinity;
		}

		public override bool ShouldOverrideExisting(Simulator sim)
		{
			return !ShopPackThreePijama.appeared;
		}

		public override List<int> GetSkins()
		{
			return new List<int>
			{
				HeroSkins.VEXX_PIJAMA,
				HeroSkins.HILT_PIJAMA,
				HeroSkins.BELLY_PIJAMA
			};
		}

		public static bool appeared;

		private int appearLevel = 1;
	}
}
