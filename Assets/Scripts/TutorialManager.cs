using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using Simulation;
using Simulation.ArtifactSystem;
using Ui;
using UnityEngine;

public static class TutorialManager
{
	public static TutorialManager.First first
	{
		get
		{
			return TutorialManager._first;
		}
		set
		{
			if (TutorialManager._first != value)
			{
				TutorialManager.timeCounter = 0f;
			}
			if (value != TutorialManager.First.FIN)
			{
				SoundArchieve.inst.LoadUiBundle("sounds/greenman-tutorial");
			}
			else
			{
				SoundArchieve.inst.UnloadUiBundle("sounds/greenman-tutorial");
			}
			TutorialManager._first = value;
		}
	}

	public static void SetSimulator(Simulator s)
	{
		TutorialManager.sim = s;
	}

	public static void Reset()
	{
		TutorialManager.first = TutorialManager.First.WELCOME;
		TutorialManager.hubTab = TutorialManager.HubTab.BEFORE_BEGIN;
		TutorialManager.modeTab = TutorialManager.ModeTab.BEFORE_BEGIN;
		TutorialManager.artifactsTab = TutorialManager.ArtifactsTab.BEFORE_BEGIN;
		TutorialManager.shopTab = TutorialManager.ShopTab.BEFORE_BEGIN;
		TutorialManager.prestige = TutorialManager.Prestige.BEFORE_BEGIN;
		TutorialManager.skillScreen = TutorialManager.SkillScreen.BEFORE_BEGIN;
		TutorialManager.fightBossButton = TutorialManager.FightBossButton.BEFORE_BEGIN;
		TutorialManager.gearScreen = TutorialManager.GearScreen.BEFORE_BEGIN;
		TutorialManager.runeScreen = TutorialManager.RuneScreen.BEFORE_BEGIN;
		TutorialManager.ringPrestigeReminder = TutorialManager.RingPrestigeReminder.BEFORE_BEGIN;
		TutorialManager.heroPrestigeReminder = TutorialManager.HeroPrestigeReminder.BEFORE_BEGIN;
		TutorialManager.mythicalArtifactsTab = TutorialManager.MythicalArtifactsTab.BEFORE_BEGIN;
		TutorialManager.trinketShop = TutorialManager.TrinketShop.BEFORE_BEGIN;
		TutorialManager.trinketHeroTab = TutorialManager.TrinketHeroTab.BEFORE_BEGIN;
		TutorialManager.mineUnlock = TutorialManager.MineUnlock.BEFORE_BEGIN;
		TutorialManager.dailyUnlock = TutorialManager.DailyUnlock.BEFORE_BEGIN;
		TutorialManager.dailyComplete = TutorialManager.DailyComplete.BEFORE_BEGIN;
		TutorialManager.riftsUnlock = TutorialManager.RiftsUnlock.BEFORE_BEGIN;
		TutorialManager.riftEffects = TutorialManager.RiftEffects.BEFORE_BEGIN;
		TutorialManager.firstCharm = TutorialManager.FirstCharm.BEFORE_BEGIN;
		TutorialManager.charmHub = TutorialManager.CharmHub.BEFORE_BEGIN;
		TutorialManager.firstCharmPack = TutorialManager.FirstCharmPack.BEFORE_BEGIN;
		TutorialManager.charmLevelUp = TutorialManager.CharmLevelUp.BEFORE_BEGIN;
		TutorialManager.aeonDust = TutorialManager.AeonDust.BEFORE_BEGIN;
		TutorialManager.repeatRifts = TutorialManager.RepeatRifts.BEFORE_BEGIN;
		TutorialManager.allRiftsFinished = TutorialManager.AllRiftsFinished.FIN;
		TutorialManager.flashOffersUnlocked = TutorialManager.FlashOffersUnlocked.BEFORE_BEGIN;
		TutorialManager.cursedGates = TutorialManager.CursedGates.BEFORE_BEGIN;
		TutorialManager.missionsFinished = TutorialManager.MissionsFinished.BEFORE_BEGIN;
		TutorialManager.trinketSmithingUnlocked = TutorialManager.TrinketSmithingUnlocked.BEFORE_BEGIN;
		TutorialManager.trinketRecycleUnlocked = TutorialManager.TrinketRecycleUnlocked.BEFORE_BEGIN;
		TutorialManager.christmasTreeEventUnlocked = TutorialManager.ChristmasTreeEventUnlocked.BEFORE_BEGIN;
		TutorialManager.artifactOverhaul = TutorialManager.ArtifactOverhaul.BEFORE_BEGIN;
		TutorialManager.timeCounter = 0f;
		TutorialManager.fightBossButtonTimer = 0f;
		TutorialManager.skillScreenTimer = 0f;
		TutorialManager.firstPeriod = 0f;
		TutorialManager.hubTabPeriod = 0f;
		TutorialManager.modeTabPeriod = 0f;
		TutorialManager.artifactsTabPeriod = 0f;
		TutorialManager.shopTabPeriod = 0f;
		TutorialManager.prestigePeriod = 0f;
		TutorialManager.skillScreenPeriod = 0f;
		TutorialManager.fightBossButtonPeriod = 0f;
		TutorialManager.gearScreenPeriod = 0f;
		TutorialManager.gearGlobalReminderPeriod = 0f;
		TutorialManager.runeScreenPeriod = 0f;
		TutorialManager.ringPrestigeReminderPeriod = 0f;
		TutorialManager.heroPrestigeReminderPeriod = 0f;
		TutorialManager.mythicalArtifactsTabPeriod = 0f;
		TutorialManager.trinketShopPeriod = 0f;
		TutorialManager.trinketHeroTabPeriod = 0f;
		TutorialManager.firstFrameOfFirst = true;
		TutorialManager.missionIndex = -1;
		TutorialManager.setText = true;
		foreach (TutorialMission tutorialMission in TutorialMission.List)
		{
			tutorialMission.Reset();
		}
	}

	public static void Done()
	{
		TutorialManager.first = TutorialManager.First.FIN;
		TutorialManager.hubTab = TutorialManager.HubTab.FIN;
		TutorialManager.modeTab = TutorialManager.ModeTab.FIN;
		TutorialManager.artifactsTab = TutorialManager.ArtifactsTab.FIN;
		TutorialManager.shopTab = TutorialManager.ShopTab.FIN;
		TutorialManager.prestige = TutorialManager.Prestige.FIN;
		TutorialManager.skillScreen = TutorialManager.SkillScreen.FIN;
		TutorialManager.fightBossButton = TutorialManager.FightBossButton.FIN;
		TutorialManager.gearScreen = TutorialManager.GearScreen.FIN;
		TutorialManager.runeScreen = TutorialManager.RuneScreen.FIN;
		TutorialManager.ringPrestigeReminder = TutorialManager.RingPrestigeReminder.FIN;
		TutorialManager.heroPrestigeReminder = TutorialManager.HeroPrestigeReminder.FIN;
		TutorialManager.mythicalArtifactsTab = TutorialManager.MythicalArtifactsTab.FIN;
		if (!Cheats.doNotFinishTrinketTutorials)
		{
			TutorialManager.trinketShop = TutorialManager.TrinketShop.FIN;
			TutorialManager.trinketHeroTab = TutorialManager.TrinketHeroTab.FIN;
		}
		TutorialManager.mineUnlock = TutorialManager.MineUnlock.FIN;
		TutorialManager.dailyUnlock = TutorialManager.DailyUnlock.FIN;
		TutorialManager.dailyComplete = TutorialManager.DailyComplete.FIN;
		if (!Cheats.doNotFinishRiftTutorials)
		{
			TutorialManager.riftsUnlock = TutorialManager.RiftsUnlock.FIN;
			TutorialManager.riftEffects = TutorialManager.RiftEffects.FIN;
			TutorialManager.firstCharm = TutorialManager.FirstCharm.FIN;
			TutorialManager.charmHub = TutorialManager.CharmHub.FIN;
			TutorialManager.firstCharmPack = TutorialManager.FirstCharmPack.FIN;
			TutorialManager.charmLevelUp = TutorialManager.CharmLevelUp.FIN;
			TutorialManager.aeonDust = TutorialManager.AeonDust.FIN;
			TutorialManager.repeatRifts = TutorialManager.RepeatRifts.FIN;
			TutorialManager.allRiftsFinished = TutorialManager.AllRiftsFinished.FIN;
			TutorialManager.cursedGates = TutorialManager.CursedGates.FIN;
		}
		TutorialManager.flashOffersUnlocked = TutorialManager.FlashOffersUnlocked.FIN;
		TutorialManager.missionsFinished = TutorialManager.MissionsFinished.FIN;
		if (!Cheats.doNotFinishTrinketSmithingTutorials)
		{
			TutorialManager.trinketSmithingUnlocked = TutorialManager.TrinketSmithingUnlocked.FIN;
			TutorialManager.trinketRecycleUnlocked = TutorialManager.TrinketRecycleUnlocked.FIN;
		}
		if (!Cheats.doNotFinishChristmasTreeEventTutorials)
		{
			TutorialManager.christmasTreeEventUnlocked = TutorialManager.ChristmasTreeEventUnlocked.FIN;
		}
		TutorialManager.artifactOverhaul = TutorialManager.ArtifactOverhaul.FIN;
		TutorialManager.missionIndex = TutorialMission.List.Length;
	}

	public static bool IsPaused()
	{
		return TutorialManager.first == TutorialManager.First.WELCOME || TutorialManager.first == TutorialManager.First.RING_OFFER || TutorialManager.first == TutorialManager.First.RING_CLAIMED || TutorialManager.first == TutorialManager.First.RING_UPGRADE || TutorialManager.first == TutorialManager.First.RING_UPGRADE_DONE || TutorialManager.first == TutorialManager.First.ULTIMATE_SELECT || (TutorialManager.firstCharm != TutorialManager.FirstCharm.BEFORE_BEGIN && TutorialManager.firstCharm != TutorialManager.FirstCharm.FIN && ((TutorialManager.uiManager.state == UiState.NONE && TutorialManager.sim.GetActiveWorld().activeChallenge.state == Challenge.State.ACTION) || TutorialManager.uiManager.state == UiState.CHARM_SELECTING || (TutorialManager.firstCharm != TutorialManager.FirstCharm.SHOW_COLLECTION && TutorialManager.uiManager.state == UiState.RIFT_RUN_CHARMS)));
	}

	public static bool CanTotemFire()
	{
		return !TutorialManager.IsPaused() && TutorialManager.first != TutorialManager.First.HEROES_TAB_UNLOCK && TutorialManager.first != TutorialManager.First.HERO_AVAILABLE && TutorialManager.first != TutorialManager.First.HERO_BUY_BUTTON && TutorialManager.first != TutorialManager.First.HERO_UPGRADE_AVAILABLE && TutorialManager.first != TutorialManager.First.HERO_UPGRADE_TAB && (TutorialManager.hubTab == TutorialManager.HubTab.BEFORE_BEGIN || TutorialManager.hubTab == TutorialManager.HubTab.FIN) && (TutorialManager.modeTab == TutorialManager.ModeTab.BEFORE_BEGIN || TutorialManager.modeTab == TutorialManager.ModeTab.FIN) && (TutorialManager.artifactsTab == TutorialManager.ArtifactsTab.BEFORE_BEGIN || TutorialManager.artifactsTab == TutorialManager.ArtifactsTab.FIN) && (TutorialManager.shopTab == TutorialManager.ShopTab.BEFORE_BEGIN || TutorialManager.shopTab == TutorialManager.ShopTab.FIN) && (TutorialManager.skillScreen == TutorialManager.SkillScreen.BEFORE_BEGIN || TutorialManager.skillScreen == TutorialManager.SkillScreen.FIN) && (TutorialManager.prestige == TutorialManager.Prestige.BEFORE_BEGIN || TutorialManager.prestige == TutorialManager.Prestige.FIN) && TutorialManager.fightBossButton != TutorialManager.FightBossButton.SHOW_BUTTON && (TutorialManager.gearScreen == TutorialManager.GearScreen.BEFORE_BEGIN || TutorialManager.gearScreen == TutorialManager.GearScreen.FIN) && (TutorialManager.runeScreen == TutorialManager.RuneScreen.BEFORE_BEGIN || TutorialManager.runeScreen == TutorialManager.RuneScreen.FIN) && TutorialManager.ringPrestigeReminder != TutorialManager.RingPrestigeReminder.UNLOCKED && TutorialManager.trinketShop != TutorialManager.TrinketShop.UNLOCKED && TutorialManager.trinketHeroTab != TutorialManager.TrinketHeroTab.UNLOCKED && (TutorialManager.mythicalArtifactsTab == TutorialManager.MythicalArtifactsTab.BEFORE_BEGIN || TutorialManager.mythicalArtifactsTab == TutorialManager.MythicalArtifactsTab.FIN);
	}

	public static bool ShouldShowTopUi()
	{
		return TutorialManager.first > TutorialManager.First.RING_CLAIMED;
	}

	public static bool ShouldShowRingUi()
	{
		return TutorialManager.first > TutorialManager.First.RING_OFFER;
	}

	public static bool ShouldShowNewHeroButton()
	{
		return TutorialManager.first >= TutorialManager.First.HERO_AVAILABLE;
	}

	public static bool ShouldShowWaveName()
	{
		return TutorialManager.first >= TutorialManager.First.FIGHT_HERO_BOSS_WAIT;
	}

	public static bool ShouldShowStageBar()
	{
		return TutorialManager.first >= TutorialManager.First.FIGHT_HERO_2;
	}

	public static bool ShouldShowSkillButtons()
	{
		return TutorialManager.first >= TutorialManager.First.ULTIMATE_SELECT;
	}

	public static bool ShouldShowBossButtons()
	{
		return TutorialManager.first >= TutorialManager.First.FIGHT_HERO_2;
	}

	public static bool IsHubTabUnlocked()
	{
		return TutorialManager.first == TutorialManager.First.FIN;
	}

	public static bool IsModeTabUnlocked()
	{
		return TutorialManager.modeTab > TutorialManager.ModeTab.BEFORE_BEGIN;
	}

	public static bool IsHeroesTabUnlocked()
	{
		return TutorialManager.first >= TutorialManager.First.HEROES_TAB_UNLOCK;
	}

	public static bool IsArtifactsTabUnlocked()
	{
		return TutorialManager.artifactsTab > TutorialManager.ArtifactsTab.BEFORE_BEGIN;
	}

	public static bool IsShopTabUnlocked()
	{
		return TutorialManager.shopTab > TutorialManager.ShopTab.BEFORE_BEGIN;
	}

	public static bool ShouldEnemiesSpawnWithoutWaiting()
	{
		return TutorialManager.first == TutorialManager.First.FIGHT_RING;
	}

	public static bool ShouldIncrementTotWave()
	{
		return TutorialManager.first >= TutorialManager.First.FIGHT_HERO;
	}

	public static bool CanPlaceFirstArtifactsButton()
	{
		return TutorialManager.artifactsTab == TutorialManager.ArtifactsTab.FIN;
	}

	public static bool ShouldLootpackRewardGearForFirstBoughtHero()
	{
		return TutorialManager.shopTab != TutorialManager.ShopTab.FIN;
	}

	public static bool IsCharmHubUnlocked()
	{
		return TutorialManager.charmHub > TutorialManager.CharmHub.BEFORE_BEGIN;
	}

	public static bool IsAeonDustUnlocked()
	{
		return TutorialManager.aeonDust > TutorialManager.AeonDust.BEFORE_BEGIN || TutorialManager.sim.riftQuestDustCollected > 0.0;
	}

	public static bool AreFlashOffersUnlocked()
	{
		return TutorialManager.flashOffersUnlocked > TutorialManager.FlashOffersUnlocked.BEFORE_BEGIN;
	}

	public static bool IsMissionCompleted()
	{
		return TutorialManager.missionIndex < 0 || TutorialManager.missionIndex >= TutorialMission.List.Length || TutorialMission.List[TutorialManager.missionIndex].IsComplete;
	}

	public static bool IsThereTutorialCurrently()
	{
		return TutorialManager.first != TutorialManager.First.FIN || (TutorialManager.hubTab != TutorialManager.HubTab.BEFORE_BEGIN && TutorialManager.hubTab != TutorialManager.HubTab.FIN) || (TutorialManager.modeTab != TutorialManager.ModeTab.BEFORE_BEGIN && TutorialManager.modeTab != TutorialManager.ModeTab.FIN) || (TutorialManager.artifactsTab != TutorialManager.ArtifactsTab.BEFORE_BEGIN && TutorialManager.artifactsTab != TutorialManager.ArtifactsTab.FIN) || (TutorialManager.shopTab != TutorialManager.ShopTab.BEFORE_BEGIN && TutorialManager.shopTab != TutorialManager.ShopTab.FIN) || (TutorialManager.prestige != TutorialManager.Prestige.BEFORE_BEGIN && TutorialManager.prestige != TutorialManager.Prestige.FIN) || (TutorialManager.skillScreen != TutorialManager.SkillScreen.BEFORE_BEGIN && TutorialManager.skillScreen != TutorialManager.SkillScreen.FIN) || (TutorialManager.fightBossButton != TutorialManager.FightBossButton.BEFORE_BEGIN && TutorialManager.fightBossButton != TutorialManager.FightBossButton.FIN) || (TutorialManager.gearScreen != TutorialManager.GearScreen.BEFORE_BEGIN && TutorialManager.gearScreen != TutorialManager.GearScreen.FIN) || (TutorialManager.runeScreen != TutorialManager.RuneScreen.BEFORE_BEGIN && TutorialManager.runeScreen != TutorialManager.RuneScreen.FIN) || TutorialManager.ringPrestigeReminder == TutorialManager.RingPrestigeReminder.UNLOCKED || TutorialManager.heroPrestigeReminder == TutorialManager.HeroPrestigeReminder.UNLOCKED || (TutorialManager.mythicalArtifactsTab != TutorialManager.MythicalArtifactsTab.BEFORE_BEGIN && TutorialManager.mythicalArtifactsTab != TutorialManager.MythicalArtifactsTab.FIN) || TutorialManager.trinketShop == TutorialManager.TrinketShop.UNLOCKED || (TutorialManager.trinketHeroTab != TutorialManager.TrinketHeroTab.BEFORE_BEGIN && TutorialManager.trinketHeroTab != TutorialManager.TrinketHeroTab.FIN) || TutorialManager.mineUnlock == TutorialManager.MineUnlock.UNLOCKED || TutorialManager.dailyUnlock == TutorialManager.DailyUnlock.UNLOCKED || TutorialManager.dailyComplete == TutorialManager.DailyComplete.UNLOCKED || (TutorialManager.riftsUnlock != TutorialManager.RiftsUnlock.BEFORE_BEGIN && TutorialManager.riftsUnlock != TutorialManager.RiftsUnlock.FIN) || (TutorialManager.riftEffects != TutorialManager.RiftEffects.BEFORE_BEGIN && TutorialManager.riftEffects != TutorialManager.RiftEffects.FIN) || (TutorialManager.firstCharm != TutorialManager.FirstCharm.BEFORE_BEGIN && TutorialManager.firstCharm != TutorialManager.FirstCharm.FIN) || (TutorialManager.charmHub != TutorialManager.CharmHub.BEFORE_BEGIN && TutorialManager.charmHub != TutorialManager.CharmHub.FIN && (TutorialManager.charmHub == TutorialManager.CharmHub.OPEN_COLLECTION || (TutorialManager.charmHub != TutorialManager.CharmHub.OPEN_COLLECTION && TutorialManager.uiManager.state == UiState.HUB_CHARMS))) || (TutorialManager.firstCharmPack != TutorialManager.FirstCharmPack.BEFORE_BEGIN && TutorialManager.firstCharmPack != TutorialManager.FirstCharmPack.FIN && (TutorialManager.uiManager.state == UiState.HUB || TutorialManager.uiManager.state == UiState.HUB_SHOP || TutorialManager.uiManager.state == UiState.SHOP_CHARM_PACK_SELECT || TutorialManager.uiManager.state == UiState.SHOP_CHARM_PACK_OPENING)) || (TutorialManager.charmLevelUp != TutorialManager.CharmLevelUp.BEFORE_BEGIN && TutorialManager.charmLevelUp != TutorialManager.CharmLevelUp.FIN && (TutorialManager.uiManager.state == UiState.HUB || TutorialManager.uiManager.state == UiState.HUB_CHARMS || TutorialManager.uiManager.state == UiState.CHARM_INFO_POPUP)) || (TutorialManager.aeonDust != TutorialManager.AeonDust.BEFORE_BEGIN && TutorialManager.aeonDust != TutorialManager.AeonDust.FIN) || (TutorialManager.repeatRifts != TutorialManager.RepeatRifts.BEFORE_BEGIN && TutorialManager.repeatRifts != TutorialManager.RepeatRifts.FIN) || (TutorialManager.allRiftsFinished != TutorialManager.AllRiftsFinished.BEFORE_BEGIN && TutorialManager.allRiftsFinished != TutorialManager.AllRiftsFinished.FIN) || (TutorialManager.flashOffersUnlocked != TutorialManager.FlashOffersUnlocked.BEFORE_BEGIN && TutorialManager.flashOffersUnlocked != TutorialManager.FlashOffersUnlocked.FIN) || (TutorialManager.cursedGates != TutorialManager.CursedGates.BEFORE_BEGIN && TutorialManager.cursedGates != TutorialManager.CursedGates.FIN) || (TutorialManager.missionsFinished != TutorialManager.MissionsFinished.BEFORE_BEGIN && TutorialManager.missionsFinished != TutorialManager.MissionsFinished.FIN) || (TutorialManager.trinketSmithingUnlocked != TutorialManager.TrinketSmithingUnlocked.BEFORE_BEGIN && TutorialManager.trinketSmithingUnlocked != TutorialManager.TrinketSmithingUnlocked.FIN) || (TutorialManager.trinketRecycleUnlocked != TutorialManager.TrinketRecycleUnlocked.BEFORE_BEGIN && TutorialManager.trinketRecycleUnlocked != TutorialManager.TrinketRecycleUnlocked.FIN) || (TutorialManager.christmasTreeEventUnlocked != TutorialManager.ChristmasTreeEventUnlocked.BEFORE_BEGIN && TutorialManager.christmasTreeEventUnlocked != TutorialManager.ChristmasTreeEventUnlocked.FIN) || (TutorialManager.artifactOverhaul != TutorialManager.ArtifactOverhaul.BEFORE_BEGIN && TutorialManager.artifactOverhaul != TutorialManager.ArtifactOverhaul.FIN);
	}

