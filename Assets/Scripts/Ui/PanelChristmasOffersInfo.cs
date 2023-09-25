using System;
using DG.Tweening;
using DynamicLoading;
using Simulation;
using Static;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelChristmasOffersInfo : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.image.transform.SetScale(0f);
		}

		public void InitStrings()
		{
			this.header.text = LM.Get("CHRISTMAS_EVENT_TITLE");
			this.okButton.text.text = LM.Get("UI_OKAY");
		}

		public void UpdatePopup(Simulator simulator)
		{
			if (simulator.candyDropAlreadyDisabled)
			{
				this.eventEndedElements.SetActive(true);
				this.duringEventElements.SetActive(false);
				if (!this.artRequested)
				{
					this.artRequested = true;
					this.artSprite.LoadObjectAsync(new Action<Sprite>(this.OnArtLoaded));
				}
				this.title.text = LM.Get("WINTERTIDE_EVENT_FINISHED_TITLE");
				this.message.text = LM.Get("WINTERTIDE_EVENT_FINISHED_MESSAGE");
				if (TrustedTime.IsReady() && PlayfabManager.christmasOfferConfigLoaded)
				{
					TimeSpan time = PlayfabManager.christmasOfferConfig.offerConfig.endDateParsed - TrustedTime.Get();
					this.timer.text = string.Format(LM.Get("CANDIES_CONVERSION_TIMER"), GameMath.GetTimeDatailedShortenedString(time));
				}
				else
				{
					this.timer.text = LM.Get("UI_SHOP_CHEST_0_WAIT");
				}
			}
			else
			{
				this.eventEndedElements.SetActive(false);
				this.duringEventElements.SetActive(true);
				this.collectCandyLabel.text = LM.Get("CHRISTMAS_POPUP_INFO_COLLECT_CANDY");
				this.spendCandyLabel.text = LM.Get("CHRISTMAS_POPUP_INFO_SPEND_CANDY");
				this.buyCandyLabel.text = LM.Get("CHRISTMAS_POPUP_INFO_BUY_CANDY");
				if (TrustedTime.IsReady())
				{
					TimeSpan dailtyCapResetTimer = simulator.GetDailtyCapResetTimer();
					this.timer.text = StringExtension.Concat(LM.Get("CANDIES_CAP_RESET"), " ", GameMath.GetTimeDatailedShortenedString(dailtyCapResetTimer));
				}
				else
				{
					this.timer.text = LM.Get("UI_SHOP_CHEST_0_WAIT");
				}
			}
		}

		private void OnArtLoaded(Sprite sprite)
		{
			this.image.sprite = sprite;
			this.image.transform.SetScale(0f);
			this.image.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack);
		}

		private void OnDisable()
		{
			if (this.artRequested)
			{
				this.artSprite.Unload();
				this.artRequested = false;
			}
			this.image.transform.DOKill(false);
			this.image.transform.SetScale(0f);
		}

		[Header("Popup")]
		public Text header;

		public GameButton closeButton;

		public GameButton closeBackgroundButton;

		public GameButton okButton;

		public Text timer;

		public RectTransform popupRect;

		[Header("During Event")]
		public GameObject duringEventElements;

		public Text collectCandyLabel;

		public Text spendCandyLabel;

		public Text buyCandyLabel;

		[Header("Event Ended")]
		public GameObject eventEndedElements;

		public Image image;

		public Text title;

		public Text message;

		public BSprite artSprite;

		private bool artRequested;
	}
}
