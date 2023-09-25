using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	[Serializable]
	public class HoopingAnimation : LoadingTransitionAnim
	{
		public override Tween GetFadeInAnimation()
		{
			this.image.gameObject.SetActive(true);
			this.image.rectTransform.localScale = new Vector3(this.upScale.x, this.upScale.y, 1f);
			this.image.rectTransform.SetAnchorPosY(200f);
			this.image.rectTransform.SetAnchorPosX(400f);
			this.image.rectTransform.rotation = Quaternion.Euler(0f, 0f, -126f);
			Sequence sequence = DOTween.Sequence().Append(this.image.DOFade(1f, 0.2f)).Join(this.image.rectTransform.DOAnchorPosX(0f, 0.2f, false)).Join(this.image.rectTransform.DOAnchorPosY(-92f, 0.2f, false).SetEase(Ease.InCubic)).Join(this.image.rectTransform.DORotate(new Vector3(0f, 0f, 0f), 0.2f, RotateMode.Fast).SetEase(Ease.InCubic)).Join(this.image.rectTransform.DOScale(new Vector3(this.downScale.x, this.downScale.y, 1f), 0.2f).SetEase(Ease.InCubic));
			if (this.playDuckSound)
			{
				sequence.AppendCallback(delegate
				{
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiDuck, 1f));
				});
			}
			sequence.Append(this.image.rectTransform.DOScale(new Vector3(this.upScale.x, this.upScale.y, 1f), 0.2f).SetEase(Ease.OutCubic)).Play<Sequence>();
			return sequence;
		}

		public override Tween GetFadeOutAnimation()
		{
			return DOTween.Sequence().Append(this.image.DOFade(0f, 0.2f)).Join(this.image.rectTransform.DOAnchorPosX(-400f, 0.2f, false)).Join(this.image.rectTransform.DOAnchorPosY(200f, 0.2f, false).SetEase(Ease.OutCubic)).Join(this.image.rectTransform.DORotate(new Vector3(0f, 0f, 126f), 0.2f, RotateMode.Fast).SetEase(Ease.OutCubic)).Join(this.image.rectTransform.DOScale(new Vector3(this.upScale.x, this.upScale.y, 1f), 0.2f).SetEase(Ease.OutCubic));
		}

		public override Tween GetLoopAnimation()
		{
			this.image.rectTransform.SetAnchorPosY(-92f);
			this.currentLoopingAnimation = DOTween.Sequence().Append(this.image.rectTransform.DOScale(new Vector3(this.downScale.x, this.downScale.y, 1f), 0.2f).SetEase(Ease.InCubic)).Append(this.image.rectTransform.DOScale(new Vector3(this.upScale.x, this.upScale.y, 1f), 0.2f).SetEase(Ease.OutCubic)).SetLoops(-1, LoopType.Restart).Play<Sequence>();
			return this.currentLoopingAnimation;
		}

		public override void OnComplete()
		{
			this.image.gameObject.SetActive(false);
		}

		public Image image;

		public Vector2 upScale;

		public Vector2 downScale;

		public bool playDuckSound;
	}
}
