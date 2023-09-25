using System;
using Simulation;

namespace SaveLoad
{
	public class SaveDataMigrator_2_6_1 : SaveDataMigrator
	{
		public SaveDataMigrator_2_6_1()
		{
			this.comingVersion = new Version("2.6.1");
		}

		protected override SaveData Migrate(SaveData saveData)
		{
			if (saveData.flashOfferBundle != null && saveData.flashOfferBundle.adventureOffers != null)
			{
				FlashOffer flashOffer = saveData.flashOfferBundle.adventureOffers.Find((FlashOffer o) => o.type == FlashOffer.Type.TRINKET_PACK);
				if (flashOffer != null)
				{
					int num = 0;
					if (saveData.worldsMaxStageReached != null && saveData.worldsMaxStageReached.ContainsKey("STANDARD"))
					{
						num = saveData.worldsMaxStageReached["STANDARD"];
					}
					num = GameMath.GetMaxInt(saveData.maxStagePrestigedAt, num);
					int index = saveData.flashOfferBundle.adventureOffers.IndexOf(flashOffer);
					FlashOfferBundle flashOfferBundle = SaveLoadManager.ConvertFlashOfferBundle(saveData.flashOfferBundle, num);
					flashOfferBundle.adventureOffers.RemoveAt(index);
					flashOfferBundle.adventureOffers.Add(FlashOfferFactory.CreateRandomAdventureOffer(flashOfferBundle.adventureOffers, false, num));
					saveData.flashOfferBundle = SaveLoadManager.ConvertFlashOfferBundle(flashOfferBundle);
				}
			}
			if (saveData.numPrestiges > 1)
			{
				saveData.tutorialMissionIndex = TutorialMission.List.Length;
				TutorialManager.missionIndex = TutorialMission.List.Length;
			}
			return saveData;
		}
	}
}
