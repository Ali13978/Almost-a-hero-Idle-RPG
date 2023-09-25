using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class CharmBuffLooseLessons : CharmBuff
	{
		protected override bool TryActivating()
		{
			UnitHealthy unitHealthy = null;
			foreach (Enemy enemy in this.world.activeChallenge.enemies)
			{
				if (!enemy.IsDead())
				{
					unitHealthy = enemy;
					break;
				}
			}
			Vector3 targetPos = World.ENEMY_CENTER;
			if (unitHealthy != null)
			{
				targetPos = unitHealthy.pos;
			}
			float randomFloat = GameMath.GetRandomFloat(0.7f, 1.1f, GameMath.RandType.NoSeed);
			Projectile projectile = new Projectile(null, Projectile.Type.DEREK_BOOK, Projectile.TargetType.ALL_ENEMIES, unitHealthy, randomFloat, new ProjectilePathLinear(), targetPos);
			projectile.targetLocked = false;
			projectile.startPointOffset = new Vector3(GameMath.GetRandomFloat(-1.5f, -2f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(0.6f, 1f, GameMath.RandType.NoSeed), 0f);
			projectile.InitPath();
			projectile.damage = new Damage(this.world.GetHeroTeamDps() * (double)this.teamDamageToDeal, false, false, false, false);
			projectile.buffs = new List<BuffData>
			{
				new BuffDataStun
				{
					dur = this.stunDuration,
					id = 300
				}
			};
			projectile.visualEffect = new VisualEffect(VisualEffect.Type.DEREK_BOOK, 0.5f);
			projectile.soundImpact = new SoundEventSound(SoundType.GAMEPLAY, "charm", false, 0f, new SoundSimple(SoundArchieve.inst.derekBookExplosion, 1f));
			projectile.visualVariation = 1;
			this.world.AddProjectile(projectile);
			this.world.AddSoundEvent(new SoundEventSound(SoundType.GAMEPLAY, "charm", false, 0f, new SoundSimple(SoundArchieve.inst.derekThrowBook, 1f)));
			return true;
		}

		public override void OnHeroHealed(Hero hero, double ratioHealed)
		{
			this.AddProgress(this.pic * (float)ratioHealed);
		}

		public float teamDamageToDeal;

		public float stunDuration;
	}
}
