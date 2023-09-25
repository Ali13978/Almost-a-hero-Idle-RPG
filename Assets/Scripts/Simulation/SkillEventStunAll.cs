using System;

namespace Simulation
{
	public class SkillEventStunAll : SkillEvent
	{
		public override void Apply(Unit by)
		{
			foreach (Unit unit in by.GetOpponents())
			{
				unit.AddBuff(this.effect, 0, false);
			}
		}

		public override void Cancel(Unit by, float timePassedSinceActivation)
		{
			if (!this.effect.isPermenant && timePassedSinceActivation <= this.effect.dur)
			{
				foreach (Unit unit in by.GetOpponents())
				{
					unit.RemoveBuff(this.effect);
				}
			}
		}

		public BuffDataStun effect;
	}
}
