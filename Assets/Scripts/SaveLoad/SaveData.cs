using System;
using System.Collections.Generic;
using Simulation;
using Simulation.ArtifactSystem;
using SocialRewards;

namespace SaveLoad
{
	[Serializable]
	public class SaveData
	{
		public string gameVersion;

		public long saveTime;

		public int languageSelected;

		public bool isMerchantUnlocked;

		public bool hasCompass;

		public bool hasDailies;

		public bool hasRift;

		public bool hasSkillPointsAutoDistribution;

		public int currentGameMode;

		public double mythstones;

		public double scraps;

		public double credits;

		public double tokens;

		public double aeon;

		public double candies;

		public DateTime lastCandyAmountCapDailyReset = DateTime.MinValue;

		public double candyAmountCollectedSinceLastDailyCapReset;

		public long lootpackFreeLastOpenTime;

		public long lootpackFreeLastOpenTimeServer;

		public long lastCappedWatchedTime;

		public long lastCappedCandiesWatchedTime;

		public bool hasEverHadPlayfabTime;

		public long lastOfflineEarnedTime;

		public int numArtifactSlots;

		public List<ArtifactSerializable> artifacts;

		public HashSet<int> artifactForcedDisableds;

		public int numArtifactSlotsMythical;

		public PrestigeRunStat lastPrestigeRun;

		public Dictionary<string, int> boughtGearLevels;

		public HashSet<string> boughtRunes;

		public Dictionary<string, HashSet<string>> wornRunes;

		public HashSet<string> unlockedWorldIds;

		public HashSet<string> unlockedTotemIds;

		public HashSet<string> unlockedHeroIds;

		public HashSet<string> newHeroIconSelectedHeroIds;

		public HashSet<int> boughtHeroSkins;

		public HashSet<int> newHeroSkins;

		public HashSet<uint> collectedUnlockIds;

		public List<int> completedQOUs;

		public List<int> failedQOUs;

		public QuestOfUpdateSerializable qou;

		public Dictionary<string, int> merchantItemLevels;

		public Dictionary<string, int> eventMerchantItemLevels;

		public Dictionary<string, int> worldsChallengeStates;

		public Dictionary<string, float> worldsChallengeTimePassed;

		public Dictionary<string, double> worldsGold;

		public Dictionary<string, double> worldsOfflineGold;

		public Dictionary<string, int> worldsTotWave;

		public Dictionary<string, int> worldsMaxStageReached;

		public Dictionary<string, int> worldsMaxHeroLevelReached;

		public Dictionary<string, int> worldsNumBoughtWorldUpgrades;

		public Dictionary<string, int> worldsNumGivenSkillPoints;

		public Dictionary<string, List<string>> worldsBoughtHeroes;

		public Dictionary<string, List<bool>> worldsDuplicatedHeroes;

		public Dictionary<string, List<int>> worldsActiveCharms;

		public Dictionary<string, List<int>> worldsDiscardedCharms;

		public Dictionary<string, List<float>> worldsCharmBuffStates;

		public Dictionary<string, List<int>> worldsCurseLevels;

		public Dictionary<string, float> worldsCurseProgress;

		public Dictionary<string, int> worldsCurseSpawnIndexes;

		public Dictionary<string, int> worldsCharmSelectionNums;

		public Dictionary<string, float> worldsCharmSelectionTimers;

		public Dictionary<string, string> worldsBoughtTotems;

		public Dictionary<string, Dictionary<string, int>> worldsNumMerchantItemsUsed;

		public Dictionary<string, Dictionary<string, int>> worldsNumMerchantItemsInInventory;

		public Dictionary<string, Dictionary<string, int>> worldsNumEventMerchantItemsUsed;

		public Dictionary<string, Dictionary<string, int>> worldsNumEventMerchantItemsInInventory;

		public Dictionary<string, WorldMerchantUseState> worldMerchantUseStates;

		public Dictionary<string, int> totemLevels;

