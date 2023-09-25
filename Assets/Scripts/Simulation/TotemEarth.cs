using System;
using UnityEngine;

namespace Simulation
{
	public class TotemEarth : Totem
	{
		public TotemEarth(TotemDataBaseEarth dataBase, int[] levelJumps, int level, int xp, World world) : base(levelJumps, world)
		{
			int progress = levelJumps[level] + xp;
			this.data = new TotemDataEarth(dataBase, progress);
			this.level = level;
			this.xp = xp;
			this.attackTarget = null;
			this.timeCharged = 0f;
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
			this.statCache.UpdateTotemEarth(this.data, this.buffTotalEffect, this.world.buffTotalEffect, this.world.universalBonus, this.world.activeChallenge.totalGainedUpgrades);
		}

		public override bool CanAutoTapOnThisFrame(float dt)
		{
			return this.timeCharged + dt >= this.GetTimeChargedMax();
		}

		public override void UpdateTotem(float dt, Taps taps, bool autoTap)
		{
			base.UpdateTotem(dt, taps, autoTap);
			this.timeCharged += dt;
			if ((taps != null && taps.HasAtLeastOneNew()) || autoTap)
			{
				if (this.timeCharged >= this.GetTimeChargedMax())
				{
					this.timeCharged = 0f;
					this.numMeteorsWaiting += this.GetMaxNumMeteors();
					this.timerMeteor = 0f;
					this.timeChargedWithTap = 0f;
					if (this.buffTotalEffect.totemEarthMeteoriteTap)
					{
						this.numMeteoritesWaiting += RuneRemnants.NUM_METEORITES;
						this.timerMeteorite = this.GetMeteorPeriod(this.numMeteoritesWaiting) * 0.6f;
					}
					base.OnMeteorShower();
					if (taps != null && taps.HasAtLeastOneNew())
					{
						VisualEffect visualEffect = new VisualEffect(VisualEffect.Type.TOTEM_EARTH_TAP, 0.267f);
						visualEffect.pos = taps.GetAnyNew();
						visualEffect.scale = 2f;
						this.world.visualEffects.Add(visualEffect);
					}
					SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, base.id, false, 0f, new SoundVariedSimple(SoundArchieve.inst.earthRingTap, 1f));
					this.world.AddSoundEvent(e);
				}
				else
				{
					if (taps != null && taps.HasAtLeastOneNew())
					{
						VisualEffect visualEffect2 = new VisualEffect(VisualEffect.Type.TOTEM_EARTH_TAP_DISABLE, 0.433f);
						visualEffect2.pos = taps.GetAnyNew();
						this.world.visualEffects.Add(visualEffect2);
						SoundEventSound e2 = new SoundEventSound(SoundType.GAMEPLAY, base.id, false, 0f, new SoundVariedSimple(SoundArchieve.inst.earthRingTapDisabled, 1f));
						this.world.AddSoundEvent(e2);
					}
					if (this.buffTotalEffect.totemEarthTapRecharge != 0f)
					{
						float num;
						if (this.buffTotalEffect.totemEarthTapRecharge <= this.GetTimeChargedMax() * RuneWishful.MAX_DURATION_RECHARGE_RATIO - this.timeChargedWithTap)
						{
							num = this.buffTotalEffect.totemEarthTapRecharge;
						}
						else
						{
							num = this.GetTimeChargedMax() * RuneWishful.MAX_DURATION_RECHARGE_RATIO - this.timeChargedWithTap;
						}
						this.timeChargedWithTap += num;
						this.timeCharged += num;
					}
				}
			}
			if (this.numMeteorsWaiting > 0)
			{
				this.timerMeteor -= dt;
				if (this.timerMeteor <= 0f)
				{
					this.timerMeteor = this.GetMeteorPeriod(this.numMeteorsWaiting);
					this.numMeteorsWaiting--;
					this.ThrowMeteor();
				}
			}
			if (this.numMeteoritesWaiting > 0)
			{
				this.timerMeteorite -= dt;
				if (this.timerMeteorite <= 0f)
				{
					this.timerMeteorite = this.GetMeteorPeriod(this.numMeteoritesWaiting) * 0.6f;
					this.numMeteoritesWaiting--;
					this.ThrowMeteorite(RuneRemnants.DAMAGE_RATIO);
				}
			}
			if (this.buffTotalEffect.totemEarthMeteoriteAuto)
			{
				this.buffTotalEffect.totemEarthMeteoriteAuto = false;
				this.ThrowMeteorite(RuneMeteorite.DMG_METEORITES);
			}
		}

