using System;
using System.Collections;
using UnityEngine;

namespace DG.Tweening.Core
{
	[AddComponentMenu("")]
	public class DOTweenComponent : MonoBehaviour, IDOTweenInit
	{
		private void Awake()
		{
			this.inspectorUpdater = 0;
			this._unscaledTime = Time.realtimeSinceStartup;
		}

		private void Start()
		{
			if (DOTween.instance != this)
			{
				this._duplicateToDestroy = true;
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		private void Update()
		{
			this._unscaledDeltaTime = Time.realtimeSinceStartup - this._unscaledTime;
			if (DOTween.useSmoothDeltaTime && this._unscaledDeltaTime > DOTween.maxSmoothUnscaledTime)
			{
				this._unscaledDeltaTime = DOTween.maxSmoothUnscaledTime;
			}
			if (TweenManager.hasActiveDefaultTweens)
			{
				TweenManager.Update(UpdateType.Normal, ((!DOTween.useSmoothDeltaTime) ? Time.deltaTime : Time.smoothDeltaTime) * DOTween.timeScale, this._unscaledDeltaTime * DOTween.timeScale);
			}
			this._unscaledTime = Time.realtimeSinceStartup;
			if (DOTween.isUnityEditor)
			{
				this.inspectorUpdater++;
				if (DOTween.showUnityEditorReport && TweenManager.hasActiveTweens)
				{
					if (TweenManager.totActiveTweeners > DOTween.maxActiveTweenersReached)
					{
						DOTween.maxActiveTweenersReached = TweenManager.totActiveTweeners;
					}
					if (TweenManager.totActiveSequences > DOTween.maxActiveSequencesReached)
					{
						DOTween.maxActiveSequencesReached = TweenManager.totActiveSequences;
					}
				}
			}
		}

		private void LateUpdate()
		{
			if (TweenManager.hasActiveLateTweens)
			{
				TweenManager.Update(UpdateType.Late, ((!DOTween.useSmoothDeltaTime) ? Time.deltaTime : Time.smoothDeltaTime) * DOTween.timeScale, this._unscaledDeltaTime * DOTween.timeScale);
			}
		}

		private void FixedUpdate()
		{
			if (TweenManager.hasActiveFixedTweens && Time.timeScale > 0f)
			{
				TweenManager.Update(UpdateType.Fixed, ((!DOTween.useSmoothDeltaTime) ? Time.deltaTime : Time.smoothDeltaTime) * DOTween.timeScale, ((!DOTween.useSmoothDeltaTime) ? Time.deltaTime : Time.smoothDeltaTime) / Time.timeScale * DOTween.timeScale);
			}
		}

		internal void ManualUpdate(float deltaTime, float unscaledDeltaTime)
		{
			if (TweenManager.hasActiveManualTweens)
			{
				TweenManager.Update(UpdateType.Manual, deltaTime * DOTween.timeScale, unscaledDeltaTime * DOTween.timeScale);
			}
		}

		private void OnDrawGizmos()
		{
			if (!DOTween.drawGizmos || !DOTween.isUnityEditor)
			{
				return;
			}
			int count = DOTween.GizmosDelegates.Count;
			if (count == 0)
			{
				return;
			}
			for (int i = 0; i < count; i++)
			{
				DOTween.GizmosDelegates[i]();
			}
		}

		private void OnDestroy()
		{
			if (this._duplicateToDestroy)
			{
				return;
			}
			if (DOTween.showUnityEditorReport)
			{
				string message = string.Concat(new object[]
				{
					"REPORT > Max overall simultaneous active Tweeners/Sequences: ",
					DOTween.maxActiveTweenersReached,
					"/",
					DOTween.maxActiveSequencesReached
				});
				Debugger.LogReport(message);
			}
			if (DOTween.instance == this)
			{
				DOTween.instance = null;
			}
		}

		private void OnApplicationQuit()
		{
			DOTween.isQuitting = true;
		}

		public IDOTweenInit SetCapacity(int tweenersCapacity, int sequencesCapacity)
		{
			TweenManager.SetCapacities(tweenersCapacity, sequencesCapacity);
			return this;
		}

		internal IEnumerator WaitForCompletion(Tween t)
		{
			while (t.active && !t.isComplete)
			{
				yield return null;
			}
			yield break;
		}

		internal IEnumerator WaitForRewind(Tween t)
		{
			while (t.active && (!t.playedOnce || t.position * (float)(t.completedLoops + 1) > 0f))
			{
				yield return null;
			}
			yield break;
		}

		internal IEnumerator WaitForKill(Tween t)
		{
			while (t.active)
			{
				yield return null;
			}
			yield break;
		}

		internal IEnumerator WaitForElapsedLoops(Tween t, int elapsedLoops)
		{
			while (t.active && t.completedLoops < elapsedLoops)
			{
				yield return null;
			}
			yield break;
		}

		internal IEnumerator WaitForPosition(Tween t, float position)
		{
			while (t.active && t.position * (float)(t.completedLoops + 1) < position)
			{
				yield return null;
			}
			yield break;
		}

		internal IEnumerator WaitForStart(Tween t)
		{
			while (t.active && !t.playedOnce)
			{
				yield return null;
			}
			yield break;
		}

		internal static void Create()
		{
			if (DOTween.instance != null)
			{
				return;
			}
			GameObject gameObject = new GameObject("[DOTween]");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			DOTween.instance = gameObject.AddComponent<DOTweenComponent>();
		}

		internal static void DestroyInstance()
		{
			if (DOTween.instance != null)
			{
				UnityEngine.Object.Destroy(DOTween.instance.gameObject);
			}
			DOTween.instance = null;
		}

		public int inspectorUpdater;

		private float _unscaledTime;

		private float _unscaledDeltaTime;

		private bool _duplicateToDestroy;
	}
}
