using System;

namespace Simulation
{
	public class BuffDataForgetful : BuffData
	{
		public override void OnNewWave(Buff buff)
		{
			if (!GameMath.GetProbabilityOutcome(this.chance, GameMath.RandType.NoSeed))
			{
				return;
			}
			Unit by = buff.GetBy();
			if (!(by is Hero))
			{
				return;
			}
			Hero hero = (Hero)by;
			hero.ZeroSkillCooldown(typeof(SkillDataBaseOutOfControl));
		}

		public float chance;
	}
}
