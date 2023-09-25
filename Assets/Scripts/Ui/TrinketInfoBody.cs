using System;
using System.Collections.Generic;
using DG.Tweening;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class TrinketInfoBody : MonoBehaviour
	{
		public void Init()
		{
			TrinketInfoBody.colorLevelUpInst = this.colorLevelUp;
			this.buttonUpgrade.gameButton.onClick = new GameButton.VoidFunc(this.Button_OnTrinketUpgradeClicked);
			this.changeColorButton.onClick = new GameButton.VoidFunc(this.OnTrinketChangeColorButtonClicked);
			this.changeBodyButton.onClick = new GameButton.VoidFunc(this.OnTrinketChangeBodyButtonClicked);
			this.panelTrinketEffects = new List<PanelTrinketEffect>();
			Utility.FillUiElementList<PanelTrinketEffect>(UiData.inst.prefabPanelTrinket, this.trinketEffectsParent, 3, this.panelTrinketEffects);
			for (int i = 0; i < 3; i++)
			{
				PanelTrinketEffect panelTrinketEffect = this.panelTrinketEffects[i];
				float y = panelTrinketEffect.rectTransform.sizeDelta.y;
				float num = y + 10f;
				panelTrinketEffect.rectTransform.anchoredPosition = new Vector2(0f, -65f - num * (float)i);
			}
			this.buttonUpgrade.changeTextDownToFit = true;
		}

		public void InitStrings()
		{
			this.buttonUpgrade.textDown.text = LM.Get("UI_UPGRADE");
			this.buttonUpgrade.ResetTextDownFontSize();
		}

		private void Button_OnTrinketUpgradeClicked()
		{
			UiCommandTrinketUpgrade command = new UiCommandTrinketUpgrade
			{
				trinket = this.selectedTrinket,
				effectIndex = this.selectedTrinket.GetTrinketEffectIndexToUpgrade()
			};
			this.forceToUpdate = true;
			this.uiManager.SetCommand(command);
			if (this.onTrinketUpgradeClicked != null)
			{
				this.onTrinketUpgradeClicked();
			}
			this.PlayTrinketUpgradedAnim();
		}

		private void OnTrinketChangeColorButtonClicked()
		{
			this.trinketUiSelected.trinketUi.simTrinket.bodyColorIndex = (this.trinketUiSelected.trinketUi.simTrinket.bodyColorIndex + 1) % 6;
			this.trinketUiSelected.trinketUi.InitVisual(true);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOn, 1f));
			if (this.onTrinketVisualChanged != null)
			{
				this.onTrinketVisualChanged(this.trinketUiSelected.trinketUi.simTrinket);
			}
		}

		private void OnTrinketChangeBodyButtonClicked()
		{
			this.trinketUiSelected.trinketUi.simTrinket.bodySpriteIndex = (this.trinketUiSelected.trinketUi.simTrinket.bodySpriteIndex + 1) % 6;
			this.trinketUiSelected.trinketUi.InitVisual(true);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOn, 1f));
			if (this.onTrinketVisualChanged != null)
			{
				this.onTrinketVisualChanged(this.trinketUiSelected.trinketUi.simTrinket);
			}
		}

		private void PlayTrinketUpgradedAnim()
		{
			if (this.upgradeAnim != null && this.upgradeAnim.IsPlaying())
			{
				this.upgradeAnim.Kill(false);
				this.trinketUiSelected.trinketParent.SetScale(1f);
				this.textLevel.transform.SetScale(1f);
			}
			this.upgradeAnim = DOTween.Sequence().Append(this.trinketUiSelected.trinketParent.DOScale(1.2f, 0.15f).SetEase(Ease.InBack)).Append(this.trinketUiSelected.trinketParent.DOScale(1f, 0.15f).SetEase(Ease.OutBack)).Insert(0f, this.textLevel.rectTransform.DOScale(1.2f, 0.15f).SetEase(Ease.InBack)).Insert(0.2f, this.textLevel.rectTransform.DOScale(1f, 0.15f).SetEase(Ease.OutBack)).Play<Sequence>();
		}

		public void SetTrinket(Simulator sim)
		{
			if (this.selectedTrinket == null)
			{
				throw new Exception("Set \"selectedTrinket\" before opening this panel");
			}
			Trinket trinket = this.selectedTrinket;
			bool flag = sim.IsTrinketCapped(trinket);
			bool flag2 = sim.CanUpgradeTrinket(trinket);
			if (this.forceToUpdate)
			{
				this.trinketUiSelected.trinketUi.Init(trinket);
				this.buttonUpgrade.gameObject.SetActive(true);
				this.textLevel.text = string.Concat(new object[]
				{
					"<color=#abe229ff>",
					LM.Get("UI_HEROES_LV"),
					"</color> ",
					trinket.GetTotalLevel()
				});
				this.buttonUpgrade.textUp.text = GameMath.GetDoubleString(trinket.GetUpgradeCost(trinket.GetTotalLevel()));
			}
			int i = 0;
			int count = this.panelTrinketEffects.Count;
			while (i < count)
			{
				PanelTrinketEffect panelTrinketEffect = this.panelTrinketEffects[i];
				panelTrinketEffect.imageMaxed.gameObject.SetActive(false);
				TrinketInfoBody.UpdatePanelTrinketEffect(trinket, flag2, i, panelTrinketEffect);
				i++;
			}
			if (flag)
			{
				this.buttonUpgrade.gameObject.SetActive(false);
				this.imageMax.gameObject.SetActive(true);
			}
			else
			{
				this.buttonUpgrade.gameObject.SetActive(true);
				this.imageMax.gameObject.SetActive(false);
			}
			this.buttonUpgrade.gameButton.interactable = flag2;
		}

		public static void UpdatePanelTrinketEffect(Trinket trinket, bool canUpgrade, int i, PanelTrinketEffect panelTrinketEffect)
		{
			TrinketEffect effectOfGroup = trinket.GetEffectOfGroup((TrinketEffectGroup)i);
			if (effectOfGroup != null)
			{
				panelTrinketEffect.textHeader.text = UiManager.GetTrinketEffectRarityLocString(effectOfGroup.GetGroup());
				panelTrinketEffect.gameObject.SetActive(true);
				panelTrinketEffect.textHeader.gameObject.SetActive(true);
				panelTrinketEffect.textDesc.gameObject.SetActive(true);
				panelTrinketEffect.nonExisting.gameObject.SetActive(false);
				Sprite[] sprites = effectOfGroup.GetSprites();
				if (sprites.Length > 1)
				{
					panelTrinketEffect.basicImagesParent.gameObject.SetActive(true);
					panelTrinketEffect.otherImage.gameObject.SetActive(false);
					if (sprites.Length > 2)
					{
						Sprite sprite = sprites[0];
						sprites[0] = sprites[1];
						sprites[1] = sprite;
					}
					for (int j = 0; j < panelTrinketEffect.basicImages.Length; j++)
					{
						Image image = panelTrinketEffect.basicImages[j];
						if (j < sprites.Length)
						{
							image.gameObject.SetActive(true);
							Sprite sprite2 = sprites[j];
							if (image.sprite != sprite2)
							{
								image.sprite = sprite2;
								image.SetNativeSize();
							}
						}
						else
						{
							image.gameObject.SetActive(false);
						}
					}
				}
				else
				{
					panelTrinketEffect.basicImagesParent.gameObject.SetActive(false);
					panelTrinketEffect.otherImage.gameObject.SetActive(true);
					Sprite sprite3 = sprites[0];
					if (panelTrinketEffect.otherImage.sprite != sprite3)
					{
						panelTrinketEffect.otherImage.sprite = sprite3;
						panelTrinketEffect.otherImage.SetNativeSize();
					}
				}
				if (effectOfGroup.level == effectOfGroup.GetMaxLevel())
				{
					panelTrinketEffect.textDesc.text = effectOfGroup.GetDesc(false, -1);
					panelTrinketEffect.selectionFrame.gameObject.SetActive(false);
					panelTrinketEffect.imageMaxed.gameObject.SetActive(true);
					panelTrinketEffect.textLevel.gameObject.SetActive(false);
				}
				else if (canUpgrade && trinket.GetTrinketEffectToUpgrade() == effectOfGroup)
				{
					panelTrinketEffect.textDesc.text = effectOfGroup.GetDesc(true, -1);
					panelTrinketEffect.imageMaxed.gameObject.SetActive(false);
					panelTrinketEffect.selectionFrame.gameObject.SetActive(true);
					panelTrinketEffect.textLevel.gameObject.SetActive(true);
					panelTrinketEffect.textLevel.text = string.Format("{0}/{1}", AM.cs(effectOfGroup.level.ToString(), TrinketInfoBody.colorLevelUpInst), effectOfGroup.GetMaxLevel().ToString());
				}
				else
				{
					panelTrinketEffect.textDesc.text = effectOfGroup.GetDesc(false, -1);
					panelTrinketEffect.imageMaxed.gameObject.SetActive(false);
					panelTrinketEffect.selectionFrame.gameObject.SetActive(false);
					panelTrinketEffect.textLevel.gameObject.SetActive(true);
					panelTrinketEffect.textLevel.text = string.Format("{0}/{1}", effectOfGroup.level.ToString(), effectOfGroup.GetMaxLevel().ToString());
				}
			}
			else
			{
				panelTrinketEffect.basicImagesParent.gameObject.SetActive(false);
				panelTrinketEffect.otherImage.gameObject.SetActive(false);
				panelTrinketEffect.textHeader.gameObject.SetActive(false);
				panelTrinketEffect.textDesc.gameObject.SetActive(false);
				panelTrinketEffect.imageMaxed.gameObject.SetActive(false);
				panelTrinketEffect.selectionFrame.gameObject.SetActive(false);
				panelTrinketEffect.textLevel.gameObject.SetActive(false);
				panelTrinketEffect.nonExisting.gameObject.SetActive(true);
			}
		}

		public static void UpdatePanelTrinketEffect(TrinketEffect te, PanelTrinketEffect panelTrinketEffect, bool showMaxLevelAvailable = false)
		{
			panelTrinketEffect.textHeader.text = UiManager.GetTrinketEffectRarityLocString(te.GetGroup());
			panelTrinketEffect.gameObject.SetActive(true);
			panelTrinketEffect.textHeader.gameObject.SetActive(true);
			panelTrinketEffect.textDesc.gameObject.SetActive(true);
			panelTrinketEffect.nonExisting.gameObject.SetActive(false);
			Sprite[] sprites = te.GetSprites();
			if (sprites.Length > 1)
			{
				panelTrinketEffect.basicImagesParent.gameObject.SetActive(true);
				panelTrinketEffect.otherImage.gameObject.SetActive(false);
				if (sprites.Length > 2)
				{
					Sprite sprite = sprites[0];
					sprites[0] = sprites[1];
					sprites[1] = sprite;
				}
				for (int i = 0; i < panelTrinketEffect.basicImages.Length; i++)
				{
					Image image = panelTrinketEffect.basicImages[i];
					if (i < sprites.Length)
					{
						image.gameObject.SetActive(true);
						Sprite sprite2 = sprites[i];
						if (image.sprite != sprite2)
						{
							image.sprite = sprite2;
							image.SetNativeSize();
						}
					}
					else
					{
						image.gameObject.SetActive(false);
					}
				}
			}
			else
			{
				panelTrinketEffect.basicImagesParent.gameObject.SetActive(false);
				panelTrinketEffect.otherImage.gameObject.SetActive(true);
				Sprite sprite3 = sprites[0];
				if (panelTrinketEffect.otherImage.sprite != sprite3)
				{
					panelTrinketEffect.otherImage.sprite = sprite3;
					panelTrinketEffect.otherImage.SetNativeSize();
				}
			}
			panelTrinketEffect.textDesc.text = te.GetDesc(false, -1);
			panelTrinketEffect.imageMaxed.gameObject.SetActive(false);
			panelTrinketEffect.selectionFrame.gameObject.SetActive(false);
			panelTrinketEffect.textLevel.gameObject.SetActive(true);
			panelTrinketEffect.textLevel.text = ((!showMaxLevelAvailable) ? string.Format("{0}/{1}", te.level.ToString(), te.GetMaxLevel().ToString()) : te.GetMaxLevel().ToString());
		}

		[NonSerialized]
		public UiManager uiManager;

		public ButtonSelectTrinket trinketUiSelected;

		public ButtonUpgradeAnim buttonUpgrade;

		public Text textLevel;

		public Image imageMax;

		public Action onTrinketUpgradeClicked;

		public Action<Trinket> onTrinketVisualChanged;

		public GameButton changeColorButton;

		public GameButton changeBodyButton;

		public List<PanelTrinketEffect> panelTrinketEffects;

		public Color colorMaxLevel;

		public Color colorLevelUp;

		public Trinket selectedTrinket;

		public RectTransform trinketEffectsParent;

		private static Color colorLevelUpInst;

		public bool forceToUpdate;

		private Sequence upgradeAnim;

		private int upgradeAnimationsLeft;
	}
}
