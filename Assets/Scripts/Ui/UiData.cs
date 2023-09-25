using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class UiData : AahMonoBehaviour
	{
		public static UiData inst;

		public Sprite spriteMainScreenReversedExcalibur;

		public Sprite spriteMainScreenHeHasThePower;

		public Sprite spriteMainScreenSpinningDeathTwirl;

		public Sprite spriteMainScreenConcentration;

		public Sprite spriteMainScreenFastCheerful;

		public Sprite spriteMainScreenShockWave;

		public Sprite spriteMainScreenFireworks;

		public Sprite spriteMainScreenJokesOnYou;

		public Sprite spriteMainScreenSelfDestruct;

		public Sprite spriteMainScreenLobMagic;

		public Sprite spriteMainScreenOutOfControl;

		public Sprite spriteMainScreenThunderSomething;

		public Sprite spriteMainScreenBombard;

		public Sprite spriteMainScreenEatApple;

		public Sprite spriteMainScreenMiniCannon;

		public Sprite spriteMainScreenRainOfAxes;

		public Sprite spriteMainScreenShieldAll;

		public Sprite spriteMainScreenSlam;

		public Sprite spriteMainScreenSliceDice;

		public Sprite spriteMainScreenMasterThief;

		public Sprite spriteMainScreenRunEmmetRun;

		public Sprite spriteMainScreenAnger;

		public Sprite spriteMainScreenLunchTime;

		public Sprite spriteMainScreenTaunt;

		public Sprite spriteMainScreenSnipe;

		public Sprite spriteMainScreenSwiftMoves;

		public Sprite spriteMainScreenTargetPractice;

		public Sprite spriteMainScreenBattlecry;

		public Sprite spriteMainScreenBittersweet;

		public Sprite spriteMainScreenWeepingSong;

		public Sprite spriteMainScreenRoar;

		public Sprite spriteMainScreenFlare;

		public Sprite spriteMainScreenCrowAttack;

		public Sprite spriteMainScreenGreedGrenade;

		public Sprite spriteMainScreenNegotiate;

		public Sprite spriteMainScreenCommonAffinities;

		public Sprite spriteMainScreenDarkRitual;

		public Sprite spriteMainScreenDarkRitualToggleOff;

		public Sprite spriteMainScreenDemonicSwarm;

		public Sprite spriteMainScreenRegret;

		public Sprite spriteMainScreenTakeOneForTheTeam;

		public Sprite spriteMainScreenGoodCupOfTea;

		public Sprite spriteMainScreenGotYourBack;

		public Sprite spriteMainScreenBeastMode;

		public Sprite spriteMainScreenStampede;

		public Sprite spriteMainScreenLarry;

		[Header("Skill Icons")]
		public Sprite[] ultiBadges;

		public Sprite spriteSkillScreenHeHasThePower;

		public Sprite spriteSkillScreenReversedExcalibur;

		public Sprite spriteSkillScreenSpinningDeathTwirl;

		public Sprite spriteSkillScreenDodge;

		public Sprite spriteSkillScreenFriendship;

		public Sprite spriteSkillScreenLuckyBoy;

		public Sprite spriteSkillScreenParanoia;

		public Sprite spriteSkillScreenSharpEdge;

		public Sprite spriteSkillScreenSonOfForest;

		public Sprite spriteSkillScreenSonOfWind;

		public Sprite spriteSkillScreenEverlastingSpin;

		public Sprite spriteSkillScreenConcentration;

		public Sprite spriteSkillScreenFastCheerful;

		public Sprite spriteSkillScreenShockWave;

		public Sprite spriteSkillScreenChillDown;

		public Sprite spriteSkillScreenCollectScraps;

		public Sprite spriteSkillScreenForge;

		public Sprite spriteSkillScreenHardTraining;

		public Sprite spriteSkillScreenMadGirl;

		public Sprite spriteSkillScreenRecycle;

		public Sprite spriteSkillScreenVillageGirl;

		public Sprite spriteSkillScreenEarthquake;

		public Sprite spriteSkillScreenFireworks;

		public Sprite spriteSkillScreenJokesOnYou;

		public Sprite spriteSkillScreenSelfDestruct;

		public Sprite spriteSkillScreenConcussion;

		public Sprite spriteSkillScreenCrackShot;

		public Sprite spriteSkillScreenExplosiveShots;

		public Sprite spriteSkillScreenFragmentation;

		public Sprite spriteSkillScreenFuelThemUp;

		public Sprite spriteSkillScreenStubbernness;

		public Sprite spriteSkillScreenMadness;

		public Sprite spriteSkillScreenWhatDoesNotKillYou;

		public Sprite spriteSkillScreenLobMagic;

		public Sprite spriteSkillScreenOutOfControl;

		public Sprite spriteSkillScreenThunderSomething;

		public Sprite spriteSkillScreenCraziness;

		public Sprite spriteSkillScreenDoubleMissile;

		public Sprite spriteSkillScreenElderness;

		public Sprite spriteSkillScreenForgetful;

		public Sprite spriteSkillScreenSpiritTalk;

		public Sprite spriteSkillScreenWisdom;

		public Sprite spriteSkillScreenRapidThunder;

		public Sprite spriteSkillScreenTricks;

		public Sprite spriteSkillScreenBombard;

		public Sprite spriteSkillScreenEatApple;

		public Sprite spriteSkillScreenMiniCannon;

		public Sprite spriteSkillScreenFastReloader;

		public Sprite spriteSkillScreenNicestKiller;

		public Sprite spriteSkillScreenRottenApple;

		public Sprite spriteSkillScreenSharpShooter;

		public Sprite spriteSkillScreenWellFed;

		public Sprite spriteSkillScreenXxl;

		public Sprite spriteSkillScreenBigStomach;

		public Sprite spriteSkillScreenConstitution;

		public Sprite spriteSkillScreenRainOfAxes;

		public Sprite spriteSkillScreenShieldAll;

		public Sprite spriteSkillScreenSlam;

		public Sprite spriteSkillScreenBlock;

		public Sprite spriteSkillScreenHoldYourGround;

		public Sprite spriteSkillScreenLetThemCome;

		public Sprite spriteSkillScreenManOfHill;

		public Sprite spriteSkillScreenPunishment;

		public Sprite spriteSkillScreenRepel;

		public Sprite spriteSkillScreenTranscendence;

		public Sprite spriteSkillScreenMasterShielder;

		public Sprite spriteSkillScreenSliceDice;

		public Sprite spriteSkillScreenMasterThief;

		public Sprite spriteSkillScreenRunEmmetRun;

		public Sprite spriteSkillScreenCityThief;

		public Sprite spriteSkillScreenEvasion;

		public Sprite spriteSkillScreenHearthSeeker;

		public Sprite spriteSkillScreenHiddenDaggers;

		public Sprite spriteSkillScreenPoisonedDaggers;

		public Sprite spriteSkillScreenTreasureHunter;

		public Sprite spriteSkillScreenWeakPoint;

		public Sprite spriteSkillScreenSwiftEmmet;

		public Sprite spriteSkillScreenAnger;

		public Sprite spriteSkillScreenLunchTime;

		public Sprite spriteSkillScreenTaunt;

		public Sprite spriteSkillScreenAngerManagement;

		public Sprite spriteSkillScreenBash;

		public Sprite spriteSkillScreenBigGuy;

		public Sprite spriteSkillScreenFullStomach;

		public Sprite spriteSkillScreenRegeneration;

		public Sprite spriteSkillScreenResilience;

		public Sprite spriteSkillScreenToughness;

		public Sprite spriteSkillScreenFreshMeat;

		public Sprite spriteSkillScreenSnipe;

		public Sprite spriteSkillScreenSwiftMoves;

		public Sprite spriteSkillScreenTargetPractice;

		public Sprite spriteSkillScreenAccuracy;

		public Sprite spriteSkillBlindNotDeaf;

		public Sprite spriteSkillScreenElegance;

		public Sprite spriteSkillScreenFeedPoor;

		public Sprite spriteSkillScreenMultiShot;

		public Sprite spriteSkillScreenOneShot;

		public Sprite spriteSkillScreenSurvivalist;

		public Sprite spriteSkillScreenTracker;

		public Sprite spriteSkillScreenBattlecry;

		public Sprite spriteSkillScreenBittersweet;

		public Sprite spriteSkillScreenWeepingSong;

		public Sprite spriteSkillScreenDepression;

		public Sprite spriteSkillScreenHeroism;

		public Sprite spriteSkillScreenNotSoFast;

		public Sprite spriteSkillScreenDividedWeFall;

		public Sprite spriteSkillScreenLullaby;

		public Sprite spriteSkillScreenPartyTime;

		public Sprite spriteSkillScreenPrettyFace;

		public Sprite spriteSkillScreenTogetherWeStand;

		public Sprite spriteSkillScreenRoar;

		public Sprite spriteSkillScreenFlare;

		public Sprite spriteSkillScreenMark;

		public Sprite spriteSkillScreenPreparation;

		public Sprite spriteSkillScreenBandage;

		public Sprite spriteSkillScreenFeignDeath;

		public Sprite spriteSkillScreenCrowAttack;

		public Sprite spriteSkillScreenFrenzy;

		public Sprite spriteSkillScreenInstincts;

		public Sprite spriteSkillScreenDeathFromAbove;

		public Sprite spriteSkillScreenCallOfWild;

		public Sprite spriteSkillScreenNegotiate;

		public Sprite spriteSkillScreenKeenNose;

		public Sprite spriteSkillScreenTradeSecret;

		public Sprite spriteSkillScreenDistraction;

		public Sprite spriteSkillScreenLooseChange;

		public Sprite spriteSkillScreenCommonAffinities;

		public Sprite spriteSkillScreenFormerFriends;

		public Sprite spriteSkillScreenConfusingPresence;

		public Sprite spriteSkillScreenBargainer;

		public Sprite spriteSkillScreenTimidFriends;

		public Sprite spriteSkillScreenGreedGrenade;

		public Sprite spriteSkillScreenDarkRitual;

		public Sprite spriteSkillScreenDemonicSwarm;

		public Sprite spriteSkillScreenRegret;

		public Sprite spriteSkillScreenSoulSacrifice;

		public Sprite spriteSkillScreenOldHabits;

		public Sprite spriteSkillScreenPracticed;

		public Sprite spriteSkillScreenWarmerSwarm;

		public Sprite spriteSkillScreenTerrifying;

		public Sprite spriteSkillScreenReflectiveWards;

		public Sprite spriteSkillScreenFeelingBetter;

		public Sprite spriteSkillScreenPowerHungry;

		public Sprite spriteSkillScreenTakeOneForTheTeam;

		public Sprite spriteSkillScreenGoodCupOfTea;

		public Sprite spriteSkillScreenToughLove;

		public Sprite spriteSkillScreenLongWinded;

		public Sprite spriteSkillScreenMoraleBooster;

		public Sprite spriteSkillScreenContagiousConfidence;

		public Sprite spriteSkillScreenGotYourBack;

		public Sprite spriteSkillScreenBrushItOff;

		public Sprite spriteSkillScreenInnerWorth;

		public Sprite spriteSkillScreenShareTheBurden;

		public Sprite spriteSkillScreenExpertLoveLore;

		public Sprite spriteSkillScreenBeastMode;

		public Sprite spriteSkillScreenStampede;

		public Sprite spriteSkillScreenStrengthInNumbers;

		public Sprite spriteSkillScreenMassivePaws;

		public Sprite spriteSkillScreenGoForTheEyes;

		public Sprite spriteSkillScreenImpulsive;

		public Sprite spriteSkillScreenLarry;

		public Sprite spriteSkillScreenHuntersInstinct;

		public Sprite spriteSkillScreenCurly;

		public Sprite spriteSkillScreenMoe;

		public Sprite spriteSkillScreenRageDriven;

		public Sprite spriteGearHoratioTunic;

		public Sprite spriteGearHoratioHeadgear;

		public Sprite spriteGearHoratioShoes;

		public Sprite spriteGearIdaTong;

		public Sprite spriteGearIdaWristband;

		public Sprite spriteGearIdaBelt;

		public Sprite spriteGearAlexLighter;

		public Sprite spriteGearAlexCigarettes;

		public Sprite spriteGearAlexDynamite;

		public Sprite spriteGearDerekBookmark;

		public Sprite spriteGearDerekInkBottle;

		public Sprite spriteGearDerekHandfulOfFleas;

		public Sprite spriteGearLennyCap;

		public Sprite spriteGearLennyTunic2;

		public Sprite spriteGearLennyCannon;

		public Sprite spriteGearSamNecklace;

		public Sprite spriteGearSamBracers;

		public Sprite spriteGearSamAxeHandle;

		public Sprite spriteGearSheelaGoldSack;

		public Sprite spriteGearSheelaTunic3;

		public Sprite spriteGearSheelaDaggerHilt;

		public Sprite spriteGearThourPieceOfWood;

		public Sprite spriteGearThourMeat;

		public Sprite spriteGearThourFlask;

		public Sprite spriteGearLiaTBD1;

		public Sprite spriteGearLiaTBD2;

		public Sprite spriteGearLiaTBD3;

		public Sprite spriteGearJimTBD1;

		public Sprite spriteGearJimTBD2;

		public Sprite spriteGearJimTBD3;

		public Sprite spriteGearTamGoldenFeather;

		public Sprite spriteGearTamJuicyMeat;

		public Sprite spriteGearTamDeadlyShells;

		public Sprite spriteGearWarlockFriendlySkull;

		public Sprite spriteGearWarlockGoldAmulet;

		public Sprite spriteGearWarlockVialofVileness;

		public Sprite spriteGearGoblinGoldenKey;

		public Sprite spriteGearGoblinSpareLeg;

		public Sprite spriteGearGoblinSpyLens;

		public Sprite spriteGearBabuCupOfTea;

		public Sprite spriteGearBabuMortar;

		public Sprite spriteGearBabuShoes;

		public Sprite spriteGearDruid01;

		public Sprite spriteGearDruid02;

		public Sprite spriteGearDruid03;

		public Sprite spriteHeroPortraitThour;

		public Sprite spriteHeroPortraitIda;

		public Sprite spriteHeroPortraitLenny;

		public Sprite spriteHeroPortraitAlex;

		public Sprite spriteHeroPortraitDerek;

		public Sprite spriteHeroPortraitSam;

		public Sprite spriteHeroPortraitSheela;

		public Sprite spriteHeroPortraitHoratio;

		public Sprite spriteHeroPortraitBlindArcher;

		public Sprite spriteHeroPortraitJim;

		public Sprite spriteHeroPortraitTam;

		public Sprite spriteHeroPortraitGoblin;

		public Sprite spriteHeroPortraitWarlock;

		public Sprite spriteHeroPortraitBabu;

		public Sprite spriteHeroPortraitDruid;

		[Header("Rings")]
		public Sprite spriteTotemFireRuneScreen;

		public Sprite spriteTotemEarthRuneScreen;

		public Sprite spriteTotemLightningRuneScreen;

		public Sprite spriteTotemIceRuneScreen;

		public Sprite spriteTotemFireSmall;

		public Sprite spriteTotemEarthSmall;

		public Sprite spriteTotemLightningSmall;

		public Sprite spriteTotemIceSmall;

		public Sprite spriteTotemFireShineSmall;

		public Sprite spriteTotemEarthShineSmall;

		public Sprite spriteTotemLightningShineSmall;

		public Sprite spriteTotemIceShineSmall;

		[Header("Runes")]
		public Color lightningRunesColor;

		public Color fireRunesColor;

		public Color iceRunesColor;

		public Color earthRunesColor;

		public Sprite spriteRune0;

		public Sprite spriteRune1;

		public Sprite spriteRune2;

		public Sprite spriteRune3;

		public Sprite spriteRune4;

		public Sprite spriteRune5;

		public Sprite spriteRune6;

		public Sprite spriteRune7;

		public Sprite spriteRune8;

		public Sprite spriteRune9;

		public Sprite spriteRune10;

		[Header("Artifacts")]
		public Sprite spriteArtifactIconHero;

		public Sprite spriteArtifactIconTotem;

		public Sprite spriteArtifactIconUtility;

		public Sprite spriteArtifactIconGold;

		public Sprite spriteArtifactIconEnergy;

		public Sprite spriteArtifactIconHealth;

		public Sprite spriteArtifactIconMythAutoTransmuter;

		public Sprite spriteArtifactIconMythBrokenTeleporter;

		public Sprite spriteArtifactIconMythCustomTailor;

		public Sprite spriteArtifactIconMythDpsMatter;

		public Sprite spriteArtifactIconMythFreeExploiter;

		public Sprite spriteArtifactIconMythGoblinLure;

		public Sprite spriteArtifactIconMythHalfRing;

		public Sprite spriteArtifactIconMythLazyFinger;

		public Sprite spriteArtifactIconMythLifeBoat;

		public Sprite spriteArtifactIconMythOldCrucible;

		public Sprite spriteArtifactIconMythPerfectQuasi;

		public Sprite spriteArtifactIconMythShinyObject;

		public Sprite spriteArtifactIconMythPowerupCritChance;

		public Sprite spriteArtifactIconMythPowerupCooldown;

		public Sprite spriteArtifactIconMythPowerupRevive;

		public Sprite spriteArtifactIconMythBodilyHarm;

		public Sprite spriteArtifactIconMythChampionsBounty;

		public Sprite spriteArtifactIconMythCorpusImperium;

		public Sprite spriteArtifactIconMythHeroDamagePerAttacker;

		public Sprite spriteArtifactIconMythHeroHealthPerDefender;

		public Sprite spriteArtifactIconMythGoldBonusPerSupporter;

		[Header("Merchant Items")]
		public Sprite spriteMerchantItemGoldPack;

		public Sprite spriteMerchantItemWarpTime;

		public Sprite spriteMerchantItemShield;

		public Sprite spriteMerchantItemAutoTap;

		public Sprite spriteMerchantItemTrainingBook;

		public Sprite spriteMerchantItemClock;

		public Sprite spriteMerchantItemPowerUp;

		public Sprite spriteMerchantItemRefresherOrb;

		public Sprite spriteMerchantItemGoldBoost;

		public Sprite spriteMerchantItemWaveClear;

		public Sprite spriteMerchantItemCatalyst;

		public Sprite spriteMerchantItemVariety;

		public Sprite spriteMerchantItemEmergency;

		public Sprite spriteMerchantItemPickRandomCharm;

		public Sprite spriteMerchantItemBlizzard;

		public Sprite spriteMerchantItemHotCocoa;

		public Sprite spriteMerchantItemOrnamentDrop;

		public Sprite spriteMerchantItemSmallWarpTime;

		public Sprite spriteMerchantItemSmallShield;

		public Sprite spriteMerchantItemSmallAutoTap;

		public Sprite spriteMerchantItemSmallTrainingBook;

		public Sprite spriteMerchantItemSmallClock;

		public Sprite spriteMerchantItemSmallRefresherOrb;

		public Sprite spriteMerchantItemSmallGoldBoost;

		public Sprite spriteMerchantItemSmallCatalyst;

		public Sprite spriteMerchantItemSmallVariety;

		public Sprite spriteMerchantItemSmallEmergency;

		public Sprite spriteMerchantItemSmallBlizzard;

		public Sprite spriteMerchantItemSmallHotCocoa;

		public Sprite spriteMerchantItemSmallOrnamentDrop;

		public Sprite spritePowerUpBoostCrit;

		public Sprite spritePowerUpBoostCooldown;

		public Sprite spritePowerUpBoostRevive;

		public Sprite spriteModeFlagStandard;

		public Sprite spriteModeFlagCrusade;

		public Sprite spriteModeFlagRift;

		[Header("Boss Portraits")]
		public Sprite spriteBossPortraitOrc;

		public Sprite spriteBossPortraitElf;

		public Sprite spriteBossPortraitDwarf;

		public Sprite spriteBossPortraitBandit;

		public Sprite spriteBossPortraitMagolies;

		public Sprite spriteBossPortraitSnowman;

		[Header("Unlock Rewards")]
		public Sprite spriteMythicalArtifactSlot;

		public Sprite spriteUnlockTrinketSlot;

		public Sprite spriteUnlockTrinketPack;

		public Sprite spriteUnlockDailyQuests;

		public Sprite spriteUnlockMineScrap;

		public Sprite spriteUnlockMineToken;

		public Sprite spriteUnlockMerchant;

		public Sprite spriteUnlockCompass;

		public Sprite spriteUnlockModeTimeChallenge;

		public Sprite spriteUnlockModeRift;

		public Sprite spriteUnlockPrestige;

		public Sprite spriteUnlockCharmPack;

		public Sprite spriteUnlockTrinketSmithing;

		public Sprite spriteUnlockPurchaseRandomHeroes;

		public Sprite spriteUnlockRandomHeroAndSkills;

		public Sprite spriteLockedNewMechanics;

		public Sprite spriteLockedNewImportantThings;

		public Sprite spriteLockedNewHeroes;

		public Sprite spriteLockedCurrency;

		public Sprite[] spriteTrinketBgs;

		public Sprite[] spriteTrinketBodies;

		public Sprite[] spriteTrinketShines;

		public Sprite[] spriteTrinketIcons;

		public Sprite[] spriteTrinketHangers;

		public Sprite[] spriteTrinketBeads;

		public GameObject prefabTrinket;

		public Sprite spriteAchievementTap;

		public Sprite spriteAchievementPrestige;

		public Sprite spriteAchievementTimeChallenge;

		public Sprite spriteAchievementEvolve;

		public Sprite spriteAchievementEvolveAll;

		public Sprite spriteAchievementCraftArtifact;

		public Sprite spriteAchievementArtifactQuality;

		public Sprite spriteAchievementCollectRune;

		public Sprite spriteAchievementCompleteSideQuest;

		public Sprite spriteMineScrapFull;

		public Sprite spriteMineScrapEmpty;

		public Sprite spriteMineTokenFull;

		public Sprite spriteMineTokenEmpty;

		public Sprite spriteShopBlueButton;

		public Sprite spriteShopGreenButton;

		public Sprite spriteShopBrownButton;

		public Sprite runeOfferBackgroundBig;

		public Sprite runeOfferBackgroundSmall;

		public Sprite spriteBgPanelBase;

		public Sprite spriteBg2x2;

		public Sprite spriteOfferTokenBig;

		public Sprite spriteofferTokenSmall;

		public Sprite spriteOfferScrapBig;

		public Sprite spriteOfferScrapSmall;

		public Sprite spriteOfferGemsSmall;

		public Sprite spriteCurrencyGoldTriangle;

		public Sprite spriteCurrencyGoldSquare;

		public Sprite shopBlueButton;

		public Sprite shopGreenButton;

		public Sprite[] currencySprites;

		public Sprite[] currencyBgSprites;

		public Sprite spriteTokenOfferSmall;

		public Sprite spriteTokenOfferBig;

		public Sprite spriteScrapsOfferSmall;

		public Sprite spriteScrapsOfferBig;

		[Header("Skins")]
		public Sprite skinFrameActive;

		public Sprite skinFramePassive;

		public Sprite skinIconRandom;

		public Sprite[] skinIconsHoratio;

		public Sprite[] skinIconsThour;

		public Sprite[] skinIconsVexx;

		public Sprite[] skinIconsKindLenny;

		public Sprite[] skinIconsWendle;

		public Sprite[] skinIconsV;

		public Sprite[] skinIconsBoomer;

		public Sprite[] skinIconsSam;

		public Sprite[] skinIconsLia;

		public Sprite[] skinIconsJim;

		public Sprite[] skinIconsTam;

		public Sprite[] skinIconsWarlock;

		public Sprite[] skinIconsGoblin;

		public Sprite[] skinIconsBabu;

		public Sprite[] skinIconsDruid;

		public Sprite spriteAnniversaryAchievement;

		public Sprite alchemyTabSymbolUp;

		public Sprite alchemyTabSymbolDown;

		public Sprite charmTabSymbolUp;

		public Sprite charmTabSymbolDown;

		public Sprite spriteRewardAeonDust;

		public Sprite spriteRewardAeonDustSmall;

		public Sprite spriteRewardAeon;

		public Sprite spriteRiftEffectLongCd;

		public Sprite spriteRiftEffectFastCd;

		public Sprite spriteRiftEffectFastSpawn;

		public Sprite spriteRiftEffectLongSpawn;

		public Sprite spriteRiftEffectDyingDealsDamage;

		public Sprite spriteRiftEffectGoldToDamage;

		public Sprite spriteRiftEffectTreasureChests;

		public Sprite spriteRiftEffectCharmProgress;

		public Sprite spriteRiftEffectReflect;

		public Sprite spriteRiftEffectRegen;

		public Sprite spriteRiftEffectUpgradeCostReduction;

		public Sprite spriteRiftEffectFastEnemies;

		public Sprite spriteRiftEffectDodge;

		public Sprite spriteRiftEffectNoGoldDrops;

		public Sprite spriteRiftEffectMeteor;

		public Sprite spriteRiftEffectDyingHeals;

		public Sprite spriteRiftEffectNoAbilityDamage;

		public Sprite spriteRiftEffectEverythingHurts;

		public Sprite spriteRiftEffectHeroHealthToDamage;

		public Sprite spriteRiftEffectCritChance;

		public Sprite spriteRiftEffectDoubledCrits;

		public Sprite spriteRiftEffectBasicAttacksToRingTabs;

		public Sprite spriteRiftEffectStunstoGold;

		public Sprite spriteRiftEffectOnlyAttackCharms;

		public Sprite spriteRiftEffectOnlyDefenseCharms;

		public Sprite spriteRiftEffectOnlyUtilityCharms;

		public Sprite spriteRiftEffectOnlyBoss;

		public Sprite spriteRiftEffectRandomHeroes;

		public Sprite spriteRiftEffectDyingHeroesDropGold;

		public Sprite spriteRiftEffectGoldDamageToHeroes;

		public Sprite spriteRiftEffectTimeDealsDamage;

		public Sprite spriteRiftEffectHalftHeal;

		public Sprite spriteRiftEffectDoubleHeal;

		public Sprite spriteRiftEffectStunningEnemies;

		public Sprite spriteRiftEffectFastHeroAttackSpeed;

		public Sprite spriteRiftEffectWiseSnakeBoss;

		public Sprite spriteRiftEffectCurse;

		public Sprite spriteRiftEffectGreyLongCd;

		public Sprite spriteRiftEffectGreyFastCd;

		public Sprite spriteRiftEffectGreyFastSpawn;

		public Sprite spriteRiftEffectGreyLongSpawn;

		public Sprite spriteRiftEffectGreyDyingDealsDamage;

		public Sprite spriteRiftEffectGreyGoldToDamage;

		public Sprite spriteRiftEffectGreyTreasureChests;

		public Sprite spriteRiftEffectGreyCharmProgress;

		public Sprite spriteRiftEffectGreyReflect;

		public Sprite spriteRiftEffectGreyRegen;

		public Sprite spriteRiftEffectGreyUpgradeCostReduction;

		public Sprite spriteRiftEffectGreyFastEnemies;

		public Sprite spriteRiftEffectGreyDodge;

		public Sprite spriteRiftEffectGreyNoGoldDrops;

		public Sprite spriteRiftEffectGreyMeteor;

		public Sprite spriteRiftEffectGreyDyingHeals;

		public Sprite spriteRiftEffectGreyNoAbilityDamage;

		public Sprite spriteRiftEffectGreyEverythingHurts;

		public Sprite spriteRiftEffectGreyHeroHealthToDamage;

		public Sprite spriteRiftEffectGreyCritChance;

		public Sprite spriteRiftEffectGreyDoubledCrits;

		public Sprite spriteRiftEffectGreyBasicAttacksToRingTabs;

		public Sprite spriteRiftEffectGreyStunstoGold;

		public Sprite spriteRiftEffectGreyOnlyAttackCharms;

		public Sprite spriteRiftEffectGreyOnlyDefenseCharms;

		public Sprite spriteRiftEffectGreyOnlyUtilityCharms;

		public Sprite spriteRiftEffectGreyOnlyBoss;

		public Sprite spriteRiftEffectGreyRandomHeroes;

		public Sprite spriteRiftEffectGreyDyingHeroesDropGold;

		public Sprite spriteRiftEffectGreyGoldDamageToHeroes;

		public Sprite spriteRiftEffectGreyTimeDealsDamage;

		public Sprite spriteRiftEffectGreyHalftHeal;

		public Sprite spriteRiftEffectGreyDoubleHeal;

		public Sprite spriteRiftEffectGreyStunningEnemies;

		public Sprite spriteRiftEffectGreyFastHeroAttackSpeed;

		public Sprite spriteRiftEffectGreyWiseSnakeBoss;

		public Sprite spriteCharmBack;

		public Sprite spriteCharmSlot;

		public Sprite spriteCharmFaceRed;

		public Sprite spriteCharmFaceGreen;

		public Sprite spriteCharmFaceBlue;

		public Sprite spriteCurseFace;

		public Color curseFaceColor;

		public Color charmFaceColor;

		public Color curseFrameNormalColor;

		public Color curseFrameGhostlyHeroesColor;

		public Sprite spriteCharmFlashRed;

		public Sprite spriteCharmFlashGreen;

		public Sprite spriteCharmFlashBlue;

		[Header("Red charms")]
		public Sprite spriteCharmBerserk;

		public Sprite spriteCharmFireyFire;

		public Sprite spriteCharmBootlegFireworks;

		public Sprite spriteCharmProfessionalStrike;

		public Sprite spriteCharmBribedAccuracy;

		public Sprite spriteThirstingFiends;

		public Sprite spriteCharmLooseLessons;

		public Sprite spriteCharmExplosiveEnergy;

		public Sprite spriteCharmBouncingBolt;

		public Sprite spriteCharmPowerOverwhelming;

		[Header("Green charms")]
		public Sprite spriteCharmGrimRewards;

		public Sprite spriteCharmSparksFromHeaven;

		public Sprite spriteCharmQuickStudy;

		public Sprite spriteCharmVengefulSparks;

		public Sprite spriteCharmSpecialDelivery;

		public Sprite spriteCharmExtremeImpatience;

		public Sprite spriteCharmWealthFromAbove;

		public Sprite spriteCharmEmergencyFlute;

		public Sprite spriteCharmTimeWarp;

		public Sprite spriteCharmLucrativeLightning;

		[Header("Blue charms")]
		public Sprite spriteCharmCallfromtheGrave;

		public Sprite spriteCharmRustyDaggers;

		public Sprite spriteCharmBodyBlock;

		public Sprite spriteCharmSweetMoves;

		public Sprite spriteCharmSpellShield;

		public Sprite spriteCharmFrostyStorm;

		public Sprite spriteCharmAngelicWard;

		public Sprite spriteCharmStaryStaryDay;

		public Sprite spriteCharmAppleADay;

		public Sprite spriteCharmQuackatoa;

		[Header("Curses")]
		public Sprite spriteCurseCDReduction;

		public Sprite spriteCurseDealDamage;

		public Sprite spriteCurseTimeSlow;

		public Sprite spriteCurseCharmProgress;

		public Sprite spriteCurseReflectDamage;

		public Sprite spriteCurseMissChance;

		public Sprite spriteCurseUpgradeCost;

		public Sprite spriteCurseHeroDamage;

		public Sprite spriteCurseStunHero;

		public Sprite spriteCurseAntiRegeneration;

		public Sprite spriteCurseHeavyLimbs;

		public Sprite spriteCurseUncannyRegeneration;

		public Sprite spriteCursePartingShot;

		public Sprite spriteCurseDelayedCharms;

		public Sprite spriteCurseHauntingVisage;

		public Sprite spriteCurseMoltenGold;

		public Sprite spriteCurseGogZeal;

		public Sprite spriteCurseDampenedWill;

		public Sprite spriteCurseIncantationInversion;

		public Sprite spriteCurseGhostlyHeroes;

		[Header("Others")]
		public Image imagePrafab;

		public Sprite spriteIconUTBdamageDark;

		public Sprite spriteIconUTBdamageLight;

		public Sprite spriteIconUTBgoldDark;

		public Sprite spriteIconUTBgoldLight;

		public Sprite spriteIconUTBhealthDark;

		public Sprite spriteIconUTBhealthLight;

		public Sprite spriteModeInfoOrnamentNormal;

		public Sprite spriteModeInfoOrnamentCorpedCorner;

		public Sprite iconTabLock;

		public Sprite iconTabLockBrown;

		public Sprite iconTabHubArtifact;

		public Sprite iconTabHubShop;

		public Sprite iconTabHubTrinket;

		public Sprite iconTabHubCharm;

		public Sprite merchantItemNormalBg;

		public Sprite merchantItemEventBg;

		public Sprite merchantItemLockedBg;

		public Material grayscaledSpriteMat;

		public PanelTrinketEffect prefabPanelTrinket;

		public Sprite regionalOfferButtonIcon;

		public Sprite halloweenOfferButtonIcon;

		public Sprite christmasOfferButtonIcon;

		public Sprite christmasOfferButtonBackground;

		public Sprite christmasOfferButtonFill;

		public Sprite secondAnniversaryEventButtonIcon;

		public Sprite artifactQpBackground;

		public Sprite artifactQpBackgroundGlow;

		public Sprite artifactMaxedQpBackground;

		public Sprite artifactMaxedQpBackgroundGlow;

		public Color artifactQpAmountColor;

		public Color artifactMaxedQpAmountColor;

		public Color artifactQpLabelColor;

		public Color artifactMaxedQpLabelColor;

		public Sprite elementMaxedBadgeIcon;

		[Header("Badges")]
		public Sprite badgeWintertideParticipant;

		public Sprite badgeWintertideCollector;

		public Sprite badgeWintertideTopOfTree;

		public Sprite badgeSnakeEater;

		public Sprite badgeOneYearAnniversaryParticipant;

		public Sprite badgeTwoYearsAnniversaryParticipant;

		public Sprite badgeCataclysmSurviver;

		[Header("Currency Carts")]
		public Sprite cartCurrencyGoldBig;

		public Sprite cartCurrencyGoldSmall;

		public Sprite cartCurrencyScrapsBig;

		public Sprite cartCurrencyScrapsSmall;

		public Sprite cartCurrencyMythstoneBig;

		public Sprite cartCurrencyMythstoneSmall;

		public Sprite cartCurrencyCreditsBig;

		public Sprite cartCurrencyCreditsSmall;

		public Sprite cartCurrencyTokensBig;

		public Sprite cartCurrencyTokensSmall;

		public Sprite cartCurrencyAeonsBig;

		public Sprite cartCurrencyAeonsSmall;

		[Header("Second Anniversary")]
		public Sprite giftBox;
	}
}
