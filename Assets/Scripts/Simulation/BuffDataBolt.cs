using System;
using UnityEngine;

namespace Simulation
{
	public class BuffDataBolt : BuffData
	{
		public BuffDataBolt(double damageRatio, float cooldown)
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
			if (this.castThunderDelay > 0f)
			{
				this.castThunderDelay -= dt;
				if (this.castThunderDelay <= 0f)
				{
					this.CastThunder(buff.GetBy());
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

		public override void OnCastSpellSelf(Buff buff, Skill skill)
		{
			if (this.genericFlag)
			{
				this.genericFlag = false;
				this.timerVisual = 1f;
				this.castThunderDelay = 1f;
			}
		}

		private bool CastThunder(Unit by)
		{
			World world = by.world;
			if (world.totem == null)
			{
				return false;
			}
			Vector3 pos = by.pos;
			if (world.GetClosestEnemy(pos) == null)
			{
				return false;
			}
			foreach (Enemy enemy in world.activeChallenge.enemies)
			{
				if (!enemy.IsDead())
				{
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
					world.DamageMain(world.totem, enemy, damage);
					VisualEffect visualEffect = new VisualEffect(VisualEffect.Type.TOTEM_THUNDERBOLT, 0.5f);
					visualEffect.pos = enemy.pos;
					world.visualEffects.Add(visualEffect);
				}
			}
			SoundVariedSimple sound = new SoundVariedSimple(SoundArchieve.inst.thunderbolts, GameMath.GetRandomFloat(0.6f, 0.85f, GameMath.RandType.NoSeed));
			SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, by.GetId(), false, 0f, sound);
			world.AddSoundEvent(e);
			return true;
		}

		private double damageRatio;

		private float cooldown;

		private float timerVisual;

		private float castThunderDelay;
	}
}
