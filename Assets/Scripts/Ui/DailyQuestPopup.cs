using System;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class DailyQuestPopup : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.buttonClose.onClick = (this.buttonCloseBg.onClick = new GameButton.VoidFunc(this.OnCloseClicked));
			this.buttonCollect.onClick = new GameButton.VoidFunc(this.OnCollectClicked);
			this.buttonSkip.gameButton.onClick = new GameButton.VoidFunc(this.OnSkipClicked);
		}

		private void OnSkipClicked()
		{
			this.uiManager.OnClickedDailySkip();
		}

		private void OnCollectClicked()
		{
			this.uiManager.OnClickedDailyCollect();
		}

		private void OnCloseClicked()
		{
			this.uiManager.state = UiState.NONE;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
		}

		public void InitStrings()
		{
			this.textHeader.text = LM.Get("DAILY_QUEST");
			this.buttonCollect.text.text = LM.Get("UI_COLLECT");
			this.buttonSkip.textDown.text = LM.Get("UI_SKIP");
		}

		public void UpdatePopup(UiManager manager)
		{
			this.uiManager = manager;
			Simulator sim = manager.sim;
			this.buttonCollect.interactable = sim.CanCollectDailyQuest();
			this.buttonSkip.gameButton.interactable = sim.CanSkipDailyQuest();
			bool flag = sim.CanAffordSkipDailyQuest();
			bool flag2 = sim.CanSkipDailyQuestWithoutCost();
			Currency currency = sim.GetCurrency(CurrencyType.AEON);
			Currency currency2 = sim.GetCurrency(CurrencyType.GEM);
			this.currencyAeon.SetCurrency(CurrencyType.AEON, GameMath.GetFlooredDoubleString(currency.GetAmount()), true, GameMode.STANDARD, true);
			this.currencyCredits.SetCurrency(CurrencyType.GEM, GameMath.GetFlooredDoubleString(currency2.GetAmount()), true, GameMode.STANDARD, true);
			DailyQuest dailyQuest = sim.dailyQuest;
			if (dailyQuest != null)
			{
				this.body.gameObject.SetActive(true);
				this.textComebackLater.gameObject.SetActive(false);
				double rewardAmount = dailyQuest.GetRewardAmount(sim);
				this.collectReward.text = GameMath.GetDoubleString(rewardAmount);
				this.textDescription.text = dailyQuest.GetQuestString();
				this.progressBar.SetScale(1f * (float)Mathf.Min(dailyQuest.progress, dailyQuest.goal) / (float)dailyQuest.goal);
				this.buttonSkip.textUp.text = GameMath.GetDoubleString(sim.GetSkipDailyCost());
				this.textProgressBar.text = string.Format("{0}/{1}", dailyQuest.progress, dailyQuest.goal);
				this.buttonSkip.textUpCantAffordColorChangeForced = !flag;
				this.buttonSkip.textCantAffordColorChangeManual = !flag;
				this.buttonSkip.textDownCantAffordColorChangeForced = !flag2;
			}
			else if (TrustedTime.IsReady())
			{
				this.body.gameObject.SetActive(false);
				this.textComebackLater.gameObject.SetActive(true);
				string timeString = GameMath.GetTimeString(DailyQuest.GetTimeBetweenQuests() - (TrustedTime.Get() - sim.dailyQuestCollectedTime).TotalSeconds);
				this.textComebackLater.text = string.Format(LM.Get("DAILY_QUEST_COME_BACK"), "<size=60>" + timeString + "</size>");
			}
			else
			{
				this.body.gameObject.SetActive(false);
				this.textComebackLater.gameObject.SetActive(true);
				this.textComebackLater.text = LM.Get("UI_ACHIEVEMENTS_DAILY_SERVER_WARNING");
			}
		}

		public Text textHeader;

		public Text textDescription;

		public GameButton buttonClose;

		public GameButton buttonCloseBg;

		public RectTransform popupBg;

		public Text collectReward;

		public GameButton buttonCollect;

		public ButtonUpgradeAnim buttonSkip;

		public RectTransform body;

		public Text textComebackLater;

		public Text textProgressBar;

		public Scaler progressBar;

		public MenuShowCurrency currencyAeon;

		public MenuShowCurrency currencyCredits;

		private UiManager uiManager;
	}
}
