using System;
using System.Collections.Generic;
using System.Linq;

namespace Simulation
{
	public class ShieldOnDeath : BuffData
	{
		public ShieldOnDeath(float cooldown, float shieldAmount)
		{
			this.cooldown = cooldown;
			this.shieldAmount = shieldAmount;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			this.genericTimer -= dt;
		}

		public override void OnDeathSelf(Buff buff)
		{
			if (this.genericTimer <= 0f)
			{
				this.genericTimer = this.cooldown;
				List<UnitHealthy> list = buff.GetBy().GetAllies().ToList<UnitHealthy>();
				list.Remove(buff.GetBy() as UnitHealthy);
				if (list.Count > 0)
				{
					UnitHealthy unitHealthy = list[GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed)];
					unitHealthy.GainShield((double)this.shieldAmount, float.PositiveInfinity);
					unitHealthy.AddVisualBuff(3f, 256);
				}
			}
		}

		private float cooldown;

		private float shieldAmount;
	}
}
