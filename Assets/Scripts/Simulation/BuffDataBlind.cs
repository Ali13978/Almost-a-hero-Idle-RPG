using System;

namespace Simulation
{
	public class BuffDataBlind : BuffData
	{
		public BuffDataBlind(float chance, float duration, float missChance)
		{
			this.chance = chance;
			this.duration = duration;
			this.missChance = missChance;
		}

		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
			if (!unitHealthy.IsAlly(target) && GameMath.GetProbabilityOutcome(this.chance, GameMath.RandType.NoSeed))
			{
				BuffDataMissChanceAdd buffDataMissChanceAdd = new BuffDataMissChanceAdd();
				buffDataMissChanceAdd.id = 132;
				buffDataMissChanceAdd.isPermenant = false;
				buffDataMissChanceAdd.isStackable = false;
				buffDataMissChanceAdd.visuals |= 2048;
				buffDataMissChanceAdd.dur = this.duration;
				buffDataMissChanceAdd.missChanceAdd = this.missChance;
				target.AddBuff(buffDataMissChanceAdd, 0, false);
			}
		}

		public float chance;

		public float duration;

		public float missChance;
	}
}
