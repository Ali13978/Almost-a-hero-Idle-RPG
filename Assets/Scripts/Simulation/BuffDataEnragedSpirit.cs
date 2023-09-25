using System;

namespace Simulation
{
	public class BuffDataEnragedSpirit : BuffData
	{
		public override void OnOverheated(Buff buff)
		{
			Unit by = buff.GetBy();
			Projectile copy = this.projectile.GetCopy();
			copy.damage = new Damage(this.damageInDps * by.GetDps(), false, false, false, false);
			by.AddProjectile(copy);
		}

		public Projectile projectile;

		public double damageInDps;
	}
}
