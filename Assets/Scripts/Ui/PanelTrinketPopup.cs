using System;
using System.Collections.Generic;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelTrinketPopup : AahMonoBehaviour
	{
		public void SetTransfer(HeroDataBase heroFrom, HeroDataBase heroTo, Trinket trinket, Dictionary<string, Sprite> spritesHeroPortrait)
		{
			this.menuShowCurrency.gameObject.SetActive(false);
			this.menuShowCurrencyTop.gameObject.SetActive(false);
			this.parentTransfer.gameObject.SetActive(true);
			this.heroPortraits[0].SetHero(spritesHeroPortrait[heroFrom.id], heroFrom.evolveLevel, false, -5f, false);
			this.heroPortraits[1].SetHero(spritesHeroPortrait[heroTo.id], heroTo.evolveLevel, false, -5f, false);
			this.trinketUi.Init(trinket);
			this.textDescs[0].text = LM.Get("TRINKET_TRANSFER_DESC0");
			this.textDescs[1].text = LM.Get("TRINKET_TRANSFER_DESC1");
		}

		public void SetDestroy(Trinket trinket, string scrapsAmount)
		{
			this.menuShowCurrency.gameObject.SetActive(true);
			this.menuShowCurrencyTop.gameObject.SetActive(true);
			this.parentTransfer.gameObject.SetActive(false);
			this.menuShowCurrencyTop.SetCurrency(CurrencyType.SCRAP, scrapsAmount, true, GameMode.STANDARD, true);
			this.menuShowCurrency.SetCurrency(CurrencyType.SCRAP, GameMath.GetDoubleString(trinket.GetDestroyReward()), true, GameMode.STANDARD, true);
			this.textDescs[0].text = LM.Get("TRINKET_DESTROY_DESC0");
			this.textDescs[1].text = LM.Get("TRINKET_DESTROY_DESC1");
		}

		public MenuShowCurrency menuShowCurrency;

		public MenuShowCurrency menuShowCurrencyTop;

		public HeroPortrait[] heroPortraits;

		public TrinketUi trinketUi;

		public GameObject parentTransfer;

		public Text[] textDescs;

		internal UiState stateToReturn;
	}
}
