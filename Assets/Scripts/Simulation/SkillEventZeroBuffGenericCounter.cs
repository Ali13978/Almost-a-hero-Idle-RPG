using System;

namespace Simulation
{
	public class SkillEventZeroBuffGenericCounter : SkillEvent
	{
		public override void Apply(Unit by)
		{
			by.ZeroBuffGenericCounter(this.buffType);
		}

		public override void Cancel(Unit by, float timePassedSinceActivation)
		{
		}

		public Type buffType;
	}
}
