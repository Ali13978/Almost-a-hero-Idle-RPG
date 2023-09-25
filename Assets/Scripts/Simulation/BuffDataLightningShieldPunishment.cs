using System;

namespace Simulation
{
	public class BuffDataLightningShieldPunishment : BuffData
	{
		public BuffDataLightningShieldPunishment(double damageRatio)
		{
			this.damageRatio = damageRatio;
		}

		public override void OnTakenDamage(Buff buff, Unit attacker, Damage damage)
		{
			if (damage.amount <= 0.0)
			{
				return;
			}
			UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
			if (unitHealthy.IsAlly(attacker))
			{
				return;
			}
			if (!(attacker is UnitHealthy))
			{
				return;
			}
			UnitHealthy damaged = (UnitHealthy)attacker;
			Unit by = buff.GetBy();
			Damage damage2 = new Damage(by.GetDpsTeam() * this.damageRatio, false, false, false, false);
			if (GameMath.GetProbabilityOutcome(by.GetCritChance(), GameMath.RandType.NoSeed))
			{
				damage2.isCrit = true;
				damage2.amount *= by.GetCritFactor();
			}
			buff.GetWorld().DamageMain(buff.GetBy(), damaged, damage2);
		}

		private double damageRatio;
	}
}
