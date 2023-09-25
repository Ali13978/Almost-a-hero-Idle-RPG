using System;
using Render;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class DropCurrency : Drop
	{
		public DropCurrency(CurrencyType type, double amount, World world, bool isCapped = false)
		{
			this.currencyType = type;
			this.amount = amount;
			this.isCapped = isCapped;
			switch (type)
			{
			case CurrencyType.GOLD:
				this.invPos = RenderManager.POS_GOLD_INV_DEFAULT;
				this.targetRotation = 80f;
				break;
			case CurrencyType.SCRAP:
			case CurrencyType.MYTHSTONE:
			case CurrencyType.GEM:
			case CurrencyType.TOKEN:
			case CurrencyType.AEON:
			{
				PanelCurrencySide panelCurrencySide = UiManager.SetPanelCurrencySide(type, string.Empty);
				if (panelCurrencySide != null)
				{
					this.invPos = panelCurrencySide.currencyFinalPosReference.position;
					this.targetToScaleOnReach = panelCurrencySide.panelCurrency.GetCurrencyTransform();
				}
				else
				{
					this.invPos = new Vector3(1.1f, 1f);
					this.targetToScaleOnReach = null;
				}
				break;
			}
			case CurrencyType.CANDY:
				if (world.currentSim.IsChristmasTreeEnabled())
				{
					this.invPos = RenderManager.POS_CURRENCY_OFFER_BUTTON;
				}
				else if (world.hasValidCandyQuest)
				{
					this.invPos = RenderManager.POS_CURRENCY_DAILY_QUEST;
				}
				else
				{
					this.invPos = RenderManager.POS_GOLD_INV_DEFAULT;
				}
				break;
			case CurrencyType.TRINKET_BOX:
				this.invPos = RenderManager.POS_CURRENCY_SHOP;
				break;
			}
		}

		public override void Apply(World world)
		{
			switch (this.currencyType)
			{
			case CurrencyType.GOLD:
				world.AddGold(this.amount);
				UiManager.imageGoldWorldToAnim = world;
				break;
			case CurrencyType.SCRAP:
				world.AddScrap(this.amount);
				break;
			case CurrencyType.MYTHSTONE:
				world.AddMythstone(this.amount);
				break;
			case CurrencyType.GEM:
				world.AddCredit(this.amount);
				break;
			case CurrencyType.TOKEN:
				world.AddToken(this.amount);
				break;
			case CurrencyType.AEON:
				world.AddAeon(this.amount);
				break;
			case CurrencyType.CANDY:
				if (this.isCapped)
				{
					world.AddCandyCapped(this.amount);
				}
				else
				{
					world.AddCandy(this.amount);
				}
				break;
			case CurrencyType.TRINKET_BOX:
				world.AddTrinketBox((int)this.amount);
				break;
			default:
				throw new Exception();
			}
		}

		public CurrencyType currencyType;

		public double amount;

		public bool isCapped;
	}
}
