using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ShineLightAnimator : MonoBehaviour
	{
		public Tweener DoShine()
		{
			base.gameObject.SetActive(true);
			this.shineImage.rectTransform.anchoredPosition = this.startPosition;
			this.shineImage.rectTransform.localRotation = Quaternion.Euler(0f, 0f, this.angle);
			Vector2 endValue = -this.shineImage.rectTransform.up * this.travelDistance;
			return this.shineImage.rectTransform.DOAnchorPos(endValue, 0.4f, false).SetEase(Ease.InOutCubic).OnComplete(delegate
			{
				base.gameObject.SetActive(false);
			});
		}

		public Image shineImage;

		public Image maskImage;

		[Range(0f, 360f)]
		public float angle;

		public float thicknes;

		public float travelDistance;

		public Vector2 startPosition;
	}
}
