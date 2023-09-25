using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class CharmBuffAppleADay : CharmBuff
	{
		protected override bool TryActivating()
		{
			this.numApplesThrown = 0;
			this.lastAppleTimer = 0f;
			return true;
		}

		protected override void OnUpdate(float dt)
		{
			if (this.state == EnchantmentBuffState.ACTIVE && this.numApplesThrown < this.totalNumHeroes)
			{
				this.lastAppleTimer += dt;
				if (this.lastAppleTimer >= CharmBuffAppleADay.APPLE_TIME_DELAY)
				{
					this.lastAppleTimer = 0f;
					this.numApplesThrown++;
					this.ThrowApple();
				}
			}
		}

		private void ThrowApple()
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
			buffDataHealthRegen.dur = this.dur;
			buffDataHealthRegen.healthRegenAdd = (double)(this.healAmount / this.dur);
			buffDataHealthRegen.visuals |= 64;
			BuffDataCritChance buffDataCritChance = new BuffDataCritChance();
			buffDataCritChance.id = 307;
			buffDataCritChance.isStackable = true;
			buffDataCritChance.dur = this.dur;
			buffDataCritChance.critChanceAdd = this.critHitIncrease;
			buffDataCritChance.visuals |= 4;
			Projectile projectile = new Projectile(null, Projectile.Type.APPLE_AID, Projectile.TargetType.SINGLE_ALLY_ANY, target, 1.2f, new ProjectilePathBomb
			{
				heightAddMax = 1.1f
			});
			projectile.startPointOffset = new Vector3(GameMath.GetRandomFloat(-1.5f, -1.6f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(0.1f, 0.2f, GameMath.RandType.NoSeed), 0f);
			projectile.InitPath();
			projectile.visualVariation = 1;
			projectile.visualEffect = new VisualEffect(VisualEffect.Type.TOTEM_EARTH_IMPACT, 0.567f);
			projectile.damageMomentTimeRatio = 1f;
			projectile.buffs = new List<BuffData>
			{
				buffDataHealthRegen,
				buffDataCritChance
			};
			this.world.AddProjectile(projectile);
		}

		public override void OnWeaponUsed(Hero hero)
		{
			this.AddProgress(this.pic);
		}

		public float critHitIncrease;

		public float dur;

		public float healAmount;

		public int totalNumHeroes;

		private int numApplesThrown;

		private float lastAppleTimer;

		public static float APPLE_TIME_DELAY = 0.26f;
	}
}