	public static void RingUpgraded()
	{
		if (TutorialManager.first == TutorialManager.First.RING_UPGRADE)
		{
			TutorialManager.NextFirstState();
			UiManager.sounds.Add(new SoundEventUiVoiceSimple(SoundArchieve.inst.voGreenManFirstRingUpgradeDone, "GREEN_MAN", 1f));
		}
	}

	public static void HeroUpgraded()
	{
		if (TutorialManager.first == TutorialManager.First.HERO_UPGRADE_TAB)
		{
			TutorialManager.NextFirstState();
		}
	}

	public static void SkillUsed()
	{
		if (TutorialManager.first == TutorialManager.First.ULTIMATE_SELECT)
		{
			TutorialManager.NextFirstState();
		}
	}

	public static void UnlockCollected()
	{
		if (TutorialManager.modeTab == TutorialManager.ModeTab.BEFORE_BEGIN)
		{
			TutorialManager.NextModeTabState();
		}
	}

	public static void UnlockedGameMode()
	{
		if (TutorialManager.hubTab == TutorialManager.HubTab.BEFORE_BEGIN)
		{
			TutorialManager.NextHubTabState();
		}
	}

	public static void Prestiged()
	{
		if (TutorialManager.hubTab == TutorialManager.HubTab.BEFORE_BEGIN)
		{
			TutorialManager.NextHubTabState();
		}
		if (TutorialManager.prestige != TutorialManager.Prestige.FIN)
		{
			TutorialManager.prestige = TutorialManager.Prestige.FIN;
			TutorialManager.OnTutorialStep(TutorialManager.prestige);
		}
	}

	public static void NotYetPrestige()
	{
		if (TutorialManager.prestige != TutorialManager.Prestige.FIN && TutorialManager.prestige != TutorialManager.Prestige.BEFORE_BEGIN)
		{
			TutorialManager.prestige = TutorialManager.Prestige.FIN;
			TutorialManager.OnTutorialStep(TutorialManager.prestige);
		}
	}

	public static void PressedCraftArtifact()
	{
		if (TutorialManager.artifactsTab == TutorialManager.ArtifactsTab.IN_TAB)
		{
			TutorialManager.NextArtifactsTabState();
		}
	}

	public static void HaveCredits()
	{
		if (TutorialManager.shopTab == TutorialManager.ShopTab.BEFORE_BEGIN)
		{
			TutorialManager.NextShopTabState();
		}
	}

	public static void MineUnlocked()
	{
		if (TutorialManager.mineUnlock == TutorialManager.MineUnlock.BEFORE_BEGIN && TutorialManager.sim.CanCollectMine(TutorialManager.sim.mineScrap))
		{
			TutorialManager.mineUnlock = TutorialManager.MineUnlock.UNLOCKED;
		}
	}

	public static void DailyCompleted()
	{
		if (TutorialManager.dailyComplete == TutorialManager.DailyComplete.BEFORE_BEGIN && TutorialManager.sim.hasDailies && TutorialManager.sim.CanCollectDailyQuest())
		{
			TutorialManager.dailyComplete = TutorialManager.DailyComplete.UNLOCKED;
		}
	}

	public static void DailyUnlocked()
	{
		if (TutorialManager.dailyUnlock == TutorialManager.DailyUnlock.BEFORE_BEGIN && TutorialManager.sim.hasDailies && TutorialManager.sim.dailyQuest != null)
		{
			UnityEngine.Debug.Log("DAILY UNLOCKED");
			TutorialManager.dailyUnlock = TutorialManager.DailyUnlock.UNLOCKED;
		}
	}

	public static void CanCraftArtifact()
	{
		if (TutorialManager.artifactsTab == TutorialManager.ArtifactsTab.BEFORE_BEGIN)
		{
			TutorialManager.NextArtifactsTabState();
		}
	}

	public static void CanPrestige()
	{
		if (TutorialManager.prestige == TutorialManager.Prestige.BEFORE_BEGIN)
		{
			TutorialManager.NextPrestigeState();
		}
	}

	public static void OpenedLootpack(ShopPack pack)
	{
		if (pack is ShopPackTrinket)
		{
			TutorialManager.trinketShop = TutorialManager.TrinketShop.FIN;
		}
		else if (TutorialManager.shopTab == TutorialManager.ShopTab.IN_TAB)
		{
			TutorialManager.NextShopTabState();
		}
	}

	public static void OnClickedTrinketPack()
	{
		TutorialManager.trinketShop = TutorialManager.TrinketShop.FIN;
	}

	public static void CanUpgradeSkill()
	{
		if (TutorialManager.skillScreen == TutorialManager.SkillScreen.BEFORE_BEGIN)
		{
			TutorialManager.NextSkillScreenState();
		}
	}

	public static void UpgradedSkill()
	{
		if (TutorialManager.skillScreen != TutorialManager.SkillScreen.FIN)
		{
			TutorialManager.skillScreen = TutorialManager.SkillScreen.FIN;
			TutorialManager.OnTutorialStep(TutorialManager.skillScreen);
		}
	}

	public static void PressedFightBossButton()
	{
		if (TutorialManager.fightBossButton != TutorialManager.FightBossButton.FIN)
		{
			TutorialManager.fightBossButton = TutorialManager.FightBossButton.FIN;
		}
	}

	public static void CanPressFightBossButton()
	{
		if (TutorialManager.fightBossButton == TutorialManager.FightBossButton.BEFORE_BEGIN)
		{
			TutorialManager.fightBossButtonTimer = 0f;
			TutorialManager.fightBossButton = TutorialManager.FightBossButton.WAIT;
		}
		else if (TutorialManager.fightBossButton == TutorialManager.FightBossButton.WAIT && TutorialManager.fightBossButtonTimer > 60f)
		{
			TutorialManager.fightBossButtonTimer = 0f;
			TutorialManager.fightBossButton = TutorialManager.FightBossButton.SHOW_BUTTON;
		}
	}

	public static void CanUpgradeGear()
	{
		if (TutorialManager.gearScreen == TutorialManager.GearScreen.BEFORE_BEGIN)
		{
			TutorialManager.gearScreen = TutorialManager.GearScreen.UNLOCKED;
		}
	}

	public static void EvolvedHero()
	{
		TutorialManager.gearScreen = TutorialManager.GearScreen.FIN;
	}

	public static void UpgradedGear()
	{
		if (TutorialManager.gearScreen != TutorialManager.GearScreen.FIN)
		{
			TutorialManager.gearScreen = TutorialManager.GearScreen.FIN;
		}
	}

	public static void HasRune()
	{
		if (TutorialManager.runeScreen == TutorialManager.RuneScreen.BEFORE_BEGIN)
		{
			TutorialManager.runeScreen = TutorialManager.RuneScreen.UNLOCKED;
		}
	}

	public static void EquippedRune()
	{
		if (TutorialManager.runeScreen != TutorialManager.RuneScreen.FIN)
		{
			TutorialManager.runeScreen = TutorialManager.RuneScreen.FIN;
		}
	}

	public static void ReadyForRingPrestigeReminder()
	{
		if (TutorialManager.ringPrestigeReminder == TutorialManager.RingPrestigeReminder.BEFORE_BEGIN)
		{
			TutorialManager.ringPrestigeReminder = TutorialManager.RingPrestigeReminder.UNLOCKED;
		}
	}

	public static void ReadyForHeroPrestigeReminder()
	{
		if (TutorialManager.heroPrestigeReminder == TutorialManager.HeroPrestigeReminder.BEFORE_BEGIN)
		{
			TutorialManager.heroPrestigeReminder = TutorialManager.HeroPrestigeReminder.UNLOCKED;
		}
	}

	public static void UnlockedMythicalArtifactsScreen()
	{
		if (TutorialManager.mythicalArtifactsTab == TutorialManager.MythicalArtifactsTab.BEFORE_BEGIN)
		{
			TutorialManager.mythicalArtifactsTab = TutorialManager.MythicalArtifactsTab.UNLOCKED;
		}
	}

	public static void HasTrinketSlot()
	{
		if (TutorialManager.trinketShop == TutorialManager.TrinketShop.BEFORE_BEGIN)
		{
			TutorialManager.trinketShop = TutorialManager.TrinketShop.UNLOCKED;
		}
	}

	public static void HasTrinketAndFirstHeroThatCanEquipIt()
	{
		if (TutorialManager.trinketHeroTab == TutorialManager.TrinketHeroTab.BEFORE_BEGIN)
		{
			TutorialManager.trinketHeroTab = TutorialManager.TrinketHeroTab.UNLOCKED;
		}
	}

	public static void OnRiftsUnlocked()
	{
		if (TutorialManager.riftsUnlock == TutorialManager.RiftsUnlock.BEFORE_BEGIN)
		{
			UiManager.sounds.Add(new SoundEventUiVoiceSimple(SoundArchieve.inst.voWiseSnakeUnlockRift, "WISE_SNAKE", 1f));
			TutorialManager.NextRiftsUnlockedState();
			TutorialManager.uiManager.panelHub.buttonGameModeRift.forceLockState = true;
			TutorialManager.uiManager.panelHub.buttonGameModeRift.Reset();
		}
	}

	public static void OnFirstRift()
	{
		TutorialManager.NextRiftEffectsState();
	}

	public static void OnGameModeRiftSelected()
	{
		if (TutorialManager.riftEffects == TutorialManager.RiftEffects.BEFORE_BEGIN)
		{
			TutorialManager.NextRiftEffectsState();
		}
	}

	public static void OnCharmSelectionAvailable()
	{
		if (TutorialManager.firstCharm == TutorialManager.FirstCharm.BEFORE_BEGIN)
		{
			if (TutorialManager.uiManager != null)
			{
				TutorialManager.uiManager.state = UiState.NONE;
			}
			TutorialManager.NextFirstCharmState();
		}
	}

	public static void CharmCollectionReady()
	{
		if (TutorialManager.charmHub == TutorialManager.CharmHub.BEFORE_BEGIN)
		{
			TutorialManager.NextCharmHubState();
		}
	}

	public static void CharmsPackAvailable()
	{
		if (TutorialManager.firstCharmPack == TutorialManager.FirstCharmPack.BEFORE_BEGIN)
		{
			TutorialManager.NextFirstCharmPackState();
		}
	}

	public static void CharmUpgradeAvailable()
	{
		if (TutorialManager.charmLevelUp == TutorialManager.CharmLevelUp.BEFORE_BEGIN)
		{
			TutorialManager.NextCharmLevelUpState();
		}
	}

	public static void AeonDustUnlocked()
	{
		if (TutorialManager.aeonDust == TutorialManager.AeonDust.BEFORE_BEGIN)
		{
			TutorialManager.NextAeonDustState();
		}
	}

	public static void OnRiftSelectionReady()
	{
		if (TutorialManager.repeatRifts == TutorialManager.RepeatRifts.BEFORE_BEGIN)
		{
			TutorialManager.NextRepeatRiftState();
		}
	}

	public static void OnAllRiftsCompleted()
	{
		if (TutorialManager.allRiftsFinished == TutorialManager.AllRiftsFinished.BEFORE_BEGIN)
		{
			TutorialManager.NextAllRiftsFinishedState();
		}
	}

	public static void OnFlashOffersUnlocked()
	{
		if (TutorialManager.flashOffersUnlocked == TutorialManager.FlashOffersUnlocked.BEFORE_BEGIN)
		{
			TutorialManager.NextFlashOffersUnlockedState();
		}
	}

	public static void OnCursedGatesUnlocked()
	{
		if (TutorialManager.cursedGates == TutorialManager.CursedGates.BEFORE_BEGIN)
		{
			TutorialManager.NextCursedGatesState();
		}
	}

	public static void OnTutorialMissionsFinished()
	{
		if (TutorialManager.missionsFinished == TutorialManager.MissionsFinished.BEFORE_BEGIN)
		{
			TutorialManager.NextMissionsFinishedState();
		}
	}

	public static void OnTrinketSmithinUnlocked()
	{
		if (TutorialManager.trinketSmithingUnlocked == TutorialManager.TrinketSmithingUnlocked.BEFORE_BEGIN)
		{
			TutorialManager.NextTrinketSmithingUnlockedState();
		}
	}

	public static void OnTrinketRecycleButtonAppeared()
	{
		if (TutorialManager.trinketRecycleUnlocked == TutorialManager.TrinketRecycleUnlocked.BEFORE_BEGIN)
		{
			TutorialManager.NextTrinketRecycleUnlockedState();
		}
	}

	public static void OnChristmasTreeEventUnlocked()
	{
		if (TutorialManager.christmasTreeEventUnlocked == TutorialManager.ChristmasTreeEventUnlocked.BEFORE_BEGIN)
		{
			TutorialManager.NextChristmasTreeEventUnlockedState();
		}
	}

	public static void OnArtifactOverhaulTriggered()
	{
		if (TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.BEFORE_BEGIN)
		{
			TutorialManager.NextArtifactOverhaulState();
		}
	}

