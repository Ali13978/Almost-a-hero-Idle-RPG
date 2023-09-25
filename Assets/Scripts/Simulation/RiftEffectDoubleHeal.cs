using System;

namespace Simulation
{
	public class RiftEffectDoubleHeal : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 0.8f;
			}
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectDoubleHeal();
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_DOUBLE_HEAL"), GameMath.GetPercentString(1.0, false));
		}

		public override void OnHeroPreHeal(Hero hero, ref double ratioHealed)
		{
			ratioHealed += ratioHealed * 1.0;
		}

		private const double increase = 1.0;
	}
}
