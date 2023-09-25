using System;
using System.Collections.Generic;
using Simulation;

namespace Ui
{
	public class UiCommandTrinketDestroy : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			if (this.trinket == null)
			{
				sim.TryDestroyTrinkets(this.trinkets, this.currencyType, this.dropPositions);
				this.panelTrinketsScroller.ClearMultipleTrinketsSelection();
			}
			else
			{
				sim.TryDestroyTrinket(this.trinket, this.currencyType, this.dropPos);
			}
		}

		public Trinket trinket;

		public List<Trinket> trinkets;

		public DropPosition dropPos;

		public List<DropPosition> dropPositions;

		public CurrencyType currencyType;

		public PanelTrinketsScroller panelTrinketsScroller;
	}
}
