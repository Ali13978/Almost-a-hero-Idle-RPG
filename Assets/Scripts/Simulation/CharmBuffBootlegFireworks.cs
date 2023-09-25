using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class CharmBuffBootlegFireworks : CharmBuff
	{
		protected override bool TryActivating()
		{
			this.numHit = 0;
			this.lastHitTimer = 0f;
			return true;
		}

		protected override void OnUpdate(float dt)
		{
			if (this.state == EnchantmentBuffState.ACTIVE && this.numHit < this.totalNumTimes)
			{
				this.lastHitTimer += dt;
				if (this.lastHitTimer >= CharmBuffBootlegFireworks.HIT_TIME_DELAY)
				{
					this.lastHitTimer = 0f;
					this.numHit++;
					this.ThrowFirework();
				}
			}
		}

		private void ThrowFirework()
		{
			List<UnitHealthy> list = new List<UnitHealthy>();
			foreach (Enemy enemy in this.world.activeChallenge.enemies)
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
			double amount = this.world.GetHeroTeamDps() * this.teamDamageToDeal;
			Damage damage = new Damage(amount, false, false, false, false);
			damage.type = DamageType.SKILL;
			Projectile projectile = new Projectile(null, Projectile.Type.BOMBERMAN_FIREWORK, Projectile.TargetType.ALL_ENEMIES, null, 0.95f, new ProjectilePathLinear(), targetPos);
			projectile.endPointOffset = new Vector3(GameMath.GetRandomFloat(-0.2f, 0.2f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.2f, 0.2f, GameMath.RandType.NoSeed), 0f);
			projectile.visualVariation = 1;
			projectile.damageMomentTimeRatio = 0.65f;
			projectile.InitPath();
			projectile.damage = damage;
			this.world.AddProjectile(projectile);
		}

		public override void OnEnemyStunned(Enemy enemy)
		{
			this.AddProgress(this.pic);
		}

		public double teamDamageToDeal;

		public int totalNumTimes;

		private int numHit;

		private float lastHitTimer;

		public static float HIT_TIME_DELAY = 0.6f;
	}
}
