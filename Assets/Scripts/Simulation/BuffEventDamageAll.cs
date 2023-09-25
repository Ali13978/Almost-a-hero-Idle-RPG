using System;

namespace Simulation
{
	public class BuffEventDamageAll : BuffEvent
	{
		public override void Apply(Unit by, World world)
		{
			double num = 0.0;
			if (this.damageInDps > 0.0)
			{
				num += by.GetDps() * this.damageInDps;
			}
			if (this.damageInTeamDps > 0.0)
			{
				num += by.GetDpsTeam() * this.damageInTeamDps;
			}
			Damage damage = new Damage(num, false, false, false, false);
			damage.type = this.damageType;
			if (GameMath.GetProbabilityOutcome(by.GetCritChance(), GameMath.RandType.NoSeed))
			{
				damage.amount *= by.GetCritFactor();
				damage.isCrit = true;
			}
			if (by is Enemy)
			{
				foreach (Hero damaged in world.heroes)
				{
					world.DamageMain(by, damaged, damage);
				}
			}
			else
			{
				damage.amount *= world.universalBonus.damageHeroSkillFactor / world.universalBonus.damageHeroNonSkillFactor;
				foreach (Enemy damaged2 in world.activeChallenge.enemies)
				{
					Damage copy = damage.GetCopy();
					world.DamageMain(by, damaged2, copy);
				}
			}
		}

		public double damageInDps;

		public double damageInTeamDps;

		public DamageType damageType;
	}
}
