using System;

namespace Simulation
{
	public class BuffDataBlaze : BuffData
	{
		public BuffDataBlaze(double reduction, float duration)
		{
			this.id = 16;
			this.effect = new BuffDataDamageAdd();
			this.effect.id = 35;
			this.effect.dur = duration;
			this.effect.isStackable = false;
			this.effect.damageAdd = -reduction;
			this.effect.visuals |= 16;
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (damage.isCrit)
			{
				target.AddBuff(this.effect, 0, false);
			}
		}

		public BuffDataDamageAdd effect;
	}
}
