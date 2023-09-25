using System;

namespace Simulation
{
	public class CharmBuffProtectiveWard : CharmBuffPermanent
	{
		protected override void OnUpdate(float dt)
		{
			this.stateTime += dt;
			this.world.buffTotalEffect.heroDamageTakenFactor *= (double)(1f - this.damageProtectionFactor);
		}

		public float damageProtectionFactor;
	}
}
