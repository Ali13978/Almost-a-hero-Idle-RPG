using System;

namespace Simulation
{
	public class BuffDataHealOnConsHit : BuffData
	{
		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			this.counter++;
			if (this.counter >= this.consCount)
			{
				this.counter = 0;
				Unit by = buff.GetBy();
				if (!(by is UnitHealthy))
				{
					return;
				}
				UnitHealthy unitHealthy = (UnitHealthy)by;
				if (unitHealthy.IsAlive())
				{
					unitHealthy.AddBuff(new BuffDataHealthRegen
					{
						dur = 0.8f,
						visuals = 64,
						healthRegenAdd = 0.0
					}, 0, false);
					unitHealthy.Heal(this.healRatio);
					unitHealthy.world.OnLiaStealHealth();
				}
			}
		}

		public override void OnMissed(Buff buff, UnitHealthy target, Damage damage)
		{
			this.counter = -1;
		}

		public int consCount;

		public double healRatio;

		private int counter;
	}
}
