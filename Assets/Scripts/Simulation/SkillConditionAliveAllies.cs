using System;

namespace Simulation
{
	public class SkillConditionAliveAllies : SkillCondition
	{
		public SkillConditionAliveAllies(int numRequired)
		{
			this.numRequiredAllies = numRequired;
		}

		public override bool IsNotOkay(Unit by)
		{
			int num = 0;
			foreach (UnitHealthy unitHealthy in by.GetAllies())
			{
				if (unitHealthy != by && unitHealthy.IsAlive())
				{
					num++;
				}
			}
			return num < this.numRequiredAllies;
		}

		public int numRequiredAllies;
	}
}
