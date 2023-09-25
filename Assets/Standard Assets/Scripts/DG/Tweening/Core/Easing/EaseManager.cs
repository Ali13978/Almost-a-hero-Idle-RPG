using System;
using System.Runtime.CompilerServices;

namespace DG.Tweening.Core.Easing
{
	public static class EaseManager
	{
		public static float Evaluate(Tween t, float time, float duration, float overshootOrAmplitude, float period)
		{
			return EaseManager.Evaluate(t.easeType, t.customEase, time, duration, overshootOrAmplitude, period);
		}

		public static float Evaluate(Ease easeType, EaseFunction customEase, float time, float duration, float overshootOrAmplitude, float period)
		{
			switch (easeType)
			{
			case Ease.Linear:
				return time / duration;
			case Ease.InSine:
				return -(float)Math.Cos((double)(time / duration * 1.57079637f)) + 1f;
			case Ease.OutSine:
				return (float)Math.Sin((double)(time / duration * 1.57079637f));
			case Ease.InOutSine:
				return -0.5f * ((float)Math.Cos((double)(3.14159274f * time / duration)) - 1f);
			case Ease.InQuad:
				return (time /= duration) * time;
			case Ease.OutQuad:
				return -(time /= duration) * (time - 2f);
			case Ease.InOutQuad:
				if ((time /= duration * 0.5f) < 1f)
				{
					return 0.5f * time * time;
				}
				return -0.5f * ((time -= 1f) * (time - 2f) - 1f);
			case Ease.InCubic:
				return (time /= duration) * time * time;
			case Ease.OutCubic:
				return (time = time / duration - 1f) * time * time + 1f;
			case Ease.InOutCubic:
				if ((time /= duration * 0.5f) < 1f)
				{
					return 0.5f * time * time * time;
				}
				return 0.5f * ((time -= 2f) * time * time + 2f);
			case Ease.InQuart:
				return (time /= duration) * time * time * time;
			case Ease.OutQuart:
				return -((time = time / duration - 1f) * time * time * time - 1f);
			case Ease.InOutQuart:
				if ((time /= duration * 0.5f) < 1f)
				{
					return 0.5f * time * time * time * time;
				}
				return -0.5f * ((time -= 2f) * time * time * time - 2f);
			case Ease.InQuint:
				return (time /= duration) * time * time * time * time;
			case Ease.OutQuint:
				return (time = time / duration - 1f) * time * time * time * time + 1f;
			case Ease.InOutQuint:
				if ((time /= duration * 0.5f) < 1f)
				{
					return 0.5f * time * time * time * time * time;
				}
				return 0.5f * ((time -= 2f) * time * time * time * time + 2f);
			case Ease.InExpo:
				return (time != 0f) ? ((float)Math.Pow(2.0, (double)(10f * (time / duration - 1f)))) : 0f;
			case Ease.OutExpo:
				if (time == duration)
				{
					return 1f;
				}
				return -(float)Math.Pow(2.0, (double)(-10f * time / duration)) + 1f;
			case Ease.InOutExpo:
				if (time == 0f)
				{
					return 0f;
				}
				if (time == duration)
				{
					return 1f;
				}
				if ((time /= duration * 0.5f) < 1f)
				{
					return 0.5f * (float)Math.Pow(2.0, (double)(10f * (time - 1f)));
				}
				return 0.5f * (-(float)Math.Pow(2.0, (double)(-10f * (time -= 1f))) + 2f);
			case Ease.InCirc:
				return -((float)Math.Sqrt((double)(1f - (time /= duration) * time)) - 1f);
			case Ease.OutCirc:
				return (float)Math.Sqrt((double)(1f - (time = time / duration - 1f) * time));
			case Ease.InOutCirc:
				if ((time /= duration * 0.5f) < 1f)
				{
					return -0.5f * ((float)Math.Sqrt((double)(1f - time * time)) - 1f);
				}
				return 0.5f * ((float)Math.Sqrt((double)(1f - (time -= 2f) * time)) + 1f);
			case Ease.InElastic:
			{
				if (time == 0f)
				{
					return 0f;
				}
				if ((time /= duration) == 1f)
				{
					return 1f;
				}
				if (period == 0f)
				{
					period = duration * 0.3f;
				}
				float num;
				if (overshootOrAmplitude < 1f)
				{
					overshootOrAmplitude = 1f;
					num = period / 4f;
				}
				else
				{
					num = period / 6.28318548f * (float)Math.Asin((double)(1f / overshootOrAmplitude));
				}
				return -(overshootOrAmplitude * (float)Math.Pow(2.0, (double)(10f * (time -= 1f))) * (float)Math.Sin((double)((time * duration - num) * 6.28318548f / period)));
			}
			case Ease.OutElastic:
			{
				if (time == 0f)
				{
					return 0f;
				}
				if ((time /= duration) == 1f)
				{
					return 1f;
				}
				if (period == 0f)
				{
					period = duration * 0.3f;
				}
				float num2;
				if (overshootOrAmplitude < 1f)
				{
					overshootOrAmplitude = 1f;
					num2 = period / 4f;
				}
				else
				{
					num2 = period / 6.28318548f * (float)Math.Asin((double)(1f / overshootOrAmplitude));
				}
				return overshootOrAmplitude * (float)Math.Pow(2.0, (double)(-10f * time)) * (float)Math.Sin((double)((time * duration - num2) * 6.28318548f / period)) + 1f;
			}
			case Ease.InOutElastic:
			{
				if (time == 0f)
				{
					return 0f;
				}
				if ((time /= duration * 0.5f) == 2f)
				{
					return 1f;
				}
				if (period == 0f)
				{
					period = duration * 0.450000018f;
				}
				float num3;
				if (overshootOrAmplitude < 1f)
				{
					overshootOrAmplitude = 1f;
					num3 = period / 4f;
				}
				else
				{
					num3 = period / 6.28318548f * (float)Math.Asin((double)(1f / overshootOrAmplitude));
				}
				if (time < 1f)
				{
					return -0.5f * (overshootOrAmplitude * (float)Math.Pow(2.0, (double)(10f * (time -= 1f))) * (float)Math.Sin((double)((time * duration - num3) * 6.28318548f / period)));
				}
				return overshootOrAmplitude * (float)Math.Pow(2.0, (double)(-10f * (time -= 1f))) * (float)Math.Sin((double)((time * duration - num3) * 6.28318548f / period)) * 0.5f + 1f;
			}
			case Ease.InBack:
				return (time /= duration) * time * ((overshootOrAmplitude + 1f) * time - overshootOrAmplitude);
			case Ease.OutBack:
				return (time = time / duration - 1f) * time * ((overshootOrAmplitude + 1f) * time + overshootOrAmplitude) + 1f;
			case Ease.InOutBack:
				if ((time /= duration * 0.5f) < 1f)
				{
					return 0.5f * (time * time * (((overshootOrAmplitude *= 1.525f) + 1f) * time - overshootOrAmplitude));
				}
				return 0.5f * ((time -= 2f) * time * (((overshootOrAmplitude *= 1.525f) + 1f) * time + overshootOrAmplitude) + 2f);
			case Ease.InBounce:
				return Bounce.EaseIn(time, duration, overshootOrAmplitude, period);
			case Ease.OutBounce:
				return Bounce.EaseOut(time, duration, overshootOrAmplitude, period);
			case Ease.InOutBounce:
				return Bounce.EaseInOut(time, duration, overshootOrAmplitude, period);
			case Ease.Flash:
				return Flash.Ease(time, duration, overshootOrAmplitude, period);
			case Ease.InFlash:
				return Flash.EaseIn(time, duration, overshootOrAmplitude, period);
			case Ease.OutFlash:
				return Flash.EaseOut(time, duration, overshootOrAmplitude, period);
			case Ease.InOutFlash:
				return Flash.EaseInOut(time, duration, overshootOrAmplitude, period);
			case Ease.INTERNAL_Zero:
				return 1f;
			case Ease.INTERNAL_Custom:
				return customEase(time, duration, overshootOrAmplitude, period);
			default:
				return -(time /= duration) * (time - 2f);
			}
		}

