using System;

namespace Simulation
{
	public class CharmBuffPermanent : CharmBuff
	{
		public override void Update(float dt)
		{
			if ((this.state == EnchantmentBuffState.INACTIVE || this.state == EnchantmentBuffState.READY) && this.TryActivating())
			{
				this.state = EnchantmentBuffState.ACTIVE;
				this.stateTime = 0f;
			}
			this.OnUpdate(dt);
		}

		public override void AddProgress(float dp)
		{
		}

		public override float GetActivationStateRate()
		{
			return 0.5f;
		}

		protected override bool TryActivating()
		{
			return true;
		}
	}
}
