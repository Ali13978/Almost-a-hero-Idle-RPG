using System;

namespace Simulation
{
	public class BuffDataDaze : BuffData
	{
		public BuffDataDaze(double damageTakenFactor)
		{
			this.damageTakenFactor = damageTakenFactor;
			this.id = 52;
		}

		public override void OnAfterThunderbolt(Buff buff, UnitHealthy target, Damage damage, bool isSecondary)
		{
			target.AddBuff(new BuffDataDefense
			{
				id = 53,
				dur = 5f,
				isStackable = false,
				damageTakenFactor = 1.0 + this.damageTakenFactor
			}, 0, false);
		}

		private double damageTakenFactor;
	}
}
