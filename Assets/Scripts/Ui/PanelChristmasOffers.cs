using System;
using System.Collections.Generic;
using DG.Tweening;
using Simulation;
using Spine.Unity;
using Static;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
	public class PanelChristmasOffers : AahMonoBehaviour
	{
		public bool showingTreeTab { get; private set; }

		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.specialOfferWidgets = new List<SpecialOfferWidget>();
			this.showingTreeTab = true;
			this.treeTabButton.interactable = false;
			this.offersTabButton.interactable = true;
			this.treeTabButton.onClick = new GameButton.VoidFunc(this.OnTreeTabButtonClicked);
			this.offersTabButton.onClick = new GameButton.VoidFunc(this.OnOffersTabButtonClicked);
			this.treeScroll.onValueChanged.AddListener(new UnityAction<Vector2>(this.OnTreeScroll));
			this.ChangeOffersTabLightsColor();
			this.treeFullyPurchasedParticles.gameObject.SetActive(false);
		}

		private void OnTreeScroll(Vector2 arg0)
		{
			this.starParallax.UpdatePositions(this.treeScroll.verticalNormalizedPosition);
		}

		public void InitStrings()
		{
			this.headerTitle.text = LM.Get("CHRISTMAS_PANEL_TITLE");
			this.treeTabText.text = LM.Get("CHRISTMAS_TREE_TAB_TITLE");
			this.offersTabText.text = LM.Get("CHRISTMAS_OFFERS_TAB_TITLE");
			this.offersTabTitle.text = LM.Get("CHRISTMAS_OFFERS_TITLE");
			this.candiesDailyCapLabel.text = LM.Get("CANDY_DAILY_CAP");
			this.candyTreats[2].buttonUpgradeAnim.textUp.text = string.Format(LM.Get("UNLOCK_REWARD_CANDIES"), GameMath.GetDoubleString(12500.0));
			this.candyTreats[2].buttonUpgradeAnim.textDown.text = IapManager.productPriceStringsLocal[IapIds.CANDY_PACK_02];
		}

		public void PlayOfferPurchasedAnimationIfNecessary()
		{
			if (!this.offerPurchased)
			{
				return;
			}
			Sequence sequence = DOTween.Sequence();
			sequence.AppendInterval(0.3f);
			if (this.uiManager.sim.christmasOfferBundle.IsTreeFullyPurchased())
			{
				this.SetTreeFullyPurchasedAnimation(sequence);
			}
			else
			{
				this.SetOfferPurchasedAnimation(sequence);
			}
			sequence.Play<Sequence>();
			this.offerPurchased = false;
		}

		public void UpdatePanel(Simulator simulator, float deltaTime)
		{
			this.candies.SetCurrency(CurrencyType.CANDY, GameMath.GetFlooredDoubleString(simulator.GetCandies().GetAmount()), true, GameMode.STANDARD, true);
			bool flag = false;
			this.lightsAnimTimer += deltaTime;
			if (this.lightsAnimTimer >= 2f)
			{
				this.lightsAnimTimer -= 1f;
				flag = true;
			}
			if (this.showingTreeTab)
			{
				this.treeTabParent.SetActive(true);
				this.offersTabParent.SetActive(false);
				this.UpdateTreeTab(simulator, flag);
				if (flag && this.treeOffers != null)
				{
					this.ChangeTreeTabLightsColor();
				}
			}
			else
			{
				this.treeTabParent.SetActive(false);
				this.offersTabParent.SetActive(true);
				this.UpdateOffersTab(simulator, flag);
				if (flag)
				{
					this.ChangeOffersTabLightsColor();
				}
			}
			this.offersNotificationBadge.numNotifications = ((TutorialManager.christmasTreeEventUnlocked <= TutorialManager.ChristmasTreeEventUnlocked.OPEN_SHOP_TAB || ((simulator.candyDropAlreadyDisabled || !(RewardedAdManager.inst != null) || !TrustedTime.IsReady() || simulator.christmasCandyCappedVideoNotificationSeen || !RewardedAdManager.inst.IsRewardedCappedVideoAvailable(simulator.GetLastCappedCurrencyWatchedTime(CurrencyType.CANDY), CurrencyType.CANDY, simulator.christmasTreatVideosWatchedSinceLastReset)) && (simulator.christmasFreeCandyNotificationSeen || !TrustedTime.IsReady() || !(simulator.lastFreeCandyTreatClaimedDate < simulator.lastCandyAmountCapDailyReset)))) ? 0 : 1);
			this.offersTabText.enabled = (TutorialManager.christmasTreeEventUnlocked >= TutorialManager.ChristmasTreeEventUnlocked.OPEN_SHOP_TAB);
			this.offersTabLock.SetActive(TutorialManager.christmasTreeEventUnlocked < TutorialManager.ChristmasTreeEventUnlocked.OPEN_SHOP_TAB);
		}

		public void GoToOffersTab()
		{
			this.offersScroll.verticalNormalizedPosition = 1f;
			this.showingTreeTab = false;
			this.treeTabButton.interactable = true;
			this.offersTabButton.interactable = false;
			this.uiManager.sim.christmasCandyCappedVideoNotificationSeen = true;
			this.uiManager.sim.christmasFreeCandyNotificationSeen = true;
		}

		public void GoToTreeTab()
		{
			this.showingTreeTab = true;
			this.treeTabButton.interactable = false;
			this.offersTabButton.interactable = true;
		}

		private void UpdateTreeTab(Simulator simulator, bool changeLightsColor)
		{
			if (simulator.candyDropAlreadyDisabled)
			{
				this.candiesDailyCapParent.gameObject.SetActive(false);
			}
			else
			{
				this.candiesDailyCapParent.gameObject.SetActive(true);
				if (simulator.candyAmountCollectedSinceLastDailyCapReset >= PlayfabManager.titleData.christmasCandyCapAmount)
				{
					this.candiesDailyCap.enabled = false;
					if (TrustedTime.IsReady())
					{
						TimeSpan dailtyCapResetTimer = simulator.GetDailtyCapResetTimer();
						this.candiesDailyCapLabel.text = string.Format(LM.Get("SHOP_FLASH_OFFER_TIMER"), GameMath.GetTimeDatailedShortenedString(dailtyCapResetTimer));
					}
					else
					{
						this.candiesDailyCapLabel.text = LM.Get("UI_SHOP_CHEST_0_WAIT");
					}
				}
				else
				{
					this.candiesDailyCap.enabled = true;
					this.candiesDailyCap.text = StringExtension.Concat(GameMath.GetDoubleString(simulator.candyAmountCollectedSinceLastDailyCapReset), "/", GameMath.GetDoubleString(PlayfabManager.titleData.christmasCandyCapAmount));
					this.candiesDailyCapLabel.text = LM.Get("CANDY_DAILY_CAP");
				}
				this.candiesDailyCapProgressBar.SetScaleX((float)(simulator.candyAmountCollectedSinceLastDailyCapReset / PlayfabManager.titleData.christmasCandyCapAmount));
			}
			if (simulator.christmasOfferBundle != null)
			{
				this.treeScroll.gameObject.SetActive(true);
				if (this.treeOffers == null)
				{
					this.InitTreeOffers(simulator);
				}
				int i = 0;
				int count = simulator.christmasOfferBundle.tree.Count;
				while (i < count)
				{
					List<CalendarTreeOfferNode> list = simulator.christmasOfferBundle.tree[i];
					int j = 0;
					int count2 = list.Count;
					while (j < count2)
					{
						ChristmasTreeOfferWidget christmasTreeOfferWidget = this.treeOffers[i][j];
						CalendarTreeOfferNode calendarTreeOfferNode = list[j];
						bool flag = simulator.christmasOfferBundle.IsOfferUnlockedInTree(i, j);
						if (flag)
						{
							christmasTreeOfferWidget.offerIcon.SetAlpha(1f);
							christmasTreeOfferWidget.buyButton.fakeDisabled = !simulator.GetCandies().CanAfford(calendarTreeOfferNode.offerCost);
							if (calendarTreeOfferNode.offer.purchasesLeft == 0)
							{
								christmasTreeOfferWidget.priceBackground.gameObject.SetActive(false);
								christmasTreeOfferWidget.outlineImage.enabled = true;
							}
							else
							{
								christmasTreeOfferWidget.priceBackground.gameObject.SetActive(true);
								christmasTreeOfferWidget.outlineImage.enabled = false;
							}
						}
						else
						{
							christmasTreeOfferWidget.offerIcon.SetAlpha(0.5f);
							christmasTreeOfferWidget.priceBackground.gameObject.SetActive(false);
							christmasTreeOfferWidget.outlineImage.enabled = false;
						}
						this.SetDependenciesVisual(calendarTreeOfferNode, christmasTreeOfferWidget, simulator.christmasOfferBundle);
						j++;
					}
					i++;
				}
			}
			else
			{
				this.treeScroll.gameObject.SetActive(false);
			}
		}

		private void SetDependenciesVisual(CalendarTreeOfferNode node, ChristmasTreeOfferWidget nodeVisual, CalendarTreeOfferBundle bundle)
		{
			int num = node.dependencies.Length;
			if (num != 1)
			{
				if (num == 2)
				{
					nodeVisual.dependencyUnlockedLeft.parent.SetActive(node.offer.purchasesLeft == 0 && bundle.tree[node.dependencies[0].Key][node.dependencies[0].Value].offer.purchasesLeft == 0);
					nodeVisual.dependencyUnlockedRight.parent.SetActive(node.offer.purchasesLeft == 0 && bundle.tree[node.dependencies[1].Key][node.dependencies[1].Value].offer.purchasesLeft == 0);
				}
			}
			else
			{
				int key = node.dependencies[0].Key;
				int value = node.dependencies[0].Value;
				if ((nodeVisual.transform as RectTransform).anchoredPosition.x < (this.treeOffers[key][value].transform as RectTransform).anchoredPosition.x)
				{
					nodeVisual.dependencyUnlockedRight.parent.SetActive(node.offer.purchasesLeft == 0 && bundle.tree[key][value].offer.purchasesLeft == 0);
				}
				else
				{
					nodeVisual.dependencyUnlockedLeft.parent.SetActive(node.offer.purchasesLeft == 0 && bundle.tree[key][value].offer.purchasesLeft == 0);
				}
			}
		}

		private void UpdateOffersTab(Simulator simulator, bool changeLightsColor)
		{
			this.candyTreats[0].buttonUpgradeAnim.textUp.text = string.Format(LM.Get("UNLOCK_REWARD_CANDIES"), GameMath.GetDoubleString(PlayfabManager.titleData.christmasAdCandiesAmount));
			this.candyTreats[1].buttonUpgradeAnim.textUp.text = string.Format(LM.Get("UNLOCK_REWARD_CANDIES"), GameMath.GetDoubleString(PlayfabManager.titleData.christmasFreeCandiesAmount));
			if (simulator.candyDropAlreadyDisabled)
			{
				this.eventTimer.gameObject.SetActive(false);
				this.candyTreats[0].buttonUpgradeAnim.textDown.text = LM.Get("UI_MERCHANT_ITEM_SOLDOUT");
				this.candyTreats[0].buttonUpgradeAnim.gameButton.interactable = false;
				this.candyTreats[1].buttonUpgradeAnim.textDown.text = LM.Get("UI_MERCHANT_ITEM_SOLDOUT");
				this.candyTreats[1].buttonUpgradeAnim.gameButton.interactable = false;
			}
			else
			{
				this.candyTreats[0].buttonUpgradeAnim.gameButton.interactable = true;
				this.candyTreats[1].buttonUpgradeAnim.gameButton.interactable = true;
				if (TrustedTime.IsReady() && PlayfabManager.christmasOfferConfigLoaded)
				{
					this.eventTimer.text = StringExtension.Concat(LM.Get("EVENT_ENDS_IN"), " ", GameMath.GetTimeDatailedShortenedString(PlayfabManager.christmasOfferConfig.candyDropLimitDateParsed - TrustedTime.Get()));
				}
				else
				{
					this.eventTimer.text = LM.Get("UI_SHOP_CHEST_0_WAIT");
				}
				if (RewardedAdManager.inst != null && TrustedTime.IsReady() && !RewardedAdManager.inst.IsRewardedCappedVideoAvailable(simulator.GetLastCappedCurrencyWatchedTime(CurrencyType.CANDY), CurrencyType.CANDY, simulator.christmasTreatVideosWatchedSinceLastReset) && RewardedAdManager.inst.IsRewardedVideoAvailable())
				{
					this.candyTreats[0].buttonUpgradeAnim.textDown.text = GameMath.GetTimeDatailedShortenedString(simulator.GetDailtyCapResetTimer());
				}
				else if ((RewardedAdManager.inst != null && TrustedTime.IsReady() && !RewardedAdManager.inst.IsRewardedCappedVideoAvailable(simulator.GetLastCappedCurrencyWatchedTime(CurrencyType.CANDY), CurrencyType.CANDY, simulator.christmasTreatVideosWatchedSinceLastReset) && !RewardedAdManager.inst.IsRewardedVideoAvailable()) || !TrustedTime.IsReady())
				{
					this.candyTreats[0].buttonUpgradeAnim.textDown.text = LM.Get("UI_SHOP_AD_LATER");
					this.candyTreats[0].buttonUpgradeAnim.gameButton.interactable = false;
				}
				else
				{
					this.candyTreats[0].buttonUpgradeAnim.textDown.text = LM.Get("UI_AD_WATCH");
				}
				if (TrustedTime.IsReady() && simulator.lastCandyAmountCapDailyReset < simulator.lastFreeCandyTreatClaimedDate)
				{
					this.candyTreats[1].buttonUpgradeAnim.textDown.text = GameMath.GetTimeDatailedShortenedString(simulator.GetDailtyCapResetTimer());
				}
				else
				{
					this.candyTreats[1].buttonUpgradeAnim.textDown.text = LM.Get("UI_SHOP_CHEST_0");
				}
			}
			if (UiManager.stateJustChanged)
			{
				this.candyTreats[2].buttonUpgradeAnim.textDown.text = IapManager.productPriceStringsLocal[IapIds.CANDY_PACK_02];
			}
			int num = 0;
			if (TrustedTime.IsReady())
			{
				int count = simulator.specialOfferBoard.outOfShopOffers.Count;
				DateTime currentTime = TrustedTime.Get();
				for (int i = 0; i < count; i++)
				{
					SpecialOfferKeeper specialOfferKeeper = simulator.specialOfferBoard.outOfShopOffers[i];
					SpecialOfferWidget orInstantiateOfferWidget = this.GetOrInstantiateOfferWidget(num++);
					if (UiManager.stateJustChanged)
					{
						ShopPackVisual shopPackVisual = this.uiManager.panelShop.GetShopPackVisual(specialOfferKeeper.offerPack);
						if (orInstantiateOfferWidget.packVisual == null)
						{
							orInstantiateOfferWidget.AddPackVisual(shopPackVisual);
							orInstantiateOfferWidget.onClick = new Action<int>(this.uiManager.OnClickedOutOfShopPackOffer);
						}
						else if (shopPackVisual.id != orInstantiateOfferWidget.packVisual.id)
						{
							orInstantiateOfferWidget.packVisual.DestoySelf();
							orInstantiateOfferWidget.AddPackVisual(shopPackVisual);
							orInstantiateOfferWidget.onClick = new Action<int>(this.uiManager.OnClickedOutOfShopPackOffer);
						}
						orInstantiateOfferWidget.rectTransform.SetAnchorPosY(210f - (float)num * 420f);
						orInstantiateOfferWidget.header.text = specialOfferKeeper.offerPack.GetName();
						orInstantiateOfferWidget.offerIndex = i;
						this.uiManager.panelShop.SetSpecialOfferVisualType(orInstantiateOfferWidget, false, false, true, false);
						if (specialOfferKeeper.offerPack.isIAP)
						{
							orInstantiateOfferWidget.price.SetCurrency(CurrencyType.GEM, specialOfferKeeper.offerPack.GetPriceString(), false, GameMode.RIFT, false);
							orInstantiateOfferWidget.price.SetTextX(0f);
						}
						else
						{
							orInstantiateOfferWidget.price.SetCurrency(specialOfferKeeper.offerPack.currency, specialOfferKeeper.offerPack.GetPriceString(), false, GameMode.STANDARD, true);
							orInstantiateOfferWidget.price.SetTextX(27.55f);
						}
						orInstantiateOfferWidget.discountBase.SetActive(specialOfferKeeper.offerPack.IsOfferFromOtherIap);
						if (specialOfferKeeper.offerPack.IsOfferFromOtherIap)
						{
							orInstantiateOfferWidget.originalPrice.text = specialOfferKeeper.offerPack.OriginalLocalizedPrice;
						}
						orInstantiateOfferWidget.SetContentText(specialOfferKeeper.offerPack, simulator.GetUniversalBonusAll());
					}
					TimeSpan remainingDur = specialOfferKeeper.GetRemainingDur(currentTime);
					orInstantiateOfferWidget.timer.text = string.Format(LM.Get("SHOP_PACK_TIME"), GameMath.GetTimeDatailedShortenedString(remainingDur));
					orInstantiateOfferWidget.timer.rectTransform.sizeDelta = ((!specialOfferKeeper.offerPack.IsOfferFromOtherIap) ? SpecialOfferWidget.NormalTimerSize : SpecialOfferWidget.DiscountTimerSize);
					if (UiManager.stateJustChanged)
					{
						this.offersScrollContentParent.SetSizeDeltaY(570f + 420f * (float)num);
					}
				}
				int j = num;
				int count2 = this.specialOfferWidgets.Count;
				while (j < count2)
				{
					this.specialOfferWidgets[num].gameObject.SetActive(false);
					j++;
				}
			}
			else
			{
				for (int k = this.specialOfferWidgets.Count - 1; k >= 0; k--)
				{
					this.specialOfferWidgets[k].gameObject.SetActive(false);
				}
			}
		}

		private SpecialOfferWidget GetOrInstantiateOfferWidget(int widgetIndex)
		{
			if (this.specialOfferWidgets.Count <= widgetIndex)
			{
				this.specialOfferWidgets.Add(UnityEngine.Object.Instantiate<SpecialOfferWidget>(this.uiManager.panelShop.specialOfferWidgetPrefab, this.offersParent));
				UiManager.stateJustChanged = true;
			}
			this.specialOfferWidgets[widgetIndex].gameObject.SetActive(true);
			return this.specialOfferWidgets[widgetIndex];
		}

		private void ChangeTreeTabLightsColor()
		{
			for (int i = this.treeOffers.Count - 1; i >= 0; i--)
			{
				for (int j = this.treeOffers[i].Count - 1; j >= 0; j--)
				{
					this.ChangeOfferDependencyLightsColor(this.treeOffers[i][j].dependencyUnlockedLeft);
					this.ChangeOfferDependencyLightsColor(this.treeOffers[i][j].dependencyUnlockedRight);
				}
			}
		}

		private void ChangeOfferDependencyLightsColor(ChristmasTreeOfferWidget.DependencyObject dependencyObject)
		{
			if (dependencyObject.parent.activeSelf)
			{
				for (int i = 0; i < dependencyObject.lights.Length; i++)
				{
					PanelChristmasOffers.TreeLightColorSetUp randomArrayElement = this.treeNodeConnectionsLightsSetUps.GetRandomArrayElement<PanelChristmasOffers.TreeLightColorSetUp>();
					dependencyObject.lights[i].baseImage.color = randomArrayElement.baseColor;
					dependencyObject.lights[i].glowImage.color = randomArrayElement.glowColor;
				}
			}
		}

		private void ChangeOffersTabLightsColor()
		{
			for (int i = 0; i < this.treeLights.Length; i++)
			{
				PanelChristmasOffers.TreeLightColorSetUp randomArrayElement = this.treeLightSetUps.GetRandomArrayElement<PanelChristmasOffers.TreeLightColorSetUp>();
				this.treeLights[i].baseImage.color = randomArrayElement.baseColor;
				this.treeLights[i].glowImage.color = randomArrayElement.glowColor;
			}
		}

		private void InitTreeOffers(Simulator simulator)
		{
			this.treeOffers = new List<List<ChristmasTreeOfferWidget>>();
			int num = simulator.christmasOfferBundle.tree.Count - 1;
			for (int i = num; i >= 0; i--)
			{
				this.treeOffers.Add(new List<ChristmasTreeOfferWidget>());
			}
			for (int j = num; j >= 0; j--)
			{
				List<CalendarTreeOfferNode> list = simulator.christmasOfferBundle.tree[j];
				int count = list.Count;
				float num2 = (count % 2 != 0) ? 0f : 82.5f;
				int num3 = count / 2;
				for (int k = 0; k < count; k++)
				{
					ChristmasTreeOfferWidget christmasTreeOfferWidget = UnityEngine.Object.Instantiate<ChristmasTreeOfferWidget>(this.treeOfferPrefab, this.treeContentParent);
					(christmasTreeOfferWidget.transform as RectTransform).anchoredPosition = new Vector2(165f * (float)(k - num3) + num2, (float)(j - num) * 155f);
					this.treeOffers[j].Add(christmasTreeOfferWidget);
					int rowIndex = j;
					int nodeIndex = k;
					christmasTreeOfferWidget.buyButton.Register();
					christmasTreeOfferWidget.buyButton.gameButton.Register();
					christmasTreeOfferWidget.buyButton.gameButton.onClick = delegate()
					{
						this.lastOfferSelectedRow = rowIndex;
						this.lastOfferSelectedNode = nodeIndex;
						this.OnTreeOfferClicked(rowIndex, nodeIndex);
					};
					CalendarTreeOfferNode calendarTreeOfferNode = simulator.christmasOfferBundle.tree[j][k];
					PanelChristmasOffers.TreeNodeOfferSetUp nodeSetupFor = this.GetNodeSetupFor(calendarTreeOfferNode.offer);
					christmasTreeOfferWidget.outlineImage.sprite = nodeSetupFor.outline;
					christmasTreeOfferWidget.offerIcon.sprite = nodeSetupFor.icon;
					christmasTreeOfferWidget.offerIcon.SetNativeSize();
					christmasTreeOfferWidget.buyButton.textDown.text = GameMath.GetDoubleString(calendarTreeOfferNode.offerCost);
				}
			}
			for (int l = num; l >= 0; l--)
			{
				int m = 0;
				int count2 = simulator.christmasOfferBundle.tree[l].Count;
				while (m < count2)
				{
					ChristmasTreeOfferWidget christmasTreeOfferWidget2 = this.treeOffers[l][m];
					CalendarTreeOfferNode calendarTreeOfferNode2 = simulator.christmasOfferBundle.tree[l][m];
					int num4 = calendarTreeOfferNode2.dependencies.Length;
					if (num4 != 0)
					{
						if (num4 != 1)
						{
							if (num4 == 2)
							{
								christmasTreeOfferWidget2.dependencyLeft.SetActive(true);
								christmasTreeOfferWidget2.dependencyRight.SetActive(true);
							}
						}
						else
						{
							int key = calendarTreeOfferNode2.dependencies[0].Key;
							int value = calendarTreeOfferNode2.dependencies[0].Value;
							if ((christmasTreeOfferWidget2.transform as RectTransform).anchoredPosition.x < (this.treeOffers[key][value].transform as RectTransform).anchoredPosition.x)
							{
								christmasTreeOfferWidget2.dependencyLeft.SetActive(false);
								christmasTreeOfferWidget2.dependencyUnlockedLeft.parent.SetActive(false);
								christmasTreeOfferWidget2.dependencyRight.SetActive(true);
							}
							else
							{
								christmasTreeOfferWidget2.dependencyLeft.SetActive(true);
								christmasTreeOfferWidget2.dependencyRight.SetActive(false);
								christmasTreeOfferWidget2.dependencyUnlockedRight.parent.SetActive(false);
							}
						}
					}
					else
					{
						christmasTreeOfferWidget2.dependencyLeft.SetActive(false);
						christmasTreeOfferWidget2.dependencyUnlockedLeft.parent.SetActive(false);
						christmasTreeOfferWidget2.dependencyRight.SetActive(false);
						christmasTreeOfferWidget2.dependencyUnlockedRight.parent.SetActive(false);
					}
					m++;
				}
			}
			this.treeContentParent.SetSizeDeltaY((float)this.treeOffers.Count * 155f + 30f);
			this.ChangeTreeTabLightsColor();
		}

		private void OnTreeTabButtonClicked()
		{
			if (this.showingTreeTab)
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiDefaultFailClick, 1f));
			}
			else
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
				this.GoToTreeTab();
			}
		}

		private void OnOffersTabButtonClicked()
		{
			if (this.showingTreeTab)
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
				this.GoToOffersTab();
			}
			else
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiDefaultFailClick, 1f));
			}
		}

		private PanelChristmasOffers.TreeNodeOfferSetUp GetNodeSetupFor(FlashOffer flashOffer)
		{
			switch (flashOffer.type)
			{
			case FlashOffer.Type.SCRAP:
				return this.scrapsNodeSetup;
			case FlashOffer.Type.GEM:
				return this.gemsNodeSetup;
			case FlashOffer.Type.TOKEN:
				return this.tokensNodeSetup;
			case FlashOffer.Type.COSTUME:
				return this.costumeNodeSetup;
			case FlashOffer.Type.MERCHANT_ITEM:
			{
				string genericStringId = flashOffer.genericStringId;
				if (genericStringId != null)
				{
					if (genericStringId == "BLIZZARD")
					{
						return this.merchantItemBlizzardNodeSetup;
					}
					if (genericStringId == "HOT_COCOA")
					{
						return this.merchantItemHotCocoaNodeSetup;
					}
					if (genericStringId == "ORNAMENT_DROP")
					{
						return this.merchantItemOrnamentDropNodeSetup;
					}
				}
				throw new NotImplementedException();
			}
			}
			throw new NotImplementedException();
		}

		private void SetTreeFullyPurchasedAnimation(Sequence sequence)
		{
			CalendarTreeOfferBundle christmasOfferBundle = this.uiManager.sim.christmasOfferBundle;
			this.treeScroll.verticalNormalizedPosition = 0f;
			sequence.Append(this.treeOffers[0][0].outlineImage.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
			UiManager.AddUiSound(SoundArchieve.inst.treeFullyPurchasedAnim);
			int num = 0;
			int i = 1;
			int num2 = christmasOfferBundle.tree.Count - 1;
			while (i < num2)
			{
				if (i % 2 == 0)
				{
					int num3 = christmasOfferBundle.tree[i].Count - 1;
					for (int j = num3; j >= 0; j--)
					{
						ChristmasTreeOfferWidget christmasTreeOfferWidget = this.treeOffers[i][j];
						num++;
						christmasTreeOfferWidget.outlineImage.transform.SetScale(0f);
						sequence.Insert(0.3f + (float)num * 0.08f, christmasTreeOfferWidget.outlineImage.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
						if (christmasTreeOfferWidget.dependencyLeft.activeSelf)
						{
							for (int k = 0; k < christmasTreeOfferWidget.dependencyUnlockedLeft.lights.Length; k++)
							{
								christmasTreeOfferWidget.dependencyUnlockedLeft.lights[k].transform.SetScale(0f);
								sequence.Insert((float)num * 0.08f + (float)k * 0.02f, christmasTreeOfferWidget.dependencyUnlockedLeft.lights[k].transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
							}
						}
						if (christmasTreeOfferWidget.dependencyRight.activeSelf)
						{
							for (int l = 0; l < christmasTreeOfferWidget.dependencyUnlockedRight.lights.Length; l++)
							{
								christmasTreeOfferWidget.dependencyUnlockedRight.lights[l].transform.SetScale(0f);
								sequence.Insert((float)num * 0.08f + (float)l * 0.02f, christmasTreeOfferWidget.dependencyUnlockedRight.lights[l].transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
							}
						}
					}
				}
				else
				{
					int m = 0;
					int count = christmasOfferBundle.tree[i].Count;
					while (m < count)
					{
						ChristmasTreeOfferWidget christmasTreeOfferWidget2 = this.treeOffers[i][m];
						num++;
						christmasTreeOfferWidget2.outlineImage.transform.SetScale(0f);
						sequence.Insert(0.3f + (float)num * 0.08f, christmasTreeOfferWidget2.outlineImage.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
						if (christmasTreeOfferWidget2.dependencyLeft.activeSelf)
						{
							for (int n = 0; n < christmasTreeOfferWidget2.dependencyUnlockedLeft.lights.Length; n++)
							{
								christmasTreeOfferWidget2.dependencyUnlockedLeft.lights[n].transform.SetScale(0f);
								sequence.Insert((float)num * 0.08f + (float)n * 0.02f, christmasTreeOfferWidget2.dependencyUnlockedLeft.lights[n].transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
							}
						}
						if (christmasTreeOfferWidget2.dependencyRight.activeSelf)
						{
							for (int num4 = 0; num4 < christmasTreeOfferWidget2.dependencyUnlockedRight.lights.Length; num4++)
							{
								christmasTreeOfferWidget2.dependencyUnlockedRight.lights[num4].transform.SetScale(0f);
								sequence.Insert((float)num * 0.08f + (float)num4 * 0.02f, christmasTreeOfferWidget2.dependencyUnlockedRight.lights[num4].transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
							}
						}
						m++;
					}
				}
				i++;
			}
			ChristmasTreeOfferWidget christmasTreeOfferWidget3 = this.treeOffers[this.treeOffers.Count - 1][0];
			num++;
			christmasTreeOfferWidget3.outlineImage.transform.SetScale(0f);
			sequence.Insert(0.3f + 0.08f * (float)num + 0.3f, christmasTreeOfferWidget3.outlineImage.transform.DOScale(1.2f, 0.1f).SetEase(Ease.OutCirc));
			sequence.Insert(0.3f + 0.08f * (float)num + 0.5f, christmasTreeOfferWidget3.outlineImage.transform.DOScale(0.8f, 0.2f).SetEase(Ease.InOutCubic));
			sequence.Insert(0.3f + 0.08f * (float)num + 0.7f, christmasTreeOfferWidget3.outlineImage.transform.DOScale(1f, 0.2f).SetEase(Ease.InOutCubic));
			for (int num5 = 0; num5 < christmasTreeOfferWidget3.dependencyUnlockedLeft.lights.Length; num5++)
			{
				christmasTreeOfferWidget3.dependencyUnlockedLeft.lights[num5].transform.SetScale(0f);
				sequence.Insert((float)num * 0.08f + (float)num5 * 0.02f, christmasTreeOfferWidget3.dependencyUnlockedLeft.lights[num5].transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
			}
			for (int num6 = 0; num6 < christmasTreeOfferWidget3.dependencyUnlockedRight.lights.Length; num6++)
			{
				christmasTreeOfferWidget3.dependencyUnlockedRight.lights[num6].transform.SetScale(0f);
				sequence.Insert((float)num * 0.08f + (float)num6 * 0.02f, christmasTreeOfferWidget3.dependencyUnlockedRight.lights[num6].transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
			}
			sequence.InsertCallback(0.3f + 0.08f * (float)num + 0.3f, delegate
			{
				this.treeFullyPurchasedParticles.gameObject.SetActive(true);
				this.treeFullyPurchasedParticles.AnimationState.SetAnimation(0, "animation", false);
			});
			sequence.Insert(0.3f, this.treeScroll.DOVerticalNormalizedPos(1f, 0.08f * (float)num, false).SetEase(this.treeFullyPruchasedScrollAnimation));
		}

		private void SetOfferPurchasedAnimation(Sequence sequence)
		{
			ChristmasTreeOfferWidget christmasTreeOfferWidget = this.treeOffers[this.lastOfferSelectedRow][this.lastOfferSelectedNode];
			christmasTreeOfferWidget.outlineImage.rectTransform.SetScale(0f);
			float num = 0.3f;
			bool flag = false;
			if (christmasTreeOfferWidget.dependencyLeft.activeSelf)
			{
				num = 0.9f;
				flag = true;
				sequence.InsertCallback(0.3f, delegate
				{
					UiManager.AddUiSound(SoundArchieve.inst.soundChristmasTreeStrings);
				});
				for (int i = 0; i < christmasTreeOfferWidget.dependencyUnlockedLeft.lights.Length; i++)
				{
					christmasTreeOfferWidget.dependencyUnlockedLeft.lights[i].transform.SetScale(0f);
					sequence.Insert(0.3f + (float)i * 0.2f, christmasTreeOfferWidget.dependencyUnlockedLeft.lights[i].transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
				}
			}
			if (christmasTreeOfferWidget.dependencyRight.activeSelf)
			{
				num = 0.9f;
				if (!flag)
				{
					sequence.InsertCallback(0.3f, delegate
					{
						UiManager.AddUiSound(SoundArchieve.inst.soundChristmasTreeStrings);
					});
				}
				for (int j = 0; j < christmasTreeOfferWidget.dependencyUnlockedRight.lights.Length; j++)
				{
					christmasTreeOfferWidget.dependencyUnlockedRight.lights[j].transform.SetScale(0f);
					sequence.Insert(0.3f + (float)j * 0.2f, christmasTreeOfferWidget.dependencyUnlockedRight.lights[j].transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
				}
			}
			sequence.Insert(num, christmasTreeOfferWidget.outlineImage.rectTransform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
			sequence.InsertCallback(num, delegate
			{
				UiManager.AddUiSound(SoundArchieve.inst.soundChristmasTreeBalls);
			});
			flag = false;
			int num2 = this.lastOfferSelectedRow + 1;
			if (num2 < this.uiManager.sim.christmasOfferBundle.tree.Count)
			{
				List<CalendarTreeOfferNode> list = this.uiManager.sim.christmasOfferBundle.tree[num2];
				for (int k = list.Count - 1; k >= 0; k--)
				{
					if (list[k].dependencies != null)
					{
						for (int l = 0; l < list[k].dependencies.Length; l++)
						{
							if (list[k].offer.purchasesLeft == 0 && list[k].dependencies[l].Key == this.lastOfferSelectedRow && list[k].dependencies[l].Value == this.lastOfferSelectedNode)
							{
								ChristmasTreeOfferWidget christmasTreeOfferWidget2 = this.treeOffers[this.lastOfferSelectedRow + 1][k];
								ChristmasTreeOfferWidget.DependencyObject dependencyObject;
								if ((christmasTreeOfferWidget.transform as RectTransform).anchoredPosition.x < (christmasTreeOfferWidget2.transform as RectTransform).anchoredPosition.x)
								{
									dependencyObject = christmasTreeOfferWidget2.dependencyUnlockedLeft;
								}
								else
								{
									dependencyObject = christmasTreeOfferWidget2.dependencyUnlockedRight;
								}
								for (int m = 0; m < dependencyObject.lights.Length; m++)
								{
									dependencyObject.lights[m].transform.SetScale(0f);
									sequence.Insert(num + 0.2f + (float)m * 0.2f, dependencyObject.lights[m].transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
								}
								if (!flag)
								{
									sequence.InsertCallback(num + 0.2f, delegate
									{
										UiManager.AddUiSound(SoundArchieve.inst.soundChristmasTreeStrings);
									});
								}
								flag = true;
							}
						}
					}
				}
			}
		}

		public PanelChristmasOffers.TreeLightColorSetUp[] treeLightSetUps;

		public PanelChristmasOffers.TreeLightColorSetUp[] treeNodeConnectionsLightsSetUps;

		[Header("Header")]
		public Text headerTitle;

		public MenuShowCurrency candies;

		public GameButton closeButton;

		[Header("Tabs")]
		public Text treeTabText;

		public Text offersTabText;

		public GameButton treeTabButton;

		public GameButton offersTabButton;

		public GameObject treeTabParent;

		public GameObject offersTabParent;

		public NotificationBadge offersNotificationBadge;

		public GameObject offersTabLock;

		[Header("Tree Tab")]
		public GameButton infoButton;

		public Text candiesDailyCapLabel;

		public Text candiesDailyCap;

		public RectTransform candiesDailyCapProgressBar;

		public RectTransform candiesDailyCapParent;

		public RectTransform treeContentParent;

		public ChristmasTreeOfferWidget treeOfferPrefab;

		public ScrollRect treeScroll;

		public PanelChristmasOffers.TreeNodeOfferSetUp tokensNodeSetup;

		public PanelChristmasOffers.TreeNodeOfferSetUp gemsNodeSetup;

		public PanelChristmasOffers.TreeNodeOfferSetUp scrapsNodeSetup;

		public PanelChristmasOffers.TreeNodeOfferSetUp costumeNodeSetup;

		public PanelChristmasOffers.TreeNodeOfferSetUp merchantItemBlizzardNodeSetup;

		public PanelChristmasOffers.TreeNodeOfferSetUp merchantItemHotCocoaNodeSetup;

		public PanelChristmasOffers.TreeNodeOfferSetUp merchantItemOrnamentDropNodeSetup;

		public AnimationCurve treeFullyPruchasedScrollAnimation;

		public SkeletonGraphic treeFullyPurchasedParticles;

		public List<List<ChristmasTreeOfferWidget>> treeOffers;

		[Header("Offers Tab")]
		public Text offersTabTitle;

		public Text eventTimer;

		public MerchantItem[] candyTreats;

		public RectTransform offersParent;

		public RectTransform offersScrollContentParent;

		public TreeLightWidget[] treeLights;

		public ScrollRect offersScroll;

		public PanelChristmasOffers.StarParallax starParallax;

		[NonSerialized]
		public List<SpecialOfferWidget> specialOfferWidgets;

		[NonSerialized]
		public UiManager uiManager;

		[NonSerialized]
		public UiState previousState;

		public HeroDataBase selectedHeroOnPreviousState;

		public SkinData selectedSkinOnPreviousState;

		public Action<int, int> OnTreeOfferClicked;

		public const int CandyTreatsIapIdIndexOffset = 18;

		private const float TreeLightsColorChangePeriod = 2f;

		private float lightsAnimTimer;

		private int lastOfferSelectedRow = -1;

		private int lastOfferSelectedNode = -1;

		[NonSerialized]
		public bool offerPurchased;

		[Serializable]
		public class TreeLightColorSetUp
		{
			public Color baseColor;

			public Color glowColor;
		}

		[Serializable]
		public class TreeNodeOfferSetUp
		{
			public Sprite outline;

			public Sprite icon;
		}

		[Serializable]
		public class StarParallax
		{
			public void UpdatePositions(float normalizedValue)
			{
				float position = Mathf.LerpUnclamped(this.max, this.min, normalizedValue);
				foreach (PanelChristmasOffers.StarParallax.ParallaxLayer parallaxLayer in this.layers)
				{
					parallaxLayer.SetPosition(position);
				}
			}

			public PanelChristmasOffers.StarParallax.ParallaxLayer[] layers;

			public float min;

			public float max = 1000f;

			[Serializable]
			public class ParallaxLayer
			{
				public void SetPosition(float pos)
				{
					this.rectTransform.SetAnchorPosY(pos * this.power);
				}

				public RectTransform rectTransform;

				public float power = 0.8f;
			}
		}
	}
}
