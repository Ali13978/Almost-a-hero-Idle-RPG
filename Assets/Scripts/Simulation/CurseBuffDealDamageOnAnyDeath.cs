using System;

namespace Simulation
{
	public class CurseBuffDealDamageOnAnyDeath : CurseBuff
	{
		public override void OnDeathAny(Unit unit)
		{
			if (this.state == EnchantmentBuffState.INACTIVE)
			{
				return;
			}
			foreach (Hero hero in this.world.heroes)
			{
				Damage damage = new Damage(hero.GetHealthMax() * this.damageFactor, false, false, false, false);
				damage.canNotBeDodged = true;
				this.world.DamageMain(null, hero, damage);
			}
			VisualEffect visualEffect = new VisualEffect(VisualEffect.Type.DEREK_BOOK, 0.6f);
			visualEffect.scale = 1.15f;
			visualEffect.pos = unit.pos;
			this.world.visualEffects.Add(visualEffect);
		}

		public override void OnDodgeAny(Unit dodger)
		{
			if (dodger is Hero)
			{
				this.AddProgress(this.pic);
			}
		}

		public double damageFactor;
	}
}