		private float GetMeteorPeriod(int numWaiting)
		{
			return (numWaiting <= 5) ? 0.6f : Mathf.Max(0.15f, 0.6f - (float)(numWaiting - 5) * 0.03f);
		}

		public float GetTimeChargedMax()
		{
			return this.data.GetTimeChargedMax() + this.buffTotalEffect.totemEarthDurationAdd;
		}

		private void ThrowMeteor()
		{
			SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, base.id, false, 0f, new SoundVariedSimple(SoundArchieve.inst.earthRingMeteor, 1f));
			this.world.AddSoundEvent(e);
			Projectile copy = this.data.projectile.GetCopy();
			copy.by = this;
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
			copy.damageArea = new Damage(num, probabilityOutcome, false, false, flag);
			copy.damageAreaR = this.GetMeteorAreaR();
			Vector3 vector = new Vector3(GameMath.GetRandomFloat(0.25f, 0.82f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.8f, 0.35f, GameMath.RandType.NoSeed));
			vector.x += AspectRatioOffset.ENEMY_X;
			copy.InitPath(vector, vector);
			copy.soundImpact = new SoundEventSound(SoundType.GAMEPLAY, base.id, false, 0f, new SoundVariedSimple(SoundArchieve.inst.earthRingMeteorImpact, 1f));
			copy.visualEffect = new VisualEffect(VisualEffect.Type.TOTEM_EARTH_IMPACT, 1.33f);
			this.AddProjectile(copy);
		}

		private bool ThrowMeteorite(double damageRatio)
		{
			UnitHealthy randomAliveEnemy = this.world.GetRandomAliveEnemy();
			if (randomAliveEnemy == null)
			{
				return false;
			}
			SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, base.id, false, 0f, new SoundVariedSimple(SoundArchieve.inst.earthRingMeteor, 0.4f));
			this.world.AddSoundEvent(e);
			Projectile copy = this.data.projectileMeteorite.GetCopy();
			copy.by = this;
			double num = base.GetDamage() * damageRatio;
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
			copy.damageAreaR = this.GetMeteoriteAreaR();
			copy.InitPath(randomAliveEnemy.pos, randomAliveEnemy.pos);
			copy.soundImpact = new SoundEventSound(SoundType.GAMEPLAY, base.id, false, 0f, new SoundVariedSimple(SoundArchieve.inst.earthRingMeteorImpact, 0.7f));
			copy.visualEffect = new VisualEffect(VisualEffect.Type.TOTEM_EARTH_IMPACT, 1.33f)
			{
				scale = 0.7f
			};
			this.AddProjectile(copy);
			return true;
		}

		public override double GetDps()
		{
			return base.GetDamage();
		}

		public float GetMeteorAreaR()
		{
			return this.data.GetMeteorAreaR();
		}

		public float GetMeteoriteAreaR()
		{
			return this.data.GetMeteoriteAreaR();
		}

		public int GetMaxNumMeteors()
		{
			return this.data.GetMaxNumMeteors();
		}

		public void RechargeMeteorShower()
		{
			this.timeCharged = this.GetTimeChargedMax();
		}

		public void RechargeMeteorShower(float dur)
		{
			this.timeCharged = Mathf.Min(this.timeCharged + dur, this.GetTimeChargedMax());
		}

		public TotemDataEarth data;

		public float timeCharged;

		public int numMeteorsWaiting;

		public int numMeteoritesWaiting;

		private float timerMeteor;

		private float timerMeteorite;

		private float timeChargedWithTap;
	}
}
