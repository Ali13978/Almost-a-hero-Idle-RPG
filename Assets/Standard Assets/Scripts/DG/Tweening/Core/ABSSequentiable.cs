using System;

namespace DG.Tweening.Core
{
	public abstract class ABSSequentiable
	{
		public TweenType tweenType;

		internal float sequencedPosition;

		internal float sequencedEndPosition;

		internal TweenCallback onStart;
	}
}
