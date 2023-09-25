using System;
using UnityEngine;

namespace DG.Tweening.Core
{
	public class DOTweenSettings : ScriptableObject
	{
		public const string AssetName = "DOTweenSettings";

		public bool useSafeMode = true;

		public float timeScale = 1f;

		public bool useSmoothDeltaTime;

		public float maxSmoothUnscaledTime = 0.15f;

		public bool showUnityEditorReport;

		public LogBehaviour logBehaviour = LogBehaviour.ErrorsOnly;

		public bool drawGizmos = true;

		public bool defaultRecyclable;

		public AutoPlay defaultAutoPlay = AutoPlay.All;

		public UpdateType defaultUpdateType;

		public bool defaultTimeScaleIndependent;

		public Ease defaultEaseType = Ease.OutQuad;

		public float defaultEaseOvershootOrAmplitude = 1.70158f;

		public float defaultEasePeriod;

		public bool defaultAutoKill = true;

		public LoopType defaultLoopType;

		public DOTweenSettings.SettingsLocation storeSettingsLocation;

		public enum SettingsLocation
		{
			AssetsDirectory,
			DOTweenDirectory,
			DemigiantDirectory
		}
	}
}
