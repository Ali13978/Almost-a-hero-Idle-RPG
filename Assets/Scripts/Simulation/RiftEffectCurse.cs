using System;

namespace Simulation
{
	public class RiftEffectCurse : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 0f;
			}
		}

		public override string GetDesc()
		{
			return LM.Get("RIFT_EFFECT_CURSED_GATE");
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectCurse();
		}
	}
}
