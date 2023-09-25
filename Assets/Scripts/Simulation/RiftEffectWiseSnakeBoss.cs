using System;

namespace Simulation
{
	public class RiftEffectWiseSnakeBoss : RiftEffectBoss
	{
		public override RiftEffect Clone()
		{
			return new RiftEffectWiseSnakeBoss();
		}

		public override string GetDesc()
		{
			return LM.Get("RIFT_EFFECT_WISE_SNAKE_BOSS");
		}
	}
}
