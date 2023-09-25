using System;

namespace Simulation
{
	public class CharmBuffTimeWarper : CharmBuffPermanent
	{
		protected override void OnUpdate(float dt)
		{
			this.stateTime += dt;
			this.world.buffTotalEffect.timeUpdateFactor *= this.timeFactor;
		}

		public float timeFactor;
	}
}
