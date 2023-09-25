using System;
using System.Collections.Generic;
using DG.Tweening;
using Simulation;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class HeroSkinChanger : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.selectedButtonIndex = -1;
			for (int i = 0; i < 15; i++)
			{
				SkinInventoryButton skinInventoryButton = UnityEngine.Object.Instantiate<SkinInventoryButton>(this.skinInventoryButtonPrefab, this.skinScroll.content);
				float x = (float)GameMath.FloorToInt((float)i / 2f) * this.collumnGap;
				float y = (i % 2 != 1) ? 0f : (-this.rowGap);
				if (i % 2 != 1)
				{
					skinInventoryButton.transform.SetAsLastSibling();
				}
				else
				{
					skinInventoryButton.transform.SetAsFirstSibling();
				}
				skinInventoryButton.rectTransform.anchoredPosition = this.startPos + new Vector2(x, y);
				int refi = i;
				skinInventoryButton.gameButton.onClick = delegate()
				{
					this.Button_OnSkinSelected(refi);
				};
				this.skinInvButtons.Add(skinInventoryButton);
			}
			this.InitStrings();
		}

		public void InitStrings()
		{
			this.header.text = LM.Get("UI_SELECT_SKIN");
			foreach (SkinInventoryButton skinInventoryButton in this.skinInvButtons)
			{
				skinInventoryButton.newLabel.text = LM.Get("UI_NEW");
			}
			this.flashOfferTitle.text = LM.Get("SKIN_FLASH_OFFER_INFO_TITLE");
			this.flashOfferDesc.text = LM.Get("SKIN_FLASH_OFFER_INFO_DESC");
			this.gotToShopButton.text.text = LM.Get("GO_TO_TREE");
			this.randomSkinMessage.text = LM.Get("RANDOM_SKIN_DESC");
		}

		private void Button_OnSkinSelected(int index)
		{
			this.setScrollPosition = false;
			this.SetSelectedSkinIndex(index);
		}

		public void SetSelectedSkinIndex(int index)
		{
			this.selectedButtonIndex = index;
			for (int i = 0; i < this.skinInvButtons.Count; i++)
			{
				if (index == i)
				{
					this.skinInvButtons[i].transform.localScale = Vector3.one * 1.2f;
				}
				else
				{
					this.skinInvButtons[i].transform.localScale = Vector3.one;
				}
			}
			if (this.setScrollPosition)
			{
				this.SetScrollPosByIndex(index);
			}
			this.setScrollPosition = true;
			this.updateDetails = true;
		}

		private void SetScrollPosByIndex(int index)
		{
			SkinInventoryButton skinInventoryButton = this.skinInvButtons[index];
			float x = skinInventoryButton.rectTransform.anchoredPosition.x;
			float width = this.skinScroll.viewport.rect.width;
			float width2 = this.skinScroll.content.rect.width;
			float num = width * 0.5f;
			float value = -num - x + this.collumnGap;
			float x2 = GameMath.Clamp(value, width - width2 - num, -num);
			this.skinScroll.content.SetAnchorPosX(x2);
		}

		public void SetScrollContentSize(int skinCount)
		{
			int num = GameMath.CeilToInt((float)(skinCount + 1) / 2f);
			float value = this.startPos.x * 2f + (float)(num - 1) * this.collumnGap;
			float x = GameMath.Clamp(value, 690f, 9999f);
			this.skinScroll.content.SetSizeDeltaX(x);
		}

		public void DoFlashHeroSpine(float dur, float delay)
		{
			this.PlayBuyFX(delay);
		}

		public void PlayBuyFX(float delay)
		{
			this.bonefollower.SetBone("etiqueta");
			this.frameAnimator.transform.SetAsLastSibling();
			this.frameAnimator.rectTransform.SetParent(this.GetSelectedButton().rectTransform);
			this.frameAnimator.rectTransform.anchoredPosition = Vector2.zero;
			this.frameAnimator.gameObject.SetActive(true);
			this.frameAnimator.AnimationState.SetAnimation(0, "idle", true);
			this.frameAnimatorCurrencyParent.gameObject.SetActive(true);
			this.revealFX.transform.SetAsLastSibling();
			Sequence s = DOTween.Sequence();
			s.Append(this.PlayDelayedFX(this.buyFx, "animation", 0f)).Join(this.PlayDelayedFraneFX()).Play<Sequence>();
			Sequence s2 = DOTween.Sequence();
			s2.AppendInterval(0.5f).Append(this.heroMaterial.DOColor(Color.white, "_Black", 0.16f).SetEase(Ease.InCubic)).AppendInterval(0.1f).Append(this.heroMaterial.DOColor(Color.black, "_Black", 0.1f).SetEase(Ease.OutCubic)).Play<Sequence>();
			Sequence s3 = DOTween.Sequence();
			s3.AppendInterval(0.6f).Append(this.PlayDelayedBuyGlowFX()).Join(this.PlayDelayedFX(this.revealFX, "animation", 0.15f)).Play<Sequence>();
		}

		public void PlayRevealFX(float delay)
		{
			this.revealFX.transform.SetAsFirstSibling();
			Sequence s = DOTween.Sequence();
			s.AppendInterval(delay).Append(this.PlayDelayedFX(this.revealFX, "animation_reveal", 0f)).Play<Sequence>();
		}

		public Sequence PlayDelayedFX(SkeletonGraphic skeletonGraphic, string animationName, float waitToStart = 0f)
		{
			Sequence sequence = DOTween.Sequence();
			if (waitToStart > 0f)
			{
				sequence.AppendInterval(waitToStart);
			}
			sequence.AppendCallback(delegate
			{
				skeletonGraphic.AnimationState.SetAnimation(0, animationName, false);
				skeletonGraphic.gameObject.SetActive(true);
			}).AppendInterval(1.6f).AppendCallback(delegate
			{
				skeletonGraphic.gameObject.SetActive(false);
			});
			return sequence;
		}

		public Sequence PlayDelayedBuyGlowFX()
		{
			Sequence sequence = DOTween.Sequence();
			sequence.AppendCallback(delegate
			{
				this.buyFxGlow.gameObject.SetActive(true);
				this.buyFxGlow.AnimationState.SetAnimation(0, "animation", false);
			}).AppendInterval(2.1f).AppendCallback(delegate
			{
				this.buyFxGlow.gameObject.SetActive(false);
			});
			return sequence;
		}

		public Sequence PlayDelayedFraneFX()
		{
			Sequence sequence = DOTween.Sequence();
			sequence.AppendCallback(delegate
			{
				this.frameAnimator.AnimationState.SetAnimation(0, "animation", false);
			}).AppendInterval(1.2f).AppendCallback(delegate
			{
				this.bonefollower.SetBone("etiqueta2");
				this.frameAnimator.rectTransform.SetParent(base.transform);
			}).AppendInterval(0.4f).AppendCallback(delegate
			{
				this.frameAnimatorCurrencyParent.gameObject.SetActive(false);
			}).AppendInterval(0.4f).AppendCallback(delegate
			{
				this.frameAnimator.gameObject.SetActive(false);
			});
			return sequence;
		}

		public SkinInventoryButton GetSelectedButton()
		{
			return this.skinInvButtons[this.selectedButtonIndex];
		}

		public Text header;

		public Text skinDescription;

		public Text skinName;

		public RectTransform skinNameParent;

		public MenuShowCurrency currency;

		public MenuShowCurrency currencySecondary;

		public CurrencyType currencyType;

		public ButtonUpgradeAnim equipButton;

		public GameButton soloEquipButton;

		public GameButton closeButton;

		public HeroAnimation heroAnimation;

		public HeroAnimation heroAnimationSecondary;

		public Material heroMaterial;

		public Image randomSkinImage;

		public Text randomSkinMessage;

		public ScrollRect skinScroll;

		public SkeletonGraphic buyFx;

		public SkeletonGraphic revealFX;

		public SkeletonGraphic buyFxGlow;

		public int selectedButtonIndex;

		public SkinInventoryButton skinInventoryButtonPrefab;

		[HideInInspector]
		public List<SkinInventoryButton> skinInvButtons;

		private const int BUTTONCOUNT = 15;

		public UiState oldState;

		public HeroDataBase selectedHero;

		public SkinData selectedSkin;

		public SkeletonGraphic frameAnimator;

		public BoneFollowerGraphic bonefollower;

		public RectTransform frameAnimatorCurrencyParent;

		public bool updateDetails;

		private bool setScrollPosition = true;

		private float collumnGap = 160f;

		private float rowGap = 180f;

		private Vector2 startPos = new Vector2(150f, -120f);

		public bool dontAnimateHero;

		public GameObject flashOfferContentParent;

		public Text flashOfferTitle;

		public Text flashOfferDesc;

		public Image originalPriceIcon;

		public Text originalPriceAmount;

		public RectTransform halloweenExtra;

		public RectTransform halloweenExtraPumpkin;

		public GameButton gotToShopButton;

		public GameObject christmasIcon;

		public RectTransform popupRect;
	}
}
