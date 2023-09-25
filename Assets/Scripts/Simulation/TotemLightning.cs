using System;
using UnityEngine;

namespace Simulation
{
	public class TotemLightning : Totem
	{
		public TotemLightning(TotemDataBaseLightning dataBase, int[] levelJumps, int level, int xp, World world) : base(levelJumps, world)
		{
			int progress = levelJumps[level] + xp;
			this.data = new TotemDataLightning(dataBase, progress);
			this.level = level;
			this.xp = xp;
			this.oldAttackTarget = null;
			this.attackTarget = null;
			this.charge = 0;
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

		public override void UpdateStats(float dt)
		{
			base.UpdateStats(dt);
			this.statCache.UpdateTotemLightning(this.data, this.buffTotalEffect, this.world.buffTotalEffect, this.world.universalBonus, this.world.activeChallenge.totalGainedUpgrades);
		}

		public override bool CanAutoTapOnThisFrame(float dt)
		{
			return this.autoTapTimeCounter + dt > 0.08f;
		}

		public override void UpdateTotem(float dt, Taps taps, bool autoTap)
		{
			base.UpdateTotem(dt, taps, autoTap);
			this.hasJustFired = false;
			this.hasJustFiredThunder = false;
			bool flag = false;
			int num = 0;
			if (autoTap)
			{
				this.autoTapTimeCounter += dt;
				if (this.autoTapTimeCounter > 0.08f)
				{
					this.autoTapTimeCounter -= 0.08f;
					flag = true;
					while (this.autoTapTimeCounter > 0.08f)
					{
						this.autoTapTimeCounter -= 0.08f;
						num++;
					}
				}
			}
			if (this.buffTotalEffect.totemHasShotAuto)
			{
				if (flag)
				{
					num++;
				}
				else
				{
					flag = true;
				}
			}
			bool flag2 = flag || (taps != null && taps.HasAtLeastOneNew());
			if (flag2 && TutorialManager.CanTotemFire())
			{
				Vector3 tapPos = (!flag) ? taps.GetAnyNew() : Taps.GetRandomAutoTapPos();
				bool flag3 = this.CastLightning(tapPos);
				if (flag)
				{
					this.charge++;
				}
				if (taps != null)
				{
					this.charge += taps.GetNumNew();
				}
				if (this.charge >= this.GetChargeReq())
				{
					flag3 = this.CastThunder(tapPos);
					if (flag3)
					{
						this.charge = 0;
					}
				}
			}
			for (int i = 0; i < num; i++)
			{
				Vector3 randomAutoTapPos = Taps.GetRandomAutoTapPos();
				bool flag4 = this.CastLightning(randomAutoTapPos);
				if (flag)
				{
					this.charge++;
				}
				if (this.charge >= this.GetChargeReq())
				{
					flag4 = this.CastThunder(randomAutoTapPos);
					if (flag4)
					{
						this.charge = 0;
					}
				}
			}
		}

		private bool CastLightning(Vector3 tapPos)
		{
			this.attackTarget = this.world.GetClosestEnemy(tapPos);
			if (this.attackTarget == null)
			{
				return false;
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
			base.OnPreLightning(this.attackTarget, damage);
			this.world.DamageMain(this, this.attackTarget, damage);
			base.OnAfterLightning(this.attackTarget, damage);
			VisualLinedEffect item = new VisualLinedEffect(VisualLinedEffect.Type.TOTEM_LIGHTNING, tapPos, this.attackTarget.pos, 0.2f);
			this.world.visualLinedEffects.Add(item);
			SoundVariedSimple sound = new SoundVariedSimple(SoundArchieve.inst.lightnings, GameMath.GetRandomFloat(0.5f, 0.85f, GameMath.RandType.NoSeed));
			SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, base.id, false, 0f, sound);
			this.world.AddSoundEvent(e);
			this.hasJustFired = true;
			return true;
		}

		private bool CastThunder(Vector3 tapPos)
		{
			this.attackTarget = this.world.GetClosestEnemy(tapPos);
			if (this.attackTarget == null)
			{
				return false;
			}
			double num = base.GetDamage() * 8.0;
			bool probabilityOutcome = GameMath.GetProbabilityOutcome(base.GetCritChance(), GameMath.RandType.NoSeed);
			if (probabilityOutcome)
			{
				num *= base.GetCritFactor();
			}
			Damage damage = new Damage(num, probabilityOutcome, false, false, false);
			base.OnPreThunderbolt(this.attackTarget, damage, false);
			this.world.DamageMain(this, this.attackTarget, damage);
			base.OnAfterThunderbolt(this.attackTarget, damage, false);
			VisualEffect visualEffect = new VisualEffect(VisualEffect.Type.TOTEM_THUNDERBOLT, 0.5f);
			visualEffect.pos = this.attackTarget.pos;
			this.world.visualEffects.Add(visualEffect);
			SoundVariedSimple sound = new SoundVariedSimple(SoundArchieve.inst.thunderbolts, GameMath.GetRandomFloat(0.6f, 0.85f, GameMath.RandType.NoSeed));
			SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, base.id, false, 0f, sound);
			this.world.AddSoundEvent(e);
			this.hasJustFiredThunder = true;
			return true;
		}

		public override double GetDps()
		{
			return base.GetDamage() * 10.0;
		}

		public int GetChargeReq()
		{
			return this.statCache.totemChargeReq;
		}

		public override void AddCharge(int amount)
		{
			this.charge += amount;
			if (this.charge > this.GetChargeReq())
			{
				this.charge = 0;
				this.CastThunder(World.ENEMY_CENTER);
			}
		}

		public int GetCharge()
		{
			return this.charge;
		}

		private const float AUTO_TAP_TIME_REQ_PER_SHOT = 0.08f;

		private const double THUNDER_MULTIPLIER = 8.0;

		private float autoTapTimeCounter;

		public TotemDataLightning data;

		private int charge;

		public bool hasJustFired;

		public bool hasJustFiredThunder;
	}
}
