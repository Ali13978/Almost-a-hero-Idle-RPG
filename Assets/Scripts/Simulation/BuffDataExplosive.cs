using System;

namespace Simulation
{
	public class BuffDataExplosive : BuffData
	{
		public BuffDataExplosive(float duration, float chance)
		{
			this.duration = duration;
			this.chance = chance;
			this.id = 80;
		}

		public override void OnOverheatFinished(Buff buff)
		{
			BuffDataCritChance buffDataCritChance = new BuffDataCritChance();
			buffDataCritChance.id = 32;
			buffDataCritChance.dur = this.duration;
			buffDataCritChance.critChanceAdd = this.chance;
			buff.GetBy().AddBuff(buffDataCritChance, 0, false);
		}

		private float chance;

		private float duration;
	}
}
