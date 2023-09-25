using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelTopHudChallenge : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.glowTimer = 0f;
			this.textTimeGlowMaxAlpha = this.textTimeGlow0.color.a;
		}

		public override void AahUpdate(float dt)
		{
			if (this.glowTimer > 0f)
			{
				this.glowTimer -= dt;
				if (this.glowTimer < this.glowTransitionPeriod)
				{
					this.imageGlow.color = new Color(this.imageGlow.color.r, this.imageGlow.color.g, this.imageGlow.color.b, this.glowTimer / this.glowTransitionPeriod);
					this.textGlow.color = new Color(this.textGlow.color.r, this.textGlow.color.g, this.textGlow.color.b, this.imageGlow.color.a);
				}
				else if (this.glowTimer < this.glowPeriod - this.glowTransitionPeriod)
				{
					this.imageGlow.color = new Color(this.imageGlow.color.r, this.imageGlow.color.g, this.imageGlow.color.b, 1f);
					this.textGlow.color = new Color(this.textGlow.color.r, this.textGlow.color.g, this.textGlow.color.b, this.imageGlow.color.a);
				}
				else
				{
					float num = (this.glowPeriod - this.glowTimer) / this.glowTransitionPeriod;
					if (num > this.imageGlow.color.a)
					{
						this.imageGlow.color = new Color(this.imageGlow.color.r, this.imageGlow.color.g, this.imageGlow.color.b, num);
						this.textGlow.color = new Color(this.textGlow.color.r, this.textGlow.color.g, this.textGlow.color.b, this.imageGlow.color.a);
					}
				}
			}
			else if (this.imageGlow.color.a != 0f)
			{
				this.imageGlow.color = new Color(this.imageGlow.color.r, this.imageGlow.color.g, this.imageGlow.color.b, 0f);
				this.textGlow.color = new Color(this.textGlow.color.r, this.textGlow.color.g, this.textGlow.color.b, this.imageGlow.color.a);
			}
			if (this.timeGlowTimer > 0f)
			{
				this.textTimeGlow0.gameObject.SetActive(true);
				float t = Easing.Linear(this.timeGlowPeriod - this.timeGlowTimer, 0f, 1f, this.timeGlowPeriod);
				float d = Easing.CircEaseOut(t, 0.5f, 0.75f, 1f);
				this.textTimeGlow0.rectTransform.localScale = Vector3.one * d;
				this.timeGlowTimer -= dt;
				if (this.timeGlowTimer < this.timeGlowPeriod - this.timeGlowTransitionPeriod)
				{
					float num2 = this.timeGlowTimer / (this.timeGlowPeriod - this.timeGlowTransitionPeriod);
					this.textTimeGlow0.color = new Color(this.textTimeGlow0.color.r, this.textTimeGlow0.color.g, this.textTimeGlow0.color.b, num2 * this.textTimeGlowMaxAlpha);
				}
				else
				{
					float num3 = (this.timeGlowPeriod - this.timeGlowTimer) / this.timeGlowTransitionPeriod;
					if (num3 > this.imageGlow.color.a)
					{
						this.textTimeGlow0.color = new Color(this.textTimeGlow0.color.r, this.textTimeGlow0.color.g, this.textTimeGlow0.color.b, num3 * this.textTimeGlowMaxAlpha);
					}
				}
			}
			else
			{
				this.textTimeGlow0.gameObject.SetActive(false);
			}
		}

		public void SetProgress(int currentWave, int totWave)
		{
			string text = Mathf.Min(totWave, currentWave).ToString() + " / " + totWave.ToString();
			if (text != this.textProgress.text)
			{
				this.textProgress.text = text;
				this.textGlow.text = currentWave.ToString();
			}
			totWave = GameMath.GetMaxInt(1, totWave);
			float minFloat = GameMath.GetMinFloat(GameMath.GetMaxFloat(1f * (float)currentWave / (float)totWave, 0f), 1f);
			this.barProgress.SetScale(minFloat);
			if (currentWave > this.currentWaveOld)
			{
				this.glowTimer = this.glowPeriod;
			}
			this.currentWaveOld = currentWave;
		}

		public void SetTime(float timeInSeconds, float dt)
		{
			string timeString = GameMath.GetTimeString(timeInSeconds);
			if (this.textTime.text != timeString)
			{
				this.textTime.text = timeString;
			}
			int num = Mathf.FloorToInt(timeInSeconds);
			if (num < 10)
			{
				if (Mathf.Floor(timeInSeconds + dt) > Mathf.Floor(timeInSeconds))
				{
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTimeChallengeCountdown, 1f));
				}
				string text = num.ToString();
				if (this.textTimeGlow0.text != text)
				{
					this.textTimeGlow0.text = text;
				}
				if (num < this.timeInSecondsOld)
				{
					this.timeGlowTimer = this.timeGlowPeriod;
				}
			}
			this.timeInSecondsOld = num;
		}

		public Image imageProgress;

		public Text textProgress;

		public Text textModeName;

		public Text textTime;

		public Text textTimeGlow0;

		private float textTimeGlowMaxAlpha;

		private float timeGlowPeriod = 1f;

		private float timeGlowTimer;

		private float timeGlowTransitionPeriod = 0.3f;

		public Text textGold;

		public Image imageGold;

		public Scaler barProgress;

		public Image imageGlow;

		public Text textGlow;

		private int currentWaveOld;

		private int timeInSecondsOld;

		private float glowPeriod = 1.6f;

		private float glowTimer;

		private float glowTransitionPeriod = 0.3f;

		public Sprite spriteTimeNormal;

		public Sprite spriteTimeAlarming;

		public Image imageTimeHolder;

		public RectTransform riftEffectsParent;

		public Button openRiftEffectsInfoButton;

		public Button openCurseEffectsInfoButton;

		public List<Image> riftEffectIcons;

		public CharmEffectWidget curseEffectWidgetPrefab;

		public RectTransform curseEffectParents;

		public RectTransform curseEffectWidgetParents;

		public Slider curseEffectsProgress;

		public List<CharmEffectWidget> curseEffectIcons;

		[NonSerialized]
		public bool initializeCursesWidgets = true;
	}
}
