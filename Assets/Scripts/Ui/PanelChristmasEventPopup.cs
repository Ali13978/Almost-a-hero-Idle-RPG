using System;
using DG.Tweening;
using DynamicLoading;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelChristmasEventPopup : AahMonoBehaviour
	{
		public void SetEvent(int popupIndex)
		{
			if (this.image.sprite == null)
			{
				this.image.transform.SetScale(0f);
				this.imageBundle.LoadObjectAsync(new Action<Sprite>(this.OnSpriteLoaded));
			}
			switch (popupIndex)
			{
			case 1:
				this.buttonClose.gameObject.SetActive(true);
				this.buttonCloseBackground.interactable = true;
				this.textBodyUp.text = string.Format(LM.Get("EVENT_POPUP_1"), AM.SizeText(AM.cs(GameMath.GetTimeDatailedShortenedString(PlayfabManager.christmasOfferConfig.candyDropLimitDateParsed - TrustedTime.Get()), this.timeStringColor), 32));
				this.textBodyDown.enabled = false;
				this.popupParent.SetSizeDeltaY(250f);
				this.buttonGoToTree.text.text = LM.Get("GO_TO_TREE");
				break;
			case 2:
				this.buttonClose.gameObject.SetActive(true);
				this.buttonCloseBackground.interactable = true;
				this.textBodyUp.text = string.Format(LM.Get("EVENT_POPUP_2"), AM.SizeText(AM.cs(GameMath.GetTimeDatailedShortenedString(PlayfabManager.christmasOfferConfig.offerConfig.endDateParsed - TrustedTime.Get()), this.timeStringColor), 32));
				this.textBodyDown.enabled = true;
				this.textBodyDown.text = LM.Get("EVENT_POPUP_2_EXTENDED");
				this.popupParent.SetSizeDeltaY(250f);
				this.buttonGoToTree.text.text = LM.Get("GO_TO_TREE");
				break;
			case 3:
				this.buttonClose.gameObject.SetActive(false);
				this.buttonCloseBackground.interactable = false;
				this.textBodyUp.text = LM.Get("EVENT_POPUP_3");
				this.textBodyDown.text = LM.Get("EVENT_POPUP_3_EXTENDED");
				this.textBodyDown.enabled = true;
				this.popupParent.SetSizeDeltaY(220f);
				this.buttonGoToTree.text.text = LM.Get("UI_OKAY");
				break;
			default:
				throw new NotImplementedException();
			}
		}

		private void OnSpriteLoaded(Sprite obj)
		{
			if (this.image.sprite != null)
			{
				this.image.sprite = null;
			}
			if (obj == null)
			{
				return;
			}
			this.image.sprite = obj;
			this.image.SetNativeSize();
			this.image.transform.SetScale(0f);
			this.image.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
		}

		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.InitStrings();
		}

		public void InitStrings()
		{
			this.textHeader.text = LM.Get("CHRISTMAS_EVENT_TITLE");
		}

		public void OnClose()
		{
			if (this.image.sprite != null)
			{
				this.image.sprite = null;
				this.image.transform.SetScale(0f);
				this.imageBundle.Unload();
			}
		}

		public Text textHeader;

		public Text textBodyUp;

		public Text textBodyDown;

		public GameButton buttonClose;

		public GameButton buttonCloseBackground;

		public GameButton buttonGoToTree;

		public Image image;

		public Color timeStringColor;

		public RectTransform popupParent;

		public BSprite imageBundle;
	}
}
