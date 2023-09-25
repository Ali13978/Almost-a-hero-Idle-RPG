using System;
using UnityEngine;

namespace Simulation
{
	public class VisualEffect
	{
		public VisualEffect(VisualEffect.Type type, float dur)
		{
			this.type = type;
			this.dur = dur;
			this.scale = 1f;
			this.time = 0f;
		}

		public VisualEffect GetCopy()
		{
			return new VisualEffect(this.type, this.dur)
			{
				time = this.time,
				pos = this.pos
			};
		}

		public void Update(float dt)
		{
			this.time += dt;
		}

		public bool IsToBeRemoved()
		{
			return this.time > this.dur;
		}

		public VisualEffect.Type type;

		public float dur;

		public float time;

		public Vector3 pos;

		public float rot;

		public float scale;

		public int skinIndex = -1;

		public float dirX = 1f;

		public enum Type
		{
			REVERSED_EXCALIBUR_MUD,
			BOMBERMAN_DINAMIT,
			DEREK_BOOK,
			TOTEM_THUNDERBOLT,
			TOTEM_THUNDERBOLT_PURPLE,
			TOTEM_FIRE_SMOKE,
			MAGOLIES_PROJECTILE_EXPLOSION,
			GREEN_APPLE_EXPLOSION,
			SAM_BOTTLE_EXPLOSION,
			DUCK,
			DUCK_CHARM,
			HIT,
			ENEMY_DEATH,
			TOTEM_EARTH_IMPACT,
			TOTEM_EARTH_TAP,
			TOTEM_EARTH_TAP_DISABLE,
			TOTEM_EARTH_STAR_IMPACT,
			TAM_FLARE,
			GOBLIN_SACK,
			GOBLIN_SMOKE,
			WARLOCK_SWARM,
			SNAKE_PROJECTILE_EXPLOSION,
			BABU_SOUP,
			ORNAMENT_DROP
		}
	}
}
