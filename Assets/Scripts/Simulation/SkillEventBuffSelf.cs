using System;

namespace Simulation
{
	public class SkillEventBuffSelf : SkillEvent
	{
		public override void Apply(Unit by)
		{
			by.AddBuff(this.buff, 0, false);
		}

		public override void Cancel(Unit by, float timePassedSinceActivation)
		{
			if (!this.buff.isPermenant && timePassedSinceActivation <= this.buff.dur)
			{
				by.RemoveBuff(this.buff);
			}
		}

		public BuffData buff;
	}
}
