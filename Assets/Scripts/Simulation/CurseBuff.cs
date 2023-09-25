using System;

namespace Simulation
{
	public abstract class CurseBuff : EnchantmentBuff
	{
		public CurseBuff()
		{
			this.state = EnchantmentBuffState.ACTIVE;
		}

		protected override bool TryActivating()
		{
			return true;
		}

		public override void AddProgress(float dp)
		{
			if (this.state == EnchantmentBuffState.ACTIVE)
			{
				this.progress += dp;
				if (this.progress >= 1f)
				{
					this.progress = 0f;
					this.state = EnchantmentBuffState.INACTIVE;
				}
			}
		}
	}
}
