using System;

namespace Simulation
{
	public abstract class SkillEvent
	{
		public abstract void Apply(Unit by);

		public abstract void Cancel(Unit by, float timePassedSinceActivation);

		public float time;
	}
}
