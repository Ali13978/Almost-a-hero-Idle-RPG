using System;

namespace Simulation
{
	public class RiftEffectCritChance : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 0.5f;
			}
		}

		public override void Apply(World world, float dt)
		{
			world.buffTotalEffect.critChanceAdd += this.critChanceAdd;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_CRIT_CHANCE"), GameMath.GetPercentString(this.critChanceAdd, false));
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectCritChance
			{
				critChanceAdd = this.critChanceAdd
			};
		}

		public float critChanceAdd = 0.5f;
	}
}
