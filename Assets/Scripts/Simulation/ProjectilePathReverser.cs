using System;
using UnityEngine;

namespace Simulation
{
	public class ProjectilePathReverser : ProjectilePath
	{
		public ProjectilePathReverser(ProjectilePath pathToReverse)
		{
			this.pathToReverse = pathToReverse;
		}

		public override void Init(Vector3 posStart, Vector3 posEnd)
		{
			this.pathToReverse.Init(posStart, posEnd);
		}

		public override Vector3 GetPos(float timeRatio)
		{
			return this.pathToReverse.GetPos(1f - timeRatio);
		}

		public override Vector3 GetDir(float timeRatio)
		{
			return this.pathToReverse.GetDir(1f - timeRatio);
		}

		public override ProjectilePath GetCopy()
		{
			return new ProjectilePathReverser(this.pathToReverse.GetCopy());
		}

		private ProjectilePath pathToReverse;
	}
}
