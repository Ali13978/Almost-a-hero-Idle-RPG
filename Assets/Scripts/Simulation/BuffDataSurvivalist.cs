using System;

namespace Simulation
{
	public class BuffDataSurvivalist : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.damageTakenFactor *= 1.0 - this.damageReduction;
		}

		public override void OnTakenDamage(Buff buff, Unit attacker, Damage damage)
		{
			if (!(attacker is Enemy))
			{
				return;
			}
			BuffDataMissChanceAdd buffDataMissChanceAdd = new BuffDataMissChanceAdd();
			buffDataMissChanceAdd.id = 357;
			buffDataMissChanceAdd.isPermenant = false;
			buffDataMissChanceAdd.isStackable = true;
			buffDataMissChanceAdd.dur = this.addedBuffDur;
			buffDataMissChanceAdd.missChanceAdd = -this.missChance;
			buff.GetBy().AddBuff(buffDataMissChanceAdd, 0, false);
		}

		public float missChance;

		public double damageReduction;

		public float addedBuffDur;
	}
}
