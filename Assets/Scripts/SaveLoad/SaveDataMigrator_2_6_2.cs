using System;

namespace SaveLoad
{
	public class SaveDataMigrator_2_6_2 : SaveDataMigrator
	{
		public SaveDataMigrator_2_6_2()
		{
			this.comingVersion = new Version("2.6.2");
		}

		protected override SaveData Migrate(SaveData saveData)
		{
			saveData.usedTrinketExploit = (saveData.maxStagePrestigedAt >= 1200 || (saveData.worldsMaxStageReached != null && saveData.worldsMaxStageReached.ContainsKey("STANDARD") && saveData.worldsMaxStageReached["STANDARD"] >= 1200));
			return saveData;
		}
	}
}
