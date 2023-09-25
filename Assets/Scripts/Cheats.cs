using System;
using System.Collections.Generic;
using System.Diagnostics;
using Simulation;

public static class Cheats
{
	public static float[] DEBUG_TIME_SCALES = new float[]
	{
		0.1f,
		0.5f,
		1f,
		5f,
		10f,
		50f,
		100f,
		1000f
	};

	public static bool dontShowNewTALMilestoneReachedPopup = false;

	public static bool dontShowRatePopup = false;

	public static bool autoTap = false;

	public static bool autoTapWithoutInput = false;

	public static bool autoPlay = false;

	public static bool autoPlayPurchaseHero = true;

	public static bool autoPlayUpgradeHero = true;

	public static bool autoPlayUpgradeRing = true;

	public static bool autoPlayUpgradeMilestones = true;

	public static bool autoPlayDistributeSkills = true;

	public static bool autoPlayGoToBossFights = true;

	public static bool autoPlayCraftRegularArtifacts = false;

	public static bool autoPlayCraftMythicalArtifacts = false;

	public static bool autoPlayUpgradeRegularArtifacts = false;

	public static bool autoPlayUpgradeMythicalArtifacts = false;

	public static bool autoPlayFakePrestige = false;

	public static bool autoPlayPickCharm = true;

	public static bool autoPlayRifts = false;

	public static RiftAutoPlayCheat autoPlayRiftData;

	public static bool autoPlayAdventure = false;

	public static AdventureAutoPlayCheat autoPlayAdventureData;

	public static float timeSinceLastAutoTap;

	public static bool autoUsageForUltimate = false;

	public static float timePassed;

	public static float timeScale = 1f;

	public static float attackSpeed = 1f;

	public static bool allFree = false;

	public static int numStageChange = 0;

	public static bool giveRandomGear = false;

	public static bool giveRandomRune = false;

	public static bool giveRandomTrinket = false;

	public static bool unlockEverything = false;

	public static bool willReset = false;

	public static bool resetUltiCooldowns;

	public static bool resetActiveCooldowns;

	public static bool callSave;

	public static bool callLoad;

	public static bool deleteSave;

	public static bool simulateOffline;

	public static bool boostArtifacts;

	public static bool shortOfferTimes;

	public static bool skipTutorials;

	public static bool addDailyQuestProgress;

	public static bool doNotFinishRiftTutorials;

	public static bool doNotFinishTrinketTutorials;

	public static bool repickCurses;

	public static bool doNotFinishTrinketSmithingTutorials;

	public static bool doNotFinishChristmasTreeEventTutorials;

	public static bool blockTrustedTimeRefresh;

	public static bool immediateUnlocks;

	public static bool render;

	public static bool damgeFloats;

	public static DateTime timeStart;

	public static string version;

	public static Version versionObject;

	public static string lastVersionToClearSave = "0.9.0";

	public static bool dontUpdateGame = false;

	public static bool addPlayfabReward;

	public static bool showAdOffer;

	public static float totalLoadTime = 0f;

	public static float loadStartTime = 0f;

	public static float shortOfferTimeMultiplier = 0.2f;

	public static Cheats.ShorfOfferTimeFlags shortOfferTimeSettings = new Cheats.ShorfOfferTimeFlags();

	public static Stopwatch stopwatch = new Stopwatch();

	public static List<Func<RiftEffect>> riftEffectCreatorFunctions = new List<Func<RiftEffect>>
	{
		() => new RiftEffectBasicAttacksToRingTabs(),
		() => new RiftEffectDoubledCrits(),
		() => new RiftEffectDyingDealsDamage(),
		() => new RiftEffectGoldToDamage(),
		() => new RiftEffectHeroHealthToDamage(),
		() => new RiftEffectLongerUltimateCD(),
		() => new RiftEffectLongerRespawns(),
		() => new RiftEffectNoAbilityDamage(),
		() => new RiftEffectShorterUltimateCD(),
		() => new RiftEffectShorterRespawns(),
		() => new RiftEffectEveryoneDodges(),
		() => new RiftEffectNoGoldDrop(),
		() => new RiftEffectMeteorShower(),
		() => new RiftEffectCritChance(),
		() => new RiftEffectFastEnemies(),
		() => new RiftEffectUpgradeCostReduction(),
		() => new RiftEffectDyingHealsAllies(),
		() => new RiftEffectAllDamageBuff(),
		() => new RiftEffectRegeneration(),
		() => new RiftEffectReflectDamage(),
		() => new RiftEffectCharmsProgress(),
		() => new RiftEffectOnlyAttackCharms(),
		() => new RiftEffectOnlyDefenseCharms(),
		() => new RiftEffectOnlyUtilityCharms(),
		() => new RiftEffectStunDropsGold(),
		() => new RiftEffectTreasureChests()
	};

	public class ShorfOfferTimeFlags
	{
		public bool chests = true;

		public bool specialOffers = true;

		public bool flashOffers = true;

		public bool dailyQuests = true;

		public bool mines = true;

		public bool restBonus = true;

		public bool cursedGate = true;

		public bool seasonal = true;
	}
}
