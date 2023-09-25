using System;
using System.Collections.Generic;
using System.Linq;
using SaveLoad;
using Simulation.ArtifactSystem;
using SocialRewards;
using Static;
using stats;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class Simulator
	{
		public void InitZero()
		{
			TutorialManager.Reset();
			this.InitCommon();
			this.unlockedHeroIds.Add(this.allHeroes[0].GetId());
			this.unlockedHeroIds.Add(this.allHeroes[1].GetId());
			this.unlockedTotemIds.Add(this.allTotems[0].id);
			this.isMerchantUnlocked = false;
			this.hasCompass = false;
			this.hasDailies = false;
			this.hasSkillPointsAutoDistribution = false;
			this.SwitchGameMode(GameMode.STANDARD);
			this.UpdateUniversalTotalBonus();
			this.lootpackFreeLastOpenTime = GameMath.GetNow();
			this.lootpackFreeLastOpenTime = this.lootpackFreeLastOpenTime.AddSeconds((double)(-(double)this.GetFreeLootpackPeriod()));
			this.lootpackFreeLastOpenTimeServer = GameMath.GetNow();
			this.lootpackFreeLastOpenTimeServer = this.lootpackFreeLastOpenTimeServer.AddSeconds(-172800.0);
			this.lastCappedWatchedTime = GameMath.GetNow();
			this.lastCappedCandiesWatchedTime = GameMath.GetNow();
			this.lastCappedWatchedTime = this.lastCappedWatchedTime.AddSeconds(-172800.0);
			this.lastCappedCandiesWatchedTime = this.lastCappedCandiesWatchedTime.AddSeconds(-172800.0);
			this.lastOfferEndTime = GameMath.GetNow();
			this.lastOfferEndTime = this.lastOfferEndTime.AddDays(-7.0);
			this.lastRiftOfferEndTime = GameMath.GetNow();
			this.lastRiftOfferEndTime = this.lastRiftOfferEndTime.AddDays(-30.0);
			this.dailyQuestCollectedTime = GameMath.GetNow();
			this.dailyQuestCollectedTime = this.dailyQuestCollectedTime.AddDays(-7.0);
			this.allTrinkets = new List<Trinket>();
			this.disassembledTinketEffects = new Dictionary<int, int>();
			ShopPackStarter.appeared = false;
			ShopPackStarter.appearedBefore = false;
			ShopPackStarter.purchased = false;
			ShopPackXmas.purchased = false;
			ShopPackThreePijama.appeared = false;
			ShopPackBigGem.appeared = false;
			ShopPackBigGemTwo.appeared = false;
			ShopPackRiftOffer01.appeared = false;
			ShopPackRiftOffer01.purchased = false;
			ShopPackRiftOffer02.appeared = false;
			ShopPackRiftOffer02.purchased = false;
			ShopPackRiftOffer03.appeared = false;
			ShopPackRiftOffer03.purchased = false;
			ShopPackRegional01.purchased = false;
			ShopPackStage100.appeared = false;
			ShopPackStage100.purchased = false;
			ShopPackHalloweenGems.appeared = false;
			ShopPackHalloweenGems.purchased = false;
			ShopPackChristmasGemsSmall.appeared = false;
			ShopPackChristmasGemsSmall.purchased = false;
			ShopPackChristmasGemsBig.appeared = false;
			ShopPackChristmasGemsBig.purchased = false;
			ShopPackStage300.appeared = false;
			ShopPackStage300.purchased = false;
			ShopPackStage800.appeared = false;
			ShopPackStage800.purchased = false;
			ShopPackSecondAnniversaryCurrencyBundle.appeared = false;
			ShopPackSecondAnniversaryCurrencyBundle.purchased = false;
			ShopPackSecondAnniversaryGems.appeared = false;
			ShopPackSecondAnniversaryGems.purchased = false;
			ShopPackSecondAnniversaryCurrencyBundleTwo.appeared = false;
			ShopPackSecondAnniversaryCurrencyBundleTwo.purchased = false;
			ShopPackSecondAnniversaryGemsTwo.appeared = false;
			ShopPackSecondAnniversaryGemsTwo.purchased = false;
			this.numTrinketSlots = 0;
			DateTime timeCollected = GameMath.GetNow().AddSeconds(-172800.0);
			this.mineToken.unlocked = false;
			this.mineToken.level = 0;
			this.mineToken.timeCollected = timeCollected;
			this.mineScrap.unlocked = false;
			this.mineScrap.level = 0;
			this.mineScrap.timeCollected = timeCollected;
			this.isMineEverCollected = false;
			this.dailySkipCount = 0;
			this.numCandyQuestCompleted = 0;
			this.numCandyQuestAnniversaryCompleted = 0;
			this.prestigeRunTimer = 0.0;
			this.lastPrestigeRunstats = null;
			this.secondaryCdUi = false;
			this.appNeverSleep = true;
			this.riftRestRewardCollectedTime = DateTime.MaxValue;
			this.numSmallCharmPacksOpened = 0;
			this.riftDiscoveryIndex = 0;
			this.numRiftQuestsCompleted = 0;
			this.cursedGatesBeaten = 0;
			this.christmasEventForbidden = true;
			this.artifactsManager = new ArtifactsManager();
			this.prestigedDuringSecondAnniversaryEvent = false;
			this.InitSecondAnniversaryOffersBundle();
		}

		public void InitLoad(SaveData saveData)
		{
			this.InitCommon();
			if (saveData.languageSelected != 0)
			{
				LM.SelectLanguage(SaveLoadManager.ConvertLanguage(saveData.languageSelected));
			}
			this.lastSaveTime = saveData.saveTime;
			this.currentGameMode = SaveLoadManager.ConvertGameMode(saveData.currentGameMode);
			this.SwitchGameMode(this.currentGameMode);
			this.numPrestiges = saveData.numPrestiges;
			this.isMerchantUnlocked = saveData.isMerchantUnlocked;
			this.hasCompass = saveData.hasCompass;
			this.hasDailies = saveData.hasDailies;
			this.hasSkillPointsAutoDistribution = saveData.hasSkillPointsAutoDistribution;
			this.maxStagePrestigedAt = saveData.maxStagePrestigedAt;
			this.lootpackFreeLastOpenTime = new DateTime(saveData.lootpackFreeLastOpenTime);
			this.lootpackFreeLastOpenTimeServer = new DateTime(saveData.lootpackFreeLastOpenTimeServer);
			this.lastCappedWatchedTime = new DateTime(saveData.lastCappedWatchedTime);
			this.lastCappedCandiesWatchedTime = new DateTime(saveData.lastCappedCandiesWatchedTime);
			this.lastOfferEndTime = new DateTime(saveData.lastOfferEndTime);
			this.lastRiftOfferEndTime = new DateTime(saveData.lastRiftOfferEndTime);
			this.mythstones.SetAmountForLoading(saveData.mythstones);
			this.scraps.SetAmountForLoading(saveData.scraps);
			this.credits.SetAmountForLoading(saveData.credits);
			this.tokens.SetAmountForLoading(saveData.tokens);
			this.aeons.SetAmountForLoading(saveData.aeon);
			this.candies.SetAmountForLoading(saveData.candies);
			this.lastCandyAmountCapDailyReset = saveData.lastCandyAmountCapDailyReset;
			this.candyAmountCollectedSinceLastDailyCapReset = saveData.candyAmountCollectedSinceLastDailyCapReset;
			if (saveData.charmStatus != null)
			{
				foreach (KeyValuePair<int, CharmLevelStatusSerializable> keyValuePair in saveData.charmStatus)
				{
					CharmEffectData charmEffectData = this.allCharmEffects[keyValuePair.Key];
					charmEffectData.level = keyValuePair.Value.lvl;
					charmEffectData.unspendDuplicates = keyValuePair.Value.usdupe;
				}
			}
			if (saveData.newLabelledCharms != null)
			{
				foreach (int key in saveData.newLabelledCharms)
				{
					this.allCharmEffects[key].isNew = true;
				}
			}
			if (saveData.newLabelledCurses != null)
			{
				foreach (KeyValuePair<int, CurseEffectData> keyValuePair2 in this.allCurseEffects)
				{
					keyValuePair2.Value.isNew = false;
				}
				foreach (int key2 in saveData.newLabelledCurses)
				{
					this.allCurseEffects[key2].isNew = true;
				}
			}
			this.worldRift.autoSkillDistribute = saveData.riftAutoSkillDistribute;
			this.worldStandard.autoSkillDistribute = saveData.adventureAutoSkillDistribute;
			this.numTrinketSlots = 0;
			foreach (uint id40 in saveData.collectedUnlockIds)
			{
				Unlock unlock = this.GetUnlock(id40);
				unlock.isCollected = true;
				if (unlock.HasRewardOfType(typeof(UnlockRewardMythicalArtifactSlot)))
				{
					this.artifactsManager.NumArtifactSlotsMythical += (unlock.GetReward() as UnlockRewardMythicalArtifactSlot).numSlotsToUnlock;
				}
				else if (unlock.HasRewardOfType(typeof(UnlockRewardTrinketSlots)))
				{
					this.numTrinketSlots += (unlock.GetReward() as UnlockRewardTrinketSlots).numSlotsToUnlock;
				}
				else if (unlock.GetId() == UnlockIds.RIFT_REWARD_036 && !this.worldRift.merchantItems[3].IsUnlocked())
				{
					this.worldRift.merchantItems[3].SetLevel(0);
				}
				else if (unlock.GetId() == UnlockIds.MINE_SCRAP)
				{
					saveData.mineScrap.unlocked = true;
				}
				else if (unlock.GetId() == UnlockIds.MINE_TOKEN)
				{
					saveData.mineToken.unlocked = true;
				}
				ChallengeWithTime timeChallengeIfExists = unlock.GetTimeChallengeIfExists();
				if (timeChallengeIfExists != null)
				{
					timeChallengeIfExists.state = Challenge.State.WON;
				}
			}
			this.worldRift.CacheLatestBeatenRiftChallengeIndex();
			Challenge challenge = null;
			for (int i = 0; i < this.worldCrusade.allChallenges.Count; i++)
			{
				Challenge challenge2 = this.worldCrusade.allChallenges[i];
				if (challenge2.state != Challenge.State.WON)
				{
					challenge = challenge2;
					break;
				}
			}
			if (challenge == null)
			{
				UnityEngine.Debug.Log("No active crusade challenge left.");
				this.worldCrusade.isCompleted = true;
			}
			else
			{
				this.worldCrusade.activeChallenge = challenge;
				challenge.Init(this.worldCrusade);
			}
			if (saveData.cursedIds != null && saveData.cursedLevs != null)
			{
				World world = this.GetWorld(GameMode.RIFT);
				for (int j = 0; j < saveData.cursedIds.Count; j++)
				{
					int id = saveData.cursedIds[j];
					int discoveryIndex = saveData.cursedLevs[j];
					int index = world.allChallenges.FindIndex((Challenge r) => (r as ChallengeRift).id == id);
					ChallengeRift item = this.GenerateCursedRift(index, discoveryIndex, world);
					world.cursedChallenges.Add(item);
				}
			}
			Challenge challenge3 = null;
			if (saveData.wasAtiveChallengeCursed)
			{
				foreach (Challenge challenge4 in this.worldRift.cursedChallenges)
				{
					ChallengeRift challengeRift = (ChallengeRift)challenge4;
					if (challengeRift.id == saveData.activeRiftChallengeId)
					{
						challenge3 = challengeRift;
						break;
					}
				}
			}
			else
			{
				foreach (Challenge challenge5 in this.worldRift.allChallenges)
				{
					ChallengeRift challengeRift2 = (ChallengeRift)challenge5;
					if (challengeRift2.id == saveData.activeRiftChallengeId)
					{
						challenge3 = challengeRift2;
						break;
					}
				}
			}
			if (challenge3 == null)
			{
				UnityEngine.Debug.Log("No active rift challenge left.");
				challenge3 = this.worldRift.allChallenges[0];
				challenge3.Init(this.worldRift);
			}
			else
			{
				this.worldRift.activeChallenge = challenge3;
				challenge3.Init(this.worldRift);
			}
            List<Simulation.Artifact> list = new List<Simulation.Artifact>();
			foreach (ArtifactSerializable artifactSerializable in saveData.artifacts)
			{
                Simulation.Artifact artifact = SaveLoadManager.ConvertArtifact(artifactSerializable);
				if (saveData.artifactForcedDisableds != null && saveData.artifactForcedDisableds.Contains(list.Count))
				{
					artifact.TryDisable();
				}
				list.Add(artifact);
			}
			this.artifactsManager.LoadState(saveData.artifactList, list, saveData.numArtifactSlots);
			this.UpdateUniversalTotalBonus();
			foreach (KeyValuePair<string, int> keyValuePair3 in saveData.boughtGearLevels)
			{
				string key3 = keyValuePair3.Key;
				int value = keyValuePair3.Value;
				GearData gearData = this.GetGearData(key3);
				Gear item2 = new Gear(gearData, value);
				this.boughtGears.Add(item2);
			}
			foreach (string id2 in saveData.boughtRunes)
			{
				Rune rune = this.GetRune(id2);
				this.boughtRunes.Add(rune);
			}
			if (saveData.boughtHeroSkins != null)
			{
				foreach (int id3 in saveData.boughtHeroSkins)
				{
					SkinData skinData = this.GetSkinData(id3);
					this.AddBoughtSkin(skinData);
				}
			}
			if (saveData.newHeroSkins != null)
			{
				foreach (int id4 in saveData.newHeroSkins)
				{
					SkinData skinData2 = this.GetSkinData(id4);
					skinData2.isNew = false;
				}
			}
			foreach (KeyValuePair<string, HashSet<string>> keyValuePair4 in saveData.wornRunes)
			{
				string key4 = keyValuePair4.Key;
				List<Rune> list2 = this.wornRunes[key4];
				foreach (string id5 in keyValuePair4.Value)
				{
					list2.Add(this.GetRune(id5));
				}
			}
			foreach (World world2 in this.allWorlds)
			{
				foreach (MerchantItem merchantItem in world2.merchantItems)
				{
					string id6 = merchantItem.GetId();
					if (saveData.merchantItemLevels.ContainsKey(id6))
					{
						merchantItem.SetLevel(saveData.merchantItemLevels[id6]);
					}
				}
				if (world2.eventMerchantItems != null && saveData.eventMerchantItemLevels != null)
				{
					foreach (MerchantItem merchantItem2 in world2.eventMerchantItems)
					{
						string id7 = merchantItem2.GetId();
						if (saveData.eventMerchantItemLevels.ContainsKey(id7))
						{
							merchantItem2.SetLevel(saveData.eventMerchantItemLevels[id7]);
						}
					}
				}
			}
			foreach (World world3 in this.allWorlds)
			{
				string id8 = world3.GetId();
				if (saveData.worldMerchantUseStates.ContainsKey(id8))
				{
					WorldMerchantUseState worldMerchantUseState = saveData.worldMerchantUseStates[id8];
					world3.timeWarpTimeLeft = worldMerchantUseState.timeWarpTimeLeft;
					world3.timeWarpSpeed = worldMerchantUseState.timeWarpSpeed;
					world3.autoTapTimeLeft = worldMerchantUseState.autoTapTimeLeft;
					world3.powerUpTimeLeft = worldMerchantUseState.powerUpTimeLeft;
					world3.powerUpDamageFactorAdd = worldMerchantUseState.powerUpDamageFactorAdd;
					world3.goldBoostTimeLeft = worldMerchantUseState.goldBoostTimeLeft;
					world3.goldBoostFactor = worldMerchantUseState.goldBoostFactorAdd;
					world3.refresherOrbTimeLeft = worldMerchantUseState.refresherOrbTimeLeft;
					world3.refresherOrbSkillCoolFactor = worldMerchantUseState.refresherOrbSkillCoolFactor;
					world3.shieldTimeToAdd = worldMerchantUseState.shieldTimeLeft;
					world3.catalystTimeLeft = worldMerchantUseState.catalystTimeLeft;
					world3.catalystProgressPercentage = worldMerchantUseState.catalystProgressPercentage;
					world3.numCharmSelectionAdd = worldMerchantUseState.numCharmSelectionAdd;
					world3.pickRandomCharms = worldMerchantUseState.pickRandomCharmsEnabled;
					world3.blizzardTimeLeft = 0f;
					world3.blizzardSlowFactor = worldMerchantUseState.blizzardSlowFactor;
					world3.hotCocoaTimeLeft = 0f;
					world3.hotCocoaCooldownReductionFactor = worldMerchantUseState.hotCocoaCooldownReductionFactor;
					world3.hotCocoaDamageFactor = worldMerchantUseState.hotCocoaDamageFactor;
					world3.ornamentDropTimeLeft = 0f;
					world3.ornamentDropTeamDamageFactor = worldMerchantUseState.ornamentDropTeamDamageFactor;
					world3.ornamentDropTargetTime = worldMerchantUseState.ornamentDropTargetTaps;
					world3.ornamentDropProjectilesCount = worldMerchantUseState.ornamentDropProjectilesCount;
					world3.ornamentDropCurrentTime = worldMerchantUseState.ornamentDropCurrentTime;
				}
			}
			foreach (World world4 in this.allWorlds)
			{
				string id9 = world4.GetId();
				if (saveData.unlockedWorldIds.Contains(id9))
				{
					this.UnlockGameMode(world4);
				}
			}
			foreach (World world5 in this.allWorlds)
			{
				string id10 = world5.GetId();
				if (saveData.worldsTotWave.ContainsKey(id10))
				{
					int num = saveData.worldsTotWave[id10];
					if (world5.activeChallenge.HasTargetTotWave())
					{
						num = GameMath.GetMinInt(num, world5.activeChallenge.GetTargetTotWave());
					}
					world5.activeChallenge.LoadTotWave(num);
				}
			}
			foreach (World world6 in this.allWorlds)
			{
				string id11 = world6.GetId();
				Challenge.State state = (!saveData.worldsChallengeStates.ContainsKey(id11)) ? Challenge.State.SETUP : SaveLoadManager.ConvertChallengeState(saveData.worldsChallengeStates[id11]);
				world6.activeChallenge.state = state;
			}
			foreach (World world7 in this.allWorlds)
			{
				string id12 = world7.GetId();
				if (world7.activeChallenge is ChallengeWithTime)
				{
					ChallengeWithTime challengeWithTime = (ChallengeWithTime)world7.activeChallenge;
					if (saveData.worldsChallengeTimePassed.ContainsKey(id12))
					{
						challengeWithTime.timeCounter = saveData.worldsChallengeTimePassed[id12];
					}
				}
			}
			foreach (World world8 in this.allWorlds)
			{
				string id13 = world8.GetId();
				double amountForLoading = (!saveData.worldsGold.ContainsKey(id13)) ? 0.0 : saveData.worldsGold[id13];
				world8.gold.SetAmountForLoading(amountForLoading);
			}
			foreach (World world9 in this.allWorlds)
			{
				string id14 = world9.GetId();
				if (saveData.worldsNumMerchantItemsUsed.ContainsKey(id14))
				{
					Dictionary<string, int> dictionary = saveData.worldsNumMerchantItemsUsed[id14];
					foreach (MerchantItem merchantItem3 in world9.merchantItems)
					{
						string id15 = merchantItem3.GetId();
						int numUsed = (!dictionary.ContainsKey(id15)) ? 0 : dictionary[id15];
						merchantItem3.SetNumUsed(numUsed);
					}
				}
			}
			if (saveData.worldsNumEventMerchantItemsUsed != null)
			{
				foreach (World world10 in this.allWorlds)
				{
					string id16 = world10.GetId();
					if (saveData.worldsNumEventMerchantItemsUsed.ContainsKey(id16))
					{
						Dictionary<string, int> dictionary2 = saveData.worldsNumEventMerchantItemsUsed[id16];
						foreach (MerchantItem merchantItem4 in world10.eventMerchantItems)
						{
							string id17 = merchantItem4.GetId();
							int numUsed2 = (!dictionary2.ContainsKey(id17)) ? 0 : dictionary2[id17];
							merchantItem4.SetNumUsed(numUsed2);
						}
					}
				}
			}
			if (saveData.worldsNumMerchantItemsInInventory != null)
			{
				foreach (World world11 in this.allWorlds)
				{
					string id18 = world11.GetId();
					if (saveData.worldsNumMerchantItemsInInventory.ContainsKey(id18))
					{
						Dictionary<string, int> dictionary3 = saveData.worldsNumMerchantItemsInInventory[id18];
						foreach (MerchantItem merchantItem5 in world11.merchantItems)
						{
							string id19 = merchantItem5.GetId();
							int numInInventory = (!dictionary3.ContainsKey(id19)) ? 0 : dictionary3[id19];
							merchantItem5.SetNumInInventory(numInInventory);
						}
					}
				}
			}
			if (saveData.worldsNumEventMerchantItemsInInventory != null)
			{
				foreach (World world12 in this.allWorlds)
				{
					string id20 = world12.GetId();
					if (saveData.worldsNumEventMerchantItemsInInventory.ContainsKey(id20))
					{
						Dictionary<string, int> dictionary4 = saveData.worldsNumEventMerchantItemsInInventory[id20];
						foreach (MerchantItem merchantItem6 in world12.eventMerchantItems)
						{
							string id21 = merchantItem6.GetId();
							int numInInventory2 = (!dictionary4.ContainsKey(id21)) ? 0 : dictionary4[id21];
							merchantItem6.SetNumInInventory(numInInventory2);
						}
					}
				}
			}
			foreach (World world13 in this.allWorlds)
			{
				string id22 = world13.GetId();
				int stageNo = (!saveData.worldsMaxStageReached.ContainsKey(id22)) ? 0 : saveData.worldsMaxStageReached[id22];
				world13.LoadMaxStageReached(stageNo);
			}
			foreach (World world14 in this.allWorlds)
			{
				string id23 = world14.GetId();
				int maxHeroLevelReached = (!saveData.worldsMaxHeroLevelReached.ContainsKey(id23)) ? 0 : saveData.worldsMaxHeroLevelReached[id23];
				world14.SetMaxHeroLevelReached(maxHeroLevelReached);
			}
			if (saveData.worldsNumGivenSkillPoints != null)
			{
				foreach (World world15 in this.allWorlds)
				{
					string id24 = world15.GetId();
					if (saveData.worldsNumGivenSkillPoints.ContainsKey(id24))
					{
						world15.givenSkillPoints = saveData.worldsNumGivenSkillPoints[id24];
					}
				}
			}
			foreach (World world16 in this.allWorlds)
			{
				string id25 = world16.GetId();
				if (saveData.worldsNumBoughtWorldUpgrades.ContainsKey(id25))
				{
					world16.RefreshChallengeUpgrades(saveData.worldsNumBoughtWorldUpgrades[id25]);
				}
			}
			if (saveData.worldsActiveCharms != null && saveData.worldsCharmBuffStates != null)
			{
				foreach (World world17 in this.allWorlds)
				{
					string id26 = world17.GetId();
					if ((world17.activeChallenge.state == Challenge.State.ACTION || world17.activeChallenge.state == Challenge.State.WON) && world17.activeChallenge is ChallengeRift && saveData.worldsActiveCharms.ContainsKey(id26))
					{
						List<int> list3 = saveData.worldsActiveCharms[id26];
						ChallengeRift challengeRift3 = world17.activeChallenge as ChallengeRift;
						List<int> list4 = new List<int>();
						foreach (int num2 in list3)
						{
							if (num2 < 1000)
							{
								CharmEffectData ceData = this.allCharmEffects[num2];
								challengeRift3.LoadCharm(ceData);
								list4.Add(0);
							}
							else
							{
								CurseEffectData curseEffectData = this.allCurseEffects[num2];
								challengeRift3.LoadCurse(curseEffectData);
								list4.Add(1);
							}
						}
						challengeRift3.ApplyAllEnchantmentEffects(list4);
						if (saveData.worldsCharmBuffStates.ContainsKey(id26))
						{
							List<float> list5 = saveData.worldsCharmBuffStates[id26];
							for (int k = 0; k < list5.Count; k++)
							{
								challengeRift3.allEnchantments[k].progress = list5[k];
							}
						}
					}
				}
			}
			if (saveData.worldsCurseLevels != null)
			{
				foreach (World world18 in this.allWorlds)
				{
					string id27 = world18.GetId();
					if (world18.activeChallenge.state == Challenge.State.ACTION && world18.activeChallenge is ChallengeRift && saveData.worldsActiveCharms.ContainsKey(id27))
					{
						List<int> list6 = saveData.worldsCurseLevels[id27];
						ChallengeRift challengeRift4 = world18.activeChallenge as ChallengeRift;
						for (int l = 0; l < list6.Count; l++)
						{
							challengeRift4.activeCurseEffects[l].level = list6[l];
						}
						challengeRift4.curseProgress = saveData.worldsCurseProgress[id27];
						challengeRift4.RefreshCurseLevels();
					}
				}
			}
			if (saveData.worldsCurseSpawnIndexes != null)
			{
				foreach (World world19 in this.allWorlds)
				{
					string id28 = world19.GetId();
					if (world19.activeChallenge.state == Challenge.State.ACTION && world19.activeChallenge is ChallengeRift && saveData.worldsActiveCharms.ContainsKey(id28))
					{
						ChallengeRift challengeRift5 = world19.activeChallenge as ChallengeRift;
						challengeRift5.curseSpawnIndex = saveData.worldsCurseSpawnIndexes[id28];
					}
				}
			}
			if (saveData.worldsDiscardedCharms != null)
			{
				foreach (World world20 in this.allWorlds)
				{
					string id29 = world20.GetId();
					if (world20.activeChallenge.state == Challenge.State.ACTION && world20.activeChallenge is ChallengeRift && saveData.worldsDiscardedCharms.ContainsKey(id29))
					{
						List<int> list7 = saveData.worldsDiscardedCharms[id29];
						ChallengeRift challengeRift6 = world20.activeChallenge as ChallengeRift;
						foreach (int item3 in list7)
						{
							challengeRift6.discardedCharms.Add(item3);
						}
					}
				}
			}
			if (saveData.worldsCharmSelectionNums != null && saveData.worldsCharmSelectionTimers != null)
			{
				foreach (World world21 in this.allWorlds)
				{
					string id30 = world21.GetId();
					if (world21.activeChallenge.state == Challenge.State.ACTION && world21.activeChallenge is ChallengeRift)
					{
						ChallengeRift challengeRift7 = world21.activeChallenge as ChallengeRift;
						if (saveData.worldsCharmSelectionNums.ContainsKey(id30))
						{
							challengeRift7.numCharmSelection = saveData.worldsCharmSelectionNums[id30];
						}
						if (saveData.worldsCharmSelectionTimers.ContainsKey(id30))
						{
							challengeRift7.charmSelectionAddTimer = saveData.worldsCharmSelectionTimers[id30];
						}
					}
				}
			}
			this.unlockedHeroIds = saveData.unlockedHeroIds;
			this.unlockedTotemIds = saveData.unlockedTotemIds;
			this.newHeroIconSelectedHeroIds = saveData.newHeroIconSelectedHeroIds;
			foreach (World world22 in this.allWorlds)
			{
				string id31 = world22.GetId();
				if (saveData.worldsBoughtTotems.ContainsKey(id31))
				{
					string id32 = saveData.worldsBoughtTotems[id31];
					TotemDataBase totemDataBase = this.GetTotemDataBase(id32);
					this.LoadTotem(totemDataBase, world22);
				}
			}
			foreach (World world23 in this.allWorlds)
			{
				string id33 = world23.GetId();
				if (world23.activeChallenge.state != Challenge.State.SETUP)
				{
					if (saveData.worldsBoughtHeroes.ContainsKey(id33))
					{
						List<string> list8 = saveData.worldsBoughtHeroes[id33];
						int m = 0;
						int count = list8.Count;
						while (m < count)
						{
							string heroId = list8[m];
							HeroDataBase heroDataBase = this.GetHeroDataBase(heroId);
							world23.LoadNewHero(heroDataBase, this.boughtGears, false);
							m++;
						}
					}
				}
			}
			foreach (World world24 in this.allWorlds)
			{
				if (world24.totem != null)
				{
					Totem totem = world24.totem;
					string id34 = totem.GetId();
					if (saveData.totemXps.ContainsKey(id34))
					{
						int minInt = GameMath.GetMinInt(saveData.totemLevels[id34], 90);
						totem.SetXp(GameMath.GetMinInt(saveData.totemXps[id34], TotemDataBase.LEVEL_XPS[minInt] - 1));
						totem.SetLevel(minInt);
					}
				}
			}
			foreach (World world25 in this.allWorlds)
			{
				if (world25.totem != null)
				{
					world25.totem.Refresh();
				}
			}
			foreach (HeroDataBase heroDataBase2 in this.allHeroes)
			{
				string id35 = heroDataBase2.id;
				heroDataBase2.evolveLevel = ((!saveData.heroEvolveLevels.ContainsKey(id35)) ? 0 : saveData.heroEvolveLevels[id35]);
				if (saveData.heroEquippedSkins != null && saveData.heroEquippedSkins.ContainsKey(id35))
				{
					heroDataBase2.equippedSkin = this.GetSkinData(saveData.heroEquippedSkins[id35]);
				}
				else
				{
					heroDataBase2.equippedSkin = this.GetHeroEvolveSkin(heroDataBase2.id, heroDataBase2.evolveLevel);
				}
				if (saveData.heroRandomSkinsEnabled != null && saveData.heroRandomSkinsEnabled.ContainsKey(id35))
				{
					heroDataBase2.randomSkinsEnabled = saveData.heroRandomSkinsEnabled[id35];
				}
				else
				{
					heroDataBase2.randomSkinsEnabled = false;
				}
				heroDataBase2.skillBranchesEverUnlocked[0] = ((saveData.heroSkillBranchesEverUnlocked == null || !saveData.heroSkillBranchesEverUnlocked.ContainsKey(id35)) ? 0 : saveData.heroSkillBranchesEverUnlocked[id35][0]);
				heroDataBase2.skillBranchesEverUnlocked[1] = ((saveData.heroSkillBranchesEverUnlocked == null || !saveData.heroSkillBranchesEverUnlocked.ContainsKey(id35)) ? 0 : saveData.heroSkillBranchesEverUnlocked[id35][1]);
			}
			bool flag = saveData.boughtHeroSkins == null;
			foreach (SkinData skinData3 in this.allSkins)
			{
				if (skinData3.unlockType == SkinData.UnlockType.HERO_EVOLVE_LEVEL && skinData3.CanAfford((double)skinData3.belongsTo.evolveLevel) && !this.boughtSkins.Contains(skinData3))
				{
					this.boughtSkins.Add(skinData3);
					if (flag)
					{
						skinData3.isNew = false;
					}
				}
			}
			foreach (World world26 in this.allWorlds)
			{
				foreach (Hero hero in world26.heroes)
				{
					string distinctId = hero.GetDistinctId();
					if (saveData.heroXps.ContainsKey(distinctId))
					{
						int minInt2 = GameMath.GetMinInt(saveData.heroLevels[distinctId], 90);
						hero.SetXp(GameMath.GetMinInt(saveData.heroXps[distinctId], world26.GetRequiredXpToLevelHero(minInt2) - 1));
						hero.SetLevel(minInt2);
						hero.SetNumUnspentSkillPoints(saveData.heroNumUnspentSkillPoints[distinctId]);
						hero.SetSkillTreeProgressGained(SaveLoadManager.ConvertSkillTreeProgress(saveData.heroSkillTreesProgressGained[distinctId]));
						hero.SetHealthRatio(saveData.heroHealthRatios[distinctId]);
						if (saveData.heroCostMultipliers != null)
						{
							hero.costMultiplier = saveData.heroCostMultipliers[distinctId];
						}
						else
						{
							hero.costMultiplier = 1.0;
						}
					}
					hero.Refresh();
					if (saveData.heroSkillCooldowns.ContainsKey(distinctId))
					{
						hero.LoadSkillCooldowns(saveData.heroSkillCooldowns[distinctId]);
					}
				}
			}
			this.cursedGatesBeaten = saveData.cursedGatesBeaten;
			this.currentCurses = saveData.currentCurses;
			this.cursedRiftSlots.Load(this.worldRift);
			foreach (World world27 in this.allWorlds)
			{
				world27.Update(0f, null);
			}
			foreach (World world28 in this.allWorlds)
			{
				foreach (Hero hero2 in world28.heroes)
				{
					string id36 = hero2.GetId();
					if (saveData.heroTillReviveTimes.ContainsKey(id36))
					{
						hero2.SetTillReviveTime(saveData.heroTillReviveTimes[id36]);
					}
				}
			}
			if (saveData.heroGeneric != null)
			{
				foreach (World world29 in this.allWorlds)
				{
					foreach (Hero hero3 in world29.heroes)
					{
						string id37 = hero3.GetId();
						if (saveData.heroGeneric.ContainsKey(id37))
						{
							hero3.LoadSaveDataGeneric(saveData.heroGeneric[id37]);
						}
					}
				}
			}
			this.prefers30Fps = saveData.prefers30Fps;
			this.appNeverSleep = saveData.sleep;
			this.scientificNotation = saveData.snot;
			this.secondaryCdUi = saveData.scd;
			this.compassDisabled = saveData.compassDisabled;
			this.setSoundsMute = saveData.soundsMute;
			this.setMusicMute = saveData.musicMute;
			this.setVoicesMute = saveData.voicesMute;
			this.skillOneTapUpgrade = saveData.skillOneTapUpgrade;
			if (this.scientificNotation)
			{
				GameMath.NOTATION_STYLE = GameMath.NotationStyle.SCIENTIFIC;
			}
			else
			{
				GameMath.NOTATION_STYLE = GameMath.NotationStyle.CLASSIC;
			}
			if (saveData.wasWatchingAd && RewardedAdManager.inst != null && RewardedAdManager.inst.canGiveCrashReward)
			{
				RewardedAdManager.inst.canGiveCrashReward = false;
				if (saveData.wasWatchingAdForFlashOffer)
				{
					RewardedAdManager.inst.targetFlashOfferType = new FlashOffer.Type?(saveData.adFlashOfferRewardType);
					RewardedAdManager inst = RewardedAdManager.inst;
					FlashOffer.Type? targetFlashOfferType = RewardedAdManager.inst.targetFlashOfferType;
					inst.shouldGiveReward = (targetFlashOfferType != null);
				}
				else
				{
					this.activeWorld.adRewardCurrencyType = SaveLoadManager.ConvertCurrencyType(saveData.adRewardCurrencyType);
					this.activeWorld.adRewardAmount = saveData.adRewardAmount;
					RewardedAdManager.inst.shouldGiveReward = true;
				}
			}
			if (saveData.wasWatchingAdCapped && RewardedAdManager.inst != null && RewardedAdManager.inst.canGiveCrashReward)
			{
				RewardedAdManager.inst.canGiveCrashReward = false;
				RewardedAdManager.inst.shouldGiveRewardCapped = true;
				RewardedAdManager.inst.currencyTypeForCappedVideo = SaveLoadManager.ConvertCurrencyType(saveData.adRewardCurrencyType);
				RewardedAdManager.inst.rewardAmountForCappedVideo = saveData.adRewardAmount;
			}
			this.askedForRate = saveData.askedForRate;
			this.ratingState = ((!this.askedForRate) ? saveData.ratingState : RatingState.DontAskAgain);
			if (this.ratingState == RatingState.NeverAsked && this.unlockedHeroIds.Contains("SAM"))
			{
				this.shouldAskForRate = true;
			}
			if (PlayfabManager.allPlayfabIds == null)
			{
				PlayfabManager.allPlayfabIds = new List<string>();
			}
			if (saveData.allPlayfabIds != null)
			{
				foreach (string item4 in saveData.allPlayfabIds)
				{
					if (!PlayfabManager.allPlayfabIds.Contains(item4))
					{
						PlayfabManager.allPlayfabIds.Add(item4);
					}
				}
			}
			this.allTrinkets = new List<Trinket>();
			if (saveData.trinkets != null)
			{
				this.hasEverOwnedATrinket = (saveData.hasEverOwnedATrinket || saveData.trinkets.Count > 0);
				foreach (TrinketSerializable t in saveData.trinkets)
				{
					this.allTrinkets.Add(SaveLoadManager.ConvertTrinket(t));
				}
			}
			this.disassembledTinketEffects = new Dictionary<int, int>();
			if (saveData.disassembledTrinketEffects != null)
			{
				this.disassembledTinketEffects = saveData.disassembledTrinketEffects;
				this.TryRevealAllTrinketEffects();
				this.CleanUnusedTrinketEffectsFromDisassembledList();
			}
			this.numTrinketsObtained = saveData.numTrinketsObtained;
			if (this.numTrinketsObtained == 0)
			{
				this.numTrinketsObtained = this.allTrinkets.Count;
			}
			foreach (HeroDataBase heroDataBase3 in this.allHeroes)
			{
				if (saveData.heroTrinkets != null && saveData.heroTrinkets.ContainsKey(heroDataBase3.id) && saveData.heroTrinkets[heroDataBase3.id] < this.allTrinkets.Count)
				{
					heroDataBase3.trinket = this.allTrinkets[saveData.heroTrinkets[heroDataBase3.id]];
				}
				else
				{
					heroDataBase3.trinket = null;
				}
				if (saveData.heroTrinketTimers != null && saveData.heroTrinketTimers.ContainsKey(heroDataBase3.id))
				{
					heroDataBase3.trinketEquipTimer = saveData.heroTrinketTimers[heroDataBase3.id];
				}
			}
			this.RefreshTrinketEffects();
			ShopPackStarter.appeared = saveData.spsa;
			ShopPackStarter.appearedBefore = saveData.spsab;
			ShopPackThreePijama.appeared = saveData.sptpa;
			ShopPackStarter.purchased = saveData.spsp;
			ShopPackXmas.purchased = saveData.spxp;
			ShopPackBigGem.appeared = saveData.spbga;
			ShopPackBigGemTwo.appeared = saveData.spbgta;
			ShopPackBigGem.purchased = saveData.spbgp;
			ShopPackBigGemTwo.purchased = saveData.spbgtp;
			ShopPackRiftOffer01.appeared = saveData.spro01a;
			ShopPackRiftOffer01.purchased = saveData.spro01p;
			ShopPackRiftOffer02.appeared = saveData.spro02a;
			ShopPackRiftOffer02.purchased = saveData.spro02p;
			ShopPackRiftOffer03.appeared = saveData.spro03a;
			ShopPackRiftOffer03.purchased = saveData.spro03p;
			ShopPackRiftOffer04.appeared = saveData.spro04a;
			ShopPackRiftOffer04.purchased = saveData.spro04p;
			ShopPackRegional01.purchased = saveData.regionalOffer01Purchased;
			ShopPackRegional01.appeared = saveData.regionalOffer01Appeared;
			ShopPackStage100.appeared = saveData.spsha;
			ShopPackStage100.purchased = saveData.spshp;
			ShopPackHalloweenGems.purchased = saveData.halloweenOfferGemsPurchased;
			ShopPackHalloweenGems.appeared = saveData.halloweenOfferGemsAppeared;
			ShopPackChristmasGemsSmall.purchased = saveData.christmasOfferGemsSmallPurchased;
			ShopPackChristmasGemsSmall.appeared = saveData.christmasOfferGemsSmallAppeared;
			ShopPackChristmasGemsBig.purchased = saveData.christmasOfferGemsBigPurchased;
			ShopPackChristmasGemsBig.appeared = saveData.christmasOfferGemsBigAppeared;
			ShopPackStage300.appeared = saveData.stage200OfferAppeared;
			ShopPackStage300.purchased = saveData.stage200OfferPurchased;
			ShopPackStage800.appeared = saveData.stage500OfferAppeared;
			ShopPackStage800.purchased = saveData.stage500OfferPurchased;
			ShopPackSecondAnniversaryGems.appeared = saveData.secondAnniversaryGemsOfferAppeared;
			ShopPackSecondAnniversaryGems.purchased = saveData.secondAnniversaryGemsOfferPurchased;
			ShopPackSecondAnniversaryCurrencyBundle.appeared = saveData.secondAnniversaryBundleOfferAppeared;
			ShopPackSecondAnniversaryCurrencyBundle.purchased = saveData.secondAnniversaryBundleOfferPurchased;
			ShopPackSecondAnniversaryGemsTwo.appeared = saveData.secondAnniversaryGemsTwoOfferAppeared;
			ShopPackSecondAnniversaryGemsTwo.purchased = saveData.secondAnniversaryGemsTwoOfferPurchased;
			ShopPackSecondAnniversaryCurrencyBundleTwo.appeared = saveData.secondAnniversaryBundleTwoOfferAppeared;
			ShopPackSecondAnniversaryCurrencyBundleTwo.purchased = saveData.secondAnniversaryBundleTwoOfferPurchased;
			this.numOffersAppeared = saveData.offersAppearedCount;
			this.numTrinketPacks = saveData.numTrinketPacks;
			this.numUnseenTrinketPacks = saveData.numUnseenTrinketPacks;
			this.numSmallCharmPacks = saveData.numCharmPacks;
			if (saveData.achiColls != null)
			{
				foreach (KeyValuePair<int, bool> keyValuePair5 in saveData.achiColls)
				{
					string key5 = SaveLoadManager.ConvertAchievementId(keyValuePair5.Key);
					if (Simulator.achievementCollecteds.ContainsKey(key5))
					{
						Simulator.achievementCollecteds[key5] = saveData.achiColls[keyValuePair5.Key];
					}
				}
			}
			if (saveData.newStats != null)
			{
				this.newStats = saveData.newStats;
			}
			if (saveData.trinketsPinned != null)
			{
				try
				{
					this.trinketsPinned = saveData.trinketsPinned;
					this.trinketsPinnedHashSet = new Dictionary<Trinket, int>();
					foreach (int num3 in this.trinketsPinned)
					{
						this.trinketsPinnedHashSet.Add(this.allTrinkets[num3], num3);
					}
				}
				catch
				{
					this.trinketsPinned = new List<int>();
					this.trinketsPinnedHashSet = new Dictionary<Trinket, int>();
				}
			}
			SaveLoadManager.SetMine(this.mineToken, saveData.mineToken);
			SaveLoadManager.SetMine(this.mineScrap, saveData.mineScrap);
			this.dailyQuest = SaveLoadManager.ConvertDailyQuest(saveData.daily);
			if (this.dailyQuest != null)
			{
				this.dailyQuest.progress = saveData.dailyProgress;
			}
			this.lastDailyQuest = saveData.lastDaily;
			this.dailySkipCount = saveData.dailySkip;
			this.dailyQuestsAppearedCount = saveData.dailyQuestsAppearedCount;
			this.isSkinsEverClicked = saveData.isSkinsEverClicked;
			if (!this.isSkinsEverClicked)
			{
				this.isSkinsEverClicked = (this.allSkins.Find((SkinData s) => !s.isNew) != null);
			}
			this.dailyQuestCollectedTime = new DateTime(saveData.dailyTime);
			this.numCandyQuestCompleted = saveData.numCandyQuest;
			this.numCandyQuestAnniversaryCompleted = saveData.numCandyAnQuest;
			this.riftRestRewardCollectedTime = new DateTime(saveData.riftTime);
			this.prestigeRunTimer = saveData.prestigeRunTime;
			this.lastPrestigeRunstats = saveData.lastPrestigeRun;
			this.riftQuestDustCollected = saveData.riftQuestDustCollected;
			this.hasRiftQuest = saveData.hasRiftQuest;
			this.questOfUpdate = SaveLoadManager.ConvertQuestOfUpdate(saveData.qou);
			if (saveData.completedQOUs != null)
			{
				this.completedQuestOfUpdates = saveData.completedQOUs;
			}
			if (saveData.failedQOUs != null)
			{
				this.failedQuestOfUpdates = saveData.failedQOUs;
			}
			this.UpdateUniversalTotalBonus();
			this.OnArtifactsChanged();
			if (this.numPrestiges > 0)
			{
				Unlock unlock2 = this.worldStandard.unlocks.Find((Unlock u) => u.GetId() == UnlockIds.PRESTIGE);
				if (unlock2 != null && !unlock2.isCollected)
				{
					this.TryCollectUnlockReward(unlock2);
				}
			}
			foreach (World world30 in this.allWorlds)
			{
				world30.StartShield(world30.shieldTimeToAdd);
				world30.shieldTimeToAdd = 0f;
			}
			if (saveData.cardPackCounters != null)
			{
				CharmDataBase.NotOpenedCounters = saveData.cardPackCounters;
			}
			if (saveData.riftRecords != null)
			{
				foreach (Challenge challenge6 in this.worldRift.allChallenges)
				{
					ChallengeRift challengeRift8 = (ChallengeRift)challenge6;
					double finishingRecord;
					if (saveData.riftRecords.TryGetValue(challengeRift8.id, out finishingRecord))
					{
						challengeRift8.finishingRecord = finishingRecord;
					}
				}
			}
			this.numSmallCharmPacksOpened = saveData.numCharmPacksOpened;
			foreach (CharmEffectData charmEffectData2 in this.allCharmEffects.Values)
			{
				if (!charmEffectData2.IsLocked() && charmEffectData2.GetNumPacksRequired() > this.numSmallCharmPacksOpened)
				{
					this.numSmallCharmPacksOpened = charmEffectData2.GetNumPacksRequired();
				}
			}
			this.riftDiscoveryIndex = saveData.riftDiscoveryIndex;
			foreach (Challenge challenge7 in this.worldRift.allChallenges)
			{
				ChallengeRift challengeRift9 = challenge7 as ChallengeRift;
				if (challengeRift9.unlock.isCollected && challengeRift9.discoveryIndex > this.riftDiscoveryIndex)
				{
					this.riftDiscoveryIndex = challengeRift9.discoveryIndex;
				}
			}
			this.numRiftQuestsCompleted = saveData.numRiftQuestsCompleted;
			if (this.numRiftQuestsCompleted == 0 && this.riftDiscoveryIndex > 0)
			{
				this.numRiftQuestsCompleted = 1;
			}
			this.socialRewardsStatus = saveData.socialRewardsStatus;
			if (this.socialRewardsStatus == null)
			{
				this.socialRewardsStatus = new Dictionary<SocialRewards.Network, Status>();
				foreach (SocialRewards.Network key6 in Manager.NetworkList)
				{
					this.socialRewardsStatus.Add(key6, new Status());
				}
			}
			this.lastNewsTimestam = saveData.lastNewsTimestam;
			this.lootpacksOpenedCount = saveData.lootpacksOpenedCount;
			if (this.lootpacksOpenedCount == null)
			{
				this.lootpacksOpenedCount = new Dictionary<string, int>();
			}
			if (saveData.lastFiveOpenedCharms != null)
			{
				foreach (int item5 in saveData.lastFiveOpenedCharms)
				{
					this.lastFiveOpenedCharms.Enqueue(item5);
				}
			}
			if (this.lastFiveOpenedCharms == null)
			{
				this.lastFiveOpenedCharms = new Queue<int>();
			}
			SaveLoadManager.LoadSpecialOfferBoard(this.specialOfferBoard, saveData);
			this.specialOfferBoard.Init();
			this.lastAddedCurseChallengeTime = new DateTime(saveData.timeLastAddedCursedRift);
			if (saveData.announcedOffersTimes != null)
			{
				this.announcedOffersTimes = saveData.announcedOffersTimes;
			}
			if (saveData.flashOfferBundle != null)
			{
				int maxInt = this.maxStagePrestigedAt;
				if (this.worldStandard.activeChallenge != null && this.worldStandard.activeChallenge.state == Challenge.State.ACTION)
				{
					maxInt = GameMath.GetMaxInt(maxInt, ChallengeStandard.GetStageNo(this.worldStandard.activeChallenge.GetTotWave()));
				}
				this.flashOfferBundle = SaveLoadManager.ConvertFlashOfferBundle(saveData.flashOfferBundle, maxInt);
			}
			if (saveData.halloweenFlashOfferBundle != null)
			{
				this.halloweenFlashOfferBundle = SaveLoadManager.ConvertServerSideFlashOfferBundle(saveData.halloweenFlashOfferBundle);
			}
			this.lastSelectedRegularGateIndex = saveData.lastSelectedRegularGateIndex;
			if (this.GetDiscoveredRiftCount() <= this.lastSelectedRegularGateIndex)
			{
				this.lastSelectedRegularGateIndex = 0;
			}
			if (this.worldRift.allChallenges.Contains(this.worldRift.activeChallenge) && this.worldRift.allChallenges.IndexOf(this.worldRift.activeChallenge) >= this.GetDiscoveredRiftCount())
			{
				this.worldRift.activeChallenge.Reset();
				this.worldRift.activeChallenge = this.worldRift.allChallenges[0];
				this.worldRift.ResetPrestige();
				if (this.currentGameMode == GameMode.RIFT)
				{
					this.currentGameMode = GameMode.STANDARD;
					this.SwitchGameMode(this.currentGameMode);
				}
			}
			this.hasTrinketSmith = saveData.trinketSmith;
			this.amountLootPacksOpenedForHint = saveData.amountLootpacksOpenedForHint;
			this.installDate = new DateTime(saveData.installDate);
			this.usedTrinketExploit = saveData.usedTrinketExploit;
			this.christmasOffersBundlePurchasesLeftByOffer = saveData.christmasOffersBundlePurchasesLeft;
			if (this.christmasOfferBundle != null)
			{
				this.christmasOfferBundle.InitTreeState(this.christmasOffersBundlePurchasesLeftByOffer);
			}
			this.christmasEventAlreadyDisabled = saveData.christmasEventAlreadyDisabled;
			this.candyDropAlreadyDisabled = saveData.candyDropAlreadyDisabled;
			if (saveData.lastFreeCandyTreatClaimedDate == 0L)
			{
				this.lastFreeCandyTreatClaimedDate = DateTime.MinValue;
			}
			else
			{
				this.lastFreeCandyTreatClaimedDate = new DateTime(saveData.lastFreeCandyTreatClaimedDate);
			}
			if (saveData.lastSessionDate == 0L)
			{
				this.lastSessionDate = DateTime.MinValue;
			}
			else
			{
				this.lastSessionDate = new DateTime(saveData.lastSessionDate);
			}
			this.christmasEventPopupsShown = saveData.christmasEventPopupsShown;
			this.christmasTreatVideosWatchedSinceLastReset = saveData.christmasTreatVideosWatchedSinceLastReset;
			if (saveData.rewardsToGive != null)
			{
				this.rewardsToGive = saveData.rewardsToGive;
			}
			this.christmasCandyCappedVideoNotificationSeen = saveData.christmasCandyCappedVideoNotificationSeen;
			this.christmasFreeCandyNotificationSeen = saveData.christmasFreeCandyNotificationSeen;
			if (saveData.earnedBadges != null)
			{
				foreach (int id38 in saveData.earnedBadges)
				{
					Badges.GetBadgeWithId((BadgeId)id38).LoadState(true, true);
				}
			}
			if (saveData.notificationDismissedBadges != null)
			{
				foreach (int id39 in saveData.notificationDismissedBadges)
				{
					Badges.GetBadgeWithId((BadgeId)id39).DimissNotification();
				}
			}
			this.christmasEventForbidden = saveData.christmasEventForbidden;
			this.prestigedDuringSecondAnniversaryEvent = saveData.prestigedDuringSecondAnniversaryEvent;
			this.isCataclysmSurviver = saveData.cataclysmSurviver;
			this.isStageRearrangeSurviver = saveData.stageRearrangeSurviver;
			this.numPrestigesSinceCataclysm = saveData.numPrestigesSinceCataclysm;
			this.maxStageReachedInCurrentAdventure = saveData.reachedMaxStageInCurrentAdventure;
			this.artifactMultiUpgradeIndex = saveData.artifactMultiUpgradeIndex;
			this.timeChallengesLostCount = saveData.timeChallengesLostCount;
			if (saveData.secondAnniversaryFlashOfferBundle != null)
			{
				this.secondAnniversaryFlashOffersBundle = SaveLoadManager.ConvertServerSideFlashOfferBundle(saveData.secondAnniversaryFlashOfferBundle);
			}
			if (this.dailyQuest is DailyQuestCollectCandy)
			{
				DailyQuestCollectCandy dailyQuestCollectCandy = this.dailyQuest as DailyQuestCollectCandy;
				dailyQuestCollectCandy.progress = dailyQuestCollectCandy.goal;
			}
			this.secondAnniversaryEventAlreadyDisabled = saveData.secondAnniversaryEventAlreadyDisabled;
		}

		public static void LoadTutorialState(SaveData saveData)
		{
			TutorialManager.setText = true;
			TutorialManager.first = SaveLoadManager.ConvertTutStateFirst(saveData.tutStateFirst);
			TutorialManager.hubTab = SaveLoadManager.ConvertTutStateHub(saveData.tutStateHub);
			if (TutorialManager.hubTab != TutorialManager.HubTab.FIN)
			{
				TutorialManager.hubTab = TutorialManager.HubTab.BEFORE_BEGIN;
			}
			TutorialManager.modeTab = SaveLoadManager.ConvertTutStateMode(saveData.tutStateMode);
			if (TutorialManager.modeTab != TutorialManager.ModeTab.FIN)
			{
				TutorialManager.modeTab = TutorialManager.ModeTab.BEFORE_BEGIN;
			}
			TutorialManager.artifactsTab = SaveLoadManager.ConvertTutStateArtifact(saveData.tutStateArtifacts);
			if (TutorialManager.artifactsTab != TutorialManager.ArtifactsTab.FIN)
			{
				TutorialManager.artifactsTab = TutorialManager.ArtifactsTab.BEFORE_BEGIN;
			}
			TutorialManager.shopTab = SaveLoadManager.ConvertTutStateShop(saveData.tutStateShop);
			if (TutorialManager.shopTab != TutorialManager.ShopTab.FIN)
			{
				TutorialManager.shopTab = TutorialManager.ShopTab.BEFORE_BEGIN;
			}
			TutorialManager.prestige = SaveLoadManager.ConvertTutStatePrestige(saveData.tutStatePrestige);
			if (TutorialManager.prestige != TutorialManager.Prestige.FIN)
			{
				TutorialManager.prestige = TutorialManager.Prestige.BEFORE_BEGIN;
			}
			TutorialManager.fightBossButton = SaveLoadManager.ConvertTutStateFightBossButton(saveData.tutStateFightBossButton);
			if (TutorialManager.fightBossButton != TutorialManager.FightBossButton.FIN)
			{
				TutorialManager.fightBossButton = TutorialManager.FightBossButton.BEFORE_BEGIN;
			}
			TutorialManager.skillScreen = SaveLoadManager.ConvertTutStateSkillScreen(saveData.tutStateSkillScreen);
			if (TutorialManager.skillScreen != TutorialManager.SkillScreen.FIN)
			{
				TutorialManager.skillScreen = TutorialManager.SkillScreen.BEFORE_BEGIN;
			}
			TutorialManager.gearScreen = SaveLoadManager.ConvertTutStateGearScreen(saveData.tutStateGearScreen);
			if (TutorialManager.gearScreen != TutorialManager.GearScreen.FIN)
			{
				TutorialManager.gearScreen = TutorialManager.GearScreen.BEFORE_BEGIN;
			}
			TutorialManager.runeScreen = SaveLoadManager.ConvertTutStateRuneScreen(saveData.tutStateRuneScreen);
			if (TutorialManager.runeScreen != TutorialManager.RuneScreen.FIN)
			{
				TutorialManager.runeScreen = TutorialManager.RuneScreen.BEFORE_BEGIN;
			}
			TutorialManager.ringPrestigeReminder = SaveLoadManager.ConvertTutStateRingPrestigeReminder(saveData.tutStateRingPrestigeReminder);
			if (TutorialManager.ringPrestigeReminder != TutorialManager.RingPrestigeReminder.FIN)
			{
				TutorialManager.ringPrestigeReminder = TutorialManager.RingPrestigeReminder.BEFORE_BEGIN;
			}
			TutorialManager.heroPrestigeReminder = SaveLoadManager.ConvertTutStateHeroPrestigeReminder(saveData.tutStateHeroPrestigeReminder);
			if (TutorialManager.heroPrestigeReminder != TutorialManager.HeroPrestigeReminder.FIN)
			{
				TutorialManager.heroPrestigeReminder = TutorialManager.HeroPrestigeReminder.BEFORE_BEGIN;
			}
			TutorialManager.mythicalArtifactsTab = SaveLoadManager.ConvertTutStateMythicalArtifactsTab(saveData.tutStateMythicalArtifactsTab);
			if (TutorialManager.mythicalArtifactsTab != TutorialManager.MythicalArtifactsTab.FIN)
			{
				TutorialManager.mythicalArtifactsTab = TutorialManager.MythicalArtifactsTab.BEFORE_BEGIN;
			}
			TutorialManager.trinketShop = SaveLoadManager.ConvertTutStateTrinketShop(saveData.tutStateTrinketShop);
			if (TutorialManager.trinketShop != TutorialManager.TrinketShop.FIN)
			{
				TutorialManager.trinketShop = TutorialManager.TrinketShop.BEFORE_BEGIN;
			}
			TutorialManager.trinketHeroTab = SaveLoadManager.ConvertTutStateTrinketHeroTab(saveData.tutStateTrinketHeroTab);
			if (TutorialManager.trinketHeroTab < TutorialManager.TrinketHeroTab.EQUIP)
			{
				TutorialManager.trinketHeroTab = TutorialManager.TrinketHeroTab.BEFORE_BEGIN;
			}
			else
			{
				TutorialManager.trinketHeroTab = TutorialManager.TrinketHeroTab.FIN;
			}
			TutorialManager.mineUnlock = SaveLoadManager.ConvertTutStateMineUnlock(saveData.tutStateMineUnlock);
			if (TutorialManager.mineUnlock != TutorialManager.MineUnlock.FIN)
			{
				TutorialManager.mineUnlock = TutorialManager.MineUnlock.BEFORE_BEGIN;
			}
			TutorialManager.dailyUnlock = SaveLoadManager.ConvertTutStateDailyUnlock(saveData.tutStateDailyUnlock);
			if (TutorialManager.dailyUnlock != TutorialManager.DailyUnlock.FIN)
			{
				TutorialManager.dailyUnlock = TutorialManager.DailyUnlock.BEFORE_BEGIN;
			}
			TutorialManager.dailyComplete = SaveLoadManager.ConvertTutStateDailyComplete(saveData.tutStateDailyComplete);
			if (TutorialManager.dailyComplete != TutorialManager.DailyComplete.FIN)
			{
				TutorialManager.dailyComplete = TutorialManager.DailyComplete.BEFORE_BEGIN;
			}
			TutorialManager.riftsUnlock = SaveLoadManager.ConvertTutStateRiftsUnlocked(saveData.tutStateRiftsUnlocked);
			if (TutorialManager.riftsUnlock != TutorialManager.RiftsUnlock.FIN)
			{
				TutorialManager.riftsUnlock = TutorialManager.RiftsUnlock.BEFORE_BEGIN;
			}
			TutorialManager.riftEffects = SaveLoadManager.ConvertTutStateRiftEffects(saveData.tutStateRiftEffects);
			if (TutorialManager.riftEffects != TutorialManager.RiftEffects.FIN)
			{
				TutorialManager.riftEffects = TutorialManager.RiftEffects.BEFORE_BEGIN;
			}
			TutorialManager.firstCharm = SaveLoadManager.ConvertTutStateFirstCharm(saveData.tutStateFirstCharm);
			if (TutorialManager.firstCharm != TutorialManager.FirstCharm.FIN)
			{
				TutorialManager.firstCharm = TutorialManager.FirstCharm.BEFORE_BEGIN;
			}
			TutorialManager.charmHub = SaveLoadManager.ConvertTutStateCharmHub(saveData.tutStateCharmHub);
			if (TutorialManager.charmHub != TutorialManager.CharmHub.FIN)
			{
				TutorialManager.charmHub = TutorialManager.CharmHub.BEFORE_BEGIN;
			}
			TutorialManager.firstCharmPack = SaveLoadManager.ConvertTutStateFirstCharmPack(saveData.tutStateFirstCharmPack);
			if (TutorialManager.firstCharmPack != TutorialManager.FirstCharmPack.FIN)
			{
				TutorialManager.firstCharmPack = TutorialManager.FirstCharmPack.BEFORE_BEGIN;
			}
			TutorialManager.charmLevelUp = SaveLoadManager.ConvertTutStateCharmLevelUp(saveData.tutStateCharmLevelUp);
			if (TutorialManager.charmLevelUp != TutorialManager.CharmLevelUp.FIN)
			{
				TutorialManager.charmLevelUp = TutorialManager.CharmLevelUp.BEFORE_BEGIN;
			}
			TutorialManager.aeonDust = SaveLoadManager.ConvertTutStateAeonDust(saveData.tutStateAeonDust);
			if (TutorialManager.aeonDust != TutorialManager.AeonDust.FIN)
			{
				TutorialManager.aeonDust = TutorialManager.AeonDust.BEFORE_BEGIN;
			}
			TutorialManager.repeatRifts = SaveLoadManager.ConvertTutStateRepeatRifts(saveData.tutStateRepeatRifts);
			if (TutorialManager.repeatRifts != TutorialManager.RepeatRifts.FIN)
			{
				TutorialManager.repeatRifts = TutorialManager.RepeatRifts.BEFORE_BEGIN;
			}
			TutorialManager.allRiftsFinished = SaveLoadManager.ConvertTutStateAllRiftsFinished(saveData.tutStateAllRiftsFinished);
			if (TutorialManager.allRiftsFinished != TutorialManager.AllRiftsFinished.FIN)
			{
				TutorialManager.allRiftsFinished = TutorialManager.AllRiftsFinished.BEFORE_BEGIN;
			}
			TutorialManager.flashOffersUnlocked = SaveLoadManager.ConvertTutStateFlashOffersUnlocked(saveData.tutStateFlashOffersUnlocked);
			if (TutorialManager.flashOffersUnlocked != TutorialManager.FlashOffersUnlocked.FIN)
			{
				TutorialManager.flashOffersUnlocked = TutorialManager.FlashOffersUnlocked.BEFORE_BEGIN;
			}
			TutorialManager.cursedGates = SaveLoadManager.ConvertTutStateCursedGates(saveData.tutStateCursedGates);
			if (TutorialManager.cursedGates != TutorialManager.CursedGates.FIN)
			{
				TutorialManager.cursedGates = TutorialManager.CursedGates.BEFORE_BEGIN;
			}
			TutorialManager.missionsFinished = SaveLoadManager.ConvertTutStateMissionsFinished(saveData.tutStateMissionsFinished);
			if (TutorialManager.missionsFinished != TutorialManager.MissionsFinished.FIN)
			{
				TutorialManager.missionsFinished = TutorialManager.MissionsFinished.BEFORE_BEGIN;
			}
			TutorialManager.trinketSmithingUnlocked = SaveLoadManager.ConvertTutStateTrinketSmithingUnlocked(saveData.tutStateTrinketSmithingUnlocked);
			if (TutorialManager.trinketSmithingUnlocked != TutorialManager.TrinketSmithingUnlocked.FIN)
			{
				TutorialManager.trinketSmithingUnlocked = TutorialManager.TrinketSmithingUnlocked.BEFORE_BEGIN;
			}
			TutorialManager.trinketRecycleUnlocked = SaveLoadManager.ConvertTutStateTrinketRecycleUnlocked(saveData.tutStateTrinketRecycleUnlocked);
			if (TutorialManager.trinketRecycleUnlocked != TutorialManager.TrinketRecycleUnlocked.FIN)
			{
				TutorialManager.trinketRecycleUnlocked = TutorialManager.TrinketRecycleUnlocked.BEFORE_BEGIN;
			}
			TutorialManager.christmasTreeEventUnlocked = SaveLoadManager.ConvertTutStateChristmasTreeEventUnlocked(saveData.tutStateChristmasTreeEventUnlocked);
			if (TutorialManager.christmasTreeEventUnlocked != TutorialManager.ChristmasTreeEventUnlocked.FIN)
			{
				TutorialManager.christmasTreeEventUnlocked = TutorialManager.ChristmasTreeEventUnlocked.BEFORE_BEGIN;
			}
			if (saveData.tutStateArtifaceOverhaul < 0)
			{
				saveData.tutStateArtifaceOverhaul = 0;
			}
			else if (saveData.tutStateArtifaceOverhaul > 7)
			{
				saveData.tutStateArtifaceOverhaul = 7;
			}
			TutorialManager.artifactOverhaul = SaveLoadManager.ConverTutArtifactOverhaul(saveData.tutStateArtifaceOverhaul);
			TutorialManager.timeCounter = saveData.tutTimeCounter;
			TutorialManager.firstPeriod = saveData.tutFirstPeriod;
			TutorialManager.hubTabPeriod = saveData.tutHubTabPeriod;
			TutorialManager.modeTabPeriod = saveData.tutModeTabPeriod;
			TutorialManager.artifactsTabPeriod = saveData.tutArtifactsTabPeriod;
			TutorialManager.shopTabPeriod = saveData.tutShopTabPeriod;
			TutorialManager.prestigePeriod = saveData.tutPrestigePeriod;
			TutorialManager.skillScreenPeriod = saveData.tutSkillScreenPeriod;
			TutorialManager.fightBossButtonPeriod = saveData.tutFightBossButtonPeriod;
			TutorialManager.gearScreenPeriod = saveData.tutGearScreenPeriod;
			TutorialManager.gearGlobalReminderPeriod = saveData.tutGearGlobalReminderPeriod;
			TutorialManager.ringPrestigeReminderPeriod = saveData.tutRingPrestigeReminderPeriod;
			TutorialManager.heroPrestigeReminderPeriod = saveData.tutHeroPrestigeReminderPeriod;
			TutorialManager.mythicalArtifactsTabPeriod = saveData.tutMythicalArtifactsTabPeriod;
			TutorialManager.missionIndex = GameMath.GetMaxInt(TutorialManager.missionIndex, saveData.tutorialMissionIndex);
			if (TutorialManager.missionIndex > -1 && TutorialManager.missionIndex < TutorialMission.List.Length)
			{
				TutorialMission.List[TutorialManager.missionIndex].SetLoadState(saveData.tutorialMissionProgress, saveData.tutorialMissionClaimed);
			}
		}

		private void InitCommon()
		{
			this.installDate = DateTime.UtcNow;
			this.mythstones = new Currency(CurrencyType.MYTHSTONE);
			this.scraps = new Currency(CurrencyType.SCRAP);
			this.credits = new Currency(CurrencyType.GEM);
			this.tokens = new Currency(CurrencyType.TOKEN);
			this.aeons = new Currency(CurrencyType.AEON);
			this.candies = new Currency(CurrencyType.CANDY);
			this.allTotems = new List<TotemDataBase>();
			this.unlockedTotemIds = new HashSet<string>();
			this.allHeroes = new List<HeroDataBase>();
			this.unlockedHeroIds = new HashSet<string>();
			this.newHeroIconSelectedHeroIds = new HashSet<string>();
			this.allGears = new List<GearData>();
			this.boughtGears = new List<Gear>();
			this.trinketsPinned = new List<int>();
			this.trinketsPinnedHashSet = new Dictionary<Trinket, int>();
			this.allSkins = new List<SkinData>();
			this.boughtSkins = new List<SkinData>();
			this.completedQuestOfUpdates = new List<int>();
			this.failedQuestOfUpdates = new List<int>();
			this.universalTotalBonus = new UniversalTotalBonus();
			this.universalTotalBonus.Init();
			this.universalTotalBonusRift = new UniversalTotalBonus();
			this.universalTotalBonusRift.Init();
			this.newStats = new List<int>();
			if (this.socialRewardsStatus == null)
			{
				this.socialRewardsStatus = new Dictionary<SocialRewards.Network, Status>();
				foreach (SocialRewards.Network key in Manager.NetworkList)
				{
					this.socialRewardsStatus.Add(key, new Status());
				}
			}
			this.InitWorlds();
			this.InitData();
			if (TutorialManager.first == TutorialManager.First.WELCOME)
			{
				TotemDataBase totemDataBase = this.allTotems[0];
				this.worldStandard.LoadTotem(totemDataBase, this.wornRunes[totemDataBase.id]);
				this.worldStandard.activeChallenge.state = Challenge.State.ACTION;
			}
			UiManager.isPurchasing = false;
			this.lootpacks = new ShopPack[]
			{
				new ShopPackLootpackFree(),
				new ShopPackLootpackRare(),
				new ShopPackLootpackEpic()
			};
			this.shopPackTrinket = new ShopPackTrinket();
			this.shopPackSmallCharm = new ShopPackCharmPackSmall();
			this.shopPackBigCharm = new ShopPackCharmPackBig();
			this.mineToken = new MineToken();
			this.mineScrap = new MineScrap();
			this.isSkinsEverClicked = false;
			this.charmComparer = new CharmComparer();
			this.trinketComparer = new TrinketComparer(this);
			this.lastFiveOpenedCharms = new Queue<int>();
			this.lootpacksOpenedCount = new Dictionary<string, int>();
			this.specialOfferBoard = new SpecialOfferBoard();
			this.currentCurses = new List<int>();
			this.cursedRiftSlots = new CurseSlots(4)
			{
				lockedSlots = new List<SlotUnlockCondition>
				{
					new SlotUnlockCondition
					{
						riftIdToBeat = 300
					},
					new SlotUnlockCondition
					{
						riftIdToBeat = 450
					},
					new SlotUnlockCondition
					{
						riftIdToBeat = 600
					},
					new SlotUnlockCondition
					{
						riftIdToBeat = 750
					},
					new SlotUnlockCondition
					{
						riftIdToBeat = 950
					},
					new SlotUnlockCondition
					{
						riftIdToBeat = 1100
					}
				}
			};
			this.forceDropCandy = false;
			this.forceDropChestCandy = false;
			this.specialOfferBoard.Init();
		}

		private void InitWorlds()
		{
			this.worldStandard = new World();
			this.worldStandard.currentSim = this;
			ChallengeStandard challengeStandard = new ChallengeStandard();
			challengeStandard.Init(this.worldStandard);
			this.worldStandard.Init(this.universalTotalBonus, GameMode.STANDARD, challengeStandard, null);
			this.worldStandard.allChallenges = new List<Challenge>
			{
				challengeStandard
			};
			this.worldCrusade = new World();
			this.worldCrusade.currentSim = this;
			this.worldCrusade.allChallenges = new List<Challenge>
			{
				new TimeChallenge01(),
				new TimeChallenge02(),
				new TimeChallenge03(),
				new TimeChallenge04(),
				new TimeChallenge05(),
				new TimeChallenge06(),
				new TimeChallenge07(),
				new TimeChallenge08(),
				new TimeChallenge09(),
				new TimeChallenge10(),
				new TimeChallenge11(),
				new TimeChallenge12(),
				new TimeChallenge13(),
				new TimeChallenge14(),
				new TimeChallenge15(),
				new TimeChallenge16(),
				new TimeChallenge17(),
				new TimeChallenge18(),
				new TimeChallenge19(),
				new TimeChallenge20(),
				new TimeChallenge21(),
				new TimeChallenge22(),
				new TimeChallenge23(),
				new TimeChallenge24(),
				new TimeChallenge25(),
				new TimeChallenge26(),
				new TimeChallenge27(),
				new TimeChallenge28(),
				new TimeChallenge29(),
				new TimeChallenge30()
			};
			foreach (Challenge challenge in this.worldCrusade.allChallenges)
			{
				challenge.Init(this.worldCrusade);
			}
			Unlock unlockMode = new Unlock(UnlockIds.UNLOCK_TIME_CHALLENGE, this.worldStandard, new UnlockReqPrestigeAfterStage(120), new UnlockRewardGameModeCrusade());
			this.worldCrusade.Init(this.universalTotalBonus, GameMode.CRUSADE, this.worldCrusade.allChallenges[0], unlockMode);
			this.worldRift = new World();
			this.worldRift.currentSim = this;
			RiftFactory.FillRiftWorld(this.worldRift);
			this.worldRift.cursedChallenges = new List<Challenge>();
			foreach (Challenge challenge2 in this.worldRift.allChallenges)
			{
				ChallengeRift challengeRift = challenge2 as ChallengeRift;
				if (challengeRift.discoveryIndex > this.maxRiftDiscoveryIndex)
				{
					this.maxRiftDiscoveryIndex = challengeRift.discoveryIndex;
				}
			}
			Unlock unlockMode2 = new Unlock(UnlockIds.UNLOCK_RIFT, this.worldStandard, new UnlockReqPrestigeAfterStage(700), new UnlockRewardGameModeRift());
			this.worldRift.Init(this.universalTotalBonusRift, GameMode.RIFT, this.worldRift.allChallenges[0], unlockMode2);
			this.allWorlds = new List<World>
			{
				this.worldStandard,
				this.worldCrusade,
				this.worldRift
			};
		}

		private void InitData()
		{
			this.InitDataTotem();
			this.InitDataHero();
			this.InitCharms();
			this.InitCurses();
			this.InitDataUnlock();
			Simulator.InitAchievements();
		}

		private void InitDataTotem()
		{
			TotemDataBaseLightning totemDataBaseLightning = new TotemDataBaseLightning();
			totemDataBaseLightning.damage = 3.0;
			totemDataBaseLightning.critChance = 0.05f;
			totemDataBaseLightning.critFactor = 2.0;
			TotemDataBaseFire totemDataBaseFire = new TotemDataBaseFire();
			totemDataBaseFire.damage = 12.0;
			totemDataBaseFire.critChance = 0.05f;
			totemDataBaseFire.critFactor = 2.0;
			TotemDataBaseIce totemDataBaseIce = new TotemDataBaseIce();
			totemDataBaseIce.damage = 10.0;
			totemDataBaseIce.critChance = 0.05f;
			totemDataBaseIce.critFactor = 2.0;
			TotemDataBaseEarth totemDataBaseEarth = new TotemDataBaseEarth();
			totemDataBaseEarth.damage = 80.0;
			totemDataBaseEarth.critChance = 0.05f;
			totemDataBaseEarth.critFactor = 2.0;
			this.allTotems = new List<TotemDataBase>
			{
				totemDataBaseLightning,
				totemDataBaseFire,
				totemDataBaseIce,
				totemDataBaseEarth
			};
			this.boughtRunes = new List<Rune>();
			this.wornRunes = new Dictionary<string, List<Rune>>();
			foreach (TotemDataBase totemDataBase in this.allTotems)
			{
				this.wornRunes.Add(totemDataBase.id, new List<Rune>());
			}
			this.allRunes = new List<Rune>
			{
				new RuneCharge(totemDataBaseLightning),
				new RuneBounce(totemDataBaseLightning),
				new RuneShock(totemDataBaseLightning),
				new RuneZap(totemDataBaseLightning),
				new RuneOverload(totemDataBaseLightning),
				new RuneThunder(totemDataBaseLightning),
				new RuneEnergy(totemDataBaseLightning),
				new RuneDaze(totemDataBaseLightning),
				new RuneRash(totemDataBaseLightning),
				new RuneDischarge(totemDataBaseLightning),
				new RuneBlessing(totemDataBaseIce),
				new RuneSharpness(totemDataBaseIce),
				new RuneColdWind(totemDataBaseIce),
				new RuneIceBlast(totemDataBaseIce),
				new RuneStormer(totemDataBaseIce),
				new RuneIceRage(totemDataBaseIce),
				new RuneNova(totemDataBaseIce),
				new RuneShatter(totemDataBaseIce),
				new RuneBleak(totemDataBaseIce),
				new RuneGlacier(totemDataBaseIce),
				new RuneStinger(totemDataBaseIce),
				new RuneIgnition(totemDataBaseFire),
				new RuneFireResistance(totemDataBaseFire),
				new RuneCooler(totemDataBaseFire),
				new RuneInnerFire(totemDataBaseFire),
				new RuneEnchantedFire(totemDataBaseFire),
				new RuneMeltdown(totemDataBaseFire),
				new RuneMagma(totemDataBaseFire),
				new RuneExplosive(totemDataBaseFire),
				new RuneLuminosity(totemDataBaseFire),
				new RuneBlaze(totemDataBaseFire),
				new RuneHotWave(totemDataBaseFire),
				new RuneMeteorite(totemDataBaseEarth),
				new RuneSpiritual(totemDataBaseEarth),
				new RuneStarfall(totemDataBaseEarth),
				new RuneEnchant(totemDataBaseEarth),
				new RuneRecycle(totemDataBaseEarth),
				new RuneSmash(totemDataBaseEarth),
				new RuneWishful(totemDataBaseEarth),
				new RuneRemnants(totemDataBaseEarth),
				new RuneAbundance(totemDataBaseEarth)
			};
		}

		private void InitDataHero()
		{
			this.allHeroes = new List<HeroDataBase>
			{
				HeroFactory.CreateHoratio(this.allGears),
				HeroFactory.CreateThour(this.allGears),
				HeroFactory.CreateIda(this.allGears),
				HeroFactory.CreateKindLenny(this.allGears),
				HeroFactory.CreateSam(this.allGears),
				HeroFactory.CreateBomberman(this.allGears),
				HeroFactory.CreateSheela(this.allGears),
				HeroFactory.CreateDerek(this.allGears),
				HeroFactory.CreateBlindArcher(this.allGears),
				HeroFactory.CreateJim(this.allGears),
				HeroFactory.CreateTam(this.allGears),
				HeroFactory.CreateGoblin(this.allGears),
				HeroFactory.CreateBabu(this.allGears),
				HeroFactory.CreateWarlock(this.allGears),
				HeroFactory.CreateDruid(this.allGears)
			};
			foreach (HeroDataBase item in this.allHeroes)
			{
				HeroSkins.FillAllSkins(item, this);
			}
			AttachmentOffsets.Init<HeroDataBase>(this.allHeroes);
		}

		private void InitDataUnlock()
		{
			this.oldUnlocks = new List<Unlock>();
			Unlock item = new Unlock(UnlockIds.TRINKET_PACK_NON_USED, this.worldStandard, new UnlockReqReachStage(1000), new UnlockRewardTrinketPack());
			this.oldUnlocks.Add(item);
			Unlock item2 = new Unlock(UnlockIds.HERO_VEXX, this.worldStandard, new UnlockReqReachStage(6), new UnlockRewardHero("IDA", "HERO_NAME_VEXX"));
			Unlock item3 = new Unlock(UnlockIds.MERCHANT, this.worldStandard, new UnlockReqReachStage(10), new UnlockRewardMerchant());
			Unlock item4 = new Unlock(UnlockIds.STANDARD_TOKENS_00, this.worldStandard, new UnlockReqReachStage(15), new UnlockRewardCurrency(CurrencyType.TOKEN, 20.0));
			Unlock item5 = new Unlock(UnlockIds.HERO_LENNY, this.worldStandard, new UnlockReqReachStage(20), new UnlockRewardHero("KIND_LENNY", "HERO_NAME_KIND_LENNY"));
			Unlock item6 = new Unlock(UnlockIds.STANDARD_CREDITS_00, this.worldStandard, new UnlockReqReachStage(25), new UnlockRewardCurrency(CurrencyType.GEM, 100.0));
			Unlock item7 = new Unlock(UnlockIds.RUNE_CHARGE, this.worldStandard, new UnlockReqReachStage(30), new UnlockRewardRune(this.GetRune(RuneIds.LIGHTNING_CHARGE)));
			Unlock item8 = new Unlock(UnlockIds.PRESTIGE, this.worldStandard, new UnlockReqReachStage(80), new UnlockRewardPrestige(), true);
			Unlock item9 = new Unlock(UnlockIds.HERO_SAM, this.worldStandard, new UnlockReqReachStage(35), new UnlockRewardHero("SAM", "HERO_NAME_SAM"));
			Unlock item10 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_00, this.worldStandard, new UnlockReqReachStage(115), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 300.0));
			Unlock item11 = new Unlock(UnlockIds.MERCHANT_TAP, this.worldStandard, new UnlockReqReachStage(45), new UnlockRewardMerchantItem(this.worldStandard.merchantItems[1]));
			Unlock item12 = new Unlock(UnlockIds.STANDARD_CREDITS_01, this.worldStandard, new UnlockReqReachStage(50), new UnlockRewardCurrency(CurrencyType.GEM, 150.0));
			Unlock item13 = new Unlock(UnlockIds.RING_FIRE, this.worldStandard, new UnlockReqReachStage(130), new UnlockRewardTotem(this.allTotems[1]));
			Unlock item14 = new Unlock(UnlockIds.STANDARD_SCRAPS_00, this.worldStandard, new UnlockReqReachStage(65), new UnlockRewardCurrency(CurrencyType.SCRAP, 75.0));
			Unlock item15 = new Unlock(UnlockIds.COMPASS, this.worldStandard, new UnlockReqReachStage(90), new UnlockRewardCompass());
			Unlock item16 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_01, this.worldStandard, new UnlockReqReachStage(160), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 500.0));
			Unlock item17 = new Unlock(UnlockIds.HERO_V, this.worldStandard, new UnlockReqReachStage(80), new UnlockRewardHero("SHEELA", "HERO_NAME_V"));
			Unlock item18 = new Unlock(UnlockIds.STANDARD_SCRAPS_01, this.worldStandard, new UnlockReqReachStage(195), new UnlockRewardCurrency(CurrencyType.SCRAP, 150.0));
			Unlock item19 = new Unlock(UnlockIds.MERCHANT_WARP, this.worldStandard, new UnlockReqReachStage(175), new UnlockRewardMerchantItem(this.worldStandard.merchantItems[2]));
			Unlock item20 = new Unlock(UnlockIds.STANDARD_CREDITS_02, this.worldStandard, new UnlockReqReachStage(210), new UnlockRewardCurrency(CurrencyType.GEM, 250.0));
			Unlock item21 = new Unlock(UnlockIds.HERO_WENDLE, this.worldStandard, new UnlockReqReachStage(100), new UnlockRewardHero("DEREK", "HERO_NAME_WENDLE"));
			Unlock item22 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_02, this.worldStandard, new UnlockReqReachStage(225), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 5000.0));
			Unlock item23 = new Unlock(UnlockIds.RING_ICE, this.worldStandard, new UnlockReqReachStage(300), new UnlockRewardTotem(this.allTotems[2]));
			Unlock item24 = new Unlock(UnlockIds.STANDARD_TOKENS_01, this.worldStandard, new UnlockReqReachStage(260), new UnlockRewardCurrency(CurrencyType.TOKEN, 30.0));
			Unlock item25 = new Unlock(UnlockIds.HERO_LIA, this.worldStandard, new UnlockReqReachStage(140), new UnlockRewardHero("BLIND_ARCHER", "HERO_NAME_LIA"));
			Unlock item26 = new Unlock(UnlockIds.STANDARD_SCRAPS_02, this.worldStandard, new UnlockReqReachStage(275), new UnlockRewardCurrency(CurrencyType.SCRAP, 200.0));
			Unlock item27 = new Unlock(UnlockIds.RUNE_COOLER, this.worldStandard, new UnlockReqReachStage(315), new UnlockRewardRune(this.GetRune(RuneIds.FIRE_COOLER)));
			Unlock item28 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_03, this.worldStandard, new UnlockReqReachStage(330), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 20000.0));
			Unlock item29 = new Unlock(UnlockIds.RUNE_ICE_STORMER, this.worldStandard, new UnlockReqReachStage(350), new UnlockRewardRune(this.GetRune(RuneIds.ICE_STORMER)));
			Unlock item30 = new Unlock(UnlockIds.STANDARD_CREDITS_03, this.worldStandard, new UnlockReqReachStage(365), new UnlockRewardCurrency(CurrencyType.GEM, 300.0));
			UnlockRewardTrinketSlots unlockRewardTrinketSlots = new UnlockRewardTrinketSlots(10);
			unlockRewardTrinketSlots.category = UnlockReward.RewardCategory.NEW_MECHANIC;
			Unlock item31 = new Unlock(UnlockIds.TRINKET_SLOT_00, this.worldStandard, new UnlockReqReachStage(380), unlockRewardTrinketSlots);
			Unlock item32 = new Unlock(UnlockIds.TRINKET_PACK_00, this.worldStandard, new UnlockReqReachStage(381), new UnlockRewardTrinketPack());
			Unlock item33 = new Unlock(UnlockIds.STANDARD_TOKENS_02, this.worldStandard, new UnlockReqReachStage(385), new UnlockRewardCurrency(CurrencyType.TOKEN, 50.0));
			Unlock item34 = new Unlock(UnlockIds.RUNE_ENERGY, this.worldStandard, new UnlockReqReachStage(395), new UnlockRewardRune(this.GetRune(RuneIds.LIGHTNING_ENERGY)));
			Unlock item35 = new Unlock(UnlockIds.TRINKET_PACK_01, this.worldStandard, new UnlockReqReachStage(400), new UnlockRewardTrinketPack());
			Unlock item36 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_04, this.worldStandard, new UnlockReqReachStage(430), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 200000.0));
			Unlock item37 = new Unlock(UnlockIds.HERO_JIM, this.worldStandard, new UnlockReqReachStage(245), new UnlockRewardHero("JIM", "HERO_NAME_HANDSUM_JIM"));
			Unlock item38 = new Unlock(UnlockIds.STANDARD_SCRAPS_03, this.worldStandard, new UnlockReqReachStage(450), new UnlockRewardCurrency(CurrencyType.SCRAP, 250.0));
			Unlock item39 = new Unlock(UnlockIds.DAILIES, this.worldStandard, new UnlockReqReachStage(465), new UnlockRewardDailies());
			Unlock item40 = new Unlock(UnlockIds.RUNE_IGNITION, this.worldStandard, new UnlockReqReachStage(475), new UnlockRewardRune(this.GetRune(RuneIds.FIRE_IGNITION)));
			Unlock item41 = new Unlock(UnlockIds.MINE_SCRAP, this.worldStandard, new UnlockReqReachStage(485), new UnlockRewardMineScrap());
			Unlock item42 = new Unlock(UnlockIds.STANDARD_CREDITS_04, this.worldStandard, new UnlockReqReachStage(500), new UnlockRewardCurrency(CurrencyType.GEM, 400.0));
			Unlock item43 = new Unlock(UnlockIds.RING_EARTH, this.worldStandard, new UnlockReqReachStage(520), new UnlockRewardTotem(this.allTotems[3]));
			Unlock item44 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_05, this.worldStandard, new UnlockReqReachStage(510), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 1000000.0));
			Unlock item45 = new Unlock(UnlockIds.TRINKET_SLOT_01, this.worldStandard, new UnlockReqReachStage(525), new UnlockRewardTrinketSlots(3));
			Unlock item46 = new Unlock(UnlockIds.TRINKET_PACK_02, this.worldStandard, new UnlockReqReachStage(530), new UnlockRewardTrinketPack());
			Unlock item47 = new Unlock(UnlockIds.RUNE_ICE_RAGE, this.worldStandard, new UnlockReqReachStage(540), new UnlockRewardRune(this.GetRune(RuneIds.ICE_ICE_RAGE)));
			Unlock item48 = new Unlock(UnlockIds.RUNE_WISHFUL, this.worldStandard, new UnlockReqReachStage(550), new UnlockRewardRune(this.GetRune(RuneIds.EARTH_WISHFUL)));
			Unlock item49 = new Unlock(UnlockIds.STANDARD_SCRAPS_04, this.worldStandard, new UnlockReqReachStage(560), new UnlockRewardCurrency(CurrencyType.SCRAP, 500.0));
			Unlock item50 = new Unlock(UnlockIds.RUNE_THUNDER, this.worldStandard, new UnlockReqReachStage(570), new UnlockRewardRune(this.GetRune(RuneIds.LIGHTNING_THUNDER)));
			UnlockRewardMythicalArtifactSlot unlockRewardMythicalArtifactSlot = new UnlockRewardMythicalArtifactSlot(1);
			unlockRewardMythicalArtifactSlot.defaultRewardCategory = UnlockReward.RewardCategory.NEW_MECHANIC;
			Unlock item51 = new Unlock(UnlockIds.MYTHICAL_SLOT_00, this.worldStandard, new UnlockReqReachStage(585), unlockRewardMythicalArtifactSlot);
			Unlock item52 = new Unlock(UnlockIds.STANDARD_CREDITS_05, this.worldStandard, new UnlockReqReachStage(695), new UnlockRewardCurrency(CurrencyType.GEM, 400.0));
			Unlock item53 = new Unlock(UnlockIds.RUNE_SPIRITUAL, this.worldStandard, new UnlockReqReachStage(610), new UnlockRewardRune(this.GetRune(RuneIds.EARTH_SPIRITUAL)));
			Unlock item54 = new Unlock(UnlockIds.RUNE_FIRE_MAGMA, this.worldStandard, new UnlockReqReachStage(630), new UnlockRewardRune(this.GetRune(RuneIds.FIRE_MAGMA)));
			Unlock item55 = new Unlock(UnlockIds.HERO_TAM, this.worldStandard, new UnlockReqReachStage(415), new UnlockRewardHero("TAM", "HERO_NAME_TAM"));
			Unlock item56 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_06, this.worldStandard, new UnlockReqReachStage(620), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 10000000.0));
			Unlock item57 = new Unlock(UnlockIds.TRINKET_SLOT_02, this.worldStandard, new UnlockReqReachStage(655), new UnlockRewardTrinketSlots(3));
			Unlock item58 = new Unlock(UnlockIds.RUNE_SHARPNESS, this.worldStandard, new UnlockReqReachStage(665), new UnlockRewardRune(this.GetRune(RuneIds.ICE_SHARPNESS)));
			Unlock item59 = new Unlock(UnlockIds.TRINKET_PACK_03, this.worldStandard, new UnlockReqReachStage(680), new UnlockRewardTrinketPack());
			Unlock item60 = new Unlock(UnlockIds.MYTHICAL_SLOT_01, this.worldStandard, new UnlockReqReachStage(700), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item61 = new Unlock(UnlockIds.STANDARD_TOKENS_03, this.worldStandard, new UnlockReqReachStage(720), new UnlockRewardCurrency(CurrencyType.TOKEN, 150.0));
			Unlock item62 = new Unlock(UnlockIds.TRINKET_PACK_04, this.worldStandard, new UnlockReqReachStage(730), new UnlockRewardTrinketPack());
			Unlock item63 = new Unlock(UnlockIds.STANDARD_CREDITS_06, this.worldStandard, new UnlockReqReachStage(740), new UnlockRewardCurrency(CurrencyType.GEM, 500.0));
			Unlock item64 = new Unlock(UnlockIds.MERCHANT_SHIELD, this.worldStandard, new UnlockReqReachStage(765), new UnlockRewardMerchantItem(this.worldStandard.merchantItems[3]));
			Unlock item65 = new Unlock(604u, this.worldStandard, new UnlockReqReachStage(755), new UnlockRewardSkillPointsAutoDistribution());
			Unlock item66 = new Unlock(UnlockIds.STANDARD_TOKENS_04, this.worldStandard, new UnlockReqReachStage(770), new UnlockRewardCurrency(CurrencyType.TOKEN, 150.0));
			Unlock item67 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_07, this.worldStandard, new UnlockReqReachStage(775), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 150000000.0));
			Unlock item68 = new Unlock(UnlockIds.MINE_TOKEN, this.worldStandard, new UnlockReqReachStage(795), new UnlockRewardMineToken());
			Unlock item69 = new Unlock(UnlockIds.MYTHICAL_SLOT_02, this.worldStandard, new UnlockReqReachStage(805), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item70 = new Unlock(UnlockIds.RUNE_STARFALL, this.worldStandard, new UnlockReqReachStage(820), new UnlockRewardRune(this.GetRune(RuneIds.EARTH_STARFALL)));
			Unlock item71 = new Unlock(UnlockIds.STANDARD_CREDITS_07, this.worldStandard, new UnlockReqReachStage(830), new UnlockRewardCurrency(CurrencyType.GEM, 150.0));
			Unlock item72 = new Unlock(UnlockIds.MYTHICAL_SLOT_03, this.worldStandard, new UnlockReqReachStage(845), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item73 = new Unlock(UnlockIds.RUNE_RASH, this.worldStandard, new UnlockReqReachStage(855), new UnlockRewardRune(this.GetRune(RuneIds.LIGHTNING_RASH)));
			Unlock item74 = new Unlock(UnlockIds.STANDARD_SCRAPS_05, this.worldStandard, new UnlockReqReachStage(865), new UnlockRewardCurrency(CurrencyType.SCRAP, 150.0));
			Unlock item75 = new Unlock(UnlockIds.HERO_GOBLIN, this.worldStandard, new UnlockReqReachStage(645), new UnlockRewardHero("GOBLIN", "HERO_NAME_GOBLIN"));
			Unlock item76 = new Unlock(UnlockIds.MERCHANT_GOLD_BONUS, this.worldStandard, new UnlockReqReachStage(875), new UnlockRewardMerchantItem(this.worldStandard.merchantItems[4]));
			Unlock item77 = new Unlock(UnlockIds.STANDARD_TOKENS_05, this.worldStandard, new UnlockReqReachStage(880), new UnlockRewardCurrency(CurrencyType.TOKEN, 150.0));
			Unlock item78 = new Unlock(UnlockIds.TRINKET_SLOT_03, this.worldStandard, new UnlockReqReachStage(885), new UnlockRewardTrinketSlots(3));
			Unlock item79 = new Unlock(UnlockIds.MYTHICAL_SLOT_04, this.worldStandard, new UnlockReqReachStage(915), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item80 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_08, this.worldStandard, new UnlockReqReachStage(930), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 2000000000.0));
			Unlock item81 = new Unlock(UnlockIds.STANDARD_CREDITS_08, this.worldStandard, new UnlockReqReachStage(950), new UnlockRewardCurrency(CurrencyType.GEM, 150.0));
			Unlock item82 = new Unlock(112u, this.worldStandard, new UnlockReqReachStage(900), new UnlockRewardHero("BABU", "HERO_NAME_BABU"));
			Unlock item83 = new Unlock(UnlockIds.MYTHICAL_SLOT_05, this.worldStandard, new UnlockReqReachStage(985), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item84 = new Unlock(UnlockIds.STANDARD_SCRAPS_06, this.worldStandard, new UnlockReqReachStage(1015), new UnlockRewardCurrency(CurrencyType.SCRAP, 200.0));
			Unlock item85 = new Unlock(UnlockIds.TRINKET_DISASSEMBLE, this.worldStandard, new UnlockReqReachStage(1000), new UnlockRewardTrinketEnch());
			Unlock item86 = new Unlock(UnlockIds.MYTHICAL_SLOT_06, this.worldStandard, new UnlockReqReachStage(1040), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item87 = new Unlock(UnlockIds.RUNE_BLAZE, this.worldStandard, new UnlockReqReachStage(965), new UnlockRewardRune(this.GetRune(RuneIds.FIRE_BLAZE)));
			Unlock item88 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_09, this.worldStandard, new UnlockReqReachStage(1060), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 20000000000.0));
			Unlock item89 = new Unlock(UnlockIds.HERO_WARLOCK, this.worldStandard, new UnlockReqReachStage(1210), new UnlockRewardHero("WARLOCK", "HERO_NAME_WARLOCK"));
			Unlock item90 = new Unlock(UnlockIds.TRINKET_SLOT_04, this.worldStandard, new UnlockReqReachStage(1080), new UnlockRewardTrinketSlots(3));
			Unlock item91 = new Unlock(UnlockIds.MYTHICAL_SLOT_07, this.worldStandard, new UnlockReqReachStage(1105), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item92 = new Unlock(UnlockIds.STANDARD_CREDITS_09, this.worldStandard, new UnlockReqReachStage(1120), new UnlockRewardCurrency(CurrencyType.GEM, 150.0));
			Unlock item93 = new Unlock(UnlockIds.MYTHICAL_SLOT_08, this.worldStandard, new UnlockReqReachStage(1145), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item94 = new Unlock(UnlockIds.TRINKET_SLOT_05, this.worldStandard, new UnlockReqReachStage(1155), new UnlockRewardTrinketSlots(4));
			Unlock item95 = new Unlock(UnlockIds.MYTHICAL_SLOT_09, this.worldStandard, new UnlockReqReachStage(1175), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item96 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_10, this.worldStandard, new UnlockReqReachStage(1190), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 200000000000.0));
			Unlock item97 = new Unlock(UnlockIds.MYTHICAL_SLOT_10, this.worldStandard, new UnlockReqReachStage(1225), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item98 = new Unlock(UnlockIds.TRINKET_SLOT_06, this.worldStandard, new UnlockReqReachStage(1245), new UnlockRewardTrinketSlots(4));
			Unlock item99 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_11, this.worldStandard, new UnlockReqReachStage(1260), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 600000000000.0));
			Unlock item100 = new Unlock(UnlockIds.MYTHICAL_SLOT_11, this.worldStandard, new UnlockReqReachStage(1280), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item101 = new Unlock(UnlockIds.STANDARD_SCRAPS_07, this.worldStandard, new UnlockReqReachStage(1290), new UnlockRewardCurrency(CurrencyType.SCRAP, 250.0));
			Unlock item102 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_12, this.worldStandard, new UnlockReqReachStage(1300), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 1500000000000.0));
			Unlock item103 = new Unlock(UnlockIds.MERCHANT_WAVE_CLEAR, this.worldStandard, new UnlockReqReachStage(1310), new UnlockRewardMerchantItem(this.worldStandard.merchantItems[5]));
			Unlock item104 = new Unlock(UnlockIds.STANDARD_TOKENS_06, this.worldStandard, new UnlockReqReachStage(1320), new UnlockRewardCurrency(CurrencyType.TOKEN, 200.0));
			Unlock item105 = new Unlock(UnlockIds.MYTHICAL_SLOT_12, this.worldStandard, new UnlockReqReachStage(1340), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item106 = new Unlock(113u, this.worldStandard, new UnlockReqReachStage(1500), new UnlockRewardHero("DRUID", "HERO_NAME_RON"));
			Unlock item107 = new Unlock(UnlockIds.RUNE_GLACIER, this.worldStandard, new UnlockReqReachStage(1355), new UnlockRewardRune(this.GetRune(RuneIds.ICE_GLACIER)));
			Unlock item108 = new Unlock(UnlockIds.STANDARD_CREDITS_10, this.worldStandard, new UnlockReqReachStage(1370), new UnlockRewardCurrency(CurrencyType.GEM, 250.0));
			Unlock item109 = new Unlock(UnlockIds.MYTHICAL_SLOT_13, this.worldStandard, new UnlockReqReachStage(1405), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item110 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_13, this.worldStandard, new UnlockReqReachStage(1390), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 6000000000000.0));
			Unlock item111 = new Unlock(UnlockIds.MYTHICAL_SLOT_14, this.worldStandard, new UnlockReqReachStage(1450), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item112 = new Unlock(UnlockIds.STANDARD_SCRAPS_08, this.worldStandard, new UnlockReqReachStage(1425), new UnlockRewardCurrency(CurrencyType.SCRAP, 2000.0));
			Unlock item113 = new Unlock(UnlockIds.STANDARD_TOKENS_07, this.worldStandard, new UnlockReqReachStage(1465), new UnlockRewardCurrency(CurrencyType.TOKEN, 300.0));
			Unlock item114 = new Unlock(UnlockIds.STANDARD_CREDITS_11, this.worldStandard, new UnlockReqReachStage(1480), new UnlockRewardCurrency(CurrencyType.GEM, 500.0));
			Unlock item115 = new Unlock(UnlockIds.MYTHICAL_SLOT_15, this.worldStandard, new UnlockReqReachStage(1510), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item116 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_14, this.worldStandard, new UnlockReqReachStage(1515), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 50000000000000.0));
			Unlock item117 = new Unlock(UnlockIds.STANDARD_SCRAPS_09, this.worldStandard, new UnlockReqReachStage(1525), new UnlockRewardCurrency(CurrencyType.SCRAP, 2500.0));
			Unlock item118 = new Unlock(UnlockIds.STANDARD_TOKENS_08, this.worldStandard, new UnlockReqReachStage(1545), new UnlockRewardCurrency(CurrencyType.TOKEN, 500.0));
			Unlock item119 = new Unlock(UnlockIds.MYTHICAL_SLOT_16, this.worldStandard, new UnlockReqReachStage(1560), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item120 = new Unlock(UnlockIds.STANDARD_CREDITS_12, this.worldStandard, new UnlockReqReachStage(1570), new UnlockRewardCurrency(CurrencyType.GEM, 500.0));
			Unlock item121 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_15, this.worldStandard, new UnlockReqReachStage(1580), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 150000000000000.0));
			Unlock item122 = new Unlock(UnlockIds.STANDARD_SCRAPS_10, this.worldStandard, new UnlockReqReachStage(1585), new UnlockRewardCurrency(CurrencyType.SCRAP, 3000.0));
			Unlock item123 = new Unlock(UnlockIds.MYTHICAL_SLOT_17, this.worldStandard, new UnlockReqReachStage(1605), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item124 = new Unlock(UnlockIds.STANDARD_TOKENS_09, this.worldStandard, new UnlockReqReachStage(1620), new UnlockRewardCurrency(CurrencyType.TOKEN, 500.0));
			Unlock item125 = new Unlock(UnlockIds.STANDARD_CREDITS_13, this.worldStandard, new UnlockReqReachStage(1630), new UnlockRewardCurrency(CurrencyType.GEM, 500.0));
			Unlock item126 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_16, this.worldStandard, new UnlockReqReachStage(1645), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 750000000000000.0));
			Unlock item127 = new Unlock(UnlockIds.TRINKET_SLOT_07, this.worldStandard, new UnlockReqReachStage(1655), new UnlockRewardTrinketSlots(3));
			Unlock item128 = new Unlock(UnlockIds.STANDARD_TOKENS_10, this.worldStandard, new UnlockReqReachStage(1675), new UnlockRewardCurrency(CurrencyType.TOKEN, 500.0));
			Unlock item129 = new Unlock(UnlockIds.STANDARD_CREDITS_14, this.worldStandard, new UnlockReqReachStage(1705), new UnlockRewardCurrency(CurrencyType.GEM, 500.0));
			Unlock item130 = new Unlock(UnlockIds.TRINKET_PACK_05, this.worldStandard, new UnlockReqReachStage(1715), new UnlockRewardTrinketPack());
			Unlock item131 = new Unlock(UnlockIds.TRINKET_SLOT_08, this.worldStandard, new UnlockReqReachStage(1735), new UnlockRewardTrinketSlots(3));
			Unlock item132 = new Unlock(UnlockIds.STANDARD_TOKENS_11, this.worldStandard, new UnlockReqReachStage(1765), new UnlockRewardCurrency(CurrencyType.TOKEN, 500.0));
			Unlock item133 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_17, this.worldStandard, new UnlockReqReachStage(1815), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 7.5E+15));
			Unlock item134 = new Unlock(UnlockIds.STANDARD_SCRAPS_13, this.worldStandard, new UnlockReqReachStage(1845), new UnlockRewardCurrency(CurrencyType.SCRAP, 2000.0));
			Unlock item135 = new Unlock(UnlockIds.STANDARD_TOKENS_12, this.worldStandard, new UnlockReqReachStage(1875), new UnlockRewardCurrency(CurrencyType.TOKEN, 3000.0));
			Unlock item136 = new Unlock(UnlockIds.STANDARD_CREDITS_17, this.worldStandard, new UnlockReqReachStage(1915), new UnlockRewardCurrency(CurrencyType.GEM, 500.0));
			Unlock item137 = new Unlock(UnlockIds.STANDARD_SCRAPS_12, this.worldStandard, new UnlockReqReachStage(1995), new UnlockRewardCurrency(CurrencyType.SCRAP, 2500.0));
			Unlock item138 = new Unlock(UnlockIds.STANDARD_CREDITS_15, this.worldStandard, new UnlockReqReachStage(2045), new UnlockRewardCurrency(CurrencyType.GEM, 500.0));
			Unlock item139 = new Unlock(UnlockIds.STANDARD_CREDITS_16, this.worldStandard, new UnlockReqReachStage(2175), new UnlockRewardCurrency(CurrencyType.GEM, 500.0));
			Unlock item140 = new Unlock(UnlockIds.MYTHICAL_SLOT_18, this.worldStandard, new UnlockReqReachStage(1900), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item141 = new Unlock(UnlockIds.MYTHICAL_SLOT_19, this.worldStandard, new UnlockReqReachStage(2200), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item142 = new Unlock(UnlockIds.MYTHICAL_SLOT_20, this.worldStandard, new UnlockReqReachStage(2500), new UnlockRewardMythicalArtifactSlot(1));
			Unlock item143 = new Unlock(UnlockIds.STANDARD_SCRAPS_14, this.worldStandard, new UnlockReqReachStage(2225), new UnlockRewardCurrency(CurrencyType.SCRAP, 500.0));
			Unlock item144 = new Unlock(UnlockIds.STANDARD_SCRAPS_15, this.worldStandard, new UnlockReqReachStage(2400), new UnlockRewardCurrency(CurrencyType.SCRAP, 500.0));
			Unlock item145 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_18, this.worldStandard, new UnlockReqReachStage(2275), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 4.5E+18));
			Unlock item146 = new Unlock(UnlockIds.STANDARD_TOKENS_13, this.worldStandard, new UnlockReqReachStage(2325), new UnlockRewardCurrency(CurrencyType.TOKEN, 500.0));
			Unlock item147 = new Unlock(UnlockIds.STANDARD_CREDITS_18, this.worldStandard, new UnlockReqReachStage(2600), new UnlockRewardCurrency(CurrencyType.GEM, 500.0));
			Unlock item148 = new Unlock(UnlockIds.STANDARD_CREDITS_19, this.worldStandard, new UnlockReqReachStage(3000), new UnlockRewardCurrency(CurrencyType.GEM, 250.0));
			Unlock item149 = new Unlock(UnlockIds.STANDARD_SCRAPS_16, this.worldStandard, new UnlockReqReachStage(3200), new UnlockRewardCurrency(CurrencyType.SCRAP, 500.0));
			Unlock item150 = new Unlock(UnlockIds.STANDARD_SCRAPS_02_1, this.worldStandard, new UnlockReqReachStage(375), new UnlockRewardCurrency(CurrencyType.SCRAP, 200.0));
			Unlock item151 = new Unlock(UnlockIds.STANDARD_TOKENS_02_1, this.worldStandard, new UnlockReqReachStage(490), new UnlockRewardCurrency(CurrencyType.TOKEN, 50.0));
			Unlock item152 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_05_1, this.worldStandard, new UnlockReqReachStage(595), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 5000000.0));
			Unlock item153 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_06_1, this.worldStandard, new UnlockReqReachStage(705), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 50000000.0));
			Unlock item154 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_07_1, this.worldStandard, new UnlockReqReachStage(815), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 250000000.0));
			Unlock item155 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_07_2, this.worldStandard, new UnlockReqReachStage(870), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 500000000.0));
			Unlock item156 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_08_1, this.worldStandard, new UnlockReqReachStage(990), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 5000000000.0));
			Unlock item157 = new Unlock(UnlockIds.STANDARD_SCRAPS_06_1, this.worldStandard, new UnlockReqReachStage(1070), new UnlockRewardCurrency(CurrencyType.SCRAP, 200.0));
			Unlock item158 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_09_1, this.worldStandard, new UnlockReqReachStage(1130), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 50000000000.0));
			Unlock item159 = new Unlock(UnlockIds.STANDARD_SCRAPS_06_2, this.worldStandard, new UnlockReqReachStage(1235), new UnlockRewardCurrency(CurrencyType.SCRAP, 200.0));
			Unlock item160 = new Unlock(UnlockIds.STANDARD_TOKENS_06_1, this.worldStandard, new UnlockReqReachStage(1380), new UnlockRewardCurrency(CurrencyType.TOKEN, 200.0));
			Unlock item161 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_13_1, this.worldStandard, new UnlockReqReachStage(1470), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 20000000000000.0));
			Unlock item162 = new Unlock(UnlockIds.STANDARD_CREDITS_12_1, this.worldStandard, new UnlockReqReachStage(1595), new UnlockRewardCurrency(CurrencyType.GEM, 150.0));
			Unlock item163 = new Unlock(UnlockIds.STANDARD_MYTHSTONES_16_1, this.worldStandard, new UnlockReqReachStage(1685), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 1E+15));
			Unlock item164 = new Unlock(UnlockIds.STANDARD_SCRAPS_10_1, this.worldStandard, new UnlockReqReachStage(1780), new UnlockRewardCurrency(CurrencyType.SCRAP, 250.0));
			Simulator.unlockCompass = item15;
			this.worldStandard.unlocks.Add(item3);
			this.worldStandard.unlocks.Add(item2);
			this.worldStandard.unlocks.Add(item5);
			this.worldStandard.unlocks.Add(item21);
			this.worldStandard.unlocks.Add(item9);
			this.worldStandard.unlocks.Add(item17);
			this.worldStandard.unlocks.Add(item25);
			this.worldStandard.unlocks.Add(item37);
			this.worldStandard.unlocks.Add(item55);
			this.worldStandard.unlocks.Add(item89);
			this.worldStandard.unlocks.Add(item75);
			this.worldStandard.unlocks.Add(item82);
			this.worldStandard.unlocks.Add(item106);
			this.worldStandard.unlocks.Add(item14);
			this.worldStandard.unlocks.Add(item18);
			this.worldStandard.unlocks.Add(item26);
			this.worldStandard.unlocks.Add(item38);
			this.worldStandard.unlocks.Add(item49);
			this.worldStandard.unlocks.Add(item74);
			this.worldStandard.unlocks.Add(item84);
			this.worldStandard.unlocks.Add(item101);
			this.worldStandard.unlocks.Add(item112);
			this.worldStandard.unlocks.Add(item117);
			this.worldStandard.unlocks.Add(item122);
			this.worldStandard.unlocks.Add(item137);
			this.worldStandard.unlocks.Add(item134);
			this.worldStandard.unlocks.Add(item143);
			this.worldStandard.unlocks.Add(item144);
			this.worldStandard.unlocks.Add(item149);
			this.worldStandard.unlocks.Add(item150);
			this.worldStandard.unlocks.Add(item157);
			this.worldStandard.unlocks.Add(item159);
			this.worldStandard.unlocks.Add(item164);
			this.worldStandard.unlocks.Add(item4);
			this.worldStandard.unlocks.Add(item24);
			this.worldStandard.unlocks.Add(item33);
			this.worldStandard.unlocks.Add(item61);
			this.worldStandard.unlocks.Add(item66);
			this.worldStandard.unlocks.Add(item77);
			this.worldStandard.unlocks.Add(item104);
			this.worldStandard.unlocks.Add(item113);
			this.worldStandard.unlocks.Add(item118);
			this.worldStandard.unlocks.Add(item124);
			this.worldStandard.unlocks.Add(item128);
			this.worldStandard.unlocks.Add(item132);
			this.worldStandard.unlocks.Add(item135);
			this.worldStandard.unlocks.Add(item146);
			this.worldStandard.unlocks.Add(item151);
			this.worldStandard.unlocks.Add(item160);
			this.worldStandard.unlocks.Add(item6);
			this.worldStandard.unlocks.Add(item12);
			this.worldStandard.unlocks.Add(item20);
			this.worldStandard.unlocks.Add(item30);
			this.worldStandard.unlocks.Add(item42);
			this.worldStandard.unlocks.Add(item52);
			this.worldStandard.unlocks.Add(item63);
			this.worldStandard.unlocks.Add(item71);
			this.worldStandard.unlocks.Add(item81);
			this.worldStandard.unlocks.Add(item92);
			this.worldStandard.unlocks.Add(item108);
			this.worldStandard.unlocks.Add(item114);
			this.worldStandard.unlocks.Add(item120);
			this.worldStandard.unlocks.Add(item125);
			this.worldStandard.unlocks.Add(item129);
			this.worldStandard.unlocks.Add(item138);
			this.worldStandard.unlocks.Add(item139);
			this.worldStandard.unlocks.Add(item136);
			this.worldStandard.unlocks.Add(item147);
			this.worldStandard.unlocks.Add(item148);
			this.worldStandard.unlocks.Add(item162);
			this.worldStandard.unlocks.Add(item10);
			this.worldStandard.unlocks.Add(item16);
			this.worldStandard.unlocks.Add(item22);
			this.worldStandard.unlocks.Add(item28);
			this.worldStandard.unlocks.Add(item36);
			this.worldStandard.unlocks.Add(item44);
			this.worldStandard.unlocks.Add(item56);
			this.worldStandard.unlocks.Add(item67);
			this.worldStandard.unlocks.Add(item80);
			this.worldStandard.unlocks.Add(item88);
			this.worldStandard.unlocks.Add(item96);
			this.worldStandard.unlocks.Add(item99);
			this.worldStandard.unlocks.Add(item102);
			this.worldStandard.unlocks.Add(item110);
			this.worldStandard.unlocks.Add(item116);
			this.worldStandard.unlocks.Add(item121);
			this.worldStandard.unlocks.Add(item126);
			this.worldStandard.unlocks.Add(item133);
			this.worldStandard.unlocks.Add(item145);
			this.worldStandard.unlocks.Add(item152);
			this.worldStandard.unlocks.Add(item153);
			this.worldStandard.unlocks.Add(item154);
			this.worldStandard.unlocks.Add(item155);
			this.worldStandard.unlocks.Add(item156);
			this.worldStandard.unlocks.Add(item158);
			this.worldStandard.unlocks.Add(item161);
			this.worldStandard.unlocks.Add(item163);
			this.worldStandard.unlocks.Add(item15);
			this.worldStandard.unlocks.Add(item65);
			this.worldStandard.unlocks.Add(item7);
			this.worldStandard.unlocks.Add(item34);
			this.worldStandard.unlocks.Add(item27);
			this.worldStandard.unlocks.Add(item29);
			this.worldStandard.unlocks.Add(item40);
			this.worldStandard.unlocks.Add(item47);
			this.worldStandard.unlocks.Add(item50);
			this.worldStandard.unlocks.Add(item54);
			this.worldStandard.unlocks.Add(item58);
			this.worldStandard.unlocks.Add(item73);
			this.worldStandard.unlocks.Add(item87);
			this.worldStandard.unlocks.Add(item107);
			this.worldStandard.unlocks.Add(item48);
			this.worldStandard.unlocks.Add(item53);
			this.worldStandard.unlocks.Add(item70);
			this.worldStandard.unlocks.Add(item13);
			this.worldStandard.unlocks.Add(item23);
			this.worldStandard.unlocks.Add(item43);
			this.worldStandard.unlocks.Add(item11);
			this.worldStandard.unlocks.Add(item19);
			this.worldStandard.unlocks.Add(item64);
			this.worldStandard.unlocks.Add(item76);
			this.worldStandard.unlocks.Add(item103);
			this.worldStandard.unlocks.Add(item51);
			this.worldStandard.unlocks.Add(item60);
			this.worldStandard.unlocks.Add(item69);
			this.worldStandard.unlocks.Add(item72);
			this.worldStandard.unlocks.Add(item79);
			this.worldStandard.unlocks.Add(item83);
			this.worldStandard.unlocks.Add(item86);
			this.worldStandard.unlocks.Add(item91);
			this.worldStandard.unlocks.Add(item93);
			this.worldStandard.unlocks.Add(item95);
			this.worldStandard.unlocks.Add(item97);
			this.worldStandard.unlocks.Add(item100);
			this.worldStandard.unlocks.Add(item105);
			this.worldStandard.unlocks.Add(item109);
			this.worldStandard.unlocks.Add(item111);
			this.worldStandard.unlocks.Add(item115);
			this.worldStandard.unlocks.Add(item119);
			this.worldStandard.unlocks.Add(item123);
			this.worldStandard.unlocks.Add(item140);
			this.worldStandard.unlocks.Add(item141);
			this.worldStandard.unlocks.Add(item142);
			this.worldStandard.unlocks.Add(item31);
			this.worldStandard.unlocks.Add(item45);
			this.worldStandard.unlocks.Add(item57);
			this.worldStandard.unlocks.Add(item78);
			this.worldStandard.unlocks.Add(item90);
			this.worldStandard.unlocks.Add(item94);
			this.worldStandard.unlocks.Add(item98);
			this.worldStandard.unlocks.Add(item127);
			this.worldStandard.unlocks.Add(item131);
			this.worldStandard.unlocks.Add(item32);
			this.worldStandard.unlocks.Add(item35);
			this.worldStandard.unlocks.Add(item46);
			this.worldStandard.unlocks.Add(item59);
			this.worldStandard.unlocks.Add(item62);
			this.worldStandard.unlocks.Add(item130);
			this.worldStandard.unlocks.Add(item68);
			this.worldStandard.unlocks.Add(item41);
			this.worldStandard.unlocks.Add(item39);
			this.worldStandard.unlocks.Add(item8);
			this.worldStandard.unlocks.Add(item85);
			this.worldStandard.unlocks.Sort((Unlock first, Unlock second) => first.GetReqAmount() - second.GetReqAmount());
			int num = 0;
			ChallengeWithTime challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock = new Unlock(UnlockIds.TC_CREDITS_00, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.GEM, 50.0));
			challengeWithTime.unlock = unlock;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock2 = new Unlock(UnlockIds.TC_TOKENS_00, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.TOKEN, 30.0));
			challengeWithTime.unlock = unlock2;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock3 = new Unlock(UnlockIds.HERO_BOOMER_BADLAD, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardHero("BOMBERMAN", "HERO_NAME_BOOMER_BADLAD"));
			challengeWithTime.unlock = unlock3;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock4 = new Unlock(UnlockIds.TC_MYTHSTONES_00, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 1500.0));
			challengeWithTime.unlock = unlock4;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock5 = new Unlock(UnlockIds.MERCHANT_REFRESHER, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardMerchantItem(this.worldCrusade.merchantItems[1]));
			challengeWithTime.unlock = unlock5;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock6 = new Unlock(UnlockIds.TC_SCRAPS_00, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.SCRAP, 100.0));
			challengeWithTime.unlock = unlock6;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock7 = new Unlock(UnlockIds.TC_CREDITS_01, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.GEM, 250.0));
			challengeWithTime.unlock = unlock7;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock8 = new Unlock(UnlockIds.TC_TOKENS_01, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.TOKEN, 40.0));
			challengeWithTime.unlock = unlock8;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock9 = new Unlock(UnlockIds.MERCHANT_CLOCK, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardMerchantItem(this.worldCrusade.merchantItems[2]));
			challengeWithTime.unlock = unlock9;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock10 = new Unlock(UnlockIds.TC_MYTHSTONES_01, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 20000.0));
			challengeWithTime.unlock = unlock10;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock11 = new Unlock(UnlockIds.TC_SCRAPS_01, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.SCRAP, 200.0));
			challengeWithTime.unlock = unlock11;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock12 = new Unlock(UnlockIds.TC_CREDITS_02, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.GEM, 400.0));
			challengeWithTime.unlock = unlock12;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock13 = new Unlock(UnlockIds.TC_TOKENS_02, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.TOKEN, 50.0));
			challengeWithTime.unlock = unlock13;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock14 = new Unlock(UnlockIds.TC_SCRAPS_02, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.SCRAP, 400.0));
			challengeWithTime.unlock = unlock14;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock15 = new Unlock(UnlockIds.TC_MYTHSTONES_02, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 100000.0));
			challengeWithTime.unlock = unlock15;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock16 = new Unlock(UnlockIds.TC_CREDITS_03, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.GEM, 500.0));
			challengeWithTime.unlock = unlock16;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock17 = new Unlock(UnlockIds.TC_TOKENS_03, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.TOKEN, 50.0));
			challengeWithTime.unlock = unlock17;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock18 = new Unlock(UnlockIds.TC_SCRAPS_03, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.SCRAP, 400.0));
			challengeWithTime.unlock = unlock18;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock19 = new Unlock(UnlockIds.TC_MYTHSTONES_03, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 5000000.0));
			challengeWithTime.unlock = unlock19;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock20 = new Unlock(UnlockIds.TC_CREDITS_04, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.GEM, 600.0));
			challengeWithTime.unlock = unlock20;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock21 = new Unlock(UnlockIds.TC_TOKENS_04, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.TOKEN, 200.0));
			challengeWithTime.unlock = unlock21;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock22 = new Unlock(UnlockIds.TC_SCRAPS_04, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.SCRAP, 400.0));
			challengeWithTime.unlock = unlock22;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock23 = new Unlock(UnlockIds.TC_MYTHSTONES_04, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 40000000.0));
			challengeWithTime.unlock = unlock23;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock24 = new Unlock(UnlockIds.TC_CREDITS_05, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.GEM, 500.0));
			challengeWithTime.unlock = unlock24;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock25 = new Unlock(UnlockIds.TC_TOKENS_05, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.TOKEN, 250.0));
			challengeWithTime.unlock = unlock25;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock26 = new Unlock(UnlockIds.TC_SCRAPS_05, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.SCRAP, 500.0));
			challengeWithTime.unlock = unlock26;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock27 = new Unlock(UnlockIds.TC_MYTHSTONES_05, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 300000000.0));
			challengeWithTime.unlock = unlock27;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock28 = new Unlock(UnlockIds.TC_CREDITS_06, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.GEM, 500.0));
			challengeWithTime.unlock = unlock28;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock29 = new Unlock(UnlockIds.TC_TOKENS_06, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.TOKEN, 500.0));
			challengeWithTime.unlock = unlock29;
			challengeWithTime = (ChallengeWithTime)this.worldCrusade.allChallenges[num++];
			Unlock unlock30 = new Unlock(UnlockIds.TC_SCRAPS_06, this.worldCrusade, new UnlockReqTimeChallenge(challengeWithTime), new UnlockRewardCurrency(CurrencyType.SCRAP, 500.0));
			challengeWithTime.unlock = unlock30;
			this.worldCrusade.unlocks.Add(unlock);
			this.worldCrusade.unlocks.Add(unlock2);
			this.worldCrusade.unlocks.Add(unlock5);
			this.worldCrusade.unlocks.Add(unlock4);
			this.worldCrusade.unlocks.Add(unlock6);
			this.worldCrusade.unlocks.Add(unlock3);
			this.worldCrusade.unlocks.Add(unlock9);
			this.worldCrusade.unlocks.Add(unlock7);
			this.worldCrusade.unlocks.Add(unlock8);
			this.worldCrusade.unlocks.Add(unlock10);
			this.worldCrusade.unlocks.Add(unlock11);
			this.worldCrusade.unlocks.Add(unlock12);
			this.worldCrusade.unlocks.Add(unlock13);
			this.worldCrusade.unlocks.Add(unlock14);
			this.worldCrusade.unlocks.Add(unlock15);
			this.worldCrusade.unlocks.Add(unlock16);
			this.worldCrusade.unlocks.Add(unlock17);
			this.worldCrusade.unlocks.Add(unlock18);
			this.worldCrusade.unlocks.Add(unlock19);
			this.worldCrusade.unlocks.Add(unlock20);
			this.worldCrusade.unlocks.Add(unlock21);
			this.worldCrusade.unlocks.Add(unlock22);
			this.worldCrusade.unlocks.Add(unlock23);
			this.worldCrusade.unlocks.Add(unlock24);
			this.worldCrusade.unlocks.Add(unlock25);
			this.worldCrusade.unlocks.Add(unlock26);
			this.worldCrusade.unlocks.Add(unlock27);
			this.worldCrusade.unlocks.Add(unlock28);
			this.worldCrusade.unlocks.Add(unlock29);
			this.worldCrusade.unlocks.Add(unlock30);
			Dictionary<int, ChallengeRift> dictionary = new Dictionary<int, ChallengeRift>();
			int i = 0;
			int count = this.worldRift.allChallenges.Count;
			while (i < count)
			{
				ChallengeRift challengeRift = this.worldRift.allChallenges[i] as ChallengeRift;
				dictionary.Add(challengeRift.id, challengeRift);
				i++;
			}
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_001, this.worldRift, dictionary[10], new UnlockRewardCurrency(CurrencyType.GEM, 100.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_002, this.worldRift, dictionary[20], new UnlockRewardCharmPack(), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_003, this.worldRift, dictionary[30], new UnlockRewardAeonDust(100.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_004, this.worldRift, dictionary[40], new UnlockRewardCurrency(CurrencyType.SCRAP, 250.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_005, this.worldRift, dictionary[50], new UnlockRewardSpecificCharm(this.allCharmEffects[105], 10), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_006, this.worldRift, dictionary[60], new UnlockRewardMerchantItem(this.worldRift.merchantItems[0]), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_007, this.worldRift, dictionary[70], new UnlockRewardCurrency(CurrencyType.SCRAP, 250.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_008, this.worldRift, dictionary[80], new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 10000000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_009, this.worldRift, dictionary[90], new UnlockRewardCurrency(CurrencyType.TOKEN, 250.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_010, this.worldRift, dictionary[100], new UnlockRewardSpecificCharm(this.allCharmEffects[203], 10), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_011, this.worldRift, dictionary[110], new UnlockRewardMerchantItem(this.worldRift.merchantItems[1]), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_012, this.worldRift, dictionary[120], new UnlockRewardCurrency(CurrencyType.AEON, 500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_013, this.worldRift, dictionary[130], new UnlockRewardCurrency(CurrencyType.GEM, 150.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_014, this.worldRift, dictionary[140], new UnlockRewardSpecificCharm(this.allCharmEffects[307], 10), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_015, this.worldRift, dictionary[150], new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 100000000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_016, this.worldRift, dictionary[160], new UnlockRewardMerchantItem(this.worldRift.merchantItems[2]), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_017, this.worldRift, dictionary[170], new UnlockRewardCurrency(CurrencyType.SCRAP, 300.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_018, this.worldRift, dictionary[180], new UnlockRewardTrinketPack(5), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_019, this.worldRift, dictionary[190], new UnlockRewardCurrency(CurrencyType.TOKEN, 500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_020, this.worldRift, dictionary[200], new UnlockRewardCurrency(CurrencyType.GEM, 200.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_021, this.worldRift, dictionary[210], new UnlockRewardCurrency(CurrencyType.SCRAP, 350.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_022, this.worldRift, dictionary[220], new UnlockRewardSpecificCharm(this.allCharmEffects[110], 20), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_023, this.worldRift, dictionary[230], new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 1000000000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_024, this.worldRift, dictionary[240], new UnlockRewardTrinketPack(5), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_025, this.worldRift, dictionary[250], new UnlockRewardCurrency(CurrencyType.AEON, 500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_026, this.worldRift, dictionary[260], new UnlockRewardCharmPack(), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_027, this.worldRift, dictionary[270], new UnlockRewardCurrency(CurrencyType.GEM, 250.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_028, this.worldRift, dictionary[280], new UnlockRewardSpecificCharm(this.allCharmEffects[208], 20), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_029, this.worldRift, dictionary[290], new UnlockRewardCurrency(CurrencyType.TOKEN, 500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_030, this.worldRift, dictionary[300], new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 10000000000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_031, this.worldRift, dictionary[310], new UnlockRewardTrinketPack(5), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_032, this.worldRift, dictionary[320], new UnlockRewardCurrency(CurrencyType.AEON, 500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_033, this.worldRift, dictionary[330], new UnlockRewardCurrency(CurrencyType.GEM, 300.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_034, this.worldRift, dictionary[340], new UnlockRewardCurrency(CurrencyType.SCRAP, 400.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_035, this.worldRift, dictionary[350], new UnlockRewardTrinketPack(5), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_036, this.worldRift, dictionary[360], new UnlockRewardMerchantItem(this.worldRift.merchantItems[3]), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_037, this.worldRift, dictionary[370], new UnlockRewardSpecificCharm(this.allCharmEffects[304], 20), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_038, this.worldRift, dictionary[380], new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 100000000000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_039, this.worldRift, dictionary[390], new UnlockRewardCurrency(CurrencyType.AEON, 600.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_040, this.worldRift, dictionary[400], new UnlockRewardCharmPack(), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_041, this.worldRift, dictionary[410], new UnlockRewardTrinketPack(5), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_042, this.worldRift, dictionary[420], new UnlockRewardCurrency(CurrencyType.TOKEN, 600.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_043, this.worldRift, dictionary[430], new UnlockRewardCurrency(CurrencyType.GEM, 350.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_044, this.worldRift, dictionary[440], new UnlockRewardCharmPack(), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_045, this.worldRift, dictionary[450], new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 1000000000000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_046, this.worldRift, dictionary[460], new UnlockRewardTrinketPack(5), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_047, this.worldRift, dictionary[470], new UnlockRewardCurrency(CurrencyType.AEON, 700.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_048, this.worldRift, dictionary[480], new UnlockRewardCurrency(CurrencyType.TOKEN, 700.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_049, this.worldRift, dictionary[490], new UnlockRewardCurrency(CurrencyType.SCRAP, 500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_050, this.worldRift, dictionary[500], new UnlockRewardCurrency(CurrencyType.GEM, 400.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_051, this.worldRift, dictionary[510], new UnlockRewardCurrency(CurrencyType.TOKEN, 800.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_052, this.worldRift, dictionary[520], new UnlockRewardSpecificCharm(this.allCharmEffects[107], 30), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_053, this.worldRift, dictionary[530], new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 10000000000000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_054, this.worldRift, dictionary[540], new UnlockRewardCharmPack(), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_055, this.worldRift, dictionary[550], new UnlockRewardCurrency(CurrencyType.AEON, 800.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_056, this.worldRift, dictionary[560], new UnlockRewardTrinketPack(5), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_057, this.worldRift, dictionary[570], new UnlockRewardCurrency(CurrencyType.GEM, 450.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_058, this.worldRift, dictionary[580], new UnlockRewardCurrency(CurrencyType.AEON, 900.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_059, this.worldRift, dictionary[590], new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 100000000000000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_060, this.worldRift, dictionary[600], new UnlockRewardCharmPack(), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_061, this.worldRift, dictionary[610], new UnlockRewardCurrency(CurrencyType.SCRAP, 750.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_062, this.worldRift, dictionary[620], new UnlockRewardCurrency(CurrencyType.GEM, 500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_063, this.worldRift, dictionary[630], new UnlockRewardCurrency(CurrencyType.TOKEN, 800.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_064, this.worldRift, dictionary[640], new UnlockRewardTrinketPack(5), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_065, this.worldRift, dictionary[650], new UnlockRewardCurrency(CurrencyType.SCRAP, 750.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_066, this.worldRift, dictionary[660], new UnlockRewardCurrency(CurrencyType.MYTHSTONE, 1E+15), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_067, this.worldRift, dictionary[670], new UnlockRewardCurrency(CurrencyType.GEM, 500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_068, this.worldRift, dictionary[680], new UnlockRewardSpecificCharm(this.allCharmEffects[204], 30), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_069, this.worldRift, dictionary[690], new UnlockRewardCurrency(CurrencyType.AEON, 1000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_070, this.worldRift, dictionary[700], new UnlockRewardCurrency(CurrencyType.SCRAP, 750.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_071, this.worldRift, dictionary[710], new UnlockRewardCharmPack(), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_072, this.worldRift, dictionary[720], new UnlockRewardCurrency(CurrencyType.TOKEN, 900.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_073, this.worldRift, dictionary[730], new UnlockRewardCurrency(CurrencyType.GEM, 500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_074, this.worldRift, dictionary[740], new UnlockRewardCurrency(CurrencyType.SCRAP, 1000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_075, this.worldRift, dictionary[750], new UnlockRewardTrinketPack(5), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_076, this.worldRift, dictionary[760], new UnlockRewardTrinketPack(5), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_077, this.worldRift, dictionary[770], new UnlockRewardTrinketPack(5), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_078, this.worldRift, dictionary[780], new UnlockRewardCurrency(CurrencyType.TOKEN, 1000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_079, this.worldRift, dictionary[790], new UnlockRewardCurrency(CurrencyType.SCRAP, 1000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_080, this.worldRift, dictionary[800], new UnlockRewardCurrency(CurrencyType.GEM, 500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_081, this.worldRift, dictionary[810], new UnlockRewardCurrency(CurrencyType.TOKEN, 1000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_082, this.worldRift, dictionary[820], new UnlockRewardCurrency(CurrencyType.SCRAP, 1000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_083, this.worldRift, dictionary[830], new UnlockRewardTrinketPack(5), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_084, this.worldRift, dictionary[840], new UnlockRewardCharmPack(), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_085, this.worldRift, dictionary[850], new UnlockRewardCurrency(CurrencyType.AEON, 1000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_086, this.worldRift, dictionary[860], new UnlockRewardCurrency(CurrencyType.TOKEN, 1000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_087, this.worldRift, dictionary[870], new UnlockRewardCurrency(CurrencyType.SCRAP, 1000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_088, this.worldRift, dictionary[880], new UnlockRewardTrinketPack(5), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_089, this.worldRift, dictionary[890], new UnlockRewardCurrency(CurrencyType.AEON, 1000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_090, this.worldRift, dictionary[900], new UnlockRewardCurrency(CurrencyType.GEM, 500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_091, this.worldRift, dictionary[910], new UnlockRewardCurrency(CurrencyType.SCRAP, 1500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_092, this.worldRift, dictionary[920], new UnlockRewardCharmPack(), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_093, this.worldRift, dictionary[930], new UnlockRewardCurrency(CurrencyType.TOKEN, 1500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_094, this.worldRift, dictionary[940], new UnlockRewardCurrency(CurrencyType.AEON, 1500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_095, this.worldRift, dictionary[950], new UnlockRewardCurrency(CurrencyType.GEM, 500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_096, this.worldRift, dictionary[960], new UnlockRewardCurrency(CurrencyType.SCRAP, 1500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_097, this.worldRift, dictionary[970], new UnlockRewardTrinketPack(5), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_098, this.worldRift, dictionary[980], new UnlockRewardTrinketPack(5), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_099, this.worldRift, dictionary[990], new UnlockRewardCurrency(CurrencyType.SCRAP, 1500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_100, this.worldRift, dictionary[1000], new UnlockRewardCurrency(CurrencyType.GEM, 1000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_101, this.worldRift, dictionary[1010], new UnlockRewardCurrency(CurrencyType.TOKEN, 1500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_102, this.worldRift, dictionary[1020], new UnlockRewardCurrency(CurrencyType.AEON, 1500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_103, this.worldRift, dictionary[1030], new UnlockRewardTrinketPack(5), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_104, this.worldRift, dictionary[1040], new UnlockRewardCurrency(CurrencyType.SCRAP, 1500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_105, this.worldRift, dictionary[1050], new UnlockRewardCurrency(CurrencyType.GEM, 500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_106, this.worldRift, dictionary[1060], new UnlockRewardCurrency(CurrencyType.TOKEN, 1500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_107, this.worldRift, dictionary[1070], new UnlockRewardCurrency(CurrencyType.AEON, 1500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_108, this.worldRift, dictionary[1080], new UnlockRewardTrinketPack(5), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_109, this.worldRift, dictionary[1090], new UnlockRewardCurrency(CurrencyType.SCRAP, 1500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_110, this.worldRift, dictionary[1100], new UnlockRewardCurrency(CurrencyType.GEM, 500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_111, this.worldRift, dictionary[1110], new UnlockRewardCurrency(CurrencyType.TOKEN, 1500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_112, this.worldRift, dictionary[1120], new UnlockRewardCurrency(CurrencyType.AEON, 1500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_113, this.worldRift, dictionary[1130], new UnlockRewardTrinketPack(5), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_114, this.worldRift, dictionary[1140], new UnlockRewardCurrency(CurrencyType.SCRAP, 1500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_115, this.worldRift, dictionary[1150], new UnlockRewardCurrency(CurrencyType.GEM, 1000.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_116, this.worldRift, dictionary[1160], new UnlockRewardCurrency(CurrencyType.TOKEN, 1500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_117, this.worldRift, dictionary[1170], new UnlockRewardCurrency(CurrencyType.AEON, 1500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_118, this.worldRift, dictionary[1180], new UnlockRewardTrinketPack(5), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_119, this.worldRift, dictionary[1190], new UnlockRewardCurrency(CurrencyType.SCRAP, 1500.0), true);
			Simulator.AddUnlock(UnlockIds.RIFT_REWARD_120, this.worldRift, dictionary[1200], new UnlockRewardCurrency(CurrencyType.GEM, 1500.0), true);
			this.rewardedRunes = new List<Rune>();
			foreach (World world in this.allWorlds)
			{
				foreach (Unlock unlock31 in world.unlocks)
				{
					if (unlock31.GetReward() is UnlockRewardRune)
					{
						this.rewardedRunes.Add((unlock31.GetReward() as UnlockRewardRune).GetRune());
					}
				}
			}
		}

		public static Unlock AddUnlock(uint id, World world, ChallengeRift challenge, UnlockReward unlock, bool add = true)
		{
			challenge.unlock = new Unlock(id, world, new UnlockReqRiftChallenge(challenge), unlock);
			if (add)
			{
				world.unlocks.Add(challenge.unlock);
			}
			return challenge.unlock;
		}

		public static void InitAchievements()
		{
			Simulator.achievementCollecteds = new Dictionary<string, bool>();
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQBw", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQCA", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQCQ", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQCg", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQCw", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQDQ", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQDg", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQDw", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQEA", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQEQ", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQEg", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQEw", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQFA", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQFQ", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQFg", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQFw", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQGA", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQGQ", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQGw", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQGg", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQLA", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQHA", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQHQ", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQHg", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQIA", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQHw", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQKw", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQIQ", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQIg", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQIw", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQJQ", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQJA", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQPQ", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQPg", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQPw", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQQA", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQQQ", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQMw", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQNA", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQNQ", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQNg", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQNw", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQLQ", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQLg", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQLw", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQMA", false);
			Simulator.achievementCollecteds.Add("CgkIlpSuuo0ZEAIQMQ", false);
			Simulator.achievementRewards = new Dictionary<string, double>();
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQBw", 15.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQCA", 30.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQCQ", 50.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQCg", 100.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQCw", 500.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQDQ", 15.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQDg", 30.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQDw", 50.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQEA", 100.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQEQ", 500.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQEg", 15.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQEw", 30.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQFA", 50.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQFQ", 100.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQFg", 500.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQFw", 15.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQGA", 30.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQGQ", 50.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQGw", 100.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQGg", 500.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQLA", 500.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQHA", 15.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQHQ", 30.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQHg", 50.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQIA", 100.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQHw", 500.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQKw", 500.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQIQ", 15.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQIg", 30.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQIw", 50.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQJQ", 100.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQJA", 500.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQPQ", 15.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQPg", 30.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQPw", 50.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQQA", 100.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQQQ", 500.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQMw", 15.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQNA", 30.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQNQ", 50.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQNg", 100.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQNw", 500.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQLQ", 15.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQLg", 30.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQLw", 50.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQMA", 100.0);
			Simulator.achievementRewards.Add("CgkIlpSuuo0ZEAIQMQ", 500.0);
		}

		public void InitCharms()
		{
			this.allCharmEffects = new Dictionary<int, CharmEffectData>();
			this.allCharmEffects.Add(101, new CharmEffectFieryFire());
			this.allCharmEffects.Add(103, new CharmEffectPowerOverwhelming());
			this.allCharmEffects.Add(105, new CharmEffectBerserk());
			this.allCharmEffects.Add(106, new CharmEffectBribedAccuracy());
			this.allCharmEffects.Add(107, new CharmEffectLooseLessons());
			this.allCharmEffects.Add(108, new CharmEffectThirstingFiends());
			this.allCharmEffects.Add(109, new CharmEffectBouncingBolt());
			this.allCharmEffects.Add(110, new CharmEffectExplosiveEnergy());
			this.allCharmEffects.Add(104, new CharmEffectProfessionalStrike());
			this.allCharmEffects.Add(102, new CharmEffectBootlegFireworks());
			this.allCharmEffects.Add(201, new CharmEffectCallFromGrave());
			this.allCharmEffects.Add(203, new CharmEffectBodyBlock());
			this.allCharmEffects.Add(204, new CharmEffectSweetMoves());
			this.allCharmEffects.Add(205, new CharmEffectSpellShield());
			this.allCharmEffects.Add(206, new CharmEffectFrostyStorm());
			this.allCharmEffects.Add(208, new CharmEffectStaryStaryDay());
			this.allCharmEffects.Add(209, new CharmEffectAppleADay());
			this.allCharmEffects.Add(210, new CharmEffectQuackatoa());
			this.allCharmEffects.Add(202, new CharmEffectRustyDagger());
			this.allCharmEffects.Add(207, new CharmEffectProtectiveWard());
			this.allCharmEffects.Add(301, new CharmEffectGrimRewards());
			this.allCharmEffects.Add(302, new CharmEffectEndlessSparks());
			this.allCharmEffects.Add(304, new CharmEffectVengefulSparks());
			this.allCharmEffects.Add(305, new CharmEffectSpecialDelivery());
			this.allCharmEffects.Add(306, new CharmEffectExtremeImpatience());
			this.allCharmEffects.Add(307, new CharmEffectWealthFromAbove());
			this.allCharmEffects.Add(308, new CharmEffectEmergencyFlute());
			this.allCharmEffects.Add(310, new CharmEffectLucrativeLightning());
			this.allCharmEffects.Add(303, new CharmEffectQuickStudy());
			this.allCharmEffects.Add(309, new CharmEffectTimeWarper());
		}

		public void InitCurses()
		{
			this.allCurseEffects = new Dictionary<int, CurseEffectData>();
			this.allCurseEffects.Add(1000, new CurseEffectCDReduction());
			this.allCurseEffects.Add(1001, new CurseEffectDealDamage());
			this.allCurseEffects.Add(1002, new CurseEffectTimeSlow());
			this.allCurseEffects.Add(1003, new CurseEffectCharmProgress());
			this.allCurseEffects.Add(1004, new CurseEffectReflectDamage());
			this.allCurseEffects.Add(1005, new CurseEffectMiss());
			this.allCurseEffects.Add(1006, new CurseEffectUpgradeCost());
			this.allCurseEffects.Add(1007, new CurseEffectHeroDamage());
			this.allCurseEffects.Add(1008, new CurseEffectStunHero());
			this.allCurseEffects.Add(1009, new CurseEffectAntiRegeneration());
			this.allCurseEffects.Add(1010, new CurseEffectHeavyLimbs());
			this.allCurseEffects.Add(1011, new CurseEffectUncannyRegeneration());
			this.allCurseEffects.Add(1012, new CurseEffectPartingShot());
			this.allCurseEffects.Add(1013, new CurseEffectDelayedCharms());
			this.allCurseEffects.Add(1014, new CurseEffectHauntingVisage());
			this.allCurseEffects.Add(1015, new CurseEffectMoltenGold());
			this.allCurseEffects.Add(1016, new CurseEffectGogZeal());
			this.allCurseEffects.Add(1017, new CurseEffectDampenedWill());
			this.allCurseEffects.Add(1018, new CurseEffectIncantationInversion());
			this.allCurseEffects.Add(1019, new CurseEffectGhostlyHeroes());
			foreach (KeyValuePair<int, CurseEffectData> keyValuePair in this.allCurseEffects)
			{
				keyValuePair.Value.isNew = true;
			}
		}

		public void DEBUGReset()
		{
			this.mythstones.InitZero();
			foreach (HeroDataBase heroDataBase in this.allHeroes)
			{
				heroDataBase.evolveLevel = 0;
			}
			this.newHeroIconSelectedHeroIds.Clear();
			this.unlockedHeroIds.Clear();
			this.unlockedHeroIds.Add(this.allHeroes[0].GetId());
			this.unlockedHeroIds.Add(this.allHeroes[1].GetId());
			this.unlockedTotemIds.Clear();
			this.unlockedTotemIds.Add(this.allTotems[0].id);
			this.boughtGears.Clear();
			this.universalTotalBonus.Init();
			this.universalTotalBonusRift.Init();
			this.worldStandard.DEBUGreset();
			this.worldCrusade.DEBUGreset();
			this.activeWorld = this.worldStandard;
			this.isMerchantUnlocked = false;
			this.numPrestiges = 0;
			this.maxStagePrestigedAt = 0;
			this.UpdateUniversalTotalBonus();
			this.lootpackFreeLastOpenTime = GameMath.GetNow();
			this.lootpackFreeLastOpenTime = this.lootpackFreeLastOpenTime.AddSeconds((double)(-(double)this.GetFreeLootpackPeriod()));
			this.lootpackFreeLastOpenTimeServer = GameMath.GetNow();
			this.lootpackFreeLastOpenTimeServer = this.lootpackFreeLastOpenTimeServer.AddSeconds(-172800.0);
		}

		public void CalculateOffline(float timeInSecondsSinceSave, out Dictionary<GameMode, double> goldEarnings, out Dictionary<GameMode, int> totWaves)
		{
			goldEarnings = new Dictionary<GameMode, double>();
			totWaves = new Dictionary<GameMode, int>();
			foreach (World world in this.allWorlds)
			{
				if (world.activeChallenge.doesUpdateInBg)
				{
					world.gold.InitZero();
					world.autoUpgradeDisabled = true;
					world.raidChestDisabled = true;
					float num = 0f;
					if (!this.hasCompass)
					{
						world.StopStageProgression();
					}
					while (num < timeInSecondsSinceSave)
					{
						num += 0.5f;
						world.Update(0.5f, null);
						world.sounds.Clear();
					}
					double num2 = 0.0;
					if (TutorialManager.first == TutorialManager.First.FIN)
					{
						num2 = world.gold.GetAmount();
						num2 *= PlayfabManager.titleData.offlineEarningsFactor;
						num2 *= this.universalTotalBonus.goldOfflineFactor;
					}
					goldEarnings.Add(world.gameMode, num2);
					if (world.activeChallenge.HasWaveProgression() && this.hasCompass)
					{
						totWaves.Add(world.gameMode, world.activeChallenge.GetTotWave());
					}
				}
			}
		}

		public void AddOfflineGolds(Dictionary<GameMode, double> offlineEarnings, float offlineEarningsFactor)
		{
			foreach (World world in this.allWorlds)
			{
				double num = (!offlineEarnings.ContainsKey(world.gameMode)) ? 0.0 : (offlineEarnings[world.gameMode] * (double)offlineEarningsFactor);
				world.offlineGold += num;
			}
		}

		public void AddOfflineCooldowns(float timeDecrement)
		{
			foreach (World world in this.allWorlds)
			{
				if (world.activeChallenge.doesUpdateInBg)
				{
					foreach (Hero hero in world.heroes)
					{
						hero.DecreaseUltiCooldown(timeDecrement);
					}
					if (world.gameMode == GameMode.STANDARD && ((ChallengeStandard)world.activeChallenge).IsThereAliveNonleavingBoss())
					{
						((ChallengeStandard)world.activeChallenge).DecreaseTimeLeftBoss(timeDecrement);
					}
				}
			}
		}

		internal int GetRiftIndex(int id)
		{
			int num = 0;
			foreach (Challenge challenge in this.worldRift.allChallenges)
			{
				ChallengeRift challengeRift = (ChallengeRift)challenge;
				if (challengeRift.id == id)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		public void AddOfflineTotWavesAdvances(Dictionary<GameMode, int> inc)
		{
			foreach (World world in this.allWorlds)
			{
				int num = (!inc.ContainsKey(world.gameMode)) ? 0 : inc[world.gameMode];
				if (num != 0)
				{
					int num2 = num - world.activeChallenge.GetTotWave();
					if (num2 > 0)
					{
						world.offlineWaveProgression = num2;
						world.activeChallenge.LoadTotWave(num);
					}
				}
			}
		}

		public void Update(float dt, Taps tap, UiCommand uiCommand)
		{
			this.frameCount++;
			if (uiCommand != null)
			{
				uiCommand.Apply(this);
			}
			foreach (HeroDataBase heroDataBase in this.allHeroes)
			{
				if (heroDataBase.trinketEquipTimer > 0f)
				{
					heroDataBase.trinketEquipTimer = Mathf.Max(0f, heroDataBase.trinketEquipTimer - dt);
				}
			}
			if (this.christmasEventForbidden)
			{
				this.christmasEnabled = false;
				this.candyDropAlreadyDisabled = true;
				this.christmasEventAlreadyDisabled = true;
			}
			if (TrustedTime.IsReady())
			{
				DateTime dateTime = TrustedTime.Get();
				if (TutorialManager.IsShopTabUnlocked())
				{
					this.specialOfferBoard.Update(this, dateTime);
				}
				if (this.dailyQuestCollectedTime == DateTime.MaxValue)
				{
					this.dailyQuestCollectedTime = TrustedTime.Get();
				}
				if (this.riftRestRewardCollectedTime == DateTime.MaxValue)
				{
					this.riftRestRewardCollectedTime = TrustedTime.Get();
				}
				if (this.dailyQuest == null && this.hasDailies && (TrustedTime.Get() - this.dailyQuestCollectedTime).TotalSeconds > DailyQuest.GetTimeBetweenQuests())
				{
					this.dailyQuest = this.GetRandomDailyQuest();
					foreach (World world in this.allWorlds)
					{
						world.ResetDailyQuestProgress();
					}
				}
				if (this.questOfUpdate == null)
				{
					if (this.HasAvailableQuestOfUpdate(dateTime, true, out this.questOfUpdate))
					{
						this.questOfUpdate.StartQuest(dateTime);
					}
				}
				else
				{
					this.questOfUpdate.Update(TrustedTime.Get());
					if (!this.questOfUpdate.IsCompleted() && this.questOfUpdate.isExpired)
					{
						this.failedQuestOfUpdates.Add(this.questOfUpdate.id);
						this.questOfUpdate = null;
					}
				}
				if (TutorialManager.AreFlashOffersUnlocked() || this.IsRiftShopUnlocked())
				{
					if (this.IsRiftShopUnlocked())
					{
						if (this.flashOfferBundle == null || this.flashOfferBundle.HasExpired())
						{
							DateTime? lastTimeCreated;
							if (this.flashOfferBundle == null)
							{
								lastTimeCreated = null;
							}
							else
							{
								lastTimeCreated = new DateTime?(this.flashOfferBundle.timeCreated);
							}
							int maxInt = this.maxStagePrestigedAt;
							if (this.worldStandard.activeChallenge != null && this.worldStandard.activeChallenge.state == Challenge.State.ACTION)
							{
								maxInt = GameMath.GetMaxInt(maxInt, ChallengeStandard.GetStageNo(this.worldStandard.activeChallenge.GetTotWave()));
							}
							this.flashOfferBundle = FlashOfferFactory.CreateRandomBundle(lastTimeCreated, maxInt);
						}
						else if (this.flashOfferBundle.offers == null)
						{
							this.flashOfferBundle.offers = new List<FlashOffer>();
							for (int i = 0; i < 3; i++)
							{
								this.flashOfferBundle.offers.Add(FlashOfferFactory.CreateRandomCharmOffer(this.flashOfferBundle.offers));
							}
						}
					}
					else if (this.flashOfferBundle == null || this.flashOfferBundle.HasExpired())
					{
						DateTime? lastTimeCreated2;
						if (this.flashOfferBundle == null)
						{
							lastTimeCreated2 = null;
						}
						else
						{
							lastTimeCreated2 = new DateTime?(this.flashOfferBundle.timeCreated);
						}
						int maxInt2 = this.maxStagePrestigedAt;
						if (this.worldStandard.activeChallenge != null && this.worldStandard.activeChallenge.state == Challenge.State.ACTION)
						{
							maxInt2 = GameMath.GetMaxInt(maxInt2, ChallengeStandard.GetStageNo(this.worldStandard.activeChallenge.GetTotWave()));
						}
						this.flashOfferBundle = FlashOfferFactory.CreateRandomBundleWithotCharms(lastTimeCreated2, maxInt2);
					}
				}
				if (this.IsCursedRiftsModeUnlocked())
				{
					TimeSpan timeSpan = TrustedTime.Get() - this.lastAddedCurseChallengeTime;
					int num = (int)GameMath.FloorDouble(timeSpan.TotalSeconds / 14400.0);
					if (num > 0)
					{
						int num2 = this.cursedRiftSlots.slotCount - this.worldRift.cursedChallenges.Count;
						if (num2 > 0)
						{
							int minInt = GameMath.GetMinInt(num, num2);
							List<int> randomRiftIndexes = this.GetRandomRiftIndexes(minInt);
							for (int j = 0; j < minInt; j++)
							{
								int index = randomRiftIndexes[j];
								ChallengeRift item = this.GenerateCursedRift(index, this.riftDiscoveryIndex - 1, this.worldRift);
								this.worldRift.cursedChallenges.Add(item);
							}
							double num3 = GameMath.Clamp(timeSpan.TotalSeconds - (double)((float)minInt * 14400f), 0.0, 14400.0);
							this.lastAddedCurseChallengeTime = TrustedTime.Get();
							this.lastAddedCurseChallengeTime = this.lastAddedCurseChallengeTime.AddSeconds(-num3);
						}
						if (this.currentCurses == null || this.currentCurses.Count == 0)
						{
							this.PickRandomCurses();
						}
					}
				}
				if (PlayfabManager.halloweenOfferConfigLoaded)
				{
					DateTime endDateParsed = PlayfabManager.halloweenOfferConfig.endDateParsed;
					this.halloweenEnabled = (dateTime >= PlayfabManager.halloweenOfferConfig.startDateParsed && dateTime < endDateParsed);
					if (this.halloweenEnabled && this.halloweenFlashOfferBundle == null)
					{
						this.halloweenFlashOfferBundle = ServerSideFlashOfferBundle.CreateHalloweenBundle();
						foreach (FlashOffer flashOffer in this.halloweenFlashOfferBundle.offers)
						{
							if (flashOffer.type == FlashOffer.Type.COSTUME_PLUS_SCRAP)
							{
								bool flag = this.IsSkinBought(flashOffer.genericIntId);
								if (flag)
								{
									flashOffer.purchasesLeft = 0;
									flashOffer.isBought = true;
								}
							}
						}
					}
					else if (!this.halloweenEnabled)
					{
						this.halloweenFlashOfferBundle = null;
					}
				}
				else
				{
					this.halloweenEnabled = false;
				}
				EventConfig eventConfig = PlayfabManager.eventsInfo.GetEventConfig("secondAnniversary");
				if (eventConfig != null)
				{
					this.secondAnniversaryEventEnabled = (dateTime >= eventConfig.startDate && dateTime < eventConfig.endDate);
					if (this.secondAnniversaryEventEnabled)
					{
						if (this.secondAnniversaryFlashOffersBundle == null)
						{
							this.InitSecondAnniversaryOffersBundle();
						}
					}
					else
					{
						this.secondAnniversaryEventAlreadyDisabled = true;
						this.secondAnniversaryFlashOffersBundle = null;
						if (!this.secondAnniversaryHintsMarked)
						{
							this.secondAnniversaryHintsMarked = true;
							LoadingHints.SetUsedHint("LOADING_HINT_063");
							LoadingHints.SetUsedHint("LOADING_HINT_071");
							LoadingHints.SetUsedHint("LOADING_HINT_072");
							LoadingHints.SetUsedHint("LOADING_HINT_073");
						}
					}
				}
				else
				{
					this.secondAnniversaryEventEnabled = false;
				}
				if (this.christmasEventForbidden)
				{
					this.christmasEnabled = false;
					this.christmasEventAlreadyDisabled = true;
					this.candyDropAlreadyDisabled = true;
				}
				else
				{
					if (PlayfabManager.christmasOfferConfigLoaded)
					{
						DateTime endDateParsed2 = PlayfabManager.christmasOfferConfig.offerConfig.endDateParsed;
						this.christmasEnabled = (dateTime >= PlayfabManager.christmasOfferConfig.offerConfig.startDateParsed && dateTime < endDateParsed2);
						this.christmasEventAlreadyDisabled = !this.christmasEnabled;
					}
					else
					{
						this.christmasEnabled = false;
					}
					if (this.lastCandyAmountCapDailyReset == DateTime.MinValue)
					{
						this.lastCandyAmountCapDailyReset = TrustedTime.Get();
					}
					else
					{
						double totalSeconds = (TrustedTime.Get() - this.lastCandyAmountCapDailyReset).TotalSeconds;
						if (totalSeconds >= 36000.0)
						{
							double num4 = totalSeconds % 36000.0;
							this.lastCandyAmountCapDailyReset = TrustedTime.Get().AddSeconds(-num4);
							this.candyAmountCollectedSinceLastDailyCapReset = 0.0;
							this.christmasTreatVideosWatchedSinceLastReset = 0;
						}
					}
					if (PlayfabManager.christmasOfferConfigLoaded && TrustedTime.Get() > PlayfabManager.christmasOfferConfig.candyDropLimitDateParsed)
					{
						this.candyDropAlreadyDisabled = true;
					}
					if (!this.christmasEventPopupChecked)
					{
						DateTime t = TrustedTime.Get();
						if (this.lastSessionDate < new DateTime(2018, 12, 3, 23, 59, 59) && t >= new DateTime(2018, 12, 10, 10, 0, 0))
						{
							this.showChristmasEventPopup = true;
						}
						if (this.lastSessionDate < new DateTime(2018, 12, 10, 23, 59, 59) && t >= new DateTime(2018, 12, 17, 10, 0, 0))
						{
							this.showChristmasEventPopup = true;
						}
						if (this.lastSessionDate < new DateTime(2018, 12, 17, 23, 59, 59) && t >= new DateTime(2018, 12, 24, 10, 0, 0))
						{
							this.showChristmasEventPopup = true;
						}
						if (this.lastSessionDate < new DateTime(2018, 12, 24, 23, 59, 59) && t >= new DateTime(2018, 12, 31, 10, 0, 0))
						{
							this.showChristmasEventPopup = true;
						}
						if (this.lastSessionDate < new DateTime(2018, 12, 31, 23, 59, 59) && t >= new DateTime(2019, 1, 7, 10, 0, 0))
						{
							this.showChristmasEventPopup = true;
						}
						this.christmasEventPopupChecked = true;
					}
				}
				this.lastSessionDate = TrustedTime.Get();
			}
			if (this.christmasOfferBundle == null)
			{
				this.christmasOfferBundle = CalendarTreeOfferBundle.CreateChristmasBundle();
				this.christmasOfferBundle.InitTreeState(this.christmasOffersBundlePurchasesLeftByOffer);
			}
			if (!this.hasRiftQuest)
			{
				this.hasRiftQuest = true;
				this.riftQuestDustCollected = 0.0;
			}
			if (this.frameCount % 20 == 0 && this.IsCursedRiftsModeUnlocked() && this.activeWorld != this.worldRift)
			{
				List<ChallengeRift> list = new List<ChallengeRift>();
				for (int k = this.worldRift.cursedChallenges.Count - 1; k >= 0; k--)
				{
					ChallengeRift challengeRift = this.worldRift.cursedChallenges[k] as ChallengeRift;
					if (challengeRift.unlock.isCollected)
					{
						this.worldRift.cursedChallenges.RemoveAt(k);
						UnityEngine.Debug.LogWarning("Deleted a cursed rift because it has finished already");
						list.Add(challengeRift);
					}
				}
				bool flag2 = false;
				foreach (ChallengeRift challengeRift2 in list)
				{
					if (challengeRift2 == this.worldRift.activeChallenge)
					{
						flag2 = true;
					}
				}
				if (flag2)
				{
					this.worldRift.activeChallenge = this.worldRift.GetNextCursedRiftOrRegular();
				}
			}
			bool flag3 = this.HasValidCandyCollectQuest();
			bool flag4 = this.CanDropCandyBasedOnDailyCap();
			foreach (World world2 in this.allWorlds)
			{
				world2.lastUsedMerchantItemDuration += dt;
				world2.hasValidCandyQuest = flag3;
				world2.canDropChestCandies = (flag3 || this.forceDropChestCandy || flag4);
				world2.canDropCandies = (flag3 || this.forceDropCandy || flag4);
				if (world2 == this.worldStandard && world2.activeChallenge.state == Challenge.State.ACTION && !world2.IsRaining())
				{
					this.prestigeRunTimer += (double)dt;
				}
				if (world2 == this.activeWorld && !this.isActiveWorldPaused)
				{
					world2.Update(dt, tap);
				}
				else if (world2.activeChallenge.doesUpdateInBg)
				{
					world2.Update(dt, null);
				}
				for (int l = world2.merchantItemsToEvaluate.Count - 1; l >= 0; l--)
				{
					MerchantItem merchantItem = world2.merchantItemsToEvaluate[l];
					if (merchantItem.CanEvaulate(world2))
					{
						merchantItem.Apply(this);
						world2.lastUsedMerchantItemDuration = 0f;
						world2.merchantItemsToEvaluate.RemoveAt(l);
					}
				}
				this.mythstones.Increment(world2.GetAndZeroEarnedMythstone());
				this.credits.Increment(world2.GetAndZeroEarnedCredit());
				this.scraps.Increment(world2.GetAndZeroEarnedScrap());
				this.tokens.Increment(world2.GetAndZeroEarnedToken());
				this.aeons.Increment(world2.GetAndZeroEarnedAeon());
				if (this.hasRiftQuest)
				{
					this.AddRiftDust(world2.GetAndZeroEarnedRiftPoints());
				}
				this.numTrinketPacks += world2.GetAndZeroEarnedTrinketBox();
				double num5 = world2.GetAndZeroEarnedCandy();
				if (num5 > 0.0)
				{
					this.candies.Increment(num5);
				}
				num5 = world2.GetAndZeroEarnedCandyCapped();
				if (num5 > 0.0)
				{
					if (PlayfabManager.titleData.christmasCandyCapAmount > 0.0 && this.candyAmountCollectedSinceLastDailyCapReset + num5 > PlayfabManager.titleData.christmasCandyCapAmount)
					{
						num5 = PlayfabManager.titleData.christmasCandyCapAmount - this.candyAmountCollectedSinceLastDailyCapReset;
					}
					this.candies.Increment(num5);
					this.candyAmountCollectedSinceLastDailyCapReset += num5;
				}
				Unlock andNullEarnedUnlock = world2.GetAndNullEarnedUnlock();
				if (andNullEarnedUnlock != null)
				{
					andNullEarnedUnlock.GiveReward(this);
				}
				if (this.dailyQuest != null)
				{
					if (world2.dailyQuestKilledEnemyCounter > 0)
					{
						this.dailyQuest.OnKilledEnemy(world2.dailyQuestKilledEnemyCounter);
						world2.dailyQuestKilledEnemyCounter = 0;
					}
					if (world2.dailyQuestPassStageCounter > 0)
					{
						this.dailyQuest.OnPassStage(world2.dailyQuestPassStageCounter);
						world2.dailyQuestPassStageCounter = 0;
					}
					if (world2.dailyQuestUltiSkillCounter > 0)
					{
						this.dailyQuest.OnUltiSkill(world2.dailyQuestUltiSkillCounter);
						world2.dailyQuestUltiSkillCounter = 0;
					}
					if (world2.dailyQuestDragonCatchCounter > 0)
					{
						this.dailyQuest.OnCatchDragon(world2.dailyQuestDragonCatchCounter);
						world2.dailyQuestDragonCatchCounter = 0;
					}
					if (world2.dailyQuestHiltDodgeCounter > 0)
					{
						this.dailyQuest.OnHiltDodge();
						world2.dailyQuestHiltDodgeCounter = 0;
					}
					if (world2.dailyQuestBellylarfAngerTimer >= 1f)
					{
						int num6 = Mathf.FloorToInt(world2.dailyQuestBellylarfAngerTimer);
						this.dailyQuest.OnBellylarfAnger(num6);
						world2.dailyQuestBellylarfAngerTimer -= (float)num6;
					}
					if (world2.dailyQuestVexxCoolCounter > 0)
					{
						this.dailyQuest.OnVexxCool(world2.dailyQuestVexxCoolCounter);
						world2.dailyQuestVexxCoolCounter = 0;
					}
					if (world2.dailyQuestLennyKillCounter > 0)
					{
						this.dailyQuest.OnLennyKillStunned(world2.dailyQuestLennyKillCounter);
						world2.dailyQuestLennyKillCounter = 0;
					}
					if (world2.dailyQuestWendleCastCounter > 0)
					{
						this.dailyQuest.OnWendleCast();
						world2.dailyQuestWendleCastCounter = 0;
					}
					if (world2.dailyQuestVStealBossCounter > 0)
					{
						this.dailyQuest.OnVStealBoss();
						world2.dailyQuestVStealBossCounter = 0;
					}
					if (world2.dailyQuestBoomerBoomCounter > 0)
					{
						this.dailyQuest.OnBoomerBoom();
						world2.dailyQuestBoomerBoomCounter = 0;
					}
					if (world2.dailyQuestSamShieldCounter > 0)
					{
						this.dailyQuest.OnSamShield(world2.dailyQuestSamShieldCounter);
						world2.dailyQuestSamShieldCounter = 0;
					}
					if (world2.dailyQuestLiaMissCounter > 0)
					{
						this.dailyQuest.OnLiaMiss(world2.dailyQuestLiaMissCounter);
						world2.dailyQuestLiaMissCounter = 0;
					}
					if (world2.dailyQuestJimAllyDeathCounter > 0)
					{
						this.dailyQuest.OnJimAllyDeath();
						world2.dailyQuestJimAllyDeathCounter = 0;
					}
					if (world2.dailyQuestTamKillBlindedCounter > 0)
					{
						this.dailyQuest.OnTamKillBlinded(world2.dailyQuestTamKillBlindedCounter);
						world2.dailyQuestTamKillBlindedCounter = 0;
					}
					if (world2.dailyQuestWendleHealCounter > 0)
					{
						this.dailyQuest.OnWendleHeal();
						world2.dailyQuestWendleHealCounter = 0;
					}
					if (world2.dailyQuestVTreasureChestKill > 0)
					{
						this.dailyQuest.OnVTreasureChestKill(world2.dailyQuestVTreasureChestKill);
						world2.dailyQuestVTreasureChestKill = 0;
					}
					if (world2.dailyQuestLennyHealAlly > 0)
					{
						this.dailyQuest.OnLennyHealAlly(world2.dailyQuestLennyHealAlly);
						world2.dailyQuestLennyHealAlly = 0;
					}
					if (world2.dailyQuestBabuHealAlly > 0)
					{
						this.dailyQuest.OnBabuHealAlly(world2.dailyQuestBabuHealAlly);
						world2.dailyQuestBabuHealAlly = 0;
					}
					if (world2.dailyQuestHiltCriticalHit > 0)
					{
						this.dailyQuest.OnHiltCriticalHit(world2.dailyQuestHiltCriticalHit);
						world2.dailyQuestHiltCriticalHit = 0;
					}
					if (world2.dailyQuestBabuGetHitCounter > 0)
					{
						this.dailyQuest.OnBabuGetHit(world2.dailyQuestBabuGetHitCounter);
						world2.dailyQuestBabuGetHitCounter = 0;
					}
					if (world2.dailyQuestTamHitMarkedTargets > 0)
					{
						this.dailyQuest.OnTamHitMarkedTargets(world2.dailyQuestTamHitMarkedTargets);
						world2.dailyQuestTamHitMarkedTargets = 0;
					}
					if (world2.dailyQuestEnemyStunned > 0)
					{
						this.dailyQuest.OnStunnedEnemy(world2.dailyQuestEnemyStunned);
						world2.dailyQuestEnemyStunned = 0;
					}
					if (world2.dailyQuestLiaStealHealth > 0)
					{
						this.dailyQuest.OnLiaStealHealth(world2.dailyQuestLiaStealHealth);
						world2.dailyQuestLiaStealHealth = 0;
					}
					if (world2.dailyQuestWarlockBlindEnemy > 0)
					{
						this.dailyQuest.OnWarlockBlindEnemy(world2.dailyQuestWarlockBlindEnemy);
						world2.dailyQuestWarlockBlindEnemy = 0;
					}
					if (world2.dailyQuestWarlockRedirectDamage > 0)
					{
						this.dailyQuest.OnWarlockRedirectDamage(world2.dailyQuestWarlockRedirectDamage);
						world2.dailyQuestWarlockRedirectDamage = 0;
					}
					if (world2.dailyQuestGoblinMissCount > 0)
					{
						this.dailyQuest.OnGoblinMiss(world2.dailyQuestGoblinMissCount);
						world2.dailyQuestGoblinMissCount = 0;
					}
					if (world2.dailyQuestGoblinDragonSummonCount > 0)
					{
						this.dailyQuest.OnGoblinSummonDragon(world2.dailyQuestGoblinDragonSummonCount);
						world2.dailyQuestGoblinDragonSummonCount = 0;
					}
					if (world2.dailyQuestGoblinKillTreasureCount > 0)
					{
						this.dailyQuest.OnGoblinKillTreasure(world2.dailyQuestGoblinKillTreasureCount);
						world2.dailyQuestGoblinKillTreasureCount = 0;
					}
					if (world2.dailyQuestRonLandedCritHit > 0)
					{
						this.dailyQuest.OnRonLandedCritHit();
						world2.dailyQuestRonLandedCritHit = 0;
					}
					if (world2.dailyQuestRonImpulsiveSkillTriggeredCounter > 0)
					{
						this.dailyQuest.OnRonImpulsiveSkillTriggered();
						world2.dailyQuestRonImpulsiveSkillTriggeredCounter = 0;
					}
					if (num5 > 0.0)
					{
						this.dailyQuest.OnCollectCandy((int)num5);
					}
				}
			}
		}

		public bool HasValidCandyCollectQuest()
		{
			return this.dailyQuest != null && this.dailyQuest is DailyQuestCollectCandy && this.dailyQuest.goal > this.dailyQuest.progress;
		}

		public bool CanDropCandyBasedOnDailyCap()
		{
			return this.IsChristmasTreeEnabled() && !this.candyDropAlreadyDisabled && this.candyAmountCollectedSinceLastDailyCapReset < PlayfabManager.titleData.christmasCandyCapAmount;
		}

		private void OnGearsChanged()
		{
			this.UpdateUniversalTotalBonus();
			foreach (World world in this.allWorlds)
			{
				world.OnGearsChanged(this.boughtGears);
			}
		}

		public void OnArtifactsChanged()
		{
			this.UpdateUniversalTotalBonus();
			foreach (World world in this.allWorlds)
			{
				world.OnGearsChanged(this.boughtGears);
			}
		}

		public void OnMythicalArtifactCrafted()
		{
			this.OnArtifactsChanged();
			this.activeWorld.shouldSave = true;
			TutorialManager.PressedCraftArtifact();
			PlayerStats.OnArtifactCraft(this.artifactsManager.TotalArtifactsLevel, this.artifactsManager.GetTotalAmountOfArtifacts());
		}

		public void OnRegularArtifactCrafted()
		{
			this.OnArtifactsChanged();
			this.activeWorld.shouldSave = true;
			TutorialManager.PressedCraftArtifact();
			PlayerStats.OnArtifactCraft(this.artifactsManager.TotalArtifactsLevel, this.artifactsManager.GetTotalAmountOfArtifacts());
		}

		public void OnCharmsChanged()
		{
			this.UpdateUniversalTotalBonus();
			if (this.worldRift.activeChallenge.state == Challenge.State.ACTION)
			{
				ChallengeRift challengeRift = this.worldRift.activeChallenge as ChallengeRift;
				challengeRift.RecalculateCharmBuffStats();
			}
		}

		public void OnMinesChanged()
		{
			this.UpdateUniversalTotalBonus();
		}

		private void UpdateUniversalTotalBonus()
		{
			this.universalTotalBonus.Init();
			this.universalTotalBonusRift.Init();
			this.artifactsManager.ApplyEffects(this.universalTotalBonus);
			this.UpdateCommonBonuses(this.universalTotalBonus);
			this.UpdateCommonBonuses(this.universalTotalBonusRift);
			this.UpdateCharmBonuses(this.universalTotalBonusRift);
			bool autoUpgradeMilestones = false;
			Artifact artifact = this.artifactsManager.MythicalArtifacts.Find((Artifact a) => a.effects != null && a.effects[0] is MythicalArtifactLazyFinger);
			if (artifact != null && artifact.GetMythicalLevel() >= 90)
			{
				autoUpgradeMilestones = true;
			}
			foreach (World world in this.allWorlds)
			{
				world.OnUniversalBonusChanged();
				world.autoUpgradeMilestones = autoUpgradeMilestones;
			}
		}

		private void UpdateCharmBonuses(UniversalTotalBonus utb)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			foreach (KeyValuePair<int, CharmEffectData> keyValuePair in this.allCharmEffects)
			{
				if (keyValuePair.Value.level >= 0)
				{
					switch (keyValuePair.Value.BaseData.charmType)
					{
					case CharmType.Attack:
						num += keyValuePair.Value.level + 1;
						break;
					case CharmType.Defense:
						num2 += keyValuePair.Value.level + 1;
						break;
					case CharmType.Utility:
						num3 += keyValuePair.Value.level + 1;
						break;
					}
				}
			}
			utb.charmDamageFactor *= GameMath.PowInt(CharmEffectData.GLOBAL_BONUS_PER_LEVEL, num);
			utb.charmHealthFactor *= GameMath.PowInt(CharmEffectData.GLOBAL_BONUS_PER_LEVEL, num2);
			utb.charmGoldFactor *= GameMath.PowInt(CharmEffectData.GLOBAL_BONUS_PER_LEVEL, num3);
		}

		private void UpdateCommonBonuses(UniversalTotalBonus utb)
		{
			if (this.mineScrap.unlocked)
			{
				utb.mineGoldFactor = this.mineScrap.GetGoldFactor();
			}
			if (this.mineToken.unlocked)
			{
				utb.mineDamageFactor = this.mineToken.GetDamageFactor();
				utb.mineHealthFactor = this.mineToken.GetHealthFactor();
			}
			double num = 0.0;
			double num2 = 0.0;
			double num3 = 0.0;
			foreach (Gear gear in this.boughtGears)
			{
				if (gear.data.universalBonusType == GearData.UniversalBonusType.GOLD)
				{
					num += 0.01 * gear.GetUniversalBonusPercent(utb, 0);
				}
				else if (gear.data.universalBonusType == GearData.UniversalBonusType.HEALTH)
				{
					num2 += 0.01 * gear.GetUniversalBonusPercent(utb, 0);
				}
				else
				{
					if (gear.data.universalBonusType != GearData.UniversalBonusType.DAMAGE)
					{
						throw new NotImplementedException();
					}
					num3 += 0.01 * gear.GetUniversalBonusPercent(utb, 0);
				}
			}
			if (utb.gearAddMultiplierFactor > 0.0)
			{
				MythicalArtifactCustomTailor.totalDamageBonus = num3 * utb.gearAddMultiplierFactor;
				MythicalArtifactCustomTailor.totalHealthBonus = num2 * utb.gearAddMultiplierFactor;
				MythicalArtifactCustomTailor.totalGoldBonus = num * utb.gearAddMultiplierFactor;
				utb.gearDamageFactor += num3 + MythicalArtifactCustomTailor.totalDamageBonus;
				utb.gearHealthFactor += num2 + MythicalArtifactCustomTailor.totalHealthBonus;
				utb.gearGoldFactor += num + MythicalArtifactCustomTailor.totalGoldBonus;
			}
			else
			{
				utb.gearHealthFactor += num2;
				utb.gearGoldFactor += num;
				utb.gearDamageFactor += num3;
			}
		}

		public List<World> GetAllWorlds()
		{
			return this.allWorlds;
		}

		public World GetActiveWorld()
		{
			return this.activeWorld;
		}

		public bool IsActiveWorld(World w)
		{
			return this.activeWorld == w;
		}

		public GameMode GetCurrentGameMode()
		{
			return this.currentGameMode;
		}

		public Dictionary<TotemDataBase, GameMode> GetBoughtTotems()
		{
			Dictionary<TotemDataBase, GameMode> dictionary = new Dictionary<TotemDataBase, GameMode>();
			foreach (World world in this.allWorlds)
			{
				if (world.totem != null)
				{
					dictionary.Add(world.totem.GetData().GetDataBase(), world.gameMode);
				}
			}
			return dictionary;
		}

		public bool IsActiveMode(GameMode mode)
		{
			return this.currentGameMode == mode;
		}

		public int GetGameModeNumNotifactions(GameMode mode)
		{
			return 0;
		}

		public int GetGameModeNumNotifactions(World world)
		{
			int numNotificationsHeroesTab = this.GetNumNotificationsHeroesTab(world);
			int numCollectableUnlocks = this.GetNumCollectableUnlocks(world);
			return numNotificationsHeroesTab + numCollectableUnlocks;
		}

		public int GetNumNotificationsHeroesTab(World world)
		{
			return world.GetNumNotificationsHeroesTab() + ((!this.CanGetNewHero() || world.heroes.Count >= world.activeChallenge.numHeroesMax) ? 0 : 1) + ((this.GetNumAvailableToWearRunes() <= 0) ? 0 : 1) + ((!this.IsThereAnySkinBought()) ? 0 : this.GetNumHeroesWithNewSkins());
		}

		public int GetGameModeNumCollectedUnlocks(GameMode mode)
		{
			return this.GetGameModeNumCollectedUnlocks(this.GetWorld(mode));
		}

		public int GetGameModeNumCollectedUnlocks(World world)
		{
			return world.GetNumCollectedUnlocks();
		}

		public int GetGameModeTotNumUnlocks(GameMode mode)
		{
			return this.GetGameModeTotNumUnlocks(this.GetWorld(mode));
		}

		public int GetGameModeTotNumUnlocks(World world)
		{
			return world.unlocks.Count;
		}

		public string GetGameModeGoldString(GameMode mode)
		{
			return this.GetWorld(mode).gold.GetString();
		}

		public bool IsModeUnlocked(GameMode mode)
		{
			return this.GetWorld(mode).IsModeUnlocked();
		}

		public void UnlockGameMode(GameMode mode)
		{
			World world = this.GetWorld(mode);
			this.UnlockGameMode(world);
			if (mode == GameMode.RIFT)
			{
				this.AddCharmCard(101, 0);
				this.AddCharmCard(103, 0);
				this.AddCharmCard(109, 0);
				this.AddCharmCard(205, 0);
				this.AddCharmCard(206, 0);
				this.AddCharmCard(209, 0);
				this.AddCharmCard(305, 0);
				this.AddCharmCard(308, 0);
				this.AddCharmCard(310, 0);
			}
		}

		public void UnlockGameMode(World world)
		{
			if (world.unlockMode != null)
			{
				world.unlockMode.isCollected = true;
			}
		}

		public FlashOffer GetSkinOfferIfExist(SkinData skinData)
		{
			FlashOffer flashOffer = (this.secondAnniversaryFlashOffersBundle != null) ? this.secondAnniversaryFlashOffersBundle.GetSkinOfferWithId(skinData.id) : null;
			if (flashOffer != null)
			{
				return flashOffer;
			}
			FlashOffer flashOffer2 = (this.halloweenFlashOfferBundle != null) ? this.halloweenFlashOfferBundle.GetSkinOfferWithId(skinData.id) : null;
			if (flashOffer2 != null)
			{
				return flashOffer2;
			}
			FlashOffer flashOffer3 = (this.flashOfferBundle != null) ? this.flashOfferBundle.GetSkinOfferWithId(skinData.id) : null;
			if (flashOffer3 != null)
			{
				return flashOffer3;
			}
			return null;
		}

		public void TryBuySkin(int id)
		{
			SkinData skinData = this.allSkins.Find((SkinData s) => s.id == id);
			if (skinData != null && skinData.unlockType == SkinData.UnlockType.CURRENCY)
			{
				if (this.boughtSkins.Contains(skinData))
				{
					throw new Exception("Skin: " + skinData.id + " has already unlocked");
				}
				Currency currency = this.GetCurrency(skinData.currency);
				FlashOffer skinOfferIfExist = this.GetSkinOfferIfExist(skinData);
				double num = (skinOfferIfExist == null) ? skinData.cost : this.GetFlashOfferCost(skinOfferIfExist);
				if (currency.CanAfford(num))
				{
					currency.Decrement(num);
					this.boughtSkins.Add(skinData);
					if (skinOfferIfExist != null)
					{
						skinOfferIfExist.purchasesLeft = 0;
					}
					PlayerStats.OnUserBuySkin(new string[]
					{
						skinData.GetKey()
					});
					this.SendSkinBuyEvent(skinOfferIfExist, num, skinData);
				}
			}
		}

		public void UnlockSkin(int id)
		{
			foreach (SkinData skinData in this.allSkins)
			{
				if (skinData.id == id)
				{
					this.AddBoughtSkin(skinData);
				}
			}
		}

		public void FillHeroSkillParams(HeroSkillInstanceParams param)
		{
			this.activeWorld.FillMainSkillIsActives(param.isSkillActives);
			this.activeWorld.FillMainSkillIsTogglable(param.isSkillTogglable);
			this.activeWorld.FillMainSkillIsToggling(param.isSkillToggling);
			this.activeWorld.FillMainSkillCanActivates(param.canActivateSkills);
			this.activeWorld.FillMainSkillCooldownRatios(param.cooldownRatios);
			this.activeWorld.FillMainSkillToggleDelta(param.toggleDeltas);
			this.activeWorld.FillAutoSkillCooldownRatios(0, param.cooldownRatios1);
			this.activeWorld.FillAutoSkillCooldownRatios(1, param.cooldownRatios2);
			this.activeWorld.FillMainSkillCooldownMaxes(param.cooldownMaxes);
			this.activeWorld.FillHeroReviveTimes(param.heroReviveTimes);
			this.activeWorld.FillHeroReviveTimeMaxes(param.heroReviveTimeMaxes);
			this.activeWorld.FillHeroRechargeBuffs(param.heroRechargeBuffs);
			this.activeWorld.FillHeroStunnedBuffs(param.heroStunnedBuffs);
			this.activeWorld.FillHeroSilencedBuffs(param.heroSilencedBuffs);
			this.activeWorld.FillSkillTypes(param.skillTypes);
		}

		public List<bool> GetMainSkillIsActives()
		{
			return this.activeWorld.GetMainSkillIsActives();
		}

		public List<bool> GetMainSkillIsTogglable()
		{
			return this.activeWorld.GetMainSkillIsTogglable();
		}

		public List<bool> GetMainSkillIsToggling()
		{
			return this.activeWorld.GetMainSkillIsToggling();
		}

		public List<bool> GetMainSkillCanActivates()
		{
			return this.activeWorld.GetMainSkillCanActivates();
		}

		public List<float> GetMainSkillCooldownRatios()
		{
			return this.activeWorld.GetMainSkillCooldownRatios();
		}

		public List<float> GetAutoActiveSkillCooldownRatios(int index)
		{
			return this.activeWorld.GetAutoSkillCooldownRatios(index);
		}

		public List<HeroDataBase.UltiCatagory> GetUltiCategories()
		{
			return this.activeWorld.GetUltiCategories();
		}

		public List<float> GetMainSkillCooldownMaxes()
		{
			return this.activeWorld.GetMainSkillCooldownMaxes();
		}

		public List<float> GetHeroReviveTimes()
		{
			return this.activeWorld.GetHeroReviveTimes();
		}

		public List<float> GetHeroReviveTimeMaxes()
		{
			return this.activeWorld.GetHeroReviveTimeMaxes();
		}

		public List<bool> GetHeroRechargeBuffs()
		{
			return this.activeWorld.GetHeroRechargeBuffs();
		}

		public List<bool> GetHeroStunnedBuffs()
		{
			return this.activeWorld.GetHeroStunnedBuffs();
		}

		public List<Type> GetSkillTypes()
		{
			return this.activeWorld.GetSkillTypes();
		}

		public double GetTotEnemyHealth()
		{
			return this.activeWorld.GetTotEnemyHealth();
		}

		public double GetTotEnemyHealthMax()
		{
			return this.activeWorld.GetTotEnemyHealthMax();
		}

		public string GetTotemName()
		{
			return this.activeWorld.GetTotemName();
		}

		public int GetTotemLevel()
		{
			return this.activeWorld.GetTotemLevel();
		}

		public int GetTotemXp()
		{
			return this.activeWorld.GetTotemXp();
		}

		public int GetTotemXpNeedForNextLevel()
		{
			return this.activeWorld.GetTotemXpNeedForNextLevel();
		}

		public double GetTotemDamageNonBuffed()
		{
			return this.activeWorld.GetTotemDamageNonBuffed();
		}

		public double GetTotemDamageAll()
		{
			return this.activeWorld.GetTotemDamageAll();
		}

		public double GetTotemDamageDifUpgrade()
		{
			return this.activeWorld.GetTotemDamageDifUpgrade();
		}

		public double GetWorldUpgradeCost()
		{
			return this.activeWorld.GetNextChallangeUpgradeCost();
		}

		public bool CanAffordWorldUpgrade()
		{
			return this.activeWorld.CanAffordWorldUpgrade();
		}

		public bool IsNextWorldUpgradeUnlocked()
		{
			return this.activeWorld.IsNextWorldUpgradeUnlocked();
		}

		public void TryBuyWorldUpgrade()
		{
			this.activeWorld.TryBuyWorldUpgrade();
		}

		public int GetWorldUpgradesTotalNumBought()
		{
			return this.activeWorld.activeChallenge.totalGainedUpgrades.numBought;
		}

		public ChallengeUpgrade GetNextWorldUpgrade()
		{
			return this.activeWorld.GetNextChallangeUpgrade();
		}

		public ChallengeUpgrade GetPrevWorldUpgrade()
		{
			return this.activeWorld.GetPrevChallangeUpgrade();
		}

		public string GetWorldUpgradeDescription()
		{
			ChallengeUpgrade nextChallangeUpgrade = this.activeWorld.GetNextChallangeUpgrade();
			if (nextChallangeUpgrade == null)
			{
				return LM.Get("UI_BOUGHT_ALL_UPGRADES");
			}
			if (this.IsNextWorldUpgradeUnlocked())
			{
				return nextChallangeUpgrade.GetDescription(this.activeWorld);
			}
			if (this.activeWorld.gameMode == GameMode.STANDARD)
			{
				int stageReq = nextChallangeUpgrade.stageReq;
				return string.Format(LM.Get("UI_REACH_STAGE_X"), "<color=#eda12bff>" + stageReq.ToString() + "</color>");
			}
			if (this.activeWorld.gameMode == GameMode.CRUSADE)
			{
				int waveReq = nextChallangeUpgrade.waveReq;
				return string.Format(LM.Get("UI_CLEAR_X_WAVES"), "<color=#eda12bff>" + waveReq.ToString() + "</color>");
			}
			if (this.activeWorld.gameMode == GameMode.RIFT)
			{
				int waveReq2 = nextChallangeUpgrade.waveReq;
				return string.Format(LM.Get("UI_CLEAR_X_WAVES"), "<color=#eda12bff>" + waveReq2.ToString() + "</color>");
			}
			throw new NotImplementedException();
		}

		public double GetTotemUpgradeCost()
		{
			return this.activeWorld.GetTotemUpgradeCost();
		}

		public bool CanAffordTotemUpgrade()
		{
			return this.activeWorld.CanAffordTotemUpgrade();
		}

		public HeroDataBase GetHeroDataBase(string heroId)
		{
			foreach (HeroDataBase heroDataBase in this.allHeroes)
			{
				if (heroDataBase.GetId() == heroId)
				{
					return heroDataBase;
				}
			}
			UnityEngine.Debug.Log("Hero with id not found: " + heroId);
			throw new EntryPointNotFoundException();
		}

		public int GetActiveWorldNumHeroes()
		{
			return this.activeWorld.heroes.Count;
		}

		public int GetActiveWorldNumHeroesMax()
		{
			return this.activeWorld.activeChallenge.numHeroesMax;
		}

		public bool HasAnyHeroLeveledUpInActiveWorld()
		{
			int i = 0;
			int count = this.activeWorld.heroes.Count;
			while (i < count)
			{
				if (this.activeWorld.heroes[i].GetLevel() - i > 0)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		public TotemDataBase GetTotemDataBase(string id)
		{
			foreach (TotemDataBase totemDataBase in this.allTotems)
			{
				if (totemDataBase.id == id)
				{
					return totemDataBase;
				}
			}
			throw new EntryPointNotFoundException();
		}

		public float GetBossTimeScale()
		{
			return this.activeWorld.GetBossTimeScale();
		}

		public float GetBossTimePassed()
		{
			return this.activeWorld.GetBossTimePassed();
		}

		public bool CanGoToBoss()
		{
			return this.activeWorld.activeChallenge.CanGoToBoss();
		}

		public bool CanPrestigeNow()
		{
			return this.activeWorld.CanPrestigeNow();
		}

		public bool CanShowPrestigeTutorial()
		{
			return this.activeWorld.CanPrestigeNow() && this.activeWorld.activeChallenge.GetTotWave() >= 1101;
		}

		public double GetNewHeroCost()
		{
			return this.activeWorld.GetNewHeroCost();
		}

		public bool CanGetNewHero()
		{
			if (!this.activeWorld.CanAffordNewHero())
			{
				return false;
			}
			HashSet<string> hashSet = this.GetUnlockedHeroIds();
			Dictionary<string, GameMode> dictionary = this.GetBoughtHeroIdsWithModes();
			foreach (string key in hashSet)
			{
				if (!dictionary.ContainsKey(key))
				{
					return true;
				}
			}
			return false;
		}

		public bool HasAvailableHero()
		{
			HashSet<string> hashSet = this.GetUnlockedHeroIds();
			Dictionary<string, GameMode> dictionary = this.GetBoughtHeroIdsWithModes();
			foreach (string key in hashSet)
			{
				if (!dictionary.ContainsKey(key))
				{
					return true;
				}
			}
			return false;
		}

		public bool CanAffordNewHero()
		{
			return this.activeWorld.CanAffordNewHero();
		}

		public Currency GetCurrency(CurrencyType type)
		{
			switch (type)
			{
			case CurrencyType.GOLD:
				return this.GetGold();
			case CurrencyType.SCRAP:
				return this.GetScraps();
			case CurrencyType.MYTHSTONE:
				return this.GetMythstones();
			case CurrencyType.GEM:
				return this.GetCredits();
			case CurrencyType.TOKEN:
				return this.GetTokens();
			case CurrencyType.AEON:
				return this.GetAeons();
			case CurrencyType.CANDY:
				return this.GetCandies();
			default:
				throw new NotImplementedException();
			}
		}

		public Currency GetMythstones()
		{
			return this.mythstones;
		}

		public Currency GetGold()
		{
			return this.activeWorld.gold;
		}

		public Currency GetScraps()
		{
			return this.scraps;
		}

		public Currency GetCredits()
		{
			return this.credits;
		}

		public Currency GetTokens()
		{
			return this.tokens;
		}

		public Currency GetAeons()
		{
			return this.aeons;
		}

		public Currency GetCandies()
		{
			return this.candies;
		}

		public List<HeroDataBase> GetAllHeroes()
		{
			return this.allHeroes;
		}

		public List<SkinData> GetAllSkins()
		{
			return this.allSkins;
		}

		public List<SkinData> GetAllBuyableSkins()
		{
			return this.allSkins.FindAll((SkinData s) => s.unlockType == SkinData.UnlockType.CURRENCY && s.IsUnlockRequirementSatisfied(this) && !this.IsSkinBought(s) && (!s.isHalloweenSkin || (PlayfabManager.halloweenOfferConfigLoaded && !this.halloweenEnabled)) && (!s.isChristmasSkin || this.christmasEventAlreadyDisabled) && (!s.isSecondAnniversarySkin || !this.IsSecondAnniversaryEventEnabled()));
		}

		public List<SkinData> GetHeroSkins(string id)
		{
			KeyValuePair<int, int> keyValuePair = this.skinIndexRangePerHero[id];
			return this.allSkins.GetRange(keyValuePair.Key, keyValuePair.Value - keyValuePair.Key);
		}

		public List<SkinData> GetHeroBoughtSkins(string id)
		{
			List<SkinData> heroSkins = this.GetHeroSkins(id);
			heroSkins.RemoveAll((SkinData skin) => !this.IsSkinBought(skin.id));
			return heroSkins;
		}

		public int GetNumHeroSkins(string heroId)
		{
			KeyValuePair<int, int> keyValuePair = this.skinIndexRangePerHero[heroId];
			return keyValuePair.Value - keyValuePair.Key;
		}

		public SkinData GetHeroEvolveSkin(string heroId, int evolveLevel)
		{
			int evolveSkinKey = HeroSkins.GetEvolveSkinKey(heroId, evolveLevel);
			return this.GetSkinData(evolveSkinKey);
		}

		public int GetHerosNewSkinCount(string id)
		{
			KeyValuePair<int, int> keyValuePair = this.skinIndexRangePerHero[id];
			int num = 0;
			for (int i = keyValuePair.Key; i < keyValuePair.Value; i++)
			{
				SkinData skinData = this.allSkins[i];
				if (skinData.isNew && skinData.IsUnlockRequirementSatisfied(this))
				{
					num++;
				}
			}
			return num;
		}

		public bool HasHeroNewSkin(string id)
		{
			KeyValuePair<int, int> keyValuePair = this.skinIndexRangePerHero[id];
			for (int i = keyValuePair.Key; i < keyValuePair.Value; i++)
			{
				SkinData skinData = this.allSkins[i];
				if (skinData.isNew && skinData.IsUnlockRequirementSatisfied(this))
				{
					return true;
				}
			}
			return false;
		}

		public List<SkinData> GetBoughtSkins()
		{
			return this.boughtSkins;
		}

		public int GetBoughSkinsFromUnlockedHeroesCount()
		{
			int num = 0;
			for (int i = this.boughtSkins.Count - 1; i >= 0; i--)
			{
				if (this.unlockedHeroIds.Contains(this.boughtSkins[i].belongsTo.id))
				{
					num++;
				}
			}
			return num;
		}

		public void AddBoughtSkin(SkinData skinData)
		{
			if (!this.boughtSkins.Contains(skinData))
			{
				this.boughtSkins.Add(skinData);
				this.OnBoughtSkinAdded(skinData);
			}
		}

		private void OnBoughtSkinAdded(SkinData skinData)
		{
			FlashOffer flashOffer = (this.flashOfferBundle != null) ? this.flashOfferBundle.GetSkinOfferWithId(skinData.id) : null;
			if (flashOffer != null)
			{
				flashOffer.purchasesLeft = 0;
			}
			flashOffer = ((this.halloweenFlashOfferBundle != null) ? this.halloweenFlashOfferBundle.GetSkinOfferWithId(skinData.id) : null);
			if (flashOffer != null)
			{
				flashOffer.purchasesLeft = 0;
			}
		}

		public void UnlockAllHeroes()
		{
			this.unlockedHeroIds.Clear();
			foreach (HeroDataBase heroDataBase in this.allHeroes)
			{
				this.unlockedHeroIds.Add(heroDataBase.GetId());
			}
		}

		public HashSet<string> GetUnlockedHeroIds()
		{
			return this.unlockedHeroIds;
		}

		public void UnlockAllTotems()
		{
			this.unlockedTotemIds.Clear();
			foreach (TotemDataBase totemDataBase in this.allTotems)
			{
				this.unlockedTotemIds.Add(totemDataBase.id);
			}
		}

		public HashSet<string> GetUnlockedTotemIds()
		{
			return this.unlockedTotemIds;
		}

		public bool IsTotemUnlocked(string totemId)
		{
			return this.unlockedTotemIds.Contains(totemId);
		}

		public List<HeroDataBase> GetAvailableHeroes()
		{
			List<HeroDataBase> list = new List<HeroDataBase>();
			foreach (HeroDataBase heroDataBase in this.allHeroes)
			{
				if (!this.IsHeroBought(heroDataBase))
				{
					list.Add(heroDataBase);
				}
			}
			return list;
		}

		public HashSet<string> GetBoughtHeroIds()
		{
			this.boughtHeroIds.Clear();
			foreach (World world in this.allWorlds)
			{
				foreach (Hero hero in world.heroes)
				{
					HeroDataBase dataBase = hero.GetData().GetDataBase();
					this.boughtHeroIds.Add(dataBase.GetId());
				}
			}
			return this.boughtHeroIds;
		}

		public Dictionary<string, GameMode> GetBoughtHeroIdsWithModes()
		{
			this.boughtHeroIdsWithModes.Clear();
			foreach (World world in this.allWorlds)
			{
				foreach (Hero hero in world.heroes)
				{
					HeroDataBase dataBase = hero.GetData().GetDataBase();
					if (!this.boughtHeroIdsWithModes.ContainsKey(dataBase.GetId()))
					{
						this.boughtHeroIdsWithModes.Add(dataBase.GetId(), world.gameMode);
					}
				}
			}
			return this.boughtHeroIdsWithModes;
		}

		public HashSet<uint> GetCollectedUnlockIds()
		{
			HashSet<uint> hashSet = new HashSet<uint>();
			foreach (World world in this.allWorlds)
			{
				foreach (Unlock unlock in world.unlocks)
				{
					if (unlock.isCollected)
					{
						hashSet.Add(unlock.GetId());
					}
				}
			}
			return hashSet;
		}

		public List<Gear> GetBoughtGears()
		{
			return this.boughtGears;
		}

		public Gear GetBoughtGearFromData(GearData data)
		{
			return this.boughtGears.Find((Gear g) => g.data == data);
		}

		public List<Rune> GetBoughtRunes()
		{
			return this.boughtRunes;
		}

		public Dictionary<string, List<Rune>> GetWornRunes()
		{
			return this.wornRunes;
		}

		public List<Gear> GetHeroBoughtGears(Hero hero)
		{
			return this.GetHeroBoughtGears(hero.GetData().GetDataBase());
		}

		public List<Gear> GetHeroBoughtGears(HeroDataBase heroDataBase)
		{
			List<Gear> list = new List<Gear>();
			for (int i = 0; i < this.boughtGears.Count; i++)
			{
				Gear gear = this.boughtGears[i];
				if (heroDataBase == gear.data.belongsTo)
				{
					list.Add(gear);
				}
			}
			return list;
		}

		public List<GearData> GetHeroGears(HeroDataBase heroDataBase)
		{
			List<GearData> list = new List<GearData>();
			for (int i = 0; i < this.allGears.Count; i++)
			{
				GearData gearData = this.allGears[i];
				if (heroDataBase == gearData.belongsTo)
				{
					list.Add(gearData);
				}
			}
			return list;
		}

		public int GetSkillEnhancerGearLevel(SkillTreeNode skill)
		{
			for (int i = 0; i < this.boughtGears.Count; i++)
			{
				Gear gear = this.boughtGears[i];
				if (skill.GetType() == gear.data.skillToLevelUp.GetType())
				{
					return gear.level + 1;
				}
			}
			return 0;
		}

		public Dictionary<string, int> GetHeroNumBoughtGears()
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			foreach (HeroDataBase heroDataBase in this.allHeroes)
			{
				dictionary.Add(heroDataBase.GetId(), 0);
			}
			foreach (Gear gear in this.boughtGears)
			{
				Dictionary<string, int> dictionary2;
				string id;
				(dictionary2 = dictionary)[id = gear.data.belongsTo.GetId()] = dictionary2[id] + 1;
			}
			return dictionary;
		}

		public Dictionary<string, List<Rune>> GetAllTotemBoughtRunes()
		{
			Dictionary<string, List<Rune>> dictionary = new Dictionary<string, List<Rune>>();
			foreach (TotemDataBase totemDataBase in this.allTotems)
			{
				dictionary.Add(totemDataBase.id, new List<Rune>());
			}
			foreach (Rune rune in this.boughtRunes)
			{
				dictionary[rune.belongsTo.id].Add(rune);
			}
			return dictionary;
		}

		public GearData GetGearData(string id)
		{
			foreach (GearData gearData in this.allGears)
			{
				if (gearData.GetId() == id)
				{
					return gearData;
				}
			}
			UnityEngine.Debug.LogError("Gear with id : " + id + " is not found");
			throw new EntryPointNotFoundException();
		}

		public SkinData GetSkinData(int id)
		{
			foreach (SkinData skinData in this.allSkins)
			{
				if (skinData.id == id)
				{
					return skinData;
				}
			}
			throw new EntryPointNotFoundException("Skin with id : " + id + " is not found");
		}

		public bool IsSkinBought(SkinData skin)
		{
			return this.boughtSkins.Contains(skin);
		}

		public bool IsSkinBought(int skinId)
		{
			return this.boughtSkins.Exists((SkinData s) => s.id == skinId);
		}

		public Rune GetRune(string id)
		{
			foreach (Rune rune in this.allRunes)
			{
				if (rune.id == id)
				{
					return rune;
				}
			}
			throw new EntryPointNotFoundException("Rune with id : " + id + " is not found");
		}

		public bool IsRuneBought(Rune rune)
		{
			return this.boughtRunes.Contains(rune);
		}

		public bool IsRuneRewardedFromUnlocks(Rune rune)
		{
			return this.rewardedRunes.Contains(rune);
		}

		public bool IsRuneWorn(Rune rune)
		{
			return this.wornRunes[rune.belongsTo.id].Contains(rune);
		}

		public void BuyEvolveSkin(HeroDataBase db)
		{
			List<SkinData> list = this.allSkins.FindAll((SkinData s) => this.EvolveSkinPredicate(s, db));
			foreach (SkinData skinData in list)
			{
				if (skinData.CanAfford((double)db.evolveLevel))
				{
					this.AddBoughtSkin(skinData);
				}
			}
		}

		public List<SkinData> GetSkinDataToBeUnlocked(HeroDataBase db, int currentLevel, int nextLevel)
		{
			List<SkinData> heroSkins = this.GetHeroSkins(db.id);
			for (int i = heroSkins.Count - 1; i >= 0; i--)
			{
				SkinData skinData = heroSkins[i];
				if (skinData.WillSkinUnlockOnHeroLevel(currentLevel) || !skinData.WillSkinUnlockOnHeroLevel(nextLevel))
				{
					heroSkins.RemoveAt(i);
				}
			}
			return heroSkins;
		}

		private bool EvolveSkinPredicate(SkinData d, HeroDataBase db)
		{
			return d.unlockType == SkinData.UnlockType.HERO_EVOLVE_LEVEL && !this.boughtSkins.Contains(d) && d.belongsTo == db;
		}

		public void TrySwitchGameMode(GameMode mode)
		{
			if (this.IsModeUnlocked(mode))
			{
				this.SwitchGameMode(mode);
			}
		}

		public void SwitchGameMode(GameMode mode)
		{
			this.currentGameMode = mode;
			this.activeWorld = this.GetWorld(mode);
			SoundEventCancelAll e = new SoundEventCancelAll();
			this.activeWorld.AddSoundEvent(e);
			if (this.activeWorld.heroes.Count > 0)
			{
				Hero randomAliveHero = this.activeWorld.GetRandomAliveHero();
				if (randomAliveHero != null && (randomAliveHero.GetId() != "DRUID" || !randomAliveHero.IsUsingTempWeapon()))
				{
					this.activeWorld.GetRandomAliveHero();
					if (GameMath.GetProbabilityOutcome(0.5f, GameMath.RandType.NoSeed))
					{
						SoundEventSound e2 = new SoundEventSound(SoundType.GAMEPLAY, randomAliveHero.GetId(), true, 0f, randomAliveHero.soundVoWelcome);
						this.activeWorld.AddSoundEvent(e2);
					}
				}
			}
		}

		public bool IsTotemUsed(TotemDataBase totemDataBase)
		{
			foreach (World world in this.allWorlds)
			{
				if (world.totem != null && world.totem.GetData().GetDataBase() == totemDataBase)
				{
					return true;
				}
			}
			return false;
		}

		public bool IsHeroBought(HeroDataBase searchedHeroBase)
		{
			foreach (World world in this.allWorlds)
			{
				foreach (Hero hero in world.heroes)
				{
					if (hero.GetData().GetDataBase() == searchedHeroBase)
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool IsHeroUnlocked(string id)
		{
			return this.unlockedHeroIds.Contains(id);
		}

		public HeroDataBase GetRandomHeroUnlockedAndNotBought()
		{
			List<HeroDataBase> heroesAvailableToBuy = this.GetHeroesAvailableToBuy();
			if (heroesAvailableToBuy.Count > 0)
			{
				return heroesAvailableToBuy[GameMath.GetRandomInt(0, heroesAvailableToBuy.Count, GameMath.RandType.NoSeed)];
			}
			return null;
		}

		public List<HeroDataBase> GetHeroesAvailableToBuy()
		{
			List<HeroDataBase> list = new List<HeroDataBase>();
			foreach (HeroDataBase heroDataBase in this.allHeroes)
			{
				if (!this.IsHeroBought(heroDataBase) && this.unlockedHeroIds.Contains(heroDataBase.GetId()))
				{
					list.Add(heroDataBase);
				}
			}
			return list;
		}

		public TotemDataBase GetRandomTotemNotBought()
		{
			List<TotemDataBase> list = new List<TotemDataBase>();
			foreach (TotemDataBase totemDataBase in this.allTotems)
			{
				if (!this.IsTotemUsed(totemDataBase))
				{
					list.Add(totemDataBase);
				}
			}
			if (list.Count > 0)
			{
				return list[GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed)];
			}
			return null;
		}

		public bool TryBuyNewHero(string heroId)
		{
			HeroDataBase heroDataBase = this.GetHeroDataBase(heroId);
			return this.TryBuyNewHero(heroDataBase);
		}

		public bool TryBuyNewHero(HeroDataBase heroToBuy)
		{
			if (!this.unlockedHeroIds.Contains(heroToBuy.GetId()))
			{
				return false;
			}
			if (this.IsHeroBought(heroToBuy))
			{
				return false;
			}
			if (!this.activeWorld.CanAffordNewHero())
			{
				return false;
			}
			this.activeWorld.BuyNewHero(heroToBuy, this.boughtGears);
			return true;
		}

		public void UnlockAllGears()
		{
			using (List<GearData>.Enumerator enumerator = this.allGears.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					GearData item = enumerator.Current;
					if (!this.boughtGears.Exists((Gear g) => g.data == item))
					{
						Gear item2 = new Gear(item, 5);
						this.boughtGears.Add(item2);
					}
				}
			}
			this.OnGearsChanged();
		}

		public Gear AddRandomGear(int minLevel, bool isForFirstBoughtHero, List<GearData> toExclude, bool forceUndiscoveredGear, bool gearLevelCanUpgrade)
		{
            List<GearData> candidates = new List<GearData>();
			int i;
			if (isForFirstBoughtHero && this.activeWorld != null && this.activeWorld.heroes.Count > 0)
			{
				string id = this.activeWorld.heroes[0].GetId();
				foreach (GearData gearData in this.allGears)
				{
					if (gearData.belongsTo.id == id)
					{
						candidates.Add(gearData);
					}
				}
			}
			else
			{
				using (List<GearData>.Enumerator enumerator2 = this.allGears.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						GearData temp = enumerator2.Current;
						if (!forceUndiscoveredGear || this.boughtGears.Find((Gear gear) => gear.data.nameKey == temp.nameKey) == null)
						{
							candidates.Add(temp);
						}
					}
				}
			}
			if (toExclude != null)
			{
				foreach (GearData item in toExclude)
				{
					candidates.Remove(item);
				}
			}
			if (candidates.Count == 0)
			{
				candidates.Add(this.allGears[0]);
			}
			int num = minLevel;
			if (gearLevelCanUpgrade)
			{
				num = this.RandomGearLevel(minLevel);
			}
			double[] array = new double[candidates.Count];
			double num2 = 0.0;
			i = 0;
			int count = candidates.Count;
			while (i < count)
			{
				bool flag = false;
				int k = 0;
				int count2 = this.boughtGears.Count;
				while (k < count2)
				{
					if (this.boughtGears[k].data == candidates[i])
					{
						flag = true;
						break;
					}
					k++;
				}
				if (this.unlockedHeroIds.Contains(candidates[i].belongsTo.GetId()))
				{
					array[i] = (double)((!flag) ? 45f : 17f);
				}
				else
				{
					Unlock unlock = this.worldStandard.unlocks.Find(delegate(Unlock u)
					{
						UnlockRewardHero unlockRewardHero = u.GetReward() as UnlockRewardHero;
						return unlockRewardHero != null && unlockRewardHero.GetHeroId() == candidates[i].belongsTo.GetId();
					});
					bool flag2;
					if (unlock != null)
					{
						flag2 = (unlock.GetReqAmount() - this.worldStandard.GetMaxStageReached() <= 50);
					}
					else if (this.worldCrusade.IsModeUnlocked())
					{
						Unlock unlock2 = this.worldCrusade.unlocks.Find(delegate(Unlock u)
						{
							UnlockRewardHero unlockRewardHero = u.GetReward() as UnlockRewardHero;
							return unlockRewardHero != null && unlockRewardHero.GetHeroId() == candidates[i].belongsTo.GetId();
						});
						flag2 = (unlock2 != null && unlock2.GetReqAmount() - this.GetNumTimeChallengesComplete() <= 3);
					}
					else
					{
						flag2 = false;
					}
					if (flag2)
					{
						array[i] = (double)((!flag) ? 20f : 10f);
					}
					else
					{
						array[i] = (double)((!flag) ? 3f : 1f);
					}
				}
				num2 += array[i];
				i++;
			}
			int index = 0;
			double num3 = num2 * GameMath.GetRandomDouble(0.0, 1.0, GameMath.RandType.Lootpack);
			num2 = 0.0;
			int j = 0;
			int count3 = candidates.Count;
			while (j < count3)
			{
				num2 += array[j];
				if (num2 >= num3)
				{
					index = j;
					break;
				}
				j++;
			}
			GearData gearData2 = candidates[index];
			GearChange gearChange = new GearChange();
			gearChange.data = gearData2;
			gearChange.level = num;
			gearChange.scraps = 0.0;
			Gear gear3 = null;
			foreach (Gear gear2 in this.boughtGears)
			{
				if (gear2.data == gearData2)
				{
					gear3 = gear2;
					break;
				}
			}
			if (gear3 != null)
			{
				if (gear3.level >= num)
				{
					gearChange.levelOld = gear3.level;
					double x = Simulator.GEAR_CONVERT_TO_SCRAP_AMOUNTS[num];
					gearChange.scraps = x;
					this.scraps.Increment(x);
				}
				else
				{
					gearChange.levelOld = gear3.level;
					gear3.level = num;
					this.OnGearsChanged();
				}
			}
			else
			{
				gearChange.levelOld = -1;
				Gear item2 = new Gear(gearData2, num);
				this.boughtGears.Add(item2);
				this.OnGearsChanged();
			}
			if (this.lootDropGearChanges != null)
			{
				this.lootDropGearChanges.Add(gearChange);
			}
			return new Gear(gearData2, num);
		}

		public int RandomGearLevel(int minLevel = 0)
		{
			int num = minLevel;
			if (num == 0 && GameMath.GetProbabilityOutcome(PlayfabManager.titleData.gearLevelUpgradeChances[num], GameMath.RandType.Lootpack))
			{
				num++;
			}
			if (num == 1 && GameMath.GetProbabilityOutcome(PlayfabManager.titleData.gearLevelUpgradeChances[num], GameMath.RandType.Lootpack))
			{
				num++;
			}
			if (num == 2 && GameMath.GetProbabilityOutcome(PlayfabManager.titleData.gearLevelUpgradeChances[num], GameMath.RandType.Lootpack))
			{
				num++;
			}
			if (num == 3 && GameMath.GetProbabilityOutcome(PlayfabManager.titleData.gearLevelUpgradeChances[num], GameMath.RandType.Lootpack))
			{
				num++;
			}
			if (num == 4 && GameMath.GetProbabilityOutcome(PlayfabManager.titleData.gearLevelUpgradeChances[num], GameMath.RandType.Lootpack))
			{
				num++;
			}
			return num;
		}

		public void LoadTotem(TotemDataBase totemDataBase, World world)
		{
			world.LoadTotem(totemDataBase, this.wornRunes[totemDataBase.id]);
		}

		public void TryUseSkill(int skillIndex)
		{
			this.activeWorld.TryUseSkill(skillIndex);
		}

		public void TryUpgradeSkill(Hero hero, int branchIndex, int skillIndex)
		{
			this.activeWorld.TryUpgradeSkill(hero, branchIndex, skillIndex);
		}

		public bool CanUpgradeSkill(string heroId, int branchIndex, int skillIndex)
		{
			return this.activeWorld.CanUpgradeSkill(heroId, branchIndex, skillIndex);
		}

		public void TryUpgradeGear(Gear gear)
		{
			if (this.CanUpgradeGear(gear))
			{
				double num = Simulator.GEAR_LEVEL_UP_COSTS[gear.level];
				this.scraps.Decrement(num);
				gear.level++;
				this.OnGearsChanged();
				this.activeWorld.shouldSave = true;
				HeroDataBase belongsTo = gear.data.belongsTo;
				PlayfabManager.SendPlayerEvent(PlayfabEventId.HERO_ITEM_UPGRADED, new Dictionary<string, object>
				{
					{
						"hero",
						belongsTo.GetId()
					},
					{
						"type",
						gear.data.universalBonusType
					},
					{
						"rarity",
						gear.level
					},
					{
						"spent_currency",
						CurrencyType.SCRAP
					},
					{
						"spent_amount",
						num
					}
				}, null, null, true);
				TutorialManager.UpgradedGear();
			}
		}

		public bool CanUpgradeGear(Gear gear)
		{
			return this.scraps.CanAfford(Simulator.GEAR_LEVEL_UP_COSTS[gear.level]) && !gear.IsMaxLevel();
		}

		public double GetGearPrice(Gear gear)
		{
			return Simulator.GEAR_LEVEL_UP_COSTS[gear.level];
		}

		public void TryUpgradeTotem()
		{
			this.activeWorld.TryUpgradeTotem();
		}

		public void TryUpgradeHeroWithIndex(int index)
		{
			this.activeWorld.TryUpgradeHeroWithIndex(index);
		}

		public double GetHeroEvolveCost(HeroDataBase hero)
		{
			int evolveLevel = hero.evolveLevel;
			return HeroDataBase.EVOLVE_COSTS[evolveLevel];
		}

		public bool GetHeroEvolvability(HeroDataBase hero)
		{
			return this.CanAffordHeroEvolve(hero) && this.CanHeroEvolve(hero);
		}

		public bool CanAffordHeroEvolve(HeroDataBase hero)
		{
			double heroEvolveCost = this.GetHeroEvolveCost(hero);
			return this.scraps.CanAfford(heroEvolveCost);
		}

		public bool CanHeroEvolve(HeroDataBase hero)
		{
			return hero.CanEvolve(this.boughtGears);
		}

		public bool GetHeroEvolveMaxedOut(HeroDataBase hero)
		{
			int evolveLevel = hero.evolveLevel;
			return evolveLevel == 6;
		}

		public void TryEvolveHero(string heroId)
		{
			foreach (World world in this.allWorlds)
			{
				foreach (Hero hero in this.activeWorld.heroes)
				{
					if (hero.GetId() == heroId)
					{
						this.TryEvolveBoughtHero(hero);
						return;
					}
				}
			}
			foreach (HeroDataBase heroDataBase in this.allHeroes)
			{
				if (heroDataBase.GetId() == heroId)
				{
					this.TryEvolveUnboughtHero(heroDataBase);
					return;
				}
			}
			throw new EntryPointNotFoundException();
		}

		public void TryEvolveBoughtHero(Hero hero)
		{
			HeroDataBase dataBase = hero.GetData().GetDataBase();
			if (!this.GetHeroEvolvability(dataBase))
			{
				return;
			}
			double heroEvolveCost = this.GetHeroEvolveCost(dataBase);
			this.scraps.Decrement(heroEvolveCost);
			hero.Evolve();
			this.BuyEvolveSkin(dataBase);
			TutorialManager.EvolvedHero();
			PlayerStats.OnHeroEvolve(this.allHeroes);
			PlayfabManager.SendPlayerEvent(PlayfabEventId.HERO_EVOLVED, new Dictionary<string, object>
			{
				{
					"hero",
					hero.GetId()
				},
				{
					"rarity",
					hero.GetEvolveLevel()
				},
				{
					"spent_currency",
					CurrencyType.SCRAP
				},
				{
					"spent_amount",
					heroEvolveCost
				}
			}, null, null, true);
		}

		public void TryEvolveUnboughtHero(HeroDataBase heroBase)
		{
			if (!this.GetHeroEvolvability(heroBase))
			{
				return;
			}
			double heroEvolveCost = this.GetHeroEvolveCost(heroBase);
			this.scraps.Decrement(heroEvolveCost);
			heroBase.evolveLevel++;
			this.BuyEvolveSkin(heroBase);
			TutorialManager.EvolvedHero();
			PlayerStats.OnHeroEvolve(this.allHeroes);
			PlayfabManager.SendPlayerEvent(PlayfabEventId.HERO_EVOLVED, new Dictionary<string, object>
			{
				{
					"hero",
					heroBase.GetId()
				},
				{
					"rarity",
					heroBase.evolveLevel
				},
				{
					"spent_currency",
					CurrencyType.SCRAP
				},
				{
					"spent_amount",
					heroEvolveCost
				}
			}, null, null, true);
		}

		public void TryLeaveBoss()
		{
			this.activeWorld.TryLeaveBoss();
		}

		public void TryGoToBoss()
		{
			this.activeWorld.TryGoToBoss();
		}

		public void TryPrestige(bool isMega)
		{
			int stageNumber = this.worldStandard.GetStageNumber();
			Totem totem = this.worldStandard.totem;
			int totalArtifactsLevel = this.artifactsManager.TotalArtifactsLevel;
			if ((!isMega || this.CanMegaPrestige()) && this.activeWorld.TryPrestige(isMega))
			{
				if (isMega)
				{
					this.credits.Decrement(100.0);
				}
				this.numPrestiges++;
				this.numPrestigesSinceCataclysm++;
				if (stageNumber > this.maxStagePrestigedAt)
				{
					this.maxStagePrestigedAt = stageNumber;
					StoreManager.ReportMaxStage(this.maxStagePrestigedAt);
				}
				PlayerStats.OnPrestige(this.numPrestiges);
				this.lastPrestigeRunstats = new PrestigeRunStat
				{
					playTime = this.prestigeRunTimer,
					stage = this.activeWorld.GetStageNumber(),
					mythStones = this.activeWorld.GetNumMythstonesOnPrestige(false),
					wasMega = isMega
				};
				this.prestigeRunTimer = 0.0;
				this.OnPrestige();
				List<string> list = new List<string>();
				foreach (Hero hero in this.worldStandard.heroes)
				{
					list.Add(hero.GetId());
				}
				list.Sort();
				PlayfabManager.SendPlayerEvent(PlayfabEventId.PRESTIGE_V2, new Dictionary<string, object>
				{
					{
						"stage",
						stageNumber
					},
					{
						"ring",
						totem.GetId()
					},
					{
						"heroes_used",
						list
					},
					{
						"tal",
						totalArtifactsLevel
					},
					{
						"isDouble",
						isMega
					}
				}, null, null, true);
			}
		}

		private void OnPrestige()
		{
			this.maxStageReachedInCurrentAdventure = false;
			if (this.questOfUpdate != null)
			{
				this.questOfUpdate.OnPrestige();
			}
			if (this.IsSecondAnniversaryEventEnabled())
			{
				this.prestigedDuringSecondAnniversaryEvent = true;
			}
		}

		public bool CanMegaPrestige()
		{
			return this.IsMegaPrestigeUnlocked() && this.CanAffordMegaPrestige();
		}

		public bool IsMegaPrestigeUnlocked()
		{
			return this.numPrestiges >= 3;
		}

		public bool CanAffordMegaPrestige()
		{
			return this.credits.CanAfford(100.0);
		}

		public double GetNumMythstonesOnPrestige(bool isMega)
		{
			return this.activeWorld.GetNumMythstonesOnPrestige(isMega);
		}

		public double GetNumMythstonesOnPrestigePure()
		{
			return this.activeWorld.GetNumMythstonesOnPrestigePure();
		}

		public double GetNumMythstonesOnPrestigeArtifactBonus()
		{
			return this.activeWorld.GetNumMythstonesOnPrestigeArtifactBonus();
		}

		public void DEBUGchangeStage(int numStageChange)
		{
			this.activeWorld.DEBUGchangeStage(numStageChange);
		}

		public List<Trinket> GetSortedTrinkets()
		{
			List<Trinket> list = new List<Trinket>(this.allTrinkets);
			list.BubbleSort(this.trinketComparer.GetComparer(this.trinketSortType, this.isTrinketSortingDescending));
			return list;
		}

		public bool HasMythicalTabHint()
		{
			return this.worldStandard.GetMaxStageReached() >= 485;
		}

		public Unlock GetNextMythicalSlotUnlock()
		{
			int i = 0;
			int count = this.worldStandard.unlocks.Count;
			while (i < count)
			{
				Unlock unlock = this.worldStandard.unlocks[i];
				if (!unlock.isCollected && unlock.HasRewardOfType(typeof(UnlockRewardMythicalArtifactSlot)))
				{
					return unlock;
				}
				i++;
			}
			return null;
		}

		public int GetNumCollectableUnlocks(World world)
		{
			int num = 0;
			foreach (Unlock unlock in world.unlocks)
			{
				if (!unlock.isCollected && unlock.IsReqSatisfied(this))
				{
					num++;
				}
			}
			return num;
		}

		public bool CanCollectUnlockReward(Unlock unlock)
		{
			return !unlock.isCollected && unlock.IsReqSatisfied(this);
		}

		public void TryCollectUnlockReward(Unlock unlock)
		{
			if (!unlock.isCollected && unlock.IsReqSatisfied(this))
			{
				unlock.GiveReward(this);
				this.activeWorld.shouldSoftSave = true;
				TutorialManager.UnlockCollected();
				if (unlock.GetId() == UnlockIds.RING_ICE || unlock.GetId() == UnlockIds.HERO_SAM)
				{
					this.shouldAskForRate = true;
				}
			}
		}

		public int GetNumFreeLootpacks()
		{
			if (TutorialManager.shopTab < TutorialManager.ShopTab.LOOTPACK_OPENED)
			{
				return 1;
			}
			if (TrustedTime.IsReady())
			{
				double num = GameMath.DeltaTimeInSecs(TrustedTime.Get(), this.lootpackFreeLastOpenTimeServer);
				return (int)Math.Min(1.0, Math.Floor(num / (double)this.GetFreeLootpackPeriod()));
			}
			return 0;
		}

		public double GetTimeForNextFreeLootpack()
		{
			if (this.GetNumFreeLootpacks() >= 1)
			{
				return -1.0;
			}
			if (TrustedTime.IsReady())
			{
				double num = GameMath.DeltaTimeInSecs(TrustedTime.Get(), this.lootpackFreeLastOpenTimeServer);
				double num2 = (double)this.GetFreeLootpackPeriod();
				return num2 - num % num2;
			}
			return -2.0;
		}

		public bool CanOpenLootpack(ShopPack pack)
		{
			if (pack is ShopPackTrinket)
			{
				throw new NotImplementedException(pack.ToString());
			}
			bool flag = true;
			if (!pack.isIAP)
			{
				flag = this.CanCurrencyAfford(pack.currency, pack.cost);
			}
			if (pack is ShopPackLootpackFree)
			{
				return this.GetNumFreeLootpacks() > 0 && flag;
			}
			return flag;
		}

		public bool CanCurrencyAfford(CurrencyType currencyType, double cost)
		{
			switch (currencyType)
			{
			case CurrencyType.GOLD:
				return this.activeWorld.gold.CanAfford(cost);
			case CurrencyType.SCRAP:
				return this.scraps.CanAfford(cost);
			case CurrencyType.MYTHSTONE:
				return this.mythstones.CanAfford(cost);
			case CurrencyType.GEM:
				return this.credits.CanAfford(cost);
			case CurrencyType.TOKEN:
				return this.tokens.CanAfford(cost);
			case CurrencyType.AEON:
				return this.aeons.CanAfford(cost);
			default:
				throw new NotImplementedException();
			}
		}

		public bool CanOpenTrinketPack(ShopPackTrinket pack, bool isSpecial)
		{
			bool flag;
			if (!isSpecial)
			{
				flag = this.CanCurrencyAfford(pack.currency, pack.cost);
			}
			else
			{
				flag = this.CanCurrencyAfford(pack.specialCurrency, pack.specialCost);
			}
			return this.HasEmptyTrinketSlot() && (flag || this.numTrinketPacks > 0);
		}

		public bool HasEmptyTrinketSlot()
		{
			return this.allTrinkets.Count < this.numTrinketSlots;
		}

		public bool HasAnyTrinket()
		{
			return this.allTrinkets.Count > 0;
		}

		public bool HasTrinketTabHint()
		{
			return this.worldStandard.GetMaxStageReached() >= 280;
		}

		public Dictionary<string, List<Gear>> GetAllHeroesBoughtGearsCopy()
		{
			Dictionary<string, List<Gear>> dictionary = new Dictionary<string, List<Gear>>();
			foreach (HeroDataBase heroDataBase in this.allHeroes)
			{
				dictionary.Add(heroDataBase.GetId(), new List<Gear>());
			}
			foreach (Gear gear in this.boughtGears)
			{
				dictionary[gear.data.belongsTo.id].Add(gear.GetCopy());
			}
			return dictionary;
		}

		public void TryOpenLootpack(ShopPack pack)
		{
			if (this.CanOpenLootpack(pack))
			{
				string name = pack.GetType().Name;
				if (!this.lootpacksOpenedCount.ContainsKey(name))
				{
					this.lootpacksOpenedCount[name] = 0;
				}
				Dictionary<string, int> dictionary;
				string key;
				this.OpenLootpack(pack, (dictionary = this.lootpacksOpenedCount)[key = name] = dictionary[key] + 1);
				if (pack is ShopPackLootpackFree || pack is ShopPackLootpackRare || pack is ShopPackLootpackEpic)
				{
					this.amountLootPacksOpenedForHint++;
				}
				this.activeWorld.shouldSave = true;
				TutorialManager.OpenedLootpack(pack);
				if (pack is ShopPackLootpackFree)
				{
					int num = this.lootpacksOpenedCount[pack.GetType().Name];
					PlayfabManager.SendPlayerEvent(PlayfabEventId.LOOTPACK_OPEN_FREE, new Dictionary<string, object>
					{
						{
							"num_total",
							num
						}
					}, null, null, true);
				}
				else if (pack is ShopPackLootpackRare || pack is ShopPackLootpackEpic)
				{
					int num2 = this.lootpacksOpenedCount[pack.GetType().Name];
					string value = (!(pack is ShopPackLootpackRare)) ? "EPIC" : "RARE";
					double cost = pack.cost;
					PlayfabManager.SendPlayerEvent(PlayfabEventId.LOOTPACK_OPEN_GEM, new Dictionary<string, object>
					{
						{
							"rarity",
							value
						},
						{
							"spent_currency",
							pack.currency
						},
						{
							"spent_amount",
							cost
						}
					}, null, null, true);
				}
				PlayerStats.OpenedLootpack(pack);
			}
		}

		public int GetTotalAmountLootpacksOpened()
		{
			int num = 0;
			foreach (int num2 in this.lootpacksOpenedCount.Values)
			{
				num += num2;
			}
			return num;
		}

		public int GetTotalFreeLootpackOpened()
		{
			string name = this.lootpacks[0].GetType().Name;
			if (this.lootpacksOpenedCount.ContainsKey(name))
			{
				return this.lootpacksOpenedCount[name];
			}
			return 0;
		}

		public int GetTotalPremiumLootpackOpened()
		{
			string name = this.lootpacks[1].GetType().Name;
			string name2 = this.lootpacks[2].GetType().Name;
			int num = 0;
			if (this.lootpacksOpenedCount.ContainsKey(name))
			{
				num += this.lootpacksOpenedCount[name];
			}
			if (this.lootpacksOpenedCount.ContainsKey(name2))
			{
				num += this.lootpacksOpenedCount[name2];
			}
			return num;
		}

		public void TryOpenTrinketPack(ShopPackTrinket pack, bool isSpecial)
		{
			if (this.CanOpenTrinketPack(pack, isSpecial))
			{
				this.OpenTrinketPack(pack, isSpecial);
				this.activeWorld.shouldSave = true;
				TutorialManager.OpenedLootpack(pack);
				PlayerStats.OpenedLootpack(pack);
			}
		}

		public void ConsumeCurrency(CurrencyType currencyType, double cost)
		{
			switch (currencyType)
			{
			case CurrencyType.GOLD:
				this.activeWorld.gold.Decrement(cost);
				break;
			case CurrencyType.SCRAP:
				this.scraps.Decrement(cost);
				break;
			case CurrencyType.MYTHSTONE:
				this.mythstones.Decrement(cost);
				break;
			case CurrencyType.GEM:
				this.credits.Decrement(cost);
				break;
			case CurrencyType.TOKEN:
				this.tokens.Decrement(cost);
				break;
			case CurrencyType.AEON:
				this.aeons.Decrement(cost);
				break;
			default:
				throw new NotImplementedException();
			}
		}

		private void OpenTrinketPack(ShopPackTrinket pack, bool isSpecial)
		{
			if (this.numTrinketPacks > 0)
			{
				PlayerStats.totalTrinketPackWithGemOrAeon++;
				this.numTrinketPacks--;
			}
			else if (isSpecial)
			{
				PlayerStats.totalTrinketPackWithGemOrAeon++;
				this.ConsumeCurrency(pack.specialCurrency, pack.specialCost);
			}
			else
			{
				PlayerStats.totalTrinketPackWithGemOrAeon++;
				PlayerStats.totalTrinketPackWithGem++;
				this.ConsumeCurrency(pack.currency, pack.cost);
				PlayfabManager.SendPlayerEvent(PlayfabEventId.TRINKET_PURCHASED_GEM, new Dictionary<string, object>
				{
					{
						"num_total_all",
						PlayerStats.totalTrinketPackWithGemOrAeon
					},
					{
						"num_total_gem",
						PlayerStats.totalTrinketPackWithGem
					},
					{
						"spent_currency",
						pack.currency
					},
					{
						"spent_amount",
						pack.cost
					}
				}, null, null, true);
			}
			this.lootDropRunes = new List<Rune>();
			this.lootDropGears = new List<Gear>();
			this.lootDropTrinkets = new List<Trinket>();
			for (int i = 0; i < pack.numTrinkets; i++)
			{
				if (this.allTrinkets.Count >= this.numTrinketSlots)
				{
					break;
				}
				Trinket random = Trinket.GetRandom(++this.numTrinketsObtained);
				this.hasEverOwnedATrinket = true;
				this.allTrinkets.Add(random);
				this.lootDropTrinkets.Add(random);
			}
		}

		private void OpenLootpack(ShopPack pack, int openedPacksCount)
		{
			if (pack is ShopPackTrinket)
			{
				throw new NotImplementedException(pack.ToString());
			}
			if (pack is ShopPackLootpackFree)
			{
				float freeLootpackPeriod = this.GetFreeLootpackPeriod();
				if (this.GetNumFreeLootpacks() == 1)
				{
					this.lootpackFreeLastOpenTime = GameMath.GetNow().AddSeconds((double)(0f * freeLootpackPeriod));
					if (TrustedTime.IsReady())
					{
						this.lootpackFreeLastOpenTimeServer = TrustedTime.Get().AddSeconds((double)(0f * freeLootpackPeriod));
					}
				}
				else
				{
					this.lootpackFreeLastOpenTime = this.lootpackFreeLastOpenTime.AddSeconds((double)freeLootpackPeriod);
					this.lootpackFreeLastOpenTimeServer = this.lootpackFreeLastOpenTimeServer.AddSeconds((double)freeLootpackPeriod);
				}
			}
			if (!pack.isIAP)
			{
				this.ConsumeCurrency(pack.currency, pack.cost);
			}
			if (pack is ShopPackThreePijama)
			{
				this.UnlockSkin(HeroSkins.VEXX_PIJAMA);
				this.UnlockSkin(HeroSkins.HILT_PIJAMA);
				this.UnlockSkin(HeroSkins.BELLY_PIJAMA);
				SkinData skinData = this.GetSkinData(HeroSkins.VEXX_PIJAMA);
				SkinData skinData2 = this.GetSkinData(HeroSkins.HILT_PIJAMA);
				SkinData skinData3 = this.GetSkinData(HeroSkins.BELLY_PIJAMA);
				PlayerStats.OnUserBuySkin(new string[]
				{
					skinData.GetKey(),
					skinData2.GetKey(),
					skinData3.GetKey()
				});
			}
			this.creditsBeforeOpeningLootpack = this.credits.GetAmount();
			this.scrapsBeforeOpeningLootpack = this.scraps.GetAmount();
			this.tokensBeforeOpeningLootpack = this.tokens.GetAmount();
			this.lootDropRunes = new List<Rune>();
			bool flag = false;
			for (int i = 0; i < pack.numRunes; i++)
			{
				if ((pack.runeAssuredFrequency > 0 && openedPacksCount % pack.runeAssuredFrequency == 0) || GameMath.GetProbabilityOutcome(pack.runeChance, GameMath.RandType.Lootpack))
				{
					List<Rune> list = new List<Rune>();
					foreach (Rune rune in this.allRunes)
					{
						if (!this.IsRuneBought(rune) && !this.IsRuneRewardedFromUnlocks(rune))
						{
							list.Add(rune);
						}
					}
					if (list.Count > 0)
					{
						Rune newRune = this.SelectRandomRune(list);
						this.lootDropRunes.Add(newRune);
						this.boughtRunes.Add(newRune);
						PlayerStats.OnRuneBought(this.boughtRunes.Count);
						if (this.flashOfferBundle != null && this.flashOfferBundle.adventureOffers != null)
						{
							FlashOffer flashOffer = this.flashOfferBundle.adventureOffers.Find((FlashOffer o) => o.type == FlashOffer.Type.RUNE && o.genericStringId == newRune.id);
							if (flashOffer != null)
							{
								flashOffer.purchasesLeft = 0;
							}
						}
						UnityEngine.Debug.Log("Got rune: " + LM.Get(newRune.nameKey));
					}
				}
			}
			this.lootDropGearChanges = new List<GearChange>();
			this.boughtHeroesCosmetic = this.GetAllHeroesBoughtGearsCopy();
			this.lootDropGears = new List<Gear>();
			if (pack.numGears != null)
			{
				List<GearData> list2 = new List<GearData>();
				bool flag2 = true;
				bool flag3 = pack.undiscoveredGearAssuredFrequency > 0 && openedPacksCount % pack.undiscoveredGearAssuredFrequency == 0;
				int j = 0;
				int num = pack.numGears.Length;
				while (j < num)
				{
					int num2 = pack.numGears[j];
					if (num2 > 0 && pack is ShopPackLootpackFree)
					{
						num2 += this.universalTotalBonus.heroItemsInFreeChestAdd;
					}
					int k = 0;
					int num3 = num2;
					while (k < num3)
					{
						if (flag)
						{
							flag = false;
						}
						else
						{
							Gear gear = this.AddRandomGear(j, flag2 && TutorialManager.ShouldLootpackRewardGearForFirstBoughtHero(), list2, flag3 && j == 0 && k == 0, openedPacksCount > 2);
							flag2 = false;
							this.lootDropGears.Add(gear);
							list2.Add(gear.data);
						}
						k++;
					}
					j++;
				}
			}
			double num4 = (!(pack is ShopPackLootpackFree)) ? 1.0 : this.universalTotalBonus.currencyInFreeChestFactor;
			this.lootDropCredits = pack.credits * num4;
			this.lootDropTokens = GameMath.GetRandomDouble(pack.tokensMin, pack.tokensMax, GameMath.RandType.Lootpack) * num4;
			this.lootDropScraps = GameMath.GetRandomDouble(pack.scrapsMin, pack.scrapsMax, GameMath.RandType.Lootpack) * num4;
			this.credits.Increment(this.lootDropCredits);
			this.tokens.Increment(this.lootDropTokens);
			this.scraps.Increment(this.lootDropScraps);
			this.lootDropTrinkets = new List<Trinket>();
			for (int l = 0; l < pack.numTrinkets; l++)
			{
				if (this.allTrinkets.Count >= this.numTrinketSlots)
				{
					break;
				}
				Trinket random = Trinket.GetRandom(++this.numTrinketsObtained);
				this.hasEverOwnedATrinket = true;
				this.allTrinkets.Add(random);
				this.lootDropTrinkets.Add(random);
			}
			foreach (World world in this.allWorlds)
			{
				foreach (Hero hero in world.heroes)
				{
					hero.OnGearsChanged();
				}
			}
			if (pack is ShopPackCharmPackSmall)
			{
				ShopPackCharmPackSmall shopPackCharmPackSmall = pack as ShopPackCharmPackSmall;
				this.OpenRandomCardPack(shopPackCharmPackSmall.numCharms);
				foreach (KeyValuePair<int, CharmEffectData> keyValuePair in this.allCharmEffects)
				{
					CharmEffectData value = keyValuePair.Value;
					if (value.level >= 0)
					{
						value.SpendDuplicated();
					}
				}
			}
		}

		public List<Rune> GetAllBuyableRunes()
		{
			List<Rune> list = new List<Rune>();
			foreach (Rune rune in this.allRunes)
			{
				if (!this.IsRuneBought(rune) && !this.IsRuneRewardedFromUnlocks(rune))
				{
					list.Add(rune);
				}
			}
			return list;
		}

		public void ResetShopPackOffer()
		{
			if (this.selectedSpecialOfferKeeper == null || !this.selectedSpecialOfferKeeper.IsAlive())
			{
				return;
			}
			if (TrustedTime.IsReady())
			{
				this.specialOfferBoard.KillOffer(this.selectedSpecialOfferKeeper, TrustedTime.Get());
			}
			else
			{
				this.specialOfferBoard.KillOffer(this.selectedSpecialOfferKeeper, this.selectedSpecialOfferKeeper.offerEndTime);
			}
			UiManager.stateJustChanged = true;
		}

		private Rune SelectRandomRune(List<Rune> candidates)
		{
			float num = 0f;
			foreach (Rune rune in candidates)
			{
				num += rune.lootpackChance;
			}
			float num2 = GameMath.GetRandomFloat(0f, num, GameMath.RandType.Lootpack);
			foreach (Rune rune2 in candidates)
			{
				num2 -= rune2.lootpackChance;
				if (num2 <= 0f)
				{
					return rune2;
				}
			}
			int index = 0;
			for (int i = candidates.Count - 1; i > 0; i--)
			{
				if (candidates[i].lootpackChance > candidates[index].lootpackChance)
				{
					index = i;
				}
			}
			return candidates[index];
		}

		public bool CanAnyRuneDropFromLootpack()
		{
			foreach (Rune rune in this.allRunes)
			{
				if (!this.IsRuneBought(rune) && !this.IsRuneRewardedFromUnlocks(rune) && this.IsTotemUnlocked(rune.belongsTo.id))
				{
					return true;
				}
			}
			return false;
		}

		public string GetLootpackSelectDescription(ShopPack pack, LootType lootType)
		{
			double num = (!(pack is ShopPackLootpackFree)) ? 1.0 : this.universalTotalBonus.currencyInFreeChestFactor;
			if (lootType == LootType.TOKENS)
			{
				if (pack.tokensMin == pack.tokensMax)
				{
					return GameMath.GetDoubleString(pack.tokensMin * num);
				}
				return GameMath.GetDoubleString(pack.tokensMin * num) + " - " + GameMath.GetDoubleString(pack.tokensMax * num);
			}
			else if (lootType == LootType.SCRAPS)
			{
				if (pack.scrapsMin == pack.scrapsMax)
				{
					return GameMath.GetDoubleString(pack.scrapsMin * num);
				}
				return GameMath.GetDoubleString(pack.scrapsMin * num) + " - " + GameMath.GetDoubleString(pack.scrapsMax * num);
			}
			else
			{
				if (lootType == LootType.CREDITS)
				{
					return GameMath.GetDoubleString(pack.credits * num);
				}
				if (lootType == LootType.CANDIES)
				{
					return GameMath.GetDoubleString(pack.candies * num);
				}
				if (lootType == LootType.GEAR)
				{
					return this.GetLootpackAtLeastString(pack);
				}
				if (lootType != LootType.RUNE)
				{
					throw new NotImplementedException();
				}
				if (pack.runeChance >= 1f)
				{
					return string.Empty;
				}
				return GameMath.GetPercentString(pack.runeChance, true) + " <size=24>" + LM.Get("UI_DROP_CHANCE") + "</size>";
			}
		}

		private string GetLootpackAtLeastString(ShopPack pack)
		{
			string result = string.Empty;
			int lootpackMaxGearLevel = pack.GetLootpackMaxGearLevel();
			int num = pack.numGears[lootpackMaxGearLevel];
			if (lootpackMaxGearLevel > 0)
			{
				string colorString = GameMath.GetColorString(UiManager.colorHeroLevels[lootpackMaxGearLevel]);
				result = string.Concat(new string[]
				{
					"<size=24>",
					LM.Get("UI_SHOP_AT_LEAST"),
					"  </size>",
					num.ToString(),
					" <color=",
					colorString,
					">",
					UiManager.GearLevelString(lootpackMaxGearLevel),
					"</color>"
				});
			}
			return result;
		}

		public bool WillLootpackDropLootType(ShopPack pack, LootType lootType)
		{
			if (lootType == LootType.TOKENS)
			{
				return pack.tokensMax > 0.0;
			}
			if (lootType == LootType.SCRAPS)
			{
				return pack.scrapsMax > 0.0;
			}
			if (lootType == LootType.CREDITS)
			{
				return pack.credits > 0.0;
			}
			if (lootType == LootType.CANDIES)
			{
				return pack.candies > 0.0;
			}
			if (lootType == LootType.GEAR)
			{
				return pack.GetNumGears(this.universalTotalBonus) > 0;
			}
			if (lootType == LootType.RUNE)
			{
				return pack.numRunes > 0;
			}
			if (lootType == LootType.TRINKET)
			{
				return pack.numTrinkets > 0;
			}
			if (lootType == LootType.TRINKET_BOX)
			{
				return pack.numTrinketPacks > 0;
			}
			if (lootType == LootType.SKINS)
			{
				return pack is ShopPackThreePijama;
			}
			throw new NotImplementedException();
		}

		public void ZeroUltiCooldowns()
		{
			this.activeWorld.ZeroUltiCooldowns();
		}

		public void ZeroSkillCooldowns()
		{
			this.activeWorld.ZeroSkillCooldowns();
		}

		public Unlock GetUnlock(uint id)
		{
			foreach (World world in this.allWorlds)
			{
				foreach (Unlock unlock in world.unlocks)
				{
					if (unlock.GetId() == id)
					{
						return unlock;
					}
				}
			}
			foreach (Unlock unlock2 in this.oldUnlocks)
			{
				if (unlock2.GetId() == id)
				{
					return unlock2;
				}
			}
			throw new NotImplementedException();
		}

		public Unlock GetNextUnhiddenUnlock()
		{
			return this.activeWorld.GetNextUnhiddenUnlock();
		}

		public int GetNextUnlockStage()
		{
			return this.activeWorld.GetNextUnlockStage();
		}

		public int GetMaxStageReached()
		{
			return this.activeWorld.GetMaxStageReached();
		}

		public int GetCurrentStage(GameMode mode)
		{
			return this.GetWorld(mode).GetStageNumber();
		}

		public int GetStandardMaxStageReached()
		{
			return this.worldStandard.GetMaxStageReached();
		}

		public int GetMaxHeroLevelReached()
		{
			return this.activeWorld.GetMaxHeroLevelReached();
		}

		public int GetMaxNumOfUnlocks()
		{
			int[] array = new int[3];
			array[0] = this.worldStandard.GetUnlocks().Count - this.worldStandard.GetUnlocks().FindAll((Unlock u) => u.isHidden).Count;
			array[1] = this.worldCrusade.GetUnlocks().Count - this.worldCrusade.GetUnlocks().FindAll((Unlock u) => u.isHidden).Count;
			array[2] = this.worldRift.GetUnlocks().Count - this.worldRift.GetUnlocks().FindAll((Unlock u) => u.isHidden).Count;
			return Mathf.Max(array);
		}

		public void UnlockTotem(string totemId)
		{
			this.unlockedTotemIds.Add(totemId);
		}

		public void UnlockHero(string heroId)
		{
			this.unlockedHeroIds.Add(heroId);
		}

		public void GainCurrency(CurrencyType currencyType, double amountOfCurrency)
		{
			if (currencyType == CurrencyType.GOLD)
			{
				this.activeWorld.gold.Increment(amountOfCurrency);
			}
			else if (currencyType == CurrencyType.SCRAP)
			{
				this.scraps.Increment(amountOfCurrency);
			}
			else if (currencyType == CurrencyType.MYTHSTONE)
			{
				this.mythstones.Increment(amountOfCurrency);
			}
			else if (currencyType == CurrencyType.GEM)
			{
				this.credits.Increment(amountOfCurrency);
			}
			else if (currencyType == CurrencyType.TOKEN)
			{
				this.tokens.Increment(amountOfCurrency);
			}
			else
			{
				if (currencyType != CurrencyType.AEON)
				{
					throw new NotImplementedException();
				}
				this.aeons.Increment(amountOfCurrency);
			}
		}

		public World GetWorld(GameMode mode)
		{
			if (mode == GameMode.STANDARD)
			{
				return this.worldStandard;
			}
			if (mode == GameMode.CRUSADE)
			{
				return this.worldCrusade;
			}
			if (mode == GameMode.RIFT)
			{
				return this.worldRift;
			}
			throw new NotImplementedException();
		}

		public List<SoundEvent> GetActiveWorldSounds()
		{
			return this.activeWorld.sounds;
		}

		public void ClearSounds()
		{
			foreach (World world in this.allWorlds)
			{
				world.sounds.Clear();
			}
		}

		public bool IsMerchantUnlocked()
		{
			return this.isMerchantUnlocked;
		}

		public void IsMerchantUnlocked(bool isMerchantUnlocked)
		{
			this.isMerchantUnlocked = isMerchantUnlocked;
		}

		public void UnlockMerchant()
		{
			this.isMerchantUnlocked = true;
		}

		public void UnlockAllRifts()
		{
			this.riftDiscoveryIndex = this.maxRiftDiscoveryIndex;
			foreach (Unlock unlock in this.worldRift.unlocks)
			{
				unlock.GiveReward(this);
			}
			this.worldRift.CacheLatestBeatenRiftChallengeIndex();
		}

		public bool IsRiftUnlocked()
		{
			return this.worldRift.IsModeUnlocked();
		}

		public bool HasRiftHint()
		{
			return this.worldStandard.GetMaxStageReached() >= 250;
		}

		public MerchantItem GetMerchantItem(int index)
		{
			return this.activeWorld.merchantItems[index];
		}

		public MerchantItem GetEventMerchantItem(int index)
		{
			return this.activeWorld.eventMerchantItems[index];
		}

		public List<MerchantItem> GetMerchantItems()
		{
			return this.activeWorld.merchantItems;
		}

		public List<MerchantItem> GetEventMerchantItems()
		{
			return this.activeWorld.eventMerchantItems;
		}

		public MerchantItem GetMerchantItemWithId(string id)
		{
			for (int i = this.allWorlds.Count - 1; i >= 0; i--)
			{
				World world = this.allWorlds[i];
				for (int j = world.merchantItems.Count - 1; j >= 0; j--)
				{
					if (world.merchantItems[j].GetId() == id)
					{
						return world.merchantItems[j];
					}
				}
				if (world.eventMerchantItems != null)
				{
					for (int k = world.eventMerchantItems.Count - 1; k >= 0; k--)
					{
						if (world.eventMerchantItems[k].GetId() == id)
						{
							return world.eventMerchantItems[k];
						}
					}
				}
			}
			throw new Exception("Merchant item not found");
		}

		public bool CanAffordMerchantItem(int index, bool merchantItemIsFromEvent)
		{
			return ((!merchantItemIsFromEvent) ? this.activeWorld.merchantItems : this.activeWorld.eventMerchantItems)[index].CanAffordAndIsLeft(this.tokens);
		}

		public bool CanAffordMerchantItem(int index, int count, bool merchantItemIsFromEvent)
		{
			return ((!merchantItemIsFromEvent) ? this.activeWorld.merchantItems : this.activeWorld.eventMerchantItems)[index].CanAffordAndIsLeft(this.tokens, count);
		}

		public List<bool> CanAffordMerchantItems()
		{
			List<bool> list = new List<bool>();
			foreach (MerchantItem merchantItem in this.activeWorld.merchantItems)
			{
				list.Add(merchantItem.CanAfford(this.tokens));
			}
			return list;
		}

		public bool IsMultiMerchantEnabled()
		{
			return this.GetTotalAmountOfMerchantItemUsed() >= 10;
		}

		public int GetTotalAmountOfMerchantItemUsed()
		{
			int num = 0;
			foreach (KeyValuePair<string, int> keyValuePair in PlayerStats.numUsedMerchantItems)
			{
				num += keyValuePair.Value;
			}
			return num;
		}

		public void TryBuyMerchantItem(int index, int count, bool isEventMerchantItem)
		{
			MerchantItem merchantItem = ((!isEventMerchantItem) ? this.activeWorld.merchantItems : this.activeWorld.eventMerchantItems)[index];
			if (merchantItem.CanAffordAndIsLeft(this.tokens, count))
			{
				this.tokens.Decrement(merchantItem.GetPrice() * (double)count);
				for (int i = 0; i < count; i++)
				{
					merchantItem.Buy();
					this.activeWorld.merchantItemsToEvaluate.Add(merchantItem);
				}
				if (merchantItem.GetNumLeft() == 0 && merchantItem.GetNumInInventory() == 0)
				{
					this.activeWorld.AddSoundEvent(new SoundEventUiVariedVoiceSimple(SoundArchieve.inst.voMerchantUseItemLast, "MERCHANT", 1f));
				}
				else
				{
					this.activeWorld.AddSoundEvent(new SoundEventUiVariedVoiceSimple(SoundArchieve.inst.voMerchantUseItem, "MERCHANT", 1f));
				}
				this.activeWorld.shouldSoftSave = true;
				string id = merchantItem.GetId();
				PlayerStats.OnUsedMerchantItem(id);
			}
		}

		public float GetActiveWorldTotalTimeSpeedFactor()
		{
			return this.activeWorld.GetTimeSpeedFactor();
		}

		public void TryCollectOfflineEarnings()
		{
			this.activeWorld.TryCollectOfflineEarnings();
		}

		public void OnNewHeroIconSelected(string heroId)
		{
			if (!this.newHeroIconSelectedHeroIds.Contains(heroId))
			{
				this.newHeroIconSelectedHeroIds.Add(heroId);
			}
		}

		public void TryLoadTotem(string totemId)
		{
			TotemDataBase totemDataBase = null;
			foreach (TotemDataBase totemDataBase2 in this.allTotems)
			{
				if (totemDataBase2.id == totemId)
				{
					totemDataBase = totemDataBase2;
					break;
				}
			}
			this.LoadTotem(totemDataBase, this.activeWorld);
		}

		public void TryUpdateRunes(TotemDataBase totemDataBase, List<Rune> totemRunes)
		{
			if (totemRunes.Count > 3)
			{
				throw new Exception("Cant equip this many runes");
			}
			this.wornRunes[totemDataBase.id] = totemRunes;
			foreach (World world in this.allWorlds)
			{
				if (world.totem != null && world.totem.GetData().GetDataBase() == totemDataBase)
				{
					world.totem.RefreshRunes(totemRunes);
					break;
				}
			}
		}

		public void TrySetupChallenge(GameMode gameMode, TotemDataBase totemDataBase, HeroDataBase[] heroDataBases)
		{
			World world = this.GetWorld(gameMode);
			if (world.activeChallenge.state != Challenge.State.SETUP)
			{
				return;
			}
			List<Rune> list = null;
			if (totemDataBase != null)
			{
				list = this.wornRunes[totemDataBase.id];
			}
			world.Setup(totemDataBase, list, heroDataBases, this.boughtGears);
		}

		public void TryDefaultSetupChallenge(GameMode gameMode)
		{
			if (gameMode != GameMode.STANDARD)
			{
				throw new NotImplementedException();
			}
			this.TrySetupChallenge(gameMode, this.allTotems[0], new HeroDataBase[0]);
		}

		public bool GameModeShouldStartWithoutSetup(GameMode gameMode)
		{
			return false;
		}

		public bool IsGameModeAlreadySetup(GameMode gameMode)
		{
			World world = this.GetWorld(gameMode);
			return world.activeChallenge.state != Challenge.State.SETUP;
		}

		public bool IsGameModeInAction(GameMode gameMode)
		{
			World world = this.GetWorld(gameMode);
			return world.activeChallenge.state == Challenge.State.ACTION;
		}

		public bool IsActiveWorldInAction()
		{
			return this.activeWorld.activeChallenge.state == Challenge.State.ACTION;
		}

		public bool CanUseRune(Rune rune)
		{
			return this.wornRunes[rune.belongsTo.id].Count < 3 && this.IsRuneBought(rune) && !this.IsRuneWorn(rune);
		}

		public void TryUseRune(Rune rune)
		{
			if (this.CanUseRune(rune))
			{
				List<Rune> list = new List<Rune>();
				foreach (Rune rune2 in this.wornRunes[rune.belongsTo.id])
				{
					if (rune2 != rune)
					{
						list.Add(rune2);
					}
				}
				list.Add(rune);
				this.TryUpdateRunes(rune.belongsTo, list);
			}
		}

		public bool CanRemoveRune(Rune rune)
		{
			return this.IsRuneBought(rune) && this.IsRuneWorn(rune);
		}

		public void TryRemoveRune(Rune rune)
		{
			if (this.CanRemoveRune(rune))
			{
				List<Rune> list = new List<Rune>();
				foreach (Rune rune2 in this.wornRunes[rune.belongsTo.id])
				{
					if (rune2 != rune)
					{
						list.Add(rune2);
					}
				}
				this.TryUpdateRunes(rune.belongsTo, list);
			}
		}

		public List<Rune> GetRunes(TotemDataBase totem)
		{
			List<Rune> list = new List<Rune>();
			foreach (Rune rune in this.allRunes)
			{
				if (rune.belongsTo == totem)
				{
					list.Add(rune);
				}
			}
			return list;
		}

		public List<Rune> GetWornRunes(TotemDataBase totem)
		{
			return this.wornRunes[totem.id];
		}

		public List<Rune> GetBoughtRunes(TotemDataBase totem)
		{
			List<Rune> list = new List<Rune>();
			foreach (Rune rune in this.boughtRunes)
			{
				if (rune.belongsTo == totem)
				{
					list.Add(rune);
				}
			}
			return list;
		}

		public int GetBoughtRunesCount(TotemDataBase totem)
		{
			int num = 0;
			foreach (Rune rune in this.boughtRunes)
			{
				if (rune.belongsTo == totem)
				{
					num++;
				}
			}
			return num;
		}

		public Rune AddRandomRune()
		{
			if (this.allRunes.Count == this.boughtRunes.Count)
			{
				return null;
			}
			Rune rune = this.allRunes[GameMath.GetRandomInt(0, this.allRunes.Count, GameMath.RandType.Lootpack)];
			while (this.boughtRunes.Contains(rune))
			{
				rune = this.allRunes[GameMath.GetRandomInt(0, this.allRunes.Count, GameMath.RandType.Lootpack)];
			}
			this.boughtRunes.Add(rune);
			PlayerStats.OnRuneBought(this.boughtRunes.Count);
			UnityEngine.Debug.Log("New rune: " + LM.Get(rune.nameKey));
			return rune;
		}

		public Rune AddRandomRuneForTotem(string totemId)
		{
			List<Rune> unboughtRunesOfTotem = this.GetUnboughtRunesOfTotem(totemId);
			if (unboughtRunesOfTotem.Count <= 0)
			{
				return null;
			}
			Rune rune = unboughtRunesOfTotem[GameMath.GetRandomInt(0, unboughtRunesOfTotem.Count, GameMath.RandType.Lootpack)];
			this.boughtRunes.Add(rune);
			PlayerStats.OnRuneBought(this.boughtRunes.Count);
			UnityEngine.Debug.Log("New rune: " + LM.Get(rune.nameKey));
			return rune;
		}

		public void AddRune(Rune rune)
		{
			this.boughtRunes.Add(rune);
			PlayerStats.OnRuneBought(this.boughtRunes.Count);
		}

		private List<Rune> GetUnboughtRunesOfTotem(string totemId)
		{
			List<Rune> list = new List<Rune>();
			int i = 0;
			int count = this.allRunes.Count;
			while (i < count)
			{
				if (this.allRunes[i].belongsTo.id == totemId && !this.boughtRunes.Contains(this.allRunes[i]))
				{
					list.Add(this.allRunes[i]);
				}
				i++;
			}
			return list;
		}

		public int GetNumAvailableToWearRunes()
		{
			if (this.activeWorld.totem == null)
			{
				return 0;
			}
			TotemDataBase dataBase = this.activeWorld.totem.GetData().GetDataBase();
			int count = this.GetWornRunes(dataBase).Count;
			int max = 3 - count;
			int value = this.GetBoughtRunesCount(dataBase) - count;
			return Mathf.Clamp(value, 0, max);
		}

		public int GetNumUpgradableTrinkets()
		{
			int num = 0;
			foreach (Hero hero in this.activeWorld.heroes)
			{
				if (hero.trinket != null)
				{
					num++;
				}
			}
			return num;
		}

		public void UnlockCompass()
		{
			this.hasCompass = true;
		}

		public void UnlockSkillPoinstAutoDistribution()
		{
			this.hasSkillPointsAutoDistribution = true;
		}

		public void UnlockDailies()
		{
			this.hasDailies = true;
		}

		public float GetFreeLootpackPeriod()
		{
			float freeLootpackPeriod = PlayfabManager.titleData.freeLootpackPeriod;
			return freeLootpackPeriod * this.universalTotalBonus.freePackCooldownFactor;
		}

		public void OnIap(int index, bool rainOnUi = false, DropPosition dp = null)
		{
			UnityEngine.Debug.Log("Sim got new iap credits: " + index);
			double amount = Simulator.CREDIT_PACKS_AMOUNT[index];
			if (rainOnUi)
			{
				this.activeWorld.RainCurrencyOnUi(UiState.HUB_SHOP, CurrencyType.GEM, amount, dp, 30, 0f, 0f, 1f, null, 0f);
			}
			else
			{
				this.activeWorld.RainCredits(amount);
			}
			if (IapManager.IsInitialized() && IapManager.productPrices != null && IapManager.productPrices.Length > index)
			{
				PlayfabManager.SendPlayerEvent(PlayfabEventId.GEM_PACK_PURCHASE, new Dictionary<string, object>
				{
					{
						"product",
						IapManager.productIds[index]
					},
					{
						"cost_currency",
						IapManager.storeCurrency
					},
					{
						"cost_amount",
						IapManager.productPrices[index]
					}
				}, null, null, true);
			}
		}

		public void OnIapCandy(int index, Vector3 dropPositionTarget)
		{
			UnityEngine.Debug.Log("Sim got new iap candy: " + index);
			DropPosition dropPos = new DropPosition
			{
				startPos = default(Vector3),
				endPos = Vector3.down * 0.1f,
				invPos = dropPositionTarget
			};
			this.activeWorld.RainCurrencyOnUi(UiState.CHRISTMAS_PANEL, CurrencyType.CANDY, 12500.0, dropPos, 30, 0f, 0f, 1f, null, 0f);
		}

		public int GetNumTimeChallengesComplete()
		{
			int num = 0;
			foreach (Challenge challenge in this.worldCrusade.allChallenges)
			{
				if (challenge.state != Challenge.State.WON)
				{
					return num;
				}
				num++;
			}
			return num;
		}

		public bool CanWatchVideoForFreeCurrency(CurrencyType currencyType)
		{
			return RewardedAdManager.inst != null && RewardedAdManager.inst.IsRewardedCappedVideoAvailable(this.GetLastCappedCurrencyWatchedTime(currencyType), currencyType, 0);
		}

		public void CheckTutorialState()
		{
			if (this.maxStagePrestigedAt > 0 || this.worldStandard.GetStageNumber() > 0)
			{
				if (TutorialManager.first != TutorialManager.First.FIN)
				{
					AdjustTracker.TrackTutorialCompleted();
				
				}
				TutorialManager.first = TutorialManager.First.FIN;
			}
			if (this.worldStandard.unlocks[0].isCollected)
			{
				TutorialManager.modeTab = TutorialManager.ModeTab.FIN;
			}
			if (TutorialManager.shopTab != TutorialManager.ShopTab.FIN && this.lootpacksOpenedCount.ContainsKey(this.lootpacks[0].GetType().Name))
			{
				TutorialManager.shopTab = TutorialManager.ShopTab.GO_TO_GEARS;
			}
			if (PlayerStats.spentMyth > 0.0)
			{
				TutorialManager.artifactsTab = TutorialManager.ArtifactsTab.FIN;
			}
			if (this.maxStagePrestigedAt > 0)
			{
				TutorialManager.prestige = TutorialManager.Prestige.FIN;
			}
			if (TutorialManager.skillScreen != TutorialManager.SkillScreen.FIN)
			{
				if (this.maxStagePrestigedAt > 0)
				{
					TutorialManager.skillScreen = TutorialManager.SkillScreen.FIN;
				}
				else
				{
					foreach (Hero hero in this.worldStandard.heroes)
					{
						if (hero.GetLevel() > 0)
						{
							TutorialManager.skillScreen = TutorialManager.SkillScreen.FIN;
							break;
						}
					}
				}
			}
			if (PlayerStats.spentScraps > 0.0)
			{
				TutorialManager.gearScreen = TutorialManager.GearScreen.FIN;
			}
			if (this.IsTotemUnlocked("totemFire"))
			{
				TutorialManager.ringPrestigeReminder = TutorialManager.RingPrestigeReminder.FIN;
			}
			if (this.unlockedHeroIds.Count > 5 && this.activeWorld.heroes.Count == 5)
			{
				TutorialManager.ringPrestigeReminder = TutorialManager.RingPrestigeReminder.FIN;
			}
			if (this.allTrinkets.Count > 1)
			{
				TutorialManager.trinketShop = TutorialManager.TrinketShop.FIN;
				if (TutorialManager.trinketHeroTab != TutorialManager.TrinketHeroTab.FIN)
				{
					if (this.allHeroes.Find((HeroDataBase h) => h.trinket != null) != null)
					{
						TutorialManager.trinketHeroTab = TutorialManager.TrinketHeroTab.FIN;
					}
				}
			}
			if (TutorialManager.missionIndex >= TutorialMission.List.Length)
			{
				TutorialManager.missionsFinished = TutorialManager.MissionsFinished.FIN;
			}
			bool flag = OldArtifactsConverter.DoesPlayerNeedsConversion(this);
			if (flag)
			{
				TutorialManager.artifactOverhaul = TutorialManager.ArtifactOverhaul.BEFORE_BEGIN;
			}
			else if (TutorialManager.artifactOverhaul < TutorialManager.ArtifactOverhaul.GIVE_MYTHSTONES)
			{
				TutorialManager.artifactOverhaul = ((TutorialManager.artifactOverhaul != TutorialManager.ArtifactOverhaul.BEFORE_BEGIN) ? TutorialManager.ArtifactOverhaul.GIVE_MYTHSTONES : TutorialManager.ArtifactOverhaul.FIN);
			}
		}

		public UniversalTotalBonus GetUniversalBonusAll()
		{
			return this.universalTotalBonus;
		}

		public UniversalTotalBonus GetUniversalBonusRift()
		{
			return this.universalTotalBonusRift;
		}

		public void ResetMerchantItems()
		{
			foreach (World world in this.allWorlds)
			{
				world.ResetMerchantItems();
			}
		}

		public void PrepareForCloseAndSave()
		{
			this.CollectDropsImmidiately();
			this.CheckForRainingGloryBeforeCloseAndSave();
		}

		public void CheckForRainingGloryBeforeCloseAndSave()
		{
			foreach (World world in this.allWorlds)
			{
				if (world.gameMode != GameMode.STANDARD && world.gameMode != GameMode.CRUSADE && world.gameMode != GameMode.RIFT)
				{
					throw new NotImplementedException();
				}
				if (world.isRainingGlory)
				{
					if (world.gameMode == GameMode.STANDARD)
					{
						world.ResetPrestige();
					}
					else if (world.gameMode == GameMode.CRUSADE)
					{
						world.SetupNextChallenge();
						world.ResetPrestige();
					}
					else
					{
						if (world.gameMode != GameMode.RIFT)
						{
							throw new NotImplementedException();
						}
						world.OnRainGloryComplete();
						UnityEngine.Debug.LogWarning(world.gameMode.ToString());
						world.ResetPrestige();
					}
				}
			}
		}

		public void CollectDropsImmidiately()
		{
			foreach (World world in this.allWorlds)
			{
				world.CollectDropsImmidiately();
				this.mythstones.Increment(world.GetAndZeroEarnedMythstone());
				this.credits.Increment(world.GetAndZeroEarnedCredit());
				this.scraps.Increment(world.GetAndZeroEarnedScrap());
				this.tokens.Increment(world.GetAndZeroEarnedToken());
				this.aeons.Increment(world.GetAndZeroEarnedAeon());
				if (this.hasRiftQuest)
				{
					this.AddRiftDust(world.GetAndZeroEarnedRiftPoints());
				}
				double andZeroEarnedCandy = world.GetAndZeroEarnedCandy();
				if (andZeroEarnedCandy > 0.0)
				{
					this.candies.Increment(andZeroEarnedCandy);
					UnityEngine.Debug.Log(andZeroEarnedCandy.ToString() + " was earned.");
				}
			}
		}

		public bool IsTrinketCapped(Trinket trinket)
		{
			return trinket.IsCapped();
		}

		public bool CanUpgradeTrinket(Trinket trinket)
		{
			return !trinket.IsCapped() && this.scraps.CanAfford(trinket.GetUpgradeCost(trinket.GetTotalLevel()));
		}

		public void TryUpgradeTrinket(Trinket trinket, int effectIndex)
		{
			if (this.CanUpgradeTrinket(trinket))
			{
				this.scraps.Decrement(trinket.GetUpgradeCost(trinket.GetTotalLevel()));
				trinket.effects[effectIndex].level++;
				trinket.OnLevelChanged();
				this.RefreshTrinketEffects(this.GetHeroWithTrinket(trinket));
			}
		}

		public HeroDataBase GetHeroWithTrinket(Trinket trinket)
		{
			foreach (HeroDataBase heroDataBase in this.allHeroes)
			{
				if (heroDataBase.trinket == trinket)
				{
					return heroDataBase;
				}
			}
			return null;
		}

		public void RefreshTrinketEffects()
		{
			foreach (World world in this.allWorlds)
			{
				world.RefreshTrinketEffects();
			}
		}

		public void RefreshTrinketEffects(HeroDataBase heroDataBase)
		{
			if (heroDataBase == null)
			{
				return;
			}
			foreach (World world in this.allWorlds)
			{
				if (world.RefreshTrinketEffects(heroDataBase))
				{
					break;
				}
			}
		}

		public bool CanDestroyTrinkets(List<Trinket> trinkets, CurrencyType currencyType)
		{
			double num = 0.0;
			if (currencyType == CurrencyType.GEM)
			{
				for (int i = trinkets.Count - 1; i >= 0; i--)
				{
					num += trinkets[i].GetDestroyCostCredits();
				}
				return this.credits.CanAfford(num);
			}
			if (currencyType != CurrencyType.TOKEN)
			{
				throw new NotImplementedException();
			}
			for (int j = trinkets.Count - 1; j >= 0; j--)
			{
				num += trinkets[j].GetDestroyCostTokens();
			}
			return this.tokens.CanAfford(num);
		}

		public bool CanDestroyTrinket(Trinket trinket, CurrencyType currencyType)
		{
			if (currencyType == CurrencyType.GEM)
			{
				return this.credits.CanAfford(trinket.GetDestroyCostCredits());
			}
			if (currencyType != CurrencyType.TOKEN)
			{
				throw new NotImplementedException();
			}
			return this.tokens.CanAfford(trinket.GetDestroyCostTokens());
		}

		public bool CanDisassembleTrinkets(List<Trinket> trinkets, CurrencyType currencyType)
		{
			double num = 0.0;
			if (currencyType == CurrencyType.GEM)
			{
				for (int i = trinkets.Count - 1; i >= 0; i--)
				{
					num += trinkets[i].GetDisassembleCostCredits();
				}
				return this.credits.CanAfford(num);
			}
			if (currencyType != CurrencyType.TOKEN)
			{
				throw new NotImplementedException();
			}
			for (int j = trinkets.Count - 1; j >= 0; j--)
			{
				num += trinkets[j].GetDisassembleCostTokens();
			}
			return this.tokens.CanAfford(num);
		}

		public bool CanDisassembleTrinket(Trinket trinket, CurrencyType currencyType)
		{
			if (currencyType == CurrencyType.GEM)
			{
				return this.credits.CanAfford(trinket.GetDisassembleCostCredits());
			}
			if (currencyType != CurrencyType.TOKEN)
			{
				throw new NotImplementedException();
			}
			return this.tokens.CanAfford(trinket.GetDisassembleCostTokens());
		}

		public void TryDestroyTrinkets(List<Trinket> trinkets, CurrencyType currencyType, List<DropPosition> dropPositions = null)
		{
			if (!this.CanDestroyTrinkets(trinkets, currencyType))
			{
				return;
			}
			double num = 0.0;
			if (currencyType != CurrencyType.GEM)
			{
				if (currencyType != CurrencyType.TOKEN)
				{
					throw new NotImplementedException();
				}
				for (int i = trinkets.Count - 1; i >= 0; i--)
				{
					num += trinkets[i].GetDestroyCostTokens();
				}
				this.tokens.Decrement(num);
			}
			else
			{
				for (int j = trinkets.Count - 1; j >= 0; j--)
				{
					num += trinkets[j].GetDestroyCostCredits();
				}
				this.credits.Decrement(num);
			}
			PlayfabManager.SendPlayerEvent(PlayfabEventId.TRINKET_DESTROYED, new Dictionary<string, object>
			{
				{
					"spent_currency",
					currencyType
				},
				{
					"spent_amount",
					num
				},
				{
					"num_trinkets_destroyed",
					trinkets.Count
				}
			}, null, null, true);
			this.DisassembleTrinkets(trinkets, dropPositions);
		}

		public void TryDestroyTrinket(Trinket trinket, CurrencyType currencyType, DropPosition dropPosition = null)
		{
			if (!this.CanDestroyTrinket(trinket, currencyType))
			{
				return;
			}
			double num = 0.0;
			if (currencyType != CurrencyType.GEM)
			{
				if (currencyType == CurrencyType.TOKEN)
				{
					num = trinket.GetDestroyCostTokens();
					this.tokens.Decrement(num);
				}
			}
			else
			{
				num = trinket.GetDestroyCostCredits();
				this.credits.Decrement(num);
			}
			PlayfabManager.SendPlayerEvent(PlayfabEventId.TRINKET_DESTROYED, new Dictionary<string, object>
			{
				{
					"spent_currency",
					currencyType
				},
				{
					"spent_amount",
					num
				},
				{
					"num_trinkets_destroyed",
					1
				}
			}, null, null, true);
			this.DisassembleTrinket(trinket, dropPosition);
		}

		public void TryDisassembleTrinkets(List<Trinket> trinkets, CurrencyType currencyType, List<DropPosition> dropPositions = null)
		{
			if (!this.CanDisassembleTrinkets(trinkets, currencyType))
			{
				return;
			}
			double num = 0.0;
			if (currencyType != CurrencyType.GEM)
			{
				if (currencyType != CurrencyType.TOKEN)
				{
					throw new NotImplementedException();
				}
				for (int i = trinkets.Count - 1; i >= 0; i--)
				{
					num += trinkets[i].GetDisassembleCostTokens();
				}
				this.tokens.Decrement(num);
			}
			else
			{
				for (int j = trinkets.Count - 1; j >= 0; j--)
				{
					num += trinkets[j].GetDisassembleCostCredits();
				}
				this.credits.Decrement(num);
			}
			PlayfabManager.SendPlayerEvent(PlayfabEventId.TRINKET_DESTROYED, new Dictionary<string, object>
			{
				{
					"spent_currency",
					currencyType
				},
				{
					"spent_amount",
					num
				},
				{
					"num_trinkets_destroyed",
					trinkets.Count
				}
			}, null, null, true);
			this.DisassembleTrinkets(trinkets, dropPositions);
		}

		public void TryDisassembleTrinket(Trinket trinket, CurrencyType currencyType, DropPosition dropPosition = null)
		{
			if (!this.CanDisassembleTrinket(trinket, currencyType))
			{
				return;
			}
			double num = 0.0;
			if (currencyType != CurrencyType.GEM)
			{
				if (currencyType == CurrencyType.TOKEN)
				{
					num = trinket.GetDisassembleCostTokens();
					this.tokens.Decrement(num);
				}
			}
			else
			{
				num = trinket.GetDisassembleCostCredits();
				this.credits.Decrement(num);
			}
			PlayfabManager.SendPlayerEvent(PlayfabEventId.TRINKET_DESTROYED, new Dictionary<string, object>
			{
				{
					"spent_currency",
					currencyType
				},
				{
					"spent_amount",
					num
				},
				{
					"num_trinkets_destroyed",
					1
				}
			}, null, null, true);
			this.DisassembleTrinket(trinket, dropPosition);
		}

		private void DisassembleTrinkets(List<Trinket> trinkets, List<DropPosition> dropPositions)
		{
			List<double> list = new List<double>();
			for (int i = trinkets.Count - 1; i >= 0; i--)
			{
				this.DisassembleTrinket(trinkets[i]);
				list.Add(trinkets[i].GetDestroyReward());
			}
			if (dropPositions == null)
			{
				double num = 0.0;
				foreach (double num2 in list)
				{
					num += num2;
				}
				this.scraps.Increment(num);
			}
			else
			{
				for (int j = 0; j < list.Count; j++)
				{
					this.activeWorld.RainCurrencyOnUi(UiState.HEROES_TRINKETS, CurrencyType.SCRAP, list[j], dropPositions[j], 5, 0.5f + (float)j * 0.1f, 0.3f, 0.3f, null, 0.1f);
				}
			}
		}

		private void DisassembleTrinket(Trinket trinket)
		{
			HeroDataBase heroWithTrinket = this.GetHeroWithTrinket(trinket);
			if (heroWithTrinket != null)
			{
				heroWithTrinket.trinket = null;
			}
			int num = this.IsTrinketPinned(trinket);
			if (num != -1)
			{
				this.TryPinTrinket(trinket);
			}
			this.UpdatePinnedTrinketsAfterDestroy(trinket);
			this.allTrinkets.Remove(trinket);
			this.RefreshTrinketEffects();
			foreach (TrinketEffect effect in trinket.effects)
			{
				int key = TypeHelper.ConvertTrinketEffect(effect);
				if (this.disassembledTinketEffects.ContainsKey(key))
				{
					int num2 = this.disassembledTinketEffects[key];
					num2++;
					this.disassembledTinketEffects[key] = num2;
				}
				else
				{
					this.disassembledTinketEffects.Add(key, 1);
				}
			}
			this.TryRevealAllTrinketEffects();
		}

		private void CleanUnusedTrinketEffectsFromDisassembledList()
		{
			List<int> list = new List<int>(this.disassembledTinketEffects.Keys);
			using (List<int>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int id = enumerator.Current;
					if (Trinket.commonEffects.Find((TrinketEffect e) => TypeHelper.ConvertTrinketEffect(e) == id) == null && Trinket.secondaryEffects.Find((TrinketEffect e) => TypeHelper.ConvertTrinketEffect(e) == id) == null && Trinket.specialEffects.Find((TrinketEffect e) => TypeHelper.ConvertTrinketEffect(e) == id) == null)
					{
						this.disassembledTinketEffects.Remove(id);
					}
				}
			}
		}

		private void TryRevealAllTrinketEffects()
		{
			if (this.disassembledTinketEffects.Count > this.REVEAL_ALL_TRINKET_EFFECT_THRESHOLD && this.disassembledTinketEffects.Count < Trinket.commonEffects.Count + Trinket.secondaryEffects.Count)
			{
				for (int i = Trinket.commonEffects.Count - 1; i >= 0; i--)
				{
					int key = TypeHelper.ConvertTrinketEffect(Trinket.commonEffects[i]);
					if (!this.disassembledTinketEffects.ContainsKey(key))
					{
						this.disassembledTinketEffects.Add(key, 0);
					}
				}
				for (int j = Trinket.secondaryEffects.Count - 1; j >= 0; j--)
				{
					int key2 = TypeHelper.ConvertTrinketEffect(Trinket.secondaryEffects[j]);
					if (!this.disassembledTinketEffects.ContainsKey(key2))
					{
						this.disassembledTinketEffects.Add(key2, 0);
					}
				}
				for (int k = Trinket.specialEffects.Count - 1; k >= 0; k--)
				{
					int key3 = TypeHelper.ConvertTrinketEffect(Trinket.specialEffects[k]);
					if (!this.disassembledTinketEffects.ContainsKey(key3))
					{
						this.disassembledTinketEffects.Add(key3, 0);
					}
				}
				for (int l = 1; l <= 67; l++)
				{
					if (!this.disassembledTinketEffects.ContainsKey(l))
					{
						this.disassembledTinketEffects.Add(l, 0);
					}
				}
			}
		}

		private void DisassembleTrinket(Trinket trinket, DropPosition dropPosition)
		{
			this.DisassembleTrinket(trinket);
			if (dropPosition == null)
			{
				this.scraps.Increment(trinket.GetDestroyReward());
			}
			else
			{
				this.activeWorld.RainCurrencyOnUi(UiState.HEROES_TRINKETS, CurrencyType.SCRAP, trinket.GetDestroyReward(), dropPosition, 30, 0f, 0f, 1f, null, 0f);
			}
		}

		private void UpdatePinnedTrinketsAfterDestroy(Trinket destroyedTrinket)
		{
			int num = this.allTrinkets.IndexOf(destroyedTrinket);
			for (int i = this.trinketsPinned.Count - 1; i >= 0; i--)
			{
				if (this.trinketsPinned[i] > num)
				{
					List<int> list;
					int index;
					(list = this.trinketsPinned)[index = i] = list[index] - 1;
				}
			}
			List<Trinket> list2 = new List<Trinket>(this.trinketsPinnedHashSet.Keys);
			foreach (Trinket trinket in list2)
			{
				if (this.trinketsPinnedHashSet[trinket] > num)
				{
					Dictionary<Trinket, int> dictionary;
					Trinket key;
					(dictionary = this.trinketsPinnedHashSet)[key = trinket] = dictionary[key] - 1;
				}
			}
		}

		public void TryEquipUnequipTrinket(Trinket trinket, HeroDataBase hero)
		{
			if (!this.CanEquipUnequipTrinket(trinket, hero))
			{
				return;
			}
			HeroDataBase heroWithTrinket = this.GetHeroWithTrinket(trinket);
			if (heroWithTrinket == hero)
			{
				hero.trinket = null;
			}
			else
			{
				if (heroWithTrinket != null)
				{
					heroWithTrinket.trinket = null;
				}
				hero.trinket = trinket;
				hero.trinketEquipTimer = 60f;
			}
			this.RefreshTrinketEffects();
		}

		public bool CanEquipUnequipTrinket(Trinket trinket, HeroDataBase hero)
		{
			return this.GetHeroWithTrinket(trinket) == hero || hero.trinketEquipTimer <= 0f;
		}

		public double GetTimeForNextShopOffer()
		{
			double result = 0.0;
			if (TutorialManager.IsShopTabUnlocked() && TrustedTime.IsReady() && (this.specialOfferBoard.standardOffer.IsAlive() || this.specialOfferBoard.reAppearingUniqueOffer.IsAlive()))
			{
				result = this.specialOfferBoard.GetNearestNextTimeOffer(TrustedTime.Get());
			}
			return result;
		}

		public int GetNumCollectableAchievements()
		{
			int num = 0;
			if (this.questOfUpdate != null)
			{
				num = ((!this.questOfUpdate.IsCompleted()) ? 0 : 1);
			}
			int i = 0;
			int count = PlayerStats.achievements.Count;
			while (i < count)
			{
				foreach (KeyValuePair<string, bool> keyValuePair in PlayerStats.achievements[i])
				{
					if (!keyValuePair.Value)
					{
						break;
					}
					if (keyValuePair.Value && !Simulator.achievementCollecteds[keyValuePair.Key])
					{
						num++;
						break;
					}
				}
				i++;
			}
			return num;
		}

		public bool CanCollectAchievement(string id)
		{
			return PlayerStats.GetAchievement(id) && !Simulator.achievementCollecteds[id];
		}

		public void TryCollectAchievement(string id, DropPosition dropPosition)
		{
			if (this.CanCollectAchievement(id))
			{
				Simulator.achievementCollecteds[id] = true;
				this.activeWorld.RainCurrencyOnUi(UiState.HUB_ACHIEVEMENTS, CurrencyType.GEM, Simulator.achievementRewards[id], dropPosition, 30, 0f, 0f, 1f, null, 0f);
			}
		}

		public double GetTrinketCraftCost(int effectCount)
		{
			return Trinket.CraftConstByEffect[effectCount - 1];
		}

		public void TryCraftTrinket(TrinketEffect common, TrinketEffect secondary, TrinketEffect special, int bodyIndex, int colorIndex)
		{
			if (this.allTrinkets.Count >= this.numTrinketSlots)
			{
				return;
			}
			List<TrinketEffect> list = new List<TrinketEffect>();
			if (common != null)
			{
				list.Add(common.GetCopy());
			}
			if (secondary != null)
			{
				list.Add(secondary.GetCopy());
			}
			if (special != null)
			{
				list.Add(special.GetCopy());
			}
			double trinketCraftCost = this.GetTrinketCraftCost(list.Count);
			if (!this.credits.CanAfford(trinketCraftCost))
			{
				return;
			}
			if (common != null && !this.TryConsumeTinketEffect(common))
			{
				throw new Exception("Trinket effect is not available");
			}
			if (secondary != null && !this.TryConsumeTinketEffect(secondary))
			{
				throw new Exception("Trinket effect is not available");
			}
			if (special != null && !this.TryConsumeTinketEffect(special))
			{
				throw new Exception("Trinket effect is not available");
			}
			this.credits.Decrement(trinketCraftCost);
			Trinket trinket = new Trinket(list);
			trinket.bodyColorIndex = colorIndex;
			trinket.bodySpriteIndex = bodyIndex;
			this.allTrinkets.Add(trinket);
			this.hasEverOwnedATrinket = true;
			this.numTrinketsObtained++;
			PlayfabManager.SendPlayerEvent(PlayfabEventId.TRINKET_FORGED, new Dictionary<string, object>
			{
				{
					"chosen_effects",
					new int[]
					{
						(common != null) ? TypeHelper.ConvertTrinketEffect(common) : -1,
						(secondary != null) ? TypeHelper.ConvertTrinketEffect(secondary) : -1,
						(special != null) ? TypeHelper.ConvertTrinketEffect(special) : -1
					}
				},
				{
					"num_trinkets_crafted",
					this.numTrinketsObtained
				},
				{
					"spent_currency",
					CurrencyType.GEM
				},
				{
					"spent_amount",
					trinketCraftCost
				}
			}, null, null, true);
		}

		private bool TryConsumeTinketEffect(TrinketEffect e)
		{
			int key = TypeHelper.ConvertTrinketEffect(e);
			int num;
			if (this.disassembledTinketEffects.TryGetValue(key, out num))
			{
				num--;
				this.disassembledTinketEffects[key] = num;
				return true;
			}
			return false;
		}

		public void CreateTrinket(TrinketUpgradeReq req, TrinketEffect common, TrinketEffect secondary, TrinketEffect special)
		{
			if (this.allTrinkets.Count >= this.numTrinketSlots)
			{
				return;
			}
			List<TrinketEffect> list = new List<TrinketEffect>();
			if (common != null)
			{
				list.Add(common);
			}
			if (secondary != null)
			{
				list.Add(secondary);
			}
			if (special != null)
			{
				list.Add(special);
			}
			Trinket item = new Trinket(list, req);
			this.allTrinkets.Add(item);
			this.hasEverOwnedATrinket = true;
			this.numTrinketsObtained++;
		}

		public int IsTrinketPinned(Trinket trinket)
		{
			int result = -1;
			if (this.trinketsPinnedHashSet.TryGetValue(trinket, out result))
			{
				return result;
			}
			return -1;
		}

		public void TryPinTrinket(Trinket trinket)
		{
			int num = this.IsTrinketPinned(trinket);
			if (num != -1)
			{
				this.trinketsPinnedHashSet.Remove(trinket);
				this.trinketsPinned.Remove(num);
			}
			else
			{
				int i = 0;
				int count = this.allTrinkets.Count;
				while (i < count)
				{
					if (this.allTrinkets[i] == trinket)
					{
						this.trinketsPinnedHashSet.Add(trinket, i);
						this.trinketsPinned.Add(i);
						break;
					}
					i++;
				}
			}
		}

		public bool MineAnyUnlocked()
		{
			return this.mineScrap.unlocked || this.mineToken.unlocked;
		}

		public bool MineScrapUnlocked()
		{
			return this.mineScrap.unlocked;
		}

		public bool MineTokenUnlocked()
		{
			return this.mineToken.unlocked;
		}

		public bool MinesAvailable()
		{
			return TrustedTime.IsReady();
		}

		public void UnlockMineToken()
		{
			this.mineToken.unlocked = true;
		}

		public void UnlockMineScrap()
		{
			this.mineScrap.unlocked = true;
		}

		public bool CanCollectMine(Mine mine)
		{
			return mine.unlocked && TrustedTime.IsReady() && (TrustedTime.Get() - mine.timeCollected).TotalSeconds >= mine.GetPeriod();
		}

		public double GetTimeToCollectMine(Mine mine)
		{
			if (TrustedTime.IsReady())
			{
				return Math.Max((mine.timeCollected.AddSeconds(mine.GetPeriod()) - TrustedTime.Get()).TotalSeconds, 0.0);
			}
			return -1.0;
		}

		public void TryCollectMine(Mine mine, DropPosition dropPosition)
		{
			if (this.CanCollectMine(mine))
			{
				this.isMineEverCollected = true;
				mine.timeCollected = TrustedTime.Get();
				this.activeWorld.RainCurrencyOnUi(UiState.SHOP_MINE, mine.rewardCurrency, mine.GetReward(), dropPosition, 30, 0f, 0f, 1f, null, 0f);
				PlayerStats.OnMineCollected();
			}
		}

		public void TryUpgradeMine(Mine mine)
		{
			if (this.CanUpgradeMine(mine))
			{
				this.aeons.Decrement(mine.GetUpgradeCost());
				mine.level++;
				this.OnMinesChanged();
			}
		}

		public bool CanUpgradeMine(Mine mine)
		{
			return mine.GetUpgradeCost() != -1.0 && this.aeons.CanAfford(mine.GetUpgradeCost());
		}

		public bool CanCollectDailyQuest()
		{
			return this.dailyQuest != null && this.dailyQuest.progress >= this.dailyQuest.goal;
		}

		public void TryCollectDailyQuest(Vector3 startPos, Transform invTransform)
		{
			if (this.CanCollectDailyQuest())
			{
				this.CollectDailyQuest(startPos, invTransform);
			}
		}

		private void CollectDailyQuest(Vector3 startPos, Transform invTransform)
		{
			if (this.dailyQuest == null)
			{
				return;
			}
			DropPosition dropPosition = new DropPosition
			{
				startPos = startPos,
				endPos = startPos,
				invPos = ((!(invTransform == null)) ? invTransform.position : Vector3.zero),
				targetToScaleOnReach = invTransform,
				showSideCurrency = false
			};
			this.dailyQuest.GiveReward(dropPosition, this);
			this.lastDailyQuest = SaveLoadManager.ConvertDailyQuest(this.dailyQuest);
			this.dailySkipCount = 0;
			PlayerStats.OnDailyComplete();
			if (TrustedTime.IsReady())
			{
				this.dailyQuestCollectedTime = TrustedTime.Get();
			}
			else
			{
				this.dailyQuestCollectedTime = DateTime.MaxValue;
			}
			if (this.dailyQuest is DailyQuestCollectCandy)
			{
				this.numCandyQuestAnniversaryCompleted++;
			}
			this.dailyQuest = null;
		}

		public double GetSkipDailyCost()
		{
			return Simulator.DAILY_SKIP_COSTS[Mathf.Min(this.dailySkipCount, Simulator.DAILY_SKIP_COSTS.Length - 1)];
		}

		public bool CanSkipDailyQuest()
		{
			return this.dailyQuest != null && this.dailyQuest.progress < this.dailyQuest.goal && this.credits.CanAfford(this.GetSkipDailyCost());
		}

		public bool CanSkipDailyQuestWithoutCost()
		{
			return this.dailyQuest != null && this.dailyQuest.progress < this.dailyQuest.goal;
		}

		public bool CanAffordSkipDailyQuest()
		{
			return this.credits.CanAfford(this.GetSkipDailyCost());
		}

		public void SkipDailyQuest()
		{
			if (this.dailyQuest == null)
			{
				return;
			}
			this.credits.Decrement(this.GetSkipDailyCost());
			this.lastDailyQuest = SaveLoadManager.ConvertDailyQuest(this.dailyQuest);
			this.dailySkipCount++;
			PlayerStats.OnDailySkip();
			this.dailyQuest = null;
			this.dailyQuest = this.GetRandomDailyQuest();
			foreach (World world in this.allWorlds)
			{
				world.ResetDailyQuestProgress();
			}
		}

		public bool IsAnyHeroEverEvolved()
		{
			foreach (HeroDataBase heroDataBase in this.allHeroes)
			{
				if (heroDataBase.evolveLevel > 0)
				{
					return true;
				}
			}
			return false;
		}

		public bool IsThereAnySkinBought()
		{
			return this.boughtSkins.Count > this.allHeroes.Count;
		}

		public int GetNumHeroesWithNewSkins()
		{
			int num = 0;
			foreach (Hero hero in this.activeWorld.heroes)
			{
				if (this.HasHeroNewSkin(hero.GetId()))
				{
					num++;
				}
			}
			return num;
		}

		public DailyQuest GetRandomDailyQuest()
		{
			float num = 0f;
			this.dailyQuestsAppearedCount++;
			List<DailyQuest> allAvailable = DailyQuest.GetAllAvailable(this);
			foreach (DailyQuest dailyQuest in allAvailable)
			{
				num += dailyQuest.GetChanceWeight(this);
			}
			if (num <= 0f)
			{
				return null;
			}
			float num2 = GameMath.GetRandomFloat(0f, num, GameMath.RandType.NoSeed);
			for (int i = allAvailable.Count - 1; i >= 0; i--)
			{
				num2 -= allAvailable[i].GetChanceWeight(this);
				if (num2 <= 0f)
				{
					return allAvailable[i];
				}
			}
			int num3 = -1;
			for (int j = allAvailable.Count - 1; j >= 0; j--)
			{
				DailyQuest dailyQuest2 = allAvailable[j];
				if (num3 < 0 || allAvailable[num3].GetChanceWeight(this) < dailyQuest2.GetChanceWeight(this))
				{
					num3 = j;
				}
			}
			return allAvailable[num3];
		}

		public double GetQuestOfUpdateProgress()
		{
			if (this.HasQuestOfUpdate())
			{
				return this.questOfUpdate.progress / this.questOfUpdate.goal;
			}
			throw new Exception("There is no quest of update");
		}

		public bool HasQuestOfUpdate()
		{
			return this.questOfUpdate != null;
		}

		public bool HasAvailableQuestOfUpdate(DateTime currentTime, bool isTimeReady, out QuestOfUpdate qou)
		{
			QuestOfUpdate questOfUpdate = QuestOfUpdate.GetQuestOfUpdate();
			if (questOfUpdate == null)
			{
				qou = null;
				return false;
			}
			if (questOfUpdate.IsAvailable(this, currentTime, isTimeReady) && !this.completedQuestOfUpdates.Contains(questOfUpdate.id) && !this.failedQuestOfUpdates.Contains(questOfUpdate.id))
			{
				qou = questOfUpdate;
				return true;
			}
			qou = null;
			return false;
		}

		public void TryToCollectQuestOfUpdate(DropPosition pos)
		{
			if (this.HasQuestOfUpdate() && this.questOfUpdate.IsCompleted())
			{
				this.CollectQuestOfUpdate(pos);
			}
		}

		public void CollectQuestOfUpdate(DropPosition pos)
		{
			this.questOfUpdate.reward.dropPosition = pos;
			this.questOfUpdate.reward.Give(this, this.activeWorld);
			this.completedQuestOfUpdates.Add(this.questOfUpdate.id);
			this.questOfUpdate = null;
		}

		public string GetTrinketHintString()
		{
			if (this.HasEmptyTrinketSlot())
			{
				return LM.Get("TRINKET_UNLOCK_HINT");
			}
			Unlock unlock = this.worldStandard.unlocks.Find((Unlock u) => u.GetId() == UnlockIds.TRINKET_SLOT_00);
			string arg = '\n' + AM.SizeText(unlock.GetReqInt().ToString(), 60) + '\n';
			return string.Format(LM.Get("ADVENTURE_STAGE_UNLOCK_HINT"), arg);
		}

		public string GetTrinketSmithHintString()
		{
			Unlock unlock = this.worldStandard.unlocks.Find((Unlock u) => u.GetId() == UnlockIds.TRINKET_DISASSEMBLE);
			string arg = '\n' + AM.SizeText(unlock.GetReqInt().ToString(), 60) + '\n';
			return string.Format(LM.Get("ADVENTURE_STAGE_UNLOCK_HINT"), arg);
		}

		public string GetMythicalHintString()
		{
			Unlock unlock = this.worldStandard.unlocks.Find((Unlock u) => u.GetId() == UnlockIds.MYTHICAL_SLOT_00);
			string arg = '\n' + AM.SizeText(unlock.GetReqInt().ToString(), 60) + '\n';
			return string.Format(LM.Get("ADVENTURE_STAGE_UNLOCK_HINT"), arg);
		}

		public Dictionary<string, HeroUnlockDescKey> GetHeroUnlockHintStrings()
		{
			List<Unlock> list = this.worldStandard.unlocks.FindAll((Unlock u) => u.GetReward() is UnlockRewardHero);
			List<Unlock> list2 = (from c in this.worldCrusade.allChallenges.FindAll((Challenge c) => c is ChallengeWithTime).OfType<ChallengeWithTime>()
			select c.unlock).ToList<Unlock>();
			Dictionary<string, HeroUnlockDescKey> dictionary = new Dictionary<string, HeroUnlockDescKey>();
			foreach (Unlock unlock in list)
			{
				UnlockRewardHero unlockRewardHero = unlock.GetReward() as UnlockRewardHero;
				if (unlockRewardHero != null)
				{
					HeroUnlockDescKey value = new HeroUnlockDescKey
					{
						descKey = "ADVENTURE_STAGE_UNLOCK_HINT",
						amount = unlock.GetReqAmount(),
						isAmountHidden = false
					};
					dictionary.Add(unlockRewardHero.GetHeroId(), value);
				}
			}
			int num = 0;
			foreach (Unlock unlock2 in list2)
			{
				UnlockRewardHero unlockRewardHero2 = unlock2.GetReward() as UnlockRewardHero;
				num++;
				if (unlockRewardHero2 != null)
				{
					HeroUnlockDescKey value2 = new HeroUnlockDescKey
					{
						descKey = "TIME_CHALLENGE_STAGE_UNLOCK_HINT",
						amount = num,
						isAmountHidden = true
					};
					dictionary.Add(unlockRewardHero2.GetHeroId(), value2);
				}
			}
			return dictionary;
		}

		public int GetWorldChallengeCount(GameMode mode)
		{
			World world = this.GetWorld(mode);
			return world.allChallenges.Count;
		}

		public bool HasCharmCard(int id)
		{
			CharmEffectData charmEffectData = this.allCharmEffects[id];
			return charmEffectData.level >= 0;
		}

		public void AddCharmCard(int id, int count)
		{
			CharmEffectData charm = this.allCharmEffects[id];
			this.AddCharmCard(charm, count);
		}

		public void AddCharmCard(CharmDuplicate duplicates)
		{
			this.AddCharmCard(duplicates.charmData, duplicates.count);
		}

		private void AddCharmCard(CharmEffectData charm, int count)
		{
			if (charm.level == -1)
			{
				charm.level = 0;
				this.OnCharmsChanged();
				charm.isNew = true;
			}
			charm.unspendDuplicates += count;
		}

		public List<CharmEffectData> GetAvailableCharms()
		{
			List<CharmEffectData> list = new List<CharmEffectData>();
			foreach (KeyValuePair<int, CharmEffectData> keyValuePair in this.allCharmEffects)
			{
				CharmEffectData value = keyValuePair.Value;
				if (value.level >= 0)
				{
					list.Add(value);
				}
			}
			return list;
		}

		public List<CharmEffectData> GetAllCharms()
		{
			List<CharmEffectData> list = new List<CharmEffectData>(this.allCharmEffects.Count);
			foreach (KeyValuePair<int, CharmEffectData> keyValuePair in this.allCharmEffects)
			{
				CharmEffectData value = keyValuePair.Value;
				list.Add(keyValuePair.Value);
			}
			return list;
		}

		public bool IsAnyCharmReadyToUpgrade()
		{
			foreach (CharmEffectData charmEffectData in this.allCharmEffects.Values)
			{
				if (charmEffectData.CanLevelUp())
				{
					return true;
				}
			}
			return false;
		}

		public int GetCharmId(CharmEffectData charm)
		{
			foreach (KeyValuePair<int, CharmEffectData> keyValuePair in this.allCharmEffects)
			{
				if (keyValuePair.Value == charm)
				{
					return keyValuePair.Key;
				}
			}
			return int.MaxValue;
		}

		public List<CharmDuplicate> GetRandomCharmDuplicatesPreview(int count)
		{
			int seedCharmpack = GameMath.seedCharmpack;
			List<CharmDuplicate> list = new List<CharmDuplicate>();
			Queue<int> queue = new Queue<int>(this.lastFiveOpenedCharms);
			List<int> notOpenedCounters = CharmDataBase.NotOpenedCounters.ToList<int>();
			int num = this.numSmallCharmPacksOpened;
			for (int i = 0; i < this.shopPackBigCharm.numCharms; i++)
			{
				CharmDuplicate randomCharmPack = this.GetRandomCharmPack();
				this.lastFiveOpenedCharms.Enqueue(randomCharmPack.charmData.BaseData.id);
				if (this.lastFiveOpenedCharms.Count > 5)
				{
					this.lastFiveOpenedCharms.Dequeue();
				}
				list.Add(randomCharmPack);
				this.numSmallCharmPacksOpened++;
			}
			GameMath.seedCharmpack = seedCharmpack;
			this.lastFiveOpenedCharms = queue;
			CharmDataBase.NotOpenedCounters = notOpenedCounters;
			this.numSmallCharmPacksOpened = num;
			return list;
		}

		public CharmDuplicate GetRandomCharmPackPreview()
		{
			int seedCharmpack = GameMath.seedCharmpack;
			List<int> notOpenedCounters = CharmDataBase.NotOpenedCounters.ToList<int>();
			CharmDuplicate randomCharmPack = this.GetRandomCharmPack();
			GameMath.seedCharmpack = seedCharmpack;
			CharmDataBase.NotOpenedCounters = notOpenedCounters;
			return randomCharmPack;
		}

		public CharmDuplicate GetRandomCharmPack()
		{
			CharmEffectData randomCharmData = this.GetRandomCharmData();
			int dupplicateCount = Simulator.GetDupplicateCount();
			int dupplicateCount2 = Simulator.GetDupplicateCount();
			int dupplicateCount3 = Simulator.GetDupplicateCount();
			return new CharmDuplicate
			{
				charmData = randomCharmData,
				counts = new int[]
				{
					dupplicateCount,
					dupplicateCount2,
					dupplicateCount3
				}
			};
		}

		private static int GetDupplicateCount()
		{
			List<float> list = CharmDataBase.CharmPackBaseRarityTable.ToList<float>();
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				float x = CharmDataBase.WeightIncreasers[i];
				int packCounter = CharmDataBase.GetPackCounter(i);
				List<float> list2;
				int index;
				(list2 = list)[index = i] = list2[index] + (GameMath.PowFloat(x, (float)packCounter) - 1f);
			}
			int rouletteOutcome = GameMath.GetRouletteOutcome(list, GameMath.RandType.CharmPack);
			IndexRange indexRange = CharmDataBase.CharmPackRarityDupeCounts[rouletteOutcome];
			int randomValue = indexRange.GetRandomValue(GameMath.RandType.CharmPack);
			Simulator.UpdateCharmPackCounter(count, rouletteOutcome);
			return randomValue;
		}

		private static void UpdateCharmPackCounter(int elementCount, int raityIndex)
		{
			for (int i = 0; i < elementCount; i++)
			{
				if (raityIndex == i)
				{
					CharmDataBase.ResetPackCounter(i);
				}
				else
				{
					CharmDataBase.IncrementPackCounter(i);
				}
			}
		}

		public CharmEffectData GetRandomCharmData()
		{
			Dictionary<int, CharmEffectData>.ValueCollection values = this.allCharmEffects.Values;
			List<CharmEffectData> list = values.ToList<CharmEffectData>();
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (this.numSmallCharmPacksOpened < list[i].GetNumPacksRequired())
				{
					list.RemoveAt(i);
				}
			}
			List<float> list2 = new List<float>();
			for (int j = 0; j < list.Count; j++)
			{
				CharmEffectData charmEffectData = list[j];
				float num = charmEffectData.BaseData.dropWeight;
				if (charmEffectData.IsMaxed() || charmEffectData.unspendDuplicates >= CharmEffectData.GetNeededDuplicateToLevelUpToTargetLevel(charmEffectData.level, charmEffectData.BaseData.maxLevel))
				{
					num *= 0.05f;
				}
				if (this.lastFiveOpenedCharms.Contains(charmEffectData.BaseData.id))
				{
					num *= 0.05f;
				}
				list2.Add(num);
			}
			int rouletteOutcome = GameMath.GetRouletteOutcome(list2, GameMath.RandType.CharmPack);
			return list[rouletteOutcome];
		}

		public void OpenRandomCardPack(int count)
		{
			List<CharmDuplicate> cardPack = this.GetCardPack(count);
			foreach (CharmDuplicate charmDuplicate in cardPack)
			{
				this.AddCharmCard(charmDuplicate.charmData, charmDuplicate.count);
			}
		}

		public List<CharmDuplicate> GetCardPack(int count)
		{
			Dictionary<int, CharmEffectData>.ValueCollection values = this.allCharmEffects.Values;
			List<CharmEffectData> list = values.ToList<CharmEffectData>();
			List<CharmDuplicate> list2 = new List<CharmDuplicate>();
			List<float> weights = (from c in values
			select c.BaseData.dropWeight).ToList<float>();
			int[] rouletteOutcome = GameMath.GetRouletteOutcome(weights, count, GameMath.RandType.CharmPack);
			for (int i = 0; i < count; i++)
			{
				CharmEffectData d = list[rouletteOutcome[i]];
				CharmDuplicate charmDuplicate = list2.Find((CharmDuplicate oc) => oc.charmData == d);
				if (charmDuplicate == null)
				{
					charmDuplicate = new CharmDuplicate
					{
						charmData = d,
						counts = new int[3]
					};
					list2.Add(charmDuplicate);
				}
				charmDuplicate.counts[0]++;
			}
			return list2;
		}

		public List<CharmEffectData> GetNextCharmEffects(int count)
		{
			List<CharmEffectData> list = this.GetAvailableCharms();
			ChallengeRift challengeRift = this.worldRift.activeChallenge as ChallengeRift;
			if (challengeRift == null)
			{
				return null;
			}
			foreach (RiftEffect riftEffect in challengeRift.riftEffects)
			{
				list = riftEffect.OnCharmDraft(list);
			}
			List<float> list2 = new List<float>();
			int i = 0;
			int count2 = list.Count;
			while (i < count2)
			{
				float num = 100f;
				foreach (int num2 in challengeRift.discardedCharms)
				{
					if (num2 == list[i].BaseData.id)
					{
						num *= 1f - ChallengeRift.DISCARDED_CARD_SHOWING_UP_REDUCE_FACTOR;
					}
				}
				foreach (CharmEffectData charmEffectData in challengeRift.activeCharmEffects)
				{
					if (charmEffectData.BaseData.id == list[i].BaseData.id)
					{
						num *= 1f - ChallengeRift.CHOSEN_CARD_SHOWING_UP_REDUCE_FACTOR;
					}
				}
				list2.Add(num);
				i++;
			}
			List<CharmEffectData> list3 = new List<CharmEffectData>();
			int seedCharmdraft = GameMath.seedCharmdraft;
			for (int j = 0; j < count; j++)
			{
				int rouletteOutcome = GameMath.GetRouletteOutcome(list2, GameMath.RandType.CharmDraft);
				list3.Add(list[rouletteOutcome]);
				list.RemoveAt(rouletteOutcome);
				list2.RemoveAt(rouletteOutcome);
			}
			this.charmDraftTempSeed = GameMath.seedCharmdraft;
			GameMath.seedCharmdraft = seedCharmdraft;
			return list3;
		}

		public void TryToPickNextCharmEffects()
		{
			if (this.IsActiveMode(GameMode.RIFT))
			{
				ChallengeRift challengeRift = this.worldRift.activeChallenge as ChallengeRift;
				challengeRift.nextCharmDraftEffects = this.GetNextCharmEffects(this.worldRift.GetCharmSelectionNum());
				return;
			}
			throw new Exception("Invalid action: You are not in rift mode");
		}

		public void TryToClaimCharm(int index)
		{
			ChallengeRift challengeRift = this.worldRift.activeChallenge as ChallengeRift;
			challengeRift.numCharmSelection--;
			challengeRift.ClaimCharmEffect(index);
			GameMath.seedCharmdraft = this.charmDraftTempSeed;
		}

		public double GetCharmUpgradeCost(int id)
		{
			CharmEffectData charm = this.allCharmEffects[id];
			return this.GetCharmUpgradeCost(charm);
		}

		public double GetCharmUpgradeCost(CharmEffectData charm)
		{
			return CharmEffectData.GetNeededScrapToLevelUpFromLevel(charm.level);
		}

		public bool CanAffordCharmUpgrade(CharmEffectData charm)
		{
			return this.scraps.CanAfford(this.GetCharmUpgradeCost(charm));
		}

		public void TryUpgradeCharm(CharmEffectData charm)
		{
			if (this.CanAffordCharmUpgrade(charm) && charm.CanLevelUp())
			{
				this.scraps.Decrement(this.GetCharmUpgradeCost(charm));
				charm.LevelUp();
				this.OnCharmsChanged();
			}
		}

		public bool TryBuyFlashOffer(FlashOffer flashOffer, DropPosition currencyDropPosition)
		{
			if (flashOffer.purchasesLeft == 0)
			{
				UnityEngine.Debug.LogError("This offer has already bought");
				return false;
			}
			if (flashOffer.costType == FlashOffer.CostType.AD)
			{
				RewardedAdManager.inst.shouldGiveReward = false;
			}
			CurrencyType? flashOfferCurrencyType = this.GetFlashOfferCurrencyType(flashOffer);
			double num = 0.0;
			bool flag;
			if (flashOfferCurrencyType != null)
			{
				Currency currency = this.GetCurrency(flashOfferCurrencyType.Value);
				num = this.GetFlashOfferCost(flashOffer);
				flag = currency.CanAfford(num);
				if (flag)
				{
					currency.Decrement(num);
				}
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				flashOffer.purchasesLeft--;
				if (flashOffer.purchasesLeft <= 0)
				{
					flashOffer.isBought = true;
				}
				flashOffer.boughtAmount = this.GetFlashOfferCount(flashOffer);
				if (flashOffer.IsCurrencyOffer())
				{
					PlayfabManager.SendPlayerEvent(PlayfabEventId.FLASH_CURRENCY_OFFER_PURCHASED, new Dictionary<string, object>
					{
						{
							"offer_currency",
							flashOffer.type
						},
						{
							"offer_amount",
							flashOffer.boughtAmount
						},
						{
							"spent_currency",
							flashOffer.costType
						},
						{
							"spent_amount",
							num
						}
					}, null, null, true);
				}
				else if (flashOffer.type == FlashOffer.Type.CHARM)
				{
					CharmEffectData charmEffectData = this.allCharmEffects[flashOffer.charmId];
					PlayfabManager.SendPlayerEvent(PlayfabEventId.FLASH_CHARM_OFFER_PURCHASED, new Dictionary<string, object>
					{
						{
							"charm_name",
							LM.GetFromEN(charmEffectData.nameKey)
						},
						{
							"charm_amount",
							flashOffer.boughtAmount
						},
						{
							"spent_currency",
							flashOffer.costType
						},
						{
							"spent_amount",
							num
						}
					}, null, null, true);
				}
				switch (flashOffer.type)
				{
				case FlashOffer.Type.CHARM:
					this.AddCharmCard(flashOffer.charmId, flashOffer.boughtAmount);
					break;
				case FlashOffer.Type.SCRAP:
					this.activeWorld.RainCurrencyOnUi(UiState.BUY_FLASH_OFFER, CurrencyType.SCRAP, (double)flashOffer.boughtAmount, currencyDropPosition, 30, 0f, 0f, 1f, null, 0f);
					break;
				case FlashOffer.Type.GEM:
					this.activeWorld.RainCurrencyOnUi(UiState.BUY_FLASH_OFFER, CurrencyType.GEM, (double)flashOffer.boughtAmount, currencyDropPosition, 30, 0f, 0f, 1f, null, 0f);
					break;
				case FlashOffer.Type.TOKEN:
					this.activeWorld.RainCurrencyOnUi(UiState.BUY_FLASH_OFFER, CurrencyType.TOKEN, (double)flashOffer.boughtAmount, currencyDropPosition, 30, 0f, 0f, 1f, null, 0f);
					break;
				case FlashOffer.Type.RUNE:
					this.boughtRunes.Add(this.allRunes.Find((Rune rune) => rune.id == flashOffer.genericStringId));
					break;
				case FlashOffer.Type.COSTUME:
				{
					SkinData skinData = this.GetSkinData(flashOffer.genericIntId);
					this.boughtSkins.Add(skinData);
					this.SendSkinBuyEvent(flashOffer, num, skinData);
					PlayerStats.OnUserBuySkin(new string[]
					{
						skinData.GetKey()
					});
					break;
				}
				case FlashOffer.Type.TRINKET_PACK:
					this.numTrinketPacks++;
					if (this.numTrinketSlots > this.allTrinkets.Count)
					{
						this.TryOpenTrinketPack(this.shopPackTrinket as ShopPackTrinket, false);
					}
					else
					{
						this.numUnseenTrinketPacks++;
					}
					break;
				case FlashOffer.Type.COSTUME_PLUS_SCRAP:
				{
					SkinData skinData2 = this.GetSkinData(flashOffer.genericIntId);
					this.boughtSkins.Add(skinData2);
					this.activeWorld.RainCurrencyOnUi(UiState.BUY_FLASH_OFFER, CurrencyType.SCRAP, (double)flashOffer.boughtAmount, currencyDropPosition, 30, 0f, 0f, 1f, null, 0f);
					PlayerStats.OnUserBuySkin(new string[]
					{
						skinData2.GetKey()
					});
					this.SendSkinBuyEvent(flashOffer, num, skinData2);
					break;
				}
				case FlashOffer.Type.MERCHANT_ITEM:
				{
					MerchantItem merchantItemWithId = this.GetMerchantItemWithId(flashOffer.genericStringId);
					merchantItemWithId.AddNumInInventory(flashOffer.boughtAmount);
					break;
				}
				default:
					throw new NotImplementedException();
				}
				if (flashOffer.type == FlashOffer.Type.CHARM)
				{
					PlayerStats.BoughtFlashOffer();
				}
				else if (flashOffer.type == FlashOffer.Type.COSTUME_PLUS_SCRAP)
				{
					PlayerStats.BoughtHalloweenFlashOffer();
				}
				else if (flashOffer.costType == FlashOffer.CostType.CANDY)
				{
					PlayerStats.BoughtChristmasFlashOffer();
				}
				else
				{
					PlayerStats.BoughtAdventureFlashOffer();
				}
				return true;
			}
			return false;
		}

		private void SendSkinBuyEvent(FlashOffer flashOffer, double cost, SkinData costume)
		{
			string value = "HERO_SCREEN";
			if (flashOffer != null)
			{
				if (flashOffer.isAnniverary)
				{
					value = "BIRTHDAY_OFFER";
				}
				else
				{
					value = "FLASH_OFFER";
				}
			}
			PlayfabManager.SendPlayerEvent(PlayfabEventId.HERO_SKIN_PURCHASED, new Dictionary<string, object>
			{
				{
					"hero",
					costume.belongsTo.GetId()
				},
				{
					"skin",
					LM.GetFromEN(costume.nameKey)
				},
				{
					"spent_currency",
					(flashOffer != null) ? flashOffer.costType.ToString() : costume.currency.ToString()
				},
				{
					"spent_amount",
					cost
				},
				{
					"placement",
					value
				}
			}, null, null, true);
		}

		public int GetFlashOfferCount(FlashOffer offer)
		{
			FlashOffer.Type type = offer.type;
			if (type == FlashOffer.Type.CHARM)
			{
				return RiftQuest.FLASH_OFFER_COUNTS[this.riftDiscoveryIndex];
			}
			if (type == FlashOffer.Type.COSTUME_PLUS_SCRAP)
			{
				return FlashOfferFactory.HallowenOffers.Find((FlashOfferFactory.AdventureOfferInfo o) => o.flashOffer.AreEquals(offer, false)).amount;
			}
			if (offer.isHalloween)
			{
				return FlashOfferFactory.HallowenOffers.Find((FlashOfferFactory.AdventureOfferInfo o) => o.flashOffer.AreEquals(offer, false)).amount;
			}
			if (offer.isAnniverary)
			{
				return FlashOfferFactory.SecondAnniversaryOffers.Find((FlashOfferFactory.AdventureOfferInfo o) => o.flashOffer.AreEquals(offer, false)).amount;
			}
			if (offer.isCrhistmas)
			{
				foreach (List<CalendarTreeOfferNode> list in this.christmasOfferBundle.tree)
				{
					foreach (CalendarTreeOfferNode calendarTreeOfferNode in list)
					{
						if (calendarTreeOfferNode.offer == offer)
						{
							return (int)calendarTreeOfferNode.offerAmount;
						}
					}
				}
				return 0;
			}
			return FlashOfferFactory.AdventureOffers.Find((FlashOfferFactory.AdventureOfferInfo o) => o.flashOffer.AreEquals(offer, true)).amount;
		}

		public bool CanAffordFlashOffer(FlashOffer offer)
		{
			CurrencyType? flashOfferCurrencyType = this.GetFlashOfferCurrencyType(offer);
			if (flashOfferCurrencyType != null)
			{
				Currency currency = this.GetCurrency(flashOfferCurrencyType.Value);
				return currency.CanAfford(this.GetFlashOfferCost(offer));
			}
			return offer.costType != FlashOffer.CostType.AD || (RewardedAdManager.inst != null && RewardedAdManager.inst.IsRewardedVideoAvailable());
		}

		public double GetFlashOfferCost(FlashOffer offer)
		{
			FlashOffer.Type type = offer.type;
			switch (type)
			{
			case FlashOffer.Type.COSTUME:
			{
				SkinData skinData = this.GetSkinData(offer.genericIntId);
				if (offer.isAnniverary || offer.isCrhistmas || !offer.PurchaseRequiresCurrency(skinData.currency))
				{
					return this.GetDefaultOfferCost(offer);
				}
				double result;
				if (FlashOfferFactory.DiscountedCostumeCosts.TryGetValue(skinData.cost, out result))
				{
					return result;
				}
				return skinData.cost * 0.3;
			}
			default:
				if (type != FlashOffer.Type.CHARM)
				{
					return this.GetDefaultOfferCost(offer);
				}
				return 100.0;
			case FlashOffer.Type.COSTUME_PLUS_SCRAP:
				return FlashOfferFactory.HallowenOffers.Find((FlashOfferFactory.AdventureOfferInfo o) => o.flashOffer.AreEquals(offer, false)).cost;
			}
		}

		private double GetDefaultOfferCost(FlashOffer offer)
		{
			if (offer.isHalloween)
			{
				return FlashOfferFactory.HallowenOffers.Find((FlashOfferFactory.AdventureOfferInfo o) => o.flashOffer.AreEquals(offer, false)).cost;
			}
			if (offer.isAnniverary)
			{
				FlashOfferFactory.AdventureOfferInfo adventureOfferInfo = FlashOfferFactory.SecondAnniversaryOffers.Find((FlashOfferFactory.AdventureOfferInfo o) => o.flashOffer.AreEquals(offer, false));
				return adventureOfferInfo.cost;
			}
			if (offer.isCrhistmas)
			{
				foreach (List<CalendarTreeOfferNode> list in this.christmasOfferBundle.tree)
				{
					foreach (CalendarTreeOfferNode calendarTreeOfferNode in list)
					{
						if (calendarTreeOfferNode.offer == offer)
						{
							return calendarTreeOfferNode.offerCost;
						}
					}
				}
				return double.PositiveInfinity;
			}
			return FlashOfferFactory.AdventureOffers.Find((FlashOfferFactory.AdventureOfferInfo o) => o.flashOffer.AreEquals(offer, true)).cost;
		}

		public CurrencyType? GetFlashOfferCurrencyType(FlashOffer offer)
		{
			switch (offer.costType)
			{
			case FlashOffer.CostType.GEM:
				return new CurrencyType?(CurrencyType.GEM);
			case FlashOffer.CostType.AD:
			case FlashOffer.CostType.FREE:
				return null;
			case FlashOffer.CostType.CANDY:
				return new CurrencyType?(CurrencyType.CANDY);
			default:
				throw new NotImplementedException();
			}
		}

		public int GetFlashOfferPurchasesAllowedCount(FlashOffer offer)
		{
			FlashOffer.Type type = offer.type;
			if (type == FlashOffer.Type.CHARM)
			{
				return 1;
			}
			if (type == FlashOffer.Type.COSTUME_PLUS_SCRAP)
			{
				return FlashOfferFactory.HallowenOffers.Find((FlashOfferFactory.AdventureOfferInfo o) => o.flashOffer.AreEquals(offer, false)).stock;
			}
			if (offer.isHalloween)
			{
				return FlashOfferFactory.HallowenOffers.Find((FlashOfferFactory.AdventureOfferInfo o) => o.flashOffer.AreEquals(offer, false)).stock;
			}
			if (offer.isAnniverary)
			{
				return FlashOfferFactory.SecondAnniversaryOffers.Find((FlashOfferFactory.AdventureOfferInfo o) => o.flashOffer.AreEquals(offer, false)).stock;
			}
			if (offer.isCrhistmas)
			{
				return 1;
			}
			return FlashOfferFactory.AdventureOffers.Find((FlashOfferFactory.AdventureOfferInfo o) => o.flashOffer.AreEquals(offer, true)).stock;
		}

		public bool IsFlashOfferLocked(FlashOffer offer)
		{
			FlashOfferFactory.AdventureOfferInfo.EventUnlockInfo eventUnlockInfo = null;
			if (offer.isAnniverary)
			{
				eventUnlockInfo = FlashOfferFactory.SecondAnniversaryOffers.Find((FlashOfferFactory.AdventureOfferInfo o) => o.flashOffer.AreEquals(offer, false)).eventUnlockInfo;
			}
			else if (offer.isHalloween)
			{
				eventUnlockInfo = FlashOfferFactory.HallowenOffers.Find((FlashOfferFactory.AdventureOfferInfo o) => o.flashOffer.AreEquals(offer, false)).eventUnlockInfo;
			}
			else if (!offer.isCrhistmas && offer.type != FlashOffer.Type.CHARM)
			{
				eventUnlockInfo = FlashOfferFactory.AdventureOffers.Find((FlashOfferFactory.AdventureOfferInfo o) => o.flashOffer.AreEquals(offer, true)).eventUnlockInfo;
			}
			if (eventUnlockInfo == null)
			{
				return false;
			}
			if (!TrustedTime.IsReady())
			{
				return true;
			}
			DateTime startDate;
			DateTime endDate;
			if (string.IsNullOrEmpty(eventUnlockInfo.internalEventId))
			{
				EventConfig eventConfig = PlayfabManager.eventsInfo.GetEventConfig(eventUnlockInfo.eventId);
				if (eventConfig == null)
				{
					return true;
				}
				startDate = eventConfig.startDate;
				endDate = eventConfig.endDate;
			}
			else
			{
				EventConfig.InternalEvent internalEventConfig = PlayfabManager.eventsInfo.GetInternalEventConfig(eventUnlockInfo.eventId, eventUnlockInfo.internalEventId);
				if (internalEventConfig == null)
				{
					return true;
				}
				startDate = internalEventConfig.startDate;
				endDate = internalEventConfig.endDate;
			}
			DateTime t = TrustedTime.Get();
			return t < startDate || t > endDate;
		}

		public DateTime? GetFlashOfferUnlockDate(FlashOffer offer)
		{
			FlashOfferFactory.AdventureOfferInfo.EventUnlockInfo eventUnlockInfo = null;
			if (offer.isAnniverary)
			{
				eventUnlockInfo = FlashOfferFactory.SecondAnniversaryOffers.Find((FlashOfferFactory.AdventureOfferInfo o) => o.flashOffer.AreEquals(offer, false)).eventUnlockInfo;
			}
			else if (offer.isHalloween)
			{
				eventUnlockInfo = FlashOfferFactory.HallowenOffers.Find((FlashOfferFactory.AdventureOfferInfo o) => o.flashOffer.AreEquals(offer, false)).eventUnlockInfo;
			}
			else if (!offer.isCrhistmas && offer.type != FlashOffer.Type.CHARM)
			{
				eventUnlockInfo = FlashOfferFactory.AdventureOffers.Find((FlashOfferFactory.AdventureOfferInfo o) => o.flashOffer.AreEquals(offer, true)).eventUnlockInfo;
			}
			if (eventUnlockInfo == null)
			{
				return null;
			}
			DateTime? result = null;
			if (string.IsNullOrEmpty(eventUnlockInfo.internalEventId))
			{
				EventConfig eventConfig = PlayfabManager.eventsInfo.GetEventConfig(eventUnlockInfo.eventId);
				if (eventConfig != null)
				{
					result = new DateTime?(eventConfig.startDate);
				}
			}
			else
			{
				EventConfig.InternalEvent internalEventConfig = PlayfabManager.eventsInfo.GetInternalEventConfig(eventUnlockInfo.eventId, eventUnlockInfo.internalEventId);
				if (internalEventConfig != null)
				{
					result = new DateTime?(internalEventConfig.startDate);
				}
			}
			return result;
		}

		public int GetTotalLevelsOfCharmType(CharmType charmType)
		{
			int num = 0;
			foreach (KeyValuePair<int, CharmEffectData> keyValuePair in this.allCharmEffects)
			{
				CharmEffectData value = keyValuePair.Value;
				if (value.BaseData.charmType == charmType && value.level >= 0)
				{
					num += value.level + 1;
				}
			}
			return num;
		}

		private void AddRiftDust(double amount)
		{
			this.riftQuestDustCollected += amount;
		}

		public bool IsRiftQuestCompleted()
		{
			return this.hasRiftQuest && this.riftQuestDustCollected >= this.GetRiftQuestDustRequired();
		}

		public double GetDaysPassedSinceLastTimeRiftRestBonusCollected()
		{
			return (TrustedTime.Get() - this.riftRestRewardCollectedTime).TotalSeconds / 86400.0;
		}

		public double GetRiftRestBonusPerDay()
		{
			return RiftQuest.REST_REWARDS[this.riftDiscoveryIndex];
		}

		private double GetRiftRestBonusPerDay(int discoveryIndex)
		{
			return RiftQuest.REST_REWARDS[discoveryIndex];
		}

		public double GetCurrentRiftQuestStandardReward()
		{
			return RiftQuest.AEON_REWARDS[this.riftDiscoveryIndex];
		}

		public double GetRiftQuestStandardReward(int discoveryIndex)
		{
			return RiftQuest.AEON_REWARDS[discoveryIndex];
		}

		public double GetCurrentRiftQuestRestReward()
		{
			if (!this.IsRestBonusAvailable())
			{
				return 0.0;
			}
			if (TrustedTime.IsReady())
			{
				double num = GameMath.Clamp(this.GetDaysPassedSinceLastTimeRiftRestBonusCollected(), 0.0, 1.0);
				return this.GetRiftRestBonusPerDay() * num;
			}
			return 0.0;
		}

		public double GetRiftQuestRestReward(int discoveryIndex)
		{
			if (!this.IsRestBonusAvailable())
			{
				return 0.0;
			}
			if (TrustedTime.IsReady())
			{
				double num = GameMath.Clamp(this.GetDaysPassedSinceLastTimeRiftRestBonusCollected(), 0.0, 1.0);
				return this.GetRiftRestBonusPerDay(discoveryIndex) * num;
			}
			return 0.0;
		}

		public bool IsRiftQuestRestBonusCapped()
		{
			return TrustedTime.IsReady() && this.GetDaysPassedSinceLastTimeRiftRestBonusCollected() >= 1.0;
		}

		public double GetTotalAeonRewardFromRiftQuest()
		{
			return this.GetCurrentRiftQuestRestReward() + this.GetCurrentRiftQuestStandardReward();
		}

		public bool IsRestBonusAvailable()
		{
			return this.numRiftQuestsCompleted > 0;
		}

		public void TryCollectRiftReward(Vector3 startPos, Transform invTransform)
		{
			if (this.IsRiftQuestCompleted())
			{
				double totalAeonRewardFromRiftQuest = this.GetTotalAeonRewardFromRiftQuest();
				DropPosition dropPos = new DropPosition
				{
					startPos = startPos,
					endPos = startPos + Vector3.down * 0.1f,
					invPos = invTransform.position - Vector3.right * 0.425f,
					showSideCurrency = true,
					targetToScaleOnReach = invTransform
				};
				this.activeWorld.RainCurrencyOnUi(UiState.HUB, CurrencyType.AEON, totalAeonRewardFromRiftQuest, dropPos, 30, 0f, 0f, 1f, null, 0f);
				if (TrustedTime.IsReady())
				{
					this.riftRestRewardCollectedTime = TrustedTime.Get();
				}
				this.riftQuestDustCollected -= this.GetRiftQuestDustRequired();
				this.numRiftQuestsCompleted++;
			}
		}

		public double GetRiftQuestProgress(double scoreToAdd)
		{
			double riftQuestDustRequired = this.GetRiftQuestDustRequired();
			double num = this.riftQuestDustCollected;
			return GameMath.Clamp((num + scoreToAdd) / riftQuestDustRequired, 0.0, 1.0);
		}

		public double GetRiftQuestDustRequired()
		{
			return RiftQuest.DUST_REQUIRED[GameMath.GetMinInt(this.riftDiscoveryIndex, RiftQuest.DUST_REQUIRED.Length - 1)];
		}

		public double GetRiftQuestDustRequired(int discoveryIndex)
		{
			return RiftQuest.DUST_REQUIRED[GameMath.GetMinInt(discoveryIndex, RiftQuest.DUST_REQUIRED.Length - 1)];
		}

		public int GetLastUnlockedRiftChallengeIndex()
		{
			for (int i = this.worldRift.allChallenges.Count - 1; i >= 0; i--)
			{
				ChallengeRift challengeRift = this.worldRift.allChallenges[i] as ChallengeRift;
				if (challengeRift.unlock.isCollected)
				{
					return GameMath.Clamp(i + 1, 0, this.worldRift.allChallenges.Count - 1);
				}
			}
			return 0;
		}

		public bool CanAffordSmallCharmPack()
		{
			return this.numSmallCharmPacks > 0 || this.aeons.CanAfford(this.shopPackSmallCharm.cost);
		}

		public bool CanAffordBigCharmPack()
		{
			return this.aeons.CanAfford(this.shopPackBigCharm.cost);
		}

		public void OpenBigCharmPack()
		{
			if (this.aeons.CanAfford(this.shopPackBigCharm.cost))
			{
				this.aeons.Decrement(this.shopPackBigCharm.cost);
				for (int i = 0; i < this.shopPackBigCharm.numCharms; i++)
				{
					CharmDuplicate randomCharmPack = this.GetRandomCharmPack();
					this.lastFiveOpenedCharms.Enqueue(randomCharmPack.charmData.BaseData.id);
					if (this.lastFiveOpenedCharms.Count > 5)
					{
						this.lastFiveOpenedCharms.Dequeue();
					}
					this.AddCharmCard(randomCharmPack);
					PlayerStats.allTimeCharmPacksOpened++;
					this.numSmallCharmPacksOpened++;
				}
				return;
			}
			throw new Exception();
		}

		public void OpenSmallCharmPack()
		{
			if (this.numSmallCharmPacks > 0)
			{
				this.numSmallCharmPacks--;
			}
			else
			{
				if (!this.aeons.CanAfford(this.shopPackSmallCharm.cost))
				{
					throw new Exception();
				}
				this.aeons.Decrement(this.shopPackSmallCharm.cost);
			}
			CharmDuplicate randomCharmPack = this.GetRandomCharmPack();
			this.lastFiveOpenedCharms.Enqueue(randomCharmPack.charmData.BaseData.id);
			if (this.lastFiveOpenedCharms.Count > 5)
			{
				this.lastFiveOpenedCharms.Dequeue();
			}
			this.AddCharmCard(randomCharmPack);
			PlayerStats.allTimeCharmPacksOpened++;
			this.numSmallCharmPacksOpened++;
		}

		public bool HasRiftDiscover()
		{
			ChallengeRift challengeRift = this.worldRift.allChallenges.GetLastItem<Challenge>() as ChallengeRift;
			return challengeRift.discoveryIndex > this.riftDiscoveryIndex;
		}

		public bool IsNextRiftsDiscoverable()
		{
			if (this.riftDiscoveryIndex >= this.maxRiftDiscoveryIndex)
			{
				return false;
			}
			bool flag = true;
			bool flag2 = false;
			for (int i = this.worldRift.allChallenges.Count - 1; i >= 0; i--)
			{
				ChallengeRift challengeRift = this.worldRift.allChallenges[i] as ChallengeRift;
				if (challengeRift.discoveryIndex == this.riftDiscoveryIndex)
				{
					flag2 = true;
					if (!challengeRift.unlock.isCollected)
					{
						flag = false;
					}
				}
			}
			return flag2 && flag;
		}

		public int GetRiftCountWillDiscover()
		{
			int num = 0;
			foreach (Challenge challenge in this.worldRift.allChallenges)
			{
				ChallengeRift challengeRift = (ChallengeRift)challenge;
				if (challengeRift.discoveryIndex == this.riftDiscoveryIndex + 1)
				{
					num++;
				}
			}
			return num;
		}

		public int GetDiscoveredRiftCount()
		{
			int num = 0;
			foreach (Challenge challenge in this.worldRift.allChallenges)
			{
				ChallengeRift challengeRift = (ChallengeRift)challenge;
				if (challengeRift.discoveryIndex <= this.riftDiscoveryIndex)
				{
					num++;
				}
			}
			return num;
		}

		public bool IsCursedRiftsModeUnlocked()
		{
			return this.GetLastUnlockedRiftChallengeIndex() >= 20;
		}

		public List<ChallengeRift> GetCursedRiftChallenges()
		{
			List<ChallengeRift> list = new List<ChallengeRift>();
			foreach (Challenge challenge in this.worldRift.cursedChallenges)
			{
				ChallengeRift item = (ChallengeRift)challenge;
				list.Add(item);
			}
			return list;
		}

		public List<ChallengeRift> GetDiscoveredRiftChallenges()
		{
			List<ChallengeRift> list = new List<ChallengeRift>();
			foreach (Challenge challenge in this.worldRift.allChallenges)
			{
				ChallengeRift challengeRift = (ChallengeRift)challenge;
				if (challengeRift.discoveryIndex <= this.riftDiscoveryIndex)
				{
					list.Add(challengeRift);
				}
			}
			return list;
		}

		public List<ChallengeRift> GetRiftChallengesFromDiscoveryIndex(int discIndex)
		{
			List<ChallengeRift> list = new List<ChallengeRift>();
			foreach (Challenge challenge in this.worldRift.allChallenges)
			{
				ChallengeRift challengeRift = (ChallengeRift)challenge;
				if (challengeRift.discoveryIndex == discIndex)
				{
					list.Add(challengeRift);
				}
			}
			return list;
		}

		public List<int> GetRandomRiftIndexes(int count)
		{
			int latestBeatenRiftChallengeIndex = this.worldRift.GetLatestBeatenRiftChallengeIndex();
			List<int> list = new List<int>(latestBeatenRiftChallengeIndex);
			for (int i = 0; i < latestBeatenRiftChallengeIndex; i++)
			{
				list.Add(i);
			}
			List<Challenge> cursedChallenges = this.worldRift.cursedChallenges;
			if (cursedChallenges != null)
			{
				foreach (Challenge challenge in cursedChallenges)
				{
					ChallengeRift challengeRift = (ChallengeRift)challenge;
					int num = list.IndexOf(challengeRift.riftData.cursesSetup.originalRiftNo);
					if (num != -1)
					{
						list.RemoveAt(num);
					}
				}
			}
			List<int> list2 = new List<int>(count);
			for (int j = 0; j < count; j++)
			{
				list2.Add(list.PopRandomItem(GameMath.RandType.CursedGate));
			}
			return list2;
		}

		public ChallengeRift GenerateCursedRift(int index, int discoveryIndex, World world)
		{
			ChallengeRift toCopy = this.worldRift.allChallenges[index] as ChallengeRift;
			return this.GenerateCursedRift(toCopy, index, discoveryIndex, world);
		}

		public ChallengeRift GenerateCursedRift(ChallengeRift toCopy, int challengeNo, int discoveryIndex, World world)
		{
			RiftData riftData = toCopy.riftData.Clone();
			List<ChallengeRift> riftChallengesFromDiscoveryIndex = this.GetRiftChallengesFromDiscoveryIndex(discoveryIndex);
			ChallengeRift challengeRift = riftChallengesFromDiscoveryIndex[0];
			riftData.startLevel = challengeRift.riftData.startLevel;
			riftData.difLevel = challengeRift.riftData.difLevel;
			riftData.discovery = challengeRift.riftData.discovery;
			riftData.cursesSetup = RiftFactory.GetCurseSetupFor(toCopy, challengeNo);
			riftData.effects.Add(new RiftEffectCurse());
			ChallengeRift challengeRift2 = RiftFactory.CreateRiftChallengeProcedurallyByLevel(riftData, challengeNo);
			challengeRift2.Init(world);
			challengeRift2.Reset();
			double amount = RiftQuest.CURSED_GATE_SCRAP_REWARD[discoveryIndex];
			challengeRift2.unlock = new Unlock(0u, challengeRift2.world, new UnlockReqRiftChallenge(challengeRift2), new UnlockRewardCurrency(CurrencyType.SCRAP, amount));
			return challengeRift2;
		}

		public void DiscoverNextSetOfRifts()
		{
			this.riftDiscoveryIndex++;
		}

		public int GetItemPosition<T>(List<T> list, T obj, IComparer<T> comparer)
		{
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				if (comparer.Compare(obj, list[i]) <= 0)
				{
					return i;
				}
			}
			return count;
		}

		public List<CharmEffectData> GetSortedCharms()
		{
			List<CharmEffectData> allCharms = this.GetAllCharms();
			CharmComparer.isDescending = this.isCharmSortingDescending;
			CharmComparer.sortType = this.charmSortType;
			allCharms.BubbleSort(this.charmComparer.generalSorter);
			return allCharms;
		}

		public bool HasSpecialOffer()
		{
			return this.specialOfferBoard.GetAliveSpecialOfferCount() > 0;
		}

		public bool IsRiftShopUnlocked()
		{
			return this.numSmallCharmPacks > 0 || PlayerStats.allTimeCharmPacksOpened > 0 || this.worldRift.GetLatestBeatenRiftChallengeIndex() >= 2;
		}

		public void PickRandomCurses()
		{
			this.currentCurses = new List<int>();
			int num;
			if (this.cursedGatesBeaten < Simulator.NUM_CURSES_BASED_ON_CURSED_GATES_BEATEN.Length)
			{
				num = Simulator.NUM_CURSES_BASED_ON_CURSED_GATES_BEATEN[this.cursedGatesBeaten];
			}
			else
			{
				num = GameMath.GetRandomInt(2, 5, GameMath.RandType.NewCurses);
			}
			if (num < 0)
			{
				num = GameMath.GetRandomInt(2, 5, GameMath.RandType.NewCurses);
			}
			List<int> list = new List<int>();
			List<float> list2 = new List<float>();
			foreach (KeyValuePair<int, CurseEffectData> keyValuePair in this.allCurseEffects)
			{
				list.Add(keyValuePair.Key);
				list2.Add(keyValuePair.Value.GetWeight());
			}
			for (int i = 0; i < num; i++)
			{
				int rouletteOutcome = GameMath.GetRouletteOutcome(list2, GameMath.RandType.NewCurses);
				this.currentCurses.Add(list[rouletteOutcome]);
				list2.RemoveAt(rouletteOutcome);
				list.RemoveAt(rouletteOutcome);
			}
		}

		public void RecalculateUnlockedCursedRiftSlots()
		{
			this.cursedRiftSlots.Load(this.worldRift);
		}

		public int GetEmptyCursedRiftSlotCount()
		{
			return this.cursedRiftSlots.slotCount - this.worldRift.cursedChallenges.Count;
		}

		public void OnNewOfferAnnounced(ShopPack offer)
		{
			if (offer != null && offer.id != null)
			{
				DateTime announcingTime = (!TrustedTime.IsReady()) ? DateTime.MinValue : TrustedTime.Get();
				Simulator.AnnouncedOfferInfo announcedOfferInfo = this.announcedOffersTimes.Find((Simulator.AnnouncedOfferInfo o) => o.offerId == offer.id.Value);
				if (announcedOfferInfo == null)
				{
					this.announcedOffersTimes.Add(new Simulator.AnnouncedOfferInfo
					{
						offerId = offer.id.Value,
						announcingTime = announcingTime
					});
				}
				else
				{
					announcedOfferInfo.announcingTime = announcingTime;
				}
			}
		}

		public FlashOffer GetActiveOfferWithScrapsForSkin(SkinData skin)
		{
			FlashOffer flashOffer = null;
			if (this.flashOfferBundle != null && this.flashOfferBundle.adventureOffers != null)
			{
				flashOffer = this.flashOfferBundle.adventureOffers.Find((FlashOffer o) => o.type == FlashOffer.Type.COSTUME_PLUS_SCRAP && o.genericIntId == skin.id);
			}
			if (flashOffer == null && this.halloweenFlashOfferBundle != null)
			{
				flashOffer = this.halloweenFlashOfferBundle.offers.Find((FlashOffer o) => o.type == FlashOffer.Type.COSTUME_PLUS_SCRAP && o.genericIntId == skin.id);
			}
			return flashOffer;
		}

		public bool HasTrinketSmithHint()
		{
			return this.worldStandard.GetMaxStageReached() >= 800;
		}

		public DateTime GetLastCappedCurrencyWatchedTime(CurrencyType currencyType)
		{
			if (currencyType == CurrencyType.GEM)
			{
				return this.lastCappedWatchedTime;
			}
			if (currencyType != CurrencyType.CANDY)
			{
				throw new NotImplementedException();
			}
			return this.lastCappedCandiesWatchedTime;
		}

		public void SetLastCappedCurrencyWatchedTime(CurrencyType currencyType, DateTime dateTime)
		{
			if (currencyType != CurrencyType.GEM)
			{
				if (currencyType != CurrencyType.CANDY)
				{
					throw new NotImplementedException();
				}
				this.lastCappedCandiesWatchedTime = dateTime;
			}
			else
			{
				this.lastCappedWatchedTime = dateTime;
			}
		}

		public bool IsChristmasTreeEnabled()
		{
			return false;
		}

		public bool IsSecondAnniversaryEventEnabled()
		{
			return this.secondAnniversaryEventEnabled || !this.secondAnniversaryEventAlreadyDisabled;
		}

		public TimeSpan GetDailtyCapResetTimer()
		{
			if (Cheats.shortOfferTimes && Cheats.shortOfferTimeSettings.seasonal)
			{
				return this.lastCandyAmountCapDailyReset.AddSeconds((double)(Cheats.shortOfferTimeMultiplier * 60f)) - TrustedTime.Get();
			}
			return this.lastCandyAmountCapDailyReset.AddSeconds(36000.0) - TrustedTime.Get();
		}

		public bool ShouldShowStagesRearrengeWarning()
		{
			return this.isStageRearrangeSurviver && this.numPrestigesSinceCataclysm < 5;
		}

		private void InitSecondAnniversaryOffersBundle()
		{
			this.secondAnniversaryFlashOffersBundle = ServerSideFlashOfferBundle.CreateSecondAnniversaryBundle();
			foreach (FlashOffer flashOffer in this.secondAnniversaryFlashOffersBundle.offers)
			{
				if (flashOffer.type == FlashOffer.Type.COSTUME || flashOffer.type == FlashOffer.Type.COSTUME_PLUS_SCRAP)
				{
					bool flag = this.IsSkinBought(flashOffer.genericIntId);
					if (flag)
					{
						flashOffer.purchasesLeft = 0;
						flashOffer.isBought = true;
					}
				}
			}
		}

		public bool IsThereNotificationsPendingFromSecondAnniversaryOffer()
		{
			return (this.specialOfferBoard != null && !this.specialOfferBoard.hasAllSeenOutOfShop) || (this.secondAnniversaryFlashOffersBundle != null && this.secondAnniversaryFlashOffersBundle.offers.Find((FlashOffer o) => o.costType == FlashOffer.CostType.FREE && o.purchasesLeft > 0 && !this.IsFlashOfferLocked(o)) != null);
		}

		public static readonly DateTime ChristmasFirstPopupDate = new DateTime(2019, 1, 14, 10, 0, 0);

		public const float FREE_CANDY_TREAT_PERIOD = 36000f;

		public static readonly double[] GEAR_LEVEL_UP_COSTS = new double[]
		{
			20.0,
			100.0,
			250.0,
			500.0,
			1500.0,
			-1.0
		};

		public static readonly double[] GEAR_CONVERT_TO_SCRAP_AMOUNTS = new double[]
		{
			10.0,
			25.0,
			75.0,
			150.0,
			300.0,
			600.0,
			-1.0
		};

		public static double[] GEAR_EFFICIENCY = new double[]
		{
			10.0,
			25.0,
			100.0,
			250.0,
			1000.0,
			2000.0,
			-1.0
		};

		public static int[] NUM_CURSES_BASED_ON_CURSED_GATES_BEATEN = new int[]
		{
			1,
			2,
			3
		};

		public const double SECONDS_IN_ONE_DAY = 86400.0;

		public const int NUM_RUNES_WORN_MAX = 3;

		public const int GOG_LEVEL_INDEX_THAT_TRIGGERS_RATING = 4;

		public static double[] DAILY_SKIP_COSTS = new double[]
		{
			15.0,
			20.0,
			25.0,
			30.0
		};

		public ShopPack[] lootpacks;

		public Dictionary<string, int> lootpacksOpenedCount;

		public const int NUM_BASE_CHARM_SELECTION = 3;

		public const int LOOTPACK_FREE_MAX_STACK = 1;

		public const int REQUIRED_STAGE_FOR_TIME_CHALLENGE = 120;

		public const int REQUIRED_STAGE_FOR_RIFT = 700;

		public const double PRESTIGE_REWARD_MULT = 2.0;

		public const double PRESTIGE_REWARD_MULT_COST = 100.0;

		public const int NUM_PRESTIGES_SINCE_CATACLYSM_TO_DISABLE_WARNING = 5;

		public const double AMOUNT_GEMS_SPENT_TO_UNLOCK_FLASH_OFFERS = 1000.0;

		private bool isMerchantUnlocked;

		public DateTime lootpackFreeLastOpenTime;

		public DateTime lootpackFreeLastOpenTimeServer;

		private DateTime lastCappedWatchedTime;

		private DateTime lastCappedCandiesWatchedTime;

		public long lastSaveTime;

		public double prestigeRunTimer;

		public PrestigeRunStat lastPrestigeRunstats;

		public double lastPrestigeDuration;

		public bool hasCompass;

		public bool hasDailies;

		public bool hasSkillPointsAutoDistribution;

		private Currency mythstones;

		private Currency scraps;

		private Currency credits;

		private Currency tokens;

		private Currency aeons;

		private Currency candies;

		public List<TotemDataBase> allTotems;

		private HashSet<string> unlockedTotemIds;

		private List<HeroDataBase> allHeroes;

		private HashSet<string> unlockedHeroIds;

		public HashSet<string> newHeroIconSelectedHeroIds;

		private List<GearData> allGears;

		private List<Gear> boughtGears;

		private List<SkinData> allSkins;

		private List<SkinData> boughtSkins;

		public Dictionary<string, KeyValuePair<int, int>> skinIndexRangePerHero = new Dictionary<string, KeyValuePair<int, int>>();

		private List<Rune> allRunes;

		private List<Rune> boughtRunes;

		private List<Rune> rewardedRunes;

		private Dictionary<string, List<Rune>> wornRunes;

		private UniversalTotalBonus universalTotalBonus;

		private UniversalTotalBonus universalTotalBonusRift;

		private GameMode currentGameMode;

		private World activeWorld;

		private World worldStandard;

		private World worldCrusade;

		private World worldRift;

		private List<World> allWorlds;

		private List<Unlock> oldUnlocks;

		public QuestOfUpdate questOfUpdate;

		public List<int> completedQuestOfUpdates;

		public List<int> failedQuestOfUpdates;

		public double creditsBeforeOpeningLootpack;

		public double scrapsBeforeOpeningLootpack;

		public double tokensBeforeOpeningLootpack;

		public double lootDropCredits;

		public double lootDropTokens;

		public double lootDropScraps;

		public List<Gear> lootDropGears;

		public List<GearChange> lootDropGearChanges;

		public List<Rune> lootDropRunes;

		public List<Trinket> lootDropTrinkets;

		public Dictionary<string, List<Gear>> boughtHeroesCosmetic;

		public static Unlock unlockCompass;

		public int numPrestiges;

		public int maxStagePrestigedAt;

		public static readonly double[] CREDIT_PACKS_AMOUNT = new double[]
		{
			100.0,
			500.0,
			1375.0,
			6000.0,
			16250.0,
			35000.0
		};

		private readonly int REVEAL_ALL_TRINKET_EFFECT_THRESHOLD = 30;

		public const double CANDY_PACK_AMOUNT = 12500.0;

		public bool prefers30Fps;

		public bool setSoundsMute;

		public bool setMusicMute;

		public bool setVoicesMute;

		public bool skillOneTapUpgrade;

		public bool askedForRate;

		public RatingState ratingState;

		public bool shouldAskForRate;

		public bool compassDisabled;

		public bool appNeverSleep;

		public bool scientificNotation;

		public bool secondaryCdUi;

		public bool isActiveWorldPaused;

		public List<Trinket> allTrinkets;

		public Dictionary<int, int> disassembledTinketEffects;

		public bool hasEverOwnedATrinket;

		public int numTrinketSlots;

		public int numTrinketsObtained;

		public SpecialOfferKeeper selectedSpecialOfferKeeper;

		public DateTime lastOfferEndTime;

		public DateTime lastRiftOfferEndTime;

		public bool shopOfferNotification;

		public ShopPack shopPackTrinket;

		public ShopPack shopPackSmallCharm;

		public ShopPack shopPackBigCharm;

		public int numTrinketPacks;

		public int numUnseenTrinketPacks;

		public int numOffersAppeared;

		public List<Simulator.AnnouncedOfferInfo> announcedOffersTimes = new List<Simulator.AnnouncedOfferInfo>();

		public static Dictionary<string, bool> achievementCollecteds;

		public static Dictionary<string, double> achievementRewards;

		public List<int> trinketsPinned;

		public Dictionary<Trinket, int> trinketsPinnedHashSet;

		public MineToken mineToken;

		public MineScrap mineScrap;

		public DailyQuest dailyQuest;

		public bool forceDropCandy;

		public bool forceDropChestCandy;

		public DateTime lastCandyAmountCapDailyReset = DateTime.MinValue;

		public double candyAmountCollectedSinceLastDailyCapReset;

		public DateTime dailyQuestCollectedTime;

		public int lastDailyQuest;

		public int dailySkipCount;

		public int dailyQuestsAppearedCount;

		public bool isMineEverCollected;

		public bool isSkinsEverClicked;

		public int numCandyQuestCompleted;

		public int numCandyQuestAnniversaryCompleted;

		public bool hasRiftQuest;

		public double riftQuestDustCollected;

		public DateTime riftRestRewardCollectedTime;

		public FlashOfferBundle flashOfferBundle;

		public ServerSideFlashOfferBundle halloweenFlashOfferBundle;

		public CalendarTreeOfferBundle christmasOfferBundle;

		public ServerSideFlashOfferBundle secondAnniversaryFlashOffersBundle;

		public bool secondAnniversaryHintsMarked;

		private List<List<int>> christmasOffersBundlePurchasesLeftByOffer;

		public bool christmasCandyCappedVideoNotificationSeen;

		public bool christmasFreeCandyNotificationSeen;

		public Dictionary<int, CharmEffectData> allCharmEffects;

		public Dictionary<int, CurseEffectData> allCurseEffects;

		public Queue<int> lastFiveOpenedCharms;

		private int charmDraftTempSeed;

		public int numSmallCharmPacksOpened;

		public int numSmallCharmPacks;

		public int riftDiscoveryIndex;

		public int numRiftQuestsCompleted;

		private int maxRiftDiscoveryIndex;

		private CharmComparer charmComparer;

		public CharmSortType charmSortType;

		public bool isCharmSortingDescending;

		public bool isCharmSortingShowing;

		private TrinketComparer trinketComparer;

		public TrinketSortType trinketSortType;

		public bool isTrinketSortingDescending;

		public bool isTrinketSortingShowing;

		public Dictionary<SocialRewards.Network, Status> socialRewardsStatus;

		public DateTime lastNewsTimestam;

		public SpecialOfferBoard specialOfferBoard;

		public DateTime lastAddedCurseChallengeTime;

		public List<int> currentCurses;

		public int cursedGatesBeaten;

		public CurseSlots cursedRiftSlots;

		public int lastSelectedRegularGateIndex;

		public int frameCount;

		public bool halloweenEnabled;

		public bool christmasEnabled;

		public bool christmasEventAlreadyDisabled;

		public bool candyDropAlreadyDisabled;

		public bool christmasEventForbidden;

		public bool hasTrinketSmith;

		public bool secondAnniversaryEventEnabled;

		public bool secondAnniversaryEventAlreadyDisabled;

		public int amountLootPacksOpenedForHint;

		public DateTime installDate;

		public bool usedTrinketExploit;

		public DateTime lastFreeCandyTreatClaimedDate;

		public DateTime lastSessionDate;

		public bool christmasEventPopupChecked;

		public bool showChristmasEventPopup;

		public int christmasEventPopupsShown;

		public int christmasTreatVideosWatchedSinceLastReset;

		public List<int> newStats;

		public List<PlayfabManager.RewardData> rewardsToGive = new List<PlayfabManager.RewardData>();

		public ArtifactsManager artifactsManager = new ArtifactsManager();

		public bool isCataclysmSurviver;

		public bool isStageRearrangeSurviver;

		public int numPrestigesSinceCataclysm;

		public bool prestigedDuringSecondAnniversaryEvent;

		public bool maxStageReachedInCurrentAdventure;

		public int artifactMultiUpgradeIndex;

		public int timeChallengesLostCount;

		private HashSet<string> boughtHeroIds = new HashSet<string>();

		private Dictionary<string, GameMode> boughtHeroIdsWithModes = new Dictionary<string, GameMode>();

		[Serializable]
		public class AnnouncedOfferInfo
		{
			public OfferId offerId;

			public DateTime announcingTime;
		}
	}
}
