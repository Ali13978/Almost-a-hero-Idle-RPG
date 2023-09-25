using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class CharmSelectWidget : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.SetCarPositions(false);
		}

		public void DoAddAnimation()
		{
			if (this.t == null || !this.t.IsPlaying())
			{
				this.t = this.button.text.transform.DOPunchScale(Vector3.one * 0.2f, 1f, 8, 1f);
			}
			else
			{
				this.t.Goto(0f, false);
			}
		}

		public void DoActivity(bool isActive, float delay = 0f)
		{
			this.isActive = isActive;
			this.disabledForeground.gameObject.SetActive(!isActive);
			Sequence s = DOTween.Sequence();
			float num = (float)((!isActive) ? -10 : 15);
			for (int i = 0; i < this.cards.Length; i++)
			{
				Image image = this.cards[i];
				if (i < this.cardCount)
				{
					image.gameObject.SetActive(true);
					s.Insert((float)i * 0.1f, image.rectTransform.DOAnchorPos(this.GetCardPosition(i, num), 0.2f, false).SetEase(Ease.OutBack));
					s.Join(image.rectTransform.DORotate(new Vector3(image.rectTransform.localEulerAngles.x, image.rectTransform.localEulerAngles.y, this.GetCardAngle(i)), 0.2f, RotateMode.Fast));
				}
				else
				{
					image.gameObject.SetActive(false);
				}
			}
			s.SetDelay(delay).Play<Sequence>();
		}

		public void SetCarPositions(bool isActive)
		{
			this.isActive = isActive;
			this.disabledForeground.gameObject.SetActive(!isActive);
			float num = (float)((!isActive) ? -10 : 15);
			for (int i = 0; i < this.cards.Length; i++)
			{
				Image image = this.cards[i];
				if (i < this.cardCount)
				{
					image.gameObject.SetActive(true);
					image.rectTransform.anchoredPosition = this.GetCardPosition(i, num);
					image.rectTransform.localEulerAngles = new Vector3(image.rectTransform.localEulerAngles.x, image.rectTransform.localEulerAngles.y, this.GetCardAngle(i));
				}
				else
				{
					image.gameObject.SetActive(false);
				}
			}
		}

		private Vector2 GetCardPosition(int index, float radius)
		{
			float num = 0.436332315f * (float)this.cardCount;
			float num2 = -num / 2f;
			return CharmSelectWidget.GetCirclePos(num2 + (float)index * 0.436332315f) * radius;
		}

		private float GetCardAngle(int index)
		{
			float num = -12f * (float)this.cardCount;
			float num2 = -10f - num / 2f;
			return num2 + (float)index * -12f;
		}

		public static Vector2 GetCirclePos(float rad)
		{
			return new Vector2(Mathf.Sin(rad), Mathf.Cos(rad));
		}

		public GameButton button;

		public Image fill;

		public int lastNumCount;

		public Image[] cards;

		public int cardCount = 3;

		public bool isActive;

		public Image disabledForeground;

		public float radius = 30f;

		private Tweener t;
	}
}
