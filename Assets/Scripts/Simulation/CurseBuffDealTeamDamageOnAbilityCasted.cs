using System;

namespace Simulation
{
	public class CurseBuffDealTeamDamageOnAbilityCasted : CurseBuff
	{
		public override void OnAbilityCast(Skill skill)
		{
			if (this.state == EnchantmentBuffState.INACTIVE)
			{
				return;
			}
			Damage damage = new Damage(this.world.GetHeroTeamDps() * this.damageFactor, false, false, false, false)
			{
				canNotBeDodged = true
			};
			foreach (Hero hero in this.world.heroes)
			{
				if (hero.IsAlive())
				{
					this.world.DamageFuture(null, hero, damage.GetCopy(), 0.1f);
					VisualEffect visualEffect = new VisualEffect(VisualEffect.Type.TOTEM_THUNDERBOLT, 0.3f);
					visualEffect.pos = hero.pos;
					this.world.visualEffects.Add(visualEffect);
				}
			}
			SoundVariedSimple sound = new SoundVariedSimple(SoundArchieve.inst.thunderbolts, GameMath.GetRandomFloat(0.6f, 0.85f, GameMath.RandType.NoSeed));
			SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, "curse", false, 0f, sound);
			this.world.AddSoundEvent(e);
		}

		public override void OnPostDamage(Unit damager, UnitHealthy damaged, Damage damage)
		{
			if (damager is Enemy)
			{
				this.AddProgress(this.pic);
			}
		}

		public double damageFactor;
	}
}
