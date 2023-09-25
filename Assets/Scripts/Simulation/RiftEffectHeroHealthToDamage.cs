using System;

namespace Simulation
{
	public class RiftEffectHeroHealthToDamage : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 1f;
			}
		}

		public override void OnPreDamage(World world, Unit damager, UnitHealthy damaged, Damage damage)
		{
			Hero hero = damager as Hero;
			if (hero == null)
			{
				return;
			}
			double num = hero.GetHealth() * this.percentage;
			damage.amount += num;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_HERO_HP_TO_DMG"), GameMath.GetPercentString(this.percentage, false));
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectHeroHealthToDamage
			{
				percentage = this.percentage
			};
		}

		public double percentage = 0.1;
	}
}
