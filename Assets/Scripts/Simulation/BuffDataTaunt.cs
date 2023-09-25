using System;

namespace Simulation
{
	public class BuffDataTaunt : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.tauntAdd += this.tauntAdd;
		}

		private const float TauntStep = 1000f;

		public static float TauntAgroBabuUlti = 1000000f;

		public static float TauntAgroThourPassive = 50000f;

		public float tauntAdd;
	}
}
