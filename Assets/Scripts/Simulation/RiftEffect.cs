using System;
using System.Collections.Generic;

namespace Simulation
{
	public abstract class RiftEffect
	{
		public abstract string GetDesc();

		public abstract float difficultyFactor { get; }

		public abstract RiftEffect Clone();

		public virtual void Apply(World world, float dt)
		{
		}

		public virtual void OnRiftSetup(World world, ChallengeRift riftChallenge, TotemDataBase totem, List<Rune> wornRunes, HeroDataBase[] heroesData, List<Gear> gears)
		{
		}

		public virtual void OnPreDamage(World world, Unit damager, UnitHealthy damaged, Damage damage)
		{
		}

		public virtual void OnPostDamage(World world, Unit damager, UnitHealthy damaged, Damage damage)
		{
		}

		public virtual void OnDeathAny(Unit unit)
		{
		}

		public virtual void OnCollectDrop(Drop drop, World world)
		{
		}

		public virtual void OnEnemyStunned(Enemy enemy, World world)
		{
		}

		public virtual void OnPreAttack(Hero by, Damage newDamage, Projectile projectile)
		{
		}

		public virtual List<CharmEffectData> OnCharmDraft(List<CharmEffectData> list)
		{
			return list;
		}

		public virtual void OnHeroPreHeal(Hero hero, ref double ratioHealed)
		{
		}
	}
}
