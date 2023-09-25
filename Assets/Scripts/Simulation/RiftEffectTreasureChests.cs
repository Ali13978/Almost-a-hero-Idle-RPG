using System;

namespace Simulation
{
	public class RiftEffectTreasureChests : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 3f;
			}
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectTreasureChests();
		}

		public override string GetDesc()
		{
			return LM.Get("RIFT_EFFECT_ENEMIES_ARE_TREASURE_CHESTS");
		}
	}
}
