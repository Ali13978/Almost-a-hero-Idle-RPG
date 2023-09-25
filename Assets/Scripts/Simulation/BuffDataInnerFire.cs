using System;

namespace Simulation
{
	public class BuffDataInnerFire : BuffData
	{
		public BuffDataInnerFire(double damageAdd, float duration)
		{
			this.damageAdd = damageAdd;
			this.duration = duration;
			this.id = 108;
		}

		public override void OnOverheated(Buff buff)
		{
			BuffDataDamageAdd buffDataDamageAdd = new BuffDataDamageAdd();
			buffDataDamageAdd.id = 273;
			buffDataDamageAdd.dur = this.duration;
			buffDataDamageAdd.damageAdd = this.damageAdd;
			buffDataDamageAdd.visuals |= 8;
			foreach (Hero hero in buff.GetWorld().heroes)
			{
				hero.AddBuff(buffDataDamageAdd, 0, false);
			}
		}

		private double damageAdd;

		private float duration;
	}
}
