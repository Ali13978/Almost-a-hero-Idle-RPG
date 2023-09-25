using System;
using UnityEngine;

namespace Simulation
{
	public abstract class ProjectilePath
	{
		public virtual void Init(Vector3 posStart, Vector3 posEnd)
		{
			this.posStart = posStart;
			this.posEnd = posEnd;
		}

		public abstract Vector3 GetPos(float timeRatio);

		public abstract Vector3 GetDir(float timeRatio);

		public abstract ProjectilePath GetCopy();

		public Vector3 posStart;

		public Vector3 posEnd;
	}
}
