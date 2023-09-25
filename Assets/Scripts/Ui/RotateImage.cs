using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	[Serializable]
	public class RotateImage : LoadingTransitionAnim
	{
		public override Tween GetFadeInAnimation()
		{
			this.image.gameObject.SetActive(true);
			this.image.rectTransform.rotation = Quaternion.identity;
			this.image.rectTransform.SetScale(0f);
			Sequence result = DOTween.Sequence().Append(this.image.DOFade(1f, 0.2f)).Join(this.image.rectTransform.DOScale(1f, 0.2f));
			this.currentLoopingAnimation = DOTween.Sequence().Append(this.image.rectTransform.DORotate(new Vector3(0f, 0f, 360f), 1f, RotateMode.FastBeyond360)).SetLoops(-1).Play<Sequence>();
			return result;
		}

		public override Tween GetFadeOutAnimation()
		{
			return DOTween.Sequence().Append(this.image.DOFade(0f, 0.2f)).Join(this.image.rectTransform.DOScale(2f, 0.2f));
		}

		public override Tween GetLoopAnimation()
		{
			return this.currentLoopingAnimation;
		}

		public override void OnComplete()
		{
			this.image.gameObject.SetActive(false);
			this.currentLoopingAnimation.Kill(false);
		}

		public Image image;
	}
}
