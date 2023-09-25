using System;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelDailyQuest : AahMonoBehaviour
	{
		public void InitStrings()
		{
			this.buttonCollect.text.text = LM.Get("UI_COLLECT");
			this.textHeader.text = LM.Get("DAILY_QUEST");
			this.textAchievements.text = LM.Get("UI_OPTIONS_ACHIEVEMENTS");
			this.textQuest.resizeTextMinSize = 22;
		}

		public void SetDetails(bool hasTimeFromServer, DailyQuest dailyQuest, DateTime dailyQuestCollectedTime, bool canCollect)
		{
			if (dailyQuest != null)
			{
				this.textHeader.gameObject.SetActive(true);
				this.parentDailyQuest.gameObject.SetActive(true);
				this.parentComeBack.gameObject.SetActive(false);
				this.textQuest.text = dailyQuest.GetQuestString();
				this.buttonCollect.interactable = canCollect;
				this.textProgress.text = string.Format("{0} / {1}", Mathf.Min(dailyQuest.progress, dailyQuest.goal).ToString(), dailyQuest.goal.ToString());
				this.barProgress.SetScale(1f * (float)Mathf.Min(dailyQuest.progress, dailyQuest.goal) / (float)dailyQuest.goal);
			}
			else if (hasTimeFromServer)
			{
				this.parentDailyQuest.gameObject.SetActive(false);
				this.parentComeBack.gameObject.SetActive(true);
				this.textHeader.gameObject.SetActive(false);
				string timeString = GameMath.GetTimeString(DailyQuest.GetTimeBetweenQuests() - (TrustedTime.Get() - dailyQuestCollectedTime).TotalSeconds);
				this.textComeBack.text = string.Format(LM.Get("DAILY_QUEST_COME_BACK"), "<size=60>" + timeString + "</size>");
			}
			else
			{
				this.parentDailyQuest.gameObject.SetActive(false);
				this.parentComeBack.gameObject.SetActive(true);
				this.textHeader.gameObject.SetActive(false);
				this.textComeBack.text = LM.Get("UI_ACHIEVEMENTS_DAILY_SERVER_WARNING");
			}
		}

		public void SetWaitForServer()
		{
			throw new NotImplementedException();
		}

		public Text textHeader;

		public Text textComeBack;

		public Scaler barProgress;

		public Text textQuest;

		public Text textProgress;

		public GameButton buttonCollect;

		public GameButton buttonSkip;

		public RectTransform parentDailyQuest;

		public RectTransform parentComeBack;

		public Text textAchievements;

		public Text textCollectAmount;
	}
}
