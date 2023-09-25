using System;
using DG.Tweening;
using UnityEngine;

namespace Ui
{
	public class TabMenuAnim : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.rect = base.GetComponent<RectTransform>();
			this.rect.anchoredPosition = Vector2.up * this.downPositionY;
		}

		public void SetSize()
		{
			float height = this.canvasRecttransform.rect.height;
			this.rect.SetSizeDeltaY(height - this.upPositionY);
		}

		public void Open(bool fullScreen = false)
		{
			if (this.isOpen)
			{
				return;
			}
			base.gameObject.SetActive(true);
			DOTween.Kill(this.rect, false);
			this.isAnimating = true;
			Tweener tweener;
			if (fullScreen)
			{
				tweener = this.rect.DOAnchorPosY(this.upPositionFullscreenY, this.unitPerSec, false).SetEase(Ease.OutQuint);
			}
			else
			{
				tweener = this.rect.DOAnchorPosY(0f, this.unitPerSec, false).SetEase(Ease.OutQuint);
			}
			tweener.SetSpeedBased<Tweener>();
			tweener.OnComplete(delegate
			{
				this.isAnimating = false;
			});
			this.isOpen = true;
		}

		public void Close()
		{
			if (!this.isOpen)
			{
				return;
			}
			this.isAnimating = true;
			DOTween.Kill(this.rect, false);
			this.rect.DOAnchorPosY(this.downPositionY, this.unitPerSec, false).SetEase(Ease.OutSine).SetSpeedBased<Tweener>().OnComplete(delegate
			{
				this.isAnimating = false;
				base.gameObject.SetActive(false);
			});
			this.isOpen = false;
		}

		public bool isAnimating;

		private RectTransform rect;

		private float downPositionY = -1640f;

		private float upPositionY = 113f;

		private float upPositionFullscreenY = 180f;

		private float unitPerSec = 5000f;

		private bool isOpen;

		public RectTransform canvasRecttransform;
	}
}
