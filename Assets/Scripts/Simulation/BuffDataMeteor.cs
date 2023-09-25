using System;
using UnityEngine;

namespace Simulation
{
	public class BuffDataMeteor : BuffData
	{
		public BuffDataMeteor(double damageRatio, float delay)
		{
			this.damageRatio = damageRatio;
			this.delay = delay;
			this.timer = delay;
			this.projectile = new Projectile();
			this.projectile.durFly = 0.5f;
			this.projectile.type = Projectile.Type.TOTEM_EARTH;
			this.projectile.targetType = Projectile.TargetType.ALL_ENEMIES;
			ProjectilePathFromAbove projectilePathFromAbove = new ProjectilePathFromAbove();
			projectilePathFromAbove.speedVertical = 3.5f;
			this.projectile.path = projectilePathFromAbove;
			this.projectile.damageMomentTimeRatio = 1f;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
			if (!unitHealthy.IsAlive())
			{
				return;
			}
			if (this.timerVisual > 0f)
			{
				this.visuals = 4096;
				this.timerVisual -= dt;
			}
			else
			{
				this.visuals = 0;
			}
			this.timer -= dt;
			if (this.timer <= 0f)
			{
				this.timer = this.delay;
				this.ThrowMeteor(unitHealthy);
				this.timerVisual = 1f;
			}
		}

		private void ThrowMeteor(UnitHealthy by)
		{
			SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, by.GetId(), false, 0f, new SoundVariedSimple(SoundArchieve.inst.earthRingMeteor, 1f));
			by.world.AddSoundEvent(e);
			Projectile copy = this.projectile.GetCopy();
			copy.by = by;
			double num = by.GetDamage() * this.damageRatio;
			bool probabilityOutcome = GameMath.GetProbabilityOutcome(by.GetCritChance(), GameMath.RandType.NoSeed);
			if (probabilityOutcome)
			{
				num *= by.GetCritFactor();
			}
			copy.damageArea = new Damage(num, probabilityOutcome, false, false, false);
			copy.damageAreaR = 1f;
			Vector3 vector = new Vector3(GameMath.GetRandomFloat(0.25f, 0.82f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.8f, 0.35f, GameMath.RandType.NoSeed));
			vector.x += AspectRatioOffset.ENEMY_X;
			copy.InitPath(vector, vector);
			copy.soundImpact = new SoundEventSound(SoundType.GAMEPLAY, by.GetId(), false, 0f, new SoundVariedSimple(SoundArchieve.inst.earthRingMeteorImpact, 1f));
			copy.visualEffect = new VisualEffect(VisualEffect.Type.TOTEM_EARTH_IMPACT, 1.33f);
			by.world.AddProjectile(copy);
		}

		private float delay;

		private double damageRatio;

		private float timer;

		private Projectile projectile;

		private float timerVisual;
	}
}
