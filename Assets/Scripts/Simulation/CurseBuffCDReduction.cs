using System;

namespace Simulation
{
	public class CurseBuffCDReduction : CurseBuff
	{
		protected override void OnUpdate(float dt)
		{
			this.world.buffTotalEffect.heroUltiCoolFactor *= this.cooldownFactor;
		}

		public override void OnPostDamage(Unit damager, UnitHealthy damaged, Damage damage)
		{
			if (damager is Hero)
			{
				this.AddProgress(this.pic);
			}
		}

		public float cooldownFactor;
	}
}
