using System;

namespace Simulation
{
	public class CharmBuffPowerOverwhelming : CharmBuff
	{
		protected override bool TryActivating()
		{
			foreach (Hero hero in this.world.heroes)
			{
				if (!hero.IsDead())
				{
					BuffDataDamageAdd buffDataDamageAdd = new BuffDataDamageAdd();
					buffDataDamageAdd.id = 311;
					buffDataDamageAdd.dur = this.dur;
					buffDataDamageAdd.isStackable = true;
					buffDataDamageAdd.damageAdd = this.damageIncrease;
					buffDataDamageAdd.visuals |= 8;
					hero.AddBuff(buffDataDamageAdd, 0, false);
				}
			}
			return true;
		}

		protected override void OnUpdate(float dt)
		{
			this.AddProgress(this.pic * dt);
		}

		public double damageIncrease;

		public float dur;
	}
}
