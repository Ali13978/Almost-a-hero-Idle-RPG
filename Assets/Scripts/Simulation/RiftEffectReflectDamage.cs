using System;

namespace Simulation
{
	public class RiftEffectReflectDamage : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 0f;
			}
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_REFLECT"), GameMath.GetPercentString(this.reflectFactor, false));
		}

		public override void OnPostDamage(World world, Unit damager, UnitHealthy damaged, Damage damage)
		{
			if (!(damager is UnitHealthy))
			{
				return;
			}
			UnitHealthy damaged2 = damager as UnitHealthy;
			Damage copy = damage.GetCopy();
			copy.amount *= this.reflectFactor;
			if (copy.blockFactor > 0.0)
			{
				copy.amount *= 1.0 / damage.blockFactor;
			}
			world.DamageMain(null, damaged2, copy);
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectReflectDamage
			{
				reflectFactor = this.reflectFactor
			};
		}

		public double reflectFactor = 0.5;
	}
}
