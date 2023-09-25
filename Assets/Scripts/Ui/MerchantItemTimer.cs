using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class MerchantItemTimer : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.fadingOut = true;
			this.animTimer = this.animPeriod;
		}

		public override void AahUpdate(float dt)
		{
			if (this.imageIcon != null)
			{
				if (this.animTimer < this.animPeriod)
				{
					this.animTimer += dt;
					float num = this.animTimer / this.animPeriod;
					if (this.fadingOut)
					{
						num = 1f - num;
					}
					this.text.color = new Color(this.text.color.r, this.text.color.g, this.text.color.b, num);
					this.imageIcon.color = new Color(this.imageIcon.color.r, this.imageIcon.color.g, this.imageIcon.color.b, num);
					this.image.rectTransform.anchoredPosition = new Vector2(this.bgDeltaX + (1f - num) * this.image.rectTransform.sizeDelta.x, 0f);
				}
				else
				{
					this.text.color = new Color(this.text.color.r, this.text.color.g, this.text.color.b, 1f);
					this.imageIcon.color = new Color(this.imageIcon.color.r, this.imageIcon.color.g, this.imageIcon.color.b, 1f);
					this.image.rectTransform.anchoredPosition = new Vector2(this.bgDeltaX, 0f);
				}
			}
		}

		public void SetTime(float time, float timeMax)
		{
			if (this.imageAnimatedCircle)
			{
				this.imageAnimatedCircle.fillAmount = time / timeMax;
			}
			this.SetTime(time);
		}

		public void SetTime(float time)
		{
			this.text.text = GameMath.GetTimeString(Mathf.Max(0f, time));
		}

		public void SetItemType(Type itemType)
		{
			this.itemType = itemType;
		}

		public void FadeInAnim()
		{
			if (this.fadingOut)
			{
				this.fadingOut = false;
				this.animTimer = 0f;
			}
		}

		public void FadeOutAnim()
		{
			if (!this.fadingOut)
			{
				this.fadingOut = true;
				this.animTimer = 0f;
			}
		}

		public bool IsStillActive()
		{
			return !this.fadingOut || this.animTimer < this.animPeriod;
		}

		public bool IsFadingOut()
		{
			return this.fadingOut && this.animTimer < this.animPeriod;
		}

		public Image image;

		public RectMask2D rectMask;

		public Image imageAnimatedCircle;

		public Image imageIcon;

		public Text text;

		public CanvasGroup canvasGroup;

		public RectTransform rectTransform;

		private bool fadingOut;

		private float animTimer;

		private float animPeriod = 0.4f;

		public float bgDeltaX;

		public Type itemType;
	}
}
