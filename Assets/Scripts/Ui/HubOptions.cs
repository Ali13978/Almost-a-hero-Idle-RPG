using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class HubOptions : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.InitStrings();
			this.buttonAdvancedOptions.onClick = delegate()
			{
				this.panelAdvancedOptions.gameObject.SetActive(true);
				this.panelAdvancedOptions.OrderWidgetPositions(this.GetNumNotificationsAvailable());
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiOpenMenu, 1f));
			};
			this.panelAdvancedOptions.closeButton.onClick = delegate()
			{
				this.panelAdvancedOptions.gameObject.SetActive(false);
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
			};
			this.cloudSaveIcon.sprite = this.googlePlayIcon;
		}

		private void Button_CoppyPlayfabId()
		{
			TextEditor textEditor = new TextEditor();
			textEditor.text = PlayfabManager.playerId;
			textEditor.SelectAll();
			textEditor.Copy();
		}

		public void InitStrings()
		{
			this.textHeader.text = LM.Get("UI_BACK");
			this.textMusicOnOff.text = LM.Get("UI_OPTIONS_MUSIC");
			this.textSoundOnOff.text = LM.Get("UI_SETTINGS_SOUND_EFFECTS");
			this.textVoiceOnOff.text = LM.Get("UI_SETTINGS_VOICES");
			this.textLanguage.text = LM.Get("UI_OPTIONS_LANGUAGE");
			Utility.OptimizeFontSizes(new Text[]
			{
				this.textLanguage,
				this.textSoundOnOff,
				this.textMusicOnOff
			});
			this.buttonContact.text.text = LM.Get("UI_OPTIONS_SUPPORT").UppercaseFirst();
			this.buttonLanguage.text.text = LM.Get("LANGUAGE_NAME");
			this.buttonWiki.text.text = LM.Get("OPTIONS_WIKI");
			this.buttonCommunity.text.text = LM.Get("OPTIONS_COMMUNITY");
			this.buttonDeleteSave.text.text = LM.Get("OPTIONS_HARD_RESET");
			this.buttonAdvancedOptions.text.text = LM.Get("OPTIONS_ADVANCED");
			this.buttonGameInfo.text.text = LM.Get("UI_BUTTON_GAME_INFO");
			this.buttonUpdateInfo.text.text = LM.Get("UI_PATCH_NOTES");
			Utility.OptimizeFontSizes(new Text[]
			{
				this.buttonContact.text,
				this.buttonLanguage.text,
				this.buttonWiki.text,
				this.buttonCommunity.text,
				this.buttonDeleteSave.text
			});
			this.storeName.text = UiManager.GetStoreNameString();
			ButtonOnOff.onString = LM.Get("UI_OPTIONS_ON");
			ButtonOnOff.offString = LM.Get("UI_OPTIONS_OFF");
		}

		public override void AahUpdate(float dt)
		{
			if (this.secret > 0)
			{
				this.secretTimer += dt;
				if (this.secretTimer > 0.4f)
				{
					this.secretTimer = 0f;
					this.secret--;
				}
			}
		}

		public int GetNumNotificationsAvailable()
		{
			return 3 + ((!this.uiManager.sim.hasDailies) ? 0 : 1) + ((!this.uiManager.sim.MineAnyUnlocked()) ? 0 : 1) + ((!TutorialManager.AreFlashOffersUnlocked()) ? 0 : 1) + ((this.uiManager.sim.riftQuestDustCollected <= 0.0) ? 0 : 1) + (this.uiManager.sim.christmasEventAlreadyDisabled ? 0 : 1);
		}

		public Text textMusicOnOff;

		public Text textSoundOnOff;

		public Text textVoiceOnOff;

		public Text textLanguage;

		public Text textStoreAuth;

		public Text storeName;

		public ButtonOnOff buttonMusicOnOff;

		public ButtonOnOff buttonSoundOnOff;

		public ButtonOnOff buttonVoiceOnOff;

		public GameButton buttonAdvancedOptions;

		public GameButton buttonGameInfo;

		public GameButton buttonUpdateInfo;

		public GameButton buttonLanguage;

		public GameButton buttonCredits;

		public GameButton buttonBack;

		public GameButton buttonAchievements;

		public GameButton buttonLeaderboards;

		public GameButton buttonContact;

		public GameButton buttonWiki;

		public GameButton buttonCommunity;

		public GameButton buttonCloudSave;

		public GameButton buttonDeleteSave;

		public GameButton buttonSecret;

		public Text textHeader;

		public Text textVersion;

		public Text textPlayfabId;

		public Button playfabidCoppier;

		public Image cloudSaveIcon;

		public Image achievementsIcon;

		public Image leaderboardsIcon;

		public Sprite googlePlayIcon;

		public Sprite miPlayIcon;

		public Sprite gameCenterIcon;

		public static Color storeIsAuthedColor = Utility.HexColor("E2F755");

		public static Color storeIsNotAuthedColor = Utility.HexColor("625242");

		public static Color storeDisabledColor = Utility.HexColor("27313A");

		public Color storeAuthenticatedColor;

		public Color storeNotAuthenticatedColor;

		[HideInInspector]
		public int secret;

		[HideInInspector]
		public float secretTimer;

		public Sprite disabledButtonSprite;

		public Sprite enabledButtonSprite;

		public PanelAdvancedOptions panelAdvancedOptions;

		[NonSerialized]
		public UiManager uiManager;
	}
}
