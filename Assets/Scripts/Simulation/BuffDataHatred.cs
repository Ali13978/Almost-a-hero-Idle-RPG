using System;

namespace Simulation
{
	public class BuffDataHatred : BuffData
	{
		public BuffDataHatred(float attackSpeed, double lessDamage, float cooldown)
		{
			this.attackSpeed = attackSpeed;
			this.lessDamage = lessDamage;
			this.cooldown = cooldown;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			if (this.enemyChosen != null && !this.enemyChosen.IsAlive())
			{
				this.enemyChosen = null;
			}
			if (this.genericFlag)
			{
				return;
			}
			this.genericTimer += dt;
			if (this.genericTimer >= this.cooldown)
			{
				this.genericTimer = 0f;
				this.genericFlag = true;
			}
		}

		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (this.enemyChosen == target)
			{
				damage.amount *= 1.0 - this.lessDamage;
			}
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (this.genericFlag && this.enemyChosen == null && target.IsAlive())
			{
				this.enemyChosen = target;
				this.genericFlag = false;
				BuffDataAttackSpeedCounted buffDataAttackSpeedCounted = new BuffDataAttackSpeedCounted();
				buffDataAttackSpeedCounted.isStackable = false;
				buffDataAttackSpeedCounted.id = 11;
				buffDataAttackSpeedCounted.dur = float.PositiveInfinity;
				buffDataAttackSpeedCounted.lifeCounter = 1;
				buffDataAttackSpeedCounted.attackSpeedFactor = this.attackSpeed;
				buffDataAttackSpeedCounted.visuals = 4096;
				buff.GetBy().AddBuff(buffDataAttackSpeedCounted, 0, false);
			}
		}

		private float attackSpeed;

		private double lessDamage;

		private float cooldown;

		private UnitHealthy enemyChosen;
	}
}
