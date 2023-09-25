using System;
using Simulation;

namespace Ui
{
	public class UiCommandTrinketEquipUnequip : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryEquipUnequipTrinket(this.trinket, this.hero);
		}

		public Trinket trinket;

		public HeroDataBase hero;
	}
}
