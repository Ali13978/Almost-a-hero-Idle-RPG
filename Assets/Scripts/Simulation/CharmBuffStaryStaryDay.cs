using System;
using System.Collections.Generic;

namespace Simulation
{
	public class CharmBuffStaryStaryDay : CharmBuff
	{
		protected override bool TryActivating()
		{
			this.numStarsFallen = 0;
			this.lastStarTimer = 0f;
			return true;
		}

		protected override void OnUpdate(float dt)
		{
			if (this.state == EnchantmentBuffState.ACTIVE && this.numStarsFallen < this.totalNumStars)
			{
				this.lastStarTimer += dt;
				if (this.lastStarTimer >= CharmBuffStaryStaryDay.STAR_TIME_DELAY)
				{
					this.lastStarTimer = 0f;
					this.numStarsFallen++;
					this.FallStar();
				}
			}
		}

		private void FallStar()
		{
			List<Hero> list = new List<Hero>();
			foreach (Hero hero in this.world.heroes)
			{
				if (hero.IsAlive())
				{
					list.Add(hero);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			Hero target = list[GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed)];
			BuffDataHealthRegen buffDataHealthRegen = new BuffDataHealthRegen();
			buffDataHealthRegen.id = 306;
			buffDataHealthRegen.isStackable = true;
			buffDataHealthRegen.dur = 0.5f;
			buffDataHealthRegen.healthRegenAdd = (double)(this.healAmountEachStar / buffDataHealthRegen.dur);
			buffDataHealthRegen.visuals |= 64;
			Projectile projectile = new Projectile(null, Projectile.Type.TOTEM_EARTH_STAR, Projectile.TargetType.SINGLE_ALLY_MIN_HEALTH, target, 0.5f, new ProjectilePathFromAbove
			{
				speedVertical = 1.5f
			});
			projectile.InitPath();
			projectile.visualEffect = new VisualEffect(VisualEffect.Type.TOTEM_EARTH_IMPACT, 0.567f);
			projectile.damageMomentTimeRatio = 1f;
			projectile.buffs = new List<BuffData>
			{
				buffDataHealthRegen
			};
			this.world.AddProjectile(projectile);
			this.world.AddSoundEvent(new SoundEventSound(SoundType.GAMEPLAY, "charm", false, 0f, new SoundSimple(SoundArchieve.inst.earthRingMeteor[1], 1f, float.MaxValue)));
		}

		public override void OnPostDamage(Unit damager, UnitHealthy damaged, Damage damage)
		{
			if (!damage.isCrit)
			{
				return;
			}
			if (damaged is Enemy)
			{
				this.AddProgress(this.pic);
			}
		}

		public float healAmountEachStar;

		public int totalNumStars;

		private int numStarsFallen;

		private float lastStarTimer;

		public static float STAR_TIME_DELAY = 0.45f;
	}
}
