using System;

namespace Simulation
{
	public class BuffDataMark : BuffData
	{
		public BuffDataMark(float chance, double damageBonus)
		{
			this.chance = chance;
			this.effect = new BuffDataMarked(damageBonus, 1);
			this.effect.id = 126;
			this.effect.dur = float.PositiveInfinity;
			this.effect.visuals |= 1024;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (GameMath.GetProbabilityOutcome(this.chance, GameMath.RandType.NoSeed))
			{
				target.AddBuff(this.effect, 0, false);
			}
		}

		private float chance;

		private BuffDataMarked effect;
	}
}
