using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class CharmBuffExplosiveEnergy : CharmBuff
	{
		protected override bool TryActivating()
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
				return false;
			}
			Hero hero2 = list[GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed)];
			Projectile projectile = new Projectile(null, Projectile.Type.BOMBERMAN_FIRE_CRACKER, Projectile.TargetType.SINGLE_ALLY_ANY, hero2, 1.2f, new ProjectilePathBomb
			{
				heightAddMax = 1.1f
			});
			projectile.damage = new Damage(hero2.GetHealthMax() * this.damagePerToDeal, false, false, false, false);
			projectile.damage.isPure = true;
			projectile.startPointOffset = new Vector3(GameMath.GetRandomFloat(-1.5f, -1.6f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(0.1f, 0.2f, GameMath.RandType.NoSeed), 0f);
			projectile.InitPath();
			projectile.visualVariation = 1;
			projectile.visualEffect = new VisualEffect(VisualEffect.Type.BOMBERMAN_DINAMIT, 0.5f);
			projectile.soundImpact = new SoundEventSound(SoundType.GAMEPLAY, "charm", false, 0f, new SoundSimple(SoundArchieve.inst.bombermanFriendlyCatch, 1f));
			BuffDataAttackSpeed buffDataAttackSpeed = new BuffDataAttackSpeed();
			buffDataAttackSpeed.id = 304;
			projectile.buffs = new List<BuffData>
			{
				buffDataAttackSpeed
			};
			buffDataAttackSpeed.visuals |= 1;
			buffDataAttackSpeed.dur = this.dur;
			buffDataAttackSpeed.isStackable = true;
			buffDataAttackSpeed.attackSpeedAdd = this.attackSpeedIncrease;
			buffDataAttackSpeed.reloadSpeedAdd = this.attackSpeedIncrease * 0.5f;
			this.world.AddProjectile(projectile);
			this.world.AddSoundEvent(new SoundEventSound(SoundType.GAMEPLAY, "charm", false, 0f, new SoundSimple(SoundArchieve.inst.bombermanFriendlyThrow, 1f)));
			return true;
		}

		public override void OnHeroShielded(Hero hero)
		{
			this.AddProgress(this.pic);
		}

		public double damagePerToDeal;

		public float attackSpeedIncrease;

		public float dur;

		private const float ProjectileFlightDuration = 1.2f;
	}
}
