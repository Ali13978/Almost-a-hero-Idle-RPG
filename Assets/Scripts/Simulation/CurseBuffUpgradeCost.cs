using System;

namespace Simulation
{
	public class CurseBuffUpgradeCost : CurseBuff
	{
		public override void Update(float dt)
		{
			base.Update(dt);
			this.world.buffTotalEffect.upgradeCostFactor *= this.upgradeCostFactor;
		}

		public override void OnPreDamage(Unit damager, UnitHealthy damaged, Damage damage)
		{
			if (!(damager is Enemy))
			{
				return;
			}
			this.AddProgress(this.pic);
		}

		public double upgradeCostFactor;
	}
}
