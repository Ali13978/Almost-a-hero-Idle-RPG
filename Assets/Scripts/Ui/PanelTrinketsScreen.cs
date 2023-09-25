using System;
using System.Collections.Generic;
using DG.Tweening;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelTrinketsScreen : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			if (this.buttonUpgrade != null)
			{
				this.buttonUpgrade.textDownContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
				this.buttonUpgrade.textDownContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
				this.buttonUpgrade.textDown.rectTransform.SetSizeDeltaX(170f);
				this.buttonUpgrade.textDown.resizeTextForBestFit = true;
				this.buttonUpgrade.textDown.resizeTextMaxSize = 36;
				this.buttonUpgrade.textDown.resizeTextMinSize = 22;
				this.buttonUpgrade.changeTextDownToFit = true;
			}
			if (this.initPaneltrinketEffects)
			{
				Utility.FillUiElementList<PanelTrinketEffect>(UiData.inst.prefabPanelTrinket, this.trinketEffectsParent, 3, this.panelTrinketEffects);
				for (int i = 0; i < 3; i++)
				{
					PanelTrinketEffect panelTrinketEffect = this.panelTrinketEffects[i];
					float y = panelTrinketEffect.rectTransform.sizeDelta.y;
					float num = y + 10f;
					panelTrinketEffect.rectTransform.anchoredPosition = new Vector2(0f, -65f - num * (float)i);
					panelTrinketEffect.rectTransform.SetSizeDeltaX(780f);
				}
			}
		}

		public void InitStrings()
		{
			if (this.buttonTab != null)
			{
				this.buttonTab.text.text = LM.Get("UI_TAB_TRINKETS");
			}
			if (this.buttonUpgrade != null)
			{
				this.buttonUpgrade.textDown.text = LM.Get("UI_UPGRADE");
				this.buttonUpgrade.ResetTextDownFontSize();
			}
			if (this.buttonDestroy != null)
			{
				this.buttonDestroy.textDown.text = LM.Get("UI_DESTROY");
			}
			if (this.textHeader != null)
			{
				this.textHeader.text = LM.Get("UI_TAB_TRINKETS");
			}
			if (this.textEquip != null)
			{
				this.textEquip.text = LM.Get("UI_TRINKET_CHANGE");
			}
			if (this.buttonUnequip != null)
			{
				this.buttonUnequip.text.text = LM.Get("UI_UNEQUIP");
			}
			if (this.buttonEquip != null)
			{
				this.buttonEquip.text.text = LM.Get("UI_TRINKET_CHANGE");
			}
			if (this.buttonDestroy != null || this.buttonEquip == null)
			{
				this.textNotSelected.text = LM.Get("TRINKET_SELECT");
			}
			else
			{
				this.textNotSelected.text = LM.Get("TRINKET_DOESNOT_HAVE");
			}
		}

		public void SetEmptyTextHint()
		{
			this.textNotSelected.text = LM.Get("TRINKET_DOESNOT_HAVE");
			this.decorationRect.SetAnchorPosY(0f);
		}

		public void SetEmptyTextNormal()
		{
			this.textNotSelected.text = LM.Get("TRINKET_DOESNOT_HAVE");
			this.decorationRect.SetAnchorPosY(85f);
		}

		public void SetDetails(Simulator sim, Trinket trinket)
		{
			bool flag = sim.IsTrinketCapped(trinket);
			bool flag2 = sim.CanUpgradeTrinket(trinket);
			if (UiManager.stateJustChanged)
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
			this.pinnedTrinketParent.SetActive(sim.IsTrinketPinned(trinket) != -1);
		}

		public void SetEquipTimer(HeroDataBase selectingFor)
		{
			this.textEquipTime.text = GameMath.GetTimeString(selectingFor.trinketEquipTimer);
		}

		public void InitButtonSelectTrinkets(int numSlots)
		{
			int num = (this.Y0_BUTTON_SELECT == this.Y1_BUTTON_SELECT) ? 1 : 2;
			int num2 = (num != 2) ? 0 : ((numSlots % 2 != 1) ? 0 : 1);
			this.scrollviewContent.sizeDelta = new Vector2(this.X_PADDING * 2f + this.X_DELTA_BUTTON_SELECT * (float)(num2 + numSlots / num), this.scrollviewContent.sizeDelta.y);
			if (this.buttonSelectTrinkets != null)
			{
				int i = 0;
				int num3 = this.buttonSelectTrinkets.Length;
				while (i < num3)
				{
					UnityEngine.Object.Destroy(this.buttonSelectTrinkets[i].gameObject);
					i++;
				}
			}
			this.buttonSelectTrinkets = new ButtonSelectTrinket[numSlots];
			for (int j = numSlots - 1; j >= 0; j--)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabButtonSelect, this.scrollviewContent);
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				int num4 = j / num;
				bool flag = j % num == 0;
				gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.X_PADDING + this.X_BUTTON_SELECT + (float)num4 * this.X_DELTA_BUTTON_SELECT, (!flag) ? this.Y1_BUTTON_SELECT : this.Y0_BUTTON_SELECT);
				this.buttonSelectTrinkets[j] = gameObject.GetComponent<ButtonSelectTrinket>();
			}
		}

		public void PlayTrinketUpgradedAnim()
		{
			DOTween.Sequence().Append(this.trinketUiSelected.trinketParent.DOScale(1.2f, 0.2f).SetEase(Ease.InBack)).Append(this.trinketUiSelected.trinketParent.DOScale(1f, 0.2f).SetEase(Ease.OutBack)).Insert(0f, this.textLevel.rectTransform.DOScale(1.2f, 0.2f).SetEase(Ease.InBack)).Insert(0.2f, this.textLevel.rectTransform.DOScale(1f, 0.2f).SetEase(Ease.OutBack)).Play<Sequence>();
		}

		public GameButton buttonTab;

		public RectTransform lockedTabIcon;

		public RectTransform decorationRect;

		public ButtonUpgradeAnim buttonUpgrade;

		public Image imageMax;

		public Text textLevel;

		public Text textDestroy;

		public GameButton pinTrinketButton;

		public GameObject pinnedTrinketParent;

		public RectTransform trinketEffectsParent;

		public List<PanelTrinketEffect> panelTrinketEffects;

		public RectTransform selectedTrinketStateParent;

		public MenuShowCurrency currencyWidget;

		public int selected;

		public GameObject parentNotSelected;

		public Text textNotSelected;

		public GameObject parentSelected;

		public GameButton buttonEquip;

		public GameButton buttonUnequip;

		public RectTransform buttonCannotEquip;

		public Text textEquip;

		public Text textEquipTime;

		public ButtonUpgradeAnim buttonDestroy;

		public ButtonSelectTrinket[] buttonSelectTrinkets;

		public GameButton[] buttonCloses;

		public Text textHeader;

		public GameObject prefabButtonSelect;

		public RectTransform scrollviewContent;

		public GameObject trinketBoxIcon;

		public float X_PADDING;

		public float X_BUTTON_SELECT;

		public float X_DELTA_BUTTON_SELECT;

		public float Y0_BUTTON_SELECT;

		public float Y1_BUTTON_SELECT;

		public float X_SCROLLVIEW;

		public Sprite spriteFrameYellow;

		public Sprite spriteFrameGreen;

		public Color colorMaxLevel;

		public Color colorLevelUp;

		public ButtonSelectTrinket trinketUiSelected;

		public ScrollRect trinketScroll;

		public bool initPaneltrinketEffects;
	}
}
