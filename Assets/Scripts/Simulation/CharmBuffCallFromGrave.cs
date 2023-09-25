using System;

namespace Simulation
{
	public class CharmBuffCallFromGrave : CharmBuff
	{
		protected override bool TryActivating()
		{
			foreach (Hero hero in this.world.heroes)
			{
				hero.AddBuff(new BuffDataReviveSpeed
				{
					id = 312,
					isStackable = true,
					reviveSpeedFactorAdd = this.reduction,
					visuals = 32768,
					dur = this.dur
				}, 0, false);
			}
			return true;
		}

		public override void OnDeathAny(Unit unit)
		{
			if (!(unit is Hero))
			{
				return;
			}
			this.AddProgress(this.pic);
		}

		public float reduction;

		public float dur;
	}
}
