using System;
using UnityEngine;

namespace Simulation
{
	public class GlobalPastHeal
	{
		public GlobalPastHeal(UnitHealthy healed, double heal)
		{
			this.healed = healed;
			this.heal = heal;
			this.time = 0f;
			this.startPos = healed.pos;
			this.startPos.y = this.startPos.y + healed.GetHeight();
			this.startPos.x = this.startPos.x + GameMath.GetRandomFloat(-0.035f, 0.035f, GameMath.RandType.NoSeed);
			this.pos = this.startPos;
			this.alpha = 1f;
			this.upOffsetY = 0.06f;
		}

		public void Initialize()
		{
			this.totTime = 1f;
			if (this.highlight)
			{
				this.totTime = 1.35f;
			}
			this.startScale = 0.85f;
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
				this.pos.y = this.startPos.y - num2 * this.upOffsetY;
			}
			else if (num < 0.75f)
			{
				float num3 = (num - 0.15f) / 0.6f;
				this.pos.y = this.startPos.y - this.upOffsetY - num3 * 0.03f;
				this.scale = this.startScale;
			}
			else
			{
				float num4 = (num - 0.15f - 0.6f) / 0.24999997f;
				this.scale = this.startScale * (1f - num4 * 0.5f);
				this.alpha = 1f - num4;
				this.pos.y = this.startPos.y - this.upOffsetY - 0.03f - num4 * 0.05f;
			}
		}

		public const float MAX_TIME = 1f;

		public const float HIGHLIGHT_MAX_TIME = 1.35f;

		private const float MIN_SCALE = 0.85f;

		private const float MAX_SCALE = 1.4f;

		public UnitHealthy healed;

		public double heal;

		public Vector3 pos;

		public Vector3 startPos;

		public float startScale;

		public float scale;

		public float alpha;

		public float time;

		public float totTime;

		public float upOffsetY;

		public bool highlight;

		public bool isPercent;

		public const float R1 = 0.15f;

		public const float R2 = 0.6f;

		public const float R3 = 0.24999997f;
	}
}
