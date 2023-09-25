using System;

namespace Simulation
{
	public class RiftEffectTimeDealsDamage : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return -0.5f;
			}
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_TIME_DEALS_DAMAGE"), GameMath.GetPercentString(0.0099999997764825821, false), GameMath.GetTimeInMilliSecondsString(0.2f));
		}

		public override void Apply(World world, float dt)
		{
			if (world.activeChallenge.state != Challenge.State.ACTION)
			{
				return;
			}
			this.timer += (double)dt;
			if (this.timer >= 0.20000000298023224)
			{
				this.timer = 0.0;
				foreach (Hero hero in world.heroes)
				{
					world.DamageMain(null, hero, new Damage(hero.GetHealthMax() * 0.0099999997764825821 * 0.20000000298023224, false, false, false, false)
					{
						canNotBeDodged = true
					});
				}
			}
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectTimeDealsDamage();
		}

		private const double damagePerSec = 0.0099999997764825821;

		private const float timeInterval = 0.2f;

		private double timer;
	}
}
