using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class HubOptionsWiki : AahMonoBehaviour
	{
		public void InitStrings(Color[] heroLevelColors)
		{
			this.title.text = LM.Get("UI_BUTTON_GAME_INFO");
			this.tabGogInfo.text = LM.Get("UI_RIFT");
			this.tabGameInfo.text = LM.Get("UI_WIKI_GAME_TAB");
			this.unlockMechanics.InitStrings("WIKI_UNLOCKING_TITLE", "WIKI_UNLOCKING_DESC");
			this.artifacts.InitStrings("WIKI_ARTIFACTS_TITLE", "WIKI_ARTIFACTS_DESC");
			this.qualityPoints.InitStrings("WIKI_QUALITY_POINTS_TITLE", "WIKI_QUALITY_POINTS_DESC");
			this.prestige.InitStrings("WIKI_PRESTIGE_TITLE", "WIKI_PRESTIGE_DESC");
			this.trinkets.InitStrings("WIKI_TRINKETS_TITLE", "WIKI_TRINKETS_DESC");
			this.timeChallenges.InitStrings("WIKI_TIME_CHALLENGES_TITLE", "WIKI_TIME_CHALLENGES_DESC");
			this.chests.InitStrings("WIKI_CHESTS_TITLE", "WIKI_CHESTS_DESC");
			this.dragon.InitStrings("WIKI_DRAGON_TITLE", "WIKI_DRAGON_DESC");
			this.qualityPointsText.text = LM.Get("WIKI_QUALITY_POINTS_TITLE");
			this.trinketSmithing.InitStrings("WIKI_TRINKET_SMITHING_TITLE", "WIKI_TRINKET_SMITHING_DESC");
			this.lore.InitStrings("WIKI_LORE_TITLE", "WIKI_LORE_DESC");
			this.effects.InitStrings("WIKI_EFFECTS_TITLE", "WIKI_EFFECTS_DESC");
			this.selectingGates.InitStrings("WIKI_SELECTING_GATE_TITLE", "WIKI_SELECTING_GATE_DESC");
			this.aeonDust.InitStrings("WIKI_DUST_TITLE", "WIKI_DUST_DESC");
			this.process.InitStrings("WIKI_PROCESS_TITLE", "WIKI_PROCESS_DESC");
			this.restBonus.InitStrings("WIKI_REST_BONUS_TITLE", "WIKI_REST_BONUS_DESC");
			this.charms.InitStrings("WIKI_CHARMS_TITLE", "WIKI_CHARMS_DESC");
			this.choose.InitStrings("WIKI_CHOOSE_TITLE", "WIKI_CHOOSE_DESC");
			this.level.InitStrings("CHARM_SORT_LVL", "WIKI_CHARM_LEVEL_DESC");
			this.curses.InitStrings("WIKI_CURSES_TITLE", "WIKI_CURSES_DESC");
			this.items.Title.text = LM.Get("WIKI_ITEMS_TITLE");
			this.items.Description.text = string.Format(LM.Get("WIKI_ITEMS_DESC"), new object[]
			{
				AM.cs(LM.Get("UI_LEVEL_COMMON"), heroLevelColors[0]),
				AM.cs(LM.Get("UI_LEVEL_UNCOMMON"), heroLevelColors[1]),
				AM.cs(LM.Get("UI_LEVEL_RARE"), heroLevelColors[2]),
				AM.cs(LM.Get("UI_LEVEL_EPIC"), heroLevelColors[3]),
				AM.cs(LM.Get("UI_LEVEL_LEGENDARY"), heroLevelColors[4]),
				AM.cs(LM.Get("UI_LEVEL_MYTHICAL"), heroLevelColors[5])
			});
		}

		public void UpdateScreen(bool riftUnlock, bool trinketsUnlock, bool trinketsSmithingUnlocked)
		{
			this.tabsParent.SetActive(riftUnlock);
			this.trinkets.Parent.gameObject.SetActive(trinketsUnlock);
			this.trinketSmithing.Parent.gameObject.SetActive(trinketsSmithingUnlocked);
			Vector2 offsetMax = this.contentParent.offsetMax;
			offsetMax.y = -((!riftUnlock) ? this.contentParentNoTabsTopAnchor : this.contentParentDefaultTopAnchor);
			this.contentParent.offsetMax = offsetMax;
			this.unlockMechanics.UpdateParentSize();
			this.artifacts.UpdateParentSize();
			this.qualityPoints.UpdateParentSize();
			this.prestige.UpdateParentSize();
			this.trinkets.UpdateParentSize();
			this.timeChallenges.UpdateParentSize();
			this.items.UpdateParentSize();
			this.chests.UpdateParentSize();
			this.dragon.UpdateParentSize();
			this.trinketSmithing.UpdateParentSize();
			LayoutRebuilder.ForceRebuildLayoutImmediate(this.gameInfoLayout.transform as RectTransform);
			this.lore.UpdateParentSize();
			this.effects.UpdateParentSize();
			this.selectingGates.UpdateParentSize();
			this.aeonDust.UpdateParentSize();
			this.process.UpdateParentSize();
			this.restBonus.UpdateParentSize();
			this.charms.UpdateParentSize();
			this.choose.UpdateParentSize();
			this.level.UpdateParentSize();
			this.curses.UpdateParentSize();
			LayoutRebuilder.ForceRebuildLayoutImmediate(this.gogInfoLayout.transform as RectTransform);
		}

		public GameButton tabGameInfoButton;

		public GameButton tabGogInfoButton;

		public GameButton closeButton;

		public GameObject gameInfoParent;

		public GameObject gogInfoParent;

		public VerticalLayoutGroup gameInfoLayout;

		public VerticalLayoutGroup gogInfoLayout;

		public RectTransform popupRect;

		[SerializeField]
		private GameObject tabsParent;

		[SerializeField]
		private RectTransform contentParent;

		[SerializeField]
		private float contentParentDefaultTopAnchor;

		[SerializeField]
		private float contentParentNoTabsTopAnchor;

		[SerializeField]
		private Text title;

		[SerializeField]
		private Text tabGameInfo;

		[SerializeField]
		private Text tabGogInfo;

		[Header("Game Info")]
		[SerializeField]
		private HubOptionsWiki.WikiInfo unlockMechanics;

		[SerializeField]
		private HubOptionsWiki.WikiInfo artifacts;

		[SerializeField]
		private HubOptionsWiki.WikiInfo qualityPoints;

		[SerializeField]
		private HubOptionsWiki.WikiInfo prestige;

		[SerializeField]
		private HubOptionsWiki.WikiInfo trinkets;

		[SerializeField]
		private HubOptionsWiki.WikiInfo timeChallenges;

		[SerializeField]
		private HubOptionsWiki.WikiInfo items;

		[SerializeField]
		private HubOptionsWiki.WikiInfo chests;

		[SerializeField]
		private HubOptionsWiki.WikiInfo dragon;

		[SerializeField]
		private Text qualityPointsText;

		[SerializeField]
		private HubOptionsWiki.WikiInfo trinketSmithing;

		[Header("GoG Info")]
		[SerializeField]
		private HubOptionsWiki.WikiInfo lore;

		[SerializeField]
		private HubOptionsWiki.WikiInfo effects;

		[SerializeField]
		private HubOptionsWiki.WikiInfo selectingGates;

		[SerializeField]
		private HubOptionsWiki.WikiInfo aeonDust;

		[SerializeField]
		private HubOptionsWiki.WikiInfo process;

		[SerializeField]
		private HubOptionsWiki.WikiInfo restBonus;

		[SerializeField]
		private HubOptionsWiki.WikiInfo charms;

		[SerializeField]
		private HubOptionsWiki.WikiInfo choose;

		[SerializeField]
		private HubOptionsWiki.WikiInfo level;

		[SerializeField]
		private HubOptionsWiki.WikiInfo curses;

		[Serializable]
		public class WikiInfo
		{
			public void InitStrings(string titleKey, string descriptionKey)
			{
				this.Title.text = LM.Get(titleKey);
				this.Description.text = LM.Get(descriptionKey);
			}

			public void UpdateParentSize()
			{
				Vector2 sizeDelta = new Vector2(this.Parent.sizeDelta.x, GameMath.GetMaxFloat(this.MinHeight, 122f + this.Description.rectTransform.sizeDelta.y));
				this.Parent.sizeDelta = sizeDelta;
			}

			public Text Title;

			public Text Description;

			public RectTransform Parent;

			public float MinHeight;
		}
	}
}
