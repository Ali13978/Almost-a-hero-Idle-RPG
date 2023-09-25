using System;
using System.Collections.Generic;
using Simulation;
using Ui;
using UnityEngine;
using UnityEngine.UI;

public class TrinketDebug : MonoBehaviour
{
	public void Init(UiManager manager)
	{
		this.questDropdown.ClearOptions();
		this.commonDropdown.ClearOptions();
		this.secondaryDropdown.ClearOptions();
		this.specialDropdown.ClearOptions();
		foreach (TrinketUpgradeReq trinketUpgradeReq in Trinket.allReqs)
		{
			this.questDropdown.AddOptions(new List<Dropdown.OptionData>
			{
				new Dropdown.OptionData
				{
					text = trinketUpgradeReq.GetDebugName()
				}
			});
		}
		this.commonDropdown.AddOptions(new List<string>
		{
			"none"
		});
		foreach (TrinketEffect trinketEffect in Trinket.commonEffects)
		{
			this.commonDropdown.AddOptions(new List<Dropdown.OptionData>
			{
				new Dropdown.OptionData
				{
					text = trinketEffect.GetDebugName()
				}
			});
		}
		this.secondaryDropdown.AddOptions(new List<string>
		{
			"none"
		});
		foreach (TrinketEffect trinketEffect2 in Trinket.secondaryEffects)
		{
			this.secondaryDropdown.AddOptions(new List<Dropdown.OptionData>
			{
				new Dropdown.OptionData
				{
					text = trinketEffect2.GetDebugName()
				}
			});
		}
		this.specialDropdown.AddOptions(new List<string>
		{
			"none"
		});
		foreach (TrinketEffect trinketEffect3 in Trinket.specialEffects)
		{
			this.specialDropdown.AddOptions(new List<Dropdown.OptionData>
			{
				new Dropdown.OptionData
				{
					text = trinketEffect3.GetDebugName()
				}
			});
		}
		for (int i = 0; i < Trinket.commonEffects.Count; i++)
		{
		}
		this.trinketUi.gameObject.SetActive(false);
		this.createButton.onClick.AddListener(delegate()
		{
			TrinketUpgradeReq reqWithDebugName = Trinket.GetReqWithDebugName(this.questDropdown.options[this.questDropdown.value].text);
			TrinketEffect effectWithDebugName = Trinket.GetEffectWithDebugName(this.commonDropdown.options[this.commonDropdown.value].text);
			TrinketEffect effectWithDebugName2 = Trinket.GetEffectWithDebugName(this.secondaryDropdown.options[this.secondaryDropdown.value].text);
			TrinketEffect effectWithDebugName3 = Trinket.GetEffectWithDebugName(this.specialDropdown.options[this.specialDropdown.value].text);
			manager.Debug_CreateNewTrinket(reqWithDebugName, effectWithDebugName, effectWithDebugName2, effectWithDebugName3);
			List<TrinketEffect> list = new List<TrinketEffect>();
			if (effectWithDebugName != null)
			{
				list.Add(effectWithDebugName);
			}
			if (effectWithDebugName2 != null)
			{
				list.Add(effectWithDebugName2);
			}
			if (effectWithDebugName3 != null)
			{
				list.Add(effectWithDebugName3);
			}
			Trinket simTrinket = new Trinket(list, reqWithDebugName);
			this.trinketUi.gameObject.SetActive(true);
			this.trinketUi.Init(simTrinket);
		});
		this.randomButton.onClick.AddListener(delegate()
		{
			Trinket random = Trinket.GetRandom(1);
			this.questDropdown.value = 0;
			this.commonDropdown.value = 0;
			this.secondaryDropdown.value = 0;
			this.specialDropdown.value = 0;
			for (int j = 0; j < this.commonDropdown.options.Count; j++)
			{
				foreach (TrinketEffect trinketEffect4 in random.effects)
				{
					if (trinketEffect4.GetDebugName() == this.commonDropdown.options[j].text)
					{
						this.commonDropdown.value = j;
						break;
					}
				}
			}
			for (int k = 0; k < this.secondaryDropdown.options.Count; k++)
			{
				foreach (TrinketEffect trinketEffect5 in random.effects)
				{
					if (trinketEffect5.GetDebugName() == this.secondaryDropdown.options[k].text)
					{
						this.secondaryDropdown.value = k;
						break;
					}
				}
			}
			for (int l = 0; l < this.specialDropdown.options.Count; l++)
			{
				foreach (TrinketEffect trinketEffect6 in random.effects)
				{
					if (trinketEffect6.GetDebugName() == this.specialDropdown.options[l].text)
					{
						this.specialDropdown.value = l;
						break;
					}
				}
			}
			this.trinketUi.gameObject.SetActive(true);
			this.trinketUi.Init(random);
		});
	}

	public Dropdown questDropdown;

	public Dropdown commonDropdown;

	public Dropdown secondaryDropdown;

	public Dropdown specialDropdown;

	public Button createButton;

	public Button randomButton;

	public TrinketUi trinketUi;
}
