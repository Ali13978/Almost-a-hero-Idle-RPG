using System;
using UnityEngine.UI;

namespace Ui
{
	public class SkillScreenPath : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.image = base.GetComponent<Image>();
		}

		public void SetOnOff(bool isOn)
		{
			this.image.enabled = isOn;
		}

		private Image image;
	}
}
