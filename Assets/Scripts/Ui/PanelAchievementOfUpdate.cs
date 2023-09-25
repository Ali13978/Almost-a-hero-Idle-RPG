using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelAchievementOfUpdate : AahMonoBehaviour
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

		public void SetAchieved()
		{
			this.imageBg.sprite = this.spriteBgUnlocked;
			this.textHeader.color = this.colorTextTitleUnlocked;
			this.textDescTime.color = this.colorTextDescUnlocked;
			this.textDescTime.rectTransform.SetAnchorPosY(-28f);
			this.barProgress.gameObject.SetActive(false);
			this.buttonCollect.gameObject.SetActive(false);
			this.rewardBg.gameObject.SetActive(false);
		}

		public void InitStrings()
		{
			this.buttonCollect.text.text = LM.Get("UI_COLLECT");
		}

		public GameButton buttonCollect;

		public Image icon;

		public Image iconColorBg;

		public Text textHeader;

		public Text textDesc;

		public Text textDescTime;

		public Text textReward;

		public Image rewardBg;

		public Scaler barProgress;

		public Image imageBg;

		public Sprite spriteBgUnlocked;

		public Sprite spriteBgLocked;

		public Color colorTextTitleUnlocked;

		public Color colorTextTitleLocked;

		public Color colorTextDescUnlocked;

		public Color colorTextDescLocked;

		private RectTransform m_rectTransform;
	}
}
