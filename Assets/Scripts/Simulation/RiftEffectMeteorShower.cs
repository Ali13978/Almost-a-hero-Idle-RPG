using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class RiftEffectMeteorShower : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 1.25f;
			}
		}

		public override void Apply(World world, float dt)
		{
			if (world.activeChallenge.state != Challenge.State.ACTION)
			{
				return;
			}
			this.timePassed += dt;
			while (this.timePassed > 1.1f)
			{
				this.timePassed -= 1.1f;
				this.RainMeteor(world);
			}
		}

		private void RainMeteor(World world)
		{
			List<UnitHealthy> list = new List<UnitHealthy>();
			foreach (Enemy enemy in world.activeChallenge.enemies)
			{
				if (enemy.IsAlive())
				{
					list.Add(enemy);
				}
			}
			Vector3 targetPos = World.ENEMY_CENTER;
			if (list.Count > 0)
			{
				targetPos = list[GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed)].pos;
			}
			double amount = world.GetHeroTeamDps() * this.teamDamageToDeal;
			Damage damage = new Damage(amount, false, false, false, false);
			damage.type = DamageType.SKILL;
			Projectile projectile = new Projectile(null, Projectile.Type.TOTEM_EARTH, Projectile.TargetType.SINGLE_ENEMY, null, 0.55f, new ProjectilePathFromAbove
			{
				speedVertical = 3.5f
			}, targetPos);
			projectile.visualEffect = new VisualEffect(VisualEffect.Type.TOTEM_EARTH_IMPACT, 1.33f);
			projectile.soundImpact = new SoundEventSound(SoundType.GAMEPLAY, string.Empty, false, 0f, new SoundVariedSimple(SoundArchieve.inst.earthRingMeteorImpact, 1f));
			projectile.damageArea = damage.GetCopy();
			projectile.damageAreaR = 0.4f;
			if (GameMath.GetProbabilityOutcome(0.3f, GameMath.RandType.NoSeed))
			{
				projectile.buffs = new List<BuffData>
				{
					new BuffDataStun
					{
						dur = 1f,
						id = 317
					}
				};
			}
			Vector3 vector = new Vector3(GameMath.GetRandomFloat(0.25f, 0.82f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.8f, 0.35f, GameMath.RandType.NoSeed));
			vector.x += AspectRatioOffset.ENEMY_X;
			projectile.InitPath(vector, vector);
			projectile.scale = 0.6f;
			projectile.damageMomentTimeRatio = 1f;
			projectile.damage = damage;
			world.AddProjectile(projectile);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_METEOR_SHOWER"), GameMath.GetPercentString(this.teamDamageToDeal, false), GameMath.GetPercentString(0.3f, false));
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectMeteorShower
			{
				teamDamageToDeal = this.teamDamageToDeal
			};
		}

		public double teamDamageToDeal = 0.1;

		private float timePassed;

		public const float TIME_FOR_METEOR = 1.1f;

		public const float STUN_CHANCE = 0.3f;

		public const float STUN_DURATION = 1f;
	}
}
