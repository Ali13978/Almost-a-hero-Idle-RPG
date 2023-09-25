using System;

namespace Simulation
{
	public class CharmBuffExtremeImpatience : CharmBuff
	{
		protected override bool TryActivating()
		{
			foreach (Hero hero in this.world.heroes)
			{
				if (!hero.IsDead())
				{
					hero.AddBuff(new BuffDataAccelerateUltiCd
					{
						id = 301,
						dur = this.dur,
						isStackable = true,
						skillCooldownFactor = this.reduction
					}, 0, false);
				}
			}
			return true;
		}

		public override void OnDodgeAny(Unit dodger)
		{
			if (!(dodger is Hero))
			{
				return;
			}
			this.AddProgress(this.pic);
		}

		public float reduction;

		public float dur;
	}
}
