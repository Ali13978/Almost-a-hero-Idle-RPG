using System;

namespace Simulation
{
	public class BuffEventChangeWeaponToTemp : BuffEvent
	{
		public override void Apply(Unit by, World world)
		{
			if (!(by is Hero))
			{
				return;
			}
			Hero hero = (Hero)by;
			hero.ChangeWeaponToTemp(this.durChange, this.weapon, this.durChangeIfAlreadyUsingTempWeapon);
		}

		public float durChange;

		public Weapon weapon;

		public float durChangeIfAlreadyUsingTempWeapon = -1f;
	}
}
