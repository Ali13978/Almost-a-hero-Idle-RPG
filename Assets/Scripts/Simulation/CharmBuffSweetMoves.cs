using System;

namespace Simulation
{
	public class CharmBuffSweetMoves : CharmBuff
	{
		protected override bool TryActivating()
		{
			foreach (Hero hero in this.world.heroes)
			{
				if (hero.IsAlive())
				{
					BuffDataDodge buffDataDodge = new BuffDataDodge();
					buffDataDodge.id = 299;
					buffDataDodge.dur = this.dur;
					buffDataDodge.isStackable = false;
					buffDataDodge.dodgeChanceAdd = this.dodgeChanceIncrease;
					buffDataDodge.visuals |= 16384;
					hero.AddBuff(buffDataDodge, 0, false);
				}
			}
			return true;
		}

		public override void OnWavePassed()
		{
			this.AddProgress(this.pic);
		}

		public float dodgeChanceIncrease;

		public float dur;
	}
}
