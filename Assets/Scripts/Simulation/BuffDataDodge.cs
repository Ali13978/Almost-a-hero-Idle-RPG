using System;

namespace Simulation
{
	public class BuffDataDodge : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.dodgeChanceAdd += this.dodgeChanceAdd;
		}

		public override void OnDodged(Buff buff, Unit attacker, Damage damage)
		{
			UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
			if (unitHealthy.IsAlly(attacker))
			{
				return;
			}
			if (buff.GetBy().GetId() == "HORATIO")
			{
				buff.GetWorld().OnHiltDodge();
			}
		}

		public float dodgeChanceAdd;
	}
}
