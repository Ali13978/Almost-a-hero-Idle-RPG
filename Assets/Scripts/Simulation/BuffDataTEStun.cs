using System;

namespace Simulation
{
	public class BuffDataTEStun : BuffData
	{
		public BuffDataTEStun(float chance, float stunDur, double damageAdd)
		{
			this.chance = chance;
			this.stunDur = stunDur;
			this.damageAdd = damageAdd;
		}

		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
			if (!unitHealthy.IsAlly(target) && GameMath.GetProbabilityOutcome(this.chance, GameMath.RandType.NoSeed) && !target.IsInvulnerable())
			{
				damage.amount *= 1.0 + this.damageAdd;
				BuffDataStun buffDataStun = new BuffDataStun();
				buffDataStun.id = 175;
				buffDataStun.visuals |= 512;
				buffDataStun.dur = this.stunDur;
				target.AddBuff(buffDataStun, 0, false);
			}
		}

		public float chance;

		public float stunDur;

		public double damageAdd;
	}
}
