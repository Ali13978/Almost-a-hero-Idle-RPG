using System;
using System.Collections.Generic;
using Simulation;

namespace SaveLoad
{
	public class SaveDataMigrator_2_2_3 : SaveDataMigrator
	{
		public SaveDataMigrator_2_2_3()
		{
			this.comingVersion = new Version("2.2.3");
		}

		protected override SaveData Migrate(SaveData saveData)
		{
			if (saveData.specialOfferBoard == null)
			{
				saveData.specialOfferBoard = new SpecialOfferBoardSerializable
				{
					standardOffer = new SpecialOfferKeeperSeralizable(),
					reAppearingUniqueOffer = new SpecialOfferKeeperSeralizable(),
					uniqueOffers = new List<SpecialOfferKeeperSeralizable>(),
					hasAllSeen = false
				};
			}
			ShopPack shopPack = SaveLoadManager.ConvertShopPack(saveData.shopPack);
			saveData.specialOfferBoard.previousStandardOffer = saveData.lastShopPackOffer;
			saveData.specialOfferBoard.standardOffer.nextOfferApperTime = saveData.lastOfferEndTime + SpecialOfferBoard.StandardOfferReAppearInterval.Ticks;
			switch (saveData.shopPack)
			{
			case 1:
			case 8:
			case 9:
			case 10:
			case 11:
			case 12:
			{
				SpecialOfferKeeperSeralizable specialOfferKeeperSeralizable = new SpecialOfferKeeperSeralizable();
				saveData.specialOfferBoard.uniqueOffers.Add(specialOfferKeeperSeralizable);
				specialOfferKeeperSeralizable.offerPack = saveData.shopPack;
				if (shopPack != null)
				{
					specialOfferKeeperSeralizable.offerEndTime = saveData.shopPackTime + TimeSpan.FromSeconds(shopPack.totalTime).Ticks;
				}
				break;
			}
			case 2:
			case 3:
			case 4:
			case 6:
				saveData.specialOfferBoard.standardOffer.offerPack = saveData.shopPack;
				if (shopPack != null)
				{
					saveData.specialOfferBoard.standardOffer.offerEndTime = saveData.shopPackTime + TimeSpan.FromSeconds(shopPack.totalTime).Ticks;
				}
				break;
			}
			if (saveData.iapsMade != null)
			{
				saveData.spbgp = (saveData.iapsMade.Count > 9 && saveData.iapsMade[9] > 0);
				saveData.spbgtp = (saveData.iapsMade.Count > 10 && saveData.iapsMade[10] > 0);
			}
			return saveData;
		}
	}
}
