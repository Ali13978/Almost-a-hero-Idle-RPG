using System;
using UnityEngine;

namespace Simulation
{
	public class TotemFire : Totem
	{
		public TotemFire(TotemDataBaseFire dataBase, int[] levelJumps, int level, int xp, World world) : base(levelJumps, world)
		{
			int progress = levelJumps[level] + xp;
			this.data = new TotemDataFire(dataBase, progress);
			this.level = level;
			this.xp = xp;
			this.startCoolTimer = 0f;
			this.oldAttackTarget = null;
			this.attackTarget = null;
			this.heat = 0f;
			this.isOverHeated = false;
			this.hasJustFired = false;
			this.autoTapTimeCounter = 0f;
			this.UpdateStats(0f);
		}

		public override TotemData GetData()
		{
			return this.data;
		}

		public override void UpdateData()
		{
			this.data.SetProgress(base.GetProgress());
		}

		public override bool CanAutoTapOnThisFrame(float dt)
		{
			return !this.isOverHeated;
		}

		public override void UpdateStats(float dt)
		{
			base.UpdateStats(dt);
			this.statCache.UpdateTotemFire(this.data, this.buffTotalEffect, this.world.buffTotalEffect, this.world.universalBonus, this.world.activeChallenge.totalGainedUpgrades);
		}

		public override void UpdateTotem(float dt, Taps taps, bool autoTap)
		{
			base.UpdateTotem(dt, taps, autoTap);
			this.hasJustFired = false;
			if (this.isOverHeated)
			{
				float num = (float)((!autoTap) ? 1 : 2);
				this.heat -= this.GetOverCoolSpeed() * dt * num;
				if (this.heat <= 0f)
				{
					this.heat = 0f;
					this.isOverHeated = false;
					base.OnOverheatFinished();
				}
				if (taps != null && taps.HasAtLeastOneNew())
				{
					VisualEffect visualEffect = new VisualEffect(VisualEffect.Type.TOTEM_FIRE_SMOKE, 0.433f);
					visualEffect.pos = taps.GetAnyNew();
					this.world.visualEffects.Add(visualEffect);
				}
			}
			else
			{
				if (this.startCoolTimer > 0f)
				{
					this.startCoolTimer -= dt;
				}
				else
				{
					this.heat -= this.GetCoolSpeed() * dt;
					if (this.heat < 0f)
					{
						this.heat = 0f;
					}
				}
				bool flag = false;
				int num2 = 0;
				if (autoTap)
				{
					this.autoTapTimeCounter += dt;
					if (this.autoTapTimeCounter >= 0.13f)
					{
						this.autoTapTimeCounter -= 0.13f;
						flag = true;
						while (this.autoTapTimeCounter > 0.13f)
						{
							this.autoTapTimeCounter -= 0.13f;
							num2++;
						}
					}
				}
				flag = (flag || (taps != null && taps.HasAtLeastOneNew()));
				if (flag)
				{
					Vector3 tapPos = (!autoTap) ? taps.GetAnyNew() : Taps.GetRandomAutoTapPos();
					this.Fire(tapPos);
				}
				for (int i = 0; i < num2; i++)
				{
					this.Fire(Taps.GetRandomAutoTapPos());
				}
			}
		}

		private void Fire(Vector3 tapPos)
		{
			this.attackTarget = this.world.GetClosestEnemy(tapPos);
			if (this.attackTarget == null)
			{
				VisualLinedEffect item = new VisualLinedEffect(VisualLinedEffect.Type.TOTEM_FIRE, tapPos, 0.33f, true);
				this.world.visualLinedEffects.Add(item);
			}
			else
			{
				this.heat += this.GetHeatPerFire();
				this.startCoolTimer = 0.1f;
				if (this.heat > this.GetHeatMax())
				{
					this.heat = this.GetHeatMax();
					this.isOverHeated = true;
					base.OnOverheated();
					SoundSimple sound = new SoundSimple(SoundArchieve.inst.totemFireOverheated, 1f, float.MaxValue);
					SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, base.id, false, 0f, sound);
					this.world.AddSoundEvent(e);
				}
				if (this.attackTarget != this.oldAttackTarget)
				{
					base.OnAttackTargetChanged(this.oldAttackTarget, this.attackTarget);
					this.oldAttackTarget = this.attackTarget;
				}
				double num = base.GetDamage();
				bool probabilityOutcome = GameMath.GetProbabilityOutcome(base.GetCritChance(), GameMath.RandType.NoSeed);
				if (probabilityOutcome)
				{
					num *= base.GetCritFactor();
				}
				bool flag = this.world.CanRingUltraCrit() && GameMath.GetProbabilityOutcome(this.world.universalBonus.ringUltraCritChance, GameMath.RandType.NoSeed);
				if (flag)
				{
					num *= this.world.universalBonus.ringUltraCritFactor;
					this.world.OnRingUltraCrit();
				}
				Damage damage = new Damage(num, probabilityOutcome, false, false, flag);
				this.world.DamageFuture(this, this.attackTarget, damage, 0.18f);
				VisualLinedEffect item2 = new VisualLinedEffect(VisualLinedEffect.Type.TOTEM_FIRE, tapPos, this.attackTarget.pos, 0.4f);
				this.world.visualLinedEffects.Add(item2);
				SoundEventSound e2 = new SoundEventSound(SoundType.GAMEPLAY, base.id, false, 0f, new SoundTotemThrow(1f));
				this.world.AddSoundEvent(e2);
				this.hasJustFired = true;
			}
		}

		public float GetHeatMax()
		{
			return this.statCache.totemHeatMax;
		}

		public float GetHeatPerFire()
		{
			return this.statCache.totemHeatPerFire;
		}

		public float GetCoolSpeed()
		{
			return this.statCache.totemCoolSpeed;
		}

		public float GetOverCoolSpeed()
		{
			return this.statCache.totemOverCoolSpeed;
		}

		public override void MultiplyTotemHeat(float factor)
		{
			this.heat *= factor;
		}

		public override double GetDps()
		{
			return base.GetDamage() * 10.0;
		}

		private const float AUTO_TAP_TIME_REQ_PER_SHOT = 0.13f;

		private float autoTapTimeCounter;

		public TotemDataFire data;

		private const float START_COOL_DURATION = 0.1f;

		public float heat;

		public float startCoolTimer;

		public bool isOverHeated;

		public bool hasJustFired;
	}
}
