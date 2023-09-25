using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class HorizontalLayout : AahMonoBehaviour
	{
		public int childCount
		{
			get
			{
				return this._childCount;
			}
			set
			{
				this._childCount = value;
				this.SetChildren();
			}
		}

		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.rectTransform = base.GetComponent<RectTransform>();
			this.children = new List<Image>();
			IEnumerator enumerator = this.rectTransform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					RectTransform rectTransform = (RectTransform)obj;
					this.children.Add(rectTransform.GetComponent<Image>());
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
			this.childCount = this.children.Count;
		}

		public void SetChildren()
		{
			if (this.childCount != this.children.Count)
			{
				if (this.childCount < 0)
				{
					this.childCount = 0;
				}
				if (this.children.Count > this.childCount)
				{
					int i = this.childCount;
					int count = this.children.Count;
					while (i < count)
					{
						UnityEngine.Object.Destroy(this.children[i].gameObject);
						i++;
					}
				}
				else if (this.childCount > this.children.Count)
				{
					int j = 0;
					int num = this.childCount - this.children.Count;
					while (j < num)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.childPrefab, this.rectTransform);
						j++;
					}
				}
				this.children = new List<Image>();
				int num2 = 0;
				IEnumerator enumerator = this.rectTransform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						RectTransform rectTransform = (RectTransform)obj;
						if (num2 >= this.childCount)
						{
							break;
						}
						this.children.Add(rectTransform.GetComponent<Image>());
						num2++;
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
				if (this.scaleChildren)
				{
					this.scaleX = this.rectTransform.sizeDelta.x - (float)(this.children.Count - 1) * this.spacing;
					this.scaleX /= this.childPrefab.GetComponent<RectTransform>().sizeDelta.x * (float)this.children.Count;
				}
				else
				{
					this.scaleX = 1f;
				}
				int k = 0;
				int count2 = this.children.Count;
				while (k < count2)
				{
					this.ScaleAndPositionChild(this.children[k].rectTransform, k);
					k++;
				}
				this.childCount = this.children.Count;
			}
		}

		private void ScaleAndPositionChild(RectTransform rt, int index)
		{
			float num = rt.sizeDelta.x * this.scaleX;
			float num2 = (float)index * (num + this.spacing) + num / 2f;
			num2 -= this.rectTransform.sizeDelta.x * 0.5f;
			rt.anchoredPosition = new Vector2(num2, 0f);
			rt.localScale = new Vector3(this.scaleX, 1f, 1f);
		}

		public GameObject childPrefab;

		private RectTransform rectTransform;

		private int _childCount;

		public List<Image> children;

		public bool scaleChildren;

		public float spacing;

		public float scaleX;
	}
}
