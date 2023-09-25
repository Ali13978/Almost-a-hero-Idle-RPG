using System;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelLootItem : AahMonoBehaviour
	{
		public void SetType(LootType lootType, double amount, string extraDescription = "", string extraDescription2 = "", int gearLevel = 0)
		{
			if (this.heroPortrait != null)
			{
				this.heroPortrait.gameObject.SetActive(false);
			}
			this.iconBg.gameObject.SetActive(true);
			this.iconQuestionMark.gameObject.SetActive(false);
			this.iconRuneQuestionMark.gameObject.SetActive(false);
			this.rectTransform.sizeDelta = new Vector2(this.rectTransform.sizeDelta.x, this.normalHeight);
			this.rectTransform.SetAnchorPosX(0f);
			this.rectTransform.SetSizeDeltaX(520f);
			this.textLootType.rectTransform.SetAnchorPosY(-4f);
			this.textLootAmount.rectTransform.SetAnchorPosY(0f);
			this.textLootAmount.alignment = TextAnchor.MiddleRight;
			this.textLootAmount.rectTransform.anchorMax = new Vector2(1f, 0.5f);
			this.textLootAmount.rectTransform.anchorMin = new Vector2(1f, 0.5f);
			this.textLootAmount.rectTransform.SetAnchorPosX(-119f);
			this.textLootType.rectTransform.SetAnchorPosX(53f);
			if (lootType == LootType.TOKENS)
			{
				this.iconBg.sprite = this.spriteTokens;
				this.textLootType.text = LM.Get("UI_TOKENS");
				if (extraDescription != string.Empty)
				{
					this.textLootAmount.text = extraDescription;
				}
				else
				{
					this.textLootAmount.text = GameMath.GetDoubleString(amount);
				}
			}
			else if (lootType == LootType.GEAR)
			{
				this.textLootType.text = extraDescription;
				this.textLootAmount.text = extraDescription2;
				this.iconBg.sprite = this.spriteGearLevels[gearLevel];
				this.iconQuestionMark.gameObject.SetActive(true);
			}
			else if (lootType == LootType.SCRAPS)
			{
				this.iconBg.sprite = this.spriteScraps;
				this.textLootType.text = LM.Get("UI_SCRAPS");
				if (extraDescription != string.Empty)
				{
					this.textLootAmount.text = extraDescription;
				}
				else
				{
					this.textLootAmount.text = GameMath.GetDoubleString(amount);
				}
			}
			else if (lootType == LootType.CREDITS)
			{
				this.iconBg.sprite = this.spriteCredits;
				this.textLootType.text = LM.Get("UI_GEMS");
				if (extraDescription != string.Empty)
				{
					this.textLootAmount.text = extraDescription;
				}
				else
				{
					this.textLootAmount.text = GameMath.GetDoubleString(amount);
				}
			}
			else if (lootType == LootType.CANDIES)
			{
				this.iconBg.sprite = this.spriteCandies;
				this.textLootType.text = LM.Get("UI_CANDIES");
				if (extraDescription != string.Empty)
				{
					this.textLootAmount.text = extraDescription;
				}
				else
				{
					this.textLootAmount.text = GameMath.GetDoubleString(amount);
				}
			}
			else if (lootType == LootType.RUNE)
			{
				if (amount > 1.0)
				{
					this.textLootType.text = LM.Get("UI_RUNE") + " x" + amount.ToString();
				}
				else
				{
					this.textLootType.text = LM.Get("UI_RUNE");
				}
				this.textLootAmount.text = extraDescription;
				this.iconBg.gameObject.SetActive(false);
				this.iconRuneQuestionMark.gameObject.SetActive(true);
			}
			else if (lootType == LootType.TRINKET)
			{
				this.rectTransform.sizeDelta = new Vector2(this.rectTransform.sizeDelta.x, 150f);
				this.textLootType.text = LM.Get("UI_TRINKET_OPENING_DESC");
				this.textLootAmount.text = extraDescription;
				this.iconBg.gameObject.SetActive(false);
			}
			else if (lootType == LootType.TRINKET_BOX)
			{
				this.textLootType.text = string.Format(LM.Get("SHOP_PACK_FIVE_TRINKET_DESC"), string.Empty);
				this.textLootAmount.text = " x" + amount.ToString();
				this.iconBg.sprite = this.spriteTrinketBox;
				this.iconBg.gameObject.SetActive(true);
			}
			else
			{
				if (lootType != LootType.SKINS)
				{
					throw new NotImplementedException();
				}
				this.textLootType.text = LM.Get("UI_SKIN");
				this.textLootAmount.text = extraDescription;
				this.SetTypeSkinBig(gearLevel);
			}
		}

		public void SetTypeSkinBig(int skinId)
		{
			this.iconBg.gameObject.SetActive(false);
			this.iconRuneQuestionMark.gameObject.SetActive(false);
			this.heroPortrait.gameObject.SetActive(true);
			Sprite heroAvatar = this.uiManager.GetHeroAvatar(skinId);
			this.heroPortrait.SetHero(heroAvatar, 1, false, 0f, false);
			this.rectTransform.sizeDelta = new Vector2(this.rectTransform.sizeDelta.x, this.skinHeight);
			this.textLootType.rectTransform.SetAnchorPosY(-20f);
			this.textLootAmount.rectTransform.SetAnchorPosY(30f);
			this.rectTransform.SetAnchorPosX(20f);
			this.rectTransform.SetSizeDeltaX(500f);
			this.textLootAmount.alignment = TextAnchor.MiddleLeft;
			this.textLootAmount.rectTransform.anchorMax = new Vector2(0f, 0.5f);
			this.textLootAmount.rectTransform.anchorMin = new Vector2(0f, 0.5f);
			this.textLootType.rectTransform.SetAnchorPosX(80f);
			this.textLootAmount.rectTransform.SetAnchorPosX(125f);
		}

		public void SetTypeGearBig(Gear gear, Sprite gearSprite, Sprite heroSprite, HeroDataBase hero)
		{
			this.heroPortrait.gameObject.SetActive(true);
			this.heroPortrait.SetHero(heroSprite, hero.evolveLevel, false, -5f, false);
			this.textLootType.text = LM.Get(hero.nameKey);
			this.textLootAmount.text = LM.Get(gear.data.nameKey);
			this.iconBg.sprite = this.spriteGearLevels[gear.level];
			this.icon.sprite = gearSprite;
			this.icon.color = Color.white;
			this.icon.enabled = true;
			if (this.trinketUi != null)
			{
				UnityEngine.Object.Destroy(this.trinketUi.gameObject);
			}
		}

		public void SetTypeGearBig(Sprite runeSprite, Sprite totemSprite, Color runeColor, Rune rune)
		{
			this.heroPortrait.gameObject.SetActive(true);
			this.heroPortrait.SetHero(totemSprite, 1, false, -5f, false);
			this.textLootType.text = rune.belongsTo.GetName();
			this.textLootAmount.text = LM.Get(rune.nameKey);
			this.iconBg.sprite = this.spriteGearLevels[4];
			this.icon.sprite = runeSprite;
			this.icon.color = runeColor;
			this.icon.enabled = true;
			if (this.trinketUi != null)
			{
				UnityEngine.Object.Destroy(this.trinketUi.gameObject);
			}
		}

		public void SetTypeGearBig(Trinket trinket, string levelString)
		{
			this.heroPortrait.gameObject.SetActive(false);
			this.textLootType.text = LM.Get("UI_TRINKET");
			this.textLootAmount.text = levelString;
			this.iconBg.sprite = this.spriteGearLevels[5];
			this.icon.enabled = false;
			if (this.trinketUi == null)
			{
				this.trinketUi = UnityEngine.Object.Instantiate<GameObject>(UiData.inst.prefabTrinket, this.icon.transform).GetComponent<TrinketUi>();
				this.trinketUi.Register();
				this.trinketUi.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
			}
			this.trinketUi.Init(trinket);
			this.trinketUi.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
		}

		public RectTransform rectTransform;

		public Text textLootType;

		public Text textLootAmount;

		public Image iconBg;

		public Image iconRuneQuestionMark;

		public Image iconQuestionMark;

		public Image icon;

		public HeroPortrait heroPortrait;

		public Sprite spriteTokens;

		public Sprite spriteScraps;

		public Sprite spriteCredits;

		public Sprite spriteCandies;

		public Sprite spriteTrinketBox;

		public Sprite[] spriteGearLevels;

		public UiManager uiManager;

		private TrinketUi trinketUi;

		private float normalHeight = 46f;

		private float skinHeight = 75f;
	}
}
