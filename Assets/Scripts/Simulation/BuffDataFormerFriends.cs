using System;

namespace Simulation
{
	public class BuffDataFormerFriends : BuffData
	{
		public override void OnNewEnemies(Buff buff)
		{
			foreach (Enemy enemy in buff.GetWorld().activeChallenge.enemies)
			{
				if (enemy.IsBoss())
				{
					this.effect.visuals |= 16;
					this.effect.dur = this.duration;
					this.effect.damageAdd = (double)(this.reduction * -1f);
					enemy.AddBuff(this.effect, 0, false);
				}
			}
		}

		public BuffDataDamageAdd effect;

		public float duration;

		public float reduction;
	}
}
