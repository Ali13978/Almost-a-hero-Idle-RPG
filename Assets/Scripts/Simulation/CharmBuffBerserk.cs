using System;

namespace Simulation
{
	public class CharmBuffBerserk : CharmBuff
	{
		protected override bool TryActivating()
		{
			foreach (Hero hero in this.world.heroes)
			{
				if (hero.IsAlive())
				{
					BuffDataAttackSpeed buffDataAttackSpeed = new BuffDataAttackSpeed();
					buffDataAttackSpeed.id = 296;
					buffDataAttackSpeed.dur = this.dur;
					buffDataAttackSpeed.isStackable = true;
					buffDataAttackSpeed.attackSpeedAdd = this.attackSpeedIncrease;
					buffDataAttackSpeed.reloadSpeedAdd = this.attackSpeedIncrease * 0.5f;
					buffDataAttackSpeed.visuals |= 1;
					hero.AddBuff(buffDataAttackSpeed, 0, false);
				}
			}
			return true;
		}

		public override void OnPreDamage(Unit damager, UnitHealthy damaged, Damage damage)
		{
			if (!(damager is Enemy))
			{
				return;
			}
			this.AddProgress(this.pic);
		}

		public float attackSpeedIncrease;

		public float dur;
	}
}
