using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
	public static class TweenExtensions
	{
		public static void Complete(this Tween t)
		{
			t.Complete(false);
		}

		public static void Complete(this Tween t, bool withCallbacks)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenManager.Complete(t, true, (!withCallbacks) ? UpdateMode.Goto : UpdateMode.Update);
		}

		public static void Flip(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenManager.Flip(t);
		}

		public static void ForceInit(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenManager.ForceInit(t, false);
		}

		public static void Goto(this Tween t, float to, bool andPlay = false)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			if (to < 0f)
			{
				to = 0f;
			}
			TweenManager.Goto(t, to, andPlay, UpdateMode.Goto);
		}

		public static void Kill(this Tween t, bool complete = false)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			if (complete)
			{
				TweenManager.Complete(t, true, UpdateMode.Goto);
				if (t.autoKill && t.loops >= 0)
				{
					return;
				}
			}
			if (TweenManager.isUpdateLoop)
			{
				t.active = false;
			}
			else
			{
				TweenManager.Despawn(t, true);
			}
		}

		public static T Pause<T>(this T t) where T : Tween
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return t;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return t;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return t;
			}
			TweenManager.Pause(t);
			return t;
		}

		public static T Play<T>(this T t) where T : Tween
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return t;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return t;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return t;
			}
			TweenManager.Play(t);
			return t;
		}

		public static void PlayBackwards(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenManager.PlayBackwards(t);
		}

		public static void PlayForward(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenManager.PlayForward(t);
		}

		public static void Restart(this Tween t, bool includeDelay = true, float changeDelayTo = -1f)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenManager.Restart(t, includeDelay, changeDelayTo);
		}

		public static void Rewind(this Tween t, bool includeDelay = true)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenManager.Rewind(t, includeDelay);
		}

		public static void SmoothRewind(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenManager.SmoothRewind(t);
		}

		public static void TogglePause(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenManager.TogglePause(t);
		}

		public static void GotoWaypoint(this Tween t, int waypointIndex, bool andPlay = false)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = t as TweenerCore<Vector3, Path, PathOptions>;
			if (tweenerCore == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNonPathTween(t);
				}
				return;
			}
			if (!t.startupDone)
			{
				TweenManager.ForceInit(t, false);
			}
			if (waypointIndex < 0)
			{
				waypointIndex = 0;
			}
			else if (waypointIndex > tweenerCore.changeValue.wps.Length - 1)
			{
				waypointIndex = tweenerCore.changeValue.wps.Length - 1;
			}
			float num = 0f;
			for (int i = 0; i < waypointIndex + 1; i++)
			{
				num += tweenerCore.changeValue.wpLengths[i];
			}
			float num2 = num / tweenerCore.changeValue.length;
			bool flag = t.loopType == LoopType.Yoyo && ((t.position >= t.duration) ? (t.completedLoops % 2 == 0) : (t.completedLoops % 2 != 0));
			if (flag)
			{
				num2 = 1f - num2;
			}
			float to = (float)((!t.isComplete) ? t.completedLoops : (t.completedLoops - 1)) * t.duration + num2 * t.duration;
			TweenManager.Goto(t, to, andPlay, UpdateMode.Goto);
		}

		public static YieldInstruction WaitForCompletion(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return DOTween.instance.StartCoroutine(DOTween.instance.WaitForCompletion(t));
		}

		public static YieldInstruction WaitForRewind(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return DOTween.instance.StartCoroutine(DOTween.instance.WaitForRewind(t));
		}

		public static YieldInstruction WaitForKill(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return DOTween.instance.StartCoroutine(DOTween.instance.WaitForKill(t));
		}

		public static YieldInstruction WaitForElapsedLoops(this Tween t, int elapsedLoops)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return DOTween.instance.StartCoroutine(DOTween.instance.WaitForElapsedLoops(t, elapsedLoops));
		}

		public static YieldInstruction WaitForPosition(this Tween t, float position)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return DOTween.instance.StartCoroutine(DOTween.instance.WaitForPosition(t, position));
		}

		public static Coroutine WaitForStart(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return DOTween.instance.StartCoroutine(DOTween.instance.WaitForStart(t));
		}

		public static int CompletedLoops(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return 0;
			}
			return t.completedLoops;
		}

		public static float Delay(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			return t.delay;
		}

		public static float Duration(this Tween t, bool includeLoops = true)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			if (includeLoops)
			{
				return (t.loops != -1) ? (t.duration * (float)t.loops) : float.PositiveInfinity;
			}
			return t.duration;
		}

		public static float Elapsed(this Tween t, bool includeLoops = true)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			if (includeLoops)
			{
				int num = (t.position < t.duration) ? t.completedLoops : (t.completedLoops - 1);
				return (float)num * t.duration + t.position;
			}
			return t.position;
		}

		public static float ElapsedPercentage(this Tween t, bool includeLoops = true)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			if (includeLoops)
			{
				int num = (t.position < t.duration) ? t.completedLoops : (t.completedLoops - 1);
				return ((float)num * t.duration + t.position) / t.fullDuration;
			}
			return t.position / t.duration;
		}

		public static float ElapsedDirectionalPercentage(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			float num = t.position / t.duration;
			bool flag = t.completedLoops > 0 && t.loopType == LoopType.Yoyo && ((!t.isComplete && t.completedLoops % 2 != 0) || (t.isComplete && t.completedLoops % 2 == 0));
			return (!flag) ? num : (1f - num);
		}

		public static bool IsActive(this Tween t)
		{
			return t.active;
		}

		public static bool IsBackwards(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return false;
			}
			return t.isBackwards;
		}

		public static bool IsComplete(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return false;
			}
			return t.isComplete;
		}

		public static bool IsInitialized(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return false;
			}
			return t.startupDone;
		}

		public static bool IsPlaying(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return false;
			}
			return t.isPlaying;
		}

		public static int Loops(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return 0;
			}
			return t.loops;
		}

		public static Vector3 PathGetPoint(this Tween t, float pathPercentage)
		{
			if (pathPercentage > 1f)
			{
				pathPercentage = 1f;
			}
			else if (pathPercentage < 0f)
			{
				pathPercentage = 0f;
			}
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return Vector3.zero;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return Vector3.zero;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return Vector3.zero;
			}
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = t as TweenerCore<Vector3, Path, PathOptions>;
			if (tweenerCore == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNonPathTween(t);
				}
				return Vector3.zero;
			}
			if (!tweenerCore.endValue.isFinalized)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogWarning("The path is not finalized yet");
				}
				return Vector3.zero;
			}
			return tweenerCore.endValue.GetPoint(pathPercentage, true);
		}

		public static Vector3[] PathGetDrawPoints(this Tween t, int subdivisionsXSegment = 10)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return null;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return null;
			}
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = t as TweenerCore<Vector3, Path, PathOptions>;
			if (tweenerCore == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNonPathTween(t);
				}
				return null;
			}
			if (!tweenerCore.endValue.isFinalized)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogWarning("The path is not finalized yet");
				}
				return null;
			}
			return Path.GetDrawPoints(tweenerCore.endValue, subdivisionsXSegment);
		}

		public static float PathLength(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return -1f;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return -1f;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return -1f;
			}
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = t as TweenerCore<Vector3, Path, PathOptions>;
			if (tweenerCore == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNonPathTween(t);
				}
				return -1f;
			}
			if (!tweenerCore.endValue.isFinalized)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogWarning("The path is not finalized yet");
				}
				return -1f;
			}
			return tweenerCore.endValue.length;
		}
	}
}
