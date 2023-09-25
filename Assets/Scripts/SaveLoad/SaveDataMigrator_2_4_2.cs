using System;
using System.Collections.Generic;

namespace SaveLoad
{
	public class SaveDataMigrator_2_4_2 : SaveDataMigrator
	{
		public SaveDataMigrator_2_4_2()
		{
			this.comingVersion = new Version("2.4.2");
		}

		protected override SaveData Migrate(SaveData saveData)
		{
			saveData.trinketsPinned = new List<int>();
			return saveData;
		}
	}
}
