using System;

namespace Simulation
{
	public class RiftEffectRegeneration : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 0.3f;
			}
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_REGEN"), GameMath.GetPercentString(this.regenAmount, false));
		}

		public override void Apply(World world, float dt)
		{
			world.buffTotalEffect.heroesRegenAdd += this.regenAmount;
			world.buffTotalEffect.enemiesRegenAdd += this.regenAmount;
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectRegeneration
			{
				regenAmount = this.regenAmount
			};
		}

		public double regenAmount = 0.02;
	}
}
