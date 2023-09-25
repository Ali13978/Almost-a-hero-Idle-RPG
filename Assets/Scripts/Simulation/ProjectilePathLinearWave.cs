using System;
using UnityEngine;

namespace Simulation
{
	public class ProjectilePathLinearWave : ProjectilePath
	{
		public override void Init(Vector3 posStart, Vector3 posEnd)
		{
			base.Init(posStart, posEnd);
		}

		public override Vector3 GetPos(float timeRatio)
		{
			float num = GameMath.Lerp(0f, 1f, timeRatio * 2.5f) * GameMath.Lerp(1f, 0f, (timeRatio - 0.6f) * 2.5f);
			float num2 = GameMath.Sin(this.freq * timeRatio * 3.14159274f) * this.mag * num;
			Vector3 result = GameMath.Lerp(this.posStart, this.posEnd, timeRatio);
			result.y += num2;
			return result;
		}

		public override Vector3 GetDir(float timeRatio)
		{
			return this.posEnd - this.posStart;
		}

		public override ProjectilePath GetCopy()
		{
			return new ProjectilePathLinearWave
			{
				posStart = this.posStart,
				posEnd = this.posEnd
			};
		}

		private float freq = 0.1f;

		private float mag = 0.6f;
	}
}