		public static EaseFunction ToEaseFunction(Ease ease)
		{
			switch (ease)
			{
			case Ease.Linear:
				return (float time, float duration, float overshootOrAmplitude, float period) => time / duration;
			case Ease.InSine:
				return (float time, float duration, float overshootOrAmplitude, float period) => -(float)Math.Cos((double)(time / duration * 1.57079637f)) + 1f;
			case Ease.OutSine:
				return (float time, float duration, float overshootOrAmplitude, float period) => (float)Math.Sin((double)(time / duration * 1.57079637f));
			case Ease.InOutSine:
				return (float time, float duration, float overshootOrAmplitude, float period) => -0.5f * ((float)Math.Cos((double)(3.14159274f * time / duration)) - 1f);
			case Ease.InQuad:
				return (float time, float duration, float overshootOrAmplitude, float period) => (time /= duration) * time;
			case Ease.OutQuad:
				return (float time, float duration, float overshootOrAmplitude, float period) => -(time /= duration) * (time - 2f);
			case Ease.InOutQuad:
				return delegate(float time, float duration, float overshootOrAmplitude, float period)
				{
					if ((time /= duration * 0.5f) < 1f)
					{
						return 0.5f * time * time;
					}
					return -0.5f * ((time -= 1f) * (time - 2f) - 1f);
				};
			case Ease.InCubic:
				return (float time, float duration, float overshootOrAmplitude, float period) => (time /= duration) * time * time;
			case Ease.OutCubic:
				return (float time, float duration, float overshootOrAmplitude, float period) => (time = time / duration - 1f) * time * time + 1f;
			case Ease.InOutCubic:
				return delegate(float time, float duration, float overshootOrAmplitude, float period)
				{
					if ((time /= duration * 0.5f) < 1f)
					{
						return 0.5f * time * time * time;
					}
					return 0.5f * ((time -= 2f) * time * time + 2f);
				};
			case Ease.InQuart:
				return (float time, float duration, float overshootOrAmplitude, float period) => (time /= duration) * time * time * time;
			case Ease.OutQuart:
				return (float time, float duration, float overshootOrAmplitude, float period) => -((time = time / duration - 1f) * time * time * time - 1f);
			case Ease.InOutQuart:
				return delegate(float time, float duration, float overshootOrAmplitude, float period)
				{
					if ((time /= duration * 0.5f) < 1f)
					{
						return 0.5f * time * time * time * time;
					}
					return -0.5f * ((time -= 2f) * time * time * time - 2f);
				};
			case Ease.InQuint:
				return (float time, float duration, float overshootOrAmplitude, float period) => (time /= duration) * time * time * time * time;
			case Ease.OutQuint:
				return (float time, float duration, float overshootOrAmplitude, float period) => (time = time / duration - 1f) * time * time * time * time + 1f;
			case Ease.InOutQuint:
				return delegate(float time, float duration, float overshootOrAmplitude, float period)
				{
					if ((time /= duration * 0.5f) < 1f)
					{
						return 0.5f * time * time * time * time * time;
					}
					return 0.5f * ((time -= 2f) * time * time * time * time + 2f);
				};
			case Ease.InExpo:
				return (float time, float duration, float overshootOrAmplitude, float period) => (time != 0f) ? ((float)Math.Pow(2.0, (double)(10f * (time / duration - 1f)))) : 0f;
			case Ease.OutExpo:
				return delegate(float time, float duration, float overshootOrAmplitude, float period)
				{
					if (time == duration)
					{
						return 1f;
					}
					return -(float)Math.Pow(2.0, (double)(-10f * time / duration)) + 1f;
				};
			case Ease.InOutExpo:
				return delegate(float time, float duration, float overshootOrAmplitude, float period)
				{
					if (time == 0f)
					{
						return 0f;
					}
					if (time == duration)
					{
						return 1f;
					}
					if ((time /= duration * 0.5f) < 1f)
					{
						return 0.5f * (float)Math.Pow(2.0, (double)(10f * (time - 1f)));
					}
					return 0.5f * (-(float)Math.Pow(2.0, (double)(-10f * (time -= 1f))) + 2f);
				};
			case Ease.InCirc:
				return (float time, float duration, float overshootOrAmplitude, float period) => -((float)Math.Sqrt((double)(1f - (time /= duration) * time)) - 1f);
			case Ease.OutCirc:
				return (float time, float duration, float overshootOrAmplitude, float period) => (float)Math.Sqrt((double)(1f - (time = time / duration - 1f) * time));
			case Ease.InOutCirc:
				return delegate(float time, float duration, float overshootOrAmplitude, float period)
				{
					if ((time /= duration * 0.5f) < 1f)
					{
						return -0.5f * ((float)Math.Sqrt((double)(1f - time * time)) - 1f);
					}
					return 0.5f * ((float)Math.Sqrt((double)(1f - (time -= 2f) * time)) + 1f);
				};
			case Ease.InElastic:
				return delegate(float time, float duration, float overshootOrAmplitude, float period)
				{
					if (time == 0f)
					{
						return 0f;
					}
					if ((time /= duration) == 1f)
					{
						return 1f;
					}
					if (period == 0f)
					{
						period = duration * 0.3f;
					}
					float num;
					if (overshootOrAmplitude < 1f)
					{
						overshootOrAmplitude = 1f;
						num = period / 4f;
					}
					else
					{
						num = period / 6.28318548f * (float)Math.Asin((double)(1f / overshootOrAmplitude));
					}
					return -(overshootOrAmplitude * (float)Math.Pow(2.0, (double)(10f * (time -= 1f))) * (float)Math.Sin((double)((time * duration - num) * 6.28318548f / period)));
				};
			case Ease.OutElastic:
				return delegate(float time, float duration, float overshootOrAmplitude, float period)
				{
					if (time == 0f)
					{
						return 0f;
					}
					if ((time /= duration) == 1f)
					{
						return 1f;
					}
					if (period == 0f)
					{
						period = duration * 0.3f;
					}
					float num;
					if (overshootOrAmplitude < 1f)
					{
						overshootOrAmplitude = 1f;
						num = period / 4f;
					}
					else
					{
						num = period / 6.28318548f * (float)Math.Asin((double)(1f / overshootOrAmplitude));
					}
					return overshootOrAmplitude * (float)Math.Pow(2.0, (double)(-10f * time)) * (float)Math.Sin((double)((time * duration - num) * 6.28318548f / period)) + 1f;
				};
			case Ease.InOutElastic:
				return delegate(float time, float duration, float overshootOrAmplitude, float period)
				{
					if (time == 0f)
					{
						return 0f;
					}
					if ((time /= duration * 0.5f) == 2f)
					{
						return 1f;
					}
					if (period == 0f)
					{
						period = duration * 0.450000018f;
					}
					float num;
					if (overshootOrAmplitude < 1f)
					{
						overshootOrAmplitude = 1f;
						num = period / 4f;
					}
					else
					{
						num = period / 6.28318548f * (float)Math.Asin((double)(1f / overshootOrAmplitude));
					}
					if (time < 1f)
					{
						return -0.5f * (overshootOrAmplitude * (float)Math.Pow(2.0, (double)(10f * (time -= 1f))) * (float)Math.Sin((double)((time * duration - num) * 6.28318548f / period)));
					}
					return overshootOrAmplitude * (float)Math.Pow(2.0, (double)(-10f * (time -= 1f))) * (float)Math.Sin((double)((time * duration - num) * 6.28318548f / period)) * 0.5f + 1f;
				};
			case Ease.InBack:
				return (float time, float duration, float overshootOrAmplitude, float period) => (time /= duration) * time * ((overshootOrAmplitude + 1f) * time - overshootOrAmplitude);
			case Ease.OutBack:
				return (float time, float duration, float overshootOrAmplitude, float period) => (time = time / duration - 1f) * time * ((overshootOrAmplitude + 1f) * time + overshootOrAmplitude) + 1f;
			case Ease.InOutBack:
				return delegate(float time, float duration, float overshootOrAmplitude, float period)
				{
					if ((time /= duration * 0.5f) < 1f)
					{
						return 0.5f * (time * time * (((overshootOrAmplitude *= 1.525f) + 1f) * time - overshootOrAmplitude));
					}
					return 0.5f * ((time -= 2f) * time * (((overshootOrAmplitude *= 1.525f) + 1f) * time + overshootOrAmplitude) + 2f);
				};
			case Ease.InBounce:
				if (EaseManager._003C_003Ef__mg_0024cache0 == null)
				{
					EaseManager._003C_003Ef__mg_0024cache0 = new EaseFunction(Bounce.EaseIn);
				}
				return EaseManager._003C_003Ef__mg_0024cache0;
			case Ease.OutBounce:
				if (EaseManager._003C_003Ef__mg_0024cache1 == null)
				{
					EaseManager._003C_003Ef__mg_0024cache1 = new EaseFunction(Bounce.EaseOut);
				}
				return EaseManager._003C_003Ef__mg_0024cache1;
			case Ease.InOutBounce:
				if (EaseManager._003C_003Ef__mg_0024cache2 == null)
				{
					EaseManager._003C_003Ef__mg_0024cache2 = new EaseFunction(Bounce.EaseInOut);
				}
				return EaseManager._003C_003Ef__mg_0024cache2;
			case Ease.Flash:
				if (EaseManager._003C_003Ef__mg_0024cache3 == null)
				{
					EaseManager._003C_003Ef__mg_0024cache3 = new EaseFunction(Flash.Ease);
				}
				return EaseManager._003C_003Ef__mg_0024cache3;
			case Ease.InFlash:
				if (EaseManager._003C_003Ef__mg_0024cache4 == null)
				{
					EaseManager._003C_003Ef__mg_0024cache4 = new EaseFunction(Flash.EaseIn);
				}
				return EaseManager._003C_003Ef__mg_0024cache4;
			case Ease.OutFlash:
				if (EaseManager._003C_003Ef__mg_0024cache5 == null)
				{
					EaseManager._003C_003Ef__mg_0024cache5 = new EaseFunction(Flash.EaseOut);
				}
				return EaseManager._003C_003Ef__mg_0024cache5;
			case Ease.InOutFlash:
				if (EaseManager._003C_003Ef__mg_0024cache6 == null)
				{
					EaseManager._003C_003Ef__mg_0024cache6 = new EaseFunction(Flash.EaseInOut);
				}
				return EaseManager._003C_003Ef__mg_0024cache6;
			default:
				return (float time, float duration, float overshootOrAmplitude, float period) => -(time /= duration) * (time - 2f);
			}
		}

		internal static bool IsFlashEase(Ease ease)
		{
			switch (ease)
			{
			case Ease.Flash:
			case Ease.InFlash:
			case Ease.OutFlash:
			case Ease.InOutFlash:
				return true;
			default:
				return false;
			}
		}

		private const float _PiOver2 = 1.57079637f;

		private const float _TwoPi = 6.28318548f;

		[CompilerGenerated]
		private static EaseFunction _003C_003Ef__mg_0024cache0;

		[CompilerGenerated]
		private static EaseFunction _003C_003Ef__mg_0024cache1;

		[CompilerGenerated]
		private static EaseFunction _003C_003Ef__mg_0024cache2;

		[CompilerGenerated]
		private static EaseFunction _003C_003Ef__mg_0024cache3;

		[CompilerGenerated]
		private static EaseFunction _003C_003Ef__mg_0024cache4;

		[CompilerGenerated]
		private static EaseFunction _003C_003Ef__mg_0024cache5;

		[CompilerGenerated]
		private static EaseFunction _003C_003Ef__mg_0024cache6;
	}
}
