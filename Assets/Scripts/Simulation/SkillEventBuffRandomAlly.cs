using System;
using System.Collections.Generic;
using System.Linq;

namespace Simulation
{
	public class SkillEventBuffRandomAlly : SkillEvent
	{
		public override void Apply(Unit by)
		{
			List<UnitHealthy> list = (from a in @by.GetAllies()
			where a.IsAlive()
			select a).ToList<UnitHealthy>();
			list.Remove(by as UnitHealthy);
			if (list.Count > 0)
			{
				this.unitToBuff = list[GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed)];
			}
			else
			{
				this.unitToBuff = (by as UnitHealthy);
			}
			foreach (BuffData buffData in this.buffs)
			{
				this.unitToBuff.AddBuff(buffData, 0, false);
			}
		}

		public override void Cancel(Unit by, float timePassedSinceActivation)
		{
			foreach (BuffData buffData in this.buffs)
			{
				if (!buffData.isPermenant && timePassedSinceActivation <= buffData.dur)
				{
					this.unitToBuff.RemoveBuff(buffData);
				}
			}
		}

		public List<BuffData> buffs;

		private UnitHealthy unitToBuff;
	}
}
