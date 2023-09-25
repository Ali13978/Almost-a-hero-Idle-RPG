using System;
using UnityEngine;

namespace Simulation
{
	public class ProjectilePathFromAbove : ProjectilePath
	{
		public override void Init(Vector3 posStart, Vector3 posEnd)
		{
			base.Init(posStart, posEnd);
		}

		public override Vector3 GetPos(float timeRatio)
		{
			Vector3 posEnd = this.posEnd;
			posEnd.y += this.speedVertical * (1f - timeRatio);
			return posEnd;
		}

		public override Vector3 GetDir(float timeRatio)
		{
			return Vector3.down;
		}

		public override ProjectilePath GetCopy()
		{
			return new ProjectilePathFromAbove
			{
				posStart = this.posStart,
				posEnd = this.posEnd,
				speedVertical = this.speedVertical
			};
		}

		public float speedVertical;
	}
}
