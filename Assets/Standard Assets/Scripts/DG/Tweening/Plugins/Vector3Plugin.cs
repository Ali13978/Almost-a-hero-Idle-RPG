using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	public class Vector3Plugin : ABSTweenPlugin<Vector3, Vector3, VectorOptions>
	{
		public override void Reset(TweenerCore<Vector3, Vector3, VectorOptions> t)
		{
		}

		public override void SetFrom(TweenerCore<Vector3, Vector3, VectorOptions> t, bool isRelative)
		{
			Vector3 endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = ((!isRelative) ? endValue : (t.endValue + endValue));
			Vector3 pNewValue = t.endValue;
			AxisConstraint axisConstraint = t.plugOptions.axisConstraint;
			switch (axisConstraint)
			{
			case AxisConstraint.X:
				pNewValue.x = t.startValue.x;
				break;
			default:
				if (axisConstraint != AxisConstraint.Z)
				{
					pNewValue = t.startValue;
				}
				else
				{
					pNewValue.z = t.startValue.z;
				}
				break;
			case AxisConstraint.Y:
				pNewValue.y = t.startValue.y;
				break;
			}
			if (t.plugOptions.snapping)
			{
				pNewValue.x = (float)Math.Round((double)pNewValue.x);
				pNewValue.y = (float)Math.Round((double)pNewValue.y);
				pNewValue.z = (float)Math.Round((double)pNewValue.z);
			}
			t.setter(pNewValue);
		}

		public override Vector3 ConvertToStartValue(TweenerCore<Vector3, Vector3, VectorOptions> t, Vector3 value)
		{
			return value;
		}

		public override void SetRelativeEndValue(TweenerCore<Vector3, Vector3, VectorOptions> t)
		{
			t.endValue += t.startValue;
		}

		public override void SetChangeValue(TweenerCore<Vector3, Vector3, VectorOptions> t)
		{
			AxisConstraint axisConstraint = t.plugOptions.axisConstraint;
			switch (axisConstraint)
			{
			case AxisConstraint.X:
				t.changeValue = new Vector3(t.endValue.x - t.startValue.x, 0f, 0f);
				break;
			default:
				if (axisConstraint != AxisConstraint.Z)
				{
					t.changeValue = t.endValue - t.startValue;
				}
				else
				{
					t.changeValue = new Vector3(0f, 0f, t.endValue.z - t.startValue.z);
				}
				break;
			case AxisConstraint.Y:
				t.changeValue = new Vector3(0f, t.endValue.y - t.startValue.y, 0f);
				break;
			}
		}

		public override float GetSpeedBasedDuration(VectorOptions options, float unitsXSecond, Vector3 changeValue)
		{
			return changeValue.magnitude / unitsXSecond;
		}

		public override void EvaluateAndApply(VectorOptions options, Tween t, bool isRelative, DOGetter<Vector3> getter, DOSetter<Vector3> setter, float elapsed, Vector3 startValue, Vector3 changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (float)((!t.isComplete) ? t.completedLoops : (t.completedLoops - 1));
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (float)((t.loopType != LoopType.Incremental) ? 1 : t.loops) * (float)((!t.sequenceParent.isComplete) ? t.sequenceParent.completedLoops : (t.sequenceParent.completedLoops - 1));
			}
			float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			AxisConstraint axisConstraint = options.axisConstraint;
			switch (axisConstraint)
			{
			case AxisConstraint.X:
			{
				Vector3 pNewValue = getter();
				pNewValue.x = startValue.x + changeValue.x * num;
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
					startValue.x += changeValue.x * num;
					startValue.y += changeValue.y * num;
					startValue.z += changeValue.z * num;
					if (options.snapping)
					{
						startValue.x = (float)Math.Round((double)startValue.x);
						startValue.y = (float)Math.Round((double)startValue.y);
						startValue.z = (float)Math.Round((double)startValue.z);
					}
					setter(startValue);
				}
				else
				{
					Vector3 pNewValue2 = getter();
					pNewValue2.z = startValue.z + changeValue.z * num;
					if (options.snapping)
					{
						pNewValue2.z = (float)Math.Round((double)pNewValue2.z);
					}
					setter(pNewValue2);
				}
				break;
			case AxisConstraint.Y:
			{
				Vector3 pNewValue3 = getter();
				pNewValue3.y = startValue.y + changeValue.y * num;
				if (options.snapping)
				{
					pNewValue3.y = (float)Math.Round((double)pNewValue3.y);
				}
				setter(pNewValue3);
				break;
			}
			}
		}
	}
}
