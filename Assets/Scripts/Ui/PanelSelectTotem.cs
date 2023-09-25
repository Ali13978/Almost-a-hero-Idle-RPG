using System;
using System.Collections.Generic;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelSelectTotem : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			int i = 0;
			int num = this.buttonTotems.Length;
			while (i < num)
			{
				int ic = i;
				this.buttonTotems[i].gameButton.onClick = delegate()
				{
					this.OnClickedButtonSelectTotem(ic);
				};
				i++;
			}
			this.InitStrings();
		}

		public void InitStrings()
		{
			this.textTitle.text = LM.Get("UI_SELECT_RING_HEADER");
			this.buttonSelectTotem.text.text = LM.Get("UI_SELECT");
			this.textNotSelected.text = LM.Get("UI_SELECT_RING_DESC");
		}

		public void SetTotems(List<TotemDataBase> allTotems, List<bool> allTotemLockeds, Dictionary<string, Sprite> spritesTotem, Dictionary<string, Sprite> spritesTotemSelected, Dictionary<TotemDataBase, Sprite> boughtTotems)
		{
			int i = 0;
			int num = this.buttonTotems.Length;
			while (i < num)
			{
				ButtonSelectTotem buttonSelectTotem = this.buttonTotems[i];
				buttonSelectTotem.rectTransform.localScale = Vector3.one * ((this.selected != i) ? 1f : 1.1f);
				bool flag = allTotems.Count > i;
				if (flag)
				{
					string id = allTotems[i].id;
					buttonSelectTotem.totem = allTotems[i];
					buttonSelectTotem.spriteIcon = ((!spritesTotem.ContainsKey(id)) ? null : spritesTotem[id]);
					buttonSelectTotem.locked = allTotemLockeds[i];
					buttonSelectTotem.gameButton.interactable = (!buttonSelectTotem.locked && !boughtTotems.ContainsKey(allTotems[i]));
					if (boughtTotems.ContainsKey(allTotems[i]))
					{
						buttonSelectTotem.imageModeFlag.sprite = boughtTotems[allTotems[i]];
						buttonSelectTotem.imageModeFlag.gameObject.SetActive(true);
						buttonSelectTotem.imageIcon.color = new Color(1f, 1f, 1f, 0.6f);
					}
					else
					{
						buttonSelectTotem.imageModeFlag.gameObject.SetActive(false);
						buttonSelectTotem.imageIcon.color = new Color(1f, 1f, 1f, 1f);
					}
				}
				else
				{
					buttonSelectTotem.totem = null;
					buttonSelectTotem.gameButton.interactable = false;
					buttonSelectTotem.locked = true;
					buttonSelectTotem.imageModeFlag.gameObject.SetActive(false);
				}
				i++;
			}
			if (this.selected == -1)
			{
				this.selectedBg.SetActive(false);
				this.notSelectedBg.SetActive(true);
			}
			else
			{
				this.selectedBg.SetActive(true);
				this.notSelectedBg.SetActive(false);
				TotemDataBase totemDataBase = allTotems[this.selected];
				string id2 = totemDataBase.id;
				this.textName.text = totemDataBase.GetName();
				this.textDesc.text = totemDataBase.GetDesc();
				this.imageSelectedTotem.sprite = ((!spritesTotemSelected.ContainsKey(id2)) ? null : spritesTotemSelected[id2]);
			}
		}

		public void OnClickedButtonSelectTotem(int index)
		{
			UiManager.stateJustChanged = true;
			if (this.selected == index)
			{
				this.selected = -1;
			}
			else
			{
				this.selected = index;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiRingSelect, 1f));
			}
		}

		public int selected;

		public ButtonSelectTotem[] buttonTotems;

		public Image imageSelectedTotem;

		public GameObject selectedBg;

		public GameObject notSelectedBg;

		public Image imageName;

		public Text textTitle;

		public Text textName;

		public Text textDesc;

		public Text textNotSelected;

		public GameButton buttonSelectTotem;

		public GameButton[] buttonCloses;

		public ScrollRect ringScroll;

		public RectTransform popupRect;
	}
}
