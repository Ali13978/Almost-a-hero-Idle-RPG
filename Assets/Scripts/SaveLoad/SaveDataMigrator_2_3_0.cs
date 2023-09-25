using System;

namespace SaveLoad
{
	public class SaveDataMigrator_2_3_0 : SaveDataMigrator
	{
		public SaveDataMigrator_2_3_0()
		{
			this.comingVersion = new Version("2.3.0");
		}

		protected override SaveData Migrate(SaveData saveData)
		{
			if (saveData.notifs != -2147483648)
			{
				saveData.notifs |= 16;
			}
			return saveData;
		}
	}
}
