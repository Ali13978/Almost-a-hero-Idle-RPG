using System;
using SA.Common.Animation;
using UnityEngine;

namespace SA.Common.Extensions
{
	public static class SA_TweenExtensions
	{
		public static void MoveTo(this GameObject go, Vector3 position, float time, EaseType easeType = EaseType.linear, Action OnCompleteAction = null)
		{
			ValuesTween valuesTween = go.AddComponent<ValuesTween>();
			valuesTween.DestoryGameObjectOnComplete = false;
			valuesTween.VectorTo(go.transform.position, position, time, easeType);
			valuesTween.OnComplete += OnCompleteAction;
		}

		public static void ScaleTo(this GameObject go, Vector3 scale, float time, EaseType easeType = EaseType.linear, Action OnCompleteAction = null)
		{
			ValuesTween valuesTween = go.AddComponent<ValuesTween>();
			valuesTween.DestoryGameObjectOnComplete = false;
			valuesTween.ScaleTo(go.transform.localScale, scale, time, easeType);
			valuesTween.OnComplete += OnCompleteAction;
		}
	}
}
