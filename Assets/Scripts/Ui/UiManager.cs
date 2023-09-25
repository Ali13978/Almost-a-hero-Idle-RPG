using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Plugins.Options;
using DynamicLoading;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Newtonsoft.Json;
using Simulation;
using Simulation.ArtifactSystem;
using SocialRewards;
using Spine.Unity;
using Static;
using stats;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
	public class UiManager : MonoBehaviour
	{
		public UiState state
		{
			get
			{
				return this._state;
			}
			set
			{
				this.isScrollViewLocked = true;
				if (this._state != value)
				{
					UiManager.stateJustChanged = true;
				}
				CrashHandler.SetUiState(this._state.ToString(), value.ToString());
				UiState state = this._state;
				switch (state)
				{
				case UiState.HEROES_GEAR:
					this.panelGearScreen.OnClose();
					break;
				default:
					if (state != UiState.AD_POPUP)
					{
						if (state != UiState.HUB_DATABASE_HEROES_ITEMS)
						{
							switch (state)
							{
							case UiState.SHARE_SCREENSHOT_PANEL:
								this.sim.isActiveWorldPaused = false;
								this.panelShareScreenshot.OnClosed();
								break;
							default:
								if (state != UiState.HUB)
								{
									if (state != UiState.MODE_UNLOCK_REWARD)
									{
										if (state != UiState.CHALLENGE_WIN)
										{
											if (state != UiState.CHANGE_SKIN)
											{
												if (state != UiState.BUY_FLASH_OFFER)
												{
													if (state == UiState.CHRISTMAS_EVENT_POPUP)
													{
														this.panelChristmasEventPopup.OnClose();
													}
												}
												else
												{
													this.panelBuyAdventureFlashOffer.costumeAnimation.OnClose();
													this.panelBuyAdventureFlashOffer.heroMaterial.SetColor("_Black", Color.black);
												}
											}
											else if (value != UiState.HUB_DATABASE_HEROES_ITEMS && value != UiState.CURRENCY_WARNING)
											{
												this.panelHeroSkinChanger.heroAnimation.OnClose();
												this.panelHubDatabase.panelGear.OnClose();
												this.panelHeroEvolveSkin.heroAnimation.OnClose();
												this.panelSkinBuyingPopup.heroAnimation.OnClose();
											}
										}
										else
										{
											this.panelChallengeWin.heroAnimation.OnClose();
										}
									}
									else
									{
										this.panelUnlockReward.heroAnimation.OnClose();
									}
								}
								else
								{
									this.panelHub.riftQuestSlider.aeonIconGlow.SetActive(false);
								}
								break;
							case UiState.ARTIFACT_SELECTED_POPUP:
								this.panelArtifactPopup.OnClose();
								break;
							}
						}
						else if (value == UiState.HUB)
						{
							this.panelHubDatabase.panelGear.OnClose();
							this.panelHeroEvolveSkin.heroAnimation.OnClose();
							this.panelHeroSkinChanger.heroAnimation.OnClose();
							this.panelSkinBuyingPopup.heroAnimation.OnClose();
						}
						else if (value == UiState.HEROES_EVOLVE)
						{
							this.panelHubDatabase.panelGear.OnClose();
						}
					}
					else
					{
						this.sim.GetActiveWorld().showingAdOffer = false;
					}
					break;
				case UiState.HEROES_NEW:
					this.panelNewHero.heroAnimation.OnClose();
					break;
				}
				switch (value)
				{
				case UiState.NONE:
					this.panelShop.isHubMode = false;
					this.OpenMenu(value, false, -1, -1, new int[0]);
					break;
				case UiState.HUB:
					this.panelHub.buttonGameModeStandard.Disableinfo();
					this.panelHub.buttonGameModeCrusade.Disableinfo();
					this.panelHub.buttonGameModeRift.Disableinfo();
					if (!this.panelHub.isFadeIn)
					{
						this.panelHub.DoFadeIn(delegate
						{
						});
					}
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						18
					});
					break;
				case UiState.MODE:
					if (this.sim.GetActiveWorld().eventMerchantItems == null)
					{
						this.panelMode.GoToMerchantItemsTab();
						this.panelMode.OnPanelOpened(this.sim.IsMerchantUnlocked(), false);
					}
					else
					{
						this.panelMode.OnPanelOpened(this.sim.IsMerchantUnlocked(), this.sim.IsChristmasTreeEnabled());
					}
					if (this.sim.numPrestiges >= 1)
					{
						this.panelMode.EnableShareButton();
					}
					else
					{
						this.panelMode.DisableShareButton();
					}
					if (this.sim.IsActiveMode(GameMode.RIFT))
					{
						this.panelMode.buttonAbandonChallenge.text.text = LM.Get("UI_MODE_ABANDON_RIFT");
					}
					else
					{
						this.panelMode.buttonAbandonChallenge.text.text = LM.Get("UI_MODE_ABANDON");
					}
					this.isScrollViewLocked = false;
					this.OpenMenu(value, false, 1, 1, new int[0]);
					break;
				case UiState.HEROES:
					this.isScrollViewLocked = false;
					this.OpenMenu(value, false, 2, 2, new int[0]);
					break;
				case UiState.ARTIFACTS:
					this.panelArtifactScroller.OnScreenAppear();
					this.isScrollViewLocked = true;
					if (this._state == UiState.ARTIFACTS_CRAFT && this.panelArtifactsCraft.stoneBoxArtifactsScreen != null)
					{
						this.panelArtifactsCraft.stoneBoxArtifactsScreen.gameObject.SetActive(true);
					}
					this.panelArtifactScroller.ResetScreenAnim();
					this.panelArtifactScroller.craftButton.gameObject.SetActive(true);
					this.panelArtifactScroller.unlockButtonWaitingForConfirm = false;
					if (this.panelArtifactScroller.selectedArtifactIndex != -1 && this._state != UiState.CURRENCY_WARNING && this._state != UiState.ARTIFACTS_INFO && this._state != UiState.POSSIBLE_ARTIFACT_EFFECTS_POPUP)
					{
						this.panelArtifactScroller.ResetArtifactSelected();
					}
					this.panelArtifactScroller.CalculateContentSize(this.sim.artifactsManager.AvailableSlotsCount);
					this.panelArtifactScroller.multipleBuyIndex = this.sim.artifactMultiUpgradeIndex;
					this.SetArtifactScrollParent(1);
					if (this._state != UiState.ARTIFACTS_CRAFT && this._state != UiState.ARTIFACTS_REROLL && this._state != UiState.ARTIFACTS_INFO && this._state != UiState.POSSIBLE_ARTIFACT_EFFECTS_POPUP)
					{
						this.panelArtifactScroller.SetScrollPosition(0f, false);
					}
					this.OpenMenu(value, false, 3, 3, new int[]
					{
						47
					});
					this.panelArtifactScroller.OnScrollValueChanged(Vector2.zero);
					this.UpdateArtifactScroller();
					if (this._state == UiState.NEW_TAL_MILESTONE_REACHED_POPUP)
					{
						this.panelArtifactScroller.PlayNewTALMilestoneReachedAnim();
					}
					break;
				case UiState.SHOP:
					this.panelShop.setSpecialOfferContentTexts = true;
					this.panelShop.forceGoblinAnimation = !this.IsShopState(this._state);
					this.panelShop.isHubMode = false;
					this.isScrollViewLocked = true;
					UiManager.zeroScrollViewContentY = (!this.IsShopState(this._state) && this._state != UiState.CURRENCY_WARNING && (this._state != UiState.GENERAL_POPUP || this.panelGeneralPopup.state != PanelGeneralPopup.State.SHOP) && (this._state != UiState.SOCIAL_REWARD_POPUP || this.panelSocialRewardPopup.previousState != UiState.SHOP));
					this.OpenMenu(value, false, 4, 4, new int[0]);
					this.SetShopParent(1);
					this.panelShop.childrenContentSizeFitter.SetSize(0f, false);
					if (!TrustedTime.IsReady())
					{
						PlayfabManager.GetTime(delegate(bool isSuccess, DateTime time)
						{
							if (isSuccess)
							{
								TrustedTime.Init(time);
							}
						});
					}
					RewardedAdManager.inst.OnShopOpened();
					break;
				case UiState.HEROES_GEAR:
					this.isScrollViewLocked = true;
					this.panelGearScreen.buttonTab.interactable = false;
					this.panelSkillsScreen.buttonTab.interactable = true;
					this.panelTrinketsScreen.buttonTab.interactable = true;
					this.OpenMenu(value, false, 2, 5, new int[1]);
					break;
				case UiState.HEROES_SKILL:
					this.isScrollViewLocked = false;
					this.isScrollViewLocked = true;
					this.panelGearScreen.buttonTab.interactable = true;
					this.panelSkillsScreen.buttonTab.interactable = false;
					this.panelTrinketsScreen.buttonTab.interactable = true;
					this.panelSkillsScreen.ResetSelectedSkill();
					this.OpenMenu(value, false, 2, 5, new int[]
					{
						1
					});
					break;
				case UiState.HEROES_NEW:
					this.panelNewHero.heroTransitionAnimation.OnClose();
					this.panelNewHero.objectButton = this.panelNewHero.buttonNewHeroBuy.gameObject;
					this.panelNewHero.selected = -1;
					this.panelNewHero.SetScrollToZero();
					this.panelNewHero.inputBlocker.SetActive(false);
					this.panelNewHero.shownHeroes.Clear();
					this.OpenMenu(value, false, 2, 2, new int[]
					{
						11
					});
					this.panelNewHero.popupRect.DoPopupAnimation();
					break;
				case UiState.MODE_UNLOCKS:
					this.isScrollViewLocked = false;
					this.OpenMenu(value, false, 1, 6, new int[0]);
					break;
				case UiState.ARTIFACTS_CRAFT:
					this.panelArtifactsCraft.state = PanelArtifactsCraft.State.PrepareTransition;
					this.panelArtifactsCraft.updateDetails = !this.panelArtifactsCraft.skipArtifactShow;
					if (this.panelArtifactsCraft.updateDetails)
					{
						this.panelArtifactsCraft.stoneBoxArtifact = this.panelArtifactScroller.buttonArtifacts[this.panelArtifactScroller.artifactCount].artifactStone;
						this.panelArtifactsCraft.stoneBoxArtifactsScreen = this.panelArtifactScroller.buttonArtifacts[this.panelArtifactScroller.artifactCount].artifactStone.rectTransform;
					}
					this.panelArtifactsCraft.uistateToReturn = this._state;
					if (this._state != UiState.ARTIFACTS_CRAFT)
					{
						this.panelArtifactScroller.qpToShow = this.sim.artifactsManager.TotalArtifactsLevel;
					}
					if (this._state == UiState.ARTIFACTS)
					{
						this.OpenMenu(value, false, 3, 3, new int[]
						{
							2,
							47
						});
					}
					else if (this._state == UiState.ARTIFACT_OVERHAUL)
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							18,
							75,
							2
						});
					}
					else
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							2,
							47,
							48
						});
					}
					break;
				case UiState.ARTIFACTS_REROLL:
					this.panelArtifactScroller.qpToShow = this.sim.artifactsManager.TotalArtifactsLevel;
					this.panelArtifactsRerollWindow.SetSelectedArtifact(1);
					this.panelArtifactsRerollWindow.uistateToReturn = this._state;
					if (this._state == UiState.ARTIFACTS)
					{
						this.OpenMenu(value, false, 3, 3, new int[]
						{
							3,
							47
						});
					}
					else
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							3,
							47,
							48
						});
					}
					break;
				case UiState.ARTIFACTS_INFO:
					this.panelArtifactsInfo.stateToReturn = this._state;
					if (this._state == UiState.ARTIFACTS)
					{
						this.OpenMenu(value, false, 3, 3, new int[]
						{
							4,
							28
						});
					}
					else
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							4,
							47,
							48
						});
					}
					break;
				case UiState.SHOP_LOOTPACK_SELECT:
					this.shopLootpackSelect.previousState = this._state;
					if (this._state == UiState.CHRISTMAS_PANEL)
					{
						this.OpenMenu(value, this.IsInHubMenus(this.panelChristmasOffer.previousState), -1, -1, new int[]
						{
							63,
							5
						});
					}
					else if (this._state == UiState.SECOND_ANNIVERSARY_POPUP)
					{
						if (this.secondAnniversaryPopup.previousState == UiState.NONE)
						{
							this.OpenMenu(value, false, -1, -1, new int[]
							{
								76,
								5
							});
						}
						else if (this.secondAnniversaryPopup.previousState == UiState.SHOP)
						{
							this.OpenMenu(value, false, 4, 4, new int[]
							{
								76,
								5
							});
						}
						else
						{
							if (this.secondAnniversaryPopup.previousState != UiState.HUB_SHOP)
							{
								throw new NotImplementedException("No flow implemented to SeconAnniversaryPopup from " + this.secondAnniversaryPopup.previousState);
							}
							this.OpenMenu(value, true, -1, -1, new int[]
							{
								49,
								76,
								5
							});
						}
					}
					else if (this.panelShop.isHubMode)
					{
						this.OpenMenu(value, true, -1, 4, new int[]
						{
							5,
							49
						});
					}
					else
					{
						UiManager.zeroScrollViewContentY = false;
						this.OpenMenu(value, false, 4, 4, new int[]
						{
							5
						});
					}
					this.shopLootpackSelect.popupRect.DoPopupAnimation();
					this.shopLootpackSelect.timer = 0f;
					break;
				case UiState.SHOP_LOOTPACK_SUMMARY:
					if (this.panelShop.isHubMode)
					{
						this.OpenMenu(value, true, -1, 4, new int[]
						{
							6,
							49
						});
					}
					else
					{
						UiManager.zeroScrollViewContentY = false;
						this.OpenMenu(value, false, 4, 4, new int[]
						{
							6
						});
					}
					this.shopLootpackSummary.timer = 0f;
					this.shopLootpackSummary.popupRect.DoPopupAnimation();
					break;
				case UiState.HUB_OPTIONS:
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						19
					});
					UiManager.stateJustChanged = true;
					this.hubOptions.panelAdvancedOptions.OrderWidgetPositions(this.hubOptions.GetNumNotificationsAvailable());
					break;
				case UiState.MODE_PRESTIGE:
					this.OpenMenu(value, false, 1, 1, new int[]
					{
						7
					});
					this.panelPrestige.SetSmallInfo();
					if (this.sim.lastPrestigeRunstats == null || this.sim.numPrestiges == 0)
					{
						this.panelPrestige.SetWithoutLastRunInfo();
					}
					else if (this.sim.numPrestiges < 3)
					{
						this.panelPrestige.SetPreLastRunInfo();
					}
					else
					{
						this.panelPrestige.SetWithLastRunInfo();
					}
					this.panelPrestige.popupRect.DoPopupAnimation();
					break;
				case UiState.MODE_UNLOCK_REWARD:
					this.panelUnlockReward.heroAnimation.OnClose();
					this.OpenMenu(value, false, -1, -1, new int[]
					{
						8
					});
					this.panelUnlockReward.timer = 0f;
					this.panelUnlockReward.fadingOut = false;
					this.panelUnlockReward.returnToStateAfterFadeOut = false;
					this.panelUnlockReward.SetStartPosition();
					this.panelUnlockReward.FadeIn();
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiUnlockPopup, 1f));
					break;
				case UiState.MODE_MERCHANT_ITEM_SELECT:
					this.OpenMenu(value, false, 1, 1, new int[]
					{
						9
					});
					this.panelMerchantItemSelect.contentParent.DoPopupAnimation();
					if (this.sim.IsMultiMerchantEnabled())
					{
						this.panelMerchantItemSelect.multiBuyParent.gameObject.SetActive(true);
					}
					else
					{
						this.panelMerchantItemSelect.multiBuyParent.gameObject.SetActive(false);
					}
					this.panelMerchantItemSelect.copyAmount = 1;
					break;
				case UiState.OFFLINE_EARNINGS:
					this.panelOfflineEarnings.timer = 0f;
					this.OpenMenu(value, false, -1, -1, new int[]
					{
						10
					});
					this.panelOfflineEarnings.popupRect.DoPopupAnimation();
					break;
				case UiState.HUB_MODE_SETUP_TOTEM:
					this.panelSelectTotem.selected = -1;
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						14,
						12
					});
					this.panelSelectTotem.popupRect.DoPopupAnimation();
					break;
				case UiState.SHOP_LOOTPACK_OPENING:
					if (this._state == UiState.SHOP_LOOTPACK_SELECT)
					{
						if (this.panelShop.isHubMode)
						{
							this.OpenMenu(value, true, -1, 4, new int[]
							{
								13,
								5,
								49
							});
							this.shopLootpackOpening.onCompledeFadeIn = delegate()
							{
								this.menuSubContents[5].gameObject.SetActive(false);
								this.menuSubContents[49].gameObject.SetActive(false);
								this.menuContents[4].gameObject.SetActive(false);
							};
						}
						else
						{
							UiManager.zeroScrollViewContentY = false;
							this.OpenMenu(value, false, 4, 4, new int[]
							{
								13,
								5
							});
							this.shopLootpackOpening.onCompledeFadeIn = delegate()
							{
								this.menuSubContents[5].gameObject.SetActive(false);
								this.menuContents[4].gameObject.SetActive(false);
							};
						}
					}
					else if (this.panelShop.isHubMode)
					{
						this.OpenMenu(value, true, -1, 4, new int[]
						{
							13,
							49
						});
						this.shopLootpackOpening.onCompledeFadeIn = delegate()
						{
							this.menuSubContents[49].gameObject.SetActive(false);
							this.menuContents[4].gameObject.SetActive(false);
						};
					}
					else
					{
						UiManager.zeroScrollViewContentY = false;
						this.OpenMenu(value, false, 4, 4, new int[]
						{
							13
						});
						this.shopLootpackOpening.onCompledeFadeIn = delegate()
						{
							this.menuContents[4].gameObject.SetActive(false);
						};
					}
					break;
				case UiState.HUB_MODE_SETUP:
					this.isScrollViewLocked = true;
					this.SetModeSetupButtons();
					this.panelHubModeSetup.challengeIndexChanged = (this._state != UiState.HUB_MODE_SETUP_HERO && this._state != UiState.HUB_MODE_SETUP_TOTEM && this._state != UiState.RIFT_EFFECTS_INFO);
					if (this.panelHubModeSetup.heroAnimation.gameObject.activeSelf)
					{
						this.panelHubModeSetup.heroAnimation.Reload();
					}
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						14
					});
					break;
				case UiState.HUB_MODE_SETUP_HERO:
					this.panelNewHero.heroTransitionAnimation.OnClose();
					this.panelNewHero.objectButton = this.panelNewHero.buttonSelectHero.gameObject;
					this.panelNewHero.selected = -1;
					this.panelNewHero.SetScrollToZero();
					this.panelNewHero.inputBlocker.SetActive(false);
					this.panelNewHero.shownHeroes.Clear();
					this.panelNewHero.popupRect.DoPopupAnimation();
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						14,
						11
					});
					break;
				case UiState.CHALLENGE_WIN:
				{
					this.OpenMenu(value, false, -1, -1, new int[]
					{
						15
					});
					this.panelChallengeWin.timer = 0f;
					this.panelChallengeWin.fadingOut = false;
					ChallengeRift challengeRift = this.sim.GetActiveWorld().activeChallenge as ChallengeRift;
					this.panelChallengeWin.isRift = this.sim.IsActiveMode(GameMode.RIFT);
					this.panelChallengeWin.isCursed = (challengeRift != null && challengeRift.riftData.cursesSetup != null);
					this.panelChallengeWin.SetStartPosition();
					this.panelChallengeWin.FadeIn();
					break;
				}
				case UiState.CHALLENGE_LOSE:
					this.OpenMenu(value, false, -1, -1, new int[]
					{
						16
					});
					this.panelChallengeLose.timer = 0f;
					this.panelChallengeLose.isRift = this.sim.IsActiveMode(GameMode.RIFT);
					if (this.panelChallengeLose.isRift)
					{
						World world = this.sim.GetWorld(GameMode.RIFT);
						if (world.sameRiftLoseCount >= 3)
						{
							this.panelChallengeLose.showHint = true;
							world.sameRiftLoseCount = 0;
						}
						else
						{
							this.panelChallengeLose.showHint = false;
						}
					}
					else
					{
						World world2 = this.sim.GetWorld(GameMode.CRUSADE);
						if (world2.sameTimeChallengeLoseCount >= 3 || this.sim.timeChallengesLostCount == 1)
						{
							this.panelChallengeLose.showHint = true;
							world2.sameTimeChallengeLoseCount = 0;
						}
						else
						{
							this.panelChallengeLose.showHint = false;
						}
					}
					this.panelChallengeLose.SetStartPosition();
					this.panelChallengeLose.FadeIn();
					break;
				case UiState.HEROES_RUNES:
					this.isScrollViewLocked = true;
					this.OpenMenu(value, false, 2, 7, new int[0]);
					break;
				case UiState.HEROES_EVOLVE:
					this.panelHeroEvolveSkin.state = PanelHeroEvolveSkin.State.APPEAR;
					this.panelHeroEvolveSkin.stateBeforeOpening = this._state;
					this.OpenMenu(value, false, 2, 5, new int[]
					{
						0,
						17
					});
					break;
				case UiState.AD_POPUP:
					if (this.panelShop.isHubMode)
					{
						this.OpenMenu(value, false, -1, 4, new int[]
						{
							20,
							49
						});
					}
					else
					{
						this.OpenMenu(value, false, -1, -1, new int[]
						{
							20
						});
					}
					this.panelAdPopup.timer = 0f;
					this.panelAdPopup.popupRect.DoPopupAnimation();
					break;
				case UiState.HUB_DATABASE_HEROES_ITEMS:
					if (this._state == UiState.HUB)
					{
						this.panelHubDatabase.panelGear.OnClose();
						this.panelHeroEvolveSkin.heroAnimation.OnClose();
						this.panelHeroSkinChanger.heroAnimation.OnClose();
						this.panelSkinBuyingPopup.heroAnimation.OnClose();
					}
					if (!this.panelHubDatabase.didSetScroll)
					{
						this.panelHubDatabase.didSetScroll = true;
						this.panelHubDatabase.heroesScrollRect.horizontalNormalizedPosition = 0f;
					}
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						21,
						22
					});
					this.panelHubDatabase.buttonTabHeroesItems.interactable = false;
					this.panelHubDatabase.buttonTabRings.interactable = true;
					this.panelHubDatabase.buttonTabHeroesSkills.interactable = true;
					break;
				case UiState.HUB_DATABASE_TOTEMS:
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						21,
						23
					});
					this.panelHubDatabase.buttonTabHeroesItems.interactable = true;
					this.panelHubDatabase.buttonTabRings.interactable = false;
					this.panelHubDatabase.buttonTabHeroesSkills.interactable = true;
					break;
				case UiState.HUB_DATABASE_TRINKETS:
					this.SetTrinketSmitherParent(0);
					this.panelTrinketSmithing.isBattlefield = false;
					if (this.panelHubTrinkets.isSmithingTab)
					{
						this.panelHubTrinkets.buttonTabSmithing.interactable = false;
						this.panelHubTrinkets.buttonTabTrinkets.interactable = true;
						this.panelHubTrinkets.smithingTabParent.gameObject.SetActive(true);
						this.panelHubTrinkets.trinketTabParent.gameObject.SetActive(false);
					}
					else
					{
						this.panelHubTrinkets.buttonTabSmithing.interactable = true;
						this.panelHubTrinkets.buttonTabTrinkets.interactable = false;
						this.panelHubTrinkets.smithingTabParent.gameObject.SetActive(false);
						this.panelHubTrinkets.trinketTabParent.gameObject.SetActive(true);
						this.SetTrinketScrollParent(2);
						this.panelTrinketsScroller.InitScroll(this.sim.numTrinketSlots, new Action<int>(this.OnTrinketSelectedHandler), new Action(this.OnMultipleTrinketDisassemble));
						this.panelTrinketsScroller.UpdateLastTrinkets(this.sim.GetSortedTrinkets());
						this.panelDatabaseTrinket.selected = -1;
						this.panelHubTrinkets.backgroundImage.color = this.panelHubTrinkets.trinketTabColor;
						if (this._state == UiState.TRINKET_INFO_POPUP)
						{
							this.panelTrinketsScroller.FocusOnTrinketIfNecessary(this.panelTrinketInfoPopup.trinketInfoBody.selectedTrinket);
						}
					}
					if (this._state != UiState.TRINKET_EFFECT_SELECT_POPUP)
					{
						this.panelTrinketSmithing.CancelCrafting();
					}
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						35
					});
					break;
				case UiState.HUB_DATABASE_EVOLVE:
					this.panelHeroEvolveSkin.state = PanelHeroEvolveSkin.State.APPEAR;
					this.panelHeroEvolveSkin.stateBeforeOpening = this._state;
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						21,
						22,
						17
					});
					break;
				case UiState.CURRENCY_WARNING:
					this.currencyWarningInHub = false;
					if (this._state == UiState.ARTIFACTS)
					{
						this.OpenMenu(value, false, 3, 3, new int[]
						{
							24,
							28
						});
					}
					else if (this._state == UiState.MODE)
					{
						this.OpenMenu(value, false, 1, 1, new int[]
						{
							24
						});
					}
					else if (this._state == UiState.MODE_MERCHANT_ITEM_SELECT)
					{
						this.OpenMenu(value, false, 1, 1, new int[]
						{
							9,
							24
						});
					}
					else if (this._state == UiState.SHOP_TRINKET_OPEN_POPUP)
					{
						if (this.panelShop.isHubMode)
						{
							this.OpenMenu(value, true, -1, -1, new int[]
							{
								49,
								38,
								24
							});
							this.currencyWarningInHub = true;
						}
						else
						{
							UiManager.zeroScrollViewContentY = false;
							this.OpenMenu(value, false, 4, 4, new int[]
							{
								38,
								24
							});
						}
					}
					else if (this._state == UiState.HEROES_GEAR)
					{
						this.OpenMenu(value, false, 2, 5, new int[]
						{
							24
						});
					}
					else if (this._state == UiState.HUB_DATABASE_HEROES_ITEMS)
					{
						this.OpenMenu(value, false, -1, -1, new int[]
						{
							21,
							22,
							24
						});
						this.currencyWarningInHub = true;
					}
					else if (this._state == UiState.HUB_ARTIFACTS)
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							47,
							48,
							24
						});
						this.currencyWarningInHub = true;
					}
					else if (this._state == UiState.SHOP_CHARM_PACK_SELECT)
					{
						if (this.panelShop.isHubMode)
						{
							this.OpenMenu(value, true, -1, -1, new int[]
							{
								49,
								51,
								24
							});
							this.currencyWarningInHub = true;
						}
						else
						{
							UiManager.zeroScrollViewContentY = false;
							this.OpenMenu(value, false, 4, 4, new int[]
							{
								51,
								24
							});
						}
					}
					else if (this._state == UiState.CHRISTMAS_PANEL)
					{
						this.OpenMenu(value, false, -1, -1, new int[]
						{
							63,
							24
						});
					}
					else if (this._state == UiState.ARTIFACT_SELECTED_POPUP)
					{
						if (this.IsInHubMenus(this.panelArtifactPopup.stateToReturn))
						{
							this.OpenMenu(value, true, -1, -1, new int[]
							{
								47,
								48,
								71,
								24
							});
							this.currencyWarningInHub = true;
						}
						else
						{
							this.OpenMenu(value, false, 3, 3, new int[]
							{
								47,
								71,
								24
							});
						}
					}
					else if (this._state == UiState.CHANGE_SKIN)
					{
						if (this.panelHeroSkinChanger.oldState == UiState.HEROES_GEAR)
						{
							this.OpenMenu(value, false, 2, 1, new int[]
							{
								39,
								24
							});
						}
						else
						{
							if (this.panelHeroSkinChanger.oldState != UiState.HUB_DATABASE_HEROES_ITEMS)
							{
								throw new NotImplementedException("Flow to currency warning not implemented from CHANGE_SKIN with old state " + this.panelHeroSkinChanger.oldState);
							}
							this.OpenMenu(value, true, -1, -1, new int[]
							{
								21,
								39,
								24
							});
							this.currencyWarningInHub = true;
						}
					}
					this.panelCurrencyWarning.previousState = this._state;
					break;
				case UiState.HUB_CREDITS:
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						26
					});
					break;
				case UiState.UPDATE_REQUIRED:
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						27
					});
					this.panelUpdateRequired.timer = 0f;
					break;
				case UiState.GENERAL_POPUP:
					if (this.panelGeneralPopup.state == PanelGeneralPopup.State.NONE)
					{
						this.OpenMenu(value, false, -1, -1, new int[]
						{
							29
						});
					}
					else if (this.panelGeneralPopup.state == PanelGeneralPopup.State.OPTIONS)
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							19,
							29
						});
					}
					else if (this.panelGeneralPopup.state == PanelGeneralPopup.State.HARD_RESET)
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							19,
							29,
							30
						});
					}
					else if (this.panelGeneralPopup.state == PanelGeneralPopup.State.MODE)
					{
						this.OpenMenu(value, false, 1, 1, new int[]
						{
							29
						});
					}
					else if (this.panelGeneralPopup.state == PanelGeneralPopup.State.SHOP)
					{
						if (this.panelShop.isHubMode)
						{
							this.OpenMenu(value, true, -1, -1, new int[]
							{
								29,
								49
							});
						}
						else
						{
							UiManager.zeroScrollViewContentY = false;
							this.OpenMenu(value, false, 4, 4, new int[]
							{
								29
							});
						}
					}
					else if (this.panelGeneralPopup.state == PanelGeneralPopup.State.SERVER_REWARD)
					{
						this.OpenMenu(value, false, -1, -1, new int[]
						{
							25,
							29
						});
					}
					else if (this.panelGeneralPopup.state == PanelGeneralPopup.State.SERVER_REWARD_HUB)
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							18,
							25,
							29
						});
					}
					else if (this.panelGeneralPopup.state == PanelGeneralPopup.State.SELECT_TRINKET)
					{
						if (this._state == UiState.HUB_DATABASE_TRINKETS)
						{
							this.OpenMenu(value, true, -1, 5, new int[]
							{
								31,
								32,
								29,
								34
							});
						}
						else
						{
							this.OpenMenu(value, false, 2, 5, new int[]
							{
								31,
								32,
								29,
								34
							});
						}
					}
					else if (this.panelGeneralPopup.state == PanelGeneralPopup.State.DATABASE_TRINKET)
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							21,
							35,
							29,
							34
						});
					}
					else if (this.panelGeneralPopup.state == PanelGeneralPopup.State.HUB_DAILY_SKIP)
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							33,
							35,
							29
						});
					}
					else if (this.panelGeneralPopup.state == PanelGeneralPopup.State.TRINKET_INFO_POPUP)
					{
						if (this.panelTrinketInfoPopup.stateToReturn == UiState.HUB_DATABASE_TRINKETS)
						{
							this.OpenMenu(value, true, -1, -1, new int[]
							{
								21,
								35,
								59,
								29,
								34
							});
						}
						else if (this.panelTrinketInfoPopup.stateToReturn == UiState.SELECT_TRINKET)
						{
							this.OpenMenu(value, false, 2, 5, new int[]
							{
								31,
								32,
								59,
								29,
								34
							});
						}
					}
					else if (this.panelGeneralPopup.state == PanelGeneralPopup.State.CHRISTMAS_SHOP)
					{
						this.OpenMenu(value, this.IsInHubMenus(this.panelChristmasOffer.previousState), -1, -1, new int[]
						{
							63,
							29
						});
					}
					else
					{
						if (this.panelGeneralPopup.state != PanelGeneralPopup.State.DAILY_QUEST)
						{
							throw new NotImplementedException(string.Concat(new object[]
							{
								"Incomming state: ",
								value,
								" General popup State: ",
								this.panelGeneralPopup.state
							}));
						}
						this.OpenMenu(value, false, -1, -1, new int[]
						{
							29,
							68
						});
					}
					this.panelGeneralPopup.rectPopup.DoPopupAnimation();
					break;
				case UiState.HEROES_TRINKETS:
					this.isScrollViewLocked = true;
					this.panelGearScreen.buttonTab.interactable = true;
					this.panelSkillsScreen.buttonTab.interactable = true;
					this.panelTrinketsScreen.buttonTab.interactable = false;
					this.OpenMenu(value, false, 2, 5, new int[]
					{
						31
					});
					break;
				case UiState.SELECT_TRINKET:
					this.isScrollViewLocked = true;
					this.panelSelectTrinket.selected = -1;
					this.SetTrinketScrollParent(1);
					this.SetTrinketSmitherParent(1);
					this.panelTrinketSmithing.isBattlefield = true;
					if (this._state != UiState.TRINKET_EFFECT_SELECT_POPUP)
					{
						this.panelTrinketSelect.isSmithing = false;
						this.panelTrinketSmithing.CancelCrafting();
					}
					this.panelTrinketSelect.trinketScrollerParent.gameObject.SetActive(!this.panelTrinketSelect.isSmithing);
					this.panelTrinketSelect.trinketSmitherParent.gameObject.SetActive(this.panelTrinketSelect.isSmithing);
					this.panelTrinketSelect.background.rectTransform.DoPopupAnimation();
					this.panelTrinketSelect.background.color = ((!this.panelTrinketSelect.isSmithing) ? this.panelTrinketSelect.trinketTabColor : this.panelTrinketSelect.smithTabColor);
					this.panelTrinketsScroller.InitScroll(this.sim.numTrinketSlots, new Action<int>(this.OnTrinketSelectedHandler), new Action(this.OnMultipleTrinketDisassemble));
					this.panelTrinketsScroller.UpdateLastTrinkets(this.sim.GetSortedTrinkets());
					if (this._state == UiState.TRINKET_INFO_POPUP)
					{
						this.panelTrinketsScroller.FocusOnTrinketIfNecessary(this.panelTrinketInfoPopup.trinketInfoBody.selectedTrinket);
					}
					this.panelTrinketsScroller.gameObject.SetActive(true);
					if (this.selectedHeroGearSkills != null)
					{
						int i = 0;
						int count = this.sim.allTrinkets.Count;
						while (i < count)
						{
							if (this.selectedHeroGearSkills.trinket == this.sim.allTrinkets[i])
							{
								this.panelSelectTrinket.selected = i;
								break;
							}
							i++;
						}
					}
					this.OpenMenu(value, false, 2, 5, new int[]
					{
						31,
						32
					});
					break;
				case UiState.HUB_ACHIEVEMENTS:
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						33
					});
					this.panelAchievements.OnPanelOpened(this.sim);
					this.panelAchievements.GoToAchievementsTab();
					this.panelAchievements.scrollview.verticalNormalizedPosition = 1f;
					if (!TrustedTime.IsReady())
					{
						PlayfabManager.GetTime(delegate(bool isSuccess, DateTime time)
						{
							if (isSuccess)
							{
								TrustedTime.Init(time);
							}
						});
					}
					break;
				case UiState.SHOP_MINE:
					this.panelMine.stateToReturn = this._state;
					if (this._state == UiState.SHOP)
					{
						UiManager.zeroScrollViewContentY = false;
						this.OpenMenu(value, false, 4, 4, new int[]
						{
							36
						});
					}
					else if (this._state == UiState.HUB_SHOP)
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							4,
							36,
							49
						});
					}
					this.panelMine.popupRect.DoPopupAnimation();
					break;
				case UiState.SUPPORT_POPUP:
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						19,
						37
					});
					this.supportPopup.popupRect.DoPopupAnimation();
					break;
				case UiState.SHOP_TRINKET_OPEN_POPUP:
					this.panelTrinketPopup.stateToReturn = this._state;
					if (this.panelShop.isHubMode)
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							38,
							49
						});
					}
					else
					{
						UiManager.zeroScrollViewContentY = false;
						this.OpenMenu(value, false, 4, 4, new int[]
						{
							38
						});
					}
					this.shopTrinketPackSelect.popupRect.DoPopupAnimation();
					break;
				case UiState.CHANGE_SKIN:
				{
					this.panelHeroSkinChanger.updateDetails = true;
					this.sim.isSkinsEverClicked = true;
					bool flag = false;
					if (this._state != UiState.SKIN_BUYING_WARNING && this._state != UiState.CHRISTMAS_PANEL && this._state != UiState.CURRENCY_WARNING)
					{
						this.panelHeroSkinChanger.oldState = this._state;
					}
					HashSet<string> unlockedHeroIds = this.sim.GetUnlockedHeroIds();
					List<HeroDataBase> list = this.SortHeroesByUnlocked(this.sim.GetAllHeroes(), unlockedHeroIds);
					if (this.state == UiState.HEROES_GEAR)
					{
						this.OpenMenu(value, false, 2, 1, new int[]
						{
							39
						});
						this.panelHeroSkinChanger.selectedHero = this.selectedHeroGearSkills.GetData().GetDataBase();
					}
					else if (this.state == UiState.HUB_DATABASE_HEROES_ITEMS)
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							21,
							39
						});
						this.panelHeroSkinChanger.selectedHero = list[this.panelHubDatabase.selectedHero];
					}
					else if (this.state == UiState.HEROES_EVOLVE)
					{
						this.panelHeroSkinChanger.oldState = UiState.HEROES_GEAR;
						this.OpenMenu(value, false, 2, 1, new int[]
						{
							39
						});
						this.panelHeroSkinChanger.selectedHero = this.selectedHeroGearSkills.GetData().GetDataBase();
					}
					else if (this.state == UiState.HUB_DATABASE_EVOLVE)
					{
						this.panelHeroSkinChanger.oldState = UiState.HUB_DATABASE_HEROES_ITEMS;
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							21,
							39
						});
						this.panelHeroSkinChanger.selectedHero = list[this.panelHubDatabase.selectedHero];
					}
					else if (this.state == UiState.SKIN_BUYING_WARNING || this.state == UiState.CURRENCY_WARNING)
					{
						if (this.panelHeroSkinChanger.oldState == UiState.HEROES_GEAR)
						{
							this.OpenMenu(value, false, 2, 1, new int[]
							{
								39
							});
							this.panelHeroSkinChanger.selectedHero = this.selectedHeroGearSkills.GetData().GetDataBase();
							flag = true;
						}
						else if (this.panelHeroSkinChanger.oldState == UiState.HUB_DATABASE_HEROES_ITEMS)
						{
							this.OpenMenu(value, true, -1, -1, new int[]
							{
								21,
								39
							});
							this.panelHeroSkinChanger.selectedHero = list[this.panelHubDatabase.selectedHero];
							flag = true;
						}
					}
					else if (this.state == UiState.CHRISTMAS_PANEL)
					{
						if (this.IsInHubMenus(this.panelChristmasOffer.previousState))
						{
							this.OpenMenu(value, true, -1, -1, new int[]
							{
								21,
								39
							});
						}
						else
						{
							this.OpenMenu(value, false, 2, 1, new int[]
							{
								39
							});
						}
						flag = true;
					}
					if (!flag)
					{
						List<SkinData> heroSkins = this.sim.GetHeroSkins(this.panelHeroSkinChanger.selectedHero.id);
						heroSkins.Sort(new Comparison<SkinData>(this.SkinDataComparer));
						int selectedSkinIndex = (!this.panelHeroSkinChanger.selectedHero.randomSkinsEnabled) ? (heroSkins.IndexOf(this.panelHeroSkinChanger.selectedHero.equippedSkin) + 1) : 0;
						int count2 = heroSkins.Count;
						this.panelHeroSkinChanger.SetScrollContentSize(count2);
						this.panelHeroSkinChanger.SetSelectedSkinIndex(selectedSkinIndex);
					}
					this.panelHeroSkinChanger.popupRect.DoPopupAnimation();
					break;
				}
				case UiState.SHOP_SKINPACK_OPENING:
					if (!this.panelShop.isHubMode)
					{
						UiManager.zeroScrollViewContentY = false;
					}
					this.OpenMenu(value, false, 4, 4, new int[]
					{
						40,
						5
					});
					break;
				case UiState.SKIN_BUYING_WARNING:
				{
					SkinData selectedSkin = this.panelHeroSkinChanger.selectedSkin;
					if (selectedSkin.belongsTo.id == "DRUID")
					{
						this.panelSkinBuyingPopup.heroAnimation.ShowTransformedVersion(this.sim.GetHeroSkins(selectedSkin.belongsTo.id).Count);
					}
					else
					{
						this.panelSkinBuyingPopup.heroAnimation.DontShowTransformedVersion();
					}
					this.panelSkinBuyingPopup.heroAnimation.SetHeroAnimation(selectedSkin.belongsTo.id, selectedSkin.index, false, false, true, true);
					this.panelSkinBuyingPopup.heroName.text = selectedSkin.GetName();
					this.panelSkinBuyingPopup.popupParent.DoPopupAnimation();
					if (this.panelHeroSkinChanger.oldState == UiState.HEROES_GEAR)
					{
						this.OpenMenu(value, false, 2, 1, new int[]
						{
							39,
							41
						});
					}
					else if (this.panelHeroSkinChanger.oldState == UiState.HUB_DATABASE_HEROES_ITEMS)
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							21,
							39,
							41
						});
					}
					break;
				}
				case UiState.XIAOMI_WARNING_POPUP:
					if (this._state == UiState.NONE)
					{
						this.xiaomiWarningPopup.oldState = UiState.NONE;
						this.OpenMenu(value, false, -1, -1, new int[]
						{
							42
						});
					}
					else if (this._state == UiState.HUB)
					{
						this.xiaomiWarningPopup.oldState = UiState.HUB;
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							18,
							42
						});
					}
					break;
				case UiState.RIFT_SELECT_POPUP:
					if (this._state != UiState.RIFT_EFFECTS_INFO)
					{
						World world3 = this.sim.GetWorld(GameMode.RIFT);
						int activeChallengeIndex = world3.GetActiveChallengeIndex();
						bool flag2 = world3.IsActiveChallengeCursed();
						if (!flag2)
						{
							this.panelRiftSelect.SetUnhiddenChallengeCount(this.sim.GetDiscoveredRiftCount());
						}
						else
						{
							this.panelRiftSelect.SetUnhiddenChallengeCount(this.sim.cursedRiftSlots.GetMaxSlotCount());
							if (this.panelRiftSelect.currentNormalSelection == -1)
							{
								this.panelRiftSelect.currentNormalSelection = this.sim.lastSelectedRegularGateIndex;
							}
						}
						if (this.sim.GetWorld(GameMode.RIFT).cursedChallenges.Count == 0)
						{
							this.panelRiftSelect.currentCurseSelection = -1;
						}
						else
						{
							this.panelRiftSelect.currentCurseSelection = this.sim.GetWorld(GameMode.RIFT).cursedChallenges.IndexOf(this.sim.GetWorld(GameMode.RIFT).activeChallenge);
						}
						this.panelRiftSelect.OnModeChange(flag2);
						this.panelRiftSelect.SetScrollPosition(activeChallengeIndex);
						this.panelRiftSelect.OnSelecRift(activeChallengeIndex);
						this.panelRiftSelect.SetButtonSelected(activeChallengeIndex);
						this.CalculateRiftDifficulties();
					}
					this.panelRiftSelect.popupRect.DoPopupAnimation();
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						14,
						43
					});
					break;
				case UiState.CHARM_SELECTING:
					if (this.panelCharmSelect.isCurseInfo)
					{
						this.panelCharmSelect.header.text = LM.Get("CURRENT_CURSES");
						this.panelCharmSelect.selectButtonText.text = LM.Get("UI_OKAY");
						this.panelCharmSelect.selectButton.interactable = true;
						this.command = new UiCommandSetPauseState
						{
							isPaused = true
						};
						this.panelCharmSelect.count = this.sim.currentCurses.Count;
						this.panelCharmSelect.HideCards();
						this.panelCharmSelect.DoBringCards();
					}
					else
					{
						this.panelCharmSelect.header.text = LM.Get("CHARM_SELECT_HEADER");
						this.panelCharmSelect.selectButtonText.text = LM.Get("UI_SELECT");
						this.command = new UiCommandGetNextCharmDraft();
						World world4 = this.sim.GetWorld(GameMode.RIFT);
						this.panelCharmSelect.count = world4.GetCharmSelectionNum();
						this.panelCharmSelect.HideCards();
						this.panelCharmSelect.DoBringCards();
						this.panelCharmSelect.selectedIndex = -1;
					}
					this.OpenMenu(value, false, -1, -1, new int[]
					{
						44
					});
					break;
				case UiState.RIFT_RUN_CHARMS:
				{
					this.isScrollViewLocked = false;
					World world5 = this.sim.GetWorld(GameMode.RIFT);
					ChallengeRift challengeRift2 = world5.activeChallenge as ChallengeRift;
					if (challengeRift2.IsCursed())
					{
						this.panelRunningCharms.riftName.text = string.Format(LM.Get("RIFT_RUN_NAME_EFFECTS"), challengeRift2.riftData.cursesSetup.originalRiftNo + 1);
					}
					else
					{
						this.panelRunningCharms.riftName.text = string.Format(LM.Get("RIFT_RUN_NAME_EFFECTS"), world5.GetActiveChallengeIndex() + 1);
					}
					this.panelRunningCharms.SetRiftEffects(this.sim.GetActiveWorld().activeChallenge as ChallengeRift, this);
					this.OpenMenu(value, false, 3, 8, new int[0]);
					break;
				}
				case UiState.HUB_CHARMS:
					this.panelHubCharms.forceUpdate = true;
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						45
					});
					break;
				case UiState.CHARM_INFO_POPUP:
				{
					if (this.panelCharmInfoPopup.sequence != null && this.panelCharmInfoPopup.sequence.IsPlaying())
					{
						this.panelCharmInfoPopup.sequence.Complete(true);
					}
					UniversalTotalBonus universalBonus = this.sim.GetWorld(GameMode.RIFT).universalBonus;
					this.panelHubCharms.bonusDamageLastAmount = universalBonus.charmDamageFactor;
					this.panelHubCharms.bonusGoldLastAmount = universalBonus.charmGoldFactor;
					this.panelHubCharms.bonusHealthLastAmount = universalBonus.charmHealthFactor;
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						45,
						46
					});
					break;
				}
				case UiState.HUB_ARTIFACTS:
					this.panelArtifactScroller.OnScreenAppear();
					if (this._state == UiState.ARTIFACTS_CRAFT && this.panelArtifactsCraft.stoneBoxArtifactsScreen != null)
					{
						this.panelArtifactsCraft.stoneBoxArtifactsScreen.gameObject.SetActive(true);
					}
					this.panelArtifactScroller.ResetScreenAnim();
					this.panelArtifactScroller.craftButton.gameObject.SetActive(true);
					this.panelArtifactScroller.unlockButtonWaitingForConfirm = false;
					if (this.panelArtifactScroller.selectedArtifactIndex != -1 && this._state != UiState.CURRENCY_WARNING && this._state != UiState.ARTIFACTS_INFO && this._state != UiState.POSSIBLE_ARTIFACT_EFFECTS_POPUP)
					{
						this.panelArtifactScroller.ResetArtifactSelected();
					}
					this.panelArtifactScroller.CalculateContentSize(this.sim.artifactsManager.AvailableSlotsCount);
					this.panelArtifactScroller.multipleBuyIndex = this.sim.artifactMultiUpgradeIndex;
					this.UpdateArtifactScroller();
					this.SetArtifactScrollParent(2);
					if (this._state != UiState.ARTIFACTS_CRAFT && this._state != UiState.ARTIFACTS_REROLL && this._state != UiState.ARTIFACTS_INFO)
					{
						this.panelArtifactScroller.SetScrollPosition(0f, false);
						this.panelArtifactScroller.OnScrollValueChanged(Vector2.zero);
					}
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						47,
						48
					});
					if (this._state == UiState.NEW_TAL_MILESTONE_REACHED_POPUP)
					{
						this.panelArtifactScroller.PlayNewTALMilestoneReachedAnim();
					}
					break;
				case UiState.HUB_SHOP:
					this.panelShop.setSpecialOfferContentTexts = true;
					this.panelShop.forceGoblinAnimation = !this.IsShopState(this._state);
					this.panelShop.isHubMode = true;
					this.OpenMenu(value, true, -1, 4, new int[]
					{
						49
					});
					this.SetShopParent(2);
					this.panelShop.childrenContentSizeFitter.SetSize(0f, false);
					if (!TrustedTime.IsReady())
					{
						PlayfabManager.GetTime(delegate(bool isSuccess, DateTime time)
						{
							if (isSuccess)
							{
								TrustedTime.Init(time);
							}
						});
					}
					RewardedAdManager.inst.OnShopOpened();
					break;
				case UiState.BUY_FLASH_OFFER_CHARM:
				{
					this.panelBuyCharmFlashOffer.previousState = this._state;
					this.panelBuyCharmFlashOffer.CancelPurchaseAnimIfNecessary();
					this.panelBuyCharmFlashOffer.DoComeIn();
					UiState previousState = this.panelBuyCharmFlashOffer.previousState;
					if (previousState != UiState.SHOP)
					{
						if (previousState != UiState.HUB_SHOP)
						{
							if (previousState != UiState.CHRISTMAS_PANEL)
							{
								throw new NotImplementedException(string.Concat(new object[]
								{
									"Incomming state: ",
									value,
									" Previous State: ",
									this.panelBuyCharmFlashOffer.previousState
								}));
							}
							this.OpenMenu(value, this.IsInHubMenus(this.panelChristmasOffer.previousState), -1, -1, new int[]
							{
								63,
								50
							});
						}
						else
						{
							this.OpenMenu(value, true, -1, 4, new int[]
							{
								49,
								50
							});
						}
					}
					else
					{
						UiManager.zeroScrollViewContentY = false;
						this.OpenMenu(value, false, 4, 4, new int[]
						{
							50
						});
					}
					break;
				}
				case UiState.BUY_FLASH_OFFER:
				{
					if (this._state != UiState.BUY_FLASH_OFFER)
					{
						this.panelBuyAdventureFlashOffer.previousState = this._state;
					}
					else
					{
						this.panelBuyAdventureFlashOffer.previousState = UiState.HUB_SHOP;
					}
					this.panelBuyAdventureFlashOffer.CancelPurchaseAnimIfNecessary();
					this.panelBuyAdventureFlashOffer.costumeAnimation.OnClose();
					this.panelBuyAdventureFlashOffer.popupParent.DoPopupAnimation();
					UiState previousState2 = this.panelBuyAdventureFlashOffer.previousState;
					if (previousState2 != UiState.SHOP)
					{
						if (previousState2 != UiState.HUB_SHOP)
						{
							if (previousState2 != UiState.CHRISTMAS_PANEL)
							{
								if (previousState2 != UiState.SECOND_ANNIVERSARY_POPUP)
								{
									throw new NotImplementedException(string.Concat(new object[]
									{
										"Incomming state: ",
										value,
										" Previous State: ",
										this.panelBuyAdventureFlashOffer.previousState
									}));
								}
								if (this.secondAnniversaryPopup.previousState == UiState.NONE)
								{
									this.OpenMenu(value, false, -1, -1, new int[]
									{
										76,
										56
									});
								}
								else if (this.secondAnniversaryPopup.previousState == UiState.SHOP)
								{
									this.OpenMenu(value, false, 4, 4, new int[]
									{
										76,
										56
									});
								}
								else
								{
									if (this.secondAnniversaryPopup.previousState != UiState.HUB_SHOP)
									{
										throw new NotImplementedException("No flow implemented to SeconAnniversaryPopup from " + this.secondAnniversaryPopup.previousState);
									}
									this.OpenMenu(value, true, -1, -1, new int[]
									{
										49,
										76,
										56
									});
								}
							}
							else
							{
								this.OpenMenu(value, this.IsInHubMenus(this.panelChristmasOffer.previousState), -1, -1, new int[]
								{
									63,
									56
								});
							}
						}
						else
						{
							this.OpenMenu(value, true, -1, 4, new int[]
							{
								49,
								56
							});
						}
					}
					else
					{
						UiManager.zeroScrollViewContentY = false;
						this.OpenMenu(value, false, 4, 4, new int[]
						{
							56
						});
					}
					break;
				}
				case UiState.SHOP_CHARM_PACK_SELECT:
					if (this.panelShop.isHubMode)
					{
						this.OpenMenu(value, true, -1, 4, new int[]
						{
							49,
							51
						});
					}
					else
					{
						UiManager.zeroScrollViewContentY = false;
						this.OpenMenu(value, false, 4, 4, new int[]
						{
							51
						});
					}
					this.panelCharmPackSelect.popupRect.DoPopupAnimation();
					break;
				case UiState.SHOP_CHARM_PACK_OPENING:
					if (this.panelShop.isHubMode)
					{
						this.OpenMenu(value, true, -1, 4, new int[]
						{
							49,
							52
						});
					}
					else
					{
						UiManager.zeroScrollViewContentY = false;
						this.OpenMenu(value, false, 4, 4, new int[]
						{
							52
						});
					}
					break;
				case UiState.RIFT_EFFECTS_INFO:
					this.panelRiftEffectInfo.stateToReturn = this._state;
					if (this.panelRiftEffectInfo.stateToReturn == UiState.RIFT_SELECT_POPUP)
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							14,
							43,
							53
						});
						this.panelRiftEffectInfo.SetCurseffects(this);
					}
					else
					{
						World world6 = this.sim.GetWorld(GameMode.RIFT);
						this.panelRiftEffectInfo.SetRiftEffects(world6.activeChallenge as ChallengeRift, this);
						if (this._state == UiState.HUB_MODE_SETUP)
						{
							this.OpenMenu(value, true, -1, -1, new int[]
							{
								14,
								53
							});
						}
						else if (this._state == UiState.RIFT_RUN_CHARMS)
						{
							this.OpenMenu(value, false, 3, 8, new int[]
							{
								53
							});
						}
					}
					this.panelRiftEffectInfo.popupParent.DoPopupAnimation();
					break;
				case UiState.SOCIAL_REWARD_POPUP:
					if (this._state == UiState.SHOP)
					{
						UiManager.zeroScrollViewContentY = false;
						this.OpenMenu(value, false, 4, 4, new int[]
						{
							54
						});
					}
					else
					{
						this.OpenMenu(value, this._state == UiState.HUB_SHOP, -1, 4, new int[]
						{
							49,
							54
						});
					}
					this.panelSocialRewardPopup.popupRect.DoPopupAnimation();
					break;
				case UiState.RATE_POPUP:
					if (this._state == UiState.HUB)
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							55,
							18
						});
					}
					else
					{
						this.OpenMenu(value, false, -1, -1, new int[]
						{
							55
						});
					}
					break;
				case UiState.HUB_OPTIONS_WIKI:
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						57,
						19
					});
					this.hubOptionsWiki.popupRect.DoPopupAnimation();
					break;
				case UiState.OFFER_POPUP:
					this.OpenMenu(value, false, -1, -1, new int[]
					{
						58
					});
					this.panelOfferPopup.popupRect.DoPopupAnimation();
					break;
				case UiState.TRINKET_INFO_POPUP:
					this.panelTrinketInfoPopup.trinketInfoBody.forceToUpdate = true;
					if (this.panelTrinketInfoPopup.isReturningBack)
					{
						if (this.panelTrinketInfoPopup.stateToReturn == UiState.HUB_DATABASE_TRINKETS)
						{
							this.OpenMenu(value, true, -1, -1, new int[]
							{
								21,
								35,
								59
							});
						}
						else if (this.panelTrinketInfoPopup.stateToReturn == UiState.SELECT_TRINKET)
						{
							this.OpenMenu(value, false, 2, 5, new int[]
							{
								31,
								32,
								59
							});
						}
					}
					else if (this._state == UiState.HUB_DATABASE_TRINKETS)
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							21,
							35,
							59
						});
					}
					else if (this._state == UiState.SELECT_TRINKET)
					{
						this.OpenMenu(value, false, 2, 5, new int[]
						{
							31,
							32,
							59
						});
					}
					if (!this.panelTrinketInfoPopup.isReturningBack)
					{
						this.panelTrinketInfoPopup.stateToReturn = this._state;
					}
					this.panelTrinketInfoPopup.popupRect.DoPopupAnimation();
					this.panelTrinketInfoPopup.isReturningBack = false;
					break;
				case UiState.SAVE_CONFLICT_POPUP:
					this.OpenMenu(value, false, -1, -1, new int[]
					{
						60
					});
					break;
				case UiState.TRINKET_RECYCLE_POPUP:
					if (this.panelTrinketRecycle.previousState == UiState.TRINKET_INFO_POPUP)
					{
						if (this.panelTrinketInfoPopup.stateToReturn == UiState.HUB_DATABASE_TRINKETS)
						{
							this.OpenMenu(value, true, -1, -1, new int[]
							{
								59,
								61
							});
						}
						else
						{
							this.OpenMenu(value, false, -1, 2, new int[]
							{
								59,
								61
							});
						}
					}
					else if (this._state == UiState.HUB_DATABASE_TRINKETS)
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							35,
							61
						});
					}
					else
					{
						this.OpenMenu(value, false, -1, 2, new int[]
						{
							32,
							61
						});
					}
					this.panelTrinketRecycle.popupRect.DoPopupAnimation();
					break;
				case UiState.TRINKET_EFFECT_SELECT_POPUP:
					this.panelTrinketEffectScroller.trinketEffects = this.sim.disassembledTinketEffects;
					this.panelTrinketEffectScroller.UpdateScrollElementCount(this.panelTrinketEffectScroller.trinketEffects.Count);
					if (this.panelTrinketSmithing.isBattlefield)
					{
						this.OpenMenu(value, false, 2, 5, new int[]
						{
							31,
							32,
							62
						});
					}
					else
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							35,
							62
						});
					}
					this.panelTrinketEffectScroller.popupRect.DoPopupAnimation();
					break;
				case UiState.CHRISTMAS_PANEL:
					if (this._state == UiState.NONE || this._state == UiState.HUB)
					{
						this.panelChristmasOffer.GoToTreeTab();
					}
					this.panelChristmasOffer.PlayOfferPurchasedAnimationIfNecessary();
					this.OpenMenu(value, this.IsInHubMenus(this.panelChristmasOffer.previousState), -1, -1, new int[]
					{
						63
					});
					if (!TrustedTime.IsReady())
					{
						PlayfabManager.GetTime(delegate(bool isSuccess, DateTime time)
						{
							if (isSuccess)
							{
								TrustedTime.Init(time);
							}
						});
					}
					break;
				case UiState.CHRISTMAS_OFFERS_INFO_POPUP:
					this.OpenMenu(value, this.IsInHubMenus(this.panelChristmasOffer.previousState), -1, -1, new int[]
					{
						63,
						64
					});
					this.panelChristmasOffersInfo.popupRect.DoPopupAnimation();
					break;
				case UiState.CHRISTMAS_CANDY_TREAT_POPUP:
					this.OpenMenu(value, this.IsInHubMenus(this.panelChristmasOffer.previousState), -1, -1, new int[]
					{
						63,
						65
					});
					this.popupChristmasCandyTreat.popupRect.DoPopupAnimation();
					break;
				case UiState.CHRISTMAS_EVENT_POPUP:
					this.OpenMenu(value, false, -1, -1, new int[]
					{
						66
					});
					this.panelChristmasEventPopup.popupParent.DoPopupAnimation();
					break;
				case UiState.PATCH_NOTES:
					this.OpenMenu(value, false, -1, -1, new int[]
					{
						19,
						67
					});
					this.versionNotesPopup.popupRect.DoPopupAnimation();
					break;
				case UiState.DAILY_QUEST:
					this.OpenMenu(value, false, -1, -1, new int[]
					{
						68
					});
					break;
				case UiState.HUB_DATABASE_HEROES_SKILLS:
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						21,
						22
					});
					this.panelHubDatabase.panelSkills.ResetSelectedSkill();
					this.panelHubDatabase.buttonTabHeroesItems.interactable = true;
					this.panelHubDatabase.buttonTabRings.interactable = true;
					this.panelHubDatabase.buttonTabHeroesSkills.interactable = false;
					break;
				case UiState.SHARE_SCREENSHOT_PANEL:
					this.sim.isActiveWorldPaused = true;
					this.panelShareScreenshot.backToModePanel = (this._state == UiState.MODE);
					this.OpenMenu(value, false, -1, -1, new int[]
					{
						69
					});
					break;
				case UiState.CONVERT_XMAS_SCRAP:
					this.OpenMenu(value, false, -1, -1, new int[]
					{
						70
					});
					this.panelScrapConverter.SetValues(this.sim);
					this.panelScrapConverter.buttonConvert.transform.parent.SetScale(1f);
					break;
				case UiState.ARTIFACT_SELECTED_POPUP:
				{
					UiState uiState;
					if (this._state == UiState.NEW_TAL_MILESTONE_REACHED_POPUP || this._state == UiState.POSSIBLE_ARTIFACT_EFFECTS_POPUP || this._state == UiState.CURRENCY_WARNING || this._state == UiState.ARTIFACT_EVOLVE || this._state == UiState.CAN_NOT_EVOLVE_ARTIFACT_POPUP)
					{
						uiState = this.panelArtifactPopup.stateToReturn;
					}
					else
					{
						uiState = this._state;
						this.panelArtifactPopup.stateToReturn = this._state;
					}
					if (uiState == UiState.ARTIFACTS)
					{
						this.OpenMenu(value, false, 3, 3, new int[]
						{
							47,
							71
						});
					}
					else if (uiState == UiState.HUB_ARTIFACTS)
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							47,
							48,
							71
						});
					}
					this.panelArtifactPopup.manager = this;
					this.panelArtifactPopup.multipleBuyIndex = this.sim.artifactMultiUpgradeIndex;
					this.panelArtifactPopup.selectedArtifactIndex = this.panelArtifactScroller.selectedArtifactIndex;
					this.panelArtifactPopup.SetArtifact(true);
					if (this._state == UiState.NEW_TAL_MILESTONE_REACHED_POPUP)
					{
						this.panelArtifactPopup.PlayNewTALMilestoneReachedAnim();
					}
					break;
				}
				case UiState.NEW_TAL_MILESTONE_REACHED_POPUP:
				{
					this.newTALMilestoneReachedPopup.OnPopupAppeared(this._state, this.sim);
					bool flag3 = this.IsInHubMenus(this._state);
					UiState state2 = this._state;
					if (state2 != UiState.ARTIFACTS)
					{
						if (state2 != UiState.HUB_ARTIFACTS)
						{
							if (state2 != UiState.ARTIFACT_SELECTED_POPUP)
							{
								if (flag3)
								{
									this.OpenMenu(value, true, -1, -1, new int[]
									{
										18,
										72
									});
								}
								else
								{
									this.OpenMenu(value, false, -1, -1, new int[]
									{
										72
									});
								}
							}
							else if (flag3)
							{
								this.OpenMenu(value, true, -1, -1, new int[]
								{
									47,
									48,
									71,
									72
								});
							}
							else
							{
								this.OpenMenu(value, false, 3, 3, new int[]
								{
									47,
									71,
									72
								});
							}
						}
						else
						{
							this.OpenMenu(value, true, -1, -1, new int[]
							{
								47,
								48,
								72
							});
						}
					}
					else
					{
						this.OpenMenu(value, false, 3, 3, new int[]
						{
							47,
							72
						});
					}
					break;
				}
				case UiState.POSSIBLE_ARTIFACT_EFFECTS_POPUP:
					this.posibleArtifactEffectPopup.manager = this;
					if (this._state == UiState.ARTIFACT_SELECTED_POPUP)
					{
						UiState stateToReturn = this.panelArtifactPopup.stateToReturn;
						this.posibleArtifactEffectPopup.stateToReturn = UiState.ARTIFACT_SELECTED_POPUP;
						if (stateToReturn == UiState.ARTIFACTS)
						{
							this.OpenMenu(value, false, 3, 3, new int[]
							{
								47,
								71,
								73
							});
						}
						else if (stateToReturn == UiState.HUB_ARTIFACTS)
						{
							this.OpenMenu(value, true, -1, -1, new int[]
							{
								47,
								48,
								71,
								73
							});
						}
					}
					else
					{
						if (this._state == UiState.ARTIFACTS)
						{
							this.OpenMenu(value, false, 3, 3, new int[]
							{
								47,
								73
							});
						}
						else if (this._state == UiState.HUB_ARTIFACTS)
						{
							this.OpenMenu(value, true, -1, -1, new int[]
							{
								47,
								48,
								73
							});
						}
						this.posibleArtifactEffectPopup.stateToReturn = this._state;
					}
					this.posibleArtifactEffectPopup.SetPossibleEffects();
					break;
				case UiState.ARTIFACT_EVOLVE:
				{
					Simulation.ArtifactSystem.Artifact artifact = this.sim.artifactsManager.Artifacts[this.panelArtifactPopup.selectedArtifactIndex];
					this.artifactEvolveWindow.SetArtifact(artifact, this);
					if (this.IsInHubMenus(this.panelArtifactPopup.stateToReturn))
					{
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							74
						});
					}
					else
					{
						this.OpenMenu(value, false, 3, 3, new int[]
						{
							74
						});
					}
					break;
				}
				case UiState.ARTIFACT_OVERHAUL:
					this.OpenMenu(value, true, -1, -1, new int[]
					{
						18,
						75
					});
					this.artifactOverhaulPopup.OnPopupAppeared();
					break;
				case UiState.SECOND_ANNIVERSARY_POPUP:
					this.command = new UiCommandLookedAtOutOfShopOffers();
					if (this.secondAnniversaryPopup.previousState == UiState.NONE)
					{
						this.OpenMenu(value, false, -1, -1, new int[]
						{
							76
						});
					}
					else if (this.secondAnniversaryPopup.previousState == UiState.SHOP)
					{
						this.OpenMenu(value, false, 4, 4, new int[]
						{
							76
						});
					}
					else
					{
						if (this.secondAnniversaryPopup.previousState != UiState.HUB_SHOP)
						{
							throw new NotImplementedException("No flow implemented to SeconAnniversaryPopup from " + this.secondAnniversaryPopup.previousState);
						}
						this.OpenMenu(value, true, -1, -1, new int[]
						{
							49,
							76
						});
					}
					if (!TrustedTime.IsReady())
					{
						PlayfabManager.GetTime(delegate(bool isSuccess, DateTime time)
						{
							if (isSuccess)
							{
								TrustedTime.Init(time);
							}
						});
					}
					break;
				case UiState.CAN_NOT_EVOLVE_ARTIFACT_POPUP:
				{
					this.notEvolveArtifactPopup.OnPopupAppeared(this.sim.artifactsManager);
					this.notEvolveArtifactPopup.previousState = this._state;
					UiState state3 = this._state;
					if (state3 != UiState.ARTIFACTS)
					{
						if (state3 != UiState.HUB_ARTIFACTS)
						{
							if (state3 != UiState.ARTIFACT_SELECTED_POPUP)
							{
								throw new NotImplementedException("Not implemented transition to CanNotEvolveArtifactPopup from " + this._state);
							}
							if (this.panelArtifactPopup.stateToReturn == UiState.ARTIFACTS)
							{
								this.OpenMenu(value, false, 3, 3, new int[]
								{
									47,
									71,
									77
								});
							}
							else
							{
								this.OpenMenu(value, true, -1, -1, new int[]
								{
									47,
									48,
									71,
									77
								});
							}
						}
						else
						{
							this.OpenMenu(value, true, -1, -1, new int[]
							{
								47,
								48,
								77
							});
						}
					}
					else
					{
						this.OpenMenu(value, false, 3, 3, new int[]
						{
							47,
							77
						});
					}
					break;
				}
				}
				if ((this._state == UiState.ARTIFACTS_CRAFT || this._state == UiState.ARTIFACTS_REROLL) && value != UiState.ARTIFACTS_CRAFT && value != UiState.ARTIFACTS_REROLL)
				{
					UiManager.sounds.Add(new SoundEventCancelBy("ALCHEMY_AMBIENT"));
				}
				string menuHeaderText = this.GetMenuHeaderText(value);
				this.textMenuHeader.text = menuHeaderText;
				UiManager.zeroScrollViewContentY = true;
				this._state = value;
				this.scrollView.enabled = !this.isScrollViewLocked;
			}
		}

		private void CalculateRiftDifficulties()
		{
			World world = this.sim.GetWorld(GameMode.RIFT);
			this.riftDiffsNormal = new List<RiftDifficulty>();
			for (int i = 0; i < world.allChallenges.Count; i++)
			{
				ChallengeRift rift = world.allChallenges[i] as ChallengeRift;
				this.riftDiffsNormal.Add(RiftUtility.GetDifficulty(this.sim, world, rift, this.sim.GetUniversalBonusRift()));
			}
			this.riftDiffsCursed = new List<RiftDifficulty>();
			for (int j = 0; j < world.cursedChallenges.Count; j++)
			{
				ChallengeRift rift2 = world.cursedChallenges[j] as ChallengeRift;
				this.riftDiffsCursed.Add(RiftUtility.GetDifficulty(this.sim, world, rift2, this.sim.GetUniversalBonusRift()));
			}
		}

		private bool IsShopState(UiState state)
		{
			return state == UiState.SHOP_MINE || state == UiState.SHOP_CHARM_PACK_SELECT || (state == UiState.SHOP_LOOTPACK_SELECT && (this.shopLootpackSelect.previousState == UiState.HUB_SHOP || this.shopLootpackSelect.previousState == UiState.SHOP)) || state == UiState.SHOP_CHARM_PACK_OPENING || state == UiState.SHOP_LOOTPACK_OPENING || state == UiState.SHOP_LOOTPACK_SUMMARY || state == UiState.SHOP_SKINPACK_OPENING || state == UiState.SHOP_TRINKET_OPEN_POPUP || state == UiState.BUY_FLASH_OFFER_CHARM || state == UiState.BUY_FLASH_OFFER || (state == UiState.SOCIAL_REWARD_POPUP && (this.panelSocialRewardPopup.previousState == UiState.SHOP || this.panelSocialRewardPopup.previousState == UiState.HUB_SHOP));
		}

		private UiCommand command
		{
			get
			{
				return this._command;
			}
			set
			{
				this._command = value;
				if (value != null)
				{
					UiManager.stateJustChanged = true;
				}
			}
		}

		public RectTransform rectTransform
		{
			get
			{
				RectTransform result;
				if ((result = this.m_rectTransform) == null)
				{
					result = (this.m_rectTransform = base.GetComponent<RectTransform>());
				}
				return result;
			}
		}

		public void RegisterObjects(RectTransform rt)
		{


            foreach(RectTransform rectTransform in rt)
            {
                AahMonoBehaviour[] components = rectTransform.GetComponents<AahMonoBehaviour>();
                foreach (AahMonoBehaviour aahMonoBehaviour in components)
                {
                    aahMonoBehaviour.Register();
                }
                this.RegisterObjects(rectTransform);
            }

			
		}

		private void RegisterObjects(List<AahMonoBehaviour> aahMono)
		{
			foreach (AahMonoBehaviour aahMonoBehaviour in aahMono)
			{
				aahMonoBehaviour.Register();
			}
		}

		private List<AahMonoBehaviour> DefaultAahMonoBehaviours(RectTransform rt)
		{
			List<AahMonoBehaviour> list = new List<AahMonoBehaviour>();

            foreach(RectTransform rectTransform in rt) {

                AahMonoBehaviour[] components = rectTransform.GetComponents<AahMonoBehaviour>();
                list.AddRange(components);
                list.AddRange(this.DefaultAahMonoBehaviours(rectTransform));
            }

			return list;
		}

		public void CallToInits()
		{
			if (UiManager.toInits.Count <= 0)
			{
				return;
			}
           
            
            foreach (AahMonoBehaviour aahMonoBehaviour in UiManager.toInits)
            {
                aahMonoBehaviour.Init();
            }
            UiManager.toInits = new List<AahMonoBehaviour>();
        }

		public void CallToUpdates(float dt)
		{
			foreach (AahMonoBehaviour item in UiManager.willRemoveFromToUpdates)
			{
				UiManager.toUpdates.RemoveFast(item);
			}
			UiManager.willRemoveFromToUpdates.Clear();
			for (int i = UiManager.toUpdates.Count - 1; i >= 0; i--)
			{
				AahMonoBehaviour aahMonoBehaviour = UiManager.toUpdates[i];
				if (aahMonoBehaviour.softGameObject.activeInHierarchy)
				{
					aahMonoBehaviour.AahUpdate(dt);
				}
			}
		}

		public void HandleInstantiate(GameObject go)
		{
			AahMonoBehaviour[] components = go.GetComponents<AahMonoBehaviour>();
			foreach (AahMonoBehaviour aahMonoBehaviour in components)
			{
				aahMonoBehaviour.Register();
			}
			this.CallToInits();
		}

		public void Init(Simulator sim, SoundManager soundManager)
		{
			this.sim = sim;
			this.soundManager = soundManager;
			UiManager.DEBUGDontShowTransitionInEditor = false;
			UiManager._panelCurrencyOnTop = this.panelCurrencyOnTop;
			UiManager._panelCurrencySides = this.panelCurrencySides;
			this.currentGraphicRaycasters = new List<GraphicRaycaster>();
			this.menuContentsGR = new GraphicRaycaster[this.menuContents.Length];
			int i = 0;
			int num = this.menuContents.Length;
			while (i < num)
			{
				this.menuContentsGR[i] = ((!(this.menuContents[i] != null)) ? null : this.menuContents[i].GetComponent<GraphicRaycaster>());
				i++;
			}
			this.menuSubContentsGR = new GraphicRaycaster[this.menuSubContents.Length];
			int j = 0;
			int num2 = this.menuSubContents.Length;
			while (j < num2)
			{
				this.menuSubContentsGR[j] = ((!(this.menuSubContents[j] != null)) ? null : this.menuSubContents[j].GetComponent<GraphicRaycaster>());
				j++;
			}
			int maxNumOfUnlocks = sim.GetMaxNumOfUnlocks();
			this.InitUiData();
            UiManager.colorCantAfford = this.colorCantAffordInit;
            UiManager.colorHeroLevels = this.colorHeroLevelsInit;
            UiManager.colorArtifactLevels = this.colorArtifactLevelsInit;
            UiManager.colorTrinketLevels = this.colorTrinketLevelsInit;
            UiManager.colorCurrencyTypes = this.colorCurrencyTypesInit;
            this.secondAnniversaryPopup.manager = this;
            this.notEvolveArtifactPopup.manager = this;
            this.InitUnlockPanels(maxNumOfUnlocks);
            this.InitAchievements();
            UiManager.toInits = new List<AahMonoBehaviour>();
            UiManager.willRemoveFromToUpdates = new List<AahMonoBehaviour>();
            UiManager.toUpdates = new List<AahMonoBehaviour>();
            this.monoBehaviours = UiManager.toUpdates;
            this.RegisterObjects(base.GetComponent<RectTransform>());
            this.panelRiftSelect.uiManager = this;
            this.CallToInits();
            this.panelHubCharms.FillCharmsScroll(new Action<int>(this.Button_OnClickHubCharm));
            this.scrollViewRect = this.scrollView.GetComponent<RectTransform>();
            this.InitSkillButtons();
            this.InitTabBarButtons();
            this.InitPanelHeroes();
            this.InitNewHero();
            this.InitSkills();
            this.InitGear();
            this.InitHeroSkinChanger();
            this.InitShop();
            this.InitHubOptions();
            this.InitHubOptionsWiki();
            this.InitMode();
            this.InitMerchantItemSelect();
            this.InitOfflineEarnings();
            this.InitHubModeSetup();
            this.InitRunes();
            this.InitAdPopup();
            this.InitSocialRewardPopup();
            this.InitTutorial();
            this.InitDatabase();
            this.InitTrinkets();
            this.InitTrinketSmithing();
            this.InitTrinketEffectScroller();
            this.InitSelectTrinket();
            this.InitMine();
            this.InitSupportPopup();
            this.InitArtifactReroll();
            this.InitSkinBuyingPopup();
            this.InitRiftSelectPopup();
            this.InitPanelCharmSelect();
            this.InitPanelCharmPackSelect();
            this.InitPanelCharmPackOpening();
            this.InitArtifactScroller();
            this.InitHubCharms();
            this.InitPanelBuyFlashCharmPopup();
            this.InitPanelBuyFlashOfferPopup();
            this.InitRunCharms();
            this.InitRatePopup();
            this.InitHubScreen();
            this.InitPanelOfferPopup();
            this.InitTrinketScroller();
            this.InitPanelTrinketRecyclePopup();
            this.InitPanelTrinketInfoPopup();
            this.InitTrinketSelect();
            this.InitChristmasOffersPanel();
            this.InitChristmasCandyTreatPopup();
            this.InitChristmasOffersInfoPopup();
            this.InitChristmasEventPopup();
            this.InitChristmasScrapConverterPopup();
            this.InitCurrencyWarningPopup();
            this.InitPanelShareScreenshot();
            this.InitPopupNewTALMilestoneReached();
            this.artifactOverhaulPopup.manager = this;
            this.panelArtifactsCraft.uiManager = this;
            this.openOfferPopupButton.onClick = new GameButton.VoidFunc(this.OnOpenOfferPopupButtonClicked);
            this.panelTopHudChallengeTimeWave.openRiftEffectsInfoButton.onClick.AddListener(delegate ()
            {
                this.state = UiState.RIFT_RUN_CHARMS;
                this.state = UiState.RIFT_EFFECTS_INFO;
                UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
                UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiOpenTabClick, 1f));
            });
            this.panelTopHudChallengeTimeWave.openCurseEffectsInfoButton.onClick.AddListener(delegate ()
            {
                this.panelCharmSelect.isCurseInfo = true;
                this.panelCharmSelect.skipAnimation = true;
                this.state = UiState.CHARM_SELECTING;
            });
            this.panelHeroEvolveSkin.buttonClose.onClick = delegate ()
            {
                this.OnClickedPanelHeroEvolve();
            };
            this.panelHeroEvolveSkin.uiManager = this;
            this.panelHubartifacts.buttonBack.onClick = delegate ()
            {
                this.state = UiState.HUB;
                UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
            };
            this.panelCredits.buttonBack.onClick = delegate ()
            {
                this.state = UiState.HUB_OPTIONS;
                UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
            };
            this.panelHubCharms.backButton.onClick = delegate ()
            {
                this.state = UiState.HUB;
                UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
            };
            this.panelArtifactsInfo.OnClose = delegate ()
            {
                this.state = this.panelArtifactsInfo.stateToReturn;
                UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
            };
            this.panelCharmInfoPopup.buttonBack.onClick = (this.panelCharmInfoPopup.buttonBackX.onClick = delegate ()
            {
                this.state = UiState.HUB_CHARMS;
                UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
            });
            this.panelCharmInfoPopup.upgradeButton.gameButton.onClick = delegate ()
            {
                this.Button_OnClickCharmUpgrade();
                UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiWorldUpgrade, 1f));
            };
            this.charmSelectWidget.button.onClick = delegate ()
            {
                this.panelCharmSelect.isCurseInfo = false;
                this.state = UiState.CHARM_SELECTING;
                UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiWorldUpgrade, 1f));
            };
            this.panelRiftEffectInfo.buttonOkay.onClick = (this.panelRiftEffectInfo.buttonBackground.onClick = delegate ()
            {
                this.state = this.panelRiftEffectInfo.stateToReturn;
                UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
            });
            this.panelChallengeWin.button.onClick = delegate ()
            {
                this.OnClickedChallengeWinCollect();
            };
            this.panelChallengeLose.button.onClick = delegate ()
            {
                this.OnClickedChallengeLoseOk();
            };
            this.panelChallengeWin.shareButton.onClick = new GameButton.VoidFunc(this.OpenScreenshotPanel);
            this.panelUpdateRequired.gameButton.onClick = delegate ()
            {
                this.OnClickedUpdateRequiredOkay();
            };
            this.dailyQuestIndicatorWidget.button.onClick = delegate ()
            {
                this.OnClickedDailyQuestWidget();
            };
            this.EnableSingleMenuContent(-1, new int[0]);
            this.volumeMusicBoss = 0f;
            this.panelTransitionFade.gameObject.SetActive(true);
            this.loadingTransition.gameObject.SetActive(true);
            this.loadingTransition.SetAnimationType(LoadingTransition.TransitionAnimType.Sword);
            this.loadingTransition.GetCurrentTransitionAnim().GetFadeInAnimation().Complete(true);
            this.buttonDebug.gameObject.SetActive(false);
            if (sim.IsGameModeAlreadySetup(sim.GetActiveWorld().gameMode) && !OldArtifactsConverter.DoesPlayerNeedsConversion(sim) && (TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.FIN || TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.BEFORE_BEGIN))
            {
                this.InitStringsTabBarButtons();
                this.state = UiState.NONE;
            }
            else
            {
                UiState uiState = (!OldArtifactsConverter.DoesPlayerNeedsConversion(sim) && TutorialManager.artifactOverhaul != TutorialManager.ArtifactOverhaul.FIN && TutorialManager.artifactOverhaul != TutorialManager.ArtifactOverhaul.BEFORE_BEGIN) ? UiState.HUB_ARTIFACTS : UiState.HUB;
                if (this.IsInHubMenus())
                {
                    this.state = uiState;
                }
                else
                {
                    DynamicLoadManager.CancelPendingRequests();
                    this.loadingTransition.DoTransition(uiState, 0f, 0f);
                }
            }
            this.isInHubMenusOld = !this.IsInHubMenus();
            this.musicStandardExistsOld = false;
            this.panelArtifactsRerollWindow.uiManager = this;
            this.heroUnlockHintKeys = sim.GetHeroUnlockHintStrings();
            this.panelNewHero.uiManager = this;
            this.heroSkillInstanceParams = new HeroSkillInstanceParams();
            if (PlayerPrefs.HasKey("localpatchnotes"))
            {
                string @string = PlayerPrefs.GetString("localpatchnotes");
                PatchNote[] notes = JsonConvert.DeserializeObject<PatchNote[]>(@string);
                PatchNote.InitPatchNotes(notes);
            }
        }

		private void OnOpenOfferPopupButtonClicked()
		{
			if (this.sim.IsSecondAnniversaryEventEnabled())
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiOpenTabClick, 1f));
				this.secondAnniversaryPopup.previousState = UiState.NONE;
				this.state = UiState.SECOND_ANNIVERSARY_POPUP;
			}
			else if (this.sim.IsChristmasTreeEnabled())
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiOpenTabClick, 1f));
				this.panelChristmasOffer.previousState = this.state;
				this.state = UiState.CHRISTMAS_PANEL;
			}
			else
			{
				SpecialOfferKeeper specialOfferKeeper = this.sim.specialOfferBoard.CurrentAnnouncingOffer(this.sim);
				ShopPack shopPack = null;
				if (specialOfferKeeper != null)
				{
					shopPack = specialOfferKeeper.offerPack;
				}
				else if (this.sim.halloweenEnabled)
				{
					shopPack = ShopPack.GetShopPacksByTag(ShopPack.Tags.SEASONAL)[0];
				}
				if (shopPack != null)
				{
					this.panelOfferPopup.SetOffer(shopPack);
					this.sim.OnNewOfferAnnounced(shopPack);
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiOpenTabClick, 1f));
					this.state = UiState.OFFER_POPUP;
				}
			}
		}

		private void SetModeButton(ButtonGameMode buttonGameModeStandard, GameMode gameMode)
		{
			buttonGameModeStandard.buttonInfo.onClick = delegate()
			{
				if (buttonGameModeStandard.IsInfoEnabled())
				{
					buttonGameModeStandard.Disableinfo();
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOff, 1f));
				}
				else
				{
					buttonGameModeStandard.Enableinfo();
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOn, 1f));
				}
			};
			buttonGameModeStandard.gameButton.onClick = delegate()
			{
				if (buttonGameModeStandard.IsInfoEnabled())
				{
					buttonGameModeStandard.Disableinfo();
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOff, 1f));
				}
				else
				{
					this.OnClickedGameMode(gameMode);
				}
			};
		}

		private void InitSkinBuyingPopup()
		{
			this.panelSkinBuyingPopup.buttonNo.onClick = delegate()
			{
				this.state = UiState.CHANGE_SKIN;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
			};
			this.panelSkinBuyingPopup.buttonYes.gameButton.onClick = new GameButton.VoidFunc(this.BuySelectedSkin);
		}

		private void InitArtifactReroll()
		{
			this.panelArtifactsRerollWindow.artifactWidget1.button.Register();
			this.panelArtifactsRerollWindow.artifactWidget1.artifactStone.Register();
			this.panelArtifactsRerollWindow.artifactWidget2.button.Register();
			this.panelArtifactsRerollWindow.artifactWidget2.artifactStone.Register();
		}

		private void OnCickedToggleDebugButton()
		{
			this.debugButtonsEnabled = !this.debugButtonsEnabled;
		}

		private void OnClickedDailyQuestWidget()
		{
			SoundEventCancelAll item = new SoundEventCancelAll();
			UiManager.sounds.Add(item);
			this.state = UiState.DAILY_QUEST;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void InitSupportPopup()
		{
			this.supportPopup.reportBug.onClick = new GameButton.VoidFunc(this.Button_SupportReportBug);
			this.supportPopup.reportPaymentIssue.onClick = new GameButton.VoidFunc(this.Button_SupportPaymentIssue);
			this.supportPopup.feedback.onClick = new GameButton.VoidFunc(this.Button_SupportFeedback);
			this.supportPopup.close.onClick = new GameButton.VoidFunc(this.Button_SupportClosePopup);
		}

		private void InitRunCharms()
		{
			this.panelRunningCharms.buttonRiftEffectsInfo.onClick = new GameButton.VoidFunc(this.OpenRiftEffectsInfoPopup);
			this.panelRunningCharms.openRiftEffectsInfoButton.onClick.AddListener(new UnityAction(this.OpenRiftEffectsInfoPopup));
		}

		private void InitRatePopup()
		{
			this.panelRatePopup.buttonRate.onClick = new GameButton.VoidFunc(this.OnRateAcceptButtonClicked);
			this.panelRatePopup.buttonAskLater.onClick = new GameButton.VoidFunc(this.OnRateAskLaterButtonClicked);
			this.panelRatePopup.buttonDontAskAgain.onClick = new GameButton.VoidFunc(this.OnRateDontAskAgainButtonClicked);
		}

		private void InitHubScreen()
		{
			this.panelHub.buttonAchievements.onClick = delegate()
			{
				this.state = UiState.HUB_ACHIEVEMENTS;
				this.panelAchievements.lastState = UiState.HUB;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.SetModeButton(this.panelHub.buttonGameModeStandard, GameMode.STANDARD);
			this.SetModeButton(this.panelHub.buttonGameModeCrusade, GameMode.CRUSADE);
			this.SetModeButton(this.panelHub.buttonGameModeRift, GameMode.RIFT);
			this.panelHub.riftQuestSlider.SetSlider(this.sim.riftQuestDustCollected, this.sim.GetRiftQuestDustRequired(), false);
			this.panelHub.buttonGameModeRift.dontAutoEnableBar = true;
			this.panelHub.buttonCollectRiftReward.onClick = delegate()
			{
				this.command = new UiCommandCollectRiftReward
				{
					invTransform = this.panelCurrencyOnTop[0].panelCurrency.GetCurrencyTransform(),
					startPos = this.panelHub.buttonCollectRiftReward.transform.position
				};
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackSelect[2], 1f));
			};
			this.panelHub.buttonHeroes.onClick = delegate()
			{
				this.state = UiState.HUB_DATABASE_HEROES_ITEMS;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelHub.buttonTrinkets.onClick = delegate()
			{
				this.panelHubTrinkets.isSmithingTab = false;
				this.state = UiState.HUB_DATABASE_TRINKETS;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelHub.buttonArtifacts.onClick = delegate()
			{
				this.state = UiState.HUB_ARTIFACTS;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelHub.buttonOptions.onClick = delegate()
			{
				this.state = UiState.HUB_OPTIONS;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelHub.buttonCharms.onClick = delegate()
			{
				this.state = UiState.HUB_CHARMS;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelHub.buttonShop.onClick = delegate()
			{
				this.panelShop.ResetScrollPosition();
				this.panelShop.isLookingAtOffers = false;
				this.panelShop.focusOnFlashOffers = false;
				this.panelShop.setSpecialOfferContentTexts = true;
				this.state = UiState.HUB_SHOP;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelHub.openChristmasPopupButton.onClick = delegate()
			{
				this.panelChristmasOffer.previousState = this.state;
				this.state = UiState.CHRISTMAS_PANEL;
				UiManager.AddUiSound(SoundArchieve.inst.uiPopupAppear);
			};
		}

		private void OpenRiftEffectsInfoPopup()
		{
			this.state = UiState.RIFT_EFFECTS_INFO;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
		}

		private void InitSkillButtons()
		{
			this.buttonSkillAnims = new List<ButtonSkillAnim>();
			for (int i = 0; i < this.buttonSkills.Count; i++)
			{
				this.buttonSkillAnims.Add(this.buttonSkills[i].GetComponent<ButtonSkillAnim>());
			}
			for (int j = this.buttonSkills.Count - 1; j >= 0; j--)
			{
				GameButton gameButton = this.buttonSkills[j];
				int ic = j;
				gameButton.onClick = delegate()
				{
					this.OnClickedButtonSkill(ic);
				};
			}
			this.panelSkillsScreen.buttonTab.onDown = delegate()
			{
				this.state = UiState.HEROES_SKILL;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelSkillsScreen.buttonHeroNext.onClick = delegate()
			{
				this.OnClickedSkillsScreenNextHero(true);
			};
		}

		private void InitTabBarButtons()
		{
			this.buttonMenuBack.onClick = delegate()
			{
				this.OnClickedMenuBack();
			};
			this.buttonMenuTopBack.onClick = delegate()
			{
				this.state = UiState.NONE;
			};
			for (int i = 0; i < this.tabBarButtons.Count; i++)
			{
				ButtonTabBar buttonTabBar = this.tabBarButtons[i];
				int ic = i;
				if (ic == 0)
				{
					buttonTabBar.gameButton.onClick = delegate()
					{
						this.OnClickedButtonTabBar(ic);
					};
				}
				else
				{
					buttonTabBar.gameButton.onDown = delegate()
					{
						this.OnClickedButtonTabBar(ic);
					};
				}
			}
			this.InitStringsTabBarButtons();
			this.openTab = -1;
		}

		private void InitStringsTabBarButtons()
		{
			this.tabBarButtons[1].text.text = LM.Get("UI_TAB_MODE");
			this.tabBarButtons[2].text.text = LM.Get("UI_TAB_HEROES");
			if (this.sim.IsActiveMode(GameMode.RIFT))
			{
				this.tabBarButtons[3].text.text = LM.Get("UI_CHARMS");
			}
			else
			{
				this.tabBarButtons[3].text.text = LM.Get("UI_TAB_ARTIFACTS");
			}
			this.tabBarButtons[4].text.text = LM.Get("UI_TAB_SHOP");
		}

		private void InitPanelHeroes()
		{
			this.panelHeroes.panelTotem.buttonHeroPortrait.onClick = delegate()
			{
				this.state = UiState.HEROES_RUNES;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiOpenMenu, 1f));
			};
			this.panelHeroes.panelTotem.buttonUpgrade.onClick = (this.panelHeroes.panelTotem.buttonUpgrade.onLongPress = delegate()
			{
				this.OnClickedUpgradeTotem();
			});
			this.panelHeroes.panelWorldUpgrade.buttonUpgrade.onClick = (this.panelHeroes.panelWorldUpgrade.buttonUpgrade.onLongPress = delegate()
			{
				this.OnClickedBuyWorldUpgrade();
			});
			this.panelHeroes.autoSkillDistributionToggle.gameButton.onClick = delegate()
			{
				World activeWorld = this.sim.GetActiveWorld();
				activeWorld.autoSkillDistribute = !activeWorld.autoSkillDistribute;
				if (activeWorld.autoSkillDistribute)
				{
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOn, 1f));
				}
				else
				{
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOff, 1f));
				}
			};
			this.buttonLeaveBoss.onClick = delegate()
			{
				this.OnClickedBossButton(false);
			};
			this.buttonFightBoss.onClick = delegate()
			{
				this.OnClickedBossButton(true);
			};
			this.panelPrestige.buttonPrestige.onClick = delegate()
			{
				this.OnClickedPrestige(false);
			};
			this.panelPrestige.buttonCancel.onClick = (this.panelPrestige.backgroundButton.onClick = delegate()
			{
				this.state = UiState.MODE;
				TutorialManager.NotYetPrestige();
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
			});
			this.panelPrestige.buttonMegaPrestige.gameButton.onClick = delegate()
			{
				this.OnClickedPrestige(true);
			};
			this.InitHeroes();
			this.imageGoldScale = this.imageGold.transform.localScale;
			this.imageGoldAnimTimer = 0.22f;
			this.panelHeroes.buttonOpenNewHeroPanel.gameButton.onClick = delegate()
			{
				this.OnClickedOpenNewHeroPanel();
			};
			this.panelHeroes.buttonBuyRandomHero.onClick = new GameButton.VoidFunc(this.OnClickedBuyRandomHero);
		}

		private void InitHeroes()
		{
			for (int i = this.panelHeroes.heroPanels.Count - 1; i >= 0; i--)
			{
				int ic = i;
				this.panelHeroes.heroPanels[i].InitOnClickEvents(delegate
				{
					this.OnClickedHeroUpgrade(ic);
				}, delegate
				{
					this.OnClickedHeroPortrait(ic);
				});
			}
		}

		private void InitNewHero()
		{
			this.panelNewHero.buttonNewHeroBuy.gameButton.onClick = delegate()
			{
				this.OnClickedBuyNewHero();
			};
			this.panelNewHero.buttonSelectHero.onClick = delegate()
			{
				this.OnClickedSelectNewHero();
			};
			this.panelNewHero.newHeroButtons = new List<ButtonNewHeroSelect>();
			List<HeroDataBase> allHeroes = this.sim.GetAllHeroes();
			int count = allHeroes.Count;
			float num = 40f;
			float num2 = 160f;
			float num3 = 110f;
			float num4 = 2f;
			int num5 = GameMath.CeilToInt((float)count / num4);
			Vector2 sizeDelta = new Vector2(num2 + num * 2f + (float)(num5 - 1) * num2, this.panelNewHero.scrollContent.sizeDelta.y);
			this.panelNewHero.scrollContent.sizeDelta = sizeDelta;
			for (int i = 0; i < count; i++)
			{
				int num6 = GameMath.FloorToInt((float)i / 2f);
				bool flag = i % 2 == 0;
				ButtonNewHeroSelect buttonNewHeroSelect = UnityEngine.Object.Instantiate<ButtonNewHeroSelect>(this.panelHubDatabase.heroAvatarPrefab, this.panelNewHero.scrollContent);
				buttonNewHeroSelect.gameButton.allowColorChangeInteractable = false;
				buttonNewHeroSelect.gameButton.allowColorChangeDown = false;
				buttonNewHeroSelect.gameButton.text.text = LM.Get("UI_NEW");
				buttonNewHeroSelect.rectTransform.anchoredPosition = new Vector2(num + num2 * 0.5f + num2 * (float)num6, (!flag) ? (-num3 + 25f) : num3);
				this.panelNewHero.newHeroButtons.Add(buttonNewHeroSelect);
			}
			this.RegisterObjects(this.panelNewHero.scrollContent);
			this.panelNewHero.initializedHeroButtons = true;
			for (int j = this.panelNewHero.newHeroButtons.Count - 1; j >= 0; j--)
			{
				GameButton gameButton = this.panelNewHero.newHeroButtons[j].gameButton;
				int ic = j;
				gameButton.onClick = delegate()
				{
					this.OnClickedNewHero(ic);
				};
			}
			foreach (GameButton gameButton2 in this.panelNewHero.buttonsClose)
			{
				gameButton2.onClick = delegate()
				{
					this.OnClickedHeroNewMenuClose();
				};
			}
		}

		private void InitSkills()
		{
			this.panelSkillsScreen.buttonUlti.gameButton.onClick = delegate()
			{
				this.SelectSkillTreeUlti(false);
			};
			this.panelSkillsScreen.buttonUlti.gameButton.onLongPress = delegate()
			{
				this.SelectSkillTreeUlti(true);
			};
			for (int i = 0; i < 2; i++)
			{
				int bic = i;
				for (int j = this.panelSkillsScreen.buttonsBranches[i].Count - 1; j >= 0; j--)
				{
					int ic = j;
					ButtonSkillUpgrade buttonSkillUpgrade = this.panelSkillsScreen.buttonsBranches[i][j];
					buttonSkillUpgrade.gameButton.onClick = delegate()
					{
						this.SelectSkillTreeNode(bic, ic, false);
					};
					buttonSkillUpgrade.gameButton.onLongPress = delegate()
					{
						this.SelectSkillTreeNode(bic, ic, true);
					};
				}
			}
			this.panelSkillsScreen.buttonUpgrade.onClick = (this.panelSkillsScreen.buttonUpgrade.onLongPress = delegate()
			{
				this.OnClickedUpgradeSkill();
			});
			this.panelSkillsScreen.buttonOneTapUpgrade.gameButton.onClick = delegate()
			{
				this.OnClickedSkillOneTapUpgradeOnOff();
			};
		}

		private void InitPanelBuyFlashCharmPopup()
		{
			this.panelBuyCharmFlashOffer.buttonBuy.gameButton.onClick = new GameButton.VoidFunc(this.Button_OnFlashOfferCharmBuyClicked);
			this.panelBuyCharmFlashOffer.buttonClose.onClick = (this.panelBuyCharmFlashOffer.buttonCloseX.onClick = delegate()
			{
				this.state = this.panelBuyCharmFlashOffer.previousState;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
			});
		}

		private void InitPanelBuyFlashOfferPopup()
		{
			this.panelBuyAdventureFlashOffer.buttonBuy.gameButton.onClick = new GameButton.VoidFunc(this.Button_OnFlashOfferBuyClicked);
			this.panelBuyAdventureFlashOffer.buttonClose.onClick = (this.panelBuyAdventureFlashOffer.buttonCloseX.onClick = delegate()
			{
				this.state = this.panelBuyAdventureFlashOffer.previousState;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
			});
		}

		private void InitTrinketSelect()
		{
			this.panelTrinketSelect.buttonTabSmith.onClick = delegate()
			{
				this.panelTrinketSelect.trinketScrollerParent.gameObject.SetActive(false);
				this.panelTrinketSelect.trinketSmitherParent.gameObject.SetActive(true);
				this.panelTrinketSelect.background.color = this.panelTrinketSelect.smithTabColor;
				this.panelTrinketSelect.isSmithing = true;
				this.panelTrinketSmithing.UpdateTrinketVisual();
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelTrinketSelect.buttonTabTrinkets.onClick = delegate()
			{
				this.panelTrinketSmithing.CancelCrafting();
				this.panelTrinketSelect.trinketScrollerParent.gameObject.SetActive(true);
				this.panelTrinketSelect.trinketSmitherParent.gameObject.SetActive(false);
				this.panelTrinketSelect.background.color = this.panelTrinketSelect.trinketTabColor;
				this.panelTrinketSelect.isSmithing = false;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
		}

		private void InitHubCharms()
		{
			this.panelHubCharms.filterToggleButton.onClick = delegate()
			{
				if (!this.panelHubCharms.isAnimatingSort)
				{
					this.SwitchCharmSortType();
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
				}
			};
			this.panelHubCharms.filterButton.onClick = delegate()
			{
				this.command = new UiToggleCharmFilterShowHide();
				if (!this.sim.isCharmSortingShowing)
				{
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuUp, 1f));
				}
				else
				{
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuDown, 1f));
				}
			};
		}

		private void InitArtifactScroller()
		{
			PanelArtifactScroller pas = this.panelArtifactScroller;
			pas.uiManager = this;
			pas.buttonArtifacts = new List<ButtonArtifact>();
			pas.CreateArtifactButtons(40);
			pas.CalculatePositions(false);
			pas.CalculateContentSize((!pas.isLookingAtMythical) ? this.sim.artifactsManager.AvailableSlotsCount : this.sim.artifactsManager.NumArtifactSlotsMythical);
			pas.SetButtonEvents();
			this.RegisterObjects(pas.artifactsParent);
			pas.buttonUnlockSlot.SetAsNormal();
			pas.buttonTabRegular.onDown = delegate()
			{
				this.panelArtifactScroller.OnScreenAppear();
				if (pas.selectedArtifactIndex != -1)
				{
					pas.ResetArtifactSelected();
				}
				pas.isLookingAtMythical = false;
				UiManager.stateJustChanged = true;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
				pas.CalculateContentSize(this.sim.artifactsManager.AvailableSlotsCount);
				pas.boilerParent.SetAsLastSibling();
				pas.buttonUnlockSlot.SetAsNormal();
				pas.unlockButtonWaitingForConfirm = false;
			};
			pas.buttonTabMythical.onDown = delegate()
			{
				this.panelArtifactScroller.OnScreenAppear();
				if (pas.selectedArtifactIndex != -1)
				{
					pas.ResetArtifactSelected();
				}
				DOTween.Kill("ArtifactPop", true);
				pas.isLookingAtMythical = true;
				UiManager.stateJustChanged = true;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
				pas.CalculateContentSize(this.sim.artifactsManager.AvailableSlotsCount);
				pas.boilerParent.SetAsFirstSibling();
			};
			pas.craftButton.gameButton.onClick = new GameButton.VoidFunc(this.OnClickedArtifactCraft);
			pas.buttonInfo.onClick = new GameButton.VoidFunc(this.OnClickedArtifactsInfo);
			pas.buttonEffects.onClick = new GameButton.VoidFunc(this.Button_OnPossibleEffectsClicked);
			pas.buttonUnlockSlot.buttonUpgrade.gameButton.onClick = new GameButton.VoidFunc(this.OnClickedArtifactsUnlockSlot);
			pas.panelSelectedArtifact.buttonReroll.gameButton.onClick = new GameButton.VoidFunc(this.OnClickedArtifactReroll);
			pas.panelSelectedArtifact.buttonEnableDisable.gameButton.onClick = new GameButton.VoidFunc(this.OnClickedArtifactsEnableDisable);
		}

		private void Button_OnPossibleEffectsClicked()
		{
			this.state = ((this.sim.artifactsManager.GetUniqueEffectsStockCount() <= 0) ? UiState.CAN_NOT_EVOLVE_ARTIFACT_POPUP : UiState.POSSIBLE_ARTIFACT_EFFECTS_POPUP);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
		}

		public void InitPanelTrinketRecyclePopup()
		{
			this.panelTrinketRecycle.backgroundButton.onClick = (this.panelTrinketRecycle.closeButton.onClick = delegate()
			{
				if (this.panelTrinketRecycle.previousState == UiState.TRINKET_INFO_POPUP)
				{
					this.panelTrinketInfoPopup.isReturningBack = true;
				}
				this.state = this.panelTrinketRecycle.previousState;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			});
			this.panelTrinketRecycle.buttonTokens.gameButton.onClick = delegate()
			{
				this.OnTrinketDestroyOrDisenchantCLicked(CurrencyType.TOKEN);
			};
			this.panelTrinketRecycle.buttonGems.gameButton.onClick = delegate()
			{
				this.OnTrinketDestroyOrDisenchantCLicked(CurrencyType.GEM);
			};
		}

		private void OnTrinketDestroyOrDisenchantCLicked(CurrencyType currencyType)
		{
			DropPosition dropPosition;
			if (this.panelTrinketInfoPopup.stateToReturn == UiState.HUB_DATABASE_TRINKETS)
			{
				dropPosition = this.panelTrinketRecycle.GetDropPosition(currencyType, this.panelHubTrinkets.showCurrency.GetCurrencyTransform());
			}
			else if (this.panelTrinketsScroller.multipleSelectedTrinkets != null && this.panelTrinketsScroller.multipleSelectedTrinkets.Count > 1)
			{
				dropPosition = new DropPosition
				{
					startPos = this.panelTrinketPopup.menuShowCurrency.transform.position,
					endPos = this.panelTrinketPopup.menuShowCurrency.transform.position - Vector3.up * 0.1f,
					invPos = this.panelHubTrinkets.showCurrency.GetCurrencyTransform().position,
					targetToScaleOnReach = this.panelHubTrinkets.showCurrency.GetCurrencyTransform()
				};
			}
			else
			{
				dropPosition = new DropPosition
				{
					startPos = this.panelTrinketPopup.menuShowCurrency.transform.position,
					endPos = this.panelTrinketPopup.menuShowCurrency.transform.position - Vector3.up * 0.1f,
					invPos = this.panelTrinketSelect.menuShowCurrencyScraps.GetCurrencyTransform().position,
					targetToScaleOnReach = this.panelTrinketSelect.menuShowCurrencyScraps.GetCurrencyTransform()
				};
			}
			if (this.sim.GetUnlock(UnlockIds.TRINKET_DISASSEMBLE).isCollected)
			{
				UiCommandTrinketDisassemble uiCommandTrinketDisassemble = new UiCommandTrinketDisassemble
				{
					dropPos = dropPosition,
					trinket = ((this.panelTrinketRecycle.previousState != UiState.TRINKET_INFO_POPUP) ? null : this.panelTrinketInfoPopup.trinketInfoBody.selectedTrinket),
					trinkets = this.panelTrinketsScroller.multipleSelectedTrinkets,
					panelTrinketsScroller = this.panelTrinketsScroller,
					currencyType = currencyType
				};
				Vector3 position;
				if (this.panelTrinketInfoPopup.stateToReturn == UiState.HUB_DATABASE_TRINKETS)
				{
					position = this.panelHubTrinkets.buttonTabSmithing.transform.position;
				}
				else
				{
					position = this.panelTrinketSelect.buttonTabSmith.transform.position;
				}
				if (this.panelTrinketsScroller.multipleSelectedTrinkets != null && this.panelTrinketsScroller.multipleSelectedTrinkets.Count > 0)
				{
					List<DropPosition> list = new List<DropPosition>();
					List<TrinketUi> list2 = new List<TrinketUi>();
					using (List<Trinket>.Enumerator enumerator = this.panelTrinketsScroller.multipleSelectedTrinkets.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Trinket item = enumerator.Current;
							TrinketUi trinketUi = this.panelTrinketsScroller.trinkets.Find((ButtonSelectTrinket t) => t.trinketUi.simTrinket == item).trinketUi;
							TrinketUi item2 = UnityEngine.Object.Instantiate<TrinketUi>(trinketUi, trinketUi.transform.parent, true);
							trinketUi.gameObject.SetActive(false);
							list2.Add(item2);
							DropPosition dropPosition2 = dropPosition.Clone();
							dropPosition2.startPos = trinketUi.transform.position;
							Vector2 vector = UnityEngine.Random.insideUnitCircle * 0f;
							dropPosition2.endPos = dropPosition2.startPos + new Vector3(vector.x, vector.y);
							list.Add(dropPosition2);
						}
					}
					this.panelTrinketsScroller.DoMassDisenchantAnim(list2, position);
					for (int i = 0; i < list.Count; i++)
					{
						TrinketUi trinketUi2 = list2[i];
						DropPosition dropPosition3 = list[i];
						dropPosition3.startPos = trinketUi2.transform.position;
						dropPosition3.endPos = dropPosition3.startPos;
					}
					uiCommandTrinketDisassemble.dropPositions = list;
				}
				else
				{
					TrinketUi trinketUi3;
					if (this.panelTrinketRecycle.previousState == UiState.TRINKET_INFO_POPUP)
					{
						trinketUi3 = UnityEngine.Object.Instantiate<TrinketUi>(this.panelTrinketInfoPopup.trinketInfoBody.trinketUiSelected.trinketUi, this.panelTrinketInfoPopup.trinketInfoBody.trinketUiSelected.trinketUi.transform.parent);
						trinketUi3.simTrinket = this.panelTrinketInfoPopup.trinketInfoBody.trinketUiSelected.trinketUi.simTrinket;
					}
					else
					{
						TrinketUi trinketUi4 = this.panelTrinketsScroller.trinkets.Find((ButtonSelectTrinket t) => t.trinketUi.simTrinket == this.panelTrinketsScroller.multipleSelectedTrinkets[0]).trinketUi;
						trinketUi3 = UnityEngine.Object.Instantiate<TrinketUi>(trinketUi4, trinketUi4.transform.parent);
						trinketUi3.simTrinket = this.panelTrinketsScroller.multipleSelectedTrinkets[0];
					}
					this.panelTrinketDisenchantAnim.DoDisenchant(trinketUi3, position);
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTrinketDisenchant, 1f));
				}
				this.command = uiCommandTrinketDisassemble;
			}
			else
			{
				UiCommandTrinketDestroy command = new UiCommandTrinketDestroy
				{
					dropPos = dropPosition,
					trinket = ((this.panelTrinketRecycle.previousState != UiState.TRINKET_INFO_POPUP) ? null : this.panelTrinketInfoPopup.trinketInfoBody.selectedTrinket),
					trinkets = this.panelTrinketsScroller.multipleSelectedTrinkets,
					panelTrinketsScroller = this.panelTrinketsScroller,
					currencyType = currencyType
				};
				this.command = command;
			}
			this.panelTrinketsScroller.disassembleMultipleTrinketsButton.interactable = false;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiWorldUpgrade, 1f));
			this.state = ((this.panelTrinketRecycle.previousState != UiState.TRINKET_INFO_POPUP) ? this.panelTrinketRecycle.previousState : this.panelTrinketInfoPopup.stateToReturn);
		}

		public void InitPanelTrinketInfoPopup()
		{
			this.panelTrinketInfoPopup.trinketInfoBody.uiManager = this;
			this.panelTrinketInfoPopup.destroyRewardText.color = UiManager.colorCurrencyTypes[1];
			this.panelTrinketInfoPopup.buttonNext.onClick = delegate()
			{
				Trinket selectedTrinket = this.panelTrinketInfoPopup.trinketInfoBody.selectedTrinket;
				List<Trinket> sortedTrinkets = this.sim.GetSortedTrinkets();
				int num = sortedTrinkets.IndexOf(selectedTrinket);
				num++;
				if (num >= sortedTrinkets.Count)
				{
					num = 0;
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOff, 1f));
				}
				else
				{
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOn, 1f));
				}
				Trinket selectedTrinket2 = sortedTrinkets[num];
				this.panelTrinketInfoPopup.trinketInfoBody.selectedTrinket = selectedTrinket2;
				this.panelTrinketInfoPopup.trinketInfoBody.forceToUpdate = true;
			};
			this.panelTrinketInfoPopup.buttonPrevious.onClick = delegate()
			{
				Trinket selectedTrinket = this.panelTrinketInfoPopup.trinketInfoBody.selectedTrinket;
				List<Trinket> sortedTrinkets = this.sim.GetSortedTrinkets();
				int num = sortedTrinkets.IndexOf(selectedTrinket);
				num--;
				if (num < 0)
				{
					num = sortedTrinkets.Count - 1;
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOn, 1f));
				}
				else
				{
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOff, 1f));
				}
				Trinket selectedTrinket2 = sortedTrinkets[num];
				this.panelTrinketInfoPopup.trinketInfoBody.selectedTrinket = selectedTrinket2;
				this.panelTrinketInfoPopup.trinketInfoBody.forceToUpdate = true;
			};
			this.panelTrinketInfoPopup.buttonClose.onClick = delegate()
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
				this.state = this.panelTrinketInfoPopup.stateToReturn;
				this.panelTrinketsScroller.PlayTrinketSelectedFeedback(this.panelTrinketInfoPopup.trinketInfoBody.selectedTrinket);
			};
			this.panelTrinketInfoPopup.backgroundCloseButton.onClick = delegate()
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
				this.state = this.panelTrinketInfoPopup.stateToReturn;
				this.panelTrinketsScroller.PlayTrinketSelectedFeedback(this.panelTrinketInfoPopup.trinketInfoBody.selectedTrinket);
			};
			this.panelTrinketInfoPopup.buttonEquip.onClick = delegate()
			{
				Trinket trinket = this.panelTrinketInfoPopup.trinketInfoBody.selectedTrinket;
				HeroDataBase heroWithTrinket = this.sim.GetHeroWithTrinket(trinket);
				if (heroWithTrinket == null)
				{
					this.command = new UiCommandTrinketEquipUnequip
					{
						trinket = trinket,
						hero = this.selectedHeroGearSkills.GetData().GetDataBase()
					};
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiWorldUpgrade, 1f));
					this.state = UiState.HEROES_TRINKETS;
				}
				else
				{
					this.panelGeneralPopup.SetDetails(PanelGeneralPopup.State.SELECT_TRINKET, LM.Get("TRINKET_TRANSFER_TITLE"), string.Empty, true, delegate
					{
						UiCommandTrinketEquipUnequip uiCommandTrinketEquipUnequip = new UiCommandTrinketEquipUnequip();
						uiCommandTrinketEquipUnequip.trinket = trinket;
						uiCommandTrinketEquipUnequip.hero = this.selectedHeroGearSkills.GetData().GetDataBase();
						this.command = uiCommandTrinketEquipUnequip;
						UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiWorldUpgrade, 1f));
						int selected = this.panelSelectTrinket.selected;
						this.state = UiState.SELECT_TRINKET;
						this.panelSelectTrinket.selected = selected;
					}, LM.Get("UI_YES"), delegate
					{
						int selected = this.panelSelectTrinket.selected;
						this.panelTrinketInfoPopup.isReturningBack = true;
						this.state = UiState.TRINKET_INFO_POPUP;
						this.panelSelectTrinket.selected = selected;
						UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
					}, LM.Get("UI_CANCEL"), 0f, 160f, null, null);
					this.panelTrinketPopup.SetTransfer(heroWithTrinket, this.selectedHeroGearSkills.GetData().GetDataBase(), trinket, this.spritesHeroPortrait);
					this.state = UiState.GENERAL_POPUP;
				}
			};
			this.panelTrinketInfoPopup.buttonRecycle.onClick = delegate()
			{
				this.panelTrinketRecycle.previousState = UiState.TRINKET_INFO_POPUP;
				this.state = UiState.TRINKET_RECYCLE_POPUP;
				UiManager.AddUiSound(SoundArchieve.inst.uiPopupAppear);
			};
			this.panelTrinketInfoPopup.togglePinTrinket.onClick = delegate()
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
				this.command = new UiCommandTrinketTogglePin
				{
					trinket = this.panelTrinketInfoPopup.trinketInfoBody.selectedTrinket
				};
			};
		}

		public static void AddUiSound(AudioClip clip)
		{
			UiManager.sounds.Add(new SoundEventUiSimple(clip, 1f));
		}

		public static void AddUiSound(AudioClip[] clips)
		{
			UiManager.sounds.Add(new SoundEventUiVariedSimple(clips, 1f));
		}

		public void InitTrinketScroller()
		{
			this.panelTrinketsScroller.trinkets = new List<ButtonSelectTrinket>();
			this.panelTrinketsScroller.InitScroll(this.sim.numTrinketSlots, new Action<int>(this.OnTrinketSelectedHandler), new Action(this.OnMultipleTrinketDisassemble));
			this.panelTrinketsScroller.changeFilterOptionButton.onClick = new GameButton.VoidFunc(this.OnChangeTrinketSortinButtonClickedHandler);
			this.panelTrinketsScroller.filterOptionsToggle.onClick = new GameButton.VoidFunc(this.OnShowFilterOptionsToggleClickedHandler);
			this.panelTrinketsScroller.dissassembleMultipleToggle.onClick = new GameButton.VoidFunc(this.OnShowMultipleTrinketSelectionToggleClickedHandler);
			this.panelTrinketsScroller.InitScrollPos(this.sim.isTrinketSortingShowing);
			TrinketInfoBody trinketInfoBody = this.panelTrinketInfoPopup.trinketInfoBody;
			trinketInfoBody.onTrinketVisualChanged = (Action<Trinket>)Delegate.Combine(trinketInfoBody.onTrinketVisualChanged, new Action<Trinket>(this.panelTrinketsScroller.OnTrinketUiChanged));
			this.panelTrinketsScroller.ClearMultipleTrinketsSelection();
		}

		private void OnTrinketSelectedHandler(int index)
		{
			if (!this.panelTrinketsScroller.isAnimatingSort)
			{
				this.panelTrinketInfoPopup.trinketInfoBody.selectedTrinket = this.panelTrinketsScroller.lastTrinkets[index];
				this.state = UiState.TRINKET_INFO_POPUP;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
			}
		}

		private void OnMultipleTrinketDisassemble()
		{
			this.panelTrinketRecycle.previousState = this.state;
			this.state = UiState.TRINKET_RECYCLE_POPUP;
			UiManager.AddUiSound(SoundArchieve.inst.uiPopupAppear);
		}

		private void OnShowFilterOptionsToggleClickedHandler()
		{
			this.command = new UiToggleTrinketFilterShowHide();
			this.panelTrinketsScroller.SetFilterOptions(!this.sim.isTrinketSortingShowing);
		}

		private void OnShowMultipleTrinketSelectionToggleClickedHandler()
		{
			if (this.sim.isTrinketSortingShowing)
			{
				this.command = new UiToggleTrinketFilterShowHide();
			}
			this.panelTrinketsScroller.ToggleMultipleSelectionMenu(this.sim.isTrinketSortingShowing);
		}

		private void OnChangeTrinketSortinButtonClickedHandler()
		{
			if (!this.panelTrinketsScroller.isAnimatingSort)
			{
				this.SwitchTrinketSortType();
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			}
		}

		private void InitUnlockPanels(int unlocksLength)
		{
			this.panelUnlocks.panelUnlocks = new PanelUnlock[unlocksLength];
			this.panelUnlocks.panelLockeds = new PanelLocked[unlocksLength];
			for (int i = 0; i < unlocksLength; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.panelUnlockPrefab);
				RectTransform component = gameObject.GetComponent<RectTransform>();
				component.SetParent(this.panelUnlocks.unlockWidgetsParent);
				component.localScale = Vector3.one;
				this.panelUnlocks.panelUnlocks[i] = gameObject.GetComponent<PanelUnlock>();
				component.gameObject.name = "PanelUnlock (" + i.ToString() + ")";
			}
			for (int j = 0; j < unlocksLength; j++)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.panelLockedPrefab);
				RectTransform component2 = gameObject2.GetComponent<RectTransform>();
				component2.SetParent(this.panelUnlocks.unlockWidgetsParent);
				component2.localScale = Vector3.one;
				component2.anchoredPosition = this.panelUnlocks.panelUnlocks[j].rect.anchoredPosition;
				this.panelUnlocks.panelLockeds[j] = gameObject2.GetComponent<PanelLocked>();
			}
			this.panelUnlocks.buttonHideCollectedUnlocks.onClick = delegate()
			{
				this.panelUnlocks.HideOrShow();
			};
			this.panelUnlockReward.button.onClick = delegate()
			{
				this.OnClickedPanelUnlockCollect();
			};
		}

		private void InitShop()
		{
			this.panelShop.buttonTabVault.onDown = delegate()
			{
				this.panelShop.ResetScrollPosition();
				this.panelShop.isLookingAtOffers = false;
				this.panelShop.focusOnFlashOffers = false;
				UiManager.stateJustChanged = true;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelShop.buttonTabOffers.onDown = delegate()
			{
				this.panelShop.ResetScrollPosition();
				this.panelShop.focusOnFlashOffers = false;
				this.panelShop.isLookingAtOffers = true;
				this.command = new UiCommandLookedAtSpecialOffers();
				UiManager.stateJustChanged = true;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			for (int i = 0; i < this.panelShop.shopLootPacks.Length; i++)
			{
				int ic = i;
				this.panelShop.shopLootPacks[i].gameButton.onClick = delegate()
				{
					this.OnClickedShopLootpack(ic);
				};
			}
			this.panelShop.panelFreeCredits.gameButton.onClick = delegate()
			{
				this.OnClickedFreeCredits();
			};
			for (int j = 0; j < this.panelShop.panelBuyCredits.Length; j++)
			{
				int ic = j + 1;
				this.panelShop.panelBuyCredits[j].gameButton.onClick = delegate()
				{
					this.OnClickedIAP(ic);
				};
			}
			for (int k = 0; k < this.panelShop.shopCharmPacks.Length; k++)
			{
				int ic = k;
				this.panelShop.shopCharmPacks[k].gameButton.onClick = delegate()
				{
					this.OnClickedShopCharmpack(ic);
				};
			}
			for (int l = 0; l < this.panelShop.flashOfferPacks.Length; l++)
			{
				int ic = l;
				this.panelShop.flashOfferPacks[l].button.gameButton.onClick = delegate()
				{
					this.OnClickedShopFlashOffer(ic, false);
				};
			}
			for (int m = 0; m < this.panelShop.adventureFlashOfferPacks.Length; m++)
			{
				int ic = m;
				this.panelShop.adventureFlashOfferPacks[m].button.gameButton.onClick = delegate()
				{
					this.OnClickedShopFlashOffer(ic, true);
				};
			}
			for (int n = 0; n < this.panelShop.halloweenFlashOfferPacks.Length; n++)
			{
				int ic = n;
				this.panelShop.halloweenFlashOfferPacks[n].button.gameButton.onClick = delegate()
				{
					this.OnClickedShopHalloweenFlashOffer(ic);
				};
			}
			this.shopLootpackSelect.buttonOpenPack.gameButton.onClick = delegate()
			{
				this.OnClickedShopLootpackSelect();
			};
			GameButton.VoidFunc onClick = delegate()
			{
				this.state = this.shopLootpackSelect.previousState;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
			};
			this.shopLootpackSelect.buttonCloseCross.onClick = onClick;
			this.shopLootpackSelect.buttonClose.onClick = onClick;
			this.shopLootpackSummary.buttonDone.onClick = delegate()
			{
				this.OnClickedShopLootpackSummary();
			};
			GameButton.VoidFunc onClick2 = delegate()
			{
				this.state = ((!this.panelShop.isHubMode) ? UiState.SHOP : UiState.HUB_SHOP);
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
			};
			this.shopTrinketPackSelect.closeBgButton.onClick = onClick2;
			this.shopTrinketPackSelect.closeCrossButton.onClick = onClick2;
			this.shopTrinketPackSelect.openButton.onClick = delegate()
			{
				this.OnClickedShopTrinketPackSelect(false);
			};
			this.shopTrinketPackSelect.buyWithAeonButton.gameButton.onClick = delegate()
			{
				this.OnClickedShopTrinketPackSelect(true);
			};
			this.shopTrinketPackSelect.buyWithGemButton.gameButton.onClick = delegate()
			{
				this.OnClickedShopTrinketPackSelect(false);
			};
			this.panelHubShop.buttonBack.onClick = delegate()
			{
				this.state = UiState.HUB;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
			};
			this.panelShop.panelTrinket.gameButton.onClick = delegate()
			{
				this.OnClickedTrinketPack();
			};
			this.panelShop.panelMineToken.gameButton.onClick = delegate()
			{
				this.OnClickedMine(this.sim.mineToken);
			};
			this.panelShop.panelMineScrap.gameButton.onClick = delegate()
			{
				this.OnClickedMine(this.sim.mineScrap);
			};
			for (int num = 0; num < this.panelShop.socialRewardOffers.Length; num++)
			{
                SocialRewards.Network network = Manager.NetworkList[num];
				this.panelShop.socialRewardOffers[num].gameButton.onClick = delegate()
				{
					this.OnClickSocialNetworkOffer(network);
				};
			}
			this.panelShop.secondAnniversaryOfferButton.onClick = delegate()
			{
				this.secondAnniversaryPopup.previousState = ((!this.panelShop.isHubMode) ? UiState.SHOP : UiState.HUB_SHOP);
				this.state = UiState.SECOND_ANNIVERSARY_POPUP;
				UiManager.AddUiSound(SoundArchieve.inst.uiPopupAppear);
			};
		}

		public void InitHeroSkinChanger()
		{
			this.panelHeroSkinChanger.closeButton.onClick = delegate()
			{
				this.state = this.panelHeroSkinChanger.oldState;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
			};
			this.panelHeroSkinChanger.equipButton.gameButton.onClick = new GameButton.VoidFunc(this.EquipSelectedSkin);
			this.panelHeroSkinChanger.equipButton.gameButton.throwClickEventWhenNoInteractable = true;
			this.panelHeroSkinChanger.soloEquipButton.onClick = new GameButton.VoidFunc(this.EquipSelectedSkin);
			this.panelHeroSkinChanger.gotToShopButton.onClick = delegate()
			{
				this.panelChristmasOffer.previousState = this.state;
				this.panelChristmasOffer.selectedHeroOnPreviousState = this.panelHeroSkinChanger.selectedHero;
				this.panelChristmasOffer.selectedSkinOnPreviousState = this.panelHeroSkinChanger.selectedSkin;
				this.state = UiState.CHRISTMAS_PANEL;
				UiManager.AddUiSound(SoundArchieve.inst.uiPopupAppear);
			};
			this.RegisterObjects(this.panelHeroSkinChanger.skinScroll.content);
		}

		public void InitGear()
		{
			this.panelGearScreen.buttonEvolve.gameButton.onClick = delegate()
			{
				this.OnClickedGearEvolveButton();
			};
			for (int i = this.panelGearScreen.panelGears.Length - 1; i >= 0; i--)
			{
				int ic = i;
				this.panelGearScreen.panelGears[i].buttonUpgrade.gameButton.onClick = delegate()
				{
					this.OnClickedGearUpgradeButton(ic);
				};
			}
			this.panelGearScreen.buttonTab.onDown = delegate()
			{
				this.state = UiState.HEROES_GEAR;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelGearScreen.buttonShop.onClick = delegate()
			{
				this.state = UiState.SHOP;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelGearScreen.buttonSkin.onClick = delegate()
			{
				this.state = UiState.CHANGE_SKIN;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelHubDatabase.panelGear.buttonShop.onClick = delegate()
			{
				this.loadingTransition.DoTransition(UiState.SHOP, 0f, 0f);
			};
		}

		public void InitHubOptions()
		{
			this.hubOptions.buttonSoundOnOff.gameButton.onClick = delegate()
			{
				this.OnClickedSoundOnOff();
			};
			this.hubOptions.buttonMusicOnOff.gameButton.onClick = delegate()
			{
				this.OnClickedMusicOnOff();
			};
			this.hubOptions.buttonVoiceOnOff.gameButton.onClick = delegate()
			{
				this.OnClickedVoicesOnOff();
			};
			this.hubOptions.buttonBack.onClick = delegate()
			{
				this.state = UiState.HUB;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
			};
			this.hubOptions.buttonCredits.onClick = delegate()
			{
				this.state = UiState.HUB_CREDITS;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.hubOptions.buttonAchievements.onClick = delegate()
			{
				this.OnClickedAchievements();
			};
			this.hubOptions.buttonLeaderboards.onClick = delegate()
			{
				this.OnClickedLeaderboards();
			};
			this.hubOptions.buttonGameInfo.onClick = new GameButton.VoidFunc(this.OnClickedGameInfo);
			this.hubOptions.buttonUpdateInfo.onClick = new GameButton.VoidFunc(this.OnClickedUpdateInfo);
			this.hubOptions.buttonContact.onClick = delegate()
			{
				this.OnClickedContact();
			};
			this.hubOptions.buttonLanguage.onClick = delegate()
			{
				this.OnClickedLanguageChange();
			};
			this.hubOptions.buttonWiki.onClick = delegate()
			{
				this.OnClickedWiki();
			};
			this.hubOptions.buttonCommunity.onClick = delegate()
			{
				this.OnClickedCommunity();
			};
			this.hubOptions.buttonCloudSave.onClick = delegate()
			{
				this.OnClickedCloudSave();
			};
			this.hubOptions.buttonDeleteSave.onClick = delegate()
			{
				this.OnClickedDeleteSave();
			};
			this.hubOptions.buttonSecret.onClick = delegate()
			{
				this.OnClickedSecretButton();
			};
			this.hubOptions.panelAdvancedOptions.compassWidget.toggleButton.gameButton.onClick = delegate()
			{
				this.OnClickedCompassOnOff();
			};
			this.hubOptions.panelAdvancedOptions.lowPowerWidget.toggleButton.gameButton.onClick = delegate()
			{
				this.OnClickedLowPowerOnOff();
			};
			this.hubOptions.panelAdvancedOptions.appSleepWidget.toggleButton.gameButton.onClick = delegate()
			{
				this.OnClickedAppSleepOnOff();
			};
			this.hubOptions.panelAdvancedOptions.notationWidget.toggleButton.gameButton.onClick = delegate()
			{
				this.OnClickedNotationOnOff();
			};
			this.hubOptions.panelAdvancedOptions.cooldownUiWidget.toggleButton.gameButton.onClick = delegate()
			{
				this.OnClickedSecondaryCdUiOnOff();
			};
			this.hubOptions.panelAdvancedOptions.notificationsWidget.toggleButton.gameButton.onClick = delegate()
			{
				this.OnClickedNotificationsOnOff();
			};
			this.hubOptions.panelAdvancedOptions.minesToggle.toggleButton.gameButton.onClick = delegate()
			{
				StoreManager.mineNotifications = !StoreManager.mineNotifications;
				this.OnClickedAdvancedNotifOnOff();
			};
			this.hubOptions.panelAdvancedOptions.specialOffersToggle.toggleButton.gameButton.onClick = delegate()
			{
				StoreManager.specialOffersNotifications = !StoreManager.specialOffersNotifications;
				this.OnClickedAdvancedNotifOnOff();
			};
			this.hubOptions.panelAdvancedOptions.freeChestsToggle.toggleButton.gameButton.onClick = delegate()
			{
				StoreManager.freeChestsNotifications = !StoreManager.freeChestsNotifications;
				this.OnClickedAdvancedNotifOnOff();
			};
			this.hubOptions.panelAdvancedOptions.sideQuestsToggle.toggleButton.gameButton.onClick = delegate()
			{
				StoreManager.sideQuestNotifications = !StoreManager.sideQuestNotifications;
				this.OnClickedAdvancedNotifOnOff();
			};
			this.hubOptions.panelAdvancedOptions.flashOffersToggle.toggleButton.gameButton.onClick = delegate()
			{
				StoreManager.flashOffersNotifications = !StoreManager.flashOffersNotifications;
				this.OnClickedAdvancedNotifOnOff();
			};
			this.hubOptions.panelAdvancedOptions.dustRestBonusToggle.toggleButton.gameButton.onClick = delegate()
			{
				StoreManager.dustRestBonusFullNotifications = !StoreManager.dustRestBonusFullNotifications;
				this.OnClickedAdvancedNotifOnOff();
			};
			this.hubOptions.panelAdvancedOptions.christmasEventToggle.toggleButton.gameButton.onClick = delegate()
			{
				StoreManager.christmasEventNotifications = !StoreManager.christmasEventNotifications;
				this.OnClickedAdvancedNotifOnOff();
			};
			this.hubOptions.panelAdvancedOptions.eventsToggle.toggleButton.gameButton.onClick = delegate()
			{
				StoreManager.eventsNotifications = !StoreManager.eventsNotifications;
				this.OnClickedAdvancedNotifOnOff();
			};
			this.hubOptions.textVersion.text = Cheats.version;
			this.hubOptions.uiManager = this;
		}

		public void InitHubOptionsWiki()
		{
			this.hubOptionsWiki.InitStrings(this.colorHeroLevelsInit);
			this.hubOptionsWiki.tabGameInfoButton.onDown = new GameButton.VoidFunc(this.OnClickedGameWikiTabButton);
			this.hubOptionsWiki.tabGogInfoButton.onDown = new GameButton.VoidFunc(this.OnClickedGogWikiTabButton);
			this.hubOptionsWiki.closeButton.onClick = new GameButton.VoidFunc(this.OnClickedCloseWikiButton);
			this.hubOptionsWiki.gameInfoParent.SetActive(true);
			this.hubOptionsWiki.gogInfoParent.SetActive(false);
			this.hubOptionsWiki.tabGameInfoButton.interactable = false;
			this.hubOptionsWiki.tabGogInfoButton.interactable = true;
		}

		private void InitPanelCharmPackOpening()
		{
			this.panelCharmPackOpening.skipButton.onClick = delegate()
			{
				if (this.panelCharmPackOpening.state == PanelCharmPackOpening.State.DONE)
				{
					if (this.panelShop.isHubMode)
					{
						this.state = UiState.HUB_SHOP;
					}
					else
					{
						this.state = UiState.SHOP;
					}
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
					this.panelCharmPackOpening.DisposeMultiPack();
				}
				else
				{
					this.panelCharmPackOpening.DoStep();
				}
			};
			this.panelCharmPackOpening.uiManager = this;
		}

		public void InitPanelCharmPackSelect()
		{
			this.panelCharmPackSelect.buttonClose.onClick = (this.panelCharmPackSelect.buttonCloseCross.onClick = delegate()
			{
				if (this.panelShop.isHubMode)
				{
					this.state = UiState.HUB_SHOP;
				}
				else
				{
					this.state = UiState.SHOP;
				}
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
			});
			this.panelCharmPackSelect.buttonBuy.gameButton.onClick = (this.panelCharmPackSelect.buttonOpen.onClick = new GameButton.VoidFunc(this.Button_OnCharmPackBuyClicked));
		}

		public void InitPanelCharmSelect()
		{
			this.panelCharmSelect.onCharmSelect = new Action<int>(this.Button_OnCharmDraftSelected);
			this.panelCharmSelect.closeButton.onClick = new GameButton.VoidFunc(this.Button_OnCharmSelectionClose);
		}

		public void InitMode()
		{
			int i = 0;
			int num = this.panelMode.merchantItems.Length;
			while (i < num)
			{
				int ic = i;
				MerchantItem merchantItem = this.panelMode.merchantItems[i];
				merchantItem.gameButton.onClick = delegate()
				{
					this.OnClickedMerchantItem(ic, this.panelMode.lookingEventMerchantItems);
				};
				i++;
			}
			this.panelMode.buttonAbandonChallenge.onClick = delegate()
			{
				this.OnClickedAbandonChallenge();
			};
			this.panelMode.buttonPrestige.onClick = delegate()
			{
				this.state = UiState.MODE_PRESTIGE;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPrestigeSelected, 1f));
			};
			this.panelMode.buttonSeeAllUnlocks.onClick = delegate()
			{
				this.state = UiState.MODE_UNLOCKS;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelMode.toggleInfo.onClick = delegate()
			{
				this.panelMode.Button_ToggleInfo((this.sim.GetActiveWorld().eventMerchantItems != null && this.sim.GetActiveWorld().gameMode != GameMode.STANDARD) || this.sim.IsChristmasTreeEnabled());
			};
			this.panelMode.shareScreenshotButton.onClick = delegate()
			{
				this.OpenScreenshotPanel();
			};
		}

		private void OpenScreenshotPanel()
		{
			this.panelShareScreenshot.UpdateScreenshot(this.sim, this, Main.GetCurrentBackgroundAssets());
			this.state = UiState.SHARE_SCREENSHOT_PANEL;
			UiManager.AddUiSound(SoundArchieve.inst.uiOpenTabClick);
		}

		public void InitMerchantItemSelect()
		{
			this.panelMerchantItemSelect.buttonBuy.gameButton.onClick = delegate()
			{
				this.OnClickedBuyMerchantItem();
			};
			this.panelMerchantItemSelect.useItemButton.onClick = delegate()
			{
				this.OnClickedUseMerchantItem();
			};
			GameButton.VoidFunc onClick = delegate()
			{
				this.state = UiState.MODE;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
			};
			this.panelMerchantItemSelect.buttonClose.onClick = onClick;
			this.panelMerchantItemSelect.buttonCloseCross.onClick = onClick;
			this.panelMerchantItemSelect.buttonInreaseCopy.onClick = new GameButton.VoidFunc(this.OnIncreaseMerhcantItemBuyAmount);
			this.panelMerchantItemSelect.buttonDecreaseCopy.onClick = new GameButton.VoidFunc(this.OnDecreaseMerhcantItemBuyAmount);
		}

		private void OnDecreaseMerhcantItemBuyAmount()
		{
			this.panelMerchantItemSelect.copyAmount--;
			if (this.panelMerchantItemSelect.copyAmount <= 0)
			{
				this.panelMerchantItemSelect.copyAmount = 1;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiDefaultFailClick, 1f));
			}
			else
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOff, 1f));
			}
			UiManager.stateJustChanged = true;
		}

		private void OnIncreaseMerhcantItemBuyAmount()
		{
            Simulation.MerchantItem merchantItem = (!this.selectedMerchantItemIsFromEvent) ? this.sim.GetMerchantItem(this.selectedMerchantItem) : this.sim.GetEventMerchantItem(this.selectedMerchantItem);
			this.panelMerchantItemSelect.copyAmount++;
			if (this.panelMerchantItemSelect.copyAmount > merchantItem.GetNumLeft())
			{
				this.panelMerchantItemSelect.copyAmount = merchantItem.GetNumLeft();
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiDefaultFailClick, 1f));
			}
			else
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOn, 1f));
			}
			UiManager.stateJustChanged = true;
		}

		public void InitOfflineEarnings()
		{
			this.panelOfflineEarnings.gameButton.onClick = delegate()
			{
				this.OnClickedCollectOfflineEarnings();
			};
		}

		public void InitHubModeSetup()
		{
			this.panelHubModeSetup.buttonStart.onClick = delegate()
			{
				this.OnClickedModeSetupComplete();
			};
			this.panelHubModeSetup.buttonsSelect[0].onClick = delegate()
			{
				this.state = UiState.HUB_MODE_SETUP_TOTEM;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
			};
			this.panelHubModeSetup.buttonsSelected[0].onClick = delegate()
			{
				this.state = UiState.HUB_MODE_SETUP_TOTEM;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
			};
			int i = 1;
			int num = this.panelHubModeSetup.buttonsSelect.Length;
			while (i < num)
			{
				int ic = i;
				this.panelHubModeSetup.buttonsSelect[i].onClick = delegate()
				{
					this.OnClickedSelectHeroModeSetup(ic - 1);
				};
				this.panelHubModeSetup.buttonsSelected[i].onClick = delegate()
				{
					this.OnClickedSelectHeroModeSetup(ic - 1);
				};
				i++;
			}
			this.panelSelectTotem.buttonSelectTotem.onClick = delegate()
			{
				this.OnClickedSelectTotem();
			};
			this.panelHubModeSetup.buttonBack.onClick = delegate()
			{
				this.state = UiState.HUB;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
			};
			this.panelHubModeSetup.buttonSelectRift.onClick = delegate()
			{
				this.state = UiState.RIFT_SELECT_POPUP;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiOpenTabClick, 1f));
			};
			this.panelHubModeSetup.buttonRiftInfo.onClick = new GameButton.VoidFunc(this.OpenRiftEffectsInfoPopup);
			this.panelHubModeSetup.riftEffectsParentButton.onClick.AddListener(new UnityAction(this.OpenRiftEffectsInfoPopup));
			this.panelHubModeSetup.randomTeam.onClick = delegate()
			{
				this.Button_OnRandomTeamClick();
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiWorldUpgrade, 1f));
			};
			int j = 0;
			int num2 = this.panelSelectTotem.buttonCloses.Length;
			while (j < num2)
			{
				this.panelSelectTotem.buttonCloses[j].onClick = delegate()
				{
					this.state = UiState.HUB_MODE_SETUP;
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
				};
				j++;
			}
		}

		public void Button_OnRandomTeamClick()
		{
			bool flag = this.sim.GetWorld(this.panelHubModeSetup.mode).DoesActiveChallengeAllowRepeatedHeroes();
			List<HeroDataBase> list = new List<HeroDataBase>();
			foreach (HeroDataBase heroDataBase in this.sim.GetAllHeroes())
			{
				if (!this.sim.IsHeroBought(heroDataBase) && this.sim.IsHeroUnlocked(heroDataBase.GetId()))
				{
					list.Add(heroDataBase);
				}
			}
			int minInt = GameMath.GetMinInt(this.panelHubModeSetup.numHeroesShouldBeSelected, list.Count);
			for (int i = 0; i < minInt; i++)
			{
				int randomInt = GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed);
				this.panelHubModeSetup.heroDatabases[i] = list[randomInt];
				if (!flag)
				{
					list.RemoveAt(randomInt);
				}
			}
			if (this.panelHubModeSetup.shouldTotemBeSelected)
			{
				TotemDataBase randomTotemNotBought = this.sim.GetRandomTotemNotBought();
				if (randomTotemNotBought != null)
				{
					this.panelHubModeSetup.totemDatabase = randomTotemNotBought;
				}
			}
			this.SetModeSetupButtons();
		}

		public void InitRiftSelectPopup()
		{
			this.panelRiftSelect.buttonClose.onClick = delegate()
			{
				if (!this.panelRiftSelect.isAnimatingLevelUp)
				{
					if (this.panelRiftSelect.isOnDiscoverMode)
					{
						this.panelRiftSelect.CloseDiscover();
						UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuDown, 1f));
					}
					else
					{
						this.state = UiState.HUB_MODE_SETUP;
						UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
					}
				}
			};
			this.panelRiftSelect.buttonSelect.onClick = delegate()
			{
				this.state = UiState.HUB_MODE_SETUP;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiOpenMenu, 1f));
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
				this.Button_OnRiftSelected(this.panelRiftSelect.selectedRiftIndex);
			};
			this.panelRiftSelect.buttonCurseInfo.onClick = delegate()
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
				this.panelRiftEffectInfo.stateToReturn = UiState.RIFT_SELECT_POPUP;
				this.state = UiState.RIFT_EFFECTS_INFO;
			};
			this.panelRiftSelect.buttonDiscover.onClick = (this.panelRiftSelect.buttonDiscoverPrimal.onClick = delegate()
			{
				this.Button_OnRiftDiscover();
			});
		}

		public void InitRunes()
		{
			int i = 0;
			int num = this.panelHeroesRunes.panelRunes.Length;
			while (i < num)
			{
				int ic = i;
				this.panelHeroesRunes.panelRunes[ic].buttonUse.onClick = delegate()
				{
					this.OnClickedPanelRuneUse(ic);
				};
				this.panelHeroesRunes.panelRunes[ic].buttonRemove.onClick = delegate()
				{
					this.OnClickedPanelRuneRemove(ic);
				};
				i++;
			}
			this.panelHeroesRunes.buttonShop.onClick = delegate()
			{
				this.panelShop.isLookingAtOffers = false;
				this.state = UiState.SHOP;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelHubDatabase.panelRunes.buttonShop.onClick = delegate()
			{
				this.panelShop.isLookingAtOffers = false;
				this.panelShop.setSpecialOfferContentTexts = true;
				if (this.state == UiState.HUB_DATABASE_TOTEMS)
				{
					this.state = UiState.HUB_SHOP;
				}
				else
				{
					this.loadingTransition.DoTransition(UiState.SHOP, 0f, 0f);
				}
			};
		}

		public void InitAdPopup()
		{
			this.panelAdPopup.buttonWatch.onClick = delegate()
			{
				this.OnClickedAdWatch();
			};
			this.panelAdPopup.buttonCancel.onClick = delegate()
			{
				this.OnClickedAdCancel();
			};
			this.panelAdPopup.buttonCollect.onClick = delegate()
			{
				this.OnClickedAdCollect();
			};
		}

		public void InitSocialRewardPopup()
		{
			this.panelSocialRewardPopup.buttonFollow.onClick = new GameButton.VoidFunc(this.OnClickSocialRewardFollow);
			this.panelSocialRewardPopup.buttonCancel.onClick = (this.panelSocialRewardPopup.buttonBg.onClick = new GameButton.VoidFunc(this.OnClickSocialRewardCancel));
			this.panelSocialRewardPopup.buttonCollect.onClick = new GameButton.VoidFunc(this.OnClickSocialRewardCollect);
		}

		public void InitTutorial()
		{
			this.panelTutorial.buttonRingOffer.onClick = delegate()
			{
				TutorialManager.NextFirstState();
				UiManager.sounds.Add(new SoundEventUiVoiceSimple(SoundArchieve.inst.voGreenManFirstRingClaimed, "GREEN_MAN", 1f));
			};
			this.panelTutorial.buttonOkay.onClick = delegate()
			{
				TutorialManager.PressedOkay();
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOn, 1f));
			};
			this.panelTutorial.missionButton.onClick = delegate()
			{
				this.panelTutorial.ToggleMissionDescription();
			};
			this.panelTutorial.claimMissionRewardButton.gameButton.onClick = delegate()
			{
				TutorialManager.MissionRewardClaimed();
			};
		}

		public void InitDatabase()
		{
			List<HeroDataBase> allHeroes = this.sim.GetAllHeroes();
			int count = allHeroes.Count;
			this.panelHubDatabase.buttonHeroes = new ButtonNewHeroSelect[count];
			for (int i = 0; i < count; i++)
			{
				int ic = i;
				ButtonNewHeroSelect buttonNewHeroSelect = UnityEngine.Object.Instantiate<ButtonNewHeroSelect>(this.panelHubDatabase.heroAvatarPrefab);
				buttonNewHeroSelect.rectTransform.SetParent(this.panelHubDatabase.heroAvatarParent);
				buttonNewHeroSelect.rectTransform.localScale = Vector3.one;
				buttonNewHeroSelect.rectTransform.anchoredPosition = new Vector2(this.panelHubDatabase.heroAvatarButtonWidth * 0.5f + (float)i * this.panelHubDatabase.heroAvatarButtonWidth + (float)i * this.panelHubDatabase.heroAvatarButtonInterval + this.panelHubDatabase.heroAvatarButtonPadding, 0f);
				buttonNewHeroSelect.gameButton.onClick = delegate()
				{
					this.OnClickedNewHeroDatabase(ic);
				};
				buttonNewHeroSelect.gameButton.text.text = LM.Get("UI_NEW");
				this.panelHubDatabase.buttonHeroes[i] = buttonNewHeroSelect;
			}
			this.panelHubDatabase.SetSelectedHero(this.panelHubDatabase.selectedHero);
			this.RegisterObjects(this.panelHubDatabase.heroAvatarParent);
			this.panelHubDatabase.heroAvatarParent.sizeDelta = new Vector2((float)count * this.panelHubDatabase.heroAvatarButtonWidth + (float)(count - 1) * this.panelHubDatabase.heroAvatarButtonInterval + this.panelHubDatabase.heroAvatarButtonPadding * 2f, this.panelHubDatabase.heroAvatarParent.sizeDelta.y);
			this.panelHubDatabase.panelGear.buttonSkin.onClick = delegate()
			{
				this.state = UiState.CHANGE_SKIN;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelHubDatabase.panelGear.buttonEvolve.gameButton.onClick = delegate()
			{
				this.OnClickedDatabaseEvolveButton();
			};
			for (int j = this.panelHubDatabase.panelGear.panelGears.Length - 1; j >= 0; j--)
			{
				int ic = j;
				this.panelHubDatabase.panelGear.panelGears[j].buttonUpgrade.gameButton.onClick = delegate()
				{
					this.OnClickedGearUpgradeButtonDatabase(ic);
				};
			}
			int k = 0;
			int num = this.panelHubDatabase.buttonTotems.Length;
			while (k < num)
			{
				int ic = k;
				this.panelHubDatabase.buttonTotems[k].gameButton.onClick = delegate()
				{
					this.OnClickedButtonSelectTotemDatabase(ic);
				};
				k++;
			}
			int l = 0;
			int num2 = this.panelHubDatabase.panelRunes.panelRunes.Length;
			while (l < num2)
			{
				int ic = l;
				this.panelHubDatabase.panelRunes.panelRunes[ic].buttonUse.onClick = delegate()
				{
					this.OnClickedPanelRuneUseDatabase(ic);
				};
				this.panelHubDatabase.panelRunes.panelRunes[ic].buttonRemove.onClick = delegate()
				{
					this.OnClickedPanelRuneRemoveDatabase(ic);
				};
				l++;
			}
			this.panelHubDatabase.buttonTabHeroesItems.onDown = delegate()
			{
				this.state = UiState.HUB_DATABASE_HEROES_ITEMS;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelHubDatabase.buttonTabRings.onDown = delegate()
			{
				this.state = UiState.HUB_DATABASE_TOTEMS;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelHubDatabase.buttonTabHeroesSkills.onDown = delegate()
			{
				this.state = UiState.HUB_DATABASE_HEROES_SKILLS;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelHubDatabase.buttonBack.onClick = delegate()
			{
				this.state = UiState.HUB;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
			};
		}

		private void InitTrinkets()
		{
			this.panelTrinketsScreen.buttonTab.onDown = delegate()
			{
				this.state = UiState.HEROES_TRINKETS;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelTrinketsScreen.buttonEquip.onClick = delegate()
			{
				this.state = UiState.SELECT_TRINKET;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelTrinketsScreen.buttonUnequip.onClick = delegate()
			{
				UiCommandTrinketEquipUnequip uiCommandTrinketEquipUnequip = new UiCommandTrinketEquipUnequip();
				uiCommandTrinketEquipUnequip.hero = this.selectedHeroGearSkills.GetData().GetDataBase();
				uiCommandTrinketEquipUnequip.trinket = uiCommandTrinketEquipUnequip.hero.trinket;
				this.command = uiCommandTrinketEquipUnequip;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiWorldUpgrade, 1f));
			};
			this.panelTrinketsScreen.buttonUpgrade.gameButton.onClick = delegate()
			{
				this.OnClickedUpgradeTrinket(this.selectedHeroGearSkills.trinket);
			};
			this.panelTrinketsScreen.pinTrinketButton.onClick = delegate()
			{
				this.command = new UiCommandTrinketTogglePin
				{
					trinket = this.selectedHeroGearSkills.GetData().GetDataBase().trinket
				};
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelHubTrinkets.buttonBack.onClick = delegate()
			{
				this.state = UiState.HUB;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
			};
			this.panelHubTrinkets.buttonTabTrinkets.onClick = delegate()
			{
				this.panelHubTrinkets.isSmithingTab = false;
				this.state = UiState.HUB_DATABASE_TRINKETS;
				UiManager.stateJustChanged = true;
				this.panelHubTrinkets.backgroundImage.color = this.panelHubTrinkets.trinketTabColor;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelHubTrinkets.buttonTabSmithing.onClick = delegate()
			{
				this.panelHubTrinkets.isSmithingTab = true;
				this.state = UiState.HUB_DATABASE_TRINKETS;
				this.panelTrinketSmithing.CancelCrafting();
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
				this.panelHubTrinkets.backgroundImage.color = this.panelHubTrinkets.smithTabColor;
				this.panelTrinketSmithing.UpdateTrinketVisual();
			};
			RectTransform component = this.panelTrinketsScreen.parentNotSelected.GetComponent<RectTransform>();
			component.pivot = new Vector2(0.5f, 1f);
			component.SetAnchorPosY(-5f);
		}

		private void InitTrinketSmithing()
		{
			List<TrinketEffectGroup> groups = Utility.GetEnumList<TrinketEffectGroup>();
			for (int i = 0; i < this.panelTrinketSmithing.effectButtons.Length; i++)
			{
				int ic = i;
				GameButton gameButton = this.panelTrinketSmithing.effectButtons[i];
				gameButton.onClick = delegate()
				{
					if (this.panelTrinketSmithing.selectedEffects[ic] == null)
					{
						this.state = UiState.TRINKET_EFFECT_SELECT_POPUP;
						this.panelTrinketEffectScroller.SetWidgetPositionsByCategory(groups[ic]);
						this.panelTrinketEffectScroller.onSelectTrinket = delegate(TrinketEffect t)
						{
							this.panelTrinketSmithing.selectedEffects[ic] = t;
							this.panelTrinketSmithing.UpdateTrinketVisual();
						};
						UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
						UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTrinketEffectButtonClick, 1f));
					}
					else
					{
						this.panelTrinketSmithing.selectedEffects[ic] = null;
						this.panelTrinketSmithing.UpdateTrinketVisual();
						UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
					}
				};
			}
			this.panelTrinketSmithing.buttonInfo.onClick = delegate()
			{
				this.state = UiState.TRINKET_EFFECT_SELECT_POPUP;
				this.panelTrinketEffectScroller.SetWidgetPositionsByCategory();
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
			};
			this.panelTrinketSmithing.buttonCancel.onClick = delegate()
			{
				this.panelTrinketSmithing.CancelCrafting();
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
			};
			this.panelTrinketSmithing.buttonCraft.gameButton.onClick = delegate()
			{
				this.Button_OnClickCraftTrinket();
			};
		}

		private void Button_OnClickCraftTrinket()
		{
			UiCommandCraftTrinket uiCommandCraftTrinket = new UiCommandCraftTrinket();
			uiCommandCraftTrinket.common = this.panelTrinketSmithing.selectedEffects[0];
			uiCommandCraftTrinket.secondary = this.panelTrinketSmithing.selectedEffects[1];
			uiCommandCraftTrinket.special = this.panelTrinketSmithing.selectedEffects[2];
			uiCommandCraftTrinket.bodyIndex = this.panelTrinketSmithing.bodyIndex;
			uiCommandCraftTrinket.colorIndex = this.panelTrinketSmithing.colorIndex;
			this.panelTrinketSmithing.trinketCraftedParent.SetParent(this.panelTrinketSmithing.transform.parent.parent);
			TrinketUi inst = UnityEngine.Object.Instantiate<TrinketUi>(this.panelTrinketSmithing.trinketVisual, this.panelTrinketSmithing.trinketCraftedParent);
			this.panelTrinketSmithing.isAnimatingCraft = true;
			this.inputBlocker.SetActive(true);
			foreach (SkeletonGraphic skeletonGraphic in this.panelTrinketSmithing.lineGraphics)
			{
				if (skeletonGraphic.gameObject.activeSelf)
				{
					skeletonGraphic.AnimationState.SetAnimation(0, "craft", false);
				}
			}
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiWorldUpgrade, 1f));
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTrinketCrafting, 1f));
			Sequence sequence = DOTween.Sequence();
			sequence.AppendInterval(3.2f);
			this.panelTrinketSmithing.trinketGlow.gameObject.SetActive(true);
			this.panelTrinketSmithing.trinketGlow.AnimationState.SetAnimation(0, "craft", false);
			sequence.InsertCallback(0.4f, delegate
			{
				foreach (ParticleSystem particleSystem in this.panelTrinketSmithing.craftParticles)
				{
					particleSystem.Play();
				}
			});
			foreach (Image target in this.panelTrinketSmithing.buttonGlows)
			{
				sequence.Insert(1.2f, target.DOColor(Color.clear, 0.5f));
			}
			sequence.Insert(1.4f, inst.movingGlow.rectTransform.DOAnchorPos(new Vector2(70f, -60f), 0.3f, false));
			sequence.OnComplete(delegate
			{
				UnityEngine.Object.Destroy(inst.gameObject);
				this.panelTrinketSmithing.trinketGlow.AnimationState.ClearTracks();
				this.panelTrinketSmithing.isAnimatingCraft = false;
				this.panelTrinketSmithing.UpdateTrinketVisual();
				this.inputBlocker.SetActive(false);
			}).Play<Sequence>();
			this.panelTrinketSmithing.CancelCrafting();
			this.command = uiCommandCraftTrinket;
		}

		private void InitTrinketEffectScroller()
		{
			this.panelTrinketEffectScroller.buttonOkay.onClick = delegate()
			{
				if (this.panelTrinketSmithing.isBattlefield)
				{
					this.state = UiState.SELECT_TRINKET;
				}
				else
				{
					this.state = UiState.HUB_DATABASE_TRINKETS;
				}
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
			};
			this.panelTrinketEffectScroller.buttonSelect.onClick = delegate()
			{
				this.inputBlocker.SetActive(true);
				this.panelTrinketEffectScroller.PlayEffectSelectedAnim(delegate
				{
					if (this.panelTrinketSmithing.isBattlefield)
					{
						this.state = UiState.SELECT_TRINKET;
					}
					else
					{
						this.state = UiState.HUB_DATABASE_TRINKETS;
					}
					this.panelTrinketEffectScroller.onSelectTrinket(this.panelTrinketEffectScroller.selectedTrinketEffect);
					this.inputBlocker.SetActive(false);
				});
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiWorldUpgrade, 1f));
			};
			this.panelTrinketEffectScroller.buttonClose.onClick = delegate()
			{
				if (this.panelTrinketSmithing.isBattlefield)
				{
					this.state = UiState.SELECT_TRINKET;
				}
				else
				{
					this.state = UiState.HUB_DATABASE_TRINKETS;
				}
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
			};
		}

		private void InitSelectTrinket()
		{
			foreach (GameButton gameButton in this.panelSelectTrinket.buttonCloses)
			{
				gameButton.onClick = delegate()
				{
					this.state = UiState.HEROES_TRINKETS;
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
				};
			}
		}

		public void InitMine()
		{
			GameButton.VoidFunc onClick = delegate()
			{
				if (this.panelShop.isHubMode)
				{
					this.state = UiState.HUB_SHOP;
				}
				else
				{
					this.state = UiState.SHOP;
				}
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
			};
			this.panelMine.buttonClose.onClick = onClick;
			this.panelMine.buttonCloseCross.onClick = onClick;
			this.panelMine.buttonMineCollect.onClick = delegate()
			{
				this.OnClickedMineCollect();
			};
			this.panelMine.buttonMineUpgrade.gameButton.onClick = delegate()
			{
				this.OnClickedMineUpgrade();
			};
		}

		public void InitButtonSelectTrinkets(PanelTrinketsScreen panel)
		{
			panel.InitButtonSelectTrinkets(this.sim.numTrinketSlots);
			int i = 0;
			int num = panel.buttonSelectTrinkets.Length;
			while (i < num)
			{
				this.HandleInstantiate(panel.buttonSelectTrinkets[i].gameObject);
				int ic = i;
				panel.buttonSelectTrinkets[i].gameButton.onClick = delegate()
				{
					if (panel.selected == ic)
					{
						panel.selected = -1;
					}
					else
					{
						panel.selected = ic;
					}
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiRingSelect, 1f));
					UiManager.stateJustChanged = true;
				};
				i++;
			}
		}

		public void InitAchievements()
		{
			this.panelAchievements.panels = new List<PanelAchievement>();
			this.panelAchievements.uiManager = this;
			int count = Static.PlayerStats.achievements.Count;
			float num = 0f;
			for (int i = 0; i < count; i++)
			{
				int num2 = Static.PlayerStats.achievementIndexes[i];
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.panelAchievementPrefab);
				RectTransform component = gameObject.GetComponent<RectTransform>();
				component.SetParent(this.panelAchievements.parentAchievements);
				component.localScale = new Vector3(1f, 1f, 1f);
				component.gameObject.name = "PanelAchievement (" + i.ToString() + ")";
				PanelAchievement component2 = gameObject.GetComponent<PanelAchievement>();
				this.panelAchievements.panels.Add(component2);
				num = component.sizeDelta.y + 10f;
				component.anchoredPosition = new Vector2(0f, (float)(-(float)num2) * num - 68f);
				component2.panelIndex = i;
				component2.achievementIds = new List<string>();
				component2.spritesIcon = new List<Sprite>();
				foreach (KeyValuePair<string, bool> keyValuePair in Static.PlayerStats.achievements[i])
				{
					component2.achievementIds.Add(keyValuePair.Key);
					component2.spritesIcon.Add(this.GetSpriteAchievement(keyValuePair.Key));
				}
				int ic = i;
				component2.buttonCollect.onClick = delegate()
				{
					this.OnClickedCollectAchievement(ic);
				};
			}
			int count2 = QuestOfUpdate.GetAllQuests().Count;
			for (int j = 0; j < count2; j++)
			{
				PanelAchievementOfUpdate panelAchievementOfUpdate = UnityEngine.Object.Instantiate<PanelAchievementOfUpdate>(this.panelQuestOfUpdatePerfab, this.panelAchievements.parentAchievements);
				panelAchievementOfUpdate.gameObject.SetActive(false);
				this.panelAchievements.panelQuestOfUpdates.Add(panelAchievementOfUpdate);
			}
			this.panelAchievements.panelQuestOfUpdate.buttonCollect.onClick = new GameButton.VoidFunc(this.OnClickedCollectQuestOfUpdate);
			this.panelAchievements.rectTransformContent.sizeDelta = new Vector2(this.panelAchievements.rectTransformContent.sizeDelta.x, (float)count * num + 300f);
			this.panelAchievements.buttonBack.onClick = delegate()
			{
				if (this.panelAchievements.lastState == UiState.NONE)
				{
					if (this.sim.IsGameModeInAction(this.panelAchievements.lastGameMode))
					{
						this.OnClickedGameMode(this.panelAchievements.lastGameMode);
					}
					else
					{
						this.state = UiState.HUB;
					}
				}
				else
				{
					this.state = UiState.HUB;
				}
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
			};
		}

		public void InitPanelOfferPopup()
		{
			this.panelOfferPopup.buttonClose.onClick = new GameButton.VoidFunc(this.OnPanelOfferPopupCloseButtonClicked);
			this.panelOfferPopup.buttonYes.onClick = new GameButton.VoidFunc(this.OnPanelOfferPopupGoButtonClicked);
		}

		private void OnPanelOfferPopupCloseButtonClicked()
		{
			this.state = UiState.NONE;
			this.panelOfferPopup.OnClose();
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
		}

		private void OnPanelOfferPopupGoButtonClicked()
		{
			this.state = UiState.SHOP;
			this.panelShop.isLookingAtOffers = true;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedCollectQuestOfUpdate()
		{
			PanelAchievementOfUpdate panelQuestOfUpdate = this.panelAchievements.panelQuestOfUpdate;
			DropPosition dropPosition = new DropPosition
			{
				startPos = panelQuestOfUpdate.buttonCollect.transform.position,
				endPos = panelQuestOfUpdate.buttonCollect.transform.position,
				invPos = this.panelAchievements.menuShowCurrencyGem.GetCurrencyTransform().position,
				targetToScaleOnReach = this.panelAchievements.menuShowCurrencyGem.GetCurrencyTransform()
			};
			UiCommand command = new UiCommandCollectQuestOfUpdate
			{
				dropPosition = dropPosition
			};
			this.command = command;
		}

		public void OnClickedDailySkip()
		{
			this.panelGeneralPopup.hasConsuming = true;
			this.panelGeneralPopup.SetDetails(PanelGeneralPopup.State.DAILY_QUEST, LM.Get("DAILY_QUEST_SKIP_POPUP_TITLE"), LM.Get("DAILY_QUEST_SKIP_POPUP_DESC"), true, delegate
			{
				UiCommandSkipDaily command = new UiCommandSkipDaily();
				this.command = command;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiWorldUpgrade, 1f));
				this.state = UiState.DAILY_QUEST;
			}, LM.Get("UI_SKIP"), delegate
			{
				this.state = UiState.DAILY_QUEST;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			}, LM.Get("UI_CANCEL"), 0f, 160f, null, null);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			this.state = UiState.GENERAL_POPUP;
		}

		public void OnClickedDailyCollect()
		{
			if (this.sim.CanCollectDailyQuest())
			{
				this.command = new UiCommandCollectDailyQuest
				{
					startPos = this.dailyQuestPopup.buttonCollect.transform.position,
					invTransform = this.dailyQuestPopup.currencyAeon.GetCurrencyTransform()
				};
			}
		}

		private void InitChristmasOffersPanel()
		{
			this.panelChristmasOffer.closeButton.onClick = delegate()
			{
				this.state = this.panelChristmasOffer.previousState;
				if (this.panelChristmasOffer.previousState == UiState.CHANGE_SKIN)
				{
					this.panelHeroSkinChanger.selectedHero = this.panelChristmasOffer.selectedHeroOnPreviousState;
					this.panelHeroSkinChanger.selectedSkin = this.panelChristmasOffer.selectedSkinOnPreviousState;
				}
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			};
			this.panelChristmasOffer.infoButton.onClick = delegate()
			{
				this.state = UiState.CHRISTMAS_OFFERS_INFO_POPUP;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
			};
			this.panelChristmasOffer.candyTreats[0].gameButton.onClick = delegate()
			{
				this.popupChristmasCandyTreat.SetCandyTreatVideo();
				this.state = UiState.CHRISTMAS_CANDY_TREAT_POPUP;
				UiManager.AddUiSound(SoundArchieve.inst.uiPopupAppear);
			};
			this.panelChristmasOffer.candyTreats[1].gameButton.graphicTarget.sprite = this.panelChristmasOffer.candyTreats[0].buttonUpgradeAnim.spriteNormal;
			this.panelChristmasOffer.candyTreats[1].buttonUpgradeAnim.spriteNormal = this.panelChristmasOffer.candyTreats[0].buttonUpgradeAnim.spriteNormal;
			this.panelChristmasOffer.candyTreats[1].buttonUpgradeAnim.spriteUpgrade = this.panelChristmasOffer.candyTreats[0].buttonUpgradeAnim.spriteNormal;
			this.panelChristmasOffer.candyTreats[1].gameButton.onClick = delegate()
			{
				this.popupChristmasCandyTreat.SetCandyTreatFree();
				this.state = UiState.CHRISTMAS_CANDY_TREAT_POPUP;
				UiManager.AddUiSound(SoundArchieve.inst.uiPopupAppear);
			};
			this.panelChristmasOffer.candyTreats[2].gameButton.onClick = delegate()
			{
				this.popupChristmasCandyTreat.SetCandyTreatIap(IapIds.CANDY_PACK_02);
				this.state = UiState.CHRISTMAS_CANDY_TREAT_POPUP;
				UiManager.AddUiSound(SoundArchieve.inst.uiPopupAppear);
			};
			this.panelChristmasOffer.OnTreeOfferClicked = new Action<int, int>(this.OnClickedChristmasOffer);
			this.panelChristmasOffer.uiManager = this;
		}

		private void InitChristmasCandyTreatPopup()
		{
			this.popupChristmasCandyTreat.buyButton.gameButton.onClick = delegate()
			{
				if (this.popupChristmasCandyTreat.IapIndex == -1)
				{
					UiCommandFreeCurrency command = new UiCommandFreeCurrency
					{
						currencyType = CurrencyType.CANDY,
						rewardAmount = PlayfabManager.titleData.christmasAdCandiesAmount
					};
					this.command = command;
					UiManager.AddUiSound(SoundArchieve.inst.uiPurchaseGemPack);
				}
				else if (this.popupChristmasCandyTreat.IapIndex == -2)
				{
					UiCommandFreeCandyDaily command2 = new UiCommandFreeCandyDaily
					{
						dropPosition = new DropPosition
						{
							startPos = this.panelChristmasOffer.candyTreats[1].transform.position,
							endPos = this.panelChristmasOffer.candyTreats[1].transform.position - Vector3.down * 0.1f,
							invPos = this.panelChristmasOffer.candies.GetCurrencyTransform().position,
							targetToScaleOnReach = this.panelChristmasOffer.candies.GetCurrencyTransform()
						}
					};
					this.command = command2;
					this.state = UiState.CHRISTMAS_PANEL;
					UiManager.AddUiSound(SoundArchieve.inst.uiPurchaseGemPack);
				}
				else
				{
					this.OnClickedIAP(this.popupChristmasCandyTreat.IapIndex);
				}
				UiManager.AddUiSound(SoundArchieve.inst.uiOpenTabClick);
			};
			this.popupChristmasCandyTreat.lockedButton.onClick = delegate()
			{
			};
			this.popupChristmasCandyTreat.closeButton.onClick = (this.popupChristmasCandyTreat.closeButtonBackground.onClick = delegate()
			{
				this.state = UiState.CHRISTMAS_PANEL;
				UiManager.AddUiSound(SoundArchieve.inst.uiPopupDisappear);
			});
		}

		private void InitChristmasOffersInfoPopup()
		{
			this.panelChristmasOffersInfo.closeButton.onClick = (this.panelChristmasOffersInfo.closeBackgroundButton.onClick = (this.panelChristmasOffersInfo.okButton.onClick = delegate()
			{
				this.state = UiState.CHRISTMAS_PANEL;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
			}));
		}

		public void InitChristmasScrapConverterPopup()
		{
			this.panelScrapConverter.buttonConvert.onClick = delegate()
			{
				this.panelScrapConverter.PlayConvertAnimation(delegate
				{
					this.command = new UiCommandConvertCandyIntoScraps
					{
						dropPosition = new DropPosition
						{
							startPos = this.panelScrapConverter.skeletonGraphic.transform.position + new Vector3(0.1f, 0.1f, 0f),
							endPos = this.panelScrapConverter.skeletonGraphic.transform.position + new Vector3(0.1f, 0f, 0f),
							invPos = this.panelScrapConverter.scrapCurrency.GetCurrencyTransform().position,
							targetToScaleOnReach = this.panelScrapConverter.scrapCurrency.GetCurrencyTransform()
						}
					};
				}, delegate
				{
					this.state = UiState.NONE;
				});
			};
		}

		public void InitChristmasEventPopup()
		{
			this.panelChristmasEventPopup.buttonClose.onClick = (this.panelChristmasEventPopup.buttonCloseBackground.onClick = delegate()
			{
				this.state = UiState.NONE;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
			});
			this.panelChristmasEventPopup.buttonGoToTree.onClick = delegate()
			{
				if (this.sim.christmasEventAlreadyDisabled && this.sim.christmasEventPopupsShown == 3)
				{
					this.sim.christmasEventPopupsShown = 4;
					this.state = UiState.CONVERT_XMAS_SCRAP;
				}
				else
				{
					this.panelChristmasOffer.previousState = UiState.NONE;
					this.state = UiState.CHRISTMAS_PANEL;
					UiManager.AddUiSound(SoundArchieve.inst.uiTabSwitch);
				}
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
			};
		}

		private void InitCurrencyWarningPopup()
		{
			this.panelCurrencyWarning.buttonOk.onClick = delegate()
			{
				if (this.panelCurrencyWarning.redirectButtonClickedCallback == null)
				{
					this.state = this.panelCurrencyWarning.previousState;
				}
				else
				{
					this.panelCurrencyWarning.redirectButtonClickedCallback();
				}
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
			};
			this.panelCurrencyWarning.buttonClose.onClick = (this.panelCurrencyWarning.buttonCloseBg.onClick = delegate()
			{
				this.state = this.panelCurrencyWarning.previousState;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
			});
		}

		private void InitPanelShareScreenshot()
		{
			this.panelShareScreenshot.backButton.onClick = delegate()
			{
				this.state = ((!this.panelShareScreenshot.backToModePanel) ? UiState.NONE : UiState.MODE);
				this.sim.isActiveWorldPaused = false;
				UiManager.AddUiSound(SoundArchieve.inst.uiOpenTabClick);
			};
		}

		private void InitPopupNewTALMilestoneReached()
		{
			this.newTALMilestoneReachedPopup.OnClose = delegate()
			{
				this.state = this.newTALMilestoneReachedPopup.previousState;
				UiManager.AddUiSound(SoundArchieve.inst.uiPopupAppear);
			};
		}

		public void InitStrings()
		{
			this.InitStringsTabBarButtons();
			this.panelAdPopup.InitStrings();
			this.panelArtifactsCraft.InitStrings();
			this.panelArtifactsInfo.InitStrings();
			this.panelArtifactsRerollWindow.InitStrings();
			this.panelChallengeLose.InitStrings();
			this.panelChallengeWin.InitStrings();
			this.panelCredits.InitStrings();
			this.panelCurrencyWarning.InitStrings();
			this.panelGearScreen.InitStrings();
			this.panelHubDatabase.panelGear.InitStrings();
			this.panelHubDatabase.panelRunes.InitStrings();
			this.panelHeroes.InitStrings();
			this.panelHeroesRunes.InitStrings();
			this.panelHeroEvolveSkin.InitStrings();
			this.panelHub.InitStrings();
			this.panelHubDatabase.InitStrings();
			this.panelHubTrinkets.InitStrings();
			this.panelHubModeSetup.InitStrings();
			this.shopLootpackSelect.InitStrings();
			this.shopLootpackSummary.InitStrings();
			this.panelMerchantItemSelect.InitStrings();
			this.panelMode.InitStrings();
			this.panelNewHero.InitStrings();
			this.panelOfflineEarnings.InitStrings();
			this.panelPrestige.InitStrings();
			this.panelSelectTotem.InitStrings();
			this.panelShop.InitStrings();
			this.panelSkillsScreen.InitStrings();
			this.panelTutorial.InitStrings();
			this.panelUnlockReward.InitStrings();
			this.panelUnlocks.InitStrings();
			this.panelUpdateRequired.InitStrings();
			this.shopTrinketPackSelect.InitStrings();
			this.hubOptions.InitStrings();
			this.hubOptions.panelAdvancedOptions.InitStrings();
			this.hubOptionsWiki.InitStrings(this.colorHeroLevelsInit);
			this.supportPopup.InitStrings();
			this.panelTrinketsScreen.InitStrings();
			this.panelSelectTrinket.InitStrings();
			this.panelTrinketSelect.InitString();
			this.panelDatabaseTrinket.InitStrings();
			this.panelSelectTotem.InitStrings();
			this.panelAchievements.InitStrings();
			this.panelHeroSkinChanger.InitStrings();
			this.panelSkinBuyingPopup.InitStrings();
			this.panelArtifactScroller.InitStrings();
			this.panelHubartifacts.InitStrings();
			this.panelHubShop.InitStrings();
			this.panelRiftEffectInfo.InitStrings();
			this.panelHubCharms.InitStrings();
			this.panelCharmInfoPopup.InitStrings();
			this.panelCharmPackSelect.InitStrings();
			this.panelRiftSelect.InitStrings();
			this.panelCharmSelect.InitStrings();
			this.panelCharmPackOpening.InitStrings();
			this.panelRunningCharms.InitStrings();
			this.panelRatePopup.InitStrings();
			this.panelSocialRewardPopup.InitStrings();
			this.panelOfferPopup.InitStrings();
			this.panelTrinketsScroller.InitStrings();
			GameMath.InitStrings();
			this.panelMine.InitStrings();
			this.panelBuyCharmFlashOffer.InitStrings();
			this.panelBuyAdventureFlashOffer.InitStrings();
			this.textIdleGain.text = LM.Get("UI_IDLE_GAIN");
			this.panelTrinketInfoPopup.InitStrings();
			this.saveConflictPopup.InitStrings();
			this.panelTrinketRecycle.InitStrings();
			this.panelTrinketEffectScroller.InitStrings();
			this.panelTrinketSmithing.InitStrings();
			this.panelChristmasOffer.InitStrings();
			this.panelChristmasOffersInfo.InitStrings();
			this.popupChristmasCandyTreat.InitStrings();
			this.panelChristmasEventPopup.InitStrings();
			this.versionNotesPopup.InitStrings();
			this.dailyQuestPopup.InitStrings();
			this.panelShareScreenshot.InitStrings();
			this.panelScrapConverter.InitStrings();
			this.maxStageReachedBanner.InitStrings();
			this.panelArtifactPopup.InitStrings();
			this.newTALMilestoneReachedPopup.InitStrings();
			this.posibleArtifactEffectPopup.InitStrings();
			this.artifactEvolveWindow.InitStrings();
			this.artifactOverhaulPopup.InitStrings();
			this.secondAnniversaryPopup.InitStrings();
			this.notEvolveArtifactPopup.InitStrings();
			LM.uiShouldInitStrings = false;
		}

		public void OnScreenSizeChanged(ScreenRes currentResolution)
		{
			float num = currentResolution.AspectRatio();
			float num2 = 0.5625f - num;
			if (num <= 0.5625f)
			{
				this.uiTopAndBottomDelta = GameMath.Lerp(0f, 75f, num2 / 0.0625f);
			}
			else
			{
				this.uiTopAndBottomDelta = 0f;
			}
			if (num > 0.75f)
			{
				float value = 252.000015f;
				float width = this.rectTransform.rect.width;
				float num3 = width - 1080f;
				float num4 = GameMath.Clamp(value, 0f, num3 * 0.5f);
				float num5 = width - 1080f - num4;
				float pos = (num5 - num4) * this.uiMainBodyRect.lossyScale.x * 0.5f;
				this.cameraTransform.transform.SetPosX(pos);
				this.uiMainBodyRect.SetRightDelta(num5);
				this.uiMainBodyRect.SetLeftDelta(num4);
			}
			else
			{
				this.cameraTransform.transform.SetPosX(0f);
				this.uiMainBodyRect.SetRightDelta(0f);
				this.uiMainBodyRect.SetLeftDelta(0f);
			}
			this.uiElementsRect.SetTopDelta(this.uiTopAndBottomDelta);
			this.uiElementsRect.SetBottomDelta(this.uiTopAndBottomDelta);
			this.uiTabMenuAnim.SetSize();
			RectTransform component = this.panelTrinketsScreen.parentNotSelected.GetComponent<RectTransform>();
			RectTransform noCharmParent = this.panelRunningCharms.noCharmParent;
			float height = this.uiElementsRect.rect.height;
			component.SetSizeDeltaY(height - 616f);
			noCharmParent.SetSizeDeltaY(height - 487f);
		}

		private void AddTextData(Transform t, bool canResize)
		{
			if (t.GetComponent<Text>() != null)
			{
				t.gameObject.AddComponent<TextData>().resizeTextForBestFit = t.GetComponent<Text>().resizeTextForBestFit;
				if (!canResize)
				{
					t.GetComponent<Text>().resizeTextForBestFit = false;
				}
			}
			IEnumerator enumerator = t.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform t2 = (Transform)obj;
					this.AddTextData(t2, canResize);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		private void UseTextData(Transform t, bool canResize)
		{
			if (t.GetComponent<TextData>() != null)
			{
				t.GetComponent<Text>().resizeTextForBestFit = (canResize && t.GetComponent<TextData>().resizeTextForBestFit);
			}
			IEnumerator enumerator = t.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform t2 = (Transform)obj;
					this.UseTextData(t2, canResize);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		public void UpdateUi(float dt, Simulator sim, Taps taps)
		{
			UiManager.secondsSinceLastButtonClick += dt;
			if (sim == null)
			{
				return;
			}
			this.sim = sim;
			if (LM.uiShouldInitStrings)
			{
				this.InitStrings();
			}
			this.screenOrientationTimer += dt;
			if (this.screenOrientationTimer >= 5f)
			{
				this.screenOrientationTimer = 0f;
				if (Screen.orientation != ScreenOrientation.Portrait)
				{
					Screen.orientation = ScreenOrientation.Portrait;
				}
			}
			bool flag = UiManager.stateJustChanged;
			if (sim.IsActiveMode(GameMode.STANDARD))
			{
				UiManager.spriteGoldIcon = this.spriteGold;
				UiManager.spriteGoldIconDisabled = this.spriteGoldDisabled;
				UiManager.spriteRewardGold = this.spriteRewardIconGold;
			}
			else if (sim.IsActiveMode(GameMode.CRUSADE))
			{
				UiManager.spriteGoldIcon = this.spriteGoldTriangle;
				UiManager.spriteGoldIconDisabled = this.spriteGoldTriangleDisabled;
				UiManager.spriteRewardGold = this.spriteRewardIconGoldTriangle;
			}
			else
			{
				if (!sim.IsActiveMode(GameMode.RIFT))
				{
					throw new NotImplementedException();
				}
				UiManager.spriteGoldIcon = this.spriteGoldSquare;
				UiManager.spriteGoldIconDisabled = this.spriteGoldSquareDisabled;
				UiManager.spriteRewardGold = this.spriteRewardIconGoldSquare;
			}
			World activeWorld = sim.GetActiveWorld();
			Challenge activeChallenge = activeWorld.activeChallenge;
			bool isModeSetup = sim.IsGameModeAlreadySetup(activeWorld.gameMode);
			this.UpdateTransition(activeWorld);
			this.CheckImmidiateStates(activeWorld, activeChallenge, dt);
			bool flag2 = !this.IsInHubMenus();
			if (flag2)
			{
				this.UpdateTopUi(activeWorld, activeChallenge, dt);
			}
			bool flag3 = !this.IsUiCoveringGame();
			if (flag3)
			{
				this.UpdateMerchantCounters(activeWorld, activeChallenge, dt);
				this.UpdateRingUi(dt, activeWorld, isModeSetup);
			}
			if (flag2)
			{
				this.UpdateTabBar(activeWorld);
			}
			if (flag3)
			{
				this.UpdateSkillButtons();
			}
			this.UpdateDrops(activeWorld);
			this.UpdateSideCurrency(activeWorld, this.panelCurrencySides);
			this.UpdateSideCurrency(activeWorld, this.panelCurrencyOnTop);
			this.openOfferPopupButtonNotif.enabled = false;
			if (TutorialManager.IsShopTabUnlocked() && !activeWorld.isRainingDuck && !activeWorld.isRainingGlory)
			{
				SpecialOfferKeeper specialOfferKeeper = sim.specialOfferBoard.CurrentAnnouncingOffer(sim);
				bool flag4 = specialOfferKeeper != null && specialOfferKeeper.offerPack != null;
				if (sim.IsSecondAnniversaryEventEnabled())
				{
					this.openOfferPopupButton.gameObject.SetActive(true);
					this.openOfferPopupButtonIcon.enabled = false;
					this.openOfferPopupButtonFillBar.enabled = false;
					if (this.openOfferPopupButton.raycastTarget.sprite != this.uiData.secondAnniversaryEventButtonIcon)
					{
						this.openOfferPopupButton.raycastTarget.sprite = this.uiData.secondAnniversaryEventButtonIcon;
						this.openOfferPopupButton.raycastTarget.SetNativeSize();
					}
					this.openOfferPopupButtonNotif.enabled = sim.IsThereNotificationsPendingFromSecondAnniversaryOffer();
				}
				else if (sim.IsChristmasTreeEnabled())
				{
					this.openOfferPopupButton.gameObject.SetActive(true);
					this.openOfferPopupButtonIcon.enabled = true;
					this.openOfferPopupButtonFillBar.enabled = true;
					this.openOfferPopupButtonFillBar.fillAmount = (float)(sim.candyAmountCollectedSinceLastDailyCapReset / PlayfabManager.titleData.christmasCandyCapAmount);
					int num = (int)sim.candyAmountCollectedSinceLastDailyCapReset;
					if (num != this.openOfferPopupWidget.lastProgress)
					{
						this.openOfferPopupWidget.PopAnim();
						this.openOfferPopupWidget.lastProgress = (int)sim.candyAmountCollectedSinceLastDailyCapReset;
					}
					if (this.openOfferPopupButton.raycastTarget.sprite != this.uiData.christmasOfferButtonBackground)
					{
						this.openOfferPopupButton.raycastTarget.sprite = this.uiData.christmasOfferButtonBackground;
						this.openOfferPopupButton.raycastTarget.SetNativeSize();
					}
					if (this.openOfferPopupButtonIcon.sprite != this.uiData.christmasOfferButtonIcon)
					{
						this.openOfferPopupButtonIcon.sprite = this.uiData.christmasOfferButtonIcon;
						this.openOfferPopupButtonIcon.SetNativeSize();
					}
					if (this.openOfferPopupButtonFillBar.sprite != this.uiData.christmasOfferButtonFill)
					{
						this.openOfferPopupButtonFillBar.sprite = this.uiData.christmasOfferButtonFill;
						this.openOfferPopupButtonFillBar.SetNativeSize();
					}
				}
				else if (flag4)
				{
					this.openOfferPopupButton.gameObject.SetActive(true);
					this.openOfferPopupButtonFillBar.enabled = false;
					this.openOfferPopupButtonIcon.enabled = false;
					if (specialOfferKeeper.offerPack is ShopPackRegionalBase)
					{
						if (this.openOfferPopupButton.raycastTarget.sprite != this.uiData.regionalOfferButtonIcon)
						{
							this.openOfferPopupButton.raycastTarget.sprite = this.uiData.regionalOfferButtonIcon;
							this.openOfferPopupButton.raycastTarget.SetNativeSize();
						}
					}
					else if (specialOfferKeeper.offerPack is ShopPackHalloweenGems && this.openOfferPopupButton.raycastTarget.sprite != this.uiData.halloweenOfferButtonIcon)
					{
						this.openOfferPopupButton.raycastTarget.sprite = this.uiData.halloweenOfferButtonIcon;
						this.openOfferPopupButton.raycastTarget.SetNativeSize();
					}
				}
				else if (sim.halloweenEnabled)
				{
					this.openOfferPopupButton.gameObject.SetActive(true);
					this.openOfferPopupButtonFillBar.enabled = false;
					this.openOfferPopupButtonIcon.enabled = false;
					if (this.openOfferPopupButton.raycastTarget.sprite != this.uiData.halloweenOfferButtonIcon)
					{
						this.openOfferPopupButton.raycastTarget.sprite = this.uiData.halloweenOfferButtonIcon;
						this.openOfferPopupButton.raycastTarget.SetNativeSize();
					}
				}
				else
				{
					this.openOfferPopupButton.gameObject.SetActive(false);
				}
				this.openOfferPopupButton.rectTransform.SetAnchorPosX((!sim.hasDailies) ? -267.8f : -212f);
			}
			else
			{
				this.openOfferPopupButton.gameObject.SetActive(false);
			}
			this.UpdateMusic(dt);
			if (UiManager.newCharmIdAdded > -1)
			{
				Vector2 widgetPosition = CharmEffectsPanel.GetWidgetPosition(this.charmEffectsPanel.widgetMatch.objs.Count);
				this.charmEffectsPanel.iconToTransform.sprite = this.spritesCharmEffectIcon[UiManager.newCharmIdAdded];
				this.charmEffectsPanel.DoCharmGoAnimation(Vector2.zero, widgetPosition);
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPrestigeActivated, 1f));
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiCharmSelected, 1f));
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPrestigeActivated, 1f));
				UiManager.newCharmIdAdded = -1;
			}
			if (!TutorialManager.IsHeroesTabUnlocked())
			{
				float num2 = ScreenRes.GetCurrentRes().AspectRatio();
				float num3 = 0.5625f - num2;
				this.bottomLongScreenCurtain.rectTransform.SetAnchorPosY(GameMath.Lerp(-6f, 79f, num3 / 0.0625f));
				this.wasBottomLongCurtainUp = true;
			}
			else if (this.wasBottomLongCurtainUp)
			{
				this.wasBottomLongCurtainUp = false;
				this.bottomLongScreenCurtain.rectTransform.DOAnchorPosY(-6f, 0.4f, false).SetEase(Ease.InBack);
			}
			switch (this.state)
			{
			case UiState.NONE:
				this.UpdateEnchantmentsEffectsPanel();
				break;
			case UiState.HUB:
				this.UpdateHubScreen();
				break;
			case UiState.MODE:
				this.UpdateModeScreen(activeWorld);
				break;
			case UiState.HEROES:
				this.UpdateHeroesScreen(activeWorld);
				break;
			case UiState.ARTIFACTS:
				this.UpdateArtifactScroller();
				break;
			case UiState.SHOP:
				this.UpdateShopScreen();
				break;
			case UiState.HEROES_GEAR:
				this.UpdateHeroesGearScreen(this.selectedHeroGearSkills, this.panelGearScreen);
				break;
			case UiState.HEROES_SKILL:
				this.UpdateHeroesSkillScreen(this.selectedHeroGearSkills);
				break;
			case UiState.HEROES_NEW:
				this.UpdateHeroesNewScreen();
				break;
			case UiState.MODE_UNLOCKS:
				this.UpdateModeUnlocksScreen();
				break;
			case UiState.ARTIFACTS_CRAFT:
				this.UpdateArtifactsCraftScreen();
				break;
			case UiState.ARTIFACTS_INFO:
				this.UpdateArtifactsInfoScreen();
				break;
			case UiState.SHOP_LOOTPACK_SELECT:
				this.UpdateShopLootpackSelectScreen(dt);
				break;
			case UiState.SHOP_LOOTPACK_SUMMARY:
				this.UpdateShopLootpackSummaryScreen(dt);
				break;
			case UiState.HUB_OPTIONS:
				this.UpdateHubOptionsScreen();
				break;
			case UiState.MODE_PRESTIGE:
				this.UpdateModePrestigeScreen();
				break;
			case UiState.MODE_UNLOCK_REWARD:
				this.UpdateModeUnlockRewardScreen(dt);
				break;
			case UiState.MODE_MERCHANT_ITEM_SELECT:
				this.UpdateModeMerchantItemSelectScreen();
				break;
			case UiState.OFFLINE_EARNINGS:
				this.UpdateOfflineEarningsScreen(activeWorld, dt);
				break;
			case UiState.HUB_MODE_SETUP_TOTEM:
				this.UpdateHubModeSetupTotemScreen(sim.allTotems, sim.GetBoughtTotems());
				break;
			case UiState.SHOP_LOOTPACK_OPENING:
				this.UpdateShopLootpackOpeningScreen();
				break;
			case UiState.HUB_MODE_SETUP:
				this.UpdateHubModeSetupScreen();
				break;
			case UiState.HUB_MODE_SETUP_HERO:
				this.UpdateHeroesNewScreen();
				break;
			case UiState.CHALLENGE_WIN:
				this.UpdateChallengeWinScreen(dt);
				break;
			case UiState.CHALLENGE_LOSE:
				this.panelChallengeLose.timer += dt;
				break;
			case UiState.HEROES_RUNES:
				if (activeWorld.totem != null)
				{
					this.UpdateHeroesRunesScreen(activeWorld.totem.GetData().GetDataBase(), this.panelHeroesRunes);
				}
				break;
			case UiState.AD_POPUP:
				this.UpdateAdPopup(dt);
				break;
			case UiState.HUB_DATABASE_HEROES_ITEMS:
			case UiState.HUB_DATABASE_HEROES_SKILLS:
				this.UpdateHubDatabaseHeroes();
				break;
			case UiState.HUB_DATABASE_TOTEMS:
				this.UpdateHubDatabaseTotems();
				break;
			case UiState.HUB_DATABASE_TRINKETS:
				this.UpdateHubDatabaseTrinkets();
				break;
			case UiState.UPDATE_REQUIRED:
				this.UpdateUpdateRequired(dt);
				break;
			case UiState.GENERAL_POPUP:
				this.UpdateGeneralPopup(dt);
				break;
			case UiState.HEROES_TRINKETS:
				this.UpdateHeroesTrinkets();
				break;
			case UiState.SELECT_TRINKET:
				this.UpdateSelectTrinket();
				break;
			case UiState.HUB_ACHIEVEMENTS:
				this.panelAchievements.UpdatePanel(sim, this);
				break;
			case UiState.SHOP_MINE:
				this.UpdateMine(dt);
				break;
			case UiState.SHOP_TRINKET_OPEN_POPUP:
				this.UpdateTrinketOpenPopup(dt);
				break;
			case UiState.CHANGE_SKIN:
				this.UpdateHeroSkinPopup();
				break;
			case UiState.SKIN_BUYING_WARNING:
				this.UpdateSkinBuyingWarningPopup();
				break;
			case UiState.RIFT_SELECT_POPUP:
				this.UpdateRiftSelectPopup();
				break;
			case UiState.CHARM_SELECTING:
				this.UpdatePanelCharmSelect();
				break;
			case UiState.RIFT_RUN_CHARMS:
				this.UpdateRunningCharmsTab();
				break;
			case UiState.HUB_CHARMS:
				this.UpdateHubCharms();
				break;
			case UiState.CHARM_INFO_POPUP:
				this.UpdateCharmInfoPopup();
				break;
			case UiState.HUB_ARTIFACTS:
				this.UpdateArtifactScroller();
				break;
			case UiState.HUB_SHOP:
				this.UpdateShopScreen();
				break;
			case UiState.BUY_FLASH_OFFER_CHARM:
				this.UpdateFlashOfferCharmPopup();
				this.panelBuyCharmFlashOffer.menuShowCurrency.SetCurrency(CurrencyType.GEM, Main.instance.GetSim().GetCurrency(CurrencyType.GEM).GetString(), true, GameMode.STANDARD, true);
				break;
			case UiState.BUY_FLASH_OFFER:
				this.UpdateAdventureFlashOfferPopup();
				break;
			case UiState.SHOP_CHARM_PACK_SELECT:
				this.UpdateCharmPackSelect();
				break;
			case UiState.SHOP_CHARM_PACK_OPENING:
				this.UpdateCharmPackOpening();
				break;
			case UiState.HUB_OPTIONS_WIKI:
				this.UpdateHubOptionsWikiScreen();
				break;
			case UiState.TRINKET_INFO_POPUP:
				this.UpdatePanelTrinketInfoPopup();
				break;
			case UiState.TRINKET_RECYCLE_POPUP:
				this.UpdateTrinketRecyclePopup();
				break;
			case UiState.CHRISTMAS_PANEL:
				this.panelChristmasOffer.UpdatePanel(sim, dt);
				break;
			case UiState.CHRISTMAS_OFFERS_INFO_POPUP:
				this.panelChristmasOffersInfo.UpdatePopup(sim);
				break;
			case UiState.CHRISTMAS_CANDY_TREAT_POPUP:
				this.popupChristmasCandyTreat.UpdatePopup(sim);
				break;
			case UiState.DAILY_QUEST:
				this.dailyQuestPopup.UpdatePopup(this);
				break;
			case UiState.CONVERT_XMAS_SCRAP:
				this.panelScrapConverter.scrapCurrency.SetCurrency(CurrencyType.SCRAP, sim.GetScraps().GetString(), true, GameMode.STANDARD, true);
				break;
			case UiState.ARTIFACT_SELECTED_POPUP:
				this.UpdatePanelArtifactPopup();
				break;
			case UiState.SECOND_ANNIVERSARY_POPUP:
				this.secondAnniversaryPopup.UpdatePopup(sim);
				break;
			}
			if (UiManager.stateJustChanged)
			{
				this.FitScrollViewContent();
			}
			this.UpdateMenuCurrencies();
			bool isModeInAction = sim.IsGameModeInAction(activeWorld.gameMode);
			if (!activeWorld.IsRaining())
			{
				this.UpdateTutorialStateChecks(isModeInAction);
			}
			TutorialManager.SetTutorial(this.panelTutorial, taps, activeWorld, this, dt);
			if (UiManager.isBuyingHero || UiManager.isPurchasing || this.acceptingServerReward || (RewardedAdManager.inst != null && RewardedAdManager.inst.IsWatchingAnyAd()))
			{
				this.modelPurchaseLoading.SetActive(true);
				this.iconPurchaseLoading.eulerAngles += new Vector3(0f, 0f, 240f * dt);
			}
			else
			{
				this.modelPurchaseLoading.SetActive(false);
			}
			this.CallToInits();
			this.CallToUpdates(dt);
			if (flag)
			{
				UiManager.stateJustChanged = false;
			}
			UiManager.resetButtonStates = false;
		}

		private void UpdatePanelTrinketInfoPopup()
		{
			this.panelTrinketInfoPopup.trinketInfoBody.SetTrinket(this.sim);
			bool active = this.sim.allTrinkets.Count > 1;
			this.panelTrinketInfoPopup.buttonNext.gameObject.SetActive(active);
			this.panelTrinketInfoPopup.buttonPrevious.gameObject.SetActive(active);
			Trinket selectedTrinket = this.panelTrinketInfoPopup.trinketInfoBody.selectedTrinket;
			if (this.panelTrinketInfoPopup.stateToReturn == UiState.HUB_DATABASE_TRINKETS)
			{
				this.panelTrinketInfoPopup.buttonEquip.gameObject.SetActive(false);
				this.panelTrinketInfoPopup.trinketInfoBody.buttonUpgrade.rectTransform.SetSizeDeltaX(250f);
				this.panelTrinketInfoPopup.trinketInfoBody.buttonUpgrade.rectTransform.SetAnchorPosX(150f);
				this.panelTrinketInfoPopup.trinketInfoBody.imageMax.rectTransform.SetAnchorPosX(150f);
				this.panelTrinketInfoPopup.buttonRecycle.rectTransform.SetSizeDeltaX(250f);
				this.panelTrinketInfoPopup.buttonRecycle.rectTransform.SetAnchorPosX(-150f);
			}
			else
			{
				this.panelTrinketInfoPopup.buttonEquip.gameObject.SetActive(true);
				this.panelTrinketInfoPopup.buttonRecycle.rectTransform.SetAnchorPosX(-230f);
				this.panelTrinketInfoPopup.buttonRecycle.rectTransform.SetSizeDeltaX(200f);
				this.panelTrinketInfoPopup.trinketInfoBody.buttonUpgrade.rectTransform.SetSizeDeltaX(200f);
				this.panelTrinketInfoPopup.trinketInfoBody.buttonUpgrade.rectTransform.SetAnchorPosX(230f);
				this.panelTrinketInfoPopup.trinketInfoBody.imageMax.rectTransform.SetAnchorPosX(230f);
				HeroDataBase heroWithTrinket = this.sim.GetHeroWithTrinket(selectedTrinket);
				HeroDataBase dataBase = this.selectedHeroGearSkills.GetData().GetDataBase();
				if (heroWithTrinket != null && dataBase == heroWithTrinket)
				{
					this.panelTrinketInfoPopup.buttonEquip.interactable = false;
					this.panelTrinketInfoPopup.buttonEquip.text.text = LM.Get("UI_EQUIPPED");
				}
				else
				{
					this.panelTrinketInfoPopup.buttonEquip.interactable = true;
					this.panelTrinketInfoPopup.buttonEquip.text.text = LM.Get("UI_EQUIP");
				}
			}
			bool isCollected = this.sim.GetUnlock(UnlockIds.TRINKET_DISASSEMBLE).isCollected;
			this.panelTrinketInfoPopup.buttonRecycle.text.text = LM.Get((!isCollected) ? "UI_DESTROY" : "TRINKET_DISASSEMBLE");
			bool active2 = this.sim.IsTrinketPinned(selectedTrinket) != -1;
			this.panelTrinketInfoPopup.pinnedTrinketParent.SetActive(active2);
			this.panelTrinketInfoPopup.menuShowCurrency.SetCurrency(CurrencyType.SCRAP, this.sim.GetCurrency(CurrencyType.SCRAP).GetString(), true, GameMode.STANDARD, true);
		}

		private void UpdateCharmPackSelect()
		{
			this.panelCharmPackSelect.buttonOpen.gameObject.SetActive(!this.panelCharmPackSelect.isBigPack && this.sim.numSmallCharmPacks > 0);
			this.panelCharmPackSelect.buttonBuy.gameObject.SetActive(this.panelCharmPackSelect.isBigPack || this.sim.numSmallCharmPacks <= 0);
			if (this.panelCharmPackSelect.isBigPack || this.sim.numSmallCharmPacks == 0)
			{
				ShopPack shopPack = (!this.panelCharmPackSelect.isBigPack) ? this.sim.shopPackSmallCharm : this.sim.shopPackBigCharm;
				this.panelCharmPackSelect.buttonBuy.textUp.text = GameMath.GetDoubleString(shopPack.cost);
				this.panelCharmPackSelect.buttonBuy.iconUpType = ButtonUpgradeAnim.GetIconTypeFromCurrency(shopPack.currency);
				this.panelCharmPackSelect.buttonBuy.textUpCantAffordColorChangeForced = !this.sim.CanCurrencyAfford(shopPack.currency, shopPack.cost);
			}
		}

		private void UpdateCharmPackOpening()
		{
			if (!this.panelCharmPackOpening.isBigPack)
			{
				if (this.panelCharmPackOpening.isCardBackActive)
				{
					this.panelCharmPackOpening.charmOptionCard.charmCard.background.sprite = this.uiData.spriteCharmBack;
					this.panelCharmPackOpening.charmOptionCard.charmCard.newLabelParent.gameObject.SetActive(false);
				}
				else
				{
					this.panelCharmPackOpening.charmOptionCard.charmCard.newLabelParent.gameObject.SetActive(this.panelCharmPackOpening.charmDupplicateToOpen.charmData.isNew);
					this.UpdateCharmOptionCard(this.panelCharmPackOpening.charmDupplicateToOpen.charmData, this.panelCharmPackOpening.charmOptionCard, false);
				}
			}
		}

		private void UpdateSkinBuyingWarningPopup()
		{
			SkinData selectedSkin = this.panelHeroSkinChanger.selectedSkin;
			if (selectedSkin != null)
			{
				this.panelSkinBuyingPopup.buttonYes.iconUpType = ButtonUpgradeAnim.GetIconTypeFromCurrency(selectedSkin.currency);
				FlashOffer skinOfferIfExist = this.sim.GetSkinOfferIfExist(selectedSkin);
				if (skinOfferIfExist != null && skinOfferIfExist.isHalloween)
				{
					this.panelSkinBuyingPopup.popupParent.SetSizeDeltaY(280f);
					this.panelSkinBuyingPopup.halloweenScrap.gameObject.SetActive(true);
					this.panelSkinBuyingPopup.halloweenScrapPlus.gameObject.SetActive(true);
				}
				else
				{
					this.panelSkinBuyingPopup.popupParent.SetSizeDeltaY(160f);
					this.panelSkinBuyingPopup.halloweenScrap.gameObject.SetActive(false);
					this.panelSkinBuyingPopup.halloweenScrapPlus.gameObject.SetActive(false);
				}
				double num = (skinOfferIfExist != null) ? this.sim.GetFlashOfferCost(skinOfferIfExist) : selectedSkin.cost;
				this.panelSkinBuyingPopup.buttonYes.gameButton.interactable = this.sim.CanCurrencyAfford(selectedSkin.currency, num);
				this.panelSkinBuyingPopup.buttonYes.textUp.text = GameMath.GetDoubleString(num);
			}
		}

		private void UpdateRiftSelectPopup()
		{
			bool isCurseMode = this.panelRiftSelect.isCurseMode;
			if (this.sim.IsNextRiftsDiscoverable() && !isCurseMode)
			{
				int riftCountWillDiscover = this.sim.GetRiftCountWillDiscover();
				this.panelRiftSelect.discoverFive.text = string.Format(LM.Get("UI_DISCOVER_NEW_RIFTS"), riftCountWillDiscover);
				if (!this.panelRiftSelect.isOnDiscoverMode)
				{
					this.panelRiftSelect.EnableDiscoverButton();
				}
			}
			else
			{
				this.panelRiftSelect.DisableDiscoverButton();
			}
			if (!this.panelRiftSelect.isOnDiscoverMode)
			{
				bool flag = this.sim.IsCursedRiftsModeUnlocked();
				this.panelRiftSelect.SetPositioning(flag, isCurseMode);
				if (flag)
				{
					this.panelRiftSelect.tabButtonsParent.gameObject.SetActive(true);
					this.panelRiftSelect.buttonTabNormal.interactable = isCurseMode;
					this.panelRiftSelect.buttonTabCursed.interactable = !isCurseMode;
				}
				else
				{
					this.panelRiftSelect.tabButtonsParent.gameObject.SetActive(false);
				}
				World world = this.sim.GetWorld(GameMode.RIFT);
				List<ChallengeRift> list;
				if (isCurseMode)
				{
					this.panelRiftSelect.aeonDustMissionParent.gameObject.SetActive(false);
					list = this.sim.GetCursedRiftChallenges();
				}
				else
				{
					this.panelRiftSelect.aeonDustMissionParent.gameObject.SetActive(true);
					list = this.sim.GetDiscoveredRiftChallenges();
				}
				int count = list.Count;
				int num = (this.sim.IsNextRiftsDiscoverable() || !this.sim.HasRiftDiscover()) ? 0 : 1;
				if (isCurseMode)
				{
					int num2 = (!this.sim.cursedRiftSlots.HasUnlock()) ? 0 : 1;
					this.panelRiftSelect.SetUnhiddenChallengeCount(this.sim.cursedRiftSlots.GetMaxSlotCount());
					this.panelRiftSelect.SetCurseEffectIcons();
				}
				else
				{
					this.panelRiftSelect.SetUnhiddenChallengeCount(count + num);
				}
				this.panelRiftSelect.UpdateScroll();
				if (this.panelRiftSelect.targetIndexToFocus != -1 && list.Count > this.panelRiftSelect.targetIndexToFocus)
				{
					this.panelRiftSelect.SetUnhiddenChallengeCount(count + 1);
					this.panelRiftSelect.AnimateScrollPosition(this.panelRiftSelect.targetIndexToFocus);
					this.panelRiftSelect.OnSelecRift(this.panelRiftSelect.targetIndexToFocus);
					this.panelRiftSelect.targetIndexToFocus = -1;
				}
				if (!isCurseMode && this.panelRiftSelect.isRestBonusInfoShowing)
				{
					this.panelRiftSelect.infoMaxRestBonusAmount.text = string.Format(LM.Get("UNLOCK_REWARD_AEONS"), GameMath.GetDoubleString(this.sim.GetRiftRestBonusPerDay()));
					if (TrustedTime.IsReady())
					{
						double num3 = 1.0 - this.sim.GetDaysPassedSinceLastTimeRiftRestBonusCollected();
						if (num3 < 0.0)
						{
							this.panelRiftSelect.infoMaxRestBonusTimeAmount.text = LM.Get("UI_HEROES_MAXED_OUT");
						}
						else
						{
							this.panelRiftSelect.infoMaxRestBonusTimeAmount.text = GameMath.GetTimeLessDetailedString(num3 * 86400.0, true);
						}
					}
					else
					{
						this.panelRiftSelect.infoMaxRestBonusTimeAmount.text = LM.Get("UI_SHOP_CHEST_0_WAIT");
					}
				}
				int elementCountToUpdate = this.panelRiftSelect.GetElementCountToUpdate();
				int firstIndexOfEnabledItem = this.panelRiftSelect.firstIndexOfEnabledItem;
				int emptyCursedRiftSlotCount = this.sim.GetEmptyCursedRiftSlotCount();
				int slotCount = this.sim.cursedRiftSlots.slotCount;
				for (int i = 0; i < elementCountToUpdate; i++)
				{
					int num4 = firstIndexOfEnabledItem + i;
					bool flag2 = false;
					RiftOptionWidget riftOptionWidget = this.panelRiftSelect.riftOptionWidgets[i];
					riftOptionWidget.curseOrnements.gameObject.SetActive(isCurseMode);
					ChallengeRift challengeRift = null;
					if (list.Count <= num4)
					{
						flag2 = true;
					}
					else
					{
						challengeRift = list[num4];
					}
					if (flag2)
					{
						if (isCurseMode)
						{
							if (list.Count == num4 && emptyCursedRiftSlotCount > 0)
							{
								riftOptionWidget.SetNewCurseCooldown();
								riftOptionWidget.nextCursedCooldown.text = this.GetNextCurseRiftString();
							}
							else if (num4 >= slotCount)
							{
								riftOptionWidget.riftEffectsParent.gameObject.SetActive(false);
								riftOptionWidget.riftDifficulty.gameObject.SetActive(false);
								riftOptionWidget.riftRecord.gameObject.SetActive(false);
								riftOptionWidget.SetLocked();
								riftOptionWidget.riftName.gameObject.SetActive(false);
								SlotUnlockCondition slot = this.sim.cursedRiftSlots.GetSlot(num4);
								int riftIndex = this.sim.GetRiftIndex(slot.riftIdToBeat);
								riftOptionWidget.lockHint.text = string.Format(LM.Get("RIFT_UNLOCK_HINT"), riftIndex + 1);
							}
							else
							{
								riftOptionWidget.SetNonExistingCurse();
							}
						}
						else
						{
							riftOptionWidget.SetHiting();
						}
					}
					else if (world.IsRiftChallengeUnlocked(num4, this.sim.riftDiscoveryIndex))
					{
						Unlock unlock = challengeRift.unlock;
						riftOptionWidget.SetUnlocked();
						if (riftOptionWidget.selectionFrame.gameObject.activeSelf)
						{
							riftOptionWidget.SetBackgroundColor();
						}
						if (this.panelRiftSelect.hardUpdate)
						{
							riftOptionWidget.riftEffectsParent.gameObject.SetActive(true);
							riftOptionWidget.SetRiftEffects(challengeRift, this);
						}
						if (unlock.isCollected)
						{
							riftOptionWidget.SetBestRecord(challengeRift.finishingRecord);
							riftOptionWidget.firstTimeRewardParent.gameObject.SetActive(false);
							riftOptionWidget.SetRewardAeonDust(challengeRift.riftPointReward, this);
						}
						else
						{
							riftOptionWidget.riftRecord.gameObject.SetActive(false);
							if (isCurseMode)
							{
								riftOptionWidget.firstTimeRewardParent.gameObject.SetActive(false);
							}
							else
							{
								riftOptionWidget.firstTimeRewardParent.gameObject.SetActive(true);
							}
							riftOptionWidget.SetReward(unlock.GetReward(), this);
						}
						RiftDifficulty difficulty = RiftUtility.GetDifficulty(this.sim, world, challengeRift, this.sim.GetUniversalBonusRift());
						if (isCurseMode)
						{
							riftOptionWidget.riftName.text = string.Format(LM.Get("RIFT_RUN_NAME"), challengeRift.riftData.cursesSetup.originalRiftNo + 1);
						}
						else
						{
							riftOptionWidget.riftName.text = string.Format(LM.Get("RIFT_RUN_NAME"), num4 + 1);
						}
						riftOptionWidget.riftDifficulty.gameObject.SetActive(true);
						riftOptionWidget.riftDifficulty.text = RiftUtility.GetColoredDifficultyName(difficulty);
					}
					else
					{
						riftOptionWidget.riftEffectsParent.gameObject.SetActive(false);
						riftOptionWidget.riftDifficulty.gameObject.SetActive(false);
						riftOptionWidget.riftRecord.gameObject.SetActive(false);
						riftOptionWidget.lockHint.text = LM.Get("UI_RIFT_UNLOCK_HINT");
						riftOptionWidget.SetLocked();
						riftOptionWidget.riftName.text = string.Format(LM.Get("RIFT_RUN_NAME"), num4 + 1);
					}
				}
				if (this.sim.hasRiftQuest && !isCurseMode && this.panelRiftSelect.selectedRiftIndex != -1)
				{
					ChallengeRift challengeRift2 = world.allChallenges[this.panelRiftSelect.selectedRiftIndex] as ChallengeRift;
					if (challengeRift2.unlock.isCollected || challengeRift2.unlock.GetReward() is UnlockRewardAeonDust)
					{
						if (challengeRift2.unlock.GetReward() is UnlockRewardAeonDust)
						{
							UnlockRewardAeonDust unlockRewardAeonDust = challengeRift2.unlock.GetReward() as UnlockRewardAeonDust;
							this.panelRiftSelect.referenceSlider.SetScale((float)unlockRewardAeonDust.GetAmount());
						}
						else
						{
							this.panelRiftSelect.referenceSlider.SetScale((float)this.sim.GetRiftQuestProgress(challengeRift2.riftPointReward));
						}
						this.panelRiftSelect.referenceSlider.SetScale((float)this.sim.GetRiftQuestProgress(challengeRift2.riftPointReward));
					}
					else
					{
						this.panelRiftSelect.referenceSlider.SetScale(0f);
					}
					if (!this.panelRiftSelect.isAnimatingLevelUp)
					{
						this.panelRiftSelect.riftScoreProgresBar.SetSlider(this.sim.riftQuestDustCollected, this.sim.GetRiftQuestDustRequired(), false);
						if (TrustedTime.IsReady())
						{
							double currentRiftQuestStandardReward = this.sim.GetCurrentRiftQuestStandardReward();
							double currentRiftQuestRestReward = this.sim.GetCurrentRiftQuestRestReward();
							this.panelRiftSelect.rewardText.text = GameMath.GetDoubleString(currentRiftQuestStandardReward);
							this.panelRiftSelect.remainingPointsToNextReardHint.text = LM.Get("RIFT_AEON_DAILY_BONUS");
							this.panelRiftSelect.rewardBonusText.text = "+" + GameMath.GetDoubleString(currentRiftQuestRestReward);
							if (this.sim.IsRiftQuestRestBonusCapped())
							{
								this.panelRiftSelect.rewardBonusText.color = new Color(0.97f, 0.73f, 0f);
							}
							else
							{
								this.panelRiftSelect.rewardBonusText.color = new Color(0.875f, 0.99f, 0.09f);
							}
						}
						else
						{
							double currentRiftQuestStandardReward2 = this.sim.GetCurrentRiftQuestStandardReward();
							this.panelRiftSelect.rewardText.text = GameMath.GetDoubleString(currentRiftQuestStandardReward2);
							this.panelRiftSelect.remainingPointsToNextReardHint.text = LM.Get("UI_SHOP_CHEST_0_WAIT");
							this.panelRiftSelect.rewardBonusText.text = "0";
							this.panelRiftSelect.rewardBonusText.color = new Color(0.875f, 0.99f, 0.09f);
						}
					}
					this.panelRiftSelect.riftScoreProgresBar.aeonIconGlow.SetActive(challengeRift2.unlock.isCollected && challengeRift2.riftPointReward + this.sim.riftQuestDustCollected >= this.sim.GetRiftQuestDustRequired());
				}
				this.panelRiftSelect.hardUpdate = false;
			}
		}

		private string GetNextCurseRiftString()
		{
			if (TrustedTime.IsReady())
			{
				double timeInSeconds = 14400.0 - (TrustedTime.Get() - this.sim.lastAddedCurseChallengeTime).TotalSeconds;
				return string.Format(LM.Get("NEW_CURSED_RIFT_IN"), GameMath.GetTimeString(timeInSeconds));
			}
			return LM.Get("UI_SHOP_CHEST_0_WAIT");
		}

		private void UpdatePanelArtifactPopup()
		{
			this.panelArtifactPopup.mythstoneCurrency.SetCurrency(CurrencyType.MYTHSTONE, GameMath.GetFlooredDoubleString(this.sim.GetMythstones().GetAmount()), true, GameMode.STANDARD, true);
			if (this.panelArtifactPopup.forceUpdate)
			{
				this.panelArtifactPopup.SetArtifact(true);
				this.panelArtifactPopup.forceUpdate = false;
			}
			this.panelArtifactPopup.buttonLeft.gameObject.SetActive(this.sim.artifactsManager.Artifacts.Count > 1);
			this.panelArtifactPopup.buttonRight.gameObject.SetActive(this.sim.artifactsManager.Artifacts.Count > 1);
			this.panelArtifactPopup.UpdateButtons();
		}

		private void UpdatePanelCharmSelect()
		{
			World world = this.sim.GetWorld(GameMode.RIFT);
			int charmSelectionNum = world.GetCharmSelectionNum();
			ChallengeRift challengeRift = world.activeChallenge as ChallengeRift;
			this.panelCharmSelect.closeButton.gameObject.SetActive(TutorialManager.firstCharm == TutorialManager.FirstCharm.FIN);
			if (this.panelCharmSelect.isCurseInfo)
			{
				int count = this.sim.currentCurses.Count;
				for (int i = 0; i < count; i++)
				{
					CurseEffectData enchantment = this.sim.allCurseEffects[this.sim.currentCurses[i]];
					CharmOptionCard charmOptionCard = this.panelCharmSelect.charmOptionCards[i];
					charmOptionCard.interactable = false;
					this.UpdateCharmOptionCard(enchantment, charmOptionCard, true);
				}
			}
			else
			{
				if (challengeRift.nextCharmDraftEffects == null)
				{
					return;
				}
				for (int j = 0; j < charmSelectionNum; j++)
				{
					CharmEffectData enchantment2 = challengeRift.nextCharmDraftEffects[j];
					CharmOptionCard charmOptionCard2 = this.panelCharmSelect.charmOptionCards[j];
					charmOptionCard2.interactable = true;
					if (this.panelCharmSelect.selectedIndex == -1 || !this.panelCharmSelect.IsSelectionInValidRange())
					{
						this.panelCharmSelect.selectButton.interactable = false;
					}
					else
					{
						this.panelCharmSelect.selectButton.interactable = true;
					}
					this.UpdateCharmOptionCard(enchantment2, charmOptionCard2, true);
				}
			}
		}

		private void UpdateCharmOptionCard(EnchantmentEffectData enchantment, CharmOptionCard card, bool skipProgresBar = true)
		{
			this.UpdateSoloEnchantmentCard(card.charmCard, enchantment, skipProgresBar);
			card.charmCardInfo.nameText.text = enchantment.GetName();
			if (enchantment is CurseEffectData)
			{
				card.charmCardInfo.background.color = new Color32(212, 169, 133, byte.MaxValue);
			}
			else
			{
				card.charmCardInfo.background.color = new Color32(217, 200, 153, byte.MaxValue);
			}
			if (!string.IsNullOrEmpty(enchantment.conditionKey))
			{
				card.charmCardInfo.activationDescText.text = string.Format("{0} {1}", enchantment.GetConditionDescFormat(), enchantment.GetConditionDescription());
			}
			else
			{
				card.charmCardInfo.activationDescText.text = LM.Get("CHARM_CONDITION_PERMANENT");
			}
			card.charmCardInfo.descText.text = enchantment.GetDesc();
			card.charmCardInfo.descBackground.SetAlpha(0f);
		}

		private void UpdateRunningCharmsTab()
		{
			World world = this.sim.GetWorld(GameMode.RIFT);
			ChallengeRift challengeRift = world.activeChallenge as ChallengeRift;
			int count = challengeRift.activeCharmEffects.Count;
			if (count == 0)
			{
				this.panelRunningCharms.noCharmParent.gameObject.SetActive(true);
				foreach (CharmOptionCard charmOptionCard in this.panelRunningCharms.runningCharmCards)
				{
					UnityEngine.Object.Destroy(charmOptionCard.gameObject);
				}
				this.panelRunningCharms.runningCharmCards.Clear();
			}
			else
			{
				this.panelRunningCharms.noCharmParent.gameObject.SetActive(false);
				if (this.panelRunningCharms.runningCharmCards.Count != count)
				{
					Utility.FillUiElementList<CharmOptionCard>(this.panelRunningCharms.charmOptionCardPrefab, this.panelRunningCharms.charmCardsParent, count, this.panelRunningCharms.runningCharmCards);
					float num = 140f;
					float num2 = 230f;
					for (int i = 0; i < count; i++)
					{
						CharmOptionCard charmOptionCard2 = this.panelRunningCharms.runningCharmCards[i];
						charmOptionCard2.rectTransform.anchorMax = new Vector2(0.5f, 1f);
						charmOptionCard2.rectTransform.anchorMin = new Vector2(0.5f, 1f);
						charmOptionCard2.rectTransform.anchoredPosition = new Vector2(0f, -num - num2 * (float)i);
						charmOptionCard2.interactable = false;
						charmOptionCard2.selectionOutline.gameObject.SetActive(false);
					}
				}
				for (int j = 0; j < count; j++)
				{
					CharmEffectData enchantment = challengeRift.activeCharmEffects[count - 1 - j];
					CharmOptionCard card = this.panelRunningCharms.runningCharmCards[j];
					this.UpdateCharmOptionCard(enchantment, card, true);
				}
				this.panelRunningCharms.sizeFitter.SetSize(0f, false);
			}
		}

		private void UpdateHubCharms()
		{
			this.panelHubCharms.scraps.SetCurrency(CurrencyType.SCRAP, this.sim.GetScraps().GetString(), true, GameMode.STANDARD, true);
			bool flag = !this.sim.IsRiftUnlocked() && this.sim.HasRiftHint();
			this.panelHubCharms.filterToggleButton.text.text = PanelHubCharms.GetLocalizedFilterName(this.sim.charmSortType);
			if (flag)
			{
				this.panelHubCharms.scrollRect.gameObject.SetActive(false);
				this.panelHubCharms.bonusTabParent.gameObject.SetActive(false);
				this.panelHubCharms.noCharmsParent.gameObject.SetActive(true);
			}
			else if ((Time.frameCount % 10 == 0 || this.panelHubCharms.forceUpdate) && !this.panelHubCharms.isAnimatingSort)
			{
				if (this.sim.isCharmSortingShowing)
				{
					this.panelHubCharms.ShowFilterOptions();
				}
				else
				{
					this.panelHubCharms.HideFilterOptions();
				}
				if (this.sim.charmSortType == CharmSortType.Default)
				{
					this.panelHubCharms.filterArrow.gameObject.SetActive(false);
				}
				else
				{
					this.panelHubCharms.filterArrow.gameObject.SetActive(true);
					if (this.sim.isCharmSortingDescending)
					{
						this.panelHubCharms.filterArrow.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
					}
					else
					{
						this.panelHubCharms.filterArrow.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
					}
				}
				this.panelHubCharms.forceUpdate = false;
				this.panelHubCharms.scrollRect.gameObject.SetActive(true);
				this.panelHubCharms.bonusTabParent.gameObject.SetActive(true);
				this.panelHubCharms.noCharmsParent.gameObject.SetActive(false);
				List<CharmEffectData> sortedCharms = this.sim.GetSortedCharms();
				int count = sortedCharms.Count;
				this.panelHubCharms.FindIndexChanges(sortedCharms);
				if (this.panelHubCharms.isAnimatingSort)
				{
					return;
				}
				UniversalTotalBonus universalBonus = this.sim.GetWorld(GameMode.RIFT).universalBonus;
				this.panelHubCharms.bonusDamage.icon.sprite = this.GetUTBSprite(CharmType.Attack, false);
				this.panelHubCharms.bonusHealth.icon.sprite = this.GetUTBSprite(CharmType.Defense, false);
				this.panelHubCharms.bonusGold.icon.sprite = this.GetUTBSprite(CharmType.Utility, false);
				if (this.panelHubCharms.animateBonus)
				{
					if (Math.Abs(universalBonus.charmDamageFactor - this.panelHubCharms.bonusDamageLastAmount) > 0.0001)
					{
						this.AnimateHubCharmBonusText(universalBonus.charmDamageFactor, this.panelHubCharms.bonusDamageLastAmount, this.panelHubCharms.bonusDamage.textBonus);
					}
					else if (Math.Abs(universalBonus.charmGoldFactor - this.panelHubCharms.bonusGoldLastAmount) > 0.0001)
					{
						this.AnimateHubCharmBonusText(universalBonus.charmGoldFactor, this.panelHubCharms.bonusGoldLastAmount, this.panelHubCharms.bonusGold.textBonus);
					}
					else if (Math.Abs(universalBonus.charmHealthFactor - this.panelHubCharms.bonusHealthLastAmount) > 0.0001)
					{
						this.AnimateHubCharmBonusText(universalBonus.charmHealthFactor, this.panelHubCharms.bonusHealthLastAmount, this.panelHubCharms.bonusHealth.textBonus);
					}
					this.panelHubCharms.animateBonus = false;
				}
				if (!this.panelHubCharms.isAnimatingBonus)
				{
					this.panelHubCharms.bonusDamage.textBonus.text = "x" + GameMath.GetDetailedNumberString(universalBonus.charmDamageFactor);
					this.panelHubCharms.bonusHealth.textBonus.text = "x" + GameMath.GetDetailedNumberString(universalBonus.charmHealthFactor);
					this.panelHubCharms.bonusGold.textBonus.text = "x" + GameMath.GetDetailedNumberString(universalBonus.charmGoldFactor);
				}
				if (this.panelHubCharms.charmCards.Count != count)
				{
					this.panelHubCharms.FillCharmsScroll(new Action<int>(this.Button_OnClickHubCharm));
				}
				for (int i = 0; i < count; i++)
				{
					CharmCard charmCard = this.panelHubCharms.charmCards[i];
					CharmEffectData charmEffectData = sortedCharms[i];
					charmCard.charmId = charmEffectData.BaseData.id;
					if (charmEffectData.IsLocked())
					{
						charmCard.button.enabled = false;
						charmCard.SetCardBack(this.uiData.spriteCharmSlot);
					}
					else
					{
						charmCard.button.enabled = true;
						charmCard.levelProgresBarParent.gameObject.SetActive(true);
						charmCard.levelParent.gameObject.SetActive(true);
						charmCard.charmIcon.gameObject.SetActive(true);
						if (charmEffectData.isNew)
						{
							charmCard.newLabelParent.gameObject.SetActive(true);
							charmCard.levelParent.gameObject.SetActive(false);
						}
						else
						{
							charmCard.newLabelParent.gameObject.SetActive(false);
							charmCard.levelParent.gameObject.SetActive(true);
						}
						this.UpdateSoloEnchantmentCard(charmCard, charmEffectData, false);
					}
				}
			}
		}

		private void AnimateHubCharmBonusText(double target, double current, Text text)
		{
			this.panelHubCharms.isAnimatingBonus = true;
			Sequence s = DOTween.Sequence();
			s.Append(DOTween.To(() => current, delegate(double x)
			{
				current = x;
			}, target, 0.4f)).AppendCallback(delegate
			{
				this.panelHubCharms.isAnimatingBonus = false;
			}).Append(text.rectTransform.DOScale(1.3f, 0.3f)).AppendInterval(0.3f).Append(text.rectTransform.DOScale(1f, 0.2f)).OnUpdate(delegate
			{
				text.text = "x" + GameMath.GetDetailedNumberString(current);
			}).Play<Sequence>();
		}

		private int SortCharmsByLockStateAndType(CharmEffectData a, CharmEffectData b)
		{
			int num = this.SortCharmByUnlockState(a, b);
			if (num != 0)
			{
				return -num;
			}
			return this.sim.GetCharmId(a).CompareTo(this.sim.GetCharmId(b));
		}

		private int SortCharmByUnlockState(CharmEffectData a, CharmEffectData b)
		{
			if (a == null || b == null)
			{
				return 0;
			}
			if (a.IsLocked() && !b.IsLocked())
			{
				return -1;
			}
			if (!a.IsLocked() && b.IsLocked())
			{
				return 1;
			}
			return 0;
		}

		private void Button_OnClickHubCharm(int charmId)
		{
			if (!this.sim.HasCharmCard(charmId))
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiDefaultFailClick, 1f));
				return;
			}
			this.state = UiState.CHARM_INFO_POPUP;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
			this.panelCharmInfoPopup.needToUpdate = true;
			this.panelCharmInfoPopup.selectedCharmId = charmId;
			this.command = new UICommandCharmSelected
			{
				id = charmId
			};
		}

		private void Button_OnClickCharmUpgrade()
		{
			this.panelHubCharms.animateBonus = true;
			this.panelCharmInfoPopup.needToUpdate = true;
			this.panelCharmInfoPopup.DoUpgradeAnim();
			this.command = new UICommandUpgradeCharm
			{
				charmId = this.panelCharmInfoPopup.selectedCharmId
			};
		}

		private void UpdateCharmInfoPopup()
		{
			if (this.panelCharmInfoPopup.needToUpdate)
			{
				this.panelCharmInfoPopup.needToUpdate = false;
				CharmEffectData charmEffectData = this.sim.allCharmEffects[this.panelCharmInfoPopup.selectedCharmId];
				this.panelCharmInfoPopup.header.text = charmEffectData.GetName();
				this.panelCharmInfoPopup.charmDesc.text = charmEffectData.GetDesc(!charmEffectData.IsMaxed());
				if (charmEffectData.BaseData.isAlwaysActive)
				{
					this.panelCharmInfoPopup.charmTriggerDesc.text = LM.Get("CHARM_CONDITION_PERMANENT");
				}
				else
				{
					this.panelCharmInfoPopup.charmTriggerDesc.text = string.Format("{0} {1}", LM.Get("CHARM_CONDITION_DESC"), charmEffectData.GetActivationDesc(!charmEffectData.IsMaxed()));
				}
				this.panelCharmInfoPopup.menuShowCurrency.SetCurrency(CurrencyType.SCRAP, this.sim.GetCurrency(CurrencyType.SCRAP).GetString(), true, GameMode.STANDARD, true);
				if (charmEffectData.IsMaxed())
				{
					this.panelCharmInfoPopup.cannotUpgradeDesc.gameObject.SetActive(true);
					this.panelCharmInfoPopup.cannotUpgradeDesc.text = LM.Get("CHARM_FULL_UPGRADED_DESC");
					this.panelCharmInfoPopup.universalTotalBonusWidget.gameObject.SetActive(false);
					this.panelCharmInfoPopup.upgradeButton.gameObject.SetActive(false);
					this.panelCharmInfoPopup.maxedTextParent.gameObject.SetActive(true);
				}
				else
				{
					this.panelCharmInfoPopup.upgradeButton.gameObject.SetActive(true);
					this.panelCharmInfoPopup.maxedTextParent.gameObject.SetActive(false);
					bool flag = this.sim.CanAffordCharmUpgrade(charmEffectData) && charmEffectData.CanLevelUp();
					this.panelCharmInfoPopup.upgradeButton.gameButton.interactable = flag;
					this.panelCharmInfoPopup.cannotUpgradeDesc.gameObject.SetActive(!flag);
					this.panelCharmInfoPopup.universalTotalBonusWidget.gameObject.SetActive(flag);
					if (flag)
					{
						this.panelCharmInfoPopup.universalTotalBonusWidget.icon.sprite = this.GetUTBSprite(charmEffectData.BaseData.charmType, true);
						string s = "+" + GameMath.GetPercentString(CharmEffectData.GLOBAL_BONUS_PER_LEVEL - 1.0, false);
						this.panelCharmInfoPopup.universalTotalBonusWidget.textBonus.text = AM.csg(s);
					}
					else if (charmEffectData.CanLevelUp())
					{
						this.panelCharmInfoPopup.cannotUpgradeDesc.text = LM.Get("CHARM_UPGRADE_COLLECT_MORE_SCRAP");
					}
					else
					{
						this.panelCharmInfoPopup.cannotUpgradeDesc.text = LM.Get("CHARM_UPGRADE_COLLECT_MORE_DUPE");
					}
					this.panelCharmInfoPopup.upgradeButton.iconUpType = ButtonUpgradeAnim.IconType.SCRAPS;
					this.panelCharmInfoPopup.upgradeButton.textUp.text = GameMath.GetDoubleString(this.sim.GetCharmUpgradeCost(charmEffectData));
				}
				if (!this.panelCharmInfoPopup.dontUpdateLevel)
				{
					this.UpdateSoloEnchantmentCard(this.panelCharmInfoPopup.charmCard, charmEffectData, false);
				}
			}
		}

		public void UpdateSoloEnchantmentCard(CharmCard charmCard, EnchantmentEffectData enchantmentData, bool skipProgressBar = false)
		{
			if (charmCard.onlyCardBack)
			{
				charmCard.backgroundFrame.enabled = false;
				return;
			}
			CharmEffectData charmEffectData = enchantmentData as CharmEffectData;
			bool flag = charmEffectData == null;
			charmCard.charmIcon.sprite = ((!flag) ? this.spritesCharmEffectIcon : this.spritesCurseEffectIcon)[enchantmentData.baseData.id];
			charmCard.charmIcon.rectTransform.anchoredPosition = ((!flag) ? charmCard.charmIconPos : charmCard.curseIconPos);
			charmCard.levelText.text = LM.Get("UI_HEROES_LV") + " " + (enchantmentData.level + 1);
			charmCard.background.sprite = ((!flag) ? this.GetCharmCardFace(charmEffectData.BaseData.charmType) : this.uiData.spriteCurseFace);
			if (charmCard.backgroundFrame != null)
			{
				charmCard.backgroundFrame.enabled = flag;
			}
			if (flag)
			{
				charmCard.background.color = this.uiData.curseFaceColor;
				bool flag2 = enchantmentData is CurseEffectGhostlyHeroes;
				if (!flag2)
				{
					charmCard.levelText.color = new Color(1f, 0.9490196f, 0.7450981f, 1f);
					charmCard.levelParentImage.color = new Color32(212, 28, 28, byte.MaxValue);
				}
				charmCard.levelParent.gameObject.SetActive(!flag2);
				charmCard.backgroundFrame.color = ((!flag2) ? this.uiData.curseFrameNormalColor : this.uiData.curseFrameGhostlyHeroesColor);
				charmCard.charmIcon.color = ((!flag2) ? this.uiData.curseFrameNormalColor : this.uiData.curseFrameGhostlyHeroesColor);
				return;
			}
			charmCard.background.color = this.uiData.charmFaceColor;
			charmCard.levelText.color = new Color(0.3490196f, 0.2784314f, 0.1960784f, 1f);
			charmCard.levelParentImage.color = new Color32(227, 187, 114, byte.MaxValue);
			charmCard.charmIcon.color = new Color32(247, 222, 160, byte.MaxValue);
			if (skipProgressBar)
			{
				return;
			}
			if (charmEffectData.CanLevelUp())
			{
				charmCard.fillImage.color = CharmCard.greenSliderColor;
				charmCard.levelupSpineAnim.gameObject.SetActive(true);
			}
			else
			{
				charmCard.fillImage.color = CharmCard.orangeSliderColor;
				charmCard.levelupSpineAnim.gameObject.SetActive(false);
			}
			if (charmEffectData.IsMaxed())
			{
				charmCard.maxedText.gameObject.SetActive(true);
				charmCard.maxedText.text = "+" + charmEffectData.unspendDuplicates.ToString();
				charmCard.levelProgresBar.gameObject.SetActive(false);
			}
			else
			{
				charmCard.maxedText.gameObject.SetActive(false);
				charmCard.levelProgresBar.gameObject.SetActive(true);
				charmCard.levelProgresBar.SetScale(charmEffectData.GetProgress());
				charmCard.levelProgresBarText.text = charmEffectData.GetProgresString();
			}
		}

		private Color GetCharmLevelBackgroundColor(int level)
		{
			if (level < 5)
			{
				return new Color(0.439f, 0.6f, 0.133f);
			}
			if (level < 10)
			{
				return new Color(0.301f, 0.572f, 0.843f);
			}
			if (level < 15)
			{
				return new Color(0.415f, 0.25f, 0.537f);
			}
			if (level < 20)
			{
				return new Color(0.8f, 0.8f, 0.25f);
			}
			throw new Exception();
		}

		private void UpdateCheats()
		{
			this.textDebugVersion.text = "version: " + Cheats.version;
			this.textPlayTime.text = "Played for: " + GameMath.GetTimeString(Cheats.timePassed);
		}

		private void UpdateTransition(World activeWorld)
		{
			if (activeWorld.doTransition)
			{
				activeWorld.doTransition = false;
				this.panelTransitionFade.StartAnim();
			}
			if (this.panelTransitionFade.callTransitionEventNow)
			{
				this.panelTransitionFade.callTransitionEventNow = false;
				this.command = new UiCommandTransition();
			}
			if (this.loadingTransition.callTransitionEventNow)
			{
				this.loadingTransition.callTransitionEventNow = false;
				this.state = this.loadingTransition.stateToGo;
				if (this.loadingTransition.stateToGo == UiState.HUB)
				{
					this.panelHub.DoFadeIn(delegate
					{
					});
				}
			}
			if (this.loadingTransition.callTransitionEndEventNow)
			{
				this.loadingTransition.callTransitionEndEventNow = false;
				if (this.state == UiState.NONE && this.willIntroduceCurses)
				{
					this.willIntroduceCurses = false;
					this.panelCharmSelect.isCurseInfo = true;
					this.state = UiState.CHARM_SELECTING;
				}
			}
		}

		private void CheckImmidiateStates(World activeWorld, Challenge challenge, float dt)
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) && !this.modelPurchaseLoading.activeSelf && !TutorialManager.IsThereTutorialCurrently())
			{
				switch (this.state)
				{
				case UiState.NONE:
					if (TutorialManager.IsHubTabUnlocked() && !this.loadingTransition.IsPlaying())
					{
						this.loadingTransition.DoTransition(UiState.HUB, 0f, 0f);
					}
					break;
				case UiState.HUB:
					Application.Quit();
					break;
				case UiState.MODE:
					this.state = UiState.NONE;
					break;
				case UiState.HEROES:
					this.state = UiState.NONE;
					break;
				case UiState.ARTIFACTS:
					this.state = UiState.NONE;
					break;
				case UiState.SHOP:
					this.state = UiState.NONE;
					break;
				case UiState.HEROES_GEAR:
					this.state = UiState.HEROES;
					break;
				case UiState.HEROES_SKILL:
					this.state = UiState.HEROES;
					break;
				case UiState.HEROES_NEW:
					this.state = UiState.HEROES;
					break;
				case UiState.MODE_UNLOCKS:
					this.state = UiState.MODE;
					break;
				case UiState.ARTIFACTS_INFO:
					this.state = this.panelArtifactsInfo.stateToReturn;
					break;
				case UiState.SHOP_LOOTPACK_SELECT:
					this.state = this.shopLootpackSelect.previousState;
					break;
				case UiState.SHOP_LOOTPACK_SUMMARY:
					if (this.panelShop.isHubMode)
					{
						this.state = UiState.HUB_SHOP;
					}
					else
					{
						this.state = UiState.SHOP;
					}
					break;
				case UiState.HUB_OPTIONS:
				case UiState.HUB_MODE_SETUP:
				case UiState.HUB_DATABASE_HEROES_ITEMS:
				case UiState.HUB_DATABASE_TOTEMS:
				case UiState.HUB_DATABASE_TRINKETS:
				case UiState.HUB_CHARMS:
				case UiState.HUB_ARTIFACTS:
				case UiState.HUB_SHOP:
					this.state = UiState.HUB;
					break;
				case UiState.MODE_PRESTIGE:
					this.state = UiState.MODE;
					break;
				case UiState.MODE_MERCHANT_ITEM_SELECT:
					this.state = UiState.MODE;
					break;
				case UiState.HUB_MODE_SETUP_TOTEM:
					this.state = UiState.HUB_MODE_SETUP;
					break;
				case UiState.HUB_MODE_SETUP_HERO:
					this.state = UiState.HUB_MODE_SETUP;
					break;
				case UiState.HEROES_RUNES:
					this.state = UiState.HEROES;
					break;
				case UiState.HUB_CREDITS:
					this.state = UiState.HUB_OPTIONS;
					break;
				case UiState.HEROES_TRINKETS:
					this.state = UiState.HEROES;
					break;
				case UiState.SELECT_TRINKET:
					this.state = UiState.HEROES_TRINKETS;
					break;
				case UiState.HUB_ACHIEVEMENTS:
					if (this.panelAchievements.lastState == UiState.NONE)
					{
						if (this.sim.IsGameModeInAction(this.panelAchievements.lastGameMode))
						{
							this.OnClickedGameMode(this.panelAchievements.lastGameMode);
						}
						else
						{
							this.state = UiState.HUB;
						}
					}
					else
					{
						this.state = UiState.HUB;
					}
					break;
				case UiState.SHOP_MINE:
					this.state = ((!this.panelShop.isHubMode) ? UiState.SHOP : UiState.HUB_SHOP);
					break;
				case UiState.SUPPORT_POPUP:
					this.state = UiState.HUB_OPTIONS;
					break;
				case UiState.SHOP_TRINKET_OPEN_POPUP:
					this.state = ((!this.panelShop.isHubMode) ? UiState.SHOP : UiState.HUB_SHOP);
					break;
				case UiState.CHANGE_SKIN:
					this.state = this.panelHeroSkinChanger.oldState;
					break;
				case UiState.CHARM_INFO_POPUP:
					this.state = UiState.HUB_CHARMS;
					break;
				case UiState.BUY_FLASH_OFFER_CHARM:
					this.state = this.panelBuyCharmFlashOffer.previousState;
					break;
				case UiState.BUY_FLASH_OFFER:
					this.state = this.panelBuyAdventureFlashOffer.previousState;
					break;
				case UiState.SHOP_CHARM_PACK_SELECT:
					this.state = ((!this.panelShop.isHubMode) ? UiState.SHOP : UiState.HUB_SHOP);
					break;
				case UiState.CHRISTMAS_PANEL:
					this.state = this.panelChristmasOffer.previousState;
					break;
				case UiState.CHRISTMAS_OFFERS_INFO_POPUP:
					this.state = UiState.CHRISTMAS_PANEL;
					break;
				case UiState.CHRISTMAS_CANDY_TREAT_POPUP:
					this.state = UiState.CHRISTMAS_PANEL;
					break;
				case UiState.SHARE_SCREENSHOT_PANEL:
					this.state = ((!this.panelShareScreenshot.backToModePanel) ? UiState.NONE : UiState.MODE);
					this.sim.isActiveWorldPaused = false;
					break;
				case UiState.NEW_TAL_MILESTONE_REACHED_POPUP:
					this.state = this.newTALMilestoneReachedPopup.previousState;
					break;
				case UiState.SECOND_ANNIVERSARY_POPUP:
					this.state = this.secondAnniversaryPopup.previousState;
					break;
				}
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
			}
			if (PlayfabManager.forceUpdate && Main.rewardToClaim == null)
			{
				if (this.state != UiState.UPDATE_REQUIRED)
				{
					this.state = UiState.UPDATE_REQUIRED;
				}
			}
			else if (!TutorialManager.IsThereTutorialCurrently() && !OldArtifactsConverter.DoesPlayerNeedsConversion(this.sim) && (TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.FIN || TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.BEFORE_BEGIN) && !this.IsInUnclosableMenus() && !this.loadingTransition.IsPlaying())
			{
				UiState uiState = UiState.NONE;
				if (activeWorld.activeChallenge.state == Challenge.State.WON)
				{
					if (uiState == UiState.NONE && this.state != UiState.CHALLENGE_WIN && this.state != UiState.SHARE_SCREENSHOT_PANEL && !activeWorld.isRainingGlory && !activeWorld.activeChallenge.ShouldWaitToFinish())
					{
						this.OnChallengeWin();
						uiState = UiState.CHALLENGE_WIN;
					}
				}
				else if (activeWorld.activeChallenge.state == Challenge.State.LOST)
				{
					if (uiState == UiState.NONE && !activeWorld.isRainingDuck && this.state != UiState.SHARE_SCREENSHOT_PANEL)
					{
						this.OnChallengeLose();
						uiState = UiState.CHALLENGE_LOSE;
					}
				}
				else if (activeWorld.activeChallenge.state == Challenge.State.SETUP)
				{
					uiState = this.CheckModeUnlock(uiState);
					if (uiState == UiState.NONE && !this.loadingTransition.IsPlaying() && !this.IsInModeSetupMenus())
					{
						this.loadingTransition.DoTransition(UiState.HUB, 0f, 0f);
					}
					else if (RewardedAdManager.inst != null && RewardedAdManager.inst.shouldGiveRewardCapped && uiState == UiState.NONE)
					{
						if (RewardedAdManager.inst.currencyTypeForCappedVideo == CurrencyType.CANDY)
						{
							RewardedAdManager.inst.shouldGiveRewardCapped = false;
							this.panelChristmasOffer.previousState = UiState.HUB;
							uiState = UiState.CHRISTMAS_PANEL;
							this.panelChristmasOffer.GoToOffersTab();
							UiCommandFreeCurrencyCollect uiCommandFreeCurrencyCollect = new UiCommandFreeCurrencyCollect();
							uiCommandFreeCurrencyCollect.rewardCurrencyType = CurrencyType.CANDY;
							uiCommandFreeCurrencyCollect.rewardAmount = RewardedAdManager.inst.rewardAmountForCappedVideo;
							uiCommandFreeCurrencyCollect.isHubShop = true;
							DropPosition dropPosition = new DropPosition
							{
								startPos = this.panelChristmasOffer.candyTreats[0].transform.position,
								endPos = this.panelChristmasOffer.candyTreats[0].transform.position + Vector3.down * 0.1f,
								invPos = this.panelChristmasOffer.candies.GetCurrencyTransform().position,
								targetToScaleOnReach = this.panelChristmasOffer.candies.GetCurrencyTransform()
							};
							uiCommandFreeCurrencyCollect.dropPosition = dropPosition;
							this.command = uiCommandFreeCurrencyCollect;
						}
						else
						{
							uiState = UiState.AD_POPUP;
							CurrencyType currencyTypeForCappedVideo = RewardedAdManager.inst.currencyTypeForCappedVideo;
							double rewardAmountForCappedVideo = RewardedAdManager.inst.rewardAmountForCappedVideo;
							this.panelAdPopup.SetDetails(currencyTypeForCappedVideo, rewardAmountForCappedVideo, true);
						}
					}
					else
					{
						if (RewardedAdManager.inst != null && RewardedAdManager.inst.shouldGiveReward)
						{
							FlashOffer.Type? targetFlashOfferType = RewardedAdManager.inst.targetFlashOfferType;
							if (targetFlashOfferType != null && uiState == UiState.NONE)
							{
								FlashOffer flashOffer = this.sim.flashOfferBundle.adventureOffers.Find((FlashOffer offer) => offer.costType == FlashOffer.CostType.AD && offer.type == RewardedAdManager.inst.targetFlashOfferType);
								if (flashOffer != null)
								{
									this.command = new UICommandBuyFlashOffer
									{
										flashOffer = flashOffer,
										dropPosition = PanelBuyAdventureFlashOffer.CurrencyMenuDropPosition()
									};
									if (this._state != UiState.BUY_FLASH_OFFER)
									{
										this.state = ((!this.IsInHubMenus()) ? UiState.SHOP : UiState.HUB_SHOP);
										this.panelShop.isLookingAtOffers = true;
										this.panelBuyAdventureFlashOffer.flashOffer = flashOffer;
										this.panelBuyAdventureFlashOffer.flashOfferUnlocked = true;
										this.panelBuyAdventureFlashOffer.canNotAffordMessageKey = null;
										this.panelBuyAdventureFlashOffer.buttonGoToShopClickedCallback = null;
										uiState = UiState.BUY_FLASH_OFFER;
									}
								}
								goto IL_9DB;
							}
						}
						if (RewardedAdManager.inst != null && RewardedAdManager.inst.shouldGiveReward && this.sim.GetActiveWorld().adRewardCurrencyType == CurrencyType.CANDY && uiState == UiState.NONE)
						{
							this.sim.GetActiveWorld().RainCurrencyOnUi(this._state, CurrencyType.CANDY, this.sim.GetActiveWorld().adRewardAmount, new DropPosition
							{
								startPos = Vector3.zero,
								endPos = Vector3.down * 0.1f,
								invPos = this.panelCurrencyOnTop[0].currencyFinalPosReference.position,
								showSideCurrency = true
							}, 30, 0f, 0f, 1f, null, 0f);
						}
						else if (this.sim.artifactsManager.HasUnlockedNewTotalArtifactsLevelMilestone())
						{
							if (Cheats.dontShowNewTALMilestoneReachedPopup || (this.state != UiState.HUB_ARTIFACTS && this.state != UiState.ARTIFACTS && this.state != UiState.ARTIFACT_SELECTED_POPUP))
							{
								this.sim.artifactsManager.UpdateCurrentTotalArtifactsLevelMilestone();
							}
							else
							{
								uiState = UiState.NEW_TAL_MILESTONE_REACHED_POPUP;
								UiManager.AddUiSound(SoundArchieve.inst.uiPopupAppear);
							}
						}
					}
					IL_9DB:;
				}
				else
				{
					if (activeWorld.activeChallenge.state != Challenge.State.ACTION)
					{
						throw new NotImplementedException();
					}
					if (this.state == UiState.NONE && uiState == UiState.NONE && activeWorld.offlineGold > 0.0)
					{
						uiState = UiState.OFFLINE_EARNINGS;
					}
					if (this.state == UiState.NONE && uiState == UiState.NONE && challenge is ChallengeStandard)
					{
						foreach (Unlock unlock in activeWorld.unlocks)
						{
							if (!unlock.isCollected)
							{
								if (unlock.IsReqSatisfied(this.sim))
								{
									if (unlock.isHidden || (unlock.HasRewardOfType(typeof(UnlockRewardPrestige)) && this.sim.numPrestiges > 0))
									{
										this.command = new UiCommandUnlockCollectReward
										{
											unlock = unlock
										};
										continue;
									}
									this.unlockAboutToBeCollected = unlock;
									uiState = UiState.MODE_UNLOCK_REWARD;
								}
								break;
							}
						}
					}
					if (uiState == UiState.NONE && activeWorld.showAdOffer)
					{
						activeWorld.showAdOffer = false;
						activeWorld.showingAdOffer = true;
						uiState = UiState.AD_POPUP;
						CurrencyType adRewardCurrencyType = activeWorld.adRewardCurrencyType;
						double adRewardAmount = activeWorld.adRewardAmount;
						this.panelAdPopup.SetDetails(adRewardCurrencyType, adRewardAmount, false);
					}
					if (RewardedAdManager.inst != null && RewardedAdManager.inst.shouldGiveReward && uiState == UiState.NONE)
					{
						if (RewardedAdManager.inst.targetFlashOfferType != null)
						{
							FlashOffer flashOffer2 = this.sim.flashOfferBundle.adventureOffers.Find((FlashOffer offer) => offer.costType == FlashOffer.CostType.AD && offer.type == RewardedAdManager.inst.targetFlashOfferType);
							if (flashOffer2 != null)
							{
								this.command = new UICommandBuyFlashOffer
								{
									flashOffer = flashOffer2,
									dropPosition = PanelBuyAdventureFlashOffer.CurrencyMenuDropPosition()
								};
								if (this._state != UiState.BUY_FLASH_OFFER)
								{
									this.state = ((!this.IsInHubMenus()) ? UiState.SHOP : UiState.HUB_SHOP);
									this.panelShop.isLookingAtOffers = true;
									this.panelBuyAdventureFlashOffer.flashOffer = flashOffer2;
									this.panelBuyAdventureFlashOffer.flashOfferUnlocked = true;
									this.panelBuyAdventureFlashOffer.canNotAffordMessageKey = null;
									this.panelBuyAdventureFlashOffer.buttonGoToShopClickedCallback = null;
									uiState = UiState.BUY_FLASH_OFFER;
								}
							}
						}
						else if (activeWorld.adRewardCurrencyType == CurrencyType.CANDY)
						{
							RewardedAdManager.inst.shouldGiveReward = false;
							UiCommandAdCollect command = new UiCommandAdCollect
							{
								canDropCandies = false
							};
							this.command = command;
						}
						else
						{
							uiState = UiState.AD_POPUP;
							CurrencyType adRewardCurrencyType2 = activeWorld.adRewardCurrencyType;
							double adRewardAmount2 = activeWorld.adRewardAmount;
							this.panelAdPopup.SetDetails(adRewardCurrencyType2, adRewardAmount2, true);
						}
					}
					if (RewardedAdManager.inst != null && RewardedAdManager.inst.shouldGiveRewardCapped && uiState == UiState.NONE && !this.IsInHubMenus(this._state))
					{
						if (RewardedAdManager.inst.currencyTypeForCappedVideo == CurrencyType.CANDY)
						{
							RewardedAdManager.inst.shouldGiveRewardCapped = false;
							this.panelChristmasOffer.previousState = UiState.NONE;
							uiState = UiState.CHRISTMAS_PANEL;
							this.panelChristmasOffer.GoToOffersTab();
							UiCommandFreeCurrencyCollect uiCommandFreeCurrencyCollect2 = new UiCommandFreeCurrencyCollect();
							uiCommandFreeCurrencyCollect2.rewardCurrencyType = CurrencyType.CANDY;
							uiCommandFreeCurrencyCollect2.rewardAmount = RewardedAdManager.inst.rewardAmountForCappedVideo;
							uiCommandFreeCurrencyCollect2.isHubShop = true;
							DropPosition dropPosition2 = new DropPosition
							{
								startPos = this.panelChristmasOffer.candyTreats[0].transform.position,
								endPos = this.panelChristmasOffer.candyTreats[0].transform.position + Vector3.down * 0.1f,
								invPos = this.panelChristmasOffer.candies.GetCurrencyTransform().position,
								targetToScaleOnReach = this.panelChristmasOffer.candies.GetCurrencyTransform()
							};
							uiCommandFreeCurrencyCollect2.dropPosition = dropPosition2;
							this.command = uiCommandFreeCurrencyCollect2;
						}
						else
						{
							uiState = UiState.AD_POPUP;
							CurrencyType currencyTypeForCappedVideo2 = RewardedAdManager.inst.currencyTypeForCappedVideo;
							double rewardAmountForCappedVideo2 = RewardedAdManager.inst.rewardAmountForCappedVideo;
							this.panelAdPopup.SetDetails(currencyTypeForCappedVideo2, rewardAmountForCappedVideo2, true);
						}
					}
					else if (RewardedAdManager.inst != null && RewardedAdManager.inst.shouldGiveRewardCapped && uiState == UiState.NONE && RewardedAdManager.inst.currencyTypeForCappedVideo != CurrencyType.CANDY)
					{
						uiState = UiState.AD_POPUP;
						CurrencyType currencyTypeForCappedVideo3 = RewardedAdManager.inst.currencyTypeForCappedVideo;
						double rewardAmountForCappedVideo3 = RewardedAdManager.inst.rewardAmountForCappedVideo;
						this.panelAdPopup.SetDetails(currencyTypeForCappedVideo3, rewardAmountForCappedVideo3, true);
					}
					if (UiManager.showPlayfabCustomLoginWarning && this.state == UiState.NONE && uiState == UiState.NONE)
					{
						UiManager.showPlayfabCustomLoginWarning = false;
						if (!UiManager.everShownPlayfabCustomLoginWarningBefore && !Main.dontStoreAuthenticate)
						{
							UiManager.everShownPlayfabCustomLoginWarningBefore = true;
							this.panelGeneralPopup.SetDetails(PanelGeneralPopup.State.NONE, LM.Get("OPTIONS_CLOUD"), string.Format(LM.Get("PLAYFAB_CUSTOM_LOGIN_WARNING"), UiManager.GetStoreNameString()), true, delegate
							{
								StoreManager.Authenticate(true, delegate
								{
									if (StoreManager.IsAuthed())
									{
										PlayfabManager.Login(delegate
										{
										}, true);
									}
									else
									{
										StoreManager.Authenticate(true, delegate
										{
											PlayfabManager.Login(delegate
											{
											}, true);
										});
									}
								});
								this.state = UiState.NONE;
								UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
							}, LM.Get("UI_CONNECT"), delegate
							{
								this.state = UiState.NONE;
								UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
							}, LM.Get("UI_IGNORE"), 0f, 160f, null, null);
							uiState = UiState.GENERAL_POPUP;
						}
					}
					this.checkImmidiateStateTimer += dt;
					if (this.checkImmidiateStateTimer > 1f && this.state == UiState.NONE && uiState == UiState.NONE)
					{
						this.checkImmidiateStateTimer = 0f;
						if (PlayfabManager.optionalUpdate && this.state == UiState.NONE && uiState == UiState.NONE)
						{
							this.panelGeneralPopup.SetDetails(PanelGeneralPopup.State.NONE, LM.Get("OPTIONAL_UPDATE_HEAD"), string.Format(LM.Get("OPTIONAL_UPDATE_DESC"), PlayfabManager.titleData.latestVersion), true, delegate
							{
								this.GoToStorePage();
								this.state = UiState.NONE;
								UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
							}, LM.Get("UI_ACCEPT"), delegate
							{
								this.state = UiState.NONE;
								UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
							}, LM.Get("UI_IGNORE"), 0f, 160f, null, null);
							uiState = UiState.GENERAL_POPUP;
						}
						uiState = this.CheckNextStateBasedOnRatePopup(uiState);
						if (!StoreManager.askedToAllowNotifications && !StoreManager.areNotificationsAllowed && this.state == UiState.NONE && uiState == UiState.NONE && TutorialManager.IsShopTabUnlocked())
						{
							string name = this.sim.lootpacks[0].GetType().Name;
							if (this.sim.lootpacksOpenedCount != null && this.sim.lootpacksOpenedCount.ContainsKey(name) && this.sim.lootpacksOpenedCount[name] > 1)
							{
								PanelGeneralPopup panelGeneralPopup = this.panelGeneralPopup;
								PanelGeneralPopup.State state = PanelGeneralPopup.State.NONE;
								string sHeader = LM.Get("NOTIFICATIONS");
								string text = LM.Get("NOTIFICATIONS_ALLOW_BODY");
								bool twoButtons = true;
								GameButton.VoidFunc onYes = delegate()
								{
									StoreManager.areNotificationsAllowed = true;
									StoreManager.askedToAllowNotifications = true;
									StoreManager.mineNotifications = true;
									StoreManager.specialOffersNotifications = true;
									StoreManager.freeChestsNotifications = true;
									StoreManager.sideQuestNotifications = true;
									StoreManager.flashOffersNotifications = true;
									StoreManager.eventsNotifications = true;
									StoreManager.RegisterForNotifications();
									this.state = UiState.NONE;
									UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
								};
								string text2 = LM.Get("UI_YES");
								GameButton.VoidFunc voidFunc = delegate()
								{
									StoreManager.askedToAllowNotifications = true;
									this.state = UiState.NONE;
									UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
								};
								string text3 = LM.Get("UI_NO");
								PanelGeneralPopup.SpineGraphicInfo chestGraphicInfo = this.panelGeneralPopup.chestGraphicInfo;
								panelGeneralPopup.SetDetails(state, sHeader, text, twoButtons, onYes, text2, voidFunc, text3, 0f, 160f, chestGraphicInfo, null);
								uiState = UiState.GENERAL_POPUP;
							}
						}
						if (uiState == UiState.NONE && TutorialManager.IsShopTabUnlocked() && (this.sim.numPrestiges > 0 || this.sim.GetWorld(GameMode.STANDARD).GetStageNumber() >= 7))
						{
							SpecialOfferKeeper nextUnannouncedOffer = this.sim.specialOfferBoard.GetNextUnannouncedOffer(this.sim);
							if (nextUnannouncedOffer != null)
							{
								this.sim.OnNewOfferAnnounced(nextUnannouncedOffer.offerPack);
								this.panelOfferPopup.SetOffer(nextUnannouncedOffer.offerPack);
								uiState = UiState.OFFER_POPUP;
							}
						}
						if (uiState == UiState.NONE && this.sim.IsChristmasTreeEnabled() && this.sim.christmasEventPopupChecked && this.sim.showChristmasEventPopup)
						{
							this.panelChristmasOffer.previousState = UiState.NONE;
							uiState = UiState.CHRISTMAS_PANEL;
							this.sim.showChristmasEventPopup = false;
						}
						if (uiState == UiState.NONE && TrustedTime.IsReady() && this.sim.IsChristmasTreeEnabled() && TrustedTime.Get() >= Simulator.ChristmasFirstPopupDate && PlayfabManager.christmasOfferConfigLoaded && TrustedTime.Get() < PlayfabManager.christmasOfferConfig.candyDropLimitDateParsed && this.sim.christmasEventPopupsShown < 1)
						{
							this.sim.christmasEventPopupsShown = 1;
							this.panelChristmasEventPopup.SetEvent(this.sim.christmasEventPopupsShown);
							uiState = UiState.CHRISTMAS_EVENT_POPUP;
						}
						if (uiState == UiState.NONE && TrustedTime.IsReady() && this.sim.IsChristmasTreeEnabled() && PlayfabManager.christmasOfferConfigLoaded && TrustedTime.Get() >= PlayfabManager.christmasOfferConfig.candyDropLimitDateParsed && this.sim.christmasEventPopupsShown < 2)
						{
							this.sim.christmasEventPopupsShown = 2;
							this.panelChristmasEventPopup.SetEvent(this.sim.christmasEventPopupsShown);
							uiState = UiState.CHRISTMAS_EVENT_POPUP;
						}
						if (uiState == UiState.NONE && this.sim.christmasEventAlreadyDisabled && this.sim.GetCandies().GetAmount() > 0.0 && TrustedTime.IsReady() && PlayfabManager.christmasOfferConfigLoaded && PlayfabManager.christmasOfferConfig.offerConfig.endDateParsed < TrustedTime.Get())
						{
							if (this.sim.christmasEventPopupsShown == 4)
							{
								this.state = UiState.CONVERT_XMAS_SCRAP;
							}
							else
							{
								this.sim.christmasEventPopupsShown = 3;
								this.panelChristmasEventPopup.SetEvent(this.sim.christmasEventPopupsShown);
								uiState = UiState.CHRISTMAS_EVENT_POPUP;
							}
						}
					}
					if (uiState == UiState.NONE && this.sim.artifactsManager.HasUnlockedNewTotalArtifactsLevelMilestone())
					{
						if (Cheats.dontShowNewTALMilestoneReachedPopup || (this.state != UiState.HUB_ARTIFACTS && this.state != UiState.ARTIFACTS && this.state != UiState.ARTIFACT_SELECTED_POPUP))
						{
							this.sim.artifactsManager.UpdateCurrentTotalArtifactsLevelMilestone();
						}
						else
						{
							uiState = UiState.NEW_TAL_MILESTONE_REACHED_POPUP;
							UiManager.AddUiSound(SoundArchieve.inst.uiPopupAppear);
						}
					}
				}
				if (TutorialManager.IsShopTabUnlocked() && (Main.rewardToClaim != null || Main.IsAnyTitleNewsReadyToShow() || this.sim.rewardsToGive.Count > 0) && uiState == UiState.NONE && (this.state == UiState.NONE || this.state == UiState.HUB))
				{
					this.panelServerReward.timer = 0f;
					bool flag = Main.rewardToClaim == null && this.sim.rewardsToGive.Count == 0;
					PlayfabManager.RewardData rewardData;
					RewardOrigin rewardOrigin;
					if (Main.rewardToClaim != null)
					{
						rewardData = Main.rewardToClaim;
						rewardOrigin = RewardOrigin.Server;
					}
					else if (this.sim.rewardsToGive.Count > 0)
					{
						rewardData = this.sim.rewardsToGive[0];
						rewardOrigin = RewardOrigin.InGame;
					}
					else
					{
						rewardData = ((!Main.PeekNextNews().HasReward()) ? null : Main.PeekNextNews().body.reward);
						rewardOrigin = RewardOrigin.News;
					}
					string title;
					string text4;
					int numRewardRowsFor;
					if (flag)
					{
						PlayfabManager.NewsData newsData = Main.PeekNextNews();
						title = newsData.body.reward.title;
						text4 = ((!newsData.body.reward.HasReward()) ? newsData.body.reward.desc : string.Empty);
						numRewardRowsFor = PanelServerReward.GetNumRewardRowsFor(newsData.body.reward);
					}
					else
					{
						title = rewardData.title;
						text4 = string.Empty;
						numRewardRowsFor = PanelServerReward.GetNumRewardRowsFor(rewardData);
					}
					float num;
					switch (numRewardRowsFor)
					{
					case 0:
						num = 20f;
						break;
					case 1:
						num = 45f;
						break;
					case 2:
						num = 135f;
						break;
					case 3:
						num = 230f;
						break;
					default:
						throw new NotImplementedException();
					}
					PanelGeneralPopup panelGeneralPopup2 = this.panelGeneralPopup;
					PanelGeneralPopup.State state = (this.state != UiState.HUB) ? PanelGeneralPopup.State.SERVER_REWARD : PanelGeneralPopup.State.SERVER_REWARD_HUB;
					string text3 = title;
					string text2 = text4;
					bool twoButtons = false;
					GameButton.VoidFunc voidFunc = delegate()
					{
						this.OnClickedAcceptServerReward(rewardOrigin);
					};
					string text = LM.Get("UI_ACCEPT");
					float popupHeight = num;
					panelGeneralPopup2.SetDetails(state, text3, text2, twoButtons, voidFunc, text, null, string.Empty, 0f, popupHeight, null, null);
					if (flag)
					{
						this.currentNewsIndex = Main.GetAvailableNewsCount();
					}
					this.panelServerReward.Refresh(this.sim, this, rewardData);
					this.acceptingServerReward = false;
					uiState = UiState.GENERAL_POPUP;
				}
				if (uiState == UiState.NONE)
				{
					uiState = this.CheckNextStateBasedOnRatePopup(uiState);
				}
				if (uiState == UiState.NONE && this.state != UiState.SOCIAL_REWARD_POPUP)
				{
					SocialRewards.Network? networkPendingOfReward = Manager.GetNetworkPendingOfReward();
					if (networkPendingOfReward != null)
					{
						this.showSocialRewardPopupTimer += dt;
						if (this.showSocialRewardPopupTimer >= 2f)
						{
							this.showSocialRewardPopupTimer = 0f;
							this.panelSocialRewardPopup.SetDetails(networkPendingOfReward.Value, 50.0, true, this.state, this.GetSocialNetworkName(networkPendingOfReward.Value));
							uiState = UiState.SOCIAL_REWARD_POPUP;
						}
					}
				}
				if (uiState != UiState.NONE && uiState != this.state)
				{
					this.state = uiState;
				}
			}
			else if (TutorialManager.first == TutorialManager.First.ULTIMATE_SELECT && this.state != UiState.NONE)
			{
				this.state = UiState.NONE;
			}
		}

		public UiState CheckNextStateBasedOnRatePopup(UiState stateToChange)
		{
			if (this.sim.shouldAskForRate && ((this.state == UiState.NONE && !this.sim.GetActiveWorld().IsRaining() && !this.loadingTransition.IsTransitioning()) || this.state == UiState.HUB) && stateToChange == UiState.NONE)
			{
				this.sim.shouldAskForRate = false;
				this.panelRatePopup.previousState = this.state;
				float num = 160f;
				if (this.sim.ratingState == RatingState.NeverAsked)
				{
					PanelGeneralPopup panelGeneralPopup = this.panelGeneralPopup;
					PanelGeneralPopup.State state = PanelGeneralPopup.State.NONE;
					string sHeader = LM.Get("UI_ENJOY_TITLE");
					string sBodyUp = LM.Get("UI_ENJOY_DESC");
					bool twoButtons = true;
					GameButton.VoidFunc onYes = delegate()
					{
						this.sim.ratingState = RatingState.AskLater;
						this.state = UiState.RATE_POPUP;
					};
					string sYes = LM.Get("UI_YES");
					GameButton.VoidFunc onNo = delegate()
					{
						this.OnRateShowFeedbackButtonClicked();
					};
					string sNo = LM.Get("UI_NO");
					float popupHeight = num;
					panelGeneralPopup.SetDetails(state, sHeader, sBodyUp, twoButtons, onYes, sYes, onNo, sNo, 0f, popupHeight, null, null);
					return UiState.GENERAL_POPUP;
				}
				if (this.sim.ratingState == RatingState.AskLater)
				{
					return UiState.RATE_POPUP;
				}
			}
			return UiState.NONE;
		}

		private void UpdateTopUi(World activeWorld, Challenge challenge, float dt)
		{
			if (activeWorld.isRainingGlory || !TutorialManager.ShouldShowTopUi())
			{
				this.panelTopHudRegular.SetActive(false);
				this.panelTopHudChallengeTimeWave.gameObject.SetActive(false);
				this.dailyQuestIndicatorWidget.gameObject.SetActive(false);
			}
			else
			{
				if (this.sim.hasDailies)
				{
					this.dailyQuestIndicatorWidget.gameObject.SetActive(true);
					DailyQuest dailyQuest = this.sim.dailyQuest;
					if (dailyQuest != null)
					{
						if (dailyQuest.progress != this.dailyQuestIndicatorWidget.lastProgress)
						{
							this.dailyQuestIndicatorWidget.PopAnim();
							this.dailyQuestIndicatorWidget.lastProgress = dailyQuest.progress;
						}
						float fillAmount = Mathf.Min(1f, (float)dailyQuest.progress / (float)dailyQuest.goal);
						this.dailyQuestIndicatorWidget.dailyFill.fillAmount = fillAmount;
						if (this.sim.CanCollectDailyQuest())
						{
							this.dailyQuestIndicatorWidget.checkMark.SetActive(true);
						}
						else
						{
							this.dailyQuestIndicatorWidget.checkMark.SetActive(false);
						}
						this.dailyQuestIndicatorWidget.dailyFill.gameObject.SetActive(true);
						this.dailyQuestIndicatorWidget.dailyIcon.sprite = this.dailyQuestIndicatorWidget.iconNormal;
					}
					else
					{
						this.dailyQuestIndicatorWidget.checkMark.SetActive(false);
						this.dailyQuestIndicatorWidget.dailyFill.gameObject.SetActive(false);
						this.dailyQuestIndicatorWidget.dailyIcon.sprite = this.dailyQuestIndicatorWidget.iconDisabled;
					}
					this.dailyQuestIndicatorWidget.RectTransform.SetAnchorPosX((!this.sim.specialOfferBoard.AnyAnnouncedOfferAvailable(this.sim) && !this.sim.halloweenEnabled && !this.sim.IsChristmasTreeEnabled() && !this.sim.IsSecondAnniversaryEventEnabled()) ? -267.8f : -329.3f);
				}
				else
				{
					this.dailyQuestIndicatorWidget.gameObject.SetActive(false);
				}
				if (UiManager.imageGoldWorldToAnim == activeWorld)
				{
					if (this.imageGoldAnimTimer >= 0.1f)
					{
						if (this.imageGoldAnimTimer < 0.15f)
						{
							this.imageGoldAnimTimer = 0.1f;
						}
						else if (this.imageGoldAnimTimer < 0.22f)
						{
							this.imageGoldAnimTimer = 0.22f - this.imageGoldAnimTimer;
						}
						else
						{
							this.imageGoldAnimTimer = 0f;
						}
					}
				}
				if (this.imageGoldAnimTimer < 0.22f)
				{
					this.imageGoldAnimTimer += dt;
				}
				UiManager.imageGoldWorldToAnim = null;
				if (challenge is ChallengeStandard)
				{
					this.panelTopHudRegular.SetActive(true);
					this.panelTopHudChallengeTimeWave.gameObject.SetActive(false);
					this.textWaveName.text = ((!TutorialManager.ShouldShowWaveName()) ? string.Empty : challenge.GetWaveName());
					this.maxStageReachedBanner.UpdateBanner(this.sim);
					if (TutorialManager.ShouldShowStageBar())
					{
						Unlock nextUnhiddenUnlock = activeWorld.GetNextUnhiddenUnlock();
						this.stageProgressionBar.gameObject.SetActive(true);
						ChallengeStandard challengeStandard = this.sim.GetActiveWorld().activeChallenge as ChallengeStandard;
						bool isBossWave = ChallengeStandard.IsBossWave(challengeStandard.totWave);
						bool hasBoss = challengeStandard.HasBoss();
						bool isAlive = challengeStandard.IsThereAliveNonleavingBoss();
						StageProgressionBar.BossState bossState = new StageProgressionBar.BossState
						{
							isBossWave = isBossWave,
							isAlive = isAlive,
							hasBoss = hasBoss
						};
						this.stageProgressionBar.SetBar(challenge.GetTotWave(), nextUnhiddenUnlock.GetReqAmount(), bossState);
					}
					else
					{
						this.stageProgressionBar.gameObject.SetActive(false);
					}
					double totEnemyHealth = this.sim.GetTotEnemyHealth();
					double num = Math.Max(1.0, this.sim.GetTotEnemyHealthMax());
					this.barEnemyHealth.localScale = new Vector3((float)(totEnemyHealth / num), 1f, 1f);
					this.textEnemyHealth.text = GameMath.GetDoubleString(totEnemyHealth);
					float bossTimeScale = this.sim.GetBossTimeScale();
					float bossTimePassed = this.sim.GetBossTimePassed();
					bool flag = this.sim.CanGoToBoss();
					if (flag)
					{
						this.bossFrame.gameObject.SetActive(false);
						this.panelBossTimer.SetActive(false);
						this.buttonLeaveBoss.gameObject.SetActive(false);
						this.buttonFightBoss.gameObject.SetActive(TutorialManager.ShouldShowBossButtons());
						if (this.buttonFightBoss.gameObject.activeSelf && this.state == UiState.NONE)
						{
							TutorialManager.CanPressFightBossButton();
						}
						this.buttonFightBoss.text.text = LM.Get("UI_FIGHT_BOSS");
					}
					else if (bossTimeScale > 0f && bossTimePassed > 0.5f)
					{
						this.bossFrame.gameObject.SetActive(true);
						this.bossFrame.boss.sprite = this.GetSpriteBossPortrait(((ChallengeStandard)challenge).bossName);
						this.panelBossTimer.SetActive(true);
						this.barBossTimer.transform.localScale = new Vector3(bossTimeScale, 1f, 1f);
						this.buttonLeaveBoss.gameObject.SetActive(TutorialManager.ShouldShowBossButtons());
						this.buttonFightBoss.gameObject.SetActive(false);
						this.buttonLeaveBoss.text.text = LM.Get("UI_LEAVE_BOSS");
					}
					else
					{
						this.bossFrame.gameObject.SetActive(false);
						this.panelBossTimer.SetActive(false);
						this.buttonLeaveBoss.gameObject.SetActive(false);
						this.buttonFightBoss.gameObject.SetActive(false);
					}
					int nextUnlockStage = this.sim.GetNextUnlockStage();
					if (TutorialManager.ShouldShowStageBar())
					{
						this.panelNextUnlockStage.gameObject.SetActive(true);
						if (nextUnlockStage == -1)
						{
							this.panelNextUnlockStage.nextUnlockStage = -1;
							this.panelNextUnlockStage.SetAllComplete();
						}
						else if (nextUnlockStage != this.panelNextUnlockStage.nextUnlockStage || UiManager.stateJustChanged)
						{
							this.panelNextUnlockStage.StartAnim(nextUnlockStage);
						}
					}
					else
					{
						this.panelNextUnlockStage.gameObject.SetActive(false);
					}
					this.textGold.text = this.sim.GetGold().GetString();
					this.imageGold.transform.localScale = this.GetImageGoldScale();
					this.textModeName.text = UiManager.GetModeName(this.sim.GetCurrentGameMode());
					if (activeWorld.skippedStageNo > -1)
					{
						this.stageProgressionBar.DoStageSkipAnim1(activeWorld.skippedStageNo);
						activeWorld.skippedStageNo = -1;
					}
					if (activeWorld.IsIdleGainActive())
					{
						this.parentIdleGainStandard.gameObject.SetActive(true);
						this.textIdleGain.gameObject.SetActive(true);
						if (this.textIdleGain.rectTransform.parent != this.parentIdleGainStandard)
						{
							this.textIdleGain.rectTransform.SetParent(this.parentIdleGainStandard);
						}
						this.textIdleGain.rectTransform.anchoredPosition = new Vector2(0f, 0f);
					}
					else
					{
						this.textIdleGain.gameObject.SetActive(false);
						this.parentIdleGainStandard.gameObject.SetActive(false);
					}
				}
				else
				{
					if (!(challenge is ChallengeWithTime))
					{
						throw new NotImplementedException();
					}
					ChallengeWithTime challengeWithTime = (ChallengeWithTime)challenge;
					this.panelTopHudRegular.SetActive(false);
					if (challenge is ChallengeRift)
					{
						ChallengeRift challengeRift = challenge as ChallengeRift;
						this.panelTopHudChallengeTimeWave.imageGold.sprite = this.spriteGoldSquare;
						this.panelTopHudChallengeTimeWave.textGold.rectTransform.anchoredPosition = new Vector2(0f, -150f);
						this.panelTopHudChallengeTimeWave.riftEffectsParent.gameObject.SetActive(true);
						int num2 = challengeRift.riftEffects.Count;
						if (challengeRift.IsCursed())
						{
							num2--;
						}
						if (num2 != this.panelTopHudChallengeTimeWave.riftEffectIcons.Count)
						{
							Utility.FillUiElementList<Image>(this.uiData.imagePrafab, this.panelTopHudChallengeTimeWave.riftEffectsParent, num2, this.panelTopHudChallengeTimeWave.riftEffectIcons);
							for (int i = 0; i < num2; i++)
							{
								Image image = this.panelTopHudChallengeTimeWave.riftEffectIcons[i];
								image.rectTransform.sizeDelta = new Vector2(75f, 75f);
							}
						}
						for (int j = 0; j < num2; j++)
						{
							Image image2 = this.panelTopHudChallengeTimeWave.riftEffectIcons[j];
							RiftEffect riftEffect = challengeRift.riftEffects[j];
							image2.sprite = this.GetRiftEffectSprite(riftEffect.GetType());
						}
						if (challengeRift.IsCursed() && challengeRift.state == Challenge.State.ACTION)
						{
							this.panelTopHudChallengeTimeWave.curseEffectParents.gameObject.SetActive(true);
							this.panelTopHudChallengeTimeWave.curseEffectsProgress.value = challengeRift.curseProgress;
							int count = this.sim.currentCurses.Count;
							if (count != this.panelTopHudChallengeTimeWave.curseEffectIcons.Count)
							{
								Utility.FillUiElementList<CharmEffectWidget>(this.panelTopHudChallengeTimeWave.curseEffectWidgetPrefab, this.panelTopHudChallengeTimeWave.curseEffectWidgetParents, count, this.panelTopHudChallengeTimeWave.curseEffectIcons);
								for (int k = 0; k < count; k++)
								{
									CharmEffectWidget charmEffectWidget = this.panelTopHudChallengeTimeWave.curseEffectIcons[k];
									charmEffectWidget.SetRectSize(new Vector2(80f, 80f));
									charmEffectWidget.rectTransform.sizeDelta = new Vector2(55f, 55f);
									charmEffectWidget.rectTransform.localScale = Vector3.one * 0.75f;
									charmEffectWidget.DisableEmitter();
									charmEffectWidget.DisableAllRaycast();
									charmEffectWidget.baseIcon.SetAlpha(0.6f);
								}
							}
							for (int l = 0; l < count; l++)
							{
								CharmEffectWidget charmEffectWidget2 = this.panelTopHudChallengeTimeWave.curseEffectIcons[l];
								int key = this.sim.currentCurses[l];
								CurseBuff curseBuff = challengeRift.curseBuffs[l];
								int num3 = curseBuff.enchantmentData.level + 1;
								bool flag2 = curseBuff is CurseBuffGhostlyHeroes;
								charmEffectWidget2.glowImage.color = ((!flag2) ? CharmEffectWidget.curseBuffColor : CharmEffectWidget.curseBuffColorGhostlyHeroes);
								charmEffectWidget2.fillIcon.color = ((!flag2) ? CharmEffectWidget.curseBuffColorB : CharmEffectWidget.curseBuffColorGhostlyHeroesB);
								charmEffectWidget2.baseIcon.color = ((!flag2) ? CharmEffectWidget.curseBuffColorB : CharmEffectWidget.curseBuffColorGhostlyHeroesB);
								if (flag2)
								{
									charmEffectWidget2.textLevel.enabled = false;
								}
								else
								{
									charmEffectWidget2.textLevel.enabled = true;
									if (charmEffectWidget2.lastLevel != num3)
									{
										charmEffectWidget2.textLevel.text = charmEffectWidget2.lastLevel.ToString();
										float num4 = Mathf.Sign((float)(num3 - charmEffectWidget2.lastLevel));
										if (num4 > 0f)
										{
											charmEffectWidget2.DoCurseLevelUpAnimation(num3, charmEffectWidget2.lastLevel <= 0, curseBuff);
										}
										else
										{
											charmEffectWidget2.DoCurseLevelDownAnimation(num3, num3 <= 0);
										}
										charmEffectWidget2.lastLevel = num3;
									}
								}
								if (!charmEffectWidget2.isAnimating || this.panelTopHudChallengeTimeWave.initializeCursesWidgets)
								{
									charmEffectWidget2.SetIcon(this.spritesCurseEffectIcon[key]);
									if (curseBuff.enchantmentData.level >= 0)
									{
										charmEffectWidget2.textLevel.gameObject.SetActive(true);
										charmEffectWidget2.textLevel.text = num3.ToString();
										charmEffectWidget2.SetFill(1f - curseBuff.progress);
										charmEffectWidget2.baseIcon.color = ((!flag2) ? CharmEffectWidget.curseBuffColorB : CharmEffectWidget.curseBuffColorGhostlyHeroesB);
										charmEffectWidget2.baseIcon.SetAlpha(0.6f);
									}
									else
									{
										charmEffectWidget2.baseIcon.color = new Color(1f, 1f, 1f, 0.3f);
										charmEffectWidget2.textLevel.text = "0";
										charmEffectWidget2.SetFill(0f);
									}
								}
							}
							this.panelTopHudChallengeTimeWave.initializeCursesWidgets = false;
						}
						else
						{
							this.panelTopHudChallengeTimeWave.curseEffectParents.gameObject.SetActive(false);
						}
					}
					else
					{
						this.panelTopHudChallengeTimeWave.imageGold.sprite = this.spriteGoldTriangle;
						this.panelTopHudChallengeTimeWave.textGold.rectTransform.anchoredPosition = new Vector2(-242f, -39f);
						this.panelTopHudChallengeTimeWave.riftEffectsParent.gameObject.SetActive(false);
						this.panelTopHudChallengeTimeWave.curseEffectParents.gameObject.SetActive(false);
					}
					this.panelTopHudChallengeTimeWave.gameObject.SetActive(true);
					if (challenge.HasTargetTotWave())
					{
						this.panelTopHudChallengeTimeWave.SetProgress(challengeWithTime.GetTotWave(), challengeWithTime.GetTargetTotWave());
					}
					this.panelTopHudChallengeTimeWave.SetTime(Mathf.Max(0f, challengeWithTime.dur - challengeWithTime.timeCounter), (!this.sim.isActiveWorldPaused) ? dt : 0f);
					GameMode currentGameMode = this.sim.GetCurrentGameMode();
					if (currentGameMode == GameMode.RIFT)
					{
						ChallengeRift challengeRift2 = challenge as ChallengeRift;
						int num5;
						if (challengeRift2.IsCursed())
						{
							num5 = challengeRift2.riftData.cursesSetup.originalRiftNo + 1;
						}
						else
						{
							num5 = Main.instance.GetSim().GetWorld(GameMode.RIFT).GetActiveChallengeIndex() + 1;
						}
						this.panelTopHudChallengeTimeWave.textModeName.text = string.Format(LM.Get("UI_GATE_WITH_NO"), num5);
					}
					if (currentGameMode == GameMode.CRUSADE)
					{
						string text = string.Format(LM.Get("STAGE_TIME_CHALLENGE"), this.sim.GetNumTimeChallengesComplete() + 1);
						this.panelTopHudChallengeTimeWave.textModeName.text = text;
					}
					else
					{
						this.panelTopHudChallengeTimeWave.textModeName.text = UiManager.GetModeName(this.sim.GetCurrentGameMode());
					}
					this.panelTopHudChallengeTimeWave.textGold.text = this.sim.GetGold().GetString();
					this.panelTopHudChallengeTimeWave.imageGold.transform.localScale = this.GetImageGoldScale();
					if (activeWorld.GetTimeHolderTimeLeft() > 0f)
					{
						this.panelTopHudChallengeTimeWave.imageTimeHolder.gameObject.SetActive(true);
					}
					else
					{
						this.panelTopHudChallengeTimeWave.imageTimeHolder.gameObject.SetActive(false);
					}
					if (activeWorld.IsIdleGainActive())
					{
						this.textIdleGain.gameObject.SetActive(true);
						this.parentIdleGainChallenge.gameObject.SetActive(true);
						if (this.textIdleGain.rectTransform.parent != this.parentIdleGainChallenge)
						{
							this.textIdleGain.rectTransform.SetParent(this.parentIdleGainChallenge);
						}
						this.textIdleGain.rectTransform.anchoredPosition = new Vector2(0f, 0f);
					}
					else
					{
						this.textIdleGain.gameObject.SetActive(false);
						this.parentIdleGainChallenge.gameObject.SetActive(false);
					}
				}
			}
		}

		private void UpdateMerchantCounters(World activeWorld, Challenge challenge, float dt)
		{
			List<Type> list = new List<Type>();
			List<float> list2 = new List<float>();
			if (activeWorld.gameMode == GameMode.STANDARD)
			{
				if (activeWorld.GetTimeWarpTimeLeft() > 0f)
				{
					list.Add(typeof(MerchantItemTimeWarp));
					list2.Add(activeWorld.GetTimeWarpTimeLeft());
				}
				if (activeWorld.GetAutoTapTimeLeft() > 0f)
				{
					list.Add(typeof(MerchantItemAutoTap));
					list2.Add(activeWorld.GetAutoTapTimeLeft());
				}
				if (activeWorld.GetShieldTimeLeft() > 0f)
				{
					list.Add(typeof(MerchantItemShield));
					list2.Add(activeWorld.GetShieldTimeLeft());
				}
				if (activeWorld.GetGoldBoostTimeLeft() > 0f)
				{
					list.Add(typeof(MerchantItemGoldBoost));
					list2.Add(activeWorld.GetGoldBoostTimeLeft());
				}
				if (activeWorld.GetBlizzardTimeLeft() > 0f)
				{
					list.Add(typeof(MerchantItemBlizzard));
					list2.Add(activeWorld.GetBlizzardTimeLeft());
				}
				if (activeWorld.GetHotCocoaTimeLeft() > 0f)
				{
					list.Add(typeof(MerchantItemHotCocoa));
					list2.Add(activeWorld.GetHotCocoaTimeLeft());
				}
				if (activeWorld.GetOrnamentDropTimeLeft() > 0f)
				{
					list.Add(typeof(MerchantItemOrnamentDrop));
					list2.Add(activeWorld.GetOrnamentDropTimeLeft());
				}
			}
			else if (activeWorld.gameMode == GameMode.CRUSADE)
			{
				if (activeWorld.GetPowerUpTimeLeft() > 0f)
				{
					list.Add(typeof(MerchantItemPowerUp));
					list2.Add(activeWorld.GetPowerUpTimeLeft());
				}
				if (activeWorld.GetRefresherOrbTimeLeft() > 0f)
				{
					list.Add(typeof(MerchantItemRefresherOrb));
					list2.Add(activeWorld.GetRefresherOrbTimeLeft());
				}
				if (activeWorld.GetTimeHolderTimeLeft() > 0f)
				{
					list.Add(typeof(MerchantItemClock));
					list2.Add(activeWorld.GetTimeHolderTimeLeft());
				}
			}
			else
			{
				if (activeWorld.gameMode != GameMode.RIFT)
				{
					throw new NotImplementedException();
				}
				if (activeWorld.GetCatalystTimeLeft() > 0f)
				{
					list.Add(typeof(MerchantItemCatalyst));
					list2.Add(activeWorld.GetCatalystTimeLeft());
				}
			}
			if (activeWorld.powerupCooldownTimeLeft > 0f)
			{
				list.Add(typeof(DropPowerupCooldown));
				list2.Add(activeWorld.powerupCooldownTimeLeft);
			}
			if (activeWorld.powerupNonCritDamageTimeLeft > 0f)
			{
				list.Add(typeof(DropPowerupNonCritDamage));
				list2.Add(activeWorld.powerupNonCritDamageTimeLeft);
			}
			if (activeWorld.powerupReviveTimeLeft > 0f)
			{
				list.Add(typeof(DropPowerupRevive));
				list2.Add(activeWorld.powerupReviveTimeLeft);
			}
			bool flag = false;
			int num = 0;
			Vector3 position = this.merchantItemTimers[0].imageIcon.transform.position;
			if (challenge is ChallengeRift)
			{
				ChallengeRift challengeRift = challenge as ChallengeRift;
				if (challengeRift.activeCharmEffects.Count > 0)
				{
					this.merchantItemTimersParent.anchoredPosition = new Vector2(0f, -130f);
				}
				else
				{
					this.merchantItemTimersParent.anchoredPosition = new Vector2(0f, -285f);
				}
			}
			else
			{
				this.merchantItemTimersParent.anchoredPosition = new Vector2(0f, -285f);
			}
			int i = 0;
			int num2 = this.merchantItemTimers.Length;
			while (i < num2)
			{
				MerchantItemTimer merchantItemTimer = this.merchantItemTimers[i];
				if (merchantItemTimer.gameObject.activeSelf)
				{
					if (num < list2.Count && list.Contains(merchantItemTimer.itemType))
					{
						if (merchantItemTimer.itemType != list[num])
						{
							merchantItemTimer.SetItemType(list[num]);
							merchantItemTimer.imageIcon.sprite = this.GetSpriteMerchantItemSmall(list[num]);
							merchantItemTimer.imageIcon.SetNativeSize();
							merchantItemTimer.FadeInAnim();
						}
						merchantItemTimer.SetTime(list2[num]);
						num++;
					}
					else
					{
						merchantItemTimer.SetTime(0f);
						merchantItemTimer.FadeOutAnim();
						if (!merchantItemTimer.IsFadingOut())
						{
							merchantItemTimer.gameObject.SetActive(false);
						}
					}
				}
				else if (num < list2.Count)
				{
					merchantItemTimer.gameObject.SetActive(true);
					merchantItemTimer.SetItemType(list[num]);
					merchantItemTimer.imageIcon.sprite = this.GetSpriteMerchantItemSmall(list[num]);
					merchantItemTimer.imageIcon.SetNativeSize();
					merchantItemTimer.FadeInAnim();
					merchantItemTimer.SetTime(list2[num]);
					num++;
				}
				else if (!flag)
				{
					flag = true;
					position = merchantItemTimer.imageIcon.transform.position;
				}
				i++;
			}
			int num3 = list.IndexOf(typeof(DropPowerupNonCritDamage));
			UiManager.POS_POWERUP_CRIT_CHANCE_FLY = ((num3 != -1) ? this.merchantItemTimers[num3].imageIcon.transform.position : position);
			num3 = list.IndexOf(typeof(DropPowerupCooldown));
			UiManager.POS_POWERUP_COOLDOWN_FLY = ((num3 != -1) ? this.merchantItemTimers[num3].imageIcon.transform.position : position);
			num3 = list.IndexOf(typeof(DropPowerupRevive));
			UiManager.POS_POWERUP_REVIVE_FLY = ((num3 != -1) ? this.merchantItemTimers[num3].imageIcon.transform.position : position);
		}

		private void UpdateRingUi(float dt, World activeWorld, bool isModeSetup)
		{
			Totem totem = activeWorld.totem;
			if (!isModeSetup || !TutorialManager.ShouldShowRingUi() || totem == null)
			{
				this.panelRing.MakePanelVisible(false);
				this.panelRing.SetViewDiscrete(0, 0);
				this.panelRing.SetViewContinuous(0f, false);
				this.panelRing.ringSpine.PlayEffectOverheat(false);
			}
			else
			{
				this.panelRing.totem = totem;
				this.panelRing.ringSpine.totem = totem.GetData().GetDataBase();
				this.panelRing.SetRingSprites(this.GetSpriteTotemSmall(totem.id), this.GetSpriteTotemShineSmall(totem.id));
				if (totem is TotemLightning)
				{
					this.panelRing.MakePanelVisible(true);
					TotemLightning totemLightning = (TotemLightning)totem;
					this.panelRing.SetViewDiscrete(totemLightning.GetChargeReq(), totemLightning.GetCharge());
					this.panelRing.SetViewContinuous(0f, false);
					if (totemLightning.hasJustFiredThunder)
					{
						this.panelRing.ringSpine.PlayEffectLightningBig();
						if (this.state != UiState.MODE_UNLOCK_REWARD)
						{
							this.panelRing.TiltRing(3f);
						}
					}
					else if (totemLightning.hasJustFired)
					{
						this.panelRing.ringSpine.PlayEffectLightning();
						if (this.state != UiState.MODE_UNLOCK_REWARD)
						{
							this.panelRing.TiltRing(1f);
						}
					}
					this.panelRing.ringSpine.PlayEffectOverheat(false);
				}
				else if (totem is TotemFire)
				{
					this.panelRing.MakePanelVisible(true);
					TotemFire totemFire = (TotemFire)totem;
					this.panelRing.SetViewContinuous(totemFire.heat / totemFire.GetHeatMax(), totemFire.isOverHeated);
					this.panelRing.SetViewDiscrete(0, 0);
					this.panelRing.SetBarContinuousSprites();
					this.panelRing.ringSpine.PlayEffectOverheat(totemFire.isOverHeated);
					if (totemFire.hasJustFired)
					{
						this.panelRing.ringSpine.PlayEffectFire();
						if (this.state != UiState.MODE_UNLOCK_REWARD)
						{
							this.panelRing.TiltRing(1f);
						}
					}
				}
				else if (totem is TotemIce)
				{
					this.panelRing.MakePanelVisible(true);
					TotemIce totemIce = (TotemIce)totem;
					this.panelRing.SetViewContinuous(totemIce.mana / totemIce.GetManaMax(), totemIce.IsUsingMana());
					this.panelRing.SetViewDiscrete(0, 0);
					this.panelRing.SetBarContinuousSprites();
					this.panelRing.ringSpine.PlayEffectOverheat(false);
				}
				else if (totem is TotemEarth)
				{
					this.panelRing.MakePanelVisible(true);
					TotemEarth totemEarth = (TotemEarth)totem;
					float num = 1f * totemEarth.timeCharged / totemEarth.GetTimeChargedMax();
					if (totemEarth.numMeteorsWaiting > 0 && this.panelRing.fillRatioLast > num)
					{
						this.panelRing.SetViewContinuous(this.panelRing.fillRatioLast - 5f * dt, true);
					}
					else
					{
						this.panelRing.SetViewContinuous(num, num >= 1f);
					}
					this.panelRing.SetViewDiscrete(0, 0);
					this.panelRing.SetBarContinuousSprites();
					this.panelRing.ringSpine.PlayEffectOverheat(false);
				}
				else
				{
					this.panelRing.MakePanelVisible(false);
					this.panelRing.SetViewDiscrete(0, 0);
					this.panelRing.SetViewContinuous(0f, false);
					this.panelRing.ringSpine.PlayEffectOverheat(false);
				}
			}
		}

		private void UpdateTabBar(World activeWorld)
		{
			if (TutorialManager.IsHeroesTabUnlocked())
			{
				this.tabBar.SetActive(true);
				this.tabBarButtons[0].gameButton.interactable = TutorialManager.IsHubTabUnlocked();
				this.tabBarButtons[1].gameButton.interactable = TutorialManager.IsModeTabUnlocked();
				this.tabBarButtons[2].gameButton.interactable = true;
				ButtonTabBar buttonTabBar = this.tabBarButtons[3];
				buttonTabBar.gameButton.interactable = TutorialManager.IsArtifactsTabUnlocked();
				this.tabBarButtons[4].gameButton.interactable = TutorialManager.IsShopTabUnlocked();
				int numNotifications = this.sim.GetNumCollectableAchievements() + Badges.GetNumBadgesToNotify(this.sim);
				this.SetTabBarNotification(0, numNotifications);
				int numNotificationsHeroesTab = this.sim.GetNumNotificationsHeroesTab(activeWorld);
				this.SetTabBarNotification(2, numNotificationsHeroesTab);
				int num = (!this.sim.MineAnyUnlocked()) ? 0 : (((!this.sim.CanCollectMine(this.sim.mineToken)) ? 0 : 1) + ((!this.sim.CanCollectMine(this.sim.mineScrap)) ? 0 : 1));
				int num2 = 0;
				if (this.sim.flashOfferBundle != null && !this.sim.flashOfferBundle.hasSeen)
				{
					num2 = 1;
				}
				int numNotifications2 = this.sim.GetNumFreeLootpacks() + ((!this.sim.shopOfferNotification) ? 0 : 1) + num + this.sim.numUnseenTrinketPacks + num2 + ((!this.sim.IsThereNotificationsPendingFromSecondAnniversaryOffer()) ? 0 : 1);
				this.SetTabBarNotification(4, numNotifications2);
				if (this.sim.IsActiveMode(GameMode.RIFT))
				{
					this.SetTabBarNotification(3, 0);
					buttonTabBar.uiState = UiState.RIFT_RUN_CHARMS;
					buttonTabBar.SetIconSprites(this.uiData.charmTabSymbolUp, this.uiData.charmTabSymbolDown);
				}
				else
				{
					int numNotifications3 = ((!this.sim.artifactsManager.CanCraftAnArtifact(this.sim)) ? 0 : 1) + ((!this.sim.artifactsManager.CanCraftMythicalArtifact(this.sim)) ? 0 : 1);
					this.SetTabBarNotification(3, numNotifications3);
					buttonTabBar.uiState = UiState.ARTIFACTS;
					buttonTabBar.SetIconSprites(this.uiData.alchemyTabSymbolUp, this.uiData.alchemyTabSymbolDown);
				}
				if (activeWorld.isRainingGlory || activeWorld.isRainingDuck)
				{
					this.tabBarBlocker.SetActive(true);
				}
				else
				{
					this.tabBarBlocker.SetActive(false);
				}
			}
			else
			{
				this.tabBar.SetActive(false);
			}
		}

		private void UpdateEnchantmentsEffectsPanel()
		{
			if (this.sim.IsActiveMode(GameMode.RIFT))
			{
				this.charmEffectsPanel.gameObject.SetActive(true);
				World world = this.sim.GetWorld(GameMode.RIFT);
				ChallengeRift challengeRift = world.activeChallenge as ChallengeRift;
				List<CharmBuff> charmBuffs = challengeRift.charmBuffs;
				int count = challengeRift.charmBuffs.Count;
				if (this.charmEffectsPanel.activateIndex != -1)
				{
					CharmBuff charmBuff = charmBuffs[this.charmEffectsPanel.activateIndex];
					charmBuff.AddProgress(1f);
					this.charmEffectsPanel.activateIndex = -1;
				}
				for (int i = 0; i < count; i++)
				{
					CharmBuff charmBuff2 = charmBuffs[i];
					CharmEffectWidget objectFor = this.charmEffectsPanel.widgetMatch.GetObjectFor(charmBuff2);
					objectFor.willDispose = false;
					CharmEffectWidget charmEffectWidget = objectFor;
					if (!charmEffectWidget.isAnimating)
					{
						if (charmEffectWidget.needInitialization)
						{
							charmEffectWidget.needInitialization = false;
							charmEffectWidget.index = i;
							this.charmEffectsPanel.needReorder = true;
						}
						charmEffectWidget.gameObject.SetActive(!this.charmEffectsPanel.isAnimatingNewCharm || i < count - 2 || (i == count - 2 && charmBuffs[count - 1] != null));
						CharmEffectData charmEffectData = charmBuff2.enchantmentData as CharmEffectData;
						charmEffectWidget.SetIcon(this.spritesCharmEffectIcon[charmBuff2.enchantmentData.baseData.id]);
						charmEffectWidget.glowImage.color = CharmEffectWidget.charmBuffColor;
						charmEffectWidget.glowImage.gameObject.SetActive(charmBuff2.state == EnchantmentBuffState.ACTIVE);
						if (charmBuff2.state == EnchantmentBuffState.ACTIVE)
						{
							if (charmEffectData.BaseData.isAlwaysActive)
							{
								charmEffectWidget.DisableEmitter();
								float num = Mathf.PingPong(charmBuff2.stateTime / 1.6f, 1f);
								charmEffectWidget.SetFill(1f);
								charmEffectWidget.fillIcon.SetAlpha(1f - num);
								charmEffectWidget.baseIcon.SetAlpha(0.3f);
								charmEffectWidget.glowImage.SetAlpha(0.4f - num * 0.4f);
							}
							else
							{
								if (charmBuff2.GetActivationStateRate() < 0.7f)
								{
									charmEffectWidget.EnableEmitter();
								}
								else
								{
									charmEffectWidget.DisableEmitter();
								}
								charmEffectWidget.SetFill(1f);
								charmEffectWidget.baseIcon.SetAlpha(0.3f + this.GetActivationValue(charmBuff2.GetActivationStateRate(), 0f, 0.7f) * 0.5f);
								charmEffectWidget.glowImage.SetAlpha(this.GetActivationValue(charmBuff2.GetActivationStateRate(), 0.3f, 0.7f));
							}
						}
						else
						{
							charmEffectWidget.DisableEmitter();
							charmEffectWidget.baseIcon.SetAlpha(0.3f);
							charmEffectWidget.SetFill(charmBuff2.GetProgress());
						}
					}
				}
				this.charmEffectsPanel.widgetMatch.RemoveDisposibles(this.sim.GetActiveWorld().activeChallenge.state == Challenge.State.ACTION);
				if (this.charmEffectsPanel.needReorder)
				{
					this.charmEffectsPanel.needReorder = false;
					for (int j = 0; j < count; j++)
					{
						CharmBuff id = charmBuffs[j];
						CharmEffectWidget objectFor2 = this.charmEffectsPanel.widgetMatch.GetObjectFor(id);
						if (!objectFor2.isAnimating)
						{
							if (objectFor2.doSelfPosCheck)
							{
								objectFor2.rectTransform.DOAnchorPos(CharmEffectsPanel.GetWidgetPosition(j), 0.2f, false).SetEase(Ease.InOutCubic);
								objectFor2.doSelfPosCheck = false;
							}
							else
							{
								objectFor2.rectTransform.anchoredPosition = CharmEffectsPanel.GetWidgetPosition(j);
							}
						}
					}
				}
				this.charmSelectWidget.gameObject.SetActive(true);
				this.charmSelectWidget.button.interactable = (challengeRift.numCharmSelection > 0 && challengeRift.state == Challenge.State.ACTION);
				int charmSelectionNum = world.GetCharmSelectionNum();
				if (this.charmSelectWidget.cardCount != charmSelectionNum)
				{
					this.charmSelectWidget.cardCount = charmSelectionNum;
					this.charmSelectWidget.cards[3].rectTransform.anchoredPosition = new Vector2(0f, -25f);
					this.charmSelectWidget.cards[3].rectTransform.rotation = Quaternion.identity;
					this.charmSelectWidget.DoActivity(this.charmSelectWidget.button.interactable, 0.7f);
				}
				if (this.charmSelectWidget.isActive != this.charmSelectWidget.button.interactable)
				{
					this.charmSelectWidget.DoActivity(this.charmSelectWidget.button.interactable, 0f);
				}
				this.charmSelectWidget.button.text.text = challengeRift.numCharmSelection.ToString();
				if (this.charmSelectWidget.lastNumCount != challengeRift.numCharmSelection)
				{
					this.charmSelectWidget.lastNumCount = challengeRift.numCharmSelection;
					this.charmSelectWidget.DoAddAnimation();
				}
				this.charmSelectWidget.fill.fillAmount = 1f - challengeRift.charmSelectionAddTimer / 45f;
			}
			else
			{
				this.charmSelectWidget.gameObject.SetActive(false);
				this.charmEffectsPanel.gameObject.SetActive(false);
			}
		}

		private float GetActivationValue(float t, float durEndOfStart = 0.3f, float durStartOfEnd = 0.7f)
		{
			float duration = 1f - durStartOfEnd;
			float result = 1f;
			if (t <= durEndOfStart)
			{
				result = EaseManager.Evaluate(Ease.OutCubic, null, t, durEndOfStart, 0f, 0f);
			}
			else if (t > durStartOfEnd)
			{
				result = 1f - EaseManager.Evaluate(Ease.OutCubic, null, t - durStartOfEnd, duration, 0f, 0f);
			}
			return result;
		}

		private void UpdateSkillButtons()
		{
			this.heroSkillInstanceParams.Clear();
			this.sim.FillHeroSkillParams(this.heroSkillInstanceParams);
			List<bool> isSkillActives = this.heroSkillInstanceParams.isSkillActives;
			List<bool> isSkillTogglable = this.heroSkillInstanceParams.isSkillTogglable;
			List<bool> isSkillToggling = this.heroSkillInstanceParams.isSkillToggling;
			List<bool> canActivateSkills = this.heroSkillInstanceParams.canActivateSkills;
			List<float> cooldownRatios = this.heroSkillInstanceParams.cooldownRatios;
			List<float> toggleDeltas = this.heroSkillInstanceParams.toggleDeltas;
			List<float> cooldownRatios2 = this.heroSkillInstanceParams.cooldownRatios1;
			List<float> cooldownRatios3 = this.heroSkillInstanceParams.cooldownRatios2;
			List<float> cooldownMaxes = this.heroSkillInstanceParams.cooldownMaxes;
			List<float> heroReviveTimes = this.heroSkillInstanceParams.heroReviveTimes;
			List<float> heroReviveTimeMaxes = this.heroSkillInstanceParams.heroReviveTimeMaxes;
			List<bool> heroRechargeBuffs = this.heroSkillInstanceParams.heroRechargeBuffs;
			List<bool> heroStunnedBuffs = this.heroSkillInstanceParams.heroStunnedBuffs;
			List<bool> heroSilencedBuffs = this.heroSkillInstanceParams.heroSilencedBuffs;
			List<Type> skillTypes = this.heroSkillInstanceParams.skillTypes;
			int num = Mathf.Max(this.sim.GetActiveWorldNumHeroesMax(), this.sim.GetActiveWorldNumHeroes());
			if (this.sim.GetActiveWorld().totem != null)
			{
				this.buttonSkillsParent.anchoredPosition = new Vector2(394f, 328f);
			}
			else
			{
				this.buttonSkillsParent.anchoredPosition = new Vector2(285f, 328f);
			}
			for (int i = this.buttonSkills.Count - 1; i >= 0; i--)
			{
				GameButton gameButton = this.buttonSkills[i];
				gameButton.gameObject.SetActive(skillTypes.Count != 0 && TutorialManager.ShouldShowSkillButtons() && i < num);
				bool flag = i < skillTypes.Count;
				ButtonSkillAnim buttonSkillAnim = this.buttonSkillAnims[i];
				if (flag)
				{
					float cooldownRatio = cooldownRatios[i];
					float cooldownMax = cooldownMaxes[i];
					float num2 = heroReviveTimes[i];
					float heroReviveMax = heroReviveTimeMaxes[i];
					bool rechargeBuff = heroRechargeBuffs[i];
					bool stunnBuff = heroStunnedBuffs[i];
					bool silenceBuff = heroSilencedBuffs[i];
					bool flag2 = isSkillTogglable[i];
					bool isToggling = isSkillToggling[i];
					Sprite spriteSkillIconMainScreen = this.GetSpriteSkillIconMainScreen(skillTypes[i]);
					Sprite toggleSprite = null;
					if (flag2)
					{
						toggleSprite = this.GetSpriteSkillToggleIconMainScreen(skillTypes[i]);
					}
					if (this.sim.secondaryCdUi && num2 <= 0f)
					{
						if (float.IsNaN(cooldownRatios2[i]) || float.IsInfinity(cooldownRatios2[i]))
						{
							buttonSkillAnim.autoActive1Parent.gameObject.SetActive(false);
						}
						else
						{
							buttonSkillAnim.autoActive1Parent.gameObject.SetActive(true);
							buttonSkillAnim.spriteHalfAutoActive1.fillAmount = 0.02f + (1f - cooldownRatios2[i]) * 0.464f;
						}
						if (float.IsNaN(cooldownRatios3[i]) || float.IsInfinity(cooldownRatios3[i]))
						{
							buttonSkillAnim.autoActive2Parent.gameObject.SetActive(false);
							buttonSkillAnim.spriteHalfAutoActive2.fillAmount = 0f;
						}
						else
						{
							buttonSkillAnim.autoActive2Parent.gameObject.SetActive(true);
							buttonSkillAnim.spriteHalfAutoActive2.fillAmount = 0.02f + (1f - cooldownRatios3[i]) * 0.464f;
						}
					}
					else
					{
						buttonSkillAnim.autoActive1Parent.gameObject.SetActive(false);
						buttonSkillAnim.autoActive2Parent.gameObject.SetActive(false);
					}
					buttonSkillAnim.toggleDelta = toggleDeltas[i];
					buttonSkillAnim.isToggling = isToggling;
					this.buttonSkillState.isLocked = !flag;
					this.buttonSkillState.isActive = isSkillActives[i];
					this.buttonSkillState.canActivate = canActivateSkills[i];
					this.buttonSkillState.rechargeBuff = rechargeBuff;
					this.buttonSkillState.stunnBuff = stunnBuff;
					this.buttonSkillState.silenceBuff = silenceBuff;
					this.buttonSkillState.heroReviveTime = num2;
					this.buttonSkillState.heroReviveMax = heroReviveMax;
					this.buttonSkillState.cooldownRatio = cooldownRatio;
					this.buttonSkillState.cooldownMax = cooldownMax;
					this.buttonSkillState.spriteSkill = spriteSkillIconMainScreen;
					this.buttonSkillState.toggleSprite = toggleSprite;
					buttonSkillAnim.SetState(this.buttonSkillState);
				}
				else
				{
					this.buttonSkillState.isLocked = !flag;
					this.buttonSkillState.cooldownMax = 1f;
					buttonSkillAnim.SetState(this.buttonSkillState);
					buttonSkillAnim.autoActive1Parent.gameObject.SetActive(false);
					buttonSkillAnim.autoActive2Parent.gameObject.SetActive(false);
				}
				bool interactable = flag && canActivateSkills[i] && !heroStunnedBuffs[i] && !heroSilencedBuffs[i];
				gameButton.interactable = interactable;
			}
		}

		private void UpdateDrops(World activeWorld)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			foreach (Drop drop in activeWorld.drops)
			{
				if (drop.state != Drop.State.AIR)
				{
					if (!(drop is DropPowerup))
					{
						if (drop is DropCurrency)
						{
							DropCurrency dropCurrency = drop as DropCurrency;
							CurrencyType currencyType = dropCurrency.currencyType;
							if (currencyType != CurrencyType.GOLD)
							{
								if (drop.uiState != UiState.NONE && dropCurrency.showSideCurrency)
								{
									UiManager.SetPanelCurrencySideTop(currencyType, (currencyType != CurrencyType.MYTHSTONE) ? this.sim.GetCurrency(currencyType).GetString() : GameMath.GetFlooredDoubleString(this.sim.GetCurrency(currencyType).GetAmount()));
								}
								else if (currencyType == CurrencyType.GEM && !flag)
								{
									flag = true;
									UiManager.SetPanelCurrencySide(CurrencyType.GEM, this.sim.GetCredits().GetString());
								}
								else if (currencyType == CurrencyType.MYTHSTONE && !flag2)
								{
									flag2 = true;
									UiManager.SetPanelCurrencySide(CurrencyType.MYTHSTONE, this.sim.GetMythstones().GetString());
								}
								else if (currencyType == CurrencyType.SCRAP && !flag3)
								{
									flag3 = true;
									UiManager.SetPanelCurrencySide(CurrencyType.SCRAP, this.sim.GetScraps().GetString());
								}
								else if (currencyType == CurrencyType.TOKEN && !flag4)
								{
									flag4 = true;
									UiManager.SetPanelCurrencySide(CurrencyType.TOKEN, this.sim.GetTokens().GetString());
								}
								else if (currencyType == CurrencyType.AEON && !flag5)
								{
									flag5 = true;
									UiManager.SetPanelCurrencySide(CurrencyType.AEON, this.sim.GetAeons().GetString());
								}
							}
						}
					}
				}
			}
		}

		private void UpdateSideCurrency(World activeWorld, PanelCurrencySide[] panelCurrencySides)
		{
			int i = 0;
			int num = panelCurrencySides.Length;
			while (i < num)
			{
				PanelCurrencySide panelCurrencySide = panelCurrencySides[i];
				if (panelCurrencySide.timer < 5f)
				{
					if (panelCurrencySide.currencyType == CurrencyType.GOLD)
					{
						panelCurrencySide.SetCurrency(panelCurrencySide.currencyType, activeWorld.gold.GetString());
					}
					else if (panelCurrencySide.currencyType == CurrencyType.GEM)
					{
						panelCurrencySide.SetCurrency(panelCurrencySide.currencyType, this.sim.GetCredits().GetString());
					}
					else if (panelCurrencySide.currencyType == CurrencyType.MYTHSTONE)
					{
						panelCurrencySide.SetCurrency(panelCurrencySide.currencyType, this.sim.GetMythstones().GetString());
					}
					else if (panelCurrencySide.currencyType == CurrencyType.SCRAP)
					{
						panelCurrencySide.SetCurrency(panelCurrencySide.currencyType, this.sim.GetScraps().GetString());
					}
					else if (panelCurrencySide.currencyType == CurrencyType.TOKEN)
					{
						panelCurrencySide.SetCurrency(panelCurrencySide.currencyType, this.sim.GetTokens().GetString());
					}
					else if (panelCurrencySide.currencyType == CurrencyType.AEON)
					{
						panelCurrencySide.SetCurrency(panelCurrencySide.currencyType, this.sim.GetAeons().GetString());
					}
					else if (panelCurrencySide.currencyType == CurrencyType.CANDY)
					{
						panelCurrencySide.SetCurrency(panelCurrencySide.currencyType, this.sim.GetCandies().GetString());
					}
					else if (panelCurrencySide.currencyType != CurrencyType.TRINKET_BOX)
					{
						throw new NotImplementedException();
					}
				}
				i++;
			}
		}

		public static PanelCurrencySide SetPanelCurrencySide(CurrencyType currencyType, string s)
		{
			int i = 0;
			int num = UiManager._panelCurrencySides.Length;
			while (i < num)
			{
				if (UiManager._panelCurrencySides[i].timer < 5f && UiManager._panelCurrencySides[i].currencyType == currencyType)
				{
					UiManager._panelCurrencySides[i].SetCurrency(currencyType, s);
					return UiManager._panelCurrencySides[i];
				}
				i++;
			}
			int j = 0;
			int num2 = UiManager._panelCurrencySides.Length;
			while (j < num2)
			{
				if (UiManager._panelCurrencySides[j].timer >= 5f)
				{
					UiManager._panelCurrencySides[j].SetCurrency(currencyType, s);
					return UiManager._panelCurrencySides[j];
				}
				j++;
			}
			return null;
		}

		private static PanelCurrencySide SetPanelCurrencySideTop(CurrencyType currencyType, string s)
		{
			int i = 0;
			int num = UiManager._panelCurrencyOnTop.Length;
			while (i < num)
			{
				PanelCurrencySide panelCurrencySide = UiManager._panelCurrencyOnTop[i];
				if (panelCurrencySide.timer < 5f && panelCurrencySide.currencyType == currencyType)
				{
					panelCurrencySide.SetCurrency(currencyType, s);
					return panelCurrencySide;
				}
				i++;
			}
			int j = 0;
			int num2 = UiManager._panelCurrencyOnTop.Length;
			while (j < num2)
			{
				PanelCurrencySide panelCurrencySide2 = UiManager._panelCurrencyOnTop[j];
				if (panelCurrencySide2.timer >= 5f)
				{
					panelCurrencySide2.SetCurrency(currencyType, s);
					return panelCurrencySide2;
				}
				j++;
			}
			return null;
		}

		private void UpdateMusic(float dt)
		{
			if (!this.soundManager.muteMusic)
			{
				bool flag = !this.IsInHubMenus() && this.sim.GetCurrentGameMode() == GameMode.STANDARD && TutorialManager.first >= TutorialManager.First.FIGHT_HERO;
				if (this.isInHubMenusOld != this.IsInHubMenus() || flag != this.musicStandardExistsOld)
				{
					this.musicStandardExistsOld = flag;
					this.isInHubMenusOld = this.IsInHubMenus();
					this.soundManager.PlayMusic(this, this.sim);
				}
				float num = (!this.loadingTransition.IsPlaying()) ? 1f : (1f - this.loadingTransition.GetAnimRatioClose());
				if (!this.isInHubMenusOld && this.sim.GetCurrentGameMode() == GameMode.STANDARD && ((ChallengeStandard)this.sim.GetActiveWorld().activeChallenge).HasEpicBoss())
				{
					this.volumeMusicBoss += 2f * dt;
				}
				else
				{
					this.volumeMusicBoss -= 2f * dt;
				}
				if (this.volumeMusicBoss > 1f)
				{
					this.volumeMusicBoss = 1f;
				}
				else if (this.volumeMusicBoss < 0f)
				{
					this.volumeMusicBoss = 0f;
				}
				float num2 = (!this.IsInSeasonalMenu() && this.state != UiState.ARTIFACTS_CRAFT && this.state != UiState.ARTIFACT_EVOLVE) ? 1f : 0.3f;
				num -= this.volumeMusicBoss * 0.75f;
				this.soundManager.SetVolumeMusic(num * num2);
				this.soundManager.SetVolumeMusicBoss(this.volumeMusicBoss);
			}
		}

		private void UpdateTutorialStateChecks(bool isModeInAction)
		{
			World activeWorld = this.sim.GetActiveWorld();
			if (!TutorialManager.IsThereTutorialCurrently())
			{
				if (TutorialManager.shopTab == TutorialManager.ShopTab.BEFORE_BEGIN && this.state == UiState.NONE && this.sim.GetCredits().CanAfford(1.0))
				{
					TutorialManager.HaveCredits();
				}
				else if (TutorialManager.artifactsTab == TutorialManager.ArtifactsTab.BEFORE_BEGIN && this.state == UiState.NONE && this.sim.artifactsManager.CanCraftAnArtifact(this.sim) && isModeInAction && !activeWorld.isRainingGlory)
				{
					TutorialManager.CanCraftArtifact();
				}
				else if (TutorialManager.prestige == TutorialManager.Prestige.BEFORE_BEGIN && this.state == UiState.NONE && this.sim.CanShowPrestigeTutorial() && this.sim.GetUnlock(UnlockIds.PRESTIGE).isCollected && this.sim.GetUnlock(UnlockIds.HERO_WENDLE).isCollected)
				{
					TutorialManager.CanPrestige();
				}
				else if (TutorialManager.skillScreen == TutorialManager.SkillScreen.BEFORE_BEGIN && (this.state == UiState.NONE || this.state == UiState.HEROES) && activeWorld.heroes.Count > 0 && activeWorld.heroes[0].CanUpgradeSkillUlti())
				{
					TutorialManager.CanUpgradeSkill();
				}
				else if (TutorialManager.runeScreen == TutorialManager.RuneScreen.BEFORE_BEGIN && this.state == UiState.NONE && this.sim.GetNumAvailableToWearRunes() > 0)
				{
					TutorialManager.HasRune();
				}
				else if (TutorialManager.ringPrestigeReminder == TutorialManager.RingPrestigeReminder.BEFORE_BEGIN && this.state == UiState.NONE && this.sim.IsTotemUnlocked("totemFire"))
				{
					TutorialManager.ReadyForRingPrestigeReminder();
				}
				else if (TutorialManager.heroPrestigeReminder == TutorialManager.HeroPrestigeReminder.BEFORE_BEGIN && this.state == UiState.NONE && this.sim.GetActiveWorld().heroes.Count == 5 && this.sim.GetUnlockedHeroIds().Count > 5 && this.sim.GetActiveWorld().gameMode == GameMode.STANDARD)
				{
					TutorialManager.ReadyForHeroPrestigeReminder();
				}
				else if (TutorialManager.mythicalArtifactsTab == TutorialManager.MythicalArtifactsTab.BEFORE_BEGIN && this.state == UiState.NONE && this.sim.artifactsManager.NumArtifactSlotsMythical > 0)
				{
					TutorialManager.UnlockedMythicalArtifactsScreen();
				}
				else if (TutorialManager.gearScreen == TutorialManager.GearScreen.BEFORE_BEGIN && this.state == UiState.NONE && this.CanShowTutorialGearScreen(activeWorld))
				{
					TutorialManager.CanUpgradeGear();
				}
				else if (TutorialManager.trinketShop == TutorialManager.TrinketShop.BEFORE_BEGIN && this.state == UiState.NONE && this.sim.numTrinketSlots > 0)
				{
					TutorialManager.HasTrinketSlot();
				}
				else if (TutorialManager.trinketHeroTab == TutorialManager.TrinketHeroTab.BEFORE_BEGIN && !this.IsInHubMenus() && !this.IsInUnclosableMenus() && this.sim.HasAnyTrinket() && this.sim.GetActiveWorld().heroes.Count > 0 && this.sim.GetActiveWorld().heroes[0].GetData().GetDataBase().trinket == null && this.sim.CanEquipUnequipTrinket(this.sim.allTrinkets[0], this.sim.GetActiveWorld().heroes[0].GetData().GetDataBase()))
				{
					TutorialManager.HasTrinketAndFirstHeroThatCanEquipIt();
				}
				else if (TutorialManager.mineUnlock == TutorialManager.MineUnlock.BEFORE_BEGIN && this.state == UiState.NONE && this.sim.MineAnyUnlocked())
				{
					TutorialManager.MineUnlocked();
				}
				else if (TutorialManager.dailyUnlock == TutorialManager.DailyUnlock.BEFORE_BEGIN && this.state == UiState.NONE && this.sim.dailyQuest != null)
				{
					TutorialManager.DailyUnlocked();
				}
				else if (TutorialManager.dailyComplete == TutorialManager.DailyComplete.BEFORE_BEGIN && this.state == UiState.NONE && this.sim.hasDailies && this.sim.CanCollectDailyQuest())
				{
					TutorialManager.DailyCompleted();
				}
				else if (TutorialManager.riftsUnlock == TutorialManager.RiftsUnlock.BEFORE_BEGIN && this.sim.GetWorld(GameMode.RIFT).IsModeUnlocked() && this.state == UiState.HUB)
				{
					TutorialManager.OnRiftsUnlocked();
				}
				else if (this.state == UiState.HUB_MODE_SETUP && this.panelHubModeSetup.IsRiftMode() && TutorialManager.riftEffects == TutorialManager.RiftEffects.BEFORE_BEGIN)
				{
					TutorialManager.OnFirstRift();
				}
				else if (TutorialManager.firstCharm == TutorialManager.FirstCharm.BEFORE_BEGIN && this.sim.GetActiveWorld().gameMode == GameMode.RIFT && (this.sim.GetActiveWorld().activeChallenge as ChallengeRift).numCharmSelection > 0)
				{
					TutorialManager.OnCharmSelectionAvailable();
				}
				else if (TutorialManager.charmHub == TutorialManager.CharmHub.BEFORE_BEGIN && TutorialManager.firstCharm == TutorialManager.FirstCharm.FIN && this.sim.GetWorld(GameMode.RIFT).unlocks[0].isCollected && this.state == UiState.HUB)
				{
					TutorialManager.CharmCollectionReady();
				}
				else if (TutorialManager.firstCharmPack == TutorialManager.FirstCharmPack.BEFORE_BEGIN && TutorialManager.charmHub == TutorialManager.CharmHub.FIN && this.state == UiState.HUB && this.sim.numSmallCharmPacks > 0)
				{
					TutorialManager.CharmsPackAvailable();
				}
				else if (TutorialManager.charmLevelUp == TutorialManager.CharmLevelUp.BEFORE_BEGIN && TutorialManager.firstCharmPack == TutorialManager.FirstCharmPack.FIN && (this.state == UiState.HUB || this.state == UiState.HUB_SHOP))
				{
					TutorialManager.CharmUpgradeAvailable();
				}
				else if (TutorialManager.aeonDust == TutorialManager.AeonDust.BEFORE_BEGIN && this.state == UiState.HUB && this.sim.hasRiftQuest && this.sim.riftQuestDustCollected >= this.sim.GetRiftQuestDustRequired() && !this.panelHub.isAnimatingRiftPoint)
				{
					TutorialManager.AeonDustUnlocked();
				}
				else if (this.state == UiState.HUB_MODE_SETUP && this.panelHubModeSetup.IsRiftMode() && TutorialManager.repeatRifts == TutorialManager.RepeatRifts.BEFORE_BEGIN && TutorialManager.aeonDust == TutorialManager.AeonDust.FIN)
				{
					TutorialManager.OnRiftSelectionReady();
				}
				else if (TutorialManager.shopTab == TutorialManager.ShopTab.FIN && (this.state == UiState.NONE || this.state == UiState.HUB || this.state == UiState.SHOP || this.state == UiState.HUB_SHOP) && this.sim.GetActiveWorld().gameMode == GameMode.STANDARD && TutorialManager.flashOffersUnlocked == TutorialManager.FlashOffersUnlocked.BEFORE_BEGIN && (this.sim.GetCredits().GetAmount() + Static.PlayerStats.spentCreditsDuringThisSaveFile >= 1000.0 || Static.PlayerStats.IsPayer))
				{
					TutorialManager.OnFlashOffersUnlocked();
				}
				else if (TutorialManager.cursedGates == TutorialManager.CursedGates.BEFORE_BEGIN && this.sim.IsCursedRiftsModeUnlocked() && this.state == UiState.HUB_MODE_SETUP && this.panelHubModeSetup.IsRiftMode())
				{
					TutorialManager.OnCursedGatesUnlocked();
				}
				else if (TutorialManager.trinketSmithingUnlocked == TutorialManager.TrinketSmithingUnlocked.BEFORE_BEGIN && this.sim.GetUnlock(UnlockIds.TRINKET_DISASSEMBLE).isCollected && this.state == UiState.NONE)
				{
					TutorialManager.OnTrinketSmithinUnlocked();
				}
				else if (TutorialManager.trinketRecycleUnlocked == TutorialManager.TrinketRecycleUnlocked.BEFORE_BEGIN && this.sim.GetUnlock(UnlockIds.TRINKET_DISASSEMBLE).isCollected && this.state == UiState.TRINKET_INFO_POPUP)
				{
					TutorialManager.OnTrinketRecycleButtonAppeared();
				}
				else if (TutorialManager.christmasTreeEventUnlocked == TutorialManager.ChristmasTreeEventUnlocked.BEFORE_BEGIN && (this.state == UiState.NONE || this.state == UiState.HUB) && this.sim.IsChristmasTreeEnabled() && this.openOfferPopupButton.gameObject.activeSelf)
				{
					TutorialManager.OnChristmasTreeEventUnlocked();
				}
				else if (TutorialManager.artifactOverhaul == TutorialManager.ArtifactOverhaul.BEFORE_BEGIN && this.state == UiState.HUB && OldArtifactsConverter.DoesPlayerNeedsConversion(this.sim))
				{
					TutorialManager.OnArtifactOverhaulTriggered();
				}
			}
		}

		private bool CanShowTutorialGearScreen(World activeWorld)
		{
			if (activeWorld.heroes.Count > 0)
			{
				for (int i = 0; i < activeWorld.heroes.Count; i++)
				{
					Hero hero = activeWorld.heroes[i];
					if (this.sim.GetHeroEvolvability(hero.GetData().GetDataBase()))
					{
						return true;
					}
				}
			}
			return false;
		}

		public int GetFirstEvolvableHeroIndex()
		{
			World activeWorld = this.sim.GetActiveWorld();
			if (activeWorld.heroes.Count > 0)
			{
				for (int i = 0; i < activeWorld.heroes.Count; i++)
				{
					Hero hero = activeWorld.heroes[i];
					if (this.sim.GetHeroEvolvability(hero.GetData().GetDataBase()))
					{
						return i;
					}
				}
			}
			return -1;
		}

		private void UpdateHubScreen()
		{
			bool flag = this.sim.IsChristmasTreeEnabled();
			this.panelHub.christmasEventParent.SetActive(flag);
			this.panelHub.gameModeButtonsParent.SetAnchorPosY((float)((!flag) ? 0 : -120));
			(this.panelHub.transform as RectTransform).SetSizeDeltaY((float)((!flag) ? 1200 : 1320));
			if (flag)
			{
				this.panelHub.christmasCandyColletedBar.fillAmount = (float)(this.sim.candyAmountCollectedSinceLastDailyCapReset / PlayfabManager.titleData.christmasCandyCapAmount);
			}
			this.UpdateGameModeButton(this.sim, GameMode.STANDARD);
			this.UpdateGameModeButton(this.sim, GameMode.CRUSADE);
			this.UpdateGameModeButton(this.sim, GameMode.RIFT);
			if (this.panelHub.buttonGameModeStandard.IsInfoEnabled())
			{
				UniversalTotalBonus universalBonus = this.sim.GetWorld(GameMode.STANDARD).universalBonus;
				this.SetModeGlobalBonus(this.panelHub.buttonGameModeStandard, universalBonus);
			}
			if (this.panelHub.buttonGameModeCrusade.IsInfoEnabled())
			{
				UniversalTotalBonus universalBonus2 = this.sim.GetWorld(GameMode.CRUSADE).universalBonus;
				this.SetModeGlobalBonus(this.panelHub.buttonGameModeCrusade, universalBonus2);
			}
			if (this.panelHub.buttonGameModeRift.IsInfoEnabled())
			{
				UniversalTotalBonus universalBonus3 = this.sim.GetWorld(GameMode.RIFT).universalBonus;
				this.panelHub.buttonGameModeRift.panelGlobalBonus.SetDamageHero(universalBonus3.charmDamageFactor * (universalBonus3.damageFactor + universalBonus3.damageHeroFactor - 1.0) * universalBonus3.gearDamageFactor * universalBonus3.mineDamageFactor - 1.0);
				this.panelHub.buttonGameModeRift.panelGlobalBonus.SetDamageRing(universalBonus3.charmDamageFactor * (universalBonus3.damageFactor + universalBonus3.damageTotemFactor - 1.0) * universalBonus3.gearDamageFactor * universalBonus3.mineDamageFactor - 1.0);
				this.panelHub.buttonGameModeRift.panelGlobalBonus.SetGold(universalBonus3.charmGoldFactor * universalBonus3.gearGoldFactor * universalBonus3.goldFactor * universalBonus3.mineGoldFactor - 1.0);
				this.panelHub.buttonGameModeRift.panelGlobalBonus.SetHealth(universalBonus3.charmHealthFactor * universalBonus3.gearHealthFactor * universalBonus3.healthHeroFactor * universalBonus3.mineHealthFactor - 1.0);
			}
			if (UiManager.stateJustChanged)
			{
				this.panelHub.notificationAchievements.numNotifications = this.sim.GetNumCollectableAchievements() + Badges.GetNumBadgesToNotify(this.sim);
				int num = 0;
				foreach (CharmEffectData charmEffectData in this.sim.GetAllCharms())
				{
					if (charmEffectData.isNew)
					{
						num++;
					}
				}
				if (TutorialManager.IsCharmHubUnlocked())
				{
					this.panelHub.notificationCharms.numNotifications = num;
				}
				else
				{
					this.panelHub.notificationCharms.numNotifications = 0;
				}
				int num2 = 0;
				if (this.sim.flashOfferBundle != null && !this.sim.flashOfferBundle.hasSeen)
				{
					num2 = 1;
				}
				int num3 = (!this.sim.MineAnyUnlocked()) ? 0 : (((!this.sim.CanCollectMine(this.sim.mineToken)) ? 0 : 1) + ((!this.sim.CanCollectMine(this.sim.mineScrap)) ? 0 : 1));
				int numNotifications = 0;
				if (TutorialManager.IsShopTabUnlocked())
				{
					numNotifications = this.sim.GetNumFreeLootpacks() + ((!this.sim.shopOfferNotification) ? 0 : 1) + num3 + this.sim.numUnseenTrinketPacks + num2 + ((!this.sim.IsThereNotificationsPendingFromSecondAnniversaryOffer()) ? 0 : 1);
				}
				this.panelHub.notificationShop.numNotifications = numNotifications;
			}
			int num4 = 0;
			int num5 = 0;
			foreach (Dictionary<string, bool> dictionary in Static.PlayerStats.achievements)
			{
				foreach (KeyValuePair<string, bool> keyValuePair in dictionary)
				{
					if (keyValuePair.Value)
					{
						num5++;
					}
					num4++;
				}
			}
			bool flag2 = this.sim.HasTrinketTabHint();
			if (flag2)
			{
				this.panelHub.buttonTrinkets.gameObject.SetActive(true);
				bool flag3 = !this.sim.hasEverOwnedATrinket;
				if (flag3)
				{
					this.panelHub.buttonTrinkets.icon.sprite = this.uiData.iconTabLockBrown;
					this.panelHub.buttonTrinkets.icon.rectTransform.sizeDelta = new Vector2(80f, 80f);
				}
				else
				{
					this.panelHub.buttonTrinkets.icon.sprite = this.uiData.iconTabHubTrinket;
					this.panelHub.buttonTrinkets.icon.SetNativeSize();
				}
			}
			else
			{
				this.panelHub.buttonTrinkets.gameObject.SetActive(false);
			}
			if (!TutorialManager.IsArtifactsTabUnlocked())
			{
				this.panelHub.buttonArtifacts.icon.sprite = this.uiData.iconTabLock;
				this.panelHub.buttonArtifacts.interactable = false;
				this.panelHub.buttonArtifacts.icon.SetNativeSize();
			}
			else
			{
				this.panelHub.buttonArtifacts.interactable = true;
				this.panelHub.buttonArtifacts.icon.sprite = this.uiData.iconTabHubArtifact;
				this.panelHub.buttonArtifacts.icon.SetNativeSize();
			}
			if (!TutorialManager.IsShopTabUnlocked())
			{
				this.panelHub.buttonShop.icon.sprite = this.uiData.iconTabLock;
				this.panelHub.buttonShop.interactable = false;
				this.panelHub.buttonShop.icon.SetNativeSize();
			}
			else
			{
				this.panelHub.buttonShop.interactable = true;
				this.panelHub.buttonShop.icon.sprite = this.uiData.iconTabHubShop;
				this.panelHub.buttonShop.icon.SetNativeSize();
			}
			if (this.sim.GetMaxStageReached() >= 600 || this.sim.GetWorld(GameMode.RIFT).IsModeUnlocked())
			{
				this.panelHub.buttonCharms.gameObject.SetActive(true);
				this.panelHub.buttonCharms.interactable = TutorialManager.IsCharmHubUnlocked();
			}
			else
			{
				this.panelHub.buttonCharms.gameObject.SetActive(false);
			}
		}

		private void SetModeGlobalBonus(ButtonGameMode panelGameMode, UniversalTotalBonus universalBonus)
		{
			panelGameMode.panelGlobalBonus.SetDamageHero((universalBonus.damageFactor + universalBonus.damageHeroFactor - 1.0) * universalBonus.gearDamageFactor * universalBonus.mineDamageFactor - 1.0);
			panelGameMode.panelGlobalBonus.SetDamageRing((universalBonus.damageFactor + universalBonus.damageTotemFactor - 1.0) * universalBonus.gearDamageFactor * universalBonus.mineDamageFactor - 1.0);
			panelGameMode.panelGlobalBonus.SetGold(universalBonus.goldFactor * universalBonus.gearGoldFactor * universalBonus.mineGoldFactor - 1.0);
			panelGameMode.panelGlobalBonus.SetHealth(universalBonus.healthHeroFactor * universalBonus.gearHealthFactor * universalBonus.mineHealthFactor - 1.0);
		}

		private void SetHubTabButtonState(GameButton button, bool buttonState)
		{
			button.interactable = buttonState;
			button.icon.gameObject.SetActive(buttonState);
		}

		private void UpdateModeScreen(World activeWorld)
		{
			if (activeWorld.activeChallenge is ChallengeStandard)
			{
				if (UiManager.stateJustChanged)
				{
					this.panelMode.toggleInfo.gameObject.SetActive(true);
					this.panelMode.textNextUnlock.text = LM.Get("UI_MODE_NEXT_QUEST");
					this.panelMode.presrigeButtonParent.SetActive(true);
					this.panelMode.buttonAbandonChallenge.gameObject.SetActive(false);
					this.panelMode.imagePrestigeBg.sprite = this.panelMode.spriteAdventureEndModeBg;
					this.panelMode.imagePrestigeBgDecor.color = PanelMode.spriteAdventureEndModeDecorColor;
					if (this.sim.numPrestiges > 0)
					{
						this.panelMode.shareScreenshotButton.rectTransform.SetAnchorPosY(3.6f);
						this.panelMode.prestigeLabel.gameObject.SetActive(false);
						this.panelMode.buttonPrestige.rectTransform.SetAnchorPosY(3.6f);
					}
					else
					{
						this.panelMode.shareScreenshotButton.rectTransform.SetAnchorPosY(-12f);
						this.panelMode.prestigeLabel.gameObject.SetActive(true);
						this.panelMode.buttonPrestige.rectTransform.SetAnchorPosY(-12f);
						int prestigeReqStageNo = this.sim.GetActiveWorld().GetPrestigeReqStageNo();
						this.panelMode.prestigeLabel.text = string.Format(LM.Get("UI_MODE_BEAT_STAGE"), prestigeReqStageNo.ToString());
					}
					Unlock nextUnhiddenUnlock = this.sim.GetNextUnhiddenUnlock();
					if (nextUnhiddenUnlock.isCollected)
					{
						this.panelMode.imageAllUnlocksCollected.gameObject.SetActive(true);
						this.panelMode.panelNextUnlock.gameObject.SetActive(false);
						this.panelMode.textNextUnlock.gameObject.SetActive(false);
					}
					else
					{
						this.panelMode.imageAllUnlocksCollected.gameObject.SetActive(false);
						this.panelMode.panelNextUnlock.gameObject.SetActive(true);
						this.panelMode.textNextUnlock.gameObject.SetActive(true);
						this.UpdatePanelUnlock(this.panelMode.panelNextUnlock, nextUnhiddenUnlock, true);
					}
					this.panelMode.buttonSeeAllUnlocks.gameObject.SetActive(true);
				}
				bool interactable = this.sim.CanPrestigeNow();
				this.panelMode.buttonPrestige.interactable = interactable;
			}
			else if (activeWorld.activeChallenge is ChallengeRift)
			{
				ChallengeRift challengeRift = activeWorld.activeChallenge as ChallengeRift;
				if (this.panelMode.isMercanDealInfoShowing)
				{
					this.panelMode.DisableInfo(activeWorld.eventMerchantItems != null);
				}
				this.panelMode.shareScreenshotButton.rectTransform.SetAnchorPosY(3.6f);
				this.panelMode.toggleInfo.gameObject.SetActive(false);
				this.panelMode.imagePrestigeBg.sprite = this.panelMode.spriteChallengeEndModeBg;
				this.panelMode.imagePrestigeBgDecor.color = PanelMode.spriteChallengeEndModeDecorColor;
				this.panelMode.imageAllUnlocksCollected.gameObject.SetActive(false);
				this.panelMode.panelNextUnlock.gameObject.SetActive(true);
				this.panelMode.textNextUnlock.gameObject.SetActive(true);
				this.panelMode.presrigeButtonParent.gameObject.SetActive(false);
				this.panelMode.buttonAbandonChallenge.gameObject.SetActive(true);
				this.panelMode.imagePrestigeBg.sprite = this.panelMode.spriteChallengeEndModeBg;
				this.panelMode.imagePrestigeBgDecor.color = PanelMode.spriteChallengeEndModeDecorColor;
				this.panelMode.buttonAbandonChallenge.interactable = !activeWorld.IsRaining();
				this.panelMode.textNextUnlock.text = LM.Get("UI_RIFT_GATE_REWARD");
				if (challengeRift.unlock.isCollected)
				{
					this.UpdatePanelUnlockAeonDust(this.panelMode.panelNextUnlock, challengeRift, false);
				}
				else
				{
					this.UpdatePanelUnlock(this.panelMode.panelNextUnlock, challengeRift.unlock, false);
				}
				this.panelMode.buttonSeeAllUnlocks.gameObject.SetActive(false);
			}
			else
			{
				if (!(activeWorld.activeChallenge is ChallengeWithTime))
				{
					throw new NotImplementedException();
				}
				if (UiManager.stateJustChanged)
				{
					if (this.panelMode.isMercanDealInfoShowing)
					{
						this.panelMode.DisableInfo(activeWorld.eventMerchantItems != null);
					}
					this.panelMode.shareScreenshotButton.rectTransform.SetAnchorPosY(3.6f);
					this.panelMode.toggleInfo.gameObject.SetActive(false);
					this.panelMode.imagePrestigeBg.sprite = this.panelMode.spriteChallengeEndModeBg;
					this.panelMode.imagePrestigeBgDecor.color = PanelMode.spriteChallengeEndModeDecorColor;
					this.panelMode.imageAllUnlocksCollected.gameObject.SetActive(false);
					this.panelMode.panelNextUnlock.gameObject.SetActive(true);
					this.panelMode.textNextUnlock.gameObject.SetActive(true);
					this.panelMode.presrigeButtonParent.gameObject.SetActive(false);
					this.panelMode.buttonAbandonChallenge.gameObject.SetActive(true);
					this.panelMode.imagePrestigeBg.sprite = this.panelMode.spriteChallengeEndModeBg;
					this.panelMode.imagePrestigeBgDecor.color = PanelMode.spriteChallengeEndModeDecorColor;
					this.panelMode.buttonAbandonChallenge.interactable = !activeWorld.IsRaining();
					this.panelMode.textNextUnlock.text = LM.Get("UI_MODE_CURRENT_CLG");
					this.panelMode.buttonSeeAllUnlocks.gameObject.SetActive(false);
				}
				ChallengeWithTime challengeWithTime = (ChallengeWithTime)activeWorld.activeChallenge;
				this.UpdatePanelUnlock(this.panelMode.panelNextUnlock, challengeWithTime.unlock, false);
			}
			bool flag = this.sim.IsMerchantUnlocked();
			if (UiManager.stateJustChanged)
			{
				this.panelMode.panelMerchant.SetActive(flag);
				this.panelMode.panelMerchantClosed.SetActive(!flag);
			}
			if (flag)
			{
				bool active = this.sim.GetEventMerchantItems() != null && this.sim.IsChristmasTreeEnabled();
				if (this.panelMode.lookingEventMerchantItems && TrustedTime.IsReady() && PlayfabManager.christmasOfferConfigLoaded)
				{
					this.panelMode.merchantItemsMessage.enabled = true;
					this.panelMode.merchantItemsMessage.text = string.Format(LM.Get("EVENT_MERCHANT_ITEMS_TIMER"), GameMath.GetLocalizedTimeString(PlayfabManager.christmasOfferConfig.offerConfig.endDateParsed - TrustedTime.Get()));
				}
				else
				{
					this.panelMode.merchantItemsMessage.enabled = false;
				}
				this.panelMode.merchantItemTabsParent.SetActive(active);
				List<Simulation.MerchantItem> list = (!this.panelMode.lookingEventMerchantItems) ? this.sim.GetMerchantItems() : this.sim.GetEventMerchantItems();
				List<bool> list2 = this.sim.CanAffordMerchantItems();
				int i = 0;
				int num = this.panelMode.merchantItems.Length;
				while (i < num)
				{
					MerchantItem merchantItem = this.panelMode.merchantItems[i];
					if (list.Count <= i)
					{
						merchantItem.gameObject.SetActive(false);
					}
					else
					{
                        Simulation.MerchantItem merchantItem2 = list[i];
						merchantItem.eventOrnament.SetActive(this.panelMode.lookingEventMerchantItems);
						if (merchantItem2.IsUnlocked())
						{
							if (UiManager.stateJustChanged)
							{
								merchantItem.unlockHintText.text = merchantItem2.GetTitleString();
							}
							merchantItem.glowImage.gameObject.SetActive(true);
							merchantItem.gameObject.SetActive(true);
							merchantItem.buttonRef.gameObject.SetActive(true);
							merchantItem.textHowManyLeft.gameObject.SetActive(true);
							merchantItem.unlockHintParent.gameObject.SetActive(false);
							merchantItem.buttonUpgradeAnim.spriteNormal = ((!this.panelMode.lookingEventMerchantItems) ? this.uiData.merchantItemNormalBg : this.uiData.merchantItemEventBg);
							merchantItem.spriteBgNormal = ((!this.panelMode.lookingEventMerchantItems) ? this.uiData.merchantItemNormalBg : this.uiData.merchantItemEventBg);
							merchantItem.background.sprite = ((!this.panelMode.lookingEventMerchantItems) ? this.uiData.merchantItemNormalBg : this.uiData.merchantItemEventBg);
							merchantItem.imageItem.material = null;
							merchantItem.imageItem.SetAlpha(1f);
							merchantItem.buttonUpgradeAnim.doNotTouchToTextColor = false;
							int numLeft = merchantItem2.GetNumLeft();
							if (UiManager.stateJustChanged)
							{
								merchantItem.textTitle.color = new Color32(241, 215, 153, byte.MaxValue);
								merchantItem.textTitle.text = merchantItem2.GetTitleString();
								merchantItem.imageItem.sprite = this.GetSpriteMerchantItem(merchantItem2.GetId());
								if (merchantItem2.GetNumMax() == 0)
								{
									merchantItem.textHowManyLeft.enabled = false;
								}
								else
								{
									merchantItem.textHowManyLeft.enabled = true;
									merchantItem.textHowManyLeft.text = merchantItem2.GetNumLeftString();
								}
								bool flag2 = numLeft <= 0;
								merchantItem.SetState(!flag2 || merchantItem2.GetPrice() == 0.0);
								if (flag2 && merchantItem2.GetPrice() > 0.0)
								{
									merchantItem.buttonRef.sprite = this.uiData.spriteShopBrownButton;
								}
								else
								{
									merchantItem.buttonRef.sprite = this.uiData.spriteShopGreenButton;
								}
								if (merchantItem2.GetPrice() == 0.0)
								{
									merchantItem.buttonUpgradeAnim.iconDownType = ButtonUpgradeAnim.IconType.NONE;
									merchantItem.priceWithoutIcon.text = merchantItem2.GetNumInInventoryString();
									merchantItem.textPrice.enabled = false;
									merchantItem.priceWithoutIcon.enabled = true;
								}
								else
								{
									merchantItem.buttonUpgradeAnim.iconDownType = ButtonUpgradeAnim.IconType.TOKENS;
									merchantItem.textPrice.enabled = true;
									merchantItem.priceWithoutIcon.enabled = false;
									merchantItem.textPrice.text = merchantItem2.GetPriceString();
								}
							}
							merchantItem.buttonUpgradeAnim.textCantAffordColorChangeManual = !list2[i];
							merchantItem.gameButton.interactable = (numLeft > 0 || merchantItem2.GetPrice() == 0.0);
							merchantItem.buttonUpgradeAnim.textCantAffordColorChangeManual = !list2[i];
						}
						else
						{
							merchantItem.buttonUpgradeAnim.doNotTouchToTextColor = true;
							merchantItem.glowImage.gameObject.SetActive(false);
							merchantItem.gameObject.SetActive(true);
							merchantItem.unlockHintParent.gameObject.SetActive(true);
							merchantItem.buttonRef.gameObject.SetActive(false);
							merchantItem.textHowManyLeft.enabled = false;
							merchantItem.gameButton.interactable = false;
							merchantItem.unlockHintText.text = this.GetMerchantItem(merchantItem2, activeWorld);
							if (UiManager.stateJustChanged)
							{
								merchantItem.textTitle.color = new Color32(70, 76, 80, byte.MaxValue);
								merchantItem.textTitle.text = LM.Get("UI_LOCKED");
								merchantItem.imageItem.SetAlpha(0.3f);
								merchantItem.imageItem.material = this.uiData.grayscaledSpriteMat;
								merchantItem.imageItem.sprite = this.GetSpriteMerchantItem(merchantItem2.GetId());
								merchantItem.background.sprite = this.uiData.merchantItemLockedBg;
							}
						}
					}
					i++;
				}
			}
			else
			{
				this.panelMode.merchantItemTabsParent.SetActive(false);
			}
		}

		private void UpdateHeroesScreen(World activeWorld)
		{
			bool flag = false;
			this.heroScreenForceUpdateTimer++;
			if (this.heroScreenForceUpdateTimer > 30)
			{
				this.heroScreenForceUpdateTimer = 0;
				flag = true;
			}
			ChallengeUpgrade nextWorldUpgrade = this.sim.GetNextWorldUpgrade();
			ChallengeUpgrade prevWorldUpgrade = this.sim.GetPrevWorldUpgrade();
			if (activeWorld.gameMode != GameMode.RIFT)
			{
				this.panelHeroes.panelWorldUpgrade.textName.text = this.sim.GetWorldUpgradeDescription();
				double worldUpgradeCost = this.sim.GetWorldUpgradeCost();
				string text;
				if (worldUpgradeCost == -1.0)
				{
					text = LM.Get("UI_HEROES_MAXED_OUT");
				}
				else
				{
					text = GameMath.GetDoubleString(worldUpgradeCost);
				}
				this.panelHeroes.panelWorldUpgrade.buttonUpgradeAnim.textUp.text = text;
				this.panelHeroes.panelWorldUpgrade.gameObject.SetActive(true);
				this.panelHeroes.panelWorldUpgrade.SetButtonState(this.sim.CanAffordWorldUpgrade(), this.sim.IsNextWorldUpgradeUnlocked(), nextWorldUpgrade);
				this.panelHeroes.isMilestoneEnabled = true;
				this.panelHeroes.autoSkillParent.gameObject.SetActive(this.sim.hasSkillPointsAutoDistribution && activeWorld.gameMode == GameMode.STANDARD);
			}
			else
			{
				this.panelHeroes.isMilestoneEnabled = false;
				this.panelHeroes.panelWorldUpgrade.gameObject.SetActive(false);
				this.panelHeroes.autoSkillParent.gameObject.SetActive(true);
			}
			this.panelHeroes.autoSkillDistributionToggle.isOn = activeWorld.autoSkillDistribute;
			if (activeWorld.gameMode == GameMode.STANDARD)
			{
				int num = -1;
				int num2 = 0;
				if (nextWorldUpgrade != null)
				{
					num = nextWorldUpgrade.stageReq;
				}
				if (prevWorldUpgrade != null)
				{
					num2 = prevWorldUpgrade.stageReq;
				}
				this.panelHeroes.panelWorldUpgrade.SetBarState(ChallengeStandard.GetStageNo(activeWorld.activeChallenge.GetTotWave() - 1) - num2, num - num2);
			}
			else if (activeWorld.gameMode == GameMode.CRUSADE)
			{
				int num3 = -1;
				int num4 = 0;
				if (nextWorldUpgrade != null)
				{
					num3 = nextWorldUpgrade.waveReq;
				}
				if (prevWorldUpgrade != null)
				{
					num4 = prevWorldUpgrade.waveReq;
				}
				this.panelHeroes.panelWorldUpgrade.SetBarState(activeWorld.activeChallenge.GetWaveNumber() - num4, num3 - num4);
			}
			bool flag2 = activeWorld.totem != null;
			this.panelHeroes.isTotemSelected = flag2;
			if (flag2)
			{
				if (UiManager.stateJustChanged || flag)
				{
					this.panelHeroes.panelTotem.rectTransform.SetAnchorPosY(-160f);
					this.panelHeroes.panelTotem.gameObject.SetActive(true);
					this.panelHeroes.panelTotem.SetName(this.sim.GetTotemName());
					double totemUpgradeCost = this.sim.GetTotemUpgradeCost();
					string text2;
					if (totemUpgradeCost == 0.0)
					{
						text2 = LM.Get("UI_SHOP_CHEST_0");
					}
					else
					{
						text2 = GameMath.GetDoubleString(totemUpgradeCost);
					}
					this.panelHeroes.panelTotem.buttonUpgradeAnim.textUp.text = text2;
					this.panelHeroes.panelTotem.SetLevel(this.sim.GetTotemLevel(), this.sim.GetTotemXp(), this.sim.GetTotemXpNeedForNextLevel());
					this.panelHeroes.panelTotem.SetDamage(this.sim.GetTotemDamageAll(), this.sim.GetTotemDamageDifUpgrade());
					this.panelHeroes.panelTotem.imageIconTotem.sprite = this.GetSpriteTotemSmall(activeWorld.totem.id);
					this.panelHeroes.panelTotem.SetNumNotifications(this.sim.GetNumAvailableToWearRunes());
					bool upgradeNotification = activeWorld.totem.buffTotalEffect.maxCostReductionReached.Count > 0 && !activeWorld.totem.buffTotalEffect.maxCostReductionReached.Contains(false);
					this.panelHeroes.panelTotem.SetUpgradeNotification(upgradeNotification);
				}
				this.panelHeroes.panelTotem.buttonUpgrade.interactable = this.sim.CanAffordTotemUpgrade();
			}
			else if (UiManager.stateJustChanged || flag)
			{
				this.panelHeroes.panelTotem.gameObject.SetActive(false);
			}
			List<Hero> heroes = activeWorld.heroes;
			this.panelHeroes.SetNumHeroes(this.sim.GetActiveWorldNumHeroes(), this.sim.GetActiveWorldNumHeroesMax(), this.sim.hasSkillPointsAutoDistribution && this.sim.GetActiveWorld().gameMode == GameMode.STANDARD);
			for (int i = heroes.Count - 1; i >= 0; i--)
			{
				Hero hero = heroes[i];
				double upgradeCost = hero.GetUpgradeCost(true);
				bool interactable = activeWorld.gold.CanAfford(upgradeCost);
				PanelHero panelHero = this.panelHeroes.heroPanels[i];
				if (UiManager.stateJustChanged || flag)
				{
					bool upgradeNotification2 = hero.buffTotalEffect.maxCostReductionReached.Count > 0 && !hero.buffTotalEffect.maxCostReductionReached.Contains(false);
					string id = heroes[i].GetId();
					int evolveLevel = heroes[i].GetEvolveLevel();
					panelHero.SetName(LM.Get(heroes[i].GetNameKey()));
					panelHero.SetHeroSprite(this.GetSpriteHeroPortrait(id), evolveLevel);
					panelHero.SetButtonUpgradeCost(upgradeCost);
					panelHero.SetUpgradeNotification(upgradeNotification2);
					int numUnspentSkillPoints = hero.GetNumUnspentSkillPoints();
					bool flag3 = hero.HasAllSkillsReachedMaxLevel();
					panelHero.SetNumNotifications((!flag3) ? (numUnspentSkillPoints + ((!this.sim.IsThereAnySkinBought()) ? 0 : this.sim.GetHerosNewSkinCount(hero.GetId()))) : 0);
					bool hasTrinket = hero.trinket != null;
					bool isMaxed = hero.trinket != null && hero.trinket.IsCapped();
					panelHero.SetTrinketStatus(hasTrinket, isMaxed);
					panelHero.SetHealthMax(hero.GetHealthMax());
					panelHero.SetLevel(hero.GetLevel(), hero.GetXp(), hero.GetXpNeedForNextLevel());
					panelHero.SetDamage(hero.GetDamageAll(), hero.GetDamageDifUpgrade());
				}
				panelHero.buttonUpgrade.interactable = interactable;
				panelHero.SetHealthRatio(hero.GetHealthRatio());
			}
			if (flag2)
			{
				bool interactable2 = this.sim.CanAffordNewHero() && this.sim.GetHeroesAvailableToBuy().Count > 0;
				this.panelHeroes.buttonOpenNewHeroPanel.gameButton.interactable = interactable2;
				this.panelHeroes.buttonBuyRandomHero.interactable = interactable2;
				if (UiManager.stateJustChanged || flag)
				{
					this.panelHeroes.buttonOpenNewHeroPanel.textUp.text = GameMath.GetDoubleString(this.sim.GetNewHeroCost());
				}
			}
		}

		private void SetShopParent(int index)
		{
			if (index == 1)
			{
				if (this.panelShop.transform.parent == this.scrollView.viewport)
				{
					return;
				}
				this.panelShop.transform.SetParent(this.scrollView.viewport);
				this.panelShop.rectTransform.SetTopDelta(0f);
				this.panelShop.rectTransform.SetBottomDelta(-1f);
			}
			else if (index == 2)
			{
				if (this.panelShop.transform.parent == this.panelHubShop.shopTargetParent)
				{
					return;
				}
				this.panelShop.transform.SetParent(this.panelHubShop.shopTargetParent);
				this.panelShop.rectTransform.SetTopDelta(0f);
				this.panelShop.rectTransform.SetBottomDelta(-1f);
			}
		}

		private void SetArtifactScrollParent(int index)
		{
			if (index == 1)
			{
				if (this.panelArtifactScroller.rectTransform.parent == this.gameplayArtifactsTabParent)
				{
					return;
				}
				this.panelArtifactScroller.rectTransform.SetParent(this.gameplayArtifactsTabParent);
				this.panelArtifactScroller.rectTransform.SetTopDelta(0f);
				this.panelArtifactScroller.rectTransform.SetBottomDelta(10f);
			}
			else if (index == 2)
			{
				if (this.panelArtifactScroller.rectTransform.parent == this.panelHubartifacts.transform)
				{
					return;
				}
				this.panelArtifactScroller.rectTransform.SetParent(this.panelHubartifacts.transform);
				this.panelArtifactScroller.rectTransform.SetAsFirstSibling();
				this.panelArtifactScroller.rectTransform.SetTopDelta(88f);
				this.panelArtifactScroller.rectTransform.SetBottomDelta(-1f);
			}
		}

		public void UpdateArtifactScroller()
		{
			bool flag = this.sim.HasMythicalTabHint();
			bool flag2 = flag || this.sim.artifactsManager.NumArtifactSlotsMythical > 0;
			bool flag3 = flag && this.sim.artifactsManager.NumArtifactSlotsMythical <= 0;
			PanelArtifactScroller panelArtifactScroller = this.panelArtifactScroller;
			panelArtifactScroller.mythicalTabLockIcon.gameObject.SetActive(flag3);
			panelArtifactScroller.buttonTabMythical.text.enabled = !flag3;
			if (!flag2)
			{
				panelArtifactScroller.isLookingAtMythical = false;
				panelArtifactScroller.CalculateContentSize(this.sim.artifactsManager.AvailableSlotsCount);
			}
			if (panelArtifactScroller.isLookingAtMythical)
			{
				panelArtifactScroller.HideFilterOptions();
				if (flag3)
				{
					panelArtifactScroller.scrollRect.gameObject.SetActive(false);
					panelArtifactScroller.noMythicalParent.gameObject.SetActive(true);
					panelArtifactScroller.textMythicalHint.text = this.sim.GetMythicalHintString();
				}
				else
				{
					panelArtifactScroller.scrollRect.gameObject.SetActive(true);
					panelArtifactScroller.noMythicalParent.gameObject.SetActive(false);
				}
			}
			else
			{
				panelArtifactScroller.scrollRect.gameObject.SetActive(true);
				panelArtifactScroller.noMythicalParent.gameObject.SetActive(false);
			}
			if (UiManager.stateJustChanged && panelArtifactScroller.alchemyTable.Skeleton != null)
			{
				string text = (!panelArtifactScroller.isLookingAtMythical) ? "table_01" : "table_02";
				if (panelArtifactScroller.alchemyTable.Skeleton.Skin.Name != text)
				{
					panelArtifactScroller.alchemyTable.Skeleton.SetSkin(text);
					panelArtifactScroller.alchemyTable.Skeleton.SetToSetupPose();
					panelArtifactScroller.alchemyTable.UpdateTTR();
				}
			}
			panelArtifactScroller.tabButtonsParent.gameObject.SetActive(flag2);
			this.panelArtifactScroller.scrollRect.viewport.SetTopDelta((float)((!flag2) ? 0 : 104));
			panelArtifactScroller.buttonTabRegular.interactable = panelArtifactScroller.isLookingAtMythical;
			panelArtifactScroller.buttonTabMythical.interactable = !panelArtifactScroller.isLookingAtMythical;
			double slotCost = this.sim.artifactsManager.GetSlotCost();
			bool flag4 = this.sim.CanCurrencyAfford(CurrencyType.GEM, slotCost);
			panelArtifactScroller.buttonUnlockSlot.buttonUpgrade.gameButton.interactable = flag4;
			panelArtifactScroller.buttonUnlockSlot.buttonUpgrade.textUp.text = GameMath.GetDoubleString(slotCost);
			if (!flag4)
			{
				panelArtifactScroller.buttonUnlockSlot.buttonUpgrade.isLevelUp = false;
			}
			if (panelArtifactScroller.isAnimatingQp || this.state == UiState.ARTIFACTS_CRAFT)
			{
				panelArtifactScroller.textTALAmount.text = panelArtifactScroller.qpToShow.ToString();
			}
			else
			{
				panelArtifactScroller.textTALAmount.text = this.sim.artifactsManager.TotalArtifactsLevel.ToString();
			}
			if (this.sim.artifactsManager.GetTotalArtifactsLevelOfNextMilestone() < 0)
			{
				panelArtifactScroller.SetInfoTraySize(false);
			}
			else
			{
				panelArtifactScroller.textNextTalLabel.text = string.Format(LM.Get("ARTIFACT_NEXT_TAL"), this.sim.artifactsManager.GetTotalArtifactsLevelOfNextMilestone());
				panelArtifactScroller.SetInfoTraySize(true);
			}
			if (panelArtifactScroller.isLookingAtMythical)
			{
				List<Simulation.Artifact> mythicalArtifacts = this.sim.artifactsManager.MythicalArtifacts;
				int num = this.sim.artifactsManager.NumArtifactSlotsMythical;
				bool flag5 = num >= 40;
				double mythicalArtifactCraftCost = this.sim.artifactsManager.GetMythicalArtifactCraftCost();
				string costString = GameMath.GetDoubleString(mythicalArtifactCraftCost);
				if (mythicalArtifactCraftCost < 0.0)
				{
					costString = LM.Get("UI_HEROES_MAXED_OUT");
					panelArtifactScroller.craftButton.iconUpType = ButtonUpgradeAnim.IconType.NONE;
					panelArtifactScroller.craftButton.textUpContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
					panelArtifactScroller.craftButton.textUp.rectTransform.SetSizeDeltaX(180f);
				}
				else
				{
					panelArtifactScroller.craftButton.iconUpType = ButtonUpgradeAnim.IconType.MYTHSTONES;
					panelArtifactScroller.craftButton.textUpContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
				}
				bool flag6 = this.sim.artifactsManager.MythicalArtifacts.Count == Simulation.Artifact.GetLegendaryEffectsTotalCount();
				if (flag6)
				{
					panelArtifactScroller.craftButton.gameObject.SetActive(false);
					panelArtifactScroller.textAllArtifactsCrafted.enabled = true;
				}
				else
				{
					panelArtifactScroller.textAllArtifactsCrafted.enabled = false;
					panelArtifactScroller.craftButton.gameObject.SetActive(true);
					bool canCraft = this.sim.artifactsManager.CanCraftMythicalArtifact(this.sim);
					bool canAffordCraft = this.sim.artifactsManager.CanAffordMythicalArtifactCraft(this.sim);
					panelArtifactScroller.SetCraftButton(costString, canCraft, canAffordCraft);
				}
				int selectedArtifactIndex = panelArtifactScroller.selectedArtifactIndex;
				UpgradeSift upgradeSift = panelArtifactScroller.multipleBuySteps[panelArtifactScroller.multipleBuyIndex];
				if (UiManager.stateJustChanged)
				{
					panelArtifactScroller.SetArtifactsParentPosition(true);
					panelArtifactScroller.CalculateContentSize(num);
					int count = mythicalArtifacts.Count;
					panelArtifactScroller.artifactCount = count;
					int count2 = panelArtifactScroller.buttonArtifacts.Count;
					int num2 = selectedArtifactIndex;
					if (selectedArtifactIndex == -1)
					{
						panelArtifactScroller.SetSelected(selectedArtifactIndex, 0f);
					}
					for (int i = 0; i < count2; i++)
					{
						ButtonArtifact buttonArtifact = panelArtifactScroller.buttonArtifacts[i];
						if (!panelArtifactScroller.isLookingAtMythical && !panelArtifactScroller.dontUpdateStoneScale)
						{
							buttonArtifact.artifactStone.rectTransform.SetScale(1f);
						}
						if (i < count)
						{
							Simulation.Artifact artifact = mythicalArtifacts[i];
							buttonArtifact.artifactStone.imageIcon.sprite = this.GetEffectTypeSprite(artifact.GetCategoryType(), artifact.effects[0].GetType());
							Graphic imageIcon = buttonArtifact.artifactStone.imageIcon;
							Color color = new Color(1f, 1f, 1f, (!artifact.IsEnabled()) ? 0.5f : 1f);
							buttonArtifact.artifactStone.imageStone.color = color;
							imageIcon.color = color;
							buttonArtifact.artifactStone.imageIcon.SetNativeSize();
							buttonArtifact.qualityPointString = GameMath.GetDoubleString(artifact.GetQuality());
							buttonArtifact.name = artifact.GetName();
							buttonArtifact.visualLevel = artifact.GetLevel();
							buttonArtifact.levelReal = artifact.GetMythicalLevel() + 1;
							buttonArtifact.maxed = artifact.IsLegendaryPlusMaxRanked();
							buttonArtifact.canEvolve = false;
							buttonArtifact.isSelected = (i == num2);
							buttonArtifact.state = ButtonArtifact.State.FULL;
							buttonArtifact.gameObject.SetActive(true);
							buttonArtifact.gameButton.interactable = true;
							buttonArtifact.imagePin.gameObject.SetActive(false);
							if (i == num2 && artifact.IsLegendaryPlus())
							{
								MultiJumpResult mythicalArtifactEffectiveLevelJump = this.GetMythicalArtifactEffectiveLevelJump(artifact, upgradeSift);
								string doubleString = GameMath.GetDoubleString(mythicalArtifactEffectiveLevelJump.amountToConsume);
								bool flag7 = artifact.CanUpgrade(mythicalArtifactEffectiveLevelJump.jumpCount);
								if (flag7)
								{
									bool flag8 = this.sim.artifactsManager.CanAffordArtifactUpgrade(artifact, this.sim, mythicalArtifactEffectiveLevelJump.jumpCount);
									panelArtifactScroller.SetUpgradeButton(doubleString, flag7 && flag8);
								}
								else
								{
									panelArtifactScroller.SetUpgradeButton(doubleString, false);
								}
								panelArtifactScroller.mythicalArtifactLevelJump = mythicalArtifactEffectiveLevelJump.jumpCount;
								panelArtifactScroller.panelSelectedArtifact.buttonFiveX.text.text = PanelArtifactPopup.GetFiveXString(upgradeSift);
							}
						}
						else if (i < num)
						{
							buttonArtifact.state = ButtonArtifact.State.EMPTY;
							buttonArtifact.gameObject.SetActive(true);
							buttonArtifact.gameButton.interactable = false;
							buttonArtifact.imagePin.gameObject.SetActive(false);
						}
						else if (i == num)
						{
							buttonArtifact.state = ButtonArtifact.State.NONEXISTANT;
							buttonArtifact.gameObject.SetActive(false);
							buttonArtifact.gameButton.interactable = false;
							panelArtifactScroller.buttonUnlockSlot.gameObject.SetActive(!panelArtifactScroller.isLookingAtMythical && !flag5);
							panelArtifactScroller.buttonUnlockSlot.rectTransform.anchoredPosition = buttonArtifact.rectTransform.anchoredPosition;
							if (panelArtifactScroller.buttonUnlockSlot.buttonUpgrade.gameButton.interactable)
							{
								if (panelArtifactScroller.unlockButtonWaitingForConfirm)
								{
									panelArtifactScroller.buttonUnlockSlot.SetAsBuying();
								}
								else
								{
									panelArtifactScroller.buttonUnlockSlot.SetAsNormal();
								}
							}
							else
							{
								panelArtifactScroller.unlockButtonWaitingForConfirm = false;
							}
						}
						else
						{
							buttonArtifact.state = ButtonArtifact.State.NONEXISTANT;
							buttonArtifact.gameObject.SetActive(false);
						}
						buttonArtifact.SetDetails();
					}
				}
				if (selectedArtifactIndex >= 0 && selectedArtifactIndex < mythicalArtifacts.Count)
				{
					panelArtifactScroller.OnMythicalArtifactSelected(selectedArtifactIndex, mythicalArtifacts[selectedArtifactIndex], num);
				}
				panelArtifactScroller.panelSelectedArtifact.hasSizeChanged = false;
			}
			else
			{
				ArtifactsManager artifactsManager = this.sim.artifactsManager;
				bool flag9 = !artifactsManager.AreThereEmptyArtifactSlots() && !artifactsManager.AreThereSlotsAvailableToPurchase();
				bool flag10 = artifactsManager.AreThereSlotsAvailableToPurchase();
				if (flag9)
				{
					panelArtifactScroller.craftButton.gameObject.SetActive(false);
					panelArtifactScroller.textAllArtifactsCrafted.enabled = true;
				}
				else
				{
					panelArtifactScroller.textAllArtifactsCrafted.enabled = false;
					panelArtifactScroller.craftButton.gameObject.SetActive(true);
					bool canCraft2 = artifactsManager.CanCraftAnArtifact(this.sim);
					bool canAffordCraft2 = artifactsManager.CanAffordCraftingArtifact(this.sim);
					double craftCost = artifactsManager.GetCraftCost();
					string doubleString2 = GameMath.GetDoubleString(craftCost);
					panelArtifactScroller.SetCraftButton(doubleString2, canCraft2, canAffordCraft2);
				}
				if (UiManager.stateJustChanged)
				{
					panelArtifactScroller.SetArtifactsParentPosition(false);
					int num = artifactsManager.AvailableSlotsCount;
					panelArtifactScroller.CalculateContentSize(num);
					int count3 = panelArtifactScroller.buttonArtifacts.Count;
					ArtifactsManager artifactsManager2 = this.sim.artifactsManager;
					List<Simulation.ArtifactSystem.Artifact> artifacts = artifactsManager2.Artifacts;
					int count4 = artifacts.Count;
					panelArtifactScroller.artifactCount = count4;
					panelArtifactScroller.panelSelectedArtifact.parentCanvas.enabled = false;
					int selectedArtifactIndex2 = panelArtifactScroller.selectedArtifactIndex;
					if (selectedArtifactIndex2 != -1)
					{
						this.state = UiState.ARTIFACT_SELECTED_POPUP;
						return;
					}
					if (selectedArtifactIndex2 == -1)
					{
						panelArtifactScroller.SetSelected(selectedArtifactIndex2, 0f);
					}
					for (int j = 0; j < count3; j++)
					{
						ButtonArtifact buttonArtifact2 = panelArtifactScroller.buttonArtifacts[j];
						if (!panelArtifactScroller.isLookingAtMythical && (j != panelArtifactScroller.popAnimationIndex || !panelArtifactScroller.isAnimatingPop) && !panelArtifactScroller.dontUpdateStoneScale)
						{
							buttonArtifact2.artifactStone.rectTransform.SetScale(1f);
						}
						if (j < count4)
						{
							Simulation.ArtifactSystem.Artifact artifact2 = artifacts[j];
							buttonArtifact2.state = ButtonArtifact.State.FULL;
							buttonArtifact2.gameObject.SetActive(true);
							buttonArtifact2.gameButton.interactable = true;
							buttonArtifact2.imagePin.gameObject.SetActive(false);
							buttonArtifact2.visualLevel = artifact2.Rarity;
							buttonArtifact2.maxed = artifactsManager2.IsLevelMaxed(artifact2);
							buttonArtifact2.canEvolve = artifactsManager2.CanEvolve(artifact2, this.sim);
							buttonArtifact2.levelReal = artifact2.Level;
							buttonArtifact2.artifactStone.imageIcon.sprite = this.GetEffectSprite(artifact2.CommonEffectId);
							buttonArtifact2.artifactStone.imageIcon.SetNativeSize();
							Graphic imageIcon2 = buttonArtifact2.artifactStone.imageIcon;
							Color color = new Color(1f, 1f, 1f, 1f);
							buttonArtifact2.artifactStone.imageStone.color = color;
							imageIcon2.color = color;
						}
						else if (j < num)
						{
							buttonArtifact2.state = ButtonArtifact.State.EMPTY;
							buttonArtifact2.gameObject.SetActive(true);
							buttonArtifact2.gameButton.interactable = false;
							buttonArtifact2.imagePin.gameObject.SetActive(false);
						}
						else if (j == num)
						{
							buttonArtifact2.state = ButtonArtifact.State.NONEXISTANT;
							buttonArtifact2.gameObject.SetActive(false);
							buttonArtifact2.gameButton.interactable = false;
							panelArtifactScroller.buttonUnlockSlot.gameObject.SetActive(flag10);
							panelArtifactScroller.buttonUnlockSlot.rectTransform.anchoredPosition = buttonArtifact2.rectTransform.anchoredPosition;
							if (panelArtifactScroller.buttonUnlockSlot.buttonUpgrade.gameButton.interactable)
							{
								if (panelArtifactScroller.unlockButtonWaitingForConfirm)
								{
									panelArtifactScroller.buttonUnlockSlot.SetAsBuying();
								}
								else
								{
									panelArtifactScroller.buttonUnlockSlot.SetAsNormal();
								}
							}
							else
							{
								panelArtifactScroller.unlockButtonWaitingForConfirm = false;
							}
						}
						else
						{
							buttonArtifact2.state = ButtonArtifact.State.NONEXISTANT;
							buttonArtifact2.gameObject.SetActive(false);
						}
						buttonArtifact2.SetDetails();
					}
					if (!flag10)
					{
						panelArtifactScroller.buttonUnlockSlot.gameObject.SetActive(false);
					}
				}
			}
		}

		public MultiJumpResult GetMythicalArtifactEffectiveLevelJump(Simulation.Artifact artifact, UpgradeSift jumpType)
		{
			int a = GameMath.Clamp(artifact.GetAffordableLevelJumpCount(this.sim.GetMythstones().GetAmount()), 1, int.MaxValue);
			int b = GameMath.Clamp(artifact.GetLegendaryPlusMaxRank() - artifact.GetLegendaryPlusRank(), 1, int.MaxValue);
			int max = Mathf.Min(a, b);
			MultiJumpResult result = default(MultiJumpResult);
			double num = 0.0;
			int num2;
			switch (jumpType)
			{
			case UpgradeSift.One:
				num2 = 1;
				break;
			case UpgradeSift.Five:
				num2 = GameMath.Clamp(5, 1, max);
				break;
			case UpgradeSift.TenPerCent:
				num = this.sim.GetMythstones().GetAmount() * 0.1;
				num2 = GameMath.Clamp(artifact.GetAffordableLevelJumpCount(num), 0, max);
				break;
			case UpgradeSift.TwentyFivePerCent:
				num = this.sim.GetMythstones().GetAmount() * 0.25;
				num2 = GameMath.Clamp(artifact.GetAffordableLevelJumpCount(num), 0, max);
				break;
			case UpgradeSift.Max:
				num2 = GameMath.Clamp(artifact.GetAffordableLevelJumpCount(this.sim.GetMythstones().GetAmount()), 1, max);
				break;
			default:
				throw new Exception("Invalid upgrade shift type: " + jumpType);
			}
			result.jumpCount = num2;
			if (num2 == 0)
			{
				result.amountToConsume = num;
			}
			else
			{
				result.amountToConsume = artifact.GetUpgradeCost(num2);
			}
			return result;
		}

		private void SetTrinketSmitherParent(int index)
		{
			if (index == 1)
			{
				if (this.panelTrinketSmithing.rectTransform.parent == this.panelTrinketSelect.trinketSmitherParent)
				{
					return;
				}
				this.panelTrinketSmithing.rectTransform.SetParent(this.panelTrinketSelect.trinketSmitherParent);
				this.panelTrinketSmithing.rectTransform.SetTopDelta(100f);
				this.panelTrinketSmithing.rectTransform.SetBottomDelta(10f);
				this.panelTrinketSmithing.rectTransform.SetSizeDeltaX(770f);
			}
			else
			{
				if (this.panelTrinketSmithing.rectTransform.parent == this.panelHubTrinkets.smithingTabParent)
				{
					return;
				}
				this.panelTrinketSmithing.rectTransform.SetParent(this.panelHubTrinkets.smithingTabParent);
				this.panelTrinketSmithing.rectTransform.SetTopDelta(192f);
				this.panelTrinketSmithing.rectTransform.SetBottomDelta(0f);
				this.panelTrinketSmithing.rectTransform.SetSizeDeltaX(815f);
			}
		}

		private void SetTrinketScrollParent(int index)
		{
			if (index == 1)
			{
				if (this.panelTrinketsScroller.rectTransform.parent == this.gameplayTrinketsTabParent)
				{
					return;
				}
				this.panelTrinketsScroller.rectTransform.SetParent(this.gameplayTrinketsTabParent);
				this.panelTrinketsScroller.rectTransform.SetTopDelta(0f);
				this.panelTrinketsScroller.rectTransform.SetBottomDelta(10f);
				this.panelTrinketsScroller.rectTransform.SetLeftDelta(0f);
				this.panelTrinketsScroller.rectTransform.SetRightDelta(0f);
			}
			else if (index == 2)
			{
				if (this.panelTrinketsScroller.rectTransform.parent == this.hubTrinketsTabParent)
				{
					return;
				}
				this.panelTrinketsScroller.rectTransform.SetParent(this.hubTrinketsTabParent);
				this.panelTrinketsScroller.rectTransform.SetTopDelta(0f);
				this.panelTrinketsScroller.rectTransform.SetBottomDelta(0f);
				this.panelTrinketsScroller.rectTransform.SetLeftDelta(0f);
				this.panelTrinketsScroller.rectTransform.SetRightDelta(0f);
			}
		}

		private void UpdateTrinketScroller()
		{
		}

		private void UpdateTrinketRecyclePopup()
		{
			bool isCollected = this.sim.GetUnlock(UnlockIds.TRINKET_DISASSEMBLE).isCollected;
			this.panelTrinketRecycle.trinketImage.sprite = ((!isCollected) ? this.panelTrinketRecycle.destroySprite : this.panelTrinketRecycle.disenchantSprite);
			double num = 0.0;
			double num2 = 0.0;
			double num3 = 0.0;
			if (this.panelTrinketRecycle.previousState == UiState.TRINKET_INFO_POPUP)
			{
				Trinket selectedTrinket = this.panelTrinketInfoPopup.trinketInfoBody.selectedTrinket;
				num = ((!isCollected) ? selectedTrinket.GetDestroyCostCredits() : selectedTrinket.GetDisassembleCostCredits());
				num2 = ((!isCollected) ? selectedTrinket.GetDestroyCostTokens() : selectedTrinket.GetDisassembleCostTokens());
				num3 = selectedTrinket.GetDestroyReward();
				this.panelTrinketRecycle.desc.text = LM.Get((!isCollected) ? "TRINKET_DESTROY_DESC0" : "TRINKET_DISASSEMBLE_DESC");
			}
			else
			{
				foreach (Trinket trinket in this.panelTrinketsScroller.multipleSelectedTrinkets)
				{
					num += ((!isCollected) ? trinket.GetDestroyCostCredits() : trinket.GetDisassembleCostCredits());
					num2 += ((!isCollected) ? trinket.GetDestroyCostTokens() : trinket.GetDisassembleCostTokens());
					num3 += trinket.GetDestroyReward();
				}
				this.panelTrinketRecycle.desc.text = string.Format(LM.Get("MULTIPLE_TRINKET_DISASSEMBLE_POPUP_DESC"), this.panelTrinketsScroller.multipleSelectedTrinkets.Count);
			}
			this.panelTrinketRecycle.buttonGems.textUp.text = GameMath.GetDoubleString(num);
			this.panelTrinketRecycle.buttonTokens.textUp.text = GameMath.GetDoubleString(num2);
			this.panelTrinketRecycle.rewardTextGems.text = "+" + GameMath.GetDoubleString(num3);
			this.panelTrinketRecycle.rewardTextTokens.text = "+" + GameMath.GetDoubleString(num3);
			this.panelTrinketRecycle.buttonGems.gameButton.interactable = this.sim.GetCurrency(CurrencyType.GEM).CanAfford(num);
			this.panelTrinketRecycle.buttonTokens.gameButton.interactable = this.sim.GetCurrency(CurrencyType.TOKEN).CanAfford(num2);
			this.panelTrinketRecycle.buttonGems.textDown.text = LM.Get((!isCollected) ? "UI_DESTROY" : "TRINKET_DISASSEMBLE");
			this.panelTrinketRecycle.buttonTokens.textDown.text = LM.Get((!isCollected) ? "UI_DESTROY" : "TRINKET_DISASSEMBLE");
			this.panelTrinketRecycle.header.text = LM.Get((!isCollected) ? "UI_DESTROY" : "TRINKET_DISASSEMBLE");
			this.panelTrinketRecycle.gems.SetCurrency(CurrencyType.GEM, this.sim.GetCredits().GetString(), true, GameMode.STANDARD, true);
			this.panelTrinketRecycle.tokens.SetCurrency(CurrencyType.TOKEN, this.sim.GetTokens().GetString(), true, GameMode.STANDARD, true);
			this.panelTrinketRecycle.scraps.SetCurrency(CurrencyType.SCRAP, this.sim.GetScraps().GetString(), true, GameMode.STANDARD, true);
		}

		private void UpdateShopScreen()
		{
			PanelShop panelShop = this.panelShop;
			bool isLookingAtOffers = panelShop.isLookingAtOffers;
			panelShop.buttonTabOffers.interactable = !isLookingAtOffers;
			panelShop.buttonTabVault.interactable = isLookingAtOffers;
			float num = -40f;
			if (!isLookingAtOffers)
			{
				panelShop.parentFlashOffers.gameObject.SetActive(false);
				panelShop.parentSocialOffers.gameObject.SetActive(false);
				panelShop.parentSpecialOffer.gameObject.SetActive(false);
				panelShop.parentLootpacks.gameObject.SetActive(true);
				panelShop.parentLootpacks.anchoredPosition = new Vector2(0f, num);
				int i = 0;
				int num2 = panelShop.shopLootPacks.Length;
				while (i < num2)
				{
					ShopPack shopPack = this.sim.lootpacks[i];
					MerchantItem merchantItem = panelShop.shopLootPacks[i];
					if (UiManager.stateJustChanged)
					{
						Text textPrice = merchantItem.textPrice;
						this.SetTextToCost(textPrice, shopPack.cost);
					}
					if (i == 0)
					{
						int numFreeLootpacks = this.sim.GetNumFreeLootpacks();
						double timeForNextFreeLootpack = this.sim.GetTimeForNextFreeLootpack();
						string text = (numFreeLootpacks <= 0) ? string.Empty : LM.Get("UI_SHOP_CHEST_0");
						string text2 = string.Empty;
						if (timeForNextFreeLootpack >= 0.0)
						{
							text2 = GameMath.GetTimeString(timeForNextFreeLootpack);
						}
						else if (timeForNextFreeLootpack == -2.0)
						{
							text2 = LM.Get("UI_SHOP_CHEST_0_WAIT");
						}
						merchantItem.textPrice.text = text + ((text.Length <= 0 || text2.Length <= 0) ? string.Empty : "\n") + text2;
					}
					merchantItem.buttonUpgradeAnim.textCantAffordColorChangeManual = (merchantItem.buttonUpgradeAnim.isLevelUp = !this.sim.CanOpenLootpack(shopPack));
					i++;
				}
				num -= panelShop.parentLootpacks.sizeDelta.y;
				bool flag = this.sim.numTrinketSlots > 0;
				bool flag2 = this.sim.MineScrapUnlocked();
				bool flag3 = this.sim.MineTokenUnlocked();
				bool flag4 = this.sim.MineAnyUnlocked();
				if (flag || flag4)
				{
					ShopPackTrinket shopPackTrinket = this.sim.shopPackTrinket as ShopPackTrinket;
					bool flag5 = this.sim.CanOpenTrinketPack(shopPackTrinket, true) || this.sim.CanOpenTrinketPack(shopPackTrinket, false);
					panelShop.panelTrinket.buttonUpgradeAnim.isLevelUp = !flag5;
					panelShop.trinketStockNotification.numNotifications = this.sim.numUnseenTrinketPacks;
					if (this.sim.numTrinketPacks > 0)
					{
						panelShop.panelTrinket.buttonUpgradeAnim.textDownCantAffordColorChangeForced = false;
					}
					else
					{
						panelShop.panelTrinket.buttonUpgradeAnim.textDownCantAffordColorChangeForced = (!this.sim.CanCurrencyAfford(shopPackTrinket.specialCurrency, shopPackTrinket.specialCost) && !this.sim.CanCurrencyAfford(shopPackTrinket.currency, shopPackTrinket.cost));
						if (this.sim.CanCurrencyAfford(shopPackTrinket.specialCurrency, shopPackTrinket.specialCost) && this.sim.CanCurrencyAfford(shopPackTrinket.currency, shopPackTrinket.cost))
						{
							panelShop.panelTrinket.buttonUpgradeAnim.spriteNormal = this.uiData.shopGreenButton;
							panelShop.panelTrinket.buttonUpgradeAnim.spriteUpgrade = this.uiData.shopGreenButton;
							panelShop.panelTrinket.buttonUpgradeAnim.iconDownType = ButtonUpgradeAnim.IconType.AEONS;
							panelShop.panelTrinket.buttonUpgradeAnim.textDown.text = GameMath.GetDoubleString(shopPackTrinket.specialCost);
						}
						else if (this.sim.CanCurrencyAfford(shopPackTrinket.specialCurrency, shopPackTrinket.specialCost))
						{
							panelShop.panelTrinket.buttonUpgradeAnim.spriteNormal = this.uiData.shopGreenButton;
							panelShop.panelTrinket.buttonUpgradeAnim.spriteUpgrade = this.uiData.shopGreenButton;
							panelShop.panelTrinket.buttonUpgradeAnim.iconDownType = ButtonUpgradeAnim.IconType.AEONS;
							panelShop.panelTrinket.buttonUpgradeAnim.textDown.text = GameMath.GetDoubleString(shopPackTrinket.specialCost);
						}
						else if (this.sim.CanCurrencyAfford(shopPackTrinket.currency, shopPackTrinket.cost))
						{
							panelShop.panelTrinket.buttonUpgradeAnim.spriteNormal = this.uiData.shopBlueButton;
							panelShop.panelTrinket.buttonUpgradeAnim.spriteUpgrade = this.uiData.shopBlueButton;
							panelShop.panelTrinket.buttonUpgradeAnim.iconDownType = ButtonUpgradeAnim.IconType.CREDITS;
							panelShop.panelTrinket.buttonUpgradeAnim.textDown.text = GameMath.GetDoubleString(shopPackTrinket.cost);
						}
						else
						{
							panelShop.panelTrinket.buttonUpgradeAnim.spriteNormal = this.uiData.shopBlueButton;
							panelShop.panelTrinket.buttonUpgradeAnim.spriteUpgrade = this.uiData.shopBlueButton;
							panelShop.panelTrinket.buttonUpgradeAnim.iconDownType = ButtonUpgradeAnim.IconType.CREDITS;
							panelShop.panelTrinket.buttonUpgradeAnim.textDown.text = GameMath.GetDoubleString(shopPackTrinket.cost);
						}
					}
					panelShop.parentTrinketPack.anchoredPosition = new Vector2(0f, num);
					num -= panelShop.parentTrinketPack.sizeDelta.y;
				}
				if (UiManager.stateJustChanged)
				{
					if (flag)
					{
						panelShop.parentTrinketPack.gameObject.SetActive(true);
						if (flag && this.sim.numTrinketPacks > 0)
						{
							panelShop.panelTrinket.buttonUpgradeAnim.textDown.text = string.Format(LM.Get("UI_MERCHANT_ITEM_HOWMANY"), this.sim.numTrinketPacks.ToString());
							panelShop.panelTrinket.buttonUpgradeAnim.iconDownType = ButtonUpgradeAnim.IconType.NONE;
							panelShop.panelTrinket.buttonUpgradeAnim.spriteNormal = this.uiData.shopGreenButton;
							panelShop.panelTrinket.buttonUpgradeAnim.spriteUpgrade = this.uiData.shopGreenButton;
						}
						if (this.sim.IsRiftShopUnlocked())
						{
							panelShop.shopCharmPacks[0].gameObject.SetActive(true);
							if (this.sim.numSmallCharmPacks > 0)
							{
								panelShop.shopCharmPacks[0].buttonUpgradeAnim.textDownCantAffordColorChangeForced = false;
								panelShop.shopCharmPacks[0].buttonUpgradeAnim.textDown.text = string.Format(LM.Get("UI_MERCHANT_ITEM_HOWMANY"), this.sim.numSmallCharmPacks);
								panelShop.shopCharmPacks[0].buttonUpgradeAnim.iconDownType = ButtonUpgradeAnim.IconType.NONE;
								panelShop.shopCharmPacks[0].buttonUpgradeAnim.isLevelUp = false;
							}
							else
							{
								panelShop.shopCharmPacks[0].buttonUpgradeAnim.textDownCantAffordColorChangeForced = !this.sim.CanCurrencyAfford(CurrencyType.AEON, this.sim.shopPackSmallCharm.cost);
								panelShop.shopCharmPacks[0].buttonUpgradeAnim.isLevelUp = !this.sim.CanCurrencyAfford(CurrencyType.AEON, this.sim.shopPackSmallCharm.cost);
								panelShop.shopCharmPacks[0].buttonUpgradeAnim.textDown.text = GameMath.GetDoubleString(this.sim.shopPackSmallCharm.cost);
								panelShop.shopCharmPacks[0].buttonUpgradeAnim.iconDownType = ButtonUpgradeAnim.IconType.AEONS;
							}
						}
						else
						{
							panelShop.shopCharmPacks[0].gameObject.SetActive(false);
						}
						if (this.sim.GetWorld(GameMode.RIFT).GetLatestUnlockedRiftChallengeIndex() >= 50)
						{
							panelShop.shopCharmPacks[1].gameObject.SetActive(true);
							panelShop.shopCharmPacks[1].buttonUpgradeAnim.textDownCantAffordColorChangeForced = !this.sim.CanCurrencyAfford(CurrencyType.AEON, this.sim.shopPackBigCharm.cost);
							panelShop.shopCharmPacks[1].buttonUpgradeAnim.isLevelUp = !this.sim.CanCurrencyAfford(CurrencyType.AEON, this.sim.shopPackBigCharm.cost);
							panelShop.shopCharmPacks[1].buttonUpgradeAnim.textDown.text = GameMath.GetDoubleString(this.sim.shopPackBigCharm.cost);
							panelShop.shopCharmPacks[1].buttonUpgradeAnim.iconDownType = ButtonUpgradeAnim.IconType.AEONS;
						}
						else
						{
							panelShop.shopCharmPacks[1].gameObject.SetActive(false);
						}
						if (flag)
						{
							panelShop.panelTrinket.gameObject.SetActive(true);
						}
						else
						{
							panelShop.panelTrinket.gameObject.SetActive(false);
						}
					}
					else
					{
						panelShop.parentTrinketPack.gameObject.SetActive(false);
					}
					if (flag3)
					{
						panelShop.panelMineToken.gameObject.SetActive(true);
						panelShop.rectTransformLocked[0].gameObject.SetActive(false);
					}
					else
					{
						panelShop.panelMineToken.gameObject.SetActive(false);
						panelShop.rectTransformLocked[0].gameObject.SetActive(true);
					}
					if (flag2)
					{
						panelShop.panelMineScrap.gameObject.SetActive(true);
						panelShop.rectTransformLocked[1].gameObject.SetActive(false);
					}
					else
					{
						panelShop.panelMineScrap.gameObject.SetActive(false);
						panelShop.rectTransformLocked[1].gameObject.SetActive(true);
					}
				}
				if (flag4)
				{
					panelShop.parentMines.gameObject.SetActive(true);
					if (flag2)
					{
						this.UpdateMineButton(panelShop.panelMineScrap, this.sim.mineScrap);
					}
					if (flag3)
					{
						this.UpdateMineButton(panelShop.panelMineToken, this.sim.mineToken);
					}
					panelShop.parentMines.anchoredPosition = new Vector2(0f, num);
					num -= panelShop.parentMines.sizeDelta.y;
				}
				else
				{
					panelShop.parentMines.gameObject.SetActive(false);
				}
				panelShop.parentGemPacks.gameObject.SetActive(true);
				panelShop.parentGemPacks.anchoredPosition = new Vector2(0f, num);
				num -= panelShop.parentGemPacks.sizeDelta.y;
				if (this.sim.CanWatchVideoForFreeCurrency(CurrencyType.GEM))
				{
					panelShop.panelFreeCredits.buttonUpgradeAnim.textDown.text = LM.Get("UI_SHOP_AD");
					panelShop.panelFreeCredits.gameButton.interactable = true;
				}
				else
				{
					panelShop.panelFreeCredits.buttonUpgradeAnim.textDown.text = LM.Get("UI_SHOP_AD_LATER");
					panelShop.panelFreeCredits.gameButton.interactable = false;
				}
				if (UiManager.stateJustChanged)
				{
					panelShop.panelFreeCredits.buttonUpgradeAnim.textUp.text = PlayfabManager.titleData.freeCreditsAmount.ToString() + " " + LM.Get("UI_GEMS");
					for (int j = 0; j < panelShop.panelBuyCredits.Length; j++)
					{
						panelShop.panelBuyCredits[j].buttonUpgradeAnim.textDown.text = IapManager.productPriceStringsLocal[j + 1];
					}
				}
			}
			else
			{
				if (panelShop.timeWasReady != TrustedTime.IsReady() || panelShop.thereWasTutorial != TutorialManager.IsThereTutorialCurrently())
				{
					UiManager.stateJustChanged = true;
				}
				panelShop.timeWasReady = TrustedTime.IsReady();
				panelShop.thereWasTutorial = TutorialManager.IsThereTutorialCurrently();
				int aliveSpecialOfferCount = this.sim.specialOfferBoard.GetAliveSpecialOfferCount();
				if (aliveSpecialOfferCount != panelShop.specialOfferWidgets.Count)
				{
					Utility.FillUiElementList<SpecialOfferWidget>(panelShop.specialOfferWidgetPrefab, panelShop.parentSpecialOffer, aliveSpecialOfferCount, panelShop.specialOfferWidgets);
					UiManager.stateJustChanged = true;
				}
				if (((aliveSpecialOfferCount > 0 && TrustedTime.IsReady()) || this.sim.IsSecondAnniversaryEventEnabled()) && (!panelShop.thereWasTutorial || panelShop.forceUpdatePackOffer))
				{
					float num3 = 300f;
					panelShop.parentSpecialOffer.gameObject.SetActive(true);
					panelShop.parentSpecialOffer.anchoredPosition = new Vector2(0f, num);
					if (this.sim.IsSecondAnniversaryEventEnabled())
					{
						panelShop.secondAnniversaryOfferParent.gameObject.SetActive(true);
						panelShop.secondAnniversaryOfferNotif.enabled = this.sim.IsThereNotificationsPendingFromSecondAnniversaryOffer();
						panelShop.secondAnniversaryOfferParent.SetAnchorPosY(-250f);
						num3 += 305f;
						num -= 305f;
					}
					else
					{
						panelShop.secondAnniversaryOfferParent.gameObject.SetActive(false);
					}
					if (aliveSpecialOfferCount > 0 && TrustedTime.IsReady())
					{
						DateTime currentTime = TrustedTime.Get();
						for (int k = 0; k < aliveSpecialOfferCount; k++)
						{
							SpecialOfferWidget specialOfferWidget = panelShop.specialOfferWidgets[k];
							specialOfferWidget.gameObject.SetActive(true);
							SpecialOfferKeeper specialOfferKeeper = this.sim.specialOfferBoard[aliveSpecialOfferCount - 1 - k];
							if (UiManager.stateJustChanged)
							{
								ShopPackVisual shopPackVisual = this.panelShop.GetShopPackVisual(specialOfferKeeper.offerPack);
								if (specialOfferWidget.packVisual == null)
								{
									specialOfferWidget.AddPackVisual(shopPackVisual);
									specialOfferWidget.onClick = new Action<int>(this.OnClickedShopPackSelect);
								}
								else if (shopPackVisual.id != specialOfferWidget.packVisual.id)
								{
									specialOfferWidget.packVisual.DestoySelf();
									specialOfferWidget.AddPackVisual(shopPackVisual);
									specialOfferWidget.onClick = new Action<int>(this.OnClickedShopPackSelect);
								}
								specialOfferWidget.rectTransform.anchoredPosition = new Vector2(0f, -num3 - (float)k * 420f);
								specialOfferWidget.header.text = specialOfferKeeper.offerPack.GetName();
								specialOfferWidget.offerIndex = aliveSpecialOfferCount - 1 - k;
								this.panelShop.SetSpecialOfferVisualType(specialOfferWidget, specialOfferKeeper.offerPack.HasTag(ShopPack.Tags.RIFT), specialOfferKeeper.offerPack.HasTag(ShopPack.Tags.REGIONAL), specialOfferKeeper.offerPack.HasTag(ShopPack.Tags.SEASONAL), specialOfferKeeper.offerPack is ShopPackStarter);
								if (specialOfferKeeper.offerPack.isIAP)
								{
									specialOfferWidget.price.SetCurrency(CurrencyType.GEM, specialOfferKeeper.offerPack.GetPriceString(), false, GameMode.RIFT, false);
									specialOfferWidget.price.SetTextX(0f);
								}
								else
								{
									specialOfferWidget.price.SetCurrency(specialOfferKeeper.offerPack.currency, specialOfferKeeper.offerPack.GetPriceString(), false, GameMode.STANDARD, true);
									specialOfferWidget.price.SetTextX(27.55f);
								}
								specialOfferWidget.discountBase.SetActive(specialOfferKeeper.offerPack.IsOfferFromOtherIap);
								if (specialOfferKeeper.offerPack.IsOfferFromOtherIap)
								{
									specialOfferWidget.originalPrice.text = specialOfferKeeper.offerPack.OriginalLocalizedPrice;
								}
							}
							if (panelShop.setSpecialOfferContentTexts || UiManager.stateJustChanged)
							{
								specialOfferWidget.SetContentText(specialOfferKeeper.offerPack, this.sim.GetUniversalBonusAll());
								panelShop.setSpecialOfferContentTexts = false;
							}
							TimeSpan remainingDur = specialOfferKeeper.GetRemainingDur(currentTime);
							specialOfferWidget.timer.text = string.Format(LM.Get("SHOP_PACK_TIME"), GameMath.GetTimeDatailedShortenedString(remainingDur));
							specialOfferWidget.timer.rectTransform.sizeDelta = ((!specialOfferKeeper.offerPack.IsOfferFromOtherIap) ? SpecialOfferWidget.NormalTimerSize : SpecialOfferWidget.DiscountTimerSize);
						}
					}
					else
					{
						for (int l = panelShop.specialOfferWidgets.Count - 1; l >= 0; l--)
						{
							SpecialOfferWidget specialOfferWidget2 = panelShop.specialOfferWidgets[l];
							specialOfferWidget2.gameObject.SetActive(false);
						}
					}
					num -= (float)aliveSpecialOfferCount * 420f + 100f;
				}
				else
				{
					panelShop.parentSpecialOffer.gameObject.SetActive(false);
				}
				bool flag6 = TutorialManager.AreFlashOffersUnlocked() || this.sim.halloweenEnabled;
				panelShop.parentFlashOffers.gameObject.SetActive(flag6);
				panelShop.parentLootpacks.gameObject.SetActive(false);
				panelShop.parentGemPacks.gameObject.SetActive(false);
				panelShop.parentTrinketPack.gameObject.SetActive(false);
				panelShop.parentMines.gameObject.SetActive(false);
				panelShop.parentSocialOffers.gameObject.SetActive(true);
				bool flag7 = flag6 && this.sim.IsRiftShopUnlocked();
				if (flag6)
				{
					FlashOfferBundle flashOfferBundle = this.sim.flashOfferBundle;
					ServerSideFlashOfferBundle halloweenFlashOfferBundle = this.sim.halloweenFlashOfferBundle;
					float num4 = 0f;
					panelShop.parentFlashOffers.anchoredPosition = new Vector2(0f, num);
					if (TrustedTime.IsReady() && this.sim.halloweenEnabled)
					{
						num4 -= 200f;
						num -= 200f;
						DateTime dateTime = TrustedTime.Get();
						DateTime endDateParsed = PlayfabManager.halloweenOfferConfig.endDateParsed;
						bool flag8 = dateTime >= PlayfabManager.halloweenOfferConfig.startDateParsed && dateTime < endDateParsed;
						TimeSpan time = endDateParsed - dateTime;
						panelShop.halloweenOfferTimeCounter.text = LM.Get("SHOP_PACK_TIME", GameMath.GetTimeDatailedShortenedString(time));
						if (PlayfabManager.halloweenOfferConfigLoaded && flag8 && halloweenFlashOfferBundle != null)
						{
							panelShop.parentHalloweenFlashOffers.gameObject.SetActive(true);
							panelShop.parentHalloweenFlashOffers.anchoredPosition = new Vector2(0f, num4);
							num -= 610f;
							num4 -= 610f;
							int m = 0;
							int count = halloweenFlashOfferBundle.offers.Count;
							while (m < count)
							{
								FlashOffer flashOffer = halloweenFlashOfferBundle.offers[m];
								FlashOfferWidget flashOfferWidget = this.panelShop.halloweenFlashOfferPacks[m];
								flashOfferWidget.SetOffer(flashOffer, this, this.sim.GetFlashOfferCurrencyType(flashOffer), this.sim.GetFlashOfferCost(flashOffer), this.sim.GetFlashOfferCount(flashOffer), this.sim.GetFlashOfferPurchasesAllowedCount(flashOffer), this.sim.IsFlashOfferLocked(flashOffer), this.sim.GetFlashOfferUnlockDate(flashOffer));
								m++;
							}
						}
						else
						{
							panelShop.parentHalloweenFlashOffers.gameObject.SetActive(false);
						}
					}
					else
					{
						panelShop.parentHalloweenFlashOffers.gameObject.SetActive(false);
					}
					num -= 130f;
					num4 -= 130f;
					panelShop.timerTrayParent.anchoredPosition = new Vector2(0f, num4);
					if (flashOfferBundle != null)
					{
						panelShop.parentAdventureFlashOffers.gameObject.SetActive(true);
						if (TrustedTime.IsReady())
						{
							double remainingDur2 = flashOfferBundle.GetRemainingDur();
							panelShop.flashOfferEndsIn.text = LM.Get("SHOP_FLASH_OFFER_TIMER", GameMath.GetTimeDatailedShortenedString(TimeSpan.FromSeconds(remainingDur2)));
						}
						else
						{
							panelShop.flashOfferEndsIn.text = LM.Get("UI_SHOP_CHEST_0_WAIT");
						}
						if (TutorialManager.AreFlashOffersUnlocked() && flashOfferBundle.adventureOffers != null)
						{
							num4 -= 100f;
							num -= 100f;
							panelShop.parentAdventureFlashOffers.anchoredPosition = new Vector2(0f, num4);
							num4 -= 400f;
							num -= 400f;
							panelShop.parentAdventureFlashOffers.gameObject.SetActive(true);
							for (int n = 0; n < 3; n++)
							{
								FlashOffer flashOffer2 = flashOfferBundle.adventureOffers[n];
								FlashOfferWidget flashOfferWidget2 = this.panelShop.adventureFlashOfferPacks[n];
								flashOfferWidget2.SetOffer(flashOffer2, this, this.sim.GetFlashOfferCurrencyType(flashOffer2), this.sim.GetFlashOfferCost(flashOffer2), this.sim.GetFlashOfferCount(flashOffer2), this.sim.GetFlashOfferPurchasesAllowedCount(flashOffer2), this.sim.IsFlashOfferLocked(flashOffer2), this.sim.GetFlashOfferUnlockDate(flashOffer2));
							}
						}
						else
						{
							panelShop.parentAdventureFlashOffers.gameObject.SetActive(false);
						}
						if (flag7 && flashOfferBundle.offers != null)
						{
							panelShop.parentRiftPack.gameObject.SetActive(true);
							panelShop.parentRiftPack.anchoredPosition = new Vector2(0f, num4);
							num4 -= 400f;
							num -= 400f;
							panelShop.parentRiftPack.gameObject.SetActive(true);
							for (int num5 = 0; num5 < 3; num5++)
							{
								FlashOffer flashOffer3 = flashOfferBundle.offers[num5];
								FlashOfferWidget flashOfferWidget3 = this.panelShop.flashOfferPacks[num5];
								flashOfferWidget3.SetOffer(flashOffer3, this, this.sim.GetFlashOfferCurrencyType(flashOffer3), this.sim.GetFlashOfferCost(flashOffer3), this.sim.GetFlashOfferCount(flashOffer3), this.sim.GetFlashOfferPurchasesAllowedCount(flashOffer3), this.sim.IsFlashOfferLocked(flashOffer3), this.sim.GetFlashOfferUnlockDate(flashOffer3));
							}
						}
						else
						{
							panelShop.parentRiftPack.gameObject.SetActive(false);
						}
					}
					else
					{
						panelShop.parentAdventureFlashOffers.gameObject.SetActive(false);
						panelShop.parentRiftPack.gameObject.SetActive(false);
					}
				}
				panelShop.parentSocialOffers.anchoredPosition = new Vector2(0f, num);
				num -= panelShop.parentSocialOffers.sizeDelta.y;
				string text3 = 50.0 + " " + LM.Get("UI_GEMS");
				for (int num6 = 0; num6 < panelShop.socialRewardOffers.Length; num6++)
				{
					SocialRewards.Network network = Manager.NetworkList[num6];
					bool flag9 = Manager.IsOfferAvailable(network);
					MerchantItem merchantItem2 = panelShop.socialRewardOffers[num6];
					string platformKey = this.GetPlatformKey(network);
					merchantItem2.textTitle.text = ((!flag9) ? this.GetSocialNetworkName(network) : text3);
					merchantItem2.iconCoin.gameObject.SetActive(flag9);
					merchantItem2.buttonUpgradeAnim.textDown.text = LM.Get((!flag9) ? "SOCIAL_REWARD_VISIT" : platformKey);
				}
			}
			panelShop.wasLookingAtOffers = panelShop.isLookingAtOffers;
			if (panelShop.previousYPos == 0f || panelShop.previousYPos != num)
			{
				panelShop.childrenContentSizeFitter.SetSize(0f, false);
				if (panelShop.focusOnFlashOffers && panelShop.isLookingAtOffers)
				{
					int num7 = 0;
					int aliveSpecialOfferCount2 = this.sim.specialOfferBoard.GetAliveSpecialOfferCount();
					if (aliveSpecialOfferCount2 > 0)
					{
						num7 += 100 + aliveSpecialOfferCount2 * 420;
					}
					if (this.sim.halloweenEnabled && this.sim.halloweenFlashOfferBundle != null)
					{
						num7 += 810;
					}
					if (this.sim.IsSecondAnniversaryEventEnabled() && this.sim.secondAnniversaryFlashOffersBundle != null)
					{
						num7 += 480;
					}
					panelShop.scrollView.content.SetAnchorPosY((float)num7);
					panelShop.focusOnFlashOffers = false;
				}
			}
			if (panelShop.scrollSizeKeeper != panelShop.scrollView.content.sizeDelta.y)
			{
				panelShop.scrollSizeKeeper = panelShop.scrollView.content.sizeDelta.y;
				if (panelShop.IsHoldingAScrollPosition())
				{
					panelShop.scrollView.verticalNormalizedPosition = panelShop.scrollPositionKeeper;
					panelShop.scrollPositionTimer = 10f;
				}
				else
				{
					panelShop.scrollPositionTimer = 0f;
				}
			}
			if (!panelShop.IsHoldingAScrollPosition())
			{
				panelShop.scrollPositionKeeper = panelShop.scrollView.verticalNormalizedPosition;
			}
			panelShop.scrollPositionTimer += Time.deltaTime;
			int num8;
			if (TutorialManager.AreFlashOffersUnlocked() && this.sim.flashOfferBundle != null && !this.sim.flashOfferBundle.hasSeen)
			{
				num8 = 1;
			}
			else
			{
				num8 = 0;
			}
			if (this.sim.IsThereNotificationsPendingFromSecondAnniversaryOffer())
			{
				num8++;
			}
			panelShop.offersNotificationBadge.numNotifications = num8;
			int aliveSpecialOfferCount3 = this.sim.specialOfferBoard.GetAliveSpecialOfferCount();
			if (aliveSpecialOfferCount3 > 0 && TrustedTime.IsReady() && (!panelShop.thereWasTutorial || panelShop.forceUpdatePackOffer))
			{
				if (!panelShop.goblinIcon.gameObject.activeSelf || panelShop.forceGoblinAnimation)
				{
					panelShop.buttonTabOffers.text.rectTransform.SetSizeDeltaX(180f);
					panelShop.goblinIcon.gameObject.SetActive(true);
					panelShop.goblinIcon.SetScale(1f, 0f);
					DOTween.Sequence().AppendInterval(0.2f).Append(panelShop.goblinIcon.DOScaleY(1f, 0.15f).SetEase(Ease.OutBack, 3.5f, 0f)).Insert(0.25f, panelShop.goblinIcon.DOScaleX(0.9f, 0.03f)).Insert(0.28f, panelShop.goblinIcon.DOScaleX(1f, 0.1f)).Play<Sequence>();
				}
			}
			else
			{
				panelShop.buttonTabOffers.text.rectTransform.SetSizeDeltaX(250f);
				panelShop.goblinIcon.gameObject.SetActive(false);
			}
			panelShop.forceGoblinAnimation = false;
		}

		private string GetPlatformKey(SocialRewards.Network network)
		{
			switch (network)
			{
			case SocialRewards.Network.Facebook:
				return "SOCIAL_REWARDS_LIKE_US";
			case SocialRewards.Network.Twitter:
				return "SOCIAL_REWARDS_FOLLOW_US";
			case SocialRewards.Network.Instagram:
				return "SOCIAL_REWARDS_FOLLOW_US";
			default:
				return "SOCIAL_REWARDS_FOLLOW_US";
			}
		}

		private void UpdateFlashOfferCharmPopup()
		{
			FlashOffer flashOffer = this.panelBuyCharmFlashOffer.flashOffer;
			if (this.panelBuyCharmFlashOffer.state != 2)
			{
				CharmEffectData charmEffectData = this.sim.allCharmEffects[flashOffer.charmId];
				CharmOptionCard charmOptionCard = this.panelBuyCharmFlashOffer.charmOptionCard;
				this.panelBuyCharmFlashOffer.cardFront.sprite = this.GetCharmCardFlashOffer(charmEffectData.BaseData.charmType);
				this.panelBuyCharmFlashOffer.icon.sprite = this.spritesCharmEffectIcon[flashOffer.charmId];
				this.panelBuyCharmFlashOffer.amount.text = "x" + this.sim.GetFlashOfferCount(flashOffer);
				this.panelBuyCharmFlashOffer.nameText.text = charmEffectData.GetName();
				this.UpdateCharmOptionCard(charmEffectData, charmOptionCard, false);
			}
			if (!this.panelBuyCharmFlashOffer.isAnimatingBuyButton)
			{
				if (flashOffer.purchasesLeft == 0)
				{
					this.panelBuyCharmFlashOffer.purchasedText.gameObject.SetActive(true);
					this.panelBuyCharmFlashOffer.buttonBuy.gameButton.gameObject.SetActive(false);
				}
				else
				{
					this.panelBuyCharmFlashOffer.purchasedText.gameObject.SetActive(false);
					this.panelBuyCharmFlashOffer.buttonBuy.gameButton.gameObject.SetActive(true);
					CurrencyType? flashOfferCurrencyType = this.sim.GetFlashOfferCurrencyType(flashOffer);
					this.panelBuyCharmFlashOffer.buttonBuy.iconUpType = ((flashOfferCurrencyType == null) ? ButtonUpgradeAnim.IconType.NONE : ButtonUpgradeAnim.GetIconTypeFromCurrency(flashOfferCurrencyType.Value));
					double flashOfferCost = this.sim.GetFlashOfferCost(flashOffer);
					this.panelBuyCharmFlashOffer.buttonBuy.textUp.text = ((flashOfferCost <= 0.0) ? LM.Get("UI_SHOP_CHEST_0") : GameMath.GetDoubleString(flashOfferCost));
					this.panelBuyCharmFlashOffer.buttonBuy.gameButton.interactable = this.sim.CanAffordFlashOffer(flashOffer);
				}
			}
		}

		private void UpdateAdventureFlashOfferPopup()
		{
			FlashOffer flashOffer = this.panelBuyAdventureFlashOffer.flashOffer;
			this.panelBuyAdventureFlashOffer.nameText.text = this.GetFlashOfferName(flashOffer, false);
			bool flag = this.sim.CanAffordFlashOffer(flashOffer);
			if (!this.panelBuyAdventureFlashOffer.isAnimatingBuyButton)
			{
				if (flashOffer.purchasesLeft == 0)
				{
					this.panelBuyAdventureFlashOffer.purchasedText.gameObject.SetActive(true);
					this.panelBuyAdventureFlashOffer.buttonBuy.gameButton.gameObject.SetActive(false);
					this.panelBuyAdventureFlashOffer.buttonBuy.rectTransform.SetAnchorPosX(0f);
					this.panelBuyAdventureFlashOffer.buttonGoToShop.gameObject.SetActive(false);
					this.panelBuyAdventureFlashOffer.buttonGoToShopLocked.gameObject.SetActive(false);
					this.panelBuyAdventureFlashOffer.offerLockedMessage.enabled = false;
					this.panelBuyAdventureFlashOffer.canNotAffordMessage.enabled = false;
				}
				else
				{
					this.panelBuyAdventureFlashOffer.purchasedText.gameObject.SetActive(false);
					this.panelBuyAdventureFlashOffer.buttonBuy.gameButton.gameObject.SetActive(true);
					if (this.panelBuyAdventureFlashOffer.flashOfferUnlocked)
					{
						double flashOfferCost = this.sim.GetFlashOfferCost(flashOffer);
						CurrencyType? flashOfferCurrencyType = this.sim.GetFlashOfferCurrencyType(flashOffer);
						this.panelBuyAdventureFlashOffer.buttonBuy.iconUpType = ((flashOfferCurrencyType == null) ? ButtonUpgradeAnim.IconType.NONE : ButtonUpgradeAnim.GetIconTypeFromCurrency(flashOfferCurrencyType.Value));
						this.panelBuyAdventureFlashOffer.buttonBuy.textUp.text = ((flashOfferCost <= 0.0) ? LM.Get("UI_SHOP_CHEST_0") : GameMath.GetDoubleString(flashOfferCost));
						this.panelBuyAdventureFlashOffer.buttonBuy.textDown.text = ((flashOfferCost <= 0.0) ? LM.Get((flashOffer.costType != FlashOffer.CostType.AD) ? "UI_COLLECT" : "UI_AD_WATCH") : LM.Get("UI_BUY"));
						this.panelBuyAdventureFlashOffer.buttonBuy.gameButton.interactable = flag;
						this.panelBuyAdventureFlashOffer.offerLockedMessage.enabled = false;
						this.panelBuyAdventureFlashOffer.canNotAffordMessage.enabled = (!flag && this.panelBuyAdventureFlashOffer.canNotAffordMessageKey != null);
						if (!flag && this.panelBuyAdventureFlashOffer.canNotAffordMessageKey != null)
						{
							this.panelBuyAdventureFlashOffer.canNotAffordMessage.text = LM.Get(this.panelBuyAdventureFlashOffer.canNotAffordMessageKey);
						}
						this.panelBuyAdventureFlashOffer.buttonBuy.rectTransform.SetAnchorPosX((float)((!flag && this.panelBuyAdventureFlashOffer.buttonGoToShopClickedCallback != null) ? -168 : 0));
						this.panelBuyAdventureFlashOffer.buttonBuy.rectTransform.SetSizeDeltaX((float)((!flag && this.panelBuyAdventureFlashOffer.buttonGoToShopClickedCallback != null) ? 290 : 270));
						this.panelBuyAdventureFlashOffer.buttonGoToShop.gameObject.SetActive(!flag && this.panelBuyAdventureFlashOffer.buttonGoToShopClickedCallback != null && TutorialManager.christmasTreeEventUnlocked >= TutorialManager.ChristmasTreeEventUnlocked.OPEN_SHOP_TAB);
						this.panelBuyAdventureFlashOffer.buttonGoToShopLocked.gameObject.SetActive(!flag && this.panelBuyAdventureFlashOffer.buttonGoToShopClickedCallback != null && TutorialManager.christmasTreeEventUnlocked < TutorialManager.ChristmasTreeEventUnlocked.OPEN_SHOP_TAB);
					}
					else
					{
						this.panelBuyAdventureFlashOffer.buttonBuy.iconUpType = ButtonUpgradeAnim.IconType.NONE;
						this.panelBuyAdventureFlashOffer.buttonBuy.textUp.text = LM.Get("UI_LOCKED");
						this.panelBuyAdventureFlashOffer.buttonBuy.textDown.text = LM.Get("UI_BUY");
						this.panelBuyAdventureFlashOffer.buttonBuy.gameButton.interactable = false;
						this.panelBuyAdventureFlashOffer.offerLockedMessage.enabled = (flashOffer.type != FlashOffer.Type.COSTUME || this.sim.IsHeroUnlocked(this.sim.GetSkinData(flashOffer.genericIntId).belongsTo.id));
						this.panelBuyAdventureFlashOffer.canNotAffordMessage.enabled = false;
						this.panelBuyAdventureFlashOffer.buttonBuy.rectTransform.SetAnchorPosX(0f);
						this.panelBuyAdventureFlashOffer.buttonGoToShop.gameObject.SetActive(false);
						this.panelBuyAdventureFlashOffer.buttonGoToShopLocked.gameObject.SetActive(false);
					}
				}
			}
			bool flag2 = this.sim.GetFlashOfferPurchasesAllowedCount(flashOffer) > 1;
			bool flag3 = flag2 || flashOffer.type == FlashOffer.Type.COSTUME || flashOffer.type == FlashOffer.Type.RUNE || flashOffer.type == FlashOffer.Type.COSTUME_PLUS_SCRAP;
			bool flag4 = flashOffer.type == FlashOffer.Type.COSTUME_PLUS_SCRAP;
			bool flag5 = flashOffer.type == FlashOffer.Type.GEM || flashOffer.type == FlashOffer.Type.SCRAP || flashOffer.type == FlashOffer.Type.TOKEN || flashOffer.type == FlashOffer.Type.COSTUME_PLUS_SCRAP;
			bool flag6 = flashOffer.costType == FlashOffer.CostType.AD && (RewardedAdManager.inst == null || !RewardedAdManager.inst.IsRewardedVideoAvailable());
			float num = 158f;
			float num2;
			if (flashOffer.type == FlashOffer.Type.MERCHANT_ITEM)
			{
				num2 = this.panelBuyAdventureFlashOffer.panelHeightMerchantItem;
				if (flashOffer.purchasesLeft == 0 && !this.panelBuyAdventureFlashOffer.merchantItemNotification.enabled)
				{
					num2 -= 100f;
				}
				else if (this.panelBuyAdventureFlashOffer.flashOfferUnlocked && flag)
				{
					num2 -= 20f;
				}
				num = 218f;
			}
			else if (flag4)
			{
				num2 = this.panelBuyAdventureFlashOffer.panelHeightBigWithHalloween;
			}
			else if (flag3 && (flag6 || !this.panelBuyAdventureFlashOffer.flashOfferUnlocked || (flashOffer.purchasesLeft > 0 && !flag && this.panelBuyAdventureFlashOffer.canNotAffordMessageKey != null)))
			{
				num2 = this.panelBuyAdventureFlashOffer.panelHeightBigWithAdWarning;
			}
			else if (flag3 || flag6 || !this.panelBuyAdventureFlashOffer.flashOfferUnlocked)
			{
				num2 = this.panelBuyAdventureFlashOffer.panelHeaightBig;
			}
			else if (flashOffer.purchasesLeft > 0 && !flag && this.panelBuyAdventureFlashOffer.canNotAffordMessageKey != null)
			{
				num2 = this.panelBuyAdventureFlashOffer.panelHeightBigWithAdWarning;
			}
			else
			{
				num2 = this.panelBuyAdventureFlashOffer.panelHeaightSmall;
			}
			this.panelBuyAdventureFlashOffer.plusText.gameObject.SetActive(flag4);
			if ((flag4 || flashOffer.type == FlashOffer.Type.COSTUME) && flashOffer.purchasesLeft > 0)
			{
				SkinData skinData = this.sim.GetSkinData(flashOffer.genericIntId);
				if (flashOffer.PurchaseRequiresCurrency(skinData.currency))
				{
					this.panelBuyAdventureFlashOffer.originalPriceParent.gameObject.SetActive(true);
					this.panelBuyAdventureFlashOffer.originalPriceText.text = GameMath.GetDoubleString(skinData.cost);
				}
				else
				{
					this.panelBuyAdventureFlashOffer.originalPriceParent.gameObject.SetActive(false);
				}
			}
			else
			{
				this.panelBuyAdventureFlashOffer.originalPriceParent.gameObject.SetActive(false);
			}
			if (this.panelBuyAdventureFlashOffer.popupParent.sizeDelta.y != num2)
			{
				this.panelBuyAdventureFlashOffer.popupParent.sizeDelta = new Vector2(this.panelBuyAdventureFlashOffer.popupParent.sizeDelta.x, num2);
			}
			if (this.panelBuyAdventureFlashOffer.popupParent.anchoredPosition.y != num)
			{
				this.panelBuyAdventureFlashOffer.popupParent.SetAnchorPosY(num);
			}
			this.panelBuyAdventureFlashOffer.costumeAnimation.gameObject.SetActive(flashOffer.type == FlashOffer.Type.COSTUME || flashOffer.type == FlashOffer.Type.COSTUME_PLUS_SCRAP);
			this.panelBuyAdventureFlashOffer.costumeNameParent.SetActive(flashOffer.type == FlashOffer.Type.COSTUME || flashOffer.type == FlashOffer.Type.COSTUME_PLUS_SCRAP);
			this.panelBuyAdventureFlashOffer.icon.enabled = (flashOffer.type != FlashOffer.Type.COSTUME && flashOffer.type != FlashOffer.Type.COSTUME_PLUS_SCRAP);
			this.panelBuyAdventureFlashOffer.currencyAmountParent.gameObject.SetActive(flag5 && !flag6);
			this.panelBuyAdventureFlashOffer.adWarning.enabled = flag6;
			this.panelBuyAdventureFlashOffer.iconBackground.enabled = (flashOffer.type == FlashOffer.Type.RUNE);
			this.panelBuyAdventureFlashOffer.offeredObjectName.enabled = (flashOffer.type == FlashOffer.Type.RUNE);
			this.panelBuyAdventureFlashOffer.merchantItemParent.SetActive(flashOffer.type == FlashOffer.Type.MERCHANT_ITEM);
			if (flashOffer.type == FlashOffer.Type.RUNE)
			{
				this.panelBuyAdventureFlashOffer.offeredObjectName.text = LM.Get(this.sim.GetRune(flashOffer.genericStringId).nameKey);
			}
			if (flashOffer.type == FlashOffer.Type.RUNE || flashOffer.type == FlashOffer.Type.TRINKET_PACK)
			{
				this.panelBuyAdventureFlashOffer.runeDescription.enabled = true;
				this.panelBuyAdventureFlashOffer.runeDescription.text = ((flashOffer.type != FlashOffer.Type.TRINKET_PACK) ? this.sim.GetRune(flashOffer.genericStringId).GetDesc() : LM.Get("UI_TRINKET_OPENING_DESC"));
			}
			else
			{
				this.panelBuyAdventureFlashOffer.runeDescription.enabled = false;
			}
			if (flashOffer.type == FlashOffer.Type.MERCHANT_ITEM)
			{
				string languageID_N = LM.GetLanguageID_N(LM.currentLanguage);
				CultureInfo culture = new CultureInfo(languageID_N);
				this.panelBuyAdventureFlashOffer.merchantItemDescription.text = this.sim.GetMerchantItemWithId(flashOffer.genericStringId).GetDescriptionString(this.sim).ToUpper(culture);
				this.panelBuyAdventureFlashOffer.merchantItemAmount.text = "x" + this.sim.GetFlashOfferCount(flashOffer).ToString();
				this.panelBuyAdventureFlashOffer.merchantItemNotification.enabled = (this.panelBuyAdventureFlashOffer.flashOfferUnlocked && flag);
			}
			this.panelBuyAdventureFlashOffer.stockParent.SetActive(flag2);
			if (flag2)
			{
				this.panelBuyAdventureFlashOffer.stock.text = string.Format(LM.Get("UI_MERCHANT_ITEM_HOWMANY"), flashOffer.purchasesLeft);
			}
			if (flashOffer.type != FlashOffer.Type.COSTUME && flashOffer.type != FlashOffer.Type.COSTUME_PLUS_SCRAP)
			{
				this.panelBuyAdventureFlashOffer.icon.sprite = this.GetFlashOfferImageBig(flashOffer);
				this.panelBuyAdventureFlashOffer.icon.color = this.GetFlashOfferImageColor(flashOffer);
				this.panelBuyAdventureFlashOffer.icon.rectTransform.anchoredPosition = ((!PanelBuyAdventureFlashOffer.IconSettingsPerOffer.ContainsKey(flashOffer.type)) ? PanelBuyAdventureFlashOffer.DefaultIconPosition : PanelBuyAdventureFlashOffer.IconSettingsPerOffer[flashOffer.type].IconPosition);
				this.panelBuyAdventureFlashOffer.icon.rectTransform.sizeDelta = ((!PanelBuyAdventureFlashOffer.IconSettingsPerOffer.ContainsKey(flashOffer.type)) ? PanelBuyAdventureFlashOffer.DefaultIconSize : PanelBuyAdventureFlashOffer.IconSettingsPerOffer[flashOffer.type].IconSize);
				this.panelBuyAdventureFlashOffer.iconBackground.enabled = (flashOffer.type == FlashOffer.Type.RUNE);
				if (this.panelBuyAdventureFlashOffer.iconBackground.enabled)
				{
					this.panelBuyAdventureFlashOffer.iconBackground.rectTransform.anchoredPosition = ((!PanelBuyAdventureFlashOffer.IconSettingsPerOffer.ContainsKey(flashOffer.type)) ? PanelBuyAdventureFlashOffer.DefaultBackgroundPosition : PanelBuyAdventureFlashOffer.IconSettingsPerOffer[flashOffer.type].BackgroundPosition);
					this.panelBuyAdventureFlashOffer.iconBackground.rectTransform.sizeDelta = ((!PanelBuyAdventureFlashOffer.IconSettingsPerOffer.ContainsKey(flashOffer.type)) ? PanelBuyAdventureFlashOffer.DefaultBackgroundSize : PanelBuyAdventureFlashOffer.IconSettingsPerOffer[flashOffer.type].BackgroundSize);
				}
				if (flashOffer.type == FlashOffer.Type.RUNE)
				{
					this.panelBuyAdventureFlashOffer.iconBackground.sprite = this.uiData.runeOfferBackgroundBig;
				}
			}
			else
			{
				SkinData skinData2 = this.sim.GetSkinData(flashOffer.genericIntId);
				bool flag7 = this.sim.IsThereAnySkinBought();
				bool flag8 = flag7 && this.sim.IsHeroUnlocked(skinData2.belongsTo.id);
				if (skinData2.belongsTo.id == "DRUID")
				{
					this.panelBuyAdventureFlashOffer.costumeAnimation.ShowTransformedVersion(this.sim.GetHeroSkins(skinData2.belongsTo.id).Count);
				}
				else
				{
					this.panelBuyAdventureFlashOffer.costumeAnimation.DontShowTransformedVersion();
				}
				this.panelBuyAdventureFlashOffer.costumeAnimation.SetHeroAnimation(skinData2.belongsTo.id, skinData2.index, false, false, true, true);
				this.panelBuyAdventureFlashOffer.costumeAnimation.SetSkeletonColor((!flag8) ? Color.black : Color.white);
				this.panelBuyAdventureFlashOffer.costumeName.text = skinData2.GetName();
				this.panelBuyAdventureFlashOffer.buttonBuy.gameButton.interactable = (this.panelBuyAdventureFlashOffer.flashOfferUnlocked && flag8 && flag);
				this.panelBuyAdventureFlashOffer.costumeNameParent.gameObject.SetActive(flag8);
				this.panelBuyAdventureFlashOffer.currencyAmountParent.gameObject.SetActive(flashOffer.type == FlashOffer.Type.COSTUME_PLUS_SCRAP && flag8);
				this.panelBuyAdventureFlashOffer.plusText.gameObject.SetActive(flashOffer.type == FlashOffer.Type.COSTUME_PLUS_SCRAP && flag8);
				this.panelBuyAdventureFlashOffer.runeDescription.enabled = !flag8;
				if (!flag8)
				{
					HeroUnlockDescKey heroUnlockDescKey;
					if (!flag7)
					{
						this.panelBuyAdventureFlashOffer.runeDescription.text = "SKIN_UNLOCK_REQ_EVOLVE_RE".LocFormat(AM.cs("UI_LEVEL_COMMON".Loc(), UiManager.colorHeroLevels[0]));
					}
					else if (this.heroUnlockHintKeys.TryGetValue(skinData2.belongsTo.id, out heroUnlockDescKey))
					{
						this.panelBuyAdventureFlashOffer.runeDescription.text = heroUnlockDescKey.GetString();
					}
					else
					{
						this.panelBuyAdventureFlashOffer.runeDescription.text = string.Empty;
					}
				}
			}
			if (flag5 && !flag6)
			{
				this.panelBuyAdventureFlashOffer.amount.text = GameMath.GetDoubleString((double)this.sim.GetFlashOfferCount(flashOffer));
				switch (flashOffer.type)
				{
				case FlashOffer.Type.SCRAP:
				case FlashOffer.Type.COSTUME_PLUS_SCRAP:
					this.panelBuyAdventureFlashOffer.currencyAmountIcon.sprite = this.uiData.currencySprites[1];
					goto IL_F45;
				case FlashOffer.Type.GEM:
					this.panelBuyAdventureFlashOffer.currencyAmountIcon.sprite = this.uiData.currencySprites[3];
					goto IL_F45;
				case FlashOffer.Type.TOKEN:
					this.panelBuyAdventureFlashOffer.currencyAmountIcon.sprite = this.uiData.currencySprites[4];
					goto IL_F45;
				}
				this.panelBuyAdventureFlashOffer.currencyAmountIcon.sprite = null;
			}
			IL_F45:
			MenuShowCurrency secondMenuShowCurrency = this.panelBuyAdventureFlashOffer.menuShowCurrency;
			if (flag2 || !this.panelBuyAdventureFlashOffer.flashOfferUnlocked)
			{
				this.panelBuyAdventureFlashOffer.currencyAmountParent.SetAnchorPosY(-15f);
			}
			else if (flashOffer.purchasesLeft > 0 && !flag && this.panelBuyAdventureFlashOffer.canNotAffordMessageKey != null)
			{
				this.panelBuyAdventureFlashOffer.currencyAmountParent.SetAnchorPosY(50f);
			}
			else
			{
				this.panelBuyAdventureFlashOffer.currencyAmountParent.SetAnchorPosY(-90f);
			}
			if (flag5)
			{
				secondMenuShowCurrency.gameObject.SetActive(true);
				switch (flashOffer.type)
				{
				case FlashOffer.Type.SCRAP:
				case FlashOffer.Type.COSTUME_PLUS_SCRAP:
					secondMenuShowCurrency.SetCurrency(CurrencyType.SCRAP, Main.instance.GetSim().GetCurrency(CurrencyType.SCRAP).GetString(), true, GameMode.STANDARD, true);
					break;
				case FlashOffer.Type.GEM:
					secondMenuShowCurrency.SetCurrency(CurrencyType.GEM, Main.instance.GetSim().GetCurrency(CurrencyType.GEM).GetString(), true, GameMode.STANDARD, true);
					break;
				case FlashOffer.Type.TOKEN:
					secondMenuShowCurrency.SetCurrency(CurrencyType.TOKEN, Main.instance.GetSim().GetCurrency(CurrencyType.TOKEN).GetString(), true, GameMode.STANDARD, true);
					break;
				}
				secondMenuShowCurrency = this.panelBuyAdventureFlashOffer.secondMenuShowCurrency;
			}
			if (flashOffer.costType == FlashOffer.CostType.GEM)
			{
				secondMenuShowCurrency.gameObject.SetActive(true);
				secondMenuShowCurrency.SetCurrency(CurrencyType.GEM, Main.instance.GetSim().GetCurrency(CurrencyType.GEM).GetString(), true, GameMode.STANDARD, true);
			}
			else
			{
				secondMenuShowCurrency.gameObject.SetActive(false);
			}
			if (secondMenuShowCurrency != this.panelBuyAdventureFlashOffer.secondMenuShowCurrency)
			{
				this.panelBuyAdventureFlashOffer.secondMenuShowCurrency.gameObject.SetActive(false);
			}
		}

		private void UpdateMineButton(MerchantItem panel, Mine mine)
		{
			panel.buttonUpgradeAnim.iconDownType = ButtonUpgradeAnim.IconType.NONE;
			Sprite sprite;
			if (this.sim.CanCollectMine(mine))
			{
				panel.buttonRef.sprite = this.uiData.spriteShopGreenButton;
				panel.buttonUpgradeAnim.textShadowEnableOnEnabled = true;
				panel.buttonUpgradeAnim.textCantAffordColorChangeManual = false;
				panel.buttonUpgradeAnim.textDown.text = LM.Get("UI_COLLECT");
				sprite = this.GetMineSpriteFull(mine);
			}
			else
			{
				sprite = this.GetMineSpriteEmpty(mine);
				panel.buttonRef.sprite = this.uiData.spriteShopBrownButton;
				panel.buttonUpgradeAnim.textShadowEnableOnEnabled = false;
				panel.buttonUpgradeAnim.textCantAffordColorChangeManual = true;
				if (TrustedTime.IsReady())
				{
					panel.buttonUpgradeAnim.textDown.text = GameMath.GetTimeString(this.sim.GetTimeToCollectMine(mine));
				}
				else
				{
					panel.buttonUpgradeAnim.textDown.text = LM.Get("UI_SHOP_CHEST_0_WAIT");
				}
			}
			if (panel.imageItem.sprite != sprite)
			{
				panel.imageItem.sprite = sprite;
				panel.imageItem.SetNativeSize();
			}
		}

		private void UpdateHeroesNewScreen()
		{
			if (UiManager.stateJustChanged)
			{
				HashSet<string> unlockedHeroIds = this.sim.GetUnlockedHeroIds();
				List<HeroDataBase> list = this.SortHeroesByUnlocked(this.sim.GetAllHeroes(), unlockedHeroIds);
				Dictionary<string, GameMode> boughtHeroIdsWithModes = this.sim.GetBoughtHeroIdsWithModes();
				HashSet<string> newHeroIconSelectedHeroIds = this.sim.newHeroIconSelectedHeroIds;
				GameMode gameMode = (this.state != UiState.HUB_MODE_SETUP_HERO) ? this.sim.GetActiveWorld().gameMode : this.panelHubModeSetup.mode;
				for (int i = this.panelNewHero.newHeroButtons.Count - 1; i >= 0; i--)
				{
					bool flag = i < list.Count;
					ButtonNewHeroSelect buttonNewHeroSelect = this.panelNewHero.newHeroButtons[i];
					if (flag)
					{
						HeroDataBase heroDataBase = list[i];
						string id = heroDataBase.id;
						bool flag2 = !boughtHeroIdsWithModes.ContainsKey(id) || (boughtHeroIdsWithModes[id] == gameMode && this.sim.GetWorld(gameMode).DoesActiveChallengeAllowRepeatedHeroes());
						bool flag3 = unlockedHeroIds.Contains(id);
						bool everSelected = newHeroIconSelectedHeroIds.Contains(id);
						int evolveLevel = list[i].evolveLevel;
						buttonNewHeroSelect.hero = heroDataBase;
						buttonNewHeroSelect.spriteHeroPortrait = this.GetSpriteHeroPortrait(id);
						GameMode mode = GameMode.STANDARD;
						if (this.state == UiState.HUB_MODE_SETUP_HERO)
						{
							int j = 0;
							int num = this.panelHubModeSetup.heroDatabases.Length;
							while (j < num)
							{
								if (this.panelHubModeSetup.heroDatabases[j] == heroDataBase && !this.sim.GetWorld(this.panelHubModeSetup.mode).DoesActiveChallengeAllowRepeatedHeroes())
								{
									flag2 = false;
									mode = this.panelHubModeSetup.mode;
									break;
								}
								j++;
							}
							if (boughtHeroIdsWithModes.ContainsKey(id))
							{
								mode = boughtHeroIdsWithModes[id];
							}
						}
						else if (this.state == UiState.HEROES_NEW)
						{
							mode = (flag2 ? GameMode.STANDARD : boughtHeroIdsWithModes[id]);
						}
						buttonNewHeroSelect.SetButtonState(flag, flag2, flag3, everSelected, evolveLevel, (!flag2) ? this.GetSpriteModeFlag(mode) : null);
						if (!flag3)
						{
							HeroUnlockDescKey heroUnlockDescKey = this.heroUnlockHintKeys[id];
							if (heroUnlockDescKey.isAmountHidden)
							{
								buttonNewHeroSelect.unlockLevelLabel.gameObject.SetActive(false);
							}
							else
							{
								buttonNewHeroSelect.unlockLevelLabel.gameObject.SetActive(true);
								buttonNewHeroSelect.unlockLevelLabel.text = heroUnlockDescKey.amount.ToString();
							}
						}
					}
					else
					{
						buttonNewHeroSelect.SetButtonState(flag, true, true, true, 0, null);
					}
				}
				int selected = this.panelNewHero.selected;
				if (selected >= 0)
				{
					HeroDataBase hero = this.panelNewHero.newHeroButtons[selected].hero;
					bool flag4 = unlockedHeroIds.Contains(hero.id);
					GameMode? heroGameModeBusy = null;
					bool flag5 = this.sim.GetWorld(gameMode).DoesActiveChallengeAllowRepeatedHeroes();
					this.panelNewHero.duplicatedHeroWidget.SetActive(flag5 && ((boughtHeroIdsWithModes.ContainsKey(hero.id) && boughtHeroIdsWithModes[hero.id] != this.panelHubModeSetup.mode) || (this.panelHubModeSetup.heroDatabases != null && this.panelHubModeSetup.heroDatabases.Contains(hero))));
					if (boughtHeroIdsWithModes.ContainsKey(hero.id) && (boughtHeroIdsWithModes[hero.id] != this.panelHubModeSetup.mode || !flag5))
					{
						heroGameModeBusy = new GameMode?(boughtHeroIdsWithModes[hero.id]);
					}
					else if (!flag5 && this.state == UiState.HUB_MODE_SETUP_HERO && this.panelHubModeSetup.heroDatabases != null && this.panelHubModeSetup.heroDatabases.Contains(hero))
					{
						heroGameModeBusy = new GameMode?(this.panelHubModeSetup.mode);
					}
					else
					{
						if (hero.randomSkinsEnabled && !this.panelNewHero.shownHeroes.ContainsKey(hero.id))
						{
							this.panelNewHero.heroTransitionAnimation.SetHeroAnimation(hero.id, this.sim.GetHeroBoughtSkins(hero.id).GetRandomListElement<SkinData>().index, false, 0);
						}
						else if (hero.randomSkinsEnabled)
						{
							this.panelNewHero.heroTransitionAnimation.SetHeroAnimation(hero.id, this.panelNewHero.shownHeroes[hero.id], false, 0);
						}
						else
						{
							this.panelNewHero.heroTransitionAnimation.SetHeroAnimation(hero.id, hero.equippedSkin.index, false, 0);
						}
						bool interactable = this.sim.CanAffordNewHero();
						this.panelNewHero.buttonNewHeroBuy.gameButton.interactable = interactable;
					}
					this.panelNewHero.SelectHero(hero, true, !flag4 || heroGameModeBusy != null, heroGameModeBusy);
				}
				else if (this.state == UiState.HUB_MODE_SETUP_HERO)
				{
					this.panelNewHero.SelectHero(null, this.sim.HasAvailableHero(), false, null);
				}
				else
				{
					this.panelNewHero.SelectHero(null, this.sim.CanGetNewHero(), false, null);
				}
				if (UiManager.stateJustChanged)
				{
					this.panelNewHero.buttonNewHeroBuy.textUp.text = GameMath.GetDoubleString(this.sim.GetNewHeroCost());
				}
				this.panelNewHero.SetSelected();
			}
		}

		private void UpdateHeroPanelOnSkillsAndGearScreen(Hero hero, PanelHero panelHero)
		{
			if (UiManager.stateJustChanged)
			{
				this.panelSkillsScreen.buttonHeroNext.gameObject.SetActive(this.sim.GetActiveWorld().heroes.Count > 1);
				panelHero.SetLevel(hero.GetLevel());
				this.UpdatePanelHero(hero.GetData().GetDataBase(), panelHero);
				if (this.sim.GetActiveWorld().heroes.Count > 1)
				{
					this.panelHeroGearSkills.panelHeroClass.rectTransform.SetAnchorPosX(-130f);
				}
				else
				{
					this.panelHeroGearSkills.panelHeroClass.rectTransform.SetAnchorPosX(-15f);
				}
			}
		}

		private void UpdatePanelHero(HeroDataBase heroBase, PanelHero panelHero)
		{
			if (UiManager.stateJustChanged)
			{
				if (panelHero.heroPortrait != null)
				{
					panelHero.heroPortrait.SetHero(this.GetSpriteHeroPortrait(heroBase.GetId()), heroBase.evolveLevel, false, -5f, false);
				}
				panelHero.SetName(LM.Get(heroBase.nameKey));
				panelHero.panelHeroClass.SetIcon(heroBase.heroClass);
			}
		}

		private void UpdateHeroesSkillScreen(Hero hero)
		{
			this.UpdateHeroPanelOnSkillsAndGearScreen(hero, this.panelHeroGearSkills);
			if (UiManager.stateJustChanged)
			{
				this.SetHeroTabBarPositions();
			}
			this.panelSkillsScreen.UpdatePanel(hero, this, this.sim);
		}

		private void SetHeroTabBarPositions()
		{
			bool flag = this.sim.hasEverOwnedATrinket || this.sim.HasTrinketTabHint();
			bool flag2 = !this.sim.hasEverOwnedATrinket && this.sim.HasTrinketTabHint();
			this.SetTabButtonPositions((!flag) ? 2 : 3, this.panelGearScreen.buttonTab, this.panelSkillsScreen.buttonTab, this.panelTrinketsScreen.buttonTab);
			this.panelTrinketsScreen.buttonTab.text.enabled = !flag2;
			this.panelTrinketsScreen.lockedTabIcon.gameObject.SetActive(flag2);
		}

		public void UpdateHeroesGearScreen(Hero hero, PanelGearScreen pgs)
		{
			this.UpdateHeroPanelOnSkillsAndGearScreen(hero, this.panelHeroGearSkills);
			if (UiManager.stateJustChanged)
			{
				this.SetHeroTabBarPositions();
			}
			HeroDataBase dataBase = hero.GetData().GetDataBase();
			this.UpdateHeroesGearScreen(dataBase, pgs);
		}

		public void UpdateHeroesGearScreen(HeroDataBase heroBase, PanelGearScreen pgs)
		{
			string id = heroBase.id;
			List<Gear> heroBoughtGears = this.sim.GetHeroBoughtGears(heroBase);
			List<GearData> heroGears = this.sim.GetHeroGears(heroBase);
			if (this.sim.IsThereAnySkinBought())
			{
				pgs.buttonSkin.gameObject.SetActive(true);
				pgs.skinsNotificationBadge.numNotifications = this.sim.GetHerosNewSkinCount(id) + ((!this.sim.isSkinsEverClicked) ? 1 : 0);
			}
			else
			{
				pgs.buttonSkin.gameObject.SetActive(false);
			}
			if (UiManager.stateJustChanged)
			{
				int evolveLevel = heroBase.evolveLevel;
				double heroEvolveCost = this.sim.GetHeroEvolveCost(heroBase);
				bool heroEvolvability = this.sim.GetHeroEvolvability(heroBase);
				bool heroEvolveMaxedOut = this.sim.GetHeroEvolveMaxedOut(heroBase);
				bool flag = this.sim.CanAffordHeroEvolve(heroBase);
				bool flag2 = this.sim.CanHeroEvolve(heroBase);
				if (heroEvolveMaxedOut)
				{
					pgs.textEvolveDesc.text = string.Empty;
				}
				else if (!flag2)
				{
					pgs.textEvolveDesc.text = string.Format(LM.Get("UI_GEAR_3_ITEMS"), string.Concat(new string[]
					{
						"<color=",
						GameMath.GetColorString(UiManager.colorHeroLevels[evolveLevel]),
						">",
						UiManager.GearLevelString(evolveLevel),
						"</color>"
					}));
				}
				else if (!flag)
				{
					pgs.textEvolveDesc.text = LM.Get("UI_GEAR_NO_SCRAPS");
				}
				else
				{
					pgs.textEvolveDesc.text = string.Format(LM.Get("UI_GEAR_EVOLVE"), string.Concat(new string[]
					{
						"<color=",
						GameMath.GetColorString(UiManager.colorHeroLevels[evolveLevel]),
						">",
						UiManager.GearLevelString(evolveLevel),
						"</color>"
					}));
				}
				pgs.buttonEvolve.textDown.text = LM.Get("UI_EVOLVE");
				pgs.buttonEvolve.textUp.text = GameMath.GetDoubleString(heroEvolveCost);
				pgs.SetDetails(id, evolveLevel, heroBase.equippedSkin.index);
				pgs.buttonEvolve.openWarningPopup = !heroEvolvability;
				if (heroEvolveMaxedOut)
				{
					pgs.buttonEvolve.gameObject.SetActive(false);
					pgs.imageMaxEvolve.gameObject.SetActive(true);
				}
				else
				{
					pgs.buttonEvolve.gameObject.SetActive(true);
					pgs.imageMaxEvolve.gameObject.SetActive(false);
					pgs.buttonEvolve.textUpCantAffordColorChangeForced = !flag;
					pgs.buttonEvolve.textDownCantAffordColorChangeForced = !flag2;
				}
				int i = 0;
				int count = heroGears.Count;
				while (i < count)
				{
					Gear gear = heroBoughtGears.Find((Gear g) => g.data == heroGears[i]);
					if (gear == null)
					{
						PanelGear panelGear = pgs.panelGears[i];
						panelGear.notLootedDescription.gameObject.SetActive(true);
						panelGear.infoParent.gameObject.SetActive(false);
						panelGear.notLootedDescription.text = LM.Get("UI_GEAR_NO_ITEM_DESC");
						panelGear.PlayTransitionAnimationIfNeeded(heroGears[i], 0, false, this.GetSpriteGearIcon(heroGears[i].GetId()));
					}
					else
					{
						bool upgradable = this.sim.CanUpgradeGear(gear);
						string doubleString = GameMath.GetDoubleString(this.sim.GetGearPrice(gear));
						pgs.panelGears[i].SetDetails(gear, upgradable, doubleString, this.GetSpriteGearIcon(gear.GetId()), this.sim.GetUniversalBonusAll());
					}
					i++;
				}
			}
		}

		private void UpdateModeUnlocksScreen()
		{
			this.panelUnlocks.UpdateUnlocks(this.sim, this);
			this.panelUnlocks.SetContentSize();
		}

		public Sprite GetRewardCategorySprite(UnlockReward.RewardCategory cat)
		{
			switch (cat)
			{
			case UnlockReward.RewardCategory.NEW_MECHANIC:
				return this.uiData.spriteLockedNewMechanics;
			case UnlockReward.RewardCategory.IMPORTANT_THING:
				return this.uiData.spriteLockedNewImportantThings;
			case UnlockReward.RewardCategory.HERO:
				return this.uiData.spriteLockedNewHeroes;
			case UnlockReward.RewardCategory.CURRENCY:
				return this.uiData.spriteLockedCurrency;
			default:
				throw new Exception();
			}
		}

		private void UpdateArtifactsCraftScreen()
		{
			if (this.panelArtifactsCraft.updateDetails && !this.panelArtifactsCraft.skipArtifactShow)
			{
				this.panelArtifactsCraft.updateDetails = false;
				if (this.panelArtifactsCraft.isMythical)
				{
					List<Simulation.Artifact> mythicalArtifacts = this.sim.artifactsManager.MythicalArtifacts;
					Simulation.Artifact artifact = mythicalArtifacts[mythicalArtifacts.Count - 1];
					Sprite effectTypeSprite = this.GetEffectTypeSprite(artifact.GetCategoryType(), artifact.effects[0].GetType());
					this.panelArtifactsCraft.craftedArtifactWidget.artifactStone.imageIcon.sprite = effectTypeSprite;
					this.panelArtifactsCraft.craftedArtifactWidget.artifactStone.imageIcon.SetNativeSize();
					this.panelArtifactsCraft.craftedArtifactRarity = artifact.GetLevel();
					this.panelArtifactsCraft.craftedArtifactWidget.artifactStone.PlaySpineAnim(this.panelArtifactsCraft.craftedArtifactRarity);
					this.panelArtifactsCraft.craftedArtifactWidget.SetDetails(artifact);
				}
				else
				{
					Simulation.ArtifactSystem.Artifact artifact2 = this.sim.artifactsManager.Artifacts[this.sim.artifactsManager.Artifacts.Count - 1];
					Sprite effectSprite = this.GetEffectSprite(artifact2.CommonEffectId);
					this.panelArtifactsCraft.craftedArtifactWidget.artifactStone.imageIcon.sprite = effectSprite;
					this.panelArtifactsCraft.craftedArtifactWidget.artifactStone.imageIcon.SetNativeSize();
					this.panelArtifactsCraft.craftedArtifactWidget.artifactStone.PlaySpineAnim(artifact2.Rarity);
					this.panelArtifactsCraft.craftedArtifactRarity = artifact2.Rarity;
					this.panelArtifactsCraft.craftedArtifactWidget.SetDetails(artifact2, this.sim.GetUniversalBonusAll());
				}
			}
			if (this.panelArtifactsCraft.state == PanelArtifactsCraft.State.FadeOut && !this.panelArtifactsCraft.skipArtifactShow)
			{
				this.panelArtifactsCraft.stoneBoxArtifact.PlaySpineAnim(this.panelArtifactsCraft.craftedArtifactRarity);
			}
			if (this.panelArtifactsCraft.state == PanelArtifactsCraft.State.End)
			{
				UiManager.zeroScrollViewContentY = false;
				this.panelArtifactScroller.StartQpAnimation(this.sim.artifactsManager.TotalArtifactsLevel);
				this.state = this.panelArtifactsCraft.uistateToReturn;
				if (this.panelArtifactsCraft.skipArtifactShow)
				{
					this.panelArtifactScroller.PlayArtifactsAppearAnimAfterConversion();
				}
			}
		}

		public ButtonArtifact GetSelectedArtifactButton()
		{
			return this.panelArtifactScroller.GetSelectedArtifactButton();
		}

		private void UpdateArtifactsInfoScreen()
		{
			if (UiManager.stateJustChanged)
			{
				this.panelArtifactsInfo.SetPlacement(this.sim);
				this.panelArtifactsInfo.SetLayoutDirty();
			}
		}

		private void UpdateHeroSkinPopup()
		{
			if (this.panelHeroSkinChanger.updateDetails)
			{
				this.panelHeroSkinChanger.updateDetails = false;
				if (this.panelHeroSkinChanger.selectedHero != null)
				{
					HeroDataBase selectedHero = this.panelHeroSkinChanger.selectedHero;
					List<SkinData> heroSkins = this.sim.GetHeroSkins(selectedHero.id);
					heroSkins.Sort(new Comparison<SkinData>(this.SkinDataComparer));
					int count = heroSkins.Count;
					this.panelHeroSkinChanger.SetScrollContentSize(count);
					this.panelHeroSkinChanger.currency.gameObject.SetActive(false);
					for (int i = 0; i < this.panelHeroSkinChanger.skinInvButtons.Count; i++)
					{
						SkinInventoryButton skinInventoryButton = this.panelHeroSkinChanger.skinInvButtons[i];
						if (i == 0)
						{
							skinInventoryButton.gameObject.SetActive(true);
							skinInventoryButton.lockIcon.gameObject.SetActive(false);
							skinInventoryButton.skinIcon.gameObject.SetActive(true);
							skinInventoryButton.skinIcon.sprite = this.uiData.skinIconRandom;
							skinInventoryButton.priceTagParent.gameObject.SetActive(false);
							skinInventoryButton.newBadge.SetActive(false);
							skinInventoryButton.equippedCheckmark.gameObject.SetActive(this.panelHeroSkinChanger.selectedHero.randomSkinsEnabled);
							skinInventoryButton.frame.sprite = this.uiData.skinFrameActive;
							if (this.panelHeroSkinChanger.selectedButtonIndex == 0)
							{
								UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiRingSelect, 1f));
								this.panelHeroSkinChanger.selectedSkin = null;
								this.panelHeroSkinChanger.heroAnimation.gameObject.SetActive(false);
								this.panelHeroSkinChanger.skinDescription.gameObject.SetActive(false);
								this.panelHeroSkinChanger.randomSkinImage.enabled = true;
								this.panelHeroSkinChanger.randomSkinMessage.enabled = true;
								this.panelHeroSkinChanger.skinNameParent.gameObject.SetActive(true);
								this.panelHeroSkinChanger.equipButton.textDownCantAffordColorChangeForced = false;
								this.panelHeroSkinChanger.halloweenExtraPumpkin.gameObject.SetActive(false);
								this.panelHeroSkinChanger.christmasIcon.SetActive(false);
								this.panelHeroSkinChanger.skinName.text = string.Format(LM.Get("RANDOM_SKIN_NAME"), LM.Get(this.panelHeroSkinChanger.selectedHero.nameKey));
								this.panelHeroSkinChanger.gotToShopButton.gameObject.SetActive(false);
								this.panelHeroSkinChanger.currencySecondary.gameObject.SetActive(false);
								this.panelHeroSkinChanger.halloweenExtra.gameObject.SetActive(false);
								this.panelHeroSkinChanger.equipButton.gameObject.SetActive(false);
								this.panelHeroSkinChanger.flashOfferContentParent.SetActive(false);
								if (this.panelHeroSkinChanger.selectedHero.randomSkinsEnabled)
								{
									this.panelHeroSkinChanger.soloEquipButton.gameObject.SetActive(false);
									skinInventoryButton.equippedCheckmark.gameObject.SetActive(true);
								}
								else
								{
									this.panelHeroSkinChanger.soloEquipButton.gameObject.SetActive(true);
									skinInventoryButton.equippedCheckmark.gameObject.SetActive(false);
									this.panelHeroSkinChanger.soloEquipButton.onClick = new GameButton.VoidFunc(this.EquipRandomSkin);
									this.panelHeroSkinChanger.soloEquipButton.text.text = LM.Get("UI_EQUIP_SKIN");
								}
							}
						}
						else if (i - 1 < count)
						{
							int index = i - 1;
							skinInventoryButton.gameObject.SetActive(true);
							SkinData skinData = heroSkins[index];
							bool flag = this.sim.GetBoughtSkins().Contains(skinData);
							bool flag2 = selectedHero.equippedSkin == skinData;
							bool flag3 = skinData.IsUnlockRequirementSatisfied(this.sim) || flag;
							bool isNew = skinData.isNew;
							Sprite heroAvatar = this.GetHeroAvatar(selectedHero.id, skinData.index);
							skinInventoryButton.lockIcon.gameObject.SetActive(!flag3);
							skinInventoryButton.skinIcon.gameObject.SetActive(flag3);
							skinInventoryButton.skinIcon.sprite = heroAvatar;
							skinInventoryButton.priceTagParent.gameObject.SetActive(!flag && skinData.unlockType == SkinData.UnlockType.CURRENCY);
							if (!flag && skinData.unlockType == SkinData.UnlockType.CURRENCY)
							{
								skinInventoryButton.priceTagIcon.sprite = UiData.inst.currencySprites[(!skinData.isChristmasSkin || !this.sim.IsChristmasTreeEnabled()) ? 3 : 6];
							}
							skinInventoryButton.newBadge.SetActive(flag3 && isNew);
							skinInventoryButton.equippedCheckmark.gameObject.SetActive(flag2);
							skinInventoryButton.frame.sprite = ((!flag3) ? this.uiData.skinFramePassive : this.uiData.skinFrameActive);
							bool flag4 = false;
							if (i == this.panelHeroSkinChanger.selectedButtonIndex)
							{
								UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiRingSelect, 1f));
								bool flag5 = false;
								this.panelHeroSkinChanger.randomSkinImage.enabled = false;
								this.panelHeroSkinChanger.randomSkinMessage.enabled = false;
								this.panelHeroSkinChanger.selectedSkin = skinData;
								this.panelHeroSkinChanger.heroAnimation.gameObject.SetActive(true);
								if (selectedHero.id == "DRUID")
								{
									this.panelHeroSkinChanger.heroAnimation.ShowTransformedVersion(count);
								}
								else
								{
									this.panelHeroSkinChanger.heroAnimation.DontShowTransformedVersion();
								}
								this.panelHeroSkinChanger.heroAnimation.SetSkeletonColor((!flag3) ? Color.black : Color.white);
								this.panelHeroSkinChanger.heroAnimation.SetHeroAnimation(selectedHero.id, skinData.index, false, false, false, true);
								this.panelHeroSkinChanger.skinDescription.gameObject.SetActive(!flag3);
								this.panelHeroSkinChanger.equipButton.textDownCantAffordColorChangeForced = false;
								if (flag3)
								{
									this.panelHeroSkinChanger.equipButton.gameObject.SetActive(true);
									if (isNew)
									{
										UiCommandSkinSelected command = new UiCommandSkinSelected
										{
											skinId = skinData.id
										};
										this.command = command;
										skinInventoryButton.newBadge.SetActive(false);
									}
									this.panelHeroSkinChanger.skinNameParent.gameObject.SetActive(true);
									this.panelHeroSkinChanger.halloweenExtraPumpkin.gameObject.SetActive(skinData.isHalloweenSkin);
									this.panelHeroSkinChanger.christmasIcon.SetActive(skinData.isChristmasSkin);
									this.panelHeroSkinChanger.skinName.text = skinData.GetName();
									if (!flag)
									{
										FlashOffer skinOfferIfExist = this.sim.GetSkinOfferIfExist(skinData);
										flag5 = (skinOfferIfExist != null);
										if (flag5 && skinOfferIfExist.isHalloween)
										{
											flag4 = true;
											this.panelHeroSkinChanger.currencySecondary.gameObject.SetActive(true);
											this.panelHeroSkinChanger.halloweenExtra.gameObject.SetActive(true);
											this.panelHeroSkinChanger.currencySecondary.SetCurrency(CurrencyType.SCRAP, this.sim.GetCurrency(CurrencyType.SCRAP).GetString(), true, GameMode.STANDARD, true);
										}
										if (skinData.isChristmasSkin && this.sim.IsChristmasTreeEnabled())
										{
											this.panelHeroSkinChanger.currency.gameObject.SetActive(false);
											this.panelHeroSkinChanger.equipButton.gameObject.SetActive(false);
											this.panelHeroSkinChanger.soloEquipButton.gameObject.SetActive(false);
											this.panelHeroSkinChanger.gotToShopButton.gameObject.SetActive(true);
										}
										else if (skinData.unlockType == SkinData.UnlockType.CURRENCY)
										{
											this.panelHeroSkinChanger.equipButton.gameObject.SetActive(true);
											this.panelHeroSkinChanger.soloEquipButton.gameObject.SetActive(false);
											this.panelHeroSkinChanger.gotToShopButton.gameObject.SetActive(false);
											this.panelHeroSkinChanger.currency.gameObject.SetActive(true);
											this.panelHeroSkinChanger.currency.SetCurrency(skinData.currency, this.sim.GetCurrency(skinData.currency).GetString(), true, GameMode.STANDARD, true);
											double cost = (!flag5) ? skinData.cost : this.sim.GetFlashOfferCost(skinOfferIfExist);
											if (this.sim.CanCurrencyAfford(skinData.currency, cost))
											{
												this.panelHeroSkinChanger.equipButton.gameButton.interactable = true;
											}
											else
											{
												this.panelHeroSkinChanger.equipButton.gameButton.interactable = false;
												this.panelHeroSkinChanger.equipButton.textCantAffordColorChange = true;
											}
											this.panelHeroSkinChanger.equipButton.iconUpType = ButtonUpgradeAnim.GetIconTypeFromCurrency(skinData.currency);
											this.panelHeroSkinChanger.equipButton.textUp.text = GameMath.GetDoubleString((!flag5) ? skinData.cost : this.sim.GetFlashOfferCost(skinOfferIfExist));
											this.panelHeroSkinChanger.equipButton.textDown.text = LM.Get("UI_BUY");
											this.panelHeroSkinChanger.equipButton.gameButton.onClick = new GameButton.VoidFunc(this.InitiateBuySelectedSkin);
											if (flag5)
											{
												this.panelHeroSkinChanger.originalPriceIcon.sprite = this.uiData.currencySprites[(int)skinData.currency];
												this.panelHeroSkinChanger.originalPriceAmount.text = GameMath.GetDoubleString(skinData.cost);
											}
										}
										else
										{
											this.panelHeroSkinChanger.equipButton.gameObject.SetActive(true);
											this.panelHeroSkinChanger.soloEquipButton.gameObject.SetActive(false);
											this.panelHeroSkinChanger.gotToShopButton.gameObject.SetActive(false);
										}
									}
									else
									{
										this.panelHeroSkinChanger.gotToShopButton.gameObject.SetActive(false);
										if (this.panelHeroSkinChanger.dontAnimateHero)
										{
											this.panelHeroSkinChanger.dontAnimateHero = false;
										}
										this.panelHeroSkinChanger.equipButton.gameObject.SetActive(false);
										this.panelHeroSkinChanger.soloEquipButton.onClick = new GameButton.VoidFunc(this.EquipSelectedSkin);
										if (!flag2 || this.panelHeroSkinChanger.selectedHero.randomSkinsEnabled)
										{
											skinInventoryButton.equippedCheckmark.gameObject.SetActive(false);
											this.panelHeroSkinChanger.soloEquipButton.gameObject.SetActive(true);
											this.panelHeroSkinChanger.soloEquipButton.text.text = LM.Get("UI_EQUIP_SKIN");
										}
										else
										{
											skinInventoryButton.equippedCheckmark.gameObject.SetActive(true);
											this.panelHeroSkinChanger.soloEquipButton.gameObject.SetActive(false);
										}
									}
								}
								else
								{
									this.panelHeroSkinChanger.equipButton.gameButton.interactable = false;
									this.panelHeroSkinChanger.skinNameParent.gameObject.SetActive(false);
									this.panelHeroSkinChanger.halloweenExtraPumpkin.gameObject.SetActive(false);
									this.panelHeroSkinChanger.gotToShopButton.gameObject.SetActive(false);
									this.panelHeroSkinChanger.christmasIcon.SetActive(false);
									if (skinData.unlockType == SkinData.UnlockType.CURRENCY)
									{
										this.panelHeroSkinChanger.soloEquipButton.gameObject.SetActive(false);
										this.panelHeroSkinChanger.currency.gameObject.SetActive(true);
										this.panelHeroSkinChanger.equipButton.gameObject.SetActive(true);
										bool flag6 = this.sim.CanCurrencyAfford(skinData.currency, skinData.cost);
										this.panelHeroSkinChanger.equipButton.textCantAffordColorChange = !flag6;
										this.panelHeroSkinChanger.equipButton.textDownCantAffordColorChangeForced = true;
										this.panelHeroSkinChanger.currency.SetCurrency(skinData.currency, this.sim.GetCurrency(skinData.currency).GetString(), true, GameMode.STANDARD, true);
										this.panelHeroSkinChanger.equipButton.iconUpType = ButtonUpgradeAnim.GetIconTypeFromCurrency(skinData.currency);
										this.panelHeroSkinChanger.equipButton.textUp.text = GameMath.GetDoubleString(skinData.cost);
										this.panelHeroSkinChanger.equipButton.textDown.text = LM.Get("UI_BUY");
										this.panelHeroSkinChanger.equipButton.gameButton.onClick = new GameButton.VoidFunc(this.InitiateBuySelectedSkin);
									}
									else
									{
										this.panelHeroSkinChanger.equipButton.gameObject.SetActive(false);
										this.panelHeroSkinChanger.soloEquipButton.gameObject.SetActive(false);
									}
									this.panelHeroSkinChanger.skinDescription.text = skinData.GetUnlockHint();
								}
								if (!flag4)
								{
									this.panelHeroSkinChanger.currencySecondary.gameObject.SetActive(false);
									this.panelHeroSkinChanger.halloweenExtra.gameObject.SetActive(false);
								}
								this.panelHeroSkinChanger.flashOfferContentParent.SetActive(flag5);
							}
						}
						else
						{
							skinInventoryButton.gameObject.SetActive(false);
						}
					}
				}
			}
		}

		private int SkinDataComparer(SkinData y, SkinData x)
		{
			bool flag = x.IsUnlockRequirementSatisfied(this.sim);
			bool value = y.IsUnlockRequirementSatisfied(this.sim);
			int num = flag.CompareTo(value);
			if (num == 0)
			{
				return x.CompareTo(y);
			}
			return num;
		}

		private void EquipSelectedSkin()
		{
			if (this.panelHeroSkinChanger.selectedSkin != null && this.panelHeroSkinChanger.selectedHero != null)
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiWorldUpgrade, 1f));
				UiCommandChangeHeroSkin command = new UiCommandChangeHeroSkin
				{
					heroId = this.panelHeroSkinChanger.selectedHero.id,
					skinId = this.panelHeroSkinChanger.selectedSkin.id,
					random = false
				};
				this.command = command;
				this.state = this.panelHeroSkinChanger.oldState;
			}
		}

		private void EquipRandomSkin()
		{
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiWorldUpgrade, 1f));
			UiCommandChangeHeroSkin command = new UiCommandChangeHeroSkin
			{
				heroId = this.panelHeroSkinChanger.selectedHero.id,
				skinId = this.sim.GetHeroBoughtSkins(this.panelHeroSkinChanger.selectedHero.id).GetRandomListElement<SkinData>().id,
				random = true
			};
			this.command = command;
			this.state = this.panelHeroSkinChanger.oldState;
		}

		private void InitiateBuySelectedSkin()
		{
			if (this.panelHeroSkinChanger.selectedSkin != null && this.panelHeroSkinChanger.selectedHero != null)
			{
				FlashOffer skinOfferIfExist = this.sim.GetSkinOfferIfExist(this.panelHeroSkinChanger.selectedSkin);
				bool flag = skinOfferIfExist != null;
				double cost = (!flag) ? this.panelHeroSkinChanger.selectedSkin.cost : this.sim.GetFlashOfferCost(skinOfferIfExist);
				if (this.sim.CanCurrencyAfford(this.panelHeroSkinChanger.selectedSkin.currency, cost))
				{
					this.state = UiState.SKIN_BUYING_WARNING;
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackTap, 1f));
				}
				else
				{
					PanelCurrencyWarning.RedirectInfo redirectInfo = new PanelCurrencyWarning.RedirectInfo
					{
						buttonMessageKey = "UI_GO_TO_SHOP",
						callback = delegate()
						{
							this.state = ((!this.currencyWarningInHub) ? UiState.SHOP : UiState.HUB_SHOP);
							this.panelShop.scrollView.verticalNormalizedPosition = 0f;
							this.panelHeroSkinChanger.heroAnimation.OnClose();
							this.panelHubDatabase.panelGear.OnClose();
							this.panelHeroEvolveSkin.heroAnimation.OnClose();
							this.panelSkinBuyingPopup.heroAnimation.OnClose();
						}
					};
					PanelCurrencyWarning panelCurrencyWarning = this.panelCurrencyWarning;
					PanelCurrencyWarning.PopupType popupTypeForCurrency = PanelCurrencyWarning.GetPopupTypeForCurrency(this.panelHeroSkinChanger.selectedSkin.currency);
					PanelCurrencyWarning.RedirectInfo redirectInfo2 = redirectInfo;
					panelCurrencyWarning.SetCurrency(popupTypeForCurrency, this, string.Empty, 0, redirectInfo2);
					this.state = UiState.CURRENCY_WARNING;
					UiManager.AddUiSound(SoundArchieve.inst.uiPopupAppear);
				}
			}
		}

		private void BuySelectedSkin()
		{
			if (this.panelHeroSkinChanger.selectedSkin != null && this.panelHeroSkinChanger.selectedHero != null)
			{
				int skinId = this.panelHeroSkinChanger.selectedSkin.id;
				FlashOffer flashOffer = (this.sim.flashOfferBundle != null) ? this.sim.flashOfferBundle.adventureOffers.Find((FlashOffer o) => o.type == FlashOffer.Type.COSTUME && o.genericIntId == skinId) : null;
				if (flashOffer == null)
				{
					flashOffer = ((this.sim.halloweenFlashOfferBundle != null) ? this.sim.halloweenFlashOfferBundle.offers.Find((FlashOffer o) => o.type == FlashOffer.Type.COSTUME_PLUS_SCRAP && o.genericIntId == skinId) : null);
				}
				this.state = UiState.CHANGE_SKIN;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackCurrency, 1f));
				if (flashOffer != null)
				{
					UICommandBuyFlashOffer command = new UICommandBuyFlashOffer
					{
						flashOffer = flashOffer,
						dropPosition = new DropPosition
						{
							showSideCurrency = true,
							invPos = this.panelCurrencyOnTop[0].currencyFinalPosReference.position,
							endPos = Vector3.down * 0.2f,
							startPos = Vector3.zero
						}
					};
					this.command = command;
				}
				else
				{
					UiCommandBuyHeroSkin command2 = new UiCommandBuyHeroSkin
					{
						skinId = this.panelHeroSkinChanger.selectedSkin.id
					};
					this.command = command2;
				}
				this.panelHeroSkinChanger.updateDetails = true;
				this.panelHeroSkinChanger.dontAnimateHero = true;
				this.panelHeroSkinChanger.DoFlashHeroSpine(1f, 0.3f);
				DOTween.To(() => this.panelHeroSkinChanger.heroAnimation.skeletonGraphic.timeScale, delegate(float x)
				{
					this.panelHeroSkinChanger.heroAnimation.skeletonGraphic.timeScale = x;
				}, 1f, 0.2f).SetDelay(0.3f);
			}
		}

		private void UpdateTrinketOpenPopup(float dt)
		{
			bool flag = this.sim.numTrinketPacks > 0;
			if (flag)
			{
				this.shopTrinketPackSelect.stockCountLabel.transform.parent.gameObject.SetActive(true);
				this.shopTrinketPackSelect.stockCountLabel.text = string.Format(LM.Get("UI_MERCHANT_ITEM_HOWMANY"), this.sim.numTrinketPacks.ToString());
			}
			else
			{
				this.shopTrinketPackSelect.stockCountLabel.transform.parent.gameObject.SetActive(false);
			}
			bool hasDailies = this.sim.hasDailies;
			Currency credits = this.sim.GetCredits();
			Currency aeons = this.sim.GetAeons();
			if (flag)
			{
				this.shopTrinketPackSelect.buyWithAeonButton.gameObject.SetActive(false);
				this.shopTrinketPackSelect.buyWithGemButton.gameObject.SetActive(false);
				this.shopTrinketPackSelect.openButton.gameObject.SetActive(true);
				this.shopTrinketPackSelect.orLabel.gameObject.SetActive(false);
			}
			else if (hasDailies)
			{
				this.shopTrinketPackSelect.buyWithAeonButton.gameObject.SetActive(true);
				this.shopTrinketPackSelect.buyWithGemButton.gameObject.SetActive(true);
				this.shopTrinketPackSelect.openButton.gameObject.SetActive(false);
				double specialCost = (this.selectedShopPack as ShopPackTrinket).specialCost;
				this.shopTrinketPackSelect.buyWithAeonButton.fakeDisabled = !aeons.CanAfford(specialCost);
				this.SetTextToCost(this.shopTrinketPackSelect.buyWithAeonButton.textUp, specialCost);
				this.shopTrinketPackSelect.buyWithGemButton.fakeDisabled = !credits.CanAfford(this.selectedShopPack.cost);
				this.SetTextToCost(this.shopTrinketPackSelect.buyWithGemButton.textUp, this.selectedShopPack.cost);
				this.shopTrinketPackSelect.buyWithAeonButton.rectTransform.SetAnchorPosX(-this.shopTrinketPackSelect.buyButtonsXpos);
				this.shopTrinketPackSelect.buyWithGemButton.rectTransform.SetAnchorPosX(this.shopTrinketPackSelect.buyButtonsXpos);
				this.shopTrinketPackSelect.orLabel.gameObject.SetActive(true);
			}
			else
			{
				this.shopTrinketPackSelect.openButton.gameObject.SetActive(false);
				this.shopTrinketPackSelect.buyWithAeonButton.gameObject.SetActive(false);
				this.shopTrinketPackSelect.buyWithGemButton.gameObject.SetActive(true);
				this.shopTrinketPackSelect.buyWithGemButton.fakeDisabled = !credits.CanAfford(this.selectedShopPack.cost);
				this.SetTextToCost(this.shopTrinketPackSelect.buyWithGemButton.textUp, this.selectedShopPack.cost);
				this.shopTrinketPackSelect.buyWithGemButton.rectTransform.SetAnchorPosX(0f);
				this.shopTrinketPackSelect.orLabel.gameObject.SetActive(true);
			}
		}

		private void UpdateShopLootpackSelectScreen(float dt)
		{
			this.shopLootpackSelect.buttonOpenPack.gameButton.interactable = this.sim.CanOpenLootpack(this.selectedShopPack);
			this.shopLootpackSelect.timer += dt;
			if (UiManager.stateJustChanged)
			{
				this.shopLootpackSelect.SetPack(this.selectedShopPack, this.sim);
				float num = 0f;
				if (this.selectedShopPack.isIAP)
				{
					this.shopLootpackSelect.buttonOpenPack.iconUpType = ButtonUpgradeAnim.IconType.NONE;
					this.shopLootpackSelect.buttonOpenPack.textUp.text = this.selectedShopPack.GetPriceString();
				}
				else
				{
					double cost = this.selectedShopPack.cost;
					Text textUp = this.shopLootpackSelect.buttonOpenPack.textUp;
					if (this.selectedShopPack.currency == CurrencyType.GEM)
					{
						if (this.selectedShopPack is ShopPackTrinket)
						{
							if (this.sim.numTrinketPacks > 0)
							{
								this.shopLootpackSelect.buttonOpenPack.iconUpType = ButtonUpgradeAnim.IconType.NONE;
								textUp.text = string.Format(LM.Get("UI_MERCHANT_ITEM_HOWMANY"), this.sim.numTrinketPacks.ToString());
							}
							else
							{
								this.shopLootpackSelect.buttonOpenPack.iconUpType = ((cost != 0.0) ? ButtonUpgradeAnim.IconType.CREDITS : ButtonUpgradeAnim.IconType.NONE);
								this.SetTextToCost(textUp, cost);
							}
						}
						else
						{
							this.shopLootpackSelect.buttonOpenPack.iconUpType = ((cost != 0.0) ? ButtonUpgradeAnim.IconType.CREDITS : ButtonUpgradeAnim.IconType.NONE);
							this.SetTextToCost(textUp, cost);
						}
					}
					else if (this.selectedShopPack.currency == CurrencyType.GOLD)
					{
						this.shopLootpackSelect.buttonOpenPack.iconUpType = ((cost != 0.0) ? ButtonUpgradeAnim.IconType.GOLD : ButtonUpgradeAnim.IconType.NONE);
						this.SetTextToCost(textUp, cost);
					}
					else if (this.selectedShopPack.currency == CurrencyType.MYTHSTONE)
					{
						this.shopLootpackSelect.buttonOpenPack.iconUpType = ((cost != 0.0) ? ButtonUpgradeAnim.IconType.MYTHSTONES : ButtonUpgradeAnim.IconType.NONE);
						this.SetTextToCost(textUp, cost);
					}
					else if (this.selectedShopPack.currency == CurrencyType.SCRAP)
					{
						this.shopLootpackSelect.buttonOpenPack.iconUpType = ((cost != 0.0) ? ButtonUpgradeAnim.IconType.SCRAPS : ButtonUpgradeAnim.IconType.NONE);
						this.SetTextToCost(textUp, cost);
					}
					else if (this.selectedShopPack.currency == CurrencyType.TOKEN)
					{
						this.shopLootpackSelect.buttonOpenPack.iconUpType = ((cost != 0.0) ? ButtonUpgradeAnim.IconType.TOKENS : ButtonUpgradeAnim.IconType.NONE);
						this.SetTextToCost(textUp, cost);
					}
					else
					{
						if (this.selectedShopPack.currency != CurrencyType.AEON)
						{
							throw new NotImplementedException();
						}
						this.shopLootpackSelect.buttonOpenPack.iconUpType = ((cost != 0.0) ? ButtonUpgradeAnim.IconType.AEONS : ButtonUpgradeAnim.IconType.NONE);
						this.SetTextToCost(textUp, cost);
					}
				}
				bool flag = this.sim.WillLootpackDropLootType(this.selectedShopPack, LootType.TOKENS);
				bool flag2 = this.sim.WillLootpackDropLootType(this.selectedShopPack, LootType.GEAR);
				bool flag3 = this.sim.WillLootpackDropLootType(this.selectedShopPack, LootType.SCRAPS);
				bool flag4 = this.sim.WillLootpackDropLootType(this.selectedShopPack, LootType.CREDITS);
				bool flag5 = this.sim.WillLootpackDropLootType(this.selectedShopPack, LootType.RUNE);
				bool flag6 = this.sim.WillLootpackDropLootType(this.selectedShopPack, LootType.TRINKET);
				bool flag7 = this.sim.WillLootpackDropLootType(this.selectedShopPack, LootType.TRINKET_BOX);
				bool flag8 = this.sim.WillLootpackDropLootType(this.selectedShopPack, LootType.SKINS);
				bool flag9 = this.sim.WillLootpackDropLootType(this.selectedShopPack, LootType.CANDIES);
				int num2 = 0;
				if (flag4)
				{
					string lootpackSelectDescription = this.sim.GetLootpackSelectDescription(this.selectedShopPack, LootType.CREDITS);
					this.shopLootpackSelect.panelLootItems[num2].SetType(LootType.CREDITS, 0.0, lootpackSelectDescription, string.Empty, 0);
					this.shopLootpackSelect.panelLootItems[num2].rectTransform.anchoredPosition = new Vector2(0f, -443f - 83f * (float)num2);
					num2++;
				}
				if (flag)
				{
					string lootpackSelectDescription2 = this.sim.GetLootpackSelectDescription(this.selectedShopPack, LootType.TOKENS);
					this.shopLootpackSelect.panelLootItems[num2].SetType(LootType.TOKENS, 0.0, lootpackSelectDescription2, string.Empty, 0);
					this.shopLootpackSelect.panelLootItems[num2].rectTransform.anchoredPosition = new Vector2(0f, -443f - 83f * (float)num2);
					num2++;
				}
				if (flag3)
				{
					string lootpackSelectDescription3 = this.sim.GetLootpackSelectDescription(this.selectedShopPack, LootType.SCRAPS);
					this.shopLootpackSelect.panelLootItems[num2].SetType(LootType.SCRAPS, 0.0, lootpackSelectDescription3, string.Empty, 0);
					this.shopLootpackSelect.panelLootItems[num2].rectTransform.anchoredPosition = new Vector2(0f, -443f - 83f * (float)num2);
					num2++;
				}
				if (flag5)
				{
					string extraDescription = string.Empty;
					if (this.sim.CanAnyRuneDropFromLootpack())
					{
						extraDescription = this.sim.GetLootpackSelectDescription(this.selectedShopPack, LootType.RUNE);
					}
					else
					{
						extraDescription = AM.cs(LM.Get("RUNE_NOT_AVAILABLE"), this.colorCantAffordInit);
					}
					this.shopLootpackSelect.panelLootItems[num2].SetType(LootType.RUNE, (double)this.selectedShopPack.numRunes, extraDescription, string.Empty, 0);
					this.shopLootpackSelect.panelLootItems[num2].rectTransform.anchoredPosition = new Vector2(0f, -443f - 83f * (float)num2);
					num2++;
				}
				if (flag2)
				{
					string extraDescription2 = string.Format(LM.Get("UI_ITEM_X"), this.selectedShopPack.GetNumGears(this.sim.GetUniversalBonusAll()).ToString());
					string lootpackSelectDescription4 = this.sim.GetLootpackSelectDescription(this.selectedShopPack, LootType.GEAR);
					this.shopLootpackSelect.panelLootItems[num2].SetType(LootType.GEAR, 0.0, extraDescription2, lootpackSelectDescription4, this.selectedShopPack.GetLootpackMaxGearLevel());
					this.shopLootpackSelect.panelLootItems[num2].rectTransform.anchoredPosition = new Vector2(0f, -443f - 83f * (float)num2);
					num2++;
				}
				if (flag6)
				{
					this.shopLootpackSelect.panelLootItems[num2].SetType(LootType.TRINKET, (double)this.selectedShopPack.numTrinkets, string.Empty, string.Empty, 0);
					this.shopLootpackSelect.panelLootItems[num2].rectTransform.anchoredPosition = new Vector2(0f, -443f - 83f * (float)num2);
					num2++;
				}
				if (flag7)
				{
					this.shopLootpackSelect.panelLootItems[num2].SetType(LootType.TRINKET_BOX, (double)this.selectedShopPack.numTrinketPacks, string.Empty, string.Empty, 0);
					this.shopLootpackSelect.panelLootItems[num2].rectTransform.anchoredPosition = new Vector2(0f, -443f - 83f * (float)num2);
					num2++;
				}
				if (flag8)
				{
					List<int> skins = this.selectedShopPack.GetSkins();
					for (int i = 0; i < this.selectedShopPack.numSkins; i++)
					{
						this.shopLootpackSelect.panelLootItems[num2].uiManager = this;
						SkinData skinData = this.sim.GetSkinData(skins[i]);
						this.shopLootpackSelect.panelLootItems[num2].rectTransform.anchoredPosition = new Vector2(0f, -443f - 120f * (float)num2);
						this.shopLootpackSelect.panelLootItems[num2].SetType(LootType.SKINS, 0.0, skinData.GetName(), string.Empty, skins[i]);
						num += 37f;
						num2++;
					}
				}
				if (flag9)
				{
					string lootpackSelectDescription5 = this.sim.GetLootpackSelectDescription(this.selectedShopPack, LootType.CANDIES);
					this.shopLootpackSelect.panelLootItems[num2].SetType(LootType.CANDIES, 0.0, lootpackSelectDescription5, string.Empty, 0);
					this.shopLootpackSelect.panelLootItems[num2].rectTransform.anchoredPosition = new Vector2(0f, -443f - 83f * (float)num2);
					num2++;
				}
				this.shopLootpackSelect.popupRect.SetSizeDeltaY(931f + num);
				int j = 0;
				int count = this.shopLootpackSelect.panelLootItems.Count;
				while (j < count)
				{
					this.shopLootpackSelect.panelLootItems[j].gameObject.SetActive(j < num2);
					j++;
				}
			}
		}

		private void SetShopLootpackSummaryScreen()
		{
			bool flag = this.sim.WillLootpackDropLootType(this.selectedShopPack, LootType.CREDITS);
			bool flag2 = this.sim.WillLootpackDropLootType(this.selectedShopPack, LootType.TOKENS);
			bool flag3 = this.sim.WillLootpackDropLootType(this.selectedShopPack, LootType.SCRAPS);
			this.SetNumberOfPanels(this.shopLootpackSummary.panelLootItems, ((!flag) ? 0 : 1) + ((!flag2) ? 0 : 1) + ((!flag3) ? 0 : 1), false);
			List<Rune> lootDropRunes = this.sim.lootDropRunes;
			int count = lootDropRunes.Count;
			List<Trinket> lootDropTrinkets = this.sim.lootDropTrinkets;
			int count2 = lootDropTrinkets.Count;
			this.SetNumberOfPanels(this.shopLootpackSummary.panelLootItemsBig, count + count2 + this.sim.lootDropGears.Count, true);
			float lastActivePanelY = this.GetLastActivePanelY(this.shopLootpackSummary.panelLootItems);
			this.PositionPanels(this.shopLootpackSummary.panelLootItemsBig, lastActivePanelY - 100f);
			int num = 0;
			if (flag)
			{
				this.shopLootpackSummary.panelLootItems[num].SetType(LootType.CREDITS, this.sim.lootDropCredits, string.Empty, string.Empty, 0);
				num++;
			}
			if (flag2)
			{
				this.shopLootpackSummary.panelLootItems[num].SetType(LootType.TOKENS, this.sim.lootDropTokens, string.Empty, string.Empty, 0);
				num++;
			}
			if (flag3)
			{
				this.shopLootpackSummary.panelLootItems[num].SetType(LootType.SCRAPS, this.sim.lootDropScraps, string.Empty, string.Empty, 0);
				num++;
			}
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				Sprite spriteTotemSmall = this.GetSpriteTotemSmall(lootDropRunes[i].belongsTo.id);
				Sprite spriteRune = this.GetSpriteRune(lootDropRunes[i].id);
				this.shopLootpackSummary.panelLootItemsBig[num2].SetTypeGearBig(spriteRune, spriteTotemSmall, this.GetRuneColor(lootDropRunes[i].belongsTo.id), lootDropRunes[i]);
				num2++;
			}
			for (int j = 0; j < count2; j++)
			{
				this.shopLootpackSummary.panelLootItemsBig[num2].SetTypeGearBig(lootDropTrinkets[j], UiManager.GearLevelString(lootDropTrinkets[j].bodyColorIndex));
				num2++;
			}
			int k = 0;
			int count3 = this.sim.lootDropGears.Count;
			while (k < count3)
			{
				HeroDataBase belongsTo = this.sim.lootDropGears[k].data.belongsTo;
				string id = belongsTo.id;
				Sprite spriteHeroPortrait = this.GetSpriteHeroPortrait(id);
				Sprite gearSprite = this.spritesGearIcon[this.sim.lootDropGears[k].GetId()];
				this.shopLootpackSummary.panelLootItemsBig[num2].SetTypeGearBig(this.sim.lootDropGears[k], gearSprite, spriteHeroPortrait, belongsTo);
				num2++;
				k++;
			}
			bool flag4 = this.sim.lootDropGears.Count > 0 && ((this.sim.amountLootPacksOpenedForHint <= 30 && (this.sim.amountLootPacksOpenedForHint - 2) % 3 == 0) || (this.sim.amountLootPacksOpenedForHint > 30 && this.sim.amountLootPacksOpenedForHint <= 100 && (this.sim.amountLootPacksOpenedForHint - 2) % 10 == 0));
			this.shopLootpackSummary.hintParent.SetActive(flag4);
			this.shopLootpackSummary.popupRect.SetAnchorPosY((float)((!flag4) ? -100 : -50));
		}

		private void UpdateShopLootpackSummaryScreen(float dt)
		{
			this.shopLootpackSummary.timer += dt;
		}

		public void UpdateHubOptionsScreen()
		{
			this.hubOptions.buttonSoundOnOff.isOn = !this.soundManager.muteSounds;
			this.hubOptions.buttonMusicOnOff.isOn = !this.soundManager.muteMusic;
			this.hubOptions.buttonVoiceOnOff.isOn = !this.soundManager.muteVoices;
			PanelAdvancedOptions panelAdvancedOptions = this.hubOptions.panelAdvancedOptions;
			panelAdvancedOptions.compassWidget.toggleButton.isOn = !this.sim.compassDisabled;
			panelAdvancedOptions.lowPowerWidget.toggleButton.isOn = this.sim.prefers30Fps;
			panelAdvancedOptions.appSleepWidget.toggleButton.isOn = this.sim.appNeverSleep;
			panelAdvancedOptions.notationWidget.toggleButton.isOn = this.sim.scientificNotation;
			panelAdvancedOptions.cooldownUiWidget.toggleButton.isOn = this.sim.secondaryCdUi;
			long value = DateTime.Now.Ticks - this.sim.lastSaveTime;
			panelAdvancedOptions.timeSinceLastSaveAmount.text = GameMath.GetTimeString(TimeSpan.FromTicks(value));
			panelAdvancedOptions.notificationsWidget.toggleButton.isOn = StoreManager.areNotificationsAllowed;
			if (StoreManager.areNotificationsAllowed)
			{
				panelAdvancedOptions.specialOffersToggle.toggleButton.isOn = StoreManager.specialOffersNotifications;
				panelAdvancedOptions.freeChestsToggle.toggleButton.isOn = StoreManager.freeChestsNotifications;
				if (this.sim.hasDailies)
				{
					panelAdvancedOptions.sideQuestsToggle.gameObject.SetActive(true);
					panelAdvancedOptions.sideQuestsToggle.toggleButton.isOn = StoreManager.sideQuestNotifications;
				}
				else
				{
					panelAdvancedOptions.sideQuestsToggle.gameObject.SetActive(false);
				}
				if (this.sim.MineAnyUnlocked())
				{
					panelAdvancedOptions.minesToggle.gameObject.SetActive(true);
					panelAdvancedOptions.minesToggle.toggleButton.isOn = StoreManager.mineNotifications;
				}
				else
				{
					panelAdvancedOptions.minesToggle.gameObject.SetActive(false);
				}
				if (TutorialManager.AreFlashOffersUnlocked())
				{
					panelAdvancedOptions.flashOffersToggle.gameObject.SetActive(true);
					panelAdvancedOptions.flashOffersToggle.toggleButton.isOn = StoreManager.flashOffersNotifications;
				}
				else
				{
					panelAdvancedOptions.flashOffersToggle.gameObject.SetActive(false);
				}
				if (this.sim.riftQuestDustCollected > 0.0)
				{
					panelAdvancedOptions.dustRestBonusToggle.gameObject.SetActive(true);
					panelAdvancedOptions.dustRestBonusToggle.toggleButton.isOn = StoreManager.dustRestBonusFullNotifications;
				}
				else
				{
					panelAdvancedOptions.dustRestBonusToggle.gameObject.SetActive(false);
				}
				if (this.sim.IsChristmasTreeEnabled())
				{
					panelAdvancedOptions.christmasEventToggle.gameObject.SetActive(true);
					panelAdvancedOptions.christmasEventToggle.toggleButton.isOn = StoreManager.christmasEventNotifications;
				}
				else
				{
					panelAdvancedOptions.christmasEventToggle.gameObject.SetActive(false);
				}
				panelAdvancedOptions.eventsToggle.toggleButton.isOn = StoreManager.eventsNotifications;
				panelAdvancedOptions.childTogglesParent.SetAnchorPosY(panelAdvancedOptions.notificationsWidget.rectTransform.anchoredPosition.y - 170f);
				panelAdvancedOptions.childTogglesParent.gameObject.SetActive(true);
			}
			else
			{
				panelAdvancedOptions.childTogglesParent.gameObject.SetActive(false);
			}
			if (StoreManager.IsAuthed())
			{
				this.hubOptions.cloudSaveIcon.color = HubOptions.storeIsAuthedColor;
				this.hubOptions.buttonLeaderboards.interactable = true;
				this.hubOptions.buttonAchievements.interactable = true;
				this.hubOptions.buttonLeaderboards.raycastTarget.sprite = this.hubOptions.enabledButtonSprite;
				this.hubOptions.buttonAchievements.raycastTarget.sprite = this.hubOptions.enabledButtonSprite;
				this.hubOptions.achievementsIcon.rectTransform.anchoredPosition = new Vector2(0f, 10.2f);
				this.hubOptions.leaderboardsIcon.rectTransform.anchoredPosition = new Vector2(0f, 10.2f);
				this.hubOptions.achievementsIcon.color = HubOptions.storeIsNotAuthedColor;
				this.hubOptions.leaderboardsIcon.color = HubOptions.storeIsNotAuthedColor;
				this.hubOptions.leaderboardsIcon.color = HubOptions.storeIsNotAuthedColor;
			}
			else
			{
				this.hubOptions.cloudSaveIcon.color = HubOptions.storeIsNotAuthedColor;
				this.hubOptions.buttonLeaderboards.interactable = false;
				this.hubOptions.buttonAchievements.interactable = false;
				this.hubOptions.achievementsIcon.color = HubOptions.storeDisabledColor;
				this.hubOptions.leaderboardsIcon.color = HubOptions.storeDisabledColor;
				this.hubOptions.achievementsIcon.rectTransform.anchoredPosition = new Vector2(0f, 0f);
				this.hubOptions.leaderboardsIcon.rectTransform.anchoredPosition = new Vector2(0f, 0f);
				this.hubOptions.buttonLeaderboards.raycastTarget.sprite = this.hubOptions.disabledButtonSprite;
				this.hubOptions.buttonAchievements.raycastTarget.sprite = this.hubOptions.disabledButtonSprite;
			}
			if (UiManager.stateJustChanged)
			{
				this.hubOptions.textPlayfabId.text = "ID: " + PlayfabManager.playerId;
				if (this.sim.hasCompass)
				{
					panelAdvancedOptions.compassWidget.gameObject.SetActive(true);
				}
				else
				{
					panelAdvancedOptions.compassWidget.gameObject.SetActive(false);
				}
				panelAdvancedOptions.OrderWidgetPositions(this.hubOptions.GetNumNotificationsAvailable());
			}
		}

		public void UpdateHubOptionsWikiScreen()
		{
			this.hubOptionsWiki.UpdateScreen(this.sim.GetWorld(GameMode.RIFT).IsModeUnlocked(), this.sim.numTrinketSlots > 0, this.sim.GetUnlock(UnlockIds.TRINKET_DISASSEMBLE).isCollected);
		}

		public void UpdateModePrestigeScreen()
		{
			double numMythstonesOnPrestigePure = this.sim.GetNumMythstonesOnPrestigePure();
			double numMythstonesOnPrestigeArtifactBonus = this.sim.GetNumMythstonesOnPrestigeArtifactBonus();
			this.panelPrestige.textMythstoneAmounts[0].text = "+" + GameMath.GetDoubleString(numMythstonesOnPrestigePure);
			this.panelPrestige.textMythstoneAmounts[1].text = "+" + GameMath.GetDoubleString(numMythstonesOnPrestigeArtifactBonus);
			bool interactable = this.sim.CanPrestigeNow();
			this.panelPrestige.buttonPrestige.interactable = interactable;
			this.panelPrestige.textAmountPrestige.text = "+" + GameMath.GetDoubleString(this.sim.GetNumMythstonesOnPrestige(false));
			this.panelPrestige.prestigeInfoMythAmount.text = "+" + GameMath.GetDoubleString(this.sim.GetNumMythstonesOnPrestige(false));
			if (this.sim.IsMegaPrestigeUnlocked())
			{
				this.panelPrestige.buttonMegaPrestige.gameButton.interactable = this.sim.CanAffordMegaPrestige();
				this.panelPrestige.bgPrestige.anchoredPosition = this.panelPrestige.posBgPrestige;
				this.panelPrestige.bgMegaPrestige.gameObject.SetActive(true);
				this.panelPrestige.textAmountMegaPrestige.text = "+" + GameMath.GetDoubleString(this.sim.GetNumMythstonesOnPrestige(true));
				this.panelPrestige.gemStoneCurrencyWidget.gameObject.SetActive(true);
				this.panelPrestige.gemStoneCurrencyWidget.SetCurrency(CurrencyType.GEM, this.sim.GetCurrency(CurrencyType.GEM).GetString(), true, GameMode.STANDARD, true);
			}
			else
			{
				this.panelPrestige.gemStoneCurrencyWidget.gameObject.SetActive(false);
				this.panelPrestige.bgPrestige.anchoredPosition = new Vector2(0f, this.panelPrestige.posBgPrestige.y);
				this.panelPrestige.bgMegaPrestige.gameObject.SetActive(false);
			}
			this.panelPrestige.mythStoneCurrencyWidget.SetCurrency(CurrencyType.MYTHSTONE, GameMath.GetFlooredDoubleString(this.sim.GetCurrency(CurrencyType.MYTHSTONE).GetAmount()), true, GameMode.STANDARD, true);
			TimeSpan time = new TimeSpan((long)this.sim.prestigeRunTimer * 10000000L);
			this.panelPrestige.textTimePlayedAmount.text = GameMath.GetLocalizedTimeString(time);
			this.panelPrestige.textStageAmount.text = this.sim.GetCurrentStage(GameMode.STANDARD).ToString();
			if (this.sim.lastPrestigeRunstats != null)
			{
				this.panelPrestige.lastRunMythstoneEarnedAmount.text = this.sim.lastPrestigeRunstats.GetMythstoneString();
				TimeSpan time2 = new TimeSpan((long)this.sim.lastPrestigeRunstats.playTime * 10000000L);
				this.panelPrestige.lastRunTimePlayedAmount.text = GameMath.GetLocalizedTimeString(time2);
				this.panelPrestige.lastRunStageAmount.text = this.sim.lastPrestigeRunstats.stage.ToString();
			}
		}

		public void UpdateModeUnlockRewardScreen(float dt)
		{
			if (UiManager.stateJustChanged)
			{
				Unlock unlock = this.unlockAboutToBeCollected;
				this.panelUnlockReward.textTitle.text = string.Format(LM.Get("UNLOCK_REWARD_STAGE_COMPLETED"), unlock.GetReqInt());
				if (unlock.HasRewardOfType(typeof(UnlockRewardGameModeCrusade)))
				{
					UiManager.sounds.Add(new SoundEventUiVoiceSimple(SoundArchieve.inst.voGreenManTimeChallengeUnlock, "GREEN_MAN", 1f));
				}
				if (unlock.HasRewardOfType(typeof(UnlockRewardHero)))
				{
					this.panelUnlockReward.textNextReward.text = LM.Get("UI_UNLOCK_CLAIM_HERO");
				}
				else if (unlock.HasRewardOfType(typeof(UnlockRewardCompass)))
				{
					this.panelUnlockReward.textNextReward.text = LM.Get("UI_UNLOCK_CLAIM_COMPASS");
				}
				else
				{
					this.panelUnlockReward.textNextReward.text = LM.Get("UI_UNLOCK_CLAIM_REWARD");
				}
				this.panelUnlockReward.SetRewardDetails(unlock, this, false);
			}
			if (this.panelUnlockReward.fadingOut)
			{
				this.panelUnlockReward.timer -= dt;
				if (this.panelUnlockReward.timer < 0f && this.panelUnlockReward.returnToStateAfterFadeOut)
				{
					this.state = this.panelUnlockReward.stateToReturnTo;
				}
			}
			else
			{
				this.panelUnlockReward.timer += dt;
			}
		}

		public void UpdateModeMerchantItemSelectScreen()
		{
            Simulation.MerchantItem merchantItem = (!this.selectedMerchantItemIsFromEvent) ? this.sim.GetMerchantItem(this.selectedMerchantItem) : this.sim.GetEventMerchantItem(this.selectedMerchantItem);
			this.panelMerchantItemSelect.textDesciption0.text = merchantItem.GetDescriptionString(this.sim, this.panelMerchantItemSelect.copyAmount);
			if (merchantItem.GetPrice() == 0.0)
			{
				this.panelMerchantItemSelect.currencyWidget.gameObject.SetActive(false);
			}
			else
			{
				this.panelMerchantItemSelect.currencyWidget.gameObject.SetActive(true);
				this.panelMerchantItemSelect.currencyWidget.SetCurrency(CurrencyType.TOKEN, this.sim.GetCurrency(CurrencyType.TOKEN).GetString(), true, GameMode.STANDARD, true);
			}
			string secondaryDescriptionString = merchantItem.GetSecondaryDescriptionString(this.sim);
			if (secondaryDescriptionString != null)
			{
				this.panelMerchantItemSelect.SetTwoDescription();
				this.panelMerchantItemSelect.textDesciptionSecondary.text = secondaryDescriptionString;
			}
			else
			{
				this.panelMerchantItemSelect.SetSingleDescription();
				this.panelMerchantItemSelect.textDesciptionSecondary.gameObject.SetActive(false);
			}
			this.panelMerchantItemSelect.buttonInreaseCopy.interactable = (this.panelMerchantItemSelect.copyAmount < merchantItem.GetNumLeft());
			this.panelMerchantItemSelect.buttonDecreaseCopy.interactable = (this.panelMerchantItemSelect.copyAmount > 1);
			if (UiManager.stateJustChanged)
			{
				this.panelMerchantItemSelect.icon.sprite = this.panelMode.merchantItems[this.selectedMerchantItem].imageItem.sprite;
				this.panelMerchantItemSelect.textTitle.text = merchantItem.GetTitleString();
				this.panelMerchantItemSelect.buttonBuy.textUp.text = merchantItem.GetPriceString(this.panelMerchantItemSelect.copyAmount);
				this.panelMerchantItemSelect.textDesciption1.text = ((merchantItem.GetNumMax() != 0) ? merchantItem.GetNumLeftString() : merchantItem.GetNumInInventoryString());
				this.panelMerchantItemSelect.useItemButton.gameObject.SetActive(merchantItem.GetNumMax() == 0);
				this.panelMerchantItemSelect.buttonBuy.gameObject.SetActive(merchantItem.GetNumMax() > 0);
				this.panelMerchantItemSelect.textOpen.text = string.Format(LM.Get("UI_OPEN"), AM.SizeText(this.panelMerchantItemSelect.copyAmount, 50));
				if (merchantItem.GetNumMax() == 0 && merchantItem.GetNumInInventory() == 0)
				{
					this.panelMerchantItemSelect.nonLeftMessage.enabled = true;
					this.panelMerchantItemSelect.nonLeftMessage.text = LM.Get("NON_CHRISTMAS_MERCHANT_ITEMS_LEFT");
					this.panelMerchantItemSelect.contentParent.SetSizeDeltaY((float)((!this.sim.IsMultiMerchantEnabled()) ? 897 : 1027));
					this.panelMerchantItemSelect.contentParent.SetAnchorPosY(-111f);
					this.panelMerchantItemSelect.useItemButton.text.text = LM.Get("GO_TO_TREE");
					this.panelMerchantItemSelect.useItemButton.interactable = this.sim.IsChristmasTreeEnabled();
				}
				else
				{
					this.panelMerchantItemSelect.nonLeftMessage.enabled = false;
					this.panelMerchantItemSelect.contentParent.SetSizeDeltaY((float)((!this.sim.IsMultiMerchantEnabled()) ? 857 : 987));
					this.panelMerchantItemSelect.contentParent.SetAnchorPosY(-91f);
					this.panelMerchantItemSelect.useItemButton.text.text = LM.Get("UI_RUNES_USE");
				}
			}
			bool flag = this.sim.CanAffordMerchantItem(this.selectedMerchantItem, this.panelMerchantItemSelect.copyAmount, this.selectedMerchantItemIsFromEvent);
			this.panelMerchantItemSelect.buttonBuy.openWarningPopup = !flag;
			this.panelMerchantItemSelect.buttonBuy.textCantAffordColorChangeManual = !flag;
		}

		public void UpdateOfflineEarningsScreen(World activeWorld, float dt)
		{
			this.panelOfflineEarnings.timer += dt;
			if (UiManager.stateJustChanged)
			{
				string doubleString = GameMath.GetDoubleString(activeWorld.offlineGold);
				this.panelOfflineEarnings.textMode.text = UiManager.GetModeName(this.sim.GetCurrentGameMode());
				this.panelOfflineEarnings.textGold.text = doubleString;
				if (this.sim.hasCompass)
				{
					if (this.sim.compassDisabled)
					{
						this.panelOfflineEarnings.textStageInfo.text = LM.Get("UI_OFFLINE_STAGE_COMPASS_DISABLED_DESC");
					}
					else
					{
						this.panelOfflineEarnings.textStageInfo.text = LM.Get("UI_OFFLINE_STAGE_DESC");
					}
					this.panelOfflineEarnings.textReward.text = LM.Get("UI_OFFLINE_REWARD_DESC");
					this.panelOfflineEarnings.textStage.text = string.Format(LM.Get("UI_OFFLINE_STAGE"), "<size=60>" + ChallengeStandard.GetStageNo(activeWorld.offlineWaveProgression) + "</size>");
				}
				else
				{
					int reqAmount = Simulator.unlockCompass.GetReqAmount();
					this.panelOfflineEarnings.textStageNo.text = reqAmount.ToString();
					this.panelOfflineEarnings.textStageInfo.text = string.Format(LM.Get("UI_OFFLINE_UNLOCK_COMPASS"), reqAmount, "<color=#40331EFF>" + LM.Get("UI_OFFLINE_COMPASS") + "</color>");
					this.panelOfflineEarnings.textReward.text = LM.Get("UI_OFFLINE_REWARD_DESC2");
				}
				this.panelOfflineEarnings.SetSpriteProgress(this.sim.hasCompass, this.sim.IsActiveMode(GameMode.CRUSADE));
			}
		}

		public void UpdateHubModeSetupTotemScreen(List<TotemDataBase> allTotems, Dictionary<TotemDataBase, GameMode> boughtTotems)
		{
			if (UiManager.stateJustChanged)
			{
				List<bool> list = new List<bool>();
				Dictionary<TotemDataBase, Sprite> dictionary = new Dictionary<TotemDataBase, Sprite>();
				foreach (TotemDataBase totemDataBase in allTotems)
				{
					list.Add(!this.sim.IsTotemUnlocked(totemDataBase.id));
					if (boughtTotems.ContainsKey(totemDataBase))
					{
						dictionary.Add(totemDataBase, this.GetSpriteModeFlag(boughtTotems[totemDataBase]));
					}
				}
				this.panelSelectTotem.SetTotems(allTotems, list, this.spritesTotemSmall, this.spritesTotem, dictionary);
			}
		}

		public void UpdateShopLootpackOpeningScreen()
		{
			if (!this.shopLootpackOpening.resultsReceived)
			{
				this.shopLootpackOpening.universalBonus = this.sim.GetUniversalBonusAll();
				bool flag = this.sim.WillLootpackDropLootType(this.selectedShopPack, LootType.CREDITS);
				bool flag2 = this.sim.WillLootpackDropLootType(this.selectedShopPack, LootType.TOKENS);
				bool flag3 = this.sim.WillLootpackDropLootType(this.selectedShopPack, LootType.SCRAPS);
				bool flag4 = this.sim.WillLootpackDropLootType(this.selectedShopPack, LootType.CANDIES);
				this.shopLootpackOpening.totalCreditsBeforeBeginning = this.sim.creditsBeforeOpeningLootpack;
				this.shopLootpackOpening.totalTokensBeforeBeginning = this.sim.tokensBeforeOpeningLootpack;
				this.shopLootpackOpening.totalScrapsBeforeBeginning = this.sim.scrapsBeforeOpeningLootpack;
				this.shopLootpackOpening.creditsWillDrop = flag;
				this.shopLootpackOpening.tokensWillDrop = flag2;
				this.shopLootpackOpening.scrapsWillDrop = flag3;
				this.shopLootpackOpening.amountCredits = this.sim.lootDropCredits;
				this.shopLootpackOpening.amountTokens = this.sim.lootDropTokens;
				this.shopLootpackOpening.amountScraps = this.sim.lootDropScraps;
				this.shopLootpackOpening.runesEarned = this.sim.lootDropRunes;
				this.shopLootpackOpening.trinketsEarned = this.sim.lootDropTrinkets;
				this.shopLootpackOpening.lootGears = this.sim.lootDropGears;
				this.shopLootpackOpening.lootGearChanges = this.sim.lootDropGearChanges;
				this.shopLootpackOpening.spritesGearIcon = this.spritesGearIcon;
				this.shopLootpackOpening.spritesHeroCircle = this.spritesHeroPortrait;
				this.shopLootpackOpening.spritesTotem = this.spritesTotem;
				this.shopLootpackOpening.spritesRune = this.spritesRune;
				this.shopLootpackOpening.runeColorForRing = this.runeColorForRing;
				this.shopLootpackOpening.allHeroes = this.sim.GetAllHeroes();
				this.shopLootpackOpening.allHeroesBoughtGears = this.sim.boughtHeroesCosmetic;
				this.shopLootpackOpening.resultsReceived = true;
				this.shopLootpackOpening.numItemsLeft = ((!flag) ? 0 : 1) + ((!flag2) ? 0 : 1) + ((!flag3) ? 0 : 1) + ((!flag4) ? 0 : 1) + this.sim.lootDropRunes.Count + this.sim.lootDropTrinkets.Count + this.sim.lootDropGears.Count;
			}
			if (this.shopLootpackOpening.finished)
			{
				if (this.selectedShopPack is ShopPackTrinket)
				{
					if (this.panelShop.isHubMode)
					{
						this.state = UiState.HUB_SHOP;
					}
					else
					{
						this.state = UiState.SHOP;
					}
				}
				else
				{
					this.state = UiState.SHOP_LOOTPACK_SUMMARY;
					this.SetShopLootpackSummaryScreen();
					if (this.selectedShopPack is ShopPackLootpackFree)
					{
						UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackOpen[0], 1f));
					}
					else if (this.selectedShopPack is ShopPackLootpackRare)
					{
						UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackOpen[1], 1f));
					}
					else
					{
						UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackOpen[2], 1f));
					}
				}
			}
		}

		public void UpdateHubModeSetupScreen()
		{
			this.panelHubModeSetup.buttonStart.interactable = this.panelHubModeSetup.CanSetupBeCompleted();
			if (UiManager.stateJustChanged)
			{
				if (this.panelHubModeSetup.mode == GameMode.RIFT)
				{
					World world = this.sim.GetWorld(this.panelHubModeSetup.mode);
					this.panelHubModeSetup.randomTeam.gameObject.SetActive(true);
					this.panelHubModeSetup.buttonStart.rectTransform.SetAnchorPosX(80f);
					this.panelHubModeSetup.textNextReward.text = LM.Get("UI_UNLOCK_REWARD");
					ChallengeRift challengeRift = world.activeChallenge as ChallengeRift;
					if (challengeRift == null)
					{
						throw new NotImplementedException();
					}
					Unlock unlock = challengeRift.unlock;
					if (this.panelHubModeSetup.challengeIndexChanged)
					{
						if (challengeRift.IsCursed())
						{
							this.panelHubModeSetup.SetButtonTheme(true);
							this.panelHubModeSetup.selectedRiftNo.text = (challengeRift.riftData.cursesSetup.originalRiftNo + 1).ToString();
							this.panelHubModeSetup.headerBackgroundImage.color = this.panelHubModeSetup.headerColorCursed;
							this.panelHubModeSetup.backgroundPatternImage.color = this.panelHubModeSetup.backgroundColorCursed;
							this.panelHubModeSetup.backgroundBaseImage.color = this.panelHubModeSetup.backgroundBaseColorCursed;
						}
						else
						{
							this.panelHubModeSetup.SetButtonTheme(false);
							this.panelHubModeSetup.selectedRiftNo.text = (world.GetActiveChallengeIndex() + 1).ToString();
							this.panelHubModeSetup.headerBackgroundImage.color = this.panelHubModeSetup.headerColorNormal;
							this.panelHubModeSetup.backgroundPatternImage.color = this.panelHubModeSetup.backgroundColorNormal;
							this.panelHubModeSetup.backgroundBaseImage.color = this.panelHubModeSetup.backgroundBaseColorNormal;
						}
						this.panelHubModeSetup.SetMode(GameMode.RIFT, challengeRift.hasRing, challengeRift.numHeroesMin, this.sim);
						this.panelHubModeSetup.teamSelectPanel.SetActive(true);
						this.panelHubModeSetup.challengePanel.SetActive(true);
						this.panelHubModeSetup.SetRiftEffects(challengeRift, this);
						this.panelHubModeSetup.textChallenge.text = string.Format(LM.Get("UI_UNLOCK_DEFEAT_WAVES"), challengeRift.GetTargetTotWave().ToString());
						this.panelHubModeSetup.textTime.text = GameMath.GetTimeString(challengeRift.dur);
						if (unlock.isCollected)
						{
							this.SetPanelModeSetupRewardAeonDust(challengeRift.riftPointReward);
						}
						else
						{
							this.SetPanelModeSetupReward(unlock);
						}
					}
				}
				else
				{
					this.panelHubModeSetup.backgroundPatternImage.color = this.panelHubModeSetup.backgroundColorNormal;
					this.panelHubModeSetup.backgroundBaseImage.color = this.panelHubModeSetup.backgroundBaseColorNormal;
					this.panelHubModeSetup.headerBackgroundImage.color = this.panelHubModeSetup.headerColorNormal;
					this.panelHubModeSetup.randomTeam.gameObject.SetActive(false);
					this.panelHubModeSetup.SetButtonTheme(false);
					this.panelHubModeSetup.buttonStart.rectTransform.SetAnchorPosX(0f);
				}
			}
			if (this.panelHubModeSetup.mode == GameMode.RIFT)
			{
				this.panelHubModeSetup.riftDiscoveryNotificationBadge.numNotifications = ((!this.sim.IsNextRiftsDiscoverable()) ? 0 : 1);
			}
			this.panelHubModeSetup.challengeIndexChanged = false;
		}

		public void UpdateChallengeWinScreen(float dt)
		{
			if (this.panelChallengeWin.fadingOut)
			{
				this.panelChallengeWin.timer -= dt;
				if (this.panelChallengeWin.timer < 0f)
				{
					this.state = UiState.NONE;
				}
			}
			else
			{
				this.panelChallengeWin.timer += dt;
			}
		}

		public void UpdateHeroesRunesScreen(TotemDataBase totemDataBase, PanelHeroesRunes panelHeroesRunes)
		{
			if (UiManager.stateJustChanged)
			{
				panelHeroesRunes.ringSpine.totem = totemDataBase;
				panelHeroesRunes.textName.text = totemDataBase.GetName();
				panelHeroesRunes.textDesc.text = totemDataBase.GetDesc();
				panelHeroesRunes.spriteRing = this.GetSpriteTotem(totemDataBase.id);
				List<Rune> runes = this.sim.GetRunes(totemDataBase);
				List<Rune> boughtRunes = this.sim.GetBoughtRunes(totemDataBase);
				List<Rune> wornRunes = this.sim.GetWornRunes(totemDataBase);
				List<Rune> list = new List<Rune>();
				foreach (Rune item in boughtRunes)
				{
					list.Add(item);
				}
				foreach (Rune item2 in runes)
				{
					if (!boughtRunes.Contains(item2))
					{
						list.Add(item2);
					}
				}
				if (boughtRunes.Count == 0)
				{
					panelHeroesRunes.scrollviewContent.SetActive(false);
					panelHeroesRunes.imageShopBg.gameObject.SetActive(TutorialManager.IsShopTabUnlocked());
				}
				else
				{
					panelHeroesRunes.scrollviewContent.SetActive(true);
					panelHeroesRunes.imageShopBg.gameObject.SetActive(false);
				}
				int i = 0;
				int num = panelHeroesRunes.wornRunes.Length;
				while (i < num)
				{
					if (wornRunes.Count <= i)
					{
						panelHeroesRunes.wornRunes[i].enabled = false;
					}
					else
					{
						panelHeroesRunes.wornRunes[i].enabled = true;
						panelHeroesRunes.wornRunes[i].sprite = this.GetSpriteRune(wornRunes[i].id);
						panelHeroesRunes.wornRunes[i].color = this.GetRuneColor(wornRunes[i].belongsTo.id);
					}
					i++;
				}
				panelHeroesRunes.SetSprites(wornRunes.Count);
				int j = 0;
				int num2 = panelHeroesRunes.panelRunes.Length;
				while (j < num2)
				{
					if (list.Count <= j)
					{
						panelHeroesRunes.panelRunes[j].gameObject.SetActive(false);
					}
					else
					{
						Rune rune = list[j];
						panelHeroesRunes.panelRunes[j].gameObject.SetActive(true);
						panelHeroesRunes.panelRunes[j].SetDetails(rune, this.sim.IsRuneBought(rune), this.sim.CanUseRune(rune), this.sim.CanRemoveRune(rune), new Color?(this.GetRuneColor(rune.belongsTo.id)), this.GetSpriteRune(rune.id));
					}
					j++;
				}
			}
		}

		private void UpdateAdPopup(float dt)
		{
			this.panelAdPopup.timer += dt;
		}

		public void UpdateHubDatabaseHeroes()
		{
			this.UpdateHubDatabase();
			HashSet<string> unlockedHeroIds = this.sim.GetUnlockedHeroIds();
			List<HeroDataBase> list = this.SortHeroesByUnlocked(this.sim.GetAllHeroes(), unlockedHeroIds);
			HeroDataBase heroDataBase = list[this.panelHubDatabase.selectedHero];
			if (UiManager.stateJustChanged)
			{
				Dictionary<string, GameMode> boughtHeroIdsWithModes = this.sim.GetBoughtHeroIdsWithModes();
				HashSet<string> newHeroIconSelectedHeroIds = this.sim.newHeroIconSelectedHeroIds;
				for (int i = this.panelHubDatabase.buttonHeroes.Length - 1; i >= 0; i--)
				{
					bool flag = i < list.Count;
					ButtonNewHeroSelect buttonNewHeroSelect = this.panelHubDatabase.buttonHeroes[i];
					if (flag)
					{
						HeroDataBase heroDataBase2 = list[i];
						string id = heroDataBase2.id;
						bool flag2 = unlockedHeroIds.Contains(id);
						bool everSelected = newHeroIconSelectedHeroIds.Contains(id);
						int evolveLevel = list[i].evolveLevel;
						buttonNewHeroSelect.hero = heroDataBase2;
						buttonNewHeroSelect.spriteHeroPortrait = this.GetSpriteHeroPortrait(id);
						bool flag3 = boughtHeroIdsWithModes.ContainsKey(id);
						GameMode mode = (!flag3) ? GameMode.STANDARD : boughtHeroIdsWithModes[id];
						buttonNewHeroSelect.SetButtonState(flag, true, flag2, everSelected, evolveLevel, (!flag3) ? null : this.GetSpriteModeFlag(mode));
						if (!flag2)
						{
							HeroUnlockDescKey heroUnlockDescKey = this.heroUnlockHintKeys[id];
							if (heroUnlockDescKey.isAmountHidden)
							{
								buttonNewHeroSelect.unlockLevelLabel.gameObject.SetActive(false);
							}
							else
							{
								buttonNewHeroSelect.unlockLevelLabel.gameObject.SetActive(true);
								buttonNewHeroSelect.unlockLevelLabel.text = heroUnlockDescKey.amount.ToString();
							}
						}
					}
					else
					{
						buttonNewHeroSelect.SetButtonState(flag, true, true, true, 0, null);
					}
				}
				bool flag4 = unlockedHeroIds.Contains(heroDataBase.id);
				if (flag4)
				{
					this.panelHubDatabase.panelHero.textName.gameObject.SetActive(true);
					this.panelHubDatabase.panelHero.panelHeroClass.gameObject.SetActive(true);
					this.panelHubDatabase.heroUnlockHintParent.SetActive(false);
					if (this._state == UiState.HUB_DATABASE_HEROES_ITEMS)
					{
						this.panelHubDatabase.heroItemsParent.SetActive(true);
						this.panelHubDatabase.panelSkills.gameObject.SetActive(false);
						this.UpdateHeroesGearScreen(heroDataBase, this.panelHubDatabase.panelGear);
					}
					else
					{
						this.panelHubDatabase.heroItemsParent.SetActive(false);
						this.panelHubDatabase.panelSkills.gameObject.SetActive(true);
					}
				}
				else
				{
					this.panelHubDatabase.panelHero.textName.gameObject.SetActive(false);
					this.panelHubDatabase.panelHero.panelHeroClass.gameObject.SetActive(false);
					this.panelHubDatabase.heroItemsParent.SetActive(false);
					this.panelHubDatabase.panelSkills.gameObject.SetActive(false);
					this.panelHubDatabase.heroUnlockHintParent.SetActive(true);
					HeroUnlockDescKey heroUnlockDescKey2 = this.heroUnlockHintKeys[heroDataBase.id];
					this.panelHubDatabase.heroUnlockHintLabel.text = heroUnlockDescKey2.GetString();
					this.panelHubDatabase.panelGear.OnClose();
				}
				this.UpdatePanelHero(heroDataBase, this.panelHubDatabase.panelHero);
			}
			if (this._state == UiState.HUB_DATABASE_HEROES_SKILLS)
			{
				this.panelHubDatabase.panelSkills.UpdatePanel(heroDataBase, this, this.sim);
			}
		}

		public void UpdateHubDatabaseTotems()
		{
			this.UpdateHubDatabase();
			if (UiManager.stateJustChanged)
			{
				Dictionary<TotemDataBase, GameMode> boughtTotems = this.sim.GetBoughtTotems();
				List<TotemDataBase> allTotems = this.sim.allTotems;
				List<bool> list = new List<bool>();
				Dictionary<TotemDataBase, Sprite> dictionary = new Dictionary<TotemDataBase, Sprite>();
				foreach (TotemDataBase totemDataBase in allTotems)
				{
					list.Add(!this.sim.IsTotemUnlocked(totemDataBase.id));
					if (boughtTotems.ContainsKey(totemDataBase))
					{
						dictionary.Add(totemDataBase, this.GetSpriteModeFlag(boughtTotems[totemDataBase]));
					}
				}
				ButtonSelectTotem[] buttonTotems = this.panelHubTotems.buttonTotems;
				int i = 0;
				int num = buttonTotems.Length;
				while (i < num)
				{
					bool flag = allTotems.Count > i;
					ButtonSelectTotem buttonSelectTotem = buttonTotems[i];
					if (flag)
					{
						string id = allTotems[i].id;
						buttonSelectTotem.totem = allTotems[i];
						buttonSelectTotem.spriteIcon = ((!this.spritesTotemSmall.ContainsKey(id)) ? null : this.spritesTotemSmall[id]);
						buttonSelectTotem.locked = list[i];
						buttonSelectTotem.gameButton.interactable = !buttonSelectTotem.locked;
						if (boughtTotems.ContainsKey(allTotems[i]))
						{
							buttonSelectTotem.imageModeFlag.sprite = dictionary[allTotems[i]];
							buttonSelectTotem.imageModeFlag.gameObject.SetActive(true);
						}
						else
						{
							buttonSelectTotem.imageModeFlag.gameObject.SetActive(false);
						}
					}
					else
					{
						buttonSelectTotem.totem = null;
						buttonSelectTotem.gameButton.interactable = false;
						buttonSelectTotem.locked = true;
						buttonSelectTotem.imageModeFlag.gameObject.SetActive(false);
					}
					i++;
				}
			}
			this.UpdateHeroesRunesScreen(this.panelHubTotems.buttonTotems[this.panelHubTotems.selectedTotem].totem, this.panelHubTotems.panelRunes);
		}

		public void UpdateHubDatabaseTrinkets()
		{
			this.UpdateHubDatabase();
			bool flag = this.sim.HasTrinketSmithHint();
			bool hasTrinketSmith = this.sim.hasTrinketSmith;
			bool flag2 = flag && !hasTrinketSmith;
			this.panelHubTrinkets.showCurrency.SetCurrency(CurrencyType.SCRAP, this.sim.GetCurrency(CurrencyType.SCRAP).GetString(), true, GameMode.STANDARD, true);
			if (flag)
			{
				bool flag3 = !this.panelHubTrinkets.tabsParent.gameObject.activeSelf;
				this.panelHubTrinkets.tabsParent.gameObject.SetActive(true);
				if (flag3)
				{
					this.panelHubTrinkets.trinketTabSelf.SetTopDelta(190f);
				}
				this.panelHubTrinkets.smithLockIcon.gameObject.SetActive(flag2);
				this.panelHubTrinkets.buttonTabSmithing.text.enabled = !flag2;
			}
			else
			{
				bool activeSelf = this.panelHubTrinkets.tabsParent.gameObject.activeSelf;
				this.panelHubTrinkets.tabsParent.gameObject.SetActive(false);
				if (activeSelf)
				{
					this.panelHubTrinkets.trinketTabSelf.SetTopDelta(90f);
				}
			}
			this.panelHubTrinkets.showCurrencySmithingTab.SetCurrency(CurrencyType.GEM, this.sim.GetCurrency(CurrencyType.GEM).GetString(), true, GameMode.STANDARD, true);
			if (!this.panelHubTrinkets.isSmithingTab)
			{
				if (UiManager.stateJustChanged)
				{
					this.panelTrinketsScroller.InitScroll(this.sim.numTrinketSlots, new Action<int>(this.OnTrinketSelectedHandler), new Action(this.OnMultipleTrinketDisassemble));
				}
				this.panelTrinketsScroller.UpdateTrinkets(this.sim, this.spritesHeroPortrait);
			}
			else
			{
				PanelTrinketSmithing panelTrinketSmithing = this.panelTrinketSmithing;
				if (!flag2)
				{
					panelTrinketSmithing.bodyParent.gameObject.SetActive(true);
					panelTrinketSmithing.hintParent.gameObject.SetActive(false);
					int num = panelTrinketSmithing.selectedEffects.Count((TrinketEffect te) => te != null);
					if (num > 0)
					{
						double trinketCraftCost = this.sim.GetTrinketCraftCost(num);
						panelTrinketSmithing.buttonCraft.textUp.text = GameMath.GetDoubleString(trinketCraftCost);
						panelTrinketSmithing.buttonCraft.textUpNoCantAffordColorChangeForced = (this.sim.GetCurrency(CurrencyType.GEM).CanAfford(trinketCraftCost) && !this.sim.HasEmptyTrinketSlot());
						panelTrinketSmithing.buttonCraft.gameButton.interactable = (this.sim.HasEmptyTrinketSlot() && this.sim.GetCurrency(CurrencyType.GEM).CanAfford(trinketCraftCost));
					}
				}
				else
				{
					panelTrinketSmithing.textUnlockSmithHint.text = this.sim.GetTrinketSmithHintString();
					panelTrinketSmithing.bodyParent.gameObject.SetActive(false);
					panelTrinketSmithing.hintParent.gameObject.SetActive(true);
				}
			}
		}

		public void UpdateHubDatabase()
		{
			this.panelHubDatabase.menuShowCurrency.SetCurrency(CurrencyType.SCRAP, this.sim.GetScraps().GetString(), true, GameMode.STANDARD, true);
			bool flag = !this.sim.hasEverOwnedATrinket && this.sim.HasTrinketTabHint();
			bool flag2 = this.sim.HasEmptyTrinketSlot();
			if (UiManager.stateJustChanged)
			{
				this.panelDatabaseTrinket.lockedTabIcon.gameObject.SetActive(flag);
				this.panelTrinketsScroller.gameObject.SetActive(this._state == UiState.HUB_DATABASE_TRINKETS && !flag);
			}
			if (flag)
			{
				if (flag2)
				{
					this.panelDatabaseTrinket.trinketBoxIcon.gameObject.SetActive(true);
				}
				else
				{
					this.panelDatabaseTrinket.trinketBoxIcon.gameObject.SetActive(false);
				}
				this.panelDatabaseTrinket.parentNotSelected.gameObject.SetActive(true);
				this.panelDatabaseTrinket.textNotSelected.text = this.sim.GetTrinketHintString();
			}
			else
			{
				this.panelDatabaseTrinket.parentNotSelected.gameObject.SetActive(false);
			}
		}

		public void UpdateDatabaseEvolveScreen()
		{
			if (this.panelHeroEvolve.state == PanelHeroEvolveState.CLOSING && this.panelHeroEvolve.stateJustChanged)
			{
				UiManager.stateJustChanged = true;
				this.UpdateHeroesGearScreen(this.panelHubDatabase.buttonHeroes[this.panelHubDatabase.selectedHero].hero, this.panelHubDatabase.panelGear);
			}
			else if (this.panelHeroEvolve.state == PanelHeroEvolveState.CLOSED)
			{
				this.state = UiState.HUB_DATABASE_HEROES_ITEMS;
			}
		}

		public void UpdateUpdateRequired(float dt)
		{
			this.panelUpdateRequired.timer += dt;
			if (this.panelUpdateRequired.timer < 0.5f)
			{
				this.panelUpdateRequired.canvasGroup.alpha = 0f;
			}
			else if (this.panelUpdateRequired.timer < 0.75f)
			{
				this.panelUpdateRequired.canvasGroup.alpha = 4f * (this.panelUpdateRequired.timer - 0.5f);
			}
			else
			{
				this.panelUpdateRequired.canvasGroup.alpha = 1f;
			}
		}

		public void UpdateGeneralPopup(float dt)
		{
			this.panelGeneralPopup.rectPopup.SetAnchorPosY(125f);
			if (this.panelGeneralPopup.state != PanelGeneralPopup.State.NONE)
			{
				if (this.panelGeneralPopup.state != PanelGeneralPopup.State.OPTIONS)
				{
					if (this.panelGeneralPopup.state == PanelGeneralPopup.State.HARD_RESET)
					{
						if (this.panelGeneralPopup.buttonYes.isDown)
						{
							this.panelGeneralPopup.timer += dt;
							if (this.panelGeneralPopup.timer >= 3f)
							{
								Main.hardReset = true;
								this.state = UiState.HUB_OPTIONS;
								UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
								this.panelGeneralPopup.buttonYes.isDown = false;
							}
						}
						else
						{
							this.panelGeneralPopup.timer = 0f;
						}
						this.panelGeneralPopup.SetViewContinuous();
					}
					else if (this.panelGeneralPopup.state != PanelGeneralPopup.State.MODE)
					{
						if (this.panelGeneralPopup.state != PanelGeneralPopup.State.SHOP)
						{
							if (this.panelGeneralPopup.state == PanelGeneralPopup.State.SERVER_REWARD || this.panelGeneralPopup.state == PanelGeneralPopup.State.SERVER_REWARD_HUB)
							{
								if (Main.rewardToClaim == null && this.sim.rewardsToGive.Count == 0 && Main.GetAvailableNewsCount() < this.currentNewsIndex)
								{
									this.state = ((this.panelGeneralPopup.state != PanelGeneralPopup.State.SERVER_REWARD_HUB) ? UiState.NONE : UiState.HUB);
									this.acceptingServerReward = false;
								}
								else
								{
									this.panelServerReward.timer += dt;
								}
								float y = this.panelGeneralPopup.rectPopup.sizeDelta.y;
								float num = -20f + y / 2f;
								this.panelGeneralPopup.rectPopup.SetAnchorPosY(num);
								this.panelServerReward.popupRect.SetAnchorPosY(-125f + num);
							}
							else if (this.panelGeneralPopup.state != PanelGeneralPopup.State.SELECT_TRINKET)
							{
								if (this.panelGeneralPopup.state != PanelGeneralPopup.State.DATABASE_TRINKET)
								{
									if (this.panelGeneralPopup.state == PanelGeneralPopup.State.HUB_DAILY_SKIP)
									{
										this.panelGeneralPopup.buttonYesCurrency.gameButton.interactable = this.sim.CanSkipDailyQuest();
										this.panelGeneralPopup.buttonYesCurrency.textUp.text = GameMath.GetDoubleString(this.sim.GetSkipDailyCost());
									}
									else if (this.panelGeneralPopup.state != PanelGeneralPopup.State.TRINKET_INFO_POPUP)
									{
										if (this.panelGeneralPopup.state != PanelGeneralPopup.State.CHRISTMAS_SHOP)
										{
											if (this.panelGeneralPopup.state != PanelGeneralPopup.State.DAILY_QUEST)
											{
												throw new NotImplementedException();
											}
											this.panelGeneralPopup.buttonYesCurrency.gameButton.interactable = this.sim.CanSkipDailyQuest();
											this.panelGeneralPopup.buttonYesCurrency.textUp.text = GameMath.GetDoubleString(this.sim.GetSkipDailyCost());
										}
									}
								}
							}
						}
					}
				}
			}
		}

		public void UpdateHeroesTrinkets()
		{
			this.UpdateHeroPanelOnSkillsAndGearScreen(this.selectedHeroGearSkills, this.panelHeroGearSkills);
			HeroDataBase dataBase = this.selectedHeroGearSkills.GetData().GetDataBase();
			bool flag = !this.sim.HasAnyTrinket() && this.sim.HasTrinketTabHint();
			bool flag2 = this.sim.HasEmptyTrinketSlot();
			this.panelTrinketsScreen.buttonEquip.rectTransform.SetAnchorPosY((float)((this.selectedHeroGearSkills.trinket != null) ? 115 : 187));
			if (UiManager.stateJustChanged)
			{
				this.SetHeroTabBarPositions();
				if (this.selectedHeroGearSkills.trinket == null)
				{
					this.panelTrinketsScreen.buttonEquip.text.text = LM.Get("UI_EQUIP");
					this.panelTrinketsScreen.textEquip.text = LM.Get("UI_EQUIP");
					this.panelTrinketsScreen.parentNotSelected.SetActive(true);
					this.panelTrinketsScreen.parentSelected.SetActive(false);
					this.panelTrinketsScreen.buttonCannotEquip.SetAnchorPosY(194f);
				}
				else
				{
					this.panelTrinketsScreen.buttonEquip.text.text = LM.Get("UI_TRINKET_CHANGE");
					this.panelTrinketsScreen.textEquip.text = LM.Get("UI_TRINKET_CHANGE");
					this.panelTrinketsScreen.parentNotSelected.SetActive(false);
					this.panelTrinketsScreen.parentSelected.SetActive(true);
					this.panelTrinketsScreen.buttonCannotEquip.SetAnchorPosY(122f);
				}
			}
			if (dataBase.trinketEquipTimer > 0f)
			{
				this.panelTrinketsScreen.buttonCannotEquip.gameObject.SetActive(true);
				this.panelTrinketsScreen.buttonEquip.gameObject.SetActive(false);
				this.panelTrinketsScreen.SetEquipTimer(dataBase);
			}
			else
			{
				this.panelTrinketsScreen.buttonCannotEquip.gameObject.SetActive(false);
				this.panelTrinketsScreen.buttonEquip.gameObject.SetActive(!flag);
			}
			if (flag)
			{
				if (flag2)
				{
					this.panelTrinketsScreen.trinketBoxIcon.gameObject.SetActive(true);
				}
				else
				{
					this.panelTrinketsScreen.trinketBoxIcon.gameObject.SetActive(false);
				}
				this.panelTrinketsScreen.textNotSelected.text = this.sim.GetTrinketHintString();
				this.panelTrinketsScreen.decorationRect.SetAnchorPosY(0f);
			}
			else
			{
				this.panelTrinketsScreen.trinketBoxIcon.gameObject.SetActive(false);
				this.panelTrinketsScreen.textNotSelected.text = LM.Get("TRINKET_DOESNOT_HAVE");
				this.panelTrinketsScreen.decorationRect.SetAnchorPosY(85f);
			}
			if (this.selectedHeroGearSkills.trinket != null)
			{
				this.panelTrinketsScreen.SetDetails(this.sim, this.selectedHeroGearSkills.trinket);
			}
		}

		public void UpdateSelectTrinket()
		{
			bool flag = this.sim.HasTrinketSmithHint();
			bool hasTrinketSmith = this.sim.hasTrinketSmith;
			bool flag2 = flag && !hasTrinketSmith;
			PanelTrinketSelect panelTrinketSelect = this.panelTrinketSelect;
			panelTrinketSelect.menuShowCurrencyScraps.SetCurrency(CurrencyType.SCRAP, this.sim.GetCurrency(CurrencyType.SCRAP).GetString(), true, GameMode.STANDARD, true);
			panelTrinketSelect.menuShowCurrencyGems.SetCurrency(CurrencyType.GEM, this.sim.GetCurrency(CurrencyType.GEM).GetString(), true, GameMode.STANDARD, true);
			if (flag)
			{
				bool flag3 = !panelTrinketSelect.tabButtonsParent.gameObject.activeSelf;
				panelTrinketSelect.tabButtonsParent.gameObject.SetActive(true);
				if (flag3)
				{
					panelTrinketSelect.trinketScrollerParent.SetTopDelta(135f);
					panelTrinketSelect.trinketSmitherParent.SetTopDelta(135f);
				}
				panelTrinketSelect.buttonSmithLock.gameObject.SetActive(flag2);
				panelTrinketSelect.buttonTabSmith.text.enabled = !flag2;
			}
			else
			{
				bool activeSelf = panelTrinketSelect.tabButtonsParent.gameObject.activeSelf;
				panelTrinketSelect.tabButtonsParent.gameObject.SetActive(false);
				if (activeSelf)
				{
					panelTrinketSelect.trinketScrollerParent.SetTopDelta(40f);
					panelTrinketSelect.trinketSmitherParent.SetTopDelta(40f);
				}
			}
			panelTrinketSelect.buttonTabTrinkets.interactable = panelTrinketSelect.isSmithing;
			panelTrinketSelect.buttonTabSmith.interactable = !panelTrinketSelect.isSmithing;
			if (!panelTrinketSelect.isSmithing)
			{
				if (UiManager.stateJustChanged)
				{
					this.panelTrinketsScroller.InitScroll(this.sim.numTrinketSlots, new Action<int>(this.OnTrinketSelectedHandler), new Action(this.OnMultipleTrinketDisassemble));
				}
				this.panelTrinketsScroller.UpdateTrinkets(this.sim, this.spritesHeroPortrait);
			}
			else
			{
				PanelTrinketSmithing panelTrinketSmithing = this.panelTrinketSmithing;
				if (!flag2)
				{
					panelTrinketSmithing.bodyParent.gameObject.SetActive(true);
					panelTrinketSmithing.hintParent.gameObject.SetActive(false);
					int num = panelTrinketSmithing.selectedEffects.Count((TrinketEffect te) => te != null);
					if (num > 0)
					{
						double trinketCraftCost = this.sim.GetTrinketCraftCost(num);
						panelTrinketSmithing.buttonCraft.textUp.text = GameMath.GetDoubleString(trinketCraftCost);
						panelTrinketSmithing.buttonCraft.textUpNoCantAffordColorChangeForced = (this.sim.GetCurrency(CurrencyType.GEM).CanAfford(trinketCraftCost) && !this.sim.HasEmptyTrinketSlot());
						panelTrinketSmithing.buttonCraft.gameButton.interactable = (this.sim.HasEmptyTrinketSlot() && this.sim.GetCurrency(CurrencyType.GEM).CanAfford(trinketCraftCost));
					}
				}
				else
				{
					panelTrinketSmithing.textUnlockSmithHint.text = this.sim.GetTrinketSmithHintString();
					panelTrinketSmithing.bodyParent.gameObject.SetActive(false);
					panelTrinketSmithing.hintParent.gameObject.SetActive(true);
				}
			}
		}

		public void UpdateMine(float dt)
		{
			this.panelMine.timer += dt;
			if (UiManager.stateJustChanged)
			{
				this.panelMine.buttonMineCollect.gameObject.SetActive(true);
				this.panelMine.currentLevel.transform.parent.gameObject.SetActive(true);
				if (!this.panelMine.dontUpdateLevel)
				{
					this.panelMine.currentLevel.text = string.Format(LM.Get("UI_LEVEL"), this.panelMine.selected.level + 1);
				}
				this.panelMine.buttonMineUpgrade.textDown.text = LM.Get("UI_UPGRADE");
				if (this.panelMine.selected.IsMaxed())
				{
					this.panelMine.nextLevelParent.SetActive(false);
					this.panelMine.iconMaxed.SetActive(true);
					this.panelMine.popupRect.SetSizeDeltaY(950f);
				}
				else
				{
					this.panelMine.nextLevelParent.SetActive(true);
					this.panelMine.iconMaxed.SetActive(false);
					this.panelMine.popupRect.SetSizeDeltaY(1108f);
					this.panelMine.buttonMineUpgrade.iconUpType = ButtonUpgradeAnim.IconType.AEONS;
					this.panelMine.buttonMineUpgrade.textUp.text = GameMath.GetDoubleString(this.panelMine.selected.GetUpgradeCost());
				}
			}
			this.panelMine.buttonMineUpgrade.gameButton.interactable = this.sim.CanUpgradeMine(this.panelMine.selected);
			this.panelMine.menuShowCurrencyAeon.SetCurrency(CurrencyType.AEON, GameMath.GetDoubleString(this.sim.GetCurrency(CurrencyType.AEON).GetAmount()), true, GameMode.STANDARD, true);
			this.panelMine.menuShowCurrency.SetCurrency(this.panelMine.selected.rewardCurrency, GameMath.GetDoubleString(this.sim.GetCurrency(this.panelMine.selected.rewardCurrency).GetAmount()), true, GameMode.STANDARD, true);
			if (this.panelMine.selected is MineScrap)
			{
				MineScrap mineScrap = this.panelMine.selected as MineScrap;
				this.panelMine.bonusAmount.text = "x" + mineScrap.GetGoldFactor();
			}
			else if (this.panelMine.selected is MineToken)
			{
				MineToken mineToken = this.panelMine.selected as MineToken;
				this.panelMine.bonusAmount.text = "x" + mineToken.GetDamageFactor();
			}
			if (this.panelMine.selected.IsMaxed())
			{
				this.panelMine.nextLevelParent.SetActive(false);
				this.panelMine.bonusNextLevelLabel.gameObject.SetActive(false);
			}
			else
			{
				this.panelMine.nextLevelParent.SetActive(true);
				this.panelMine.bonusNextLevelLabel.gameObject.SetActive(true);
				this.panelMine.nextLevelAmountLabel.text = GameMath.GetDoubleString(this.panelMine.selected.GetNextLevelReward());
				if (this.panelMine.selected is MineScrap)
				{
					MineScrap mineScrap2 = this.panelMine.selected as MineScrap;
					this.panelMine.bonusNextLevelAmount.text = "x" + mineScrap2.GetGoldFactorNext();
				}
				else if (this.panelMine.selected is MineToken)
				{
					MineToken mineToken2 = this.panelMine.selected as MineToken;
					this.panelMine.bonusNextLevelAmount.text = "x" + mineToken2.GetDamageFactorNext();
				}
			}
			this.panelMine.amountToCollect.text = GameMath.GetDoubleString(this.panelMine.selected.GetReward());
			if (this.sim.CanCollectMine(this.panelMine.selected))
			{
				this.panelMine.buttonMineCollect.interactable = true;
				if (!this.panelMine.isCd)
				{
					this.panelMine.SetMineStatus(true, this.panelMine.selected);
				}
				if (this.panelMine.buttonMineCollect.rectTransform.sizeDelta.x != 270f)
				{
					this.panelMine.buttonMineCollect.rectTransform.SetSizeDeltaX(270f);
				}
				this.panelMine.buttonMineCollect.text.text = LM.Get("UI_COLLECT");
				this.panelMine.isCd = false;
			}
			else
			{
				this.panelMine.buttonMineCollect.interactable = false;
				if (this.panelMine.isCd)
				{
					this.panelMine.SetMineStatus(false, this.panelMine.selected);
				}
				if (this.panelMine.buttonMineCollect.rectTransform.sizeDelta.x != 430f)
				{
					this.panelMine.buttonMineCollect.rectTransform.SetSizeDeltaX(430f);
				}
				if (TrustedTime.IsReady())
				{
					this.panelMine.buttonMineCollect.text.text = GameMath.GetTimeString(this.sim.GetTimeToCollectMine(this.panelMine.selected));
				}
				else
				{
					this.panelMine.buttonMineCollect.text.text = LM.Get("UI_SHOP_CHEST_0_WAIT");
				}
				this.panelMine.isCd = true;
			}
		}

		private Sprite GetMineSpriteFull(Mine mine)
		{
			if (mine is MineToken)
			{
				return this.uiData.spriteMineTokenFull;
			}
			if (mine is MineScrap)
			{
				return this.uiData.spriteMineScrapFull;
			}
			throw new Exception();
		}

		private Sprite GetMineSpriteEmpty(Mine mine)
		{
			if (mine is MineToken)
			{
				return this.uiData.spriteMineTokenEmpty;
			}
			if (mine is MineScrap)
			{
				return this.uiData.spriteMineScrapEmpty;
			}
			throw new Exception();
		}

		private UiState CheckModeUnlock(UiState stateToChange)
		{
			foreach (World world in this.sim.GetAllWorlds())
			{
				if (!world.IsModeUnlocked() && world.unlockMode.IsReqSatisfied(this.sim))
				{
					this.unlockAboutToBeCollected = world.unlockMode;
					if (this.unlockAboutToBeCollected.HasRewardOfType(typeof(UnlockRewardGameModeCrusade)))
					{
						this.soundManager.LoadUiBundle("sounds/timechallenge-mode");
					}
					if (this.unlockAboutToBeCollected.HasRewardOfType(typeof(UnlockRewardGameModeRift)))
					{
						this.soundManager.LoadUiBundle("sounds/rift-mode");
					}
					return UiState.MODE_UNLOCK_REWARD;
				}
			}
			return stateToChange;
		}

		private void UpdatePanelUnlockAeonDust(PanelUnlock panelUnlock, ChallengeRift challengeRift, bool addRewardString = false)
		{
			panelUnlock.SetDetails(challengeRift, false, addRewardString);
			panelUnlock.SetIconSprite(this.uiData.spriteRewardAeonDustSmall, 1f, null, null);
			panelUnlock.SetProgress(challengeRift.totWave);
		}

		public void UpdatePanelUnlock(PanelUnlock panelUnlock, Unlock unlock, bool addRewardString = false)
		{
			panelUnlock.SetDetails(unlock, unlock.IsReqSatisfied(this.sim), addRewardString);
			panelUnlock.SetCanCollect(unlock.IsReqSatisfied(this.sim), unlock.isCollected);
			if (unlock.HasRewardOfType(typeof(UnlockRewardHero)))
			{
				panelUnlock.SetIconSprite(this.GetSpriteHeroPortrait(unlock.GetHeroId()), 1f, null, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardMerchant)))
			{
				panelUnlock.SetIconSpriteMerchant();
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardCompass)))
			{
				panelUnlock.SetIconSpriteCompass();
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardGameModeCrusade)))
			{
				panelUnlock.SetIconSprite(GameMode.CRUSADE);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardCurrency)))
			{
				UnlockRewardCurrency unlockRewardCurrency = unlock.GetReward() as UnlockRewardCurrency;
				panelUnlock.SetIconSprite(unlockRewardCurrency.currencyType);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardTotem)))
			{
				panelUnlock.SetIconSprite(this.GetSpriteTotemSmall(unlock.GetTotemId()), 0.8f, null, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardRune)))
			{
				Rune rune = (unlock.GetReward() as UnlockRewardRune).GetRune();
				Sprite spriteRune = this.GetSpriteRune(rune.id);
				Color? color = new Color?(this.GetRuneColor(rune.belongsTo.id));
				panelUnlock.SetIconSprite(spriteRune, 1f, color, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardMerchantItem)))
			{
				panelUnlock.SetIconSprite(this.GetSpriteMerchantItem(unlock.GetMerchantItemId()), 0.5f, null, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardMythicalArtifactSlot)))
			{
				panelUnlock.SetIconSprite(this.uiData.spriteMythicalArtifactSlot, 0.55f, null, null);
				if (this.sim.artifactsManager.NumArtifactSlotsMythical == 0)
				{
					panelUnlock.textUnlockReward.text = LM.Get("UNLOCK_REWARD_MYTH_ART_FIRST");
				}
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardTrinketSlots)))
			{
				panelUnlock.SetIconSprite(this.uiData.spriteUnlockTrinketSlot, 0.65f, null, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardTrinketPack)))
			{
				panelUnlock.SetIconSprite(this.uiData.spriteUnlockTrinketPack, 0.45f, null, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardCharmPack)))
			{
				panelUnlock.SetIconSprite(this.uiData.spriteUnlockCharmPack, 1f, null, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardSpecificCharm)))
			{
				UnlockRewardSpecificCharm unlockRewardSpecificCharm = unlock.GetReward() as UnlockRewardSpecificCharm;
				panelUnlock.SetIconSprite(this.spritesCharmEffectIcon[unlockRewardSpecificCharm.ced.BaseData.id], 0.45f, new Color?(UiManager.CHARM_ICON_COLOR_IN_CARD), this.GetCharmCardFace(unlockRewardSpecificCharm.ced.BaseData.charmType));
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardMineScrap)))
			{
				panelUnlock.SetIconSprite(this.uiData.spriteUnlockMineScrap, 0.45f, null, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardMineToken)))
			{
				panelUnlock.SetIconSprite(this.uiData.spriteUnlockMineToken, 0.45f, null, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardDailies)))
			{
				panelUnlock.SetIconSprite(this.uiData.spriteUnlockDailyQuests, 0.5f, null, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardPrestige)))
			{
				panelUnlock.SetIconSprite(this.uiData.spriteUnlockPrestige, 1f, null, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardAeonDust)))
			{
				panelUnlock.SetIconSprite(this.uiData.spriteRewardAeonDustSmall, 1f, null, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardSkillPointsAutoDistribution)))
			{
				panelUnlock.SetIconSpriteSkillPointsAutoDistribution();
			}
			else
			{
				if (!unlock.HasRewardOfType(typeof(UnlockRewardTrinketEnch)))
				{
					throw new NotImplementedException();
				}
				panelUnlock.SetIconSprite(this.uiData.spriteUnlockTrinketSmithing, 0.5f, null, null);
			}
			if (unlock.HasReqOfType(typeof(UnlockReqReachStage)))
			{
				panelUnlock.SetProgress(this.sim.GetMaxStageReached());
			}
			else if (unlock.HasReqOfType(typeof(UnlockReqReachHeroLevel)))
			{
				panelUnlock.SetProgress(this.sim.GetMaxHeroLevelReached());
			}
			else if (unlock.HasReqOfType(typeof(UnlockReqTimeChallenge)))
			{
				panelUnlock.SetProgress(this.sim.GetActiveWorld().activeChallenge.GetTotWave());
			}
			else
			{
				if (!unlock.HasReqOfType(typeof(UnlockReqRiftChallenge)))
				{
					throw new NotImplementedException();
				}
				panelUnlock.SetProgress(this.sim.GetActiveWorld().activeChallenge.GetTotWave());
			}
		}

		private void SetTextToCost(Text text, double cost)
		{
			if (cost == 0.0)
			{
				text.text = LM.Get("UI_SHOP_CHEST_0");
			}
			else
			{
				text.text = GameMath.GetDoubleString(cost);
			}
		}

		private void UpdateGameModeButton(Simulator sim, GameMode mode)
		{
			int gameModeNumCollectedUnlocks = sim.GetGameModeNumCollectedUnlocks(mode);
			int gameModeTotNumUnlocks = sim.GetGameModeTotNumUnlocks(mode);
			ButtonGameMode buttonGameMode = this.GetButtonGameMode(mode);
			World world = sim.GetWorld(mode);
			bool flag = sim.IsActiveMode(mode);
			bool flag2 = sim.IsModeUnlocked(mode);
			bool flag3 = sim.IsModeUnlocked(GameMode.RIFT);
			buttonGameMode.panelGlobalBonus.ornament.sprite = ((!flag3) ? this.uiData.spriteModeInfoOrnamentNormal : this.uiData.spriteModeInfoOrnamentCorpedCorner);
			buttonGameMode.panelGlobalBonus.nonEffectingBonusIcon.gameObject.SetActive(flag3);
			buttonGameMode.gameButton.interactable = (flag2 && !world.isCompleted && !buttonGameMode.unlockAnimationPlaying);
			buttonGameMode.buttonInfo.gameObject.SetActive(flag2);
			if (flag2)
			{
				buttonGameMode.state = ((!flag) ? ButtonGameMode.State.NONE : ButtonGameMode.State.SELECTED);
			}
			else
			{
				buttonGameMode.state = ButtonGameMode.State.LOCKED;
				buttonGameMode.textLock.text = world.unlockMode.GetReqString();
			}
			if (mode == GameMode.STANDARD)
			{
				if (sim.IsGameModeInAction(GameMode.STANDARD))
				{
					StringBuilder stringBuilder = StringExtension.StringBuilder;
					stringBuilder.Append(sim.GetCurrentStage(mode)).Append(" (");
					sim.GetWorld(mode).activeChallenge.GetWaveProgress(stringBuilder).Append(")");
					buttonGameMode.goldString = stringBuilder.ToString();
				}
				else
				{
					buttonGameMode.goldString = LM.Get("TO_PREPERE");
				}
			}
			else if (mode == GameMode.CRUSADE)
			{
				if (world.isCompleted)
				{
					buttonGameMode.goldString = LM.Get("CHALLENGE_COMPLETED");
				}
				else if (sim.IsGameModeInAction(GameMode.CRUSADE))
				{
					TimeChallenge timeChallenge = sim.GetWorld(mode).activeChallenge as TimeChallenge;
					StringBuilder stringBuilder2 = StringExtension.StringBuilder.Append("(");
					timeChallenge.GetWaveProgress(stringBuilder2).Append(") ");
					buttonGameMode.goldString = GameMath.GetLocalizedTimeString(timeChallenge.dur - timeChallenge.timeCounter, stringBuilder2).ToString();
				}
				else
				{
					buttonGameMode.goldString = LM.Get("TO_PREPERE");
				}
			}
			else if (mode == GameMode.RIFT)
			{
				if (world.isCompleted)
				{
					buttonGameMode.goldString = LM.Get("CHALLENGE_COMPLETED");
				}
				else if (sim.IsGameModeInAction(GameMode.RIFT))
				{
					ChallengeRift challengeRift = sim.GetWorld(mode).activeChallenge as ChallengeRift;
					StringBuilder stringBuilder3 = StringExtension.StringBuilder.Append("(");
					challengeRift.GetWaveProgress(stringBuilder3).Append(") ");
					buttonGameMode.goldString = GameMath.GetLocalizedTimeString(challengeRift.dur - challengeRift.timeCounter, stringBuilder3).ToString();
				}
				else
				{
					buttonGameMode.goldString = LM.Get("TO_PREPERE");
				}
				if (sim.hasRiftQuest)
				{
					if (!this.panelHub.isAnimatingRiftPoint)
					{
						if (this.panelHub.riftPointToAnimate != double.NegativeInfinity)
						{
							double riftQuestDustRequired = sim.GetRiftQuestDustRequired();
							double riftQuestDustCollected = sim.riftQuestDustCollected;
							this.panelHub.riftQuestSlider.SetLockState(false);
							this.panelHub.DoFillRiftDustBar(riftQuestDustCollected, riftQuestDustRequired);
						}
						else if (TutorialManager.IsAeonDustUnlocked())
						{
							this.panelHub.riftQuestSlider.SetLockState(false);
							double riftQuestDustRequired2 = sim.GetRiftQuestDustRequired();
							double current = Math.Floor(sim.riftQuestDustCollected);
							this.panelHub.riftQuestSlider.SetSlider(current, riftQuestDustRequired2, false);
							if (sim.IsRiftQuestCompleted())
							{
								this.panelHub.textAeontoCollect.text = GameMath.GetDoubleString(sim.GetTotalAeonRewardFromRiftQuest());
								string hexCode = (!sim.IsRiftQuestRestBonusCapped()) ? "707D1AFF" : "B68000FF";
								this.panelHub.textAeontoCollectFull.text = StringExtension.Concat("(", GameMath.GetDoubleString(sim.GetCurrentRiftQuestStandardReward()), " ", AM.cs(StringExtension.Concat("+", GameMath.GetDoubleString(sim.GetCurrentRiftQuestRestReward())), hexCode), ")");
								if (!this.panelHub.isShowingCollectButton)
								{
									this.panelHub.DoRiftCollectButtonShow();
								}
								buttonGameMode.gameButton.interactable = false;
							}
							else if (this.panelHub.isShowingCollectButton)
							{
								this.panelHub.DoRiftCollectButtonHide();
							}
						}
						else
						{
							this.panelHub.riftQuestSlider.SetLockState(true);
							this.panelHub.riftQuestSlider.SetSlider(0.0, 1.0, false);
							if (this.panelHub.isShowingCollectButton)
							{
								this.panelHub.DoRiftCollectButtonHide();
							}
						}
					}
				}
				else if (this.panelHub.isShowingCollectButton)
				{
					this.panelHub.DoRiftCollectButtonHide();
				}
			}
			if (UiManager.stateJustChanged)
			{
				if (mode == GameMode.CRUSADE)
				{
					if (sim.GetWorld(GameMode.CRUSADE).isCompleted)
					{
						buttonGameMode.text.text = LM.Get("UI_TIME_CHALLENGE");
					}
					else
					{
						string text = string.Format(LM.Get("UI_TIME_CHALLENGE_STAGE"), sim.GetNumTimeChallengesComplete() + 1);
						buttonGameMode.text.text = text;
					}
				}
				else
				{
					buttonGameMode.text.text = UiManager.GetModeName(mode);
				}
				buttonGameMode.barUnlocks.SetScale(1f * (float)gameModeNumCollectedUnlocks / (float)gameModeTotNumUnlocks);
				if (mode == GameMode.CRUSADE)
				{
					if (world.isCompleted)
					{
						buttonGameMode.textBarUnlocks.text = string.Format(LM.Get("UI_HUB_CHALLENGES"), gameModeTotNumUnlocks.ToString(), gameModeTotNumUnlocks.ToString());
						buttonGameMode.barUnlocks.SetScale(1f);
					}
					else
					{
						buttonGameMode.textBarUnlocks.text = string.Format(LM.Get("UI_HUB_CHALLENGES"), gameModeNumCollectedUnlocks.ToString(), gameModeTotNumUnlocks.ToString());
					}
				}
				else if (mode == GameMode.RIFT)
				{
					if (world.isCompleted)
					{
						buttonGameMode.textBarUnlocks.text = string.Format(LM.Get("UI_HUB_CHALLENGES"), gameModeTotNumUnlocks.ToString(), gameModeTotNumUnlocks.ToString());
					}
					else
					{
						buttonGameMode.textBarUnlocks.text = string.Format(LM.Get("UI_HUB_CHALLENGES"), gameModeNumCollectedUnlocks.ToString(), gameModeTotNumUnlocks.ToString());
					}
				}
				else
				{
					buttonGameMode.textBarUnlocks.text = string.Format(LM.Get("UI_HUB_QUESTS"), gameModeNumCollectedUnlocks.ToString(), gameModeTotNumUnlocks.ToString());
				}
			}
		}

		private void SetPanelModeSetupRewardAeonDust(double amount)
		{
			this.panelHubModeSetup.imageRewardBackground.gameObject.SetActive(false);
			this.panelHubModeSetup.imageReward.color = Color.white;
			this.panelHubModeSetup.textReward.text = string.Format(LM.Get("RIFT_AEON_DUSTS"), GameMath.GetDoubleString(amount));
			this.panelHubModeSetup.rewarCart.gameObject.SetActive(false);
			this.panelHubModeSetup.SetIconSprite(this.uiData.spriteRewardAeonDustSmall, 1f, null);
		}

		private void SetPanelModeSetupReward(Unlock unlock)
		{
			if (unlock == null || unlock.isCollected)
			{
				this.panelHubModeSetup.SetAllUnlocksCollected();
			}
			else
			{
				this.SetRewardNormal(unlock);
			}
		}

		private void SetRewardNormal(Unlock unlock)
		{
			this.panelHubModeSetup.imageReward.color = Color.white;
			this.panelHubModeSetup.textReward.text = unlock.GetRewardString();
			this.panelHubModeSetup.imageRewardBackground.gameObject.SetActive(false);
			this.panelHubModeSetup.rewarCart.gameObject.SetActive(false);
			if (unlock.HasRewardOfType(typeof(UnlockRewardHero)))
			{
				this.panelHubModeSetup.SetHeroAnimation(unlock.GetHeroId());
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardMerchant)))
			{
				this.panelHubModeSetup.SetIconSpriteMerchant();
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardCompass)))
			{
				this.panelHubModeSetup.SetIconSpriteCompass();
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardGameModeCrusade)))
			{
				this.panelHubModeSetup.SetIconSprite(GameMode.CRUSADE);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardCurrency)))
			{
				UnlockRewardCurrency unlockRewardCurrency = unlock.GetReward() as UnlockRewardCurrency;
				this.panelHubModeSetup.SetIconSprite(unlockRewardCurrency.currencyType);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardTotem)))
			{
				this.panelHubModeSetup.SetIconSprite(this.GetSpriteTotem(unlock.GetTotemId()), 0.6f, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardRune)))
			{
				Rune rune = (unlock.GetReward() as UnlockRewardRune).GetRune();
				this.panelHubModeSetup.SetIconSprite(this.GetSpriteRune(rune.id), 2.4f, new Color?(this.GetRuneColor(rune.belongsTo.id)));
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardMerchantItem)))
			{
				this.panelHubModeSetup.SetIconSprite(this.GetSpriteMerchantItem(unlock.GetMerchantItemId()), 0.6f, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardMythicalArtifactSlot)))
			{
				this.panelHubModeSetup.SetIconSprite(this.uiData.spriteMythicalArtifactSlot, 1f, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardTrinketSlots)))
			{
				this.panelHubModeSetup.SetIconSprite(this.uiData.spriteUnlockTrinketSlot, 1f, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardTrinketPack)))
			{
				if (this.panelHubModeSetup.mode == GameMode.RIFT)
				{
					this.panelHubModeSetup.SetIconSprite(this.uiData.spriteUnlockTrinketPack, 0.6f, null);
				}
				else
				{
					this.panelHubModeSetup.SetIconSprite(this.uiData.spriteUnlockTrinketPack, 1f, null);
				}
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardCharmPack)))
			{
				this.panelHubModeSetup.SetIconSprite(this.uiData.spriteUnlockCharmPack, 1f, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardSpecificCharm)))
			{
				UnlockRewardSpecificCharm unlockRewardSpecificCharm = unlock.GetReward() as UnlockRewardSpecificCharm;
				this.panelHubModeSetup.imageReward.color = new Color32(247, 222, 160, byte.MaxValue);
				this.panelHubModeSetup.imageRewardBackground.sprite = this.GetCharmCardFace(unlockRewardSpecificCharm.ced.BaseData.charmType);
				this.panelHubModeSetup.imageRewardBackground.gameObject.SetActive(true);
				this.panelHubModeSetup.imageRewardBackground.transform.localScale = Vector3.one * 0.58f;
				this.panelHubModeSetup.SetIconSprite(this.spritesCharmEffectIcon[unlockRewardSpecificCharm.ced.BaseData.id], 0.58f, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardMineScrap)))
			{
				this.panelHubModeSetup.SetIconSprite(this.uiData.spriteUnlockMineScrap, 1f, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardMineToken)))
			{
				this.panelHubModeSetup.SetIconSprite(this.uiData.spriteUnlockMineToken, 1f, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardDailies)))
			{
				this.panelHubModeSetup.SetIconSprite(this.uiData.spriteUnlockDailyQuests, 1f, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardGameModeRift)))
			{
				this.panelHubModeSetup.SetIconSprite(this.uiData.spriteUnlockModeRift, 1f, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardAeonDust)))
			{
				this.panelHubModeSetup.SetIconSprite(this.uiData.spriteRewardAeonDustSmall, 1f, null);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardTrinketEnch)))
			{
				this.panelHubModeSetup.SetIconSprite(this.uiData.spriteUnlockTrinketSmithing, 1f, null);
			}
			else
			{
				if (!unlock.HasRewardOfType(typeof(UnlockRewardSkillPointsAutoDistribution)))
				{
					throw new NotImplementedException();
				}
				this.panelHubModeSetup.SetIconSprite(this.uiData.spriteUnlockRandomHeroAndSkills, 1f, null);
			}
		}

		public static string GetModeName(GameMode gameMode)
		{
			if (gameMode == GameMode.STANDARD)
			{
				return LM.Get("UI_ADVENTURE");
			}
			if (gameMode == GameMode.CRUSADE)
			{
				return LM.Get("UI_TIME_CHALLENGE");
			}
			if (gameMode == GameMode.RIFT)
			{
				return LM.Get("UI_RIFT");
			}
			throw new NotImplementedException();
		}

		public int GetSkillTreeBranchIndex()
		{
			return this.panelSkillsScreen.selectedBranchIndex;
		}

		public int GetSkillTreeSkillIndex()
		{
			return this.panelSkillsScreen.selectedSkillIndex;
		}

		public ButtonGameMode GetButtonGameMode(GameMode mode)
		{
			if (mode == GameMode.STANDARD)
			{
				return this.panelHub.buttonGameModeStandard;
			}
			if (mode == GameMode.CRUSADE)
			{
				return this.panelHub.buttonGameModeCrusade;
			}
			if (mode == GameMode.RIFT)
			{
				return this.panelHub.buttonGameModeRift;
			}
			throw new NotImplementedException();
		}

		public void SetNumberOfPanels(List<PanelLootItem> panelLootItems, int noOfPanels, bool isBig)
		{
			if (noOfPanels <= panelLootItems.Count)
			{
				int i = 0;
				int count = panelLootItems.Count;
				while (i < count)
				{
					panelLootItems[i].gameObject.SetActive(i < noOfPanels);
					i++;
				}
			}
			else
			{
				int count2 = panelLootItems.Count;
				int j = 0;
				int num = count2;
				while (j < num)
				{
					panelLootItems[j].gameObject.SetActive(true);
					j++;
				}
				for (int k = count2; k < noOfPanels; k++)
				{
					GameObject gameObject = panelLootItems[k - 1].gameObject;
					RectTransform component = gameObject.GetComponent<RectTransform>();
					GameObject gameObject2 = panelLootItems[k - 2].gameObject;
					RectTransform component2 = gameObject2.GetComponent<RectTransform>();
					GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>((!isBig) ? this.panelLootItemPrefab : this.panelLootItemBigPrefab);
					RectTransform component3 = gameObject3.GetComponent<RectTransform>();
					component3.SetParent(component.parent);
					component3.localScale = component.localScale;
					component3.anchoredPosition = component.anchoredPosition + (component.anchoredPosition - component2.anchoredPosition);
					gameObject3.name = string.Concat(new string[]
					{
						"PanelLootItem",
						(!isBig) ? string.Empty : "Big",
						" (",
						k.ToString(),
						")"
					});
					panelLootItems.Add(gameObject3.GetComponent<PanelLootItem>());
					this.HandleInstantiate(gameObject3);
				}
			}
		}

		public void PositionPanels(List<PanelLootItem> panelLootItems, float startY)
		{
			float num = panelLootItems[1].rectTransform.anchoredPosition.y - panelLootItems[0].rectTransform.anchoredPosition.y;
			int i = 0;
			int count = panelLootItems.Count;
			while (i < count)
			{
				panelLootItems[i].rectTransform.anchoredPosition = new Vector2(panelLootItems[i].rectTransform.anchoredPosition.x, startY + num * (float)i);
				i++;
			}
		}

		public float GetLastActivePanelY(List<PanelLootItem> panelLootItems)
		{
			int i = 0;
			int count = panelLootItems.Count;
			while (i < count)
			{
				if (!panelLootItems[i].gameObject.activeSelf)
				{
					if (i == 0)
					{
						float num = panelLootItems[1].rectTransform.anchoredPosition.y - panelLootItems[0].rectTransform.anchoredPosition.y;
						return panelLootItems[0].rectTransform.anchoredPosition.y - num;
					}
					return panelLootItems[i - 1].rectTransform.anchoredPosition.y;
				}
				else
				{
					i++;
				}
			}
			return panelLootItems[panelLootItems.Count - 1].rectTransform.anchoredPosition.y;
		}

		public void SetTabBarNotification(int tabButtonIndex, int numNotifications)
		{
			this.tabBarButtons[tabButtonIndex].notificationBadge.numNotifications = ((!this.tabBarButtons[tabButtonIndex].gameButton.interactable) ? 0 : numNotifications);
		}

		public void SetModeSetupButtons()
		{
			Sprite totemSprite = null;
			if (this.panelHubModeSetup.totemDatabase != null)
			{
				totemSprite = this.GetSpriteTotemSmall(this.panelHubModeSetup.totemDatabase.id);
			}
			Sprite[] array = new Sprite[Mathf.Max(0, this.panelHubModeSetup.numHeroesShouldBeSelected)];
			int i = 0;
			int num = array.Length;
			while (i < num)
			{
				if (this.panelHubModeSetup.heroDatabases[i] == null)
				{
					array[i] = null;
				}
				else
				{
					array[i] = this.GetSpriteHeroPortrait(this.panelHubModeSetup.heroDatabases[i].id);
				}
				i++;
			}
			this.panelHubModeSetup.SetSelectedButtons(totemSprite, array);
		}

		private List<HeroDataBase> SortHeroesByUnlocked(List<HeroDataBase> notSorted, HashSet<string> unlockedHeroIds)
		{
			List<HeroDataBase> list = new List<HeroDataBase>();
			int i = 0;
			int count = notSorted.Count;
			while (i < count)
			{
				list.Add(notSorted[i]);
				i++;
			}
			int j = 0;
			int count2 = list.Count;
			while (j < count2)
			{
				if (!unlockedHeroIds.Contains(list[j].id))
				{
					bool flag = false;
					int k = j + 1;
					int count3 = list.Count;
					while (k < count3)
					{
						if (unlockedHeroIds.Contains(list[k].id))
						{
							flag = true;
							HeroDataBase value = list[j];
							list[j] = list[k];
							list[k] = value;
							break;
						}
						k++;
					}
					if (!flag)
					{
						break;
					}
				}
				j++;
			}
			return list;
		}

		private void OpenMenu(UiState newState, bool isHubState, int buttonIndex, int contentIndex, params int[] subContexIndexes)
		{
			if (buttonIndex != -1)
			{
				this.MenuOpenCloseAnim(newState, true, isHubState);
				if (!isHubState)
				{
					if (newState == UiState.MODE_UNLOCKS)
					{
						this.uiBg.GetComponent<Image>().color = this.colorTabBgs[2];
					}
					else if (newState == UiState.RIFT_RUN_CHARMS)
					{
						this.uiBg.GetComponent<Image>().color = this.colorTabBgs[5];
					}
					else
					{
						this.uiBg.GetComponent<Image>().color = this.colorTabBgs[buttonIndex];
					}
				}
			}
			else if (buttonIndex == -1)
			{
				this.MenuOpenCloseAnim(newState, false, isHubState);
			}
			this.openTab = buttonIndex;
			this.SetTabBarButtonsDown(this.openTab);
			this.EnableSingleMenuContent(contentIndex, subContexIndexes);
			if (!isHubState && buttonIndex > -1 && buttonIndex == contentIndex)
			{
				this.topBarIcon.SetImage(buttonIndex);
				this.topBarIcon.gameObject.SetActive(true);
			}
			else if (newState == UiState.RIFT_RUN_CHARMS || this.panelRiftEffectInfo.stateToReturn == UiState.RIFT_RUN_CHARMS)
			{
				this.topBarIcon.SetImage(5);
				RectTransform component = this.topBarIcon.GetComponent<RectTransform>();
				component.sizeDelta *= 0.8f;
				this.topBarIcon.gameObject.SetActive(true);
			}
			else if (newState == UiState.ARTIFACTS_REROLL && this.panelArtifactsRerollWindow.uistateToReturn == UiState.ARTIFACTS)
			{
				this.topBarIcon.SetImage(3);
			}
			else
			{
				this.topBarIcon.gameObject.SetActive(false);
			}
			this.SetMenuHeader(newState, this.openTab, contentIndex);
			this.topLongScreenCurtain.gameObject.SetActive(isHubState);
			if (isHubState && this.sim.GetActiveWorld().gameMode != GameMode.STANDARD)
			{
				this.sim.TrySwitchGameMode(GameMode.STANDARD);
			}
			int keystrokeContextFromState = this.GetKeystrokeContextFromState(newState);
			this.keyStroke.SetActiveContext(keystrokeContextFromState);
		}

		private void MenuOpenCloseAnim(UiState newState, bool isUp, bool isHubState)
		{
			if (isHubState)
			{
				this.scrollViewRect.anchoredPosition = new Vector2(this.scrollViewRect.anchoredPosition.x, -90f);
			}
			else
			{
				this.scrollViewRect.anchoredPosition = new Vector2(this.scrollViewRect.anchoredPosition.x, -90f);
			}
			if ((isUp && !this.isMenuUpForSound) || (isHubState && !this.isMenuHubStateForSound))
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuUp, 1f));
			}
			else if ((!isUp && this.isMenuUpForSound) || (!isHubState && this.isMenuHubStateForSound))
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuDown, 1f));
			}
			else if (this.state == UiState.NONE || newState == UiState.NONE || isHubState == this.isMenuHubStateForSound)
			{
			}
			this.isMenuUpForSound = isUp;
			this.isMenuHubStateForSound = isHubState;
			if (isUp)
			{
				this.uiTabMenuAnim.Open(isHubState);
			}
			else
			{
				this.uiTabMenuAnim.Close();
			}
			if (this.panelRiftEffectInfo.stateToReturn != UiState.RIFT_RUN_CHARMS)
			{
				this.buttonMenuTopBack.gameObject.SetActive(isUp);
			}
		}

		private void EnableSingleMenuContent(int contentIndex, int[] subContentIndexes)
		{
			this.currentGraphicRaycasters.Clear();
			for (int i = this.menuContents.Length - 1; i >= 0; i--)
			{
				if (i == contentIndex)
				{
					this.scrollView.content = this.menuContents[i];
					if (UiManager.zeroScrollViewContentY)
					{
						this.menuContents[i].anchoredPosition = new Vector2(this.menuContents[i].anchoredPosition.x, 0f);
					}
					this.menuContents[i].gameObject.SetActive(true);
					this.currentGraphicRaycasters.Add(this.menuContentsGR[i]);
				}
				else
				{
					this.menuContents[i].gameObject.SetActive(false);
				}
			}
			for (int j = this.menuSubContents.Length - 1; j >= 0; j--)
			{
				if (subContentIndexes.Contains(j))
				{
					this.menuSubContents[j].gameObject.SetActive(true);
					this.currentGraphicRaycasters.Add(this.menuSubContentsGR[j]);
				}
				else
				{
					this.menuSubContents[j].gameObject.SetActive(false);
				}
			}
		}

		public void FitScrollViewContent()
		{
			if (this.isScrollViewLocked)
			{
				this.scrollView.content.anchoredPosition = Vector2.zero;
			}
			else
			{
				ChildrenContentSizeFitter childrenContentSizeFitter = this.scrollView.content.GetComponent<ChildrenContentSizeFitter>();
				if (childrenContentSizeFitter == null)
				{
					childrenContentSizeFitter = this.scrollView.content.gameObject.AddComponent<ChildrenContentSizeFitter>();
				}
				if (this.state == UiState.ARTIFACTS_REROLL)
				{
					childrenContentSizeFitter.SetSize(620f, true);
				}
				else if (this.state != UiState.MODE_UNLOCKS)
				{
					if (this.state == UiState.HEROES)
					{
						RectTransform component = childrenContentSizeFitter.GetComponent<RectTransform>();
						if (this.sim.GetActiveWorld().gameMode != GameMode.STANDARD || !this.sim.hasSkillPointsAutoDistribution)
						{
							component.SetSizeDeltaY(1080f);
						}
						else
						{
							int activeWorldNumHeroes = this.sim.GetActiveWorldNumHeroes();
							if (activeWorldNumHeroes > 2)
							{
								component.SetSizeDeltaY((float)(1200 + 100 * (activeWorldNumHeroes - 2)));
							}
							else
							{
								component.SetSizeDeltaY(1080f);
							}
						}
					}
					else
					{
						childrenContentSizeFitter.SetSize(0f, false);
					}
				}
			}
		}

		private void SetTabBarButtonsDown(int ic)
		{
			for (int i = this.tabBarButtons.Count - 1; i >= 0; i--)
			{
				this.tabBarButtons[i].isDown = (i == ic);
			}
		}

		private void SetMenuHeader(UiState newState, int buttonIndex, int contentIndex)
		{
			if (newState == UiState.RIFT_RUN_CHARMS || this.panelRiftEffectInfo.stateToReturn == UiState.RIFT_RUN_CHARMS)
			{
				this.buttonMenuBack.gameObject.SetActive(false);
			}
			else
			{
				this.buttonMenuBack.gameObject.SetActive(buttonIndex < contentIndex);
			}
		}

		private string GetMenuHeaderText(UiState state)
		{
			string result = string.Empty;
			switch (state)
			{
			case UiState.MODE:
				result = LM.Get("UI_TAB_MODE");
				break;
			case UiState.HEROES:
				result = LM.Get("UI_TAB_HEROES");
				break;
			case UiState.ARTIFACTS:
				result = LM.Get("UI_TAB_ARTIFACTS");
				break;
			case UiState.SHOP:
				result = LM.Get("UI_TAB_SHOP");
				break;
			case UiState.HEROES_GEAR:
				result = LM.Get("UI_TAB_HEROES");
				break;
			case UiState.HEROES_SKILL:
				result = LM.Get("UI_TAB_HEROES");
				break;
			case UiState.HEROES_NEW:
				result = LM.Get("UI_TAB_HEROES");
				break;
			case UiState.MODE_UNLOCKS:
				result = LM.Get("UI_TAB_UNLOCKS");
				break;
			case UiState.ARTIFACTS_CRAFT:
				result = LM.Get("UI_TAB_ARTIFACTS");
				break;
			case UiState.ARTIFACTS_REROLL:
				result = LM.Get("UI_TAB_ARTIFACTS");
				break;
			case UiState.ARTIFACTS_INFO:
				result = LM.Get("UI_TAB_ARTIFACTS");
				break;
			default:
				switch (state)
				{
				case UiState.HEROES_TRINKETS:
					result = LM.Get("UI_TAB_HEROES");
					break;
				case UiState.SELECT_TRINKET:
					result = LM.Get("UI_TAB_TRINKETS");
					break;
				case UiState.HUB_ACHIEVEMENTS:
					result = LM.Get("UI_OPTIONS_ACHIEVEMENTS");
					break;
				default:
					if (state != UiState.RIFT_RUN_CHARMS)
					{
						if (state != UiState.RIFT_EFFECTS_INFO)
						{
							if (state != UiState.POSSIBLE_ARTIFACT_EFFECTS_POPUP)
							{
								result = string.Empty;
							}
							else
							{
								result = "UI_TAB_ARTIFACTS".Loc();
							}
						}
						else if (this.panelRiftEffectInfo.stateToReturn == UiState.RIFT_RUN_CHARMS)
						{
							result = LM.Get("UI_RUNNING_CHARMS_HEADER");
						}
					}
					else
					{
						result = LM.Get("UI_RUNNING_CHARMS_HEADER");
					}
					break;
				}
				break;
			case UiState.MODE_PRESTIGE:
				result = LM.Get("UI_TAB_MODE");
				break;
			case UiState.HEROES_RUNES:
				result = LM.Get("UI_TAB_RUNES");
				break;
			}
			return result;
		}

		public void UpdateMenuCurrencies()
		{
			UiState state = this.state;
			switch (state)
			{
			case UiState.NONE:
				this.menuShowCurrency[0].gameObject.SetActive(false);
				this.menuShowCurrency[1].gameObject.SetActive(false);
				break;
			case UiState.HUB:
				this.menuShowCurrency[0].gameObject.SetActive(false);
				this.menuShowCurrency[1].gameObject.SetActive(false);
				break;
			case UiState.MODE:
				this.menuShowCurrency[0].gameObject.SetActive(true);
				this.menuShowCurrency[0].SetCurrency(CurrencyType.TOKEN, this.sim.GetTokens().GetString(), true, GameMode.STANDARD, true);
				this.menuShowCurrency[1].gameObject.SetActive(false);
				break;
			case UiState.HEROES:
				this.menuShowCurrency[0].gameObject.SetActive(true);
				this.menuShowCurrency[0].SetCurrency(CurrencyType.GOLD, this.sim.GetGold().GetString(), true, this.sim.GetCurrentGameMode(), true);
				this.menuShowCurrency[1].gameObject.SetActive(false);
				break;
			case UiState.ARTIFACTS:
				this.menuShowCurrency[0].gameObject.SetActive(true);
				this.menuShowCurrency[0].SetCurrency(CurrencyType.GEM, this.sim.GetCredits().GetString(), true, GameMode.STANDARD, true);
				this.menuShowCurrency[1].gameObject.SetActive(true);
				this.menuShowCurrency[1].SetCurrency(CurrencyType.MYTHSTONE, GameMath.GetFlooredDoubleString(this.sim.GetMythstones().GetAmount()), true, GameMode.STANDARD, true);
				break;
			case UiState.SHOP:
				this.menuShowCurrency[0].gameObject.SetActive(true);
				this.menuShowCurrency[0].SetCurrency(CurrencyType.GEM, this.sim.GetCredits().GetString(), true, GameMode.STANDARD, true);
				if (this.sim.hasDailies)
				{
					this.menuShowCurrency[1].gameObject.SetActive(true);
					this.menuShowCurrency[1].SetCurrency(CurrencyType.AEON, this.sim.GetAeons().GetString(), true, GameMode.STANDARD, true);
				}
				else
				{
					this.menuShowCurrency[1].gameObject.SetActive(false);
				}
				break;
			case UiState.HEROES_GEAR:
				this.menuShowCurrency[0].gameObject.SetActive(true);
				this.menuShowCurrency[0].SetCurrency(CurrencyType.SCRAP, this.sim.GetScraps().GetString(), true, GameMode.STANDARD, true);
				this.menuShowCurrency[1].gameObject.SetActive(false);
				break;
			case UiState.HEROES_SKILL:
				this.menuShowCurrency[0].gameObject.SetActive(true);
				this.menuShowCurrency[0].SetCurrency(CurrencyType.SCRAP, this.sim.GetScraps().GetString(), true, GameMode.STANDARD, true);
				this.menuShowCurrency[1].gameObject.SetActive(false);
				break;
			case UiState.HEROES_NEW:
				this.menuShowCurrency[0].gameObject.SetActive(true);
				this.menuShowCurrency[0].SetCurrency(CurrencyType.GOLD, this.sim.GetGold().GetString(), true, this.sim.GetCurrentGameMode(), true);
				this.menuShowCurrency[1].gameObject.SetActive(false);
				break;
			case UiState.MODE_UNLOCKS:
				this.menuShowCurrency[0].gameObject.SetActive(true);
				this.menuShowCurrency[0].SetCurrency(CurrencyType.SCRAP, this.sim.GetScraps().GetString(), true, GameMode.STANDARD, true);
				this.menuShowCurrency[1].gameObject.SetActive(true);
				this.menuShowCurrency[1].SetCurrency(CurrencyType.MYTHSTONE, this.sim.GetMythstones().GetString(), true, GameMode.STANDARD, true);
				break;
			default:
				switch (state)
				{
				case UiState.RIFT_RUN_CHARMS:
					this.menuShowCurrency[0].gameObject.SetActive(false);
					this.menuShowCurrency[1].gameObject.SetActive(false);
					break;
				default:
					switch (state)
					{
					case UiState.HEROES_TRINKETS:
						this.menuShowCurrency[0].gameObject.SetActive(true);
						this.menuShowCurrency[0].SetCurrency(CurrencyType.SCRAP, this.sim.GetScraps().GetString(), true, GameMode.STANDARD, true);
						this.menuShowCurrency[1].gameObject.SetActive(false);
						break;
					default:
						if (state != UiState.HEROES_RUNES)
						{
							if (state == UiState.ARTIFACT_SELECTED_POPUP)
							{
								UiState stateToReturn = this.panelArtifactPopup.stateToReturn;
								if (stateToReturn != UiState.HUB_ARTIFACTS)
								{
									if (stateToReturn == UiState.ARTIFACTS)
									{
										this.menuShowCurrency[0].gameObject.SetActive(true);
										this.menuShowCurrency[0].SetCurrency(CurrencyType.GEM, this.sim.GetCredits().GetString(), true, GameMode.STANDARD, true);
										this.menuShowCurrency[1].gameObject.SetActive(true);
										this.menuShowCurrency[1].SetCurrency(CurrencyType.MYTHSTONE, GameMath.GetFlooredDoubleString(this.sim.GetMythstones().GetAmount()), true, GameMode.STANDARD, true);
									}
								}
								else
								{
									this.panelHubartifacts.menuShowCurrencyCredits.SetCurrency(CurrencyType.GEM, this.sim.GetCredits().GetString(), true, GameMode.STANDARD, true);
									this.panelHubartifacts.menuShowCurrencyMythstone.SetCurrency(CurrencyType.MYTHSTONE, GameMath.GetFlooredDoubleString(this.sim.GetMythstones().GetAmount()), true, GameMode.STANDARD, true);
								}
							}
						}
						else
						{
							this.menuShowCurrency[0].gameObject.SetActive(true);
							this.menuShowCurrency[0].SetCurrency(CurrencyType.GOLD, this.sim.GetGold().GetString(), true, GameMode.STANDARD, true);
							this.menuShowCurrency[1].gameObject.SetActive(false);
						}
						break;
					case UiState.HUB_ACHIEVEMENTS:
						this.menuShowCurrency[0].gameObject.SetActive(true);
						this.menuShowCurrency[0].SetCurrency(CurrencyType.GEM, this.sim.GetCredits().GetString(), true, GameMode.STANDARD, true);
						this.menuShowCurrency[1].gameObject.SetActive(true);
						this.menuShowCurrency[1].SetCurrency(CurrencyType.AEON, this.sim.GetAeons().GetString(), true, GameMode.STANDARD, true);
						break;
					}
					break;
				case UiState.HUB_ARTIFACTS:
					this.panelHubartifacts.menuShowCurrencyCredits.SetCurrency(CurrencyType.GEM, this.sim.GetCredits().GetString(), true, GameMode.STANDARD, true);
					this.panelHubartifacts.menuShowCurrencyMythstone.SetCurrency(CurrencyType.MYTHSTONE, GameMath.GetFlooredDoubleString(this.sim.GetMythstones().GetAmount()), true, GameMode.STANDARD, true);
					break;
				case UiState.HUB_SHOP:
					this.panelHubShop.menuShowCurrencyCredits.SetCurrency(CurrencyType.GEM, this.sim.GetCredits().GetString(), true, GameMode.STANDARD, true);
					if (this.sim.hasDailies)
					{
						this.panelHubShop.menuShowCurrencyAeons.gameObject.SetActive(true);
						this.panelHubShop.menuShowCurrencyAeons.SetCurrency(CurrencyType.AEON, this.sim.GetAeons().GetString(), true, GameMode.STANDARD, true);
					}
					else
					{
						this.panelHubShop.menuShowCurrencyAeons.gameObject.SetActive(false);
					}
					break;
				}
				break;
			}
		}

		public void SetScrollViewContentY(float y)
		{
			this.SetScrollViewContentY(this.scrollView.content, y);
		}

		public void SetScrollViewContentYIm(RectTransform content, float y)
		{
			float num = 450f;
			content.SetAnchorPosY(Mathf.Max(0f, -y - num));
		}

		public void SetScrollViewContentY(RectTransform content, float y)
		{
			float num = 450f;
			if (content.anchoredPosition.y > -y - num && content.anchoredPosition.y < -y)
			{
				return;
			}
			UnityEngine.Debug.Log("scrollPosSet");
			content.DOAnchorPosY(Mathf.Max(0f, -y - num), 0.2f, false).SetEase(Ease.OutCirc);
		}

		private void OnClickedNewHero(int index)
		{
			if (this.panelNewHero.selected != index)
			{
				if (this.panelNewHero.selected != -1)
				{
					UiManager.sounds.Add(new SoundEventCancelBy(this.panelNewHero.newHeroButtons[this.panelNewHero.selected].hero.id));
				}
				this.panelNewHero.selected = index;
				if (this.sim.IsHeroUnlocked(this.panelNewHero.newHeroButtons[index].hero.id))
				{
					UiManager.sounds.Add(new SoundEventUiVariedVoiceSimple(this.panelNewHero.newHeroButtons[index].hero.soundVoSelected, this.panelNewHero.newHeroButtons[index].hero.id, 1f));
					this.command = new UiCommandNewHeroIconSelected
					{
						heroId = this.panelNewHero.newHeroButtons[index].hero.id
					};
				}
				UiManager.stateJustChanged = true;
			}
			else
			{
				this.panelNewHero.selected = -1;
				UiManager.stateJustChanged = true;
			}
		}

		private void OnClickedNewHeroDatabase(int index)
		{
			if (this.panelHubDatabase.selectedHero != -1)
			{
				UiManager.sounds.Add(new SoundEventCancelBy(this.panelHubDatabase.buttonHeroes[this.panelHubDatabase.selectedHero].hero.id));
			}
			this.panelHubDatabase.SetSelectedHero(index);
			this.panelHubDatabase.panelSkills.ResetSelectedSkill();
			if (this.sim.IsHeroUnlocked(this.panelHubDatabase.buttonHeroes[index].hero.id))
			{
				UiManager.sounds.Add(new SoundEventUiVariedVoiceSimple(this.panelHubDatabase.buttonHeroes[index].hero.soundVoSelected, this.panelHubDatabase.buttonHeroes[index].hero.id, 1f));
				this.command = new UiCommandNewHeroIconSelected
				{
					heroId = this.panelHubDatabase.buttonHeroes[index].hero.id
				};
			}
			UiManager.stateJustChanged = true;
		}

		private void OnClickedButtonSelectTotemDatabase(int index)
		{
			this.panelHubTotems.selectedTotem = index;
			for (int i = 0; i < this.panelHubDatabase.buttonTotems.Length; i++)
			{
				this.panelHubDatabase.buttonTotems[i].transform.localScale = Vector3.one * ((index != i) ? 1f : 1.1f);
			}
			if (this.panelHubTotems.selectedTotem != index)
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiRingSelect, 1f));
			}
			UiManager.stateJustChanged = true;
		}

		private void OnClickedPanelRuneUse(int index)
		{
			this.command = new UiCommandRuneUse
			{
				runeId = this.panelHeroesRunes.panelRunes[index].runeId
			};
			string id = this.sim.GetActiveWorld().totem.id;
			this.panelHeroesRunes.TweenToSlot(index, this.sim.GetWornRunes()[id].Count);
			TutorialManager.EquippedRune();
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOn, 1f));
		}

		private void OnClickedPanelRuneRemove(int index)
		{
			this.command = new UiCommandRuneRemove
			{
				runeId = this.panelHeroesRunes.panelRunes[index].runeId
			};
			TutorialManager.EquippedRune();
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOff, 1f));
		}

		private void OnClickedPanelRuneUseDatabase(int index)
		{
			this.command = new UiCommandRuneUse
			{
				runeId = this.panelHubDatabase.panelRunes.panelRunes[index].runeId
			};
			string id = this.sim.allTotems[this.panelHubTotems.selectedTotem].id;
			this.panelHubDatabase.panelRunes.TweenToSlot(index, this.sim.GetWornRunes()[id].Count);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOn, 1f));
		}

		private void OnClickedPanelRuneRemoveDatabase(int index)
		{
			this.command = new UiCommandRuneRemove
			{
				runeId = this.panelHubDatabase.panelRunes.panelRunes[index].runeId
			};
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOff, 1f));
		}

		private void SelectSkillTreeUlti(bool isLongPress)
		{
			if (this.sim.skillOneTapUpgrade && this.selectedHeroGearSkills.CanUpgradeSkillUlti())
			{
				this.panelSkillsScreen.SelectSkill(-1, 0);
				this.panelSkillsScreen.UpdateDetails(this.selectedHeroGearSkills.GetNumUnspentSkillPoints());
				this.UpgradeSkillTreeUlti();
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiSkillSelect, 1f));
				this.panelSkillsScreen.ResetSelectedSkill();
			}
			else if (!isLongPress && this.panelSkillsScreen.selectedBranchIndex != -1)
			{
				this.panelSkillsScreen.SelectSkill(-1, 0);
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOn, 1f));
			}
		}

		private void SelectSkillTreeNode(int branchIndex, int skillIndex, bool isLongPres)
		{
			if (this.sim.skillOneTapUpgrade && this.selectedHeroGearSkills.CanUpgradeSkill(branchIndex, skillIndex))
			{
				this.panelSkillsScreen.SelectSkill(branchIndex, skillIndex);
				this.panelSkillsScreen.UpdateDetails(this.selectedHeroGearSkills.GetNumUnspentSkillPoints());
				this.UpgradeSkillTreeNode(branchIndex, skillIndex);
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiSkillSelect, 1f));
				this.panelSkillsScreen.ResetSelectedSkill();
			}
			else if (!isLongPres && (this.panelSkillsScreen.selectedSkillIndex != skillIndex || this.panelSkillsScreen.selectedBranchIndex != branchIndex))
			{
				this.panelSkillsScreen.SelectSkill(branchIndex, skillIndex);
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOn, 1f));
			}
		}

		private void OnClickedUpgradeSkill()
		{
			if (this.GetSkillTreeBranchIndex() == -2)
			{
				return;
			}
			SkillTreeNode skill;
			if (this.GetSkillTreeBranchIndex() == -1)
			{
				this.UpgradeSkillTreeUlti();
				skill = this.selectedHeroGearSkills.GetSkillTree().ulti;
			}
			else
			{
				int skillTreeBranchIndex = this.GetSkillTreeBranchIndex();
				int skillTreeSkillIndex = this.GetSkillTreeSkillIndex();
				this.UpgradeSkillTreeNode(skillTreeBranchIndex, skillTreeSkillIndex);
				skill = this.selectedHeroGearSkills.GetSkillTree().branches[skillTreeBranchIndex][skillTreeSkillIndex];
			}
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiSkillSelect, 1f));
			if (this.selectedHeroGearSkills.GetNumUnspentSkillPoints() < 2 || this.selectedHeroGearSkills.IsSkillNodeMaxMinusOne(this.GetSkillTreeBranchIndex(), this.GetSkillTreeSkillIndex(), this.sim.GetSkillEnhancerGearLevel(skill)))
			{
				this.panelSkillsScreen.ResetSelectedSkill();
			}
		}

		private void UpgradeSkillTreeUlti()
		{
			this.command = new UiCommandUpgradeSkill
			{
				hero = this.selectedHeroGearSkills,
				branchIndex = -1
			};
			this.panelSkillsScreen.OnClickUpgradeSkill();
		}

		private void UpgradeSkillTreeNode(int branchIndex, int skillIndex)
		{
			this.command = new UiCommandUpgradeSkill
			{
				hero = this.selectedHeroGearSkills,
				branchIndex = branchIndex,
				skillIndex = skillIndex
			};
			this.panelSkillsScreen.OnClickUpgradeSkill();
		}

		private void OnChallengeWin()
		{
			this.panelChallengeWin.textNextReward.text = LM.Get("UI_UNLOCK_REWARD");
			World activeWorld = this.sim.GetActiveWorld();
			if (activeWorld.gameMode != GameMode.CRUSADE && activeWorld.gameMode != GameMode.RIFT)
			{
				throw new Exception("Wrong game mode");
			}
			Challenge activeChallenge = activeWorld.activeChallenge;
			if (activeChallenge is ChallengeRift)
			{
				ChallengeRift challengeRift = activeChallenge as ChallengeRift;
				Unlock unlock = challengeRift.unlock;
				this.panelChallengeWin.textTime.text = GameMath.GetTimeString(challengeRift.dur);
				this.panelChallengeWin.textChallenge.text = unlock.GetReqString();
				if (!unlock.isCollected)
				{
					this.panelChallengeWin.SetRewardDetails(unlock, this, true);
				}
				else
				{
					this.panelChallengeWin.textReward.text = LM.Get("RIFT_WON_AEON_REWARD_DESC", GameMath.GetDoubleString(challengeRift.riftPointReward));
					this.panelChallengeWin.heroAnimation.gameObject.SetActive(false);
					this.panelChallengeWin.charmPack.gameObject.SetActive(false);
					this.panelChallengeWin.currencyCart.gameObject.SetActive(false);
					this.panelChallengeWin.imageRewardBackground.gameObject.SetActive(false);
					this.panelChallengeWin.imageReward.gameObject.SetActive(true);
					this.panelChallengeWin.imageReward.color = Color.white;
					this.panelChallengeWin.imageReward.transform.localScale = new Vector3(1f, 1f, 1f);
					this.panelChallengeWin.imageReward.sprite = UiData.inst.spriteRewardAeonDust;
					this.panelChallengeWin.imageReward.SetNativeSize();
					this.panelChallengeWin.ShowShareButton();
				}
			}
			else
			{
				if (!(activeChallenge is ChallengeWithTime))
				{
					throw new NotImplementedException();
				}
				this.panelChallengeWin.textSecondaryReward.gameObject.SetActive(false);
				ChallengeWithTime challengeWithTime = activeChallenge as ChallengeWithTime;
				Unlock unlock2 = challengeWithTime.unlock;
				this.panelChallengeWin.textTime.text = GameMath.GetTimeString(challengeWithTime.dur);
				this.panelChallengeWin.textChallenge.text = unlock2.GetReqString();
				this.panelChallengeWin.SetRewardDetails(unlock2, this, true);
				int num = activeWorld.allChallenges.IndexOf(activeChallenge);
				PlayfabManager.SendPlayerEvent(PlayfabEventId.TIME_CHALLENGE_COMPLETED, new Dictionary<string, object>
				{
					{
						"index",
						num
					},
					{
						"max_stage_prestiged",
						this.sim.maxStagePrestigedAt
					}
				}, null, null, true);
			}
		}

		private void OnChallengeLose()
		{
			World activeWorld = this.sim.GetActiveWorld();
			Challenge activeChallenge = this.sim.GetActiveWorld().activeChallenge;
			if (activeChallenge is ChallengeWithTime)
			{
				this.panelChallengeLose.SetProgress(activeChallenge.GetTotWave(), activeChallenge.GetTargetTotWave());
				return;
			}
			throw new NotImplementedException();
		}

		private void OnClickedChallengeWinCollect()
		{
			if (this.panelChallengeWin.timer < 1f)
			{
				return;
			}
			this.command = new UiCommandChallengeWinLose
			{
				isWin = true
			};
			this.panelChallengeWin.FadeOut();
			if (this.sim.IsActiveMode(GameMode.RIFT))
			{
				ChallengeRift challengeRift = this.sim.GetWorld(GameMode.RIFT).activeChallenge as ChallengeRift;
				if (challengeRift.unlock.isCollected)
				{
					this.panelHub.riftPointToAnimate = challengeRift.riftPointReward;
					this.sim.GetWorld(GameMode.RIFT).AddEarnedRiftPoints(challengeRift.riftPointReward);
				}
				if (challengeRift.unlock.GetReward() is UnlockRewardAeonDust)
				{
					UnlockRewardAeonDust unlockRewardAeonDust = challengeRift.unlock.GetReward() as UnlockRewardAeonDust;
					this.panelHub.riftPointToAnimate = unlockRewardAeonDust.GetAmount();
				}
			}
			this.panelChallengeWin.fadingOut = true;
			this.panelChallengeWin.timer = 0.75f;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiUnlockPopup, 1f));
		}

		private void OnClickedChallengeLoseOk()
		{
			if (this.panelChallengeLose.timer < 1f)
			{
				return;
			}
			UiCommandChallengeWinLose uiCommandChallengeWinLose = new UiCommandChallengeWinLose();
			this.loadingTransition.abandonOrFailed = true;
			uiCommandChallengeWinLose.isWin = false;
			this.command = uiCommandChallengeWinLose;
			this.panelChallengeLose.FadeOut(delegate
			{
				this.state = UiState.NONE;
			});
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
		}

		public void OnClickedGameMode(GameMode mode)
		{
			if (this.sim.GameModeShouldStartWithoutSetup(mode))
			{
				this.sim.TryDefaultSetupChallenge(mode);
			}
			bool flag = this.sim.IsGameModeAlreadySetup(mode);
			if (flag)
			{
				if (this.state == UiState.HUB_ACHIEVEMENTS)
				{
					this.command = new UiCommandGameModeSwitch
					{
						toMode = mode
					};
					this.loadingTransition.DoTransition(UiState.NONE, 0f, 0f);
				}
				else
				{
					Sequence t = this.panelHub.DoFadeOut(delegate
					{
						UiCommandGameModeSwitch uiCommandGameModeSwitch = new UiCommandGameModeSwitch();
						uiCommandGameModeSwitch.toMode = mode;
						this.command = uiCommandGameModeSwitch;
					});
					float loadingDelay = t.Duration(true);
					this.loadingTransition.DoTransition(UiState.NONE, 0f, loadingDelay);
				}
			}
			else
			{
				if (mode == GameMode.STANDARD || mode == GameMode.SOLO)
				{
					this.panelHubModeSetup.SetForNormal();
					World world = this.sim.GetWorld(mode);
					Challenge activeChallenge = world.activeChallenge;
					this.panelHubModeSetup.SetMode(mode, activeChallenge.hasRing, activeChallenge.numHeroesMin, this.sim);
					this.panelHubModeSetup.teamSelectPanel.SetActive(true);
					this.panelHubModeSetup.challengePanel.SetActive(true);
					Unlock nextUnhiddenUnlock = world.GetNextUnhiddenUnlock();
					this.panelHubModeSetup.textChallenge.text = nextUnhiddenUnlock.GetReqString();
					this.panelHubModeSetup.textNextReward.text = LM.Get("UI_UNLOCK_NEXT_REWARD");
					this.SetPanelModeSetupReward(nextUnhiddenUnlock);
				}
				else if (mode == GameMode.CRUSADE)
				{
					this.panelHubModeSetup.SetForNormal();
					World world2 = this.sim.GetWorld(mode);
					this.panelHubModeSetup.textNextReward.text = LM.Get("UI_UNLOCK_REWARD");
					Challenge activeChallenge2 = world2.activeChallenge;
					if (!(activeChallenge2 is ChallengeWithTime))
					{
						throw new NotImplementedException();
					}
					ChallengeWithTime challengeWithTime = (ChallengeWithTime)activeChallenge2;
					Unlock unlock = challengeWithTime.unlock;
					if (unlock == null)
					{
						this.panelHubModeSetup.SetMode(mode, false, -1, this.sim);
						this.panelHubModeSetup.teamSelectPanel.SetActive(false);
						this.panelHubModeSetup.challengePanel.SetActive(false);
					}
					else
					{
						this.panelHubModeSetup.SetMode(mode, activeChallenge2.hasRing, activeChallenge2.numHeroesMin, this.sim);
						this.panelHubModeSetup.teamSelectPanel.SetActive(true);
						this.panelHubModeSetup.challengePanel.SetActive(true);
						this.panelHubModeSetup.textChallenge.text = string.Format(LM.Get("UI_UNLOCK_DEFEAT_WAVES"), activeChallenge2.GetTargetTotWave().ToString());
						this.panelHubModeSetup.textTime.text = GameMath.GetTimeString(challengeWithTime.dur);
					}
					this.SetPanelModeSetupReward(unlock);
				}
				else
				{
					if (mode != GameMode.RIFT)
					{
						throw new NotImplementedException();
					}
					if (this.panelHub.isAnimatingRiftPoint)
					{
						this.panelHub.dustBarAnim.Complete(true);
						return;
					}
					World world3 = this.sim.GetWorld(mode);
					this.panelHubModeSetup.textNextReward.text = LM.Get("UI_UNLOCK_REWARD");
					Challenge activeChallenge3 = world3.activeChallenge;
					this.panelHubModeSetup.SetForRift();
					if (!(activeChallenge3 is ChallengeRift))
					{
						throw new NotImplementedException();
					}
					ChallengeRift challengeRift = (ChallengeRift)activeChallenge3;
					Unlock unlock2 = challengeRift.unlock;
					if (challengeRift.IsCursed())
					{
						this.panelHubModeSetup.selectedRiftNo.text = (challengeRift.riftData.cursesSetup.originalRiftNo + 1).ToString();
					}
					else
					{
						this.panelHubModeSetup.selectedRiftNo.text = (world3.GetActiveChallengeIndex() + 1).ToString();
					}
					this.panelHubModeSetup.SetRiftEffects(challengeRift, this);
					this.panelHubModeSetup.SetMode(mode, activeChallenge3.hasRing, activeChallenge3.numHeroesMin, this.sim);
					this.panelHubModeSetup.teamSelectPanel.SetActive(true);
					this.panelHubModeSetup.challengePanel.SetActive(true);
					this.panelHubModeSetup.textChallenge.text = string.Format(LM.Get("UI_UNLOCK_DEFEAT_WAVES"), activeChallenge3.GetTargetTotWave().ToString());
					this.panelHubModeSetup.textTime.text = GameMath.GetTimeString(challengeRift.dur);
					if (unlock2.isCollected)
					{
						this.SetPanelModeSetupRewardAeonDust(challengeRift.riftPointReward);
					}
					else
					{
						this.SetPanelModeSetupReward(unlock2);
					}
				}
				this.state = UiState.HUB_MODE_SETUP;
			}
			if (mode == GameMode.RIFT)
			{
				this.tabBarButtons[3].text.text = LM.Get("UI_CHARMS");
			}
			else
			{
				this.tabBarButtons[3].text.text = LM.Get("UI_TAB_ARTIFACTS");
			}
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiInteractModeButton, 1f));
		}

		private void OnClickedBossButton(bool fightBoss)
		{
			this.command = new UiCommandBoss();
		}

		private void OnClickedPrestige(bool isMega)
		{
			this.command = new UiCommandPrestige
			{
				isMega = isMega
			};
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPrestigeActivated, 1f));
			UiManager.sounds.Add(new SoundEventUiVariedVoiceSimple(SoundArchieve.inst.voGreenManPrestige, "GREEN_MAN", 1f));
			this.state = UiState.NONE;
		}

		private void OnClickedOpenNewHeroPanel()
		{
			this.state = UiState.HEROES_NEW;
		}

		private void OnClickedBuyRandomHero()
		{
			HeroDataBase randomHeroUnlockedAndNotBought = this.sim.GetRandomHeroUnlockedAndNotBought();
			if (randomHeroUnlockedAndNotBought != null)
			{
				this.command = new UiCommandBuyNewHero
				{
					heroId = randomHeroUnlockedAndNotBought.id,
					loadHeroMainAssets = true,
					panelNewHero = this.panelNewHero
				};
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiNewHeroBuy, 1f));
			}
		}

		private void OnSlidedDebugSpeedGame()
		{
			int num = (int)this.sliderDebugSpeedGame.value;
			float num2 = Cheats.DEBUG_TIME_SCALES[num];
			this.textDebugSpeedGame.text = "GAME SPEED:\n x" + num2;
			Cheats.timeScale = num2;
		}

		private void OnClickedHeroNewMenuClose()
		{
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
			if (this.state == UiState.HUB_MODE_SETUP_HERO)
			{
				this.state = UiState.HUB_MODE_SETUP;
			}
			else
			{
				if (this.state != UiState.HEROES_NEW)
				{
					throw new NotImplementedException();
				}
				this.state = UiState.HEROES;
			}
		}

		private void OnClickedBuyNewHero()
		{
			this.panelNewHero.inputBlocker.SetActive(true);
			this.panelNewHero.heroAnimation.ignoreNextOnClose = true;
			UiCommandBuyNewHero uiCommandBuyNewHero = new UiCommandBuyNewHero();
			int selected = this.panelNewHero.selected;
			HeroDataBase hero = this.panelNewHero.newHeroButtons[selected].hero;
			uiCommandBuyNewHero.heroId = hero.id;
			uiCommandBuyNewHero.panelNewHero = this.panelNewHero;
			this.command = uiCommandBuyNewHero;
			this.state = UiState.NONE;
			hero.equippedSkin = this.sim.GetHeroSkins(hero.id).Find((SkinData skin) => skin.index == this.panelNewHero.heroAnimation.lastSkinIndex);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiNewHeroBuy, 1f));
		}

		private void OnClickedSelectNewHero()
		{
			HeroDataBase hero = this.panelNewHero.newHeroButtons[this.panelNewHero.selected].hero;
			hero.equippedSkin = this.sim.GetHeroSkins(hero.id).Find((SkinData skin) => skin.index == this.panelNewHero.heroAnimation.lastSkinIndex);
			this.panelHubModeSetup.OnClickedSelectNewHero(hero);
			this.state = UiState.HUB_MODE_SETUP;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiNewHeroBuy, 1f));
		}

		private void OnClickedSelectTotem()
		{
			if (this.sim.allTotems.Count == 0 || this.panelSelectTotem.selected < 0)
			{
				return;
			}
			if (this.panelSelectTotem.selected >= this.sim.allTotems.Count)
			{
				this.panelSelectTotem.selected = this.sim.allTotems.Count - 1;
			}
			TotemDataBase totemDatabase = this.sim.allTotems[this.panelSelectTotem.selected];
			this.panelHubModeSetup.OnClickedSelectTotem(totemDatabase);
			this.state = UiState.HUB_MODE_SETUP;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiNewHeroBuy, 1f));
		}

		private void OnClickedButtonSkill(int buttonIndex)
		{
			if (!this.sim.GetActiveWorld().isRainingGlory && !this.sim.GetActiveWorld().isRainingDuck && this.sim.GetActiveWorld().activeChallenge.state == Challenge.State.ACTION)
			{
				UiCommandSkill uiCommandSkill = new UiCommandSkill();
				this.command = uiCommandSkill;
				uiCommandSkill.index = buttonIndex;
				this.buttonSkillAnims[buttonIndex].PressedButton();
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiActivateSkill, 1f));
			}
			else
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiDefaultFailClick, 1f));
			}
		}

		public void OnClickedButtonTabBar(int buttonIndex)
		{
			UiState uiState = this.tabBarButtons[buttonIndex].uiState;
			if (uiState == this.state)
			{
				this.state = UiState.NONE;
			}
			else if (uiState == UiState.HUB)
			{
				SoundEventCancelAll item = new SoundEventCancelAll();
				UiManager.sounds.Add(item);
				this.loadingTransition.DoTransition(uiState, 0f, 0f);
				this.sim.GetActiveWorld().shouldSoftSave = true;
			}
			else
			{
				if (this.state != UiState.NONE)
				{
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiOpenTabClick, 1f));
				}
				this.state = uiState;
				if (uiState == UiState.SHOP)
				{
					this.panelShop.ResetScrollPosition();
					this.panelShop.isLookingAtOffers = false;
				}
			}
		}

		private void OnClickedUpgradeTotem()
		{
			this.command = new UiCommandUpgradeTotem();
			this.panelHeroes.panelTotem.OnUpgraded();
		}

		private void OnClickedOpenSelectTotem()
		{
			this.state = UiState.HUB_MODE_SETUP_TOTEM;
		}

		private void OnClickedBuyWorldUpgrade()
		{
			World activeWorld = this.sim.GetActiveWorld();
			if (activeWorld.activeChallenge is ChallengeRift)
			{
				this.panelCharmSelect.isCurseInfo = false;
				this.state = UiState.CHARM_SELECTING;
			}
			else
			{
				this.command = new UiCommandBuyWorldUpgrade();
				this.panelHeroes.panelWorldUpgrade.OnUpgrade();
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiWorldUpgrade, 1f));
			}
		}

		private void OnClickedHeroUpgrade(int index)
		{
			this.command = new UiCommandUpgradeHero
			{
				index = index
			};
			this.panelHeroes.heroPanels[index].OnUpgraded();
		}

		private void OnClickedHeroPortrait(int index)
		{
			this.selectedHeroGearSkills = this.sim.GetActiveWorld().heroes[index];
			int num = 0;
			if (this.sim.IsThereAnySkinBought())
			{
				num = this.sim.GetHerosNewSkinCount(this.selectedHeroGearSkills.GetId()) + ((!this.sim.isSkinsEverClicked) ? 1 : 0);
			}
			if (num > 0)
			{
				this.state = UiState.HEROES_GEAR;
			}
			else if (this.selectedHeroGearSkills.GetNumUnspentSkillPoints() > 0)
			{
				this.state = UiState.HEROES_SKILL;
			}
			else
			{
				this.state = UiState.HEROES_GEAR;
			}
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiOpenMenu, 1f));
		}

		public void OnClickedModeSetupComplete()
		{
			UICommandCompleteModeSetup uicommandCompleteModeSetup = new UICommandCompleteModeSetup();
			uicommandCompleteModeSetup.mode = this.panelHubModeSetup.mode;
			uicommandCompleteModeSetup.totemDatabase = this.panelHubModeSetup.totemDatabase;
			uicommandCompleteModeSetup.heroDatabases = this.panelHubModeSetup.heroDatabases;
			this.command = uicommandCompleteModeSetup;
			if (uicommandCompleteModeSetup.mode == GameMode.RIFT)
			{
				World world = this.sim.GetWorld(GameMode.RIFT);
				ChallengeRift challengeRift = world.activeChallenge as ChallengeRift;
				if (challengeRift.IsCursed())
				{
					this.sim.isActiveWorldPaused = true;
					this.willIntroduceCurses = true;
					this.panelTopHudChallengeTimeWave.initializeCursesWidgets = true;
				}
			}
			this.loadingTransition.DoTransition(UiState.NONE, 0f, 0f);
		}

		private void OnClickedSelectHeroModeSetup(int index)
		{
			this.state = UiState.HUB_MODE_SETUP_HERO;
			this.panelHubModeSetup.heroDatabases[index] = null;
			this.panelHubModeSetup.whichHeroButtonSelected = index;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
		}

		private void OnClickedMenuBack()
		{
			if (this.openTab >= 0)
			{
				this.state = this.openTab + UiState.HUB;
			}
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
		}

		private void OnClickedGearUpgradeButton(int ic)
		{
			UiCommandUpgradeGear uiCommandUpgradeGear = new UiCommandUpgradeGear();
			uiCommandUpgradeGear.gear = this.sim.GetBoughtGearFromData(this.panelGearScreen.panelGears[ic].gear);
			this.command = uiCommandUpgradeGear;
			UiManager.sounds.Add(new SoundEventUiVariedVoiceSimple(uiCommandUpgradeGear.gear.data.belongsTo.soundVoItem, uiCommandUpgradeGear.gear.data.belongsTo.id, 1f));
		}

		private void OnClickedGearUpgradeButtonDatabase(int ic)
		{
			UiCommandUpgradeGear uiCommandUpgradeGear = new UiCommandUpgradeGear();
			uiCommandUpgradeGear.gear = this.sim.GetBoughtGearFromData(this.panelHubDatabase.panelGear.panelGears[ic].gear);
			this.command = uiCommandUpgradeGear;
			UiManager.sounds.Add(new SoundEventUiVariedVoiceSimple(uiCommandUpgradeGear.gear.data.belongsTo.soundVoItem, uiCommandUpgradeGear.gear.data.belongsTo.id, 1f));
		}

		private void OnClickedGearEvolveButton()
		{
			if (this.panelGearScreen.buttonEvolve.openWarningPopup)
			{
				HeroDataBase heroDataBase = this.sim.GetHeroDataBase(this.GetHeroIdOnScreen());
				if (!this.sim.CanHeroEvolve(heroDataBase))
				{
					int evolveLevel = heroDataBase.evolveLevel;
					string extraString = UiManager.GearLevelString(evolveLevel);
					this.panelCurrencyWarning.SetCurrency(PanelCurrencyWarning.PopupType.EVOLVE, this, extraString, evolveLevel, null);
				}
				else
				{
					this.panelCurrencyWarning.SetCurrency(PanelCurrencyWarning.PopupType.SCRAPS, this, string.Empty, 0, null);
				}
				this.state = UiState.CURRENCY_WARNING;
			}
			else
			{
				this.command = new UiCommandEvolveHero
				{
					heroId = this.GetHeroIdOnScreen()
				};
				this.state = UiState.HEROES_EVOLVE;
				Hero hero = this.selectedHeroGearSkills;
				this.panelHeroEvolveSkin.skinsToBeShowed = this.sim.GetSkinDataToBeUnlocked(hero.GetData().GetDataBase(), hero.GetData().GetDataBase().evolveLevel, hero.GetData().GetDataBase().evolveLevel + 1);
				this.panelHeroEvolveSkin.DoEvolve(hero.GetId(), hero.GetEquippedSkinIndex(), hero.GetEvolveLevel());
			}
		}

		private void OnClickedDatabaseEvolveButton()
		{
			if (this.panelHubDatabase.panelGear.buttonEvolve.openWarningPopup)
			{
				HeroDataBase hero = this.panelHubDatabase.buttonHeroes[this.panelHubDatabase.selectedHero].hero;
				if (this.sim.CanAffordHeroEvolve(hero))
				{
					int evolveLevel = hero.evolveLevel;
					string extraString = string.Concat(new string[]
					{
						"<color=",
						GameMath.GetColorString(UiManager.colorHeroLevels[evolveLevel]),
						">",
						UiManager.GearLevelString(evolveLevel),
						"</color>"
					});
					this.panelCurrencyWarning.SetCurrency(PanelCurrencyWarning.PopupType.EVOLVE, this, extraString, evolveLevel, null);
				}
				else
				{
					this.panelCurrencyWarning.SetCurrency(PanelCurrencyWarning.PopupType.SCRAPS, this, string.Empty, 0, null);
				}
				this.state = UiState.CURRENCY_WARNING;
			}
			else
			{
				this.command = new UiCommandEvolveHero
				{
					heroId = this.panelHubDatabase.buttonHeroes[this.panelHubDatabase.selectedHero].hero.id
				};
				this.state = UiState.HUB_DATABASE_EVOLVE;
				HeroDataBase hero2 = this.panelHubDatabase.buttonHeroes[this.panelHubDatabase.selectedHero].hero;
				this.panelHeroEvolveSkin.skinsToBeShowed = this.sim.GetSkinDataToBeUnlocked(hero2, hero2.evolveLevel, hero2.evolveLevel + 1);
				this.panelHeroEvolveSkin.DoEvolve(hero2.GetId(), hero2.equippedSkin.index, hero2.evolveLevel);
			}
		}

		private void OnClickedArtifactCraft()
		{
			bool isLookingAtMythical = this.panelArtifactScroller.isLookingAtMythical;
			if (this.panelArtifactScroller.craftButton.openWarningPopup)
			{
				if (!isLookingAtMythical && !this.sim.artifactsManager.AreThereEmptyArtifactSlots())
				{
					this.panelCurrencyWarning.SetCurrency(PanelCurrencyWarning.PopupType.NO_ARTIFACT_SLOTS, this, string.Empty, 0, null);
				}
				else if (isLookingAtMythical && !this.sim.artifactsManager.HaveRoomToCraftMythicalArtifact())
				{
					this.panelCurrencyWarning.SetCurrency(PanelCurrencyWarning.PopupType.NO_MYTHICAL_ARTIFACT_SLOTS, this, string.Empty, 0, null);
				}
				else
				{
					this.panelCurrencyWarning.SetCurrency(PanelCurrencyWarning.GetPopupTypeForCurrency(CurrencyType.MYTHSTONE), this, string.Empty, 0, null);
				}
				this.state = UiState.CURRENCY_WARNING;
			}
			else
			{
				this.panelArtifactsCraft.isMythical = isLookingAtMythical;
				this.panelArtifactsCraft.skipArtifactShow = false;
				this.command = new UiCommandArtifactCraft
				{
					isMythical = isLookingAtMythical
				};
				this.state = UiState.ARTIFACTS_CRAFT;
				this.panelArtifactScroller.PlayArtifactCraftAnim();
			}
		}

		private void OnClickedArtifactReroll()
		{
			if (this.panelArtifactScroller.isLookingAtMythical)
			{
				try
				{
					Simulation.Artifact artifact = this.sim.artifactsManager.MythicalArtifacts[this.panelArtifactScroller.selectedArtifactIndex];
					bool flag = artifact.CanUpgrade() && this.sim.GetMythstones().CanAfford(artifact.GetUpgradeCost());
					if (flag)
					{
						this.command = new UiCommandArtifactUpgrade
						{
							index = this.panelArtifactScroller.selectedArtifactIndex,
							jumpCount = this.panelArtifactScroller.mythicalArtifactLevelJump
						};
					}
					else
					{
						this.panelCurrencyWarning.SetCurrency(PanelCurrencyWarning.PopupType.MYTHSTONES, this, string.Empty, 0, null);
						this.state = UiState.CURRENCY_WARNING;
					}
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiWorldUpgrade, 1f));
				}
				catch (ArgumentOutOfRangeException ex)
				{
					throw new ArgumentOutOfRangeException(ex.ParamName, "Selected artifact index value was: " + this.panelArtifactScroller.selectedArtifactIndex);
				}
			}
		}

		private void OnClickedArtifactsInfo()
		{
			this.state = UiState.ARTIFACTS_INFO;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
		}

		private void OnClickedArtifactsUnlockSlot()
		{
			if (this.panelArtifactScroller.unlockButtonWaitingForConfirm)
			{
				this.panelArtifactScroller.unlockButtonWaitingForConfirm = false;
				this.panelArtifactScroller.buttonUnlockSlot.SetAsNormal();
				UiCommandArtifactsUnlockSlot command = new UiCommandArtifactsUnlockSlot();
				this.command = command;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiArtifactSlotUnlock, 1f));
				UiManager.sounds.Add(new SoundEventUiVariedVoiceSimple(SoundArchieve.inst.voAlchemistSlotUnlock, "ALCHEMIST", 1f));
			}
			else
			{
				this.panelArtifactScroller.unlockButtonWaitingForConfirm = true;
				UiManager.stateJustChanged = true;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiArtifactSlotSelect, 1f));
			}
		}

		private void OnClickedArtifactsEnableDisable()
		{
			Simulation.Artifact artifact = this.sim.artifactsManager.MythicalArtifacts[this.panelArtifactScroller.selectedArtifactIndex];
			if (artifact.CanBeDisabled())
			{
				this.command = new UiCommandEnableDisableArtifact
				{
					artifact = artifact
				};
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			}
		}

		private void OnClickedCollectAchievement(int index)
		{
			PanelAchievement panelAchievement = this.panelAchievements.panels[index];
			DropPosition dropPosition = new DropPosition
			{
				startPos = panelAchievement.buttonCollect.transform.position,
				endPos = panelAchievement.buttonCollect.transform.position,
				invPos = this.panelAchievements.menuShowCurrencyGem.GetCurrencyTransform().position,
				targetToScaleOnReach = this.panelAchievements.menuShowCurrencyGem.GetCurrencyTransform()
			};
			UiCommandCollectAchievement command = new UiCommandCollectAchievement
			{
				id = panelAchievement.achievementIds[panelAchievement.achievementIndex],
				dropPosition = dropPosition
			};
			this.command = command;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPurchaseGemPack, 1f));
		}

		private void OnClickedPanelUnlockCollect()
		{
			if (this.panelUnlockReward.timer < 1f)
			{
				return;
			}
			if (this.panelUnlockReward.fadingOut)
			{
				return;
			}
			if (this.unlockAboutToBeCollected.HasRewardOfType(typeof(UnlockRewardGameMode)))
			{
				if (this.unlockAboutToBeCollected.HasRewardOfType(typeof(UnlockRewardGameModeCrusade)))
				{
					this.panelHub.buttonGameModeCrusade.StartUnlockAnim(0.6f);
				}
				else
				{
					if (!this.unlockAboutToBeCollected.HasRewardOfType(typeof(UnlockRewardGameModeRift)))
					{
						throw new NotImplementedException();
					}
					PlayfabManager.SendPlayerEvent(PlayfabEventId.GOG_UNLOCKED, new Dictionary<string, object>(), null, null, true);
				}
				this.loadingTransition.DoTransition(UiState.HUB, 0f, 0f);
				TutorialManager.UnlockedGameMode();
				this.panelUnlockReward.returnToStateAfterFadeOut = false;
			}
			else if (this.unlockAboutToBeCollected.HasRewardOfType(typeof(UnlockRewardMerchant)))
			{
				this.panelUnlockReward.stateToReturnTo = UiState.MODE;
				this.panelUnlockReward.returnToStateAfterFadeOut = true;
			}
			else if (this.unlockAboutToBeCollected.HasRewardOfType(typeof(UnlockRewardHero)) && this.unlockAboutToBeCollected.GetHeroId() == "SAM")
			{
				this.panelUnlockReward.stateToReturnTo = UiState.NONE;
				this.panelUnlockReward.returnToStateAfterFadeOut = true;
			}
			else
			{
				this.panelUnlockReward.stateToReturnTo = UiState.NONE;
				this.panelUnlockReward.returnToStateAfterFadeOut = true;
			}
			this.panelUnlockReward.fadingOut = true;
			this.panelUnlockReward.timer = 0.75f;
			this.panelUnlockReward.FadeOut();
			this.command = new UiCommandUnlockCollectReward
			{
				unlock = this.unlockAboutToBeCollected
			};
			UiManager.stateJustChanged = false;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
		}

		private void OnRateShowFeedbackButtonClicked()
		{
			this.sim.ratingState = RatingState.AskLater;
			this.panelGeneralPopup.SetDetails(PanelGeneralPopup.State.NONE, LM.Get("UI_FEEDBACK_TITLE"), LM.Get("UI_FEEDBACK_DESC"), true, delegate
			{
				this.SendFeedbackMail();
				this.state = this.panelRatePopup.previousState;
			}, LM.Get("UI_YES"), delegate
			{
				this.state = this.panelRatePopup.previousState;
			}, LM.Get("UI_NO"), 0f, 160f, null, null);
		}

		private void OnRateAcceptButtonClicked()
		{
			this.ShowRatePage();
			this.sim.ratingState = RatingState.PlayerRated;
			this.state = this.panelRatePopup.previousState;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnRateAskLaterButtonClicked()
		{
			this.sim.ratingState = RatingState.AskLater;
			this.state = this.panelRatePopup.previousState;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnRateDontAskAgainButtonClicked()
		{
			this.sim.ratingState = RatingState.DontAskAgain;
			this.state = this.panelRatePopup.previousState;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedShopLootpack(int index)
		{
			if (index == 0 && !TrustedTime.IsReady())
			{
				PlayfabManager.GetTime(delegate(bool isSuccess, DateTime time)
				{
					if (isSuccess)
					{
						TrustedTime.Init(time);
					}
				});
			}
			this.selectedShopPack = this.sim.lootpacks[index];
			this.state = UiState.SHOP_LOOTPACK_SELECT;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackTap, 1f));
		}

		private void OnClickedShopHalloweenFlashOffer(int index)
		{
			FlashOffer offer = this.sim.halloweenFlashOfferBundle.offers[index];
			this.ShowBuyFlashOfferPopup(offer, true, null, null, null, false);
		}

		public void OnClickedShopSecondAnniversaryFlashOffer(int index)
		{
			FlashOffer offer = this.sim.secondAnniversaryFlashOffersBundle.offers[index];
			this.ShowBuyFlashOfferPopup(offer, true, null, null, null, false);
		}

		private void OnClickedShopFlashOffer(int index, bool isAdventureOffer)
		{
			FlashOffer offer = (!isAdventureOffer) ? this.sim.flashOfferBundle.offers[index] : this.sim.flashOfferBundle.adventureOffers[index];
			this.ShowBuyFlashOfferPopup(offer, true, null, null, null, false);
		}

		private void OnClickedChristmasOffer(int row, int nodeIndex)
		{
			FlashOffer offer = this.sim.christmasOfferBundle.tree[row][nodeIndex].offer;
			this.ShowBuyFlashOfferPopup(offer, this.sim.christmasOfferBundle.IsOfferUnlockedInTree(row, nodeIndex), "CHRISTMAS_TREE_OFFER_LOCKED", new Action(this.RedirectToCandiesShop), "UI_WARNING_CANDIES_DESC", true);
		}

		private void RedirectToCandiesShop()
		{
			this.state = UiState.CHRISTMAS_PANEL;
			this.panelChristmasOffer.GoToOffersTab();
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiOpenMenu, 1f));
		}

		private void ShowBuyFlashOfferPopup(FlashOffer offer, bool isUnlocked = true, string offerLockedKey = null, Action goToShopButtonClickedCallback = null, string canNotAffordMessageKey = null, bool closeAfterPurchase = false)
		{
			FlashOffer.Type type = offer.type;
			PanelBuyFlashOffer panelBuyFlashOffer;
			UiState state;
			if (type != FlashOffer.Type.CHARM)
			{
				panelBuyFlashOffer = this.panelBuyAdventureFlashOffer;
				state = UiState.BUY_FLASH_OFFER;
			}
			else
			{
				panelBuyFlashOffer = this.panelBuyCharmFlashOffer;
				state = UiState.BUY_FLASH_OFFER_CHARM;
			}
			panelBuyFlashOffer.flashOffer = offer;
			panelBuyFlashOffer.flashOfferUnlocked = isUnlocked;
			panelBuyFlashOffer.canNotAffordMessageKey = canNotAffordMessageKey;
			panelBuyFlashOffer.buttonGoToShopClickedCallback = goToShopButtonClickedCallback;
			if (!isUnlocked)
			{
				panelBuyFlashOffer.offerLockedMessage.text = LM.Get(offerLockedKey);
			}
			this.state = state;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackTap, 1f));
		}

		private void Button_OnFlashOfferCharmBuyClicked()
		{
			if (this.panelBuyCharmFlashOffer.timeInPanel < 0.5f)
			{
				return;
			}
			FlashOffer flashOffer = this.panelBuyCharmFlashOffer.flashOffer;
			if (flashOffer.type == FlashOffer.Type.CHARM)
			{
				CharmEffectData charmEffectData = this.sim.allCharmEffects[flashOffer.charmId];
				this.panelBuyCharmFlashOffer.oldDupplicateCount = charmEffectData.unspendDuplicates;
				this.panelBuyCharmFlashOffer.targetDupplicate = charmEffectData.unspendDuplicates + this.sim.GetFlashOfferCount(flashOffer);
				this.panelBuyCharmFlashOffer.neededCount = CharmEffectData.GetNeededDuplicateToLevelUpFromLevel((charmEffectData.level != -1) ? charmEffectData.level : 0);
				this.panelBuyCharmFlashOffer.charmOptionCard.charmCard.copyCounterText.text = "x" + this.sim.GetFlashOfferCount(flashOffer);
				this.panelBuyCharmFlashOffer.DoBuyCount();
				this.panelBuyCharmFlashOffer.DoButtonDisappear(this);
				this.command = new UICommandBuyFlashOffer
				{
					flashOffer = this.panelBuyCharmFlashOffer.flashOffer
				};
				return;
			}
			throw new EntryPointNotFoundException("This should not happen");
		}

		private void Button_OnFlashOfferBuyClicked()
		{
			if (this.panelBuyAdventureFlashOffer.timeInPanel < 0.5f)
			{
				return;
			}
			FlashOffer flashOffer = this.panelBuyAdventureFlashOffer.flashOffer;
			if (flashOffer.costType == FlashOffer.CostType.AD)
			{
				this.command = new UiCommandAdWatch
				{
					targetFlashOffer = flashOffer
				};
			}
			else
			{
				if (flashOffer.purchasesLeft == 1)
				{
					this.panelBuyAdventureFlashOffer.DoButtonDisappear(this);
					if (flashOffer.isCrhistmas)
					{
						this.panelChristmasOffer.offerPurchased = true;
					}
				}
				this.command = new UICommandBuyFlashOffer
				{
					flashOffer = this.panelBuyAdventureFlashOffer.flashOffer,
					dropPosition = PanelBuyAdventureFlashOffer.CurrencyMenuDropPosition()
				};
				if (flashOffer.type == FlashOffer.Type.COSTUME || flashOffer.type == FlashOffer.Type.COSTUME_PLUS_SCRAP)
				{
					this.panelBuyAdventureFlashOffer.PlaySkinBoughAnimation();
				}
				else if (flashOffer.type == FlashOffer.Type.TRINKET_PACK)
				{
					if (this.sim.numTrinketSlots > this.sim.allTrinkets.Count)
					{
						this.selectedShopPack = this.sim.shopPackTrinket;
						this.shopLootpackOpening.SetLootpack(this.selectedShopPack, this.sim.GetTotalAmountLootpacksOpened(), false);
						this.shopLootpackSummary.SetPack(this.selectedShopPack);
						this.state = UiState.SHOP_LOOTPACK_OPENING;
					}
					else if (this.panelShop.isHubMode)
					{
						DropPosition dropPos = new DropPosition
						{
							startPos = this.panelBuyAdventureFlashOffer.icon.transform.position,
							endPos = this.panelBuyAdventureFlashOffer.icon.transform.position,
							showSideCurrency = false,
							invPos = this.panelShop.buttonTabVault.transform.position
						};
						this.sim.GetActiveWorld().RainCurrencyOnUi(UiState.HUB_SHOP, CurrencyType.TRINKET_BOX, (double)this.sim.GetFlashOfferCount(flashOffer), dropPos, 30, 0f, 0f, 1f, null, 0f);
						this.state = UiState.HUB_SHOP;
					}
					else
					{
						this.sim.GetActiveWorld().RainTrinketPacks(this.sim.GetFlashOfferCount(flashOffer));
						this.state = UiState.NONE;
					}
				}
			}
		}

		private void OnClickedShopCharmpack(int index)
		{
			if (index == 0)
			{
				this.panelCharmPackSelect.isBigPack = false;
				this.panelCharmPackSelect.textHeader.text = LM.Get("CHARM_PACK");
				this.panelCharmPackSelect.textOneCharm.text = LM.Get("CHARM_PACK_DESC_1");
				this.panelCharmPackSelect.SetPackSkin(false);
				this.panelCharmPackSelect.textCopyCount.text = string.Format(LM.Get("CHARM_PACK_DESC_2"), 15);
			}
			else if (index == 1)
			{
				this.panelCharmPackSelect.isBigPack = true;
				this.panelCharmPackSelect.textHeader.text = LM.Get("CHARM_PACK_BIG");
				this.panelCharmPackSelect.textOneCharm.text = string.Format(LM.Get("CHARM_PACK_BIG_DESC"), 9);
				this.panelCharmPackSelect.SetPackSkin(true);
				this.panelCharmPackSelect.textCopyCount.text = string.Format(LM.Get("CHARM_PACK_BIG_DESC_2"), 15);
			}
			this.state = UiState.SHOP_CHARM_PACK_SELECT;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackTap, 1f));
		}

		private void OnClickedTrinketPack()
		{
			TutorialManager.OnClickedTrinketPack();
			if (this.sim.HasEmptyTrinketSlot())
			{
				this.selectedShopPack = this.sim.shopPackTrinket;
				this.state = UiState.SHOP_TRINKET_OPEN_POPUP;
			}
			else
			{
				this.panelGeneralPopup.SetDetails(PanelGeneralPopup.State.SHOP, LM.Get("SHOP_PACK_TRINKET"), (!this.sim.HasEmptyTrinketSlot()) ? LM.Get("SHOP_PACK_TRINKET_WARNING_0") : LM.Get("SHOP_PACK_TRINKET_WARNING_1"), false, delegate
				{
					this.state = ((!this.panelShop.isHubMode) ? UiState.SHOP : UiState.HUB_SHOP);
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
				}, LM.Get("UI_OKAY"), null, string.Empty, 0f, 160f, null, null);
				this.state = UiState.GENERAL_POPUP;
			}
			this.sim.numUnseenTrinketPacks = 0;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackTap, 1f));
		}

		private void OnClickedMine(Mine mine)
		{
			this.panelMine.selected = mine;
			if (mine is MineToken)
			{
				this.panelMine.imageIcon.sprite = this.uiData.spriteMineTokenFull;
				this.panelMine.amountToCollectCurrencyIcon.sprite = this.uiData.currencySprites[4];
				this.panelMine.nextLevelCurrencyIcon.sprite = this.uiData.currencySprites[4];
				this.panelMine.flagPlate.color = this.panelMine.tokenMineColor;
				this.panelMine.textBanner.text = LM.Get("MINE_TOKEN");
				this.panelMine.bonusLabel.text = LM.Get("MINE_TOKEN_BONUS");
				this.panelMine.bonusNextLevelLabel.text = LM.Get("MINE_TOKEN_BONUS");
			}
			else
			{
				if (!(mine is MineScrap))
				{
					throw new NotImplementedException();
				}
				this.panelMine.imageIcon.sprite = this.uiData.spriteMineScrapFull;
				this.panelMine.amountToCollectCurrencyIcon.sprite = this.uiData.currencySprites[1];
				this.panelMine.nextLevelCurrencyIcon.sprite = this.uiData.currencySprites[1];
				this.panelMine.flagPlate.color = this.panelMine.scrapMineColor;
				this.panelMine.textBanner.text = LM.Get("MINE_SCRAP");
				this.panelMine.bonusLabel.text = LM.Get("MINE_SCRAP_BONUS");
				this.panelMine.bonusNextLevelLabel.text = LM.Get("MINE_SCRAP_BONUS");
			}
			this.panelMine.imageIcon.SetNativeSize();
			this.state = UiState.SHOP_MINE;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackTap, 1f));
		}

		private void OnClickedMineCollect()
		{
			float num = 0.5f;
			if (TutorialManager.mineUnlock == TutorialManager.MineUnlock.UNLOCKED)
			{
				return;
			}
			if (this.panelMine.timer > num && this.panelMine.selected != null)
			{
				DropPosition dropPos = new DropPosition
				{
					startPos = this.panelMine.imageIcon.transform.position + Vector3.up * 0.1f,
					endPos = this.panelMine.imageIcon.transform.position + Vector3.down * 0.05f,
					invPos = this.panelMine.menuShowCurrency.GetCurrencyTransform().position,
					targetToScaleOnReach = this.panelMine.menuShowCurrency.GetCurrencyTransform()
				};
				UiCommandCollectMine uiCommandCollectMine = new UiCommandCollectMine
				{
					dropPos = dropPos
				};
				uiCommandCollectMine.mine = this.panelMine.selected;
				this.command = uiCommandCollectMine;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPurchaseGemPack, 1f));
			}
		}

		private void OnClickedMineUpgrade()
		{
			float num = 0.5f;
			if (this.panelMine.timer > num && this.panelMine.selected != null)
			{
				this.command = new UiCommandUpgradeMine
				{
					mine = this.panelMine.selected
				};
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiWorldUpgrade, 1f));
				string levelLabel = string.Format(LM.Get("UI_LEVEL"), this.panelMine.selected.level + 2);
				this.panelMine.DoUpgradeAnimation(levelLabel);
			}
		}

		private void OnClickedShopLootpackSelect()
		{
			if (this.shopLootpackSelect.timer < this.shopLootpackSelect.targetTimer)
			{
				return;
			}
			if (this.selectedShopPack.isOffer)
			{
				if (this.selectedShopPack.isIAP)
				{
					this.OnClickedIAP(this.selectedShopPack.iapIndex);
					this.selectedShopPack.OnPurchaseClicked();
				}
				else
				{
					this.selectedShopPack.OnPurchaseCompleted();
					this.BuyShopPack();
				}
			}
			else if (this.selectedShopPack.isIAP)
			{
				this.OnClickedIAP(this.selectedShopPack.iapIndex);
				this.selectedShopPack.OnPurchaseClicked();
			}
			else
			{
				this.selectedShopPack.OnPurchaseCompleted();
				this.BuyShopPack();
			}
		}

		private void OnClickedShopTrinketPackSelect(bool isSpecial)
		{
			bool flag;
			if (this.sim.numTrinketPacks > 0)
			{
				flag = true;
			}
			else if (isSpecial)
			{
				flag = this.sim.CanCurrencyAfford(CurrencyType.AEON, (this.selectedShopPack as ShopPackTrinket).specialCost);
			}
			else
			{
				flag = this.sim.CanCurrencyAfford(CurrencyType.GEM, this.selectedShopPack.cost);
			}
			if (flag)
			{
				this.selectedShopPack.OnPurchaseCompleted();
				this.BuyShopTrinketPack(isSpecial);
			}
			else if (isSpecial)
			{
				this.panelCurrencyWarning.SetCurrency(PanelCurrencyWarning.PopupType.AEONS, this, string.Empty, 0, null);
				this.state = UiState.CURRENCY_WARNING;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiDefaultFailClick, 1f));
			}
			else
			{
				this.panelCurrencyWarning.SetCurrency(PanelCurrencyWarning.PopupType.GEMS, this, string.Empty, 0, null);
				this.state = UiState.CURRENCY_WARNING;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiDefaultFailClick, 1f));
			}
		}

		public void BuyShopPack()
		{
			if (this.selectedShopPack is ShopPackFiveTrinkets)
			{
				this.command = new UiCommandRainLootpack
				{
					shopPack = this.selectedShopPack
				};
				World activeWorld = this.sim.GetActiveWorld();
				IapManager.inst.boughtProductIndex = -1;
				if (this.panelShop.isHubMode)
				{
					DropPosition dropPos = new DropPosition
					{
						startPos = default(Vector3),
						endPos = Vector3.down * 0.1f,
						invPos = this.panelShop.shopLootPacks[0].transform.position + Vector3.up * 0.45f
					};
					activeWorld.RainCurrencyOnUi(UiState.HUB_SHOP, CurrencyType.TRINKET_BOX, (double)this.selectedShopPack.numTrinketPacks, dropPos, 30, 0f, 0f, 1f, null, 0f);
					this.state = UiState.HUB_SHOP;
				}
				else
				{
					activeWorld.RainTrinketPacks(this.selectedShopPack.numTrinketPacks);
					this.state = UiState.NONE;
				}
				this.sim.numUnseenTrinketPacks += this.selectedShopPack.numTrinketPacks;
				this.sim.ResetShopPackOffer();
			}
			else if (this.selectedShopPack is ShopPackToken)
			{
				this.sim.ConsumeCurrency(this.selectedShopPack.currency, this.selectedShopPack.cost);
				World activeWorld2 = this.sim.GetActiveWorld();
				UnityEngine.Debug.LogWarning("Rain boxes");
				IapManager.inst.boughtProductIndex = -1;
				if (this.panelShop.isHubMode)
				{
					DropPosition dropPos2 = new DropPosition
					{
						startPos = default(Vector3),
						endPos = Vector3.down * 0.1f,
						invPos = this.panelCurrencyOnTop[0].panelCurrency.GetCurrencyTransform().position,
						targetToScaleOnReach = this.panelCurrencyOnTop[0].panelCurrency.GetCurrencyTransform(),
						showSideCurrency = true
					};
					activeWorld2.RainCurrencyOnUi(UiState.HUB_SHOP, CurrencyType.TOKEN, this.selectedShopPack.tokensMax, dropPos2, 30, 0f, 0f, 1f, null, 0f);
					this.state = UiState.HUB_SHOP;
				}
				else
				{
					activeWorld2.RainTokens(this.selectedShopPack.tokensMax);
					this.state = UiState.NONE;
				}
				this.sim.ResetShopPackOffer();
			}
			else if (this.selectedShopPack is ShopPackBigGem || this.selectedShopPack is ShopPackBigGemTwo || this.selectedShopPack is ShopPackStage300)
			{
				World activeWorld3 = this.sim.GetActiveWorld();
				UnityEngine.Debug.LogWarning("Rain boxes");
				IapManager.inst.boughtProductIndex = -1;
				if (this.panelShop.isHubMode)
				{
					DropPosition dropPos3 = new DropPosition
					{
						startPos = default(Vector3),
						endPos = Vector3.down,
						invPos = this.panelHubShop.menuShowCurrencyCredits.GetCurrencyTransform().position,
						targetToScaleOnReach = this.panelHubShop.menuShowCurrencyCredits.GetCurrencyTransform()
					};
					activeWorld3.RainCurrencyOnUi(UiState.HUB_SHOP, CurrencyType.GEM, this.selectedShopPack.credits, dropPos3, 30, 0f, 0f, 1f, null, 0f);
					this.state = UiState.HUB_SHOP;
				}
				else
				{
					activeWorld3.RainCredits(this.selectedShopPack.credits);
					this.state = UiState.NONE;
				}
				this.sim.ResetShopPackOffer();
			}
			else if (this.selectedShopPack is ShopPackStage100 || this.selectedShopPack is ShopPackStage800)
			{
				this.sim.ConsumeCurrency(this.selectedShopPack.currency, this.selectedShopPack.cost);
				World activeWorld4 = this.sim.GetActiveWorld();
				IapManager.inst.boughtProductIndex = -1;
				if (this.panelShop.isHubMode)
				{
					DropPosition dropPos4 = new DropPosition
					{
						startPos = default(Vector3),
						endPos = Vector3.down * 0.1f,
						invPos = this.panelCurrencyOnTop[0].panelCurrency.GetCurrencyTransform().position,
						targetToScaleOnReach = this.panelCurrencyOnTop[0].panelCurrency.GetCurrencyTransform(),
						showSideCurrency = true
					};
					activeWorld4.RainCurrencyOnUi(UiState.HUB_SHOP, CurrencyType.SCRAP, this.selectedShopPack.scrapsMax, dropPos4, 30, 0f, 0f, 1f, null, 0f);
					this.state = UiState.HUB_SHOP;
				}
				else
				{
					activeWorld4.RainScraps(this.selectedShopPack.scrapsMax);
					this.state = UiState.NONE;
				}
				this.sim.ResetShopPackOffer();
			}
			else if (this.selectedShopPack is ShopPackThreePijama)
			{
				this.command = new UiCommandRainLootpack
				{
					shopPack = this.selectedShopPack
				};
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackSelect[1], 1f));
				IapManager.inst.boughtProductIndex = -1;
				this.state = UiState.SHOP_SKINPACK_OPENING;
				this.shopSkinPackOpening.uiManager = this;
				this.shopSkinPackOpening.StartShopPack(this.selectedShopPack, this.selectedShopPack.GetSkins().ToArray());
				this.sim.ResetShopPackOffer();
			}
			else if (this.selectedShopPack is ShopPackCharmPackSmall)
			{
				ShopPackCharmPackSmall shopPack = this.selectedShopPack as ShopPackCharmPackSmall;
				this.command = new UiCommandOpenLootpack
				{
					shopPack = shopPack
				};
			}
			else
			{
				this.shopLootpackOpening.SetLootpack(this.selectedShopPack, (!(this.selectedShopPack is ShopPackLootpackFree)) ? this.sim.GetTotalPremiumLootpackOpened() : this.sim.GetTotalFreeLootpackOpened(), true);
				this.shopLootpackSummary.SetPack(this.selectedShopPack);
				if (this.selectedShopPack is ShopPackLootpackFree)
				{
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackSelect[0], 1f));
				}
				else if (this.selectedShopPack is ShopPackLootpackRare)
				{
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackSelect[1], 1f));
				}
				else
				{
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackSelect[2], 1f));
				}
				this.command = new UiCommandOpenLootpack
				{
					shopPack = this.selectedShopPack
				};
				this.state = UiState.SHOP_LOOTPACK_OPENING;
				if (this.selectedShopPack is ShopPackRune)
				{
					this.sim.ResetShopPackOffer();
				}
			}
		}

		public void BuyShopTrinketPack(bool isSpecial)
		{
			this.shopLootpackOpening.SetLootpack(this.selectedShopPack, this.sim.GetTotalAmountLootpacksOpened(), false);
			this.shopLootpackSummary.SetPack(this.selectedShopPack);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackSelect[2], 1f));
			UiCommandOpenTrinketPack command = new UiCommandOpenTrinketPack
			{
				shopPacktrinket = (this.selectedShopPack as ShopPackTrinket),
				isSpecial = isSpecial
			};
			this.command = command;
			this.state = UiState.SHOP_LOOTPACK_OPENING;
		}

		public void OnClickedShopPackSelect(int index)
		{
			SpecialOfferKeeper specialOfferKeeper = this.sim.specialOfferBoard[index];
			this.selectedShopPack = specialOfferKeeper.offerPack;
			this.state = UiState.SHOP_LOOTPACK_SELECT;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackTap, 1f));
			specialOfferKeeper.offerPack.OnCheckout();
			this.sim.selectedSpecialOfferKeeper = specialOfferKeeper;
		}

		public void OnClickedOutOfShopPackOffer(int index)
		{
			SpecialOfferKeeper specialOfferKeeper = this.sim.specialOfferBoard.outOfShopOffers[index];
			this.selectedShopPack = specialOfferKeeper.offerPack;
			this.state = UiState.SHOP_LOOTPACK_SELECT;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackTap, 1f));
			specialOfferKeeper.offerPack.OnCheckout();
			this.sim.selectedSpecialOfferKeeper = specialOfferKeeper;
		}

		private void OnClickedShopLootpackSummary()
		{
			if (this.panelShop.isHubMode)
			{
				this.state = UiState.HUB_SHOP;
			}
			else
			{
				this.state = UiState.SHOP;
			}
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
		}

		public void OnClickedPanelHeroEvolve()
		{
			if (!this.panelHeroEvolveSkin.isAnimating)
			{
				if (this.panelHeroEvolveSkin.state == PanelHeroEvolveSkin.State.WAITING_EVOLVE)
				{
					this.panelHeroEvolveSkin.DoSkinTransform(this.sim);
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.heroEvolveFirstClick, 1f));
				}
				else if (this.panelHeroEvolveSkin.state == PanelHeroEvolveSkin.State.WAITING_REVEAL_SKIN)
				{
					this.panelHeroEvolveSkin.DoStep(this.sim);
				}
				else if (this.panelHeroEvolveSkin.state == PanelHeroEvolveSkin.State.END)
				{
					this.state = this.panelHeroEvolveSkin.stateBeforeOpening;
				}
			}
		}

		private void OnClickedSoundOnOff()
		{
			UiCommandSoundOnOff command = new UiCommandSoundOnOff();
			this.command = command;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedMusicOnOff()
		{
			UiCommandMusicOnOff command = new UiCommandMusicOnOff();
			this.command = command;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedVoicesOnOff()
		{
			UiCommandVoicesOnOff command = new UiCommandVoicesOnOff();
			this.command = command;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedLowPowerOnOff()
		{
			this.sim.prefers30Fps = !this.sim.prefers30Fps;
			if (this.sim.prefers30Fps)
			{
				Application.targetFrameRate = 30;
			}
			else
			{
				Application.targetFrameRate = 60;
			}
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedAppSleepOnOff()
		{
			this.sim.appNeverSleep = !this.sim.appNeverSleep;
			Main.SetAppSleep(this.sim);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedNotationOnOff()
		{
			this.sim.scientificNotation = !this.sim.scientificNotation;
			if (this.sim.scientificNotation)
			{
				GameMath.NOTATION_STYLE = GameMath.NotationStyle.SCIENTIFIC;
			}
			else
			{
				GameMath.NOTATION_STYLE = GameMath.NotationStyle.CLASSIC;
			}
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedSecondaryCdUiOnOff()
		{
			this.sim.secondaryCdUi = !this.sim.secondaryCdUi;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedCompassOnOff()
		{
			this.sim.compassDisabled = !this.sim.compassDisabled;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedStoreAuthOnOff()
		{
			Main.dontStoreAuthenticate = !Main.dontStoreAuthenticate;
			if (!Main.dontStoreAuthenticate)
			{
				StoreManager.Authenticate(true, delegate
				{
					PlayfabManager.Login(delegate
					{
					}, true);
				});
			}
			else
			{
				StoreManager.LogOut();
			}
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedAdvancedNotifOnOff()
		{
			UiManager.stateJustChanged = true;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedNotificationsOnOff()
		{
			StoreManager.areNotificationsAllowed = !StoreManager.areNotificationsAllowed;
			if (StoreManager.areNotificationsAllowed)
			{
				StoreManager.RegisterForNotifications();
				this.hubOptions.panelAdvancedOptions.childTogglesParent.gameObject.SetActive(true);
			}
			else
			{
				this.hubOptions.panelAdvancedOptions.childTogglesParent.gameObject.SetActive(false);
			}
			UiManager.stateJustChanged = true;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedSkillOneTapUpgradeOnOff()
		{
			this.sim.skillOneTapUpgrade = !this.sim.skillOneTapUpgrade;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedAchievements()
		{
			//PlayGamesPlatform.Instance.ShowAchievementsUI(new Action<UIStatus>(this.OnPlayerReturnedFromPlayGamesUI));
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedLeaderboards()
		{
			//if (Social.Active is PlayGamesPlatform)
			//{
			//	PlayGamesPlatform.Instance.ShowLeaderboardUI(LeaderboardId.MAX_STAGE, new Action<UIStatus>(this.OnPlayerReturnedFromPlayGamesUI));
			//}
			//else
			//{
			//	Social.ShowLeaderboardUI();
			//}
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnPlayerReturnedFromPlayGamesUI(UIStatus status)
		{
			Main.dontTryAuth = (status == UIStatus.NotAuthorized);
		}

		private void OnClickedContact()
		{
			this.state = UiState.SUPPORT_POPUP;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private string GetOtherIds()
		{
			string text = string.Empty;
			foreach (string text2 in PlayfabManager.allPlayfabIds)
			{
				if (text2 != PlayfabManager.playerId)
				{
					text += string.Format("{0} - ", text2);
				}
			}
			if (text.Length >= 3)
			{
				text = text.Substring(0, text.Length - 3);
			}
			return text;
		}

		private void Button_SupportClosePopup()
		{
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
			this.state = UiState.HUB_OPTIONS;
		}

		private void Button_SupportReportBug()
		{
			if (this.supportPopup.bTimer >= 2f)
			{
				string value = string.Concat(new string[]
				{
					"id: ",
					PlayfabManager.playerId,
					" | other: ",
					this.GetOtherIds(),
					" | version: ",
					Cheats.version,
					" | ",
					SystemInfo.operatingSystem,
					"\n"
				});
				StringBuilder stringBuilder = new StringBuilder(400);
				stringBuilder.Append("Please describe what happened:").AppendLine().Append("If you have a screenshot of the issue, please attach it to this e-mail.").AppendLine().AppendLine().AppendLine().Append(value);
				Application.OpenURL("mailto:support@beesquare.net?subject=" + GameMath.ClearUrl("[BUG REPORT] AaH Support") + "&body=" + GameMath.ClearUrl(stringBuilder.ToString()));
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
				this.supportPopup.bTimer = 0f;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
				this.supportPopup.bTimer = 0f;
			}
		}

		private void Button_SupportPaymentIssue()
		{
			if (this.supportPopup.bTimer >= 2f)
			{
				string value = string.Concat(new string[]
				{
					"id: ",
					PlayfabManager.playerId,
					" | other: ",
					this.GetOtherIds(),
					" | version: ",
					Cheats.version,
					" | ",
					SystemInfo.operatingSystem,
					"\n"
				});
				StringBuilder stringBuilder = new StringBuilder(400);
				stringBuilder.Append("Please describe what happened:").AppendLine().Append("If you have a purchase receipt, please attach it to this e-mail.").AppendLine().AppendLine().AppendLine().Append(value);
				Application.OpenURL("mailto:support@beesquare.net?subject=" + GameMath.ClearUrl("[PAYMENT ISSUE] AaH Support") + "&body=" + GameMath.ClearUrl(stringBuilder.ToString()));
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
				this.supportPopup.bTimer = 0f;
			}
		}

		private void Button_SupportFeedback()
		{
			if (this.supportPopup.bTimer >= 2f)
			{
				this.SendFeedbackMail();
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
				this.supportPopup.bTimer = 0f;
			}
		}

		public void OnClickedLanguageChange()
		{
			LM.SelectLanguage(LM.GetNextLanguage());
			this.InitStrings();
			UiManager.stateJustChanged = true;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedWiki()
		{
			Application.OpenURL("https://www.reddit.com/r/AlmostAHero/wiki/index");
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedGameInfo()
		{
			this.state = UiState.HUB_OPTIONS_WIKI;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedUpdateInfo()
		{
			this.state = UiState.PATCH_NOTES;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedCommunity()
		{
			this.panelGeneralPopup.SetDetails(PanelGeneralPopup.State.OPTIONS, LM.Get("OPTIONS_COMMUNITY"), LM.Get("OPTIONS_COMMUNITY_DESC"), true, delegate
			{
				Application.OpenURL("https://discord.gg/V9CewPh");
				this.state = UiState.HUB_OPTIONS;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			}, LM.Get("UI_OKAY"), delegate
			{
				this.state = UiState.HUB_OPTIONS;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			}, LM.Get("UI_CANCEL"), 0f, 160f, null, null);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			this.state = UiState.GENERAL_POPUP;
		}

		private void OnClickedCloudSave()
		{
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			if (StoreManager.IsAuthed())
			{
				this.panelGeneralPopup.SetDetails(PanelGeneralPopup.State.OPTIONS, LM.Get("OPTIONS_CLOUD"), string.Format(LM.Get("OPTIONS_CLOUD_DESC"), UiManager.GetStoreNameString()), true, delegate
				{
					this.state = UiState.HUB_OPTIONS;
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
				}, LM.Get("UI_OKAY"), delegate
				{
					Main.dontTryAuth = true;
					StoreManager.LogOut();
					PlayfabManager.Login(delegate
					{
					}, true);
					this.state = UiState.HUB_OPTIONS;
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
				}, LM.Get("UI_OPTIONS_SIGN_OUT"), 0f, 160f, null, null);
				this.state = UiState.GENERAL_POPUP;
			}
			else
			{
				string sBodyUp = LM.Get("OPTIONS_CLOUD_IOS");
				sBodyUp = LM.Get("OPTIONS_CLOUD_ANDROID");
				this.panelGeneralPopup.SetDetails(PanelGeneralPopup.State.OPTIONS, LM.Get("OPTIONS_CLOUD"), sBodyUp, true, delegate
				{
					StoreManager.Authenticate(true, delegate
					{
						if (StoreManager.IsAuthed())
						{
							Main.dontTryAuth = false;
						}
						PlayfabManager.Login(delegate
						{
						}, true);
					});
					this.state = UiState.HUB_OPTIONS;
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
				}, LM.Get("UI_OPTIONS_SIGN_IN"), delegate
				{
					this.state = UiState.HUB_OPTIONS;
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
				}, LM.Get("UI_CANCEL"), 0f, 160f, null, null);
				this.state = UiState.GENERAL_POPUP;
			}
		}

		private void OnClickedDeleteSave()
		{
			this.panelGeneralPopup.SetDetails(PanelGeneralPopup.State.HARD_RESET, LM.Get("OPTIONS_HARD_RESET"), LM.Get("OPTIONS_HARD_RESET_DESC"), true, delegate
			{
			}, LM.Get("UI_YES"), delegate
			{
				this.state = UiState.HUB_OPTIONS;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			}, LM.Get("UI_NO"), -60f, 160f, null, null);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			this.state = UiState.GENERAL_POPUP;
		}

		private void OnClickedSecretButton()
		{
			this.OnCickedToggleDebugButton();
			this.hubOptions.secret++;
			this.hubOptions.secretTimer = 0f;
			if (this.hubOptions.secret > 2)
			{
				string text = Cheats.version + "\n";
				foreach (string arg in PlayfabManager.allPlayfabIds)
				{
					text += string.Format("{0}\n", arg);
				}
				text += SystemInfo.operatingSystem;
				this.panelGeneralPopup.SetDetails(PanelGeneralPopup.State.OPTIONS, "SHHH THIS IS A SECRET", text, false, delegate
				{
					this.state = UiState.HUB_OPTIONS;
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
				}, LM.Get("UI_OKAY"), null, string.Empty, 0f, 160f, null, null);
				this.state = UiState.GENERAL_POPUP;
			}
		}

		private void OnClickedMerchantItem(int index, bool isEventMerchantItem)
		{
			this.selectedMerchantItem = index;
			this.selectedMerchantItemIsFromEvent = isEventMerchantItem;
			this.state = UiState.MODE_MERCHANT_ITEM_SELECT;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
		}

		private void OnClickedBuyMerchantItem()
		{
			if (this.panelMerchantItemSelect.buttonBuy.openWarningPopup)
			{
				this.panelCurrencyWarning.SetCurrency(PanelCurrencyWarning.PopupType.TOKENS, this, string.Empty, 0, null);
				this.state = UiState.CURRENCY_WARNING;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiDefaultFailClick, 1f));
			}
			else
			{
				this.state = UiState.NONE;
				UiCommandBuyMerchantItem command = new UiCommandBuyMerchantItem
				{
					count = this.panelMerchantItemSelect.copyAmount,
					index = this.selectedMerchantItem,
					isEventMerchantItem = this.selectedMerchantItemIsFromEvent
				};
				this.command = command;
			}
		}

		private void OnClickedUseMerchantItem()
		{
			Simulation.MerchantItem merchantItem = (!this.selectedMerchantItemIsFromEvent) ? this.sim.GetMerchantItem(this.selectedMerchantItem) : this.sim.GetEventMerchantItem(this.selectedMerchantItem);
			if (merchantItem.GetNumInInventory() == 0)
			{
				UiManager.AddUiSound(SoundArchieve.inst.uiOpenTabClick);
				this.panelChristmasOffer.GoToTreeTab();
				this.panelChristmasOffer.previousState = UiState.NONE;
				this.state = UiState.CHRISTMAS_PANEL;
			}
			else
			{
				this.state = UiState.NONE;
				UiCommandBuyMerchantItem command = new UiCommandBuyMerchantItem
				{
					count = this.panelMerchantItemSelect.copyAmount,
					index = this.selectedMerchantItem,
					isEventMerchantItem = this.selectedMerchantItemIsFromEvent
				};
				this.command = command;
			}
		}

		private void OnClickedGameWikiTabButton()
		{
			this.hubOptionsWiki.gameInfoParent.SetActive(true);
			this.hubOptionsWiki.gogInfoParent.SetActive(false);
			this.hubOptionsWiki.tabGameInfoButton.interactable = false;
			this.hubOptionsWiki.tabGogInfoButton.interactable = true;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedGogWikiTabButton()
		{
			this.hubOptionsWiki.gameInfoParent.SetActive(false);
			this.hubOptionsWiki.gogInfoParent.SetActive(true);
			this.hubOptionsWiki.tabGameInfoButton.interactable = true;
			this.hubOptionsWiki.tabGogInfoButton.interactable = false;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedCloseWikiButton()
		{
			this.state = UiState.HUB_OPTIONS;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
		}

		private void OnClickedCollectOfflineEarnings()
		{
			this.state = UiState.NONE;
			UiCommandCollectOfflineEarnings command = new UiCommandCollectOfflineEarnings();
			this.command = command;
		}

		private void OnClickedAbandonChallenge()
		{
			string id = "UI_MODE_ABANDON";
			string id2 = "UI_ABANDON_CHECK";
			if (this.sim.IsActiveMode(GameMode.RIFT))
			{
				id = "UI_MODE_ABANDON_RIFT";
				id2 = "UI_ABANDON_CHECK_RIFT";
			}
			this.panelGeneralPopup.SetDetails(PanelGeneralPopup.State.MODE, LM.Get(id), LM.Get(id2), true, delegate
			{
				this.command = new UiCommandAbandonChallenge();
				this.loadingTransition.abandonOrFailed = true;
				this.state = UiState.NONE;
			}, LM.Get("UI_YES"), delegate
			{
				this.state = UiState.MODE;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			}, LM.Get("UI_NO"), 0f, 160f, null, null);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			this.state = UiState.GENERAL_POPUP;
		}

		private void OnClickedIAP(int index)
		{
			
			UiManager.isPurchasing = true;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPurchaseGemPack, 1f));
			PlayfabManager.PrepareForIap(delegate(bool isReady)
			{
				if (isReady)
				{
					UiCommandIAP uiCommandIAP = new UiCommandIAP();
					uiCommandIAP.index = index;
					
				}
				else
				{
					UiManager.isPurchasing = false;
					bool isChristmasIap = index == IapIds.CANDY_PACK_01 || index == IapIds.CANDY_PACK_02 || index == IapIds.CHRISTMAS_GEMS_BIG_PACK || index == IapIds.CHRISTMAS_GEMS_SMALL_PACK;
					panelGeneralPopup.SetDetails((!isChristmasIap) ? PanelGeneralPopup.State.SHOP : PanelGeneralPopup.State.CHRISTMAS_SHOP, LM.Get("UI_PLAYFAB_WARNING_HEADER"), LM.Get("UI_PLAYFAB_WARNING_DESC"), false, delegate
					{
						if (isChristmasIap)
						{
							state = UiState.CHRISTMAS_PANEL;
						}
						else
						{
							state = ((!panelShop.isHubMode) ? UiState.SHOP : UiState.HUB_SHOP);
						}
						UiManager.AddUiSound(SoundArchieve.inst.uiPopupDisappear);
					}, LM.Get("UI_OKAY"), null, string.Empty, 0f, 160f, null, null);
					state = UiState.GENERAL_POPUP;
				}
			});
		}

		private void OnClickedFreeCredits()
		{
			UiCommandFreeCurrency command = new UiCommandFreeCurrency
			{
				currencyType = CurrencyType.GEM,
				rewardAmount = PlayfabManager.titleData.freeCreditsAmount
			};
			World activeWorld = this.sim.GetActiveWorld();
			this.command = command;
			if (!this.panelShop.isHubMode)
			{
				this.state = UiState.NONE;
			}
		}

		private void OnClickSocialNetworkOffer(SocialRewards.Network socialNetwork)
		{
			this.panelSocialRewardPopup.SetDetails(socialNetwork, 50.0, false, this.state, this.GetSocialNetworkName(socialNetwork));
			this.state = UiState.SOCIAL_REWARD_POPUP;
			UiManager.AddUiSound(SoundArchieve.inst.uiTabSwitch);
		}

		private void OnClickedAdCollect()
		{
			if (RewardedAdManager.inst.shouldGiveRewardCapped)
			{
				RewardedAdManager.inst.shouldGiveRewardCapped = false;
				UiCommandFreeCurrencyCollect uiCommandFreeCurrencyCollect = new UiCommandFreeCurrencyCollect();
				uiCommandFreeCurrencyCollect.rewardCurrencyType = RewardedAdManager.inst.currencyTypeForCappedVideo;
				uiCommandFreeCurrencyCollect.rewardAmount = RewardedAdManager.inst.rewardAmountForCappedVideo;
				if (this.panelShop.isHubMode)
				{
					uiCommandFreeCurrencyCollect.isHubShop = true;
					DropPosition dropPosition = new DropPosition
					{
						startPos = this.panelShop.panelFreeCredits.transform.position,
						endPos = this.panelShop.panelFreeCredits.transform.position - Vector3.up * 0.1f,
						invPos = this.panelCurrencyOnTop[0].panelCurrency.GetCurrencyTransform().position - Vector3.right * 0.425f,
						targetToScaleOnReach = this.panelCurrencyOnTop[0].panelCurrency.GetCurrencyTransform(),
						showSideCurrency = true
					};
					uiCommandFreeCurrencyCollect.dropPosition = dropPosition;
				}
				this.command = uiCommandFreeCurrencyCollect;
				World activeWorld = this.sim.GetActiveWorld();
			}
			if (RewardedAdManager.inst.shouldGiveReward)
			{
				RewardedAdManager.inst.shouldGiveReward = false;
				UiCommandAdCollect command = new UiCommandAdCollect
				{
					canDropCandies = true
				};
				this.command = command;
				World activeWorld2 = this.sim.GetActiveWorld();
				PlayfabManager.SendPlayerEvent(PlayfabEventId.AD_COLLECTED_DRAGON, new Dictionary<string, object>
				{
					{
						"current_stage",
						activeWorld2.GetStageNumber()
					},
					{
						"reward_currency",
						activeWorld2.adRewardCurrencyType
					},
					{
						"reward_amount",
						(activeWorld2.adRewardCurrencyType != CurrencyType.GOLD) ? activeWorld2.adRewardAmount : 0.0
					}
				}, null, null, true);
			}
			if (!this.panelShop.isHubMode)
			{
				this.state = UiState.NONE;
			}
			else
			{
				this.state = UiState.HUB_SHOP;
			}
		}

		private void OnClickedAdCancel()
		{
			UiCommandAdCancel command = new UiCommandAdCancel();
			this.command = command;
			this.state = UiState.NONE;
		}

		private void OnClickedAdWatch()
		{
			UiCommandAdWatch command = new UiCommandAdWatch();
			this.command = command;
			this.state = UiState.NONE;
			World activeWorld = this.sim.GetActiveWorld();
			PlayfabManager.SendPlayerEvent(PlayfabEventId.AD_STARTED_DRAGON, new Dictionary<string, object>
			{
				{
					"current_stage",
					activeWorld.GetStageNumber()
				},
				{
					"reward_currency",
					activeWorld.adRewardCurrencyType
				},
				{
					"reward_amount",
					(activeWorld.adRewardCurrencyType != CurrencyType.GOLD) ? activeWorld.adRewardAmount : 0.0
				}
			}, null, null, true);
		}

		private void OnClickedAcceptServerReward(RewardOrigin rewardOrigin)
		{
			if (this.panelServerReward.timer < 0.1f)
			{
				return;
			}
			if (!this.acceptingServerReward)
			{
				this.command = new UiCommandServerRewardCollect
				{
					currencySidesTop = this.panelCurrencyOnTop,
					isHub = (this.panelGeneralPopup.state == PanelGeneralPopup.State.SERVER_REWARD_HUB),
					shopTabButton = this.panelHub.buttonShop.rectTransform,
					rewardOrigin = rewardOrigin
				};
				this.acceptingServerReward = true;
			}
		}

		private void OnClickedUpdateRequiredOkay()
		{
			if (this.panelUpdateRequired.timer < 0.1f)
			{
				return;
			}
			this.GoToStorePage();
			PlayfabManager.SendPlayerEvent(PlayfabEventId.UPDATE_FORCED_CLICK, new Dictionary<string, object>(), null, null, true);
		}

		private void GoToStorePage()
		{
			Application.OpenURL("market://details?id=com.beesquare.almostahero");
		}

		private void ShowRatePage()
		{
			this.GoToStorePage();
		}

		private void OnClickedSkillsScreenNextHero(bool isNext)
		{
			int num = -1;
			List<Hero> heroes = this.sim.GetActiveWorld().heroes;
            Debug.Log("###### heroes " + heroes.Count);
			int i = 0;
			int count = heroes.Count;
			while (i < count)
			{
				if (heroes[i] == this.selectedHeroGearSkills)
				{
					num = i;
					break;
				}
				i++;
			}
			if (num >= 0)
			{
				if (isNext)
				{
					if (num < heroes.Count - 1)
					{
						num++;
					}
					else
					{
						num = 0;
					}
				}
				else if (num > 0)
				{
					num--;
				}
				else
				{
					num = heroes.Count - 1;
				}
				this.selectedHeroGearSkills = heroes[num];
				UiManager.stateJustChanged = true;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			}
			else
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiDefaultFailClick, 1f));
			}
		}

		private void OnClickedUpgradeTrinket(Trinket trinket)
		{
			this.command = new UiCommandTrinketUpgrade
			{
				trinket = trinket,
				effectIndex = trinket.GetTrinketEffectIndexToUpgrade()
			};
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiWorldUpgrade, 1f));
			this.panelTrinketsScreen.PlayTrinketUpgradedAnim();
		}

		private void Button_OnCharmPackBuyClicked()
		{
			if (this.panelCharmPackSelect.isBigPack && this.sim.CanAffordBigCharmPack())
			{
				List<CharmDuplicate> randomCharmDuplicatesPreview = this.sim.GetRandomCharmDuplicatesPreview(this.sim.shopPackBigCharm.numCharms);
				this.panelCharmPackOpening.charmDupplicatesToOpen = new List<CharmDuplicate>(randomCharmDuplicatesPreview.Count);
				this.panelCharmPackOpening.oldDupplicateCounts = new List<int>(randomCharmDuplicatesPreview.Count);
				this.panelCharmPackOpening.oldLevels = new List<int>(randomCharmDuplicatesPreview.Count);
				this.panelCharmPackOpening.neededCounts = new List<int>(randomCharmDuplicatesPreview.Count);
				foreach (CharmDuplicate charmDuplicate in randomCharmDuplicatesPreview)
				{
					this.panelCharmPackOpening.charmDupplicatesToOpen.Add(charmDuplicate);
					this.panelCharmPackOpening.oldDupplicateCounts.Add(charmDuplicate.charmData.unspendDuplicates);
					this.panelCharmPackOpening.oldLevels.Add(charmDuplicate.charmData.level);
					this.panelCharmPackOpening.neededCounts.Add(CharmEffectData.GetNeededDuplicateToLevelUpFromLevel((charmDuplicate.charmData.level != -1) ? charmDuplicate.charmData.level : 0));
				}
				this.panelCharmPackOpening.SetMultipleHidden();
				this.panelCharmPackOpening.DoMultiPackOpen();
				UICommandCharmPackOpen command = new UICommandCharmPackOpen
				{
					isBigPack = true
				};
				this.panelCharmPackOpening.isBigPack = true;
				this.command = command;
				this.state = UiState.SHOP_CHARM_PACK_OPENING;
			}
			else if (!this.panelCharmPackSelect.isBigPack && this.sim.CanAffordSmallCharmPack())
			{
				CharmDuplicate randomCharmPackPreview = this.sim.GetRandomCharmPackPreview();
				this.panelCharmPackOpening.charmDupplicateToOpen = randomCharmPackPreview;
				this.panelCharmPackOpening.oldDupplicateCount = randomCharmPackPreview.charmData.unspendDuplicates;
				this.panelCharmPackOpening.oldLevel = randomCharmPackPreview.charmData.level;
				this.panelCharmPackOpening.neededCount = CharmEffectData.GetNeededDuplicateToLevelUpFromLevel((randomCharmPackPreview.charmData.level != -1) ? randomCharmPackPreview.charmData.level : 0);
				this.panelCharmPackOpening.SetHidden();
				this.panelCharmPackOpening.DoPackOpen(randomCharmPackPreview.charmData.BaseData.charmType);
				UICommandCharmPackOpen command2 = new UICommandCharmPackOpen
				{
					isBigPack = false
				};
				this.panelCharmPackOpening.isBigPack = false;
				this.command = command2;
				this.state = UiState.SHOP_CHARM_PACK_OPENING;
			}
			else
			{
				this.panelCurrencyWarning.SetCurrency(PanelCurrencyWarning.PopupType.AEONS, this, string.Empty, 0, null);
				this.state = UiState.CURRENCY_WARNING;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiDefaultFailClick, 1f));
			}
		}

		private void OnClickSocialRewardCollect()
		{
			UiCommandFreeCurrencyCollect uiCommandFreeCurrencyCollect = new UiCommandFreeCurrencyCollect();
			uiCommandFreeCurrencyCollect.isSocialReward = true;
			uiCommandFreeCurrencyCollect.socialNetwork = this.panelSocialRewardPopup.selectedSocialNetwork;
			uiCommandFreeCurrencyCollect.rewardCurrencyType = CurrencyType.GEM;
			uiCommandFreeCurrencyCollect.rewardAmount = 50.0;
			if (this.panelShop.isHubMode)
			{
				uiCommandFreeCurrencyCollect.isHubShop = true;
				Transform transform = this.panelShop.socialRewardOffers[(int)this.panelSocialRewardPopup.selectedSocialNetwork].transform;
				DropPosition dropPosition = new DropPosition
				{
					startPos = transform.position,
					endPos = transform.position - Vector3.up * 0.1f,
					invPos = this.panelCurrencyOnTop[0].panelCurrency.GetCurrencyTransform().position - Vector3.right * 0.425f,
					targetToScaleOnReach = this.panelCurrencyOnTop[0].panelCurrency.GetCurrencyTransform(),
					showSideCurrency = true
				};
				uiCommandFreeCurrencyCollect.dropPosition = dropPosition;
			}
			this.command = uiCommandFreeCurrencyCollect;
			if (!this.panelShop.isHubMode)
			{
				this.state = UiState.NONE;
			}
			else
			{
				this.state = UiState.HUB_SHOP;
			}
			UiManager.AddUiSound(SoundArchieve.inst.uiTabSwitch);
			UiManager.AddUiSound(SoundArchieve.inst.uiPurchaseGemPack);
		}

		private void OnClickSocialRewardCancel()
		{
			this.state = this.panelSocialRewardPopup.previousState;
			UiManager.AddUiSound(SoundArchieve.inst.uiPopupDisappear);
		}

		private void OnClickSocialRewardFollow()
		{
			this.state = ((!this.panelShop.isHubMode) ? UiState.SHOP : UiState.HUB_SHOP);
			Manager.PlayerWantsToGoTo(this.panelSocialRewardPopup.selectedSocialNetwork);
		}

		public bool IsAnyUiShown()
		{
			return this.state != UiState.NONE;
		}

		public bool IsInModeSetupMenus()
		{
			return this.IsInModeSetupMenus(this.state);
		}

		public bool IsInHubMenus()
		{
			return this.IsInHubMenus(this.state);
		}

		public bool IsInSeasonalMenu()
		{
			return this.state == UiState.CHRISTMAS_PANEL && !this.IsInHubMenus(this.panelChristmasOffer.previousState);
		}

		public bool IsUiCoveringGame()
		{
			return (this.isMenuHubStateForSound || this.isMenuUpForSound) && !this.uiTabMenuAnim.isAnimating;
		}

		public bool IsInModeSetupMenus(UiState state)
		{
			return state == UiState.MODE_UNLOCK_REWARD || this.IsInHubMenus();
		}

		public bool IsInHubMenus(UiState state, UiState previousState)
		{
			if (state == previousState)
			{
				UnityEngine.Debug.LogError("Asked same ui state in IsInHubMenus, this is illegal");
				return true;
			}
			return this.IsInHubMenus(state);
		}

		public bool IsInHubMenus(UiState state)
		{
			return state == UiState.HUB || state == UiState.SUPPORT_POPUP || state == UiState.HUB_ACHIEVEMENTS || state == UiState.HUB_MODE_SETUP || state == UiState.HUB_OPTIONS || state == UiState.HUB_MODE_SETUP_HERO || state == UiState.HUB_MODE_SETUP_TOTEM || state == UiState.HUB_DATABASE_HEROES_ITEMS || state == UiState.HUB_DATABASE_TOTEMS || state == UiState.HUB_DATABASE_TRINKETS || state == UiState.HUB_DATABASE_EVOLVE || state == UiState.HUB_CREDITS || state == UiState.UPDATE_REQUIRED || (state == UiState.CURRENCY_WARNING && this.currencyWarningInHub) || (state == UiState.GENERAL_POPUP && (this.panelGeneralPopup.state == PanelGeneralPopup.State.OPTIONS || this.panelGeneralPopup.state == PanelGeneralPopup.State.HARD_RESET || this.panelGeneralPopup.state == PanelGeneralPopup.State.HUB_DAILY_SKIP || this.panelGeneralPopup.state == PanelGeneralPopup.State.DATABASE_TRINKET || (this.panelGeneralPopup.state == PanelGeneralPopup.State.CHRISTMAS_SHOP && this.IsInHubMenus(this.panelChristmasOffer.previousState, state)))) || (state == UiState.CHANGE_SKIN && this.panelHeroSkinChanger.oldState == UiState.HUB_DATABASE_HEROES_ITEMS) || (state == UiState.SKIN_BUYING_WARNING && this.panelHeroSkinChanger.oldState == UiState.HUB_DATABASE_HEROES_ITEMS) || state == UiState.HUB_CHARMS || state == UiState.CHARM_INFO_POPUP || state == UiState.HUB_ARTIFACTS || (state == UiState.ARTIFACTS_INFO && this.panelArtifactsInfo.stateToReturn == UiState.HUB_ARTIFACTS) || state == UiState.RIFT_SELECT_POPUP || state == UiState.HUB_SHOP || state == UiState.HUB_DATABASE_HEROES_SKILLS || (state == UiState.CURRENCY_WARNING && this.currencyWarningInHub) || (state == UiState.RIFT_EFFECTS_INFO && (this.panelRiftEffectInfo.stateToReturn == UiState.HUB_MODE_SETUP || this.panelRiftEffectInfo.stateToReturn == UiState.RIFT_SELECT_POPUP)) || (state == UiState.SOCIAL_REWARD_POPUP && this.IsInHubMenus(this.panelSocialRewardPopup.previousState, state)) || state == UiState.HUB_OPTIONS_WIKI || (state == UiState.TRINKET_INFO_POPUP && this.IsInHubMenus(this.panelTrinketInfoPopup.stateToReturn, state)) || (state == UiState.TRINKET_EFFECT_SELECT_POPUP && !this.panelTrinketSmithing.isBattlefield) || (state == UiState.TRINKET_RECYCLE_POPUP && this.IsInHubMenus(this.panelTrinketRecycle.previousState, state)) || (state == UiState.CHRISTMAS_PANEL && this.IsInHubMenus(this.panelChristmasOffer.previousState, state)) || (state == UiState.CHRISTMAS_OFFERS_INFO_POPUP && this.IsInHubMenus(this.panelChristmasOffer.previousState, state)) || (state == UiState.CHRISTMAS_CANDY_TREAT_POPUP && this.IsInHubMenus(this.panelChristmasOffer.previousState, state)) || (state == UiState.SHOP_LOOTPACK_SELECT && (this.shopLootpackSelect.previousState == UiState.HUB_SHOP || (this.shopLootpackSelect.previousState == UiState.SECOND_ANNIVERSARY_POPUP && this.secondAnniversaryPopup.previousState == UiState.HUB_SHOP))) || ((state == UiState.SHOP_LOOTPACK_OPENING || state == UiState.SHOP_LOOTPACK_SUMMARY || state == UiState.SHOP_MINE || state == UiState.BUY_FLASH_OFFER_CHARM || state == UiState.SHOP_CHARM_PACK_SELECT || state == UiState.SHOP_CHARM_PACK_OPENING || state == UiState.SELECT_TRINKET || state == UiState.SHOP_TRINKET_OPEN_POPUP || state == UiState.BUY_FLASH_OFFER) && this.panelShop.isHubMode) || state == UiState.PATCH_NOTES || (state == UiState.ARTIFACTS_CRAFT && this.panelArtifactsCraft.uistateToReturn == UiState.HUB_ARTIFACTS) || ((state == UiState.ARTIFACT_SELECTED_POPUP || state == UiState.ARTIFACT_EVOLVE || state == UiState.POSSIBLE_ARTIFACT_EFFECTS_POPUP) && this.IsInHubMenus(this.panelArtifactPopup.stateToReturn, state)) || state == UiState.ARTIFACT_OVERHAUL || (state == UiState.SECOND_ANNIVERSARY_POPUP && this.secondAnniversaryPopup.previousState == UiState.HUB_SHOP) || (state == UiState.NEW_TAL_MILESTONE_REACHED_POPUP && this.IsInHubMenus(this.newTALMilestoneReachedPopup.previousState)) || (state == UiState.CAN_NOT_EVOLVE_ARTIFACT_POPUP && (this.notEvolveArtifactPopup.previousState == UiState.HUB_ARTIFACTS || (this.notEvolveArtifactPopup.previousState == UiState.ARTIFACT_SELECTED_POPUP && this.panelArtifactPopup.stateToReturn == UiState.HUB_ARTIFACTS))) || (state == UiState.NEW_TAL_MILESTONE_REACHED_POPUP && this.panelArtifactPopup.stateToReturn == UiState.HUB_ARTIFACTS) || (state == UiState.POSSIBLE_ARTIFACT_EFFECTS_POPUP && this.posibleArtifactEffectPopup.stateToReturn == UiState.HUB_ARTIFACTS) || (state == UiState.ARTIFACT_SELECTED_POPUP && this.panelArtifactPopup.stateToReturn == UiState.HUB_ARTIFACTS);
		}

		public bool IsInUnclosableMenus()
		{
			return this.IsInUnclosableMenus(this.state);
		}

		public bool IsInUnclosableMenus(UiState state)
		{
			return state == UiState.ARTIFACTS_CRAFT || state == UiState.SHOP_SKINPACK_OPENING || state == UiState.SHOP_LOOTPACK_OPENING || state == UiState.ARTIFACTS_REROLL || state == UiState.UPDATE_REQUIRED || state == UiState.GENERAL_POPUP || state == UiState.AD_POPUP || state == UiState.HUB_DATABASE_EVOLVE || state == UiState.HEROES_EVOLVE;
		}

		public string GetHeroIdOnScreen()
		{
			return this.selectedHeroGearSkills.GetId();
		}

		public void SetCommand(UiCommand command)
		{
			this.command = command;
		}

		public UiCommand GetCommand()
		{
			UiCommand command = this.command;
			this.command = null;
			return command;
		}

		public bool HasCommand()
		{
			return this.command != null;
		}

		public static string ArtifactLevelString(int level)
		{
			if (level == 0)
			{
				return LM.Get("UI_LEVEL_BASIC");
			}
			if (level == 1)
			{
				return LM.Get("UI_LEVEL_COMMON");
			}
			if (level == 2)
			{
				return LM.Get("UI_LEVEL_UNCOMMON");
			}
			if (level == 3)
			{
				return LM.Get("UI_LEVEL_RARE");
			}
			if (level == 4)
			{
				return LM.Get("UI_LEVEL_EPIC");
			}
			if (level == 5)
			{
				return LM.Get("UI_LEVEL_LEGENDARY");
			}
			if (level == 6)
			{
				return LM.Get("UI_ARTIFACTS_MYTHICAL");
			}
			throw new NotImplementedException();
		}

		public static string GearLevelString(int level)
		{
			if (level == 0)
			{
				return LM.Get("UI_LEVEL_COMMON");
			}
			if (level == 1)
			{
				return LM.Get("UI_LEVEL_UNCOMMON");
			}
			if (level == 2)
			{
				return LM.Get("UI_LEVEL_RARE");
			}
			if (level == 3)
			{
				return LM.Get("UI_LEVEL_EPIC");
			}
			if (level == 4)
			{
				return LM.Get("UI_LEVEL_LEGENDARY");
			}
			if (level == 5)
			{
				return LM.Get("UI_LEVEL_MYTHICAL");
			}
			throw new NotImplementedException();
		}

		public static void Set(Image image, Sprite sprite)
		{
			if (image.sprite != sprite)
			{
				image.sprite = sprite;
			}
		}

		public static void Set(Text text, string t)
		{
			if (text.text != t)
			{
				text.text = t;
			}
		}

		public Vector3 GetImageGoldScale()
		{
			if (this.imageGoldAnimTimer < 0.1f)
			{
				return this.imageGoldScale * Easing.SineEaseOut(this.imageGoldAnimTimer, 1f, 0.299999952f, 0.1f);
			}
			if (this.imageGoldAnimTimer < 0.15f)
			{
				return this.imageGoldScale * 1.3f;
			}
			if (this.imageGoldAnimTimer < 0.22f)
			{
				return this.imageGoldScale * Easing.SineEaseOut(0.22f - this.imageGoldAnimTimer, 1f, 0.299999952f, 0.07f);
			}
			return this.imageGoldScale;
		}

		public static string GetStoreNameString()
		{
			return LM.Get("UI_GOOGLE_PLAY");
		}

		public void SetTabButtonPositions(int numEnabled, GameButton b0, GameButton b1, GameButton b2)
		{
			RectTransform rectTransform = b0.raycastTarget.rectTransform;
			RectTransform rectTransform2 = b1.raycastTarget.rectTransform;
			RectTransform rectTransform3 = b2.raycastTarget.rectTransform;
			if (numEnabled == 2)
			{
				rectTransform.sizeDelta = new Vector2(410f, rectTransform.sizeDelta.y);
				rectTransform.anchoredPosition = new Vector2(-205f, rectTransform.anchoredPosition.y);
				rectTransform.gameObject.SetActive(true);
				rectTransform2.sizeDelta = new Vector2(410f, rectTransform2.sizeDelta.y);
				rectTransform2.anchoredPosition = new Vector2(205f, rectTransform2.anchoredPosition.y);
				rectTransform2.gameObject.SetActive(true);
				rectTransform3.gameObject.SetActive(false);
			}
			else
			{
				if (numEnabled != 3)
				{
					throw new EntryPointNotFoundException();
				}
				rectTransform.sizeDelta = new Vector2(273f, rectTransform.sizeDelta.y);
				rectTransform.anchoredPosition = new Vector2(-273f, rectTransform.anchoredPosition.y);
				rectTransform.gameObject.SetActive(true);
				rectTransform2.sizeDelta = new Vector2(273f, rectTransform2.sizeDelta.y);
				rectTransform2.anchoredPosition = new Vector2(0f, rectTransform2.anchoredPosition.y);
				rectTransform2.gameObject.SetActive(true);
				rectTransform3.sizeDelta = new Vector2(273f, rectTransform3.sizeDelta.y);
				rectTransform3.anchoredPosition = new Vector2(273f, rectTransform3.anchoredPosition.y);
				rectTransform3.gameObject.SetActive(true);
			}
		}

		public static string GetAchievementTitle(string id)
		{
			if (UiManager.achievementTitle.ContainsKey(id))
			{
				string text = LM.Get(UiManager.achievementTitle[id]);
				if (UiManager.achievementTitleFormat.ContainsKey(id))
				{
					text = string.Format(UiManager.achievementTitleFormat[id].Loc(), text);
				}
				return text;
			}
			return string.Empty;
		}

		public static string GetAchievementDesc(string id)
		{
			if (!UiManager.achievementDesc.ContainsKey(id))
			{
				return string.Empty;
			}
			if (!UiManager.achievementDescParam.ContainsKey(id))
			{
				return LM.Get(UiManager.achievementDesc[id]);
			}
			string text = UiManager.achievementDescParam[id];
			if (LM.current.ContainsKey(text))
			{
				return string.Format(LM.Get(UiManager.achievementDesc[id]), LM.Get(text));
			}
			return string.Format(LM.Get(UiManager.achievementDesc[id]), text);
		}

		public void Debug_CreateNewTrinket(TrinketUpgradeReq req, TrinketEffect common, TrinketEffect secondary, TrinketEffect special)
		{
			this.command = new UiCommandCreateTrinket
			{
				common = common,
				secondary = secondary,
				special = special,
				req = req
			};
		}

		private void SendFeedbackMail()
		{
			string value = string.Concat(new string[]
			{
				"id: ",
				PlayfabManager.playerId,
				" | other: ",
				this.GetOtherIds(),
				" | version: ",
				Cheats.version,
				" | ",
				SystemInfo.operatingSystem,
				"\n"
			});
			StringBuilder stringBuilder = new StringBuilder(400);
			stringBuilder.Append("Please give us your feedback:").AppendLine().AppendLine().AppendLine().Append(value);
			Application.OpenURL("mailto:support@beesquare.net?subject=" + GameMath.ClearUrl("[FEEDBACK] AaH Support") + "&body=" + GameMath.ClearUrl(stringBuilder.ToString()));
		}

		public void EnableInputBlocker()
		{
			this.uiInputBlocker.SetActive(true);
		}

		public void DisableInputBlocker()
		{
			this.uiInputBlocker.SetActive(false);
		}

		private void Button_OnRiftDiscover()
		{
			if (this.panelRiftSelect.isOnDiscoverMode)
			{
				this.panelRiftSelect.DoLevelUp(this.sim);
				this.command = new UiCommandDiscoverNextRiftChallenges();
			}
			else
			{
				int riftCountWillDiscover = this.sim.GetRiftCountWillDiscover();
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuUp, 1f));
				this.panelRiftSelect.riftDiscover.text = string.Format(LM.Get("UI_DISCOVER_NEW_RIFTS"), riftCountWillDiscover);
				this.panelRiftSelect.riftDiscoverBonuses.text = string.Concat(new string[]
				{
					LM.Get("UI_DISCOVER_D_4"),
					"\n- ",
					LM.Get("UI_DISCOVER_D_0"),
					"\n- ",
					LM.Get("UI_DISCOVER_D_2"),
					"\n- ",
					LM.Get("UI_DISCOVER_D_3"),
					"\n- ",
					LM.Get("UI_DISCOVER_D_6")
				});
				if (this.sim.IsCursedRiftsModeUnlocked())
				{
					Text riftDiscoverBonuses = this.panelRiftSelect.riftDiscoverBonuses;
					riftDiscoverBonuses.text = riftDiscoverBonuses.text + "\n- " + LM.Get("UI_DISCOVER_D_5");
				}
				this.panelRiftSelect.OpenDiscover();
			}
		}

		private void Button_OnRiftSelected(int selectedRiftIndex)
		{
			if (selectedRiftIndex != -1)
			{
				UiCommandSetActiveRiftChallenge command = new UiCommandSetActiveRiftChallenge
				{
					challengeIndex = selectedRiftIndex,
					isCursed = this.panelRiftSelect.isCurseMode
				};
				this.command = command;
				this.panelHubModeSetup.challengeIndexChanged = true;
			}
		}

		private void Button_OnCharmDraftSelected(int index)
		{
			if (!this.panelCharmSelect.isCurseInfo)
			{
				if (index != -1)
				{
					CharmOptionCard charmOptionCard = this.panelCharmSelect.charmOptionCards[index];
					Vector2 widgetPosition = CharmEffectsPanel.GetWidgetPosition(this.charmEffectsPanel.widgetMatch.objs.Count);
					this.charmEffectsPanel.iconToTransform.sprite = charmOptionCard.charmCard.charmIcon.sprite;
					this.charmEffectsPanel.DoCharmGoAnimation(charmOptionCard.charmCard.charmIcon.rectTransform.position, widgetPosition);
					UiCommandSelectCharmOffer command = new UiCommandSelectCharmOffer
					{
						index = index
					};
					this.command = command;
					this.state = UiState.NONE;
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiWorldUpgrade, 1f));
				}
			}
			else
			{
				this.state = UiState.NONE;
				this.command = new UiCommandSetPauseState
				{
					isPaused = false
				};
			}
		}

		private void Button_OnCharmSelectionClose()
		{
			this.state = UiState.NONE;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
			this.sim.isActiveWorldPaused = false;
		}

		[InspectButton]
		public void OpenTrinketInfoPopup()
		{
			this.panelTrinketInfoPopup.trinketInfoBody.selectedTrinket = this.sim.allTrinkets[0];
			this.panelTrinketInfoPopup.trinketInfoBody.forceToUpdate = true;
			this.state = UiState.TRINKET_INFO_POPUP;
		}

		public void PrepareArtifactOverhaulAnimForFadeOut()
		{
			this.panelArtifactScroller.craftButton.gameObject.SetActive(true);
			this.panelArtifactScroller.unlockButtonWaitingForConfirm = false;
			if (this.panelArtifactScroller.selectedArtifactIndex != -1 && this._state != UiState.CURRENCY_WARNING && this._state != UiState.ARTIFACTS_INFO && this._state != UiState.POSSIBLE_ARTIFACT_EFFECTS_POPUP)
			{
				this.panelArtifactScroller.ResetArtifactSelected();
			}
			this.panelArtifactScroller.CalculateContentSize(this.sim.artifactsManager.AvailableSlotsCount);
			this.panelArtifactScroller.multipleBuyIndex = this.sim.artifactMultiUpgradeIndex;
			this.SetArtifactScrollParent(2);
			this.OpenMenu(UiState.ARTIFACTS_CRAFT, true, -1, -1, new int[]
			{
				48,
				2
			});
			this.panelArtifactScroller.gameObject.SetActive(true);
			bool flag = UiManager.stateJustChanged;
			UiManager.stateJustChanged = true;
			this.UpdateArtifactScroller();
			UiManager.stateJustChanged = flag;
			this.panelArtifactScroller.PrepareForArtifactsAppearAnimAfterConversion();
		}

		private void InitUiData()
		{
			this.uiData = base.GetComponent<UiData>();
			UiData.inst = this.uiData;
			this.spritesSkillIconMainScreen = new Dictionary<Type, Sprite>
			{
				{
					typeof(SkillDataBaseHeHasThePower),
					this.uiData.spriteMainScreenHeHasThePower
				},
				{
					typeof(SkillDataBaseReversedExcalibur),
					this.uiData.spriteMainScreenReversedExcalibur
				},
				{
					typeof(SkillDataBaseDeadlyTwirl),
					this.uiData.spriteMainScreenSpinningDeathTwirl
				},
				{
					typeof(SkillDataBaseConcentration),
					this.uiData.spriteMainScreenConcentration
				},
				{
					typeof(SkillDataBaseFastCheerful),
					this.uiData.spriteMainScreenFastCheerful
				},
				{
					typeof(SkillDataBaseShockWave),
					this.uiData.spriteMainScreenShockWave
				},
				{
					typeof(SkillDataBaseFireworks),
					this.uiData.spriteMainScreenFireworks
				},
				{
					typeof(SkillDataBaseJokesOnYou),
					this.uiData.spriteMainScreenJokesOnYou
				},
				{
					typeof(SkillDataBaseSelfDestruct),
					this.uiData.spriteMainScreenSelfDestruct
				},
				{
					typeof(SkillDataBaseLobMagic),
					this.uiData.spriteMainScreenLobMagic
				},
				{
					typeof(SkillDataBaseOutOfControl),
					this.uiData.spriteMainScreenOutOfControl
				},
				{
					typeof(SkillDataBaseThunderSomething),
					this.uiData.spriteMainScreenThunderSomething
				},
				{
					typeof(SkillDataBaseBombard),
					this.uiData.spriteMainScreenBombard
				},
				{
					typeof(SkillDataBaseEatApple),
					this.uiData.spriteMainScreenEatApple
				},
				{
					typeof(SkillDataBaseMiniCannon),
					this.uiData.spriteMainScreenMiniCannon
				},
				{
					typeof(SkillDataBaseRevenge),
					this.uiData.spriteMainScreenRainOfAxes
				},
				{
					typeof(SkillDataBaseShieldAll),
					this.uiData.spriteMainScreenShieldAll
				},
				{
					typeof(SkillDataBaseSlam),
					this.uiData.spriteMainScreenSlam
				},
				{
					typeof(SkillDataBaseAnger),
					this.uiData.spriteMainScreenAnger
				},
				{
					typeof(SkillDataBaseMasterThief),
					this.uiData.spriteMainScreenMasterThief
				},
				{
					typeof(SkillDataBaseRunEmmetRun),
					this.uiData.spriteMainScreenRunEmmetRun
				},
				{
					typeof(SkillDataBaseSliceDice),
					this.uiData.spriteMainScreenSliceDice
				},
				{
					typeof(SkillDataBaseLunchTime),
					this.uiData.spriteMainScreenLunchTime
				},
				{
					typeof(SkillDataBaseTaunt),
					this.uiData.spriteMainScreenTaunt
				},
				{
					typeof(SkillDataBaseSnipe),
					this.uiData.spriteMainScreenSnipe
				},
				{
					typeof(SkillDataBaseSwiftMoves),
					this.uiData.spriteMainScreenSwiftMoves
				},
				{
					typeof(SkillDataBaseTargetPractice),
					this.uiData.spriteMainScreenTargetPractice
				},
				{
					typeof(SkillDataBaseBattlecry),
					this.uiData.spriteMainScreenBattlecry
				},
				{
					typeof(SkillDataBaseBittersweet),
					this.uiData.spriteMainScreenBittersweet
				},
				{
					typeof(SkillDataBaseWeepingSong),
					this.uiData.spriteMainScreenWeepingSong
				},
				{
					typeof(SkillDataBaseRoar),
					this.uiData.spriteMainScreenRoar
				},
				{
					typeof(SkillDataBaseFlare),
					this.uiData.spriteMainScreenFlare
				},
				{
					typeof(SkillDataBaseCrowAttack),
					this.uiData.spriteMainScreenCrowAttack
				},
				{
					typeof(SkillDataBaseGreedGrenade),
					this.uiData.spriteMainScreenGreedGrenade
				},
				{
					typeof(SkillDataBaseNegotiate),
					this.uiData.spriteMainScreenNegotiate
				},
				{
					typeof(SkillDataBaseCommonAffinities),
					this.uiData.spriteMainScreenCommonAffinities
				},
				{
					typeof(SkillDataBaseDarkRitual),
					this.uiData.spriteMainScreenDarkRitual
				},
				{
					typeof(SkillDataBaseDemonicSwarm),
					this.uiData.spriteMainScreenDemonicSwarm
				},
				{
					typeof(SkillDataBaseRegret),
					this.uiData.spriteMainScreenRegret
				},
				{
					typeof(SkillDataBaseTakeOneForTheTeam),
					this.uiData.spriteMainScreenTakeOneForTheTeam
				},
				{
					typeof(SkillDataBaseGoodCupOfTea),
					this.uiData.spriteMainScreenGoodCupOfTea
				},
				{
					typeof(SkillDataBaseGotYourBack),
					this.uiData.spriteMainScreenGotYourBack
				},
				{
					typeof(SkillDataBaseBeastMode),
					this.uiData.spriteMainScreenBeastMode
				},
				{
					typeof(SkillDataBaseStampede),
					this.uiData.spriteMainScreenStampede
				},
				{
					typeof(SkillDataBaseLarry),
					this.uiData.spriteMainScreenLarry
				}
			};
			this.spritesSkillToggleIconMainScreen = new Dictionary<Type, Sprite>
			{
				{
					typeof(SkillDataBaseDarkRitual),
					this.uiData.spriteMainScreenDarkRitualToggleOff
				}
			};
			this.spritesSkillIconSkillScreen = new Dictionary<Type, Sprite>
			{
				{
					typeof(SkillDataBaseHeHasThePower),
					this.uiData.spriteSkillScreenHeHasThePower
				},
				{
					typeof(SkillDataBaseReversedExcalibur),
					this.uiData.spriteSkillScreenReversedExcalibur
				},
				{
					typeof(SkillDataBaseDeadlyTwirl),
					this.uiData.spriteSkillScreenSpinningDeathTwirl
				},
				{
					typeof(SkillDataBaseDodge),
					this.uiData.spriteSkillScreenDodge
				},
				{
					typeof(SkillDataBaseFriendship),
					this.uiData.spriteSkillScreenFriendship
				},
				{
					typeof(SkillDataBaseLuckyBoy),
					this.uiData.spriteSkillScreenLuckyBoy
				},
				{
					typeof(SkillDataBaseParanoia),
					this.uiData.spriteSkillScreenParanoia
				},
				{
					typeof(SkillDataBaseSharpEdge),
					this.uiData.spriteSkillScreenSharpEdge
				},
				{
					typeof(SkillDataBaseSonOfForest),
					this.uiData.spriteSkillScreenSonOfForest
				},
				{
					typeof(SkillDataBaseSonOfWind),
					this.uiData.spriteSkillScreenSonOfWind
				},
				{
					typeof(SkillEnhancerBaseEverlastingSpin),
					this.uiData.spriteSkillScreenEverlastingSpin
				},
				{
					typeof(SkillDataBaseConcentration),
					this.uiData.spriteSkillScreenConcentration
				},
				{
					typeof(SkillDataBaseFastCheerful),
					this.uiData.spriteSkillScreenFastCheerful
				},
				{
					typeof(SkillDataBaseShockWave),
					this.uiData.spriteSkillScreenShockWave
				},
				{
					typeof(SkillDataBaseChillDown),
					this.uiData.spriteSkillScreenChillDown
				},
				{
					typeof(SkillDataBaseCollectScraps),
					this.uiData.spriteSkillScreenCollectScraps
				},
				{
					typeof(SkillDataBaseForge),
					this.uiData.spriteSkillScreenForge
				},
				{
					typeof(SkillDataBaseHardTraining),
					this.uiData.spriteSkillScreenHardTraining
				},
				{
					typeof(SkillDataBaseMadGirl),
					this.uiData.spriteSkillScreenMadGirl
				},
				{
					typeof(SkillDataBaseRecycle),
					this.uiData.spriteSkillScreenRecycle
				},
				{
					typeof(SkillDataBaseVillageGirl),
					this.uiData.spriteSkillScreenVillageGirl
				},
				{
					typeof(SkillEnhancerBaseEarthquake),
					this.uiData.spriteSkillScreenEarthquake
				},
				{
					typeof(SkillDataBaseFireworks),
					this.uiData.spriteSkillScreenFireworks
				},
				{
					typeof(SkillDataBaseJokesOnYou),
					this.uiData.spriteSkillScreenJokesOnYou
				},
				{
					typeof(SkillDataBaseSelfDestruct),
					this.uiData.spriteSkillScreenSelfDestruct
				},
				{
					typeof(SkillDataBaseConcussion),
					this.uiData.spriteSkillScreenConcussion
				},
				{
					typeof(SkillDataBaseCrackShot),
					this.uiData.spriteSkillScreenCrackShot
				},
				{
					typeof(SkillDataBaseExplosiveShots),
					this.uiData.spriteSkillScreenExplosiveShots
				},
				{
					typeof(SkillDataBaseFragmentation),
					this.uiData.spriteSkillScreenFragmentation
				},
				{
					typeof(SkillDataBaseFuelThemUp),
					this.uiData.spriteSkillScreenFuelThemUp
				},
				{
					typeof(SkillDataBaseStubbernness),
					this.uiData.spriteSkillScreenStubbernness
				},
				{
					typeof(SkillEnhancerBaseMadness),
					this.uiData.spriteSkillScreenMadness
				},
				{
					typeof(SkillDataBaseWhatDoesNotKillYou),
					this.uiData.spriteSkillScreenWhatDoesNotKillYou
				},
				{
					typeof(SkillDataBaseLobMagic),
					this.uiData.spriteSkillScreenLobMagic
				},
				{
					typeof(SkillDataBaseOutOfControl),
					this.uiData.spriteSkillScreenOutOfControl
				},
				{
					typeof(SkillDataBaseThunderSomething),
					this.uiData.spriteSkillScreenThunderSomething
				},
				{
					typeof(SkillDataBaseCraziness),
					this.uiData.spriteSkillScreenCraziness
				},
				{
					typeof(SkillDataBaseDoubleMissile),
					this.uiData.spriteSkillScreenDoubleMissile
				},
				{
					typeof(SkillDataBaseElderness),
					this.uiData.spriteSkillScreenElderness
				},
				{
					typeof(SkillDataBaseForgetful),
					this.uiData.spriteSkillScreenForgetful
				},
				{
					typeof(SkillDataBaseSpiritTalk),
					this.uiData.spriteSkillScreenSpiritTalk
				},
				{
					typeof(SkillDataBaseWisdom),
					this.uiData.spriteSkillScreenWisdom
				},
				{
					typeof(SkillEnhancerBaseRapidThunder),
					this.uiData.spriteSkillScreenRapidThunder
				},
				{
					typeof(SkillEnhancerBaseTricks),
					this.uiData.spriteSkillScreenTricks
				},
				{
					typeof(SkillDataBaseBombard),
					this.uiData.spriteSkillScreenBombard
				},
				{
					typeof(SkillDataBaseEatApple),
					this.uiData.spriteSkillScreenEatApple
				},
				{
					typeof(SkillDataBaseMiniCannon),
					this.uiData.spriteSkillScreenMiniCannon
				},
				{
					typeof(SkillDataBaseFastReloader),
					this.uiData.spriteSkillScreenFastReloader
				},
				{
					typeof(SkillDataBaseNicestKiller),
					this.uiData.spriteSkillScreenNicestKiller
				},
				{
					typeof(SkillDataBaseRottenApple),
					this.uiData.spriteSkillScreenRottenApple
				},
				{
					typeof(SkillDataBaseSharpShooter),
					this.uiData.spriteSkillScreenSharpShooter
				},
				{
					typeof(SkillDataBaseWellFed),
					this.uiData.spriteSkillScreenWellFed
				},
				{
					typeof(SkillDataBaseXxl),
					this.uiData.spriteSkillScreenXxl
				},
				{
					typeof(SkillEnhancerBaseBigStomach),
					this.uiData.spriteSkillScreenBigStomach
				},
				{
					typeof(SkillEnhancerBaseConstitution),
					this.uiData.spriteSkillScreenConstitution
				},
				{
					typeof(SkillDataBaseRevenge),
					this.uiData.spriteSkillScreenRainOfAxes
				},
				{
					typeof(SkillDataBaseShieldAll),
					this.uiData.spriteSkillScreenShieldAll
				},
				{
					typeof(SkillDataBaseSlam),
					this.uiData.spriteSkillScreenSlam
				},
				{
					typeof(SkillDataBaseBlock),
					this.uiData.spriteSkillScreenBlock
				},
				{
					typeof(SkillDataBaseHoldYourGround),
					this.uiData.spriteSkillScreenHoldYourGround
				},
				{
					typeof(SkillEnhancerLetThemCome),
					this.uiData.spriteSkillScreenLetThemCome
				},
				{
					typeof(SkillDataBaseManOfHill),
					this.uiData.spriteSkillScreenManOfHill
				},
				{
					typeof(SkillDataBasePunishment),
					this.uiData.spriteSkillScreenPunishment
				},
				{
					typeof(SkillDataBaseRepel),
					this.uiData.spriteSkillScreenRepel
				},
				{
					typeof(SkillDataBaseTranscendence),
					this.uiData.spriteSkillScreenTranscendence
				},
				{
					typeof(SkillEnhancerMasterShielder),
					this.uiData.spriteSkillScreenMasterShielder
				},
				{
					typeof(SkillDataBaseSliceDice),
					this.uiData.spriteSkillScreenSliceDice
				},
				{
					typeof(SkillDataBaseMasterThief),
					this.uiData.spriteSkillScreenMasterThief
				},
				{
					typeof(SkillDataBaseRunEmmetRun),
					this.uiData.spriteSkillScreenRunEmmetRun
				},
				{
					typeof(SkillDataBaseCityThief),
					this.uiData.spriteSkillScreenCityThief
				},
				{
					typeof(SkillDataBaseEvasion),
					this.uiData.spriteSkillScreenEvasion
				},
				{
					typeof(SkillDataBaseHearthSeeker),
					this.uiData.spriteSkillScreenHearthSeeker
				},
				{
					typeof(SkillDataBaseHiddenDaggers),
					this.uiData.spriteSkillScreenHiddenDaggers
				},
				{
					typeof(SkillDataBasePoisonedDaggers),
					this.uiData.spriteSkillScreenPoisonedDaggers
				},
				{
					typeof(SkillDataBaseTreasureHunter),
					this.uiData.spriteSkillScreenTreasureHunter
				},
				{
					typeof(SkillDataBaseWeakPoint),
					this.uiData.spriteSkillScreenWeakPoint
				},
				{
					typeof(SkillEnhancerBaseSwiftEmmet),
					this.uiData.spriteSkillScreenSwiftEmmet
				},
				{
					typeof(SkillDataBaseAnger),
					this.uiData.spriteSkillScreenAnger
				},
				{
					typeof(SkillDataBaseLunchTime),
					this.uiData.spriteSkillScreenLunchTime
				},
				{
					typeof(SkillDataBaseTaunt),
					this.uiData.spriteSkillScreenTaunt
				},
				{
					typeof(SkillDataBaseAngerManagement),
					this.uiData.spriteSkillScreenAngerManagement
				},
				{
					typeof(SkillDataBaseBash),
					this.uiData.spriteSkillScreenBash
				},
				{
					typeof(SkillDataBaseBigGuy),
					this.uiData.spriteSkillScreenBigGuy
				},
				{
					typeof(SkillDataBaseFullStomach),
					this.uiData.spriteSkillScreenFullStomach
				},
				{
					typeof(SkillDataBaseRegeneration),
					this.uiData.spriteSkillScreenRegeneration
				},
				{
					typeof(SkillDataBaseResilience),
					this.uiData.spriteSkillScreenResilience
				},
				{
					typeof(SkillDataBaseToughness),
					this.uiData.spriteSkillScreenToughness
				},
				{
					typeof(SkillEnhancerBaseFreshMeat),
					this.uiData.spriteSkillScreenFreshMeat
				},
				{
					typeof(SkillDataBaseSnipe),
					this.uiData.spriteSkillScreenSnipe
				},
				{
					typeof(SkillDataBaseSwiftMoves),
					this.uiData.spriteSkillScreenSwiftMoves
				},
				{
					typeof(SkillDataBaseTargetPractice),
					this.uiData.spriteSkillScreenTargetPractice
				},
				{
					typeof(SkillDataBaseAccuracy),
					this.uiData.spriteSkillScreenAccuracy
				},
				{
					typeof(SkillDataBaseBlindNotDeaf),
					this.uiData.spriteSkillBlindNotDeaf
				},
				{
					typeof(SkillDataBaseElegance),
					this.uiData.spriteSkillScreenElegance
				},
				{
					typeof(SkillDataBaseFeedPoor),
					this.uiData.spriteSkillScreenFeedPoor
				},
				{
					typeof(SkillDataBaseMultiShot),
					this.uiData.spriteSkillScreenMultiShot
				},
				{
					typeof(SkillDataBaseOneShot),
					this.uiData.spriteSkillScreenOneShot
				},
				{
					typeof(SkillDataBaseSurvivalist),
					this.uiData.spriteSkillScreenSurvivalist
				},
				{
					typeof(SkillDataBaseTracker),
					this.uiData.spriteSkillScreenTracker
				},
				{
					typeof(SkillDataBaseBattlecry),
					this.uiData.spriteSkillScreenBattlecry
				},
				{
					typeof(SkillDataBaseBittersweet),
					this.uiData.spriteSkillScreenBittersweet
				},
				{
					typeof(SkillDataBaseWeepingSong),
					this.uiData.spriteSkillScreenWeepingSong
				},
				{
					typeof(SkillEnhancerBaseDepression),
					this.uiData.spriteSkillScreenDepression
				},
				{
					typeof(SkillEnhancerBaseHeroism),
					this.uiData.spriteSkillScreenHeroism
				},
				{
					typeof(SkillEnhancerBaseNotSoFast),
					this.uiData.spriteSkillScreenNotSoFast
				},
				{
					typeof(SkillDataBaseDividedWeFall),
					this.uiData.spriteSkillScreenDividedWeFall
				},
				{
					typeof(SkillDataBaseLullaby),
					this.uiData.spriteSkillScreenLullaby
				},
				{
					typeof(SkillDataBasePartyTime),
					this.uiData.spriteSkillScreenPartyTime
				},
				{
					typeof(SkillDataBasePrettyFace),
					this.uiData.spriteSkillScreenPrettyFace
				},
				{
					typeof(SkillDataBaseTogetherWeStand),
					this.uiData.spriteSkillScreenTogetherWeStand
				},
				{
					typeof(SkillDataBaseRoar),
					this.uiData.spriteSkillScreenRoar
				},
				{
					typeof(SkillDataBaseFlare),
					this.uiData.spriteSkillScreenFlare
				},
				{
					typeof(SkillDataBaseMark),
					this.uiData.spriteSkillScreenMark
				},
				{
					typeof(SkillDataBasePreparation),
					this.uiData.spriteSkillScreenPreparation
				},
				{
					typeof(SkillDataBaseBandage),
					this.uiData.spriteSkillScreenBandage
				},
				{
					typeof(SkillDataBaseFeignDeath),
					this.uiData.spriteSkillScreenFeignDeath
				},
				{
					typeof(SkillDataBaseCrowAttack),
					this.uiData.spriteSkillScreenCrowAttack
				},
				{
					typeof(SkillDataBaseFrenzy),
					this.uiData.spriteSkillScreenFrenzy
				},
				{
					typeof(SkillEnhancerBaseInstincts),
					this.uiData.spriteSkillScreenInstincts
				},
				{
					typeof(SkillEnhancerBaseDeathFromAbove),
					this.uiData.spriteSkillScreenDeathFromAbove
				},
				{
					typeof(SkillDataBaseCallOfWild),
					this.uiData.spriteSkillScreenCallOfWild
				},
				{
					typeof(SkillDataBaseNegotiate),
					this.uiData.spriteSkillScreenNegotiate
				},
				{
					typeof(SkillDataBaseKeenNose),
					this.uiData.spriteSkillScreenKeenNose
				},
				{
					typeof(SkillDataBaseTradeSecret),
					this.uiData.spriteSkillScreenTradeSecret
				},
				{
					typeof(SkillEnhancerBaseDistraction),
					this.uiData.spriteSkillScreenDistraction
				},
				{
					typeof(SkillDataBaseLooseChange),
					this.uiData.spriteSkillScreenLooseChange
				},
				{
					typeof(SkillDataBaseCommonAffinities),
					this.uiData.spriteSkillScreenCommonAffinities
				},
				{
					typeof(SkillDataBaseFormerFriends),
					this.uiData.spriteSkillScreenFormerFriends
				},
				{
					typeof(SkillDataBaseConfusingPresence),
					this.uiData.spriteSkillScreenConfusingPresence
				},
				{
					typeof(SkillDataBaseEasyTargets),
					this.uiData.spriteSkillScreenBargainer
				},
				{
					typeof(SkillDataBaseTimidFriends),
					this.uiData.spriteSkillScreenTimidFriends
				},
				{
					typeof(SkillDataBaseGreedGrenade),
					this.uiData.spriteSkillScreenGreedGrenade
				},
				{
					typeof(SkillDataBaseDarkRitual),
					this.uiData.spriteSkillScreenDarkRitual
				},
				{
					typeof(SkillDataBaseDemonicSwarm),
					this.uiData.spriteSkillScreenDemonicSwarm
				},
				{
					typeof(SkillDataBaseRegret),
					this.uiData.spriteSkillScreenRegret
				},
				{
					typeof(SkillDataBaseSoulSacrifice),
					this.uiData.spriteSkillScreenSoulSacrifice
				},
				{
					typeof(SkillDataBaseTasteOfRevege),
					this.uiData.spriteSkillScreenOldHabits
				},
				{
					typeof(SkillEnhancerChakraBooster),
					this.uiData.spriteSkillScreenPracticed
				},
				{
					typeof(SkillEnhancerWarmerSwarm),
					this.uiData.spriteSkillScreenWarmerSwarm
				},
				{
					typeof(SkillDataBaseTerror),
					this.uiData.spriteSkillScreenTerrifying
				},
				{
					typeof(SkillDataBaseStrikeMirror),
					this.uiData.spriteSkillScreenReflectiveWards
				},
				{
					typeof(SkillDataBaseFeelsBetter),
					this.uiData.spriteSkillScreenFeelingBetter
				},
				{
					typeof(SkillEnhancerMyPittyTeam),
					this.uiData.spriteSkillScreenPowerHungry
				},
				{
					typeof(SkillDataBaseTakeOneForTheTeam),
					this.uiData.spriteSkillScreenTakeOneForTheTeam
				},
				{
					typeof(SkillDataBaseGoodCupOfTea),
					this.uiData.spriteSkillScreenGoodCupOfTea
				},
				{
					typeof(SkillDataBaseToughLove),
					this.uiData.spriteSkillScreenToughLove
				},
				{
					typeof(SkillEnhancerLongWinded),
					this.uiData.spriteSkillScreenLongWinded
				},
				{
					typeof(SkillDataBaseMoraleBooster),
					this.uiData.spriteSkillScreenMoraleBooster
				},
				{
					typeof(SkillDataBaseParticipationTrophy),
					this.uiData.spriteSkillScreenContagiousConfidence
				},
				{
					typeof(SkillDataBaseGotYourBack),
					this.uiData.spriteSkillScreenGotYourBack
				},
				{
					typeof(SkillDataBaseBrushItOff),
					this.uiData.spriteSkillScreenBrushItOff
				},
				{
					typeof(SkillDataBaseInnerWorth),
					this.uiData.spriteSkillScreenInnerWorth
				},
				{
					typeof(SkillDataBaseShareTheBurden),
					this.uiData.spriteSkillScreenShareTheBurden
				},
				{
					typeof(SkillDataBaseExpertLoveLore),
					this.uiData.spriteSkillScreenExpertLoveLore
				},
				{
					typeof(SkillDataBaseBeastMode),
					this.uiData.spriteSkillScreenBeastMode
				},
				{
					typeof(SkillDataBaseStampede),
					this.uiData.spriteSkillScreenStampede
				},
				{
					typeof(SkillEnhancerStrengthInNumbers),
					this.uiData.spriteSkillScreenStrengthInNumbers
				},
				{
					typeof(SkillEnhancerMassivePaws),
					this.uiData.spriteSkillScreenMassivePaws
				},
				{
					typeof(SkillDataBaseGoForTheEyes),
					this.uiData.spriteSkillScreenGoForTheEyes
				},
				{
					typeof(SkillDataBaseImpulsive),
					this.uiData.spriteSkillScreenImpulsive
				},
				{
					typeof(SkillDataBaseLarry),
					this.uiData.spriteSkillScreenLarry
				},
				{
					typeof(SkillEnhancerHunterInstinct),
					this.uiData.spriteSkillScreenHuntersInstinct
				},
				{
					typeof(SkillEnhancerCurly),
					this.uiData.spriteSkillScreenCurly
				},
				{
					typeof(SkillEnhancerMoe),
					this.uiData.spriteSkillScreenMoe
				},
				{
					typeof(SkillEnhancerRageDriven),
					this.uiData.spriteSkillScreenRageDriven
				}
			};
			this.spritesGearIcon = new Dictionary<string, Sprite>
			{
				{
					"gear.HORATIO.0",
					this.uiData.spriteGearHoratioTunic
				},
				{
					"gear.HORATIO.1",
					this.uiData.spriteGearHoratioHeadgear
				},
				{
					"gear.HORATIO.2",
					this.uiData.spriteGearHoratioShoes
				},
				{
					"gear.IDA.0",
					this.uiData.spriteGearIdaTong
				},
				{
					"gear.IDA.1",
					this.uiData.spriteGearIdaWristband
				},
				{
					"gear.IDA.2",
					this.uiData.spriteGearIdaBelt
				},
				{
					"gear.BOMBERMAN.0",
					this.uiData.spriteGearAlexLighter
				},
				{
					"gear.BOMBERMAN.1",
					this.uiData.spriteGearAlexCigarettes
				},
				{
					"gear.BOMBERMAN.2",
					this.uiData.spriteGearAlexDynamite
				},
				{
					"gear.DEREK.0",
					this.uiData.spriteGearDerekBookmark
				},
				{
					"gear.DEREK.1",
					this.uiData.spriteGearDerekInkBottle
				},
				{
					"gear.DEREK.2",
					this.uiData.spriteGearDerekHandfulOfFleas
				},
				{
					"gear.KIND_LENNY.0",
					this.uiData.spriteGearLennyCap
				},
				{
					"gear.KIND_LENNY.1",
					this.uiData.spriteGearLennyTunic2
				},
				{
					"gear.KIND_LENNY.2",
					this.uiData.spriteGearLennyCannon
				},
				{
					"gear.SAM.0",
					this.uiData.spriteGearSamNecklace
				},
				{
					"gear.SAM.1",
					this.uiData.spriteGearSamBracers
				},
				{
					"gear.SAM.2",
					this.uiData.spriteGearSamAxeHandle
				},
				{
					"gear.SHEELA.0",
					this.uiData.spriteGearSheelaGoldSack
				},
				{
					"gear.SHEELA.1",
					this.uiData.spriteGearSheelaTunic3
				},
				{
					"gear.SHEELA.2",
					this.uiData.spriteGearSheelaDaggerHilt
				},
				{
					"gear.THOUR.0",
					this.uiData.spriteGearThourPieceOfWood
				},
				{
					"gear.THOUR.1",
					this.uiData.spriteGearThourMeat
				},
				{
					"gear.THOUR.2",
					this.uiData.spriteGearThourFlask
				},
				{
					"gear.BLIND_ARCHER.0",
					this.uiData.spriteGearLiaTBD1
				},
				{
					"gear.BLIND_ARCHER.1",
					this.uiData.spriteGearLiaTBD2
				},
				{
					"gear.BLIND_ARCHER.2",
					this.uiData.spriteGearLiaTBD3
				},
				{
					"gear.JIM.0",
					this.uiData.spriteGearJimTBD1
				},
				{
					"gear.JIM.1",
					this.uiData.spriteGearJimTBD2
				},
				{
					"gear.JIM.2",
					this.uiData.spriteGearJimTBD3
				},
				{
					"gear.TAM.0",
					this.uiData.spriteGearTamGoldenFeather
				},
				{
					"gear.TAM.1",
					this.uiData.spriteGearTamJuicyMeat
				},
				{
					"gear.TAM.2",
					this.uiData.spriteGearTamDeadlyShells
				},
				{
					"gear.WARLOCK.0",
					this.uiData.spriteGearWarlockGoldAmulet
				},
				{
					"gear.WARLOCK.1",
					this.uiData.spriteGearWarlockFriendlySkull
				},
				{
					"gear.WARLOCK.2",
					this.uiData.spriteGearWarlockVialofVileness
				},
				{
					"gear.GOBLIN.0",
					this.uiData.spriteGearGoblinGoldenKey
				},
				{
					"gear.GOBLIN.1",
					this.uiData.spriteGearGoblinSpareLeg
				},
				{
					"gear.GOBLIN.2",
					this.uiData.spriteGearGoblinSpyLens
				},
				{
					"gear.BABU.0",
					this.uiData.spriteGearBabuCupOfTea
				},
				{
					"gear.BABU.1",
					this.uiData.spriteGearBabuMortar
				},
				{
					"gear.BABU.2",
					this.uiData.spriteGearBabuShoes
				},
				{
					"gear.DRUID.0",
					this.uiData.spriteGearDruid02
				},
				{
					"gear.DRUID.1",
					this.uiData.spriteGearDruid03
				},
				{
					"gear.DRUID.2",
					this.uiData.spriteGearDruid01
				}
			};
			this.spritesHeroPortrait = new Dictionary<string, Sprite>
			{
				{
					"HORATIO",
					this.uiData.spriteHeroPortraitHoratio
				},
				{
					"IDA",
					this.uiData.spriteHeroPortraitIda
				},
				{
					"KIND_LENNY",
					this.uiData.spriteHeroPortraitLenny
				},
				{
					"BOMBERMAN",
					this.uiData.spriteHeroPortraitAlex
				},
				{
					"DEREK",
					this.uiData.spriteHeroPortraitDerek
				},
				{
					"SAM",
					this.uiData.spriteHeroPortraitSam
				},
				{
					"SHEELA",
					this.uiData.spriteHeroPortraitSheela
				},
				{
					"THOUR",
					this.uiData.spriteHeroPortraitThour
				},
				{
					"BLIND_ARCHER",
					this.uiData.spriteHeroPortraitBlindArcher
				},
				{
					"JIM",
					this.uiData.spriteHeroPortraitJim
				},
				{
					"TAM",
					this.uiData.spriteHeroPortraitTam
				},
				{
					"GOBLIN",
					this.uiData.spriteHeroPortraitGoblin
				},
				{
					"WARLOCK",
					this.uiData.spriteHeroPortraitWarlock
				},
				{
					"BABU",
					this.uiData.spriteHeroPortraitBabu
				},
				{
					"DRUID",
					this.uiData.spriteHeroPortraitDruid
				}
			};
			this.spritesHeroPortraits = new Dictionary<string, Sprite[]>
			{
				{
					"HORATIO",
					this.uiData.skinIconsHoratio
				},
				{
					"IDA",
					this.uiData.skinIconsVexx
				},
				{
					"KIND_LENNY",
					this.uiData.skinIconsKindLenny
				},
				{
					"BOMBERMAN",
					this.uiData.skinIconsBoomer
				},
				{
					"DEREK",
					this.uiData.skinIconsWendle
				},
				{
					"SAM",
					this.uiData.skinIconsSam
				},
				{
					"SHEELA",
					this.uiData.skinIconsV
				},
				{
					"THOUR",
					this.uiData.skinIconsThour
				},
				{
					"BLIND_ARCHER",
					this.uiData.skinIconsLia
				},
				{
					"JIM",
					this.uiData.skinIconsJim
				},
				{
					"TAM",
					this.uiData.skinIconsTam
				},
				{
					"GOBLIN",
					this.uiData.skinIconsGoblin
				},
				{
					"WARLOCK",
					this.uiData.skinIconsWarlock
				},
				{
					"BABU",
					this.uiData.skinIconsBabu
				},
				{
					"DRUID",
					this.uiData.skinIconsDruid
				}
			};
			this.spritesTotem = new Dictionary<string, Sprite>
			{
				{
					"totemFire",
					this.uiData.spriteTotemFireRuneScreen
				},
				{
					"totemEarth",
					this.uiData.spriteTotemEarthRuneScreen
				},
				{
					"totemLightning",
					this.uiData.spriteTotemLightningRuneScreen
				},
				{
					"totemIce",
					this.uiData.spriteTotemIceRuneScreen
				}
			};
			this.spritesTotemSmall = new Dictionary<string, Sprite>
			{
				{
					"totemFire",
					this.uiData.spriteTotemFireSmall
				},
				{
					"totemEarth",
					this.uiData.spriteTotemEarthSmall
				},
				{
					"totemLightning",
					this.uiData.spriteTotemLightningSmall
				},
				{
					"totemIce",
					this.uiData.spriteTotemIceSmall
				}
			};
			this.spritesTotemShineSmall = new Dictionary<string, Sprite>
			{
				{
					"totemFire",
					this.uiData.spriteTotemFireShineSmall
				},
				{
					"totemEarth",
					this.uiData.spriteTotemEarthShineSmall
				},
				{
					"totemLightning",
					this.uiData.spriteTotemLightningShineSmall
				},
				{
					"totemIce",
					this.uiData.spriteTotemIceShineSmall
				}
			};
			this.runeColorForRing = new Dictionary<string, Color>
			{
				{
					"totemLightning",
					this.uiData.lightningRunesColor
				},
				{
					"totemFire",
					this.uiData.fireRunesColor
				},
				{
					"totemIce",
					this.uiData.iceRunesColor
				},
				{
					"totemEarth",
					this.uiData.earthRunesColor
				}
			};
			this.spritesRune = new Dictionary<string, Sprite>
			{
				{
					RuneIds.FIRE_COOLER,
					this.uiData.spriteRune0
				},
				{
					RuneIds.FIRE_ENCHANTED_FIRE,
					this.uiData.spriteRune1
				},
				{
					RuneIds.FIRE_EXPLOSIVE,
					this.uiData.spriteRune2
				},
				{
					RuneIds.FIRE_FIRE_RESISTANCE,
					this.uiData.spriteRune3
				},
				{
					RuneIds.FIRE_IGNITION,
					this.uiData.spriteRune4
				},
				{
					RuneIds.FIRE_INNER_FIRE,
					this.uiData.spriteRune5
				},
				{
					RuneIds.FIRE_MAGMA,
					this.uiData.spriteRune6
				},
				{
					RuneIds.FIRE_MELTDOWN,
					this.uiData.spriteRune7
				},
				{
					RuneIds.FIRE_LUMINOSITY,
					this.uiData.spriteRune8
				},
				{
					RuneIds.FIRE_BLAZE,
					this.uiData.spriteRune9
				},
				{
					RuneIds.FIRE_HOTWAVE,
					this.uiData.spriteRune10
				},
				{
					RuneIds.ICE_COLD_WIND,
					this.uiData.spriteRune0
				},
				{
					RuneIds.ICE_ICE_BLAST,
					this.uiData.spriteRune1
				},
				{
					RuneIds.ICE_ICE_RAGE,
					this.uiData.spriteRune2
				},
				{
					RuneIds.ICE_LUNAR_BLESSING,
					this.uiData.spriteRune3
				},
				{
					RuneIds.ICE_NOVA,
					this.uiData.spriteRune4
				},
				{
					RuneIds.ICE_SHARPNESS,
					this.uiData.spriteRune5
				},
				{
					RuneIds.ICE_SHATTER,
					this.uiData.spriteRune6
				},
				{
					RuneIds.ICE_STORMER,
					this.uiData.spriteRune7
				},
				{
					RuneIds.ICE_BLEAK,
					this.uiData.spriteRune8
				},
				{
					RuneIds.ICE_GLACIER,
					this.uiData.spriteRune9
				},
				{
					RuneIds.ICE_STINGER,
					this.uiData.spriteRune10
				},
				{
					RuneIds.LIGHTNING_BOUNCE,
					this.uiData.spriteRune0
				},
				{
					RuneIds.LIGHTNING_CHARGE,
					this.uiData.spriteRune1
				},
				{
					RuneIds.LIGHTNING_DAZE,
					this.uiData.spriteRune2
				},
				{
					RuneIds.LIGHTNING_ENERGY,
					this.uiData.spriteRune3
				},
				{
					RuneIds.LIGHTNING_OVERLOAD,
					this.uiData.spriteRune4
				},
				{
					RuneIds.LIGHTNING_SHOCK,
					this.uiData.spriteRune5
				},
				{
					RuneIds.LIGHTNING_THUNDER,
					this.uiData.spriteRune6
				},
				{
					RuneIds.LIGHTNING_ZAP,
					this.uiData.spriteRune7
				},
				{
					RuneIds.LIGHTNING_RASH,
					this.uiData.spriteRune8
				},
				{
					RuneIds.LIGHTNING_DISCHARGE,
					this.uiData.spriteRune9
				},
				{
					RuneIds.EARTH_METEORITE,
					this.uiData.spriteRune0
				},
				{
					RuneIds.EARTH_SPIRITUAL,
					this.uiData.spriteRune1
				},
				{
					RuneIds.EARTH_STARFALL,
					this.uiData.spriteRune2
				},
				{
					RuneIds.EARTH_ENCHANT,
					this.uiData.spriteRune3
				},
				{
					RuneIds.EARTH_RECYCLE,
					this.uiData.spriteRune4
				},
				{
					RuneIds.EARTH_REMNANTS,
					this.uiData.spriteRune5
				},
				{
					RuneIds.EARTH_SMASH,
					this.uiData.spriteRune6
				},
				{
					RuneIds.EARTH_WISHFUL,
					this.uiData.spriteRune7
				},
				{
					RuneIds.EARTH_ABUDANCE,
					this.uiData.spriteRune8
				},
				{
					RuneIds.EARTH_9,
					this.uiData.spriteRune9
				},
				{
					RuneIds.EARTH_10,
					this.uiData.spriteRune10
				}
			};
			this.spritesMerchantItem = new Dictionary<string, Sprite>
			{
				{
					"AUTO_TAP",
					this.uiData.spriteMerchantItemAutoTap
				},
				{
					"CLOCK",
					this.uiData.spriteMerchantItemClock
				},
				{
					"GOLD_PACK",
					this.uiData.spriteMerchantItemGoldPack
				},
				{
					"POWER_UP",
					this.uiData.spriteMerchantItemPowerUp
				},
				{
					"REFRESHER_ORB",
					this.uiData.spriteMerchantItemRefresherOrb
				},
				{
					"SHIELD",
					this.uiData.spriteMerchantItemShield
				},
				{
					"GOLD_BOOST",
					this.uiData.spriteMerchantItemGoldBoost
				},
				{
					"WAVE_CLEAR",
					this.uiData.spriteMerchantItemWaveClear
				},
				{
					"TRAINING_BOOK",
					this.uiData.spriteMerchantItemTrainingBook
				},
				{
					"WARP_TIME",
					this.uiData.spriteMerchantItemWarpTime
				},
				{
					"CHARM_CATALYST",
					this.uiData.spriteMerchantItemCatalyst
				},
				{
					"CHARM_VARIETY",
					this.uiData.spriteMerchantItemVariety
				},
				{
					"EMERGENCY_CHARM",
					this.uiData.spriteMerchantItemEmergency
				},
				{
					"PICK_RANDOM_CHARMS",
					this.uiData.spriteMerchantItemPickRandomCharm
				},
				{
					"BLIZZARD",
					this.uiData.spriteMerchantItemBlizzard
				},
				{
					"HOT_COCOA",
					this.uiData.spriteMerchantItemHotCocoa
				},
				{
					"ORNAMENT_DROP",
					this.uiData.spriteMerchantItemOrnamentDrop
				}
			};
			this.spritesMerchantItemSmall = new Dictionary<Type, Sprite>
			{
				{
					typeof(MerchantItemAutoTap),
					this.uiData.spriteMerchantItemSmallAutoTap
				},
				{
					typeof(MerchantItemRefresherOrb),
					this.uiData.spriteMerchantItemSmallRefresherOrb
				},
				{
					typeof(MerchantItemShield),
					this.uiData.spriteMerchantItemSmallShield
				},
				{
					typeof(MerchantItemGoldBoost),
					this.uiData.spriteMerchantItemSmallGoldBoost
				},
				{
					typeof(MerchantItemPowerUp),
					this.uiData.spriteMerchantItemSmallTrainingBook
				},
				{
					typeof(MerchantItemTimeWarp),
					this.uiData.spriteMerchantItemSmallWarpTime
				},
				{
					typeof(MerchantItemClock),
					this.uiData.spriteMerchantItemSmallClock
				},
				{
					typeof(MerchantItemCatalyst),
					this.uiData.spriteMerchantItemSmallCatalyst
				},
				{
					typeof(DropPowerupCooldown),
					this.uiData.spritePowerUpBoostCooldown
				},
				{
					typeof(DropPowerupNonCritDamage),
					this.uiData.spritePowerUpBoostCrit
				},
				{
					typeof(DropPowerupRevive),
					this.uiData.spritePowerUpBoostRevive
				},
				{
					typeof(MerchantItemBlizzard),
					this.uiData.spriteMerchantItemSmallBlizzard
				},
				{
					typeof(MerchantItemHotCocoa),
					this.uiData.spriteMerchantItemSmallHotCocoa
				},
				{
					typeof(MerchantItemOrnamentDrop),
					this.uiData.spriteMerchantItemSmallOrnamentDrop
				}
			};
			this.spritesModeFlag = new Dictionary<GameMode, Sprite>
			{
				{
					GameMode.STANDARD,
					this.uiData.spriteModeFlagStandard
				},
				{
					GameMode.CRUSADE,
					this.uiData.spriteModeFlagCrusade
				},
				{
					GameMode.RIFT,
					this.uiData.spriteModeFlagRift
				}
			};
			this.spritesBossPortrait = new Dictionary<string, Sprite>
			{
				{
					"BOSS",
					this.uiData.spriteBossPortraitOrc
				},
				{
					"BOSS ELF",
					this.uiData.spriteBossPortraitElf
				},
				{
					"BOSS DWARF",
					this.uiData.spriteBossPortraitDwarf
				},
				{
					"BOSS HUMAN",
					this.uiData.spriteBossPortraitBandit
				},
				{
					"BOSS MANGOLIES",
					this.uiData.spriteBossPortraitMagolies
				},
				{
					"BOSS SNOWMAN",
					this.uiData.spriteBossPortraitSnowman
				}
			};
			this.spritesAchievements = new Dictionary<string, Sprite>
			{
				{
					"CgkIlpSuuo0ZEAIQBw",
					this.uiData.spriteAchievementTap
				},
				{
					"CgkIlpSuuo0ZEAIQCA",
					this.uiData.spriteAchievementTap
				},
				{
					"CgkIlpSuuo0ZEAIQCQ",
					this.uiData.spriteAchievementTap
				},
				{
					"CgkIlpSuuo0ZEAIQCg",
					this.uiData.spriteAchievementTap
				},
				{
					"CgkIlpSuuo0ZEAIQCw",
					this.uiData.spriteAchievementTap
				},
				{
					"CgkIlpSuuo0ZEAIQDQ",
					this.uiData.spriteAchievementPrestige
				},
				{
					"CgkIlpSuuo0ZEAIQDg",
					this.uiData.spriteAchievementPrestige
				},
				{
					"CgkIlpSuuo0ZEAIQDw",
					this.uiData.spriteAchievementPrestige
				},
				{
					"CgkIlpSuuo0ZEAIQEA",
					this.uiData.spriteAchievementPrestige
				},
				{
					"CgkIlpSuuo0ZEAIQEQ",
					this.uiData.spriteAchievementPrestige
				},
				{
					"CgkIlpSuuo0ZEAIQEg",
					this.uiData.spriteAchievementTimeChallenge
				},
				{
					"CgkIlpSuuo0ZEAIQEw",
					this.uiData.spriteAchievementTimeChallenge
				},
				{
					"CgkIlpSuuo0ZEAIQFA",
					this.uiData.spriteAchievementTimeChallenge
				},
				{
					"CgkIlpSuuo0ZEAIQFQ",
					this.uiData.spriteAchievementTimeChallenge
				},
				{
					"CgkIlpSuuo0ZEAIQFg",
					this.uiData.spriteAchievementTimeChallenge
				},
				{
					"CgkIlpSuuo0ZEAIQFw",
					this.uiData.spriteAchievementEvolve
				},
				{
					"CgkIlpSuuo0ZEAIQGA",
					this.uiData.spriteAchievementEvolve
				},
				{
					"CgkIlpSuuo0ZEAIQGQ",
					this.uiData.spriteAchievementEvolve
				},
				{
					"CgkIlpSuuo0ZEAIQGw",
					this.uiData.spriteAchievementEvolve
				},
				{
					"CgkIlpSuuo0ZEAIQGg",
					this.uiData.spriteAchievementEvolve
				},
				{
					"CgkIlpSuuo0ZEAIQLA",
					this.uiData.spriteAchievementEvolve
				},
				{
					"CgkIlpSuuo0ZEAIQHA",
					this.uiData.spriteAchievementEvolveAll
				},
				{
					"CgkIlpSuuo0ZEAIQHQ",
					this.uiData.spriteAchievementEvolveAll
				},
				{
					"CgkIlpSuuo0ZEAIQHg",
					this.uiData.spriteAchievementEvolveAll
				},
				{
					"CgkIlpSuuo0ZEAIQIA",
					this.uiData.spriteAchievementEvolveAll
				},
				{
					"CgkIlpSuuo0ZEAIQHw",
					this.uiData.spriteAchievementEvolveAll
				},
				{
					"CgkIlpSuuo0ZEAIQKw",
					this.uiData.spriteAchievementEvolveAll
				},
				{
					"CgkIlpSuuo0ZEAIQIQ",
					this.uiData.spriteAchievementCraftArtifact
				},
				{
					"CgkIlpSuuo0ZEAIQIg",
					this.uiData.spriteAchievementCraftArtifact
				},
				{
					"CgkIlpSuuo0ZEAIQIw",
					this.uiData.spriteAchievementCraftArtifact
				},
				{
					"CgkIlpSuuo0ZEAIQJQ",
					this.uiData.spriteAchievementCraftArtifact
				},
				{
					"CgkIlpSuuo0ZEAIQJA",
					this.uiData.spriteAchievementCraftArtifact
				},
				{
					"CgkIlpSuuo0ZEAIQPQ",
					this.uiData.spriteAchievementArtifactQuality
				},
				{
					"CgkIlpSuuo0ZEAIQPg",
					this.uiData.spriteAchievementArtifactQuality
				},
				{
					"CgkIlpSuuo0ZEAIQPw",
					this.uiData.spriteAchievementArtifactQuality
				},
				{
					"CgkIlpSuuo0ZEAIQQA",
					this.uiData.spriteAchievementArtifactQuality
				},
				{
					"CgkIlpSuuo0ZEAIQQQ",
					this.uiData.spriteAchievementArtifactQuality
				},
				{
					"CgkIlpSuuo0ZEAIQMw",
					this.uiData.spriteAchievementCollectRune
				},
				{
					"CgkIlpSuuo0ZEAIQNA",
					this.uiData.spriteAchievementCollectRune
				},
				{
					"CgkIlpSuuo0ZEAIQNQ",
					this.uiData.spriteAchievementCollectRune
				},
				{
					"CgkIlpSuuo0ZEAIQNg",
					this.uiData.spriteAchievementCollectRune
				},
				{
					"CgkIlpSuuo0ZEAIQNw",
					this.uiData.spriteAchievementCollectRune
				},
				{
					"CgkIlpSuuo0ZEAIQLQ",
					this.uiData.spriteAchievementCompleteSideQuest
				},
				{
					"CgkIlpSuuo0ZEAIQLg",
					this.uiData.spriteAchievementCompleteSideQuest
				},
				{
					"CgkIlpSuuo0ZEAIQLw",
					this.uiData.spriteAchievementCompleteSideQuest
				},
				{
					"CgkIlpSuuo0ZEAIQMA",
					this.uiData.spriteAchievementCompleteSideQuest
				},
				{
					"CgkIlpSuuo0ZEAIQMQ",
					this.uiData.spriteAchievementCompleteSideQuest
				}
			};
			UiManager.achievementTitleFormat = new Dictionary<string, string>
			{
				{
					"CgkIlpSuuo0ZEAIQPQ",
					"ACHIEVEMENT_TITLE_ARTIFACT_QUALITY_NEW"
				},
				{
					"CgkIlpSuuo0ZEAIQPg",
					"ACHIEVEMENT_TITLE_ARTIFACT_QUALITY_NEW"
				},
				{
					"CgkIlpSuuo0ZEAIQPw",
					"ACHIEVEMENT_TITLE_ARTIFACT_QUALITY_NEW"
				},
				{
					"CgkIlpSuuo0ZEAIQQA",
					"ACHIEVEMENT_TITLE_ARTIFACT_QUALITY_NEW"
				},
				{
					"CgkIlpSuuo0ZEAIQQQ",
					"ACHIEVEMENT_TITLE_ARTIFACT_QUALITY_NEW"
				}
			};
			UiManager.achievementTitle = new Dictionary<string, string>
			{
				{
					"CgkIlpSuuo0ZEAIQBw",
					"ACHIEVEMENT_TITLE_TAP_1"
				},
				{
					"CgkIlpSuuo0ZEAIQCA",
					"ACHIEVEMENT_TITLE_TAP_2"
				},
				{
					"CgkIlpSuuo0ZEAIQCQ",
					"ACHIEVEMENT_TITLE_TAP_3"
				},
				{
					"CgkIlpSuuo0ZEAIQCg",
					"ACHIEVEMENT_TITLE_TAP_4"
				},
				{
					"CgkIlpSuuo0ZEAIQCw",
					"ACHIEVEMENT_TITLE_TAP_5"
				},
				{
					"CgkIlpSuuo0ZEAIQDQ",
					"ACHIEVEMENT_TITLE_PRESTIGE_1"
				},
				{
					"CgkIlpSuuo0ZEAIQDg",
					"ACHIEVEMENT_TITLE_PRESTIGE_2"
				},
				{
					"CgkIlpSuuo0ZEAIQDw",
					"ACHIEVEMENT_TITLE_PRESTIGE_3"
				},
				{
					"CgkIlpSuuo0ZEAIQEA",
					"ACHIEVEMENT_TITLE_PRESTIGE_4"
				},
				{
					"CgkIlpSuuo0ZEAIQEQ",
					"ACHIEVEMENT_TITLE_PRESTIGE_5"
				},
				{
					"CgkIlpSuuo0ZEAIQEg",
					"ACHIEVEMENT_TITLE_TIME_CHALLENGE_1"
				},
				{
					"CgkIlpSuuo0ZEAIQEw",
					"ACHIEVEMENT_TITLE_TIME_CHALLENGE_2"
				},
				{
					"CgkIlpSuuo0ZEAIQFA",
					"ACHIEVEMENT_TITLE_TIME_CHALLENGE_3"
				},
				{
					"CgkIlpSuuo0ZEAIQFQ",
					"ACHIEVEMENT_TITLE_TIME_CHALLENGE_4"
				},
				{
					"CgkIlpSuuo0ZEAIQFg",
					"ACHIEVEMENT_TITLE_TIME_CHALLENGE_5"
				},
				{
					"CgkIlpSuuo0ZEAIQFw",
					"ACHIEVEMENT_TITLE_EVOLVE_1"
				},
				{
					"CgkIlpSuuo0ZEAIQGA",
					"ACHIEVEMENT_TITLE_EVOLVE_2"
				},
				{
					"CgkIlpSuuo0ZEAIQGQ",
					"ACHIEVEMENT_TITLE_EVOLVE_3"
				},
				{
					"CgkIlpSuuo0ZEAIQGw",
					"ACHIEVEMENT_TITLE_EVOLVE_4"
				},
				{
					"CgkIlpSuuo0ZEAIQGg",
					"ACHIEVEMENT_TITLE_EVOLVE_5"
				},
				{
					"CgkIlpSuuo0ZEAIQLA",
					"ACHIEVEMENT_TITLE_EVOLVE_6"
				},
				{
					"CgkIlpSuuo0ZEAIQHA",
					"ACHIEVEMENT_TITLE_EVOLVE_ALL_1"
				},
				{
					"CgkIlpSuuo0ZEAIQHQ",
					"ACHIEVEMENT_TITLE_EVOLVE_ALL_2"
				},
				{
					"CgkIlpSuuo0ZEAIQHg",
					"ACHIEVEMENT_TITLE_EVOLVE_ALL_3"
				},
				{
					"CgkIlpSuuo0ZEAIQIA",
					"ACHIEVEMENT_TITLE_EVOLVE_ALL_4"
				},
				{
					"CgkIlpSuuo0ZEAIQHw",
					"ACHIEVEMENT_TITLE_EVOLVE_ALL_5"
				},
				{
					"CgkIlpSuuo0ZEAIQKw",
					"ACHIEVEMENT_TITLE_EVOLVE_ALL_6"
				},
				{
					"CgkIlpSuuo0ZEAIQIQ",
					"ACHIEVEMENT_TITLE_CRAFT_ARTIFACT_1"
				},
				{
					"CgkIlpSuuo0ZEAIQIg",
					"ACHIEVEMENT_TITLE_CRAFT_ARTIFACT_2"
				},
				{
					"CgkIlpSuuo0ZEAIQIw",
					"ACHIEVEMENT_TITLE_CRAFT_ARTIFACT_3"
				},
				{
					"CgkIlpSuuo0ZEAIQJQ",
					"ACHIEVEMENT_TITLE_CRAFT_ARTIFACT_4"
				},
				{
					"CgkIlpSuuo0ZEAIQJA",
					"ACHIEVEMENT_TITLE_CRAFT_ARTIFACT_5"
				},
				{
					"CgkIlpSuuo0ZEAIQPQ",
					"ACHIEVEMENT_TITLE_ARTIFACT_QUALITY_1"
				},
				{
					"CgkIlpSuuo0ZEAIQPg",
					"ACHIEVEMENT_TITLE_ARTIFACT_QUALITY_2"
				},
				{
					"CgkIlpSuuo0ZEAIQPw",
					"ACHIEVEMENT_TITLE_ARTIFACT_QUALITY_3"
				},
				{
					"CgkIlpSuuo0ZEAIQQA",
					"ACHIEVEMENT_TITLE_ARTIFACT_QUALITY_4"
				},
				{
					"CgkIlpSuuo0ZEAIQQQ",
					"ACHIEVEMENT_TITLE_ARTIFACT_QUALITY_5"
				},
				{
					"CgkIlpSuuo0ZEAIQMw",
					"ACHIEVEMENT_TITLE_COLLECT_RUNES_1"
				},
				{
					"CgkIlpSuuo0ZEAIQNA",
					"ACHIEVEMENT_TITLE_COLLECT_RUNES_2"
				},
				{
					"CgkIlpSuuo0ZEAIQNQ",
					"ACHIEVEMENT_TITLE_COLLECT_RUNES_3"
				},
				{
					"CgkIlpSuuo0ZEAIQNg",
					"ACHIEVEMENT_TITLE_COLLECT_RUNES_4"
				},
				{
					"CgkIlpSuuo0ZEAIQNw",
					"ACHIEVEMENT_TITLE_COLLECT_RUNES_5"
				},
				{
					"CgkIlpSuuo0ZEAIQLQ",
					"ACHIEVEMENT_TITLE_COMPLETE_SIDE_QUEST_1"
				},
				{
					"CgkIlpSuuo0ZEAIQLg",
					"ACHIEVEMENT_TITLE_COMPLETE_SIDE_QUEST_2"
				},
				{
					"CgkIlpSuuo0ZEAIQLw",
					"ACHIEVEMENT_TITLE_COMPLETE_SIDE_QUEST_3"
				},
				{
					"CgkIlpSuuo0ZEAIQMA",
					"ACHIEVEMENT_TITLE_COMPLETE_SIDE_QUEST_4"
				},
				{
					"CgkIlpSuuo0ZEAIQMQ",
					"ACHIEVEMENT_TITLE_COMPLETE_SIDE_QUEST_5"
				}
			};
			UiManager.achievementDesc = new Dictionary<string, string>
			{
				{
					"CgkIlpSuuo0ZEAIQBw",
					"ACHIEVEMENT_DESC_TAP_1"
				},
				{
					"CgkIlpSuuo0ZEAIQCA",
					"ACHIEVEMENT_DESC_TAP_1"
				},
				{
					"CgkIlpSuuo0ZEAIQCQ",
					"ACHIEVEMENT_DESC_TAP_1"
				},
				{
					"CgkIlpSuuo0ZEAIQCg",
					"ACHIEVEMENT_DESC_TAP_1"
				},
				{
					"CgkIlpSuuo0ZEAIQCw",
					"ACHIEVEMENT_DESC_TAP_1"
				},
				{
					"CgkIlpSuuo0ZEAIQDQ",
					"ACHIEVEMENT_DESC_PRESTIGE_1"
				},
				{
					"CgkIlpSuuo0ZEAIQDg",
					"ACHIEVEMENT_DESC_PRESTIGE_2"
				},
				{
					"CgkIlpSuuo0ZEAIQDw",
					"ACHIEVEMENT_DESC_PRESTIGE_2"
				},
				{
					"CgkIlpSuuo0ZEAIQEA",
					"ACHIEVEMENT_DESC_PRESTIGE_2"
				},
				{
					"CgkIlpSuuo0ZEAIQEQ",
					"ACHIEVEMENT_DESC_PRESTIGE_2"
				},
				{
					"CgkIlpSuuo0ZEAIQEg",
					"ACHIEVEMENT_DESC_TIME_CHALLENGE_1"
				},
				{
					"CgkIlpSuuo0ZEAIQEw",
					"ACHIEVEMENT_DESC_TIME_CHALLENGE_2"
				},
				{
					"CgkIlpSuuo0ZEAIQFA",
					"ACHIEVEMENT_DESC_TIME_CHALLENGE_2"
				},
				{
					"CgkIlpSuuo0ZEAIQFQ",
					"ACHIEVEMENT_DESC_TIME_CHALLENGE_2"
				},
				{
					"CgkIlpSuuo0ZEAIQFg",
					"ACHIEVEMENT_DESC_TIME_CHALLENGE_2"
				},
				{
					"CgkIlpSuuo0ZEAIQFw",
					"ACHIEVEMENT_DESC_EVOLVE_1"
				},
				{
					"CgkIlpSuuo0ZEAIQGA",
					"ACHIEVEMENT_DESC_EVOLVE_1"
				},
				{
					"CgkIlpSuuo0ZEAIQGQ",
					"ACHIEVEMENT_DESC_EVOLVE_1"
				},
				{
					"CgkIlpSuuo0ZEAIQGw",
					"ACHIEVEMENT_DESC_EVOLVE_1"
				},
				{
					"CgkIlpSuuo0ZEAIQGg",
					"ACHIEVEMENT_DESC_EVOLVE_1"
				},
				{
					"CgkIlpSuuo0ZEAIQLA",
					"ACHIEVEMENT_DESC_EVOLVE_1"
				},
				{
					"CgkIlpSuuo0ZEAIQHA",
					"ACHIEVEMENT_DESC_EVOLVE_ALL_1"
				},
				{
					"CgkIlpSuuo0ZEAIQHQ",
					"ACHIEVEMENT_DESC_EVOLVE_ALL_1"
				},
				{
					"CgkIlpSuuo0ZEAIQHg",
					"ACHIEVEMENT_DESC_EVOLVE_ALL_1"
				},
				{
					"CgkIlpSuuo0ZEAIQIA",
					"ACHIEVEMENT_DESC_EVOLVE_ALL_1"
				},
				{
					"CgkIlpSuuo0ZEAIQHw",
					"ACHIEVEMENT_DESC_EVOLVE_ALL_1"
				},
				{
					"CgkIlpSuuo0ZEAIQKw",
					"ACHIEVEMENT_DESC_EVOLVE_ALL_1"
				},
				{
					"CgkIlpSuuo0ZEAIQIQ",
					"ACHIEVEMENT_DESC_CRAFT_ARTIFACT_1"
				},
				{
					"CgkIlpSuuo0ZEAIQIg",
					"ACHIEVEMENT_DESC_CRAFT_ARTIFACT_2"
				},
				{
					"CgkIlpSuuo0ZEAIQIw",
					"ACHIEVEMENT_DESC_CRAFT_ARTIFACT_2"
				},
				{
					"CgkIlpSuuo0ZEAIQJQ",
					"ACHIEVEMENT_DESC_CRAFT_ARTIFACT_2"
				},
				{
					"CgkIlpSuuo0ZEAIQJA",
					"ACHIEVEMENT_DESC_CRAFT_ARTIFACT_2"
				},
				{
					"CgkIlpSuuo0ZEAIQPQ",
					"ACHIEVEMENT_DESC_ARTIFACT_TAL"
				},
				{
					"CgkIlpSuuo0ZEAIQPg",
					"ACHIEVEMENT_DESC_ARTIFACT_TAL"
				},
				{
					"CgkIlpSuuo0ZEAIQPw",
					"ACHIEVEMENT_DESC_ARTIFACT_TAL"
				},
				{
					"CgkIlpSuuo0ZEAIQQA",
					"ACHIEVEMENT_DESC_ARTIFACT_TAL"
				},
				{
					"CgkIlpSuuo0ZEAIQQQ",
					"ACHIEVEMENT_DESC_ARTIFACT_TAL"
				},
				{
					"CgkIlpSuuo0ZEAIQMw",
					"ACHIEVEMENT_DESC_COLLECT_RUNES_1"
				},
				{
					"CgkIlpSuuo0ZEAIQNA",
					"ACHIEVEMENT_DESC_COLLECT_RUNES_2"
				},
				{
					"CgkIlpSuuo0ZEAIQNQ",
					"ACHIEVEMENT_DESC_COLLECT_RUNES_2"
				},
				{
					"CgkIlpSuuo0ZEAIQNg",
					"ACHIEVEMENT_DESC_COLLECT_RUNES_2"
				},
				{
					"CgkIlpSuuo0ZEAIQNw",
					"ACHIEVEMENT_DESC_COLLECT_RUNES_2"
				},
				{
					"CgkIlpSuuo0ZEAIQLQ",
					"ACHIEVEMENT_DESC_COMPLETE_SIDE_QUEST_1"
				},
				{
					"CgkIlpSuuo0ZEAIQLg",
					"ACHIEVEMENT_DESC_COMPLETE_SIDE_QUEST_2"
				},
				{
					"CgkIlpSuuo0ZEAIQLw",
					"ACHIEVEMENT_DESC_COMPLETE_SIDE_QUEST_2"
				},
				{
					"CgkIlpSuuo0ZEAIQMA",
					"ACHIEVEMENT_DESC_COMPLETE_SIDE_QUEST_2"
				},
				{
					"CgkIlpSuuo0ZEAIQMQ",
					"ACHIEVEMENT_DESC_COMPLETE_SIDE_QUEST_2"
				}
			};
			UiManager.achievementDescParam = new Dictionary<string, string>
			{
				{
					"CgkIlpSuuo0ZEAIQBw",
					"2K"
				},
				{
					"CgkIlpSuuo0ZEAIQCA",
					"50K"
				},
				{
					"CgkIlpSuuo0ZEAIQCQ",
					"200K"
				},
				{
					"CgkIlpSuuo0ZEAIQCg",
					"500K"
				},
				{
					"CgkIlpSuuo0ZEAIQCw",
					"1M"
				},
				{
					"CgkIlpSuuo0ZEAIQDg",
					"5"
				},
				{
					"CgkIlpSuuo0ZEAIQDw",
					"25"
				},
				{
					"CgkIlpSuuo0ZEAIQEA",
					"50"
				},
				{
					"CgkIlpSuuo0ZEAIQEQ",
					"100"
				},
				{
					"CgkIlpSuuo0ZEAIQEw",
					"5"
				},
				{
					"CgkIlpSuuo0ZEAIQFA",
					"10"
				},
				{
					"CgkIlpSuuo0ZEAIQFQ",
					"15"
				},
				{
					"CgkIlpSuuo0ZEAIQFg",
					"20"
				},
				{
					"CgkIlpSuuo0ZEAIQFw",
					"UI_LEVEL_COMMON"
				},
				{
					"CgkIlpSuuo0ZEAIQGA",
					"UI_LEVEL_UNCOMMON"
				},
				{
					"CgkIlpSuuo0ZEAIQGQ",
					"UI_LEVEL_RARE"
				},
				{
					"CgkIlpSuuo0ZEAIQGw",
					"UI_LEVEL_EPIC"
				},
				{
					"CgkIlpSuuo0ZEAIQGg",
					"UI_LEVEL_LEGENDARY"
				},
				{
					"CgkIlpSuuo0ZEAIQLA",
					"UI_LEVEL_MYTHICAL"
				},
				{
					"CgkIlpSuuo0ZEAIQHA",
					"UI_LEVEL_COMMON"
				},
				{
					"CgkIlpSuuo0ZEAIQHQ",
					"UI_LEVEL_UNCOMMON"
				},
				{
					"CgkIlpSuuo0ZEAIQHg",
					"UI_LEVEL_RARE"
				},
				{
					"CgkIlpSuuo0ZEAIQIA",
					"UI_LEVEL_EPIC"
				},
				{
					"CgkIlpSuuo0ZEAIQHw",
					"UI_LEVEL_LEGENDARY"
				},
				{
					"CgkIlpSuuo0ZEAIQKw",
					"UI_LEVEL_MYTHICAL"
				},
				{
					"CgkIlpSuuo0ZEAIQIg",
					"5"
				},
				{
					"CgkIlpSuuo0ZEAIQIw",
					"10"
				},
				{
					"CgkIlpSuuo0ZEAIQJQ",
					"20"
				},
				{
					"CgkIlpSuuo0ZEAIQJA",
					"30"
				},
				{
					"CgkIlpSuuo0ZEAIQPQ",
					"100"
				},
				{
					"CgkIlpSuuo0ZEAIQPg",
					"500"
				},
				{
					"CgkIlpSuuo0ZEAIQPw",
					"1000"
				},
				{
					"CgkIlpSuuo0ZEAIQQA",
					"5000"
				},
				{
					"CgkIlpSuuo0ZEAIQQQ",
					"15000"
				},
				{
					"CgkIlpSuuo0ZEAIQMw",
					"1"
				},
				{
					"CgkIlpSuuo0ZEAIQNA",
					"5"
				},
				{
					"CgkIlpSuuo0ZEAIQNQ",
					"9"
				},
				{
					"CgkIlpSuuo0ZEAIQNg",
					"15"
				},
				{
					"CgkIlpSuuo0ZEAIQNw",
					"30"
				},
				{
					"CgkIlpSuuo0ZEAIQLQ",
					"1"
				},
				{
					"CgkIlpSuuo0ZEAIQLg",
					"5"
				},
				{
					"CgkIlpSuuo0ZEAIQLw",
					"10"
				},
				{
					"CgkIlpSuuo0ZEAIQMA",
					"25"
				},
				{
					"CgkIlpSuuo0ZEAIQMQ",
					"40"
				}
			};
			this.spritesQuesfOfUpdate = new Dictionary<int, Sprite>
			{
				{
					QuestOfUpdateIds.Anniversary01,
					this.uiData.spriteAnniversaryAchievement
				}
			};
			this.spritesRiftEffectIcon = new Dictionary<Type, Sprite>
			{
				{
					typeof(RiftEffectBasicAttacksToRingTabs),
					this.uiData.spriteRiftEffectBasicAttacksToRingTabs
				},
				{
					typeof(RiftEffectDoubledCrits),
					this.uiData.spriteRiftEffectDoubledCrits
				},
				{
					typeof(RiftEffectDyingDealsDamage),
					this.uiData.spriteRiftEffectDyingDealsDamage
				},
				{
					typeof(RiftEffectShorterUltimateCD),
					this.uiData.spriteRiftEffectFastCd
				},
				{
					typeof(RiftEffectShorterRespawns),
					this.uiData.spriteRiftEffectFastSpawn
				},
				{
					typeof(RiftEffectLongerUltimateCD),
					this.uiData.spriteRiftEffectLongCd
				},
				{
					typeof(RiftEffectLongerRespawns),
					this.uiData.spriteRiftEffectLongSpawn
				},
				{
					typeof(RiftEffectGoldToDamage),
					this.uiData.spriteRiftEffectGoldToDamage
				},
				{
					typeof(RiftEffectHeroHealthToDamage),
					this.uiData.spriteRiftEffectHeroHealthToDamage
				},
				{
					typeof(RiftEffectNoAbilityDamage),
					this.uiData.spriteRiftEffectNoAbilityDamage
				},
				{
					typeof(RiftEffectRandomHeroes),
					this.uiData.spriteRiftEffectRandomHeroes
				},
				{
					typeof(RiftEffectEveryoneDodges),
					this.uiData.spriteRiftEffectDodge
				},
				{
					typeof(RiftEffectNoGoldDrop),
					this.uiData.spriteRiftEffectNoGoldDrops
				},
				{
					typeof(RiftEffectMeteorShower),
					this.uiData.spriteRiftEffectMeteor
				},
				{
					typeof(RiftEffectCritChance),
					this.uiData.spriteRiftEffectCritChance
				},
				{
					typeof(RiftEffectFastEnemies),
					this.uiData.spriteRiftEffectFastEnemies
				},
				{
					typeof(RiftEffectDyingHealsAllies),
					this.uiData.spriteRiftEffectDyingHeals
				},
				{
					typeof(RiftEffectUpgradeCostReduction),
					this.uiData.spriteRiftEffectUpgradeCostReduction
				},
				{
					typeof(RiftEffectAllDamageBuff),
					this.uiData.spriteRiftEffectEverythingHurts
				},
				{
					typeof(RiftEffectRegeneration),
					this.uiData.spriteRiftEffectRegen
				},
				{
					typeof(RiftEffectReflectDamage),
					this.uiData.spriteRiftEffectReflect
				},
				{
					typeof(RiftEffectCharmsProgress),
					this.uiData.spriteRiftEffectCharmProgress
				},
				{
					typeof(RiftEffectTreasureChests),
					this.uiData.spriteRiftEffectTreasureChests
				},
				{
					typeof(RiftEffectStunDropsGold),
					this.uiData.spriteRiftEffectStunstoGold
				},
				{
					typeof(RiftEffectOnlyAttackCharms),
					this.uiData.spriteRiftEffectOnlyAttackCharms
				},
				{
					typeof(RiftEffectOnlyDefenseCharms),
					this.uiData.spriteRiftEffectOnlyDefenseCharms
				},
				{
					typeof(RiftEffectOnlyUtilityCharms),
					this.uiData.spriteRiftEffectOnlyUtilityCharms
				},
				{
					typeof(RiftEffectBoss),
					this.uiData.spriteRiftEffectOnlyBoss
				},
				{
					typeof(RiftEffectDyingHeroesDropGold),
					this.uiData.spriteRiftEffectDyingHeroesDropGold
				},
				{
					typeof(RiftEffectGoldDamageToHeroes),
					this.uiData.spriteRiftEffectGoldDamageToHeroes
				},
				{
					typeof(RiftEffectTimeDealsDamage),
					this.uiData.spriteRiftEffectTimeDealsDamage
				},
				{
					typeof(RiftEffectHalfHeal),
					this.uiData.spriteRiftEffectHalftHeal
				},
				{
					typeof(RiftEffectDoubleHeal),
					this.uiData.spriteRiftEffectDoubleHeal
				},
				{
					typeof(RiftEffectStunningEnemies),
					this.uiData.spriteRiftEffectStunningEnemies
				},
				{
					typeof(RiftEffectFastHeroAttackSpeed),
					this.uiData.spriteRiftEffectFastHeroAttackSpeed
				},
				{
					typeof(RiftEffectWiseSnakeBoss),
					this.uiData.spriteRiftEffectWiseSnakeBoss
				},
				{
					typeof(RiftEffectCurse),
					this.uiData.spriteRiftEffectCurse
				}
			};
			this.spritesRiftEffectGreyIcon = new Dictionary<Type, Sprite>
			{
				{
					typeof(RiftEffectBasicAttacksToRingTabs),
					this.uiData.spriteRiftEffectGreyBasicAttacksToRingTabs
				},
				{
					typeof(RiftEffectDoubledCrits),
					this.uiData.spriteRiftEffectGreyDoubledCrits
				},
				{
					typeof(RiftEffectDyingDealsDamage),
					this.uiData.spriteRiftEffectGreyDyingDealsDamage
				},
				{
					typeof(RiftEffectShorterUltimateCD),
					this.uiData.spriteRiftEffectGreyFastCd
				},
				{
					typeof(RiftEffectShorterRespawns),
					this.uiData.spriteRiftEffectGreyFastSpawn
				},
				{
					typeof(RiftEffectLongerUltimateCD),
					this.uiData.spriteRiftEffectGreyLongCd
				},
				{
					typeof(RiftEffectLongerRespawns),
					this.uiData.spriteRiftEffectGreyLongSpawn
				},
				{
					typeof(RiftEffectGoldToDamage),
					this.uiData.spriteRiftEffectGreyGoldToDamage
				},
				{
					typeof(RiftEffectHeroHealthToDamage),
					this.uiData.spriteRiftEffectGreyHeroHealthToDamage
				},
				{
					typeof(RiftEffectNoAbilityDamage),
					this.uiData.spriteRiftEffectGreyNoAbilityDamage
				},
				{
					typeof(RiftEffectRandomHeroes),
					this.uiData.spriteRiftEffectGreyRandomHeroes
				},
				{
					typeof(RiftEffectEveryoneDodges),
					this.uiData.spriteRiftEffectGreyDodge
				},
				{
					typeof(RiftEffectNoGoldDrop),
					this.uiData.spriteRiftEffectGreyNoGoldDrops
				},
				{
					typeof(RiftEffectMeteorShower),
					this.uiData.spriteRiftEffectGreyMeteor
				},
				{
					typeof(RiftEffectCritChance),
					this.uiData.spriteRiftEffectGreyCritChance
				},
				{
					typeof(RiftEffectFastEnemies),
					this.uiData.spriteRiftEffectGreyFastEnemies
				},
				{
					typeof(RiftEffectDyingHealsAllies),
					this.uiData.spriteRiftEffectGreyDyingHeals
				},
				{
					typeof(RiftEffectUpgradeCostReduction),
					this.uiData.spriteRiftEffectGreyUpgradeCostReduction
				},
				{
					typeof(RiftEffectAllDamageBuff),
					this.uiData.spriteRiftEffectGreyEverythingHurts
				},
				{
					typeof(RiftEffectRegeneration),
					this.uiData.spriteRiftEffectGreyRegen
				},
				{
					typeof(RiftEffectReflectDamage),
					this.uiData.spriteRiftEffectGreyReflect
				},
				{
					typeof(RiftEffectCharmsProgress),
					this.uiData.spriteRiftEffectGreyCharmProgress
				},
				{
					typeof(RiftEffectTreasureChests),
					this.uiData.spriteRiftEffectGreyTreasureChests
				},
				{
					typeof(RiftEffectStunDropsGold),
					this.uiData.spriteRiftEffectGreyStunstoGold
				},
				{
					typeof(RiftEffectOnlyAttackCharms),
					this.uiData.spriteRiftEffectGreyOnlyAttackCharms
				},
				{
					typeof(RiftEffectOnlyDefenseCharms),
					this.uiData.spriteRiftEffectGreyOnlyDefenseCharms
				},
				{
					typeof(RiftEffectOnlyUtilityCharms),
					this.uiData.spriteRiftEffectGreyOnlyUtilityCharms
				},
				{
					typeof(RiftEffectBoss),
					this.uiData.spriteRiftEffectGreyOnlyBoss
				},
				{
					typeof(RiftEffectDyingHeroesDropGold),
					this.uiData.spriteRiftEffectGreyDyingHeroesDropGold
				},
				{
					typeof(RiftEffectGoldDamageToHeroes),
					this.uiData.spriteRiftEffectGreyGoldDamageToHeroes
				},
				{
					typeof(RiftEffectTimeDealsDamage),
					this.uiData.spriteRiftEffectGreyTimeDealsDamage
				},
				{
					typeof(RiftEffectHalfHeal),
					this.uiData.spriteRiftEffectGreyHalftHeal
				},
				{
					typeof(RiftEffectDoubleHeal),
					this.uiData.spriteRiftEffectGreyDoubleHeal
				},
				{
					typeof(RiftEffectStunningEnemies),
					this.uiData.spriteRiftEffectGreyStunningEnemies
				},
				{
					typeof(RiftEffectFastHeroAttackSpeed),
					this.uiData.spriteRiftEffectGreyFastHeroAttackSpeed
				},
				{
					typeof(RiftEffectWiseSnakeBoss),
					this.uiData.spriteRiftEffectGreyWiseSnakeBoss
				}
			};
			this.spritesCharmEffectIcon = new Dictionary<int, Sprite>
			{
				{
					101,
					this.uiData.spriteCharmFireyFire
				},
				{
					102,
					this.uiData.spriteCharmBootlegFireworks
				},
				{
					103,
					this.uiData.spriteCharmPowerOverwhelming
				},
				{
					104,
					this.uiData.spriteCharmProfessionalStrike
				},
				{
					105,
					this.uiData.spriteCharmBerserk
				},
				{
					106,
					this.uiData.spriteCharmBribedAccuracy
				},
				{
					107,
					this.uiData.spriteCharmLooseLessons
				},
				{
					108,
					this.uiData.spriteThirstingFiends
				},
				{
					109,
					this.uiData.spriteCharmBouncingBolt
				},
				{
					110,
					this.uiData.spriteCharmExplosiveEnergy
				},
				{
					201,
					this.uiData.spriteCharmCallfromtheGrave
				},
				{
					202,
					this.uiData.spriteCharmRustyDaggers
				},
				{
					203,
					this.uiData.spriteCharmBodyBlock
				},
				{
					204,
					this.uiData.spriteCharmSweetMoves
				},
				{
					205,
					this.uiData.spriteCharmSpellShield
				},
				{
					206,
					this.uiData.spriteCharmFrostyStorm
				},
				{
					207,
					this.uiData.spriteCharmAngelicWard
				},
				{
					208,
					this.uiData.spriteCharmStaryStaryDay
				},
				{
					209,
					this.uiData.spriteCharmAppleADay
				},
				{
					210,
					this.uiData.spriteCharmQuackatoa
				},
				{
					301,
					this.uiData.spriteCharmGrimRewards
				},
				{
					302,
					this.uiData.spriteCharmSparksFromHeaven
				},
				{
					303,
					this.uiData.spriteCharmQuickStudy
				},
				{
					304,
					this.uiData.spriteCharmVengefulSparks
				},
				{
					305,
					this.uiData.spriteCharmSpecialDelivery
				},
				{
					306,
					this.uiData.spriteCharmExtremeImpatience
				},
				{
					307,
					this.uiData.spriteCharmWealthFromAbove
				},
				{
					308,
					this.uiData.spriteCharmEmergencyFlute
				},
				{
					309,
					this.uiData.spriteCharmTimeWarp
				},
				{
					310,
					this.uiData.spriteCharmLucrativeLightning
				}
			};
			this.spritesCurseEffectIcon = new Dictionary<int, Sprite>
			{
				{
					1000,
					this.uiData.spriteCurseCDReduction
				},
				{
					1001,
					this.uiData.spriteCurseDealDamage
				},
				{
					1002,
					this.uiData.spriteCurseTimeSlow
				},
				{
					1003,
					this.uiData.spriteCurseCharmProgress
				},
				{
					1004,
					this.uiData.spriteCurseReflectDamage
				},
				{
					1005,
					this.uiData.spriteCurseMissChance
				},
				{
					1006,
					this.uiData.spriteCurseUpgradeCost
				},
				{
					1007,
					this.uiData.spriteCurseHeroDamage
				},
				{
					1008,
					this.uiData.spriteCurseStunHero
				},
				{
					1009,
					this.uiData.spriteCurseAntiRegeneration
				},
				{
					1010,
					this.uiData.spriteCurseHeavyLimbs
				},
				{
					1011,
					this.uiData.spriteCurseUncannyRegeneration
				},
				{
					1012,
					this.uiData.spriteCursePartingShot
				},
				{
					1013,
					this.uiData.spriteCurseDelayedCharms
				},
				{
					1014,
					this.uiData.spriteCurseHauntingVisage
				},
				{
					1015,
					this.uiData.spriteCurseMoltenGold
				},
				{
					1016,
					this.uiData.spriteCurseGogZeal
				},
				{
					1017,
					this.uiData.spriteCurseDampenedWill
				},
				{
					1018,
					this.uiData.spriteCurseIncantationInversion
				},
				{
					1019,
					this.uiData.spriteCurseGhostlyHeroes
				}
			};
		}

		public Sprite GetEffectSprite(int id)
		{
			switch (id)
			{
			case 1:
				return this.uiData.spriteArtifactIconGold;
			case 2:
				return this.uiData.spriteArtifactIconTotem;
			case 3:
				return this.uiData.spriteArtifactIconHero;
			case 4:
				return this.uiData.spriteArtifactIconHealth;
			default:
				throw new NotImplementedException("Invalid artifact effect id: " + id);
			}
		}

		public Sprite GetEffectTypeSprite(ArtifactEffectCategory aet, Type type)
		{
			switch (aet)
			{
			case ArtifactEffectCategory.HERO:
				return this.uiData.spriteArtifactIconHero;
			case ArtifactEffectCategory.RING:
				return this.uiData.spriteArtifactIconTotem;
			case ArtifactEffectCategory.UTILITY:
				return this.uiData.spriteArtifactIconUtility;
			case ArtifactEffectCategory.GOLD:
				return this.uiData.spriteArtifactIconGold;
			case ArtifactEffectCategory.ENERGY:
				return this.uiData.spriteArtifactIconEnergy;
			case ArtifactEffectCategory.HEALTH:
				return this.uiData.spriteArtifactIconHealth;
			case ArtifactEffectCategory.MYTH:
				if (type == typeof(MythicalArtifactDPSMatter))
				{
					return this.uiData.spriteArtifactIconMythDpsMatter;
				}
				if (type == typeof(MythicalArtifactOldCrucible))
				{
					return this.uiData.spriteArtifactIconMythOldCrucible;
				}
				if (type == typeof(MythicalArtifactFreeExploiter))
				{
					return this.uiData.spriteArtifactIconMythFreeExploiter;
				}
				if (type == typeof(MythicalArtifactCustomTailor))
				{
					return this.uiData.spriteArtifactIconMythCustomTailor;
				}
				if (type == typeof(MythicalArtifactLifeBoat))
				{
					return this.uiData.spriteArtifactIconMythLifeBoat;
				}
				if (type == typeof(MythicalArtifactAutoTransmuter))
				{
					return this.uiData.spriteArtifactIconMythAutoTransmuter;
				}
				if (type == typeof(MythicalArtifactPerfectQuasi))
				{
					return this.uiData.spriteArtifactIconMythPerfectQuasi;
				}
				if (type == typeof(MythicalArtifactBrokenTeleporter))
				{
					return this.uiData.spriteArtifactIconMythBrokenTeleporter;
				}
				if (type == typeof(MythicalArtifactHalfRing))
				{
					return this.uiData.spriteArtifactIconMythHalfRing;
				}
				if (type == typeof(MythicalArtifactGoblinLure))
				{
					return this.uiData.spriteArtifactIconMythGoblinLure;
				}
				if (type == typeof(MythicalArtifactLazyFinger))
				{
					return this.uiData.spriteArtifactIconMythLazyFinger;
				}
				if (type == typeof(MythicalArtifactShinyObject))
				{
					return this.uiData.spriteArtifactIconMythShinyObject;
				}
				if (type == typeof(MythicalArtifactBluntRelic))
				{
					return this.uiData.spriteArtifactIconMythPowerupCritChance;
				}
				if (type == typeof(MythicalArtifactImpatientRelic))
				{
					return this.uiData.spriteArtifactIconMythPowerupCooldown;
				}
				if (type == typeof(MythicalArtifactBandAidRelic))
				{
					return this.uiData.spriteArtifactIconMythPowerupRevive;
				}
				if (type == typeof(MythicalArtifactBodilyHarm))
				{
					return this.uiData.spriteArtifactIconMythBodilyHarm;
				}
				if (type == typeof(MythicalArtifactChampionsBounty))
				{
					return this.uiData.spriteArtifactIconMythChampionsBounty;
				}
				if (type == typeof(MythicalArtifactCorpusImperium))
				{
					return this.uiData.spriteArtifactIconMythCorpusImperium;
				}
				if (type == typeof(MythicalArtifactCrestOfViloence))
				{
					return this.uiData.spriteArtifactIconMythHeroDamagePerAttacker;
				}
				if (type == typeof(MythicalArtifactCrestOfSturdiness))
				{
					return this.uiData.spriteArtifactIconMythHeroHealthPerDefender;
				}
				if (type == typeof(MythicalArtifactCrestOfUsefulness))
				{
					return this.uiData.spriteArtifactIconMythGoldBonusPerSupporter;
				}
				return null;
			default:
				throw new NotImplementedException();
			}
		}

		public Sprite GetCharmCardFace(CharmType charmClass)
		{
			switch (charmClass)
			{
			case CharmType.Attack:
				return this.uiData.spriteCharmFaceRed;
			case CharmType.Defense:
				return this.uiData.spriteCharmFaceBlue;
			case CharmType.Utility:
				return this.uiData.spriteCharmFaceGreen;
			default:
				return null;
			}
		}

		public Sprite GetCharmCardFlashOffer(CharmType charmClass)
		{
			switch (charmClass)
			{
			case CharmType.Attack:
				return this.uiData.spriteCharmFlashRed;
			case CharmType.Defense:
				return this.uiData.spriteCharmFlashBlue;
			case CharmType.Utility:
				return this.uiData.spriteCharmFlashGreen;
			default:
				return null;
			}
		}

		public Color GetFlashOfferImageColor(FlashOffer flashOffer)
		{
			return (flashOffer.type != FlashOffer.Type.RUNE) ? Color.white : this.GetRuneColor(this.sim.GetRune(flashOffer.genericStringId).belongsTo.id);
		}

		public Sprite GetFlashOfferImageSmall(FlashOffer flashOffer)
		{
			switch (flashOffer.type)
			{
			case FlashOffer.Type.CHARM:
				return this.spritesCharmEffectIcon[flashOffer.charmId];
			case FlashOffer.Type.SCRAP:
				return this.uiData.spriteScrapsOfferBig;
			case FlashOffer.Type.GEM:
				return this.uiData.spriteOfferGemsSmall;
			case FlashOffer.Type.TOKEN:
				return this.uiData.spriteTokenOfferBig;
			case FlashOffer.Type.RUNE:
				return this.GetSpriteRune(flashOffer.genericStringId);
			case FlashOffer.Type.COSTUME:
			case FlashOffer.Type.COSTUME_PLUS_SCRAP:
				return this.uiData.skinFrameActive;
			case FlashOffer.Type.TRINKET_PACK:
				return this.uiData.spriteUnlockTrinketPack;
			case FlashOffer.Type.MERCHANT_ITEM:
				return this.GetSpriteMerchantItemSmall(this.sim.GetMerchantItemWithId(flashOffer.genericStringId).GetType());
			default:
				throw new NotImplementedException();
			}
		}

		public Sprite GetFlashOfferImageBig(FlashOffer flashOffer)
		{
			switch (flashOffer.type)
			{
			case FlashOffer.Type.CHARM:
				return this.spritesCharmEffectIcon[flashOffer.charmId];
			case FlashOffer.Type.SCRAP:
				return this.uiData.spriteScrapsOfferBig;
			case FlashOffer.Type.GEM:
				return this.uiData.spriteOfferGemsSmall;
			case FlashOffer.Type.TOKEN:
				return this.uiData.spriteTokenOfferBig;
			case FlashOffer.Type.RUNE:
				return this.GetSpriteRune(flashOffer.genericStringId);
			case FlashOffer.Type.COSTUME:
			case FlashOffer.Type.COSTUME_PLUS_SCRAP:
				return this.uiData.skinFrameActive;
			case FlashOffer.Type.TRINKET_PACK:
				return this.uiData.spriteUnlockTrinketPack;
			case FlashOffer.Type.MERCHANT_ITEM:
				return this.GetSpriteMerchantItem(flashOffer.genericStringId);
			default:
				throw new NotImplementedException();
			}
		}

		public string GetFlashOfferName(FlashOffer flashOffer, bool showAmount)
		{
			switch (flashOffer.type)
			{
			case FlashOffer.Type.CHARM:
				return this.sim.allCharmEffects[flashOffer.charmId].GetName();
			case FlashOffer.Type.SCRAP:
				return (!showAmount) ? LM.Get("UI_SCRAPS") : string.Format(LM.Get("UNLOCK_REWARD_SCRAPS"), GameMath.GetDoubleString((double)this.sim.GetFlashOfferCount(flashOffer)));
			case FlashOffer.Type.GEM:
				return (!showAmount) ? LM.Get("UI_GEMS") : string.Format(LM.Get("UNLOCK_REWARD_GEMS"), GameMath.GetDoubleString((double)this.sim.GetFlashOfferCount(flashOffer)));
			case FlashOffer.Type.TOKEN:
				return (!showAmount) ? LM.Get("UI_TOKENS") : string.Format(LM.Get("UNLOCK_REWARD_TOKENS"), GameMath.GetDoubleString((double)this.sim.GetFlashOfferCount(flashOffer)));
			case FlashOffer.Type.RUNE:
			{
				Rune rune = this.sim.GetRune(flashOffer.genericStringId);
				string id = rune.belongsTo.id;
				if (id != null)
				{
					if (id == "totemEarth")
					{
						return LM.Get("UI_EARTH_RUNE");
					}
					if (id == "totemIce")
					{
						return LM.Get("UI_ICE_RUNE");
					}
					if (id == "totemLightning")
					{
						return LM.Get("UI_LIGHTNING_RUNE");
					}
					if (id == "totemFire")
					{
						return LM.Get("UI_FIRE_RUNE");
					}
				}
				throw new NotImplementedException();
			}
			case FlashOffer.Type.COSTUME:
				return LM.Get("UI_SKIN");
			case FlashOffer.Type.TRINKET_PACK:
				return LM.Get("UI_TRINKET");
			case FlashOffer.Type.COSTUME_PLUS_SCRAP:
				return LM.Get("SKIN_PLUS_SCRAP_FLASH_OFFER");
			case FlashOffer.Type.MERCHANT_ITEM:
				return string.Format(LM.Get("UNLOCK_REWARD_MERCHANT_ITEM"), this.sim.GetMerchantItemWithId(flashOffer.genericStringId).GetTitleString());
			default:
				throw new NotImplementedException();
			}
		}

		public Sprite GetSpriteGearIcon(string key)
		{
			if (this.spritesGearIcon.ContainsKey(key))
			{
				return this.spritesGearIcon[key];
			}
			return null;
		}

		public Sprite GetSpriteTotem(string key)
		{
			if (this.spritesTotem.ContainsKey(key))
			{
				return this.spritesTotem[key];
			}
			return null;
		}

		public Sprite GetSpriteTotemSmall(string key)
		{
			if (this.spritesTotemSmall.ContainsKey(key))
			{
				return this.spritesTotemSmall[key];
			}
			return null;
		}

		public Sprite GetSpriteTotemShineSmall(string key)
		{
			if (this.spritesTotemShineSmall.ContainsKey(key))
			{
				return this.spritesTotemShineSmall[key];
			}
			return null;
		}

		public Sprite GetSpriteQuestOfUpdate(int key)
		{
			if (this.spritesQuesfOfUpdate.ContainsKey(key))
			{
				return this.spritesQuesfOfUpdate[key];
			}
			return null;
		}

		public Sprite GetRiftEffectSprite(Type type)
		{
			Sprite result = null;
			if (this.spritesRiftEffectIcon.TryGetValue(type, out result))
			{
				return result;
			}
			return null;
		}

		public Sprite GetRiftEffectSmallSprite(Type type)
		{
			Sprite result = null;
			if (this.spritesRiftEffectGreyIcon.TryGetValue(type, out result))
			{
				return result;
			}
			return null;
		}

		public Sprite GetSpriteRune(string key)
		{
			if (this.spritesRune.ContainsKey(key))
			{
				return this.spritesRune[key];
			}
			return null;
		}

		public Color GetRuneColor(string ringId)
		{
			Color white = Color.white;
			this.runeColorForRing.TryGetValue(ringId, out white);
			return white;
		}

		public Sprite GetSpriteSkillIconMainScreen(Type key)
		{
			if (this.spritesSkillIconMainScreen.ContainsKey(key))
			{
				return this.spritesSkillIconMainScreen[key];
			}
			return null;
		}

		public Sprite GetSpriteSkillToggleIconMainScreen(Type key)
		{
			if (this.spritesSkillToggleIconMainScreen.ContainsKey(key))
			{
				return this.spritesSkillToggleIconMainScreen[key];
			}
			return null;
		}

		public Sprite GetSpriteSkillIconSkillScreen(Type key)
		{
			if (this.spritesSkillIconSkillScreen.ContainsKey(key))
			{
				return this.spritesSkillIconSkillScreen[key];
			}
			return null;
		}

		public Sprite GetSpriteMerchantItem(string key)
		{
			if (this.spritesMerchantItem.ContainsKey(key))
			{
				return this.spritesMerchantItem[key];
			}
			return null;
		}

		public Sprite GetSpriteMerchantItemSmall(Type key)
		{
			if (this.spritesMerchantItemSmall.ContainsKey(key))
			{
				return this.spritesMerchantItemSmall[key];
			}
			return null;
		}

		public Sprite GetSpriteHeroPortrait(string heroId)
		{
			if (this.spritesHeroPortrait.ContainsKey(heroId))
			{
				return this.spritesHeroPortrait[heroId];
			}
			return null;
		}

		public Sprite GetSpriteModeFlag(GameMode mode)
		{
			if (this.spritesModeFlag.ContainsKey(mode))
			{
				return this.spritesModeFlag[mode];
			}
			return null;
		}

		public Sprite GetSpriteBossPortrait(string bossId)
		{
			if (this.spritesBossPortrait.ContainsKey(bossId))
			{
				return this.spritesBossPortrait[bossId];
			}
			return null;
		}

		public Sprite GetSpriteAchievement(string id)
		{
			if (this.spritesAchievements.ContainsKey(id))
			{
				return this.spritesAchievements[id];
			}
			return null;
		}

		public Sprite GetHeroAvatar(int skinId)
		{
			SkinData skinData = this.sim.GetAllSkins().Find((SkinData s) => s.id == skinId);
			return this.GetHeroAvatar(skinData.belongsTo.id, skinData.index);
		}

		public Sprite GetHeroAvatar(SkinData skinData)
		{
			return this.GetHeroAvatar(skinData.belongsTo.id, skinData.index);
		}

		public Sprite GetHeroAvatar(string id, int skinIndex)
		{
			Sprite[] array;
			if (!this.spritesHeroPortraits.TryGetValue(id, out array))
			{
				return null;
			}
			if (skinIndex - 1 >= array.Length)
			{
				return array[0];
			}
			return array[skinIndex - 1];
		}

		public string GetAttributeString(int i, bool isAmount, out double amount, out bool maxedOut)
		{
			UniversalTotalBonus universalBonusAll = this.sim.GetUniversalBonusAll();
			switch (i)
			{
			case 0:
				amount = (double)universalBonusAll.bossTimeAdd;
				maxedOut = ((double)universalBonusAll.bossTimeAdd >= 89.55);
				if (isAmount)
				{
					return OLD_ArtifactEffectBossTime.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectBossTime.GetString();
			case 1:
				amount = (double)(universalBonusAll.chestChanceFactor - 1f);
				maxedOut = ((double)universalBonusAll.chestChanceFactor >= 3.98);
				if (isAmount)
				{
					return OLD_ArtifactEffectChestChance.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectChestChance.GetString();
			case 2:
				amount = universalBonusAll.costHeroUpgradeFactor - 1.0;
				maxedOut = (1.0 - universalBonusAll.costHeroUpgradeFactor >= 0.8955);
				if (isAmount)
				{
					return OLD_ArtifactEffectCostHeroUpgrade.GetAmountString(amount);
				}
				return OLD_ArtifactEffectCostHeroUpgrade.GetString();
			case 3:
				amount = universalBonusAll.costTotemUpgradeFactor - 1.0;
				maxedOut = (1.0 - universalBonusAll.costTotemUpgradeFactor >= 0.8955);
				if (isAmount)
				{
					return OLD_ArtifactEffectCostTotemUpgrade.GetAmountString(amount);
				}
				return OLD_ArtifactEffectCostTotemUpgrade.GetString();
			case 4:
				amount = (double)universalBonusAll.critChanceHeroAdd;
				maxedOut = ((double)universalBonusAll.critChanceHeroAdd >= 0.398);
				if (isAmount)
				{
					return OLD_ArtifactEffectCritChanceHero.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectCritChanceHero.GetString();
			case 5:
				amount = (double)universalBonusAll.critChanceTotemAdd;
				maxedOut = ((double)universalBonusAll.critChanceTotemAdd >= 0.398);
				if (isAmount)
				{
					return OLD_ArtifactEffectCritChanceTotem.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectCritChanceTotem.GetString();
			case 6:
				amount = universalBonusAll.critFactorHeroAdd;
				maxedOut = (universalBonusAll.critFactorHeroAdd >= 5.97);
				if (isAmount)
				{
					return OLD_ArtifactEffectCritFactorHero.GetAmountString(amount);
				}
				return OLD_ArtifactEffectCritFactorHero.GetString();
			case 7:
				amount = universalBonusAll.critFactorTotemAdd;
				maxedOut = (universalBonusAll.critFactorTotemAdd >= 5.97);
				if (isAmount)
				{
					return OLD_ArtifactEffectCritFactorTotem.GetAmountString(amount);
				}
				return OLD_ArtifactEffectCritFactorTotem.GetString();
			case 8:
				amount = universalBonusAll.damageFactor - 1.0;
				maxedOut = (universalBonusAll.damageFactor >= double.PositiveInfinity);
				if (isAmount)
				{
					return OLD_ArtifactEffectDamage.GetAmountString(amount);
				}
				return OLD_ArtifactEffectDamage.GetString();
			case 9:
				amount = universalBonusAll.damageHeroFactor - 1.0;
				maxedOut = (universalBonusAll.damageHeroFactor >= double.PositiveInfinity);
				if (isAmount)
				{
					return OLD_ArtifactEffectDamageHero.GetAmountString(amount);
				}
				return OLD_ArtifactEffectDamageHero.GetString();
			case 10:
				amount = universalBonusAll.damageHeroSkillFactor - 1.0;
				maxedOut = (universalBonusAll.damageHeroSkillFactor >= 2.985);
				if (isAmount)
				{
					return OLD_ArtifactEffectDamageHeroSkill.GetAmountString(amount);
				}
				return OLD_ArtifactEffectDamageHeroSkill.GetString();
			case 11:
				amount = universalBonusAll.damageTotemFactor - 1.0;
				maxedOut = (universalBonusAll.damageTotemFactor >= double.PositiveInfinity);
				if (isAmount)
				{
					return OLD_ArtifactEffectDamageTotem.GetAmountString(amount);
				}
				return OLD_ArtifactEffectDamageTotem.GetString();
			case 12:
				amount = (double)(universalBonusAll.dragonSpawnRateFactor - 1f);
				maxedOut = ((double)(universalBonusAll.dragonSpawnRateFactor - 1f) >= 1.99);
				if (isAmount)
				{
					return OLD_ArtifactEffectDroneSpawnRate.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectDroneSpawnRate.GetString();
			case 13:
				amount = (double)universalBonusAll.epicBossDropMythstonesAdd;
				maxedOut = ((double)universalBonusAll.epicBossDropMythstonesAdd >= 300.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectEpicBossDropMythstone.GetAmountString((int)amount);
				}
				return OLD_ArtifactEffectEpicBossDropMythstone.GetString();
			case 14:
				amount = (double)(universalBonusAll.freePackCooldownFactor - 1f);
				maxedOut = ((double)(1f - universalBonusAll.freePackCooldownFactor) >= 0.74625);
				if (isAmount)
				{
					return OLD_ArtifactEffectFreePackCooldown.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectFreePackCooldown.GetString();
			case 15:
				amount = universalBonusAll.goldFactor - 1.0;
				maxedOut = (universalBonusAll.goldFactor >= double.PositiveInfinity);
				if (isAmount)
				{
					return OLD_ArtifactEffectGold.GetAmountString(amount);
				}
				return OLD_ArtifactEffectGold.GetString();
			case 16:
				amount = universalBonusAll.goldBossFactor - 1.0;
				maxedOut = (universalBonusAll.goldBossFactor - 1.0 >= 4.975);
				if (isAmount)
				{
					return OLD_ArtifactEffectGoldBoss.GetAmountString(amount);
				}
				return OLD_ArtifactEffectGoldBoss.GetString();
			case 17:
				amount = universalBonusAll.goldChestFactor - 1.0;
				maxedOut = (universalBonusAll.goldChestFactor - 1.0 >= 4.975);
				if (isAmount)
				{
					return OLD_ArtifactEffectGoldChest.GetAmountString(amount);
				}
				return OLD_ArtifactEffectGoldChest.GetString();
			case 18:
				amount = (double)universalBonusAll.goldMultTenChanceAdd;
				maxedOut = ((double)universalBonusAll.goldMultTenChanceAdd >= 0.995);
				if (isAmount)
				{
					return OLD_ArtifactEffectGoldMultTenChance.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectGoldMultTenChance.GetString();
			case 19:
				amount = universalBonusAll.goldOfflineFactor - 1.0;
				maxedOut = (universalBonusAll.goldOfflineFactor >= 7.96);
				if (isAmount)
				{
					return OLD_ArtifactEffectGoldOffline.GetAmountString(amount);
				}
				return OLD_ArtifactEffectGoldOffline.GetString();
			case 20:
				amount = universalBonusAll.healthBossFactor - 1.0;
				maxedOut = (1.0 - universalBonusAll.healthBossFactor >= 0.8955);
				if (isAmount)
				{
					return OLD_ArtifactEffectHealthBoss.GetAmountString(amount);
				}
				return OLD_ArtifactEffectHealthBoss.GetString();
			case 21:
				amount = universalBonusAll.healthHeroFactor - 1.0;
				maxedOut = (universalBonusAll.healthHeroFactor >= double.PositiveInfinity);
				if (isAmount)
				{
					return OLD_ArtifactEffectHealthHero.GetAmountString(amount);
				}
				return OLD_ArtifactEffectHealthHero.GetString();
			case 22:
				amount = (double)universalBonusAll.heroLevelRequiredForSkillDecrease;
				maxedOut = ((double)universalBonusAll.heroLevelRequiredForSkillDecrease >= 8.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectHeroLevelReqForSkill.GetAmountString((int)amount);
				}
				return OLD_ArtifactEffectHeroLevelReqForSkill.GetString();
			case 23:
				amount = (double)(universalBonusAll.reviveTimeFactor - 1f);
				maxedOut = ((double)(1f - universalBonusAll.reviveTimeFactor) >= 0.64675);
				if (isAmount)
				{
					return OLD_ArtifactEffectReviveTime.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectReviveTime.GetString();
			case 24:
				amount = (double)(universalBonusAll.ultiCoolDownMaxFactor - 1f);
				maxedOut = ((double)(1f - universalBonusAll.ultiCoolDownMaxFactor) >= 0.4975);
				if (isAmount)
				{
					return OLD_ArtifactEffectUltiCooldown.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectUltiCooldown.GetString();
			case 25:
				amount = (double)universalBonusAll.waveSkipChanceAdd;
				maxedOut = ((double)universalBonusAll.waveSkipChanceAdd >= 0.94524999999999992);
				if (isAmount)
				{
					return OLD_ArtifactEffectWaveSkipChance.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectWaveSkipChance.GetString();
			case 26:
				amount = universalBonusAll.prestigeMythFactor - 1.0;
				maxedOut = (universalBonusAll.prestigeMythFactor - 1.0 >= 14.925);
				if (isAmount)
				{
					return OLD_ArtifactEffectPrestigeMyth.GetAmountString(amount);
				}
				return OLD_ArtifactEffectPrestigeMyth.GetString();
			case 27:
				amount = (double)universalBonusAll.autoTapDurationAdd;
				maxedOut = ((double)universalBonusAll.autoTapDurationAdd >= 995.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectAutoTapTime.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectAutoTapTime.GetString();
			case 28:
				amount = (double)universalBonusAll.autoTapCountAdd;
				maxedOut = ((double)universalBonusAll.autoTapCountAdd >= 6.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectAutoTapCount.GetAmountString((int)amount);
				}
				return OLD_ArtifactEffectAutoTapCount.GetString();
			case 29:
				amount = (double)universalBonusAll.goldBagCountAdd;
				maxedOut = ((double)universalBonusAll.goldBagCountAdd >= 8.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectGoldBagCount.GetAmountString((int)amount);
				}
				return OLD_ArtifactEffectGoldBagCount.GetString();
			case 30:
				amount = (double)universalBonusAll.timeWarpCountAdd;
				maxedOut = ((double)universalBonusAll.timeWarpCountAdd >= 6.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectTimeWarpCount.GetAmountString((int)amount);
				}
				return OLD_ArtifactEffectTimeWarpCount.GetString();
			case 31:
				amount = (double)(universalBonusAll.goldBagValueFactor - 1f);
				maxedOut = ((double)(universalBonusAll.goldBagValueFactor - 1f) >= 2.985);
				if (isAmount)
				{
					return OLD_ArtifactEffectGoldBagValue.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectGoldBagValue.GetString();
			case 32:
				amount = (double)(universalBonusAll.timeWarpSpeedFactor - 1f);
				maxedOut = ((double)(universalBonusAll.timeWarpSpeedFactor - 1f) >= 1.99);
				if (isAmount)
				{
					return OLD_ArtifactEffectTimeWarpSpeed.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectTimeWarpSpeed.GetString();
			case 33:
				amount = (double)universalBonusAll.timeWarpDurationAdd;
				maxedOut = ((double)universalBonusAll.timeWarpDurationAdd >= 298.5);
				if (isAmount)
				{
					return OLD_ArtifactEffectTimeWarpDuration.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectTimeWarpDuration.GetString();
			case 34:
				amount = (double)(universalBonusAll.afterBossDurationFactor - 1f);
				maxedOut = ((double)(universalBonusAll.afterBossDurationFactor - 1f) >= 1.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectQuickWaveAfterBoss.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectQuickWaveAfterBoss.GetString();
			case 35:
			{
				Simulation.Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactHalfRing));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(artifactMythical);
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactHalfRing.GetNameStatic();
			}
			case 36:
			{
				Simulation.Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactBrokenTeleporter));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(artifactMythical);
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactBrokenTeleporter.GetNameStatic();
			}
			case 37:
			{
				Simulation.Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactLifeBoat));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(artifactMythical);
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactLifeBoat.GetNameStatic();
			}
			case 38:
				amount = (double)universalBonusAll.fastEnemySpawnBelow;
				maxedOut = ((double)universalBonusAll.fastEnemySpawnBelow >= 900.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectFastSpawn.GetAmountString((int)amount);
				}
				return OLD_ArtifactEffectFastSpawn.GetString();
			case 39:
			{
				Simulation.Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactGoblinLure));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(artifactMythical);
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactGoblinLure.GetNameStatic();
			}
			case 40:
			{
				Simulation.Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactPerfectQuasi));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(artifactMythical);
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactPerfectQuasi.GetNameStatic();
			}
			case 41:
				amount = universalBonusAll.healthEnemyFactor - 1.0;
				maxedOut = (1.0 - universalBonusAll.healthEnemyFactor >= 0.895);
				if (isAmount)
				{
					return OLD_ArtifactEffectHealthEnemy.GetAmountString(amount);
				}
				return OLD_ArtifactEffectHealthEnemy.GetString();
			case 42:
				amount = universalBonusAll.damageEnemyFactor - 1.0;
				maxedOut = (1.0 - universalBonusAll.damageEnemyFactor >= 0.895);
				if (isAmount)
				{
					return OLD_ArtifactEffectDamageEnemy.GetAmountString(amount);
				}
				return OLD_ArtifactEffectDamageEnemy.GetString();
			case 43:
				amount = universalBonusAll.damageBossFactor - 1.0;
				maxedOut = (1.0 - universalBonusAll.damageBossFactor >= 0.895);
				if (isAmount)
				{
					return OLD_ArtifactEffectDamageBoss.GetAmountString(amount);
				}
				return OLD_ArtifactEffectDamageBoss.GetString();
			case 44:
			{
				Simulation.Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactCustomTailor));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(artifactMythical);
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactCustomTailor.GetNameStatic();
			}
			case 45:
			{
				Simulation.Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactDPSMatter));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(artifactMythical);
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactDPSMatter.GetNameStatic();
			}
			case 46:
			{
				Simulation.Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactFreeExploiter));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(artifactMythical);
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactFreeExploiter.GetNameStatic();
			}
			case 47:
			{
				Simulation.Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactOldCrucible));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(artifactMythical);
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactOldCrucible.GetNameStatic();
			}
			case 48:
			{
				Simulation.Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactAutoTransmuter));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(artifactMythical);
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactAutoTransmuter.GetNameStatic();
			}
			case 49:
			{
				Simulation.Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactLazyFinger));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(artifactMythical);
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactLazyFinger.GetNameStatic();
			}
			case 50:
			{
				Simulation.Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactShinyObject));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(artifactMythical);
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactShinyObject.GetNameStatic();
			}
			case 51:
			{
				Simulation.Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactBluntRelic));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(artifactMythical);
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactBluntRelic.GetNameStatic();
			}
			case 52:
			{
				Simulation.Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactImpatientRelic));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(artifactMythical);
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactImpatientRelic.GetNameStatic();
			}
			case 53:
			{
				Simulation.Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactBandAidRelic));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(artifactMythical);
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactBandAidRelic.GetNameStatic();
			}
			case 54:
				amount = universalBonusAll.damageHeroNonSkillFactor - 1.0;
				maxedOut = (universalBonusAll.damageHeroNonSkillFactor >= 4.975);
				if (isAmount)
				{
					return OLD_ArtifactEffectDamageHeroNonSkill.GetAmountString(amount);
				}
				return OLD_ArtifactEffectDamageHeroNonSkill.GetString();
			case 55:
			{
				Simulation.Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactBodilyHarm));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(artifactMythical);
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactBodilyHarm.GetNameStatic();
			}
			case 56:
			{
				Simulation.Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactChampionsBounty));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(artifactMythical);
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactChampionsBounty.GetNameStatic();
			}
			case 57:
			{
				Simulation.Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactCorpusImperium));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(artifactMythical);
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactCorpusImperium.GetNameStatic();
			}
			case 58:
				amount = (double)universalBonusAll.shieldCountAdd;
				maxedOut = ((double)universalBonusAll.shieldCountAdd >= 3.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectShieldCount.GetAmountString((int)amount);
				}
				return OLD_ArtifactEffectShieldCount.GetString();
			case 59:
				amount = (double)universalBonusAll.shieldDurationAdd;
				maxedOut = ((double)universalBonusAll.shieldDurationAdd >= 119.4);
				if (isAmount)
				{
					return OLD_ArtifactEffectShieldDuration.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectShieldDuration.GetString();
			case 60:
				amount = (double)universalBonusAll.horseshoeCountAdd;
				maxedOut = ((double)universalBonusAll.horseshoeCountAdd >= 6.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectHorseshoeCount.GetAmountString((int)amount);
				}
				return OLD_ArtifactEffectHorseshoeCount.GetString();
			case 61:
				amount = (double)universalBonusAll.horseshoeDurationAdd;
				maxedOut = ((double)universalBonusAll.horseshoeDurationAdd >= 597.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectHorseshoeDuration.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectHorseshoeDuration.GetString();
			case 62:
				amount = (double)(universalBonusAll.horseshoeValueFactor - 1f);
				maxedOut = ((double)(universalBonusAll.horseshoeValueFactor - 1f) >= 3.98);
				if (isAmount)
				{
					return OLD_ArtifactEffectHorseshoeValue.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectHorseshoeValue.GetString();
			case 63:
				amount = (double)universalBonusAll.destructionCountAdd;
				maxedOut = ((double)universalBonusAll.destructionCountAdd >= 4.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectDestructionCount.GetAmountString((int)amount);
				}
				return OLD_ArtifactEffectDestructionCount.GetString();
			default:
				throw new NotImplementedException();
			}
		}

		private MythicalArtifactEffect GetArtifactEffectMythical(Simulation.Artifact a)
		{
			if (a == null)
			{
				return null;
			}
			return a.effects[0] as MythicalArtifactEffect;
		}

		private Simulation.Artifact GetArtifactMythical(Type t)
		{
			List<Simulation.Artifact> mythicalArtifacts = this.sim.artifactsManager.MythicalArtifacts;
			int i = 0;
			int count = mythicalArtifacts.Count;
			while (i < count)
			{
				Simulation.Artifact artifact = mythicalArtifacts[i];
				if (artifact.effects[0].GetType() == t)
				{
					return artifact;
				}
				i++;
			}
			return null;
		}

		public Sprite GetUTBSprite(CharmType type, bool isLight)
		{
			switch (type)
			{
			case CharmType.Attack:
				return (!isLight) ? this.uiData.spriteIconUTBdamageDark : this.uiData.spriteIconUTBdamageLight;
			case CharmType.Defense:
				return (!isLight) ? this.uiData.spriteIconUTBhealthDark : this.uiData.spriteIconUTBhealthLight;
			case CharmType.Utility:
				return (!isLight) ? this.uiData.spriteIconUTBgoldDark : this.uiData.spriteIconUTBgoldLight;
			default:
				throw new Exception();
			}
		}

		private CharmSortType GetNextCharmSortType()
		{
			switch (this.sim.charmSortType)
			{
			case CharmSortType.Default:
				return CharmSortType.Level;
			case CharmSortType.Level:
				return CharmSortType.Type;
			case CharmSortType.Type:
				return CharmSortType.LevelupStatus;
			case CharmSortType.LevelupStatus:
				return CharmSortType.Default;
			default:
				return CharmSortType.Default;
			}
		}

		private TrinketSortType GetNextTrinketSortType()
		{
			switch (this.sim.trinketSortType)
			{
			case TrinketSortType.Default:
				return TrinketSortType.NumberOfEffects;
			case TrinketSortType.NumberOfEffects:
				return TrinketSortType.Level;
			case TrinketSortType.Level:
				return TrinketSortType.Color;
			case TrinketSortType.Color:
				return TrinketSortType.Default;
			default:
				return TrinketSortType.Default;
			}
		}

		public void SwitchCharmSortType()
		{
			UiCommandSetCharmSortingSettings uiCommandSetCharmSortingSettings = new UiCommandSetCharmSortingSettings();
			if (this.sim.charmSortType != CharmSortType.Default && this.sim.isCharmSortingDescending)
			{
				uiCommandSetCharmSortingSettings.isDescending = false;
				uiCommandSetCharmSortingSettings.sortType = this.sim.charmSortType;
			}
			else if (this.sim.charmSortType != CharmSortType.Default)
			{
				uiCommandSetCharmSortingSettings.isDescending = true;
				uiCommandSetCharmSortingSettings.sortType = this.GetNextCharmSortType();
			}
			else
			{
				uiCommandSetCharmSortingSettings.isDescending = this.sim.isCharmSortingDescending;
				uiCommandSetCharmSortingSettings.sortType = this.GetNextCharmSortType();
			}
			this.command = uiCommandSetCharmSortingSettings;
		}

		private void SwitchTrinketSortType()
		{
			UiCommandSetTrinketSortingSettings uiCommandSetTrinketSortingSettings = new UiCommandSetTrinketSortingSettings();
			if (this.sim.trinketSortType != TrinketSortType.Default && this.sim.isTrinketSortingDescending)
			{
				uiCommandSetTrinketSortingSettings.isDescending = false;
				uiCommandSetTrinketSortingSettings.sortType = this.sim.trinketSortType;
			}
			else if (this.sim.trinketSortType != TrinketSortType.Default)
			{
				uiCommandSetTrinketSortingSettings.isDescending = true;
				uiCommandSetTrinketSortingSettings.sortType = this.GetNextTrinketSortType();
			}
			else
			{
				uiCommandSetTrinketSortingSettings.isDescending = this.sim.isTrinketSortingDescending;
				uiCommandSetTrinketSortingSettings.sortType = this.GetNextTrinketSortType();
			}
			this.command = uiCommandSetTrinketSortingSettings;
		}

		public RectTransform GetTargetArtifactSit()
		{
			if (this.panelArtifactScroller.isLookingAtMythical)
			{
				int count = this.sim.artifactsManager.MythicalArtifacts.Count;
				return this.panelArtifactScroller.buttonArtifacts[count - 1].artifactStone.rectTransform;
			}
			return this.panelArtifactScroller.buttonArtifacts[this.sim.artifactsManager.Artifacts.Count - 1].artifactStone.rectTransform;
		}

		public int GetKeystrokeContextFromState(UiState state)
		{
			switch (state)
			{
			case UiState.NONE:
				return 20;
			case UiState.MODE:
				return 30;
			case UiState.HEROES:
				return 40;
			case UiState.ARTIFACTS:
				return 50;
			case UiState.SHOP:
				return 60;
			}
			return 0;
		}

		private KeyCommand AddCommand(KeyCode key)
		{
			KeyCommand keyCommand = KeyCommand.Create(key);
			this.keyStroke.AddCommand(keyCommand);
			return keyCommand;
		}

		private KeyCommand AddCommand(string name, KeyCode key)
		{
			KeyCommand keyCommand = KeyCommand.Create(name, key);
			this.keyStroke.AddCommand(keyCommand);
			return keyCommand;
		}

		public void InitKeyboard()
		{
			this.keyStroke.AddContext(20, true);
			KeyCommand keyCommand = this.AddCommand("Goto Home", KeyCode.Q).SetAction(delegate
			{
				this.OnClickedButtonTabBar(0);
			});
			
			keyCommand.SetCondition(new CommandCondition(TutorialManager.IsHubTabUnlocked));
			KeyCommand keyCommand2 = this.AddCommand("Goto Mode Tab", KeyCode.W).SetAction(delegate
			{
				this.OnClickedButtonTabBar(1);
			});
			
			keyCommand2.SetCondition(new CommandCondition(TutorialManager.IsModeTabUnlocked));
			KeyCommand keyCommand3 = this.AddCommand("Goto Heroes Tab", KeyCode.E).SetAction(delegate
			{
				this.OnClickedButtonTabBar(2);
			});
			
			keyCommand3.SetCondition(new CommandCondition(TutorialManager.IsHeroesTabUnlocked));
			KeyCommand keyCommand4 = this.AddCommand("Goto Artifacts Tab", KeyCode.R).SetAction(delegate
			{
				this.OnClickedButtonTabBar(3);
			});
			
			keyCommand4.SetCondition(new CommandCondition(TutorialManager.IsArtifactsTabUnlocked));
			KeyCommand keyCommand5 = this.AddCommand("Goto Shop Tab", KeyCode.T).SetAction(delegate
			{
				this.OnClickedButtonTabBar(4);
			});
			
			keyCommand5.SetCondition(new CommandCondition(TutorialManager.IsShopTabUnlocked));
			this.keyStroke.AddContext(30, true);
			this.AddCommand("Goto None", KeyCode.W).SetAction(delegate
			{
				this.OnClickedButtonTabBar(1);
			});
			this.keyStroke.AddContext(40, true);
			this.AddCommand("Goto None", KeyCode.E).SetAction(delegate
			{
				this.OnClickedButtonTabBar(2);
			});
			this.keyStroke.AddContext(50, true);
			this.AddCommand("Goto None", KeyCode.R).SetAction(delegate
			{
				this.OnClickedButtonTabBar(3);
			});
			this.keyStroke.AddContext(60, true);
			this.AddCommand("Goto None", KeyCode.T).SetAction(delegate
			{
				this.OnClickedButtonTabBar(4);
			});
		}

		public string GetMerchantItem(Simulation.MerchantItem item, World world)
		{
			switch (world.gameMode)
			{
			case GameMode.STANDARD:
			{
				Unlock unlock = null;
				for (int i = 0; i < world.unlocks.Count; i++)
				{
					Unlock unlock2 = world.unlocks[i];
					if (unlock2.GetMerchantItemId() == item.GetId())
					{
						unlock = unlock2;
						break;
					}
				}
				return string.Format(LM.Get("STAGE_ADVENTURE"), unlock.GetReqInt());
			}
			case GameMode.CRUSADE:
			{
				int num = 0;
				for (int j = 0; j < world.allChallenges.Count; j++)
				{
					ChallengeWithTime challengeWithTime = world.allChallenges[j] as ChallengeWithTime;
					if (challengeWithTime.unlock.GetMerchantItemId() == item.GetId())
					{
						num = j;
					}
				}
				return string.Format(LM.Get("STAGE_TIME_CHALLENGE"), num + 1);
			}
			case GameMode.RIFT:
			{
				int num2 = 0;
				for (int k = 0; k < world.allChallenges.Count; k++)
				{
					ChallengeRift challengeRift = world.allChallenges[k] as ChallengeRift;
					if (challengeRift.unlock.GetMerchantItemId() == item.GetId())
					{
						num2 = k;
					}
				}
				return string.Format(LM.Get("STAGE_RIFT"), num2 + 1);
			}
			}
			throw new Exception("Unlock could not found");
		}

		public string GetSocialNetworkName(SocialRewards.Network socialNetwork)
		{
			switch (socialNetwork)
			{
			case SocialRewards.Network.Facebook:
				return LM.Get("FACEBOOK_NAME");
			case SocialRewards.Network.Twitter:
				return LM.Get("TWITTER_NAME");
			case SocialRewards.Network.Instagram:
				return LM.Get("INSTAGRAM_NAME");
			default:
				throw new NotImplementedException();
			}
		}

		public static string GetTrinketEffectRarityLocString(TrinketEffectGroup group)
		{
			switch (group)
			{
			case TrinketEffectGroup.COMMON:
				return LM.Get("TRINKET_EFFECT_RARITY_COMMON");
			case TrinketEffectGroup.SECONDARY:
				return LM.Get("TRINKET_EFFECT_RARITY_SECONDARY");
			case TrinketEffectGroup.SPECIAL:
				return LM.Get("TRINKET_EFFECT_RARITY_SPECIAL");
			default:
				throw new NotImplementedException(group.ToString());
			}
		}

		public static Sprite GetBadgeIcon(BadgeId badgeId)
		{
			switch (badgeId)
			{
			case BadgeId.WintertadeParticipant:
				return UiData.inst.badgeWintertideParticipant;
			case BadgeId.WintertideCollector:
				return UiData.inst.badgeWintertideCollector;
			case BadgeId.SnakeEater:
				return UiData.inst.badgeSnakeEater;
			case BadgeId.OneYearAnniversaryParticipant:
				return UiData.inst.badgeOneYearAnniversaryParticipant;
			case BadgeId.WintertadeTopOfTree:
				return UiData.inst.badgeWintertideTopOfTree;
			case BadgeId.CataclysmSurviver:
				return UiData.inst.badgeCataclysmSurviver;
			case BadgeId.TwoYearsAnniversaryParticipant:
				return UiData.inst.badgeTwoYearsAnniversaryParticipant;
			default:
				throw new NotImplementedException();
			}
		}

		private UiState _state;

		public const float MinSecondsToAllowButtonClickEvent = 0.1f;

		public static bool stateJustChanged;

		public static float secondsSinceLastButtonClick = 0.1f;

		public static bool adventureMaxStageJustReached;

		public Simulator sim;

		private SoundManager soundManager;

		private UiData uiData;

		public static readonly Color CHARM_ICON_COLOR_IN_CARD = new Color32(247, 222, 160, byte.MaxValue);

		public Dictionary<Type, Sprite> spritesSkillIconMainScreen;

		public Dictionary<Type, Sprite> spritesSkillToggleIconMainScreen;

		public Dictionary<Type, Sprite> spritesSkillIconSkillScreen;

		public Dictionary<Type, Sprite> spritesRiftEffectIcon;

		public Dictionary<Type, Sprite> spritesRiftEffectGreyIcon;

		public Dictionary<int, Sprite> spritesCharmEffectIcon;

		public Dictionary<int, Sprite> spritesCurseEffectIcon;

		public Dictionary<string, Sprite> spritesGearIcon;

		public Dictionary<string, Sprite> spritesHeroPortrait;

		public Dictionary<string, Sprite[]> spritesHeroPortraits;

		public Dictionary<string, Sprite> spritesTotem;

		public Dictionary<string, Sprite> spritesTotemSmall;

		public Dictionary<string, Sprite> spritesTotemShineSmall;

		public Dictionary<string, Color> runeColorForRing;

		public Dictionary<string, Sprite> spritesRune;

		public Dictionary<string, Sprite> spritesMerchantItem;

		public Dictionary<Type, Sprite> spritesMerchantItemSmall;

		public Dictionary<GameMode, Sprite> spritesModeFlag;

		public Dictionary<string, Sprite> spritesBossPortrait;

		public Dictionary<string, Sprite> spritesAchievements;

		public Dictionary<int, Sprite> spritesQuesfOfUpdate;

		private static Dictionary<string, string> achievementTitle;

		private static Dictionary<string, string> achievementTitleFormat;

		private static Dictionary<string, string> achievementDesc;

		private static Dictionary<string, string> achievementDescParam;

		public static List<AahMonoBehaviour> toInits;

		public static List<AahMonoBehaviour> toUpdates;

		public static List<AahMonoBehaviour> willRemoveFromToUpdates;

		public static List<SoundEvent> sounds = new List<SoundEvent>();

		public const float popupBackgroundOpacity = 0.85f;

		public Canvas canvas;

		[SerializeField]
		private Color colorCantAffordInit;

		public static Color colorCantAfford;

		[SerializeField]
		private Color[] colorHeroLevelsInit;

		public static Color[] colorHeroLevels;

		[SerializeField]
		private Color[] colorArtifactLevelsInit;

		public static Color[] colorArtifactLevels;

		[SerializeField]
		private Color[] colorTrinketLevelsInit;

		public static Color[] colorTrinketLevels;

		[SerializeField]
		private Color[] colorCurrencyTypesInit;

		public static Color[] colorCurrencyTypes;

		public Color[] colorTabBgs;

		public Sprite[] spriteTabBgShadows;

		public Image[] imageTabBgShadows;

		public RectTransform uiElementsRect;

		public RectTransform uiMainBodyRect;

		public Transform cameraTransform;

		public List<ButtonTabBar> tabBarButtons;

		public GameObject tabBarBlocker;

		public GameObject tabBar;

		public PanelHub panelHub;

		public Text textModeName;

		public Text textWaveName;

		public Text textGold;

		public Image imageGold;

		private float imageGoldAnimTimer;

		private const float ImageGoldAnimStart = 0.1f;

		private const float ImageGoldAnimWait = 0.05f;

		private const float ImageGoldAnimEnd = 0.07f;

		private const float ImageGoldGoalScale = 1.3f;

		private Vector3 imageGoldScale;

		public static World imageGoldWorldToAnim;

		public Text textIdleGain;

		public Transform parentIdleGainStandard;

		public Transform parentIdleGainChallenge;

		public MerchantItemTimer[] merchantItemTimers;

		public RectTransform merchantItemTimersParent;

		public StageBar stageBar;

		public StageProgressionBar stageProgressionBar;

		public Transform barEnemyHealth;

		public Text textEnemyHealth;

		public List<GameButton> buttonSkills;

		public RectTransform buttonSkillsParent;

		private List<ButtonSkillAnim> buttonSkillAnims;

		public PanelHeroes panelHeroes;

		public PanelNextUnlockStage panelNextUnlockStage;

		public GameObject panelBossTimer;

		public Transform barBossTimer;

		public BossFrame bossFrame;

		public GameButton buttonLeaveBoss;

		public GameButton buttonFightBoss;

		public PanelMode panelMode;

		public PanelMerchantItemSelect panelMerchantItemSelect;

		public int selectedMerchantItem;

		public bool selectedMerchantItemIsFromEvent;

		public GameObject inputBlocker;

		public GameObject uiInputBlocker;

		[Header("Panel Debug")]
		public Button buttonDebug;

		public Button buttonDebugLog;

		public CanvasGroup debugButtonCanvasGroup;

		public GameObject panelDebug;

		private bool debugButtonsEnabled = true;

		public Text textDebugSpeedGame;

		public Slider sliderDebugSpeedGame;

		public Toggle toggleDebugAllFree;

		public Toggle toggleDebugAutoTap;

		public List<Button> buttonsStageChange;

		public Button buttonDebugGear;

		public Button buttonDebugRune;

		public Button buttonDebugRepickCurse;

		public Button buttonDebugTrinket;

		public Button buttonDebugUnlockEverything;

		public Button buttonDebugReset;

		public Button buttonDebugResetUltimates;

		public Button buttonDebugResetAutoActiveUltimates;

		public Button buttonDebugSimulateOffline;

		public Toggle toggleDebugAutoPlay;

		public Toggle toggleDebugShortOfferTimes;

		public Toggle toggleDebugDontUpdate;

		public Button buttonDebugBoostArtifacts;

		public Button buttonDebugSkipTutorials;

		public Button buttonDebugAddDailyProgress;

		public Button buttonDebugShowAdOffer;

		public Button buttonDebugRefreshSave;

		public Text buttonDebugRefreshSaveText;

		public Button buttonDebugLoadSave;

		public Text buttonDebugLoadSaveText;

		public Dropdown dropDownDebugCharmSelect;

		public Dropdown dropDownDebugRiftEffectSelect;

		public Dropdown dropDownDebugSaveSelect;

		public Button buttondebugOpenTapdaqDebugger;

		public Button buttondebugUnlockAllAdvanced;

		public Button buttondebugToggleFPS;

		public FPSDisplay fPSDisplay;

		public Button buttonDebugShortcutToTrinketTutorials;

		public Text textPlayTime;

		public Text textDebugVersion;

		public Button buttonPlayfabReward;

		public Button buttonCopyCrashes;

		public Button buttonlogsCrashes;

		public Button buttonDebugUnlockAllButRiftTutorials;

		[NonSerialized]
		public Button buttonDebugUnlockAllBadges;

		public InputField debugTrustetTimeDay;

		public InputField debugTrustetTimeMonth;

		public InputField debugTrustetTimeYear;

		public InputField debugTrustetTimeHour;

		public InputField debugTrustetTimeMinute;

		public InputField debugTrustetTimeSecond;

		public Button changeTrustedTimeDebugButton;

		[Header("Trinket Debug")]
		public TrinketDebug trinketDebug;

		[Header("------")]
		public CanNotEvolveArtifactPopup notEvolveArtifactPopup;

		public SecondAnniversaryPopup secondAnniversaryPopup;

		public ArtifactOverhaulPopup artifactOverhaulPopup;

		public ArtifactEvolveWindow artifactEvolveWindow;

		public PosibleArtifactEffectPopup posibleArtifactEffectPopup;

		public MaxStageReachedBanner maxStageReachedBanner;

		public PanelShareScreenshot panelShareScreenshot;

		public PanelScrapConverter panelScrapConverter;

		public PanelChristmasEventPopup panelChristmasEventPopup;

		public PopupChristmasCandyTreat popupChristmasCandyTreat;

		public PanelChristmasOffers panelChristmasOffer;

		public PanelChristmasOffersInfo panelChristmasOffersInfo;

		public PanelSkillsScreen panelSkillsScreen;

		public PanelGearScreen panelGearScreen;

		public PanelTrinketsScreen panelTrinketsScreen;

		public PanelTrinketsScreen panelSelectTrinket;

		public PanelTrinketsScreen panelDatabaseTrinket;

		public PanelTrinketSmithing panelTrinketSmithing;

		public PanelTrinketEffectScroller panelTrinketEffectScroller;

		public PanelHubTrinkets panelHubTrinkets;

		public PanelNewHero panelNewHero;

		public RectTransform gameplayArtifactsTabParent;

		public PanelArtifactsInfo panelArtifactsInfo;

		public PanelArtifactsCraft panelArtifactsCraft;

		public ArtifactRerollWindow panelArtifactsRerollWindow;

		public HeroSkinChanger panelHeroSkinChanger;

		public PanelRiftSelect panelRiftSelect;

		public PanelSelectTotem panelSelectTotem;

		public DailyQuestPopup dailyQuestPopup;

		public RectTransform gameplayTrinketsTabParent;

		public RectTransform hubTrinketsTabParent;

		public int openTab;

		public TabMenuAnim uiTabMenuAnim;

		public ScrollRect scrollView;

		private RectTransform scrollViewRect;

		public PanelHero panelHeroGearSkills;

		public Text textMenuHeader;

		public GameObject tabMenuHeader;

		public GameButton buttonMenuBack;

		public TopBarIcon topBarIcon;

		public GameButton buttonMenuTopBack;

		public DailyQuestIndicator openOfferPopupWidget;

		public GameButton openOfferPopupButton;

		public Image openOfferPopupButtonFillBar;

		public Image openOfferPopupButtonIcon;

		public Image openOfferPopupButtonNotif;

		public GameObject uiBg;

		public const float scrollViewY = -90f;

		public const float scrollViewYFullscreen = -90f;

		public const float scrollViewSizeY = 1110f;

		public const float scrollViewSizeYFullscreen = 1260f;

		public MenuShowCurrency[] menuShowCurrency;

		private UiCommand _command;

		public Hero selectedHeroGearSkills;

		public DailyQuestIndicator dailyQuestIndicatorWidget;

		public PanelUnlocks panelUnlocks;

		public GameObject panelUnlockPrefab;

		public GameObject panelLockedPrefab;

		public string nextUnlockId;

		public Unlock unlockAboutToBeCollected;

		public PanelShop panelShop;

		public PanelArtifactPopup panelArtifactPopup;

		public PanelNewTALMilestoneReachedPopup newTALMilestoneReachedPopup;

		public TrinketPackPopup shopTrinketPackSelect;

		public ShopLootpackSelect shopLootpackSelect;

		public ShopLootpackOpening shopLootpackOpening;

		public ShopSkinPackOpening shopSkinPackOpening;

		public ShopLootpackSummary shopLootpackSummary;

		public ShopPack selectedShopPack;

		public SpecialOfferKeeper selectedSpecialOfferKeeper;

		public GameObject panelLootItemPrefab;

		public GameObject panelLootItemBigPrefab;

		public HubOptions hubOptions;

		public HubOptionsWiki hubOptionsWiki;

		public PanelSupportPopup supportPopup;

		public PanelPrestige panelPrestige;

		public PanelChallengeWin panelUnlockReward;

		public PanelOfflineEarnings panelOfflineEarnings;

		public PanelHubModeSetup panelHubModeSetup;

		public PanelRing panelRing;

		public PanelChallengeWin panelChallengeWin;

		public PanelChallengeLose panelChallengeLose;

		public GameObject panelTopHudRegular;

		public PanelTopHudChallenge panelTopHudChallengeTimeWave;

		public PanelArtifactScroller panelArtifactScroller;

		public PanelTrinketsScroller panelTrinketsScroller;

		public PanelHubArtifacts panelHubartifacts;

		public PanelHubShop panelHubShop;

		public PanelBuyCharmFlashOffer panelBuyCharmFlashOffer;

		public PanelBuyAdventureFlashOffer panelBuyAdventureFlashOffer;

		public PanelTrinketSelect panelTrinketSelect;

		public PanelHeroesRunes panelHeroesRunes;

		public PanelHeroEvolve panelHeroEvolve;

		public PanelHeroEvolveSkin panelHeroEvolveSkin;

		public LoadingTransition loadingTransition;

		public PanelTransitionFade panelTransitionFade;

		public PanelSkinBuyingPopup panelSkinBuyingPopup;

		public PanelAdPopup panelAdPopup;

		public PanelSocialRewardPopup panelSocialRewardPopup;

		public PanelCurrencySide[] panelCurrencySides;

		public PanelCurrencySide[] panelCurrencyOnTop;

		public PanelTutorial panelTutorial;

		public PanelHubDatabase panelHubDatabase;

		public PanelHubTotems panelHubTotems;

		public GameObject modelPurchaseLoading;

		public Transform iconPurchaseLoading;

		public PanelCurrencyWarning panelCurrencyWarning;

		public PanelServerReward panelServerReward;

		public PanelCredits panelCredits;

		public PanelUpdateRequired panelUpdateRequired;

		public PanelGeneralPopup panelGeneralPopup;

		public PanelTrinketPopup panelTrinketPopup;

		public PanelAchievements panelAchievements;

		public GameObject panelAchievementPrefab;

		public PanelAchievementOfUpdate panelQuestOfUpdatePerfab;

		public XiaomiWarningPopup xiaomiWarningPopup;

		public PanelCharmSelect panelCharmSelect;

		public PanelRunningCharms panelRunningCharms;

		public PanelHubCharms panelHubCharms;

		public CharmInfoPopup panelCharmInfoPopup;

		public PanelCharmPackSelect panelCharmPackSelect;

		public PanelCharmPackOpening panelCharmPackOpening;

		public CharmSelectWidget charmSelectWidget;

		public PanelRiftEffectInfo panelRiftEffectInfo;

		public PanelTrinketInfoPopup panelTrinketInfoPopup;

		public PanelTrinketDisenchantAnim panelTrinketDisenchantAnim;

		public PanelTrinketRecycle panelTrinketRecycle;

		public SaveConflictPopup saveConflictPopup;

		public VersionNotesPopup versionNotesPopup;

		public PanelRatePopup panelRatePopup;

		public PanelMine panelMine;

		public PanelOfferPopup panelOfferPopup;

		public Sprite spriteGold;

		public Sprite spriteGoldTriangle;

		public Sprite spriteGoldSquare;

		public Sprite spriteGoldDisabled;

		public Sprite spriteGoldTriangleDisabled;

		public Sprite spriteGoldSquareDisabled;

		public Sprite spriteRewardIconGold;

		public Sprite spriteRewardIconGoldTriangle;

		public Sprite spriteRewardIconGoldSquare;

		public CharmEffectsPanel charmEffectsPanel;

		public RectTransform[] menuContents;

		public RectTransform[] menuSubContents;

		[HideInInspector]
		public GraphicRaycaster[] menuContentsGR;

		[HideInInspector]
		public GraphicRaycaster[] menuSubContentsGR;

		[HideInInspector]
		public List<GraphicRaycaster> currentGraphicRaycasters;

		public Dictionary<string, HeroUnlockDescKey> heroUnlockHintKeys;

		private bool isScrollViewLocked;

		private bool isMenuUpForSound;

		private bool isMenuHubStateForSound;

		private List<RiftDifficulty> riftDiffsNormal;

		private List<RiftDifficulty> riftDiffsCursed;

		public static bool DEBUGDontShowTransitionInEditor = false;

		public static Sprite spriteGoldIcon;

		public static Sprite spriteGoldIconDisabled;

		public static Sprite spriteRewardGold;

		public static bool isPurchasing;

		public static bool isBuyingHero;

		private bool acceptingServerReward;

		public static bool zeroScrollViewContentY = true;

		private int currentNewsIndex = int.MaxValue;

		private bool isInHubMenusOld;

		private bool currencyWarningInHub;

		private float volumeMusicBoss;

		private bool musicStandardExistsOld;

		private float screenOrientationTimer;

		private const float screenOrientationPeriod = 5f;

		public static bool showPlayfabCustomLoginWarning;

		public static bool resetButtonStates;

		public static int newCharmIdAdded = -1;

		public const float TabButton3X = 273f;

		public const float TabButton2X = 410f;

		private float checkImmidiateStateTimer;

		private float showSocialRewardPopupTimer;

		public static Vector3 POS_POWERUP_CRIT_CHANCE_FLY;

		public static Vector3 POS_POWERUP_COOLDOWN_FLY;

		public static Vector3 POS_POWERUP_REVIVE_FLY;

		private const string SpaceBetweenStringsFormat = "{0} {1}";

		private RectTransform m_rectTransform;

		public KeyStroke keyStroke;

		public bool willIntroduceCurses;

		public Image topLongScreenCurtain;

		public Image bottomLongScreenCurtain;

		public bool wasBottomLongCurtainUp;

		private float uiTopAndBottomDelta;

		private HeroSkillInstanceParams heroSkillInstanceParams = new HeroSkillInstanceParams();

		private const float TopUIButtonsCenteredPosition = -267.8f;

		private const float TopUIButtonsLeftPosition = -329.3f;

		private const float TopUIButtonsRightPosition = -212f;

		private static PanelCurrencySide[] _panelCurrencySides;

		private static PanelCurrencySide[] _panelCurrencyOnTop;

		public List<AahMonoBehaviour> monoBehaviours;

		private ButtonSkillAnim.ButtonStateArguments buttonSkillState = new ButtonSkillAnim.ButtonStateArguments();

		private int heroScreenForceUpdateTimer;

		private static bool everShownPlayfabCustomLoginWarningBefore;

		
	}
}
