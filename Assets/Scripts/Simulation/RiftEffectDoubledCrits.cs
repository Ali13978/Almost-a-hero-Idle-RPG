using System;

namespace Simulation
{
	public class RiftEffectDoubledCrits : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 0.25f;
			}
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectDoubledCrits();
		}

		public override string GetDesc()
		{
			return LM.Get("RIFT_EFFECT_DOUBLED_CRITICAL_DAMAGES");
		}

		public override void OnPreDamage(World world, Unit damager, UnitHealthy damaged, Damage damage)
		{
			if (damage.isCrit)
			{
				damage.amount *= 2.0;
			}
		}
	}
}
