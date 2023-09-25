using System;

namespace Simulation
{
	public class BuffDataBigGuy : BuffData
	{
		public override void OnTakenDamage(Buff buff, Unit attacker, Damage damage)
		{
			Hero hero = null;
			float num = 0f;
			foreach (Hero hero2 in buff.GetWorld().heroes)
			{
				if (hero2.IsDead() && hero2.GetTillReviveTime() > num)
				{
					hero = hero2;
					num = hero2.GetTillReviveTime();
				}
			}
			if (hero != null)
			{
				hero.SetTillReviveTime(GameMath.GetMaxFloat(0f, num - this.reviveDecPerHit));
			}
		}

		public float reviveDecPerHit;
	}
}
