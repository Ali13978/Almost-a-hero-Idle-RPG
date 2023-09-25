using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	public class Vector3ArrayPlugin : ABSTweenPlugin<Vector3, Vector3[], Vector3ArrayOptions>
	{
		public override void Reset(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t)
		{
			t.startValue = (t.endValue = (t.changeValue = null));
		}

		public override void SetFrom(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t, bool isRelative)
		{
		}

		public override Vector3[] ConvertToStartValue(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t, Vector3 value)
		{
			int num = t.endValue.Length;
			Vector3[] array = new Vector3[num];
			for (int i = 0; i < num; i++)
			{
				if (i == 0)
				{
					array[i] = value;
				}
				else
				{
					array[i] = t.endValue[i - 1];
				}
			}
			return array;
		}

		public override void SetRelativeEndValue(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t)
		{
			int num = t.endValue.Length;
			for (int i = 0; i < num; i++)
			{
				if (i > 0)
				{
					t.startValue[i] = t.endValue[i - 1];
				}
				t.endValue[i] = t.startValue[i] + t.endValue[i];
			}
		}

		public override void SetChangeValue(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t)
		{
			int num = t.endValue.Length;
			t.changeValue = new Vector3[num];
			for (int i = 0; i < num; i++)
			{
				t.changeValue[i] = t.endValue[i] - t.startValue[i];
			}
		}

		public override float GetSpeedBasedDuration(Vector3ArrayOptions options, float unitsXSecond, Vector3[] changeValue)
		{
			float num = 0f;
			int num2 = changeValue.Length;
			for (int i = 0; i < num2; i++)
			{
				float num3 = changeValue[i].magnitude / options.durations[i];
				options.durations[i] = num3;
				num += num3;
			}
			return num;
		}

		public override void EvaluateAndApply(Vector3ArrayOptions options, Tween t, bool isRelative, DOGetter<Vector3> getter, DOSetter<Vector3> setter, float elapsed, Vector3[] startValue, Vector3[] changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			Vector3 a = Vector3.zero;
			if (t.loopType == LoopType.Incremental)
			{
				int num = (!t.isComplete) ? t.completedLoops : (t.completedLoops - 1);
				if (num > 0)
				{
					int num2 = startValue.Length - 1;
					a = (startValue[num2] + changeValue[num2] - startValue[0]) * (float)num;
				}
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				int num3 = ((t.loopType != LoopType.Incremental) ? 1 : t.loops) * ((!t.sequenceParent.isComplete) ? t.sequenceParent.completedLoops : (t.sequenceParent.completedLoops - 1));
				if (num3 > 0)
				{
					int num4 = startValue.Length - 1;
					a += (startValue[num4] + changeValue[num4] - startValue[0]) * (float)num3;
				}
			}
			int num5 = 0;
			float num6 = 0f;
			float num7 = 0f;
			int num8 = options.durations.Length;
			float num9 = 0f;
			for (int i = 0; i < num8; i++)
			{
				num7 = options.durations[i];
				num9 += num7;
				if (elapsed <= num9)
				{
					num5 = i;
					num6 = elapsed - num6;
					break;
				}
				num6 += num7;
			}
			float num10 = EaseManager.Evaluate(t.easeType, t.customEase, num6, num7, t.easeOvershootOrAmplitude, t.easePeriod);
			AxisConstraint axisConstraint = options.axisConstraint;
			switch (axisConstraint)
			{
			case AxisConstraint.X:
			{
				Vector3 pNewValue = getter();
				pNewValue.x = startValue[num5].x + a.x + changeValue[num5].x * num10;
				if (options.snapping)
				{
					pNewValue.x = (float)Math.Round((double)pNewValue.x);
				}
				setter(pNewValue);
				break;
			}
			default:
				if (axisConstraint != AxisConstraint.Z)
				{
					Vector3 pNewValue;
					pNewValue.x = startValue[num5].x + a.x + changeValue[num5].x * num10;
					pNewValue.y = startValue[num5].y + a.y + changeValue[num5].y * num10;
					pNewValue.z = startValue[num5].z + a.z + changeValue[num5].z * num10;
					if (options.snapping)
					{
						pNewValue.x = (float)Math.Round((double)pNewValue.x);
						pNewValue.y = (float)Math.Round((double)pNewValue.y);
						pNewValue.z = (float)Math.Round((double)pNewValue.z);
					}
					setter(pNewValue);
				}
				else
				{
					Vector3 pNewValue = getter();
					pNewValue.z = startValue[num5].z + a.z + changeValue[num5].z * num10;
					if (options.snapping)
					{
						pNewValue.z = (float)Math.Round((double)pNewValue.z);
					}
					setter(pNewValue);
				}
				break;
			case AxisConstraint.Y:
			{
				Vector3 pNewValue = getter();
				pNewValue.y = startValue[num5].y + a.y + changeValue[num5].y * num10;
				if (options.snapping)
				{
					pNewValue.y = (float)Math.Round((double)pNewValue.y);
				}
				setter(pNewValue);
				return;
			}
			}
		}
	}
}
