using System;

namespace Simulation
{
	public class FlashOffer
	{
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			FlashOffer flashOffer = obj as FlashOffer;
			return flashOffer != null && this.AreEquals(flashOffer, false);
		}

		public bool IsCurrencyOffer()
		{
			return this.type == FlashOffer.Type.GEM || this.type == FlashOffer.Type.SCRAP || this.type == FlashOffer.Type.TOKEN;
		}

		public bool AreEquals(FlashOffer offer, bool ignoreId)
		{
			if (offer.type != this.type || offer.costType != this.costType || offer.tier != this.tier)
			{
				return false;
			}
			switch (this.type)
			{
			case FlashOffer.Type.CHARM:
				return ignoreId || offer.genericIntId == this.genericIntId;
			case FlashOffer.Type.SCRAP:
			case FlashOffer.Type.TOKEN:
				return offer.genericIntId == this.genericIntId;
			case FlashOffer.Type.GEM:
			case FlashOffer.Type.TRINKET_PACK:
				return true;
			case FlashOffer.Type.RUNE:
			case FlashOffer.Type.MERCHANT_ITEM:
				return ignoreId || offer.genericStringId == this.genericStringId;
			case FlashOffer.Type.COSTUME:
			case FlashOffer.Type.COSTUME_PLUS_SCRAP:
				return ignoreId || offer.genericIntId == this.genericIntId;
			default:
				throw new NotImplementedException("Flash offer comparison not implement for type " + this.type);
			}
		}

		public bool PurchaseRequiresCurrency(CurrencyType currencyType)
		{
			FlashOffer.CostType costType = this.costType;
			if (costType != FlashOffer.CostType.CANDY)
			{
				return costType == FlashOffer.CostType.GEM && currencyType == CurrencyType.GEM;
			}
			return currencyType == CurrencyType.CANDY;
		}

		public FlashOffer Clone()
		{
			return new FlashOffer
			{
				type = this.type,
				costType = this.costType,
				charmId = this.charmId,
				genericIntId = this.genericIntId,
				genericStringId = this.genericStringId,
				isBought = this.isBought,
				boughtAmount = this.boughtAmount,
				purchasesLeft = this.purchasesLeft,
				tier = this.tier,
				isHalloween = this.isHalloween,
				isCrhistmas = this.isCrhistmas,
				isAnniverary = this.isAnniverary
			};
		}

		public FlashOffer.Type type;

		public FlashOffer.CostType costType;

		public int charmId;

		public int genericIntId;

		public string genericStringId;

		public bool isBought;

		public int boughtAmount;

		public int purchasesLeft = 1;

		public int tier;

		public bool isHalloween;

		public bool isAnniverary;

		public bool isCrhistmas;

		public enum Type
		{
			CHARM,
			SCRAP,
			GEM,
			TOKEN,
			RUNE,
			COSTUME,
			TRINKET_PACK,
			COSTUME_PLUS_SCRAP,
			MERCHANT_ITEM
		}

		public enum CostType
		{
			GEM,
			AD,
			FREE,
			CANDY
		}
	}
}
