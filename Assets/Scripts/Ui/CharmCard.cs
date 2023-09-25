using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
	public class CharmCard : MonoBehaviour
	{
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

		public void InitStrings()
		{
			this.newLabel.text = LM.Get("UI_NEW");
		}

		public void SetSoloCard()
		{
			this.levelProgresBarParent.gameObject.SetActive(true);
			this.rectTransform.anchorMin = new Vector2(0f, 1f);
			this.rectTransform.anchorMax = new Vector2(0f, 1f);
			this.button.interactable = true;
		}

		public void SetCardBack(Sprite cardBackSprite)
		{
			this.newLabelParent.gameObject.SetActive(false);
			this.levelProgresBarParent.gameObject.SetActive(false);
			this.levelParent.gameObject.SetActive(false);
			this.charmIcon.gameObject.SetActive(false);
			this.background.sprite = cardBackSprite;
			this.background.color = Color.white;
			this.backgroundFrame.enabled = false;
		}

		public void InitButton()
		{
			this.button.onClick.RemoveAllListeners();
			this.button.onClick.AddListener(new UnityAction(this.Button_OnButtonClicked));
			this.fillImage = this.levelProgresBar.scaled.GetComponent<Image>();
		}

		private void Button_OnButtonClicked()
		{
			if (this.onClicked == null)
			{
				return;
			}
			this.onClicked(this.charmId);
		}

		private RectTransform m_rectTransform;

		public RectTransform levelParent;

		public RectTransform levelProgresBarParent;

		public RectTransform newLabelParent;

		public RectTransform levelupSpineAnim;

		public Image levelParentImage;

		public Image levelBackground;

		public Image charmIcon;

		public Image background;

		public Image backgroundFrame;

		public Image fillImage;

		public Image copyCounterParent;

		public Text levelText;

		public Text maxedText;

		public Text levelProgresBarText;

		public Text newLabel;

		public Text copyCounterText;

		public Button button;

		public Scaler levelProgresBar;

		public Action<int> onClicked;

		public bool onlyCardBack;

		public int charmId;

		public Vector2 charmIconPos;

		public Vector2 curseIconPos;

		public static Color orangeSliderColor = new Color32(212, 125, 24, byte.MaxValue);

		public static Color greenSliderColor = new Color32(157, 175, 39, byte.MaxValue);
	}
}
