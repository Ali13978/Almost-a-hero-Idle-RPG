using System;

namespace Simulation
{
	public class BuffDataMeltdown : BuffData
	{
		public BuffDataMeltdown(double damageFactor, double cap)
		{
			this.damageFactor = damageFactor;
			this.cap = cap;
			this.id = 129;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			double num = this.damageFactor * (double)buff.GetGenericCounter();
			if (num > this.cap)
			{
				num = this.cap;
			}
			totEffect.damageAddFactor += num;
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			buff.IncreaseGenericCounter();
		}

		public override void OnAttackTargetChanged(Buff buff, UnitHealthy oldTarget, UnitHealthy newTarget)
		{
			buff.ZeroGenericCounter();
		}

		private double damageFactor;

		private double cap;
	}
}
