using System;

namespace Simulation
{
	public class BuffEventChangeWeaponToOrig : BuffEvent
	{
		public override void Apply(Unit by, World world)
		{
			if (!(by is Hero))
			{
				return;
			}
			Hero hero = (Hero)by;
			hero.ChangeWeaponToOrig(this.durChange);
		}

		public float durChange;
	}
}
