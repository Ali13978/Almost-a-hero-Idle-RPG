using System;
using System.Collections.Generic;
using Simulation;

namespace Ui
{
	public class UiCommandTrinketDisassemble : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			if (this.trinket == null)
			{
				sim.TryDisassembleTrinkets(this.trinkets, this.currencyType, this.dropPositions);
			}
			else
			{
				sim.TryDisassembleTrinket(this.trinket, this.currencyType, this.dropPos);
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
