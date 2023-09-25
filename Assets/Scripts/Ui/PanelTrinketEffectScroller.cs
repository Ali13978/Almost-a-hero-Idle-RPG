using System;
using System.Collections.Generic;
using System.Linq;
using Simulation;
using Static;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelTrinketEffectScroller : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public void UpdateScrollElementCount(int count)
		{
			Utility.FillUiElementList<TrinketEffectOptionWidget>(this.trinketEffectOptionWidgetPrefab, this.scrollRect.content, count + 3, this.trinketEffectOptionWidgets, new Utility.ElementStateChange<TrinketEffectOptionWidget>(this.OnSpawnWidget), new Utility.ElementStateChange<TrinketEffectOptionWidget>(this.OnDestroyWidget));
		}

		private void OnDestroyWidget(TrinketEffectOptionWidget obj, RectTransform parent, int index)
		{
			this.isDirty = true;
		}

		private void OnSpawnWidget(TrinketEffectOptionWidget obj, RectTransform parent, int index)
		{
			obj.rectTransform.SetRightDelta(10f);
			obj.rectTransform.SetLeftDelta(10f);
			obj.selectionOutline.gameObject.SetActive(false);
			obj.index = index;
			obj.onClick = new Action<int>(this.Button_OnEffectSelect);
			this.isDirty = true;
		}

		private List<KeyValuePair<TrinketEffect, int>> GetGroup(TrinketEffectGroup group)
		{
			List<KeyValuePair<TrinketEffect, int>> list = new List<KeyValuePair<TrinketEffect, int>>();
			foreach (KeyValuePair<int, int> keyValuePair in this.trinketEffects)
			{
				TrinketEffect trinketEffect = TypeHelper.cachedTrinketEffects[keyValuePair.Key];
				if (trinketEffect.GetGroup() == group)
				{
					list.Add(new KeyValuePair<TrinketEffect, int>(trinketEffect, keyValuePair.Value));
				}
			}
			list.Sort(new Comparison<KeyValuePair<TrinketEffect, int>>(this.TrinketEffectOrderComparer));
			return list;
		}

		private int TrinketEffectOrderComparer(KeyValuePair<TrinketEffect, int> a, KeyValuePair<TrinketEffect, int> b)
		{
			int item = TypeHelper.ConvertTrinketEffect(a.Key);
			int item2 = TypeHelper.ConvertTrinketEffect(b.Key);
			int num = TypeHelper.TrinketEffectsOrder.IndexOf(item);
			int value = TypeHelper.TrinketEffectsOrder.IndexOf(item2);
			return num.CompareTo(value);
		}

		private void EnableDontHave(string groupName)
		{
			this.dontHaveEffectTypeParent.gameObject.SetActive(true);
			this.scrollRect.gameObject.SetActive(false);
			this.selectGroupDesc.gameObject.SetActive(false);
			this.dontHaveEffectTypeDesc.text = string.Format(LM.Get("TRINKET_EFFECT_TYPE_NOT_AVAILABLE_DESC"), groupName);
			this.buttonOkay.gameObject.SetActive(true);
		}

		private void DisableDontHave()
		{
			this.dontHaveEffectTypeParent.gameObject.SetActive(false);
			this.scrollRect.gameObject.SetActive(true);
			this.selectGroupDesc.gameObject.SetActive(true);
			this.buttonOkay.gameObject.SetActive(false);
		}

		public void SetWidgetPositionsByCategory()
		{
			RectTransform component = this.scrollRect.GetComponent<RectTransform>();
			component.SetTopDelta(30f);
			component.SetBottomDelta(30f);
			List<KeyValuePair<TrinketEffect, int>> group = this.GetGroup(TrinketEffectGroup.COMMON);
			List<KeyValuePair<TrinketEffect, int>> group2 = this.GetGroup(TrinketEffectGroup.SECONDARY);
			List<KeyValuePair<TrinketEffect, int>> group3 = this.GetGroup(TrinketEffectGroup.SPECIAL);
			float num = this.startOffest;
			int num2 = 0;
			this.textHeader.text = LM.Get("TRINKET_EFFECT_INVENTORY");
			this.scrollDisenchantFlowChard.gameObject.SetActive(true);
			num -= 200f;
			List<List<KeyValuePair<TrinketEffect, int>>> list = new List<List<KeyValuePair<TrinketEffect, int>>>
			{
				group,
				group2,
				group3
			};
			List<TrinketCategoryHeader> list2 = new List<TrinketCategoryHeader>
			{
				this.commonHeader,
				this.secondaryHeader,
				this.specialHeader
			};
			this.DisableDontHave();
			this.selectGroupDesc.gameObject.SetActive(false);
			for (int i = 0; i < 3; i++)
			{
				List<KeyValuePair<TrinketEffect, int>> list3 = list[i];
				TrinketCategoryHeader trinketCategoryHeader = list2[i];
				trinketCategoryHeader.gameObject.SetActive(true);
				trinketCategoryHeader.rectTransform.SetAnchorPosY(num);
				num += this.headerDisdance;
				if (list3.Count<KeyValuePair<TrinketEffect, int>>() == 0)
				{
					TrinketEffectOptionWidget trinketEffectOptionWidget = this.trinketEffectOptionWidgets[num2];
					trinketEffectOptionWidget.gameObject.SetActive(true);
					trinketEffectOptionWidget.interactable = false;
					trinketEffectOptionWidget.rectTransform.SetAnchorPosY(num);
					trinketEffectOptionWidget.EnableNotAvailableDesc();
					trinketEffectOptionWidget.notavailableDesc.text = string.Format(LM.Get("TRINKET_EFFECT_TYPE_NOT_AVAILABLE_DESC"), PanelTrinketEffectScroller.GetGroupName((TrinketEffectGroup)i));
					trinketEffectOptionWidget.DisableBottomLine();
					num2++;
					num += this.distance;
				}
				else
				{
					int num3 = 0;
					foreach (KeyValuePair<TrinketEffect, int> keyValuePair in list3)
					{
						TrinketEffectOptionWidget trinketEffectOptionWidget2 = this.trinketEffectOptionWidgets[num2];
						trinketEffectOptionWidget2.gameObject.SetActive(true);
						trinketEffectOptionWidget2.interactable = false;
						trinketEffectOptionWidget2.rectTransform.SetAnchorPosY(num);
						trinketEffectOptionWidget2.DisableNotAvailableDesc();
						trinketEffectOptionWidget2.SetNormal();
						if (num3 < list3.Count - 1)
						{
							trinketEffectOptionWidget2.EnableBottomLine();
						}
						else
						{
							trinketEffectOptionWidget2.DisableBottomLine();
						}
						trinketEffectOptionWidget2.textDescription.text = keyValuePair.Key.GetDesc(false, -1);
						trinketEffectOptionWidget2.textMaxLevel.text = string.Format(LM.Get("UI_LEVELS_PLURAL"), keyValuePair.Key.GetMaxLevel());
						if (keyValuePair.Value <= 0)
						{
							trinketEffectOptionWidget2.interactable = false;
							trinketEffectOptionWidget2.canvasGroup.alpha = 0.2f;
							trinketEffectOptionWidget2.textDescription.color = this.effectDescColorWhenNoCopies;
							trinketEffectOptionWidget2.textMaxLevel.color = this.effectMaxLevelsColorWhenNoCopies;
							trinketEffectOptionWidget2.textDescription.text = keyValuePair.Key.GetDescFirstWithoutColor();
						}
						else
						{
							trinketEffectOptionWidget2.canvasGroup.alpha = 1f;
							trinketEffectOptionWidget2.interactable = true;
							trinketEffectOptionWidget2.textDescription.color = this.effectDescColorWhenAtLeatOneCopy;
							trinketEffectOptionWidget2.textMaxLevel.color = this.effectMaxLevelsColorWhenAtLeatOneCopy;
							trinketEffectOptionWidget2.textDescription.text = keyValuePair.Key.GetDesc(false, -1);
						}
						trinketEffectOptionWidget2.textRemaining.text = "x" + keyValuePair.Value;
						num += this.distance;
						num2++;
						num3++;
					}
				}
				num += this.padding * 2f;
			}
			num -= this.padding;
			this.scrollRect.content.SetSizeDeltaY(-num);
			for (int j = num2; j < this.trinketEffectOptionWidgets.Count; j++)
			{
				this.trinketEffectOptionWidgets[j].gameObject.SetActive(false);
			}
			this.buttonSelect.interactable = false;
		}

		public static string GetGroupName(TrinketEffectGroup group)
		{
			switch (group)
			{
			case TrinketEffectGroup.COMMON:
				return LM.Get("TRINKET_EFFECT_RARITY_COMMON");
			case TrinketEffectGroup.SECONDARY:
				return LM.Get("TRINKET_EFFECT_RARITY_SECONDARY");
			case TrinketEffectGroup.SPECIAL:
				return LM.Get("TRINKET_EFFECT_RARITY_SPECIAL");
			default:
				throw new Exception();
			}
		}

		public void SetWidgetPositionsByCategory(TrinketEffectGroup group)
		{
			this.currentGroup = this.GetGroup(group);
			RectTransform component = this.scrollRect.GetComponent<RectTransform>();
			component.SetTopDelta(125f);
			component.SetBottomDelta(230f);
			this.selectGroupDesc.gameObject.SetActive(true);
			this.scrollDisenchantFlowChard.gameObject.SetActive(false);
			this.buttonSelect.text.text = LM.Get("UI_SELECT");
			this.textHeader.text = PanelTrinketEffectScroller.GetGroupName(group);
			this.selectGroupDesc.text = string.Format(LM.Get("UI_TRINKET_SELECT_EFFECT_DESC"), this.textHeader.text);
			float num = this.startOffest;
			int num2 = 0;
			List<TrinketCategoryHeader> list = new List<TrinketCategoryHeader>
			{
				this.commonHeader,
				this.secondaryHeader,
				this.specialHeader
			};
			foreach (TrinketCategoryHeader trinketCategoryHeader in list)
			{
				trinketCategoryHeader.gameObject.SetActive(false);
			}
			if (this.currentGroup.Count<KeyValuePair<TrinketEffect, int>>() == 0)
			{
				TrinketEffectOptionWidget trinketEffectOptionWidget = this.trinketEffectOptionWidgets[num2];
				trinketEffectOptionWidget.gameObject.SetActive(true);
				trinketEffectOptionWidget.interactable = false;
				trinketEffectOptionWidget.rectTransform.SetAnchorPosY(num);
				trinketEffectOptionWidget.EnableNotAvailableDesc();
				trinketEffectOptionWidget.notavailableDesc.text = string.Format(LM.Get("TRINKET_EFFECT_TYPE_NOT_AVAILABLE_DESC"), PanelTrinketEffectScroller.GetGroupName(group));
				trinketEffectOptionWidget.DisableBottomLine();
				num2++;
				num += this.distance;
				this.EnableDontHave(this.textHeader.text);
			}
			else
			{
				this.DisableDontHave();
				foreach (KeyValuePair<TrinketEffect, int> keyValuePair in this.currentGroup)
				{
					TrinketEffectOptionWidget trinketEffectOptionWidget2 = this.trinketEffectOptionWidgets[num2];
					trinketEffectOptionWidget2.gameObject.SetActive(true);
					trinketEffectOptionWidget2.interactable = true;
					trinketEffectOptionWidget2.SetNormal();
					trinketEffectOptionWidget2.DisableNotAvailableDesc();
					trinketEffectOptionWidget2.DisableBottomLine();
					trinketEffectOptionWidget2.rectTransform.SetAnchorPosY(num);
					if (keyValuePair.Value <= 0)
					{
						trinketEffectOptionWidget2.canvasGroup.alpha = 0.2f;
						trinketEffectOptionWidget2.interactable = false;
						trinketEffectOptionWidget2.textDescription.color = this.effectDescColorWhenNoCopies;
						trinketEffectOptionWidget2.textMaxLevel.color = this.effectMaxLevelsColorWhenNoCopies;
						trinketEffectOptionWidget2.textDescription.text = keyValuePair.Key.GetDescFirstWithoutColor();
					}
					else
					{
						trinketEffectOptionWidget2.canvasGroup.alpha = 1f;
						trinketEffectOptionWidget2.interactable = true;
						trinketEffectOptionWidget2.textDescription.color = this.effectDescColorWhenAtLeatOneCopy;
						trinketEffectOptionWidget2.textMaxLevel.color = this.effectMaxLevelsColorWhenAtLeatOneCopy;
						trinketEffectOptionWidget2.textDescription.text = keyValuePair.Key.GetDesc(false, -1);
					}
					trinketEffectOptionWidget2.textMaxLevel.text = string.Format(LM.Get("UI_LEVELS_PLURAL"), keyValuePair.Key.GetMaxLevel());
					trinketEffectOptionWidget2.textRemaining.text = "x" + keyValuePair.Value;
					num += this.distance + this.padding;
					num2++;
				}
			}
			this.scrollRect.content.SetSizeDeltaY(-num);
			for (int i = num2; i < this.trinketEffectOptionWidgets.Count; i++)
			{
				this.trinketEffectOptionWidgets[i].gameObject.SetActive(false);
			}
			this.lastSelectedIndex = -1;
			this.buttonSelect.interactable = false;
		}

		private void Button_OnEffectSelect(int obj)
		{
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiOpenTabClick, 1f));
			if (this.lastSelectedIndex == obj)
			{
				return;
			}
			KeyValuePair<TrinketEffect, int> keyValuePair = this.currentGroup[obj];
			TrinketEffectOptionWidget trinketEffectOptionWidget = this.trinketEffectOptionWidgets[obj];
			this.selectedTrinketEffect = keyValuePair.Key;
			trinketEffectOptionWidget.selectionOutline.gameObject.SetActive(true);
			trinketEffectOptionWidget.DoBoingAnimation();
			trinketEffectOptionWidget.SetSelected();
			if (this.lastSelectedIndex != -1)
			{
				this.trinketEffectOptionWidgets[this.lastSelectedIndex].SetNormal();
			}
			else
			{
				this.buttonSelect.interactable = true;
			}
			this.lastSelectedIndex = obj;
		}

		public override void Init()
		{
			this.trinketEffectOptionWidgets = new List<TrinketEffectOptionWidget>();
			this.lastSelectedIndex = -1;
		}

		public void InitStrings()
		{
			this.commonHeader.header.text = PanelTrinketEffectScroller.GetGroupName(TrinketEffectGroup.COMMON);
			this.secondaryHeader.header.text = PanelTrinketEffectScroller.GetGroupName(TrinketEffectGroup.SECONDARY);
			this.specialHeader.header.text = PanelTrinketEffectScroller.GetGroupName(TrinketEffectGroup.SPECIAL);
			this.buttonOkay.text.text = LM.Get("UI_OKAY");
			this.infoFlowChartText.text = LM.Get("INFO_FLOW_CHART_TEXT");
		}

		public void PlayEffectSelectedAnim(Action onComplete)
		{
			TrinketEffectOptionWidget trinketEffectOptionWidget = this.trinketEffectOptionWidgets[this.lastSelectedIndex];
			KeyValuePair<TrinketEffect, int> keyValuePair = this.currentGroup[this.lastSelectedIndex];
			trinketEffectOptionWidget.DoBoingAnimation();
			trinketEffectOptionWidget.PlayEffectSelectedAnim(keyValuePair.Value - 1, keyValuePair.Key, onComplete, new Action<TrinketEffectOptionWidget, TrinketEffect>(this.SetWidgetDisabledAfterSelectedAnim));
		}

		private void SetWidgetDisabledAfterSelectedAnim(TrinketEffectOptionWidget widget, TrinketEffect trinketEffect)
		{
			widget.canvasGroup.alpha = 0.2f;
			widget.textDescription.color = this.effectDescColorWhenNoCopiesAndSelected;
			widget.textMaxLevel.color = this.effectMaxLevelsColorWhenNoCopies;
			widget.textDescription.text = trinketEffect.GetDescFirstWithoutColor();
		}

		public TrinketEffectOptionWidget trinketEffectOptionWidgetPrefab;

		[NonSerialized]
		public List<TrinketEffectOptionWidget> trinketEffectOptionWidgets;

		public TrinketCategoryHeader commonHeader;

		public TrinketCategoryHeader secondaryHeader;

		public TrinketCategoryHeader specialHeader;

		public const int maxItemToShow = 6;

		public ScrollRect scrollRect;

		public float startOffest = -10f;

		public float padding = -10f;

		public float distance = -123f;

		public float headerDisdance = -50f;

		private float lastVerticalPos;

		public GameButton buttonClose;

		public GameButton buttonSelect;

		public RectTransform buttonSelectParent;

		public Dictionary<int, int> trinketEffects;

		public GameButton buttonOkay;

		public Text textHeader;

		public Text selectGroupDesc;

		public Text dontHaveEffectTypeDesc;

		public RectTransform dontHaveEffectTypeParent;

		private int lastSelectedIndex;

		public TrinketEffect selectedTrinketEffect;

		public Action<TrinketEffect> onSelectTrinket;

		public RectTransform scrollDisenchantFlowChard;

		public Text infoFlowChartText;

		public RectTransform popupRect;

		public Color effectMaxLevelsColorWhenAtLeatOneCopy;

		public Color effectMaxLevelsColorWhenNoCopies;

		public Color effectDescColorWhenAtLeatOneCopy;

		public Color effectDescColorWhenNoCopies;

		public Color effectDescColorWhenNoCopiesAndSelected;

		public bool isDirty;

		private List<KeyValuePair<TrinketEffect, int>> currentGroup;
	}
}
