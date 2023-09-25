using System;
using System.Collections.Generic;

namespace Simulation
{
	public class CharmBuffEmergencyFlute : CharmBuff
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
				return true;
			}
			Hero target = list[GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed)];
			BuffDataAccelerateCdSingle buffDataAccelerateCdSingle = new BuffDataAccelerateCdSingle();
			buffDataAccelerateCdSingle.id = 305;
			buffDataAccelerateCdSingle.dur = this.dur;
			buffDataAccelerateCdSingle.isStackable = false;
			buffDataAccelerateCdSingle.skillCooldownFactor = 1f + this.reduction;
			Projectile projectile = new Projectile(null, Projectile.Type.FLUTE, Projectile.TargetType.SINGLE_ALLY_MIN_HEALTH, target, 1.2f, new ProjectilePathFromAbove
			{
				speedVertical = 1.2f
			});
			projectile.soundImpact = new SoundEventSound(SoundType.GAMEPLAY, "charm", false, 0f, new SoundSimple(SoundArchieve.inst.jimAttacks[0], 1f));
			projectile.rotateSpeed = 320f;
			projectile.InitPath();
			projectile.visualEffect = new VisualEffect(VisualEffect.Type.HIT, 0.25f);
			projectile.damageMomentTimeRatio = 1f;
			projectile.buffs = new List<BuffData>
			{
				buffDataAccelerateCdSingle
			};
			this.world.AddProjectile(projectile);
			return true;
		}

		public override void OnDeathAny(Unit unit)
		{
			if (!(unit is Enemy))
			{
				return;
			}
			this.AddProgress(this.pic);
		}

		public float reduction;

		public float dur;
	}
}
