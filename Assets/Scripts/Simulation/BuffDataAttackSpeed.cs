using System;

namespace Simulation
{
	public class BuffDataAttackSpeed : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.attackSpeedAdd += this.attackSpeedAdd;
			totEffect.reloadSpeedAdd += this.reloadSpeedAdd;
		}

		public float attackSpeedAdd;

		public float reloadSpeedAdd;
	}
}