		public Dictionary<string, int> totemXps;

		public Dictionary<string, int> heroEvolveLevels;

		public Dictionary<string, int> heroEquippedSkins;

		public Dictionary<string, bool> heroRandomSkinsEnabled;

		public Dictionary<string, int[]> heroSkillBranchesEverUnlocked;

		public Dictionary<string, int> heroLevels;

		public Dictionary<string, int> heroXps;

		public Dictionary<string, int> heroNumUnspentSkillPoints;

		public Dictionary<string, SkillTreeProgressSerializable> heroSkillTreesProgressGained;

		public Dictionary<string, double> heroHealthRatios;

		public Dictionary<string, double> heroCostMultipliers;

		public Dictionary<string, float> heroTillReviveTimes;

		public Dictionary<string, List<float>> heroSkillCooldowns;

		public Dictionary<string, Dictionary<string, string>> heroGeneric;

		public Dictionary<int, double> riftRecords;

		public Dictionary<int, CharmLevelStatusSerializable> charmStatus;

		public List<int> cardPackCounters;

		public int numPrestiges;

		public int maxStagePrestigedAt;

		public int tutStateFirst;

		public int tutStateHub;

		public int tutStateMode;

		public int tutStateArtifacts;

		public int tutStateShop;

		public int tutStatePrestige;

		public int tutStatePostPrestige;

		public int tutStateSkillScreen;

		public int tutStateFightBossButton;

		public int tutStateGearScreen;

		public int tutStateGearGlobalReminder;

		public int tutStateRuneScreen;

		public int tutStateRingPrestigeReminder;

		public int tutStateHeroPrestigeReminder;

		public int tutStateMythicalArtifactsTab;

		public int tutStateTrinketShop;

		public int tutStateTrinketHeroTab;

		public int tutStateMineUnlock;

		public int tutStateDailyUnlock;

		public int tutStateDailyComplete;

		public int tutStateRiftsUnlocked;

		public int tutStateRiftEffects;

		public int tutStateFirstCharm;

		public int tutStateCharmHub;

		public int tutStateFirstCharmPack;

		public int tutStateCharmLevelUp;

		public int tutStateAeonDust;

		public int tutStateRepeatRifts;

		public int tutStateAllRiftsFinished;

		public int tutStateFlashOffersUnlocked;

		public int tutStateCursedGates;

		public int tutStateMissionsFinished;

		public int tutStateTrinketSmithingUnlocked;

		public int tutStateTrinketRecycleUnlocked;

		public int tutStateChristmasTreeEventUnlocked;

		public int tutStateArtifaceOverhaul;

		public float tutTimeCounter;

		public float tutFirstPeriod;

		public float tutHubTabPeriod;

		public float tutModeTabPeriod;

		public float tutArtifactsTabPeriod;

		public float tutShopTabPeriod;

		public float tutPrestigePeriod;

		public float tutSkillScreenPeriod;

		public float tutFightBossButtonPeriod;

		public float tutGearScreenPeriod;

		public float tutGearGlobalReminderPeriod;

		public float tutRingPrestigeReminderPeriod;

		public float tutHeroPrestigeReminderPeriod;

		public float tutMythicalArtifactsTabPeriod;

		public int tutorialMissionIndex = -1;

		public float tutorialMissionProgress;

		public bool tutorialMissionClaimed;

		public float playerStatLifeTime;

		public float playerStatLifeTimeInCurrentSaveFile;

		public long playerStatLifeTimeInTicks;

		public long playerStatLifeTimeInTicksInCurrentSaveFile;

		public int playerStatNumLogins;

		public double playerStatSpentCredits;

		public double playerStatSpentCreditsFirstDay;

		public double playerStatSpentCreditsDuringThisSaveFile;

		public double playerStatSpentMyth;

		public double playerStatSpentScraps;

		public double playerStatSpentTokens;

		public double playerStatSpentAeons;

		public int playerStatNumAdDragonCatch;

