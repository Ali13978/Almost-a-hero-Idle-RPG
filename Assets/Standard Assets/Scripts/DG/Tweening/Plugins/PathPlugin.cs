using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	public class PathPlugin : ABSTweenPlugin<Vector3, Path, PathOptions>
	{
		public override void Reset(TweenerCore<Vector3, Path, PathOptions> t)
		{
			t.endValue.Destroy();
			t.startValue = (t.endValue = (t.changeValue = null));
		}

		public override void SetFrom(TweenerCore<Vector3, Path, PathOptions> t, bool isRelative)
		{
		}

		public static ABSTweenPlugin<Vector3, Path, PathOptions> Get()
		{
			return PluginsManager.GetCustomPlugin<PathPlugin, Vector3, Path, PathOptions>();
		}

		public override Path ConvertToStartValue(TweenerCore<Vector3, Path, PathOptions> t, Vector3 value)
		{
			return t.endValue;
		}

		public override void SetRelativeEndValue(TweenerCore<Vector3, Path, PathOptions> t)
		{
			if (t.endValue.isFinalized)
			{
				return;
			}
			Vector3 b = t.getter();
			int num = t.endValue.wps.Length;
			for (int i = 0; i < num; i++)
			{
				t.endValue.wps[i] += b;
			}
		}

		public override void SetChangeValue(TweenerCore<Vector3, Path, PathOptions> t)
		{
			Transform transform = ((Component)t.target).transform;
			if (t.plugOptions.orientType == OrientType.ToPath && t.plugOptions.useLocalPosition)
			{
				t.plugOptions.parent = transform.parent;
			}
			if (t.endValue.isFinalized)
			{
				t.changeValue = t.endValue;
				return;
			}
			Vector3 vector = t.getter();
			Path endValue = t.endValue;
			int num = endValue.wps.Length;
			int num2 = 0;
			bool flag = false;
			bool flag2 = false;
			if (!Utils.Vector3AreApproximatelyEqual(endValue.wps[0], vector))
			{
				flag = true;
				num2++;
			}
			if (t.plugOptions.isClosedPath && endValue.wps[num - 1] != vector)
			{
				flag2 = true;
				num2++;
			}
			int num3 = num + num2;
			Vector3[] array = new Vector3[num3];
			int num4 = (!flag) ? 0 : 1;
			if (flag)
			{
				array[0] = vector;
			}
			for (int i = 0; i < num; i++)
			{
				array[i + num4] = endValue.wps[i];
			}
			if (flag2)
			{
				array[array.Length - 1] = array[0];
			}
			endValue.wps = array;
			endValue.FinalizePath(t.plugOptions.isClosedPath, t.plugOptions.lockPositionAxis, vector);
			t.plugOptions.startupRot = transform.rotation;
			t.plugOptions.startupZRot = transform.eulerAngles.z;
			t.changeValue = t.endValue;
		}

		public override float GetSpeedBasedDuration(PathOptions options, float unitsXSecond, Path changeValue)
		{
			return changeValue.length / unitsXSecond;
		}

		public override void EvaluateAndApply(PathOptions options, Tween t, bool isRelative, DOGetter<Vector3> getter, DOSetter<Vector3> setter, float elapsed, Path startValue, Path changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental && !options.isClosedPath)
			{
				int num = (!t.isComplete) ? t.completedLoops : (t.completedLoops - 1);
				if (num > 0)
				{
					changeValue = changeValue.CloneIncremental(num);
				}
			}
			float perc = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			float num2 = changeValue.ConvertToConstantPathPerc(perc);
			Vector3 point = changeValue.GetPoint(num2, false);
			changeValue.targetPosition = point;
			setter(point);
			if (options.mode != PathMode.Ignore && options.orientType != OrientType.None)
			{
				this.SetOrientation(options, t, changeValue, num2, point, updateNotice);
			}
			bool flag = !usingInversePosition;
			if (t.isBackwards)
			{
				flag = !flag;
			}
			int waypointIndexFromPerc = changeValue.GetWaypointIndexFromPerc(perc, flag);
			if (waypointIndexFromPerc != t.miscInt)
			{
				int miscInt = t.miscInt;
				t.miscInt = waypointIndexFromPerc;
				if (t.onWaypointChange != null)
				{
					bool flag2 = waypointIndexFromPerc < miscInt;
					if (flag2)
					{
						for (int i = miscInt - 1; i > waypointIndexFromPerc - 1; i--)
						{
							Tween.OnTweenCallback<int>(t.onWaypointChange, i);
						}
					}
					else
					{
						for (int j = miscInt + 1; j < waypointIndexFromPerc + 1; j++)
						{
							Tween.OnTweenCallback<int>(t.onWaypointChange, j);
						}
					}
				}
			}
		}

		public void SetOrientation(PathOptions options, Tween t, Path path, float pathPerc, Vector3 tPos, UpdateNotice updateNotice)
		{
			Transform transform = ((Component)t.target).transform;
			Quaternion quaternion = Quaternion.identity;
			if (updateNotice == UpdateNotice.RewindStep)
			{
				transform.rotation = options.startupRot;
			}
			OrientType orientType = options.orientType;
			if (orientType != OrientType.LookAtPosition)
			{
				if (orientType != OrientType.LookAtTransform)
				{
					if (orientType == OrientType.ToPath)
					{
						Vector3 vector;
						if (path.type == PathType.Linear && options.lookAhead <= 0.0001f)
						{
							vector = tPos + path.wps[path.linearWPIndex] - path.wps[path.linearWPIndex - 1];
						}
						else
						{
							float num = pathPerc + options.lookAhead;
							if (num > 1f)
							{
								num = ((!options.isClosedPath) ? ((path.type != PathType.Linear) ? 1.00001f : 1f) : (num - 1f));
							}
							vector = path.GetPoint(num, false);
						}
						if (path.type == PathType.Linear)
						{
							Vector3 vector2 = path.wps[path.wps.Length - 1];
							if (vector == vector2)
							{
								vector = ((!(tPos == vector2)) ? vector2 : (vector2 + (vector2 - path.wps[path.wps.Length - 2])));
							}
						}
						Vector3 upwards = transform.up;
						if (options.useLocalPosition && options.parent != null)
						{
							vector = options.parent.TransformPoint(vector);
						}
						if (options.lockRotationAxis != AxisConstraint.None)
						{
							if ((options.lockRotationAxis & AxisConstraint.X) == AxisConstraint.X)
							{
								Vector3 position = transform.InverseTransformPoint(vector);
								position.y = 0f;
								vector = transform.TransformPoint(position);
								upwards = ((!options.useLocalPosition || !(options.parent != null)) ? Vector3.up : options.parent.up);
							}
							if ((options.lockRotationAxis & AxisConstraint.Y) == AxisConstraint.Y)
							{
								Vector3 position2 = transform.InverseTransformPoint(vector);
								if (position2.z < 0f)
								{
									position2.z = -position2.z;
								}
								position2.x = 0f;
								vector = transform.TransformPoint(position2);
							}
							if ((options.lockRotationAxis & AxisConstraint.Z) == AxisConstraint.Z)
							{
								if (options.useLocalPosition && options.parent != null)
								{
									upwards = options.parent.TransformDirection(Vector3.up);
								}
								else
								{
									upwards = transform.TransformDirection(Vector3.up);
								}
								upwards.z = options.startupZRot;
							}
						}
						if (options.mode == PathMode.Full3D)
						{
							Vector3 vector3 = vector - transform.position;
							if (vector3 == Vector3.zero)
							{
								vector3 = transform.forward;
							}
							quaternion = Quaternion.LookRotation(vector3, upwards);
						}
						else
						{
							float y = 0f;
							float num2 = Utils.Angle2D(transform.position, vector);
							if (num2 < 0f)
							{
								num2 = 360f + num2;
							}
							if (options.mode == PathMode.Sidescroller2D)
							{
								y = (float)((vector.x >= transform.position.x) ? 0 : 180);
								if (num2 > 90f && num2 < 270f)
								{
									num2 = 180f - num2;
								}
							}
							quaternion = Quaternion.Euler(0f, y, num2);
						}
					}
				}
				else if (options.lookAtTransform != null)
				{
					path.lookAtPosition = new Vector3?(options.lookAtTransform.position);
					quaternion = Quaternion.LookRotation(options.lookAtTransform.position - transform.position, transform.up);
				}
			}
			else
			{
				path.lookAtPosition = new Vector3?(options.lookAtPosition);
				quaternion = Quaternion.LookRotation(options.lookAtPosition - transform.position, transform.up);
			}
			if (options.hasCustomForwardDirection)
			{
				quaternion *= options.forward;
			}
			if (options.isRigidbody)
			{
				((Rigidbody)t.target).rotation = quaternion;
			}
			else
			{
				transform.rotation = quaternion;
			}
		}

		public const float MinLookAhead = 0.0001f;
	}
}
