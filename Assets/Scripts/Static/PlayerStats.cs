using System;
using System.Collections.Generic;
using PlayFab.ClientModels;
using SaveLoad;
using Simulation;
using stats;
using UnityEngine;

namespace Static
{
	public static class PlayerStats
	{
		public static bool IsPayer { get; private set; }

		public static void LoadPayerStatus(PlayerProfileModel profile)
		{
			PlayerStats.IsPayer = (profile != null && profile.TotalValueToDateInUSD != null && profile.TotalValueToDateInUSD.Value > 0u);
		}

		public static void LoadPlayerLocations(List<LocationModel> locations)
		{
			if (locations != null)
			{
				foreach (LocationModel locationModel in locations)
				{
					if (locationModel.CountryCode != null)
					{
						PlayerStats.CountriesPlayerLoggedIn.Add(locationModel.CountryCode.Value);
					}
				}
			}
		}

		public static void ResetSoft()
		{
			PlayerStats.numTotTap = 0;
			PlayerStats.lifeTimeInTicksInCurrentSaveFile = 0L;
			PlayerStats.numTotalDailyCompleted = 0;
			PlayerStats.numTotalDailySkip = 0;
			PlayerStats.allTimeCharmPacksOpened = 0;
			PlayerStats.spentCreditsDuringThisSaveFile = 0.0;
			PlayerStats.enemiesKilled = 0;
			PlayerStats.ultimatesUsedCount = 0;
			PlayerStats.minesCollectedCount = 0;
			PlayerStats.secondaryAbilitiesCastedCount = 0;
			PlayerStats.goblinChestsDestroyedCount = 0;
			PlayerStats.timeHeroesDied = 0;
			PlayerStats.numUsedMerchantItems.Clear();
			PlayerStats.numAdDragonCatch = 0;
			PlayerStats.numAdDragonMiss = 0;
			PlayerStats.numTrinketPacksOpened = 0;
		}

		public static void OnSessionStart()
		{
			PlayerStats.numLogins++;
		}

		public static float GetSessionTime()
		{
			return Time.realtimeSinceStartup;
		}

		public static void Update()
		{
			long num = (long)(Time.deltaTime * 1E+07f);
			PlayerStats.lifeTimeInTicks += num;
			PlayerStats.lifeTimeInTicksInCurrentSaveFile += num;
		}

		public static void OnCurrencySpent(CurrencyType type, double amount)
		{
			if (type == CurrencyType.GEM)
			{
				PlayerStats.spentCredits += amount;
				PlayerStats.spentCreditsDuringThisSaveFile += amount;
				if (Main.instance.GetSim().installDate.AddDays(1.0) >= DateTime.UtcNow)
				{
					PlayerStats.spentCreditsFirstDay += amount;
				}
			}
			else if (type == CurrencyType.MYTHSTONE)
			{
				PlayerStats.spentMyth += amount;
			}
			else if (type == CurrencyType.SCRAP)
			{
				PlayerStats.spentScraps += amount;
			}
			else if (type == CurrencyType.TOKEN)
			{
				PlayerStats.spentTokens += amount;
			}
			else if (type != CurrencyType.GOLD)
			{
				if (type == CurrencyType.AEON)
				{
					PlayerStats.spentAeons += amount;
				}
				else if (type != CurrencyType.CANDY)
				{
					throw new NotImplementedException();
				}
			}
		}

		public static void OnDailySkip()
		{
			PlayerStats.numTotalDailySkip++;
		}

		public static void OnDailyComplete()
		{
			PlayerStats.numTotalDailyCompleted++;
			PlayerStats.CheckDailtyQuestAchievement(PlayerStats.numTotalDailyCompleted);
		}

