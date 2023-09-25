using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class CharmBuffRustyDagger : CharmBuff
	{
		protected override bool TryActivating()
		{
			this.numDaggersThrown = 0;
			this.lastDaggerThrownTimer = 0f;
			return true;
		}

		protected override void OnUpdate(float dt)
		{
			if (this.state == EnchantmentBuffState.ACTIVE && this.numDaggersThrown < this.numDaggers)
			{
				this.lastDaggerThrownTimer += dt;
				if (this.lastDaggerThrownTimer >= CharmBuffRustyDagger.DAGGER_THROW_TIME_DELAY)
				{
					this.lastDaggerThrownTimer = 0f;
					this.numDaggersThrown++;
					this.ThrowDagger();
				}
			}
		}

		private void ThrowDagger()
		{
			List<UnitHealthy> list = new List<UnitHealthy>();
			foreach (Enemy enemy in this.world.activeChallenge.enemies)
			{
				if (enemy.IsAlive())
				{
					list.Add(enemy);
				}
			}
			if (list.Count > 0)
			{
				UnitHealthy target = list[GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed)];
				double amount = this.world.GetHeroTeamDps() * this.damageMul;
				Damage damage = new Damage(amount, false, false, false, false);
				damage.type = DamageType.SKILL;
				Projectile projectile = new Projectile(null, Projectile.Type.CHARM_DAGGER, Projectile.TargetType.SINGLE_ENEMY, target, 0.9f, new ProjectilePathLinear());
				projectile.damage = damage;
				projectile.startPointOffset = new Vector3(GameMath.GetRandomFloat(-1.5f, -1.6f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(0.4f, 0.5f, GameMath.RandType.NoSeed), 0f);
				projectile.InitPath();
				projectile.visualVariation = GameMath.GetRandomInt(1, 9, GameMath.RandType.NoSeed);
				projectile.visualEffect = new VisualEffect(VisualEffect.Type.HIT, 0.2f);
				BuffDataDamageAdd buffDataDamageAdd = new BuffDataDamageAdd();
				buffDataDamageAdd.id = 320;
				projectile.buffs = new List<BuffData>
				{
					buffDataDamageAdd
				};
				buffDataDamageAdd.visuals |= 16;
				buffDataDamageAdd.dur = 10f;
				buffDataDamageAdd.isStackable = true;
				buffDataDamageAdd.damageAdd = this.damageReduction;
				this.world.AddProjectile(projectile);
				return;
			}
		}

		public override void OnCollectGold()
		{
			this.AddProgress(this.pic);
		}

		public int numDaggers;

		public double damageMul;

		public double damageReduction;

		private int numDaggersThrown;

		private float lastDaggerThrownTimer;

		public static float DAGGER_THROW_TIME_DELAY = 0.12f;
	}
}
