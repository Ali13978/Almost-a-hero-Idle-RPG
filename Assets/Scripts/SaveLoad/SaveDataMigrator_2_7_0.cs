using System;
using System.Collections.Generic;
using Simulation;

namespace SaveLoad
{
	public class SaveDataMigrator_2_7_0 : SaveDataMigrator
	{
		public SaveDataMigrator_2_7_0()
		{
			this.comingVersion = new Version("2.7.0");
		}

		protected override SaveData Migrate(SaveData saveData)
		{
			double num = 0.5;
			if (saveData.artifacts != null)
			{
				int i = 0;
				int count = saveData.artifacts.Count;
				while (i < count)
				{
					ArtifactSerializable artifactSerializable = saveData.artifacts[i];
					if (artifactSerializable.effectTypes != null)
					{
						int j = 0;
						int count2 = artifactSerializable.effectTypes.Count;
						while (j < count2)
						{
							int type = artifactSerializable.effectTypes[j];
							ArtifactEffectType artifactEffectTypeFromInt = SaveLoadManager.GetArtifactEffectTypeFromInt(type);
							if (artifactEffectTypeFromInt == ArtifactEffectType.GoldChest)
							{
								List<double> amounts;
								int index;
								(amounts = artifactSerializable.amounts)[index = j] = amounts[index] * num;
							}
							j++;
						}
					}
					i++;
				}
			}
			return saveData;
		}
	}
}
