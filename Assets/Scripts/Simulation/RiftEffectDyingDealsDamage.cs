using System;

namespace Simulation
{
	public class RiftEffectDyingDealsDamage : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 0.25f;
			}
		}

		public override void OnDeathAny(Unit unit)
		{
			World world = unit.world;
			VisualEffect visualEffect = new VisualEffect(VisualEffect.Type.DEREK_BOOK, 0.6f);
			visualEffect.scale = 1.15f;
			visualEffect.pos = unit.pos;
			world.visualEffects.Add(visualEffect);
			Damage damage = new Damage(0.0, false, false, false, false);
			if (unit is Hero)
			{
				damage.amount = world.GetEnemyTeamDps() * this.damageFactor;
			}
			else
			{
				if (!(unit is Enemy))
				{
					return;
				}
				damage.amount = world.GetHeroTeamDps() * this.damageFactor;
			}
			foreach (Hero damaged in world.heroes)
			{
				world.DamageFuture(null, damaged, damage.GetCopy(), 0.25f);
			}
			foreach (Enemy damaged2 in world.activeChallenge.enemies)
			{
				world.DamageFuture(null, damaged2, damage.GetCopy(), 0.25f);
			}
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_DYING_DEALS_DAMAGE"), GameMath.GetPercentString(this.damageFactor, false));
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectDyingDealsDamage
			{
				damageFactor = this.damageFactor
			};
		}

		public double damageFactor = 0.3;
	}
}
