using System;

namespace SaveLoad
{
	public class SaveDataMigrator_2_8_0 : SaveDataMigrator
	{
		public SaveDataMigrator_2_8_0()
		{
			this.comingVersion = new Version("2.8.0");
		}

		protected override SaveData Migrate(SaveData saveData)
		{
			if (saveData.notifs != -2147483648)
			{
				saveData.notifs |= 64;
			}
			return saveData;
		}
	}
}
