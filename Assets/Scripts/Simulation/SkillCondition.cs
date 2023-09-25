using System;

namespace Simulation
{
	public abstract class SkillCondition
	{
		public abstract bool IsNotOkay(Unit by);
	}
}
