using System;

namespace Simulation
{
	public class BuffDataDischarge : BuffData
	{
		public BuffDataDischarge(float chance)
		{
			this.chance = chance;
			this.id = 64;
		}

		public override void OnAfterThunderbolt(Buff buff, UnitHealthy target, Damage damage, bool isSecondary)
		{
			if (isSecondary)
			{
				return;
			}
			if (!GameMath.GetProbabilityOutcome(this.chance, GameMath.RandType.NoSeed))
			{
				return;
			}
			foreach (UnitHealthy unitHealthy in buff.GetBy().GetOpponents())
			{
				if (unitHealthy != target && unitHealthy.IsAttackable())
				{
					Damage copy = damage.GetCopy();
					buff.GetWorld().DamageMain(buff.GetBy(), unitHealthy, copy);
					VisualEffect visualEffect = new VisualEffect(VisualEffect.Type.TOTEM_THUNDERBOLT, 0.5f);
					visualEffect.pos = unitHealthy.pos;
					buff.GetBy().OnPreThunderbolt(target, copy, true);
					buff.GetWorld().visualEffects.Add(visualEffect);
					buff.GetBy().OnAfterThunderbolt(target, copy, true);
				}
			}
		}

		private float chance;
	}
}
