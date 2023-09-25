using System;

namespace Simulation
{
	public class SkillEventBuffAllOpponents : SkillEvent
	{
		public override void Apply(Unit by)
		{
			foreach (Unit unit in by.GetOpponents())
			{
				unit.AddBuff(this.buff, 0, false);
			}
		}

		public override void Cancel(Unit by, float timePassedSinceActivation)
		{
			if (!this.buff.isPermenant && timePassedSinceActivation <= this.buff.dur)
			{
				foreach (Unit unit in by.GetOpponents())
				{
					unit.RemoveBuff(this.buff);
				}
			}
		}

		public BuffData buff;
	}
}
