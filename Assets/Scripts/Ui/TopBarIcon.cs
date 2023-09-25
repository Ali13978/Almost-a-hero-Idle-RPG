using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class TopBarIcon : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.image = base.GetComponent<Image>();
		}

		public void SetImage(int tabIndex)
		{
			this.image.sprite = this.tabIcons[tabIndex];
			this.image.SetNativeSize();
		}

		public Sprite[] tabIcons;

		private Image image;
	}
}
