using System;

namespace Simulation
{
	public class CurseBuffDealDamage : CurseBuff
	{
		public override void OnDeathAny(Unit unit)
		{
			if (!(unit is Enemy))
			{
				return;
			}
			this.AddProgress(this.pic);
		}

		public override void OnAnyCharmTriggered(World world, CharmBuff charmBuff)
		{
			if (this.state == EnchantmentBuffState.INACTIVE || world.activeChallenge.state != Challenge.State.ACTION)
			{
				return;
			}
			Hero randomAliveHero = world.GetRandomAliveHero();
			if (randomAliveHero != null)
			{
				world.DamageMain(null, randomAliveHero, new Damage(randomAliveHero.GetHealthMax() * this.damageFactor, false, false, false, false)
				{
					isPure = true
				});
				VisualEffect visualEffect = new VisualEffect(VisualEffect.Type.TOTEM_THUNDERBOLT, 0.5f);
				visualEffect.pos = randomAliveHero.pos;
				world.visualEffects.Add(visualEffect);
			}
		}

		public double damageFactor;
	}
}
