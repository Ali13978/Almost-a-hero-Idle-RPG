using System;

namespace Simulation
{
	public class RiftEffectNoAbilityDamage : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return -1.8f;
			}
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectNoAbilityDamage();
		}

		public override string GetDesc()
		{
			return LM.Get("RIFT_EFFECT_NO_ABILITY_DAMAGE");
		}

		public override void OnPreDamage(World world, Unit damager, UnitHealthy damaged, Damage damage)
		{
			if (!(damager is Hero))
			{
				return;
			}
			if (damage.type == DamageType.SKILL)
			{
				damage.amount = 0.0;
			}
		}
	}
}
