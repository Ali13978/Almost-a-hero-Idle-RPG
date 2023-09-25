using System;

namespace Simulation
{
	public class BuffDataReloadSpeed : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.reloadSpeedAdd += this.amount;
			totEffect.weaponLoadAdd += this.loadAdd;
		}

		public float amount;

		public int loadAdd;
	}
}
