using System;

namespace Simulation
{
	public class SkillEventDamageAll : SkillEvent
	{
		public override void Apply(Unit by)
		{
			double num = 0.0;
			if (this.damageInDps > 0.0)
			{
				num += this.damageInDps * by.GetDps();
			}
			if (this.damageInTeamDps > 0.0)
			{
				num += this.damageInTeamDps * by.GetDpsTeam();
			}
			bool isCrit = false;
			if (this.canCrit && GameMath.GetProbabilityOutcome(by.GetCritChance(), GameMath.RandType.NoSeed))
			{
				num *= by.GetCritFactor();
				isCrit = true;
			}
			by.DamageAll(new Damage(num, false, false, false, false)
			{
				isCrit = isCrit,
				type = this.damageType
			});
		}

		public override void Cancel(Unit by, float timePassedSinceActivation)
		{
		}

		public double damageInDps;

		public double damageInTeamDps;

		public DamageType damageType;

		public bool canCrit;
	}
}
