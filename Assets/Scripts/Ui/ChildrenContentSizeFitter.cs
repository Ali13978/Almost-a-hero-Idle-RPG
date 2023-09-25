using System;
using System.Collections;
using UnityEngine;

namespace Ui
{
	public class ChildrenContentSizeFitter : AahMonoBehaviour
	{
		public override void Register()
		{
			if (this.callOnUpdate)
			{
				base.AddToUpdates();
			}
		}

		public void SetSize(float forceAdd = 0f, bool dontIgnoreDisableds = false)
		{
			RectTransform component = base.GetComponent<RectTransform>();
			float num = this.GetMinY(component, dontIgnoreDisableds) - this.yOffset;
			component.sizeDelta = new Vector2(component.sizeDelta.x, Mathf.Max(0f, -num) + forceAdd);
		}

		private float GetMinY(RectTransform rectTransform, bool dontIgnoreDisableds)
		{
			float num = 0f;
			IEnumerator enumerator = rectTransform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					RectTransform rectTransform2 = (RectTransform)obj;
					if ((rectTransform2.gameObject.activeSelf || dontIgnoreDisableds) && rectTransform2.gameObject.GetComponent<DoNotCountInSizeFitter>() == null)
					{
						float num2 = rectTransform2.anchoredPosition.y;
						float num3 = -rectTransform2.sizeDelta.y * rectTransform2.localScale.y;
						float minY = this.GetMinY(rectTransform2, dontIgnoreDisableds);
						if (minY < num3)
						{
							num3 = minY;
						}
						num2 += num3;
						if (num2 < num)
						{
							num = num2;
						}
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return num * rectTransform.localScale.y;
		}

		public override void AahUpdate(float dt)
		{
			this.SetSize(0f, false);
		}

		public bool callOnUpdate;

		public float yOffset = 100f;
	}
}
