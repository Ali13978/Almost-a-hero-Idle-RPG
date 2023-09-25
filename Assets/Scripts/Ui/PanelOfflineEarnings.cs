using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelOfflineEarnings : AahMonoBehaviour
	{
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
			this.textHeader.text = LM.Get("UI_OFFLINE_EARNINGS");
			this.gameButton.text.text = LM.Get("UI_COLLECT");
		}

		public void SetSpriteProgress(bool hasCompass, bool isGoldTriangle)
		{
			if (hasCompass)
			{
				this.imageStageProgression.sprite = this.spriteProgressWithCompass;
				this.textStage.gameObject.SetActive(true);
				this.imageStageNo.gameObject.SetActive(false);
				this.imageCompass.gameObject.SetActive(false);
				this.imageCoin.sprite = ((!isGoldTriangle) ? this.spriteCoin : this.spriteCoinTriangle);
			}
			else
			{
				this.imageStageProgression.sprite = this.spriteProgressWithoutCompass;
				this.textStage.gameObject.SetActive(false);
				this.imageStageNo.gameObject.SetActive(true);
				this.imageCompass.gameObject.SetActive(true);
			}
		}

		public Text textHeader;

		public Text textStageInfo;

		public Text textMode;

		public Text textStage;

		public Text textReward;

		public Text textGold;

		public Image imageStageProgression;

		public Image imageMode;

		public Image imageStageNo;

		public Text textStageNo;

		public Image imageCompass;

		public GameButton gameButton;

		public Sprite spriteProgressWithCompass;

		public Sprite spriteProgressWithoutCompass;

		public Image imageCoin;

		public Sprite spriteCoin;

		public Sprite spriteCoinTriangle;

		public float timer;

		public RectTransform popupRect;
	}
}
