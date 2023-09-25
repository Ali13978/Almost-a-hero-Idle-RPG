using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ButtonUpgradeAnim : AahMonoBehaviour
	{
		public ContentSizeFitter textDownContentSizeFitter
		{
			get
			{
				ContentSizeFitter result;
				if ((result = this.m_textDownContentSizeFitter) == null)
				{
					result = (this.m_textDownContentSizeFitter = this.textDown.GetComponent<ContentSizeFitter>());
				}
				return result;
			}
		}

		public ContentSizeFitter textUpContentSizeFitter
		{
			get
			{
				ContentSizeFitter result;
				if ((result = this.m_textUpContentSizeFitter) == null)
				{
					result = (this.m_textUpContentSizeFitter = this.textUp.GetComponent<ContentSizeFitter>());
				}
				return result;
			}
		}

		public void ResetTextDownFontSize()
		{
			this.textDown.fontSize = this.textDownInitialFontSize;
		}

		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			Image image = (!(this.gameButton.graphicTarget == null)) ? this.gameButton.graphicTarget : this.gameButton.raycastTarget;
			this.spriteNormal = image.sprite;
			this.ResetIcons();
			this.textUpPosY = this.textUp.rectTransform.anchoredPosition.y;
			this.textDownPosY = this.textDown.rectTransform.anchoredPosition.y;
			this.rectTransform = base.GetComponent<RectTransform>();
			if (this.textUp != null)
			{
				this.textUpDefaultPos = this.textUp.rectTransform.anchoredPosition;
			}
			if (this.textDown != null)
			{
				this.textDownDefaultPos = this.textDown.rectTransform.anchoredPosition;
				this.textDownInitialFontSize = this.textDown.fontSize;
			}
		}

		public override void AahUpdate(float dt)
		{
			Image image = (!(this.gameButton.graphicTarget == null)) ? this.gameButton.graphicTarget : this.gameButton.raycastTarget;
			if (this.gameButton.interactable)
			{
				if (!this.dontChangeSprites)
				{
					if (this.fakeDisabled)
					{
						image.sprite = this.spriteNotInteractable;
					}
					else if (this.spriteLevelUp != null && this.spriteUpgrade != null)
					{
						if (this.isLevelUp)
						{
							image.sprite = this.spriteLevelUp;
						}
						else
						{
							image.sprite = this.spriteUpgrade;
						}
					}
					else
					{
						image.sprite = this.spriteNormal;
					}
				}
				if (!this.doNotTouchToTextColor)
				{
					if (this.fakeDisabled)
					{
						this.textUp.color = this.textUpDisabledColor;
						this.textDown.color = this.textDownDisabledColor;
					}
					else if (!this.changeColorAtLevelUp || !this.isLevelUp)
					{
						this.textUp.color = this.textUpEnabledColor;
						this.textDown.color = this.textDownEnabledColor;
					}
					else
					{
						this.textUp.color = this.textUpLevelUpColor;
						this.textDown.color = this.textDownLevelUpColor;
					}
				}
				if (this.fakeDisabled)
				{
					this.textUp.rectTransform.anchoredPosition = new Vector2(this.textUp.rectTransform.anchoredPosition.x, this.textUpPosY + this.textUpDisabledOffset + ((!this.isLevelUp) ? 0f : this.textUpLevelUpOffset));
					this.textDown.rectTransform.anchoredPosition = new Vector2(this.textDown.rectTransform.anchoredPosition.x, this.textDownPosY + this.textDownDisabledOffset + ((!this.isLevelUp) ? 0f : this.textDownLevelUpOffset));
				}
				else if (this.textUpDisabledOffset != 0f || this.textDownDisabledOffset != 0f || this.textUpLevelUpOffset != 0f || this.textDownLevelUpOffset != 0f)
				{
					this.textUp.rectTransform.anchoredPosition = new Vector2(this.textUp.rectTransform.anchoredPosition.x, this.textUpPosY + ((!this.isLevelUp) ? 0f : this.textUpLevelUpOffset));
					this.textDown.rectTransform.anchoredPosition = new Vector2(this.textDown.rectTransform.anchoredPosition.x, this.textDownPosY + ((!this.isLevelUp) ? 0f : this.textDownLevelUpOffset));
				}
				if (this.textOutlineUp != null)
				{
					this.textOutlineUp.enabled = this.textOutlineEnableOnEnabled;
				}
				if (this.textOutlineDown != null)
				{
					this.textOutlineDown.enabled = this.textOutlineEnableOnEnabled;
				}
				if (this.textShadowUp != null)
				{
					this.textShadowUp.enabled = this.textShadowEnableOnEnabled;
				}
				if (this.textShadowDown != null)
				{
					if (this.isLevelUp)
					{
						this.textShadowDown.enabled = this.textShadowEnableOnLevelUp;
					}
					else
					{
						this.textShadowDown.enabled = this.textShadowEnableOnEnabled;
					}
				}
			}
			else
			{
				if (!this.dontChangeSprites)
				{
					image.sprite = this.spriteNotInteractable;
				}
				if (this.textUpDisabledOffset != 0f || this.textDownDisabledOffset != 0f || this.textUpLevelUpOffset != 0f || this.textDownLevelUpOffset != 0f)
				{
					this.textUp.rectTransform.anchoredPosition = new Vector2(this.textUp.rectTransform.anchoredPosition.x, this.textUpPosY + this.textUpDisabledOffset + ((!this.isLevelUp) ? 0f : this.textUpLevelUpOffset));
					this.textDown.rectTransform.anchoredPosition = new Vector2(this.textDown.rectTransform.anchoredPosition.x, this.textDownPosY + this.textDownDisabledOffset + ((!this.isLevelUp) ? 0f : this.textDownLevelUpOffset));
				}
				if (this.textOutlineUp != null)
				{
					this.textOutlineUp.enabled = this.textOutlineEnableOnDisabled;
				}
				if (this.textOutlineDown != null)
				{
					this.textOutlineDown.enabled = this.textOutlineEnableOnDisabled;
				}
				if (this.textShadowUp != null)
				{
					this.textShadowUp.enabled = this.textShadowEnableOnDisabled;
				}
				if (this.textShadowDown != null)
				{
					this.textShadowDown.enabled = this.textShadowEnableOnDisabled;
				}
				if (this.textCantAffordColorChange)
				{
					this.SetCurrencyTextsToCantAffordColor();
				}
				else
				{
					this.textUp.color = this.textUpDisabledColor;
					this.textDown.color = this.textDownDisabledColor;
				}
			}
			if (this.textCantAffordColorChangeManual)
			{
				this.SetCurrencyTextsToCantAffordColor();
			}
			if (this.textUpCantAffordColorChangeForced)
			{
				this.textUp.color = UiManager.colorCantAfford;
			}
			if (this.textDownCantAffordColorChangeForced)
			{
				this.textDown.color = UiManager.colorCantAfford;
			}
			if (this.deltaTextPosYOnDown != 0f)
			{
				if (!this.gameButton.isDown)
				{
					this.textUp.rectTransform.anchoredPosition = this.textUpDefaultPos;
					this.textDown.rectTransform.anchoredPosition = this.textDownDefaultPos;
				}
				else
				{
					this.textUp.rectTransform.anchoredPosition = this.textUpDefaultPos + new Vector2(0f, this.deltaTextPosYOnDown);
					this.textDown.rectTransform.anchoredPosition = this.textDownDefaultPos + new Vector2(0f, this.deltaTextPosYOnDown);
				}
			}
			this.ResetIcons();
			if (this.iconUp != null && this.iconUp.gameObject.activeSelf)
			{
				Sprite spriteFromIconType = this.GetSpriteFromIconType(this.iconUpType, this.gameButton.interactable);
				if (this.iconUp.sprite != spriteFromIconType)
				{
					this.iconUp.sprite = spriteFromIconType;
				}
			}
			if (this.iconDown != null && this.iconDown.gameObject.activeSelf)
			{
				Sprite spriteFromIconType2 = this.GetSpriteFromIconType(this.iconDownType, this.gameButton.interactable);
				if (this.iconDown.sprite != spriteFromIconType2)
				{
					this.iconDown.sprite = spriteFromIconType2;
				}
			}
			if (this.textDown != null)
			{
				if (this.changeTextDownToFit)
				{
					if (this.textDown.rectTransform.sizeDelta.x >= (this.gameButton.graphicTarget ?? this.gameButton.raycastTarget).rectTransform.sizeDelta.x)
					{
						this.textDown.fontSize = (int)((float)this.textDown.fontSize * 0.8f);
					}
				}
				else
				{
					this.textDown.fontSize = this.textDownInitialFontSize;
				}
			}
		}

		private void SetCurrencyTextsToCantAffordColor()
		{
			if (!this.textUpNoCantAffordColorChangeForced && this.iconUpType != ButtonUpgradeAnim.IconType.NONE && this.iconUpType != ButtonUpgradeAnim.IconType.UPGRADE_ARROW)
			{
				this.textUp.color = UiManager.colorCantAfford;
			}
			else
			{
				this.textUp.color = this.textUpDisabledColor;
			}
			if (!this.textDownNoCantAffordColorChangeForced && this.iconDownType != ButtonUpgradeAnim.IconType.NONE && this.iconDownType != ButtonUpgradeAnim.IconType.UPGRADE_ARROW)
			{
				this.textDown.color = UiManager.colorCantAfford;
			}
			else
			{
				this.textDown.color = this.textDownDisabledColor;
			}
		}

		public void ResetIcons()
		{
			if (this.iconUp != null)
			{
				if (this.iconUpType == ButtonUpgradeAnim.IconType.NONE)
				{
					this.iconUp.gameObject.SetActive(false);
					this.textUp.rectTransform.anchoredPosition = new Vector2(0f, this.textUp.rectTransform.anchoredPosition.y);
				}
				else
				{
					this.iconUp.gameObject.SetActive(true);
					this.textUp.rectTransform.anchoredPosition = new Vector2(this.iconOffset, this.textUp.rectTransform.anchoredPosition.y);
				}
			}
			if (this.iconDown != null)
			{
				if (this.iconDownType == ButtonUpgradeAnim.IconType.NONE)
				{
					this.iconDown.gameObject.SetActive(false);
					this.textDown.rectTransform.anchoredPosition = new Vector2(0f, this.textDown.rectTransform.anchoredPosition.y);
				}
				else
				{
					this.iconDown.gameObject.SetActive(true);
					this.textDown.rectTransform.anchoredPosition = new Vector2(this.iconOffset, this.textDown.rectTransform.anchoredPosition.y);
				}
			}
		}

		public Sprite GetSpriteFromIconType(ButtonUpgradeAnim.IconType iconType, bool isEnabled)
		{
			this.spritesIcon[1] = UiManager.spriteGoldIcon;
			this.spritesIconDisabled[1] = UiManager.spriteGoldIconDisabled;
			if (isEnabled)
			{
				return this.spritesIcon[(int)iconType];
			}
			return this.spritesIconDisabled[(int)iconType];
		}

		public static ButtonUpgradeAnim.IconType GetIconTypeFromCurrency(CurrencyType currencyType)
		{
			switch (currencyType)
			{
			case CurrencyType.GOLD:
				return ButtonUpgradeAnim.IconType.GOLD;
			case CurrencyType.SCRAP:
				return ButtonUpgradeAnim.IconType.SCRAPS;
			case CurrencyType.MYTHSTONE:
				return ButtonUpgradeAnim.IconType.MYTHSTONES;
			case CurrencyType.GEM:
				return ButtonUpgradeAnim.IconType.CREDITS;
			case CurrencyType.TOKEN:
				return ButtonUpgradeAnim.IconType.TOKENS;
			case CurrencyType.AEON:
				return ButtonUpgradeAnim.IconType.AEONS;
			case CurrencyType.CANDY:
				return ButtonUpgradeAnim.IconType.CANDY;
			default:
				throw new NotImplementedException();
			}
		}

		public bool isLevelUp;

		public bool openWarningPopup;

		public Text textUp;

		public Text textDown;

		private int textDownInitialFontSize;

		[NonSerialized]
		public bool changeTextDownToFit;

		public Shadow textShadowUp;

		public Shadow textShadowDown;

		public Outline textOutlineUp;

		public Outline textOutlineDown;

		public Image iconUp;

		public Image iconDown;

		public Color textUpEnabledColor;

		public Color textDownEnabledColor;

		public Color textUpDisabledColor;

		public Color textDownDisabledColor;

		[SerializeField]
		private bool changeColorAtLevelUp;

		[SerializeField]
		public bool doNotTouchToTextColor;

		[SerializeField]
		private Color textUpLevelUpColor;

		[SerializeField]
		private Color textDownLevelUpColor;

		[SerializeField]
		public Sprite spriteNormal;

		[SerializeField]
		public Sprite spriteLevelUp;

		[SerializeField]
		public Sprite spriteUpgrade;

		[SerializeField]
		public Sprite spriteNotInteractable;

		public Sprite[] spritesIcon;

		public Sprite[] spritesIconDisabled;

		public ButtonUpgradeAnim.IconType iconUpType;

		public ButtonUpgradeAnim.IconType iconDownType;

		public GameButton gameButton;

		public float textUpDisabledOffset;

		public float textDownDisabledOffset;

		private float textUpPosY;

		private float textDownPosY;

		public bool textOutlineEnableOnEnabled;

		public bool textOutlineEnableOnDisabled;

		public bool textShadowEnableOnEnabled = true;

		public bool textShadowEnableOnDisabled = true;

		public bool textShadowEnableOnLevelUp = true;

		public bool textCantAffordColorChange = true;

		public bool textCantAffordColorChangeManual;

		public bool textUpCantAffordColorChangeForced;

		public bool textDownCantAffordColorChangeForced;

		public bool textUpNoCantAffordColorChangeForced;

		public bool textDownNoCantAffordColorChangeForced;

		public float textUpLevelUpOffset;

		public float textDownLevelUpOffset;

		public float iconOffset = 15f;

		public RectTransform rectTransform;

		public bool dontChangeSprites;

		public float deltaTextPosYOnDown;

		private Vector2 textUpDefaultPos;

		private Vector2 textDownDefaultPos;

		private ContentSizeFitter m_textDownContentSizeFitter;

		private ContentSizeFitter m_textUpContentSizeFitter;

		public bool fakeDisabled;

		public enum IconType
		{
			NONE,
			GOLD,
			SCRAPS,
			MYTHSTONES,
			CREDITS,
			TOKENS,
			UPGRADE_ARROW,
			SKILL_POINT,
			AEONS,
			CANDY
		}
	}
}
