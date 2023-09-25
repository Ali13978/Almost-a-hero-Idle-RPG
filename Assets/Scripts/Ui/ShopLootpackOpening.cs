using System;
using System.Collections.Generic;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ShopLootpackOpening : AahMonoBehaviour
	{
		public ShopLootpackOpening.SubState subState
		{
			get
			{
				return this._subState;
			}
			set
			{
				this._subState = value;
				this.animatingSub = true;
				this.timerSub = 0f;
				switch (this._subState)
				{
				case ShopLootpackOpening.SubState.None:
				{
					int i = 0;
					int num = this.lootSimpleSubs.Length;
					while (i < num)
					{
						this.lootSimpleSubs[i].color = new Color(1f, 1f, 1f, 0f);
						i++;
					}
					break;
				}
				case ShopLootpackOpening.SubState.LootSimpleSlide:
					this.periodSub = this.periodLootSimpleSlide;
					break;
				case ShopLootpackOpening.SubState.LootSimpleFadeOut:
				{
					int j = 0;
					int num2 = this.lootSimpleSubs.Length;
					while (j < num2)
					{
						this.lootSimpleSubs[j].sprite = this.lootSimples[j].sprite;
						this.lootSimpleSubs[j].rectTransform.eulerAngles = this.lootSimples[j].rectTransform.eulerAngles;
						j++;
					}
					int k = 0;
					int num3 = this.lootSimples.Length;
					while (k < num3)
					{
						this.lootSimples[k].rectTransform.localScale = Vector3.zero;
						k++;
					}
					this.periodSub = this.periodLootSimpleFadeOut;
					break;
				}
				case ShopLootpackOpening.SubState.LootSimpleGearFadeOut:
				{
					int l = 0;
					int num4 = this.lootSimpleSubs.Length;
					while (l < num4)
					{
						this.lootSimpleSubs[l].sprite = this.lootSimples[l].sprite;
						this.lootSimpleSubs[l].rectTransform.eulerAngles = this.lootSimples[l].rectTransform.eulerAngles;
						l++;
					}
					int m = 0;
					int num5 = this.lootSimples.Length;
					while (m < num5)
					{
						this.lootSimples[m].rectTransform.localScale = Vector3.zero;
						m++;
					}
					this.periodSub = this.periodLootSimpleFadeOut;
					break;
				}
				default:
					throw new NotImplementedException();
				}
			}
		}

		public ShopLootpackOpening.State state
		{
			get
			{
				return this._state;
			}
			set
			{
				this.animating = true;
				this.timer = 0f;
				switch (value)
				{
				case ShopLootpackOpening.State.None:
					break;
				case ShopLootpackOpening.State.Begin:
					this.panelSideCurrencyTimer = 1f;
					this.period = 1.6f;
					this.notificationBadge.gameObject.SetActive(false);
					this.notificationBadge.numNotifications = 0;
					this.chestSpine.skeletonGraphic.SetAlpha(1f);
					break;
				case ShopLootpackOpening.State.OpenChest:
					this.period = 1.3f;
					this.chestSpine.Open();
					break;
				case ShopLootpackOpening.State.LootSimple:
				{
					int i = 0;
					int num = this.lootSimples.Length;
					while (i < num)
					{
						this.lootSimples[i].rectTransform.localScale = Vector3.zero;
						if (this.currencyType == CurrencyType.TOKEN)
						{
							this.lootSimples[i].sprite = this.spriteCurrencyToken;
						}
						else if (this.currencyType == CurrencyType.SCRAP)
						{
							this.lootSimples[i].sprite = this.spriteCurrencyScrap;
						}
						else
						{
							if (this.currencyType != CurrencyType.GEM)
							{
								throw new NotImplementedException();
							}
							this.lootSimples[i].sprite = this.spriteCurrencyCredit;
						}
						i++;
					}
					this.period = this.periodLootSimple;
					this.chestSpine.NewItemAppears(this.numItemsLeft);
					this.numItemsLeft--;
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackCurrency, 1f));
					break;
				}
				case ShopLootpackOpening.State.LootSimpleCount:
					this.textLootSimpleCount.rectTransform.anchoredPosition = this.lootSimpleTextPos;
					this.period = this.periodLootSimpleCount;
					break;
				case ShopLootpackOpening.State.RuneBegin:
				{
					this.period = this.periodRuneBegin;
					Rune rune = this.runesEarned[this.runeIndex];
					this.imageRune.sprite = ((!this.spritesRune.ContainsKey(rune.id)) ? null : this.spritesRune[rune.id]);
					this.imageTotem.sprite = ((!this.spritesTotem.ContainsKey(rune.belongsTo.id)) ? null : this.spritesTotem[rune.belongsTo.id]);
					this.textRune.text = LM.Get(rune.nameKey);
					this.textTotem.text = rune.belongsTo.nameKey;
					this.chestSpine.NewItemAppears(this.numItemsLeft);
					this.numItemsLeft--;
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackRune, 1f));
					break;
				}
				case ShopLootpackOpening.State.RuneFadeOut:
					this.period = this.periodRuneFadeOut;
					break;
				case ShopLootpackOpening.State.GearBegin:
				{
					Gear gear = this.lootGears[this.gearIndex];
					string id = gear.data.belongsTo.id;
					string id2 = gear.GetId();
					this.imageGearBg.sprite = this.spritesGearLevel[gear.level];
					this.imageGearHero.sprite = ((!this.spritesHeroCircle.ContainsKey(id)) ? null : this.spritesHeroCircle[id]);
					this.imageGearHero.SetNativeSize();
					this.imageGear.sprite = ((!this.spritesGearIcon.ContainsKey(id2)) ? null : this.spritesGearIcon[id2]);
					this.textGearName.text = LM.Get(gear.data.nameKey);
					this.textGearBonus.text = gear.UniversalBonusString();
					Text text = this.textGearBonus;
					text.text = text.text + "\n" + gear.SkillBonusString();
					this.textGearPercent.text = gear.UniversalBonusPercentString(this.universalBonus, 0);
					Text text2 = this.textGearPercent;
					text2.text = text2.text + "\n" + gear.SkillBonusPercentString(0);
					this.gearInfoParent.gameObject.SetActive(false);
					this.gearEmptyParent.gameObject.SetActive(true);
					this.gearParent.localScale = Vector3.zero;
					this.gearGlowOrnament.alpha = 0f;
					this.period = this.periodGearBegin;
					this.chestSpine.NewItemAppears(this.numItemsLeft);
					this.numItemsLeft--;
					break;
				}
				case ShopLootpackOpening.State.GearOpen:
				{
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackItem[this.lootGears[this.gearIndex].level], 1f));
					this.gearIdForShineEffect = this.UpdateHeroGears(true);
					this.updatedHeroGears = false;
					Color color = new Color(1f, 1f, 1f, 0f);
					foreach (Image image in this.imageHeroGearBgs)
					{
						image.color = color;
					}
					foreach (Image image2 in this.imageHeroGears)
					{
						image2.color = color;
					}
					foreach (Image image3 in this.imageHeroGearFlashes)
					{
						image3.color = color;
					}
					this.period = this.periodGearOpen;
					break;
				}
				case ShopLootpackOpening.State.GearReceive:
					this.period = this.periodGearReceive;
					this.PlayGearReceivedVO(this.lootGears[this.gearIndex].data.belongsTo);
					break;
				case ShopLootpackOpening.State.GearScraps:
				{
					int m = 0;
					int num2 = this.lootSimples.Length;
					while (m < num2)
					{
						this.lootSimples[m].rectTransform.localScale = Vector3.zero;
						this.lootSimples[m].sprite = this.spriteCurrencyScrap;
						m++;
					}
					this.textLootSimpleCount.rectTransform.anchoredPosition = this.lootSimpleTextPos + this.lootSimpleGearDeltaPos;
					this.period = this.periodGearScraps;
					break;
				}
				case ShopLootpackOpening.State.GearFadeOut:
					this.period = this.periodGearFadeOut;
					break;
				case ShopLootpackOpening.State.CurrencyCard:
				{
					this.period = this.periodSmallCardAppear;
					this.smallCard.gameObject.SetActive(true);
					this.menuShowCurrencySmallCard.gameObject.SetActive(true);
					Sprite sprite = null;
					if (sprite != this.imageSmallCardRuneIcon.sprite)
					{
						this.imageSmallCardRuneIcon.sprite = sprite;
						this.imageSmallCardRuneIcon.SetNativeSize();
					}
					if (this.currencyType == CurrencyType.TOKEN)
					{
						sprite = this.spriteRewardCurrencyToken;
						this.textSmallCard.text = LM.Get("UI_TOKENS");
						this.menuShowCurrencySmallCard.SetCurrency(this.currencyType, GameMath.GetDoubleString(this.amountTokens), true, GameMode.STANDARD, true);
					}
					else if (this.currencyType == CurrencyType.SCRAP)
					{
						sprite = this.spriteRewardCurrencyScrap;
						this.textSmallCard.text = LM.Get("UI_SCRAPS");
						this.menuShowCurrencySmallCard.SetCurrency(this.currencyType, GameMath.GetDoubleString(this.amountScraps), true, GameMode.STANDARD, true);
					}
					else
					{
						if (this.currencyType != CurrencyType.GEM)
						{
							throw new NotImplementedException();
						}
						sprite = this.spriteRewardCurrencyCredit;
						this.textSmallCard.text = LM.Get("UI_GEMS");
						this.menuShowCurrencySmallCard.SetCurrency(this.currencyType, GameMath.GetDoubleString(this.amountCredits), true, GameMode.STANDARD, true);
					}
					if (sprite != this.imageSmallCardRuneIcon.sprite)
					{
						this.imageSmallCardRuneIcon.sprite = sprite;
						this.imageSmallCardRuneIcon.SetNativeSize();
						this.imageSmallCardRuneIcon.transform.localScale = new Vector3(1f, 1f, 1f);
					}
					this.textSmallCardDesc.gameObject.SetActive(false);
					this.smallCardEmptyParent.gameObject.SetActive(true);
					this.smallCardInfoParent.gameObject.SetActive(false);
					this.canvasGroupSmallCard.alpha = 1f;
					this.smallCardEmptyGlowOrnament.alpha = 0f;
					this.smallCard.anchoredPosition = this.lootFirstPos;
					this.smallCard.localScale = new Vector3(0f, 0f, 0f);
					this.chestSpine.NewItemAppears(this.numItemsLeft);
					this.numItemsLeft--;
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackCurrency, 1f));
					break;
				}
				case ShopLootpackOpening.State.CurrencyCardFadeOut:
					this.period = this.periodSmallCardFadeOut;
					break;
				case ShopLootpackOpening.State.RuneCard:
				{
					this.period = this.periodSmallCardAppear;
					Rune rune2 = this.runesEarned[this.runeIndex];
					this.smallCard.gameObject.SetActive(true);
					this.imageSmallCardRuneIcon.enabled = true;
					Sprite sprite2 = (!this.spritesRune.ContainsKey(rune2.id)) ? null : this.spritesRune[rune2.id];
					Color color2 = (!this.runeColorForRing.ContainsKey(rune2.belongsTo.id)) ? Color.white : this.runeColorForRing[rune2.belongsTo.id];
					if (sprite2 != this.imageSmallCardRuneIcon.sprite)
					{
						this.imageSmallCardRuneIcon.sprite = sprite2;
						this.imageSmallCardRuneIcon.SetNativeSize();
						this.imageSmallCardRuneIcon.transform.localScale = new Vector3(2f, 2f, 2f);
					}
					this.imageSmallCardRuneIcon.color = color2;
					if (this.trinketUi != null)
					{
						UnityEngine.Object.Destroy(this.trinketUi.gameObject);
					}
					this.menuShowCurrencySmallCard.gameObject.SetActive(false);
					this.textSmallCard.text = LM.Get(rune2.nameKey);
					this.textSmallCardDesc.gameObject.SetActive(true);
					this.textSmallCardDesc.text = rune2.GetDesc();
					this.smallCardEmptyParent.gameObject.SetActive(true);
					this.smallCardInfoParent.gameObject.SetActive(false);
					this.canvasGroupSmallCard.alpha = 1f;
					this.smallCardEmptyGlowOrnament.alpha = 0f;
					this.smallCard.anchoredPosition = this.lootFirstPos;
					this.smallCard.localScale = new Vector3(0f, 0f, 0f);
					this.chestSpine.NewItemAppears(this.numItemsLeft);
					this.numItemsLeft--;
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackRune, 1f));
					break;
				}
				case ShopLootpackOpening.State.RuneCardFadeOut:
					this.period = this.periodSmallCardFadeOut;
					break;
				case ShopLootpackOpening.State.TrinketCard:
				{
					this.period = this.periodSmallCardAppear;
					Trinket trinket = this.trinketsEarned[this.trinketIndex];
					this.trinketCard.gameObject.SetActive(true);
					this.imageTrinketCardTrinketBg.gameObject.SetActive(true);
					this.imageTrinketCardTrinketIcon.enabled = false;
					if (this.trinketUi == null)
					{
						this.trinketUi = UnityEngine.Object.Instantiate<GameObject>(UiData.inst.prefabTrinket, this.imageTrinketCardTrinketIcon.transform).GetComponent<TrinketUi>();
						this.trinketUi.Register();
						this.trinketUi.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
					}
					this.trinketUi.Init(trinket);
					this.trinketUi.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
					this.textTrinketCard.text = LM.Get("UI_NEW_TRINKET");
					this.lootpackTrinketCard.SetTrinkets(trinket);
					this.trinketCardEmptyParent.gameObject.SetActive(true);
					this.trinketCardInfoParent.gameObject.SetActive(false);
					this.canvasGroupTrinketCard.alpha = 1f;
					this.trinketCardEmptyGlowOrnament.alpha = 0f;
					this.trinketCard.anchoredPosition = this.lootFirstPos;
					this.trinketCard.localScale = new Vector3(0f, 0f, 0f);
					this.chestSpine.NewItemAppears(this.numItemsLeft);
					this.numItemsLeft--;
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackRune, 1f));
					break;
				}
				case ShopLootpackOpening.State.TrinketCardFadeOut:
					this.period = this.periodSmallCardFadeOut;
					break;
				default:
					throw new NotImplementedException();
				}
				this._state = value;
			}
		}

		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.gameButton.onDown = delegate()
			{
				this.OnClicked();
			};
			this.skipAnimationButton.onClick = new GameButton.VoidFunc(this.OnSkipAnimationButtonClicked);
			this.lootFirstPos = this.chestTransform.anchoredPosition;
			this.lootSimpleGoalPos = new List<Vector2>();
			this.lootSimpleGoalRot = new List<float>();
			this.lootSimpleGoalScale = new List<float>();
			int i = 0;
			int num = this.lootSimples.Length;
			while (i < num)
			{
				this.lootSimpleGoalPos.Add(this.lootSimples[i].rectTransform.anchoredPosition);
				this.lootSimpleGoalRot.Add(this.lootSimples[i].rectTransform.eulerAngles.z);
				this.lootSimpleGoalScale.Add(this.lootSimples[i].rectTransform.localScale.x);
				i++;
			}
			this.gearGoalPos = this.gearParent.anchoredPosition;
			this.runeGoalPos = this.imageRune.rectTransform.anchoredPosition;
			this.gearGoalScale = this.gearParent.localScale.x;
			this.heroGearScale = this.imageHeroGears[0].rectTransform.localScale.x;
			this.lootSimpleTextPos = this.textLootSimpleCount.rectTransform.anchoredPosition;
			this.smallCardFirstPos = this.smallCard.anchoredPosition;
			this.trinketCardFirstPos = this.trinketCard.anchoredPosition;
			this.ResetStuff();
		}

		public void SetLootpack(ShopPack shopPack, int amountLootpacksOpened, bool allowSkipButton = false)
		{
			this.selectedShopPack = shopPack;
			this.chestSpine.SetSkin(shopPack, true);
			this.chestSpine.rectTransform.SetAnchorPosY((float)((!(shopPack is ShopPackTrinket)) ? 180 : 128));
			this.chestSpineSummary.SetSkin(shopPack, false);
			this.chestSpine.Spawn();
			this.chestSpine.transform.localScale = new Vector3(0f, 0f, 0f);
			this.skipAnimationButton.gameObject.SetActive(allowSkipButton && amountLootpacksOpened >= 3);
			this.canvasGroup.alpha = 0f;
			this.finished = false;
			this.resultsReceived = false;
			int i = 0;
			int num = this.lootSimples.Length;
			while (i < num)
			{
				this.lootSimples[i].rectTransform.localScale = Vector3.zero;
				i++;
			}
			this.textLootSimpleCount.color = new Color(1f, 1f, 1f, 0f);
			this.gearIndex = 0;
			this.runeIndex = 0;
			this.trinketIndex = 0;
			this.ResetStuff();
			this.state = ShopLootpackOpening.State.Begin;
			this.subState = ShopLootpackOpening.SubState.None;
		}

		public void ResetStuff()
		{
			this.gearParent.localScale = Vector3.zero;
			Color color = new Color(1f, 1f, 1f, 0f);
			foreach (Image image in this.imageHeroGearBgs)
			{
				image.color = color;
			}
			foreach (Image image2 in this.imageHeroGears)
			{
				image2.color = color;
			}
			foreach (Image image3 in this.imageHeroGearFlashes)
			{
				image3.color = color;
			}
			this.gearCanvasGroup.alpha = 0f;
			this.imageRune.color = new Color(this.imageRune.color.r, this.imageRune.color.g, this.imageRune.color.b, 0f);
			this.textRune.color = new Color(this.textRune.color.r, this.textRune.color.g, this.textRune.color.b, 0f);
			this.textTotem.color = new Color(this.textTotem.color.r, this.textTotem.color.g, this.textTotem.color.b, 0f);
			this.imageTotem.color = new Color(this.imageTotem.color.r, this.imageTotem.color.g, this.imageTotem.color.b, 0f);
			this.smallCard.gameObject.SetActive(false);
			this.trinketCard.gameObject.SetActive(false);
		}

		public override void AahUpdate(float dt)
		{
			switch (this.subState)
			{
			case ShopLootpackOpening.SubState.None:
				if (this.animatingSub)
				{
					this.timerSub += dt;
					if (this.timerSub > this.periodSub || this.clicked)
					{
						this.animatingSub = false;
					}
				}
				else if (this.clicked)
				{
					this.subState = ShopLootpackOpening.SubState.None;
				}
				break;
			case ShopLootpackOpening.SubState.LootSimpleSlide:
				if (this.animatingSub)
				{
					this.timerSub += dt;
					if (this.timerSub > this.periodSub || this.clicked)
					{
						this.animatingSub = false;
					}
					else
					{
						float a = Easing.Linear(this.timerSub, 0f, 1f, this.periodSub);
						float d = Easing.SineEaseOut(this.timerSub, 0f, 1f, this.periodSub);
						int i = 0;
						int num = this.lootSimples.Length;
						while (i < num)
						{
							float num2 = Easing.BackEaseOut(Mathf.Min(this.periodSub, this.timerSub + (float)i * 0.1f), 0f, 1f, this.periodSub);
							this.lootSimples[i].rectTransform.anchoredPosition = this.lootSimpleGoalPos[i] + d * this.lootSimpleCountDeltaPos;
							this.lootSimples[i].rectTransform.eulerAngles = Vector3.forward * (this.lootSimpleGoalRot[i] + num2 * 5f * (float)(1 + i));
							this.lootSimples[i].rectTransform.localScale = Vector3.one * this.lootSimpleGoalScale[i];
							i++;
						}
						Color color = UiManager.colorCurrencyTypes[(int)this.currencyType];
						color.a = a;
						this.textLootSimpleCount.color = color;
					}
				}
				else
				{
					int j = 0;
					int num3 = this.lootSimples.Length;
					while (j < num3)
					{
						this.lootSimples[j].rectTransform.anchoredPosition = this.lootSimpleGoalPos[j] + this.lootSimpleCountDeltaPos;
						this.lootSimples[j].rectTransform.eulerAngles = Vector3.forward * (this.lootSimpleGoalRot[j] + 5f * (float)(1 + j));
						this.lootSimples[j].rectTransform.localScale = Vector3.one * this.lootSimpleGoalScale[j];
						j++;
					}
					Color color2 = UiManager.colorCurrencyTypes[(int)this.currencyType];
					this.textLootSimpleCount.color = color2;
					if (this.clicked)
					{
						this.subState = ShopLootpackOpening.SubState.None;
					}
				}
				break;
			case ShopLootpackOpening.SubState.LootSimpleFadeOut:
				if (this.animatingSub)
				{
					this.timerSub += dt;
					if (this.timerSub > this.periodSub || this.clicked)
					{
						this.animatingSub = false;
					}
					else
					{
						float num4 = Easing.Linear(this.timerSub, 0f, 1f, this.periodSub);
						int k = 0;
						int num5 = this.lootSimpleSubs.Length;
						while (k < num5)
						{
							this.lootSimpleSubs[k].color = new Color(1f, 1f, 1f, 1f - num4);
							Vector2 vector = this.lootSimpleGoalPos[k] + this.lootSimpleCountDeltaPos;
							this.lootSimpleSubs[k].rectTransform.anchoredPosition = vector + num4 * (this.lootSimpleFadeOutPos - vector);
							this.lootSimpleSubs[k].rectTransform.localScale = Vector3.one * this.lootSimpleGoalScale[k] * (1f - num4);
							k++;
						}
						this.textLootSimpleCount.color = new Color(this.textLootSimpleCount.color.r, this.textLootSimpleCount.color.g, this.textLootSimpleCount.color.b, 1f - num4);
					}
				}
				else
				{
					int l = 0;
					int num6 = this.lootSimpleSubs.Length;
					while (l < num6)
					{
						this.lootSimpleSubs[l].color = new Color(1f, 1f, 1f, 0f);
						this.lootSimpleSubs[l].rectTransform.anchoredPosition = this.lootSimpleFadeOutPos;
						this.lootSimpleSubs[l].rectTransform.localScale = Vector3.zero;
						l++;
					}
					Color color3 = UiManager.colorCurrencyTypes[(int)this.currencyType];
					color3.a = 0f;
					this.textLootSimpleCount.color = color3;
					if (this.clicked)
					{
						this.subState = ShopLootpackOpening.SubState.None;
					}
				}
				break;
			case ShopLootpackOpening.SubState.LootSimpleGearFadeOut:
				if (this.animatingSub)
				{
					this.timerSub += dt;
					if (this.timerSub > this.periodSub || this.clicked)
					{
						this.animatingSub = false;
					}
					else
					{
						float num7 = Easing.Linear(this.timerSub, 0f, 1f, this.periodSub);
						int m = 0;
						int num8 = this.lootSimpleSubs.Length;
						while (m < num8)
						{
							this.lootSimpleSubs[m].color = new Color(1f, 1f, 1f, 1f - num7);
							Vector2 vector2 = this.lootSimpleGoalPos[m] + this.lootSimpleGearDeltaPos;
							this.lootSimpleSubs[m].rectTransform.anchoredPosition = vector2 + num7 * (this.lootSimpleFadeOutPos - vector2);
							this.lootSimpleSubs[m].rectTransform.localScale = Vector3.one * this.lootSimpleGoalScale[m] * (1f - num7);
							m++;
						}
						this.textLootSimpleCount.color = new Color(this.textLootSimpleCount.color.r, this.textLootSimpleCount.color.g, this.textLootSimpleCount.color.b, 1f - num7);
					}
				}
				else
				{
					int n = 0;
					int num9 = this.lootSimpleSubs.Length;
					while (n < num9)
					{
						this.lootSimpleSubs[n].color = new Color(1f, 1f, 1f, 0f);
						this.lootSimpleSubs[n].rectTransform.anchoredPosition = this.lootSimpleFadeOutPos;
						this.lootSimpleSubs[n].rectTransform.localScale = Vector3.zero;
						n++;
					}
					Color color4 = UiManager.colorCurrencyTypes[(int)this.currencyType];
					color4.a = 0f;
					this.textLootSimpleCount.color = color4;
					if (this.clicked)
					{
						this.subState = ShopLootpackOpening.SubState.None;
					}
				}
				break;
			}
			switch (this.state)
			{
			case ShopLootpackOpening.State.None:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer > this.period || this.clicked)
					{
						this.animating = false;
					}
				}
				else if (this.clicked)
				{
					this.state = ShopLootpackOpening.State.Begin;
				}
				break;
			case ShopLootpackOpening.State.Begin:
			{
				Vector3 chestScale = this.GetChestScale(this.selectedShopPack);
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer > this.period || this.clicked)
					{
						this.animating = false;
						if (this.clicked)
						{
							this.notificationBadge.gameObject.SetActive(true);
							this.notificationBadge.numNotifications = this.numItemsLeft;
						}
					}
					else
					{
						float num10 = Easing.Linear(this.timer, 0f, 1f, this.period);
						if (num10 > 0.2f && this.chestSpine.transform.localScale.x == 0f)
						{
							this.chestSpine.transform.localScale = chestScale;
							this.chestSpine.Spawn();
						}
						this.canvasGroup.alpha = num10 * 4f;
					}
					if (this.numItemsLeft > 1 && this.timer >= 1.2f)
					{
						this.notificationBadge.gameObject.SetActive(true);
						this.notificationBadge.numNotifications = this.numItemsLeft;
					}
				}
				else
				{
					this.canvasGroup.alpha = 1f;
					if (this.chestSpine.transform.localScale.x == 0f)
					{
						this.chestSpine.transform.localScale = chestScale;
						this.chestSpine.Spawn();
					}
					if (this.onCompledeFadeIn != null)
					{
						this.onCompledeFadeIn();
					}
					this.state = ShopLootpackOpening.State.OpenChest;
				}
				break;
			}
			case ShopLootpackOpening.State.OpenChest:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer > this.period || this.clicked)
					{
						this.animating = false;
					}
				}
				else if (this.creditsWillDrop)
				{
					this.currencyType = CurrencyType.GEM;
					this.creditsWillDrop = false;
					this.state = ShopLootpackOpening.State.CurrencyCard;
				}
				else if (this.tokensWillDrop)
				{
					this.currencyType = CurrencyType.TOKEN;
					this.tokensWillDrop = false;
					this.state = ShopLootpackOpening.State.CurrencyCard;
				}
				else if (this.scrapsWillDrop)
				{
					this.currencyType = CurrencyType.SCRAP;
					this.scrapsWillDrop = false;
					this.state = ShopLootpackOpening.State.CurrencyCard;
				}
				else if (this.runesEarned.Count > this.runeIndex)
				{
					this.state = ShopLootpackOpening.State.RuneCard;
				}
				else if (this.trinketsEarned.Count > this.trinketIndex)
				{
					this.state = ShopLootpackOpening.State.TrinketCard;
				}
				else if (this.gearIndex < this.lootGears.Count)
				{
					this.state = ShopLootpackOpening.State.GearBegin;
				}
				else
				{
					this.Finish();
				}
				break;
			case ShopLootpackOpening.State.LootSimple:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer > this.period || this.clicked)
					{
						this.animating = false;
					}
					else
					{
						float num11 = Easing.SineEaseOut(this.timer, 0f, 1f, this.period);
						int num12 = 0;
						int num13 = this.lootSimples.Length;
						while (num12 < num13)
						{
							float d2 = Easing.BounceEaseOut(Mathf.Min(this.period, this.timer + (float)num12 * 0.1f), 0f, 1f, this.period);
							this.lootSimples[num12].rectTransform.anchoredPosition = this.lootFirstPos + d2 * (this.lootSimpleGoalPos[num12] - this.lootFirstPos);
							this.lootSimples[num12].rectTransform.eulerAngles = Vector3.forward * (this.lootSimpleGoalRot[num12] - 70f + 70f * num11);
							this.lootSimples[num12].rectTransform.localScale = Vector3.one * d2 * this.lootSimpleGoalScale[num12];
							num12++;
						}
					}
				}
				else
				{
					int num14 = 0;
					int num15 = this.lootSimples.Length;
					while (num14 < num15)
					{
						this.lootSimples[num14].rectTransform.anchoredPosition = this.lootSimpleGoalPos[num14];
						this.lootSimples[num14].rectTransform.eulerAngles = Vector3.forward * this.lootSimpleGoalRot[num14];
						this.lootSimples[num14].rectTransform.localScale = Vector3.one * this.lootSimpleGoalScale[num14];
						num14++;
					}
					this.subState = ShopLootpackOpening.SubState.LootSimpleSlide;
					this.state = ShopLootpackOpening.State.LootSimpleCount;
				}
				break;
			case ShopLootpackOpening.State.LootSimpleCount:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer > this.period || this.clicked)
					{
						this.animating = false;
						if (this.currencyType == CurrencyType.GEM)
						{
							this.StartPanelSideCurrencyAnim(this.currencyType, this.totalCreditsBeforeBeginning, this.amountCredits);
							this.totalCreditsBeforeBeginning += this.amountCredits;
						}
						else if (this.currencyType == CurrencyType.TOKEN)
						{
							this.StartPanelSideCurrencyAnim(this.currencyType, this.totalTokensBeforeBeginning, this.amountTokens);
							this.totalTokensBeforeBeginning += this.amountTokens;
						}
						else
						{
							if (this.currencyType != CurrencyType.SCRAP)
							{
								throw new NotImplementedException();
							}
							this.StartPanelSideCurrencyAnim(this.currencyType, this.totalScrapsBeforeBeginning, this.amountScraps);
							this.totalScrapsBeforeBeginning += this.amountScraps;
						}
					}
					else
					{
						float num16 = Easing.CubicEaseOut(this.timer, 0f, 1f, this.period);
						double num17;
						if (this.currencyType == CurrencyType.TOKEN)
						{
							num17 = this.amountTokens;
						}
						else
						{
							if (this.currencyType != CurrencyType.SCRAP)
							{
								throw new NotImplementedException();
							}
							num17 = this.amountScraps;
						}
						this.textLootSimpleCount.text = GameMath.GetDoubleString(Math.Round(num17 * (double)num16));
					}
				}
				else
				{
					double x;
					if (this.currencyType == CurrencyType.TOKEN)
					{
						x = this.amountTokens;
					}
					else
					{
						if (this.currencyType != CurrencyType.SCRAP)
						{
							throw new NotImplementedException();
						}
						x = this.amountScraps;
					}
					this.textLootSimpleCount.text = GameMath.GetDoubleString(x);
					if (this.clicked)
					{
						this.subState = ShopLootpackOpening.SubState.LootSimpleFadeOut;
						if (this.tokensWillDrop)
						{
							this.currencyType = CurrencyType.TOKEN;
							this.tokensWillDrop = false;
							this.state = ShopLootpackOpening.State.LootSimple;
						}
						else if (this.scrapsWillDrop)
						{
							this.currencyType = CurrencyType.SCRAP;
							this.scrapsWillDrop = false;
							this.state = ShopLootpackOpening.State.LootSimple;
						}
						else if (this.runesEarned.Count > this.runeIndex)
						{
							this.state = ShopLootpackOpening.State.RuneBegin;
						}
						else if (this.trinketsEarned.Count > this.trinketIndex)
						{
							this.state = ShopLootpackOpening.State.TrinketCard;
						}
						else if (this.gearIndex < this.lootGears.Count)
						{
							this.state = ShopLootpackOpening.State.GearBegin;
						}
						else
						{
							this.Finish();
						}
					}
				}
				break;
			case ShopLootpackOpening.State.RuneBegin:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer > this.period || this.clicked)
					{
						this.animating = false;
						this.imageRune.rectTransform.anchoredPosition = this.runeGoalPos;
						this.imageRune.rectTransform.localScale = Vector3.one;
						this.imageRune.color = new Color(this.imageRune.color.r, this.imageRune.color.g, this.imageRune.color.b, 1f);
						this.textRune.color = new Color(this.textRune.color.r, this.textRune.color.g, this.textRune.color.b, 1f);
						this.textTotem.color = new Color(this.textTotem.color.r, this.textTotem.color.g, this.textTotem.color.b, 1f);
						this.imageTotem.color = new Color(this.imageTotem.color.r, this.imageTotem.color.g, this.imageTotem.color.b, 1f);
					}
					else
					{
						float a2 = Easing.Linear(this.timer, 0f, 1f, this.period);
						float d3 = Easing.CircEaseOut(this.timer, 0f, 1f, this.period);
						float num18 = Easing.BackEaseOut(this.timer, 0f, 1f, this.period);
						this.imageRune.rectTransform.anchoredPosition = this.lootFirstPos + (this.runeGoalPos - this.lootFirstPos) * d3;
						this.imageRune.rectTransform.localScale = Vector3.one * (0.3f + 0.7f * num18);
						this.imageRune.color = new Color(this.imageRune.color.r, this.imageRune.color.g, this.imageRune.color.b, a2);
						this.textRune.color = new Color(this.textRune.color.r, this.textRune.color.g, this.textRune.color.b, a2);
						this.textTotem.color = new Color(this.textTotem.color.r, this.textTotem.color.g, this.textTotem.color.b, a2);
						this.imageTotem.color = new Color(this.imageTotem.color.r, this.imageTotem.color.g, this.imageTotem.color.b, a2);
					}
				}
				else if (this.clicked)
				{
					this.state = ShopLootpackOpening.State.RuneFadeOut;
				}
				break;
			case ShopLootpackOpening.State.RuneFadeOut:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer > this.period || this.clicked)
					{
						this.animating = false;
						this.imageRune.rectTransform.anchoredPosition = this.runeFadeOutPos;
						this.imageRune.color = new Color(this.imageRune.color.r, this.imageRune.color.g, this.imageRune.color.b, 0f);
						this.textRune.color = new Color(this.textRune.color.r, this.textRune.color.g, this.textRune.color.b, 0f);
						this.textTotem.color = new Color(this.textTotem.color.r, this.textTotem.color.g, this.textTotem.color.b, 0f);
						this.imageTotem.color = new Color(this.imageTotem.color.r, this.imageTotem.color.g, this.imageTotem.color.b, 0f);
					}
					else
					{
						float a3 = Easing.Linear(this.timer, 1f, -1f, this.period);
						float d4 = Easing.CircEaseIn(this.timer, 0f, 1f, this.period);
						this.imageRune.rectTransform.anchoredPosition = this.runeGoalPos + (this.runeFadeOutPos - this.runeGoalPos) * d4;
						this.imageRune.color = new Color(this.imageRune.color.r, this.imageRune.color.g, this.imageRune.color.b, a3);
						this.textRune.color = new Color(this.textRune.color.r, this.textRune.color.g, this.textRune.color.b, a3);
						this.textTotem.color = new Color(this.textTotem.color.r, this.textTotem.color.g, this.textTotem.color.b, a3);
						this.imageTotem.color = new Color(this.imageTotem.color.r, this.imageTotem.color.g, this.imageTotem.color.b, a3);
					}
				}
				else
				{
					this.runeIndex++;
					if (this.runesEarned.Count > this.runeIndex)
					{
						this.state = ShopLootpackOpening.State.RuneBegin;
					}
					else if (this.trinketsEarned.Count > this.trinketIndex)
					{
						this.state = ShopLootpackOpening.State.TrinketCard;
					}
					else if (this.gearIndex < this.lootGears.Count)
					{
						this.state = ShopLootpackOpening.State.GearBegin;
					}
					else
					{
						this.Finish();
					}
				}
				break;
			case ShopLootpackOpening.State.GearBegin:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer > this.period || this.clicked)
					{
						this.animating = false;
					}
					else
					{
						float num19 = Easing.SineEaseOut(this.timer, 0f, 1f, this.period);
						this.gearParent.anchoredPosition = this.lootFirstPos + num19 * (this.gearGoalPos - this.lootFirstPos);
						this.gearParent.localScale = new Vector3(num19, num19, 1f) * this.gearGoalScale;
						this.gearCanvasGroup.alpha = 1f;
					}
				}
				else
				{
					this.gearParent.anchoredPosition = this.gearGoalPos;
					this.gearParent.localScale = Vector3.one * this.gearGoalScale;
					this.gearCanvasGroup.alpha = 1f;
					this.state = ShopLootpackOpening.State.GearOpen;
				}
				break;
			case ShopLootpackOpening.State.GearOpen:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer > this.period || this.clicked)
					{
						this.animating = false;
					}
					else
					{
						float num20 = Easing.Linear(this.timer, 0f, 1f, this.period);
						this.gearParent.localScale = Vector3.one * this.gearGoalScale;
						Color color5 = new Color(1f, 1f, 1f, num20);
						foreach (Image image in this.imageHeroGearBgs)
						{
							image.color = color5;
						}
						foreach (Image image2 in this.imageHeroGears)
						{
							image2.color = color5;
						}
						float num23 = (this.timer - 0.3f) / (this.period * 0.7f);
						float alpha = (num20 <= 0.65f) ? (num20 * 6f) : (1f - 6f * (num20 - 0.65f));
						float t = (num20 <= 0.3f) ? 0f : num23;
						this.gearGlowOrnament.alpha = alpha;
						float num24 = Easing.CircEaseInOut(t, 0f, 1f, 1f);
						if (num24 < 0.5f)
						{
							this.gearParent.transform.localEulerAngles = new Vector3(0f, -180f * num24, 0f);
						}
						else
						{
							this.gearParent.transform.localEulerAngles = new Vector3(0f, 180f - 180f * num24, 0f);
							this.gearInfoParent.gameObject.SetActive(true);
							this.gearEmptyParent.gameObject.SetActive(false);
						}
					}
				}
				else
				{
					this.gearParent.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					this.gearParent.localScale = Vector3.one * this.gearGoalScale;
					this.gearInfoParent.gameObject.SetActive(true);
					this.gearEmptyParent.gameObject.SetActive(false);
					foreach (Image image3 in this.imageHeroGearBgs)
					{
						image3.color = Color.white;
					}
					foreach (Image image4 in this.imageHeroGears)
					{
						image4.color = Color.white;
					}
					this.gearGlowOrnament.alpha = 1f;
					if (this.clicked)
					{
						this.gearParent.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
						this.gearInfoParent.gameObject.SetActive(true);
						this.gearEmptyParent.gameObject.SetActive(false);
						if (this.lootGearChanges[this.gearIndex].scraps != 0.0)
						{
							this.state = ShopLootpackOpening.State.GearScraps;
						}
						else
						{
							this.state = ShopLootpackOpening.State.GearReceive;
						}
					}
				}
				break;
			case ShopLootpackOpening.State.GearReceive:
				if (this.animating)
				{
					this.gearParent.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					this.timer += dt;
					if (this.timer > this.period || this.clicked)
					{
						this.animating = false;
					}
					else
					{
						float num27 = Easing.Linear(this.timer, 0f, 1f, this.period);
						float a4 = (num27 >= 0.5f) ? (2f * (1f - num27)) : (num27 * 2f);
						if (num27 >= 0.5f && !this.updatedHeroGears)
						{
							this.updatedHeroGears = true;
							this.UpdateHeroGears(false);
						}
						this.imageHeroGearFlashes[this.gearIdForShineEffect].color = new Color(1f, 1f, 1f, a4);
					}
				}
				else
				{
					this.gearParent.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					if (!this.updatedHeroGears)
					{
						this.updatedHeroGears = true;
						this.UpdateHeroGears(false);
					}
					this.imageHeroGearFlashes[this.gearIdForShineEffect].color = new Color(1f, 1f, 1f, 0f);
					if (this.clicked)
					{
						this.state = ShopLootpackOpening.State.GearFadeOut;
					}
				}
				break;
			case ShopLootpackOpening.State.GearScraps:
				if (this.animating)
				{
					this.gearParent.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					this.timer += dt;
					if (this.timer > this.period || this.clicked)
					{
						this.animating = false;
						this.StartPanelSideCurrencyAnim(CurrencyType.SCRAP, this.totalScrapsBeforeBeginning, this.lootGearChanges[this.gearIndex].scraps);
						this.totalScrapsBeforeBeginning += this.lootGearChanges[this.gearIndex].scraps;
						int num28 = 0;
						int num29 = this.lootSimples.Length;
						while (num28 < num29)
						{
							this.lootSimples[num28].rectTransform.anchoredPosition = this.lootSimpleGoalPos[num28] + this.lootSimpleGearDeltaPos;
							this.lootSimples[num28].rectTransform.eulerAngles = Vector3.forward * (this.lootSimpleGoalRot[num28] + 20f * (float)(1 + num28));
							this.lootSimples[num28].rectTransform.localScale = Vector3.one * this.lootSimpleGoalScale[num28];
							num28++;
						}
						Color color6 = UiManager.colorCurrencyTypes[1];
						this.textLootSimpleCount.color = color6;
						this.textLootSimpleCount.text = GameMath.GetDoubleString(this.lootGearChanges[this.gearIndex].scraps);
						this.gearCanvasGroup.alpha = 0f;
						Color color7 = new Color(1f, 1f, 1f, 0f);
						foreach (Image image5 in this.imageHeroGearBgs)
						{
							image5.color = color7;
						}
						foreach (Image image6 in this.imageHeroGears)
						{
							image6.color = color7;
						}
					}
					else
					{
						float num32 = Easing.CubicEaseOut(this.timer, 0f, 1f, this.period);
						float num33 = Easing.Linear(this.timer, 0f, 1f, this.period);
						int num34 = 0;
						int num35 = this.lootSimples.Length;
						while (num34 < num35)
						{
							float num36 = Easing.BackEaseOut(Mathf.Min(this.period, this.timer + (float)num34 * 0.1f), 0f, 1f, this.period);
							float d5 = Easing.BounceEaseOut(Mathf.Min(this.period, this.timer + (float)num34 * 0.1f), 0f, 1f, this.period);
							this.lootSimples[num34].rectTransform.anchoredPosition = this.lootSimpleGoalPos[num34] + this.lootSimpleGearDeltaPos;
							this.lootSimples[num34].rectTransform.eulerAngles = Vector3.forward * (this.lootSimpleGoalRot[num34] + num36 * 20f * (float)(1 + num34));
							this.lootSimples[num34].rectTransform.localScale = Vector3.one * d5 * this.lootSimpleGoalScale[num34];
							num34++;
						}
						Color color8 = UiManager.colorCurrencyTypes[1];
						color8.a = num33;
						this.textLootSimpleCount.color = color8;
						this.textLootSimpleCount.text = GameMath.GetDoubleString(Math.Round(this.lootGearChanges[this.gearIndex].scraps * (double)num32));
						Color color9 = new Color(1f, 1f, 1f, 1f - num33);
						foreach (Image image7 in this.imageHeroGearBgs)
						{
							image7.color = color9;
						}
						foreach (Image image8 in this.imageHeroGears)
						{
							image8.color = color9;
						}
					}
				}
				else
				{
					this.gearParent.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					if (this.clicked)
					{
						this.state = ShopLootpackOpening.State.GearFadeOut;
						this.subState = ShopLootpackOpening.SubState.LootSimpleGearFadeOut;
					}
				}
				break;
			case ShopLootpackOpening.State.GearFadeOut:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer > this.period || this.clicked)
					{
						this.animating = false;
						Color color10 = new Color(1f, 1f, 1f, 0f);
						foreach (Image image9 in this.imageHeroGearBgs)
						{
							image9.color = color10;
						}
						foreach (Image image10 in this.imageHeroGears)
						{
							image10.color = color10;
						}
						this.gearCanvasGroup.alpha = 0f;
					}
					else if (this.gearCanvasGroup.alpha > 0f)
					{
						float num41 = Easing.Linear(this.timer, 0f, 1f, this.period);
						this.gearCanvasGroup.alpha = 1f - num41;
						Color color11 = new Color(1f, 1f, 1f, 1f - num41);
						foreach (Image image11 in this.imageHeroGearBgs)
						{
							image11.color = color11;
						}
						foreach (Image image12 in this.imageHeroGears)
						{
							image12.color = color11;
						}
					}
				}
				else
				{
					this.gearIndex++;
					if (this.gearIndex < this.lootGears.Count)
					{
						this.state = ShopLootpackOpening.State.GearBegin;
					}
					else
					{
						this.Finish();
					}
				}
				break;
			case ShopLootpackOpening.State.CurrencyCard:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer > this.period || this.clicked)
					{
						this.animating = false;
						if (this.currencyType == CurrencyType.GEM)
						{
							this.StartPanelSideCurrencyAnim(this.currencyType, this.totalCreditsBeforeBeginning, this.amountCredits);
							this.totalCreditsBeforeBeginning += this.amountCredits;
						}
						else if (this.currencyType == CurrencyType.TOKEN)
						{
							this.StartPanelSideCurrencyAnim(this.currencyType, this.totalTokensBeforeBeginning, this.amountTokens);
							this.totalTokensBeforeBeginning += this.amountTokens;
						}
						else
						{
							if (this.currencyType != CurrencyType.SCRAP)
							{
								throw new NotImplementedException();
							}
							this.StartPanelSideCurrencyAnim(this.currencyType, this.totalScrapsBeforeBeginning, this.amountScraps);
							this.totalScrapsBeforeBeginning += this.amountScraps;
						}
						this.AnimSmallCardAppeared();
					}
					else
					{
						float animRatio = Easing.Linear(this.timer, 0f, 1f, this.period);
						this.AnimSmallCardAppearing(animRatio);
					}
				}
				else if (this.clicked)
				{
					this.state = ShopLootpackOpening.State.CurrencyCardFadeOut;
				}
				break;
			case ShopLootpackOpening.State.CurrencyCardFadeOut:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer > this.period || this.clicked)
					{
						this.animating = false;
						this.canvasGroupSmallCard.alpha = 0f;
					}
					else
					{
						float num44 = Easing.Linear(this.timer, 0f, 1f, this.period);
						this.canvasGroupSmallCard.alpha = 1f - num44;
					}
				}
				else if (this.creditsWillDrop)
				{
					this.currencyType = CurrencyType.GEM;
					this.creditsWillDrop = false;
					this.state = ShopLootpackOpening.State.CurrencyCard;
				}
				else if (this.tokensWillDrop)
				{
					this.currencyType = CurrencyType.TOKEN;
					this.tokensWillDrop = false;
					this.state = ShopLootpackOpening.State.CurrencyCard;
				}
				else if (this.scrapsWillDrop)
				{
					this.currencyType = CurrencyType.SCRAP;
					this.scrapsWillDrop = false;
					this.state = ShopLootpackOpening.State.CurrencyCard;
				}
				else if (this.runesEarned.Count > this.runeIndex)
				{
					this.state = ShopLootpackOpening.State.RuneCard;
				}
				else if (this.trinketsEarned.Count > this.trinketIndex)
				{
					this.state = ShopLootpackOpening.State.TrinketCard;
				}
				else if (this.gearIndex < this.lootGears.Count)
				{
					this.state = ShopLootpackOpening.State.GearBegin;
				}
				else
				{
					this.Finish();
				}
				break;
			case ShopLootpackOpening.State.RuneCard:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer > this.period || this.clicked)
					{
						this.animating = false;
						this.AnimSmallCardAppeared();
					}
					else
					{
						float animRatio2 = Easing.Linear(this.timer, 0f, 1f, this.period);
						this.AnimSmallCardAppearing(animRatio2);
					}
				}
				else if (this.clicked)
				{
					this.state = ShopLootpackOpening.State.RuneCardFadeOut;
				}
				break;
			case ShopLootpackOpening.State.RuneCardFadeOut:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer > this.period || this.clicked)
					{
						this.animating = false;
						this.canvasGroupSmallCard.alpha = 0f;
					}
					else
					{
						float num45 = Easing.Linear(this.timer, 0f, 1f, this.period);
						this.canvasGroupSmallCard.alpha = 1f - num45;
					}
				}
				else
				{
					this.runeIndex++;
					if (this.runesEarned.Count > this.runeIndex)
					{
						this.state = ShopLootpackOpening.State.RuneCard;
					}
					else if (this.trinketsEarned.Count > this.trinketIndex)
					{
						this.state = ShopLootpackOpening.State.TrinketCard;
					}
					else if (this.gearIndex < this.lootGears.Count)
					{
						this.state = ShopLootpackOpening.State.GearBegin;
					}
					else
					{
						this.Finish();
					}
				}
				break;
			case ShopLootpackOpening.State.TrinketCard:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer > this.period || this.clicked)
					{
						this.animating = false;
						this.AnimTrinketCardAppeared();
					}
					else
					{
						float animRatio3 = Easing.Linear(this.timer, 0f, 1f, this.period);
						this.AnimTrinketCardAppearing(animRatio3);
					}
				}
				else if (this.clicked)
				{
					this.state = ShopLootpackOpening.State.TrinketCardFadeOut;
				}
				break;
			case ShopLootpackOpening.State.TrinketCardFadeOut:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer > this.period || this.clicked)
					{
						this.animating = false;
						this.canvasGroupTrinketCard.alpha = 0f;
					}
					else
					{
						float num46 = Easing.Linear(this.timer, 0f, 1f, this.period);
						this.canvasGroupTrinketCard.alpha = 1f - num46;
					}
				}
				else
				{
					this.trinketIndex++;
					if (this.trinketsEarned.Count > this.trinketIndex)
					{
						this.state = ShopLootpackOpening.State.TrinketCard;
					}
					else if (this.gearIndex < this.lootGears.Count)
					{
						this.state = ShopLootpackOpening.State.GearBegin;
					}
					else
					{
						this.Finish();
					}
				}
				break;
			}
			if (this.notificationBadge.gameObject.activeSelf)
			{
				this.notificationBadge.numNotifications = this.numItemsLeft;
			}
			if (this.panelSideCurrencyTimer < 1f)
			{
				this.panelSideCurrencyTimer += dt;
				if (this.panelSideCurrencyTimer >= 1f)
				{
					this.panelCurrencySide.SetCurrency(this.panelSideCurrencyType, GameMath.GetDoubleString(this.panelSideCurrencyAmountStart + this.panelSideCurrencyAmountAdd));
				}
				else if (this.panelSideCurrencyTimer >= 0.5f)
				{
					this.panelCurrencySide.SetCurrency(this.panelSideCurrencyType, GameMath.GetDoubleString(Easing.SineEaseOut(this.panelSideCurrencyTimer - 0.5f, this.panelSideCurrencyAmountStart, this.panelSideCurrencyAmountAdd, 0.5f)));
				}
			}
			if (this.clicked)
			{
				this.clicked = false;
			}
		}

		private void AnimSmallCardAppearing(float animRatio)
		{
			if (animRatio < 0.3f)
			{
				animRatio /= 0.3f;
				float num = Easing.SineEaseOut(animRatio, 0f, 1f, this.period);
				this.smallCard.anchoredPosition = this.lootFirstPos + num * (this.smallCardFirstPos - this.lootFirstPos);
				this.smallCard.localScale = new Vector3(num, num, 1f) * 0.8f;
			}
			else if (animRatio < 0.6f)
			{
				this.smallCard.anchoredPosition = this.smallCardFirstPos;
				animRatio = (animRatio - 0.3f) / 0.7f;
				this.smallCardEmptyGlowOrnament.alpha = animRatio * 3f;
			}
			else
			{
				animRatio = (animRatio - 0.6f) / 0.399999976f;
				this.smallCard.anchoredPosition = this.smallCardFirstPos;
				float num2 = Easing.CircEaseInOut(animRatio * 2f, 0f, 1f, 2f);
				if (num2 < 0.5f)
				{
					this.smallCard.transform.localEulerAngles = new Vector3(0f, -180f * num2, 0f);
					this.smallCard.localScale = new Vector3(0.8f, 0.8f, 0.8f);
				}
				else
				{
					this.smallCard.localScale = new Vector3(0.8f, 0.8f, 0.8f);
					this.smallCard.transform.localEulerAngles = new Vector3(0f, 180f - 180f * num2, 0f);
					this.smallCardEmptyParent.gameObject.SetActive(false);
					this.smallCardInfoParent.gameObject.SetActive(true);
				}
			}
		}

		private void AnimTrinketCardAppearing(float animRatio)
		{
			if (animRatio < 0.3f)
			{
				animRatio /= 0.3f;
				float num = Easing.SineEaseOut(animRatio, 0f, 1f, this.period);
				this.trinketCard.anchoredPosition = this.lootFirstPos + num * (this.trinketCardFirstPos - this.lootFirstPos);
				this.trinketCard.localScale = new Vector3(num, num, 1f) * 0.8f;
			}
			else if (animRatio < 0.6f)
			{
				this.trinketCard.anchoredPosition = this.trinketCardFirstPos;
				animRatio = (animRatio - 0.3f) / 0.7f;
				this.trinketCardEmptyGlowOrnament.alpha = animRatio * 3f;
			}
			else
			{
				animRatio = (animRatio - 0.3f) / 0.7f;
				this.trinketCard.anchoredPosition = this.trinketCardFirstPos;
				this.trinketCard.localScale = new Vector3(1f, 1f, 1f);
				float num2 = Easing.CircEaseInOut(animRatio * 2f, 0f, 1f, 2f);
				if (num2 < 0.5f)
				{
					this.trinketCard.transform.localEulerAngles = new Vector3(0f, -180f * num2, 0f);
					this.trinketCard.localScale = new Vector3(0.8f, 0.8f, 0.8f);
				}
				else
				{
					this.trinketCard.localScale = new Vector3(0.8f, 0.8f, 0.8f);
					this.trinketCard.transform.localEulerAngles = new Vector3(0f, 180f - 180f * num2, 0f);
					this.trinketCardEmptyParent.gameObject.SetActive(false);
					this.trinketCardInfoParent.gameObject.SetActive(true);
				}
			}
		}

		private void AnimSmallCardAppeared()
		{
			this.smallCard.anchoredPosition = this.smallCardFirstPos;
			this.smallCard.localScale = Vector3.one * 0.8f;
			this.canvasGroupSmallCard.alpha = 1f;
			this.smallCard.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			this.smallCardEmptyParent.gameObject.SetActive(false);
			this.smallCardInfoParent.gameObject.SetActive(true);
		}

		private void AnimTrinketCardAppeared()
		{
			this.trinketCard.anchoredPosition = this.trinketCardFirstPos;
			this.trinketCard.localScale = Vector3.one * 0.8f;
			this.canvasGroupTrinketCard.alpha = 1f;
			this.trinketCard.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			this.trinketCardEmptyParent.gameObject.SetActive(false);
			this.trinketCardInfoParent.gameObject.SetActive(true);
		}

		private void OnClicked()
		{
			this.clicked = true;
		}

		private int UpdateHeroGears(bool addToGears)
		{
			HeroDataBase belongsTo = this.lootGears[this.gearIndex].data.belongsTo;
			List<Gear> list = this.allHeroesBoughtGears[belongsTo.GetId()];
			int num = -1;
			int i = 0;
			int num2 = this.imageHeroGearBgs.Length;
			while (i < num2)
			{
				Gear gear = (list.Count <= i) ? null : list[i];
				if (gear == null)
				{
					if (num == -1)
					{
						num = i;
					}
					this.imageHeroGears[i].rectTransform.localScale = Vector3.zero;
					this.imageHeroGearBgs[i].sprite = this.spritesGearLevelSmall[0];
				}
				else
				{
					if (num == -1 && gear.GetId() == this.lootGears[this.gearIndex].GetId())
					{
						num = i;
					}
					this.imageHeroGears[i].rectTransform.localScale = Vector3.one * this.heroGearScale;
					this.imageHeroGearBgs[i].sprite = this.spritesGearLevelSmall[gear.level];
					this.imageHeroGears[i].sprite = ((!this.spritesGearIcon.ContainsKey(gear.GetId())) ? null : this.spritesGearIcon[gear.GetId()]);
				}
				i++;
			}
			if (addToGears && this.lootGearChanges[this.gearIndex].scraps == 0.0)
			{
				if (this.lootGearChanges[this.gearIndex].levelOld == -1)
				{
					num = list.Count;
					list.Add(new Gear(this.lootGearChanges[this.gearIndex].data, this.lootGearChanges[this.gearIndex].level));
				}
				else
				{
					list[num].level = this.lootGearChanges[this.gearIndex].level;
				}
			}
			return num;
		}

		private Vector3 GetChestScale(ShopPack pack)
		{
			if (pack is ShopPackLootpackFree)
			{
				return Vector3.one;
			}
			if (pack is ShopPackLootpackRare)
			{
				return Vector3.one * 0.8f;
			}
			if (pack is ShopPackLootpackEpic)
			{
				return Vector3.one * 0.8f;
			}
			if (pack is ShopPackCurrency || pack is ShopPackRune || pack is ShopPackStarter || pack is ShopPackToken)
			{
				return Vector3.one * 0.8f;
			}
			if (pack is ShopPackTrinket)
			{
				return Vector3.one;
			}
			return Vector3.one;
		}

		private void PlayGearReceivedVO(HeroDataBase hero)
		{
			UiManager.sounds.Add(new SoundEventUiVariedVoiceSimple(hero.soundVoItem, hero.id, 1f));
		}

		private void StartPanelSideCurrencyAnim(CurrencyType currency, double amountStart, double amountAdd)
		{
			this.panelCurrencySide.SetCurrency(currency, GameMath.GetDoubleString(amountStart));
			this.panelSideCurrencyType = currency;
			this.panelSideCurrencyAmountStart = amountStart;
			this.panelSideCurrencyAmountAdd = amountAdd;
			this.panelSideCurrencyTimer = 0f;
		}

		private void Finish()
		{
			this.finished = true;
			this.chestSpineSummary.NewItemAppears(0);
			this.chestSpineSummary.skeletonGraphic.color = Color.white;
			if (this.panelSideCurrencyTimer < 1f)
			{
				this.panelSideCurrencyTimer = 1f - Time.deltaTime * 0.5f;
			}
		}

		private void OnSkipAnimationButtonClicked()
		{
			this.Finish();
			UiManager.AddUiSound(SoundArchieve.inst.uiTabSwitch);
		}

		public const int CHESTS_OPENED_AMOUNT_TO_ALLOW_TO_SKIP_ANIMATION = 3;

		public float periodChestFirst;

		public float periodOpenChest;

		public float periodLootSimple;

		public float periodLootSimpleCount;

		public Vector2 lootSimpleCountDeltaPos;

		private Vector2 lootSimpleTextPos;

		public Vector2 lootSimpleGearDeltaPos;

		public Vector2 lootSimpleFadeOutPos;

		public Vector2 runeFadeOutPos;

		public float periodLootSimpleSlide;

		public float periodLootSimpleFadeOut;

		public float periodRuneBegin;

		public float periodRuneFadeOut;

		public float periodGearBegin;

		public float periodGearOpen;

		public float periodGearReceive;

		public float periodGearScraps;

		public float periodGearFadeOut;

		public float periodSmallCardAppear;

		public float periodSmallCardFadeOut;

		private bool clicked;

		private ShopLootpackOpening.SubState _subState;

		private ShopLootpackOpening.State _state;

		public Image imageBg;

		public CanvasGroup canvasGroup;

		public Image imageBgRadialBlur;

		public Image imageChestOld;

		public RectTransform chestTransform;

		public GameButton gameButton;

		public Sprite spriteCurrencyToken;

		public Sprite spriteCurrencyScrap;

		public Sprite spriteCurrencyCredit;

		public Sprite[] spritesGearLevel;

		public Sprite[] spritesGearLevelSmall;

		public Sprite[] spritesGearLevelRibbon;

		public Sprite[] spritesGearLevelBg;

		private bool animating;

		private float timer;

		private float period;

		private bool animatingSub;

		private float timerSub;

		private float periodSub;

		public bool finished;

		public bool resultsReceived;

		public Image[] lootSimples;

		public Image[] lootSimpleSubs;

		public LootPackTrinketCard lootpackTrinketCard;

		public RectTransform gearParent;

		public RectTransform gearInfoParent;

		public RectTransform gearEmptyParent;

		public CanvasGroup gearCanvasGroup;

		public CanvasGroup gearGlowOrnament;

		public Image imageGearBg;

		public Image imageGearHero;

		public Image imageGearRibbon;

		public Image imageGearCard;

		public Image imageGear;

		public Text textGearName;

		public Text textGearBonus;

		public Text textGearPercent;

		public Text textLootSimpleCount;

		public Image[] imageHeroGearBgs;

		public Image[] imageHeroGears;

		public Image[] imageHeroGearFlashes;

		public Image imageSummaryBg;

		public ChestSpine chestSpineSummary;

		public Image imageRune;

		public Image imageTotem;

		public Text textRune;

		public Text textTotem;

		private Vector2 lootFirstPos;

		private Vector2 gearGoalPos;

		private Vector2 runeGoalPos;

		private float gearGoalScale;

		private List<Vector2> lootSimpleGoalPos;

		private List<float> lootSimpleGoalRot;

		private List<float> lootSimpleGoalScale;

		public double totalCreditsBeforeBeginning;

		public double totalTokensBeforeBeginning;

		public double totalScrapsBeforeBeginning;

		public bool creditsWillDrop;

		public bool tokensWillDrop;

		public bool scrapsWillDrop;

		public double amountCredits;

		public double amountTokens;

		public double amountScraps;

		private CurrencyType currencyType;

		public List<Rune> runesEarned;

		private int runeIndex;

		public List<Trinket> trinketsEarned;

		private int trinketIndex;

		public List<Gear> lootGears;

		public List<GearChange> lootGearChanges;

		public List<HeroDataBase> allHeroes;

		private int gearIndex;

		public Dictionary<string, Sprite> spritesHeroCircle;

		public Dictionary<string, Sprite> spritesGearIcon;

		public Dictionary<string, Sprite> spritesTotem;

		public Dictionary<string, Sprite> spritesRune;

		public Dictionary<string, List<Gear>> allHeroesBoughtGears;

		public Dictionary<string, Color> runeColorForRing;

		private float heroGearScale;

		private bool updatedHeroGears;

		public ChestSpine chestSpine;

		public NotificationBadge notificationBadge;

		public int numItemsLeft;

		private int gearIdForShineEffect;

		public RectTransform smallCard;

		public RectTransform smallCardInfoParent;

		public RectTransform smallCardEmptyParent;

		public CanvasGroup smallCardEmptyGlowOrnament;

		public CanvasGroup canvasGroupSmallCard;

		public MenuShowCurrency menuShowCurrencySmallCard;

		public RectTransform trinketCard;

		public RectTransform trinketCardInfoParent;

		public RectTransform trinketCardEmptyParent;

		public CanvasGroup trinketCardEmptyGlowOrnament;

		public CanvasGroup canvasGroupTrinketCard;

		public Image imageSmallCardRuneBg;

		public Image imageSmallCardRuneIcon;

		public Image imageTrinketCardTrinketBg;

		public Image imageTrinketCardTrinketIcon;

		private TrinketUi trinketUi;

		public PanelCurrencySide panelCurrencySide;

		public Text textSmallCard;

		public Text textSmallCardDesc;

		public Text textTrinketCard;

		public Sprite spriteRewardCurrencyToken;

		public Sprite spriteRewardCurrencyScrap;

		public Sprite spriteRewardCurrencyCredit;

		public Sprite spriteRewardCurrencyCandy;

		public Action onCompledeFadeIn;

		private Vector2 smallCardFirstPos;

		private Vector2 trinketCardFirstPos;

		private float panelSideCurrencyTimer;

		private CurrencyType panelSideCurrencyType;

		private double panelSideCurrencyAmountStart;

		private double panelSideCurrencyAmountAdd;

		public UniversalTotalBonus universalBonus;

		private ShopPack selectedShopPack;

		public GameButton skipAnimationButton;

		public enum SubState
		{
			None,
			LootSimpleSlide,
			LootSimpleFadeOut,
			LootSimpleGearFadeOut
		}

		public enum State
		{
			None,
			Begin,
			OpenChest,
			LootSimple,
			LootSimpleCount,
			RuneBegin,
			RuneFadeOut,
			GearBegin,
			GearOpen,
			GearReceive,
			GearScraps,
			GearFadeOut,
			CurrencyCard,
			CurrencyCardFadeOut,
			RuneCard,
			RuneCardFadeOut,
			TrinketCard,
			TrinketCardFadeOut
		}
	}
}
