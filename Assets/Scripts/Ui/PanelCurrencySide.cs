using System;
using UnityEngine;

namespace Ui
{
	public class PanelCurrencySide : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.timer = 5f;
			this.currencyType = CurrencyType.GOLD;
		}

		public override void AahUpdate(float dt)
		{
			if (this.timer < 5f)
			{
				this.timer += dt;
				if (this.timer >= 5f)
				{
					this.rectTransform.anchoredPosition = Vector2.zero;
					this.currencyType = CurrencyType.GOLD;
				}
				else if (this.timer < 0.3f)
				{
					this.rectTransform.anchoredPosition = new Vector2(Easing.SineEaseOut(this.timer, 0f, -230f, 0.3f), 0f);
				}
				else if (this.timer < 4.7f)
				{
					this.rectTransform.anchoredPosition = new Vector2(-230f, 0f);
				}
				else
				{
					this.rectTransform.anchoredPosition = new Vector2(Easing.SineEaseOut(5f - this.timer, 0f, -230f, 0.3f), 0f);
				}
			}
		}

		public void SetCurrency(CurrencyType v, string amount)
		{
			this.panelCurrency.SetCurrency(v, amount, true, GameMode.STANDARD, true);
			if (v != this.currencyType)
			{
				this.currencyType = v;
				if (this.timer >= 5f)
				{
					this.timer = 0f;
				}
				else if (this.timer >= 0.3f)
				{
					this.timer = 0.3f;
				}
			}
		}

		public MenuShowCurrency panelCurrency;

		public RectTransform rectTransform;

		public RectTransform currencyFinalPosReference;

		public float timer;

		private const float MovePeriod = 0.3f;

		public const float Period = 5f;

		public CurrencyType currencyType;

		public const float XOffset = -230f;
	}
}
