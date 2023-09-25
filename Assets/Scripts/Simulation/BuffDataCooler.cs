using System;

namespace Simulation
{
	public class BuffDataCooler : BuffData
	{
		public BuffDataCooler(float coolFactor, float overCoolFactor)
		{
			this.coolFactor = coolFactor;
			this.overCoolFactor = overCoolFactor;
			this.id = 29;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.totemCoolFactor -= this.coolFactor;
			totEffect.totemOverCoolFactor += this.overCoolFactor;
		}

		private float coolFactor;

		private float overCoolFactor;
	}
}
