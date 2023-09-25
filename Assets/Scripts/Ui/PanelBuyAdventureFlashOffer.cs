using System;
using System.Collections.Generic;
using DG.Tweening;
using Simulation;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelBuyAdventureFlashOffer : PanelBuyFlashOffer
	{
		public override void Register()
		{
			base.Register();
			base.AddToInits();
		}

		public override void Init()
		{
			this.buttonGoToShop.onClick = delegate()
			{
				this.buttonGoToShopClickedCallback();
			};
			PanelBuyAdventureFlashOffer.CurrencyMenuDropPosition = new Func<DropPosition>(this.DropPosition);
			this.revealSkinFx.Initialize(true);
			this.InitStrings();
		}

		public override void CancelPurchaseAnimIfNecessary()
		{
			base.CancelPurchaseAnimIfNecessary();
			if ((this.tintAnim == null || !this.tintAnim.IsPlaying()) && (this.glowAnim == null || !this.glowAnim.IsPlaying()))
			{
				return;
			}
			this.tintAnim.Kill(false);
			this.glowAnim.Kill(false);
			this.buySkinFx.gameObject.SetActive(false);
			this.revealSkinFx.gameObject.SetActive(false);
			if (this.buySkinFx.AnimationState != null)
			{
				this.buySkinFx.AnimationState.ClearTracks();
			}
		}

		private DropPosition DropPosition()
		{
			return new DropPosition
			{
				startPos = this.icon.transform.position,
				endPos = this.icon.transform.position - Vector3.up * 0.1f,
				invPos = this.menuShowCurrency.transform.position + Vector3.left * 0.33f,
				targetToScaleOnReach = this.menuShowCurrency.GetCurrencyTransform(),
				showSideCurrency = false
			};
		}

		public override void InitStrings()
		{
			base.InitStrings();
			this.adWarning.text = LM.Get("FLASH_OFFER_AD_NOT_AVAILABLE");
			this.merchantItemNotification.text = LM.Get("MERCHANT_ITEM_OFFER_NOTIFICATION");
		}

		public void PlaySkinBoughAnimation()
		{
			this.tintAnim = DOTween.Sequence();
			this.tintAnim.AppendInterval(0.5f).Append(this.heroMaterial.DOColor(Color.white, "_Black", 0.16f).SetEase(Ease.InCubic)).AppendInterval(0.1f).Append(this.heroMaterial.DOColor(Color.black, "_Black", 0.1f).SetEase(Ease.OutCubic)).Play<Sequence>();
			this.glowAnim = DOTween.Sequence();
			this.glowAnim.AppendInterval(0.6f).Append(this.PlayDelayedBuyGlowFX()).Join(this.PlayDelayedFX(this.revealSkinFx, "animation", 0.15f)).Play<Sequence>();
		}

		private Sequence PlayDelayedBuyGlowFX()
		{
			Sequence sequence = DOTween.Sequence();
			sequence.AppendCallback(delegate
			{
				this.buySkinFx.gameObject.SetActive(true);
				this.buySkinFx.AnimationState.SetAnimation(0, "animation", false);
			}).AppendInterval(2.1f).AppendCallback(delegate
			{
				this.buySkinFx.gameObject.SetActive(false);
			});
			return sequence;
		}

		private Sequence PlayDelayedFX(SkeletonGraphic skeletonGraphic, string animationName, float waitToStart = 0f)
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

		public float panelHeaightSmall;

		public float panelHeaightBig;

		public float panelHeightBigWithAdWarning;

		public float panelHeightBigWithHalloween;

		public float panelHeightMerchantItem;

		public Image iconBackground;

		public RectTransform currencyAmountParent;

		public Image currencyAmountIcon;

		public GameObject costumeNameParent;

		public Text costumeName;

		public HeroAnimation costumeAnimation;

		public SkeletonGraphic buySkinFx;

		public SkeletonGraphic revealSkinFx;

		public Material heroMaterial;

		public GameObject stockParent;

		public Text stock;

		public Text offeredObjectName;

		public Text runeDescription;

		public Text plusText;

		public MenuShowCurrency secondMenuShowCurrency;

		public Text adWarning;

		public RectTransform originalPriceParent;

		public Text originalPriceText;

		public GameObject merchantItemParent;

		public Text merchantItemDescription;

		public Text merchantItemAmount;

		public Text merchantItemNotification;

		public static Func<DropPosition> CurrencyMenuDropPosition;

		private Sequence tintAnim;

		private Sequence glowAnim;

		public static readonly Vector2 DefaultIconPosition = new Vector2(0f, -37.2f);

		public static readonly Vector2 DefaultIconSize = new Vector2(300f, 300f);

		public static readonly Vector2 DefaultBackgroundPosition = new Vector2(0f, -37.2f);

		public static readonly Vector2 DefaultBackgroundSize = new Vector2(300f, 300f);

		public static readonly Dictionary<FlashOffer.Type, PanelBuyAdventureFlashOffer.IconSettings> IconSettingsPerOffer = new Dictionary<FlashOffer.Type, PanelBuyAdventureFlashOffer.IconSettings>
		{
			{
				FlashOffer.Type.RUNE,
				new PanelBuyAdventureFlashOffer.IconSettings
				{
					IconPosition = new Vector2(0f, 0f),
					IconSize = new Vector2(150f, 150f),
					BackgroundPosition = new Vector2(0f, 0f),
					BackgroundSize = new Vector2(220f, 220f)
				}
			},
			{
				FlashOffer.Type.GEM,
				new PanelBuyAdventureFlashOffer.IconSettings
				{
					IconPosition = new Vector2(-4f, -21.5f),
					IconSize = new Vector2(427f, 427f)
				}
			},
			{
				FlashOffer.Type.MERCHANT_ITEM,
				new PanelBuyAdventureFlashOffer.IconSettings
				{
					IconPosition = new Vector2(0f, -21.5f),
					IconSize = new Vector2(210f, 210f)
				}
			}
		};

		public class IconSettings
		{
			public Vector2 IconPosition;

			public Vector2 IconSize;

			public Vector2 BackgroundPosition;

			public Vector2 BackgroundSize;
		}
	}
}
