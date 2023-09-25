using System;
using UnityEngine;

namespace Simulation
{
	public class BuffDataLightning : BuffData
	{
		public BuffDataLightning(double damageRatio, float cooldown)
		{
			this.damageRatio = damageRatio;
			this.cooldown = cooldown;
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

		public override void OnTakenDamage(Buff buff, Unit attacker, Damage damage)
		{
			if (this.genericFlag)
			{
				UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
				if (!unitHealthy.IsAlly(attacker))
				{
					this.genericFlag = false;
					this.CastLightning(buff.GetBy(), attacker as UnitHealthy);
					this.timerVisual = 0.75f;
				}
			}
		}

		private bool CastLightning(Unit by, UnitHealthy attackTarget)
		{
			World world = by.world;
			if (world.totem == null)
			{
				return false;
			}
			if (attackTarget == null)
			{
				return false;
			}
			double num = world.totem.GetDamage() * this.damageRatio;
			bool probabilityOutcome = GameMath.GetProbabilityOutcome(by.GetCritChance(), GameMath.RandType.NoSeed);
			if (probabilityOutcome)
			{
				num *= by.GetCritFactor();
			}
			bool flag = world.CanRingUltraCrit() && GameMath.GetProbabilityOutcome(world.universalBonus.ringUltraCritChance, GameMath.RandType.NoSeed);
			if (flag)
			{
				num *= world.universalBonus.ringUltraCritFactor;
				world.OnRingUltraCrit();
			}
			Damage damage = new Damage(num, probabilityOutcome, false, false, flag);
			world.DamageMain(world.totem, attackTarget, damage);
			VisualLinedEffect item = new VisualLinedEffect(VisualLinedEffect.Type.TOTEM_LIGHTNING, by.pos + new Vector3(0f, by.GetHeight() * 0.5f, 0f), attackTarget.pos + new Vector3(0f, attackTarget.GetHeight() * 0.5f, 0f), 0.25f);
			by.world.visualLinedEffects.Add(item);
			SoundVariedSimple sound = new SoundVariedSimple(SoundArchieve.inst.lightnings, GameMath.GetRandomFloat(0.5f, 0.85f, GameMath.RandType.NoSeed));
			SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, by.GetId(), false, 0f, sound);
			by.world.AddSoundEvent(e);
			return true;
		}

		private double damageRatio;

		private float cooldown;

		private float timerVisual;
	}
}
