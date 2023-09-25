using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class MerchantItem : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			if (this.textHowManyLeft != null)
			{
				this.colorHowManyLeft = this.textHowManyLeft.color;
				this.spriteBgNormal = this.image.sprite;
			}
		}

		public override void AahUpdate(float dt)
		{
			this.imageItem.SetRGB(this.image.color);
		}

		public void SetState(bool someLeft)
		{
			if (someLeft)
			{
				this.image.sprite = this.spriteBgNormal;
				this.textPrice.gameObject.SetActive(true);
				this.textHowManyLeft.rectTransform.SetAnchorPosY(-80f);
				this.textHowManyLeft.color = MerchantItem.COLOR_ITEMCOUNT_INSTOCK;
			}
			else
			{
				this.buttonUpgradeAnim.textCantAffordColorChangeManual = false;
				this.textPrice.gameObject.SetActive(false);
				this.textHowManyLeft.rectTransform.SetAnchorPosY(-240f);
				this.textHowManyLeft.color = MerchantItem.COLOR_ITEMCOUNT_SOLDOUT;
			}
		}

		public Text textTitle;

		public GameButton gameButton;

		public ButtonUpgradeAnim buttonUpgradeAnim;

		public Text priceWithoutIcon;

		public Text textHowManyLeft;

		public Text textPrice;

		public Image iconCoin;

		public Image imageItem;

		public Image image;

		public Image glowImage;

		public Color colorHowManyLeft;

		public float yTextSoldOut;

		public Sprite spriteBgNormal;

		public Image buttonRef;

		public Image background;

		public ContentSizeFitter contentSizeFitter;

		public GameObject eventOrnament;

		private static Color COLOR_ITEMCOUNT_INSTOCK = Utility.HexColor("5F707B");

		private static Color COLOR_ITEMCOUNT_SOLDOUT = Utility.HexColor("392A1A");

		public RectTransform unlockHintParent;

		public Text unlockHintText;
	}
}
