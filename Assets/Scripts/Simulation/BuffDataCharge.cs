using System;

namespace Simulation
{
	public class BuffDataCharge : BuffData
	{
		public BuffDataCharge(int chargeRed)
		{
			this.chargeRed = chargeRed;
			this.id = 24;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.totemChargeReqAdd -= this.chargeRed;
		}

		private int chargeRed;
	}
}
