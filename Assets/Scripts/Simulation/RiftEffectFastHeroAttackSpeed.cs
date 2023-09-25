using System;

namespace Simulation
{
	public class RiftEffectFastHeroAttackSpeed : RiftEffect
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
			return string.Format(LM.Get("RIFT_EFFECT_HERO_ATTACK_SPEED"), GameMath.GetPercentString(1f, false));
		}

		public override void Apply(World world, float dt)
		{
			world.buffTotalEffect.heroAttackSpeedAdd += 1f;
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectFastHeroAttackSpeed();
		}

		private const float increase = 1f;
	}
}
