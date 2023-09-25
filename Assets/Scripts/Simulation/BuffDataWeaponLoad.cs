using System;

namespace Simulation
{
	public class BuffDataWeaponLoad : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.weaponLoadAdd += this.weaponLoadAdd;
			totEffect.damageAddFactor += this.damageAdd;
		}

		public int weaponLoadAdd;

		public double damageAdd;
	}
}
