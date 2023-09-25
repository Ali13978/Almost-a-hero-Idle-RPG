using System;

namespace Simulation
{
	public class RiftEffectFastEnemies : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return -1f;
			}
		}

		public override void Apply(World world, float dt)
		{
			world.buffTotalEffect.enemyAttackSpeedAdd += this.attackSpeedAdd;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_FAST_ENEMIES"), GameMath.GetPercentString(this.attackSpeedAdd, false));
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectFastEnemies
			{
				attackSpeedAdd = this.attackSpeedAdd
			};
		}

		public float attackSpeedAdd = 2f;
	}
}
