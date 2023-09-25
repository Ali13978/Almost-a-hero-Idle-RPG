using System;

namespace Simulation
{
	public class RiftEffectHalfHeal : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return -0.25f;
			}
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectHalfHeal();
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_HALF_HEAL"), GameMath.GetPercentString(0.5, false));
		}

		public override void OnHeroPreHeal(Hero hero, ref double ratioHealed)
		{
			ratioHealed *= 0.5;
		}

		private const double reduction = 0.5;
	}
}
