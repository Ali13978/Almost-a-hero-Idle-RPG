using System;

namespace Simulation
{
	public class SkillEventBuffAllAllies : SkillEvent
	{
		public override void Apply(Unit by)
		{
			foreach (UnitHealthy unitHealthy in by.GetAllies())
			{
				if (unitHealthy.IsAlive() && (unitHealthy != by || this.applySelf))
				{
					unitHealthy.AddBuff(this.buff, 0, false);
				}
			}
		}

		public override void Cancel(Unit by, float timePassedSinceActivation)
		{
			if (!this.buff.isPermenant && timePassedSinceActivation <= this.buff.dur)
			{
				foreach (Unit unit in by.GetAllies())
				{
					if (unit != by || this.applySelf)
					{
						unit.RemoveBuff(this.buff);
					}
				}
			}
		}

		public BuffData buff;

		public bool applySelf = true;
	}
}
