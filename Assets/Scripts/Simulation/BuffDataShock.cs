using System;

namespace Simulation
{
	public class BuffDataShock : BuffData
	{
		public BuffDataShock(float probability, float duration)
		{
			this.probability = probability;
			this.duration = duration;
			this.id = 160;
		}

		public override void OnAfterThunderbolt(Buff buff, UnitHealthy target, Damage damage, bool isSecondary)
		{
			if (GameMath.GetProbabilityOutcome(this.probability, GameMath.RandType.NoSeed) && !target.IsInvulnerable())
			{
				BuffDataStun buffDataStun = new BuffDataStun();
				buffDataStun.id = 167;
				buffDataStun.dur = this.duration;
				buffDataStun.visuals |= 512;
				target.AddBuff(buffDataStun, 0, false);
			}
		}

		private float probability;

		private float duration;
	}
}
