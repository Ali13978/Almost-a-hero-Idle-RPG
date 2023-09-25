using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
	public class ShortOfferTimePanel : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.timerSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnSliderChange));
			this.timerSlider.value = 1f;
			this.chests.onValueChanged.AddListener(new UnityAction<bool>(this.OnChestStateChange));
			this.specialOffers.onValueChanged.AddListener(new UnityAction<bool>(this.OnSpecialOfferChange));
			this.flashOffers.onValueChanged.AddListener(new UnityAction<bool>(this.OnFlashOfferChange));
			this.mines.onValueChanged.AddListener(new UnityAction<bool>(this.OnMinesChange));
			this.dailyQuest.onValueChanged.AddListener(new UnityAction<bool>(this.OnDailyChange));
			this.restBonus.onValueChanged.AddListener(new UnityAction<bool>(this.OnRestBonusChange));
			this.cursedGate.onValueChanged.AddListener(new UnityAction<bool>(this.OnCursedGateChange));
			this.seasonal.onValueChanged.AddListener(new UnityAction<bool>(this.OnSeasonalChange));
			this.textMax.text = ShortOfferTimePanel.timerDurations.GetLastItem<float>() + "s";
			this.textMin.text = ShortOfferTimePanel.timerDurations[0] + "s";
			Cheats.ShorfOfferTimeFlags shortOfferTimeSettings = Cheats.shortOfferTimeSettings;
			this.chests.isOn = shortOfferTimeSettings.chests;
			this.specialOffers.isOn = shortOfferTimeSettings.specialOffers;
			this.flashOffers.isOn = shortOfferTimeSettings.flashOffers;
			this.mines.isOn = shortOfferTimeSettings.mines;
			this.dailyQuest.isOn = shortOfferTimeSettings.dailyQuests;
			this.restBonus.isOn = shortOfferTimeSettings.restBonus;
		}

		private void OnSeasonalChange(bool arg0)
		{
			Cheats.shortOfferTimeSettings.seasonal = arg0;
		}

		private void OnCursedGateChange(bool arg0)
		{
			Cheats.shortOfferTimeSettings.cursedGate = arg0;
		}

		private void OnRestBonusChange(bool arg0)
		{
			Cheats.shortOfferTimeSettings.restBonus = arg0;
		}

		private void OnDailyChange(bool arg0)
		{
			Cheats.shortOfferTimeSettings.dailyQuests = arg0;
		}

		private void OnMinesChange(bool arg0)
		{
			Cheats.shortOfferTimeSettings.mines = arg0;
		}

		private void OnFlashOfferChange(bool arg0)
		{
			Cheats.shortOfferTimeSettings.flashOffers = arg0;
		}

		private void OnSpecialOfferChange(bool arg0)
		{
			Cheats.shortOfferTimeSettings.specialOffers = arg0;
		}

		private void OnChestStateChange(bool arg0)
		{
			Cheats.shortOfferTimeSettings.chests = arg0;
		}

		private void OnSliderChange(float sliderValue)
		{
			int num = (int)sliderValue;
			this.currentTimeDuration = ShortOfferTimePanel.timerDurations[num];
			this.textCurrentDuration.text = this.currentTimeDuration + "s";
			Cheats.shortOfferTimeMultiplier = this.currentTimeDuration / 60f;
		}

		public Slider timerSlider;

		public Text textCurrentDuration;

		public Text textMax;

		public Text textMin;

		public Toggle chests;

		public Toggle specialOffers;

		public Toggle flashOffers;

		public Toggle mines;

		public Toggle dailyQuest;

		public Toggle restBonus;

		public Toggle cursedGate;

		public Toggle seasonal;

		private float currentTimeDuration;

		private static float[] timerDurations = new float[]
		{
			5f,
			10f,
			20f,
			40f,
			80f,
			120f,
			200f,
			300f,
			450f,
			600f
		};
	}
}
