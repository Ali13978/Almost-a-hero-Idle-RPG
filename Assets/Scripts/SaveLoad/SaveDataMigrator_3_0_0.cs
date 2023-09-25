using System;
using System.Collections.Generic;
using Simulation;

namespace SaveLoad
{
	public class SaveDataMigrator_3_0_0 : SaveDataMigrator
	{
		public SaveDataMigrator_3_0_0()
		{
			this.comingVersion = new Version("3.0.0");
		}

		protected override SaveData Migrate(SaveData saveData)
		{
			string idFromMode = World.GetIdFromMode(GameMode.STANDARD);
			if (saveData.worldsGold.ContainsKey(idFromMode))
			{
				saveData.worldsGold[idFromMode] = 0.0;
			}
			saveData.voicesMute = saveData.soundsMute;
			saveData.stageRearrangeSurviver = (saveData.collectedUnlockIds != null && saveData.collectedUnlockIds.Count > 0);
			saveData.playerStatLifeTimeInTicks = (long)saveData.playerStatLifeTime * 10000000L;
			saveData.playerStatLifeTimeInTicksInCurrentSaveFile = ((saveData.playerStatLifeTimeInCurrentSaveFile != 0f) ? ((long)saveData.playerStatLifeTimeInCurrentSaveFile * 10000000L) : saveData.playerStatLifeTimeInTicks);
			if (saveData.disassembledTrinketEffects != null)
			{
				this.CollapseMigratedTrinketEffects(saveData, 4, new int[]
				{
					52
				});
				this.CollapseMigratedTrinketEffects(saveData, 60, new int[]
				{
					3,
					59
				});
				this.CollapseMigratedTrinketEffects(saveData, 62, new int[]
				{
					1,
					61
				});
			}
			if (saveData.tutorialMissionIndex >= 20)
			{
				saveData.tutorialMissionIndex = TutorialMission.List.Length;
			}
			if (saveData.notifs > 0)
			{
				saveData.notifs |= 128;
			}
			return saveData;
		}

		private void CollapseMigratedTrinketEffects(SaveData saveData, int originalEffectId, params int[] refactoredEffectsId)
		{
			foreach (int key in refactoredEffectsId)
			{
				if (saveData.disassembledTrinketEffects.ContainsKey(key))
				{
					if (saveData.disassembledTrinketEffects.ContainsKey(originalEffectId))
					{
						Dictionary<int, int> disassembledTrinketEffects;
						(disassembledTrinketEffects = saveData.disassembledTrinketEffects)[originalEffectId] = disassembledTrinketEffects[originalEffectId] + saveData.disassembledTrinketEffects[key];
					}
					else
					{
						saveData.disassembledTrinketEffects.Add(originalEffectId, saveData.disassembledTrinketEffects[key]);
					}
					saveData.disassembledTrinketEffects.Remove(key);
				}
			}
		}
	}
}
