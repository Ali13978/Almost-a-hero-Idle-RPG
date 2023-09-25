using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SaveLoad;
using Simulation;
using Simulation.ArtifactSystem;
using Static;
using stats;
using Ui;
using UnityEngine;

public static class SaveLoadManager
{
	public static string GetJsonOfCurrentState(Simulator sim, bool hasEverHadPlayfabTime, DateTime lastOfflineEarnedTime)
	{
		SaveData value = SaveLoadManager.GenerateSaveData(sim, hasEverHadPlayfabTime, lastOfflineEarnedTime);
		return JsonConvert.SerializeObject(value);
	}

	public static bool HasSave()
	{
		return PlayerPrefs.HasKey("save_data");
	}

	public static bool CanSaveCloud()
	{
		return !SaveLoadManager.cloudSaveMustBeBackedUp;
	}

	public static void MigrationBackupLocalSave()
	{
		string @string = PlayerPrefs.GetString("save_data", "NAN");
		SaveLoadManager.currentBackupIndex = PlayerPrefs.GetInt("migration_backup_index", 0);
		if (@string != "NAN")
		{
			string key = "migration_backup_save_data_" + SaveLoadManager.currentBackupIndex;
			PlayerPrefs.SetString(key, @string);
			SaveLoadManager.currentBackupIndex++;
			if (SaveLoadManager.currentBackupIndex > 5)
			{
				SaveLoadManager.currentBackupIndex = 0;
			}
			PlayerPrefs.SetInt("migration_backup_index", SaveLoadManager.currentBackupIndex);
			UnityEngine.Debug.Log("Did migration backup locally");
		}
	}

