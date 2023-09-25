using System;

namespace DG.Tweening.Core
{
	internal class SequenceCallback : ABSSequentiable
	{
		public SequenceCallback(float sequencedPosition, TweenCallback callback)
		{
			this.tweenType = TweenType.Callback;
			this.sequencedPosition = sequencedPosition;
			this.onStart = callback;
		}
	}
}
