using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	public class Vector4Plugin : ABSTweenPlugin<Vector4, Vector4, VectorOptions>
	{
		public override void Reset(TweenerCore<Vector4, Vector4, VectorOptions> t)
		{
		}

		public override void SetFrom(TweenerCore<Vector4, Vector4, VectorOptions> t, bool isRelative)
		{
			Vector4 endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = ((!isRelative) ? endValue : (t.endValue + endValue));
			Vector4 pNewValue = t.endValue;
			AxisConstraint axisConstraint = t.plugOptions.axisConstraint;
			switch (axisConstraint)
			{
			case AxisConstraint.X:
				pNewValue.x = t.startValue.x;
				break;
			default:
				if (axisConstraint != AxisConstraint.Z)
				{
					if (axisConstraint != AxisConstraint.W)
					{
						pNewValue = t.startValue;
					}
					else
					{
						pNewValue.w = t.startValue.w;
					}
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
				pNewValue.w = (float)Math.Round((double)pNewValue.w);
			}
			t.setter(pNewValue);
		}

		public override Vector4 ConvertToStartValue(TweenerCore<Vector4, Vector4, VectorOptions> t, Vector4 value)
		{
			return value;
		}

		public override void SetRelativeEndValue(TweenerCore<Vector4, Vector4, VectorOptions> t)
		{
			t.endValue += t.startValue;
		}

		public override void SetChangeValue(TweenerCore<Vector4, Vector4, VectorOptions> t)
		{
			AxisConstraint axisConstraint = t.plugOptions.axisConstraint;
			switch (axisConstraint)
			{
			case AxisConstraint.X:
				t.changeValue = new Vector4(t.endValue.x - t.startValue.x, 0f, 0f, 0f);
				break;
			default:
				if (axisConstraint != AxisConstraint.Z)
				{
					if (axisConstraint != AxisConstraint.W)
					{
						t.changeValue = t.endValue - t.startValue;
					}
					else
					{
						t.changeValue = new Vector4(0f, 0f, 0f, t.endValue.w - t.startValue.w);
					}
				}
				else
				{
					t.changeValue = new Vector4(0f, 0f, t.endValue.z - t.startValue.z, 0f);
				}
				break;
			case AxisConstraint.Y:
				t.changeValue = new Vector4(0f, t.endValue.y - t.startValue.y, 0f, 0f);
				break;
			}
		}

		public override float GetSpeedBasedDuration(VectorOptions options, float unitsXSecond, Vector4 changeValue)
		{
			return changeValue.magnitude / unitsXSecond;
		}

		public override void EvaluateAndApply(VectorOptions options, Tween t, bool isRelative, DOGetter<Vector4> getter, DOSetter<Vector4> setter, float elapsed, Vector4 startValue, Vector4 changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
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
				Vector4 pNewValue = getter();
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
					if (axisConstraint != AxisConstraint.W)
					{
						startValue.x += changeValue.x * num;
						startValue.y += changeValue.y * num;
						startValue.z += changeValue.z * num;
						startValue.w += changeValue.w * num;
						if (options.snapping)
						{
							startValue.x = (float)Math.Round((double)startValue.x);
							startValue.y = (float)Math.Round((double)startValue.y);
							startValue.z = (float)Math.Round((double)startValue.z);
							startValue.w = (float)Math.Round((double)startValue.w);
						}
						setter(startValue);
					}
					else
					{
						Vector4 pNewValue2 = getter();
						pNewValue2.w = startValue.w + changeValue.w * num;
						if (options.snapping)
						{
							pNewValue2.w = (float)Math.Round((double)pNewValue2.w);
						}
						setter(pNewValue2);
					}
				}
				else
				{
					Vector4 pNewValue3 = getter();
					pNewValue3.z = startValue.z + changeValue.z * num;
					if (options.snapping)
					{
						pNewValue3.z = (float)Math.Round((double)pNewValue3.z);
					}
					setter(pNewValue3);
				}
				break;
			case AxisConstraint.Y:
			{
				Vector4 pNewValue4 = getter();
				pNewValue4.y = startValue.y + changeValue.y * num;
				if (options.snapping)
				{
					pNewValue4.y = (float)Math.Round((double)pNewValue4.y);
				}
				setter(pNewValue4);
				break;
			}
			}
		}
	}
}
