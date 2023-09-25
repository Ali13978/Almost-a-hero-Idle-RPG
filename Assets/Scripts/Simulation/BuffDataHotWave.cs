using System;
using System.Collections.Generic;
using System.Linq;

namespace Simulation
{
	public class BuffDataHotWave : BuffData
	{
		public BuffDataHotWave(double percentage, float radius)
		{
			this.percentage = percentage;
			this.r2 = radius * radius;
			this.id = 43;
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			List<UnitHealthy> list = target.GetAllies().ToList<UnitHealthy>();
			list.Remove(target);
			Damage damage2 = new Damage(damage.amount * this.percentage, false, false, false, false);
			foreach (UnitHealthy unitHealthy in list)
			{
				if (GameMath.AreInsideRangeXY(target.pos, unitHealthy.pos, this.r2))
				{
					unitHealthy.TakeDamage(damage2, null, 0.0);
				}
			}
		}

		public double percentage;

		public float r2;
	}
}
