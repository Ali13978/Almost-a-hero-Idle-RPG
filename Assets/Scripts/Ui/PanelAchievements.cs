using System;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using Simulation;
using Static;
using stats;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelAchievements : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.tabAchievementsButton.onClick = new GameButton.VoidFunc(this.OnAchievementsTabButtonClicked);
			this.tabStatsButton.onClick = new GameButton.VoidFunc(this.OnStatsTabButtonClicked);
			this.GoToAchievementsTab();
			if (this.stats == null)
			{
				this.InitStats();
			}
			this.badges = new List<BadgeWidget>();
			this.badgeWidgetSize = this.badgeWidgetPrefab.rectTransform.sizeDelta;
			this.gapBetweenBadges = (this.badgesParent.sizeDelta.x - 45f - this.badgeWidgetSize.x * 4f) / 3f + this.badgeWidgetSize.x;
			this.badgeDescriptionParentHeight = this.badgeDescriptionParent.sizeDelta.y + 20f;
			this.badgesParentHeight = 45f + this.badgeWidgetSize.y;
			this.badgeDescriptionParent.SetScaleY(0f);
		}

		public void InitStrings()
		{
			string text = LM.Get("UI_OPTIONS_ACHIEVEMENTS");
			string languageID_N = LM.GetLanguageID_N(LM.currentLanguage);
			CultureInfo culture = new CultureInfo(languageID_N);
			text = text.ToUpper(culture);
			this.tabAchievementsButton.text.text = text;
			this.tabStatsButton.text.text = LM.Get("UI_TAB_STATS");
			this.textHeader.text = LM.Get("UI_BACK");
			this.achievementsSectionHeader.text = LM.Get("UI_OPTIONS_ACHIEVEMENTS").ToUpper(LM.culture);
			this.badgesHeader.text = LM.Get("UI_BADGES");
			this.noBadgesEarnedLabel.text = LM.Get("UI_NO_BADGES_EARNED");
			this.panelQuestOfUpdate.InitStrings();
			foreach (PanelAchievementOfUpdate panelAchievementOfUpdate in this.panelQuestOfUpdates)
			{
				panelAchievementOfUpdate.InitStrings();
			}
			foreach (PanelAchievement panelAchievement in this.panels)
			{
				panelAchievement.InitStrings();
			}
			if (this.stats == null)
			{
				this.InitStats();
			}
			for (int i = 0; i < this.stats.Count; i++)
			{
				StatWidget statWidget = this.stats[i];
				PlayerStat playerStat = PlayerStat.playerStats[i];
				statWidget.newTag.text = LM.Get("UI_NEW");
				statWidget.newTag.transform.parent.gameObject.SetActive(false);
				statWidget.statValue.gameObject.SetActive(true);
			}
		}

		private void InitStats()
		{
			this.stats = new List<StatWidget>();
			Utility.FillUiElementList<StatWidget>(this.statWidgetPrefab, this.statsContentParent, PlayerStat.playerStats.Count, this.stats);
			for (int i = this.stats.Count - 1; i >= 0; i--)
			{
				StatWidget statWidget = this.stats[i];
				statWidget.rectTransform.SetAnchorPosY(-20f + (float)i * -statWidget.rectTransform.sizeDelta.y);
				statWidget.background.enabled = (i % 2 == 0);
			}
		}

		public void OnPanelOpened(Simulator simulator)
		{
			List<PlayerStat> activeStats = PlayerStat.GetActiveStats(simulator);
			int count = activeStats.Count;
			int num = 0;
			for (int i = 0; i < this.stats.Count; i++)
			{
				StatWidget statWidget = this.stats[i];
				if (i < count)
				{
					statWidget.gameObject.SetActive(true);
					statWidget.rectTransform.SetAnchorPosY(-20f + (float)num * -statWidget.rectTransform.sizeDelta.y);
					statWidget.background.enabled = (num % 2 == 0);
					PlayerStat playerStat = activeStats[i];
					if (simulator.newStats.Contains(playerStat.id))
					{
						statWidget.newTag.transform.parent.gameObject.SetActive(true);
						statWidget.statValue.gameObject.SetActive(false);
					}
					else
					{
						statWidget.newTag.transform.parent.gameObject.SetActive(false);
						statWidget.statValue.gameObject.SetActive(true);
					}
					if (!playerStat.isUpdatedEveryFrame)
					{
						statWidget.statValue.text = playerStat.GetValue(simulator);
					}
					statWidget.statName.text = LM.Get(playerStat.nameKey);
					num++;
				}
				else
				{
					statWidget.gameObject.SetActive(false);
				}
			}
			this.UpdateBadgesList(simulator);
		}

		public void UpdatePanel(Simulator sim, UiManager uiManager)
		{
			if (this.isShowingStats)
			{
				this.UpdateStatsTab(sim);
			}
			else
			{
				this.UpdateAchievementsTab(sim, uiManager);
			}
		}

		public void GoToAchievementsTab()
		{
			this.isShowingStats = false;
			this.tabAchievementsButton.interactable = false;
			this.tabStatsButton.interactable = true;
			this.tabAchievements.SetActive(true);
			this.tabStats.SetActive(false);
		}

		private void UpdateStatsTab(Simulator sim)
		{
			List<PlayerStat> activeStats = PlayerStat.GetActiveStats(sim);
			int count = activeStats.Count;
			for (int i = 0; i < this.stats.Count; i++)
			{
				StatWidget statWidget = this.stats[i];
				if (i < count)
				{
					PlayerStat playerStat = activeStats[i];
					if (playerStat.isUpdatedEveryFrame)
					{
						statWidget.statValue.text = playerStat.GetValue(sim);
					}
				}
			}
		}

		private void UpdateAchievementsTab(Simulator sim, UiManager uiManager)
		{
			this.badgesSectionParent.SetAnchorPosY(0f);
			this.achievementsPivot.SetAnchorPosY(this.badgesParent.anchoredPosition.y - this.badgesParentHeight);
			float num = 0f;
			if (sim.HasQuestOfUpdate())
			{
				num = 170f;
				QuestOfUpdate questOfUpdate = sim.questOfUpdate;
				bool interactable = questOfUpdate.IsCompleted();
				this.panelQuestOfUpdate.gameObject.SetActive(true);
				this.panelQuestOfUpdate.barProgress.SetScale(questOfUpdate.GetProgress());
				this.panelQuestOfUpdate.buttonCollect.interactable = interactable;
				this.panelQuestOfUpdate.textReward.text = "+" + GameMath.GetDoubleString(questOfUpdate.reward.GetAmount());
				this.panelQuestOfUpdate.textHeader.text = QuestOfUpdate.GetQuestName(questOfUpdate.id);
				this.panelQuestOfUpdate.textDesc.text = QuestOfUpdate.GetQuestDesc(questOfUpdate.id);
				this.panelQuestOfUpdate.icon.sprite = uiManager.GetSpriteQuestOfUpdate(questOfUpdate.id);
				this.panelQuestOfUpdate.iconColorBg.sprite = this.achievementBackgrounds[6];
				string text = string.Empty;
				if (questOfUpdate.IsCompleted())
				{
					text = LM.Get("QUEST_OF_UPDATE_COMPLETED_WARNING");
					this.panelQuestOfUpdate.textDescTime.text = text;
				}
				else if (TrustedTime.IsReady())
				{
					text = GameMath.GetTimeString(TimeSpan.FromSeconds(questOfUpdate.GetRemainingDuration(TrustedTime.Get())));
					this.panelQuestOfUpdate.textDescTime.text = string.Format(LM.Get("QUEST_OF_UPDATE_TIME_WARNING"), text);
				}
				else
				{
					text = LM.Get("QUEST_OF_UPDATE_TIME_WARNING");
					this.panelQuestOfUpdate.textDescTime.text = LM.Get("UI_SHOP_CHEST_0_WAIT");
				}
			}
			else
			{
				this.panelQuestOfUpdate.gameObject.SetActive(false);
			}
			float num2 = 0f;
			if (UiManager.stateJustChanged)
			{
				for (int i = 0; i < this.panelQuestOfUpdates.Count; i++)
				{
					PanelAchievementOfUpdate panelAchievementOfUpdate = this.panelQuestOfUpdates[i];
					if (i < sim.completedQuestOfUpdates.Count)
					{
						int num3 = sim.completedQuestOfUpdates[i];
						if (num3 != QuestOfUpdateIds.Anniversary01)
						{
							panelAchievementOfUpdate.gameObject.SetActive(true);
							panelAchievementOfUpdate.rectTransform.anchoredPosition = new Vector2(0f, num2 - 68f - num);
							panelAchievementOfUpdate.SetAchieved();
							panelAchievementOfUpdate.textHeader.text = QuestOfUpdate.GetQuestName(num3);
							panelAchievementOfUpdate.textDescTime.text = QuestOfUpdate.GetQuestDesc(num3);
							panelAchievementOfUpdate.icon.sprite = uiManager.GetSpriteQuestOfUpdate(num3);
							panelAchievementOfUpdate.iconColorBg.sprite = this.achievementBackgrounds[5];
							num2 -= 175f;
						}
					}
					else
					{
						panelAchievementOfUpdate.gameObject.SetActive(false);
					}
				}
				int j = 0;
				int count = this.panels.Count;
				while (j < count)
				{
					this.panels[j].SetState(sim, this);
					int num4 = PlayerStats.achievementIndexes[j];
					this.panels[j].rectTransform.anchoredPosition = new Vector2(0f, num2 - 175f * (float)num4 - 68f - num);
					j++;
				}
				this.contentSizeFitter.SetSize(0f, false);
			}
			this.menuShowCurrencyGem.SetCurrency(CurrencyType.GEM, sim.GetCredits().GetString(), true, GameMode.STANDARD, true);
			if (sim.hasDailies)
			{
				this.menuShowCurrencyAeon.gameObject.SetActive(true);
				this.menuShowCurrencyAeon.SetCurrency(CurrencyType.AEON, sim.GetAeons().GetString(), true, GameMode.STANDARD, true);
			}
			else
			{
				this.menuShowCurrencyAeon.gameObject.SetActive(false);
			}
		}

		private void UpdateBadgesList(Simulator simulator)
		{
			List<Badge> displayableBadges = Badges.GetDisplayableBadges(simulator);
			int count = displayableBadges.Count;
			if (count != this.badges.Count)
			{
				Utility.FillUiElementList<BadgeWidget>(this.badgeWidgetPrefab, this.badgesParent, count, this.badges);
				int maxInt = GameMath.GetMaxInt(1, GameMath.CeilToInt((float)count / 4f));
				this.badgesParentHeight = 45f + (float)maxInt * this.badgeWidgetSize.y;
				if (maxInt > 1)
				{
					this.badgesParentHeight += (float)(maxInt - 1) * 11f;
				}
				for (int i = 0; i < count; i++)
				{
					BadgeWidget badgeWidget = this.badges[i];
					Badge badge = displayableBadges[i];
					badgeWidget.Init(badge, new Action<BadgeWidget>(this.OnBadgeClicked), badge.HasBeenEarnedByPlayer(simulator), badge.IsNotificationEnabled(simulator));
					badgeWidget.rectTransform.anchoredPosition = new Vector2((float)(i % 4) * this.gapBetweenBadges + 22.5f, -(22.5f + (this.badgeWidgetSize.y + 11f) * (float)(i / 4)));
				}
			}
			else
			{
				for (int j = 0; j < count; j++)
				{
					BadgeWidget badgeWidget2 = this.badges[j];
					Badge badge2 = displayableBadges[j];
					badgeWidget2.Init(badge2, new Action<BadgeWidget>(this.OnBadgeClicked), badge2.HasBeenEarnedByPlayer(simulator), badge2.IsNotificationEnabled(simulator));
					badgeWidget2.rectTransform.anchoredPosition = new Vector2((float)(j % 4) * this.gapBetweenBadges + 22.5f, -(22.5f + (this.badgeWidgetSize.y + 11f) * (float)(j / 4)));
				}
			}
			if (this.selectedBadge != null)
			{
				this.selectedBadge.scalerPivot.SetScale(1f);
				this.badgeDescriptionParent.SetScaleY(0f);
				this.selectedBadge = null;
			}
			this.noBadgesEarnedLabel.enabled = (count == 0);
			this.badgesParent.SetSizeDeltaY(this.badgesParentHeight);
			this.parentAchievements.SetAnchorPosY(0f);
		}

		private void OnAchievementsTabButtonClicked()
		{
			if (this.isShowingStats)
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
				this.GoToAchievementsTab();
			}
			else
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiDefaultFailClick, 1f));
			}
		}

		private void OnStatsTabButtonClicked()
		{
			if (this.isShowingStats)
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiDefaultFailClick, 1f));
			}
			else
			{
				this.isShowingStats = true;
				this.tabAchievementsButton.interactable = true;
				this.tabStatsButton.interactable = false;
				this.tabAchievements.SetActive(false);
				this.tabStats.SetActive(true);
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTabSwitch, 1f));
			}
			this.uiManager.SetCommand(new UiCommandStatsSeen());
		}

		private void OnBadgeClicked(BadgeWidget badge)
		{
			if (this.selectedBadge == null || this.selectedBadge != badge)
			{
				badge.Badge.DimissNotification();
				badge.DisableNotifaction();
				if (this.selectedBadge != null)
				{
					this.selectedBadge.scalerPivot.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
					int num = this.badges.IndexOf(this.selectedBadge) / 4;
					int num2 = this.badges.IndexOf(badge) / 4;
					this.selectedBadge.scalerPivot.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
					if (num != num2)
					{
						int i = 0;
						int count = this.badges.Count;
						while (i < count)
						{
							this.badges[i].rectTransform.anchoredPosition = new Vector2((float)(i % 4) * this.gapBetweenBadges + 22.5f, -(22.5f + (this.badgeWidgetSize.y + 11f) * (float)(i / 4)));
							i++;
						}
						this.badgeDescriptionParent.SetScaleY(0f);
						this.ExpandBadgeDescriptionOnRow(num2, badge);
					}
					else
					{
						this.pointingArrow.DOAnchorPosX(badge.rectTransform.anchoredPosition.x - 20f + this.badgeWidgetSize.x * 0.5f, 0.2f, false).SetEase(Ease.InOutBack);
					}
				}
				else
				{
					int rowIndex = this.badges.IndexOf(badge) / 4;
					this.ExpandBadgeDescriptionOnRow(rowIndex, badge);
				}
				this.selectedBadge = badge;
				this.selectedBadge.scalerPivot.DOScale(1.2f, 0.2f).SetEase(Ease.OutBack);
				this.badgeDescription.text = this.selectedBadge.Badge.Description;
			}
			else
			{
				this.selectedBadge.scalerPivot.DOScale(1f, 0.2f).SetEase(Ease.InBack);
				int num3 = this.badges.IndexOf(this.selectedBadge) / 4;
				int j = 4 * (num3 + 1);
				int count2 = this.badges.Count;
				while (j < count2)
				{
					this.badges[j].rectTransform.DOAnchorPosY(-(22.5f + (this.badgeWidgetSize.y + 11f) * (float)(j / 4)), 0.2f, false).SetEase(Ease.InBack);
					j++;
				}
				this.badgeDescriptionParent.DOScaleY(0f, 0.1f).SetEase(Ease.InBack);
				this.badgesParent.DOSizeDeltaY(this.badgesParentHeight, 0.2f, false).SetEase(Ease.InBack);
				this.parentAchievements.DOAnchorPosY(0f, 0.2f, false).SetEase(Ease.InBack);
				this.selectedBadge = null;
			}
		}

		private void ExpandBadgeDescriptionOnRow(int rowIndex, BadgeWidget badgeWidget)
		{
			this.pointingArrow.SetAnchorPosX(badgeWidget.rectTransform.anchoredPosition.x - 20f + this.badgeWidgetSize.x * 0.5f);
			int i = 4 * (rowIndex + 1);
			int count = this.badges.Count;
			while (i < count)
			{
				int num = i / 4;
				this.badges[i].rectTransform.DOAnchorPosY(-(22.5f + this.badgeWidgetSize.y * (float)num + 11f * (float)(num - 1)) - this.badgeDescriptionParentHeight, 0.2f, false).SetEase(Ease.OutBack);
				i++;
			}
			this.badgeDescriptionParent.SetAnchorPosY(this.badgesParent.anchoredPosition.y + badgeWidget.rectTransform.anchoredPosition.y - this.badgeWidgetSize.y - 10f);
			this.badgeDescriptionParent.DOScaleY(1f, 0.2f).SetEase(Ease.OutBack);
			this.badgesParent.DOSizeDeltaY(this.badgesParentHeight + this.badgeDescriptionParentHeight, 0.2f, false).SetEase(Ease.OutBack);
			this.parentAchievements.DOAnchorPosY(-this.badgeDescriptionParentHeight, 0.2f, false).SetEase(Ease.OutBack);
		}

		[Header("Header")]
		public GameButton tabAchievementsButton;

		public GameButton tabStatsButton;

		public GameObject tabAchievements;

		public GameObject tabStats;

		public GameButton buttonBack;

		[Header("Achievements Tab")]
		public ScrollRect scrollview;

		public RectTransform rectTransformContent;

		public Text textHeader;

		public MenuShowCurrency menuShowCurrencyGem;

		public MenuShowCurrency menuShowCurrencyAeon;

		public List<PanelAchievementOfUpdate> panelQuestOfUpdates;

		public PanelAchievementOfUpdate panelQuestOfUpdate;

		public RectTransform parentAchievements;

		public ChildrenContentSizeFitter contentSizeFitter;

		public Sprite[] achievementBackgrounds;

		[SerializeField]
		private RectTransform achievementsPivot;

		[SerializeField]
		private Text achievementsSectionHeader;

		[SerializeField]
		private RectTransform badgesSectionParent;

		[SerializeField]
		private Text badgesHeader;

		[SerializeField]
		private Text noBadgesEarnedLabel;

		[SerializeField]
		private RectTransform badgesParent;

		[SerializeField]
		private BadgeWidget badgeWidgetPrefab;

		[SerializeField]
		private RectTransform badgeDescriptionParent;

		[SerializeField]
		private Text badgeDescription;

		[SerializeField]
		private RectTransform pointingArrow;

		[Header("Stats Tab")]
		public RectTransform statsContentParent;

		public StatWidget statWidgetPrefab;

		[NonSerialized]
		public UiManager uiManager;

		[NonSerialized]
		public List<PanelAchievement> panels;

		[NonSerialized]
		public List<StatWidget> stats;

		[NonSerialized]
		public bool isShowingStats;

		[NonSerialized]
		public UiState lastState;

		[NonSerialized]
		public GameMode lastGameMode;

		private const int BadgesPerRow = 4;

		private const float BadgesContainerSidesGap = 22.5f;

		private const float GapBetweenBadgeRows = 11f;

		private const float GapBetweenBadgeAndDescription = 10f;

		private BadgeWidget selectedBadge;

		private List<BadgeWidget> badges;

		private Vector2 badgeWidgetSize;

		private float gapBetweenBadges;

		private float badgeDescriptionParentHeight;

		private float badgesParentHeight;
	}
}
