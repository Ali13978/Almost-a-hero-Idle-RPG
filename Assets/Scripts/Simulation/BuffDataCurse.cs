using System;

namespace Simulation
{
	public class BuffDataCurse : BuffData
	{
		public BuffDataCurse(float chance, float duration, double damageTakeFactorAdd)
		{
			this.chance = chance;
			this.duration = duration;
			this.damageTakeFactorAdd = damageTakeFactorAdd;
		}

		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
			if (!unitHealthy.IsAlly(target) && GameMath.GetProbabilityOutcome(this.chance, GameMath.RandType.NoSeed))
			{
				BuffDataDefense buffDataDefense = new BuffDataDefense();
				buffDataDefense.isStackable = false;
				buffDataDefense.id = 254;
				buffDataDefense.damageTakenFactor = 1.0 + this.damageTakeFactorAdd;
				buffDataDefense.visuals |= 32;
				buffDataDefense.dur = this.duration;
				target.AddBuff(buffDataDefense, 0, false);
			}
		}

		public float chance;

		public float duration;

		public double damageTakeFactorAdd;
	}
}
