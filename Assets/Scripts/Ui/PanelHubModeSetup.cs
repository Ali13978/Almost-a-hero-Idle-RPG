using System;
using System.Collections.Generic;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelHubModeSetup : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.InitStrings();
			this.riftEffectImages = new List<RiftEffectImage>();
		}

		public void InitStrings()
		{
			this.textHeader.text = LM.Get("UI_MODE_SETUP_HEADER");
			this.textAllUnlocksCollected.text = LM.Get("UI_MODE_SETUP_ALL_COLLECTED");
			this.buttonStart.text.text = LM.Get("UI_MODE_SETUP_START");
			this.buttonsSelect[0].text.text = LM.Get("UI_RING");
			this.buttonSelectRift.text.text = LM.Get("UI_SELECT_RIFT");
			this.riftEffectsHeader.text = LM.Get("UI_RIFT_EFFECTS");
			int i = 1;
			int num = this.buttonsSelect.Length;
			while (i < num)
			{
				this.buttonsSelect[i].text.text = string.Format(LM.Get("UI_HERO_X"), i.ToString());
				i++;
			}
			this.stageRearrangeWarining.text = LM.Get("UI_STAGE_REARRANGE_WARNING");
		}

		public void SetMode(GameMode mode, bool shouldTotemBeSelected, int numHeroes, Simulator sim)
		{
			this.mode = mode;
			this.shouldTotemBeSelected = shouldTotemBeSelected;
			if (mode == GameMode.STANDARD)
			{
				this.textSelectTeam.text = LM.Get("UI_SELECT_RING_HEADER");
				this.imageModeBg.sprite = this.spritesModeStandard;
				this.imageModeChallengeIcon.sprite = this.spritesChallengeIconStandard;
				bool flag = sim.ShouldShowStagesRearrengeWarning();
				this.stageRearrangeWarining.enabled = flag;
				this.panelBackgroundImage.SetBottomDelta((!flag) ? this.panelBackgroundImageBottomOffsetWithoutWarning : this.panelBackgroundImageBottomOffsetWithWarning);
				this.startButtonsParent.SetAnchorPosY((!flag) ? this.buttonPosWithoutWarning : this.buttonPosWithWarning);
			}
			else if (mode == GameMode.CRUSADE)
			{
				this.textSelectTeam.text = LM.Get("UI_MODE_SETUP_SELECT");
				this.imageModeBg.sprite = this.spritesModeCrusade;
				this.imageModeChallengeIcon.sprite = this.spritesChallengeIconCrusade;
				this.stageRearrangeWarining.enabled = false;
				this.panelBackgroundImage.SetBottomDelta(this.panelBackgroundImageBottomOffsetWithoutWarning);
				this.startButtonsParent.SetAnchorPosY(this.buttonPosWithoutWarning);
			}
			else
			{
				if (mode != GameMode.RIFT)
				{
					throw new NotImplementedException();
				}
				this.textSelectTeam.text = LM.Get("UI_MODE_SETUP_SELECT");
				this.imageModeBg.sprite = this.spritesModeCrusade;
				this.imageModeChallengeIcon.sprite = this.spritesChallengeIconCrusade;
				this.stageRearrangeWarining.enabled = false;
				this.panelBackgroundImage.SetBottomDelta(this.panelBackgroundImageBottomOffsetWithoutWarning);
				this.startButtonsParent.SetAnchorPosY(this.buttonPosWithoutWarning);
			}
			this.imageModeChallengeIcon.SetNativeSize();
			this.timePanel.SetActive(mode == GameMode.CRUSADE || mode == GameMode.RIFT);
			if (mode == GameMode.CRUSADE)
			{
				string text = string.Format(LM.Get("UI_TIME_CHALLENGE_STAGE"), sim.GetNumTimeChallengesComplete() + 1);
				this.textTitle.text = text;
			}
			else
			{
				this.textTitle.text = UiManager.GetModeName(mode);
			}
			this.numHeroesShouldBeSelected = numHeroes;
			this.SetButtons(shouldTotemBeSelected, this.numHeroesShouldBeSelected);
			this.totemDatabase = null;
			this.heroDatabases = new HeroDataBase[Mathf.Max(0, this.numHeroesShouldBeSelected)];
			int i = 0;
			int num = this.numHeroesShouldBeSelected;
			while (i < num)
			{
				this.heroDatabases[i] = null;
				i++;
			}
		}

		private void SetButtons(bool isTotem, int numHeroes)
		{
			int num = (!isTotem) ? 0 : 1;
			int num2 = num + numHeroes;
			int i = 0;
			int num3 = this.buttonsSelect.Length;
			while (i < num3)
			{
				if (i == 0)
				{
					this.buttonsSelect[i].gameObject.SetActive(isTotem);
				}
				else if (i < numHeroes + 1)
				{
					this.buttonsSelect[i].gameObject.SetActive(true);
				}
				else
				{
					this.buttonsSelect[i].gameObject.SetActive(false);
				}
				this.buttonsSelected[i].gameObject.SetActive(false);
				i++;
			}
			float num4 = (num2 != 5) ? (this.buttonsDeltaX + 20f) : this.buttonsDeltaX;
			float num5 = -num4 * (float)(num2 - 1) / 2f;
			int num6 = 0;
			int j = 0;
			int num7 = this.buttonsSelect.Length;
			while (j < num7)
			{
				if (this.buttonsSelect[j].gameObject.activeSelf)
				{
					RectTransform rectTransform = this.buttonsSelect[j].raycastTarget.rectTransform;
					rectTransform.anchoredPosition = new Vector2(num5 + (float)num6 * num4, rectTransform.anchoredPosition.y);
					this.buttonsSelected[j].gameObject.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;
					num6++;
				}
				j++;
			}
		}

		public void SetSelectedButtons(Sprite totemSprite, Sprite[] heroSprites)
		{
			if (this.numHeroesShouldBeSelected == -1 || !this.shouldTotemBeSelected)
			{
				this.buttonsSelect[0].gameObject.SetActive(false);
				this.buttonsSelected[0].gameObject.SetActive(false);
			}
			else if (this.totemDatabase == null)
			{
				this.buttonsSelect[0].gameObject.SetActive(true);
				this.buttonsSelected[0].gameObject.SetActive(false);
			}
			else
			{
				this.buttonsSelect[0].gameObject.SetActive(false);
				this.buttonsSelected[0].gameObject.SetActive(true);
				this.buttonSelectTotem.spriteIcon = totemSprite;
			}
			int i = 0;
			int num = this.numHeroesShouldBeSelected;
			while (i < num)
			{
				if (this.heroDatabases[i] == null)
				{
					this.buttonsSelect[i + 1].gameObject.SetActive(true);
					this.buttonsSelected[i + 1].gameObject.SetActive(false);
				}
				else
				{
					this.buttonsSelect[i + 1].gameObject.SetActive(false);
					this.buttonsSelected[i + 1].gameObject.SetActive(true);
					this.heroPortraits[i].SetHero(heroSprites[i], this.heroDatabases[i].evolveLevel, false, -5f, false);
				}
				i++;
			}
		}

		public bool CanSetupBeCompleted()
		{
			int i = 0;
			int num = this.heroDatabases.Length;
			while (i < num)
			{
				if (this.heroDatabases[i] == null)
				{
					return false;
				}
				i++;
			}
			return !this.shouldTotemBeSelected || this.totemDatabase != null;
		}

		public void OnClickedSelectTotem(TotemDataBase totemDatabase)
		{
			this.totemDatabase = totemDatabase;
		}

		public void OnClickedSelectNewHero(HeroDataBase heroDataBase)
		{
			this.heroDatabases[this.whichHeroButtonSelected] = heroDataBase;
		}

		public void SetIconSprite(CurrencyType currencyType)
		{
			this.imageAllUnlocksCollected.gameObject.SetActive(false);
			this.imageReward.gameObject.SetActive(false);
			this.rewarCart.gameObject.SetActive(true);
			this.heroAnimation.gameObject.SetActive(false);
			this.rewarCart.SetCurrency(currencyType);
			if (this.mode == GameMode.RIFT)
			{
				this.rewarCart.transform.localScale = Vector3.one * 0.5f;
			}
			else
			{
				this.rewarCart.transform.localScale = Vector3.one * 0.8f;
			}
		}

		public void SetIconSprite(GameMode mode)
		{
			this.imageAllUnlocksCollected.gameObject.SetActive(false);
			this.imageReward.gameObject.SetActive(true);
			this.heroAnimation.gameObject.SetActive(false);
			if (mode == GameMode.CRUSADE)
			{
				this.imageReward.sprite = UiData.inst.spriteUnlockModeTimeChallenge;
			}
			else
			{
				if (mode != GameMode.RIFT)
				{
					throw new NotImplementedException();
				}
				this.imageReward.sprite = UiData.inst.spriteUnlockModeRift;
			}
			this.imageReward.transform.localScale = new Vector3(1f, 1f, 1f);
			this.imageReward.SetNativeSize();
		}

		public void SetIconSpriteMerchant()
		{
			this.imageAllUnlocksCollected.gameObject.SetActive(false);
			this.imageReward.gameObject.SetActive(true);
			this.heroAnimation.gameObject.SetActive(false);
			this.imageReward.sprite = UiData.inst.spriteUnlockMerchant;
			this.imageReward.transform.localScale = new Vector3(1f, 1f, 1f);
			this.imageReward.SetNativeSize();
		}

		public void SetIconSpriteCompass()
		{
			this.imageAllUnlocksCollected.gameObject.SetActive(false);
			this.imageReward.gameObject.SetActive(true);
			this.heroAnimation.gameObject.SetActive(false);
			this.imageReward.sprite = UiData.inst.spriteUnlockCompass;
			this.imageReward.transform.localScale = new Vector3(1f, 1f, 1f);
			this.imageReward.SetNativeSize();
		}

		public void SetHeroAnimation(string heroId)
		{
			this.imageAllUnlocksCollected.gameObject.SetActive(false);
			this.imageReward.gameObject.SetActive(false);
			this.heroAnimation.gameObject.SetActive(true);
			this.heroAnimation.OnClose();
			this.heroAnimation.SetHeroAnimation(heroId, 1, false, false, true, true);
			this.imageReward.transform.localScale = new Vector3(1f, 1f, 1f);
			this.imageReward.SetNativeSize();
		}

		public void SetIconSprite(Sprite s, float scale = 1f, Color? color = null)
		{
			this.imageAllUnlocksCollected.gameObject.SetActive(false);
			this.imageReward.gameObject.SetActive(true);
			this.heroAnimation.gameObject.SetActive(false);
			this.imageReward.sprite = s;
			if (color != null)
			{
				this.imageReward.color = color.Value;
			}
			else
			{
				this.imageReward.color = Color.white;
			}
			this.imageReward.transform.localScale = new Vector3(1f, 1f, 1f) * scale;
			this.imageReward.SetNativeSize();
		}

		public void SetAllUnlocksCollected()
		{
			this.imageAllUnlocksCollected.gameObject.SetActive(true);
			this.imageReward.gameObject.SetActive(false);
			this.rewarCart.gameObject.SetActive(false);
			this.heroAnimation.gameObject.SetActive(false);
			this.textReward.text = string.Empty;
			this.imageReward.transform.localScale = new Vector3(1f, 1f, 1f);
			this.imageReward.SetNativeSize();
		}

		public void SetForRift()
		{
			this.riftInfosParent.gameObject.SetActive(true);
			this.modeImageParent.gameObject.SetActive(false);
			this.buttonSelectRift.interactable = (TutorialManager.repeatRifts > TutorialManager.RepeatRifts.BEFORE_BEGIN);
			this.panelBg.SetSizeDeltaY(this.panelHightForRift);
			this.textNextReward.rectTransform.SetAnchorPosY(22f);
			this.textReward.rectTransform.SetSizeDeltaY(65f);
			this.panelBg.SetAnchorPosY(25f);
			this.rewardBg.SetSizeDeltaY(140f);
			this.imageReward.rectTransform.SetAnchorPosX(-110f);
			this.imageRewardBackground.rectTransform.SetAnchorPosX(-110f);
		}

		public void SetForNormal()
		{
			this.riftInfosParent.gameObject.SetActive(false);
			this.modeImageParent.gameObject.SetActive(true);
			this.textReward.gameObject.SetActive(true);
			this.panelBg.SetSizeDeltaY(this.panelHightForNormal);
			this.textNextReward.rectTransform.SetAnchorPosY(67f);
			this.textReward.rectTransform.SetSizeDeltaY(130f);
			this.panelBg.SetAnchorPosY(-130f);
			this.rewardBg.SetSizeDeltaY(242f);
			this.imageReward.rectTransform.SetAnchorPosX(-150f);
			this.imageRewardBackground.rectTransform.SetAnchorPosX(-150f);
		}

		public void SetRiftEffects(ChallengeRift riftChallenge, UiManager uiManager)
		{
			int count = riftChallenge.riftEffects.Count;
			Utility.FillUiElementList<RiftEffectImage>(this.imagePrafab, this.riftEffectsParent, count, this.riftEffectImages);
			for (int i = 0; i < count; i++)
			{
				this.riftEffectImages[i].icon.sprite = uiManager.GetRiftEffectSprite(riftChallenge.riftEffects[i].GetType());
			}
		}

		public bool IsRiftMode()
		{
			return this.riftInfosParent.gameObject.activeSelf;
		}

		public void SetButtonTheme(bool isCursed)
		{
			if (isCursed)
			{
				this.buttonStart.colorTextNotInteractable = this.startButtonCursedDisabledTextColor;
				this.buttonStart.spriteNotInteractable = this.startButtonCursedDisabled;
			}
			else
			{
				this.buttonStart.colorTextNotInteractable = this.startButtonNormalDisabledTextColor;
				this.buttonStart.spriteNotInteractable = this.startButtonNormalDisabled;
			}
		}

		[Header("Common")]
		public GameButton buttonStart;

		public Text textTitle;

		public Image imageModeBg;

		public Image imageModeChallengeIcon;

		public Text textChallenge;

		public GameObject challengePanel;

		public GameObject teamSelectPanel;

		public GameObject timePanel;

		public Text textTime;

		public CartWidget rewarCart;

		public Image imageReward;

		public Image imageRewardBackground;

		public HeroAnimation heroAnimation;

		public Text textNextReward;

		public Text textReward;

		public Text textSelectTeam;

		public GameButton[] buttonsSelect;

		public GameButton[] buttonsSelected;

		public ButtonSelectTotem buttonSelectTotem;

		public HeroPortrait[] heroPortraits;

		public GameButton buttonBack;

		public Text textHeader;

		public Image imageAllUnlocksCollected;

		public Image headerBackgroundImage;

		public Image backgroundPatternImage;

		public Image backgroundBaseImage;

		public Text textAllUnlocksCollected;

		public Sprite spritesModeStandard;

		public Sprite spritesModeCrusade;

		public Sprite spritesChallengeIconStandard;

		public Sprite spritesChallengeIconCrusade;

		public float buttonsDeltaX;

		public GameMode mode;

		public TotemDataBase totemDatabase;

		public HeroDataBase[] heroDatabases;

		public bool shouldTotemBeSelected;

		public int numHeroesShouldBeSelected;

		public int whichHeroButtonSelected;

		public RectTransform panelBg;

		public RectTransform modeImageParent;

		public RectTransform rewardBg;

		[Header("Rift")]
		public RectTransform riftInfosParent;

		public RectTransform riftEffectsParent;

		public GameButton buttonSelectRift;

		public GameButton buttonRiftInfo;

		public Text selectedRiftNo;

		public Text riftEffectsHeader;

		public float panelHightForRift = 1000f;

		public float panelHightForNormal = 800f;

		public bool challengeIndexChanged;

		public RiftEffectImage imagePrafab;

		public GameButton randomTeam;

		public NotificationBadge riftDiscoveryNotificationBadge;

		public Button riftEffectsParentButton;

		public Sprite startButtonCursedDisabled;

		public Sprite startButtonNormalDisabled;

		public Color startButtonCursedDisabledTextColor;

		public Color startButtonNormalDisabledTextColor;

		public Color headerColorNormal;

		public Color headerColorCursed;

		public Color backgroundColorNormal;

		public Color backgroundColorCursed;

		public Color backgroundBaseColorNormal;

		public Color backgroundBaseColorCursed;

		[Header("Stage Rearange Warning")]
		public Text stageRearrangeWarining;

		public RectTransform panelBackgroundImage;

		public RectTransform startButtonsParent;

		public float panelBackgroundImageBottomOffsetWithWarning;

		public float panelBackgroundImageBottomOffsetWithoutWarning;

		public float buttonPosWithWarning;

		public float buttonPosWithoutWarning;

		private List<RiftEffectImage> riftEffectImages;
	}
}
