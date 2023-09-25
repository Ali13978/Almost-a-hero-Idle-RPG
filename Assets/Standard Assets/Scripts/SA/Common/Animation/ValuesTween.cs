using System;
using System.Diagnostics;
using UnityEngine;

namespace SA.Common.Animation
{
	public class ValuesTween : MonoBehaviour
	{
		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action OnComplete;



		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<float> OnValueChanged;



		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Vector3> OnVectorValueChanged;



		public static ValuesTween Create()
		{
			return new GameObject("SA.Common.Animation.ValuesTween").AddComponent<ValuesTween>();
		}

		private void Update()
		{
			this.OnValueChanged(base.transform.position.x);
			this.OnVectorValueChanged(base.transform.position);
		}

		public void ValueTo(float from, float to, float time, EaseType easeType = EaseType.linear)
		{
			Vector3 position = base.transform.position;
			position.x = from;
			base.transform.position = position;
			this.FinalFloatValue = to;
			SA_iTween.MoveTo(base.gameObject, SA_iTween.Hash(new object[]
			{
				"x",
				to,
				"time",
				time,
				"easeType",
				easeType.ToString(),
				"oncomplete",
				"onTweenComplete",
				"oncompletetarget",
				base.gameObject
			}));
		}

		public void VectorTo(Vector3 from, Vector3 to, float time, EaseType easeType = EaseType.linear)
		{
			base.transform.position = from;
			this.FinalVectorValue = to;
			SA_iTween.MoveTo(base.gameObject, SA_iTween.Hash(new object[]
			{
				"position",
				to,
				"time",
				time,
				"easeType",
				easeType.ToString(),
				"oncomplete",
				"onTweenComplete",
				"oncompletetarget",
				base.gameObject
			}));
		}

		public void ScaleTo(Vector3 from, Vector3 to, float time, EaseType easeType = EaseType.linear)
		{
			base.transform.localScale = from;
			this.FinalVectorValue = to;
			SA_iTween.ScaleTo(base.gameObject, SA_iTween.Hash(new object[]
			{
				"scale",
				to,
				"time",
				time,
				"easeType",
				easeType.ToString(),
				"oncomplete",
				"onTweenComplete",
				"oncompletetarget",
				base.gameObject
			}));
		}

		public void VectorToS(Vector3 from, Vector3 to, float speed, EaseType easeType = EaseType.linear)
		{
			base.transform.position = from;
			this.FinalVectorValue = to;
			SA_iTween.MoveTo(base.gameObject, SA_iTween.Hash(new object[]
			{
				"position",
				to,
				"speed",
				speed,
				"easeType",
				easeType.ToString(),
				"oncomplete",
				"onTweenComplete",
				"oncompletetarget",
				base.gameObject
			}));
		}

		public void Stop()
		{
			SA_iTween.Stop(base.gameObject);
			UnityEngine.Object.Destroy(base.gameObject);
		}

		private void onTweenComplete()
		{
			this.OnValueChanged(this.FinalFloatValue);
			this.OnVectorValueChanged(this.FinalVectorValue);
			this.OnComplete();
			if (this.DestoryGameObjectOnComplete)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else
			{
				UnityEngine.Object.Destroy(this);
			}
		}

		public bool DestoryGameObjectOnComplete = true;

		private float FinalFloatValue;

		private Vector3 FinalVectorValue;
	}
}
