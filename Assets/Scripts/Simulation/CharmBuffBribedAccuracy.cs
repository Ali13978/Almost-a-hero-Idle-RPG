using System;

namespace Simulation
{
	public class CharmBuffBribedAccuracy : CharmBuff
	{
		protected override bool TryActivating()
		{
			foreach (Hero hero in this.world.heroes)
			{
				if (hero.IsAlive())
				{
					BuffDataCritChance buffDataCritChance = new BuffDataCritChance();
					buffDataCritChance.id = 297;
					buffDataCritChance.dur = this.dur;
					buffDataCritChance.isStackable = true;
					buffDataCritChance.critChanceAdd = this.critChanceIncrease;
					buffDataCritChance.visuals |= 4;
					hero.AddBuff(buffDataCritChance, 0, false);
				}
			}
			return true;
		}

		public override void OnCollectGold()
		{
			this.AddProgress(this.pic);
		}

		public float critChanceIncrease;

		public float dur;
	}
}
