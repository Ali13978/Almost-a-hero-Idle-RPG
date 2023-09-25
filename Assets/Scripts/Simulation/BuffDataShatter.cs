using System;

namespace Simulation
{
	public class BuffDataShatter : BuffData
	{
		public BuffDataShatter(float damageDec, float manaReq)
		{
			this.damageDec = damageDec;
			this.manaReq = manaReq;
			this.id = 159;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.totemIceShardReqManaFactor -= this.manaReq;
			totEffect.damageAddFactor -= (double)this.damageDec;
		}

		private float damageDec;

		private float manaReq;
	}
}
