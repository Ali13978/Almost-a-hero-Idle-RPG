using System;

namespace Simulation
{
	public class BuffDataBash : BuffData
	{
		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			this.bashJustHappened = GameMath.GetProbabilityOutcome(this.chance, GameMath.RandType.NoSeed);
			if (this.bashJustHappened)
			{
				damage.amount *= this.damageFactor;
			}
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (!this.bashJustHappened)
			{
				return;
			}
			target.AddBuff(this.effect, 0, false);
			this.bashJustHappened = false;
		}

		public float chance;

		public BuffDataStun effect;

		public double damageFactor;

		private bool bashJustHappened;
	}
}
