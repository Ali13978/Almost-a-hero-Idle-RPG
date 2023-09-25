using System;

namespace Simulation
{
	public class TotemDataBaseIce : TotemDataBase
	{
		public TotemDataBaseIce()
		{
			this.id = "totemIce";
			this.nameKey = "RING_NAME_ICE";
			this.descKey = this.GetDesc();
			this.projectileShard = new Projectile();
			this.projectileShard.durFly = 0.5f;
			this.projectileShard.type = Projectile.Type.TOTEM_ICE_SHARD;
			this.projectileShard.targetType = Projectile.TargetType.SINGLE_ENEMY;
			this.projectileShard.path = new ProjectilePathFromAbove();
			this.projectileShard.damageMomentTimeRatio = 0.4f;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RING_DESC_ICE"), LM.Get("RING_SPECIAL_ICE"));
		}

		public float GetManaMax()
		{
			return 100f;
		}

		public float GetManaGatherSpeed()
		{
			return 50f;
		}

		public float GetManaUseSpeed()
		{
			return 40f;
		}

		public float GetShardReqMana()
		{
			return 4f;
		}

		public float GetShardAreaR()
		{
			return 0.3f;
		}

		public const float MANA_MAX_DEFAULT = 100f;

		public Projectile projectileShard;
	}
}
