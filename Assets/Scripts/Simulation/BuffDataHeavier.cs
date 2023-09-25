using System;

namespace Simulation
{
	public class BuffDataHeavier : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.damageAddFactor += this.damageFactorAdd;
			totEffect.totemChargeReqAdd += this.chargeReqAdd;
		}

		public double damageFactorAdd;

		public int chargeReqAdd;
	}
}
