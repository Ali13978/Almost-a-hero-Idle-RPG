using System;

namespace Simulation
{
	public class RiftEffectEveryoneDodges : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return -0.25f;
			}
		}

		public override void Apply(World world, float dt)
		{
			world.buffTotalEffect.allUnitsDodgeChanceAdd = this.dodgeFactor;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_EVERYONE_DODGES"), GameMath.GetPercentString(this.dodgeFactor, false));
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectEveryoneDodges();
		}

		public float dodgeFactor = 0.25f;
	}
}
