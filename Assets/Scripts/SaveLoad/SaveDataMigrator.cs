using System;
using UnityEngine;

namespace SaveLoad
{
	public abstract class SaveDataMigrator
	{
		public SaveData BeginMigrate(SaveData saveData)
		{
			UnityEngine.Debug.Log("Migrating save data with version migrator: " + this.comingVersion);
			return this.Migrate(saveData);
		}

		protected abstract SaveData Migrate(SaveData saveData);

		public Version comingVersion;
	}
}
