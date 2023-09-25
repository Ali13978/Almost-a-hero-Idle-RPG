using System;
using UnityEngine;

namespace Simulation
{
	public class GlobalPastDamage
	{
		public GlobalPastDamage(UnitHealthy damaged, Damage damage)
		{
			this.damaged = damaged;
			this.damage = damage;
			this.time = 0f;
			this.startPos = damaged.pos;
			this.startPos.y = this.startPos.y + (damaged.GetHeight() + GameMath.GetRandomFloat(-0.01f, 0.01f, GameMath.RandType.NoSeed));
			this.startPos.x = this.startPos.x + GameMath.GetRandomFloat(-0.035f, 0.035f, GameMath.RandType.NoSeed);
			this.pos = this.startPos;
			this.alpha = 1f;
			this.upOffsetY = 0.06f;
		}

		public void Initialize()
		{
			this.totTime = 0.6f;
			if (this.highlight)
			{
				this.totTime = 1.35f;
			}
			else if (this.damage.isCrit || this.damage.isUltraCrit)
			{
				this.totTime = 0.9f;
			}
			float num = (float)(this.damage.amount / this.damaged.GetHealthMax());
			this.startScale = 0.65f + 0.350000024f * num * 5f;
			if (this.startScale > 1f)
			{
				this.startScale = 1f;
			}
			this.totTime *= this.startScale;
		}

		public void Update(float dt)
		{
			this.time += dt;
			float num = this.time / this.totTime;
			this.pos.z = num;
			if (num < 0.15f)
			{
				float num2 = num / 0.15f;
				this.scale = this.startScale * num2;
				this.pos.y = this.startPos.y + num2 * this.upOffsetY;
			}
			else if (num < 0.75f)
			{
				float num3 = (num - 0.15f) / 0.6f;
				this.pos.y = this.startPos.y + this.upOffsetY + num3 * 0.03f;
				this.scale = this.startScale;
			}
			else
			{
				float num4 = (num - 0.15f - 0.6f) / 0.24999997f;
				this.scale = this.startScale * (1f - num4 * 0.5f);
				this.alpha = 1f - num4;
				this.pos.y = this.startPos.y + this.upOffsetY + 0.025f + num4 * 0.05f;
			}
		}

		public const float MAX_TIME = 0.6f;

		public const float CRIT_MAX_TIME = 0.9f;

		public const float HIGHLIGHT_MAX_TIME = 1.35f;

		private const float MIN_SCALE = 0.65f;

		private const float MAX_SCALE = 1f;

		public UnitHealthy damaged;

		public Damage damage;

		public Vector3 pos;

		public Vector3 startPos;

		public float startScale;

		public float scale;

		public float alpha;

		public float time;

		public float totTime;

		public float upOffsetY;

		public bool highlight;

		public const float R1 = 0.15f;

		public const float R2 = 0.6f;

		public const float R3 = 0.24999997f;
	}
}
