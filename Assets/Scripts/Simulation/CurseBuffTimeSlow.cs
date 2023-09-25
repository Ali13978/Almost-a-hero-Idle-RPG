using System;

namespace Simulation
{
	public class CurseBuffTimeSlow : CurseBuff
	{
		protected override void OnUpdate(float dt)
		{
			if (this.world.activeChallenge.state == Challenge.State.ACTION)
			{
				this.world.buffTotalEffect.timeUpdateFactor *= this.timeFactor;
			}
		}

		public override void OnPostDamage(Unit damager, UnitHealthy damaged, Damage damage)
		{
			if (damager is Hero && damage.isCrit)
			{
				this.AddProgress(this.pic);
			}
		}

		public float timeFactor;
	}
}
