using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class MenuShowCurrency : AahMonoBehaviour
	{
		public Transform GetCurrencyTransform()
		{
			return this.imageCurrency.transform;
		}

		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			if (this.text == null)
			{
				this.text = base.GetComponentInChildren<Text>();
			}
		}

		public void SetCurrency(CurrencyType v, string amount, bool setColor = true, GameMode coinType = GameMode.STANDARD, bool showIcon = true)
		{
			if (showIcon)
			{
				this.imageCurrency.gameObject.SetActive(true);
				if (coinType == GameMode.STANDARD)
				{
					Sprite sprite = UiData.inst.currencySprites[(int)v];
					if (this.imageCurrency.sprite != sprite)
					{
						this.imageCurrency.sprite = sprite;
						this.imageCurrency.SetNativeSize();
					}
				}
				else if (coinType == GameMode.CRUSADE)
				{
					this.imageCurrency.sprite = UiData.inst.spriteCurrencyGoldTriangle;
				}
				else if (coinType == GameMode.RIFT)
				{
					this.imageCurrency.sprite = UiData.inst.spriteCurrencyGoldSquare;
				}
				if (this.image != null && UiData.inst.currencyBgSprites.Length > 0)
				{
					this.image.sprite = UiData.inst.currencyBgSprites[(int)v];
				}
			}
			else
			{
				this.imageCurrency.gameObject.SetActive(false);
			}
			this.text.text = amount;
			if (setColor)
			{
				if (this.overrideColors)
				{
					if (this.textColors.Length > 0)
					{
						this.text.color = this.textColors[(int)v];
					}
				}
				else
				{
					this.text.color = UiManager.colorCurrencyTypes[(int)v];
				}
			}
		}

		public void SetTextX(float x)
		{
			this.text.rectTransform.SetAnchorPosX(x);
		}

		internal void SetCurrencyImageSize(int x, int y)
		{
			this.imageCurrency.rectTransform.sizeDelta = new Vector2((float)x, (float)y);
		}

		public void CenterAmountText()
		{
			this.text.alignment = TextAnchor.MiddleCenter;
		}

		[SerializeField]
		private Image imageCurrency;

		[SerializeField]
		private Image image;

		[SerializeField]
		private bool overrideColors;

		[SerializeField]
		private Color[] textColors;

		[SerializeField]
		private Text text;
	}
}