		public static void CheckDailtyQuestAchievement(int count)
		{
			if (count >= AchievementProgress.COMPLETE_SIDEQUEST_1)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQLQ");
			}
			if (count >= AchievementProgress.COMPLETE_SIDEQUEST_2)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQLg");
			}
			if (count >= AchievementProgress.COMPLETE_SIDEQUEST_3)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQLw");
			}
			if (count >= AchievementProgress.COMPLETE_SIDEQUEST_4)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQMA");
			}
			if (count >= AchievementProgress.COMPLETE_SIDEQUEST_5)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQMQ");
			}
		}

		public static void OnRuneBought(int runeCount)
		{
			if (runeCount >= AchievementProgress.COLLECT_RUNES_1)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQMw");
			}
			if (runeCount >= AchievementProgress.COLLECT_RUNES_2)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQNA");
			}
			if (runeCount >= AchievementProgress.COLLECT_RUNES_3)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQNQ");
			}
			if (runeCount >= AchievementProgress.COLLECT_RUNES_4)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQNg");
			}
			if (runeCount >= AchievementProgress.COLLECT_RUNES_5)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQNw");
			}
		}

		public static void OnUsedMerchantItem(string id)
		{
			if (!PlayerStats.numUsedMerchantItems.ContainsKey(id))
			{
				PlayerStats.numUsedMerchantItems.Add(id, 1);
			}
			else
			{
				Dictionary<string, int> dictionary;
				(dictionary = PlayerStats.numUsedMerchantItems)[id] = dictionary[id] + 1;
			}
		}

		public static void OnAdDragonCatch()
		{
			PlayerStats.numAdDragonCatch++;
		}

		public static void OnAdDragonMiss()
		{
			PlayerStats.numAdDragonMiss++;
		}

		public static void OnEnemyKilled()
		{
			PlayerStats.enemiesKilled++;
		}

		public static void OnGoblinChestDestroyed()
		{
			PlayerStats.goblinChestsDestroyedCount++;
		}

		public static void OnHeroDead()
		{
			PlayerStats.timeHeroesDied++;
		}

		public static void OnUltimateUsed()
		{
			PlayerStats.ultimatesUsedCount++;
		}

		public static void OnSecondaryAbilityCasted()
		{
			PlayerStats.secondaryAbilitiesCastedCount++;
		}

		public static void OnMineCollected()
		{
			PlayerStats.minesCollectedCount++;
		}

		public static void OnAdAccept()
		{
			PlayerStats.numAdAccept++;
		}

		public static void OnOfferOffered(OfferId id)
		{
		}

		public static void OnOfferCheckout(OfferId id)
		{
		}

		public static void OnOfferAccepted(OfferId id)
		{
			PlayerStats.IsPayer = true;
		}

		public static void OnUserBuySkin(string[] skinNames)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			foreach (string str in skinNames)
			{
				string key = "SKIN_" + str + "_BOUGHT";
				dictionary.Add(key, 1);
			}
		}

		public static void OnAdCancel()
		{
			PlayerStats.numAdCancel++;
		}

		public static void OnFreeCredits()
		{
			PlayerStats.numFreeCredits++;
		}

		public static void OnTap()
		{
			PlayerStats.numTotTap++;
			if (PlayerStats.numTotTap >= AchievementProgress.TAP_1)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQBw");
			}
			if (PlayerStats.numTotTap >= AchievementProgress.TAP_2)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQCA");
			}
			if (PlayerStats.numTotTap >= AchievementProgress.TAP_3)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQCQ");
			}
			if (PlayerStats.numTotTap >= AchievementProgress.TAP_4)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQCg");
			}
			if (PlayerStats.numTotTap >= AchievementProgress.TAP_5)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQCw");
			}
		}

		public static void OnTap(int count)
		{
			PlayerStats.numTotTap += count;
			if (PlayerStats.numTotTap >= AchievementProgress.TAP_1)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQBw");
			}
			if (PlayerStats.numTotTap >= AchievementProgress.TAP_2)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQCA");
			}
			if (PlayerStats.numTotTap >= AchievementProgress.TAP_3)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQCQ");
			}
			if (PlayerStats.numTotTap >= AchievementProgress.TAP_4)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQCg");
			}
			if (PlayerStats.numTotTap >= AchievementProgress.TAP_5)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQCw");
			}
		}

		public static void OnPrestige(int numPrestiges)
		{
			if (numPrestiges >= AchievementProgress.PRESTIGE_1)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQDQ");
			}
			if (numPrestiges >= AchievementProgress.PRESTIGE_2)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQDg");
			}
			if (numPrestiges >= AchievementProgress.PRESTIGE_3)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQDw");
			}
			if (numPrestiges >= AchievementProgress.PRESTIGE_4)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQEA");
			}
			if (numPrestiges >= AchievementProgress.PRESTIGE_5)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQEQ");
			}
		}

		public static void OnHeroEvolve(List<HeroDataBase> allHeroes)
		{
			int num = 0;
			Dictionary<int, int> dictionary = new Dictionary<int, int>
			{
				{
					0,
					0
				},
				{
					1,
					0
				},
				{
					2,
					0
				},
				{
					3,
					0
				},
				{
					4,
					0
				},
				{
					5,
					0
				},
				{
					6,
					0
				}
			};
			foreach (HeroDataBase heroDataBase in allHeroes)
			{
				if (heroDataBase.evolveLevel > num)
				{
					num = heroDataBase.evolveLevel;
				}
				for (int i = 0; i <= heroDataBase.evolveLevel; i++)
				{
					Dictionary<int, int> dictionary2;
					int key;
					(dictionary2 = dictionary)[key = i] = dictionary2[key] + 1;
				}
			}
			if (num >= 1)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQFw");
			}
			if (num >= 2)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQGA");
			}
			if (num >= 3)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQGQ");
			}
			if (num >= 4)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQGw");
			}
			if (num >= 5)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQGg");
			}
			if (num >= 6)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQLA");
			}
			int num2 = 0;
			if (dictionary[1] >= 11)
			{
				num2++;
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQHA");
			}
			if (dictionary[2] >= 11)
			{
				num2++;
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQHQ");
			}
			if (dictionary[3] >= 11)
			{
				num2++;
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQHg");
			}
			if (dictionary[4] >= 11)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQIA");
			}
			if (dictionary[5] >= 11)
			{
				num2++;
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQHw");
			}
			if (dictionary[6] >= 11)
			{
				num2++;
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQKw");
			}
		}

		public static void OnChallengeWin(int numChallengesWon)
		{
			if (numChallengesWon >= AchievementProgress.TIME_CHALLENGE_1)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQEg");
			}
			if (numChallengesWon >= AchievementProgress.TIME_CHALLENGE_2)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQEw");
			}
			if (numChallengesWon >= AchievementProgress.TIME_CHALLENGE_3)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQFA");
			}
			if (numChallengesWon >= AchievementProgress.TIME_CHALLENGE_4)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQFQ");
			}
			if (numChallengesWon >= AchievementProgress.TIME_CHALLENGE_5)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQFg");
			}
		}

		public static void OnArtifactCraft(int totalArtifactsLevel, int numArtifacts)
		{
			if (numArtifacts >= AchievementProgress.CRAFT_ARTIFACT_1)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQIQ");
			}
			if (numArtifacts >= AchievementProgress.CRAFT_ARTIFACT_2)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQIg");
			}
			if (numArtifacts >= AchievementProgress.CRAFT_ARTIFACT_3)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQIw");
			}
			if (numArtifacts >= AchievementProgress.CRAFT_ARTIFACT_4)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQJQ");
			}
			if (numArtifacts >= AchievementProgress.CRAFT_ARTIFACT_5)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQJA");
			}
			PlayerStats.OnTotalArtifactsLevelChanged(totalArtifactsLevel);
		}

		public static void OnTotalArtifactsLevelChanged(int totalArtifactsLevel)
		{
			if (totalArtifactsLevel >= AchievementProgress.ARTIFACT_TOTAL_LEVEL_5)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQQQ");
			}
			if (totalArtifactsLevel >= AchievementProgress.ARTIFACT_TOTAL_LEVEL_4)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQQA");
			}
			if (totalArtifactsLevel >= AchievementProgress.ARTIFACT_TOTAL_LEVEL_3)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQPw");
			}
			if (totalArtifactsLevel >= AchievementProgress.ARTIFACT_TOTAL_LEVEL_2)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQPg");
			}
			if (totalArtifactsLevel >= AchievementProgress.ARTIFACT_TOTAL_LEVEL_1)
			{
				PlayerStats.EarnAchievement("CgkIlpSuuo0ZEAIQPQ");
			}
		}

		public static void EarnAchievement(string achievementId)
		{
			int index;
			if (!PlayerStats.GetAchievement(achievementId, out index))
			{
				PlayerStats.achievements[index][achievementId] = true;
				StoreManager.ReportAchievement(achievementId);
			}
		}

		public static Dictionary<string, bool> GetAchievementsDict()
		{
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			if (PlayerStats.achievements != null)
			{
				int i = 0;
				int count = PlayerStats.achievements.Count;
				while (i < count)
				{
					foreach (KeyValuePair<string, bool> keyValuePair in PlayerStats.achievements[i])
					{
						dictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
					i++;
				}
			}
			return dictionary;
		}

		public static void CheckAchievements()
		{
			Dictionary<string, bool> achievementsDict = PlayerStats.GetAchievementsDict();
			StoreManager.CheckAchievements(achievementsDict);
		}

		public static bool GetAchievement(string id)
		{
			int i = 0;
			int count = PlayerStats.achievements.Count;
			while (i < count)
			{
				Dictionary<string, bool> dictionary = PlayerStats.achievements[i];
				if (dictionary.ContainsKey(id))
				{
					return dictionary[id];
				}
				i++;
			}
			return false;
		}

		public static float GetAchievementProgress(string id, Simulator sim)
		{
			switch (id)
			{
			case "CgkIlpSuuo0ZEAIQBw":
				return 1f * (float)PlayerStats.numTotTap / (float)AchievementProgress.TAP_1;
			case "CgkIlpSuuo0ZEAIQCA":
				return 1f * (float)PlayerStats.numTotTap / (float)AchievementProgress.TAP_2;
			case "CgkIlpSuuo0ZEAIQCQ":
				return 1f * (float)PlayerStats.numTotTap / (float)AchievementProgress.TAP_3;
			case "CgkIlpSuuo0ZEAIQCg":
				return 1f * (float)PlayerStats.numTotTap / (float)AchievementProgress.TAP_4;
			case "CgkIlpSuuo0ZEAIQCw":
				return 1f * (float)PlayerStats.numTotTap / (float)AchievementProgress.TAP_5;
			case "CgkIlpSuuo0ZEAIQDQ":
				return 1f * (float)sim.numPrestiges / (float)AchievementProgress.PRESTIGE_1;
			case "CgkIlpSuuo0ZEAIQDg":
				return 1f * (float)sim.numPrestiges / (float)AchievementProgress.PRESTIGE_2;
			case "CgkIlpSuuo0ZEAIQDw":
				return 1f * (float)sim.numPrestiges / (float)AchievementProgress.PRESTIGE_3;
			case "CgkIlpSuuo0ZEAIQEA":
				return 1f * (float)sim.numPrestiges / (float)AchievementProgress.PRESTIGE_4;
			case "CgkIlpSuuo0ZEAIQEQ":
				return 1f * (float)sim.numPrestiges / (float)AchievementProgress.PRESTIGE_5;
			case "CgkIlpSuuo0ZEAIQEg":
				return 1f * (float)PlayerStats.GetNumChallengesWon(sim) / (float)AchievementProgress.TIME_CHALLENGE_1;
			case "CgkIlpSuuo0ZEAIQEw":
				return 1f * (float)PlayerStats.GetNumChallengesWon(sim) / (float)AchievementProgress.TIME_CHALLENGE_2;
			case "CgkIlpSuuo0ZEAIQFA":
				return 1f * (float)PlayerStats.GetNumChallengesWon(sim) / (float)AchievementProgress.TIME_CHALLENGE_3;
			case "CgkIlpSuuo0ZEAIQFQ":
				return 1f * (float)PlayerStats.GetNumChallengesWon(sim) / (float)AchievementProgress.TIME_CHALLENGE_4;
			case "CgkIlpSuuo0ZEAIQFg":
				return 1f * (float)PlayerStats.GetNumChallengesWon(sim) / (float)AchievementProgress.TIME_CHALLENGE_5;
			case "CgkIlpSuuo0ZEAIQIQ":
				return 1f * (float)sim.artifactsManager.GetTotalAmountOfArtifacts() / (float)AchievementProgress.CRAFT_ARTIFACT_1;
			case "CgkIlpSuuo0ZEAIQIg":
				return 1f * (float)sim.artifactsManager.GetTotalAmountOfArtifacts() / (float)AchievementProgress.CRAFT_ARTIFACT_2;
			case "CgkIlpSuuo0ZEAIQIw":
				return 1f * (float)sim.artifactsManager.GetTotalAmountOfArtifacts() / (float)AchievementProgress.CRAFT_ARTIFACT_3;
			case "CgkIlpSuuo0ZEAIQJQ":
				return 1f * (float)sim.artifactsManager.GetTotalAmountOfArtifacts() / (float)AchievementProgress.CRAFT_ARTIFACT_4;
			case "CgkIlpSuuo0ZEAIQJA":
				return 1f * (float)sim.artifactsManager.GetTotalAmountOfArtifacts() / (float)AchievementProgress.CRAFT_ARTIFACT_5;
			case "CgkIlpSuuo0ZEAIQPQ":
				return (float)sim.artifactsManager.TotalArtifactsLevel / (float)AchievementProgress.ARTIFACT_TOTAL_LEVEL_1;
			case "CgkIlpSuuo0ZEAIQPg":
				return (float)sim.artifactsManager.TotalArtifactsLevel / (float)AchievementProgress.ARTIFACT_TOTAL_LEVEL_2;
			case "CgkIlpSuuo0ZEAIQPw":
				return (float)sim.artifactsManager.TotalArtifactsLevel / (float)AchievementProgress.ARTIFACT_TOTAL_LEVEL_3;
			case "CgkIlpSuuo0ZEAIQQA":
				return (float)sim.artifactsManager.TotalArtifactsLevel / (float)AchievementProgress.ARTIFACT_TOTAL_LEVEL_4;
			case "CgkIlpSuuo0ZEAIQQQ":
				return (float)sim.artifactsManager.TotalArtifactsLevel / (float)AchievementProgress.ARTIFACT_TOTAL_LEVEL_5;
			case "CgkIlpSuuo0ZEAIQFw":
				return PlayerStats.GetHeroEvolveLevelProgress(sim, 1);
			case "CgkIlpSuuo0ZEAIQGA":
				return PlayerStats.GetHeroEvolveLevelProgress(sim, 2);
			case "CgkIlpSuuo0ZEAIQGQ":
				return PlayerStats.GetHeroEvolveLevelProgress(sim, 3);
			case "CgkIlpSuuo0ZEAIQGw":
				return PlayerStats.GetHeroEvolveLevelProgress(sim, 4);
			case "CgkIlpSuuo0ZEAIQGg":
				return PlayerStats.GetHeroEvolveLevelProgress(sim, 5);
			case "CgkIlpSuuo0ZEAIQLA":
				return PlayerStats.GetHeroEvolveLevelProgress(sim, 6);
			case "CgkIlpSuuo0ZEAIQHA":
				return PlayerStats.GetElevenHeroEvolveLevelProgress(sim, 1);
			case "CgkIlpSuuo0ZEAIQHQ":
				return PlayerStats.GetElevenHeroEvolveLevelProgress(sim, 2);
			case "CgkIlpSuuo0ZEAIQHg":
				return PlayerStats.GetElevenHeroEvolveLevelProgress(sim, 3);
			case "CgkIlpSuuo0ZEAIQIA":
				return PlayerStats.GetElevenHeroEvolveLevelProgress(sim, 4);
			case "CgkIlpSuuo0ZEAIQHw":
				return PlayerStats.GetElevenHeroEvolveLevelProgress(sim, 5);
			case "CgkIlpSuuo0ZEAIQKw":
				return PlayerStats.GetElevenHeroEvolveLevelProgress(sim, 6);
			case "CgkIlpSuuo0ZEAIQMw":
				return 1f * (float)sim.GetBoughtRunes().Count / (float)AchievementProgress.COLLECT_RUNES_1;
			case "CgkIlpSuuo0ZEAIQNA":
				return 1f * (float)sim.GetBoughtRunes().Count / (float)AchievementProgress.COLLECT_RUNES_2;
			case "CgkIlpSuuo0ZEAIQNQ":
				return 1f * (float)sim.GetBoughtRunes().Count / (float)AchievementProgress.COLLECT_RUNES_3;
			case "CgkIlpSuuo0ZEAIQNg":
				return 1f * (float)sim.GetBoughtRunes().Count / (float)AchievementProgress.COLLECT_RUNES_4;
			case "CgkIlpSuuo0ZEAIQNw":
				return 1f * (float)sim.GetBoughtRunes().Count / (float)AchievementProgress.COLLECT_RUNES_5;
			case "CgkIlpSuuo0ZEAIQLQ":
				return 1f * (float)PlayerStats.numTotalDailyCompleted / (float)AchievementProgress.COMPLETE_SIDEQUEST_1;
			case "CgkIlpSuuo0ZEAIQLg":
				return 1f * (float)PlayerStats.numTotalDailyCompleted / (float)AchievementProgress.COMPLETE_SIDEQUEST_2;
			case "CgkIlpSuuo0ZEAIQLw":
				return 1f * (float)PlayerStats.numTotalDailyCompleted / (float)AchievementProgress.COMPLETE_SIDEQUEST_3;
			case "CgkIlpSuuo0ZEAIQMA":
				return 1f * (float)PlayerStats.numTotalDailyCompleted / (float)AchievementProgress.COMPLETE_SIDEQUEST_4;
			case "CgkIlpSuuo0ZEAIQMQ":
				return 1f * (float)PlayerStats.numTotalDailyCompleted / (float)AchievementProgress.COMPLETE_SIDEQUEST_5;
			}
			return -1f;
		}

		public static void SetAchievementProgressToTarget(string id, SaveData saveData)
		{
			switch (id)
			{
			case "CgkIlpSuuo0ZEAIQBw":
				saveData.playerStatNumTotTap = AchievementProgress.TAP_1;
				break;
			case "CgkIlpSuuo0ZEAIQCA":
				saveData.playerStatNumTotTap = AchievementProgress.TAP_2;
				break;
			case "CgkIlpSuuo0ZEAIQCQ":
				saveData.playerStatNumTotTap = AchievementProgress.TAP_3;
				break;
			case "CgkIlpSuuo0ZEAIQCg":
				saveData.playerStatNumTotTap = AchievementProgress.TAP_4;
				break;
			case "CgkIlpSuuo0ZEAIQCw":
				saveData.playerStatNumTotTap = AchievementProgress.TAP_5;
				break;
			case "CgkIlpSuuo0ZEAIQDQ":
				saveData.numPrestiges = AchievementProgress.PRESTIGE_1;
				break;
			case "CgkIlpSuuo0ZEAIQDg":
				saveData.numPrestiges = AchievementProgress.PRESTIGE_2;
				break;
			case "CgkIlpSuuo0ZEAIQDw":
				saveData.numPrestiges = AchievementProgress.PRESTIGE_3;
				break;
			case "CgkIlpSuuo0ZEAIQEA":
				saveData.numPrestiges = AchievementProgress.PRESTIGE_4;
				break;
			case "CgkIlpSuuo0ZEAIQEQ":
				saveData.numPrestiges = AchievementProgress.PRESTIGE_5;
				break;
			case "CgkIlpSuuo0ZEAIQLQ":
				saveData.totalDailyCompleted = AchievementProgress.COMPLETE_SIDEQUEST_1;
				break;
			case "CgkIlpSuuo0ZEAIQLg":
				saveData.totalDailyCompleted = AchievementProgress.COMPLETE_SIDEQUEST_2;
				break;
			case "CgkIlpSuuo0ZEAIQLw":
				saveData.totalDailyCompleted = AchievementProgress.COMPLETE_SIDEQUEST_3;
				break;
			case "CgkIlpSuuo0ZEAIQMA":
				saveData.totalDailyCompleted = AchievementProgress.COMPLETE_SIDEQUEST_4;
				break;
			case "CgkIlpSuuo0ZEAIQMQ":
				saveData.totalDailyCompleted = AchievementProgress.COMPLETE_SIDEQUEST_5;
				break;
			}
		}

		public static int GetAchievementProgressCurrent(string id, SaveData saveData)
		{
			switch (id)
			{
			case "CgkIlpSuuo0ZEAIQBw":
				return saveData.playerStatNumTotTap;
			case "CgkIlpSuuo0ZEAIQCA":
				return saveData.playerStatNumTotTap;
			case "CgkIlpSuuo0ZEAIQCQ":
				return saveData.playerStatNumTotTap;
			case "CgkIlpSuuo0ZEAIQCg":
				return saveData.playerStatNumTotTap;
			case "CgkIlpSuuo0ZEAIQCw":
				return saveData.playerStatNumTotTap;
			case "CgkIlpSuuo0ZEAIQDQ":
				return saveData.numPrestiges;
			case "CgkIlpSuuo0ZEAIQDg":
				return saveData.numPrestiges;
			case "CgkIlpSuuo0ZEAIQDw":
				return saveData.numPrestiges;
			case "CgkIlpSuuo0ZEAIQEA":
				return saveData.numPrestiges;
			case "CgkIlpSuuo0ZEAIQEQ":
				return saveData.numPrestiges;
			case "CgkIlpSuuo0ZEAIQLQ":
				return saveData.totalDailyCompleted;
			case "CgkIlpSuuo0ZEAIQLg":
				return saveData.totalDailyCompleted;
			case "CgkIlpSuuo0ZEAIQLw":
				return saveData.totalDailyCompleted;
			case "CgkIlpSuuo0ZEAIQMA":
				return saveData.totalDailyCompleted;
			case "CgkIlpSuuo0ZEAIQMQ":
				return saveData.totalDailyCompleted;
			}
			return -1;
		}

		public static int GetAchievementProgressTarget(string id)
		{
			switch (id)
			{
			case "CgkIlpSuuo0ZEAIQBw":
				return AchievementProgress.TAP_1;
			case "CgkIlpSuuo0ZEAIQCA":
				return AchievementProgress.TAP_2;
			case "CgkIlpSuuo0ZEAIQCQ":
				return AchievementProgress.TAP_3;
			case "CgkIlpSuuo0ZEAIQCg":
				return AchievementProgress.TAP_4;
			case "CgkIlpSuuo0ZEAIQCw":
				return AchievementProgress.TAP_5;
			case "CgkIlpSuuo0ZEAIQDQ":
				return AchievementProgress.PRESTIGE_1;
			case "CgkIlpSuuo0ZEAIQDg":
				return AchievementProgress.PRESTIGE_2;
			case "CgkIlpSuuo0ZEAIQDw":
				return AchievementProgress.PRESTIGE_3;
			case "CgkIlpSuuo0ZEAIQEA":
				return AchievementProgress.PRESTIGE_4;
			case "CgkIlpSuuo0ZEAIQEQ":
				return AchievementProgress.PRESTIGE_5;
			case "CgkIlpSuuo0ZEAIQLQ":
				return AchievementProgress.COMPLETE_SIDEQUEST_1;
			case "CgkIlpSuuo0ZEAIQLg":
				return AchievementProgress.COMPLETE_SIDEQUEST_2;
			case "CgkIlpSuuo0ZEAIQLw":
				return AchievementProgress.COMPLETE_SIDEQUEST_3;
			case "CgkIlpSuuo0ZEAIQMA":
				return AchievementProgress.COMPLETE_SIDEQUEST_4;
			case "CgkIlpSuuo0ZEAIQMQ":
				return AchievementProgress.COMPLETE_SIDEQUEST_5;
			}
			return -1;
		}

		public static int GetNumChallengesWon(Simulator sim)
		{
			int num = 0;
			World world = sim.GetWorld(GameMode.CRUSADE);
			int i = 0;
			int count = world.unlocks.Count;
			while (i < count)
			{
				if (!world.unlocks[i].isCollected)
				{
					break;
				}
				num++;
				i++;
			}
			return num;
		}

		private static float GetHeroEvolveAllLevelProgress(Simulator sim, int goalLevel)
		{
			float num = 0f;
			int count = sim.GetAllHeroes().Count;
			foreach (HeroDataBase heroDataBase in sim.GetAllHeroes())
			{
				if (heroDataBase.evolveLevel >= goalLevel)
				{
					num += 1f / (float)count;
				}
			}
			return num;
		}

		private static float GetElevenHeroEvolveLevelProgress(Simulator sim, int goalLevel)
		{
			float num = 0f;
			int num2 = 11;
			foreach (HeroDataBase heroDataBase in sim.GetAllHeroes())
			{
				if (heroDataBase.evolveLevel >= goalLevel)
				{
					num += 1f / (float)num2;
				}
			}
			return GameMath.Clamp(num, 0f, 1f);
		}

		private static float GetHeroEvolveLevelProgress(Simulator sim, int goalLevel)
		{
			foreach (HeroDataBase heroDataBase in sim.GetAllHeroes())
			{
				if (heroDataBase.evolveLevel >= goalLevel)
				{
					return 1f;
				}
			}
			return 0f;
		}

		public static bool GetAchievement(string id, out int index)
		{
			int i = 0;
			int count = PlayerStats.achievements.Count;
			while (i < count)
			{
				Dictionary<string, bool> dictionary = PlayerStats.achievements[i];
				if (dictionary.ContainsKey(id))
				{
					index = i;
					return dictionary[id];
				}
				i++;
			}
			index = -1;
			return false;
		}

		public static void Initialize()
		{
			PlayerStats.iapsMade = new List<int>();
			foreach (string text in IapManager.productIds)
			{
				PlayerStats.iapsMade.Add(0);
			}
			PlayerStats.achievements = new List<Dictionary<string, bool>>();
			PlayerStats.achievementIndexes = new List<int>();
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			dictionary.Add("CgkIlpSuuo0ZEAIQBw", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQCA", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQCQ", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQCg", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQCw", false);
			PlayerStats.achievements.Add(dictionary);
			PlayerStats.achievementIndexes.Add(0);
			dictionary = new Dictionary<string, bool>();
			dictionary.Add("CgkIlpSuuo0ZEAIQDQ", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQDg", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQDw", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQEA", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQEQ", false);
			PlayerStats.achievements.Add(dictionary);
			PlayerStats.achievementIndexes.Add(1);
			dictionary = new Dictionary<string, bool>();
			dictionary.Add("CgkIlpSuuo0ZEAIQEg", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQEw", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQFA", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQFQ", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQFg", false);
			PlayerStats.achievements.Add(dictionary);
			PlayerStats.achievementIndexes.Add(7);
			dictionary = new Dictionary<string, bool>();
			dictionary.Add("CgkIlpSuuo0ZEAIQFw", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQGA", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQGQ", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQGw", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQGg", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQLA", false);
			PlayerStats.achievements.Add(dictionary);
			PlayerStats.achievementIndexes.Add(3);
			dictionary = new Dictionary<string, bool>();
			dictionary.Add("CgkIlpSuuo0ZEAIQHA", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQHQ", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQHg", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQIA", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQHw", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQKw", false);
			PlayerStats.achievements.Add(dictionary);
			PlayerStats.achievementIndexes.Add(4);
			dictionary = new Dictionary<string, bool>();
			dictionary.Add("CgkIlpSuuo0ZEAIQIQ", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQIg", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQIw", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQJQ", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQJA", false);
			PlayerStats.achievements.Add(dictionary);
			PlayerStats.achievementIndexes.Add(5);
			dictionary = new Dictionary<string, bool>();
			dictionary.Add("CgkIlpSuuo0ZEAIQPQ", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQPg", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQPw", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQQA", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQQQ", false);
			PlayerStats.achievements.Add(dictionary);
			PlayerStats.achievementIndexes.Add(6);
			dictionary = new Dictionary<string, bool>();
			dictionary.Add("CgkIlpSuuo0ZEAIQMw", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQNA", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQNQ", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQNg", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQNw", false);
			PlayerStats.achievements.Add(dictionary);
			PlayerStats.achievementIndexes.Add(2);
			dictionary = new Dictionary<string, bool>();
			dictionary.Add("CgkIlpSuuo0ZEAIQLQ", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQLg", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQLw", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQMA", false);
			dictionary.Add("CgkIlpSuuo0ZEAIQMQ", false);
			PlayerStats.achievements.Add(dictionary);
			PlayerStats.achievementIndexes.Add(8);
		}

		public static void BoughtIAP(int boughtProductIndex)
		{
			List<int> list;
			(list = PlayerStats.iapsMade)[boughtProductIndex] = list[boughtProductIndex] + 1;
		}

		public static void OpenedLootpack(ShopPack shopPack)
		{
			if (shopPack is ShopPackTrinket)
			{
				PlayerStats.numTrinketPacksOpened++;
			}
		}

		public static void BoughtFlashOffer()
		{
			PlayerStats.allTimeFlashOffersPurchased++;
		}

		public static void BoughtAdventureFlashOffer()
		{
			PlayerStats.allTimeAdventureFlashOffersPurchased++;
		}

		public static void BoughtHalloweenFlashOffer()
		{
			PlayerStats.allTimeHalloweenFlashOffersPurchased++;
		}

		public static void BoughtChristmasFlashOffer()
		{
			PlayerStats.allTimeChristmasFlashOffersPurchased++;
		}

		public static void ScreenshotShared(GameMode gameMode)
		{
			if (gameMode != GameMode.STANDARD)
			{
				if (gameMode != GameMode.CRUSADE)
				{
					if (gameMode == GameMode.RIFT)
					{
						PlayerStats.screenshotsSharedInGog++;
					}
				}
				else
				{
					PlayerStats.screenshotsSharedInTimeChallenges++;
				}
			}
			else
			{
				PlayerStats.screenshotsSharedInAdventure++;
			}
			PlayfabManager.SendPlayerEvent(PlayfabEventId.SCREENSHOT_SHARED, new Dictionary<string, object>
			{
				{
					"mode",
					gameMode
				}
			}, null, null, true);
		}

		private static void CreateAchievements()
		{
			PlayerStats.acvs = new List<Achievement>();
			double[] rewardAmounts = new double[]
			{
				15.0,
				30.0,
				50.0,
				100.0,
				500.0
			};
			double[] rewardAmounts2 = new double[]
			{
				15.0,
				30.0,
				50.0,
				100.0,
				500.0,
				500.0
			};
			AchievementData data = new AchievementData(5, new string[]
			{
				"CgkIlpSuuo0ZEAIQBw",
				"CgkIlpSuuo0ZEAIQCA",
				"CgkIlpSuuo0ZEAIQCQ",
				"CgkIlpSuuo0ZEAIQCg",
				"CgkIlpSuuo0ZEAIQCw"
			}, new int[]
			{
				AchievementProgress.TAP_1,
				AchievementProgress.TAP_2,
				AchievementProgress.TAP_3,
				AchievementProgress.TAP_4,
				AchievementProgress.TAP_5
			}, rewardAmounts);
			PlayerStats.tapAch = new Achievement(data);
			AchievementData data2 = new AchievementData(5, new string[]
			{
				"CgkIlpSuuo0ZEAIQDQ",
				"CgkIlpSuuo0ZEAIQDg",
				"CgkIlpSuuo0ZEAIQDw",
				"CgkIlpSuuo0ZEAIQEA",
				"CgkIlpSuuo0ZEAIQEQ"
			}, new int[]
			{
				AchievementProgress.PRESTIGE_1,
				AchievementProgress.PRESTIGE_2,
				AchievementProgress.PRESTIGE_3,
				AchievementProgress.PRESTIGE_4,
				AchievementProgress.PRESTIGE_5
			}, rewardAmounts);
			PlayerStats.prestigeAch = new Achievement(data2);
			AchievementData data3 = new AchievementData(5, new string[]
			{
				"CgkIlpSuuo0ZEAIQEg",
				"CgkIlpSuuo0ZEAIQEw",
				"CgkIlpSuuo0ZEAIQFA",
				"CgkIlpSuuo0ZEAIQFQ",
				"CgkIlpSuuo0ZEAIQFg"
			}, new int[]
			{
				AchievementProgress.TIME_CHALLENGE_1,
				AchievementProgress.TIME_CHALLENGE_2,
				AchievementProgress.TIME_CHALLENGE_3,
				AchievementProgress.TIME_CHALLENGE_4,
				AchievementProgress.TIME_CHALLENGE_5
			}, rewardAmounts);
			PlayerStats.challengeWinAch = new Achievement(data3);
			AchievementData data4 = new AchievementData(6, new string[]
			{
				"CgkIlpSuuo0ZEAIQFw",
				"CgkIlpSuuo0ZEAIQGA",
				"CgkIlpSuuo0ZEAIQGQ",
				"CgkIlpSuuo0ZEAIQGw",
				"CgkIlpSuuo0ZEAIQGg",
				"CgkIlpSuuo0ZEAIQLA"
			}, new int[]
			{
				1,
				2,
				3,
				4,
				5,
				6
			}, rewardAmounts2);
			PlayerStats.heroEvovleAch = new Achievement(data4);
			AchievementData data5 = new AchievementData(6, new string[]
			{
				"CgkIlpSuuo0ZEAIQHA",
				"CgkIlpSuuo0ZEAIQHQ",
				"CgkIlpSuuo0ZEAIQHg",
				"CgkIlpSuuo0ZEAIQIA",
				"CgkIlpSuuo0ZEAIQHw",
				"CgkIlpSuuo0ZEAIQKw"
			}, new int[]
			{
				1,
				2,
				3,
				4,
				5,
				6
			}, rewardAmounts2);
			PlayerStats.heroElevenEvolveAch = new Achievement(data5);
			AchievementData data6 = new AchievementData(5, new string[]
			{
				"CgkIlpSuuo0ZEAIQIQ",
				"CgkIlpSuuo0ZEAIQIg",
				"CgkIlpSuuo0ZEAIQIw",
				"CgkIlpSuuo0ZEAIQJQ",
				"CgkIlpSuuo0ZEAIQJA"
			}, new int[]
			{
				AchievementProgress.CRAFT_ARTIFACT_1,
				AchievementProgress.CRAFT_ARTIFACT_2,
				AchievementProgress.CRAFT_ARTIFACT_3,
				AchievementProgress.CRAFT_ARTIFACT_4,
				AchievementProgress.CRAFT_ARTIFACT_5
			}, rewardAmounts);
			PlayerStats.artifactCraftAch = new Achievement(data6);
			AchievementData data7 = new AchievementData(5, new string[]
			{
				"CgkIlpSuuo0ZEAIQPQ",
				"CgkIlpSuuo0ZEAIQPg",
				"CgkIlpSuuo0ZEAIQPw",
				"CgkIlpSuuo0ZEAIQQA",
				"CgkIlpSuuo0ZEAIQQQ"
			}, new int[]
			{
				AchievementProgress.ARTIFACT_TOTAL_LEVEL_1,
				AchievementProgress.ARTIFACT_TOTAL_LEVEL_2,
				AchievementProgress.ARTIFACT_TOTAL_LEVEL_3,
				AchievementProgress.ARTIFACT_TOTAL_LEVEL_4,
				AchievementProgress.ARTIFACT_TOTAL_LEVEL_5
			}, rewardAmounts);
			PlayerStats.artifactQpAch = new Achievement(data7);
			AchievementData data8 = new AchievementData(5, new string[]
			{
				"CgkIlpSuuo0ZEAIQMw",
				"CgkIlpSuuo0ZEAIQNA",
				"CgkIlpSuuo0ZEAIQNQ",
				"CgkIlpSuuo0ZEAIQNg",
				"CgkIlpSuuo0ZEAIQNw"
			}, new int[]
			{
				AchievementProgress.COLLECT_RUNES_1,
				AchievementProgress.COLLECT_RUNES_2,
				AchievementProgress.COLLECT_RUNES_3,
				AchievementProgress.COLLECT_RUNES_4,
				AchievementProgress.COLLECT_RUNES_5
			}, rewardAmounts);
			PlayerStats.collectRunesAch = new Achievement(data8);
			AchievementData data9 = new AchievementData(5, new string[]
			{
				"CgkIlpSuuo0ZEAIQLQ",
				"CgkIlpSuuo0ZEAIQLg",
				"CgkIlpSuuo0ZEAIQLw",
				"CgkIlpSuuo0ZEAIQMA",
				"CgkIlpSuuo0ZEAIQMQ"
			}, new int[]
			{
				AchievementProgress.COMPLETE_SIDEQUEST_1,
				AchievementProgress.COMPLETE_SIDEQUEST_2,
				AchievementProgress.COMPLETE_SIDEQUEST_3,
				AchievementProgress.COMPLETE_SIDEQUEST_4,
				AchievementProgress.COMPLETE_SIDEQUEST_5
			}, rewardAmounts);
			PlayerStats.completeSideQuestsAch = new Achievement(data9);
			PlayerStats.acvs.Add(PlayerStats.tapAch);
			PlayerStats.acvs.Add(PlayerStats.prestigeAch);
			PlayerStats.acvs.Add(PlayerStats.heroEvovleAch);
			PlayerStats.acvs.Add(PlayerStats.heroElevenEvolveAch);
			PlayerStats.acvs.Add(PlayerStats.challengeWinAch);
			PlayerStats.acvs.Add(PlayerStats.artifactCraftAch);
			PlayerStats.acvs.Add(PlayerStats.artifactQpAch);
			PlayerStats.acvs.Add(PlayerStats.collectRunesAch);
			PlayerStats.acvs.Add(PlayerStats.completeSideQuestsAch);
		}

		public static List<CountryCode> CountriesPlayerLoggedIn = new List<CountryCode>();

		public static int numLogins;

		public static float lifeTimeInCurrentSaveFile;

		public static long lifeTimeInTicks;

		public static long lifeTimeInTicksInCurrentSaveFile;

		public static long? playfabCreationDate;

		public static double spentCreditsDuringThisSaveFile;

		public static double spentCredits;

		public static double spentCreditsFirstDay;

		public static double spentMyth;

		public static double spentScraps;

		public static double spentTokens;

		public static double spentAeons;

		public static Dictionary<string, int> numUsedMerchantItems = new Dictionary<string, int>();

		public static int numAdDragonCatch;

		public static int numAdDragonMiss;

		public static int numAdAccept;

		public static int numAdCancel;

		public static int numFreeCredits;

		public static int numTotTap;

		public static List<Dictionary<string, bool>> achievements;

		public static List<int> achievementIndexes;

		public static List<int> iapsMade;

		public static int numTrinketPacksOpened;

		public static int numTotalDailySkip;

		public static int numTotalDailyCompleted;

		public static int allTimeCharmPacksOpened;

		public static int allTimeFlashOffersPurchased;

		public static int allTimeAdventureFlashOffersPurchased;

		public static int allTimeHalloweenFlashOffersPurchased;

		public static int allTimeChristmasFlashOffersPurchased;

		public static bool acquiredTrhoughInstantGame;

		public static int enemiesKilled;

		public static int timeHeroesDied;

		public static int ultimatesUsedCount;

		public static int minesCollectedCount;

		public static int secondaryAbilitiesCastedCount;

		public static int goblinChestsDestroyedCount;

		public static int screenshotsSharedInAdventure;

		public static int screenshotsSharedInTimeChallenges;

		public static int screenshotsSharedInGog;

		public static int totalTrinketPackWithGem;

		public static int totalTrinketPackWithGemOrAeon;

		public static int numRiftWon;

		public static int numCursedRiftWon;

		private static Achievement tapAch;

		private static Achievement prestigeAch;

		private static Achievement heroEvovleAch;

		private static Achievement heroElevenEvolveAch;

		private static Achievement challengeWinAch;

		private static Achievement artifactCraftAch;

		private static Achievement artifactQpAch;

		private static Achievement collectRunesAch;

		private static Achievement completeSideQuestsAch;

		public static List<Achievement> acvs;

		public delegate void AchievementEvent(int amount);
	}
}
