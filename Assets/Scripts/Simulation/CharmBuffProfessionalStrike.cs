using System;

namespace Simulation
{
	public class CharmBuffProfessionalStrike : CharmBuffPermanent
	{
		protected override void OnUpdate(float dt)
		{
			this.stateTime += dt;
			this.world.buffTotalEffect.critChanceAdd = this.critFactorIncrease;
		}

		public float critFactorIncrease;
	}
}
