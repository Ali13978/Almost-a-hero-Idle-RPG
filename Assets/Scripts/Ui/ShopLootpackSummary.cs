using System;
using System.Collections.Generic;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ShopLootpackSummary : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.InitStrings();
		}

		public void InitStrings()
		{
			this.buttonDone.text.text = LM.Get("UI_OKAY");
			this.hintLabel.text = LM.Get("UI_GEAR_GLOBAL_HINT");
		}

		public void SetPack(ShopPack shopPack)
		{
			this.textBanner.text = shopPack.GetName();
		}

		public Sprite spriteBannerStandard;

		public Sprite spriteBannerRare;

		public Sprite spriteBannerEpic;

		public Image imageBanner;

		public Text textBanner;

		public List<PanelLootItem> panelLootItems;

		public List<PanelLootItem> panelLootItemsBig;

		public RectTransform popupRect;

		public RectTransform buttonRect;

		public GameButton buttonDone;

		public Image openingBackground;

		public ChestSpine chestSpine;

		public float periodOpeningFadeOut;

		public float timer;

		public GameObject hintParent;

		public Text hintLabel;
	}
}
