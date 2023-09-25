using System;

namespace Simulation
{
	public class BuffDataSwiftMoves : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.dodgeChanceAdd += this.dodgeChanceAdd;
			totEffect.critChanceAdd += this.critChanceAdd;
		}

		public float dodgeChanceAdd;

		public float critChanceAdd;
	}
}
