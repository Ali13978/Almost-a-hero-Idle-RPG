using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ui
{
	public class ShineAnimation : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.children = new List<RectTransform>();
			IEnumerator enumerator = base.gameObject.GetComponent<RectTransform>().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					RectTransform item = (RectTransform)obj;
					this.children.Add(item);
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
		}

		public override void AahUpdate(float dt)
		{
			for (int i = 0; i < this.children.Count; i++)
			{
				float num = 130f * (Time.realtimeSinceStartup % 0.4f / 0.4f + (float)i - 4f);
				this.children[i].anchoredPosition = new Vector2(-num, num);
			}
		}

		private List<RectTransform> children;
	}
}
