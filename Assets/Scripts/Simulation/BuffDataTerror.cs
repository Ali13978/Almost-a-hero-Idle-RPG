using System;

namespace Simulation
{
	public class BuffDataTerror : BuffData
	{
		public override void OnPreTakeDamage(Buff buff, Unit attacker, Damage damage)
		{
			UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
			if (!unitHealthy.IsAlly(attacker) && GameMath.GetProbabilityOutcome(this.terrorizeChance, GameMath.RandType.NoSeed))
			{
				BuffDataMissChanceAdd buffDataMissChanceAdd = new BuffDataMissChanceAdd();
				buffDataMissChanceAdd.id = 279;
				buffDataMissChanceAdd.isPermenant = false;
				buffDataMissChanceAdd.isStackable = false;
				buffDataMissChanceAdd.visuals |= 2048;
				buffDataMissChanceAdd.dur = this.terrorizeDuration;
				buffDataMissChanceAdd.missChanceAdd = this.missChance;
				attacker.AddBuff(buffDataMissChanceAdd, 0, false);
				if (unitHealthy.GetId() == "WARLOCK")
				{
					unitHealthy.world.OnWarlockBlindEnemy();
				}
			}
		}

		public float terrorizeChance;

		public float terrorizeDuration;

		public float missChance;
	}
}
