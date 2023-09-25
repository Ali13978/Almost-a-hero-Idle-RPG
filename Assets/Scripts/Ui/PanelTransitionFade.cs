using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelTransitionFade : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToUpdates();
			base.AddToInits();
		}

		public override void Init()
		{
			this.time = 0.2f;
			this.willCallTransitionEvent = false;
			this.callTransitionEventNow = false;
		}

		public override void AahUpdate(float dt)
		{
			this.time += dt;
			if (this.willCallTransitionEvent && this.time >= 0.2f)
			{
				this.willCallTransitionEvent = false;
				this.callTransitionEventNow = true;
			}
			if (this.time <= 0.2f)
			{
				float a = this.time / 0.2f;
				Color black = Color.black;
				black.a = a;
				this.transitionFadeImage.color = black;
			}
			else if (this.time > 0.2f && this.time < 0.6f)
			{
				this.transitionFadeImage.color = Color.black;
			}
			else if (this.time >= 0.6f && this.time < 0.8f)
			{
				float a2 = 1f - (this.time - 0.2f - 0.4f) / 0.2f;
				Color black2 = Color.black;
				black2.a = a2;
				this.transitionFadeImage.color = black2;
			}
			else
			{
				this.transitionFadeImage.gameObject.SetActive(false);
			}
		}

		public void StartAnim()
		{
			this.time = 0f;
			this.transitionFadeImage.gameObject.SetActive(true);
			this.transitionFadeImage.color = Color.black;
			this.willCallTransitionEvent = true;
		}

		private const float DUR_CLOSE = 0.2f;

		private const float DUR_STAY = 0.4f;

		private const float DUR_OPEN = 0.2f;

		public Image transitionFadeImage;

		private float time;

		public bool callTransitionEventNow;

		private bool willCallTransitionEvent;
	}
}
