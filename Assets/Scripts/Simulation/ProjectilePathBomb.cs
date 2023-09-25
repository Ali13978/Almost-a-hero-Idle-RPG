using System;
using UnityEngine;

namespace Simulation
{
	public class ProjectilePathBomb : ProjectilePath
	{
		public override void Init(Vector3 posStart, Vector3 posEnd)
		{
			base.Init(posStart, posEnd);
		}

		public override Vector3 GetPos(float timeRatio)
		{
			float num = timeRatio - 0.5f;
			num *= 2f;
			num *= num;
			num = 1f - num;
			num *= this.heightAddMax;
			Vector3 result = GameMath.Lerp(this.posStart, this.posEnd, timeRatio);
			result.y += num;
			return result;
		}

		public override Vector3 GetDir(float timeRatio)
		{
			float num = 0.01f;
			return this.GetPos(timeRatio + num) - this.GetPos(timeRatio - num);
		}

		public override ProjectilePath GetCopy()
		{
			return new ProjectilePathBomb
			{
				posStart = this.posStart,
				posEnd = this.posEnd,
				heightAddMax = this.heightAddMax
			};
		}

		public float heightAddMax;
	}
}
