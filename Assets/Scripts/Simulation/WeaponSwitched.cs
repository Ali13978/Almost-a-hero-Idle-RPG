using System;

namespace Simulation
{
	public class WeaponSwitched : Weapon
	{
		public override void Init(Hero by, World world)
		{
			base.Init(by, world);
			this.weapon_1.Init(by, world);
			this.weapon_2.Init(by, world);
			this.activeWeapon = this.weapon_1;
		}

		public override Weapon Clone()
		{
			return new WeaponSwitched
			{
				damageType = this.damageType,
				id = this.id,
				projectileIndexPattern = this.projectileIndexPattern,
				weapon_1 = this.weapon_1.Clone(),
				weapon_2 = this.weapon_2.Clone(),
				switchPattern = this.switchPattern
			};
		}

		public override double GetDps()
		{
			return this.activeWeapon.GetDps();
		}

		public override float GetBarTimeRatio()
		{
			return -1f;
		}

		public override float GetAnimTimeRatio()
		{
			return this.activeWeapon.GetAnimTimeRatio();
		}

		public override void UpdateActive(float dt)
		{
			int numHits = this.activeWeapon.GetNumHits();
			this.activeWeapon.UpdateActive(dt);
			if (numHits < this.activeWeapon.GetNumHits())
			{
				this.numHits++;
				this.weaponIndex = this.switchPattern[this.numHits % this.switchPattern.Length];
				if (this.weaponIndex == 0)
				{
					this.activeWeapon = this.weapon_1;
				}
				else
				{
					this.activeWeapon = this.weapon_2;
				}
			}
		}

		public override void UpdatePassive(float dt)
		{
			this.activeWeapon.UpdatePassive(dt);
		}

		public override void OnDied()
		{
			this.activeWeapon.OnDied();
		}

		public override void OnInterrupted()
		{
			this.activeWeapon.OnInterrupted();
		}

		public override bool IsActive()
		{
			return this.activeWeapon.IsActive();
		}

		public override void TryActivate()
		{
			this.activeWeapon.TryActivate();
		}

		public override void AttackImmediately(UnitHealthy unit)
		{
			this.activeWeapon.AttackImmediately(unit);
		}

		public Weapon weapon_1;

		public Weapon weapon_2;

		public int weaponIndex;

		private Weapon activeWeapon;

		public int[] switchPattern = new int[]
		{
			0,
			0,
			1,
			0,
			1,
			1,
			0,
			0,
			1,
			0,
			0,
			0,
			1
		};
	}
}
