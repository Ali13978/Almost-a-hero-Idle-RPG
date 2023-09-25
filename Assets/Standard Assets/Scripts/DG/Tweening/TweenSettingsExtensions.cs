using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Plugins;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
	public static class TweenSettingsExtensions
	{
		public static T SetAutoKill<T>(this T t) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.autoKill = true;
			return t;
		}

		public static T SetAutoKill<T>(this T t, bool autoKillOnCompletion) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.autoKill = autoKillOnCompletion;
			return t;
		}

		public static T SetId<T>(this T t, object objectId) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.id = objectId;
			return t;
		}

		public static T SetId<T>(this T t, string stringId) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.stringId = stringId;
			return t;
		}

		public static T SetId<T>(this T t, int intId) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.intId = intId;
			return t;
		}

		public static T SetTarget<T>(this T t, object target) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.target = target;
			return t;
		}

		public static T SetLoops<T>(this T t, int loops) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			if (loops < -1)
			{
				loops = -1;
			}
			else if (loops == 0)
			{
				loops = 1;
			}
			t.loops = loops;
			if (t.tweenType == TweenType.Tweener)
			{
				if (loops > -1)
				{
					t.fullDuration = t.duration * (float)loops;
				}
				else
				{
					t.fullDuration = float.PositiveInfinity;
				}
			}
			return t;
		}

		public static T SetLoops<T>(this T t, int loops, LoopType loopType) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			if (loops < -1)
			{
				loops = -1;
			}
			else if (loops == 0)
			{
				loops = 1;
			}
			t.loops = loops;
			t.loopType = loopType;
			if (t.tweenType == TweenType.Tweener)
			{
				if (loops > -1)
				{
					t.fullDuration = t.duration * (float)loops;
				}
				else
				{
					t.fullDuration = float.PositiveInfinity;
				}
			}
			return t;
		}

		public static T SetEase<T>(this T t, Ease ease) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.easeType = ease;
			if (EaseManager.IsFlashEase(ease))
			{
				t.easeOvershootOrAmplitude = (float)((int)t.easeOvershootOrAmplitude);
			}
			t.customEase = null;
			return t;
		}

		public static T SetEase<T>(this T t, Ease ease, float overshoot) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.easeType = ease;
			if (EaseManager.IsFlashEase(ease))
			{
				overshoot = (float)((int)overshoot);
			}
			t.easeOvershootOrAmplitude = overshoot;
			t.customEase = null;
			return t;
		}

		public static T SetEase<T>(this T t, Ease ease, float amplitude, float period) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.easeType = ease;
			if (EaseManager.IsFlashEase(ease))
			{
				amplitude = (float)((int)amplitude);
			}
			t.easeOvershootOrAmplitude = amplitude;
			t.easePeriod = period;
			t.customEase = null;
			return t;
		}

		public static T SetEase<T>(this T t, AnimationCurve animCurve) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.easeType = Ease.INTERNAL_Custom;
			t.customEase = new EaseFunction(new EaseCurve(animCurve).Evaluate);
			return t;
		}

		public static T SetEase<T>(this T t, EaseFunction customEase) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.easeType = Ease.INTERNAL_Custom;
			t.customEase = customEase;
			return t;
		}

		public static T SetRecyclable<T>(this T t) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.isRecyclable = true;
			return t;
		}

		public static T SetRecyclable<T>(this T t, bool recyclable) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.isRecyclable = recyclable;
			return t;
		}

		public static T SetUpdate<T>(this T t, bool isIndependentUpdate) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			TweenManager.SetUpdateType(t, DOTween.defaultUpdateType, isIndependentUpdate);
			return t;
		}

		public static T SetUpdate<T>(this T t, UpdateType updateType) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			TweenManager.SetUpdateType(t, updateType, DOTween.defaultTimeScaleIndependent);
			return t;
		}

		public static T SetUpdate<T>(this T t, UpdateType updateType, bool isIndependentUpdate) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			TweenManager.SetUpdateType(t, updateType, isIndependentUpdate);
			return t;
		}

		public static T OnStart<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onStart = action;
			return t;
		}

		public static T OnPlay<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onPlay = action;
			return t;
		}

		public static T OnPause<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onPause = action;
			return t;
		}

		public static T OnRewind<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onRewind = action;
			return t;
		}

		public static T OnUpdate<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onUpdate = action;
			return t;
		}

		public static T OnStepComplete<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onStepComplete = action;
			return t;
		}

		public static T OnComplete<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onComplete = action;
			return t;
		}

		public static T OnKill<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onKill = action;
			return t;
		}

		public static T OnWaypointChange<T>(this T t, TweenCallback<int> action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onWaypointChange = action;
			return t;
		}

		public static T SetAs<T>(this T t, Tween asTween) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.timeScale = asTween.timeScale;
			t.isBackwards = asTween.isBackwards;
			TweenManager.SetUpdateType(t, asTween.updateType, asTween.isIndependentUpdate);
			t.id = asTween.id;
			t.onStart = asTween.onStart;
			t.onPlay = asTween.onPlay;
			t.onRewind = asTween.onRewind;
			t.onUpdate = asTween.onUpdate;
			t.onStepComplete = asTween.onStepComplete;
			t.onComplete = asTween.onComplete;
			t.onKill = asTween.onKill;
			t.onWaypointChange = asTween.onWaypointChange;
			t.isRecyclable = asTween.isRecyclable;
			t.isSpeedBased = asTween.isSpeedBased;
			t.autoKill = asTween.autoKill;
			t.loops = asTween.loops;
			t.loopType = asTween.loopType;
			if (t.tweenType == TweenType.Tweener)
			{
				if (t.loops > -1)
				{
					t.fullDuration = t.duration * (float)t.loops;
				}
				else
				{
					t.fullDuration = float.PositiveInfinity;
				}
			}
			t.delay = asTween.delay;
			t.delayComplete = (t.delay <= 0f);
			t.isRelative = asTween.isRelative;
			t.easeType = asTween.easeType;
			t.customEase = asTween.customEase;
			t.easeOvershootOrAmplitude = asTween.easeOvershootOrAmplitude;
			t.easePeriod = asTween.easePeriod;
			return t;
		}

		public static T SetAs<T>(this T t, TweenParams tweenParams) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			TweenManager.SetUpdateType(t, tweenParams.updateType, tweenParams.isIndependentUpdate);
			t.id = tweenParams.id;
			t.onStart = tweenParams.onStart;
			t.onPlay = tweenParams.onPlay;
			t.onRewind = tweenParams.onRewind;
			t.onUpdate = tweenParams.onUpdate;
			t.onStepComplete = tweenParams.onStepComplete;
			t.onComplete = tweenParams.onComplete;
			t.onKill = tweenParams.onKill;
			t.onWaypointChange = tweenParams.onWaypointChange;
			t.isRecyclable = tweenParams.isRecyclable;
			t.isSpeedBased = tweenParams.isSpeedBased;
			t.autoKill = tweenParams.autoKill;
			t.loops = tweenParams.loops;
			t.loopType = tweenParams.loopType;
			if (t.tweenType == TweenType.Tweener)
			{
				if (t.loops > -1)
				{
					t.fullDuration = t.duration * (float)t.loops;
				}
				else
				{
					t.fullDuration = float.PositiveInfinity;
				}
			}
			t.delay = tweenParams.delay;
			t.delayComplete = (t.delay <= 0f);
			t.isRelative = tweenParams.isRelative;
			if (tweenParams.easeType == Ease.Unset)
			{
				if (t.tweenType == TweenType.Sequence)
				{
					t.easeType = Ease.Linear;
				}
				else
				{
					t.easeType = DOTween.defaultEaseType;
				}
			}
			else
			{
				t.easeType = tweenParams.easeType;
			}
			t.customEase = tweenParams.customEase;
			t.easeOvershootOrAmplitude = tweenParams.easeOvershootOrAmplitude;
			t.easePeriod = tweenParams.easePeriod;
			return t;
		}

		public static Sequence Append(this Sequence s, Tween t)
		{
			if (s == null || !s.active || s.creationLocked)
			{
				return s;
			}
			if (t == null || !t.active || t.isSequenced)
			{
				return s;
			}
			Sequence.DoInsert(s, t, s.duration);
			return s;
		}

		public static Sequence Prepend(this Sequence s, Tween t)
		{
			if (s == null || !s.active || s.creationLocked)
			{
				return s;
			}
			if (t == null || !t.active || t.isSequenced)
			{
				return s;
			}
			Sequence.DoPrepend(s, t);
			return s;
		}

		public static Sequence Join(this Sequence s, Tween t)
		{
			if (s == null || !s.active || s.creationLocked)
			{
				return s;
			}
			if (t == null || !t.active || t.isSequenced)
			{
				return s;
			}
			Sequence.DoInsert(s, t, s.lastTweenInsertTime);
			return s;
		}

		public static Sequence Insert(this Sequence s, float atPosition, Tween t)
		{
			if (s == null || !s.active || s.creationLocked)
			{
				return s;
			}
			if (t == null || !t.active || t.isSequenced)
			{
				return s;
			}
			Sequence.DoInsert(s, t, atPosition);
			return s;
		}

		public static Sequence AppendInterval(this Sequence s, float interval)
		{
			if (s == null || !s.active || s.creationLocked)
			{
				return s;
			}
			Sequence.DoAppendInterval(s, interval);
			return s;
		}

		public static Sequence PrependInterval(this Sequence s, float interval)
		{
			if (s == null || !s.active || s.creationLocked)
			{
				return s;
			}
			Sequence.DoPrependInterval(s, interval);
			return s;
		}

		public static Sequence AppendCallback(this Sequence s, TweenCallback callback)
		{
			if (s == null || !s.active || s.creationLocked)
			{
				return s;
			}
			if (callback == null)
			{
				return s;
			}
			Sequence.DoInsertCallback(s, callback, s.duration);
			return s;
		}

		public static Sequence PrependCallback(this Sequence s, TweenCallback callback)
		{
			if (s == null || !s.active || s.creationLocked)
			{
				return s;
			}
			if (callback == null)
			{
				return s;
			}
			Sequence.DoInsertCallback(s, callback, 0f);
			return s;
		}

		public static Sequence InsertCallback(this Sequence s, float atPosition, TweenCallback callback)
		{
			if (s == null || !s.active || s.creationLocked)
			{
				return s;
			}
			if (callback == null)
			{
				return s;
			}
			Sequence.DoInsertCallback(s, callback, atPosition);
			return s;
		}

		public static T From<T>(this T t) where T : Tweener
		{
			if (t == null || !t.active || t.creationLocked || !t.isFromAllowed)
			{
				return t;
			}
			t.isFrom = true;
			t.SetFrom(false);
			return t;
		}

		public static T From<T>(this T t, bool isRelative) where T : Tweener
		{
			if (t == null || !t.active || t.creationLocked || !t.isFromAllowed)
			{
				return t;
			}
			t.isFrom = true;
			if (!isRelative)
			{
				t.SetFrom(false);
			}
			else
			{
				t.SetFrom(!t.isBlendable);
			}
			return t;
		}

		public static T SetDelay<T>(this T t, float delay) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			if (t.tweenType == TweenType.Sequence)
			{
				(t as Sequence).PrependInterval(delay);
			}
			else
			{
				t.delay = delay;
				t.delayComplete = (delay <= 0f);
			}
			return t;
		}

		public static T SetRelative<T>(this T t) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked || t.isFrom || t.isBlendable)
			{
				return t;
			}
			t.isRelative = true;
			return t;
		}

		public static T SetRelative<T>(this T t, bool isRelative) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked || t.isFrom || t.isBlendable)
			{
				return t;
			}
			t.isRelative = isRelative;
			return t;
		}

		public static T SetSpeedBased<T>(this T t) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.isSpeedBased = true;
			return t;
		}

		public static T SetSpeedBased<T>(this T t, bool isSpeedBased) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.isSpeedBased = isSpeedBased;
			return t;
		}

		public static Tweener SetOptions(this TweenerCore<float, float, FloatOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static Tweener SetOptions(this TweenerCore<Vector2, Vector2, VectorOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static Tweener SetOptions(this TweenerCore<Vector2, Vector2, VectorOptions> t, AxisConstraint axisConstraint, bool snapping = false)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.axisConstraint = axisConstraint;
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static Tweener SetOptions(this TweenerCore<Vector3, Vector3, VectorOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static Tweener SetOptions(this TweenerCore<Vector3, Vector3, VectorOptions> t, AxisConstraint axisConstraint, bool snapping = false)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.axisConstraint = axisConstraint;
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static Tweener SetOptions(this TweenerCore<Vector4, Vector4, VectorOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static Tweener SetOptions(this TweenerCore<Vector4, Vector4, VectorOptions> t, AxisConstraint axisConstraint, bool snapping = false)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.axisConstraint = axisConstraint;
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static Tweener SetOptions(this TweenerCore<Quaternion, Vector3, QuaternionOptions> t, bool useShortest360Route = true)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.rotateMode = ((!useShortest360Route) ? RotateMode.FastBeyond360 : RotateMode.Fast);
			return t;
		}

		public static Tweener SetOptions(this TweenerCore<Color, Color, ColorOptions> t, bool alphaOnly)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.alphaOnly = alphaOnly;
			return t;
		}

		public static Tweener SetOptions(this TweenerCore<Rect, Rect, RectOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static Tweener SetOptions(this TweenerCore<string, string, StringOptions> t, bool richTextEnabled, ScrambleMode scrambleMode = ScrambleMode.None, string scrambleChars = null)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.richTextEnabled = richTextEnabled;
			t.plugOptions.scrambleMode = scrambleMode;
			if (!string.IsNullOrEmpty(scrambleChars))
			{
				if (scrambleChars.Length <= 1)
				{
					scrambleChars += scrambleChars;
				}
				t.plugOptions.scrambledChars = scrambleChars.ToCharArray();
				t.plugOptions.scrambledChars.ScrambleChars();
			}
			return t;
		}

		public static Tweener SetOptions(this TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static Tweener SetOptions(this TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t, AxisConstraint axisConstraint, bool snapping = false)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.axisConstraint = axisConstraint;
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static TweenerCore<Vector3, Path, PathOptions> SetOptions(this TweenerCore<Vector3, Path, PathOptions> t, AxisConstraint lockPosition, AxisConstraint lockRotation = AxisConstraint.None)
		{
			return t.SetOptions(false, lockPosition, lockRotation);
		}

		public static TweenerCore<Vector3, Path, PathOptions> SetOptions(this TweenerCore<Vector3, Path, PathOptions> t, bool closePath, AxisConstraint lockPosition = AxisConstraint.None, AxisConstraint lockRotation = AxisConstraint.None)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.isClosedPath = closePath;
			t.plugOptions.lockPositionAxis = lockPosition;
			t.plugOptions.lockRotationAxis = lockRotation;
			return t;
		}

		public static TweenerCore<Vector3, Path, PathOptions> SetLookAt(this TweenerCore<Vector3, Path, PathOptions> t, Vector3 lookAtPosition, Vector3? forwardDirection = null, Vector3? up = null)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.orientType = OrientType.LookAtPosition;
			t.plugOptions.lookAtPosition = lookAtPosition;
			t.SetPathForwardDirection(forwardDirection, up);
			return t;
		}

		public static TweenerCore<Vector3, Path, PathOptions> SetLookAt(this TweenerCore<Vector3, Path, PathOptions> t, Transform lookAtTransform, Vector3? forwardDirection = null, Vector3? up = null)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.orientType = OrientType.LookAtTransform;
			t.plugOptions.lookAtTransform = lookAtTransform;
			t.SetPathForwardDirection(forwardDirection, up);
			return t;
		}

		public static TweenerCore<Vector3, Path, PathOptions> SetLookAt(this TweenerCore<Vector3, Path, PathOptions> t, float lookAhead, Vector3? forwardDirection = null, Vector3? up = null)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.orientType = OrientType.ToPath;
			if (lookAhead < 0.0001f)
			{
				lookAhead = 0.0001f;
			}
			t.plugOptions.lookAhead = lookAhead;
			t.SetPathForwardDirection(forwardDirection, up);
			return t;
		}

		private static void SetPathForwardDirection(this TweenerCore<Vector3, Path, PathOptions> t, Vector3? forwardDirection = null, Vector3? up = null)
		{
			if (t == null || !t.active)
			{
				return;
			}
			t.plugOptions.hasCustomForwardDirection = ((forwardDirection != null && forwardDirection != Vector3.zero) || (up != null && up != Vector3.zero));
			if (t.plugOptions.hasCustomForwardDirection)
			{
				if (forwardDirection == Vector3.zero)
				{
					forwardDirection = new Vector3?(Vector3.forward);
				}
				t.plugOptions.forward = Quaternion.LookRotation((forwardDirection != null) ? forwardDirection.Value : Vector3.forward, (up != null) ? up.Value : Vector3.up);
			}
		}
	}
}
