using System;
using DG.Tweening.Core.Easing;
using UnityEngine;

namespace DG.Tweening
{
	public class EaseFactory
	{
		public static EaseFunction StopMotion(int motionFps, Ease? ease = null)
		{
			EaseFunction customEase = EaseManager.ToEaseFunction((ease != null) ? ease.Value : DOTween.defaultEaseType);
			return EaseFactory.StopMotion(motionFps, customEase);
		}

		public static EaseFunction StopMotion(int motionFps, AnimationCurve animCurve)
		{
			return EaseFactory.StopMotion(motionFps, new EaseFunction(new EaseCurve(animCurve).Evaluate));
		}

		public static EaseFunction StopMotion(int motionFps, EaseFunction customEase)
		{
			float motionDelay = 1f / (float)motionFps;
			return delegate(float time, float duration, float overshootOrAmplitude, float period)
			{
				float time2 = (time >= duration) ? time : (time - time % motionDelay);
				return customEase(time2, duration, overshootOrAmplitude, period);
			};
		}

		public static EaseFunction Kick(Ease inEase = Ease.InSine, Ease outEase = Ease.InSine)
		{
			return delegate(float time, float duration, float overshootOrAmplitude, float period)
			{
				if (time < duration * 0.5f)
				{
					return EaseManager.Evaluate(inEase, null, time, duration * 0.5f, overshootOrAmplitude, period);
				}
				return EaseManager.Evaluate(outEase, null, (duration - time) * 2f, duration, overshootOrAmplitude, period);
			};
		}
	}
}