	public static void Save(Simulator sim, bool hasEverHadPlayfabTime, DateTime lastOfflineEarnedTime)
	{
        Debug.Log("!!!!!!!!!!!!!!!! save");
		PlayerPrefs.SetInt("migration_backup_flag", (!SaveLoadManager.cloudSaveMustBeBackedUp) ? 0 : 1);
		if (SaveLoadManager.loadingSaveFailed)
		{
			UnityEngine.Debug.LogWarning("Abording save process because an error happened during load process");
			return;
		}
		SaveData saveData = SaveLoadManager.GenerateSaveData(sim, hasEverHadPlayfabTime, lastOfflineEarnedTime);
		PlayerPrefs.SetInt("MaxStageReached", sim.GetStandardMaxStageReached());
		if (saveData.unlockedTotemIds.Count == 0)
		{
			return;
		}
		string text = JsonConvert.SerializeObject(saveData);
		string value = string.Empty;
		try
		{
			value = SaveLoadManager.EncodeString(text);
			PlayerPrefs.SetString("save_data", value);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.ToString());
			PlayerPrefs.SetString("save_data", text);
		}
		PlayerPrefs.SetString("CurrentPlayfabId", PlayfabManager.playerId);
		PlayerPrefs.SetInt("CurrentLang", SaveLoadManager.ConvertLanguage(LM.currentLanguage));
		Delegates.PlayfabSaveEventArgs @object = new Delegates.PlayfabSaveEventArgs
		{
			sim = sim,
			saveData = saveData
		};
		if (SaveLoadManager.CanSaveCloud())
		{
			PlayfabManager.Save(text, new Action(@object.OnSuccess));
		}
		else
		{
			UnityEngine.Debug.LogWarning("Cloud save is blocked due to migration backup system. App should backup cloud save before overriding it");
			UnityEngine.Debug.LogWarning("Initiating cloud save backup");
			if (PlayfabManager.HaveLoggedIn())
			{
				PlayfabManager.BackupSaveData(delegate(bool success)
				{
					if (success)
					{
						SaveLoadManager.cloudSaveMustBeBackedUp = false;
					}
				});
			}
			else
			{
				StoreManager.Authenticate(false, delegate
				{
					if (PlayfabManager.HaveLoggedIn())
					{
						PlayfabManager.BackupSaveData(delegate(bool success)
						{
							if (success)
							{
								SaveLoadManager.cloudSaveMustBeBackedUp = false;
							}
						});
					}
				});
			}
		}
		AdjustTracker.TrackLevelProgress(sim.numPrestiges, sim.GetWorld(GameMode.STANDARD).GetMaxStageReached());
	}

	public static SaveData Load(string json)
	{
		SaveData saveData = null;
		try
		{
			saveData = JsonConvert.DeserializeObject<SaveData>(json);
			saveData = SaveLoadManager.MigrateSaveData(saveData);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("Save file is not encoded");
			UnityEngine.Debug.Log(ex.ToString());
			throw ex;
		}
		return saveData;
	}

	public static SaveData LoadWithoutMigrating(string json)
	{
		SaveData result = null;
		try
		{
			result = JsonConvert.DeserializeObject<SaveData>(json);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.ToString());
			throw ex;
		}
		return result;
	}

	public static string LoadSaveJson()
	{
		string @string = PlayerPrefs.GetString("save_data", "unsetSaveData");
		if (@string != "unsetSaveData")
		{
			return @string;
		}
		return null;
	}

	public static SaveData Load(bool migrate)
	{

		SaveLoadManager.cloudSaveMustBeBackedUp = (PlayerPrefs.GetInt("migration_backup_flag", 0) == 1);
		UiManager.stateJustChanged = true;
		string @string = PlayerPrefs.GetString("save_data", "unsetSaveData");
		if (@string == "unsetSaveData")
		{
			return null;
		}

		SaveData saveData = null;
		try
		{
			string value = SaveLoadManager.DecodeString(@string);
			saveData = JsonConvert.DeserializeObject<SaveData>(value);
			if (migrate)
			{
				saveData = SaveLoadManager.MigrateSaveData(saveData);
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("Save file is not encoded");
			UnityEngine.Debug.Log(ex.ToString());
			saveData = JsonConvert.DeserializeObject<SaveData>(@string);
		}
		return saveData;
	}

	public static SaveData GenerateSaveData(Simulator sim, bool hasEverHadPlayfabTime, DateTime lastOfflineEarnedTime)
	{
		if (sim == null)
		{
			return null;
		}
		SaveData saveData = new SaveData();
		saveData.gameVersion = Cheats.version;
		saveData.saveTime = GameMath.GetNow().Ticks;
		saveData.hasEverHadPlayfabTime = hasEverHadPlayfabTime;
		saveData.lastOfflineEarnedTime = lastOfflineEarnedTime.Ticks;
		saveData.languageSelected = SaveLoadManager.ConvertLanguage(LM.currentLanguage);
		saveData.currentGameMode = SaveLoadManager.ConvertGameMode(sim.GetCurrentGameMode());
		saveData.numPrestiges = sim.numPrestiges;
		saveData.isMerchantUnlocked = sim.IsMerchantUnlocked();
		saveData.hasCompass = sim.hasCompass;
		saveData.hasDailies = sim.hasDailies;
		saveData.hasSkillPointsAutoDistribution = sim.hasSkillPointsAutoDistribution;
		saveData.mythstones = sim.GetMythstones().GetAmount();
		saveData.scraps = sim.GetScraps().GetAmount();
		saveData.credits = sim.GetCredits().GetAmount();
		saveData.tokens = sim.GetTokens().GetAmount();
		saveData.aeon = sim.GetAeons().GetAmount();
		saveData.candies = sim.GetCandies().GetAmount();
		saveData.lastCandyAmountCapDailyReset = sim.lastCandyAmountCapDailyReset;
		saveData.candyAmountCollectedSinceLastDailyCapReset = sim.candyAmountCollectedSinceLastDailyCapReset;
		saveData.lootpackFreeLastOpenTime = sim.lootpackFreeLastOpenTime.Ticks;
		saveData.lootpackFreeLastOpenTimeServer = sim.lootpackFreeLastOpenTimeServer.Ticks;
		saveData.lastCappedWatchedTime = sim.GetLastCappedCurrencyWatchedTime(CurrencyType.GEM).Ticks;
		saveData.lastCappedCandiesWatchedTime = sim.GetLastCappedCurrencyWatchedTime(CurrencyType.CANDY).Ticks;
		saveData.collectedUnlockIds = sim.GetCollectedUnlockIds();
		saveData.unlockedHeroIds = sim.GetUnlockedHeroIds();
		saveData.unlockedTotemIds = sim.GetUnlockedTotemIds();
		saveData.numArtifactSlots = sim.artifactsManager.AvailableSlotsCount;
		saveData.artifacts = new List<ArtifactSerializable>();
		saveData.artifactForcedDisableds = new HashSet<int>();
		saveData.cardPackCounters = CharmDataBase.NotOpenedCounters;
		foreach (Simulation.Artifact artifact in sim.artifactsManager.OldArtifacts)
		{
			if (!artifact.IsEnabled())
			{
				saveData.artifactForcedDisableds.Add(saveData.artifacts.Count);
			}
			saveData.artifacts.Add(SaveLoadManager.ConvertArtifact(artifact));
		}
		saveData.numArtifactSlotsMythical = sim.artifactsManager.NumArtifactSlotsMythical;
		saveData.boughtGearLevels = new Dictionary<string, int>();
		foreach (Gear gear in sim.GetBoughtGears())
		{
			saveData.boughtGearLevels.Add(gear.GetId(), gear.level);
		}
		saveData.boughtRunes = new HashSet<string>();
		foreach (Rune rune in sim.GetBoughtRunes())
		{
			saveData.boughtRunes.Add(rune.id);
		}
		saveData.wornRunes = new Dictionary<string, HashSet<string>>();
		foreach (KeyValuePair<string, List<Rune>> keyValuePair in sim.GetWornRunes())
		{
			string key = keyValuePair.Key;
			HashSet<string> hashSet = new HashSet<string>();
			saveData.wornRunes.Add(key, hashSet);
			foreach (Rune rune2 in keyValuePair.Value)
			{
				hashSet.Add(rune2.id);
			}
		}
		saveData.newHeroIconSelectedHeroIds = new HashSet<string>();
		foreach (string item in sim.newHeroIconSelectedHeroIds)
		{
			saveData.newHeroIconSelectedHeroIds.Add(item);
		}
		List<World> allWorlds = sim.GetAllWorlds();
		saveData.unlockedWorldIds = new HashSet<string>();
		foreach (World world in allWorlds)
		{
			if (world.IsModeUnlocked())
			{
				saveData.unlockedWorldIds.Add(world.GetId());
			}
		}
		saveData.worldsChallengeStates = new Dictionary<string, int>();
		foreach (World world2 in allWorlds)
		{
			saveData.worldsChallengeStates.Add(world2.GetId(), SaveLoadManager.ConvertChallengeState(world2.activeChallenge.state));
		}
		saveData.worldsChallengeTimePassed = new Dictionary<string, float>();
		foreach (World world3 in allWorlds)
		{
			if (world3.activeChallenge is ChallengeWithTime)
			{
				float timeCounter = ((ChallengeWithTime)world3.activeChallenge).timeCounter;
				saveData.worldsChallengeTimePassed.Add(world3.GetId(), timeCounter);
			}
		}
		saveData.worldsGold = new Dictionary<string, double>();
		foreach (World world4 in allWorlds)
		{
			saveData.worldsGold.Add(world4.GetId(), world4.gold.GetAmount());
		}
		saveData.worldsOfflineGold = new Dictionary<string, double>();
		foreach (World world5 in allWorlds)
		{
			saveData.worldsOfflineGold.Add(world5.GetId(), world5.offlineGold);
		}
		saveData.worldsMaxStageReached = new Dictionary<string, int>();
		foreach (World world6 in allWorlds)
		{
			saveData.worldsMaxStageReached.Add(world6.GetId(), world6.GetMaxStageReached());
		}
		saveData.worldsMaxHeroLevelReached = new Dictionary<string, int>();
		foreach (World world7 in allWorlds)
		{
			saveData.worldsMaxHeroLevelReached.Add(world7.GetId(), world7.GetMaxHeroLevelReached());
		}
		saveData.worldsBoughtTotems = new Dictionary<string, string>();
		foreach (World world8 in allWorlds)
		{
			if (world8.totem != null)
			{
				saveData.worldsBoughtTotems.Add(world8.GetId(), world8.totem.GetId());
			}
		}
		saveData.worldsBoughtHeroes = new Dictionary<string, List<string>>();
		foreach (World world9 in allWorlds)
		{
			List<string> list = new List<string>();
			saveData.worldsBoughtHeroes.Add(world9.GetId(), list);
			foreach (Hero hero in world9.heroes)
			{
				list.Add(hero.GetId());
			}
		}
		saveData.worldsDuplicatedHeroes = new Dictionary<string, List<bool>>();
		foreach (World world10 in allWorlds)
		{
			List<bool> list2 = new List<bool>();
			saveData.worldsDuplicatedHeroes.Add(world10.GetId(), list2);
			foreach (Hero hero2 in world10.heroes)
			{
				list2.Add(hero2.IsDuplicate());
			}
		}
		saveData.merchantItemLevels = new Dictionary<string, int>();
		foreach (World world11 in allWorlds)
		{
			foreach (Simulation.MerchantItem merchantItem in world11.merchantItems)
			{
				saveData.merchantItemLevels[merchantItem.GetId()] = merchantItem.GetLevelForLoading();
			}
		}
		saveData.eventMerchantItemLevels = new Dictionary<string, int>();
		foreach (World world12 in allWorlds)
		{
			if (world12.eventMerchantItems != null)
			{
				foreach (Simulation.MerchantItem merchantItem2 in world12.eventMerchantItems)
				{
					saveData.eventMerchantItemLevels[merchantItem2.GetId()] = merchantItem2.GetLevelForLoading();
				}
			}
		}
		saveData.worldsNumMerchantItemsUsed = new Dictionary<string, Dictionary<string, int>>();
		foreach (World world13 in allWorlds)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			saveData.worldsNumMerchantItemsUsed.Add(world13.GetId(), dictionary);
			foreach (Simulation.MerchantItem merchantItem3 in world13.merchantItems)
			{
				dictionary.Add(merchantItem3.GetId(), merchantItem3.GetNumUsed());
			}
		}
		saveData.worldsNumMerchantItemsInInventory = new Dictionary<string, Dictionary<string, int>>();
		foreach (World world14 in allWorlds)
		{
			Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
			saveData.worldsNumMerchantItemsInInventory.Add(world14.GetId(), dictionary2);
			foreach (Simulation.MerchantItem merchantItem4 in world14.merchantItems)
			{
				dictionary2.Add(merchantItem4.GetId(), merchantItem4.GetNumInInventory());
			}
		}
		saveData.worldsNumEventMerchantItemsUsed = new Dictionary<string, Dictionary<string, int>>();
		foreach (World world15 in allWorlds)
		{
			if (world15.eventMerchantItems != null)
			{
				Dictionary<string, int> dictionary3 = new Dictionary<string, int>();
				saveData.worldsNumEventMerchantItemsUsed.Add(world15.GetId(), dictionary3);
				foreach (Simulation.MerchantItem merchantItem5 in world15.eventMerchantItems)
				{
					dictionary3.Add(merchantItem5.GetId(), merchantItem5.GetNumUsed());
				}
			}
		}
		saveData.worldsNumEventMerchantItemsInInventory = new Dictionary<string, Dictionary<string, int>>();
		foreach (World world16 in allWorlds)
		{
			if (world16.eventMerchantItems != null)
			{
				Dictionary<string, int> dictionary4 = new Dictionary<string, int>();
				saveData.worldsNumEventMerchantItemsInInventory.Add(world16.GetId(), dictionary4);
				foreach (Simulation.MerchantItem merchantItem6 in world16.eventMerchantItems)
				{
					dictionary4.Add(merchantItem6.GetId(), merchantItem6.GetNumInInventory());
				}
			}
		}
		saveData.worldMerchantUseStates = new Dictionary<string, WorldMerchantUseState>();
		foreach (World world17 in allWorlds)
		{
			WorldMerchantUseState worldMerchantUseState = new WorldMerchantUseState();
			worldMerchantUseState.timeWarpTimeLeft = world17.timeWarpTimeLeft;
			worldMerchantUseState.timeWarpSpeed = world17.timeWarpSpeed;
			worldMerchantUseState.autoTapTimeLeft = world17.autoTapTimeLeft;
			worldMerchantUseState.powerUpTimeLeft = world17.powerUpTimeLeft;
			worldMerchantUseState.powerUpDamageFactorAdd = world17.powerUpDamageFactorAdd;
			worldMerchantUseState.refresherOrbTimeLeft = world17.refresherOrbTimeLeft;
			worldMerchantUseState.refresherOrbSkillCoolFactor = world17.refresherOrbSkillCoolFactor;
			worldMerchantUseState.goldBoostTimeLeft = world17.goldBoostTimeLeft;
			worldMerchantUseState.goldBoostFactorAdd = world17.goldBoostFactor;
			worldMerchantUseState.shieldTimeLeft = world17.shieldTimeLeft;
			worldMerchantUseState.catalystTimeLeft = world17.catalystTimeLeft;
			worldMerchantUseState.catalystProgressPercentage = world17.catalystProgressPercentage;
			worldMerchantUseState.numCharmSelectionAdd = world17.numCharmSelectionAdd;
			worldMerchantUseState.pickRandomCharmsEnabled = world17.pickRandomCharms;
			worldMerchantUseState.blizzardTimeLeft = world17.blizzardTimeLeft;
			worldMerchantUseState.blizzardSlowFactor = world17.blizzardSlowFactor;
			worldMerchantUseState.hotCocoaTimeLeft = world17.hotCocoaTimeLeft;
			worldMerchantUseState.hotCocoaCooldownReductionFactor = world17.hotCocoaCooldownReductionFactor;
			worldMerchantUseState.hotCocoaDamageFactor = world17.hotCocoaDamageFactor;
			worldMerchantUseState.ornamentDropTimeLeft = world17.ornamentDropTimeLeft;
			worldMerchantUseState.ornamentDropTargetTaps = world17.ornamentDropTargetTime;
			worldMerchantUseState.ornamentDropTeamDamageFactor = world17.ornamentDropTeamDamageFactor;
			worldMerchantUseState.ornamentDropCurrentTime = world17.ornamentDropCurrentTime;
			worldMerchantUseState.ornamentDropProjectilesCount = world17.ornamentDropProjectilesCount;
			saveData.worldMerchantUseStates.Add(world17.GetId(), worldMerchantUseState);
		}
		saveData.worldsTotWave = new Dictionary<string, int>();
		foreach (World world18 in allWorlds)
		{
			if (world18.activeChallenge.HasWaveProgression())
			{
				string id = world18.GetId();
				int totWave = world18.activeChallenge.GetTotWave();
				saveData.worldsTotWave.Add(id, totWave);
			}
		}
		saveData.worldsNumBoughtWorldUpgrades = new Dictionary<string, int>();
		foreach (World world19 in allWorlds)
		{
			saveData.worldsNumBoughtWorldUpgrades.Add(world19.GetId(), world19.activeChallenge.totalGainedUpgrades.numBought);
		}
		saveData.worldsNumGivenSkillPoints = new Dictionary<string, int>();
		foreach (World world20 in allWorlds)
		{
			if (world20.givenSkillPoints > 0)
			{
				string id2 = world20.GetId();
				saveData.worldsNumGivenSkillPoints.Add(id2, world20.givenSkillPoints);
			}
		}
		saveData.worldsActiveCharms = new Dictionary<string, List<int>>();
		saveData.worldsDiscardedCharms = new Dictionary<string, List<int>>();
		saveData.worldsCharmBuffStates = new Dictionary<string, List<float>>();
		saveData.worldsCurseLevels = new Dictionary<string, List<int>>();
		saveData.worldsCurseProgress = new Dictionary<string, float>();
		saveData.worldsCurseSpawnIndexes = new Dictionary<string, int>();
		saveData.worldsCharmSelectionNums = new Dictionary<string, int>();
		saveData.worldsCharmSelectionTimers = new Dictionary<string, float>();
		foreach (World world21 in allWorlds)
		{
			List<int> list3 = new List<int>();
			List<int> list4 = new List<int>();
			List<int> list5 = new List<int>();
			List<float> list6 = new List<float>();
			if ((world21.activeChallenge.state == Challenge.State.ACTION || world21.activeChallenge.state == Challenge.State.WON) && world21.activeChallenge is ChallengeRift)
			{
				ChallengeRift challengeRift = world21.activeChallenge as ChallengeRift;
				foreach (EnchantmentBuff enchantmentBuff in challengeRift.allEnchantments)
				{
					list3.Add(enchantmentBuff.enchantmentData.baseData.id);
				}
				foreach (EnchantmentBuff enchantmentBuff2 in challengeRift.allEnchantments)
				{
					list6.Add(enchantmentBuff2.progress);
				}
				foreach (int item2 in challengeRift.discardedCharms)
				{
					list4.Add(item2);
				}
				foreach (CurseEffectData curseEffectData in challengeRift.activeCurseEffects)
				{
					list5.Add(curseEffectData.level);
				}
				saveData.worldsActiveCharms.Add(world21.GetId(), list3);
				saveData.worldsDiscardedCharms.Add(world21.GetId(), list4);
				saveData.worldsCurseLevels.Add(world21.GetId(), list5);
				saveData.worldsCurseProgress.Add(world21.GetId(), challengeRift.curseProgress);
				saveData.worldsCharmBuffStates.Add(world21.GetId(), list6);
				saveData.worldsCharmSelectionNums.Add(world21.GetId(), challengeRift.numCharmSelection);
				saveData.worldsCharmSelectionTimers.Add(world21.GetId(), challengeRift.charmSelectionAddTimer);
				saveData.worldsCurseSpawnIndexes.Add(world21.GetId(), challengeRift.curseSpawnIndex);
			}
		}
		saveData.heroEvolveLevels = new Dictionary<string, int>();
		saveData.heroEquippedSkins = new Dictionary<string, int>();
		saveData.heroRandomSkinsEnabled = new Dictionary<string, bool>();
		saveData.heroSkillBranchesEverUnlocked = new Dictionary<string, int[]>();
		foreach (HeroDataBase heroDataBase in sim.GetAllHeroes())
		{
			string id3 = heroDataBase.id;
			saveData.heroEvolveLevels.Add(id3, heroDataBase.evolveLevel);
			saveData.heroSkillBranchesEverUnlocked.Add(id3, heroDataBase.skillBranchesEverUnlocked);
			saveData.heroEquippedSkins.Add(id3, heroDataBase.equippedSkin.id);
			saveData.heroRandomSkinsEnabled.Add(id3, heroDataBase.randomSkinsEnabled);
		}
		saveData.boughtHeroSkins = new HashSet<int>();
		foreach (SkinData skinData in sim.GetBoughtSkins())
		{
			saveData.boughtHeroSkins.Add(skinData.id);
		}
		saveData.newHeroSkins = new HashSet<int>();
		foreach (SkinData skinData2 in sim.GetAllSkins())
		{
			if (!skinData2.isNew)
			{
				saveData.newHeroSkins.Add(skinData2.id);
			}
		}
		saveData.totemLevels = new Dictionary<string, int>();
		saveData.totemXps = new Dictionary<string, int>();
		foreach (World world22 in allWorlds)
		{
			if (world22.totem != null)
			{
				Totem totem = world22.totem;
				string id4 = totem.GetId();
				saveData.totemLevels.Add(id4, totem.GetLevel());
				saveData.totemXps.Add(id4, totem.GetXp());
			}
		}
		saveData.heroLevels = new Dictionary<string, int>();
		saveData.heroXps = new Dictionary<string, int>();
		saveData.heroNumUnspentSkillPoints = new Dictionary<string, int>();
		saveData.heroSkillTreesProgressGained = new Dictionary<string, SkillTreeProgressSerializable>();
		saveData.heroTillReviveTimes = new Dictionary<string, float>();
		saveData.heroHealthRatios = new Dictionary<string, double>();
		saveData.heroCostMultipliers = new Dictionary<string, double>();
		saveData.heroSkillCooldowns = new Dictionary<string, List<float>>();
		saveData.heroGeneric = new Dictionary<string, Dictionary<string, string>>();
		saveData.qou = new QuestOfUpdateSerializable();
		saveData.qou.type = SaveLoadManager.ConvertQuestOfUpdate(sim.questOfUpdate);
		if (sim.questOfUpdate != null)
		{
			saveData.qou.progress = sim.questOfUpdate.progress;
			saveData.qou.startTime = sim.questOfUpdate.startTime.Ticks;
			saveData.qou.isExpired = sim.questOfUpdate.isExpired;
		}
		saveData.completedQOUs = sim.completedQuestOfUpdates;
		saveData.failedQOUs = sim.failedQuestOfUpdates;
		foreach (World world23 in allWorlds)
		{
			foreach (Hero hero3 in world23.heroes)
			{
				string distinctId = hero3.GetDistinctId();
				saveData.heroLevels.Add(distinctId, hero3.GetLevel());
				saveData.heroXps.Add(distinctId, hero3.GetXp());
				saveData.heroNumUnspentSkillPoints.Add(distinctId, hero3.GetNumUnspentSkillPoints());
				saveData.heroSkillTreesProgressGained.Add(distinctId, SaveLoadManager.ConvertSkillTreeProgress(hero3.GetSkillTreeProgressGained()));
				saveData.heroTillReviveTimes.Add(distinctId, hero3.GetTillReviveTime());
				saveData.heroHealthRatios.Add(distinctId, hero3.GetHealthRatio());
				saveData.heroSkillCooldowns.Add(distinctId, hero3.GetSkillCooldowns());
				saveData.heroGeneric.Add(distinctId, hero3.GetSaveDataGeneric());
				saveData.heroCostMultipliers.Add(distinctId, hero3.costMultiplier);
			}
		}
		saveData.maxStagePrestigedAt = sim.maxStagePrestigedAt;
		saveData.tutStateFirst = SaveLoadManager.ConvertTutStateFirst(TutorialManager.first);
		saveData.tutStateHub = SaveLoadManager.ConvertTutStateHub(TutorialManager.hubTab);
		saveData.tutStateMode = SaveLoadManager.ConvertTutStateMode(TutorialManager.modeTab);
		saveData.tutStateArtifacts = SaveLoadManager.ConvertTutStateArtifact(TutorialManager.artifactsTab);
		saveData.tutStateShop = SaveLoadManager.ConvertTutStateShop(TutorialManager.shopTab);
		saveData.tutStatePrestige = SaveLoadManager.ConvertTutStatePrestige(TutorialManager.prestige);
		saveData.tutStateSkillScreen = SaveLoadManager.ConvertTutStateSkillScreen(TutorialManager.skillScreen);
		saveData.tutStateFightBossButton = SaveLoadManager.ConvertTutStateFightBossButton(TutorialManager.fightBossButton);
		saveData.tutStateGearScreen = SaveLoadManager.ConvertTutStateGearScreen(TutorialManager.gearScreen);
		saveData.tutStateRuneScreen = SaveLoadManager.ConvertTutStateRuneScreen(TutorialManager.runeScreen);
		saveData.tutStateRingPrestigeReminder = SaveLoadManager.ConvertTutStateRingPrestigeReminder(TutorialManager.ringPrestigeReminder);
		saveData.tutStateHeroPrestigeReminder = SaveLoadManager.ConvertTutStateHeroPrestigeReminder(TutorialManager.heroPrestigeReminder);
		saveData.tutStateMythicalArtifactsTab = SaveLoadManager.ConvertTutStateMythicalArtifactsTab(TutorialManager.mythicalArtifactsTab);
		saveData.tutStateTrinketShop = SaveLoadManager.ConvertTutStateTrinketShop(TutorialManager.trinketShop);
		saveData.tutStateTrinketHeroTab = SaveLoadManager.ConvertTutStateTrinketHeroTab(TutorialManager.trinketHeroTab);
		saveData.tutStateMineUnlock = SaveLoadManager.ConvertTutStateMineUnlock(TutorialManager.mineUnlock);
		saveData.tutStateDailyUnlock = SaveLoadManager.ConvertTutStateDailyUnlock(TutorialManager.dailyUnlock);
		saveData.tutStateDailyComplete = SaveLoadManager.ConvertTutStateDailyComplete(TutorialManager.dailyComplete);
		saveData.tutStateRiftsUnlocked = SaveLoadManager.ConvertTutStateRiftsUnlocked(TutorialManager.riftsUnlock);
		saveData.tutStateRiftEffects = SaveLoadManager.ConvertTutStateRiftEffects(TutorialManager.riftEffects);
		saveData.tutStateFirstCharm = SaveLoadManager.ConvertTutStateFirstCharm(TutorialManager.firstCharm);
		saveData.tutStateCharmHub = SaveLoadManager.ConvertTutStateCharmHub(TutorialManager.charmHub);
		saveData.tutStateFirstCharmPack = SaveLoadManager.ConvertTutStateFirstCharmPack(TutorialManager.firstCharmPack);
		saveData.tutStateCharmLevelUp = SaveLoadManager.ConvertTutStateCharmLevelUp(TutorialManager.charmLevelUp);
		saveData.tutStateAeonDust = SaveLoadManager.ConvertTutStateAeonDust(TutorialManager.aeonDust);
		saveData.tutStateRepeatRifts = SaveLoadManager.ConvertTutStateRepeatRifts(TutorialManager.repeatRifts);
		saveData.tutStateAllRiftsFinished = SaveLoadManager.ConvertTutStateAllRiftsFinished(TutorialManager.allRiftsFinished);
		saveData.tutStateFlashOffersUnlocked = SaveLoadManager.ConvertTutStateFlashOffersUnlocked(TutorialManager.flashOffersUnlocked);
		saveData.tutStateCursedGates = SaveLoadManager.ConvertTutStateCursedGates(TutorialManager.cursedGates);
		saveData.tutStateMissionsFinished = SaveLoadManager.ConvertTutMissionsFinished(TutorialManager.missionsFinished);
		saveData.tutStateTrinketSmithingUnlocked = SaveLoadManager.ConvertTutTrinketSmithingUnlocked(TutorialManager.trinketSmithingUnlocked);
		saveData.tutStateTrinketRecycleUnlocked = SaveLoadManager.ConvertTutTrinketRecycleUnlocked(TutorialManager.trinketRecycleUnlocked);
		saveData.tutStateChristmasTreeEventUnlocked = SaveLoadManager.ConvertTutChristmasTreeEventUnlocked(TutorialManager.christmasTreeEventUnlocked);
		saveData.tutStateArtifaceOverhaul = SaveLoadManager.ConverTutArtifactOverhaul(TutorialManager.artifactOverhaul);
		saveData.tutTimeCounter = TutorialManager.timeCounter;
		saveData.tutFirstPeriod = TutorialManager.firstPeriod;
		saveData.tutHubTabPeriod = TutorialManager.hubTabPeriod;
		saveData.tutModeTabPeriod = TutorialManager.modeTabPeriod;
		saveData.tutArtifactsTabPeriod = TutorialManager.artifactsTabPeriod;
		saveData.tutShopTabPeriod = TutorialManager.shopTabPeriod;
		saveData.tutPrestigePeriod = TutorialManager.prestigePeriod;
		saveData.tutSkillScreenPeriod = TutorialManager.skillScreenPeriod;
		saveData.tutFightBossButtonPeriod = TutorialManager.fightBossButtonPeriod;
		saveData.tutGearScreenPeriod = TutorialManager.gearScreenPeriod;
		saveData.tutGearGlobalReminderPeriod = TutorialManager.gearGlobalReminderPeriod;
		saveData.tutRingPrestigeReminderPeriod = TutorialManager.ringPrestigeReminderPeriod;
		saveData.tutHeroPrestigeReminderPeriod = TutorialManager.heroPrestigeReminderPeriod;
		saveData.tutMythicalArtifactsTabPeriod = TutorialManager.mythicalArtifactsTabPeriod;
		saveData.tutorialMissionIndex = TutorialManager.missionIndex;
		if (TutorialManager.missionIndex > -1 && TutorialManager.missionIndex < TutorialMission.List.Length)
		{
			TutorialMission tutorialMission = TutorialMission.List[TutorialManager.missionIndex];
			saveData.tutorialMissionProgress = tutorialMission.Progress;
			saveData.tutorialMissionClaimed = tutorialMission.Claimed;
		}
		saveData.playerStatLifeTimeInTicks = PlayerStats.lifeTimeInTicks;
		saveData.playerStatLifeTimeInTicksInCurrentSaveFile = PlayerStats.lifeTimeInTicksInCurrentSaveFile;
		saveData.playerStatNumLogins = PlayerStats.numLogins;
		saveData.playerStatSpentCreditsDuringThisSaveFile = PlayerStats.spentCreditsDuringThisSaveFile;
		saveData.playerStatSpentCredits = PlayerStats.spentCredits;
		saveData.playerStatSpentCreditsFirstDay = PlayerStats.spentCreditsFirstDay;
		saveData.playerStatSpentMyth = PlayerStats.spentMyth;
		saveData.playerStatSpentScraps = PlayerStats.spentScraps;
		saveData.playerStatSpentTokens = PlayerStats.spentTokens;
		saveData.playerStatSpentAeons = PlayerStats.spentAeons;
		saveData.playerStatNumAdDragonCatch = PlayerStats.numAdDragonCatch;
		saveData.playerStatNumAdDragonMiss = PlayerStats.numAdDragonMiss;
		saveData.playerStatNumAdAccept = PlayerStats.numAdAccept;
		saveData.playerStatNumAdCancel = PlayerStats.numAdCancel;
		saveData.playerStatNumFreeCredits = PlayerStats.numFreeCredits;
		saveData.playerStatNumTotTap = PlayerStats.numTotTap;
		saveData.playerStatNumUsedMerchantItems = PlayerStats.numUsedMerchantItems;
		saveData.playerStatEnemiesKilled = PlayerStats.enemiesKilled;
		saveData.playerStatGoblinChestsDestroyedCount = PlayerStats.goblinChestsDestroyedCount;
		saveData.playerStatTimeHeroesDied = PlayerStats.timeHeroesDied;
		saveData.playerStatUltimatesUsedCount = PlayerStats.ultimatesUsedCount;
		saveData.playerStatSecondaryAbilitiesCastedCount = PlayerStats.secondaryAbilitiesCastedCount;
		saveData.playerStatMinesCollectedCount = PlayerStats.minesCollectedCount;
		saveData.playerStatScreenshotsSharedInAdventure = PlayerStats.screenshotsSharedInAdventure;
		saveData.playerStatScreenshotsSharedInTimeChallenges = PlayerStats.screenshotsSharedInTimeChallenges;
		saveData.playerStatScreenshotsSharedInGog = PlayerStats.screenshotsSharedInGog;
		saveData.totalTrinketPackWithGem = PlayerStats.totalTrinketPackWithGem;
		saveData.totalTrinketPackWithGemOrAeon = PlayerStats.totalTrinketPackWithGemOrAeon;
		saveData.achievements = new Dictionary<int, bool>();
		foreach (KeyValuePair<string, bool> keyValuePair2 in PlayerStats.GetAchievementsDict())
		{
			saveData.achievements.Add(SaveLoadManager.ConvertAchievementId(keyValuePair2.Key), keyValuePair2.Value);
		}
		saveData.achiColls = new Dictionary<int, bool>();
		foreach (KeyValuePair<string, bool> keyValuePair3 in Simulator.achievementCollecteds)
		{
			saveData.achiColls.Add(SaveLoadManager.ConvertAchievementId(keyValuePair3.Key), keyValuePair3.Value);
		}
		saveData.prefers30Fps = sim.prefers30Fps;
		saveData.sleep = sim.appNeverSleep;
		saveData.snot = sim.scientificNotation;
		saveData.scd = sim.secondaryCdUi;
		saveData.compassDisabled = sim.compassDisabled;
		saveData.soundsMute = sim.setSoundsMute;
		saveData.musicMute = sim.setMusicMute;
		saveData.voicesMute = sim.setVoicesMute;
		saveData.skillOneTapUpgrade = sim.skillOneTapUpgrade;
		saveData.wasWatchingAd = (RewardedAdManager.inst != null && RewardedAdManager.inst.isWatchingAd && RewardedAdManager.inst.hasBeenWatchingAdWithoutIntermission);
		saveData.wasWatchingAdForFlashOffer = (saveData.wasWatchingAd && RewardedAdManager.inst.targetFlashOfferType != null);
		saveData.wasWatchingAdCapped = (RewardedAdManager.inst != null && RewardedAdManager.inst.isWatchingAdCapped && RewardedAdManager.inst.hasBeenWatchingAdWithoutIntermission);
		if (RewardedAdManager.inst != null && RewardedAdManager.inst.isWatchingAd)
		{
			FlashOffer.Type? targetFlashOfferType = RewardedAdManager.inst.targetFlashOfferType;
			if (targetFlashOfferType == null)
			{
				saveData.adRewardCurrencyType = SaveLoadManager.ConvertCurrencyType(sim.GetActiveWorld().adRewardCurrencyType);
				saveData.adRewardAmount = sim.GetActiveWorld().adRewardAmount;
				goto IL_1D44;
			}
		}
		if (RewardedAdManager.inst != null && RewardedAdManager.inst.isWatchingAd && RewardedAdManager.inst.targetFlashOfferType != null)
		{
			saveData.adFlashOfferRewardType = RewardedAdManager.inst.targetFlashOfferType.Value;
		}
		else if (RewardedAdManager.inst != null && RewardedAdManager.inst.isWatchingAdCapped)
		{
			saveData.adRewardCurrencyType = SaveLoadManager.ConvertCurrencyType(RewardedAdManager.inst.currencyTypeForCappedVideo);
			saveData.adRewardAmount = RewardedAdManager.inst.rewardAmountForCappedVideo;
		}
		IL_1D44:
		saveData.askedForRate = sim.askedForRate;
		saveData.allPlayfabIds = PlayfabManager.allPlayfabIds;
		saveData.dontStoreAuth = Main.dontStoreAuthenticate;
		saveData.dontTryAuth = Main.dontTryAuth;
		saveData.dontAskAuth = Main.dontAskForAuthenticatingPopup;
		saveData.seedArtifact = GameMath.seedArtifact;
		saveData.seedLootpack = GameMath.seedLootpack;
		saveData.seedTrinket = GameMath.seedTrinket;
		saveData.seedCharmpack = GameMath.seedCharmpack;
		saveData.seedCharmdraft = GameMath.seedCharmdraft;
		saveData.seedCursedGate = GameMath.seedCursedGate;
		saveData.seedNewCurses = GameMath.seedNewCurses;
		saveData.trinkets = new List<TrinketSerializable>();
		saveData.disassembledTrinketEffects = new Dictionary<int, int>();
		saveData.hasEverOwnedATrinket = sim.hasEverOwnedATrinket;
		saveData.numTrinketsObtained = sim.numTrinketsObtained;
		foreach (Trinket t in sim.allTrinkets)
		{
			saveData.trinkets.Add(SaveLoadManager.ConvertTrinket(t));
		}
		saveData.disassembledTrinketEffects = sim.disassembledTinketEffects;
		saveData.heroTrinkets = new Dictionary<string, int>();
		foreach (HeroDataBase heroDataBase2 in sim.GetAllHeroes())
		{
			if (heroDataBase2.trinket != null)
			{
				int i = 0;
				int count = sim.allTrinkets.Count;
				while (i < count)
				{
					if (heroDataBase2.trinket == sim.allTrinkets[i])
					{
						saveData.heroTrinkets.Add(heroDataBase2.id, i);
						break;
					}
					i++;
				}
			}
		}
		saveData.heroTrinketTimers = new Dictionary<string, float>();
		foreach (HeroDataBase heroDataBase3 in sim.GetAllHeroes())
		{
			if (heroDataBase3.trinketEquipTimer > 0f)
			{
				saveData.heroTrinketTimers.Add(heroDataBase3.id, heroDataBase3.trinketEquipTimer);
			}
		}
		saveData.numTrinketPacks = sim.numTrinketPacks;
		saveData.numUnseenTrinketPacks = sim.numUnseenTrinketPacks;
		saveData.numCharmPacks = sim.numSmallCharmPacks;
		SaveLoadManager.SetShopPack(saveData);
		saveData.lastOfferEndTime = sim.lastOfferEndTime.Ticks;
		saveData.lastRiftOfferEndTime = sim.lastRiftOfferEndTime.Ticks;
		saveData.notifOn = StoreManager.areNotificationsAllowed;
		saveData.notifAsked = StoreManager.askedToAllowNotifications;
		int num = (!StoreManager.mineNotifications) ? 0 : 1;
		int num2 = (!StoreManager.specialOffersNotifications) ? 0 : 2;
		int num3 = (!StoreManager.freeChestsNotifications) ? 0 : 4;
		int num4 = (!StoreManager.sideQuestNotifications) ? 0 : 8;
		int num5 = (!StoreManager.flashOffersNotifications) ? 0 : 16;
		int num6 = (!StoreManager.dustRestBonusFullNotifications) ? 0 : 32;
		int num7 = (!StoreManager.christmasEventNotifications) ? 0 : 64;
		int num8 = (!StoreManager.eventsNotifications) ? 0 : 128;
		saveData.notifs = (num | num2 | num3 | num4 | num5 | num6 | num7 | num8);
		saveData.iapsMade = PlayerStats.iapsMade;
		saveData.numTrinketPacksOpened = PlayerStats.numTrinketPacksOpened;
		saveData.trinketsPinned = sim.trinketsPinned;
		saveData.mineToken = SaveLoadManager.ConvertMine(sim.mineToken);
		saveData.mineScrap = SaveLoadManager.ConvertMine(sim.mineScrap);
		saveData.daily = SaveLoadManager.ConvertDailyQuest(sim.dailyQuest);
		if (sim.dailyQuest != null)
		{
			saveData.dailyProgress = sim.dailyQuest.progress;
		}
		saveData.lastDaily = sim.lastDailyQuest;
		saveData.dailySkip = sim.dailySkipCount;
		saveData.dailyQuestsAppearedCount = sim.dailyQuestsAppearedCount;
		saveData.dailyTime = sim.dailyQuestCollectedTime.Ticks;
		saveData.totalDailySkip = PlayerStats.numTotalDailySkip;
		saveData.totalDailyCompleted = PlayerStats.numTotalDailyCompleted;
		saveData.numCandyQuest = sim.numCandyQuestCompleted;
		saveData.numCandyAnQuest = sim.numCandyQuestAnniversaryCompleted;
		saveData.isSkinsEverClicked = sim.isSkinsEverClicked;
		World world24 = sim.GetWorld(GameMode.RIFT);
		saveData.prestigeRunTime = sim.prestigeRunTimer;
		saveData.lastPrestigeRun = sim.lastPrestigeRunstats;
		saveData.riftQuestDustCollected = sim.riftQuestDustCollected;
		saveData.hasRiftQuest = sim.hasRiftQuest;
		saveData.riftTime = sim.riftRestRewardCollectedTime.Ticks;
		saveData.activeRiftChallengeId = (world24.activeChallenge as ChallengeRift).id;
		saveData.riftRecords = new Dictionary<int, double>();
		foreach (Challenge challenge in world24.allChallenges)
		{
			ChallengeRift challengeRift2 = (ChallengeRift)challenge;
			if (challengeRift2.unlock.isCollected)
			{
				saveData.riftRecords.Add(challengeRift2.id, challengeRift2.finishingRecord);
			}
		}
		saveData.charmStatus = new Dictionary<int, CharmLevelStatusSerializable>();
		saveData.newLabelledCharms = new HashSet<int>();
		saveData.newLabelledCurses = new HashSet<int>();
		foreach (KeyValuePair<int, CharmEffectData> keyValuePair4 in sim.allCharmEffects)
		{
			if (keyValuePair4.Value.level >= 0)
			{
				saveData.charmStatus.Add(keyValuePair4.Key, SaveLoadManager.ConvertCharmEffect(keyValuePair4.Value));
				if (keyValuePair4.Value.isNew)
				{
					saveData.newLabelledCharms.Add(keyValuePair4.Key);
				}
			}
		}
		foreach (KeyValuePair<int, CurseEffectData> keyValuePair5 in sim.allCurseEffects)
		{
			if (keyValuePair5.Value.isNew)
			{
				saveData.newLabelledCurses.Add(keyValuePair5.Key);
			}
		}
		saveData.flashOfferBundle = SaveLoadManager.ConvertFlashOfferBundle(sim.flashOfferBundle);
		saveData.halloweenFlashOfferBundle = SaveLoadManager.ConvertServerSideFlashOfferBundle(sim.halloweenFlashOfferBundle);
		saveData.riftAutoSkillDistribute = world24.autoSkillDistribute;
		saveData.adventureAutoSkillDistribute = sim.GetWorld(GameMode.STANDARD).autoSkillDistribute;
		saveData.riftDiscoveryIndex = sim.riftDiscoveryIndex;
		saveData.numCharmPacksOpened = sim.numSmallCharmPacksOpened;
		saveData.numRiftQuestsCompleted = sim.numRiftQuestsCompleted;
		saveData.allTimeCharmPacksOpened = PlayerStats.allTimeCharmPacksOpened;
		saveData.allTimeAdventureFlashOffersPurchased = PlayerStats.allTimeAdventureFlashOffersPurchased;
		saveData.allTimeHalloweenFlashOffersPurchased = PlayerStats.allTimeHalloweenFlashOffersPurchased;
		saveData.allTimeChristmasFlashOffersPurchased = PlayerStats.allTimeChristmasFlashOffersPurchased;
		saveData.allTimeFlashOffersPurchased = PlayerStats.allTimeFlashOffersPurchased;
		saveData.cst = SaveLoadManager.ConvertCharmSortType(sim.charmSortType);
		saveData.icsd = sim.isCharmSortingDescending;
		saveData.isCharSS = sim.isCharmSortingShowing;
		saveData.isTrinketSortingDescending = sim.isTrinketSortingDescending;
		saveData.trinketSortingType = SaveLoadManager.ConvertTrinketSortType(sim.trinketSortType);
		saveData.isTrinketSortingShowing = sim.isTrinketSortingShowing;
		saveData.socialRewardsStatus = sim.socialRewardsStatus;
		saveData.lastNewsTimestam = sim.lastNewsTimestam;
		saveData.ratingState = sim.ratingState;
		saveData.lootpacksOpenedCount = sim.lootpacksOpenedCount;
		saveData.lastFiveOpenedCharms = new List<int>();
		foreach (int item3 in sim.lastFiveOpenedCharms)
		{
			saveData.lastFiveOpenedCharms.Add(item3);
		}
		SaveLoadManager.SaveSpecialOfferBoard(sim.specialOfferBoard, saveData);
		saveData.cursedIds = new List<int>();
		saveData.cursedLevs = new List<int>();
		foreach (Challenge challenge2 in world24.cursedChallenges)
		{
			ChallengeRift challengeRift3 = (ChallengeRift)challenge2;
			saveData.cursedIds.Add(challengeRift3.id);
			saveData.cursedLevs.Add(challengeRift3.discoveryIndex);
		}
		saveData.wasAtiveChallengeCursed = (world24.activeChallenge as ChallengeRift).IsCursed();
		saveData.timeLastAddedCursedRift = sim.lastAddedCurseChallengeTime.Ticks;
		saveData.currentCurses = sim.currentCurses;
		saveData.cursedGatesBeaten = sim.cursedGatesBeaten;
		saveData.lastSelectedRegularGateIndex = sim.lastSelectedRegularGateIndex;
		saveData.announcedOffersTimes = sim.announcedOffersTimes;
		saveData.amountLootpacksOpenedForHint = sim.amountLootPacksOpenedForHint;
		saveData.trinketSmith = sim.hasTrinketSmith;
		saveData.installDate = sim.installDate.Ticks;
		saveData.usedTrinketExploit = sim.usedTrinketExploit;
		if (sim.christmasOfferBundle != null)
		{
			saveData.christmasOffersBundlePurchasesLeft = new List<List<int>>();
			int j = 0;
			int count2 = sim.christmasOfferBundle.tree.Count;
			while (j < count2)
			{
				saveData.christmasOffersBundlePurchasesLeft.Add(new List<int>());
				int k = 0;
				int count3 = sim.christmasOfferBundle.tree[j].Count;
				while (k < count3)
				{
					saveData.christmasOffersBundlePurchasesLeft[j].Add(sim.christmasOfferBundle.tree[j][k].offer.purchasesLeft);
					k++;
				}
				j++;
			}
		}
		saveData.christmasEventAlreadyDisabled = sim.christmasEventAlreadyDisabled;
		saveData.candyDropAlreadyDisabled = sim.candyDropAlreadyDisabled;
		saveData.lastFreeCandyTreatClaimedDate = sim.lastFreeCandyTreatClaimedDate.Ticks;
		saveData.lastSessionDate = sim.lastSessionDate.Ticks;
		saveData.christmasEventPopupsShown = sim.christmasEventPopupsShown;
		saveData.christmasTreatVideosWatchedSinceLastReset = sim.christmasTreatVideosWatchedSinceLastReset;
		saveData.rewardsToGive = sim.rewardsToGive;
		saveData.christmasCandyCappedVideoNotificationSeen = sim.christmasCandyCappedVideoNotificationSeen;
		saveData.christmasFreeCandyNotificationSeen = sim.christmasFreeCandyNotificationSeen;
		saveData.newStats = sim.newStats;
		saveData.earnedBadges = new List<int>();
		saveData.notificationDismissedBadges = new List<int>();
		foreach (Badge badge in Badges.All)
		{
			if (badge.HasBeenEarnedByPlayer(sim))
			{
				saveData.earnedBadges.Add((int)badge.Id);
			}
			else if (badge.NotificationDismissed)
			{
				saveData.notificationDismissedBadges.Add((int)badge.Id);
			}
		}
		saveData.christmasEventForbidden = sim.christmasEventForbidden;
		saveData.artifactList = sim.artifactsManager.Artifacts;
		saveData.prestigedDuringSecondAnniversaryEvent = sim.prestigedDuringSecondAnniversaryEvent;
		saveData.cataclysmSurviver = sim.isCataclysmSurviver;
		saveData.stageRearrangeSurviver = sim.isStageRearrangeSurviver;
		saveData.numPrestigesSinceCataclysm = sim.numPrestigesSinceCataclysm;
		saveData.reachedMaxStageInCurrentAdventure = sim.maxStageReachedInCurrentAdventure;
		saveData.secondAnniversaryFlashOfferBundle = SaveLoadManager.ConvertServerSideFlashOfferBundle(sim.secondAnniversaryFlashOffersBundle);
		saveData.artifactMultiUpgradeIndex = sim.artifactMultiUpgradeIndex;
		saveData.timeChallengesLostCount = sim.timeChallengesLostCount;
		saveData.secondAnniversaryEventAlreadyDisabled = sim.secondAnniversaryEventAlreadyDisabled;
		return saveData;
	}

	public static int GetArtifactEffectIntFromType(ArtifactEffectType type)
	{
		switch (type)
		{
		case ArtifactEffectType.BossTime:
			return 1;
		case ArtifactEffectType.ChestChance:
			return 2;
		case ArtifactEffectType.CostHeroUpgrade:
			return 3;
		case ArtifactEffectType.CostTotemUpgrade:
			return 4;
		case ArtifactEffectType.CritChanceHero:
			return 5;
		case ArtifactEffectType.CritChanceTotem:
			return 6;
		case ArtifactEffectType.CritFactorHero:
			return 7;
		case ArtifactEffectType.CritFactorTotem:
			return 8;
		case ArtifactEffectType.Damage:
			return 9;
		case ArtifactEffectType.DamageHero:
			return 10;
		case ArtifactEffectType.DamageHeroSkill:
			return 11;
		case ArtifactEffectType.DamageTotem:
			return 12;
		case ArtifactEffectType.DroneSpawnRate:
			return 13;
		case ArtifactEffectType.EpicBossDropMythstone:
			return 14;
		case ArtifactEffectType.FreePackCooldown:
			return 15;
		case ArtifactEffectType.Gold:
			return 16;
		case ArtifactEffectType.GoldBoss:
			return 17;
		case ArtifactEffectType.GoldChest:
			return 18;
		case ArtifactEffectType.GoldMultTenChance:
			return 19;
		case ArtifactEffectType.GoldOffline:
			return 20;
		case ArtifactEffectType.HealthBoss:
			return 21;
		case ArtifactEffectType.HealthHero:
			return 22;
		case ArtifactEffectType.HeroLevelReqForSkill:
			return 23;
		case ArtifactEffectType.ReviveTime:
			return 24;
		case ArtifactEffectType.UltiCooldown:
			return 25;
		case ArtifactEffectType.WaveSkipChance:
			return 26;
		case ArtifactEffectType.PrestigeMythstoneBonus:
			return 27;
		case ArtifactEffectType.AutoTapTime:
			return 28;
		case ArtifactEffectType.AutoTapCount:
			return 29;
		case ArtifactEffectType.GoldBagCount:
			return 30;
		case ArtifactEffectType.TimeWarpCount:
			return 31;
		case ArtifactEffectType.GoldBagValue:
			return 32;
		case ArtifactEffectType.TimeWarpSpeed:
			return 33;
		case ArtifactEffectType.TimeWarpDuration:
			return 34;
		case ArtifactEffectType.QuickWaveAfterBoss:
			return 35;
		case ArtifactEffectType.TotemUltraCrit:
			return 36;
		case ArtifactEffectType.StageSkipChance:
			return 37;
		case ArtifactEffectType.LifeBoat:
			return 38;
		case ArtifactEffectType.FastSpawn:
			return 39;
		case ArtifactEffectType.RaidTreasureGoblinChance:
			return 40;
		case ArtifactEffectType.PerfectQuasi:
			return 41;
		case ArtifactEffectType.HealthEnemy:
			return 42;
		case ArtifactEffectType.DamageEnemy:
			return 43;
		case ArtifactEffectType.DamageBoss:
			return 44;
		case ArtifactEffectType.GearMultiplier:
			return 45;
		case ArtifactEffectType.DpsMaster:
			return 46;
		case ArtifactEffectType.FreeUpgrade:
			return 47;
		case ArtifactEffectType.OldCrucible:
			return 48;
		case ArtifactEffectType.IdleGoldGain:
			return 49;
		case ArtifactEffectType.AutoUpgrade:
			return 50;
		case ArtifactEffectType.ShinyObject:
			return 51;
		case ArtifactEffectType.PowerupCritChance:
			return 52;
		case ArtifactEffectType.PowerupCooldown:
			return 53;
		case ArtifactEffectType.PowerupRevive:
			return 54;
		case ArtifactEffectType.DamageHeroNonSkill:
			return 55;
		case ArtifactEffectType.BodilyHarm:
			return 56;
		case ArtifactEffectType.ChampionsBounty:
			return 57;
		case ArtifactEffectType.CorpusImperium:
			return 58;
		case ArtifactEffectType.ShieldCount:
			return 59;
		case ArtifactEffectType.ShieldDuration:
			return 60;
		case ArtifactEffectType.HorseshoeCount:
			return 61;
		case ArtifactEffectType.HorseshoeDuration:
			return 62;
		case ArtifactEffectType.HorseshoeValue:
			return 63;
		case ArtifactEffectType.DestructionCount:
			return 64;
		case ArtifactEffectType.HeroDamagePerAttacker:
			return 65;
		case ArtifactEffectType.HeroHealthPerDefender:
			return 66;
		case ArtifactEffectType.GoldBonusPerSupporter:
			return 67;
		default:
			throw new NotImplementedException();
		}
	}

	public static ArtifactEffectType GetArtifactEffectTypeFromInt(int type)
	{
		switch (type)
		{
		case 1:
			return ArtifactEffectType.BossTime;
		case 2:
			return ArtifactEffectType.ChestChance;
		case 3:
			return ArtifactEffectType.CostHeroUpgrade;
		case 4:
			return ArtifactEffectType.CostTotemUpgrade;
		case 5:
			return ArtifactEffectType.CritChanceHero;
		case 6:
			return ArtifactEffectType.CritChanceTotem;
		case 7:
			return ArtifactEffectType.CritFactorHero;
		case 8:
			return ArtifactEffectType.CritFactorTotem;
		case 9:
			return ArtifactEffectType.Damage;
		case 10:
			return ArtifactEffectType.DamageHero;
		case 11:
			return ArtifactEffectType.DamageHeroSkill;
		case 12:
			return ArtifactEffectType.DamageTotem;
		case 13:
			return ArtifactEffectType.DroneSpawnRate;
		case 14:
			return ArtifactEffectType.EpicBossDropMythstone;
		case 15:
			return ArtifactEffectType.FreePackCooldown;
		case 16:
			return ArtifactEffectType.Gold;
		case 17:
			return ArtifactEffectType.GoldBoss;
		case 18:
			return ArtifactEffectType.GoldChest;
		case 19:
			return ArtifactEffectType.GoldMultTenChance;
		case 20:
			return ArtifactEffectType.GoldOffline;
		case 21:
			return ArtifactEffectType.HealthBoss;
		case 22:
			return ArtifactEffectType.HealthHero;
		case 23:
			return ArtifactEffectType.HeroLevelReqForSkill;
		case 24:
			return ArtifactEffectType.ReviveTime;
		case 25:
			return ArtifactEffectType.UltiCooldown;
		case 26:
			return ArtifactEffectType.WaveSkipChance;
		case 27:
			return ArtifactEffectType.PrestigeMythstoneBonus;
		case 28:
			return ArtifactEffectType.AutoTapTime;
		case 29:
			return ArtifactEffectType.AutoTapCount;
		case 30:
			return ArtifactEffectType.GoldBagCount;
		case 31:
			return ArtifactEffectType.TimeWarpCount;
		case 32:
			return ArtifactEffectType.GoldBagValue;
		case 33:
			return ArtifactEffectType.TimeWarpSpeed;
		case 34:
			return ArtifactEffectType.TimeWarpDuration;
		case 35:
			return ArtifactEffectType.QuickWaveAfterBoss;
		case 36:
			return ArtifactEffectType.TotemUltraCrit;
		case 37:
			return ArtifactEffectType.StageSkipChance;
		case 38:
			return ArtifactEffectType.LifeBoat;
		case 39:
			return ArtifactEffectType.FastSpawn;
		case 40:
			return ArtifactEffectType.RaidTreasureGoblinChance;
		case 41:
			return ArtifactEffectType.PerfectQuasi;
		case 42:
			return ArtifactEffectType.HealthEnemy;
		case 43:
			return ArtifactEffectType.DamageEnemy;
		case 44:
			return ArtifactEffectType.DamageBoss;
		case 45:
			return ArtifactEffectType.GearMultiplier;
		case 46:
			return ArtifactEffectType.DpsMaster;
		case 47:
			return ArtifactEffectType.FreeUpgrade;
		case 48:
			return ArtifactEffectType.OldCrucible;
		case 49:
			return ArtifactEffectType.IdleGoldGain;
		case 50:
			return ArtifactEffectType.AutoUpgrade;
		case 51:
			return ArtifactEffectType.ShinyObject;
		case 52:
			return ArtifactEffectType.PowerupCritChance;
		case 53:
			return ArtifactEffectType.PowerupCooldown;
		case 54:
			return ArtifactEffectType.PowerupRevive;
		case 55:
			return ArtifactEffectType.DamageHeroNonSkill;
		case 56:
			return ArtifactEffectType.BodilyHarm;
		case 57:
			return ArtifactEffectType.ChampionsBounty;
		case 58:
			return ArtifactEffectType.CorpusImperium;
		case 59:
			return ArtifactEffectType.ShieldCount;
		case 60:
			return ArtifactEffectType.ShieldDuration;
		case 61:
			return ArtifactEffectType.HorseshoeCount;
		case 62:
			return ArtifactEffectType.HorseshoeDuration;
		case 63:
			return ArtifactEffectType.HorseshoeValue;
		case 64:
			return ArtifactEffectType.DestructionCount;
		case 65:
			return ArtifactEffectType.HeroDamagePerAttacker;
		case 66:
			return ArtifactEffectType.HeroHealthPerDefender;
		case 67:
			return ArtifactEffectType.GoldBonusPerSupporter;
		default:
			throw new NotImplementedException();
		}
	}

	public static ArtifactSerializable ConvertArtifact(Simulation.Artifact artifact)
	{
		ArtifactSerializable artifactSerializable = new ArtifactSerializable();
		artifactSerializable.effectTypes = new List<int>();
		artifactSerializable.amounts = new List<double>();
		foreach (Simulation.ArtifactEffect artifactEffect in artifact.effects)
		{
			ArtifactEffectType effectTypeSelf = artifactEffect.GetEffectTypeSelf();
			int artifactEffectIntFromType = SaveLoadManager.GetArtifactEffectIntFromType(effectTypeSelf);
			artifactSerializable.effectTypes.Add(artifactEffectIntFromType);
			double item;
			if (artifactEffect is MythicalArtifactEffect)
			{
				item = (double)(artifactEffect as MythicalArtifactEffect).GetRank();
			}
			else
			{
				item = artifactEffect.GetAmount();
			}
			artifactSerializable.amounts.Add(item);
		}
		return artifactSerializable;
	}

	public static Simulation.Artifact ConvertArtifact(ArtifactSerializable artifactSerializable)
	{
		Simulation.Artifact artifact = new Simulation.Artifact();
		artifact.effects = new List<Simulation.ArtifactEffect>();
		int i = 0;
		int count = artifactSerializable.effectTypes.Count;
		while (i < count)
		{
			int type = artifactSerializable.effectTypes[i];
			ArtifactEffectType artifactEffectTypeFromInt = SaveLoadManager.GetArtifactEffectTypeFromInt(type);
			double num = artifactSerializable.amounts[i];
			switch (artifactEffectTypeFromInt)
			{
			case ArtifactEffectType.BossTime:
			{
				OLD_ArtifactEffectBossTime old_ArtifactEffectBossTime = new OLD_ArtifactEffectBossTime();
				old_ArtifactEffectBossTime.amount = (float)num;
				artifact.effects.Add(old_ArtifactEffectBossTime);
				break;
			}
			case ArtifactEffectType.ChestChance:
			{
				OLD_ArtifactEffectChestChance old_ArtifactEffectChestChance = new OLD_ArtifactEffectChestChance();
				old_ArtifactEffectChestChance.amount = (float)num;
				artifact.effects.Add(old_ArtifactEffectChestChance);
				break;
			}
			case ArtifactEffectType.CostHeroUpgrade:
			{
				OLD_ArtifactEffectCostHeroUpgrade old_ArtifactEffectCostHeroUpgrade = new OLD_ArtifactEffectCostHeroUpgrade();
				old_ArtifactEffectCostHeroUpgrade.amount = num;
				artifact.effects.Add(old_ArtifactEffectCostHeroUpgrade);
				break;
			}
			case ArtifactEffectType.CostTotemUpgrade:
			{
				OLD_ArtifactEffectCostTotemUpgrade old_ArtifactEffectCostTotemUpgrade = new OLD_ArtifactEffectCostTotemUpgrade();
				old_ArtifactEffectCostTotemUpgrade.amount = num;
				artifact.effects.Add(old_ArtifactEffectCostTotemUpgrade);
				break;
			}
			case ArtifactEffectType.CritChanceHero:
			{
				OLD_ArtifactEffectCritChanceHero old_ArtifactEffectCritChanceHero = new OLD_ArtifactEffectCritChanceHero();
				old_ArtifactEffectCritChanceHero.amount = (float)num;
				artifact.effects.Add(old_ArtifactEffectCritChanceHero);
				break;
			}
			case ArtifactEffectType.CritChanceTotem:
			{
				OLD_ArtifactEffectCritChanceTotem old_ArtifactEffectCritChanceTotem = new OLD_ArtifactEffectCritChanceTotem();
				old_ArtifactEffectCritChanceTotem.amount = (float)num;
				artifact.effects.Add(old_ArtifactEffectCritChanceTotem);
				break;
			}
			case ArtifactEffectType.CritFactorHero:
			{
				OLD_ArtifactEffectCritFactorHero old_ArtifactEffectCritFactorHero = new OLD_ArtifactEffectCritFactorHero();
				old_ArtifactEffectCritFactorHero.amount = num;
				artifact.effects.Add(old_ArtifactEffectCritFactorHero);
				break;
			}
			case ArtifactEffectType.CritFactorTotem:
			{
				OLD_ArtifactEffectCritFactorTotem old_ArtifactEffectCritFactorTotem = new OLD_ArtifactEffectCritFactorTotem();
				old_ArtifactEffectCritFactorTotem.amount = num;
				artifact.effects.Add(old_ArtifactEffectCritFactorTotem);
				break;
			}
			case ArtifactEffectType.Damage:
			{
				OLD_ArtifactEffectDamage old_ArtifactEffectDamage = new OLD_ArtifactEffectDamage();
				old_ArtifactEffectDamage.amount = num;
				artifact.effects.Add(old_ArtifactEffectDamage);
				break;
			}
			case ArtifactEffectType.DamageHero:
			{
				OLD_ArtifactEffectDamageHero old_ArtifactEffectDamageHero = new OLD_ArtifactEffectDamageHero();
				old_ArtifactEffectDamageHero.amount = num;
				artifact.effects.Add(old_ArtifactEffectDamageHero);
				break;
			}
			case ArtifactEffectType.DamageHeroSkill:
			{
				OLD_ArtifactEffectDamageHeroSkill old_ArtifactEffectDamageHeroSkill = new OLD_ArtifactEffectDamageHeroSkill();
				old_ArtifactEffectDamageHeroSkill.amount = num;
				artifact.effects.Add(old_ArtifactEffectDamageHeroSkill);
				break;
			}
			case ArtifactEffectType.DamageTotem:
			{
				OLD_ArtifactEffectDamageTotem old_ArtifactEffectDamageTotem = new OLD_ArtifactEffectDamageTotem();
				old_ArtifactEffectDamageTotem.amount = num;
				artifact.effects.Add(old_ArtifactEffectDamageTotem);
				break;
			}
			case ArtifactEffectType.DroneSpawnRate:
			{
				OLD_ArtifactEffectDroneSpawnRate old_ArtifactEffectDroneSpawnRate = new OLD_ArtifactEffectDroneSpawnRate();
				old_ArtifactEffectDroneSpawnRate.amount = (float)num;
				artifact.effects.Add(old_ArtifactEffectDroneSpawnRate);
				break;
			}
			case ArtifactEffectType.EpicBossDropMythstone:
			{
				OLD_ArtifactEffectEpicBossDropMythstone old_ArtifactEffectEpicBossDropMythstone = new OLD_ArtifactEffectEpicBossDropMythstone();
				old_ArtifactEffectEpicBossDropMythstone.amount = (int)num;
				artifact.effects.Add(old_ArtifactEffectEpicBossDropMythstone);
				break;
			}
			case ArtifactEffectType.FreePackCooldown:
			{
				OLD_ArtifactEffectFreePackCooldown old_ArtifactEffectFreePackCooldown = new OLD_ArtifactEffectFreePackCooldown();
				old_ArtifactEffectFreePackCooldown.amount = (float)num;
				artifact.effects.Add(old_ArtifactEffectFreePackCooldown);
				break;
			}
			case ArtifactEffectType.Gold:
			{
				OLD_ArtifactEffectGold old_ArtifactEffectGold = new OLD_ArtifactEffectGold();
				old_ArtifactEffectGold.amount = num;
				artifact.effects.Add(old_ArtifactEffectGold);
				break;
			}
			case ArtifactEffectType.GoldBoss:
			{
				OLD_ArtifactEffectGoldBoss old_ArtifactEffectGoldBoss = new OLD_ArtifactEffectGoldBoss();
				old_ArtifactEffectGoldBoss.amount = num;
				artifact.effects.Add(old_ArtifactEffectGoldBoss);
				break;
			}
			case ArtifactEffectType.GoldChest:
			{
				OLD_ArtifactEffectGoldChest old_ArtifactEffectGoldChest = new OLD_ArtifactEffectGoldChest();
				old_ArtifactEffectGoldChest.amount = num;
				artifact.effects.Add(old_ArtifactEffectGoldChest);
				break;
			}
			case ArtifactEffectType.GoldMultTenChance:
			{
				OLD_ArtifactEffectGoldMultTenChance old_ArtifactEffectGoldMultTenChance = new OLD_ArtifactEffectGoldMultTenChance();
				old_ArtifactEffectGoldMultTenChance.amount = (float)num;
				artifact.effects.Add(old_ArtifactEffectGoldMultTenChance);
				break;
			}
			case ArtifactEffectType.GoldOffline:
			{
				OLD_ArtifactEffectGoldOffline old_ArtifactEffectGoldOffline = new OLD_ArtifactEffectGoldOffline();
				old_ArtifactEffectGoldOffline.amount = num;
				artifact.effects.Add(old_ArtifactEffectGoldOffline);
				break;
			}
			case ArtifactEffectType.HealthBoss:
			{
				OLD_ArtifactEffectHealthBoss old_ArtifactEffectHealthBoss = new OLD_ArtifactEffectHealthBoss();
				old_ArtifactEffectHealthBoss.amount = num;
				artifact.effects.Add(old_ArtifactEffectHealthBoss);
				break;
			}
			case ArtifactEffectType.HealthHero:
			{
				OLD_ArtifactEffectHealthHero old_ArtifactEffectHealthHero = new OLD_ArtifactEffectHealthHero();
				old_ArtifactEffectHealthHero.amount = num;
				artifact.effects.Add(old_ArtifactEffectHealthHero);
				break;
			}
			case ArtifactEffectType.HeroLevelReqForSkill:
			{
				OLD_ArtifactEffectHeroLevelReqForSkill old_ArtifactEffectHeroLevelReqForSkill = new OLD_ArtifactEffectHeroLevelReqForSkill();
				old_ArtifactEffectHeroLevelReqForSkill.amount = (int)num;
				artifact.effects.Add(old_ArtifactEffectHeroLevelReqForSkill);
				break;
			}
			case ArtifactEffectType.ReviveTime:
			{
				OLD_ArtifactEffectReviveTime old_ArtifactEffectReviveTime = new OLD_ArtifactEffectReviveTime();
				old_ArtifactEffectReviveTime.amount = (float)num;
				artifact.effects.Add(old_ArtifactEffectReviveTime);
				break;
			}
			case ArtifactEffectType.UltiCooldown:
			{
				OLD_ArtifactEffectUltiCooldown old_ArtifactEffectUltiCooldown = new OLD_ArtifactEffectUltiCooldown();
				old_ArtifactEffectUltiCooldown.amount = (float)num;
				artifact.effects.Add(old_ArtifactEffectUltiCooldown);
				break;
			}
			case ArtifactEffectType.WaveSkipChance:
			{
				OLD_ArtifactEffectWaveSkipChance old_ArtifactEffectWaveSkipChance = new OLD_ArtifactEffectWaveSkipChance();
				old_ArtifactEffectWaveSkipChance.amount = (float)num;
				artifact.effects.Add(old_ArtifactEffectWaveSkipChance);
				break;
			}
			case ArtifactEffectType.PrestigeMythstoneBonus:
			{
				OLD_ArtifactEffectPrestigeMyth old_ArtifactEffectPrestigeMyth = new OLD_ArtifactEffectPrestigeMyth();
				old_ArtifactEffectPrestigeMyth.amount = (double)((float)num);
				artifact.effects.Add(old_ArtifactEffectPrestigeMyth);
				break;
			}
			case ArtifactEffectType.AutoTapTime:
			{
				OLD_ArtifactEffectAutoTapTime old_ArtifactEffectAutoTapTime = new OLD_ArtifactEffectAutoTapTime();
				old_ArtifactEffectAutoTapTime.amount = (float)num;
				artifact.effects.Add(old_ArtifactEffectAutoTapTime);
				break;
			}
			case ArtifactEffectType.AutoTapCount:
			{
				OLD_ArtifactEffectAutoTapCount old_ArtifactEffectAutoTapCount = new OLD_ArtifactEffectAutoTapCount();
				old_ArtifactEffectAutoTapCount.amount = (int)num;
				artifact.effects.Add(old_ArtifactEffectAutoTapCount);
				break;
			}
			case ArtifactEffectType.GoldBagCount:
			{
				OLD_ArtifactEffectGoldBagCount old_ArtifactEffectGoldBagCount = new OLD_ArtifactEffectGoldBagCount();
				old_ArtifactEffectGoldBagCount.amount = (int)num;
				artifact.effects.Add(old_ArtifactEffectGoldBagCount);
				break;
			}
			case ArtifactEffectType.TimeWarpCount:
			{
				OLD_ArtifactEffectTimeWarpCount old_ArtifactEffectTimeWarpCount = new OLD_ArtifactEffectTimeWarpCount();
				old_ArtifactEffectTimeWarpCount.amount = (int)num;
				artifact.effects.Add(old_ArtifactEffectTimeWarpCount);
				break;
			}
			case ArtifactEffectType.GoldBagValue:
			{
				OLD_ArtifactEffectGoldBagValue old_ArtifactEffectGoldBagValue = new OLD_ArtifactEffectGoldBagValue();
				old_ArtifactEffectGoldBagValue.amount = (float)num;
				artifact.effects.Add(old_ArtifactEffectGoldBagValue);
				break;
			}
			case ArtifactEffectType.TimeWarpSpeed:
			{
				OLD_ArtifactEffectTimeWarpSpeed old_ArtifactEffectTimeWarpSpeed = new OLD_ArtifactEffectTimeWarpSpeed();
				old_ArtifactEffectTimeWarpSpeed.amount = (float)num;
				artifact.effects.Add(old_ArtifactEffectTimeWarpSpeed);
				break;
			}
			case ArtifactEffectType.TimeWarpDuration:
			{
				OLD_ArtifactEffectTimeWarpDuration old_ArtifactEffectTimeWarpDuration = new OLD_ArtifactEffectTimeWarpDuration();
				old_ArtifactEffectTimeWarpDuration.amount = (float)num;
				artifact.effects.Add(old_ArtifactEffectTimeWarpDuration);
				break;
			}
			case ArtifactEffectType.QuickWaveAfterBoss:
			{
				OLD_ArtifactEffectQuickWaveAfterBoss old_ArtifactEffectQuickWaveAfterBoss = new OLD_ArtifactEffectQuickWaveAfterBoss();
				old_ArtifactEffectQuickWaveAfterBoss.amount = (float)num;
				artifact.effects.Add(old_ArtifactEffectQuickWaveAfterBoss);
				break;
			}
			case ArtifactEffectType.TotemUltraCrit:
			{
				MythicalArtifactHalfRing mythicalArtifactHalfRing = new MythicalArtifactHalfRing();
				mythicalArtifactHalfRing.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactHalfRing);
				break;
			}
			case ArtifactEffectType.StageSkipChance:
			{
				MythicalArtifactBrokenTeleporter mythicalArtifactBrokenTeleporter = new MythicalArtifactBrokenTeleporter();
				mythicalArtifactBrokenTeleporter.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactBrokenTeleporter);
				break;
			}
			case ArtifactEffectType.LifeBoat:
			{
				MythicalArtifactLifeBoat mythicalArtifactLifeBoat = new MythicalArtifactLifeBoat();
				mythicalArtifactLifeBoat.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactLifeBoat);
				break;
			}
			case ArtifactEffectType.FastSpawn:
			{
				OLD_ArtifactEffectFastSpawn old_ArtifactEffectFastSpawn = new OLD_ArtifactEffectFastSpawn();
				old_ArtifactEffectFastSpawn.amount = (int)num;
				artifact.effects.Add(old_ArtifactEffectFastSpawn);
				break;
			}
			case ArtifactEffectType.RaidTreasureGoblinChance:
			{
				MythicalArtifactGoblinLure mythicalArtifactGoblinLure = new MythicalArtifactGoblinLure();
				mythicalArtifactGoblinLure.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactGoblinLure);
				break;
			}
			case ArtifactEffectType.PerfectQuasi:
			{
				MythicalArtifactPerfectQuasi mythicalArtifactPerfectQuasi = new MythicalArtifactPerfectQuasi();
				mythicalArtifactPerfectQuasi.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactPerfectQuasi);
				break;
			}
			case ArtifactEffectType.HealthEnemy:
			{
				OLD_ArtifactEffectHealthEnemy old_ArtifactEffectHealthEnemy = new OLD_ArtifactEffectHealthEnemy();
				old_ArtifactEffectHealthEnemy.amount = num;
				artifact.effects.Add(old_ArtifactEffectHealthEnemy);
				break;
			}
			case ArtifactEffectType.DamageEnemy:
			{
				OLD_ArtifactEffectDamageEnemy old_ArtifactEffectDamageEnemy = new OLD_ArtifactEffectDamageEnemy();
				old_ArtifactEffectDamageEnemy.amount = num;
				artifact.effects.Add(old_ArtifactEffectDamageEnemy);
				break;
			}
			case ArtifactEffectType.DamageBoss:
			{
				OLD_ArtifactEffectDamageBoss old_ArtifactEffectDamageBoss = new OLD_ArtifactEffectDamageBoss();
				old_ArtifactEffectDamageBoss.amount = num;
				artifact.effects.Add(old_ArtifactEffectDamageBoss);
				break;
			}
			case ArtifactEffectType.GearMultiplier:
			{
				MythicalArtifactCustomTailor mythicalArtifactCustomTailor = new MythicalArtifactCustomTailor();
				mythicalArtifactCustomTailor.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactCustomTailor);
				break;
			}
			case ArtifactEffectType.DpsMaster:
			{
				MythicalArtifactDPSMatter mythicalArtifactDPSMatter = new MythicalArtifactDPSMatter();
				mythicalArtifactDPSMatter.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactDPSMatter);
				break;
			}
			case ArtifactEffectType.FreeUpgrade:
			{
				MythicalArtifactFreeExploiter mythicalArtifactFreeExploiter = new MythicalArtifactFreeExploiter();
				mythicalArtifactFreeExploiter.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactFreeExploiter);
				break;
			}
			case ArtifactEffectType.OldCrucible:
			{
				MythicalArtifactOldCrucible mythicalArtifactOldCrucible = new MythicalArtifactOldCrucible();
				mythicalArtifactOldCrucible.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactOldCrucible);
				break;
			}
			case ArtifactEffectType.IdleGoldGain:
			{
				MythicalArtifactAutoTransmuter mythicalArtifactAutoTransmuter = new MythicalArtifactAutoTransmuter();
				mythicalArtifactAutoTransmuter.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactAutoTransmuter);
				break;
			}
			case ArtifactEffectType.AutoUpgrade:
			{
				MythicalArtifactLazyFinger mythicalArtifactLazyFinger = new MythicalArtifactLazyFinger();
				mythicalArtifactLazyFinger.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactLazyFinger);
				break;
			}
			case ArtifactEffectType.ShinyObject:
			{
				MythicalArtifactShinyObject mythicalArtifactShinyObject = new MythicalArtifactShinyObject();
				mythicalArtifactShinyObject.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactShinyObject);
				break;
			}
			case ArtifactEffectType.PowerupCritChance:
			{
				MythicalArtifactBluntRelic mythicalArtifactBluntRelic = new MythicalArtifactBluntRelic();
				mythicalArtifactBluntRelic.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactBluntRelic);
				break;
			}
			case ArtifactEffectType.PowerupCooldown:
			{
				MythicalArtifactImpatientRelic mythicalArtifactImpatientRelic = new MythicalArtifactImpatientRelic();
				mythicalArtifactImpatientRelic.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactImpatientRelic);
				break;
			}
			case ArtifactEffectType.PowerupRevive:
			{
				MythicalArtifactBandAidRelic mythicalArtifactBandAidRelic = new MythicalArtifactBandAidRelic();
				mythicalArtifactBandAidRelic.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactBandAidRelic);
				break;
			}
			case ArtifactEffectType.DamageHeroNonSkill:
			{
				OLD_ArtifactEffectDamageHeroNonSkill old_ArtifactEffectDamageHeroNonSkill = new OLD_ArtifactEffectDamageHeroNonSkill();
				old_ArtifactEffectDamageHeroNonSkill.amount = num;
				artifact.effects.Add(old_ArtifactEffectDamageHeroNonSkill);
				break;
			}
			case ArtifactEffectType.BodilyHarm:
			{
				MythicalArtifactBodilyHarm mythicalArtifactBodilyHarm = new MythicalArtifactBodilyHarm();
				mythicalArtifactBodilyHarm.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactBodilyHarm);
				break;
			}
			case ArtifactEffectType.ChampionsBounty:
			{
				MythicalArtifactChampionsBounty mythicalArtifactChampionsBounty = new MythicalArtifactChampionsBounty();
				mythicalArtifactChampionsBounty.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactChampionsBounty);
				break;
			}
			case ArtifactEffectType.CorpusImperium:
			{
				MythicalArtifactCorpusImperium mythicalArtifactCorpusImperium = new MythicalArtifactCorpusImperium();
				mythicalArtifactCorpusImperium.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactCorpusImperium);
				break;
			}
			case ArtifactEffectType.ShieldCount:
			{
				OLD_ArtifactEffectShieldCount old_ArtifactEffectShieldCount = new OLD_ArtifactEffectShieldCount();
				old_ArtifactEffectShieldCount.amount = (int)num;
				artifact.effects.Add(old_ArtifactEffectShieldCount);
				break;
			}
			case ArtifactEffectType.ShieldDuration:
			{
				OLD_ArtifactEffectShieldDuration old_ArtifactEffectShieldDuration = new OLD_ArtifactEffectShieldDuration();
				old_ArtifactEffectShieldDuration.amount = (float)num;
				artifact.effects.Add(old_ArtifactEffectShieldDuration);
				break;
			}
			case ArtifactEffectType.HorseshoeCount:
			{
				OLD_ArtifactEffectHorseshoeCount old_ArtifactEffectHorseshoeCount = new OLD_ArtifactEffectHorseshoeCount();
				old_ArtifactEffectHorseshoeCount.amount = (int)num;
				artifact.effects.Add(old_ArtifactEffectHorseshoeCount);
				break;
			}
			case ArtifactEffectType.HorseshoeDuration:
			{
				OLD_ArtifactEffectHorseshoeDuration old_ArtifactEffectHorseshoeDuration = new OLD_ArtifactEffectHorseshoeDuration();
				old_ArtifactEffectHorseshoeDuration.amount = (float)num;
				artifact.effects.Add(old_ArtifactEffectHorseshoeDuration);
				break;
			}
			case ArtifactEffectType.HorseshoeValue:
			{
				OLD_ArtifactEffectHorseshoeValue old_ArtifactEffectHorseshoeValue = new OLD_ArtifactEffectHorseshoeValue();
				old_ArtifactEffectHorseshoeValue.amount = (float)num;
				artifact.effects.Add(old_ArtifactEffectHorseshoeValue);
				break;
			}
			case ArtifactEffectType.DestructionCount:
			{
				OLD_ArtifactEffectDestructionCount old_ArtifactEffectDestructionCount = new OLD_ArtifactEffectDestructionCount();
				old_ArtifactEffectDestructionCount.amount = (int)num;
				artifact.effects.Add(old_ArtifactEffectDestructionCount);
				break;
			}
			case ArtifactEffectType.HeroDamagePerAttacker:
			{
				MythicalArtifactCrestOfViloence mythicalArtifactCrestOfViloence = new MythicalArtifactCrestOfViloence();
				mythicalArtifactCrestOfViloence.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactCrestOfViloence);
				break;
			}
			case ArtifactEffectType.HeroHealthPerDefender:
			{
				MythicalArtifactCrestOfSturdiness mythicalArtifactCrestOfSturdiness = new MythicalArtifactCrestOfSturdiness();
				mythicalArtifactCrestOfSturdiness.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactCrestOfSturdiness);
				break;
			}
			case ArtifactEffectType.GoldBonusPerSupporter:
			{
				MythicalArtifactCrestOfUsefulness mythicalArtifactCrestOfUsefulness = new MythicalArtifactCrestOfUsefulness();
				mythicalArtifactCrestOfUsefulness.SetRank((int)num);
				artifact.effects.Add(mythicalArtifactCrestOfUsefulness);
				break;
			}
			default:
				throw new NotImplementedException();
			}
			i++;
		}
		return artifact;
	}

	public static SkillTreeProgressSerializable ConvertSkillTreeProgress(SkillTreeProgress skillTreeProgress)
	{
		return new SkillTreeProgressSerializable
		{
			ulti = skillTreeProgress.ulti,
			branches = skillTreeProgress.branches
		};
	}

	public static SkillTreeProgress ConvertSkillTreeProgress(SkillTreeProgressSerializable skillTreeProgressSerializable)
	{
		return new SkillTreeProgress
		{
			ulti = skillTreeProgressSerializable.ulti,
			branches = skillTreeProgressSerializable.branches
		};
	}

	public static int ConvertGameMode(GameMode mode)
	{
		switch (mode)
		{
		case GameMode.STANDARD:
			return 1;
		case GameMode.SOLO:
			return 2;
		case GameMode.CRUSADE:
			return 3;
		case GameMode.RIFT:
			return 4;
		default:
			throw new NotImplementedException();
		}
	}

	public static GameMode ConvertGameMode(int mode)
	{
		switch (mode)
		{
		case 1:
			return GameMode.STANDARD;
		case 2:
			return GameMode.SOLO;
		case 3:
			return GameMode.CRUSADE;
		case 4:
			return GameMode.RIFT;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertChallengeState(Challenge.State state)
	{
		switch (state)
		{
		case Challenge.State.SETUP:
			return 1;
		case Challenge.State.ACTION:
			return 2;
		case Challenge.State.WON:
			return 3;
		case Challenge.State.LOST:
			return 4;
		default:
			throw new NotImplementedException();
		}
	}

	public static Challenge.State ConvertChallengeState(int state)
	{
		switch (state)
		{
		case 1:
			return Challenge.State.SETUP;
		case 2:
			return Challenge.State.ACTION;
		case 3:
			return Challenge.State.WON;
		case 4:
			return Challenge.State.LOST;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertTutStateFirst(TutorialManager.First state)
	{
		switch (state)
		{
		case TutorialManager.First.WELCOME:
			return 1;
		case TutorialManager.First.RING_OFFER:
			return 2;
		case TutorialManager.First.RING_CLAIMED:
			return 3;
		case TutorialManager.First.FIGHT_RING:
			return 4;
		case TutorialManager.First.HEROES_TAB_UNLOCK:
			return 5;
		case TutorialManager.First.RING_UPGRADE:
			return 6;
		case TutorialManager.First.RING_UPGRADE_DONE:
			return 7;
		case TutorialManager.First.FIGHT_RING_2:
			return 8;
		case TutorialManager.First.HERO_AVAILABLE:
			return 9;
		case TutorialManager.First.HERO_BUY_BUTTON:
			return 10;
		case TutorialManager.First.FIGHT_HERO:
			return 11;
		case TutorialManager.First.FIGHT_HERO_BOSS_WAIT:
			return 12;
		case TutorialManager.First.ULTIMATE_SELECT:
			return 13;
		case TutorialManager.First.FIGHT_HERO_BOSS_DIE:
			return 14;
		case TutorialManager.First.FIGHT_HERO_2:
			return 15;
		case TutorialManager.First.HERO_UPGRADE_AVAILABLE:
			return 16;
		case TutorialManager.First.HERO_UPGRADE_TAB:
			return 17;
		case TutorialManager.First.FIN:
			return 18;
		default:
			throw new NotImplementedException();
		}
	}

	public static TutorialManager.First ConvertTutStateFirst(int state)
	{
		switch (state)
		{
		case 1:
			return TutorialManager.First.WELCOME;
		case 2:
			return TutorialManager.First.WELCOME;
		case 3:
			return TutorialManager.First.WELCOME;
		case 4:
			return TutorialManager.First.FIGHT_RING;
		case 5:
			return TutorialManager.First.HEROES_TAB_UNLOCK;
		case 6:
			return TutorialManager.First.RING_UPGRADE;
		case 7:
			return TutorialManager.First.RING_UPGRADE_DONE;
		case 8:
			return TutorialManager.First.FIGHT_RING_2;
		case 9:
			return TutorialManager.First.HERO_AVAILABLE;
		case 10:
			return TutorialManager.First.HERO_BUY_BUTTON;
		case 11:
			return TutorialManager.First.FIGHT_HERO;
		case 12:
			return TutorialManager.First.FIGHT_HERO;
		case 13:
			return TutorialManager.First.FIGHT_HERO;
		case 14:
			return TutorialManager.First.FIGHT_HERO_BOSS_DIE;
		case 15:
			return TutorialManager.First.FIGHT_HERO_2;
		case 16:
			return TutorialManager.First.HERO_UPGRADE_AVAILABLE;
		case 17:
			return TutorialManager.First.HERO_UPGRADE_TAB;
		case 18:
			return TutorialManager.First.FIN;
		case 19:
			return TutorialManager.First.FIN;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertTutStateHub(TutorialManager.HubTab state)
	{
		if (state == TutorialManager.HubTab.BEFORE_BEGIN)
		{
			return 1;
		}
		if (state != TutorialManager.HubTab.FIN)
		{
			throw new NotImplementedException();
		}
		return 2;
	}

	public static TutorialManager.HubTab ConvertTutStateHub(int state)
	{
		if (state == 1)
		{
			return TutorialManager.HubTab.BEFORE_BEGIN;
		}
		if (state != 2)
		{
			throw new NotImplementedException();
		}
		return TutorialManager.HubTab.FIN;
	}

	public static int ConvertTutStateMode(TutorialManager.ModeTab state)
	{
		switch (state)
		{
		case TutorialManager.ModeTab.BEFORE_BEGIN:
			return 1;
		case TutorialManager.ModeTab.UNLOCKED:
			return 2;
		case TutorialManager.ModeTab.IN_TAB:
			return 3;
		case TutorialManager.ModeTab.FIN:
			return 4;
		default:
			throw new NotImplementedException();
		}
	}

	public static TutorialManager.ModeTab ConvertTutStateMode(int state)
	{
		switch (state)
		{
		case 1:
			return TutorialManager.ModeTab.BEFORE_BEGIN;
		case 2:
			return TutorialManager.ModeTab.UNLOCKED;
		case 3:
			return TutorialManager.ModeTab.IN_TAB;
		case 4:
			return TutorialManager.ModeTab.FIN;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertTutStateArtifact(TutorialManager.ArtifactsTab state)
	{
		switch (state)
		{
		case TutorialManager.ArtifactsTab.BEFORE_BEGIN:
			return 1;
		case TutorialManager.ArtifactsTab.UNLOCKED:
			return 2;
		case TutorialManager.ArtifactsTab.IN_TAB:
			return 3;
		case TutorialManager.ArtifactsTab.CRAFTING_ARTIFACT:
			return 4;
		case TutorialManager.ArtifactsTab.SELECT_ARTIFACT:
			return 5;
		case TutorialManager.ArtifactsTab.FIN:
			return 6;
		default:
			throw new NotImplementedException();
		}
	}

	public static TutorialManager.ArtifactsTab ConvertTutStateArtifact(int state)
	{
		switch (state)
		{
		case 1:
			return TutorialManager.ArtifactsTab.BEFORE_BEGIN;
		case 2:
			return TutorialManager.ArtifactsTab.UNLOCKED;
		case 3:
			return TutorialManager.ArtifactsTab.IN_TAB;
		case 4:
			return TutorialManager.ArtifactsTab.CRAFTING_ARTIFACT;
		case 5:
			return TutorialManager.ArtifactsTab.SELECT_ARTIFACT;
		case 6:
			return TutorialManager.ArtifactsTab.FIN;
		case 7:
			return TutorialManager.ArtifactsTab.FIN;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertTutStateShop(TutorialManager.ShopTab state)
	{
		switch (state)
		{
		case TutorialManager.ShopTab.BEFORE_BEGIN:
			return 1;
		case TutorialManager.ShopTab.UNLOCKED:
			return 2;
		case TutorialManager.ShopTab.IN_TAB:
			return 3;
		case TutorialManager.ShopTab.LOOTPACK_OPENED:
			return 5;
		case TutorialManager.ShopTab.GO_TO_GEARS:
			return 4;
		case TutorialManager.ShopTab.FIN:
			return 6;
		default:
			throw new NotImplementedException();
		}
	}

	public static TutorialManager.ShopTab ConvertTutStateShop(int state)
	{
		switch (state)
		{
		case 1:
			return TutorialManager.ShopTab.BEFORE_BEGIN;
		case 2:
			return TutorialManager.ShopTab.UNLOCKED;
		case 3:
			return TutorialManager.ShopTab.IN_TAB;
		case 4:
			return TutorialManager.ShopTab.GO_TO_GEARS;
		case 5:
			return TutorialManager.ShopTab.LOOTPACK_OPENED;
		case 6:
			return TutorialManager.ShopTab.FIN;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertTutStatePrestige(TutorialManager.Prestige state)
	{
		switch (state)
		{
		case TutorialManager.Prestige.BEFORE_BEGIN:
			return 1;
		case TutorialManager.Prestige.UNLOCKED:
			return 2;
		case TutorialManager.Prestige.IN_TAB:
			return 3;
		case TutorialManager.Prestige.FIN:
			return 4;
		default:
			throw new NotImplementedException();
		}
	}

	public static TutorialManager.Prestige ConvertTutStatePrestige(int state)
	{
		switch (state)
		{
		case 1:
			return TutorialManager.Prestige.BEFORE_BEGIN;
		case 2:
			return TutorialManager.Prestige.UNLOCKED;
		case 3:
			return TutorialManager.Prestige.IN_TAB;
		case 4:
			return TutorialManager.Prestige.FIN;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertTutStateSkillScreen(TutorialManager.SkillScreen state)
	{
		if (state == TutorialManager.SkillScreen.BEFORE_BEGIN)
		{
			return 1;
		}
		if (state == TutorialManager.SkillScreen.UNLOCKED)
		{
			return 2;
		}
		if (state != TutorialManager.SkillScreen.FIN)
		{
			throw new NotImplementedException();
		}
		return 3;
	}

	public static TutorialManager.SkillScreen ConvertTutStateSkillScreen(int state)
	{
		if (state == 1)
		{
			return TutorialManager.SkillScreen.BEFORE_BEGIN;
		}
		if (state == 2)
		{
			return TutorialManager.SkillScreen.UNLOCKED;
		}
		if (state != 3)
		{
			throw new NotImplementedException();
		}
		return TutorialManager.SkillScreen.FIN;
	}

	public static int ConvertTutStateFightBossButton(TutorialManager.FightBossButton state)
	{
		switch (state)
		{
		case TutorialManager.FightBossButton.BEFORE_BEGIN:
			return 1;
		case TutorialManager.FightBossButton.WAIT:
			return 2;
		case TutorialManager.FightBossButton.SHOW_BUTTON:
			return 3;
		case TutorialManager.FightBossButton.FIN:
			return 4;
		default:
			throw new NotImplementedException();
		}
	}

	public static TutorialManager.FightBossButton ConvertTutStateFightBossButton(int state)
	{
		switch (state)
		{
		case 1:
			return TutorialManager.FightBossButton.BEFORE_BEGIN;
		case 2:
			return TutorialManager.FightBossButton.WAIT;
		case 3:
			return TutorialManager.FightBossButton.SHOW_BUTTON;
		case 4:
			return TutorialManager.FightBossButton.FIN;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertTutStateGearScreen(TutorialManager.GearScreen state)
	{
		if (state == TutorialManager.GearScreen.BEFORE_BEGIN)
		{
			return 1;
		}
		if (state == TutorialManager.GearScreen.UNLOCKED)
		{
			return 2;
		}
		if (state != TutorialManager.GearScreen.FIN)
		{
			throw new NotImplementedException();
		}
		return 3;
	}

	public static TutorialManager.GearScreen ConvertTutStateGearScreen(int state)
	{
		switch (state)
		{
		case 0:
			return TutorialManager.GearScreen.BEFORE_BEGIN;
		case 1:
			return TutorialManager.GearScreen.BEFORE_BEGIN;
		case 2:
			return TutorialManager.GearScreen.UNLOCKED;
		case 3:
			return TutorialManager.GearScreen.FIN;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertTutStateRingPrestigeReminder(TutorialManager.RingPrestigeReminder state)
	{
		if (state == TutorialManager.RingPrestigeReminder.BEFORE_BEGIN)
		{
			return 0;
		}
		if (state == TutorialManager.RingPrestigeReminder.UNLOCKED)
		{
			return 1;
		}
		if (state != TutorialManager.RingPrestigeReminder.FIN)
		{
			throw new NotImplementedException();
		}
		return 2;
	}

	public static TutorialManager.RingPrestigeReminder ConvertTutStateRingPrestigeReminder(int state)
	{
		if (state == 0)
		{
			return TutorialManager.RingPrestigeReminder.BEFORE_BEGIN;
		}
		if (state == 1)
		{
			return TutorialManager.RingPrestigeReminder.UNLOCKED;
		}
		if (state != 2)
		{
			throw new NotImplementedException();
		}
		return TutorialManager.RingPrestigeReminder.FIN;
	}

	public static int ConvertTutStateHeroPrestigeReminder(TutorialManager.HeroPrestigeReminder state)
	{
		if (state == TutorialManager.HeroPrestigeReminder.BEFORE_BEGIN)
		{
			return 0;
		}
		if (state == TutorialManager.HeroPrestigeReminder.UNLOCKED)
		{
			return 1;
		}
		if (state != TutorialManager.HeroPrestigeReminder.FIN)
		{
			throw new NotImplementedException();
		}
		return 2;
	}

	public static TutorialManager.HeroPrestigeReminder ConvertTutStateHeroPrestigeReminder(int state)
	{
		if (state == 0)
		{
			return TutorialManager.HeroPrestigeReminder.BEFORE_BEGIN;
		}
		if (state == 1)
		{
			return TutorialManager.HeroPrestigeReminder.UNLOCKED;
		}
		if (state != 2)
		{
			throw new NotImplementedException();
		}
		return TutorialManager.HeroPrestigeReminder.FIN;
	}

	public static int ConvertTutStateMythicalArtifactsTab(TutorialManager.MythicalArtifactsTab state)
	{
		if (state == TutorialManager.MythicalArtifactsTab.BEFORE_BEGIN)
		{
			return 0;
		}
		if (state == TutorialManager.MythicalArtifactsTab.UNLOCKED)
		{
			return 1;
		}
		if (state != TutorialManager.MythicalArtifactsTab.FIN)
		{
			throw new NotImplementedException();
		}
		return 2;
	}

	public static TutorialManager.MythicalArtifactsTab ConvertTutStateMythicalArtifactsTab(int state)
	{
		if (state == 0)
		{
			return TutorialManager.MythicalArtifactsTab.BEFORE_BEGIN;
		}
		if (state == 1)
		{
			return TutorialManager.MythicalArtifactsTab.UNLOCKED;
		}
		if (state != 2)
		{
			throw new NotImplementedException();
		}
		return TutorialManager.MythicalArtifactsTab.FIN;
	}

	public static int ConvertTutStateRuneScreen(TutorialManager.RuneScreen state)
	{
		if (state == TutorialManager.RuneScreen.BEFORE_BEGIN)
		{
			return 1;
		}
		if (state == TutorialManager.RuneScreen.UNLOCKED)
		{
			return 2;
		}
		if (state != TutorialManager.RuneScreen.FIN)
		{
			throw new NotImplementedException();
		}
		return 3;
	}

	public static TutorialManager.RuneScreen ConvertTutStateRuneScreen(int state)
	{
		if (state == 1)
		{
			return TutorialManager.RuneScreen.BEFORE_BEGIN;
		}
		if (state == 2)
		{
			return TutorialManager.RuneScreen.UNLOCKED;
		}
		if (state != 3)
		{
			throw new NotImplementedException();
		}
		return TutorialManager.RuneScreen.FIN;
	}

	public static int ConvertTutStateTrinketShop(TutorialManager.TrinketShop state)
	{
		if (state == TutorialManager.TrinketShop.BEFORE_BEGIN)
		{
			return 0;
		}
		if (state == TutorialManager.TrinketShop.UNLOCKED)
		{
			return 1;
		}
		if (state != TutorialManager.TrinketShop.FIN)
		{
			throw new NotImplementedException();
		}
		return 2;
	}

	public static TutorialManager.TrinketShop ConvertTutStateTrinketShop(int state)
	{
		if (state == 0)
		{
			return TutorialManager.TrinketShop.BEFORE_BEGIN;
		}
		if (state == 1)
		{
			return TutorialManager.TrinketShop.UNLOCKED;
		}
		if (state != 2)
		{
			throw new NotImplementedException();
		}
		return TutorialManager.TrinketShop.FIN;
	}

	public static int ConvertTutStateTrinketHeroTab(TutorialManager.TrinketHeroTab state)
	{
		switch (state)
		{
		case TutorialManager.TrinketHeroTab.BEFORE_BEGIN:
			return 0;
		case TutorialManager.TrinketHeroTab.UNLOCKED:
			return 1;
		case TutorialManager.TrinketHeroTab.EQUIP:
			return 2;
		case TutorialManager.TrinketHeroTab.EFFECTS:
			return 3;
		case TutorialManager.TrinketHeroTab.FIN:
			return 4;
		default:
			throw new NotImplementedException();
		}
	}

	public static TutorialManager.TrinketHeroTab ConvertTutStateTrinketHeroTab(int state)
	{
		switch (state)
		{
		case 0:
			return TutorialManager.TrinketHeroTab.BEFORE_BEGIN;
		case 1:
			return TutorialManager.TrinketHeroTab.UNLOCKED;
		case 2:
			return TutorialManager.TrinketHeroTab.EQUIP;
		case 3:
			return TutorialManager.TrinketHeroTab.EFFECTS;
		case 4:
			return TutorialManager.TrinketHeroTab.FIN;
		case 5:
			return TutorialManager.TrinketHeroTab.FIN;
		case 6:
			return TutorialManager.TrinketHeroTab.FIN;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertTutStateMineUnlock(TutorialManager.MineUnlock state)
	{
		if (state == TutorialManager.MineUnlock.BEFORE_BEGIN)
		{
			return 0;
		}
		if (state == TutorialManager.MineUnlock.UNLOCKED)
		{
			return 1;
		}
		if (state != TutorialManager.MineUnlock.FIN)
		{
			throw new NotImplementedException();
		}
		return 2;
	}

	public static int ConvertTutStateDailyUnlock(TutorialManager.DailyUnlock state)
	{
		if (state == TutorialManager.DailyUnlock.BEFORE_BEGIN)
		{
			return 0;
		}
		if (state == TutorialManager.DailyUnlock.UNLOCKED)
		{
			return 1;
		}
		if (state != TutorialManager.DailyUnlock.FIN)
		{
			throw new NotImplementedException();
		}
		return 2;
	}

	public static int ConvertTutStateDailyComplete(TutorialManager.DailyComplete state)
	{
		if (state == TutorialManager.DailyComplete.BEFORE_BEGIN)
		{
			return 0;
		}
		if (state == TutorialManager.DailyComplete.UNLOCKED)
		{
			return 1;
		}
		if (state != TutorialManager.DailyComplete.FIN)
		{
			throw new NotImplementedException();
		}
		return 2;
	}

	public static TutorialManager.MineUnlock ConvertTutStateMineUnlock(int state)
	{
		if (state == 0)
		{
			return TutorialManager.MineUnlock.BEFORE_BEGIN;
		}
		if (state == 1)
		{
			return TutorialManager.MineUnlock.UNLOCKED;
		}
		if (state != 2)
		{
			throw new NotImplementedException();
		}
		return TutorialManager.MineUnlock.FIN;
	}

	public static TutorialManager.DailyUnlock ConvertTutStateDailyUnlock(int state)
	{
		if (state == 0)
		{
			return TutorialManager.DailyUnlock.BEFORE_BEGIN;
		}
		if (state == 1)
		{
			return TutorialManager.DailyUnlock.UNLOCKED;
		}
		if (state != 2)
		{
			throw new NotImplementedException();
		}
		return TutorialManager.DailyUnlock.FIN;
	}

	public static TutorialManager.DailyComplete ConvertTutStateDailyComplete(int state)
	{
		if (state == 0)
		{
			return TutorialManager.DailyComplete.BEFORE_BEGIN;
		}
		if (state == 1)
		{
			return TutorialManager.DailyComplete.UNLOCKED;
		}
		if (state != 2)
		{
			throw new NotImplementedException();
		}
		return TutorialManager.DailyComplete.FIN;
	}

	public static int ConvertTutStateRiftsUnlocked(TutorialManager.RiftsUnlock state)
	{
		if (state == TutorialManager.RiftsUnlock.BEFORE_BEGIN)
		{
			return 0;
		}
		if (state == TutorialManager.RiftsUnlock.UNLOCKED)
		{
			return 1;
		}
		if (state != TutorialManager.RiftsUnlock.FIN)
		{
			throw new NotImplementedException();
		}
		return 2;
	}

	public static TutorialManager.RiftsUnlock ConvertTutStateRiftsUnlocked(int state)
	{
		if (state == 0)
		{
			return TutorialManager.RiftsUnlock.BEFORE_BEGIN;
		}
		if (state == 1)
		{
			return TutorialManager.RiftsUnlock.UNLOCKED;
		}
		if (state != 2)
		{
			throw new NotImplementedException();
		}
		return TutorialManager.RiftsUnlock.FIN;
	}

	public static int ConvertTutStateRiftEffects(TutorialManager.RiftEffects state)
	{
		if (state == TutorialManager.RiftEffects.BEFORE_BEGIN)
		{
			return 0;
		}
		if (state == TutorialManager.RiftEffects.IN_TAB)
		{
			return 1;
		}
		if (state != TutorialManager.RiftEffects.FIN)
		{
			throw new NotImplementedException();
		}
		return 2;
	}

	public static TutorialManager.RiftEffects ConvertTutStateRiftEffects(int state)
	{
		if (state == 0)
		{
			return TutorialManager.RiftEffects.BEFORE_BEGIN;
		}
		if (state == 1)
		{
			return TutorialManager.RiftEffects.IN_TAB;
		}
		if (state != 2)
		{
			throw new NotImplementedException();
		}
		return TutorialManager.RiftEffects.FIN;
	}

	public static int ConvertTutStateFirstCharm(TutorialManager.FirstCharm state)
	{
		switch (state)
		{
		case TutorialManager.FirstCharm.BEFORE_BEGIN:
			return 0;
		case TutorialManager.FirstCharm.WARNING:
			return 1;
		case TutorialManager.FirstCharm.WAIT_SELECT:
			return 2;
		case TutorialManager.FirstCharm.SHOW_COLLECTION:
			return 3;
		case TutorialManager.FirstCharm.EXPLAIN_EFFECTS:
			return 4;
		case TutorialManager.FirstCharm.EXPLAIN_TRIGGER:
			return 5;
		case TutorialManager.FirstCharm.FIN:
			return 6;
		default:
			throw new NotImplementedException();
		}
	}

	public static TutorialManager.FirstCharm ConvertTutStateFirstCharm(int state)
	{
		switch (state)
		{
		case 0:
			return TutorialManager.FirstCharm.BEFORE_BEGIN;
		case 1:
			return TutorialManager.FirstCharm.WARNING;
		case 2:
			return TutorialManager.FirstCharm.WAIT_SELECT;
		case 3:
			return TutorialManager.FirstCharm.SHOW_COLLECTION;
		case 4:
			return TutorialManager.FirstCharm.EXPLAIN_EFFECTS;
		case 5:
			return TutorialManager.FirstCharm.EXPLAIN_TRIGGER;
		case 6:
			return TutorialManager.FirstCharm.FIN;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertTutStateCharmHub(TutorialManager.CharmHub state)
	{
		switch (state)
		{
		case TutorialManager.CharmHub.BEFORE_BEGIN:
			return 0;
		case TutorialManager.CharmHub.OPEN_COLLECTION:
			return 1;
		case TutorialManager.CharmHub.MESSAGE_1:
			return 2;
		case TutorialManager.CharmHub.MESSAGE_2:
			return 3;
		case TutorialManager.CharmHub.FIN:
			return 4;
		default:
			throw new NotImplementedException();
		}
	}

	public static TutorialManager.CharmHub ConvertTutStateCharmHub(int state)
	{
		switch (state)
		{
		case 0:
			return TutorialManager.CharmHub.BEFORE_BEGIN;
		case 1:
			return TutorialManager.CharmHub.OPEN_COLLECTION;
		case 2:
			return TutorialManager.CharmHub.MESSAGE_1;
		case 3:
			return TutorialManager.CharmHub.MESSAGE_2;
		case 4:
			return TutorialManager.CharmHub.FIN;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertTutStateFirstCharmPack(TutorialManager.FirstCharmPack state)
	{
		switch (state)
		{
		case TutorialManager.FirstCharmPack.BEFORE_BEGIN:
			return 0;
		case TutorialManager.FirstCharmPack.OPEN_SHOP:
			return 1;
		case TutorialManager.FirstCharmPack.WAIT_TO_OPEN:
			return 2;
		case TutorialManager.FirstCharmPack.OPEN_PACK:
			return 3;
		case TutorialManager.FirstCharmPack.FIN:
			return 4;
		default:
			throw new NotImplementedException();
		}
	}

	public static TutorialManager.FirstCharmPack ConvertTutStateFirstCharmPack(int state)
	{
		switch (state)
		{
		case 0:
			return TutorialManager.FirstCharmPack.BEFORE_BEGIN;
		case 1:
			return TutorialManager.FirstCharmPack.OPEN_SHOP;
		case 2:
			return TutorialManager.FirstCharmPack.WAIT_TO_OPEN;
		case 3:
			return TutorialManager.FirstCharmPack.OPEN_PACK;
		case 4:
			return TutorialManager.FirstCharmPack.FIN;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertTutStateCharmLevelUp(TutorialManager.CharmLevelUp state)
	{
		switch (state)
		{
		case TutorialManager.CharmLevelUp.BEFORE_BEGIN:
			return 0;
		case TutorialManager.CharmLevelUp.EXIT_SHOP:
			return 1;
		case TutorialManager.CharmLevelUp.OPEN_COLLECTION:
			return 2;
		case TutorialManager.CharmLevelUp.SELECT_CHARM:
			return 3;
		case TutorialManager.CharmLevelUp.EXPLAIN_LEVELUP:
			return 4;
		case TutorialManager.CharmLevelUp.FIN:
			return 5;
		default:
			throw new NotImplementedException();
		}
	}

	public static TutorialManager.CharmLevelUp ConvertTutStateCharmLevelUp(int state)
	{
		switch (state)
		{
		case 0:
			return TutorialManager.CharmLevelUp.BEFORE_BEGIN;
		case 1:
			return TutorialManager.CharmLevelUp.EXIT_SHOP;
		case 2:
			return TutorialManager.CharmLevelUp.OPEN_COLLECTION;
		case 3:
			return TutorialManager.CharmLevelUp.SELECT_CHARM;
		case 4:
			return TutorialManager.CharmLevelUp.EXPLAIN_LEVELUP;
		case 5:
			return TutorialManager.CharmLevelUp.FIN;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertTutStateAeonDust(TutorialManager.AeonDust state)
	{
		if (state == TutorialManager.AeonDust.BEFORE_BEGIN)
		{
			return 0;
		}
		if (state == TutorialManager.AeonDust.COLLECT)
		{
			return 1;
		}
		if (state != TutorialManager.AeonDust.FIN)
		{
			throw new NotImplementedException();
		}
		return 2;
	}

	public static TutorialManager.AeonDust ConvertTutStateAeonDust(int state)
	{
		if (state == 0)
		{
			return TutorialManager.AeonDust.BEFORE_BEGIN;
		}
		if (state == 1)
		{
			return TutorialManager.AeonDust.COLLECT;
		}
		if (state != 2)
		{
			throw new NotImplementedException();
		}
		return TutorialManager.AeonDust.FIN;
	}

	public static int ConvertTutStateRepeatRifts(TutorialManager.RepeatRifts state)
	{
		switch (state)
		{
		case TutorialManager.RepeatRifts.BEFORE_BEGIN:
			return 0;
		case TutorialManager.RepeatRifts.UNLOCK:
			return 1;
		case TutorialManager.RepeatRifts.SELECT:
			return 2;
		case TutorialManager.RepeatRifts.WAIT_CLOSE_SELECT:
			return 3;
		case TutorialManager.RepeatRifts.FINAL_TEXT:
			return 4;
		case TutorialManager.RepeatRifts.FIN:
			return 5;
		default:
			throw new NotImplementedException();
		}
	}

	public static TutorialManager.RepeatRifts ConvertTutStateRepeatRifts(int state)
	{
		switch (state)
		{
		case 0:
			return TutorialManager.RepeatRifts.BEFORE_BEGIN;
		case 1:
			return TutorialManager.RepeatRifts.UNLOCK;
		case 2:
			return TutorialManager.RepeatRifts.SELECT;
		case 3:
			return TutorialManager.RepeatRifts.WAIT_CLOSE_SELECT;
		case 4:
			return TutorialManager.RepeatRifts.FINAL_TEXT;
		case 5:
			return TutorialManager.RepeatRifts.FIN;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertTutStateAllRiftsFinished(TutorialManager.AllRiftsFinished state)
	{
		if (state == TutorialManager.AllRiftsFinished.BEFORE_BEGIN)
		{
			return 0;
		}
		if (state == TutorialManager.AllRiftsFinished.MESSAGE)
		{
			return 1;
		}
		if (state != TutorialManager.AllRiftsFinished.FIN)
		{
			throw new NotImplementedException();
		}
		return 2;
	}

	public static TutorialManager.AllRiftsFinished ConvertTutStateAllRiftsFinished(int state)
	{
		if (state == 0)
		{
			return TutorialManager.AllRiftsFinished.BEFORE_BEGIN;
		}
		if (state == 1)
		{
			return TutorialManager.AllRiftsFinished.MESSAGE;
		}
		if (state != 2)
		{
			throw new NotImplementedException();
		}
		return TutorialManager.AllRiftsFinished.FIN;
	}

	public static int ConvertTutStateFlashOffersUnlocked(TutorialManager.FlashOffersUnlocked state)
	{
		switch (state)
		{
		case TutorialManager.FlashOffersUnlocked.BEFORE_BEGIN:
			return 0;
		case TutorialManager.FlashOffersUnlocked.OPEN_SHOP:
			return 1;
		case TutorialManager.FlashOffersUnlocked.SHOW_MESSAGE:
			return 2;
		case TutorialManager.FlashOffersUnlocked.FIN:
			return 3;
		default:
			throw new NotImplementedException();
		}
	}

	public static TutorialManager.FlashOffersUnlocked ConvertTutStateFlashOffersUnlocked(int state)
	{
		switch (state)
		{
		case 0:
			return TutorialManager.FlashOffersUnlocked.BEFORE_BEGIN;
		case 1:
			return TutorialManager.FlashOffersUnlocked.OPEN_SHOP;
		case 2:
			return TutorialManager.FlashOffersUnlocked.SHOW_MESSAGE;
		case 3:
			return TutorialManager.FlashOffersUnlocked.FIN;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertTutStateCursedGates(TutorialManager.CursedGates state)
	{
		switch (state)
		{
		case TutorialManager.CursedGates.BEFORE_BEGIN:
			return 0;
		case TutorialManager.CursedGates.OPEN_SELECT_GATE_PANEL:
			return 1;
		case TutorialManager.CursedGates.OPEN_CURSES_TAB:
			return 2;
		case TutorialManager.CursedGates.FIN:
			return 3;
		default:
			throw new NotImplementedException();
		}
	}

	public static TutorialManager.CursedGates ConvertTutStateCursedGates(int state)
	{
		switch (state)
		{
		case 0:
			return TutorialManager.CursedGates.BEFORE_BEGIN;
		case 1:
			return TutorialManager.CursedGates.OPEN_SELECT_GATE_PANEL;
		case 2:
			return TutorialManager.CursedGates.OPEN_CURSES_TAB;
		case 3:
			return TutorialManager.CursedGates.FIN;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertTutMissionsFinished(TutorialManager.MissionsFinished state)
	{
		if (state == TutorialManager.MissionsFinished.BEFORE_BEGIN)
		{
			return 0;
		}
		if (state == TutorialManager.MissionsFinished.MESSAGE)
		{
			return 1;
		}
		if (state != TutorialManager.MissionsFinished.FIN)
		{
			throw new NotImplementedException();
		}
		return 2;
	}

	public static TutorialManager.MissionsFinished ConvertTutStateMissionsFinished(int state)
	{
		if (state == 0)
		{
			return TutorialManager.MissionsFinished.BEFORE_BEGIN;
		}
		if (state == 1)
		{
			return TutorialManager.MissionsFinished.MESSAGE;
		}
		if (state != 2)
		{
			throw new NotImplementedException();
		}
		return TutorialManager.MissionsFinished.FIN;
	}

	public static int ConvertTutTrinketSmithingUnlocked(TutorialManager.TrinketSmithingUnlocked state)
	{
		switch (state)
		{
		case TutorialManager.TrinketSmithingUnlocked.BEFORE_BEGIN:
			return 0;
		case TutorialManager.TrinketSmithingUnlocked.GO_TO_HUB:
			return 1;
		case TutorialManager.TrinketSmithingUnlocked.OPEN_TRINKETS_SCREEN:
			return 2;
		case TutorialManager.TrinketSmithingUnlocked.SELECT_SMITHING_TAB:
			return 3;
		case TutorialManager.TrinketSmithingUnlocked.FIN:
			return 4;
		default:
			throw new NotImplementedException();
		}
	}

	public static TutorialManager.TrinketSmithingUnlocked ConvertTutStateTrinketSmithingUnlocked(int state)
	{
		switch (state)
		{
		case 0:
			return TutorialManager.TrinketSmithingUnlocked.BEFORE_BEGIN;
		case 1:
			return TutorialManager.TrinketSmithingUnlocked.GO_TO_HUB;
		case 2:
			return TutorialManager.TrinketSmithingUnlocked.OPEN_TRINKETS_SCREEN;
		case 3:
			return TutorialManager.TrinketSmithingUnlocked.SELECT_SMITHING_TAB;
		case 4:
			return TutorialManager.TrinketSmithingUnlocked.FIN;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertTutTrinketRecycleUnlocked(TutorialManager.TrinketRecycleUnlocked state)
	{
		if (state == TutorialManager.TrinketRecycleUnlocked.BEFORE_BEGIN)
		{
			return 0;
		}
		if (state == TutorialManager.TrinketRecycleUnlocked.MESSAGE)
		{
			return 1;
		}
		if (state != TutorialManager.TrinketRecycleUnlocked.FIN)
		{
			throw new NotImplementedException();
		}
		return 2;
	}

	public static TutorialManager.TrinketRecycleUnlocked ConvertTutStateTrinketRecycleUnlocked(int state)
	{
		if (state == 0)
		{
			return TutorialManager.TrinketRecycleUnlocked.BEFORE_BEGIN;
		}
		if (state == 1)
		{
			return TutorialManager.TrinketRecycleUnlocked.MESSAGE;
		}
		if (state != 2)
		{
			throw new NotImplementedException();
		}
		return TutorialManager.TrinketRecycleUnlocked.FIN;
	}

	public static int ConvertTutChristmasTreeEventUnlocked(TutorialManager.ChristmasTreeEventUnlocked state)
	{
		switch (state)
		{
		case TutorialManager.ChristmasTreeEventUnlocked.BEFORE_BEGIN:
			return 0;
		case TutorialManager.ChristmasTreeEventUnlocked.OPEN_POPUP:
			return 1;
		case TutorialManager.ChristmasTreeEventUnlocked.WAIT_ANIM:
			return 2;
		case TutorialManager.ChristmasTreeEventUnlocked.MESSAGE_1:
			return 3;
		case TutorialManager.ChristmasTreeEventUnlocked.MESSAGE_2:
			return 4;
		case TutorialManager.ChristmasTreeEventUnlocked.OPEN_SHOP_TAB:
			return 5;
		case TutorialManager.ChristmasTreeEventUnlocked.FIN:
			return 7;
		default:
			throw new NotImplementedException();
		}
	}

	public static TutorialManager.ChristmasTreeEventUnlocked ConvertTutStateChristmasTreeEventUnlocked(int state)
	{
		switch (state)
		{
		case 0:
			return TutorialManager.ChristmasTreeEventUnlocked.BEFORE_BEGIN;
		case 1:
			return TutorialManager.ChristmasTreeEventUnlocked.OPEN_POPUP;
		case 2:
			return TutorialManager.ChristmasTreeEventUnlocked.WAIT_ANIM;
		case 3:
			return TutorialManager.ChristmasTreeEventUnlocked.MESSAGE_1;
		case 4:
			return TutorialManager.ChristmasTreeEventUnlocked.MESSAGE_2;
		case 5:
			return TutorialManager.ChristmasTreeEventUnlocked.OPEN_SHOP_TAB;
		case 7:
			return TutorialManager.ChristmasTreeEventUnlocked.FIN;
		}
		throw new NotImplementedException();
	}

	public static int ConverTutArtifactOverhaul(TutorialManager.ArtifactOverhaul state)
	{
		switch (state)
		{
		case TutorialManager.ArtifactOverhaul.BEFORE_BEGIN:
			return 0;
		case TutorialManager.ArtifactOverhaul.WELCOME:
			return 1;
		case TutorialManager.ArtifactOverhaul.WAIT_GIVE_NEW_ARTIFACTS:
			return 2;
		case TutorialManager.ArtifactOverhaul.WAIT_POSITIONING_ARTIFACTS:
			return 3;
		case TutorialManager.ArtifactOverhaul.GIVE_MYTHSTONES:
			return 4;
		case TutorialManager.ArtifactOverhaul.WAIT_DROPS:
			return 5;
		case TutorialManager.ArtifactOverhaul.SELECT_ARTIFACT:
			return 6;
		case TutorialManager.ArtifactOverhaul.FIN:
			return 7;
		default:
			throw new NotImplementedException();
		}
	}

	public static TutorialManager.ArtifactOverhaul ConverTutArtifactOverhaul(int state)
	{
		switch (state)
		{
		case 0:
			return TutorialManager.ArtifactOverhaul.BEFORE_BEGIN;
		case 1:
			return TutorialManager.ArtifactOverhaul.WELCOME;
		case 2:
			return TutorialManager.ArtifactOverhaul.WAIT_GIVE_NEW_ARTIFACTS;
		case 3:
			return TutorialManager.ArtifactOverhaul.WAIT_POSITIONING_ARTIFACTS;
		case 4:
			return TutorialManager.ArtifactOverhaul.GIVE_MYTHSTONES;
		case 5:
			return TutorialManager.ArtifactOverhaul.WAIT_DROPS;
		case 6:
			return TutorialManager.ArtifactOverhaul.SELECT_ARTIFACT;
		case 7:
			return TutorialManager.ArtifactOverhaul.FIN;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertAchievementId(string id)
	{
		switch (id)
		{
		case "CgkIlpSuuo0ZEAIQBw":
			return 1;
		case "CgkIlpSuuo0ZEAIQCA":
			return 2;
		case "CgkIlpSuuo0ZEAIQCQ":
			return 3;
		case "CgkIlpSuuo0ZEAIQCg":
			return 4;
		case "CgkIlpSuuo0ZEAIQCw":
			return 5;
		case "CgkIlpSuuo0ZEAIQDQ":
			return 6;
		case "CgkIlpSuuo0ZEAIQDg":
			return 7;
		case "CgkIlpSuuo0ZEAIQDw":
			return 8;
		case "CgkIlpSuuo0ZEAIQEA":
			return 9;
		case "CgkIlpSuuo0ZEAIQEQ":
			return 10;
		case "CgkIlpSuuo0ZEAIQEg":
			return 11;
		case "CgkIlpSuuo0ZEAIQEw":
			return 12;
		case "CgkIlpSuuo0ZEAIQFA":
			return 13;
		case "CgkIlpSuuo0ZEAIQFQ":
			return 14;
		case "CgkIlpSuuo0ZEAIQFg":
			return 15;
		case "CgkIlpSuuo0ZEAIQFw":
			return 16;
		case "CgkIlpSuuo0ZEAIQGA":
			return 17;
		case "CgkIlpSuuo0ZEAIQGQ":
			return 18;
		case "CgkIlpSuuo0ZEAIQGw":
			return 19;
		case "CgkIlpSuuo0ZEAIQGg":
			return 20;
		case "CgkIlpSuuo0ZEAIQHA":
			return 21;
		case "CgkIlpSuuo0ZEAIQHQ":
			return 22;
		case "CgkIlpSuuo0ZEAIQHg":
			return 23;
		case "CgkIlpSuuo0ZEAIQIA":
			return 24;
		case "CgkIlpSuuo0ZEAIQHw":
			return 25;
		case "CgkIlpSuuo0ZEAIQIQ":
			return 26;
		case "CgkIlpSuuo0ZEAIQIg":
			return 27;
		case "CgkIlpSuuo0ZEAIQIw":
			return 28;
		case "CgkIlpSuuo0ZEAIQJQ":
			return 29;
		case "CgkIlpSuuo0ZEAIQJA":
			return 30;
		case "CgkIlpSuuo0ZEAIQJg":
			return 31;
		case "CgkIlpSuuo0ZEAIQJw":
			return 32;
		case "CgkIlpSuuo0ZEAIQKA":
			return 33;
		case "CgkIlpSuuo0ZEAIQKQ":
			return 34;
		case "CgkIlpSuuo0ZEAIQKg":
			return 35;
		case "CgkIlpSuuo0ZEAIQLA":
			return 36;
		case "CgkIlpSuuo0ZEAIQKw":
			return 37;
		case "CgkIlpSuuo0ZEAIQMw":
			return 38;
		case "CgkIlpSuuo0ZEAIQNA":
			return 39;
		case "CgkIlpSuuo0ZEAIQNQ":
			return 40;
		case "CgkIlpSuuo0ZEAIQNg":
			return 41;
		case "CgkIlpSuuo0ZEAIQNw":
			return 42;
		case "CgkIlpSuuo0ZEAIQLQ":
			return 44;
		case "CgkIlpSuuo0ZEAIQLg":
			return 45;
		case "CgkIlpSuuo0ZEAIQLw":
			return 46;
		case "CgkIlpSuuo0ZEAIQMA":
			return 47;
		case "CgkIlpSuuo0ZEAIQMQ":
			return 48;
		case "CgkIlpSuuo0ZEAIQPQ":
			return 50;
		case "CgkIlpSuuo0ZEAIQPg":
			return 51;
		case "CgkIlpSuuo0ZEAIQPw":
			return 52;
		case "CgkIlpSuuo0ZEAIQQA":
			return 53;
		case "CgkIlpSuuo0ZEAIQQQ":
			return 54;
		}
		throw new NotImplementedException();
	}

	public static string ConvertAchievementId(int index)
	{
		switch (index)
		{
		case 1:
			return "CgkIlpSuuo0ZEAIQBw";
		case 2:
			return "CgkIlpSuuo0ZEAIQCA";
		case 3:
			return "CgkIlpSuuo0ZEAIQCQ";
		case 4:
			return "CgkIlpSuuo0ZEAIQCg";
		case 5:
			return "CgkIlpSuuo0ZEAIQCw";
		case 6:
			return "CgkIlpSuuo0ZEAIQDQ";
		case 7:
			return "CgkIlpSuuo0ZEAIQDg";
		case 8:
			return "CgkIlpSuuo0ZEAIQDw";
		case 9:
			return "CgkIlpSuuo0ZEAIQEA";
		case 10:
			return "CgkIlpSuuo0ZEAIQEQ";
		case 11:
			return "CgkIlpSuuo0ZEAIQEg";
		case 12:
			return "CgkIlpSuuo0ZEAIQEw";
		case 13:
			return "CgkIlpSuuo0ZEAIQFA";
		case 14:
			return "CgkIlpSuuo0ZEAIQFQ";
		case 15:
			return "CgkIlpSuuo0ZEAIQFg";
		case 16:
			return "CgkIlpSuuo0ZEAIQFw";
		case 17:
			return "CgkIlpSuuo0ZEAIQGA";
		case 18:
			return "CgkIlpSuuo0ZEAIQGQ";
		case 19:
			return "CgkIlpSuuo0ZEAIQGw";
		case 20:
			return "CgkIlpSuuo0ZEAIQGg";
		case 21:
			return "CgkIlpSuuo0ZEAIQHA";
		case 22:
			return "CgkIlpSuuo0ZEAIQHQ";
		case 23:
			return "CgkIlpSuuo0ZEAIQHg";
		case 24:
			return "CgkIlpSuuo0ZEAIQIA";
		case 25:
			return "CgkIlpSuuo0ZEAIQHw";
		case 26:
			return "CgkIlpSuuo0ZEAIQIQ";
		case 27:
			return "CgkIlpSuuo0ZEAIQIg";
		case 28:
			return "CgkIlpSuuo0ZEAIQIw";
		case 29:
			return "CgkIlpSuuo0ZEAIQJQ";
		case 30:
			return "CgkIlpSuuo0ZEAIQJA";
		case 31:
			return "CgkIlpSuuo0ZEAIQJg";
		case 32:
			return "CgkIlpSuuo0ZEAIQJw";
		case 33:
			return "CgkIlpSuuo0ZEAIQKA";
		case 34:
			return "CgkIlpSuuo0ZEAIQKQ";
		case 35:
			return "CgkIlpSuuo0ZEAIQKg";
		case 36:
			return "CgkIlpSuuo0ZEAIQLA";
		case 37:
			return "CgkIlpSuuo0ZEAIQKw";
		case 38:
			return "CgkIlpSuuo0ZEAIQMw";
		case 39:
			return "CgkIlpSuuo0ZEAIQNA";
		case 40:
			return "CgkIlpSuuo0ZEAIQNQ";
		case 41:
			return "CgkIlpSuuo0ZEAIQNg";
		case 42:
			return "CgkIlpSuuo0ZEAIQNw";
		case 44:
			return "CgkIlpSuuo0ZEAIQLQ";
		case 45:
			return "CgkIlpSuuo0ZEAIQLg";
		case 46:
			return "CgkIlpSuuo0ZEAIQLw";
		case 47:
			return "CgkIlpSuuo0ZEAIQMA";
		case 48:
			return "CgkIlpSuuo0ZEAIQMQ";
		case 50:
			return "CgkIlpSuuo0ZEAIQPQ";
		case 51:
			return "CgkIlpSuuo0ZEAIQPg";
		case 52:
			return "CgkIlpSuuo0ZEAIQPw";
		case 53:
			return "CgkIlpSuuo0ZEAIQQA";
		case 54:
			return "CgkIlpSuuo0ZEAIQQQ";
		}
		throw new NotImplementedException();
	}

	public static Language ConvertLanguage(int index)
	{
		switch (index)
		{
		case 1:
			return Language.EN;
		case 2:
			return Language.TR;
		case 3:
			return Language.ES;
		case 4:
			return Language.IT;
		case 5:
			return Language.FR;
		case 6:
			return Language.DE;
		case 7:
			return Language.PT;
		case 8:
			return Language.RU;
		case 9:
			return Language.ZH;
		case 10:
			return Language.JP;
		case 11:
			return Language.KR;
		default:
			return Language.EN;
		}
	}

	public static int ConvertLanguage(Language index)
	{
		switch (index)
		{
		case Language.EN:
			return 1;
		case Language.TR:
			return 2;
		case Language.ES:
			return 3;
		case Language.IT:
			return 4;
		case Language.FR:
			return 5;
		case Language.DE:
			return 6;
		case Language.PT:
			return 7;
		case Language.RU:
			return 8;
		case Language.ZH:
			return 9;
		case Language.JP:
			return 10;
		case Language.KR:
			return 11;
		default:
			throw new NotImplementedException();
		}
	}

	public static int ConvertCurrencyType(CurrencyType c)
	{
		switch (c)
		{
		case CurrencyType.GOLD:
			return 2;
		case CurrencyType.SCRAP:
			return 4;
		case CurrencyType.MYTHSTONE:
			return 3;
		case CurrencyType.GEM:
			return 1;
		case CurrencyType.TOKEN:
			return 5;
		case CurrencyType.AEON:
			return 7;
		case CurrencyType.CANDY:
			return 6;
		case CurrencyType.TRINKET_BOX:
			return 8;
		default:
			throw new NotImplementedException();
		}
	}

	public static CurrencyType ConvertCurrencyType(int c)
	{
		switch (c)
		{
		case 1:
			return CurrencyType.GEM;
		case 2:
			return CurrencyType.GOLD;
		case 3:
			return CurrencyType.MYTHSTONE;
		case 4:
			return CurrencyType.SCRAP;
		case 5:
			return CurrencyType.TOKEN;
		case 6:
			return CurrencyType.CANDY;
		case 7:
			return CurrencyType.AEON;
		case 8:
			return CurrencyType.TRINKET_BOX;
		default:
			throw new NotImplementedException();
		}
	}

	public static string EncodeString(string s)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(s);
		return Convert.ToBase64String(bytes);
	}

	public static string DecodeString(string s)
	{
		byte[] bytes = Convert.FromBase64String(s);
		return Encoding.UTF8.GetString(bytes);
	}

	public static int GetTotalArtifactsLevel(List<ArtifactSerializable> artifacts, List<Simulation.ArtifactSystem.Artifact> newArtifacts)
	{
		int num = 0;
		foreach (ArtifactSerializable artifactSerializable in artifacts)
		{
			Simulation.Artifact artifact = SaveLoadManager.ConvertArtifact(artifactSerializable);
			if (artifact.IsLegendaryPlus())
			{
				num += artifact.GetLegendaryPlusRank();
			}
		}
		foreach (Simulation.ArtifactSystem.Artifact artifact2 in newArtifacts)
		{
			num += artifact2.Level;
		}
		return num;
	}

	public static UnityEngine.Random.State ConvertRandomState(string s)
	{
		UnityEngine.Debug.Log(s);
		return JsonUtility.FromJson<UnityEngine.Random.State>(s.Replace('*', '"'));
	}

	public static string ConvertRandomState(UnityEngine.Random.State s)
	{
		return JsonUtility.ToJson(s).Replace('"', '*');
	}

	public static Trinket ConvertTrinket(TrinketSerializable t)
	{
		List<TrinketEffect> list = new List<TrinketEffect>();
		int i = 0;
		int count = t.effects.Count;
		while (i < count)
		{
			int index = t.effects[i];
			list.Add(TypeHelper.ConvertTrinketEffect(index));
			list[i].level = t.levels[i];
			i++;
		}
		TrinketUpgradeReq trinketUpgradeReq = SaveLoadManager.ConvertTrinketUpgradeReq(t.req);
		if (trinketUpgradeReq != null)
		{
			Trinket trinket = new Trinket(list, trinketUpgradeReq);
			trinket.bodyColorIndex = t.bodyColorIndex;
			trinket.bodySpriteIndex = t.bodySpriteIndex;
			trinket.OnLevelChanged();
			return trinket;
		}
		Trinket trinket2 = new Trinket(list);
		trinket2.bodyColorIndex = t.bodyColorIndex;
		trinket2.bodySpriteIndex = t.bodySpriteIndex;
		trinket2.OnLevelChanged();
		return trinket2;
	}

	public static TrinketUpgradeReq ConvertTrinketUpgradeReq(int id)
	{
		switch (id)
		{
		case 1:
			return new TrinketUpgradeReqKill();
		case 2:
			return new TrinketUpgradeReqAttack();
		case 3:
			return new TrinketUpgradeReqCastSpell();
		case 4:
			return new TrinketUpgradeReqTakeDamage();
		case 5:
			return new TrinketUpgradeReqUpgrade();
		case 6:
			return new TrinketUpgradeReqDie();
		case 7:
			return new TrinketUpgradeReqEpicBoss();
		case 8:
			return new TrinketUpgradeReqCritAttack();
		case 9:
			return new TrinketUpgradeReqStage();
		default:
			return null;
		}
	}

	public static int ConverTrinketUpgradeReq(TrinketUpgradeReq req)
	{
		if (req is TrinketUpgradeReqKill)
		{
			return 1;
		}
		if (req is TrinketUpgradeReqAttack)
		{
			return 2;
		}
		if (req is TrinketUpgradeReqCastSpell)
		{
			return 3;
		}
		if (req is TrinketUpgradeReqTakeDamage)
		{
			return 4;
		}
		if (req is TrinketUpgradeReqUpgrade)
		{
			return 5;
		}
		if (req is TrinketUpgradeReqDie)
		{
			return 6;
		}
		if (req is TrinketUpgradeReqEpicBoss)
		{
			return 7;
		}
		if (req is TrinketUpgradeReqCritAttack)
		{
			return 8;
		}
		if (req is TrinketUpgradeReqStage)
		{
			return 9;
		}
		if (req == null)
		{
			return -1;
		}
		throw new NotImplementedException();
	}

	public static TrinketSerializable ConvertTrinket(Trinket t)
	{
		TrinketSerializable trinketSerializable = new TrinketSerializable();
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		foreach (TrinketEffect trinketEffect in t.effects)
		{
			list.Add(TypeHelper.ConvertTrinketEffect(trinketEffect));
			list2.Add(trinketEffect.level);
		}
		trinketSerializable.effects = list;
		trinketSerializable.levels = list2;
		trinketSerializable.bodySpriteIndex = t.bodySpriteIndex;
		trinketSerializable.bodyColorIndex = t.bodyColorIndex;
		trinketSerializable.req = SaveLoadManager.ConverTrinketUpgradeReq(t.req);
		return trinketSerializable;
	}

	public static ShopPack GetShopPack(SaveData saveData)
	{
		ShopPack shopPack = SaveLoadManager.ConvertShopPack(saveData.shopPack);
		if (shopPack != null)
		{
			shopPack.timeStart = new DateTime(saveData.shopPackTime);
		}
		return shopPack;
	}

	public static void SetShopPack(SaveData saveData)
	{
		saveData.spsa = ShopPackStarter.appeared;
		saveData.spsab = ShopPackStarter.appearedBefore;
		saveData.spsp = ShopPackStarter.purchased;
		saveData.spxp = ShopPackXmas.purchased;
		saveData.sptpa = ShopPackThreePijama.appeared;
		saveData.spbga = ShopPackBigGem.appeared;
		saveData.spbgp = ShopPackBigGem.purchased;
		saveData.spbgta = ShopPackBigGemTwo.appeared;
		saveData.spbgtp = ShopPackBigGemTwo.purchased;
		saveData.spro01a = ShopPackRiftOffer01.appeared;
		saveData.spro01p = ShopPackRiftOffer01.purchased;
		saveData.spro02a = ShopPackRiftOffer02.appeared;
		saveData.spro02p = ShopPackRiftOffer02.purchased;
		saveData.spro03a = ShopPackRiftOffer03.appeared;
		saveData.spro03p = ShopPackRiftOffer03.purchased;
		saveData.spro04a = ShopPackRiftOffer04.appeared;
		saveData.spro04p = ShopPackRiftOffer04.purchased;
		saveData.spsha = ShopPackStage100.appeared;
		saveData.spshp = ShopPackStage100.purchased;
		saveData.regionalOffer01Purchased = ShopPackRegional01.purchased;
		saveData.regionalOffer01Appeared = ShopPackRegional01.appeared;
		saveData.halloweenOfferGemsPurchased = ShopPackHalloweenGems.purchased;
		saveData.halloweenOfferGemsAppeared = ShopPackHalloweenGems.appeared;
		saveData.christmasOfferGemsSmallPurchased = ShopPackChristmasGemsSmall.purchased;
		saveData.christmasOfferGemsSmallAppeared = ShopPackChristmasGemsSmall.appeared;
		saveData.christmasOfferGemsBigPurchased = ShopPackChristmasGemsBig.purchased;
		saveData.christmasOfferGemsBigAppeared = ShopPackChristmasGemsBig.appeared;
		saveData.stage200OfferAppeared = ShopPackStage300.appeared;
		saveData.stage200OfferPurchased = ShopPackStage300.purchased;
		saveData.stage500OfferAppeared = ShopPackStage800.appeared;
		saveData.stage500OfferPurchased = ShopPackStage800.purchased;
		saveData.secondAnniversaryGemsOfferAppeared = ShopPackSecondAnniversaryGems.appeared;
		saveData.secondAnniversaryGemsOfferPurchased = ShopPackSecondAnniversaryGems.purchased;
		saveData.secondAnniversaryBundleOfferAppeared = ShopPackSecondAnniversaryCurrencyBundle.appeared;
		saveData.secondAnniversaryBundleOfferPurchased = ShopPackSecondAnniversaryCurrencyBundle.purchased;
		saveData.secondAnniversaryGemsTwoOfferAppeared = ShopPackSecondAnniversaryGemsTwo.appeared;
		saveData.secondAnniversaryGemsTwoOfferPurchased = ShopPackSecondAnniversaryGemsTwo.purchased;
		saveData.secondAnniversaryBundleTwoOfferAppeared = ShopPackSecondAnniversaryCurrencyBundleTwo.appeared;
		saveData.secondAnniversaryBundleTwoOfferPurchased = ShopPackSecondAnniversaryCurrencyBundleTwo.purchased;
	}

	public static int ConvertShopPack(ShopPack shopPack)
	{
		if (shopPack == null)
		{
			return 0;
		}
		if (shopPack is ShopPackStarter)
		{
			return 1;
		}
		if (shopPack is ShopPackRune)
		{
			return 2;
		}
		if (shopPack is ShopPackToken)
		{
			return 3;
		}
		if (shopPack is ShopPackCurrency)
		{
			return 4;
		}
		if (shopPack is ShopPackXmas)
		{
			return 5;
		}
		if (shopPack is ShopPackFiveTrinkets)
		{
			return 6;
		}
		if (shopPack is ShopPackThreePijama)
		{
			return 7;
		}
		if (shopPack is ShopPackBigGem)
		{
			return 8;
		}
		if (shopPack is ShopPackBigGemTwo)
		{
			return 9;
		}
		if (shopPack is ShopPackRiftOffer01)
		{
			return 10;
		}
		if (shopPack is ShopPackRiftOffer02)
		{
			return 11;
		}
		if (shopPack is ShopPackRiftOffer03)
		{
			return 12;
		}
		if (shopPack is ShopPackRegional01)
		{
			return 13;
		}
		if (shopPack is ShopPackStage100)
		{
			return 14;
		}
		if (shopPack is ShopPackRiftOffer04)
		{
			return 15;
		}
		if (shopPack is ShopPackStage300)
		{
			return 16;
		}
		if (shopPack is ShopPackStage800)
		{
			return 17;
		}
		throw new NotImplementedException();
	}

	public static ShopPack ConvertShopPack(int shopPack)
	{
		if (shopPack == 0)
		{
			return null;
		}
		if (shopPack == 1)
		{
			return new ShopPackStarter();
		}
		if (shopPack == 2)
		{
			return new ShopPackRune();
		}
		if (shopPack == 3)
		{
			return new ShopPackToken();
		}
		if (shopPack == 4)
		{
			return new ShopPackCurrency();
		}
		if (shopPack == 5)
		{
			return new ShopPackXmas();
		}
		if (shopPack == 6)
		{
			return new ShopPackFiveTrinkets();
		}
		if (shopPack == 7)
		{
			return new ShopPackThreePijama();
		}
		if (shopPack == 8)
		{
			return new ShopPackBigGem();
		}
		if (shopPack == 9)
		{
			return new ShopPackBigGemTwo();
		}
		if (shopPack == 10)
		{
			return new ShopPackRiftOffer01();
		}
		if (shopPack == 11)
		{
			return new ShopPackRiftOffer02();
		}
		if (shopPack == 12)
		{
			return new ShopPackRiftOffer03();
		}
		if (shopPack == 13)
		{
			return new ShopPackRegional01();
		}
		if (shopPack == 14)
		{
			return new ShopPackStage100();
		}
		if (shopPack == 15)
		{
			return new ShopPackRiftOffer04();
		}
		if (shopPack == 16)
		{
			return new ShopPackStage300();
		}
		if (shopPack == 17)
		{
			return new ShopPackStage800();
		}
		throw new NotImplementedException();
	}

	public static MineSerializable ConvertMine(Mine mine)
	{
		return new MineSerializable
		{
			unlocked = mine.unlocked,
			level = mine.level,
			time = mine.timeCollected.Ticks
		};
	}

	public static void SetMine(Mine mine, MineSerializable saveDataMine)
	{
		if (saveDataMine == null)
		{
			return;
		}
		mine.unlocked = saveDataMine.unlocked;
		mine.level = saveDataMine.level;
		mine.timeCollected = new DateTime(saveDataMine.time);
	}

	public static DailyQuest ConvertDailyQuest(int dailyQuest)
	{
		switch (dailyQuest)
		{
		case 1:
			return new DailyQuestKillEnemiesEasy();
		case 2:
			return new DailyQuestPassStagesEasy();
		case 3:
			return new DailyQuestUltiSkillEasy();
		case 4:
			return new DailyQuestKillEnemiesHard();
		case 5:
			return new DailyQuestPassStagesHard();
		case 6:
			return new DailyQuestUltiSkillHard();
		case 7:
			return new DailyQuestCatchDragonEasy();
		case 8:
			return new DailyQuestCatchDragonHard();
		case 9:
			return new DailyQuestHiltDodgeAttacks();
		case 10:
			return new DailyQuestBellylarfStayAngry();
		case 11:
			return new DailyQuestVexxCoolWeapon();
		case 12:
			return new DailyQuestLennyKillStunned();
		case 13:
			return new DailyQuestWendleCastSpell();
		case 14:
			return new DailyQuestVStealFromBoss();
		case 15:
			return new DailyQuestBoomerBoom();
		case 16:
			return new DailyQuestSamShield();
		case 17:
			return new DailyQuestLiaMiss();
		case 18:
			return new DailyQuestJimAllyDeath();
		case 19:
			return new DailyQuestTamKillBlinded();
		case 20:
			return new DailyQuestWendleHeal();
		case 21:
			return new DailyQuestVTreasureChestKill();
		case 22:
			return new DailyQuestLennyHealAlly();
		case 23:
			return new DailyQuestHiltCriticalHit();
		case 24:
			return new DailyQuestTamHitMarkedTargets();
		case 25:
			return new DailyQuestStunEnemies();
		case 26:
			return new DailyQuestLiaStealHealth();
		case 27:
			return new DailyQuestCollectCandy();
		case 28:
			return new DailyQuestWarlockEnemyBlind();
		case 29:
			return new DailyQuestWarlockRedirectDamage();
		case 30:
			return new DailyQuestGoblinMiss();
		case 31:
			return new DailyQuestGoblinKillTreasure();
		case 32:
			return new DailyQuestGoblinSummonDragons();
		case 33:
			return new DailyQuestHealAlliesBabu();
		case 34:
			return new DailyQuestBodyBlockWithBabu();
		case 35:
			return new DailyQuestUseRonLandCritHit();
		case 36:
			return new DailyQuestTriggerRonImpulsiveSkill();
		default:
			return null;
		}
	}

	public static int ConvertDailyQuest(DailyQuest dailyQuest)
	{
		if (dailyQuest == null)
		{
			return 0;
		}
		if (dailyQuest is DailyQuestKillEnemiesEasy)
		{
			return 1;
		}
		if (dailyQuest is DailyQuestPassStagesEasy)
		{
			return 2;
		}
		if (dailyQuest is DailyQuestUltiSkillEasy)
		{
			return 3;
		}
		if (dailyQuest is DailyQuestKillEnemiesHard)
		{
			return 4;
		}
		if (dailyQuest is DailyQuestPassStagesHard)
		{
			return 5;
		}
		if (dailyQuest is DailyQuestUltiSkillHard)
		{
			return 6;
		}
		if (dailyQuest is DailyQuestCatchDragonEasy)
		{
			return 7;
		}
		if (dailyQuest is DailyQuestCatchDragonHard)
		{
			return 8;
		}
		if (dailyQuest is DailyQuestHiltDodgeAttacks)
		{
			return 9;
		}
		if (dailyQuest is DailyQuestBellylarfStayAngry)
		{
			return 10;
		}
		if (dailyQuest is DailyQuestVexxCoolWeapon)
		{
			return 11;
		}
		if (dailyQuest is DailyQuestLennyKillStunned)
		{
			return 12;
		}
		if (dailyQuest is DailyQuestWendleCastSpell)
		{
			return 13;
		}
		if (dailyQuest is DailyQuestVStealFromBoss)
		{
			return 14;
		}
		if (dailyQuest is DailyQuestBoomerBoom)
		{
			return 15;
		}
		if (dailyQuest is DailyQuestSamShield)
		{
			return 16;
		}
		if (dailyQuest is DailyQuestLiaMiss)
		{
			return 17;
		}
		if (dailyQuest is DailyQuestJimAllyDeath)
		{
			return 18;
		}
		if (dailyQuest is DailyQuestTamKillBlinded)
		{
			return 19;
		}
		if (dailyQuest is DailyQuestWendleHeal)
		{
			return 20;
		}
		if (dailyQuest is DailyQuestVTreasureChestKill)
		{
			return 21;
		}
		if (dailyQuest is DailyQuestLennyHealAlly)
		{
			return 22;
		}
		if (dailyQuest is DailyQuestHiltCriticalHit)
		{
			return 23;
		}
		if (dailyQuest is DailyQuestTamHitMarkedTargets)
		{
			return 24;
		}
		if (dailyQuest is DailyQuestStunEnemies)
		{
			return 25;
		}
		if (dailyQuest is DailyQuestLiaStealHealth)
		{
			return 26;
		}
		if (dailyQuest is DailyQuestCollectCandy)
		{
			return 27;
		}
		if (dailyQuest is DailyQuestWarlockEnemyBlind)
		{
			return 28;
		}
		if (dailyQuest is DailyQuestWarlockRedirectDamage)
		{
			return 29;
		}
		if (dailyQuest is DailyQuestGoblinMiss)
		{
			return 30;
		}
		if (dailyQuest is DailyQuestGoblinKillTreasure)
		{
			return 31;
		}
		if (dailyQuest is DailyQuestGoblinSummonDragons)
		{
			return 32;
		}
		if (dailyQuest is DailyQuestHealAlliesBabu)
		{
			return 33;
		}
		if (dailyQuest is DailyQuestBodyBlockWithBabu)
		{
			return 34;
		}
		if (dailyQuest is DailyQuestUseRonLandCritHit)
		{
			return 35;
		}
		if (dailyQuest is DailyQuestTriggerRonImpulsiveSkill)
		{
			return 36;
		}
		return 0;
	}

	public static CharmLevelStatusSerializable ConvertCharmEffect(CharmEffectData effect)
	{
		return new CharmLevelStatusSerializable
		{
			lvl = effect.level,
			usdupe = effect.unspendDuplicates
		};
	}

	public static QuestOfUpdate ConvertQuestOfUpdate(QuestOfUpdateSerializable qou)
	{
		if (qou == null || qou.type == 0)
		{
			return null;
		}
		if (qou.type == 1)
		{
			return new QuestOfUpdateAnniversary01
			{
				progress = qou.progress,
				startTime = new DateTime(qou.startTime),
				isExpired = qou.isExpired
			};
		}
		return null;
	}

	public static int ConvertQuestOfUpdate(QuestOfUpdate qou)
	{
		if (qou == null)
		{
			return 0;
		}
		if (qou is QuestOfUpdateAnniversary01)
		{
			return 1;
		}
		return 0;
	}

	public static ServerSideFlashOfferBundleSerializable ConvertServerSideFlashOfferBundle(ServerSideFlashOfferBundle flashOfferBundle)
	{
		if (flashOfferBundle == null)
		{
			return null;
		}
		return new ServerSideFlashOfferBundleSerializable
		{
			offers = flashOfferBundle.offers,
			hasSeen = flashOfferBundle.hasSeen
		};
	}

	public static ServerSideFlashOfferBundle ConvertServerSideFlashOfferBundle(ServerSideFlashOfferBundleSerializable flashOfferBundle)
	{
		if (flashOfferBundle == null)
		{
			return null;
		}
		return new ServerSideFlashOfferBundle
		{
			offers = flashOfferBundle.offers,
			hasSeen = flashOfferBundle.hasSeen
		};
	}

	public static FlashOfferBundleSerializable ConvertFlashOfferBundle(FlashOfferBundle flashOfferBundle)
	{
		if (flashOfferBundle == null)
		{
			return null;
		}
		return new FlashOfferBundleSerializable
		{
			timeCreated = flashOfferBundle.timeCreated.Ticks,
			offers = flashOfferBundle.offers,
			adventureOffers = flashOfferBundle.adventureOffers,
			hasSeen = flashOfferBundle.hasSeen
		};
	}

	public static FlashOfferBundle ConvertFlashOfferBundle(FlashOfferBundleSerializable flashOfferBundleSerializable, int maxStage)
	{
		if (flashOfferBundleSerializable.offers != null && flashOfferBundleSerializable.offers[0].charmId == flashOfferBundleSerializable.offers[1].charmId)
		{
			flashOfferBundleSerializable.offers = null;
		}
		FlashOfferBundle flashOfferBundle = new FlashOfferBundle
		{
			timeCreated = new DateTime(flashOfferBundleSerializable.timeCreated),
			offers = flashOfferBundleSerializable.offers,
			adventureOffers = flashOfferBundleSerializable.adventureOffers,
			hasSeen = flashOfferBundleSerializable.hasSeen
		};
		if (flashOfferBundle.adventureOffers == null)
		{
			flashOfferBundle.adventureOffers = new List<FlashOffer>();
			for (int i = 0; i < 3; i++)
			{
				flashOfferBundle.adventureOffers.Add(FlashOfferFactory.CreateRandomAdventureOffer(flashOfferBundle.adventureOffers, true, maxStage));
			}
		}
		if (flashOfferBundle.offers != null)
		{
			foreach (FlashOffer flashOffer in flashOfferBundle.offers)
			{
				if (!flashOffer.isBought && flashOffer.purchasesLeft == 0)
				{
					flashOffer.purchasesLeft = 1;
				}
			}
		}
		return flashOfferBundle;
	}

	public static int ConvertCharmSortType(CharmSortType charmSortType)
	{
		switch (charmSortType)
		{
		case CharmSortType.Default:
			return 0;
		case CharmSortType.Level:
			return 1;
		case CharmSortType.Type:
			return 2;
		case CharmSortType.LevelupStatus:
			return 3;
		default:
			throw new Exception(charmSortType.ToString());
		}
	}

	public static CharmSortType ConvertCharmSortType(int charmSortType)
	{
		switch (charmSortType)
		{
		case 0:
			return CharmSortType.Default;
		case 1:
			return CharmSortType.Level;
		case 2:
			return CharmSortType.Type;
		case 3:
			return CharmSortType.LevelupStatus;
		default:
			throw new Exception(charmSortType.ToString());
		}
	}

	public static int ConvertTrinketSortType(TrinketSortType trinketSortType)
	{
		switch (trinketSortType)
		{
		case TrinketSortType.Default:
			return 0;
		case TrinketSortType.NumberOfEffects:
			return 1;
		case TrinketSortType.Level:
			return 2;
		case TrinketSortType.Color:
			return 3;
		default:
			throw new Exception(trinketSortType.ToString());
		}
	}

	public static TrinketSortType ConvertTrinketSortType(int trinketSortType)
	{
		switch (trinketSortType)
		{
		case 0:
			return TrinketSortType.Default;
		case 1:
			return TrinketSortType.NumberOfEffects;
		case 2:
			return TrinketSortType.Level;
		case 3:
			return TrinketSortType.Color;
		default:
			throw new Exception(trinketSortType.ToString());
		}
	}

	public static SpecialOfferKeeper ConvertSpecialOfferKeeper(SpecialOfferKeeperSeralizable offerKeeperSeralizable)
	{
		if (offerKeeperSeralizable == null)
		{
			return SpecialOfferKeeper.Empty();
		}
		ShopPack pack = SaveLoadManager.ConvertShopPack(offerKeeperSeralizable.offerPack);
		DateTime endTime = new DateTime(offerKeeperSeralizable.offerEndTime);
		DateTime nextOfferTime = new DateTime(offerKeeperSeralizable.nextOfferApperTime);
		return SpecialOfferKeeper.CreateLoad(pack, endTime, nextOfferTime);
	}

	public static SpecialOfferKeeperSeralizable ConvertSpecialOfferKeeper(SpecialOfferKeeper offerKeeper)
	{
		return new SpecialOfferKeeperSeralizable
		{
			offerPack = SaveLoadManager.ConvertShopPack(offerKeeper.offerPack),
			offerEndTime = offerKeeper.offerEndTime.Ticks,
			nextOfferApperTime = offerKeeper.nextOfferApperTime.Ticks
		};
	}

	public static void LoadSpecialOfferBoard(SpecialOfferBoard specialOfferBoard, SaveData saveData)
	{
		if (saveData.specialOfferBoard != null)
		{
			specialOfferBoard.previousStandardOffer = saveData.specialOfferBoard.previousStandardOffer;
			specialOfferBoard.previousReAppearOffer = saveData.specialOfferBoard.previousReAppearOffer;
			specialOfferBoard.standardOffer = SaveLoadManager.ConvertSpecialOfferKeeper(saveData.specialOfferBoard.standardOffer);
			specialOfferBoard.reAppearingUniqueOffer = SaveLoadManager.ConvertSpecialOfferKeeper(saveData.specialOfferBoard.reAppearingUniqueOffer);
			specialOfferBoard.hasAllSeen = saveData.specialOfferBoard.hasAllSeen;
			specialOfferBoard.hasAllSeenOutOfShop = saveData.specialOfferBoard.hasAllSeenOutOfShop;
			if (saveData.specialOfferBoard.uniqueOffers != null)
			{
				foreach (SpecialOfferKeeperSeralizable offerKeeperSeralizable in saveData.specialOfferBoard.uniqueOffers)
				{
					specialOfferBoard.uniqueOffers.Add(SaveLoadManager.ConvertSpecialOfferKeeper(offerKeeperSeralizable));
				}
			}
		}
	}

	public static void SaveSpecialOfferBoard(SpecialOfferBoard specialOfferBoard, SaveData saveData)
	{
		saveData.specialOfferBoard = new SpecialOfferBoardSerializable
		{
			previousStandardOffer = specialOfferBoard.previousStandardOffer,
			standardOffer = SaveLoadManager.ConvertSpecialOfferKeeper(specialOfferBoard.standardOffer),
			previousReAppearOffer = specialOfferBoard.previousReAppearOffer,
			reAppearingUniqueOffer = SaveLoadManager.ConvertSpecialOfferKeeper(specialOfferBoard.reAppearingUniqueOffer),
			uniqueOffers = new List<SpecialOfferKeeperSeralizable>(specialOfferBoard.uniqueOffers.Count),
			hasAllSeen = specialOfferBoard.hasAllSeen,
			hasAllSeenOutOfShop = specialOfferBoard.hasAllSeenOutOfShop
		};
		foreach (SpecialOfferKeeper offerKeeper in specialOfferBoard.uniqueOffers)
		{
			saveData.specialOfferBoard.uniqueOffers.Add(SaveLoadManager.ConvertSpecialOfferKeeper(offerKeeper));
		}
	}

	public static SaveData MigrateSaveData(SaveData saveData)
	{
		Version v = new Version(saveData.gameVersion);
		Version v2 = new Version(Cheats.version);
		if (v < v2)
		{
			saveData = SaveDataMigrationUtility.Migrate(saveData);
		}
		return saveData;
	}

	public const string SAVE_DATA_STRING = "save_data";

	private const string SAVE_DATA_MIGRATION_BACKUP_KEY = "migration_backup_save_data";

	private const string MIGRATION_BACKUP_INDEX_KEY = "migration_backup_index";

	private const string MIGRATION_BACKUP_FLAG_KEY = "migration_backup_flag";

	private static int currentBackupIndex;

	public const string MaxStageReachedKey = "MaxStageReached";

	public static bool cloudSaveMustBeBackedUp;

	public static float timeSinceLastSave;

	internal static bool loadingSaveFailed;
}
