using System;

namespace Simulation
{
	public abstract class CurseBuffPermanent : CurseBuff
	{
		public CurseBuffPermanent()
		{
			this.state = EnchantmentBuffState.ACTIVE;
		}

		public override void AddProgress(float dp)
		{
		}
	}
}
