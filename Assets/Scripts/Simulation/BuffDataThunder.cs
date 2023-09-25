using System;

namespace Simulation
{
	public class BuffDataThunder : BuffData
	{
		public BuffDataThunder(double damageBonus, int chargeAdd)
		{
			this.damageBonus = damageBonus;
			this.chargeAdd = chargeAdd;
			this.id = 184;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.totemChargeReqAdd += this.chargeAdd;
		}

		public override void OnPreThunderbolt(Buff buff, UnitHealthy target, Damage damage, bool isSecondary)
		{
			if (isSecondary)
			{
				return;
			}
			damage.amount *= 1.0 + this.damageBonus;
		}

		private double damageBonus;

		private int chargeAdd;
	}
}
