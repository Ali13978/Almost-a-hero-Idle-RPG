using System;

namespace Simulation
{
	public class BuffDataIgnition : BuffData
	{
		public BuffDataIgnition(float damageInc, float heatInc)
		{
			this.damageInc = damageInc;
			this.heatInc = heatInc;
			this.id = 107;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			Unit by = buff.GetBy();
			if (!(by is TotemFire))
			{
				return;
			}
			TotemFire totemFire = (TotemFire)by;
			totEffect.damageAddFactor += (double)(totemFire.heat / totemFire.GetHeatMax() * this.damageInc);
			totEffect.totemHeatFactor += this.heatInc;
		}

		private float damageInc;

		private float heatInc;
	}
}
