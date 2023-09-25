using System;

namespace Simulation
{
	public class RiftEffectNoGoldDrop : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return -1.5f;
			}
		}

		public override void Apply(World world, float dt)
		{
			world.buffTotalEffect.noGoldDrop = true;
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectNoGoldDrop();
		}

		public override string GetDesc()
		{
			return LM.Get("RIFT_EFFECT_NO_GOLD_DROP");
		}
	}
}