	public static void SetTutorial(PanelTutorial panelTutorial, Taps taps, World activeWorld, UiManager ui, float dt)
	{
		if (TutorialManager.timeCounter > 1000f)
		{
			TutorialManager.timeCounter %= 1000f;
		}
		TutorialManager.timeCounter += dt;
		TutorialManager.pt = panelTutorial;
		TutorialManager.uiManager = ui;
		if (TutorialManager.first != TutorialManager.First.FIN)
		{
			if (TutorialManager.first == TutorialManager.First.WELCOME || TutorialManager.first == TutorialManager.First.RING_CLAIMED)
			{
				if (TutorialManager.firstFrameOfFirst)
				{
					TutorialManager.firstFrameOfFirst = false;
					UiManager.sounds.Add(new SoundEventUiVoiceSimple(SoundArchieve.inst.voGreenManFirstWelcome, "GREEN_MAN", 1f));
				}
				TutorialManager.firstPeriod += dt;
				if (TutorialManager.timeCounter >= 1f && taps != null && !taps.HasNoNew())
				{
					TutorialManager.NextFirstState();
					if (TutorialManager.first == TutorialManager.First.RING_OFFER)
					{
						UiManager.sounds.Add(new SoundEventUiVoiceSimple(SoundArchieve.inst.voGreenManFirstRingOffer, "GREEN_MAN", 1f));
					}
				}
			}
			else if (TutorialManager.first == TutorialManager.First.FIGHT_RING)
			{
				TutorialManager.firstPeriod += dt;
				if (activeWorld.CanAffordTotemUpgrade())
				{
					TutorialManager.NextFirstState();
					UiManager.sounds.Add(new SoundEventUiVoiceSimple(SoundArchieve.inst.voGreenManFirstHeroesTabUnlock, "GREEN_MAN", 1f));
				}
			}
			else if (TutorialManager.first == TutorialManager.First.HEROES_TAB_UNLOCK)
			{
				TutorialManager.firstPeriod += dt;
				if (TutorialManager.uiManager.state == UiState.HEROES)
				{
					TutorialManager.NextFirstState();
					UiManager.sounds.Add(new SoundEventUiVoiceSimple(SoundArchieve.inst.voGreenManFirstRingUpgrade, "GREEN_MAN", 1f));
				}
				if (!activeWorld.CanAffordTotemUpgrade())
				{
					TutorialManager.first = TutorialManager.First.FIGHT_RING;
				}
			}
			else if (TutorialManager.first == TutorialManager.First.RING_UPGRADE)
			{
				TutorialManager.firstPeriod += dt;
				if (TutorialManager.uiManager.state == UiState.NONE)
				{
					TutorialManager.first = TutorialManager.First.HEROES_TAB_UNLOCK;
					UiManager.sounds.Add(new SoundEventCancelBy("GREEN_MAN"));
				}
			}
			else if (TutorialManager.first == TutorialManager.First.RING_UPGRADE_DONE)
			{
				TutorialManager.firstPeriod += dt;
				if (TutorialManager.uiManager.state == UiState.NONE)
				{
					TutorialManager.NextFirstState();
				}
			}
			else if (TutorialManager.first == TutorialManager.First.FIGHT_RING_2)
			{
				TutorialManager.firstPeriod += dt;
				if (activeWorld.CanAffordNewHero())
				{
					TutorialManager.NextFirstState();
				}
			}
			else if (TutorialManager.first == TutorialManager.First.HERO_AVAILABLE)
			{
				TutorialManager.firstPeriod += dt;
				if (!activeWorld.CanAffordNewHero())
				{
					TutorialManager.first = TutorialManager.First.FIGHT_RING_2;
					UiManager.sounds.Add(new SoundEventCancelBy("GREEN_MAN"));
				}
				else if (TutorialManager.uiManager.state == UiState.HEROES)
				{
					TutorialManager.NextFirstState();
				}
			}
			else if (TutorialManager.first == TutorialManager.First.HERO_BUY_BUTTON)
			{
				TutorialManager.firstPeriod += dt;
				if (activeWorld.heroes.Count > 0)
				{
					TutorialManager.NextFirstState();
				}
				else if (!activeWorld.CanAffordNewHero())
				{
					TutorialManager.first = TutorialManager.First.FIGHT_RING_2;
					UiManager.sounds.Add(new SoundEventCancelBy("GREEN_MAN"));
				}
				else if (TutorialManager.uiManager.state == UiState.NONE)
				{
					TutorialManager.first = TutorialManager.First.HERO_AVAILABLE;
					UiManager.sounds.Add(new SoundEventCancelBy("GREEN_MAN"));
				}
			}
			else if (TutorialManager.first == TutorialManager.First.FIGHT_HERO)
			{
				TutorialManager.firstPeriod += dt;
				if (ChallengeStandard.IsBossWave(activeWorld.activeChallenge.GetTotWave()))
				{
					TutorialManager.NextFirstState();
				}
			}
			else if (TutorialManager.first == TutorialManager.First.FIGHT_HERO_BOSS_WAIT)
			{
				TutorialManager.firstPeriod += dt;
				if (TutorialManager.timeCounter >= 1f && activeWorld.heroes[0].HasMainSkillAvailable())
				{
					TutorialManager.NextFirstState();
				}
			}
			else if (TutorialManager.first == TutorialManager.First.ULTIMATE_SELECT)
			{
				TutorialManager.firstPeriod += dt;
			}
			else if (TutorialManager.first == TutorialManager.First.FIGHT_HERO_BOSS_DIE)
			{
				TutorialManager.firstPeriod += dt;
				if (!ChallengeStandard.IsBossWave(activeWorld.activeChallenge.GetTotWave()))
				{
					TutorialManager.NextFirstState();
				}
			}
			else if (TutorialManager.first == TutorialManager.First.FIGHT_HERO_2)
			{
				TutorialManager.firstPeriod += dt;
				double upgradeCost = activeWorld.heroes[0].GetUpgradeCost(true);
				bool flag = activeWorld.gold.CanAfford(upgradeCost);
				if (TutorialManager.timeCounter > 3f && flag)
				{
					TutorialManager.NextFirstState();
				}
			}
			else if (TutorialManager.first == TutorialManager.First.HERO_UPGRADE_AVAILABLE)
			{
				TutorialManager.firstPeriod += dt;
				double upgradeCost2 = activeWorld.heroes[0].GetUpgradeCost(true);
				if (!activeWorld.gold.CanAfford(upgradeCost2))
				{
					TutorialManager.first = TutorialManager.First.FIGHT_HERO_2;
					UiManager.sounds.Add(new SoundEventCancelBy("GREEN_MAN"));
				}
				else if (TutorialManager.uiManager.state == UiState.HEROES)
				{
					TutorialManager.NextFirstState();
				}
			}
			else if (TutorialManager.first == TutorialManager.First.HERO_UPGRADE_TAB)
			{
				TutorialManager.firstPeriod += dt;
				double upgradeCost3 = activeWorld.heroes[0].GetUpgradeCost(true);
				if (!activeWorld.gold.CanAfford(upgradeCost3))
				{
					TutorialManager.first = TutorialManager.First.FIGHT_HERO_2;
					UiManager.sounds.Add(new SoundEventCancelBy("GREEN_MAN"));
				}
				else if (TutorialManager.uiManager.state == UiState.NONE)
				{
					TutorialManager.first = TutorialManager.First.HERO_AVAILABLE;
					UiManager.sounds.Add(new SoundEventCancelBy("GREEN_MAN"));
				}
			}
		}
		if (TutorialManager.modeTab != TutorialManager.ModeTab.FIN && TutorialManager.modeTab != TutorialManager.ModeTab.BEFORE_BEGIN)
		{
			if (TutorialManager.modeTab == TutorialManager.ModeTab.UNLOCKED)
			{
				TutorialManager.modeTabPeriod += dt;
				if (TutorialManager.uiManager.state == UiState.MODE)
				{
					TutorialManager.NextModeTabState();
				}
			}
			else if (TutorialManager.modeTab == TutorialManager.ModeTab.IN_TAB)
			{
				TutorialManager.modeTabPeriod += dt;
				if (TutorialManager.pressedOkay || TutorialManager.uiManager.state != UiState.MODE)
				{
					TutorialManager.NextModeTabState();
				}
			}
		}
		if (TutorialManager.shopTab != TutorialManager.ShopTab.FIN && TutorialManager.shopTab != TutorialManager.ShopTab.BEFORE_BEGIN)
		{
			if (TutorialManager.shopTab == TutorialManager.ShopTab.UNLOCKED)
			{
				TutorialManager.shopTabPeriod += dt;
				if (TutorialManager.uiManager.state == UiState.SHOP)
				{
					TutorialManager.NextShopTabState();
					TutorialManager.waitOneFrame = true;
				}
			}
			else if (TutorialManager.shopTab == TutorialManager.ShopTab.IN_TAB)
			{
				TutorialManager.shopTabPeriod += dt;
			}
			else if (TutorialManager.shopTab == TutorialManager.ShopTab.LOOTPACK_OPENED)
			{
				TutorialManager.shopTabPeriod += dt;
				if (TutorialManager.uiManager.state == UiState.SHOP)
				{
					TutorialManager.NextShopTabState();
				}
			}
			else if (TutorialManager.shopTab == TutorialManager.ShopTab.GO_TO_GEARS)
			{
				TutorialManager.shopTabPeriod += dt;
				if (TutorialManager.uiManager.state == UiState.HEROES_GEAR)
				{
					TutorialManager.NextShopTabState();
				}
			}
		}
		if (TutorialManager.artifactsTab != TutorialManager.ArtifactsTab.FIN && TutorialManager.artifactsTab != TutorialManager.ArtifactsTab.BEFORE_BEGIN)
		{
			if (TutorialManager.artifactsTab == TutorialManager.ArtifactsTab.UNLOCKED)
			{
				TutorialManager.artifactsTabPeriod += dt;
				if (TutorialManager.uiManager.state == UiState.ARTIFACTS)
				{
					TutorialManager.NextArtifactsTabState();
				}
			}
			else if (TutorialManager.artifactsTab == TutorialManager.ArtifactsTab.IN_TAB)
			{
				TutorialManager.artifactsTabPeriod += dt;
			}
			else if (TutorialManager.artifactsTab == TutorialManager.ArtifactsTab.CRAFTING_ARTIFACT)
			{
				TutorialManager.artifactsTabPeriod += dt;
				if (TutorialManager.uiManager.state == UiState.ARTIFACTS)
				{
					TutorialManager.NextArtifactsTabState();
				}
			}
			else if (TutorialManager.artifactsTab == TutorialManager.ArtifactsTab.SELECT_ARTIFACT)
			{
				TutorialManager.artifactsTabPeriod += dt;
				if (TutorialManager.uiManager.panelArtifactScroller.selectedArtifactIndex > -1)
				{
					TutorialManager.NextArtifactsTabState();
				}
			}
		}
		if (TutorialManager.prestige != TutorialManager.Prestige.FIN && TutorialManager.prestige != TutorialManager.Prestige.BEFORE_BEGIN)
		{
			if (TutorialManager.prestige == TutorialManager.Prestige.UNLOCKED)
			{
				TutorialManager.prestigePeriod += dt;
				if (TutorialManager.uiManager.state == UiState.MODE)
				{
					TutorialManager.NextPrestigeState();
				}
			}
			else if (TutorialManager.prestige == TutorialManager.Prestige.IN_TAB)
			{
				TutorialManager.prestigePeriod += dt;
			}
		}
		if (TutorialManager.skillScreen != TutorialManager.SkillScreen.FIN && TutorialManager.skillScreen != TutorialManager.SkillScreen.BEFORE_BEGIN)
		{
			if (TutorialManager.skillScreen == TutorialManager.SkillScreen.UNLOCKED)
			{
				TutorialManager.skillScreenPeriod += dt;
				TutorialManager.skillScreenTimer += dt;
			}
		}
		if (TutorialManager.fightBossButton != TutorialManager.FightBossButton.FIN && TutorialManager.fightBossButton != TutorialManager.FightBossButton.BEFORE_BEGIN)
		{
			if (TutorialManager.fightBossButton == TutorialManager.FightBossButton.WAIT)
			{
				TutorialManager.fightBossButtonPeriod += dt;
				TutorialManager.fightBossButtonTimer += dt;
			}
			else if (TutorialManager.fightBossButton == TutorialManager.FightBossButton.SHOW_BUTTON)
			{
				TutorialManager.fightBossButtonPeriod += dt;
				TutorialManager.fightBossButtonTimer += dt;
				if (TutorialManager.pressedOkay && TutorialManager.fightBossButtonTimer > 1f)
				{
					TutorialManager.fightBossButtonTimer = 0f;
					TutorialManager.fightBossButton = TutorialManager.FightBossButton.WAIT;
				}
			}
		}
		if (TutorialManager.gearScreen != TutorialManager.GearScreen.FIN && TutorialManager.gearScreen != TutorialManager.GearScreen.BEFORE_BEGIN)
		{
			if (TutorialManager.gearScreen == TutorialManager.GearScreen.UNLOCKED)
			{
				TutorialManager.gearScreenPeriod += dt;
			}
		}
		if (TutorialManager.runeScreen != TutorialManager.RuneScreen.FIN && TutorialManager.runeScreen != TutorialManager.RuneScreen.BEFORE_BEGIN)
		{
			if (TutorialManager.runeScreen == TutorialManager.RuneScreen.UNLOCKED)
			{
				TutorialManager.runeScreenPeriod += dt;
			}
		}
		if (TutorialManager.ringPrestigeReminder != TutorialManager.RingPrestigeReminder.FIN && TutorialManager.ringPrestigeReminder != TutorialManager.RingPrestigeReminder.BEFORE_BEGIN)
		{
			if (TutorialManager.ringPrestigeReminder == TutorialManager.RingPrestigeReminder.UNLOCKED)
			{
				TutorialManager.ringPrestigeReminderPeriod += dt;
				if (TutorialManager.ringPrestigeReminderPeriod > 1f && TutorialManager.pressedOkay)
				{
					TutorialManager.ringPrestigeReminder = TutorialManager.RingPrestigeReminder.FIN;
				}
			}
		}
		if (TutorialManager.heroPrestigeReminder != TutorialManager.HeroPrestigeReminder.FIN && TutorialManager.heroPrestigeReminder != TutorialManager.HeroPrestigeReminder.BEFORE_BEGIN)
		{
			if (TutorialManager.heroPrestigeReminder == TutorialManager.HeroPrestigeReminder.UNLOCKED)
			{
				TutorialManager.heroPrestigeReminderPeriod += dt;
				TutorialManager.heroPrestigeReminder = TutorialManager.HeroPrestigeReminder.FIN;
			}
		}
		if (TutorialManager.mythicalArtifactsTab != TutorialManager.MythicalArtifactsTab.FIN && TutorialManager.mythicalArtifactsTab != TutorialManager.MythicalArtifactsTab.BEFORE_BEGIN)
		{
			if (TutorialManager.mythicalArtifactsTab == TutorialManager.MythicalArtifactsTab.UNLOCKED)
			{
				TutorialManager.mythicalArtifactsTabPeriod += dt;
				if (TutorialManager.pressedOkay)
				{
					TutorialManager.mythicalArtifactsTab = TutorialManager.MythicalArtifactsTab.FIN;
				}
			}
		}
		if (TutorialManager.trinketShop != TutorialManager.TrinketShop.FIN && TutorialManager.trinketShop != TutorialManager.TrinketShop.BEFORE_BEGIN)
		{
			if (TutorialManager.trinketShop == TutorialManager.TrinketShop.UNLOCKED)
			{
				TutorialManager.trinketShopPeriod += dt;
				if (TutorialManager.pressedOkay)
				{
					TutorialManager.trinketShop = TutorialManager.TrinketShop.FIN;
				}
			}
		}
		if (TutorialManager.trinketHeroTab != TutorialManager.TrinketHeroTab.FIN && TutorialManager.trinketHeroTab != TutorialManager.TrinketHeroTab.BEFORE_BEGIN)
		{
			if (TutorialManager.trinketHeroTab == TutorialManager.TrinketHeroTab.UNLOCKED)
			{
				TutorialManager.trinketHeroTabPeriod += dt;
				if (TutorialManager.uiManager.state == UiState.SELECT_TRINKET)
				{
					TutorialManager.trinketHeroTab = TutorialManager.TrinketHeroTab.EQUIP;
				}
			}
			else if (TutorialManager.trinketHeroTab == TutorialManager.TrinketHeroTab.EQUIP)
			{
				TutorialManager.trinketHeroTabPeriod += dt;
				if (TutorialManager.sim.GetActiveWorld().heroes.Count > 0 && TutorialManager.sim.GetActiveWorld().heroes[0].GetData().GetDataBase().trinket != null)
				{
					TutorialManager.trinketHeroTab = TutorialManager.TrinketHeroTab.EFFECTS;
				}
			}
			else if (TutorialManager.trinketHeroTab == TutorialManager.TrinketHeroTab.EFFECTS)
			{
				TutorialManager.trinketHeroTabPeriod += dt;
				if (TutorialManager.pressedOkay)
				{
					TutorialManager.trinketHeroTab = TutorialManager.TrinketHeroTab.FIN;
				}
			}
		}
		if (TutorialManager.mineUnlock == TutorialManager.MineUnlock.UNLOCKED && TutorialManager.pressedOkay)
		{
			TutorialManager.mineUnlock = TutorialManager.MineUnlock.FIN;
		}
		if (TutorialManager.dailyUnlock == TutorialManager.DailyUnlock.UNLOCKED && TutorialManager.pressedOkay)
		{
			TutorialManager.dailyUnlock = TutorialManager.DailyUnlock.FIN;
		}
		if (TutorialManager.dailyComplete == TutorialManager.DailyComplete.UNLOCKED && !TutorialManager.sim.CanCollectDailyQuest())
		{
			TutorialManager.dailyComplete = TutorialManager.DailyComplete.FIN;
		}
		if (TutorialManager.riftsUnlock == TutorialManager.RiftsUnlock.UNLOCKED && TutorialManager.pressedOkay)
		{
			TutorialManager.uiManager.panelHub.buttonGameModeRift.forceLockState = false;
			TutorialManager.uiManager.panelHub.buttonGameModeRift.StartUnlockAnim(0.6f);
			TutorialManager.NextRiftsUnlockedState();
		}
		if (TutorialManager.riftEffects == TutorialManager.RiftEffects.IN_TAB && TutorialManager.uiManager.state == UiState.RIFT_EFFECTS_INFO)
		{
			TutorialManager.NextRiftEffectsState();
		}
		if (TutorialManager.firstCharm != TutorialManager.FirstCharm.BEFORE_BEGIN && TutorialManager.firstCharm != TutorialManager.FirstCharm.FIN && TutorialManager.uiManager.state == UiState.HUB)
		{
			TutorialManager.firstCharm = TutorialManager.FirstCharm.BEFORE_BEGIN;
		}
		if (TutorialManager.firstCharm == TutorialManager.FirstCharm.WARNING)
		{
			if (TutorialManager.uiManager.state == UiState.CHARM_SELECTING)
			{
				TutorialManager.NextFirstCharmState();
			}
		}
		else if (TutorialManager.firstCharm == TutorialManager.FirstCharm.WAIT_SELECT)
		{
			if (TutorialManager.uiManager.state == UiState.NONE)
			{
				TutorialManager.NextFirstCharmState();
			}
		}
		else if (TutorialManager.firstCharm == TutorialManager.FirstCharm.SHOW_COLLECTION)
		{
			if (TutorialManager.uiManager.state == UiState.RIFT_RUN_CHARMS)
			{
				TutorialManager.NextFirstCharmState();
			}
		}
		else if (TutorialManager.firstCharm == TutorialManager.FirstCharm.EXPLAIN_EFFECTS)
		{
			if (TutorialManager.pressedOkay)
			{
				TutorialManager.NextFirstCharmState();
			}
		}
		else if (TutorialManager.firstCharm == TutorialManager.FirstCharm.EXPLAIN_TRIGGER && TutorialManager.pressedOkay)
		{
			TutorialManager.NextFirstCharmState();
		}
		if (TutorialManager.charmHub == TutorialManager.CharmHub.OPEN_COLLECTION)
		{
			if (TutorialManager.uiManager.state == UiState.HUB_CHARMS)
			{
				TutorialManager.NextCharmHubState();
				TutorialManager.uiManager.panelHub.tabBarLayout.enabled = true;
			}
			else if (TutorialManager.uiManager.state != UiState.HUB)
			{
				TutorialManager.charmHub = TutorialManager.CharmHub.BEFORE_BEGIN;
			}
		}
		else if (TutorialManager.charmHub == TutorialManager.CharmHub.MESSAGE_1)
		{
			if (TutorialManager.pressedOkay)
			{
				TutorialManager.NextCharmHubState();
			}
		}
		else if (TutorialManager.charmHub == TutorialManager.CharmHub.MESSAGE_2 && TutorialManager.pressedOkay)
		{
			TutorialManager.NextCharmHubState();
		}
		if (TutorialManager.firstCharmPack == TutorialManager.FirstCharmPack.OPEN_SHOP)
		{
			if (TutorialManager.uiManager.state == UiState.HUB_SHOP)
			{
				TutorialManager.NextFirstCharmPackState();
				TutorialManager.uiManager.panelHub.tabBarLayout.enabled = true;
				TutorialManager.waitTime = 0.2f;
			}
			else if (TutorialManager.uiManager.state != UiState.HUB)
			{
				TutorialManager.firstCharmPack = TutorialManager.FirstCharmPack.BEFORE_BEGIN;
				TutorialManager.uiManager.panelHub.tabBarLayout.enabled = true;
			}
		}
		else if (TutorialManager.firstCharmPack == TutorialManager.FirstCharmPack.WAIT_TO_OPEN)
		{
			TutorialManager.waitTime -= dt;
			if (TutorialManager.waitTime <= 0f)
			{
				TutorialManager.NextFirstCharmPackState();
			}
		}
		else if (TutorialManager.firstCharmPack == TutorialManager.FirstCharmPack.OPEN_PACK && TutorialManager.uiManager.state == UiState.SHOP_CHARM_PACK_OPENING)
		{
			TutorialManager.NextFirstCharmPackState();
		}
		if (TutorialManager.charmLevelUp == TutorialManager.CharmLevelUp.EXIT_SHOP)
		{
			if (TutorialManager.uiManager.state == UiState.HUB)
			{
				TutorialManager.NextCharmLevelUpState();
			}
		}
		else if (TutorialManager.charmLevelUp == TutorialManager.CharmLevelUp.OPEN_COLLECTION)
		{
			if (TutorialManager.uiManager.state == UiState.HUB_CHARMS)
			{
				TutorialManager.uiManager.panelHub.tabBarLayout.enabled = true;
				TutorialManager.NextCharmLevelUpState();
			}
		}
		else if (TutorialManager.charmLevelUp == TutorialManager.CharmLevelUp.SELECT_CHARM)
		{
			if (TutorialManager.uiManager.state == UiState.CHARM_INFO_POPUP)
			{
				TutorialManager.NextCharmLevelUpState();
				TutorialManager.uiManager.panelHubCharms.scrollRect.enabled = true;
			}
		}
		else if (TutorialManager.charmLevelUp == TutorialManager.CharmLevelUp.EXPLAIN_LEVELUP && (TutorialManager.pressedOkay || TutorialManager.uiManager.state == UiState.HUB_CHARMS))
		{
			TutorialManager.NextCharmLevelUpState();
		}
		if (TutorialManager.aeonDust == TutorialManager.AeonDust.COLLECT && !TutorialManager.sim.IsRiftQuestCompleted())
		{
			TutorialManager.NextAeonDustState();
		}
		if (TutorialManager.repeatRifts == TutorialManager.RepeatRifts.UNLOCK)
		{
			if (TutorialManager.uiManager.state == UiState.RIFT_SELECT_POPUP)
			{
				TutorialManager.NextRepeatRiftState();
			}
		}
		else if (TutorialManager.repeatRifts == TutorialManager.RepeatRifts.SELECT)
		{
			if (TutorialManager.pressedOkay || TutorialManager.uiManager.state == UiState.HUB_MODE_SETUP)
			{
				TutorialManager.NextRepeatRiftState();
			}
		}
		else if (TutorialManager.repeatRifts == TutorialManager.RepeatRifts.WAIT_CLOSE_SELECT)
		{
			if (TutorialManager.uiManager.state == UiState.HUB_MODE_SETUP)
			{
				TutorialManager.NextRepeatRiftState();
			}
		}
		else if (TutorialManager.repeatRifts == TutorialManager.RepeatRifts.FINAL_TEXT && (TutorialManager.pressedOkay || TutorialManager.uiManager.state != UiState.HUB_MODE_SETUP))
		{
			TutorialManager.NextRepeatRiftState();
		}
		if (TutorialManager.allRiftsFinished == TutorialManager.AllRiftsFinished.MESSAGE && TutorialManager.pressedOkay)
		{
			TutorialManager.NextAllRiftsFinishedState();
		}
		if (TutorialManager.flashOffersUnlocked == TutorialManager.FlashOffersUnlocked.OPEN_SHOP)
		{
			if (TutorialManager.uiManager.state == UiState.SHOP || TutorialManager.uiManager.state == UiState.HUB_SHOP)
			{
				TutorialManager.NextFlashOffersUnlockedState();
				TutorialManager.uiManager.panelShop.isLookingAtOffers = true;
				TutorialManager.waitOneFrame = true;
				TutorialManager.uiManager.panelShop.forceUpdatePackOffer = true;
				TutorialManager.uiManager.panelHub.tabBarLayout.enabled = true;
			}
		}
		else if (TutorialManager.flashOffersUnlocked == TutorialManager.FlashOffersUnlocked.SHOW_MESSAGE && (TutorialManager.pressedOkay || (TutorialManager.uiManager.state != UiState.SHOP && TutorialManager.uiManager.state != UiState.HUB_SHOP)))
		{
			TutorialManager.uiManager.panelShop.focusOnFlashOffers = TutorialManager.pressedOkay;
			TutorialManager.NextFlashOffersUnlockedState();
		}
		if (TutorialManager.cursedGates == TutorialManager.CursedGates.OPEN_SELECT_GATE_PANEL)
		{
			if (TutorialManager.uiManager.state == UiState.RIFT_SELECT_POPUP)
			{
				TutorialManager.NextCursedGatesState();
			}
		}
		else if (TutorialManager.cursedGates == TutorialManager.CursedGates.OPEN_CURSES_TAB && TutorialManager.uiManager.panelRiftSelect.isCurseMode)
		{
			TutorialManager.NextCursedGatesState();
		}
		if (TutorialManager.missionsFinished == TutorialManager.MissionsFinished.MESSAGE && TutorialManager.pressedOkay)
		{
			TutorialManager.NextMissionsFinishedState();
		}
		if (TutorialManager.trinketSmithingUnlocked == TutorialManager.TrinketSmithingUnlocked.GO_TO_HUB && TutorialManager.uiManager.state == UiState.HUB)
		{
			TutorialManager.NextTrinketSmithingUnlockedState();
			TutorialManager.uiManager.panelHub.tabBarLayout.enabled = true;
		}
		else if (TutorialManager.trinketSmithingUnlocked == TutorialManager.TrinketSmithingUnlocked.OPEN_TRINKETS_SCREEN && TutorialManager.uiManager.state == UiState.HUB_DATABASE_TRINKETS)
		{
			TutorialManager.NextTrinketSmithingUnlockedState();
		}
		else if (TutorialManager.trinketSmithingUnlocked == TutorialManager.TrinketSmithingUnlocked.SELECT_SMITHING_TAB && TutorialManager.uiManager.panelHubTrinkets.isSmithingTab)
		{
			TutorialManager.NextTrinketSmithingUnlockedState();
			TutorialManager.uiManager.panelHubTrinkets.tabsLayout.enabled = true;
		}
		if (TutorialManager.trinketRecycleUnlocked == TutorialManager.TrinketRecycleUnlocked.MESSAGE && TutorialManager.uiManager.state != UiState.TRINKET_INFO_POPUP)
		{
			TutorialManager.NextTrinketRecycleUnlockedState();
		}
		if (TutorialManager.christmasTreeEventUnlocked == TutorialManager.ChristmasTreeEventUnlocked.OPEN_POPUP && TutorialManager.uiManager.state == UiState.CHRISTMAS_PANEL)
		{
			TutorialManager.NextChristmasTreeEventUnlockedState();
			TutorialManager.timeCounter = 0f;
		}
		else if (TutorialManager.christmasTreeEventUnlocked == TutorialManager.ChristmasTreeEventUnlocked.WAIT_ANIM && TutorialManager.timeCounter >= 2.5f)
		{
			TutorialManager.NextChristmasTreeEventUnlockedState();
		}
		else if (TutorialManager.christmasTreeEventUnlocked == TutorialManager.ChristmasTreeEventUnlocked.MESSAGE_1 && TutorialManager.uiManager.state != UiState.CHRISTMAS_PANEL)
		{
			TutorialManager.NextChristmasTreeEventUnlockedState();
		}
		else if (TutorialManager.christmasTreeEventUnlocked == TutorialManager.ChristmasTreeEventUnlocked.MESSAGE_2 && TutorialManager.pressedOkay)
		{
			TutorialManager.NextChristmasTreeEventUnlockedState();
		}
		else if (TutorialManager.christmasTreeEventUnlocked == TutorialManager.ChristmasTreeEventUnlocked.OPEN_SHOP_TAB && !TutorialManager.uiManager.panelChristmasOffer.showingTreeTab)
		{
			TutorialManager.NextChristmasTreeEventUnlockedState();
		}
		if (TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.WELCOME && TutorialManager.pressedOkay)
		{
			TutorialManager.NextArtifactOverhaulState();
			TutorialManager.uiManager.state = UiState.ARTIFACT_OVERHAUL;
		}
		else if (TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.WAIT_GIVE_NEW_ARTIFACTS && TutorialManager.uiManager.state == UiState.HUB_ARTIFACTS)
		{
			TutorialManager.NextArtifactOverhaulState();
		}
		else if (TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.WAIT_POSITIONING_ARTIFACTS && TutorialManager.uiManager.panelArtifactScroller.artifactAppearAnimAfterConversion != null && !TutorialManager.uiManager.panelArtifactScroller.artifactAppearAnimAfterConversion.isPlaying)
		{
			TutorialManager.NextArtifactOverhaulState();
		}
		else if (TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.GIVE_MYTHSTONES && TutorialManager.pressedOkay)
		{
			double halfConversionReward = OldArtifactsConverter.GetHalfConversionReward(TutorialManager.sim);
			DropPosition dropPos = new DropPosition
			{
				startPos = TutorialManager.uiManager.panelArtifactScroller.boilerParent.position,
				endPos = TutorialManager.uiManager.panelArtifactScroller.boilerParent.position + new Vector3(-0.03f, 0.01f, 0f),
				invPos = TutorialManager.uiManager.panelHubartifacts.menuShowCurrencyMythstone.GetCurrencyTransform().position,
				targetToScaleOnReach = TutorialManager.uiManager.panelHubartifacts.menuShowCurrencyMythstone.GetCurrencyTransform()
			};
			TutorialManager.sim.GetActiveWorld().RainCurrencyOnUi(TutorialManager.uiManager.state, CurrencyType.MYTHSTONE, halfConversionReward, dropPos, 30, 0f, 0f, 1f, null, 0f);
			UiManager.AddUiSound(SoundArchieve.inst.uiPurchaseGemPack);
			TutorialManager.NextArtifactOverhaulState();
		}
		else if (TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.WAIT_DROPS && TutorialManager.timeCounter >= 2f)
		{
			TutorialManager.NextArtifactOverhaulState();
		}
		else if (TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.SELECT_ARTIFACT && TutorialManager.uiManager.state == UiState.ARTIFACT_SELECTED_POPUP)
		{
			TutorialManager.NextArtifactOverhaulState();
		}
		TutorialManager.ptActive = true;
		TutorialManager.ptOkayActive = false;
		TutorialManager.pt.imageRingBg.gameObject.SetActive(false);
		TutorialManager.uiManager.panelRing.canvasGroup.alpha = 1f;
		TutorialManager.uiManager.panelRing.imageRing.gameObject.SetActive(true);
		TutorialManager.pt.imageArrow.gameObject.SetActive(false);
		TutorialManager.pt.imageBlackCurtain.gameObject.SetActive(false);
		TutorialManager.pt.buttonOkay.text.text = LM.Get("UI_OKAY");
		int i = 0;
		int count = TutorialManager.maskedElements.Count;
		while (i < count)
		{
			if (TutorialManager.maskedElements[i].parent != TutorialManager.parents[i])
			{
				TutorialManager.maskedElements[i].SetParent(TutorialManager.parents[i]);
				TutorialManager.maskedElements[i].SetSiblingIndex(TutorialManager.siblingIndexList[i]);
			}
			i++;
		}
		if (!TutorialManager.IsThereTutorialCurrently())
		{
			TutorialManager.maskedElements.Clear();
			TutorialManager.parents.Clear();
			TutorialManager.localPositions.Clear();
			TutorialManager.siblingIndexList.Clear();
		}
		TutorialManager.pt.imageCharacter.gameObject.SetActive(true);
		TutorialManager.pt.messageParent.gameObject.SetActive(true);
		TutorialManager.pt.text.gameObject.SetActive(true);
		TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharGreenMan;
		TutorialManager.spriteTutoCharPosition = TutorialManager.DefaultCharacterPosition;
		if (TutorialManager.uiManager.loadingTransition.IsTransitioning() || !TutorialManager.IsThereTutorialCurrently() || !TutorialManager.UpdateCheckActiveOutput())
		{
			TutorialManager.ptActive = false;
			TutorialManager.uiManager.panelHub.tabBarLayout.enabled = true;
			TutorialManager.uiManager.panelHubTrinkets.tabsLayout.enabled = true;
			TutorialManager.uiManager.panelHubCharms.scrollRect.enabled = true;
		}
		if (TutorialManager.pt.imageCharacter.sprite != TutorialManager.spriteTutoChar && TutorialManager.ptActive)
		{
			TutorialManager.pt.imageCharacter.sprite = TutorialManager.spriteTutoChar;
		}
		if (TutorialManager.pt.imageCharacter.rectTransform.anchoredPosition != TutorialManager.spriteTutoCharPosition && TutorialManager.ptActive)
		{
			TutorialManager.pt.imageCharacter.rectTransform.anchoredPosition = TutorialManager.spriteTutoCharPosition;
		}
		TutorialManager.pt.OpenClose(TutorialManager.ptActive);
		TutorialManager.pt.OpenCloseOkay(TutorialManager.ptOkayActive);
		TutorialManager.pressedOkay = false;
		if (TutorialManager.missionIndex < 0 || TutorialManager.missionIndex >= TutorialMission.List.Length || TutorialManager.uiManager.IsInHubMenus() || TutorialManager.sim.GetActiveWorld().gameMode != GameMode.STANDARD)
		{
			TutorialManager.pt.HideMission(false, true);
		}
		else if (TutorialManager.uiManager.state == UiState.NONE)
		{
			TutorialManager.pt.UpdateMission(TutorialMission.List[TutorialManager.missionIndex], TutorialManager.missionIndex, TutorialMission.List.Length, TutorialManager.sim.GetActiveWorld().isRainingGlory);
			if (TutorialMission.List[TutorialManager.missionIndex].Claimed)
			{
				TutorialManager.pt.HideMission(false, true);
			}
		}
		if (TutorialManager.missionIndex == -1)
		{
			if (TutorialManager.first > TutorialManager.First.RING_CLAIMED)
			{
				TutorialManager.missionIndex = 0;
			}
		}
		else if (TutorialManager.missionIndex < TutorialMission.List.Length)
		{
			if (TutorialMission.List[TutorialManager.missionIndex].Claimed)
			{
				TutorialManager.missionIndex++;
			}
			else
			{
				TutorialMission.List[TutorialManager.missionIndex].Update(TutorialManager.sim, taps);
			}
		}
		else
		{
			TutorialManager.OnTutorialMissionsFinished();
		}
	}

