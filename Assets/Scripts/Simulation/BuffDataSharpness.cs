using System;

namespace Simulation
{
	public class BuffDataSharpness : BuffData
	{
		public BuffDataSharpness(double damage, double cap)
		{
			this.damage = damage;
			this.cap = cap;
			this.id = 158;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			double num = (double)buff.GetGenericCounter() * this.damage;
			if (num > this.cap)
			{
				num = this.cap;
			}
			totEffect.damageAreaFactor += num;
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			buff.IncreaseGenericCounter();
		}

		public override void OnAfterIceShardRain(Buff buff)
		{
			buff.ZeroGenericCounter();
		}

		private double damage;

		private double cap;
	}
}
