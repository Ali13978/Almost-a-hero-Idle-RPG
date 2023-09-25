using System;
using Simulation;
using Static;

namespace SaveLoad
{
	public class SaveDataMigrator_2_6_0 : SaveDataMigrator
	{
		public SaveDataMigrator_2_6_0()
		{
			this.comingVersion = new Version("2.6.0");
		}

		protected override SaveData Migrate(SaveData saveData)
		{
			if (saveData.trinkets != null)
			{
				foreach (TrinketSerializable trinketSerializable in saveData.trinkets)
				{
					int num = -1;
					foreach (int index in trinketSerializable.effects)
					{
						TrinketEffect trinketEffect = TypeHelper.ConvertTrinketEffect(index);
						if (trinketEffect.GetRarity() > num)
						{
							num = trinketEffect.GetRarity();
						}
					}
					trinketSerializable.bodyColorIndex = num;
					TrinketUpgradeReq trinketUpgradeReq = SaveLoadManager.ConvertTrinketUpgradeReq(trinketSerializable.req);
					if (trinketUpgradeReq != null)
					{
						trinketSerializable.bodySpriteIndex = trinketUpgradeReq.GetBodySpriteIndex();
					}
				}
			}
			if (saveData.specialOfferBoard != null && saveData.specialOfferBoard.standardOffer.offerPack == 6)
			{
				saveData.specialOfferBoard.standardOffer = null;
			}
			saveData.installDate = DateTime.MinValue.Ticks;
			saveData.playerStatSpentCreditsFirstDay = -1.0;
			if (saveData.tutorialMissionIndex == 9)
			{
				saveData.tutorialMissionIndex = TutorialMission.List.Length;
			}
			return saveData;
		}
	}
}
