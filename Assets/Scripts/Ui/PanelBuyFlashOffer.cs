using System;
using DG.Tweening;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelBuyFlashOffer : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToUpdates();
		}

		public override void AahUpdate(float dt)
		{
			this.timeInPanel += dt;
		}

		public virtual void InitStrings()
		{
			this.buttonBuy.textDown.text = LM.Get("UI_BUY");
			if (this.buttonGoToShopText != null)
			{
				this.buttonGoToShopText.text = LM.Get("UI_GO_TO_SHOP");
			}
			if (this.buttonGoToShopLocked != null)
			{
				this.buttonGoToShopLocked.textUp.text = LM.Get("UI_LOCKED");
				this.buttonGoToShopLocked.textDown.text = LM.Get("UI_TAB_SHOP");
			}
		}

		public void DoButtonDisappear(UiManager uiManager)
		{
			this.purchasedText.text = ((this.flashOffer.costType != FlashOffer.CostType.FREE) ? "UI_PURCHASED".Loc() : "UI_COLLECTED".Loc());
			UiManager.AddUiSound(SoundArchieve.inst.uiPurchaseGemPack);
			this.isAnimatingBuyButton = true;
			this.buttonAnimSize = this.buttonBuy.gameButton.deltaScaleOnDown;
			this.buttonBuy.gameButton.deltaScaleOnDown = 0f;
			this.purchaseSequence = DOTween.Sequence();
			this.purchasedText.gameObject.SetActive(true);
			this.purchasedText.rectTransform.localScale = Vector3.one * 2f;
			this.purchasedText.rectTransform.localRotation = Quaternion.Euler(0f, 0f, 15f);
			this.purchasedText.SetAlpha(0f);
			this.purchaseSequence.Append(this.buttonBuy.rectTransform.DOScale(0f, 0.2f).SetEase(Ease.OutCubic)).AppendCallback(delegate
			{
				this.buttonBuy.gameObject.SetActive(false);
			}).Join(this.purchasedText.rectTransform.DOScale(1f, 0.4f).SetEase(Ease.InCirc)).Join(this.purchasedText.rectTransform.DORotate(default(Vector3), 0.4f, RotateMode.Fast).SetEase(Ease.InCirc)).Join(this.purchasedText.DOFade(1f, 0.4f).SetEase(Ease.InCubic)).Append(this.purchasedText.rectTransform.DOShakeAnchorPos(0.3f, 10f, 20, 90f, false, true)).OnComplete(delegate
			{
				this.isAnimatingBuyButton = false;
				this.buttonBuy.gameButton.deltaScaleOnDown = this.buttonAnimSize;
			});
			this.purchaseSequence.Play<Sequence>();
		}

		public virtual void CancelPurchaseAnimIfNecessary()
		{
			if (this.purchaseSequence == null || !this.purchaseSequence.IsPlaying())
			{
				return;
			}
			this.purchaseSequence.Kill(false);
			this.isAnimatingBuyButton = false;
			this.SetInitialState();
			this.buttonBuy.gameButton.deltaScaleOnDown = this.buttonAnimSize;
		}

		private void SetInitialState()
		{
			this.purchasedText.gameObject.SetActive(true);
			this.purchasedText.rectTransform.localScale = Vector3.one * 2f;
			this.purchasedText.rectTransform.localRotation = Quaternion.Euler(0f, 0f, 15f);
			this.purchasedText.SetAlpha(0f);
		}

		public ButtonUpgradeAnim buttonBuy;

		public ButtonUpgradeAnim buttonGoToShopLocked;

		public GameButton buttonGoToShop;

		public Text buttonGoToShopText;

		public GameButton buttonClose;

		public GameButton buttonCloseX;

		public Image icon;

		public Text amount;

		public Text nameText;

		public Text purchasedText;

		public RectTransform popupParent;

		public Text offerLockedMessage;

		public Text canNotAffordMessage;

		public MenuShowCurrency menuShowCurrency;

		public const float MinTimeToAllowToBuy = 0.5f;

		[NonSerialized]
		public float timeInPanel;

		[NonSerialized]
		public FlashOffer flashOffer;

		[NonSerialized]
		public bool isAnimatingBuyButton;

		[NonSerialized]
		public UiState previousState;

		[NonSerialized]
		public bool flashOfferUnlocked;

		[NonSerialized]
		public Action buttonGoToShopClickedCallback;

		[NonSerialized]
		public string canNotAffordMessageKey;

		private Sequence purchaseSequence;

		private float buttonAnimSize;
	}
}
