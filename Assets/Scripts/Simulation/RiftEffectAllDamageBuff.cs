using System;

namespace Simulation
{
	public class RiftEffectAllDamageBuff : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 0.5f;
			}
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_ALL_DAMAGE_BUFF"), GameMath.GetPercentString(this.damageBuff, false));
		}

		public override void Apply(World world, float dt)
		{
			world.buffTotalEffect.heroDamageFactor *= this.damageBuff;
			world.buffTotalEffect.enemyDamageFactor *= this.damageBuff;
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectAllDamageBuff
			{
				damageBuff = this.damageBuff
			};
		}

		public double damageBuff = 5.0;
	}
}
