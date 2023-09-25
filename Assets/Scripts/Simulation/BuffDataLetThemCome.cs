using System;

namespace Simulation
{
	public class BuffDataLetThemCome : BuffData
	{
		public override void OnNewWave(Buff buff)
		{
			int count = buff.GetWorld().activeChallenge.enemies.Count;
			BuffDataDamageAdd buffDataDamageAdd = new BuffDataDamageAdd();
			buffDataDamageAdd.id = 39;
			buffDataDamageAdd.isStackable = true;
			buffDataDamageAdd.dur = this.effectDur;
			buffDataDamageAdd.damageAdd = (double)count * this.damageAddPerEnemy;
			buff.GetBy().AddBuff(buffDataDamageAdd, 0, false);
		}

		public float effectDur;

		public double damageAddPerEnemy;
	}
}
