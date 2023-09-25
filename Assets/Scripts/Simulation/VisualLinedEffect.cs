using System;
using UnityEngine;

namespace Simulation
{
	public class VisualLinedEffect
	{
		public VisualLinedEffect(VisualLinedEffect.Type type, Vector3 posStart, Vector3 posEnd, float dur)
		{
			this.type = type;
			this.dur = dur;
			this.time = 0f;
			this.isWithoutEnemies = false;
			this.posStart = posStart;
			this.posEnd = posEnd;
		}

		public VisualLinedEffect(VisualLinedEffect.Type type, Vector3 posStart, float dur, bool isWithoutEnemies = true)
		{
			this.type = type;
			this.dur = dur;
			this.time = 0f;
			this.isWithoutEnemies = isWithoutEnemies;
			this.posStart = posStart;
		}

		public void Update(float dt)
		{
			this.time += dt;
		}

		public bool IsToBeRemoved()
		{
			return this.time > this.dur;
		}

		public VisualLinedEffect.Type type;

		public float dur;

		public float time;

		public bool isWithoutEnemies;

		public Vector3 posStart;

		public Vector3 posEnd;

		public enum Type
		{
			TOTEM_LIGHTNING,
			TOTEM_FIRE
		}
	}
}
