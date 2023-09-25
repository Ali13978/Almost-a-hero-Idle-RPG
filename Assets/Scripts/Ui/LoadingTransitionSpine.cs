using System;
using DG.Tweening;
using Spine;
using Spine.Unity;

namespace Ui
{
	[Serializable]
	public class LoadingTransitionSpine : LoadingTransitionAnim
	{
		public void Init()
		{
			ExposedList<Animation> exposedList = this.skeletonGraphic.SkeletonDataAsset.GetSkeletonData(true).animations;
			this.animations = new Animation[exposedList.Count];
			exposedList.CopyTo(this.animations);
		}

		public override Tween GetFadeInAnimation()
		{
			Animation randomArrayElement = this.animations.GetRandomArrayElement<Animation>();
			this.skeletonGraphic.gameObject.SetActive(true);
			this.skeletonGraphic.rectTransform.SetAnchorPosY(-50f);
			Sequence sequence = DOTween.Sequence().Append(this.skeletonGraphic.DOFade(1f, 0.2f)).Join(this.skeletonGraphic.rectTransform.DOAnchorPosY(0f, 0.2f, false));
			if (!this.loop && randomArrayElement.duration > 0.2f)
			{
				sequence.AppendInterval(randomArrayElement.duration - 0.2f);
			}
			this.skeletonGraphic.AnimationState.SetAnimation(0, this.animations.GetRandomArrayElement<Animation>(), this.loop);
			this.currentLoopingAnimation = DOTween.Sequence().AppendInterval(1f).SetLoops(-1).Play<Sequence>();
			return sequence;
		}

		public override Tween GetFadeOutAnimation()
		{
			return DOTween.Sequence().Append(this.skeletonGraphic.DOFade(0f, 0.2f)).Join(this.skeletonGraphic.rectTransform.DOAnchorPosY(50f, 0.2f, false));
		}

		public override Tween GetLoopAnimation()
		{
			return this.currentLoopingAnimation;
		}

		public override void OnComplete()
		{
			this.skeletonGraphic.gameObject.SetActive(false);
			this.currentLoopingAnimation.Kill(false);
		}

		public string name;

		public SkeletonGraphic skeletonGraphic;

		public Animation[] animations;

		public bool loop;
	}
}
