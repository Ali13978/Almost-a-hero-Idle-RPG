using System;

namespace Simulation
{
	public class RiftEffectLongerUltimateCD : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return -1.2f;
			}
		}

		public override void Apply(World world, float dt)
		{
			world.buffTotalEffect.heroUltiCooldownMaxFactor *= 1f + this.ultiCooldownMaxFactor;
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectLongerUltimateCD
			{
				ultiCooldownMaxFactor = this.ultiCooldownMaxFactor
			};
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_LONGER_COOLDOWNS"), GameMath.GetPercentString(this.ultiCooldownMaxFactor, false));
		}

		public float ultiCooldownMaxFactor = 1f;
	}
}
