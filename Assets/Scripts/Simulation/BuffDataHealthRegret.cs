using System;

namespace Simulation
{
	public class BuffDataHealthRegret : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			if (buff.GetBy() is UnitHealthy)
			{
				UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
				Damage damage = new Damage(unitHealthy.GetHealthMax() * this.damagePer, false, false, false, false);
				damage.isPure = true;
				damage.isExact = true;
				damage.ignoreShield = true;
				damage.ignoreReduction = true;
				double healthRatio = unitHealthy.GetHealthRatio();
				unitHealthy.TakeDamage(damage, null, 0.001);
				double num = healthRatio - unitHealthy.GetHealthRatio();
				buff.DecreaseLifeCounter();
				BuffDataHealthRegen buffDataHealthRegen = new BuffDataHealthRegen();
				buffDataHealthRegen.id = 99;
				buffDataHealthRegen.isStackable = true;
				buffDataHealthRegen.dur = this.healDur;
				buffDataHealthRegen.healthRegenAdd = num / (double)this.healDur;
				buffDataHealthRegen.visuals |= 64;
				buff.GetBy().AddBuff(buffDataHealthRegen, 0, false);
			}
		}

		public double damagePer;

		public float healDur;
	}
}
