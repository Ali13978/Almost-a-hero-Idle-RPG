using System;

namespace Simulation
{
	public class RiftEffectStunningEnemies : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return -0.75f;
			}
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectStunningEnemies();
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_STUNNING_ENEMIES"), GameMath.GetPercentString(1f, false), GameMath.GetTimeInMilliSecondsString(0.4f));
		}

		public override void OnPostDamage(World world, Unit damager, UnitHealthy damaged, Damage damage)
		{
			if (damager is Enemy && damaged is Hero && GameMath.GetProbabilityOutcome(1f, GameMath.RandType.NoSeed) && !damaged.CanStunned())
			{
				damaged.AddBuff(new BuffDataStun
				{
					id = 317,
					dur = 0.4f
				}, 0, false);
			}
		}

		private const float chance = 1f;

		private const float duration = 0.4f;
	}
}
