using System;

namespace Simulation
{
	public class BuffDataHemorrhage : BuffData
	{
		public BuffDataHemorrhage(double damageTotal, float cooldown, float bleedDuration)
		{
			this.damageMultiplier = damageTotal;
			this.cooldown = cooldown;
			this.bleedDuration = bleedDuration;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			if (this.timerVisual > 0f)
			{
				this.visuals = 4096;
				this.timerVisual -= dt;
			}
			else
			{
				this.visuals = 0;
			}
			if (this.enemyChosen != null)
			{
				if (!this.enemyChosen.IsAlive())
				{
					this.enemyChosen = null;
				}
				else
				{
					this.timerTick += dt;
					this.timerBleed += dt;
					while (this.timerTick >= 0.25f)
					{
						this.timerTick -= 0.25f;
						Damage damage = new Damage(this.damageTotalAmount / (double)this.bleedDuration * 0.25, false, false, false, false);
						this.enemyChosen.TakeDamage(damage, buff.GetBy(), 0.0);
					}
					if (this.timerBleed >= this.bleedDuration)
					{
						this.enemyChosen = null;
					}
				}
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
			if (target.IsAlive() && this.genericFlag)
			{
				UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
				if (!unitHealthy.IsAlly(target))
				{
					this.timerVisual = 1f;
					this.genericFlag = false;
					this.damageTotalAmount = damage.amount * this.damageMultiplier;
					this.enemyChosen = target;
					this.timerBleed = 0f;
					this.timerTick = 0.25f;
				}
			}
		}

		public const float EVERY_TICK = 0.25f;

		private double damageMultiplier;

		private float cooldown;

		private double damageTotalAmount;

		private UnitHealthy enemyChosen;

		private float timerBleed;

		private float timerTick;

		private float timerVisual;

		private float bleedDuration;
	}
}
