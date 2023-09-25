using System;
using System.Collections.Generic;
using Static;

namespace SaveLoad
{
	public class SaveDataMigrator_2_5_0 : SaveDataMigrator
	{
		public SaveDataMigrator_2_5_0()
		{
			this.comingVersion = new Version("2.5.0");
		}

		protected override SaveData Migrate(SaveData saveData)
		{
			saveData.numCandyQuest = 0;
			if (saveData.achiColls != null)
			{
				foreach (KeyValuePair<int, bool> keyValuePair in saveData.achiColls)
				{
					if (keyValuePair.Value && saveData.achievements.ContainsKey(keyValuePair.Key))
					{
						saveData.achievements[keyValuePair.Key] = true;
						string id = SaveLoadManager.ConvertAchievementId(keyValuePair.Key);
						int achievementProgressTarget = PlayerStats.GetAchievementProgressTarget(id);
						int achievementProgressCurrent = PlayerStats.GetAchievementProgressCurrent(id, saveData);
						if (achievementProgressCurrent < achievementProgressTarget)
						{
							PlayerStats.SetAchievementProgressToTarget(id, saveData);
						}
					}
				}
			}
			if (saveData.numPrestiges > 0)
			{
				TutorialManager.missionIndex = TutorialMission.List.Length;
			}
			return saveData;
		}
	}
}
