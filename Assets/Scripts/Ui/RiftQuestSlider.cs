using System;
using System.Text;
using Static;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class RiftQuestSlider : AahMonoBehaviour
	{
		public void SetSlider(double current, double max, bool isLocked = false)
		{
			float scale = (float)GameMath.Clamp(current / max, 0.0, 1.0);
			this.sliderBar.SetScale(scale);
			StringBuilder stringBuilder = StringExtension.StringBuilder;
			stringBuilder = GameMath.GetFlooredDoubleString(current, stringBuilder).Append(" / ");
			this.sliderText.text = GameMath.GetDoubleString(max, stringBuilder).ToString();
			if (this.aeonIconGlow != null)
			{
				this.aeonIconGlow.SetActive(current >= max);
			}
		}

		public double GetSliderVal(double max)
		{
			return (double)this.sliderBar.scale * max;
		}

		public void SetLockState(bool locked)
		{
			this.icon.sprite = ((!locked) ? this.dustSprite : this.lockSprite);
			this.sliderText.gameObject.SetActive(!locked);
			this.aeonIcon.SetActive(!locked);
		}

		public Scaler sliderBar;

		public Text sliderText;

		public Image icon;

		public Sprite dustSprite;

		public Sprite lockSprite;

		public GameObject aeonIcon;

		public GameObject aeonIconGlow;
	}
}
