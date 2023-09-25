using System;

namespace Simulation
{
	public class SkillEventDamageSelfWithoutKilling : SkillEvent
	{
		public override void Apply(Unit by)
		{
			UnitHealthy unitHealthy = (UnitHealthy)by;
			unitHealthy.DecrementHealthRatioWithoutKilling(this.healthRatio);
		}

		public override void Cancel(Unit by, float timePassedSinceActivation)
		{
		}

		public double healthRatio;
	}
}
