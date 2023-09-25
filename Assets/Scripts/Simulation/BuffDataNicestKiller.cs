using System;

namespace Simulation
{
	public class BuffDataNicestKiller : BuffData
	{
		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (damage.isCrit)
			{
				this.counter++;
				if (this.counter == this.numCritsRequired)
				{
					this.counter = 0;
					foreach (Hero hero in buff.GetWorld().heroes)
					{
						hero.AddBuff(new BuffDataDamageAdd
						{
							id = 38,
							isStackable = true,
							dur = this.effectDuration,
							damageAdd = this.effectDamageAdd
						}, 0, false);
					}
				}
			}
		}

		public int numCritsRequired;

		public int counter;

		public float effectDuration;

		public double effectDamageAdd;
	}
}
