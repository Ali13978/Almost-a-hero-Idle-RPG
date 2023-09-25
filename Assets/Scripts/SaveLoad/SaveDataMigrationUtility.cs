using System;
using System.Collections.Generic;

namespace SaveLoad
{
	public static class SaveDataMigrationUtility
	{
		public static SaveData Migrate(SaveData saveData)
		{
			Version v = new Version(saveData.gameVersion);
			foreach (SaveDataMigrator saveDataMigrator in SaveDataMigrationUtility.allMigrators)
			{
				if (v < saveDataMigrator.comingVersion)
				{
					saveData = saveDataMigrator.BeginMigrate(saveData);
				}
			}
			return saveData;
		}

		private static List<SaveDataMigrator> allMigrators = new List<SaveDataMigrator>
		{
			new SaveDataMigrator_2_2_3(),
			new SaveDataMigrator_2_3_0(),
			new SaveDataMigrator_2_4_2(),
			new SaveDataMigrator_2_5_0(),
			new SaveDataMigrator_2_6_0(),
			new SaveDataMigrator_2_6_1(),
			new SaveDataMigrator_2_6_2(),
			new SaveDataMigrator_2_7_0(),
			new SaveDataMigrator_2_8_0(),
			new SaveDataMigrator_2_9_0(),
			new SaveDataMigrator_3_0_0()
		};
	}
}