		public int playerStatNumAdDragonMiss;

		public int playerStatNumAdAccept;

		public int playerStatNumAdCancel;

		public int playerStatNumFreeCredits;

		public int playerStatNumTotTap;

		public int playerStatEnemiesKilled;

		public int playerStatTimeHeroesDied;

		public int playerStatUltimatesUsedCount;

		public int playerStatMinesCollectedCount;

		public int playerStatSecondaryAbilitiesCastedCount;

		public int playerStatGoblinChestsDestroyedCount;

		public int playerStatScreenshotsSharedInAdventure;

		public int playerStatScreenshotsSharedInTimeChallenges;

		public int playerStatScreenshotsSharedInGog;

		public Dictionary<string, int> playerStatNumUsedMerchantItems;

		public Dictionary<int, bool> achievements;

		public Dictionary<int, bool> achiColls;

		public bool prefers30Fps;

		public bool compassDisabled;

		public bool soundsMute;

		public bool musicMute;

		public bool voicesMute;

		public bool skillOneTapUpgrade;

		public bool sleep;

		public bool snot;

		public bool scd;

		public bool wasWatchingAd;

		public bool wasWatchingAdCapped;

		public bool wasWatchingAdForFlashOffer;

		public FlashOffer.Type adFlashOfferRewardType;

		public int adRewardCurrencyType;

		public double adRewardAmount;

		public bool askedForRate;

		public List<string> allPlayfabIds;

		public bool dontStoreAuth;

		public int seedArtifact;

		public int seedLootpack;

		public int seedTrinket;

		public int seedCharmpack;

		public int seedCharmdraft;

		public int seedCursedGate;

		public int seedNewCurses;

		public List<TrinketSerializable> trinkets;

		public Dictionary<int, int> disassembledTrinketEffects;

		public bool hasEverOwnedATrinket;

		public int numTrinketsObtained;

		public Dictionary<string, int> heroTrinkets;

		public Dictionary<string, float> heroTrinketTimers;

		public int shopPack;

		public long shopPackTime;

		public long lastOfferEndTime;

		public long lastRiftOfferEndTime;

		public int lastShopPackOffer;

		public int offersAppearedCount;

		public bool isInstantPlayer;

		public bool spsa;

		public bool spsab;

		public bool sptpa;

		public bool spsp;

		public bool spxp;

		public bool spbga;

		public bool spbgta;

		public bool spbgp;

		public bool spbgtp;

		public bool spro01a;

		public bool spro01p;

		public bool spro02a;

		public bool spro02p;

		public bool spro03a;

		public bool spro03p;

		public bool spro04a;

		public bool spro04p;

		public bool spsha;

		public bool spshp;

		public bool regionalOffer01Purchased;

		public bool regionalOffer01Appeared;

		public bool halloweenOfferGemsPurchased;

		public bool halloweenOfferGemsAppeared;

		public bool christmasOfferGemsSmallPurchased;

		public bool christmasOfferGemsSmallAppeared;

		public bool christmasOfferGemsBigPurchased;

		public bool christmasOfferGemsBigAppeared;

		public bool stage200OfferAppeared;

		public bool stage200OfferPurchased;

		public bool stage500OfferAppeared;

		public bool stage500OfferPurchased;

		public bool secondAnniversaryGemsOfferAppeared;

		public bool secondAnniversaryGemsOfferPurchased;

		public bool secondAnniversaryBundleOfferAppeared;

		public bool secondAnniversaryBundleOfferPurchased;

		public bool secondAnniversaryGemsTwoOfferAppeared;

		public bool secondAnniversaryGemsTwoOfferPurchased;

		public bool secondAnniversaryBundleTwoOfferAppeared;

		public bool secondAnniversaryBundleTwoOfferPurchased;

		public int numTrinketPacks;

		public int numUnseenTrinketPacks;

		public int numCharmPacks;

		public bool notifOn;

		public bool notifAsked;

