using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ButtonSkillUpgrade : AahMonoBehaviour
	{
		public void SetDetails(Sprite spriteIconSkill, ButtonSkillUpgrade.Kind sKind, string sName, string sInfo, int sLevel, int sEnhancedLevel, int sMaxLevel, int hLevelReq, int hLevelCurrent, bool canSeeSkill, bool everUnlocked)
		{
			this.kind = sKind;
			this.skillName = sName;
			this.skillInfo = sInfo;
			this.skillLevel = sLevel;
			this.skillEnhancedLevel = sEnhancedLevel;
			this.skillMaxLevel = sMaxLevel;
			this.heroLevelRequired = hLevelReq;
			this.heroLevelCurrent = hLevelCurrent;
			if (UiManager.stateJustChanged)
			{
				if (!canSeeSkill && !everUnlocked)
				{
					if (this.kind == ButtonSkillUpgrade.Kind.AutoActive || this.kind == ButtonSkillUpgrade.Kind.Passive)
					{
						this.image.sprite = this.spriteBgLocked;
						this.imageIconSkill.color = new Color(1f, 1f, 1f, 0f);
					}
					else
					{
						this.image.sprite = spriteIconSkill;
					}
					if (this.heroLevelCurrent >= this.heroLevelRequired)
					{
						this.text.text = string.Empty;
						this.imageSkillPointPanel.gameObject.SetActive(false);
					}
					else
					{
						this.text.text = LM.Get("UI_HEROES_LV") + " " + (this.heroLevelRequired + 1).ToString();
						this.imageSkillPointPanel.sprite = this.spriteSkillPointPanel;
						this.text.resizeTextMaxSize = this.textSizeSkillPointPanelSmall;
						this.text.color = this.colorTextLevelLocked;
						this.imageSkillPointPanel.SetNativeSize();
						this.imageSkillPointPanel.gameObject.SetActive(true);
					}
				}
				else
				{
					if (this.kind == ButtonSkillUpgrade.Kind.AutoActive || this.kind == ButtonSkillUpgrade.Kind.Passive)
					{
						this.imageIconSkill.sprite = spriteIconSkill;
						this.imageIconSkill.SetNativeSize();
						if (this.skillLevel > -1)
						{
							this.imageIconSkill.color = this.colorIconUpgraded;
						}
						else
						{
							this.imageIconSkill.color = this.colorIconDisabled;
						}
						if (canSeeSkill)
						{
							if (this.kind == ButtonSkillUpgrade.Kind.Passive)
							{
								if (this.skillLevel > -1)
								{
									this.image.sprite = this.spriteBgPassiveOn;
								}
								else
								{
									this.image.sprite = this.spriteBgPassive;
								}
							}
							else if (this.kind == ButtonSkillUpgrade.Kind.AutoActive)
							{
								if (this.skillLevel > -1)
								{
									this.image.sprite = this.spriteBgAutoActiveOn;
								}
								else
								{
									this.image.sprite = this.spriteBgAutoActive;
								}
							}
						}
						else
						{
							this.image.sprite = this.spriteBgPassiveGlowless;
						}
					}
					else
					{
						this.image.sprite = spriteIconSkill;
					}
					if (canSeeSkill)
					{
						if (this.skillLevel == this.skillMaxLevel + this.skillEnhancedLevel)
						{
							this.imageSkillPointPanel.sprite = this.spriteSkillPointPanelMaxed;
							this.text.text = (this.skillLevel + 1).ToString() + " /" + (this.skillMaxLevel + 1 + this.skillEnhancedLevel).ToString();
							this.text.color = this.colorSkillPoint;
						}
						else
						{
							this.imageSkillPointPanel.sprite = this.spriteSkillPointPanel;
							Color c = (this.skillEnhancedLevel <= 0) ? this.colorSkillPoint : this.colorEnhancedSkillPoint;
							this.text.text = string.Concat(new string[]
							{
								"<color=",
								GameMath.GetColorString(c),
								">",
								(this.skillLevel + 1).ToString(),
								" </color>/",
								(this.skillMaxLevel + 1 + this.skillEnhancedLevel).ToString()
							});
							this.text.color = this.colorTextSkill;
						}
						this.imageSkillPointPanel.SetNativeSize();
						float textPreferredWidth = this.text.GetTextPreferredWidth();
						if (textPreferredWidth + 30f > this.imageSkillPointPanel.rectTransform.sizeDelta.x)
						{
							this.imageSkillPointPanel.rectTransform.SetSizeDeltaX(textPreferredWidth + 30f);
						}
						this.imageSkillPointPanel.gameObject.SetActive(true);
						this.text.resizeTextMaxSize = this.textSizeSkillPointPanelLarge;
					}
					else if (this.heroLevelCurrent >= this.heroLevelRequired)
					{
						this.text.text = string.Empty;
						this.imageSkillPointPanel.gameObject.SetActive(false);
					}
					else
					{
						this.text.text = LM.Get("UI_HEROES_LV") + " " + (this.heroLevelRequired + 1).ToString();
						this.imageSkillPointPanel.sprite = this.spriteSkillPointPanel;
						this.text.resizeTextMaxSize = this.textSizeSkillPointPanelSmall;
						this.text.color = this.colorTextLevelLocked;
						this.imageSkillPointPanel.SetNativeSize();
						this.imageSkillPointPanel.gameObject.SetActive(true);
					}
				}
				this.gameButton.interactable = (canSeeSkill || everUnlocked);
			}
		}

		public void SetDetails(Sprite spriteIconSkill, ButtonSkillUpgrade.Kind sKind, int hLevelReq, string sName, string sInfo, bool everUnlocked)
		{
			this.kind = sKind;
			this.skillName = sName;
			this.skillInfo = sInfo;
			this.skillLevel = 0;
			this.skillEnhancedLevel = 0;
			this.skillMaxLevel = 0;
			this.heroLevelRequired = hLevelReq;
			this.heroLevelCurrent = 0;
			if (UiManager.stateJustChanged)
			{
				if (!everUnlocked)
				{
					if (this.kind == ButtonSkillUpgrade.Kind.AutoActive || this.kind == ButtonSkillUpgrade.Kind.Passive)
					{
						this.image.sprite = this.spriteBgLocked;
						this.imageIconSkill.color = new Color(1f, 1f, 1f, 0f);
					}
					else
					{
						this.image.sprite = spriteIconSkill;
					}
				}
				else if (this.kind == ButtonSkillUpgrade.Kind.AutoActive || this.kind == ButtonSkillUpgrade.Kind.Passive)
				{
					this.imageIconSkill.sprite = spriteIconSkill;
					this.imageIconSkill.SetNativeSize();
					this.imageIconSkill.color = this.colorIconDisabled;
					if (this.kind == ButtonSkillUpgrade.Kind.Passive)
					{
						this.image.sprite = this.spriteBgPassiveGlowless;
					}
					else if (this.kind == ButtonSkillUpgrade.Kind.AutoActive)
					{
						this.image.sprite = this.spriteBgAutoActive;
					}
				}
				else
				{
					this.image.sprite = spriteIconSkill;
				}
				this.text.text = LM.Get("UI_HEROES_LV") + " " + (this.heroLevelRequired + 1).ToString();
				this.imageSkillPointPanel.sprite = this.spriteSkillPointPanel;
				this.text.resizeTextMaxSize = this.textSizeSkillPointPanelSmall;
				this.text.color = ((!everUnlocked) ? this.colorTextLevelLocked : this.colorTextSkill);
				this.imageSkillPointPanel.SetNativeSize();
				this.imageSkillPointPanel.gameObject.SetActive(true);
				this.gameButton.interactable = everUnlocked;
			}
		}

		public void StealInfo(ButtonSkillUpgrade buttonSkillUpgrade)
		{
			this.skillName = buttonSkillUpgrade.skillName;
			this.skillInfo = buttonSkillUpgrade.skillInfo;
			this.skillLevel = buttonSkillUpgrade.skillLevel;
			this.skillEnhancedLevel = buttonSkillUpgrade.skillEnhancedLevel;
			this.heroLevelRequired = buttonSkillUpgrade.heroLevelRequired;
			this.imageIconSkill.sprite = buttonSkillUpgrade.imageIconSkill.sprite;
			this.imageIconSkill.SetNativeSize();
			this.imageSkillPointPanel.sprite = buttonSkillUpgrade.imageSkillPointPanel.sprite;
			this.imageSkillPointPanel.SetNativeSize();
			this.imageSkillPointPanel.gameObject.SetActive(buttonSkillUpgrade.imageSkillPointPanel.gameObject.activeSelf);
			this.text.text = buttonSkillUpgrade.text.text;
			this.text.fontSize = buttonSkillUpgrade.text.fontSize;
			this.text.color = buttonSkillUpgrade.text.color;
			this.kind = buttonSkillUpgrade.kind;
			if (this.kind == ButtonSkillUpgrade.Kind.Passive)
			{
				this.image.sprite = this.spriteBgPassiveOn;
			}
			else if (this.kind == ButtonSkillUpgrade.Kind.AutoActive)
			{
				this.image.sprite = this.spriteBgAutoActiveOn;
			}
		}

		private ButtonSkillUpgrade.Kind kind;

		public GameButton gameButton;

		public Image image;

		public Image imageIconSkill;

		public Image imageSkillPointPanel;

		public Text text;

		public Text textUltimate;

		public Sprite spriteBgLocked;

		public Sprite spriteBgPassive;

		public Sprite spriteBgAutoActive;

		public Sprite spriteBgPassiveOn;

		public Sprite spriteBgAutoActiveOn;

		public Sprite spriteBgPassiveGlowless;

		public Sprite spriteSkillPointPanel;

		public Sprite spriteSkillPointPanelMaxed;

		public int textSizeSkillPointPanelSmall;

		public int textSizeSkillPointPanelLarge;

		public Color colorIconUpgraded;

		public Color colorIconDisabled;

		public Color colorTextLevelLocked;

		public Color colorTextSkill;

		public Color colorSkillPoint;

		public Color colorEnhancedSkillPoint;

		[NonSerialized]
		public int heroLevelRequired;

		[NonSerialized]
		public int heroLevelCurrent;

		[NonSerialized]
		public int skillLevel;

		[NonSerialized]
		public int skillEnhancedLevel;

		[NonSerialized]
		public int skillMaxLevel;

		[NonSerialized]
		public string skillName;

		[NonSerialized]
		public string skillInfo;

		public enum Kind
		{
			Ultimate,
			AutoActive,
			Passive
		}
	}
}