	public static void OnApplicationPaused()
	{
		if (TutorialManager.pt != null)
		{
			TutorialManager.pt.CancelMissionAnimations();
			TutorialManager.pt.HideMission(true, false);
			TutorialManager.pt.waitTimeToShow = 2f;
		}
	}

	private static void SetText(PanelTutorial pt, string text)
	{
		TutorialManager.SetText(pt, text, TutorialManager.DefaultTextPosition);
	}

	private static void SetText(PanelTutorial pt, string text, Vector2 position)
	{
		if (TutorialManager.timeCounter > (float)text.Length / 60f)
		{
			pt.text.text = text;
		}
		else
		{
			int num = Mathf.RoundToInt(TutorialManager.timeCounter * 60f);
			pt.text.text = text.Substring(0, num) + "<color=#00000000>" + text.Substring(num) + "</color>";
		}
		pt.text.rectTransform.anchoredPosition = position;
	}

	private static void SetArrow(PanelTutorial pt, Transform button, bool isUp, bool isBlackCurtain, float yOffset = 0f, bool horizontalAxis = false, float xOffset = 0f)
	{
		TutorialManager.SetArrow(pt, new Transform[]
		{
			button
		}, isUp, isBlackCurtain, yOffset, horizontalAxis, xOffset);
	}

	private static void SetArrow(PanelTutorial pt, Transform[] buttons, bool isUp, bool isBlackCurtain, float yOffset = 0f, bool horizontalAxis = false, float xOffset = 0f)
	{
		if (isBlackCurtain)
		{
			TutorialManager.MaskElements(buttons);
		}
		pt.imageArrow.gameObject.SetActive(true);
		float num = (!isUp) ? -1f : 1f;
		float num2 = TutorialManager.timeCounter % 2f;
		float num3 = (TutorialManager.timeCounter + 0.4f) % 2f;
		if (num2 > 1f)
		{
			num2 = 2f - num2;
		}
		if (num3 > 1f)
		{
			num3 = 2f - num3;
		}
		float num4 = EaseManager.Evaluate(Ease.InOutSine, null, num2, 1f, 1f, 1f);
		float num5 = EaseManager.Evaluate(Ease.InOutCubic, null, num3, 1f, 1f, 1f);
		pt.imageArrow.rectTransform.localRotation = ((!horizontalAxis) ? TutorialManager.VerticalRotation : TutorialManager.HorizontalRotation);
		pt.imageArrow.rectTransform.localScale = new Vector3(1f + (0.5f - num5) / 5f, num - num * (0.5f - num5) / 8f, 1f);
		float d = yOffset * 0.6f + num * (0.25f + ((!isUp) ? 0f : 0.1f) + num4 * 0.1f);
		Vector3 position = buttons[0].position + ((!horizontalAxis) ? Vector3.up : Vector3.left) * d;
		if (horizontalAxis)
		{
			position.y += xOffset;
		}
		else
		{
			position.x += xOffset;
		}
		pt.imageArrow.rectTransform.position = position;
	}

	private static void MaskElement(Transform element)
	{
		TutorialManager.MaskElements(new Transform[]
		{
			element
		});
	}

	private static void MaskElements(Transform[] elements)
	{
		TutorialManager.pt.imageBlackCurtain.gameObject.SetActive(true);
		TutorialManager.pt.imageBlackCurtain.rectTransform.anchorMin = new Vector2(0f, 0f);
		TutorialManager.pt.imageBlackCurtain.rectTransform.SetBottomDelta(-150f);
		TutorialManager.pt.imageBlackCurtain.rectTransform.SetTopDelta(-150f);
		TutorialManager.pt.imageBlackCurtain.SetAlpha(0.753f);
		int i = elements.Length - 1;
		int num = 0;
		while (i >= num)
		{
			Transform transform = elements[i];
			if (!TutorialManager.maskedElements.Contains(transform))
			{
				TutorialManager.maskedElements.Add(transform);
				TutorialManager.parents.Add(transform.parent);
				TutorialManager.localPositions.Add(transform.localPosition);
				TutorialManager.siblingIndexList.Add(transform.GetSiblingIndex());
			}
			int j = TutorialManager.maskedElements.Count - 1;
			int num2 = 0;
			while (j >= num2)
			{
				if (transform == TutorialManager.maskedElements[j])
				{
					Vector3 position = TutorialManager.parents[j].TransformPoint(TutorialManager.localPositions[j]);
					transform.SetParent(TutorialManager.pt.maskedElementsParent);
					transform.transform.SetSiblingIndex(TutorialManager.pt.imageBlackCurtain.transform.GetSiblingIndex() + 1);
					transform.position = position;
					break;
				}
				j--;
			}
			i--;
		}
	}

