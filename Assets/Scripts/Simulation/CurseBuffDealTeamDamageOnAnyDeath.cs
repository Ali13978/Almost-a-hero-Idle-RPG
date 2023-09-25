using System;

namespace Simulation
{
	public class CurseBuffDealTeamDamageOnAnyDeath : CurseBuff
	{
		public override void OnDeathAny(Unit unit)
		{
			if (this.state == EnchantmentBuffState.INACTIVE)
			{
				return;
			}
			Damage damage = new Damage(this.world.GetHeroTeamDps() * this.damageFactor, false, false, false, false)
			{
				canNotBeDodged = true
			};
			foreach (Hero damaged in this.world.heroes)
			{
				this.world.DamageFuture(null, damaged, damage.GetCopy(), 0.6f);
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
