using System;
using DG.Tweening;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelGear : AahMonoBehaviour
	{
		public void SetUnLooted()
		{
			this.notLootedDescription.gameObject.SetActive(true);
			this.infoParent.gameObject.SetActive(false);
			this.imageIcon.color = PanelGear.IconNotLootedColor;
			this.imageIconBg.color = PanelGear.BackgroundNotLootedColor;
		}

		public void SetDetails(Gear gear, bool upgradable, string gearPrice, Sprite spriteGearIcon, UniversalTotalBonus universalTotalBonus)
		{
			this.notLootedDescription.gameObject.SetActive(false);
			this.infoParent.gameObject.SetActive(true);
			int level = gear.level;
			bool flag = gear.IsMaxLevel();
			if (UiManager.stateJustChanged)
			{
				this.PlayTransitionAnimationIfNeeded(gear.data, level, true, spriteGearIcon);
				this.textGearName.text = LM.Get(gear.data.nameKey).ToUpper(LM.culture);
				this.textGearName.color = UiManager.colorHeroLevels[level];
				this.textBonus.text = gear.UniversalBonusString();
				Text text = this.textBonus;
				text.text = text.text + "\n" + gear.SkillBonusString();
				this.textBonusPercent.text = gear.UniversalBonusPercentString(universalTotalBonus, 0);
				Text text2 = this.textBonusPercent;
				text2.text = text2.text + "\n" + gear.SkillBonusPercentString(0);
				this.textBonusNextPercent.text = gear.UniversalBonusPercentString(universalTotalBonus, 1);
				Text text3 = this.textBonusNextPercent;
				text3.text = text3.text + "\n" + gear.SkillBonusPercentString(1);
				this.badgeMaxLevel.SetActive(flag);
				this.textBonusNextPercent.gameObject.SetActive(!flag);
				this.buttonUpgrade.gameObject.SetActive(!flag);
			}
			if (!flag)
			{
				this.buttonUpgrade.gameButton.interactable = upgradable;
				if (UiManager.stateJustChanged)
				{
					this.buttonUpgrade.textUp.text = gearPrice;
				}
			}
		}

		public void PlayTransitionAnimationIfNeeded(GearData gear, int gearLevel, bool isLooted, Sprite gearIcon)
		{
			bool flag = this.gear != null && this.gear != gear;
			this.gear = gear;
			if (flag)
			{
				if (this.anim != null)
				{
					this.anim.Kill(false);
				}
				this.imageIconBg.rectTransform.localScale = Vector3.one;
				this.anim = DOTween.Sequence().Append(this.imageIconBg.rectTransform.DOScale(new Vector3(0.75f, 0.75f, 1f), 0.2f).SetEase(Ease.InBack, 2.5f)).AppendCallback(delegate
				{
					this.SetGearVisual(isLooted, gearIcon, gearLevel);
				}).Append(this.imageIconBg.rectTransform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack, 2.5f)).AppendCallback(new TweenCallback(this.AnimFinished)).Play<Sequence>();
			}
			else
			{
				this.SetGearVisual(isLooted, gearIcon, gearLevel);
			}
		}

		private void SetGearVisual(bool isLooted, Sprite gearIcon, int gearLevel)
		{
			this.SetGearImage(gearIcon, gearLevel);
			if (isLooted)
			{
				this.imageIcon.color = PanelGear.LootedColor;
				this.imageIconBg.color = PanelGear.LootedColor;
			}
			else
			{
				this.imageIcon.color = PanelGear.IconNotLootedColor;
				this.imageIconBg.color = PanelGear.BackgroundNotLootedColor;
			}
		}

		private void AnimFinished()
		{
			this.anim = null;
		}

		private void SetGearImage(Sprite spriteGearIcon, int gearLevel)
		{
			this.imageIconBg.sprite = this.spritesIconBg[gearLevel];
			this.imageIcon.sprite = spriteGearIcon;
		}

		public GearData gear;

		public ButtonUpgradeAnim buttonUpgrade;

		public Image imageIconBg;

		public Image imageIcon;

		public Text textGearName;

		public Text textBonus;

		public Text textBonusPercent;

		public Text textBonusNextPercent;

		public Text notLootedDescription;

		public RectTransform infoParent;

		public Sprite[] spritesIconBg;

		public GameObject badgeMaxLevel;

		public Sequence anim;

		public const float AnimDurationInSeconds = 0.3f;

		private static readonly Color LootedColor = Color.white;

		private static readonly Color IconNotLootedColor = new Color(1f, 1f, 1f, 0.35f);

		private static readonly Color BackgroundNotLootedColor = new Color(0.4f, 0.4f, 0.4f, 1f);
	}
}
