using System;
using UnityEngine;

namespace Simulation
{
	public class TotemIce : Totem
	{
		public TotemIce(TotemDataBaseIce dataBase, int[] levelJumps, int level, int xp, World world) : base(levelJumps, world)
		{
			int progress = levelJumps[level] + xp;
			this.data = new TotemDataIce(dataBase, progress);
			this.level = level;
			this.xp = xp;
			this.attackTarget = null;
			this.mana = 0f;
			this.isGatheringMana = false;
			this.isHoldingWhileRaining = false;
			this.timeManaGather = float.NegativeInfinity;
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
			return this.isGatheringMana;
		}

		public override void UpdateStats(float dt)
		{
			base.UpdateStats(dt);
			this.statCache.UpdateTotemIce(this.data, this.buffTotalEffect, this.world.buffTotalEffect, this.world.universalBonus, this.world.activeChallenge.totalGainedUpgrades);
		}

		public override void UpdateTotem(float dt, Taps taps, bool autoTap)
		{
			base.UpdateTotem(dt, taps, autoTap);
			this.isHoldingWhileRaining = false;
			if (this.buffTotalEffect.totemHasShotAuto)
			{
				this.ThrowShard();
			}
			if (this.isGatheringMana)
			{
				bool flag = taps == null || taps.HasNone();
				if (flag && autoTap)
				{
					flag = (this.mana >= this.GetManaMax());
				}
				if (flag)
				{
					this.isGatheringMana = false;
					this.timeManaGather = 0f;
					this.world.AddSoundEvent(new SoundEventCancelBy(base.id));
					this.world.AddSoundEvent(new SoundEventSound(SoundType.GAMEPLAY, base.id, false, 0f, new SoundSimple(SoundArchieve.inst.totemIceChargeEnd, 1f, float.MaxValue)));
				}
				else
				{
					this.mana += this.GetManaGatherSpeed() * dt;
					this.mana = GameMath.GetMinFloat(this.GetManaMax(), this.mana);
					if (this.mana == this.GetManaMax())
					{
						base.OnIceManaFull();
					}
					this.timeManaGather += dt;
					this.posManaGather = ((!autoTap) ? taps.GetAny() : Taps.ICE_RING_AUTO_CHARGE_POS);
				}
			}
			else if (this.mana > 0f)
			{
				if (autoTap || (taps != null && taps.HasAny()))
				{
					this.isHoldingWhileRaining = true;
				}
				float minFloat = GameMath.GetMinFloat(this.mana, this.GetManaUseSpeed() * dt);
				this.mana -= this.GetManaUseSpeed() * dt * this.buffTotalEffect.totemIceManaSpendSpeedFactor;
				this.manaSpent += minFloat;
				if (this.mana <= 0f)
				{
					base.OnAfterIceShardRain();
					this.mana = 0f;
				}
				float shardReqMana = this.GetShardReqMana();
				while (this.manaSpent >= shardReqMana)
				{
					this.manaSpent -= shardReqMana;
					this.ThrowShard();
				}
				this.timeManaGather -= dt;
			}
			else if (autoTap || (taps != null && taps.HasAny()))
			{
				this.isGatheringMana = true;
				this.timeManaGather = 0f;
				this.mana = 5f;
				this.world.AddSoundEvent(new SoundEventSound(SoundType.GAMEPLAY, base.id, false, 0f, new SoundTotemIceCharge()));
			}
			else
			{
				this.timeManaGather -= dt;
			}
		}

		private void ThrowShard()
		{
			Projectile copy = this.data.projectileShard.GetCopy();
			copy.by = this;
			double num = base.GetDamage() * this.buffTotalEffect.damageAreaFactor;
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
			copy.damageArea = new Damage(num, probabilityOutcome, false, false, flag);
			copy.damageAreaR = this.GetShardAreaR();
			Vector3 vector = new Vector3(GameMath.GetRandomFloat(0.25f, 0.82f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.8f, 0.35f, GameMath.RandType.NoSeed));
			vector.x += AspectRatioOffset.ENEMY_X;
			copy.InitPath(vector, vector);
			copy.soundImpact = new SoundEventSound(SoundType.GAMEPLAY, base.id, false, 0f, new SoundVariedSimple(SoundArchieve.inst.totemIceStrikes, 0.6f));
			this.AddProjectile(copy);
		}

		public float GetManaMax()
		{
			return this.statCache.totemIceManaMax;
		}

		public float GetManaGatherSpeed()
		{
			return this.statCache.totemIceManaGatherSpeed;
		}

		public float GetManaUseSpeed()
		{
			return this.statCache.totemIceManaUseSpeed;
		}

		public float GetShardReqMana()
		{
			return this.statCache.totemIceShardReqMana;
		}

		public float GetShardAreaR()
		{
			return this.data.GetShardAreaR() * this.statCache.damageAreaRFactor;
		}

		public override double GetDps()
		{
			return base.GetDamage() * 10.0;
		}

		public override void AddIceMana(float amount)
		{
			this.mana = GameMath.GetMinFloat(this.mana + amount, this.GetManaMax());
		}

		public bool IsUsingMana()
		{
			return this.mana > 0f && !this.isGatheringMana;
		}

		public TotemDataIce data;

		public float mana;

		public float manaSpent;

		public bool isGatheringMana;

		public Vector3 posManaGather;

		public float timeManaGather;

		public bool isHoldingWhileRaining;
	}
}