		public int notifs = int.MinValue;

		public List<int> iapsMade;

		public int numTrinketPacksOpened;

		public List<int> trinketsPinned;

		public HashSet<int> newLabelledCharms;

		public HashSet<int> newLabelledCurses;

		public MineSerializable mineToken;

		public MineSerializable mineScrap;

		public bool dontTryAuth;

		public bool dontAskAuth;

		public int daily;

		public int dailyProgress;

		public int lastDaily;

		public int dailySkip;

		public bool isSkinsEverClicked;

		public long dailyTime;

		public int dailyQuestsAppearedCount;

		public int totalDailySkip;

		public int totalDailyCompleted;

		public int numCandyQuest;

		public int numCandyAnQuest;

		public int riftDiscoveryIndex;

		public double riftQuestDustCollected;

		public bool hasRiftQuest;

		public double prestigeRunTime;

		public long riftTime;

		public int activeRiftChallengeId;

		public FlashOfferBundleSerializable flashOfferBundle;

		public ServerSideFlashOfferBundleSerializable halloweenFlashOfferBundle;

		public bool riftAutoSkillDistribute;

		public bool adventureAutoSkillDistribute;

		public int allTimeCharmPacksOpened;

		public int allTimeFlashOffersPurchased;

		public int allTimeAdventureFlashOffersPurchased;

		public int allTimeHalloweenFlashOffersPurchased;

		public int allTimeChristmasFlashOffersPurchased;

		public int numCharmPacksOpened;

		public int numRiftQuestsCompleted;

		public bool icsd;

		public int cst;

		public bool isCharSS;

		public bool isTrinketSortingDescending;

		public int trinketSortingType;

		public bool isTrinketSortingShowing;

		public Dictionary<SocialRewards.Network, Status> socialRewardsStatus;

		public DateTime lastNewsTimestam;

		public RatingState ratingState;

		public Dictionary<string, int> lootpacksOpenedCount;

		public List<int> lastFiveOpenedCharms;

		public SpecialOfferBoardSerializable specialOfferBoard;

		public List<int> cursedIds;

		public List<int> cursedLevs;

		public bool wasAtiveChallengeCursed;

		public long timeLastAddedCursedRift;

		public List<int> currentCurses;

		public int cursedGatesBeaten;

		public int curseSpawnIndex;

		public int lastSelectedRegularGateIndex;

		public List<Simulator.AnnouncedOfferInfo> announcedOffersTimes;

		public int amountLootpacksOpenedForHint;

		public bool trinketSmith;

		public long installDate;

		public bool usedTrinketExploit;

		public List<List<int>> christmasOffersBundlePurchasesLeft;

		public bool christmasEventAlreadyDisabled;

		public bool candyDropAlreadyDisabled;

		public long lastFreeCandyTreatClaimedDate;

		public long lastSessionDate;

		public int christmasEventPopupsShown;

		public int christmasTreatVideosWatchedSinceLastReset;

		public List<PlayfabManager.RewardData> rewardsToGive;

		public bool christmasCandyCappedVideoNotificationSeen;

		public bool christmasFreeCandyNotificationSeen;

		public List<int> newStats;

		public List<int> earnedBadges;

		public List<int> notificationDismissedBadges;

		public bool christmasEventForbidden;

		public bool cataclysmSurviver;

		public bool stageRearrangeSurviver;

		public int numPrestigesSinceCataclysm;

		public List<Simulation.ArtifactSystem.Artifact> artifactList;

		public bool prestigedDuringSecondAnniversaryEvent;

		public bool reachedMaxStageInCurrentAdventure;

		public ServerSideFlashOfferBundleSerializable secondAnniversaryFlashOfferBundle;

		public int artifactMultiUpgradeIndex;

		public int timeChallengesLostCount;

		public bool secondAnniversaryEventAlreadyDisabled;

		public int totalTrinketPackWithGem;

		public int totalTrinketPackWithGemOrAeon;
	}
}
