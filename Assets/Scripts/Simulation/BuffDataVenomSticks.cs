using System;

namespace Simulation
{
	public class BuffDataVenomSticks : BuffData
	{
		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (!GameMath.GetProbabilityOutcome(this.chance, GameMath.RandType.NoSeed))
			{
				return;
			}
			target.AddBuff(this.effect, 0, false);
		}

		public float chance;

		public BuffDataAttackSpeed effect;
	}
}
