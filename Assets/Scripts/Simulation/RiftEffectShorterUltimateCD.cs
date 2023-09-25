using System;

namespace Simulation
{
	public class RiftEffectShorterUltimateCD : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 1f;
			}
		}

		public override void Apply(World world, float dt)
		{
			world.buffTotalEffect.heroUltiCooldownMaxFactor *= this.ultiCooldownMaxFactor;
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectShorterUltimateCD
			{
				ultiCooldownMaxFactor = this.ultiCooldownMaxFactor
			};
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_SHORTER_COOLDOWNS"), GameMath.GetPercentString(1f - this.ultiCooldownMaxFactor, false));
		}

		public float ultiCooldownMaxFactor = 0.1f;
	}
}