	public static void NextFirstState()
	{
		if (TutorialManager.first >= TutorialManager.First.FIN)
		{
			return;
		}
		TutorialManager.first++;
		if (TutorialManager.first == TutorialManager.First.FIN)
		{
			AdjustTracker.TrackTutorialCompleted();
			
		}
		if (TutorialManager.first >= TutorialManager.First.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.first);
		}
	}

	public static void NextHubTabState()
	{
		if (TutorialManager.hubTab >= TutorialManager.HubTab.FIN)
		{
			return;
		}
		TutorialManager.hubTab++;
		if (TutorialManager.hubTab >= TutorialManager.HubTab.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.hubTab);
		}
	}

	public static void NextModeTabState()
	{
		if (TutorialManager.modeTab >= TutorialManager.ModeTab.FIN)
		{
			return;
		}
		TutorialManager.modeTab++;
		if (TutorialManager.modeTab >= TutorialManager.ModeTab.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.modeTab);
		}
	}

	public static void NextArtifactsTabState()
	{
		if (TutorialManager.artifactsTab >= TutorialManager.ArtifactsTab.FIN)
		{
			return;
		}
		TutorialManager.artifactsTab++;
		if (TutorialManager.artifactsTab >= TutorialManager.ArtifactsTab.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.artifactsTab);
		}
	}

	public static void NextShopTabState()
	{
		if (TutorialManager.shopTab >= TutorialManager.ShopTab.FIN)
		{
			return;
		}
		TutorialManager.shopTab++;
		if (TutorialManager.shopTab >= TutorialManager.ShopTab.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.shopTab);
		}
	}

	public static void NextPrestigeState()
	{
		if (TutorialManager.prestige >= TutorialManager.Prestige.FIN)
		{
			return;
		}
		TutorialManager.prestige++;
		if (TutorialManager.prestige >= TutorialManager.Prestige.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.prestige);
		}
	}

	public static void NextSkillScreenState()
	{
		if (TutorialManager.skillScreen >= TutorialManager.SkillScreen.FIN)
		{
			return;
		}
		TutorialManager.skillScreen++;
		if (TutorialManager.skillScreen >= TutorialManager.SkillScreen.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.skillScreen);
		}
	}

	public static void NextRiftsUnlockedState()
	{
		if (TutorialManager.riftsUnlock >= TutorialManager.RiftsUnlock.FIN)
		{
			return;
		}
		TutorialManager.riftsUnlock++;
		if (TutorialManager.riftsUnlock >= TutorialManager.RiftsUnlock.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.riftsUnlock);
		}
	}

	public static void NextRiftEffectsState()
	{
		if (TutorialManager.riftEffects >= TutorialManager.RiftEffects.FIN)
		{
			return;
		}
		TutorialManager.riftEffects++;
		if (TutorialManager.riftEffects >= TutorialManager.RiftEffects.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.riftEffects);
		}
	}

	public static void NextFirstCharmState()
	{
		if (TutorialManager.firstCharm >= TutorialManager.FirstCharm.FIN)
		{
			return;
		}
		TutorialManager.firstCharm++;
		if (TutorialManager.firstCharm >= TutorialManager.FirstCharm.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.firstCharm);
		}
	}

	public static void NextCharmHubState()
	{
		if (TutorialManager.charmHub >= TutorialManager.CharmHub.FIN)
		{
			return;
		}
		TutorialManager.charmHub++;
		if (TutorialManager.charmHub >= TutorialManager.CharmHub.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.charmHub);
		}
	}

	public static void NextFirstCharmPackState()
	{
		if (TutorialManager.firstCharmPack >= TutorialManager.FirstCharmPack.FIN)
		{
			return;
		}
		TutorialManager.firstCharmPack++;
		if (TutorialManager.firstCharmPack >= TutorialManager.FirstCharmPack.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.firstCharmPack);
		}
	}

	public static void NextCharmLevelUpState()
	{
		if (TutorialManager.charmLevelUp >= TutorialManager.CharmLevelUp.FIN)
		{
			return;
		}
		TutorialManager.charmLevelUp++;
		if (TutorialManager.charmLevelUp >= TutorialManager.CharmLevelUp.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.charmLevelUp);
		}
	}

	public static void NextAeonDustState()
	{
		if (TutorialManager.aeonDust >= TutorialManager.AeonDust.FIN)
		{
			return;
		}
		TutorialManager.aeonDust++;
		if (TutorialManager.aeonDust >= TutorialManager.AeonDust.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.aeonDust);
		}
	}

	public static void NextRepeatRiftState()
	{
		if (TutorialManager.repeatRifts >= TutorialManager.RepeatRifts.FIN)
		{
			return;
		}
		TutorialManager.repeatRifts++;
		if (TutorialManager.repeatRifts >= TutorialManager.RepeatRifts.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.repeatRifts);
		}
	}

	public static void NextAllRiftsFinishedState()
	{
		if (TutorialManager.allRiftsFinished >= TutorialManager.AllRiftsFinished.FIN)
		{
			return;
		}
		TutorialManager.allRiftsFinished++;
		if (TutorialManager.allRiftsFinished >= TutorialManager.AllRiftsFinished.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.allRiftsFinished);
		}
	}

	public static void NextFlashOffersUnlockedState()
	{
		if (TutorialManager.flashOffersUnlocked >= TutorialManager.FlashOffersUnlocked.FIN)
		{
			return;
		}
		TutorialManager.flashOffersUnlocked++;
		if (TutorialManager.flashOffersUnlocked >= TutorialManager.FlashOffersUnlocked.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.flashOffersUnlocked);
		}
	}

	public static void NextCursedGatesState()
	{
		if (TutorialManager.cursedGates >= TutorialManager.CursedGates.FIN)
		{
			return;
		}
		TutorialManager.cursedGates++;
		if (TutorialManager.cursedGates >= TutorialManager.CursedGates.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.cursedGates);
		}
	}

	public static void NextMissionsFinishedState()
	{
		if (TutorialManager.missionsFinished >= TutorialManager.MissionsFinished.FIN)
		{
			return;
		}
		TutorialManager.missionsFinished++;
		if (TutorialManager.missionsFinished >= TutorialManager.MissionsFinished.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.missionsFinished);
		}
	}

	public static void NextTrinketSmithingUnlockedState()
	{
		if (TutorialManager.trinketSmithingUnlocked >= TutorialManager.TrinketSmithingUnlocked.FIN)
		{
			return;
		}
		TutorialManager.trinketSmithingUnlocked++;
		if (TutorialManager.trinketSmithingUnlocked >= TutorialManager.TrinketSmithingUnlocked.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.trinketSmithingUnlocked);
		}
	}

	public static void NextTrinketRecycleUnlockedState()
	{
		if (TutorialManager.trinketRecycleUnlocked >= TutorialManager.TrinketRecycleUnlocked.FIN)
		{
			return;
		}
		TutorialManager.trinketRecycleUnlocked++;
		if (TutorialManager.trinketRecycleUnlocked >= TutorialManager.TrinketRecycleUnlocked.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.trinketRecycleUnlocked);
		}
	}

	public static void NextChristmasTreeEventUnlockedState()
	{
		if (TutorialManager.christmasTreeEventUnlocked >= TutorialManager.ChristmasTreeEventUnlocked.FIN)
		{
			return;
		}
		TutorialManager.christmasTreeEventUnlocked++;
		if (TutorialManager.christmasTreeEventUnlocked >= TutorialManager.ChristmasTreeEventUnlocked.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.christmasTreeEventUnlocked);
		}
	}

	public static void NextArtifactOverhaulState()
	{
		if (TutorialManager.artifactOverhaul >= TutorialManager.ArtifactOverhaul.FIN)
		{
			return;
		}
		if (TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.WAIT_DROPS || TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.WAIT_POSITIONING_ARTIFACTS)
		{
			TutorialManager.uiManager.inputBlocker.gameObject.SetActive(false);
		}
		TutorialManager.artifactOverhaul++;
		if (TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.WAIT_DROPS || TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.WAIT_POSITIONING_ARTIFACTS)
		{
			TutorialManager.uiManager.inputBlocker.gameObject.SetActive(true);
			TutorialManager.timeCounter = 0f;
		}
		if (TutorialManager.artifactOverhaul >= TutorialManager.ArtifactOverhaul.FIN)
		{
			TutorialManager.OnTutorialStep(TutorialManager.artifactOverhaul);
		}
	}

	public static void OnEnemyTookDamage(Damage damage, Unit damager, UnitHealthy enemy)
	{
		if (TutorialManager.missionIndex != -1 && TutorialManager.missionIndex < TutorialMission.List.Length)
		{
			TutorialMission.List[TutorialManager.missionIndex].OnEnemyTookDamage(damage, damager, enemy);
		}
	}

	public static void OnUltiUsed(Hero hero)
	{
		if (TutorialManager.missionIndex != -1 && TutorialManager.missionIndex < TutorialMission.List.Length)
		{
			TutorialMission.List[TutorialManager.missionIndex].OnUltiUsed(hero);
		}
	}

	public static void OnEnemyKilled(UnitHealthy enemy, Unit killer)
	{
		if (TutorialManager.missionIndex != -1 && TutorialManager.missionIndex < TutorialMission.List.Length)
		{
			TutorialMission.List[TutorialManager.missionIndex].OnEnemyKilled(enemy, killer);
		}
	}

	public static void OnGoldCollected(double amount)
	{
		if (TutorialManager.missionIndex != -1 && TutorialManager.missionIndex < TutorialMission.List.Length)
		{
			TutorialMission.List[TutorialManager.missionIndex].OnGoldCollected(amount);
		}
	}

	public static void OnPrestige()
	{
		if (TutorialManager.missionIndex != -1 && TutorialManager.missionIndex < TutorialMission.List.Length)
		{
			TutorialMission.List[TutorialManager.missionIndex].OnPrestige();
		}
	}

	public static void MissionRewardClaimed()
	{
		if (!TutorialManager.pt.missionTransitioning && !TutorialMission.List[TutorialManager.missionIndex].Claimed)
		{
			TutorialMission.List[TutorialManager.missionIndex].Claim(TutorialManager.sim);
			PlayfabManager.SendPlayerEvent(PlayfabEventId.TUTORIAL_MISSION_COMPLETED, new Dictionary<string, object>
			{
				{
					"index",
					TutorialManager.missionIndex
				}
			}, null, null, true);
		}
	}

	public static void PressedOkay()
	{
		TutorialManager.pressedOkay = true;
	}

	private static void SendPlayerEvent(string eventName, Dictionary<string, object> body, Simulator sim, bool finished)
	{
	}

	private static bool UpdateCheckActiveOutput()
	{
		return TutorialManager.UpdateAndCheckActiveFirst() || TutorialManager.UpdateAndCheckActiveModeTab() || TutorialManager.UpdateAndCheckActiveShopTab() || TutorialManager.UpdateAndCheckActiveArtifactsTab() || TutorialManager.UpdateAndCheckActivePrestige() || TutorialManager.UpdateAndCheckActiveSkillScreen() || TutorialManager.UpdateAndCheckActiveFightBossButton() || TutorialManager.UpdateAndCheckActiveGearScreen() || TutorialManager.UpdateAndCheckActiveRuneScreen() || TutorialManager.UpdateAndCheckActiveRingPrestigeReminder() || TutorialManager.UpdateAndCheckActiveHeroPrestigeReminder() || TutorialManager.UpdateAndCheckActiveMythicalArtifacts() || TutorialManager.UpdateAndCheckActiveTrinketShop() || TutorialManager.UpdateAndCheckActiveTrinketHeroTab() || TutorialManager.UpdateAndCheckActiveMineShop() || TutorialManager.UpdateAndCheckDailyUnlock() || TutorialManager.UpdateAndCheckDailyComplete() || TutorialManager.UpdateAndCheckRiftsUnlockComplete() || TutorialManager.UpdateAndCheckRiftEffectsComplete() || TutorialManager.UpdateAndCheckFirstCharmComplete() || TutorialManager.UpdateAndCheckCharmHubComplete() || TutorialManager.UpdateAndCheckFirstCharmPackComplete() || TutorialManager.UpdateAndCheckCharmLevelUpComplete() || TutorialManager.UpdateAndCheckAeonDustComplete() || TutorialManager.UpdateAndCheckRepeatRiftsComplete() || TutorialManager.UpdateAndCheckAllRiftsFinishedComplete() || TutorialManager.UpdateAndCheckFlashOffersUnlockedComplete() || TutorialManager.UpdateAndCheckCursedGatesComplete() || TutorialManager.UpdateAndCheckMissionsFinishedComplete() || TutorialManager.UpdateAndCheckTrinketSmithingUnlockedComplete() || TutorialManager.UpdateAndCheckTrinketRecycleUnlockedComplete() || TutorialManager.UpdateAndCheckChristmasTreeEventUnlockedComplete() || TutorialManager.UpdateAndCheckArtifactOverhaulComplete();
	}

	private static bool UpdateAndCheckActiveFirst()
	{
		if (TutorialManager.first == TutorialManager.First.FIN)
		{
			return false;
		}
		if (TutorialManager.first == TutorialManager.First.WELCOME)
		{
			TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_FIRST0"));
		}
		else if (TutorialManager.first == TutorialManager.First.RING_OFFER)
		{
			TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_FIRST1"));
			TutorialManager.pt.imageRingBg.gameObject.SetActive(true);
			TutorialManager.pt.imageRing.transform.localPosition = TutorialManager.pt.positionRingFirstLocal;
			TutorialManager.pt.canvasGroupRingBg.alpha = 1f;
			TutorialManager.pt.canvasGroupRingBg.interactable = true;
			TutorialManager.pt.canvasGroupRingBg.gameObject.SetActive(true);
			TutorialManager.pt.buttonRingOffer.interactable = true;
			TutorialManager.pt.canvasGroupRing.alpha = 1f;
			if (TutorialManager.timeCounter < 0.35f)
			{
				TutorialManager.pt.imageRingBg.rectTransform.anchoredPosition = new Vector2(TutorialManager.pt.imageRingBg.rectTransform.anchoredPosition.x, Easing.CircEaseInOut(TutorialManager.timeCounter, 0f, -240f, 0.35f));
			}
			else
			{
				TutorialManager.pt.imageRingBg.rectTransform.anchoredPosition = new Vector2(TutorialManager.pt.imageRingBg.rectTransform.anchoredPosition.x, -240f);
			}
		}
		else if (TutorialManager.first == TutorialManager.First.RING_CLAIMED)
		{
			TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
			TutorialManager.pt.imageRingBg.gameObject.SetActive(true);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_FIRST2"));
			TutorialManager.uiManager.panelRing.canvasGroup.alpha = 0f;
			TutorialManager.pt.canvasGroupRingBg.interactable = false;
			TutorialManager.pt.buttonRingOffer.interactable = false;
			if (TutorialManager.timeCounter < 0.25f)
			{
				TutorialManager.uiManager.panelRing.imageRing.gameObject.SetActive(false);
				TutorialManager.pt.canvasGroupRingBg.gameObject.SetActive(true);
				TutorialManager.pt.canvasGroupRingBg.alpha = 1f - TutorialManager.timeCounter / 0.25f;
				TutorialManager.pt.imageRing.transform.position = TutorialManager.pt.positionRingFirst + (TutorialManager.uiManager.panelRing.imageRing.transform.position - TutorialManager.pt.positionRingFirst) * Easing.CircEaseInOut(TutorialManager.timeCounter, 0f, 1f, 0.25f);
			}
			else
			{
				TutorialManager.pt.canvasGroupRingBg.alpha = 0f;
				TutorialManager.pt.canvasGroupRingBg.gameObject.SetActive(false);
				TutorialManager.pt.imageRing.transform.position = TutorialManager.uiManager.panelRing.imageRing.transform.position;
				TutorialManager.uiManager.panelRing.imageRing.gameObject.SetActive(true);
			}
		}
		else if (TutorialManager.first == TutorialManager.First.FIGHT_RING)
		{
			TutorialManager.uiManager.panelRing.imageRing.gameObject.SetActive(true);
			if (TutorialManager.timeCounter < 0.4f)
			{
				TutorialManager.uiManager.panelRing.canvasGroup.alpha = TutorialManager.timeCounter / 0.4f;
			}
			else
			{
				TutorialManager.uiManager.panelRing.canvasGroup.alpha = 1f;
			}
			TutorialManager.ptActive = false;
		}
		else if (TutorialManager.first == TutorialManager.First.HEROES_TAB_UNLOCK)
		{
			TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_FIRST3"));
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.tabBarButtons[2].transform, true, false, 0f, false, 0f);
		}
		else if (TutorialManager.first == TutorialManager.First.RING_UPGRADE)
		{
			if (TutorialManager.uiManager.state == UiState.HEROES)
			{
				TutorialManager.pt.Move(-100f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_FIRST4"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelHeroes.panelTotem.buttonUpgrade.transform, false, false, 0f, false, 0f);
			}
			else
			{
				TutorialManager.ptActive = false;
			}
		}
		else if (TutorialManager.first == TutorialManager.First.RING_UPGRADE_DONE)
		{
			TutorialManager.pt.Move(-200f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_FIRST5"));
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.tabBarButtons[2].transform, true, false, 0f, false, 0f);
		}
		else if (TutorialManager.first == TutorialManager.First.FIGHT_RING_2)
		{
			TutorialManager.ptActive = false;
		}
		else if (TutorialManager.first == TutorialManager.First.HERO_AVAILABLE)
		{
			TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_FIRST6"));
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.tabBarButtons[2].transform, true, false, 0f, false, 0f);
		}
		else if (TutorialManager.first == TutorialManager.First.HERO_BUY_BUTTON)
		{
			if (TutorialManager.uiManager.state == UiState.HEROES)
			{
				TutorialManager.pt.Move(-400f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_FIRST7"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelHeroes.buttonOpenNewHeroPanel.transform, false, true, 0f, false, 0.53f);
			}
			else
			{
				TutorialManager.ptActive = false;
			}
		}
		else if (TutorialManager.first == TutorialManager.First.FIGHT_HERO)
		{
			TutorialManager.ptActive = false;
		}
		else if (TutorialManager.first == TutorialManager.First.FIGHT_HERO_BOSS_WAIT)
		{
			TutorialManager.ptActive = false;
		}
		else if (TutorialManager.first == TutorialManager.First.ULTIMATE_SELECT)
		{
			TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_FIRST8"));
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.buttonSkills[0].transform, true, true, 0f, false, 0f);
		}
		else if (TutorialManager.first == TutorialManager.First.FIGHT_HERO_BOSS_DIE)
		{
			TutorialManager.ptActive = false;
		}
		else if (TutorialManager.first == TutorialManager.First.FIGHT_HERO_2)
		{
			TutorialManager.ptActive = false;
		}
		else if (TutorialManager.first == TutorialManager.First.HERO_UPGRADE_AVAILABLE)
		{
			TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_FIRST9"));
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.tabBarButtons[2].transform, true, false, 0f, false, 0f);
		}
		else
		{
			if (TutorialManager.first != TutorialManager.First.HERO_UPGRADE_TAB)
			{
				throw new NotImplementedException();
			}
			if (TutorialManager.uiManager.state == UiState.HEROES)
			{
				TutorialManager.pt.Move(-400f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_FIRST10"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelHeroes.heroPanels[0].buttonUpgrade.transform, false, true, 0f, false, 0f);
			}
			else
			{
				TutorialManager.ptActive = false;
			}
		}
		return true;
	}

	private static bool UpdateAndCheckActiveModeTab()
	{
		if (TutorialManager.modeTab == TutorialManager.ModeTab.FIN || TutorialManager.modeTab == TutorialManager.ModeTab.BEFORE_BEGIN)
		{
			return false;
		}
		if (TutorialManager.modeTab == TutorialManager.ModeTab.UNLOCKED)
		{
			TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharGreenMan;
			TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_MODE_TAB0"));
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.tabBarButtons[1].transform, true, true, 0f, false, 0f);
		}
		else
		{
			if (TutorialManager.modeTab != TutorialManager.ModeTab.IN_TAB)
			{
				throw new NotImplementedException();
			}
			TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharGreenMan;
			TutorialManager.pt.Move(-200f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_MODE_TAB1"));
			TutorialManager.ptOkayActive = true;
			TutorialManager.MaskElements(new Transform[]
			{
				TutorialManager.uiManager.panelMode.unlockWidgetParent.transform
			});
		}
		return true;
	}

	private static bool UpdateAndCheckActiveShopTab()
	{
		if (TutorialManager.shopTab == TutorialManager.ShopTab.FIN || TutorialManager.shopTab == TutorialManager.ShopTab.BEFORE_BEGIN)
		{
			return false;
		}
		if (TutorialManager.shopTab == TutorialManager.ShopTab.UNLOCKED)
		{
			TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_SHOP_TAB0"));
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.tabBarButtons[4].transform, true, true, 0f, false, 0f);
		}
		else if (TutorialManager.shopTab == TutorialManager.ShopTab.IN_TAB)
		{
			if (TutorialManager.uiManager.state == UiState.SHOP)
			{
				TutorialManager.pt.Move(-400f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_SHOP_TAB1"));
				TutorialManager.uiManager.panelShop.scrollView.verticalNormalizedPosition = 1f;
				TutorialManager.uiManager.scrollView.content.anchoredPosition = Vector2.zero;
				if (TutorialManager.waitOneFrame)
				{
					TutorialManager.pt.imageBlackCurtain.gameObject.SetActive(true);
					TutorialManager.waitOneFrame = false;
				}
				else
				{
					TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelShop.shopLootPacks[0].transform, false, true, -0.13f, false, 0f);
				}
			}
			else if (TutorialManager.uiManager.state == UiState.SHOP_LOOTPACK_SELECT)
			{
				TutorialManager.ptActive = false;
			}
		}
		else if (TutorialManager.shopTab == TutorialManager.ShopTab.LOOTPACK_OPENED)
		{
			TutorialManager.ptActive = false;
		}
		else
		{
			if (TutorialManager.shopTab != TutorialManager.ShopTab.GO_TO_GEARS)
			{
				throw new NotImplementedException();
			}
			if (TutorialManager.uiManager.state == UiState.HEROES_SKILL)
			{
				TutorialManager.pt.Move(-200f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_SHOP_TAB3"));
				TutorialManager.SetArrow(TutorialManager.pt, new Transform[]
				{
					TutorialManager.uiManager.panelGearScreen.buttonTab.transform
				}, false, true, 0f, false, 0f);
			}
			else if (TutorialManager.uiManager.state == UiState.HEROES)
			{
				TutorialManager.pt.Move(-300f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_SHOP_TAB4"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelHeroes.heroPanels[0].buttonHeroPortrait.transform, false, true, 0f, false, 0f);
			}
			else
			{
				TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_SHOP_TAB2"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.tabBarButtons[2].transform, true, true, 0f, false, 0f);
			}
		}
		return true;
	}

	private static bool UpdateAndCheckActiveArtifactsTab()
	{
		if (TutorialManager.artifactsTab == TutorialManager.ArtifactsTab.FIN || TutorialManager.artifactsTab == TutorialManager.ArtifactsTab.BEFORE_BEGIN)
		{
			return false;
		}
		if (TutorialManager.artifactsTab == TutorialManager.ArtifactsTab.CRAFTING_ARTIFACT)
		{
			TutorialManager.ptActive = false;
		}
		else if (TutorialManager.artifactsTab == TutorialManager.ArtifactsTab.UNLOCKED)
		{
			TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharAlchemist;
			TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_ARTIFACTS_TAB0"));
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.tabBarButtons[3].transform, true, true, 0f, false, 0f);
		}
		else if (TutorialManager.artifactsTab == TutorialManager.ArtifactsTab.IN_TAB)
		{
			TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharAlchemist;
			TutorialManager.pt.Move(-400f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_ARTIFACTS_TAB1"));
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelArtifactScroller.craftButton.transform, false, true, 0f, false, 0f);
		}
		else
		{
			if (TutorialManager.artifactsTab != TutorialManager.ArtifactsTab.SELECT_ARTIFACT)
			{
				throw new NotImplementedException();
			}
			TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharAlchemist;
			TutorialManager.pt.Move(200f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_ARTIFACTS_TAB2"));
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelArtifactScroller.buttonArtifacts[0].transform, false, true, 0f, false, 0f);
		}
		return true;
	}

	private static bool UpdateAndCheckActivePrestige()
	{
		if (TutorialManager.prestige == TutorialManager.Prestige.FIN || TutorialManager.prestige == TutorialManager.Prestige.BEFORE_BEGIN)
		{
			return false;
		}
		if (TutorialManager.prestige == TutorialManager.Prestige.UNLOCKED)
		{
			TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharGreenMan;
			TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_PRESTIGE0"));
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.tabBarButtons[1].transform, true, true, 0f, false, 0f);
		}
		else
		{
			if (TutorialManager.prestige != TutorialManager.Prestige.IN_TAB)
			{
				throw new NotImplementedException();
			}
			TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharGreenMan;
			if (TutorialManager.uiManager.state == UiState.MODE)
			{
				TutorialManager.pt.Move(-300f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_PRESTIGE1"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelMode.buttonPrestige.transform, false, true, 0f, false, 0f);
			}
			else if (TutorialManager.uiManager.state == UiState.MODE_PRESTIGE)
			{
				TutorialManager.ptActive = false;
			}
		}
		return true;
	}

	private static bool UpdateAndCheckActiveSkillScreen()
	{
		if (TutorialManager.skillScreen == TutorialManager.SkillScreen.FIN || TutorialManager.skillScreen == TutorialManager.SkillScreen.BEFORE_BEGIN)
		{
			return false;
		}
		if (TutorialManager.skillScreen == TutorialManager.SkillScreen.UNLOCKED)
		{
			if (TutorialManager.skillScreenTimer < 2f || TutorialManager.uiManager.IsInHubMenus())
			{
				TutorialManager.ptActive = false;
			}
			else if (TutorialManager.uiManager.state == UiState.HEROES_SKILL)
			{
				if (TutorialManager.uiManager.panelSkillsScreen.selectedBranchIndex == -2)
				{
					TutorialManager.pt.Move(450f, 64f, 620f, 0f, 400f);
					TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_SKILL_SCREEN3"));
				}
				else
				{
					TutorialManager.pt.Move(450f, 64f, 620f, 0f, 400f);
					TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_SKILL_SCREEN4"));
				}
			}
			else if (TutorialManager.uiManager.state == UiState.HEROES_GEAR)
			{
				TutorialManager.pt.Move(-200f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_SKILL_SCREEN2"));
				TutorialManager.SetArrow(TutorialManager.pt, new Transform[]
				{
					TutorialManager.uiManager.panelSkillsScreen.buttonTab.text.transform,
					TutorialManager.uiManager.panelSkillsScreen.buttonTab.transform
				}, false, true, 0f, false, 0f);
			}
			else if (TutorialManager.uiManager.state == UiState.HEROES)
			{
				TutorialManager.pt.Move(-300f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_SKILL_SCREEN1"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelHeroes.heroPanels[0].buttonHeroPortrait.transform, false, true, 0f, false, 0f);
			}
			else
			{
				TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_SKILL_SCREEN0"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.tabBarButtons[2].transform, true, true, 0f, false, 0f);
			}
			return true;
		}
		throw new NotImplementedException();
	}

	private static bool UpdateAndCheckActiveFightBossButton()
	{
		if (TutorialManager.fightBossButton == TutorialManager.FightBossButton.FIN || TutorialManager.fightBossButton == TutorialManager.FightBossButton.BEFORE_BEGIN)
		{
			return false;
		}
		if (TutorialManager.fightBossButton == TutorialManager.FightBossButton.WAIT)
		{
			TutorialManager.ptActive = false;
		}
		else
		{
			if (TutorialManager.fightBossButton != TutorialManager.FightBossButton.SHOW_BUTTON)
			{
				throw new NotImplementedException();
			}
			TutorialManager.pt.Move(-300f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_FIGHT_BOSS_BUTTON"));
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.buttonFightBoss.transform, false, false, 0f, false, 0f);
			TutorialManager.ptOkayActive = true;
		}
		return true;
	}

	private static bool UpdateAndCheckActiveGearScreen()
	{
		if (TutorialManager.gearScreen == TutorialManager.GearScreen.FIN || TutorialManager.gearScreen == TutorialManager.GearScreen.BEFORE_BEGIN)
		{
			return false;
		}
		if (TutorialManager.gearScreen == TutorialManager.GearScreen.UNLOCKED)
		{
			if (TutorialManager.uiManager.state == UiState.HEROES_GEAR)
			{
				TutorialManager.pt.Move(400f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_GEAR_SCREEN1"));
				TutorialManager.ptOkayActive = true;
				if (TutorialManager.pressedOkay)
				{
					TutorialManager.gearScreen = TutorialManager.GearScreen.FIN;
				}
			}
			else if (TutorialManager.uiManager.state == UiState.HEROES_SKILL)
			{
				TutorialManager.pt.Move(-200f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_GEAR_SCREEN2"));
				TutorialManager.SetArrow(TutorialManager.pt, new Transform[]
				{
					TutorialManager.uiManager.panelGearScreen.buttonTab.text.transform,
					TutorialManager.uiManager.panelGearScreen.buttonTab.transform
				}, false, true, 0f, false, 0f);
			}
			else if (TutorialManager.uiManager.state == UiState.HEROES)
			{
				TutorialManager.pt.Move(-300f, 64f, 620f, 0f, 400f);
				int firstEvolvableHeroIndex = TutorialManager.uiManager.GetFirstEvolvableHeroIndex();
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_GEAR_SCREEN3"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelHeroes.heroPanels[firstEvolvableHeroIndex].buttonHeroPortrait.transform, false, true, 0f, false, 0f);
			}
			else
			{
				TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_GEAR_SCREEN0"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.tabBarButtons[2].transform, true, true, 0f, false, 0f);
			}
			return true;
		}
		throw new NotImplementedException();
	}

	private static bool UpdateAndCheckActiveRuneScreen()
	{
		if (TutorialManager.runeScreen == TutorialManager.RuneScreen.FIN || TutorialManager.runeScreen == TutorialManager.RuneScreen.BEFORE_BEGIN)
		{
			return false;
		}
		if (TutorialManager.runeScreen == TutorialManager.RuneScreen.UNLOCKED)
		{
			if (TutorialManager.uiManager.state == UiState.HEROES)
			{
				TutorialManager.pt.Move(-100f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RUNE_SCREEN1"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelHeroes.panelTotem.buttonHeroPortrait.transform, false, true, 0f, false, 0f);
			}
			else if (TutorialManager.uiManager.state == UiState.HEROES_RUNES)
			{
				TutorialManager.pt.Move(-400f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RUNE_SCREEN2"));
				TutorialManager.SetArrow(TutorialManager.pt, new Transform[]
				{
					TutorialManager.uiManager.panelHeroesRunes.panelRunes[0].buttonUse.transform,
					TutorialManager.uiManager.panelHeroesRunes.panelRunes[0].buttonRemove.transform
				}, false, true, 0f, false, 0f);
			}
			else
			{
				TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RUNE_SCREEN0"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.tabBarButtons[2].transform, true, true, 0f, false, 0f);
			}
			return true;
		}
		throw new NotImplementedException();
	}

	private static bool UpdateAndCheckActiveRingPrestigeReminder()
	{
		if (TutorialManager.ringPrestigeReminder == TutorialManager.RingPrestigeReminder.FIN || TutorialManager.ringPrestigeReminder == TutorialManager.RingPrestigeReminder.BEFORE_BEGIN)
		{
			return false;
		}
		if (TutorialManager.ringPrestigeReminder == TutorialManager.RingPrestigeReminder.UNLOCKED)
		{
			TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RING_PRESTIGE_REMINDER"));
			TutorialManager.ptOkayActive = true;
			return true;
		}
		throw new NotImplementedException();
	}

	private static bool UpdateAndCheckActiveHeroPrestigeReminder()
	{
		if (TutorialManager.heroPrestigeReminder == TutorialManager.HeroPrestigeReminder.FIN || TutorialManager.heroPrestigeReminder == TutorialManager.HeroPrestigeReminder.BEFORE_BEGIN)
		{
			return false;
		}
		if (TutorialManager.heroPrestigeReminder == TutorialManager.HeroPrestigeReminder.UNLOCKED)
		{
			TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_HERO_PRESTIGE_REMINDER"));
			TutorialManager.ptOkayActive = true;
			return true;
		}
		throw new NotImplementedException();
	}

	private static bool UpdateAndCheckActiveMythicalArtifacts()
	{
		if (TutorialManager.mythicalArtifactsTab == TutorialManager.MythicalArtifactsTab.FIN || TutorialManager.mythicalArtifactsTab == TutorialManager.MythicalArtifactsTab.BEFORE_BEGIN)
		{
			return false;
		}
		if (TutorialManager.mythicalArtifactsTab == TutorialManager.MythicalArtifactsTab.UNLOCKED)
		{
			if (TutorialManager.uiManager.state == UiState.ARTIFACTS)
			{
				if (TutorialManager.uiManager.panelArtifactScroller.isLookingAtMythical)
				{
					TutorialManager.pt.Move(-250f, 64f, 620f, 0f, 400f);
					TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_MYTHICAL_ARTIFACTS_2"));
					TutorialManager.ptOkayActive = true;
				}
				else
				{
					TutorialManager.pt.Move(-200f, 64f, 620f, 0f, 400f);
					TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_MYTHICAL_ARTIFACTS_1"));
					TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelArtifactScroller.buttonTabMythical.transform, false, true, 0f, false, 0f);
				}
			}
			else
			{
				TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_MYTHICAL_ARTIFACTS_0"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.tabBarButtons[3].transform, true, false, 0f, false, 0f);
			}
			return true;
		}
		throw new NotImplementedException();
	}

	private static bool UpdateAndCheckActiveTrinketShop()
	{
		if (TutorialManager.trinketShop == TutorialManager.TrinketShop.BEFORE_BEGIN || TutorialManager.trinketShop == TutorialManager.TrinketShop.FIN)
		{
			return false;
		}
		if (TutorialManager.trinketShop == TutorialManager.TrinketShop.UNLOCKED)
		{
			if (TutorialManager.uiManager.state == UiState.SHOP)
			{
				TutorialManager.pt.Move(350f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_TRINKET_SHOP_1"));
				TutorialManager.SetArrow(TutorialManager.pt, new Transform[]
				{
					TutorialManager.uiManager.panelShop.panelTrinket.gameButton.raycastTarget.transform,
					TutorialManager.uiManager.panelShop.panelTrinket.transform
				}, false, true, -0.4f, false, 0f);
				TutorialManager.ptOkayActive = true;
			}
			else
			{
				TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_TRINKET_SHOP_0"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.tabBarButtons[4].transform, true, true, 0f, false, 0f);
			}
			return true;
		}
		throw new NotImplementedException();
	}

	private static bool UpdateAndCheckActiveTrinketHeroTab()
	{
		if (TutorialManager.trinketHeroTab == TutorialManager.TrinketHeroTab.BEFORE_BEGIN || TutorialManager.trinketHeroTab == TutorialManager.TrinketHeroTab.FIN)
		{
			return false;
		}
		if (TutorialManager.trinketHeroTab == TutorialManager.TrinketHeroTab.UNLOCKED)
		{
			if (TutorialManager.uiManager.state == UiState.HEROES_TRINKETS)
			{
				TutorialManager.pt.Move(100f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_TRINKET_HERO_TAB_3"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelTrinketsScreen.buttonEquip.transform, true, true, -0.1f, false, 0f);
			}
			else if (TutorialManager.uiManager.state == UiState.HEROES_SKILL || TutorialManager.uiManager.state == UiState.HEROES_GEAR)
			{
				TutorialManager.pt.Move(-100f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_TRINKET_HERO_TAB_2"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelTrinketsScreen.buttonTab.transform, false, true, 0f, false, 0f);
			}
			else if (TutorialManager.uiManager.state == UiState.HEROES)
			{
				TutorialManager.pt.Move(-350f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_TRINKET_HERO_TAB_1"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelHeroes.heroPanels[0].buttonHeroPortrait.transform, false, true, 0f, false, 0f);
			}
			else
			{
				TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_TRINKET_HERO_TAB_0"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.tabBarButtons[2].transform, true, true, 0f, false, 0f);
			}
		}
		else if (TutorialManager.trinketHeroTab == TutorialManager.TrinketHeroTab.EQUIP)
		{
			if (TutorialManager.uiManager.state != UiState.TRINKET_INFO_POPUP)
			{
				TutorialManager.pt.Move(-250f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_TRINKET_HERO_TAB_4"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelTrinketsScroller.trinkets[0].transform, false, true, -0.1f, false, 0f);
			}
			else
			{
				TutorialManager.pt.Move(250f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_TRINKET_HERO_TAB_5"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelTrinketInfoPopup.buttonEquip.transform, true, true, 0f, false, 0f);
			}
		}
		else
		{
			if (TutorialManager.trinketHeroTab != TutorialManager.TrinketHeroTab.EFFECTS)
			{
				throw new NotImplementedException();
			}
			if (TutorialManager.uiManager.state == UiState.HEROES_TRINKETS)
			{
				TutorialManager.pt.Move(350f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_TRINKET_HERO_TAB_7"));
				TutorialManager.MaskElements(new Transform[]
				{
					TutorialManager.uiManager.panelTrinketsScreen.panelTrinketEffects[2].transform,
					TutorialManager.uiManager.panelTrinketsScreen.panelTrinketEffects[1].transform,
					TutorialManager.uiManager.panelTrinketsScreen.panelTrinketEffects[0].transform
				});
				TutorialManager.ptOkayActive = true;
			}
			else
			{
				TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_TRINKET_HERO_TAB_6"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelSelectTrinket.buttonCloses[0].transform, false, true, 0f, false, 0f);
			}
		}
		return true;
	}

	private static bool UpdateAndCheckActiveMineShop()
	{
		if (TutorialManager.mineUnlock == TutorialManager.MineUnlock.BEFORE_BEGIN || TutorialManager.mineUnlock == TutorialManager.MineUnlock.FIN)
		{
			return false;
		}
		if (TutorialManager.mineUnlock == TutorialManager.MineUnlock.UNLOCKED)
		{
			if (TutorialManager.uiManager.state == UiState.NONE)
			{
				TutorialManager.pt.Move(-150f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_MINE_SHOP1"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.tabBarButtons[4].transform, true, true, 0f, false, 0f);
			}
			else if (TutorialManager.uiManager.state == UiState.SHOP)
			{
				TutorialManager.pt.Move(370f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_MINE_SHOP2"));
				TutorialManager.uiManager.panelShop.isLookingAtOffers = false;
				TutorialManager.uiManager.panelShop.focusOnFlashOffers = false;
				TutorialManager.uiManager.panelShop.scrollView.verticalNormalizedPosition = 0.5f;
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelShop.panelMineScrap.transform, true, true, -0.5f, false, 0f);
			}
			else if (TutorialManager.uiManager.state == UiState.SHOP_MINE)
			{
				TutorialManager.pt.Move(-200f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_MINE_SHOP3"));
				TutorialManager.ptOkayActive = true;
			}
			return true;
		}
		throw new NotImplementedException();
	}

	private static bool UpdateAndCheckDailyUnlock()
	{
		if (TutorialManager.dailyUnlock != TutorialManager.DailyUnlock.UNLOCKED)
		{
			return false;
		}
		if (TutorialManager.uiManager.state == UiState.NONE)
		{
			TutorialManager.pt.Move(-150f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_DAILY_QUEST_START_1"));
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.dailyQuestIndicatorWidget.transform, false, true, 0f, false, 0f);
		}
		else if (TutorialManager.uiManager.state == UiState.DAILY_QUEST)
		{
			TutorialManager.pt.Move(450f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_DAILY_QUEST_START_2"));
			TutorialManager.ptOkayActive = true;
		}
		return true;
	}

	private static bool UpdateAndCheckDailyComplete()
	{
		if (TutorialManager.dailyComplete != TutorialManager.DailyComplete.UNLOCKED)
		{
			return false;
		}
		if (TutorialManager.uiManager.state == UiState.NONE)
		{
			TutorialManager.pt.Move(-150f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_DAILY_QUEST_COMPLETE_1"));
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.dailyQuestIndicatorWidget.transform, false, true, 0f, false, 0f);
		}
		else if (TutorialManager.uiManager.state == UiState.DAILY_QUEST)
		{
			TutorialManager.pt.Move(200f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_DAILY_QUEST_COMPLETE_2"));
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.dailyQuestPopup.buttonCollect.transform, false, true, 0f, false, 0f);
		}
		return true;
	}

	private static bool UpdateAndCheckRiftsUnlockComplete()
	{
		if (TutorialManager.riftsUnlock == TutorialManager.RiftsUnlock.BEFORE_BEGIN || TutorialManager.riftsUnlock == TutorialManager.RiftsUnlock.FIN)
		{
			return false;
		}
		if (TutorialManager.riftsUnlock == TutorialManager.RiftsUnlock.UNLOCKED)
		{
			TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoSerpent;
			TutorialManager.spriteTutoCharPosition = TutorialManager.SpriteTutoCharRightPosition;
			TutorialManager.pt.Move(170f, -19f, 630f, -45f, 420f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RIFT_01"), TutorialManager.TextPositionWhenCharAtRight);
			TutorialManager.ptOkayActive = true;
			TutorialManager.pt.imageBlackCurtain.gameObject.SetActive(true);
		}
		return true;
	}

	private static bool UpdateAndCheckRiftEffectsComplete()
	{
		if (TutorialManager.riftEffects == TutorialManager.RiftEffects.BEFORE_BEGIN || TutorialManager.riftEffects == TutorialManager.RiftEffects.FIN)
		{
			return false;
		}
		if (TutorialManager.riftEffects == TutorialManager.RiftEffects.IN_TAB)
		{
			TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoSerpent;
			TutorialManager.spriteTutoCharPosition = TutorialManager.SpriteTutoCharRightPosition;
			TutorialManager.pt.Move(-194f, -19f, 630f, -45f, 420f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RIFT_02"), TutorialManager.TextPositionWhenCharAtRight);
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelHubModeSetup.buttonRiftInfo.transform, true, true, -0.15f, true, 0f);
		}
		return true;
	}

	private static bool UpdateAndCheckFirstCharmComplete()
	{
		if (TutorialManager.firstCharm == TutorialManager.FirstCharm.BEFORE_BEGIN || TutorialManager.firstCharm == TutorialManager.FirstCharm.FIN || TutorialManager.firstCharm == TutorialManager.FirstCharm.WAIT_SELECT)
		{
			return false;
		}
		if (TutorialManager.firstCharm == TutorialManager.FirstCharm.WARNING)
		{
			TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharGreenMan;
			TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RIFT_03"));
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.charmSelectWidget.transform, true, true, -0.1f, false, 0f);
		}
		else if (TutorialManager.firstCharm == TutorialManager.FirstCharm.SHOW_COLLECTION)
		{
			if (TutorialManager.uiManager.state == UiState.NONE && TutorialManager.sim.GetActiveWorld().activeChallenge.state == Challenge.State.ACTION)
			{
				TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharGreenMan;
				TutorialManager.pt.Move(-220f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RIFT_04"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.tabBarButtons[3].transform, true, true, 0f, false, 0f);
			}
			else
			{
				TutorialManager.ptActive = false;
			}
		}
		else if (TutorialManager.firstCharm == TutorialManager.FirstCharm.EXPLAIN_EFFECTS)
		{
			TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharGreenMan;
			TutorialManager.pt.Move(-19.5f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RIFT_05"));
			if (TutorialManager.uiManager.panelRunningCharms.runningCharmCards.Count > 0)
			{
				TutorialManager.uiManager.panelRunningCharms.runningCharmCards[0].charmCardInfo.descBackground.color = TutorialManager.uiManager.panelRunningCharms.runningCharmCards[0].charmCardInfo.background.color;
				TutorialManager.MaskElement(TutorialManager.uiManager.panelRunningCharms.runningCharmCards[0].charmCardInfo.descBackground.transform);
			}
			else
			{
				TutorialManager.pt.imageBlackCurtain.gameObject.SetActive(true);
			}
			TutorialManager.ptOkayActive = true;
		}
		else if (TutorialManager.firstCharm == TutorialManager.FirstCharm.EXPLAIN_TRIGGER)
		{
			TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharGreenMan;
			TutorialManager.pt.Move(-19.5f, 64f, 620f, 0f, 400f);
			TutorialManager.MaskElement(TutorialManager.uiManager.panelRunningCharms.runningCharmCards[0].charmCardInfo.activationBackground.transform);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RIFT_06"));
			TutorialManager.ptOkayActive = true;
		}
		return true;
	}

	private static bool UpdateAndCheckCharmHubComplete()
	{
		if (TutorialManager.charmHub == TutorialManager.CharmHub.BEFORE_BEGIN || TutorialManager.charmHub == TutorialManager.CharmHub.FIN)
		{
			return false;
		}
		if (TutorialManager.charmHub == TutorialManager.CharmHub.OPEN_COLLECTION)
		{
			TutorialManager.uiManager.panelHub.tabBarLayout.enabled = false;
			TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharGreenMan;
			TutorialManager.pt.Move(-318f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RIFT_07"));
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelHub.buttonCharms.transform, true, true, -0.15f, false, 0f);
		}
		else if (TutorialManager.charmHub == TutorialManager.CharmHub.MESSAGE_1)
		{
			if (TutorialManager.uiManager.state == UiState.HUB_CHARMS)
			{
				TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharGreenMan;
				TutorialManager.pt.Move(-290f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RIFT_08"));
				TutorialManager.pt.imageBlackCurtain.gameObject.SetActive(true);
				TutorialManager.ptOkayActive = true;
			}
			else
			{
				TutorialManager.ptActive = false;
			}
		}
		else if (TutorialManager.charmHub == TutorialManager.CharmHub.MESSAGE_2)
		{
			if (TutorialManager.uiManager.state == UiState.HUB_CHARMS)
			{
				TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoSerpent;
				TutorialManager.pt.Move(368.5f, -19f, 630f, -45f, 420f);
				TutorialManager.spriteTutoCharPosition = TutorialManager.SpriteTutoCharRightPosition;
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RIFT_09"), TutorialManager.TextPositionWhenCharAtRight);
				TutorialManager.MaskElement(TutorialManager.uiManager.panelHubCharms.bonusTabParent);
				TutorialManager.ptOkayActive = true;
			}
			else
			{
				TutorialManager.ptActive = false;
			}
		}
		return true;
	}

	private static bool UpdateAndCheckFirstCharmPackComplete()
	{
		if (TutorialManager.firstCharmPack == TutorialManager.FirstCharmPack.BEFORE_BEGIN || TutorialManager.firstCharmPack == TutorialManager.FirstCharmPack.FIN)
		{
			return false;
		}
		if (TutorialManager.firstCharmPack == TutorialManager.FirstCharmPack.OPEN_SHOP)
		{
			if (TutorialManager.uiManager.state == UiState.HUB)
			{
				TutorialManager.uiManager.panelHub.tabBarLayout.enabled = false;
				TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharGreenMan;
				TutorialManager.pt.Move(-168f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RIFT_10"));
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelHub.buttonShop.transform, true, true, -0.15f, false, 0f);
			}
			else
			{
				TutorialManager.ptActive = false;
			}
		}
		else if (TutorialManager.firstCharmPack == TutorialManager.FirstCharmPack.WAIT_TO_OPEN)
		{
			TutorialManager.ptActive = false;
			TutorialManager.uiManager.panelHubShop.MoveScrollTo(0.65f);
			TutorialManager.pt.imageBlackCurtain.gameObject.SetActive(true);
		}
		else if (TutorialManager.firstCharmPack == TutorialManager.FirstCharmPack.OPEN_PACK)
		{
			if (TutorialManager.uiManager.state == UiState.HUB_SHOP)
			{
				TutorialManager.uiManager.panelHubShop.MoveScrollTo(0.65f);
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelShop.shopCharmPacks[0].transform, false, true, 0f, false, 0f);
			}
			TutorialManager.ptActive = false;
		}
		return true;
	}

	private static bool UpdateAndCheckCharmLevelUpComplete()
	{
		if (TutorialManager.charmLevelUp == TutorialManager.CharmLevelUp.BEFORE_BEGIN || TutorialManager.charmLevelUp == TutorialManager.CharmLevelUp.FIN)
		{
			return false;
		}
		if (TutorialManager.charmLevelUp == TutorialManager.CharmLevelUp.EXIT_SHOP)
		{
			if (TutorialManager.uiManager.state == UiState.HUB_SHOP)
			{
				TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoSerpent;
				TutorialManager.spriteTutoCharPosition = TutorialManager.SpriteTutoCharRightPosition;
				TutorialManager.pt.Move(0f, -19f, 630f, -45f, 420f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RIFT_11"), TutorialManager.TextPositionWhenCharAtRight);
				TutorialManager.SetArrow(TutorialManager.pt, new Transform[]
				{
					TutorialManager.uiManager.panelHubShop.buttonBack.transform,
					TutorialManager.uiManager.panelHubShop.headerParent
				}, false, true, 0.1f, false, 0.05f);
			}
			else
			{
				TutorialManager.ptActive = false;
			}
		}
		else if (TutorialManager.charmLevelUp == TutorialManager.CharmLevelUp.OPEN_COLLECTION)
		{
			if (TutorialManager.uiManager.state == UiState.HUB)
			{
				TutorialManager.uiManager.panelHub.tabBarLayout.enabled = false;
				TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharGreenMan;
				TutorialManager.pt.Move(-318f, 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RIFT_12"));
				TutorialManager.SetArrow(TutorialManager.pt, new Transform[]
				{
					TutorialManager.uiManager.panelHub.buttonCharms.transform,
					TutorialManager.uiManager.panelHub.notificationCharms.transform
				}, true, true, -0.15f, false, 0f);
			}
			else
			{
				TutorialManager.ptActive = false;
			}
		}
		else if (TutorialManager.charmLevelUp == TutorialManager.CharmLevelUp.SELECT_CHARM)
		{
			if (TutorialManager.uiManager.state == UiState.HUB_CHARMS)
			{
				TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharGreenMan;
				int num;
				CharmCard charmReadyToUpgradeOrFirst = TutorialManager.uiManager.panelHubCharms.GetCharmReadyToUpgradeOrFirst(out num);
				TutorialManager.pt.Move((float)((num >= 2) ? 70 : -310), 64f, 620f, 0f, 400f);
				TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RIFT_13"));
				if (charmReadyToUpgradeOrFirst != null)
				{
					TutorialManager.SetArrow(TutorialManager.pt, charmReadyToUpgradeOrFirst.transform, false, false, -0.15f, false, 0f);
					TutorialManager.pt.imageBlackCurtain.gameObject.SetActive(true);
					TutorialManager.pt.imageBlackCurtain.rectTransform.anchorMin = new Vector2(0f, 1f);
					TutorialManager.pt.imageBlackCurtain.rectTransform.SetSizeDeltaY(375f);
					TutorialManager.uiManager.panelHubCharms.scrollRect.enabled = false;
				}
				else
				{
					TutorialManager.pt.imageBlackCurtain.gameObject.SetActive(true);
				}
			}
			else
			{
				TutorialManager.ptActive = false;
			}
		}
		else if (TutorialManager.charmLevelUp == TutorialManager.CharmLevelUp.EXPLAIN_LEVELUP)
		{
			TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharGreenMan;
			TutorialManager.pt.Move(-350f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RIFT_14"));
			TutorialManager.ptOkayActive = true;
		}
		return true;
	}

	private static bool UpdateAndCheckAeonDustComplete()
	{
		if (TutorialManager.aeonDust == TutorialManager.AeonDust.BEFORE_BEGIN || TutorialManager.aeonDust == TutorialManager.AeonDust.FIN)
		{
			return false;
		}
		TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharAlchemist;
		TutorialManager.pt.Move(36f, 64f, 620f, 0f, 400f);
		TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RIFT_15"));
		TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelHub.buttonCollectRiftReward.transform, true, true, -0.1f, false, 0f);
		return true;
	}

	private static bool UpdateAndCheckRepeatRiftsComplete()
	{
		if (TutorialManager.repeatRifts == TutorialManager.RepeatRifts.BEFORE_BEGIN || TutorialManager.repeatRifts == TutorialManager.RepeatRifts.FIN || TutorialManager.repeatRifts == TutorialManager.RepeatRifts.WAIT_CLOSE_SELECT)
		{
			return false;
		}
		if (TutorialManager.repeatRifts == TutorialManager.RepeatRifts.UNLOCK)
		{
			TutorialManager.uiManager.panelHubModeSetup.buttonSelectRift.interactable = true;
			TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharAlchemist;
			TutorialManager.pt.Move(-155f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RIFT_16"));
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelHubModeSetup.buttonSelectRift.transform, true, true, 0f, true, 0f);
		}
		else if (TutorialManager.repeatRifts == TutorialManager.RepeatRifts.SELECT)
		{
			TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharAlchemist;
			TutorialManager.pt.Move(240f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RIFT_17"));
			TutorialManager.ptOkayActive = true;
			TutorialManager.MaskElement(TutorialManager.uiManager.panelRiftSelect.aeonDustMissionParent);
		}
		else if (TutorialManager.repeatRifts == TutorialManager.RepeatRifts.FINAL_TEXT)
		{
			TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoSerpent;
			TutorialManager.spriteTutoCharPosition = TutorialManager.SpriteTutoCharRightPosition;
			TutorialManager.pt.Move(-105f, -19f, 630f, -45f, 420f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RIFT_18"), TutorialManager.TextPositionWhenCharAtRight);
			TutorialManager.pt.imageBlackCurtain.gameObject.SetActive(true);
			TutorialManager.ptOkayActive = true;
		}
		return true;
	}

	private static bool UpdateAndCheckAllRiftsFinishedComplete()
	{
		if (TutorialManager.allRiftsFinished == TutorialManager.AllRiftsFinished.BEFORE_BEGIN || TutorialManager.allRiftsFinished == TutorialManager.AllRiftsFinished.FIN)
		{
			return false;
		}
		TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoSerpent;
		TutorialManager.pt.Move(-59.5f, 0f, 620f, 0f, 400f);
		TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_RIFT_19"), TutorialManager.TextPositionWhenCharAtRight);
		TutorialManager.ptOkayActive = true;
		return true;
	}

	private static bool UpdateAndCheckFlashOffersUnlockedComplete()
	{
		if (TutorialManager.flashOffersUnlocked == TutorialManager.FlashOffersUnlocked.BEFORE_BEGIN || TutorialManager.flashOffersUnlocked == TutorialManager.FlashOffersUnlocked.FIN)
		{
			return false;
		}
		TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharGreenMan;
		if (TutorialManager.flashOffersUnlocked == TutorialManager.FlashOffersUnlocked.OPEN_SHOP)
		{
			TutorialManager.pt.Move(120f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_FLASH_OFFERS_UNLOCKED_1"));
			TutorialManager.uiManager.panelHub.tabBarLayout.enabled = false;
			TutorialManager.SetArrow(TutorialManager.pt, (TutorialManager.uiManager.state != UiState.HUB) ? TutorialManager.uiManager.tabBarButtons[4].transform : TutorialManager.uiManager.panelHub.buttonShop.transform, true, true, 0f, false, 0f);
		}
		else if (TutorialManager.flashOffersUnlocked == TutorialManager.FlashOffersUnlocked.SHOW_MESSAGE)
		{
			TutorialManager.pt.Move(500f, 64f, 620f, 0f, 400f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_FLASH_OFFERS_UNLOCKED_2"));
			int num = 0;
			if (TutorialManager.sim.halloweenEnabled && TutorialManager.sim.halloweenFlashOfferBundle != null)
			{
				num += 810;
			}
			TutorialManager.uiManager.panelShop.scrollView.content.SetAnchorPosY((float)num);
			if (TutorialManager.waitOneFrame)
			{
				TutorialManager.pt.imageBlackCurtain.gameObject.SetActive(true);
				TutorialManager.waitOneFrame = false;
			}
			else
			{
				TutorialManager.MaskElement(TutorialManager.uiManager.panelShop.parentAdventureFlashOffers);
				TutorialManager.uiManager.panelShop.forceUpdatePackOffer = false;
			}
			TutorialManager.ptOkayActive = true;
		}
		return true;
	}

	private static bool UpdateAndCheckCursedGatesComplete()
	{
		if (TutorialManager.cursedGates == TutorialManager.CursedGates.BEFORE_BEGIN || TutorialManager.cursedGates == TutorialManager.CursedGates.FIN)
		{
			return false;
		}
		TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoSerpent;
		TutorialManager.spriteTutoCharPosition = TutorialManager.SpriteTutoCharRightPosition;
		if (TutorialManager.cursedGates == TutorialManager.CursedGates.OPEN_SELECT_GATE_PANEL)
		{
			TutorialManager.pt.Move(-120f, -19f, 630f, -45f, 420f);
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelHubModeSetup.buttonSelectRift.transform, true, true, 0.1f, true, 0f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_CURSE_1"), TutorialManager.TextPositionWhenCharAtRight);
		}
		else if (TutorialManager.cursedGates == TutorialManager.CursedGates.OPEN_CURSES_TAB)
		{
			TutorialManager.pt.Move(0f, -19f, 630f, -45f, 420f);
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelRiftSelect.buttonTabCursed.transform, false, true, 0.1f, false, 0f);
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_CURSE_2"), TutorialManager.TextPositionWhenCharAtRight);
		}
		return true;
	}

	private static bool UpdateAndCheckMissionsFinishedComplete()
	{
		if (TutorialManager.missionsFinished == TutorialManager.MissionsFinished.BEFORE_BEGIN || TutorialManager.missionsFinished == TutorialManager.MissionsFinished.FIN)
		{
			return false;
		}
		if (TutorialManager.missionsFinished == TutorialManager.MissionsFinished.MESSAGE)
		{
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_MISSIONS_FINISHED_MESSAGE"));
			TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
			TutorialManager.pt.imageBlackCurtain.gameObject.SetActive(true);
			TutorialManager.ptOkayActive = true;
		}
		return true;
	}

	private static bool UpdateAndCheckTrinketSmithingUnlockedComplete()
	{
		if (TutorialManager.trinketSmithingUnlocked == TutorialManager.TrinketSmithingUnlocked.BEFORE_BEGIN || TutorialManager.trinketSmithingUnlocked == TutorialManager.TrinketSmithingUnlocked.FIN)
		{
			return false;
		}
		if (TutorialManager.trinketSmithingUnlocked == TutorialManager.TrinketSmithingUnlocked.GO_TO_HUB)
		{
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_TRINKET_SMITHING_UNLOCKED_1"));
			TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.tabBarButtons[0].transform, true, true, 0f, false, 0f);
		}
		else if (TutorialManager.trinketSmithingUnlocked == TutorialManager.TrinketSmithingUnlocked.OPEN_TRINKETS_SCREEN)
		{
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_TRINKET_SMITHING_UNLOCKED_2"));
			TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
			TutorialManager.uiManager.panelHub.tabBarLayout.enabled = false;
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelHub.buttonTrinkets.transform, true, true, -0.15f, false, 0f);
		}
		else if (TutorialManager.trinketSmithingUnlocked == TutorialManager.TrinketSmithingUnlocked.SELECT_SMITHING_TAB)
		{
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_TRINKET_SMITHING_UNLOCKED_3"));
			TutorialManager.pt.Move(20f, 64f, 620f, 0f, 400f);
			TutorialManager.uiManager.panelHubTrinkets.tabsLayout.enabled = false;
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelHubTrinkets.buttonTabSmithing.transform, false, true, 0.1f, false, 0f);
		}
		return true;
	}

	private static bool UpdateAndCheckTrinketRecycleUnlockedComplete()
	{
		if (TutorialManager.trinketRecycleUnlocked == TutorialManager.TrinketRecycleUnlocked.BEFORE_BEGIN || TutorialManager.trinketRecycleUnlocked == TutorialManager.TrinketRecycleUnlocked.FIN)
		{
			return false;
		}
		if (TutorialManager.trinketRecycleUnlocked == TutorialManager.TrinketRecycleUnlocked.MESSAGE)
		{
			TutorialManager.SetText(TutorialManager.pt, LM.Get("TUTO_TRINKET_RECYCLE_UNLOCKED"));
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelTrinketInfoPopup.buttonRecycle.transform, false, false, 0f, false, 0f);
			TutorialManager.pt.Move(12f, 64f, 620f, 0f, 400f);
		}
		return true;
	}

	private static bool UpdateAndCheckChristmasTreeEventUnlockedComplete()
	{
		if (TutorialManager.christmasTreeEventUnlocked == TutorialManager.ChristmasTreeEventUnlocked.BEFORE_BEGIN || TutorialManager.christmasTreeEventUnlocked == TutorialManager.ChristmasTreeEventUnlocked.FIN)
		{
			return false;
		}
		TutorialManager.uiManager.inputBlocker.SetActive(false);
		if (TutorialManager.christmasTreeEventUnlocked == TutorialManager.ChristmasTreeEventUnlocked.OPEN_POPUP)
		{
			TutorialManager.SetText(TutorialManager.pt, LM.Get("CHRISTMAS_TREE_EVENT_TUTORIAL_1"));
			TutorialManager.SetArrow(TutorialManager.pt, (TutorialManager.uiManager.state != UiState.HUB) ? TutorialManager.uiManager.openOfferPopupButton.transform : TutorialManager.uiManager.panelHub.christmasEventParent.transform, false, true, (TutorialManager.uiManager.state != UiState.HUB) ? 0f : -0.1f, false, 0f);
			TutorialManager.pt.Move(0f, 64f, 620f, 0f, 400f);
		}
		else if (TutorialManager.christmasTreeEventUnlocked == TutorialManager.ChristmasTreeEventUnlocked.WAIT_ANIM)
		{
			if (TutorialManager.timeCounter <= 0.5f)
			{
				TutorialManager.uiManager.panelChristmasOffer.treeScroll.verticalNormalizedPosition = 1f;
			}
			else if (TutorialManager.timeCounter <= 2f)
			{
				TutorialManager.uiManager.panelChristmasOffer.treeScroll.verticalNormalizedPosition = 1f - (TutorialManager.timeCounter - 0.5f) / 1.5f;
			}
			else
			{
				TutorialManager.uiManager.panelChristmasOffer.treeScroll.verticalNormalizedPosition = 0f;
			}
			TutorialManager.uiManager.inputBlocker.SetActive(true);
			TutorialManager.ptActive = false;
		}
		else if (TutorialManager.christmasTreeEventUnlocked == TutorialManager.ChristmasTreeEventUnlocked.MESSAGE_1)
		{
			TutorialManager.SetText(TutorialManager.pt, LM.Get("CHRISTMAS_TREE_EVENT_TUTORIAL_2"));
			TutorialManager.pt.Move(415f, 64f, 620f, 0f, 400f);
			TutorialManager.pt.imageBlackCurtain.rectTransform.anchorMin = new Vector2(0f, 1f);
			TutorialManager.pt.imageBlackCurtain.rectTransform.SetSizeDeltaY(375f);
			TutorialManager.pt.imageBlackCurtain.gameObject.SetActive(true);
			if (TutorialManager.uiManager.panelChristmasOffer.treeOffers != null)
			{
				TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelChristmasOffer.treeOffers[0][0].transform, true, false, -0.1f, true, -0.1f);
			}
		}
		else if (TutorialManager.christmasTreeEventUnlocked == TutorialManager.ChristmasTreeEventUnlocked.MESSAGE_2)
		{
			if (TutorialManager.uiManager.state == UiState.CHRISTMAS_PANEL)
			{
				TutorialManager.SetText(TutorialManager.pt, string.Format(LM.Get("CHRISTMAS_TREE_EVENT_TUTORIAL_3"), (PlayfabManager.titleData != null) ? PlayfabManager.titleData.christmasCandyCapAmount : 0.0, GameMath.GetTimeLessDetailedString(36000.0, true)));
				TutorialManager.ptOkayActive = true;
				TutorialManager.pt.Move(80f, 64f, 620f, 0f, 400f);
				TutorialManager.MaskElement(TutorialManager.uiManager.panelChristmasOffer.candiesDailyCapParent);
			}
			else
			{
				TutorialManager.ptActive = false;
			}
		}
		else if (TutorialManager.christmasTreeEventUnlocked == TutorialManager.ChristmasTreeEventUnlocked.OPEN_SHOP_TAB)
		{
			TutorialManager.SetText(TutorialManager.pt, LM.Get("CHRISTMAS_TREE_EVENT_TUTORIAL_4"));
			TutorialManager.SetArrow(TutorialManager.pt, new Transform[]
			{
				TutorialManager.uiManager.panelChristmasOffer.offersTabButton.transform,
				TutorialManager.uiManager.panelChristmasOffer.treeTabButton.transform
			}, false, true, 0f, false, 0f);
			TutorialManager.pt.Move(80f, 64f, 620f, 0f, 400f);
		}
		return true;
	}

	private static bool UpdateAndCheckArtifactOverhaulComplete()
	{
		if (TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.BEFORE_BEGIN || TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.FIN)
		{
			return false;
		}
		TutorialManager.uiManager.panelArtifactScroller.isLookingAtMythical = false;
		TutorialManager.uiManager.panelArtifactScroller.CalculatePositions(false);
		TutorialManager.spriteTutoChar = TutorialManager.pt.spriteTutoCharAlchemist;
		if (TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.WELCOME)
		{
			TutorialManager.ptActive = true;
			TutorialManager.ptOkayActive = true;
			TutorialManager.SetText(TutorialManager.pt, "TUTORIAL_ARTIFACT_OVERHAUL_1".Loc());
			TutorialManager.pt.imageBlackCurtain.gameObject.SetActive(true);
			TutorialManager.pt.imageBlackCurtain.SetAlpha(0.753f);
			TutorialManager.pt.Move(270f, 64f, 620f, 0f, 400f);
		}
		else if (TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.GIVE_MYTHSTONES)
		{
			TutorialManager.ptActive = true;
			TutorialManager.ptOkayActive = true;
			if (TutorialManager.setText)
			{
				TutorialManager.setText = false;
				TutorialManager.SetText(TutorialManager.pt, "TUTORIAL_ARTIFACT_OVERHAUL_2".LocFormat(GameMath.GetDoubleString(OldArtifactsConverter.GetHalfConversionReward(TutorialManager.sim))));
				TutorialManager.pt.buttonOkay.text.text = LM.Get("UI_COLLECT");
			}
			TutorialManager.pt.imageBlackCurtain.gameObject.SetActive(true);
			TutorialManager.pt.Move(-65f, 64f, 620f, 0f, 400f);
		}
		else if (TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.SELECT_ARTIFACT)
		{
			TutorialManager.ptActive = true;
			TutorialManager.SetText(TutorialManager.pt, "TUTORIAL_ARTIFACT_OVERHAUL_3".Loc());
			TutorialManager.pt.Move(280f, 64f, 620f, 0f, 400f);
			TutorialManager.SetArrow(TutorialManager.pt, TutorialManager.uiManager.panelArtifactScroller.buttonArtifacts[0].transform, false, true, 0f, false, 0f);
		}
		else
		{
			TutorialManager.ptActive = false;
		}
		return true;
	}

	private static void OnTutorialStep(object name)
	{
		Type type = name.GetType();
		PlayfabManager.SendPlayerEvent(PlayfabEventId.TUTORIAL_STEP_COMPLETE, new Dictionary<string, object>
		{
			{
				"name",
				type.Name
			}
		}, null, null, true);
	}

	private static TutorialManager.First _first;

	public static TutorialManager.HubTab hubTab;

	public static TutorialManager.ModeTab modeTab;

	public static TutorialManager.ArtifactsTab artifactsTab;

	public static TutorialManager.ShopTab shopTab;

	public static TutorialManager.Prestige prestige;

	public static TutorialManager.SkillScreen skillScreen;

	public static TutorialManager.FightBossButton fightBossButton;

	public static TutorialManager.GearScreen gearScreen;

	public static TutorialManager.RuneScreen runeScreen;

	public static TutorialManager.RingPrestigeReminder ringPrestigeReminder;

	public static TutorialManager.HeroPrestigeReminder heroPrestigeReminder;

	public static TutorialManager.MythicalArtifactsTab mythicalArtifactsTab;

	public static TutorialManager.TrinketShop trinketShop;

	public static TutorialManager.TrinketHeroTab trinketHeroTab;

	public static TutorialManager.MineUnlock mineUnlock;

	public static TutorialManager.DailyUnlock dailyUnlock;

	public static TutorialManager.DailyComplete dailyComplete;

	public static TutorialManager.RiftsUnlock riftsUnlock;

	public static TutorialManager.RiftEffects riftEffects;

	public static TutorialManager.FirstCharm firstCharm;

	public static TutorialManager.CharmHub charmHub;

	public static TutorialManager.FirstCharmPack firstCharmPack;

	public static TutorialManager.CharmLevelUp charmLevelUp;

	public static TutorialManager.AeonDust aeonDust;

	public static TutorialManager.RepeatRifts repeatRifts;

	public static TutorialManager.AllRiftsFinished allRiftsFinished;

	public static TutorialManager.FlashOffersUnlocked flashOffersUnlocked;

	public static TutorialManager.CursedGates cursedGates;

	public static TutorialManager.MissionsFinished missionsFinished;

	public static TutorialManager.TrinketSmithingUnlocked trinketSmithingUnlocked;

	public static TutorialManager.TrinketRecycleUnlocked trinketRecycleUnlocked;

	public static TutorialManager.ChristmasTreeEventUnlocked christmasTreeEventUnlocked;

	public static TutorialManager.ArtifactOverhaul artifactOverhaul;

	public static int missionIndex = -1;

	public static float timeCounter = 0f;

	public static float fightBossButtonTimer = 0f;

	public static float skillScreenTimer = 0f;

	public static float firstPeriod = 0f;

	public static float hubTabPeriod = 0f;

	public static float modeTabPeriod = 0f;

	public static float artifactsTabPeriod = 0f;

	public static float shopTabPeriod = 0f;

	public static float prestigePeriod = 0f;

	public static float skillScreenPeriod = 0f;

	public static float fightBossButtonPeriod = 0f;

	public static float gearScreenPeriod = 0f;

	public static float gearGlobalReminderPeriod = 0f;

	public static float runeScreenPeriod = 0f;

	public static float ringPrestigeReminderPeriod = 0f;

	public static float heroPrestigeReminderPeriod = 0f;

	public static float mythicalArtifactsTabPeriod = 0f;

	public static float trinketShopPeriod = 0f;

	public static float trinketHeroTabPeriod = 0f;

	private const float TutorialPanelPositionWhenCharAtRight = -19f;

	private const float TutorialPanelBgWidthWhenCharAtRight = 630f;

	private const float TutorialPanelBgPosWhenCharAtRight = -45f;

	private const float TutorialPanelTextWidthWhenCharAtRight = 420f;

	private static readonly Vector2 DefaultTextPosition = new Vector2(23f, 2f);

	private static readonly Vector2 TextPositionWhenCharAtRight = new Vector2(-76f, 2f);

	private static readonly Vector2 DefaultCharacterPosition = new Vector2(-321f, 47.44f);

	private static readonly Vector2 SpriteTutoCharRightPosition = new Vector2(270.7f, 62.7f);

	private const float ChristmasTreePauseDurBeforeAnim = 0.5f;

	private const float ChristmasTreePauseDurAfterAnim = 0.5f;

	private const float ChristmasTreeScrollAnimDur = 1.5f;

	private static List<Transform> parents = new List<Transform>();

	private static List<Transform> maskedElements = new List<Transform>();

	private static List<Vector3> localPositions = new List<Vector3>();

	private static List<int> siblingIndexList = new List<int>();

	private static Simulator sim;

	private static bool pressedOkay;

	private static bool firstFrameOfFirst = true;

	public static bool setText = true;

	private static float? prestigeCountdown;

	private static bool ptActive = true;

	private static bool ptOkayActive = false;

	private static PanelTutorial pt;

	private static UiManager uiManager;

	private static Sprite spriteTutoChar;

	private static Vector2 spriteTutoCharPosition;

	private static float waitTime;

	private static bool waitOneFrame;

	private const float charPerSec = 60f;

	private static readonly Quaternion VerticalRotation = Quaternion.Euler(Vector3.zero);

	private static readonly Quaternion HorizontalRotation = Quaternion.Euler(Vector3.forward * 90f);

	public enum First
	{
		WELCOME,
		RING_OFFER,
		RING_CLAIMED,
		FIGHT_RING,
		HEROES_TAB_UNLOCK,
		RING_UPGRADE,
		RING_UPGRADE_DONE,
		FIGHT_RING_2,
		HERO_AVAILABLE,
		HERO_BUY_BUTTON,
		FIGHT_HERO,
		FIGHT_HERO_BOSS_WAIT,
		ULTIMATE_SELECT,
		FIGHT_HERO_BOSS_DIE,
		FIGHT_HERO_2,
		HERO_UPGRADE_AVAILABLE,
		HERO_UPGRADE_TAB,
		FIN
	}

	public enum HubTab
	{
		BEFORE_BEGIN,
		FIN
	}

	public enum ModeTab
	{
		BEFORE_BEGIN,
		UNLOCKED,
		IN_TAB,
		FIN
	}

	public enum ArtifactsTab
	{
		BEFORE_BEGIN,
		UNLOCKED,
		IN_TAB,
		CRAFTING_ARTIFACT,
		SELECT_ARTIFACT,
		FIN
	}

	public enum ShopTab
	{
		BEFORE_BEGIN,
		UNLOCKED,
		IN_TAB,
		LOOTPACK_OPENED,
		GO_TO_GEARS,
		FIN
	}

	public enum Prestige
	{
		BEFORE_BEGIN,
		UNLOCKED,
		IN_TAB,
		FIN
	}

	public enum SkillScreen
	{
		BEFORE_BEGIN,
		UNLOCKED,
		FIN
	}

	public enum FightBossButton
	{
		BEFORE_BEGIN,
		WAIT,
		SHOW_BUTTON,
		FIN
	}

	public enum GearScreen
	{
		BEFORE_BEGIN,
		UNLOCKED,
		FIN
	}

	public enum RuneScreen
	{
		BEFORE_BEGIN,
		UNLOCKED,
		FIN
	}

	public enum RingPrestigeReminder
	{
		BEFORE_BEGIN,
		UNLOCKED,
		FIN
	}

	public enum HeroPrestigeReminder
	{
		BEFORE_BEGIN,
		UNLOCKED,
		FIN
	}

	public enum MythicalArtifactsTab
	{
		BEFORE_BEGIN,
		UNLOCKED,
		FIN
	}

	public enum TrinketShop
	{
		BEFORE_BEGIN,
		UNLOCKED,
		FIN
	}

	public enum TrinketHeroTab
	{
		BEFORE_BEGIN,
		UNLOCKED,
		EQUIP,
		EFFECTS,
		FIN
	}

	public enum MineUnlock
	{
		BEFORE_BEGIN,
		UNLOCKED,
		FIN
	}

	public enum DailyUnlock
	{
		BEFORE_BEGIN,
		UNLOCKED,
		FIN
	}

	public enum DailyComplete
	{
		BEFORE_BEGIN,
		UNLOCKED,
		FIN
	}

	public enum RiftsUnlock
	{
		BEFORE_BEGIN,
		UNLOCKED,
		FIN
	}

	public enum RiftEffects
	{
		BEFORE_BEGIN,
		IN_TAB,
		FIN
	}

	public enum FirstCharm
	{
		BEFORE_BEGIN,
		WARNING,
		WAIT_SELECT,
		SHOW_COLLECTION,
		EXPLAIN_EFFECTS,
		EXPLAIN_TRIGGER,
		FIN
	}

	public enum CharmHub
	{
		BEFORE_BEGIN,
		OPEN_COLLECTION,
		MESSAGE_1,
		MESSAGE_2,
		FIN
	}

	public enum FirstCharmPack
	{
		BEFORE_BEGIN,
		OPEN_SHOP,
		WAIT_TO_OPEN,
		OPEN_PACK,
		FIN
	}

	public enum CharmLevelUp
	{
		BEFORE_BEGIN,
		EXIT_SHOP,
		OPEN_COLLECTION,
		SELECT_CHARM,
		EXPLAIN_LEVELUP,
		FIN
	}

	public enum AeonDust
	{
		BEFORE_BEGIN,
		COLLECT,
		FIN
	}

	public enum RepeatRifts
	{
		BEFORE_BEGIN,
		UNLOCK,
		SELECT,
		WAIT_CLOSE_SELECT,
		FINAL_TEXT,
		FIN
	}

	public enum AllRiftsFinished
	{
		BEFORE_BEGIN,
		MESSAGE,
		FIN
	}

	public enum FlashOffersUnlocked
	{
		BEFORE_BEGIN,
		OPEN_SHOP,
		SHOW_MESSAGE,
		FIN
	}

	public enum CursedGates
	{
		BEFORE_BEGIN,
		OPEN_SELECT_GATE_PANEL,
		OPEN_CURSES_TAB,
		FIN
	}

	public enum MissionsFinished
	{
		BEFORE_BEGIN,
		MESSAGE,
		FIN
	}

	public enum TrinketSmithingUnlocked
	{
		BEFORE_BEGIN,
		GO_TO_HUB,
		OPEN_TRINKETS_SCREEN,
		SELECT_SMITHING_TAB,
		FIN
	}

	public enum TrinketRecycleUnlocked
	{
		BEFORE_BEGIN,
		MESSAGE,
		FIN
	}

	public enum ChristmasTreeEventUnlocked
	{
		BEFORE_BEGIN,
		OPEN_POPUP,
		WAIT_ANIM,
		MESSAGE_1,
		MESSAGE_2,
		OPEN_SHOP_TAB,
		FIN
	}

	public enum ArtifactOverhaul
	{
		BEFORE_BEGIN,
		WELCOME,
		WAIT_GIVE_NEW_ARTIFACTS,
		WAIT_POSITIONING_ARTIFACTS,
		GIVE_MYTHSTONES,
		WAIT_DROPS,
		SELECT_ARTIFACT,
		FIN
	}
}
