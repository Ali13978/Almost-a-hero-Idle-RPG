using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelAdvancedOptions : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.widgets = new List<OptionWidget>();
			this.widgets.Add(this.compassWidget);
			this.widgets.Add(this.lowPowerWidget);
			this.widgets.Add(this.notificationsWidget);
			this.widgets.Add(this.appSleepWidget);
			this.widgets.Add(this.cooldownUiWidget);
			this.widgets.Add(this.notationWidget);
			this.csf.SetSize(10f, false);
			this.InitStrings();
		}

		private OptionWidget CreateWidget(float yPos)
		{
			OptionWidget optionWidget = UnityEngine.Object.Instantiate<OptionWidget>(this.optionWidgetPrefab, this.widgetsParent);
			optionWidget.rectTransform.anchoredPosition = new Vector2(0f, yPos);
			return optionWidget;
		}

		public void InitStrings()
		{
			this.headerText.text = LM.Get("OPTIONS_ADVANCED");
			this.compassWidget.headerText.text = LM.Get("UI_OFFLINE_COMPASS");
			this.compassWidget.descriptionText.text = LM.Get("UI_OFFLINE_COMPASS_DESC");
			this.lowPowerWidget.headerText.text = LM.Get("UI_OPTIONS_LOW_POWER");
			this.lowPowerWidget.descriptionText.text = LM.Get("UI_OPTIONS_LOW_POWER_DESC");
			this.appSleepWidget.headerText.text = LM.Get("UI_OPTIONS_APP_SLEEP");
			this.appSleepWidget.descriptionText.text = LM.Get("UI_OPTIONS_APP_SLEEP_DESC");
			this.notationWidget.headerText.text = LM.Get("UI_OPTIONS_NOTATION");
			this.notationWidget.descriptionText.text = LM.Get("UI_OPTIONS_NOTATION_DESC");
			this.notificationsWidget.headerText.text = LM.Get("NOTIFICATIONS");
			this.notificationsWidget.descriptionText.text = LM.Get("NOTIFICATIONS_DESC");
			this.cooldownUiWidget.headerText.text = LM.Get("UI_OPTIONS_SECONDARY_CDS");
			this.cooldownUiWidget.descriptionText.text = LM.Get("UI_OPTIONS_SECONDARY_CDS_DESC");
			this.minesToggle.headerText.text = LM.Get("UI_MINES");
			this.specialOffersToggle.headerText.text = LM.Get("UI_SPECIAL_OFFERS");
			this.freeChestsToggle.headerText.text = LM.Get("UI_SHOP_CHEST_0_L");
			this.sideQuestsToggle.headerText.text = LM.Get("UI_SIDE_QUEST_NOT");
			this.flashOffersToggle.headerText.text = LM.Get("SHOP_FLASH_OFFER");
			this.dustRestBonusToggle.headerText.text = LM.Get("WIKI_REST_BONUS_TITLE");
			this.christmasEventToggle.headerText.text = LM.Get("CHRISTMAS_PANEL_TITLE");
			this.eventsToggle.headerText.text = "UI_EVENTS".Loc();
			this.timeSinceLastSave.text = LM.Get("UI_TIME_SINCE_LAST_SAVE");
		}

		public void OrderWidgetPositions(int childCount)
		{
			float num = -10f;
			float num2 = 210f;
			float num3 = num;
			float num4 = (float)(childCount * 96);
			for (int i = 0; i < this.widgets.Count; i++)
			{
				OptionWidget optionWidget = this.widgets[i];
				if (optionWidget.gameObject.activeSelf)
				{
					optionWidget.rectTransform.SetAnchorPosY(num3);
					if (optionWidget == this.notificationsWidget && this.notificationsWidget.toggleButton.isOn)
					{
						num3 -= num4 - 30f;
					}
					num3 -= num2;
				}
			}
			this.csf.SetSize(10f, false);
		}

		public Text headerText;

		public ChildrenContentSizeFitter csf;

		public GameButton closeButton;

		public OptionWidget optionWidgetPrefab;

		public OptionWidget compassWidget;

		public OptionWidget lowPowerWidget;

		public OptionWidget appSleepWidget;

		public OptionWidget notificationsWidget;

		public OptionWidget cooldownUiWidget;

		public RectTransform childTogglesParent;

		public ToggleWidget minesToggle;

		public ToggleWidget specialOffersToggle;

		public ToggleWidget freeChestsToggle;

		public ToggleWidget sideQuestsToggle;

		public ToggleWidget flashOffersToggle;

		public ToggleWidget dustRestBonusToggle;

		public ToggleWidget christmasEventToggle;

		public ToggleWidget eventsToggle;

		public OptionWidget notationWidget;

		public Text timeSinceLastSave;

		public Text timeSinceLastSaveAmount;

		private List<OptionWidget> widgets;

		public RectTransform widgetsParent;
	}
}
