using System;
using UnityEngine;

namespace Simulation
{
	public class ProjectilePathLinear : ProjectilePath
	{
		public override void Init(Vector3 posStart, Vector3 posEnd)
		{
			base.Init(posStart, posEnd);
		}

		public override Vector3 GetPos(float timeRatio)
		{
			return GameMath.Lerp(this.posStart, this.posEnd, timeRatio);
		}

		public override Vector3 GetDir(float timeRatio)
		{
			return this.posEnd - this.posStart;
		}

		public override ProjectilePath GetCopy()
		{
			return new ProjectilePathLinear
			{
				posStart = this.posStart,
				posEnd = this.posEnd
			};
		}
	}
}
