using System;

namespace Simulation
{
	public class BuffDataDamageConsecutive : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			int genericCounter = buff.GetGenericCounter();
			if (genericCounter > this.maxStack)
			{
				genericCounter = this.maxStack;
			}
			totEffect.damageTEFactor += this.damageAdd * (double)genericCounter;
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			buff.IncreaseGenericCounter();
		}

		public override void OnAttackTargetChanged(Buff buff, UnitHealthy oldTarget, UnitHealthy newTarget)
		{
			buff.ZeroGenericCounter();
		}

		public double damageAdd;

		public int maxStack;
	}
}
