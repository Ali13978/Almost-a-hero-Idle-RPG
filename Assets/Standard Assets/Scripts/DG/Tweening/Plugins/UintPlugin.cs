using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening.Plugins
{
	public class UintPlugin : ABSTweenPlugin<uint, uint, UintOptions>
	{
		public override void Reset(TweenerCore<uint, uint, UintOptions> t)
		{
		}

		public override void SetFrom(TweenerCore<uint, uint, UintOptions> t, bool isRelative)
		{
			uint endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = ((!isRelative) ? endValue : (t.endValue + endValue));
			t.setter(t.startValue);
		}

		public override uint ConvertToStartValue(TweenerCore<uint, uint, UintOptions> t, uint value)
		{
			return value;
		}

		public override void SetRelativeEndValue(TweenerCore<uint, uint, UintOptions> t)
		{
			t.endValue += t.startValue;
		}

		public override void SetChangeValue(TweenerCore<uint, uint, UintOptions> t)
		{
			t.plugOptions.isNegativeChangeValue = (t.endValue < t.startValue);
			t.changeValue = ((!t.plugOptions.isNegativeChangeValue) ? (t.endValue - t.startValue) : (t.startValue - t.endValue));
		}

		public override float GetSpeedBasedDuration(UintOptions options, float unitsXSecond, uint changeValue)
		{
			float num = changeValue / unitsXSecond;
			if (num < 0f)
			{
				num = -num;
			}
			return num;
		}

		public override void EvaluateAndApply(UintOptions options, Tween t, bool isRelative, DOGetter<uint> getter, DOSetter<uint> setter, float elapsed, uint startValue, uint changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			uint num;
			if (t.loopType == LoopType.Incremental)
			{
				num = (uint)((ulong)changeValue * (ulong)((long)((!t.isComplete) ? t.completedLoops : (t.completedLoops - 1))));
				if (options.isNegativeChangeValue)
				{
					startValue -= num;
				}
				else
				{
					startValue += num;
				}
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				num = (uint)((ulong)changeValue * (ulong)((long)((t.loopType != LoopType.Incremental) ? 1 : t.loops)) * (ulong)((long)((!t.sequenceParent.isComplete) ? t.sequenceParent.completedLoops : (t.sequenceParent.completedLoops - 1))));
				if (options.isNegativeChangeValue)
				{
					startValue -= num;
				}
				else
				{
					startValue += num;
				}
			}
			num = (uint)Math.Round((double)(changeValue * EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod)));
			if (options.isNegativeChangeValue)
			{
				setter(startValue - num);
			}
			else
			{
				setter(startValue + num);
			}
		}
	}
}
