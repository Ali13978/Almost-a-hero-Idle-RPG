using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class RingBar : MonoBehaviour
	{
		public void SetOnOff(bool exists, bool isOn)
		{
			this.imageOn.enabled = (exists && isOn);
			this.imageOff.enabled = (exists && !isOn);
		}

		public Image imageOff;

		public Image imageOn;
	}
}
