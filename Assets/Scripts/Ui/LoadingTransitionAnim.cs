using System;
using DG.Tweening;

namespace Ui
{
	public abstract class LoadingTransitionAnim
	{
		public abstract Tween GetFadeInAnimation();

		public abstract Tween GetFadeOutAnimation();

		public abstract Tween GetLoopAnimation();

		public abstract void OnComplete();

		public Tween currentLoopingAnimation;
	}
}
