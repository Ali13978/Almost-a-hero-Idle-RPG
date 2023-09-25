using System;

namespace Simulation
{
	public class BuffDataRainOfAxes : BuffData
	{
		public override void OnPreProjectile(Buff buff, Projectile projectile)
		{
			int num = 0;
			foreach (UnitHealthy unitHealthy in buff.GetBy().GetAllies())
			{
				if (!unitHealthy.IsAlive())
				{
					num++;
				}
			}
			projectile.type = Projectile.Type.SAM_AXE;
			projectile.InitPath();
			projectile.damage.amount = (this.damageInTeamDps + this.damageBonusPerDeadHero * (double)num) * buff.GetBy().GetDpsTeam();
			buff.DecreaseLifeCounter();
			if (buff.GetLifeCounter() == 0f)
			{
				Unit by = buff.GetBy();
				if (!(by is Hero))
				{
					return;
				}
				Hero hero = (Hero)by;
				hero.ChangeWeaponToOrig(this.durChangeWeaponToOrig);
			}
		}

		public double damageInTeamDps;

		public double damageBonusPerDeadHero;

		public float durChangeWeaponToOrig;
	}
}
