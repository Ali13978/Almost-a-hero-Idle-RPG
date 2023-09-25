using System;

namespace Simulation
{
	public class RiftEffectBoss : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return -0.5f;
			}
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectBoss();
		}

		public override string GetDesc()
		{
			return LM.Get("RIFT_EFFECT_BOSS");
		}
	}
}
