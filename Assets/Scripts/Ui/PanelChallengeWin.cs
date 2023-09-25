using System;
using DG.Tweening;
using Simulation;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelChallengeWin : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.InitStrings();
			this.rangeEndY = this.leftRangeRectTransform.anchoredPosition.y;
			this.crownEndY = this.crownRectTransform.anchoredPosition.y;
		}

		public void InitStrings()
		{
			this.textTitle.text = LM.Get("UI_CHALLENGE_WIN");
			if (this.isActuallyUnlock)
			{
				this.button.text.text = LM.Get("UI_ACCEPT");
			}
			else
			{
				this.button.text.text = LM.Get("UI_COLLECT");
			}
		}

		public void SetStartPosition()
		{
			if (this.isRift)
			{
				foreach (SkeletonGraphic skeletonGraphic in this.torchAnimations)
				{
					if (this.isCursed)
					{
						skeletonGraphic.Skeleton.SetSkin(1);
					}
					else
					{
						skeletonGraphic.Skeleton.SetSkin(0);
					}
				}
			}
			this.popupRectTransform.localScale = Vector3.zero;
			this.textTitle.transform.localScale = Vector3.one * 2f;
			Color color = this.textTitle.color;
			color.a = 0f;
			this.textTitle.color = color;
			this.popupRectTransform.SetSizeDeltaY(this.startHeight);
			this.paperScrollRectTrasform.SetSizeDeltaX(this.paperRollStartWidth);
			this.normalParent.gameObject.SetActive(!this.isRift);
			this.riftParent.gameObject.SetActive(this.isRift);
			if (this.isRift)
			{
				this.stoneRectTransform.SetAnchorPosY(this.rangeStartY);
				this.leftTorchRectTransform.SetAnchorPosY(this.rangeStartY);
				this.rightTorchRectTransform.SetAnchorPosY(this.rangeStartY);
			}
			else
			{
				this.crownRectTransform.SetAnchorPosY(this.rangeStartY);
				this.leftRangeRectTransform.SetAnchorPosY(this.rangeStartY);
				this.rightRangeRectTransform.SetAnchorPosY(this.rangeStartY);
				this.crownRectTransform.eulerAngles = new Vector3(0f, 0f, (float)((!GameMath.GetProbabilityOutcome(0.5f, GameMath.RandType.NoSeed)) ? -15 : 15));
			}
			this.canvasGroup.alpha = 0f;
		}

		public void FadeIn()
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(this.popupRectTransform.DOScale(0.8f, 0.2f).SetEase(Ease.OutQuint)).Insert(0.05f, this.paperScrollRectTrasform.DOSizeDelta(new Vector2(this.paperRollEndWidth, this.paperScrollRectTrasform.sizeDelta.y), 0.5f, false).SetEase(Ease.OutElastic, 1.7f, 0.4f)).Insert(0.4f, this.textTitle.rectTransform.DOScale(1f, 0.15f).SetEase(Ease.InSine)).Insert(0.4f, this.textTitle.DOFade(1f, 0.15f).SetEase(Ease.InSine)).Insert(0.1f, this.popupRectTransform.DOSizeDelta(new Vector2(this.popupRectTransform.sizeDelta.x, this.endHeight), 0.3f, false).SetEase(Ease.OutBack));
			if (this.isRift)
			{
				sequence.Insert(0.3f, this.leftTorchRectTransform.DOAnchorPosY(this.rangeEndY, 0.3f, false).SetEase(Ease.OutBack)).Insert(0.35f, this.rightTorchRectTransform.DOAnchorPosY(this.rangeEndY, 0.3f, false).SetEase(Ease.OutBack)).Insert(0.4f, this.stoneRectTransform.DOAnchorPosY(this.crownEndY, 0.3f, false).SetEase(Ease.OutBack));
			}
			else
			{
				sequence.Insert(0.3f, this.leftRangeRectTransform.DOAnchorPosY(this.rangeEndY, 0.3f, false).SetEase(Ease.OutBack)).Insert(0.35f, this.rightRangeRectTransform.DOAnchorPosY(this.rangeEndY, 0.3f, false).SetEase(Ease.OutBack)).Insert(0.4f, this.crownRectTransform.DOAnchorPosY(this.crownEndY, 0.3f, false).SetEase(Ease.OutBack)).Insert(0.5f, this.crownRectTransform.DORotate(new Vector3(0f, 0f, 0f), 0.3f, RotateMode.Fast).SetEase(Ease.OutElastic, 1.7f, 0.4f));
			}
			sequence.Insert(0f, this.canvasGroup.DOFade(0.85f, 0.3f).SetEase(Ease.OutCubic));
			sequence.Play<Sequence>();
		}

		public void FadeOut()
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(this.paperScrollRectTrasform.DOSizeDelta(new Vector2(this.paperRollStartWidth, this.paperScrollRectTrasform.sizeDelta.y), 0.5f, false).SetEase(Ease.InBack)).Insert(0.1f, this.textTitle.rectTransform.DOScale(0f, 0.4f).SetEase(Ease.InBack)).Insert(0.1f, this.popupRectTransform.DOSizeDelta(new Vector2(this.popupRectTransform.sizeDelta.x, this.startHeight), 0.3f, false).SetEase(Ease.InBack)).Insert(0.4f, this.popupRectTransform.DOScale(0f, 0.2f).SetEase(Ease.InSine));
			if (this.isRift)
			{
				sequence.Insert(0.2f, this.stoneRectTransform.DOAnchorPosY(this.rangeStartY, 0.3f, false).SetEase(Ease.InBack)).Insert(0.25f, this.rightTorchRectTransform.DOAnchorPosY(this.rangeStartY, 0.3f, false).SetEase(Ease.InBack)).Insert(0.3f, this.leftTorchRectTransform.DOAnchorPosY(this.rangeStartY, 0.3f, false).SetEase(Ease.InBack));
			}
			else
			{
				sequence.Insert(0.2f, this.crownRectTransform.DOAnchorPosY(this.rangeStartY, 0.3f, false).SetEase(Ease.InBack)).Insert(0.25f, this.rightRangeRectTransform.DOAnchorPosY(this.rangeStartY, 0.3f, false).SetEase(Ease.InBack)).Insert(0.3f, this.leftRangeRectTransform.DOAnchorPosY(this.rangeStartY, 0.3f, false).SetEase(Ease.InBack));
			}
			sequence.Insert(0f, this.canvasGroup.DOFade(0f, 0.3f).SetEase(Ease.OutCubic));
			sequence.Play<Sequence>();
		}

		public void SetIconCurrency(CurrencyType currencyType)
		{
			this.currencyCart.gameObject.SetActive(true);
			this.imageReward.gameObject.SetActive(false);
			this.currencyCart.SetCurrency(currencyType);
		}

		public void SetIconSprite(GameMode mode)
		{
			if (mode == GameMode.CRUSADE)
			{
				this.imageReward.sprite = UiData.inst.spriteUnlockModeTimeChallenge;
			}
			else
			{
				if (mode != GameMode.RIFT)
				{
					throw new NotImplementedException();
				}
				this.imageReward.sprite = UiData.inst.spriteUnlockModeRift;
			}
		}

		public void SetIconSpriteMerchant()
		{
			this.imageReward.sprite = UiData.inst.spriteUnlockMerchant;
		}

		public void SetIconSpriteCompass()
		{
			this.imageReward.sprite = UiData.inst.spriteUnlockCompass;
		}

		public void SetIconSpriteMythicalSlot()
		{
			this.imageReward.sprite = UiData.inst.spriteMythicalArtifactSlot;
		}

		public void SetIconSpriteTrinketSlot()
		{
			this.imageReward.sprite = UiData.inst.spriteUnlockTrinketSlot;
		}

		public void SetIconSpriteTrinketPack()
		{
			this.imageReward.sprite = UiData.inst.spriteUnlockTrinketPack;
		}

		public void SetIconSpriteCharmPack()
		{
			this.imageReward.sprite = UiData.inst.spriteUnlockCharmPack;
		}

		public void SetIconSpriteDailies()
		{
			this.imageReward.sprite = UiData.inst.spriteUnlockDailyQuests;
		}

		public void SetIconSpritePrestige()
		{
			this.imageReward.sprite = UiData.inst.spriteUnlockPrestige;
		}

		public void SetIconSprite(Sprite s)
		{
			this.imageReward.sprite = s;
		}

		private void SetIconSpriteSpecificCharm(UiManager manager, CharmEffectData ced)
		{
			this.charmPack.gameObject.SetActive(false);
			this.imageRewardBackground.gameObject.SetActive(true);
			this.imageRewardBackground.sprite = manager.GetCharmCardFace(ced.BaseData.charmType);
			this.imageRewardBackground.SetNativeSize();
			this.imageReward.color = UiManager.CHARM_ICON_COLOR_IN_CARD;
			this.imageReward.sprite = manager.spritesCharmEffectIcon[ced.BaseData.id];
		}

		public void ShowShareButton()
		{
			this.shareButton.gameObject.SetActive(true);
			this.button.rectTransform.SetAnchorPosX(74.3f);
			this.button.rectTransform.SetSizeDeltaX(340f);
		}

		public void SetRewardDetails(Unlock unlock, UiManager uiManager, bool showShareButton)
		{
			if (showShareButton)
			{
				this.ShowShareButton();
			}
			else
			{
				this.shareButton.gameObject.SetActive(false);
				this.button.rectTransform.SetAnchorPosX(0f);
				this.button.rectTransform.SetSizeDeltaX(356f);
			}
			this.textReward.text = unlock.GetRewardedString();
			this.imageReward.color = Color.white;
			this.charmPack.gameObject.SetActive(false);
			this.currencyCart.gameObject.SetActive(false);
			this.heroAnimation.gameObject.SetActive(false);
			this.imageReward.gameObject.SetActive(true);
			this.imageReward.transform.localScale = new Vector3(1f, 1f, 1f);
			this.imageRewardBackground.gameObject.SetActive(false);
			if (unlock.HasRewardOfType(typeof(UnlockRewardMerchant)))
			{
				this.SetIconSpriteMerchant();
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardCompass)))
			{
				this.SetIconSpriteCompass();
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardGameModeCrusade)))
			{
				this.SetIconSprite(GameMode.CRUSADE);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardGameModeRift)))
			{
				this.SetIconSprite(GameMode.RIFT);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardCurrency)))
			{
				UnlockRewardCurrency unlockRewardCurrency = unlock.GetReward() as UnlockRewardCurrency;
				this.SetIconCurrency(unlockRewardCurrency.currencyType);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardTotem)))
			{
				this.SetIconSprite(uiManager.GetSpriteTotem(unlock.GetTotemId()));
				this.imageReward.transform.localScale = new Vector3(1f, 1f, 1f) * 0.7f;
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardRune)))
			{
				UnlockRewardRune unlockRewardRune = unlock.GetReward() as UnlockRewardRune;
				this.SetIconSprite(uiManager.GetSpriteRune(unlockRewardRune.GetRuneId()));
				this.imageReward.color = uiManager.GetRuneColor(unlockRewardRune.GetRune().belongsTo.id);
				this.imageReward.transform.localScale = new Vector3(1f, 1f, 1f) * 1.4f;
				this.imageRewardBackground.gameObject.SetActive(true);
				this.imageRewardBackground.sprite = UiData.inst.runeOfferBackgroundBig;
				this.imageRewardBackground.SetNativeSize();
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardMerchantItem)))
			{
				this.SetIconSprite(uiManager.GetSpriteMerchantItem(unlock.GetMerchantItemId()));
				this.imageReward.transform.localScale = new Vector3(1f, 1f, 1f) * 0.9f;
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardHero)))
			{
				if (this.heroAnimation.loadingHeroName != "nobody" && this.heroAnimation.loadingHeroName != unlock.GetHeroId())
				{
					this.heroAnimation.OnClose();
				}
				this.heroAnimation.SetHeroAnimation(unlock.GetHeroId(), 1, false, false, true, true);
				this.heroAnimation.gameObject.SetActive(true);
				this.imageReward.gameObject.SetActive(false);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardMythicalArtifactSlot)))
			{
				this.SetIconSpriteMythicalSlot();
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardTrinketSlots)))
			{
				this.SetIconSpriteTrinketSlot();
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardTrinketPack)))
			{
				this.SetIconSpriteTrinketPack();
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardCharmPack)))
			{
				this.charmPack.gameObject.SetActive(true);
				this.imageReward.gameObject.SetActive(false);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardSpecificCharm)))
			{
				UnlockRewardSpecificCharm unlockRewardSpecificCharm = unlock.GetReward() as UnlockRewardSpecificCharm;
				this.SetIconSpriteSpecificCharm(uiManager, unlockRewardSpecificCharm.ced);
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardMineToken)))
			{
				this.heroAnimation.gameObject.SetActive(false);
				this.imageReward.sprite = UiData.inst.spriteUnlockMineToken;
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardMineScrap)))
			{
				this.heroAnimation.gameObject.SetActive(false);
				this.imageReward.sprite = UiData.inst.spriteUnlockMineScrap;
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardDailies)))
			{
				this.SetIconSpriteDailies();
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardPrestige)))
			{
				this.SetIconSpritePrestige();
				this.imageReward.transform.localScale = new Vector3(1f, 1f, 1f) * 2.2f;
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardAeonDust)))
			{
				this.imageReward.sprite = UiData.inst.spriteRewardAeonDust;
			}
			else if (unlock.HasRewardOfType(typeof(UnlockRewardTrinketEnch)))
			{
				this.imageReward.sprite = UiData.inst.spriteUnlockTrinketSmithing;
			}
			else
			{
				if (!unlock.HasRewardOfType(typeof(UnlockRewardSkillPointsAutoDistribution)))
				{
					throw new NotImplementedException();
				}
				this.imageReward.sprite = UiData.inst.spriteUnlockPurchaseRandomHeroes;
			}
			this.imageReward.SetNativeSize();
		}

		public GameButton button;

		public GameButton shareButton;

		public Text textTitle;

		public Image imageModeChallengeIcon;

		public Text textChallenge;

		public GameObject timePanel;

		public Text textTime;

		public Image imageReward;

		public Image imageRewardBackground;

		public Text textNextReward;

		public Text textReward;

		public Text textSecondaryReward;

		public HeroAnimation heroAnimation;

		public float timer;

		public bool fadingOut;

		public bool returnToStateAfterFadeOut;

		public UiState stateToReturnTo;

		public PopupSpine popupSpine;

		public CanvasGroup canvasGroup;

		public CartWidget currencyCart;

		public bool isActuallyUnlock;

		public RectTransform popupRectTransform;

		public RectTransform paperScrollRectTrasform;

		[Header("Normal")]
		public RectTransform normalParent;

		public RectTransform crownRectTransform;

		public RectTransform leftRangeRectTransform;

		public RectTransform rightRangeRectTransform;

		[Header("Rift")]
		public RectTransform riftParent;

		public RectTransform stoneRectTransform;

		public RectTransform leftTorchRectTransform;

		public RectTransform rightTorchRectTransform;

		public RectTransform charmPack;

		public SkeletonGraphic[] torchAnimations;

		public float startHeight = 60f;

		public float endHeight = 894f;

		public float paperRollStartWidth = 150f;

		public float paperRollEndWidth = 422f;

		public float rangeStartY;

		public float rangeEndY;

		public float crownEndY;

		public bool isRift;

		public bool isCursed;
	}
}
