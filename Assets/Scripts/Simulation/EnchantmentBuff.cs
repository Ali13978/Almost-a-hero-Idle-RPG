using System;

namespace Simulation
{
	public abstract class EnchantmentBuff
	{
		public virtual void Update(float dt)
		{
			this.OnUpdate(dt);
		}

		public abstract void AddProgress(float dp);

		public float GetProgress()
		{
			return this.progress;
		}

		protected virtual bool TryActivating()
		{
			return false;
		}

		public virtual void OnHeroRevived(Hero hero)
		{
		}

		public virtual void OnPreDamage(Unit damager, UnitHealthy damaged, Damage damage)
		{
		}

		public virtual void OnPostDamage(Unit damager, UnitHealthy damaged, Damage damage)
		{
		}

		public virtual void OnDeathAny(Unit unit)
		{
		}

		public virtual void OnEnemyStunned(Enemy enemy)
		{
		}

		public virtual void OnHeroShielded(Hero hero)
		{
		}

		public virtual void OnHeroHealed(Hero hero, double ratioHealed)
		{
		}

		public virtual void OnDodgeAny(Unit dodger)
		{
		}

		public virtual void OnAbilityCast(Skill skill)
		{
		}

		public virtual void OnCollectGold()
		{
		}

		public virtual void OnCollectDrop(Drop drop)
		{
		}

		public virtual void OnWavePassed()
		{
		}

		public virtual void OnAnyCharmTriggered(World world, CharmBuff charmBuff)
		{
		}

		public virtual void OnWeaponUsed(Hero hero)
		{
		}

		public virtual void OnCurseDispelled()
		{
		}

		protected virtual void OnUpdate(float dt)
		{
		}

		public World world;

		public EnchantmentEffectData enchantmentData;

		public EnchantmentBuffState state;

		public float pic;

		public float progress;

		public bool isLoaded;
	}
}
