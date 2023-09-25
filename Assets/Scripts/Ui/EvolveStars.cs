using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class EvolveStars : AahMonoBehaviour
	{
		public void SetNumberOfStars(int numStars, int maxStars)
		{
			for (int i = 0; i < this.stars.Length; i++)
			{
				this.stars[i].sprite = ((i >= numStars) ? this.starEmpty : this.starFull);
				this.stars[i].gameObject.SetActive(i < maxStars);
			}
		}

		public void SetNumberOfStarsAnimated(int numStars, int maxStars)
		{
			for (int i = 0; i < this.stars.Length; i++)
			{
				Image image = this.stars[i];
				image.sprite = ((i >= numStars) ? this.starEmpty : this.starFull);
				image.gameObject.SetActive(i < maxStars);
				if (i == numStars - 1)
				{
					this.imageEmpty.gameObject.SetActive(true);
					this.imageEmpty.transform.position = image.transform.position;
					image.transform.localScale = Vector3.zero;
					image.transform.DOScale(1f, 0.6f).SetEase(Ease.OutElastic, 1.2f, 0.5f).OnComplete(delegate
					{
						this.imageEmpty.gameObject.SetActive(false);
					});
				}
			}
		}

		public Sequence SetNumberOfStarsAnimatedFancy(int numStars, int maxStars, TweenCallback OnScreenShake)
		{
			Sequence result = null;
			for (int i = 0; i < this.stars.Length; i++)
			{
				Image image = this.stars[i];
				image.sprite = ((i >= numStars) ? this.starEmpty : this.starFull);
				image.gameObject.SetActive(i < maxStars);
				if (i == numStars - 1)
				{
					this.imageEmpty.gameObject.SetActive(true);
					this.imageEmpty.rectTransform.anchoredPosition = image.rectTransform.anchoredPosition;
					image.transform.rotation = Quaternion.Euler(0f, 0f, -10f);
					Vector2 anchoredPosition = new Vector2(-400f, -20f);
					Vector2 endValue = new Vector2(-50f, -120f);
					Vector2 anchoredPosition2 = image.rectTransform.anchoredPosition;
					image.rectTransform.anchoredPosition = anchoredPosition;
					image.transform.localScale = Vector3.zero;
					Sequence sequence = DOTween.Sequence();
					sequence.Append(image.rectTransform.DOJumpAnchorPos(endValue, -150f, 1, 0.3f, false));
					sequence.Join(image.rectTransform.DOScale(2f, 0.3f).SetEase(Ease.OutSine));
					sequence.Append(image.rectTransform.DORotate(new Vector3(0f, 0f, 10f), 0.6f, RotateMode.Fast).SetEase(Ease.OutSine));
					sequence.Join(image.rectTransform.DOAnchorPosX(-45f, 0.6f, false).SetEase(Ease.Linear));
					sequence.Insert(0.75f, image.rectTransform.DOAnchorPos(anchoredPosition2, 0.2f, false).SetEase(Ease.InQuint));
					sequence.Insert(0.75f, image.rectTransform.DOScale(1f, 0.2f).SetEase(Ease.InQuint));
					sequence.Insert(0.9f, image.rectTransform.DORotate(new Vector3(0f, 0f, 0f), 0.05f, RotateMode.Fast).SetEase(Ease.InQuint));
					sequence.AppendCallback(delegate
					{
						this.imageEmpty.gameObject.SetActive(false);
						OnScreenShake();
					});
					sequence.Append(image.rectTransform.DOPunchScale(-Vector3.one * 0.2f, 0.5f, 7, 1f));
					sequence.Play<Sequence>();
					result = sequence;
				}
			}
			return result;
		}

		[SerializeField]
		private Image[] stars;

		[SerializeField]
		private Sprite starEmpty;

		[SerializeField]
		private Sprite starFull;

		[SerializeField]
		private Image imageEmpty;
	}
}
