using System;

namespace Simulation
{
	public class BuffEventDamageRandom : BuffEvent
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
			UnitHealthy unitHealthy;
			if (by is Enemy)
			{
				unitHealthy = world.GetRandomHero();
			}
			else
			{
				damage.amount *= world.universalBonus.damageHeroSkillFactor / world.universalBonus.damageHeroNonSkillFactor;
				unitHealthy = world.GetRandomEnemy();
			}
			if (unitHealthy != null)
			{
				world.DamageMain(by, unitHealthy, damage);
			}
		}

		public double damageInDps;

		public double damageInTeamDps;
	}
}
