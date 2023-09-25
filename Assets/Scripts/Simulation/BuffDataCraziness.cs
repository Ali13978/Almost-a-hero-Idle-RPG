using System;

namespace Simulation
{
	public class BuffDataCraziness : BuffData
	{
		public override void OnCastSpellSelf(Buff buff, Skill skill)
		{
			buff.GetBy().AddBuff(this.effect, 0, false);
		}

		public BuffDataCritChance effect;
	}
}
