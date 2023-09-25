using System;

namespace Simulation
{
	public class BuffDataEnchantEarth : BuffData
	{
		public BuffDataEnchantEarth(float durationAdd, double multNextAttack)
		{
			this.durationAdd = durationAdd;
			this.multNextAttack = multNextAttack;
			this.id = 73;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.totemEarthDurationAdd += this.durationAdd;
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			BuffDataDefenseCounted buffDataDefenseCounted = new BuffDataDefenseCounted(this.multNextAttack + 1.0, 1);
			buffDataDefenseCounted.id = 63;
			buffDataDefenseCounted.visuals |= 32;
			buffDataDefenseCounted.dur = float.PositiveInfinity;
			buffDataDefenseCounted.isStackable = false;
			target.AddBuff(buffDataDefenseCounted, 0, false);
		}

		private double multNextAttack;

		private float durationAdd;
	}
}
