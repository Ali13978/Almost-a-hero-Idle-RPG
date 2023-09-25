using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class CharmBuffFrostyStorm : CharmBuff
	{
		protected override bool TryActivating()
		{
			this.numShardsThrown = 0;
			this.lastShardTimer = 0f;
			return true;
		}

		protected override void OnUpdate(float dt)
		{
			this.AddProgress(this.pic * dt);
			if (this.state == EnchantmentBuffState.ACTIVE && this.numShardsThrown < this.numShards)
			{
				this.lastShardTimer += dt;
				if (this.lastShardTimer >= CharmBuffFrostyStorm.SHARD_TIME_DELAY)
				{
					this.lastShardTimer = 0f;
					this.numShardsThrown++;
					this.ThrowShard();
				}
			}
		}

		private void ThrowShard()
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
			UnitHealthy unitHealthy = null;
			if (list.Count > 0)
			{
				unitHealthy = list[GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed)];
				targetPos = unitHealthy.pos;
			}
			double amount = this.world.GetHeroTeamDps() * this.shardDamage;
			Damage damage = new Damage(amount, false, false, false, false);
			damage.type = DamageType.SKILL;
			Projectile projectile = new Projectile(null, Projectile.Type.TOTEM_ICE_SHARD, Projectile.TargetType.SINGLE_ENEMY, unitHealthy, 0.95f, new ProjectilePathLinear(), targetPos);
			Vector3 vector = new Vector3(GameMath.GetRandomFloat(0.25f, 0.82f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.8f, 0.35f, GameMath.RandType.NoSeed));
			vector.x += AspectRatioOffset.ENEMY_X;
			projectile.InitPath(vector, vector);
			projectile.damageMomentTimeRatio = 0.3f;
			projectile.InitPath();
			projectile.damage = damage;
			projectile.soundImpact = new SoundEventSound(SoundType.GAMEPLAY, "totemIce", false, 0f, new SoundVariedSimple(SoundArchieve.inst.totemIceStrikes, 0.6f));
			BuffDataAttackSpeed buffDataAttackSpeed = new BuffDataAttackSpeed();
			buffDataAttackSpeed.id = 314;
			buffDataAttackSpeed.dur = this.slowDuration;
			buffDataAttackSpeed.isStackable = false;
			buffDataAttackSpeed.attackSpeedAdd = -this.slowAmount;
			buffDataAttackSpeed.visuals |= 2;
			projectile.buffs = new List<BuffData>
			{
				buffDataAttackSpeed
			};
			this.world.AddProjectile(projectile);
		}

		public int numShards;

		public double shardDamage;

		public float slowAmount;

		public float slowDuration;

		private int numShardsThrown;

		private float lastShardTimer;

		public static float SHARD_TIME_DELAY = 0.3f;
	}
}
