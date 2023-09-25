using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	[Serializable]
	public class SwordAnimation : LoadingTransitionAnim
	{
		public override Tween GetFadeInAnimation()
		{
			this.swordAnimationParent.gameObject.SetActive(true);
			this.swordAnimationCanvas.alpha = 0f;
			this.swordAnimationParent.SetAnchorPosY(-50f);
			this.sword1.rectTransform.localRotation = Quaternion.Euler(0f, 0f, -120f);
			this.sword2.rectTransform.localRotation = Quaternion.Euler(0f, 0f, 120f);
			this.particleCanvas.alpha = 0f;
			this.particleCanvas.transform.SetScale(1f);
			return DOTween.Sequence().Append(this.swordAnimationCanvas.DOFade(1f, 0.2f)).Join(this.swordAnimationParent.DOAnchorPosY(0f, 0.2f, false).SetEase(Ease.OutQuart)).Join(this.sword1.rectTransform.DORotate(new Vector3(0f, 0f, 25f), 0.2f, RotateMode.Fast).SetEase(Ease.OutQuart)).Join(this.sword2.rectTransform.DORotate(new Vector3(0f, 0f, -25f), 0.2f, RotateMode.Fast).SetEase(Ease.OutQuart)).Join(this.sword1.rectTransform.DOAnchorPosX(-120f, 0.2f, false).SetEase(Ease.OutQuart)).Join(this.sword2.rectTransform.DOAnchorPosX(120f, 0.2f, false).SetEase(Ease.OutQuart)).Append(this.sword1.rectTransform.DORotate(new Vector3(0f, 0f, -45f), 0.2f, RotateMode.Fast).SetEase(Ease.InQuart)).Join(this.sword1.rectTransform.DOAnchorPosX(-53f, 0.2f, false).SetEase(Ease.InQuart)).Join(this.sword2.rectTransform.DORotate(new Vector3(0f, 0f, 45f), 0.2f, RotateMode.Fast).SetEase(Ease.InQuart)).Join(this.sword2.rectTransform.DOAnchorPosX(53f, 0.2f, false).SetEase(Ease.InQuart)).AppendCallback(delegate
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTransitionAppear, 1f));
			}).Append(this.particleCanvas.DOFade(1f, 0.01f)).Append(this.particleCanvas.transform.DOScale(1.4f, 0.2f).SetEase(Ease.OutQuart)).Join(this.particleCanvas.DOFade(0f, 0.1f)).SetDelay(0.2f).Play<Sequence>();
		}

		public override Tween GetFadeOutAnimation()
		{
			return this.swordAnimationCanvas.DOFade(0f, 0.05f);
		}

		public override Tween GetLoopAnimation()
		{
			this.currentLoopingAnimation = null;
			return this.currentLoopingAnimation;
		}

		public override void OnComplete()
		{
			this.swordAnimationParent.gameObject.SetActive(false);
		}

		public RectTransform swordAnimationParent;

		public CanvasGroup swordAnimationCanvas;

		public CanvasGroup particleCanvas;

		public Image sword1;

		public Image sword2;
	}
}
