using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class DailyQuestIndicator : MonoBehaviour
	{
		public RectTransform RectTransform { get; private set; }

		public void PopAnim()
		{
			if (this.tween != null && this.tween.isPlaying)
			{
				this.tween.Goto(0f, true);
			}
			else
			{
				this.tween = DOTween.Sequence();
				this.buttonAnimSize = this.button.deltaScaleOnDown;
				this.button.deltaScaleOnDown = 0f;
				this.tween.Append(base.transform.DOPunchScale(new Vector3(0.1f, 0.2f, 0.1f), 0.2f, 10, 1f));
				if (this.dailyFillWhite != null)
				{
					this.dailyFillWhite.fillAmount = this.dailyFill.fillAmount - 0.01f;
					this.dailyFillWhite.color = new Color(1f, 1f, 1f, 1f);
					this.tween.Join(this.dailyFillWhite.DOFillAmount(this.dailyFill.fillAmount + 0.02f, 0.1f).SetEase(Ease.OutQuad)).Join(this.dailyFillWhite.DOFade(1f, 0.15f).SetEase(Ease.InQuart)).Insert(0.2f, this.dailyFillWhite.DOFade(0f, 0.05f));
				}
				this.tween.OnComplete(delegate
				{
					this.button.deltaScaleOnDown = this.buttonAnimSize;
				});
				this.tween.Play<Sequence>();
			}
		}

		private void Awake()
		{
			this.RectTransform = (base.transform as RectTransform);
		}

		public GameButton button;

		public Image dailyIcon;

		public Image dailyFill;

		public Image dailyFillWhite;

		public Sprite iconNormal;

		public Sprite iconDisabled;

		public GameObject checkMark;

		public int lastProgress;

		private float buttonAnimSize;

		private Sequence tween;
	}
}
