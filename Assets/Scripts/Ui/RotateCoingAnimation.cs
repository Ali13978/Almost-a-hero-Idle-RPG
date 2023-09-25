using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	[Serializable]
	public class RotateCoingAnimation : LoadingTransitionAnim
	{
		public override Tween GetFadeInAnimation()
		{
			this.parent.gameObject.SetActive(true);
			this.parent.SetAnchorPosY(-50f);
			Sequence result = DOTween.Sequence().Append(this.parentCanvas.DOFade(1f, 0.2f)).Join(this.parent.DOAnchorPosY(0f, 0.2f, false));
			this.currentLoopingAnimation = DOTween.Sequence().AppendCallback(new TweenCallback(this.StepSprite)).AppendInterval(1f / (float)this.fps).SetLoops(-1).Play<Sequence>();
			return result;
		}

		public override Tween GetFadeOutAnimation()
		{
			return DOTween.Sequence().Append(this.parentCanvas.DOFade(0f, 0.2f)).Join(this.parent.DOAnchorPosY(50f, 0.2f, false));
		}

		public override Tween GetLoopAnimation()
		{
			return this.currentLoopingAnimation;
		}

		private void StepSprite()
		{
			this.currentSpriteIndex++;
			if (this.currentSpriteIndex >= this.keyframes.Length)
			{
				this.currentSpriteIndex = 0;
			}
			RotateCoingAnimation.Keyframe keyframe = this.keyframes[this.currentSpriteIndex];
			this.image.rectTransform.SetScaleX((float)((!keyframe.isSwapped) ? 1 : -1));
			this.image.sprite = this.keyframes[this.currentSpriteIndex].sprite;
			this.image.SetNativeSize();
		}

		public override void OnComplete()
		{
			this.parent.gameObject.SetActive(false);
			this.currentLoopingAnimation.Kill(false);
		}

		public RectTransform parent;

		public CanvasGroup parentCanvas;

		public Image image;

		public RotateCoingAnimation.Keyframe[] keyframes;

		public int swapIndex;

		public int fps;

		private int currentSpriteIndex;

		[Serializable]
		public struct Keyframe
		{
			public Sprite sprite;

			public bool isSwapped;
		}
	}
}
