using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public abstract class Unit
	{
		public Unit(World world)
		{
			this.world = world;
			this.pos = Vector3.zero;
			this.buffs = new List<Buff>();
			this.buffTotalEffect = new BuffTotalUnitEffect();
			this.statCache = new UnitStatCache();
			this.buffTimeCounterAttackFast = float.NegativeInfinity;
			this.buffTimeCounterAttackSlow = float.NegativeInfinity;
			this.buffTimeCounterCritChance = float.NegativeInfinity;
			this.buffTimeCounterDamageAdd = float.NegativeInfinity;
			this.buffTimeCounterDamageDec = float.NegativeInfinity;
			this.buffTimeCounterDefenseless = float.NegativeInfinity;
			this.buffTimeCounterHeathRegen = float.NegativeInfinity;
			this.buffTimeCounterImmunity = float.NegativeInfinity;
			this.buffTimeCounterShield = float.NegativeInfinity;
			this.buffTimeCounterStun = float.NegativeInfinity;
			this.buffTimeCounterMark = float.NegativeInfinity;
			this.buffTimeCounterMiss = float.NegativeInfinity;
			this.buffTimeCounterTrinket = float.NegativeInfinity;
			this.buffTimeCounterCritDamage = float.NegativeInfinity;
			this.buffTimeCounterDodgeChance = float.NegativeInfinity;
			this.buffTimeCounterReduceRevive = float.NegativeInfinity;
		}

		public void OnActionBuffEventTriggered(BuffEventAction buffEventAction, Unit by)
		{
			foreach (Buff buff in this.buffs)
			{
				buff.OnActionBuffEventTriggered(buffEventAction, by);
			}
		}

		public abstract string GetId();

		public abstract float GetHeight();

		public abstract float GetScaleHealthBar();

		public abstract float GetScaleBuffVisual();

		public float GetAttackSpeed()
		{
			return this.statCache.attackSpeed;
		}

		public double GetDamage()
		{
			return this.statCache.damage;
		}

		public double GetDamageAreaFactor()
		{
			return this.statCache.damageAreaFactor;
		}

		public float GetMissChance()
		{
			return this.statCache.missChance;
		}

		public float GetCritChance()
		{
			return this.statCache.critChance;
		}

		public double GetCritFactor()
		{
			return this.statCache.critFactor;
		}

		public float GetReloadSpeed()
		{
			return this.statCache.reloadSpeed;
		}

		public abstract double GetDps();

		public abstract double GetDpsTeam();

		public abstract IEnumerable<UnitHealthy> GetAllies();

		public abstract IEnumerable<UnitHealthy> GetOpponents();

		public virtual void CoolWeapon(float coolRatio)
		{
		}

		public virtual void AttackImmediately(UnitHealthy unit)
		{
		}

		public virtual void CancelCurrentOverheat()
		{
		}

		public virtual void AddCharge(int amount)
		{
		}

		public virtual void MultiplyTotemHeat(float factor)
		{
		}

		public virtual void AddIceMana(float amount)
		{
		}

		private void UpdateBuffTotalEffect(float dt)
		{
			this.buffTotalEffect.Init();
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.ApplyEffects(this.buffTotalEffect, dt);
			}
			if (this.buffToAdd != null)
			{
				this.buffs.Add(this.buffToAdd);
				this.buffToAdd = null;
			}
		}

		public virtual void UpdateBuffTotalWorldEffect(BuffTotalWorldEffect buffTotalWorldEffect)
		{
			foreach (Buff buff in this.buffs)
			{
				buff.ApplyEffects(buffTotalWorldEffect);
			}
		}

		public virtual void UpdateStats(float dt)
		{
			this.UpdateBuffTotalEffect(dt);
		}

		public virtual void Update(float dt, int addVisuals)
		{
			this.UpdateBuffs(dt);
			int num = addVisuals;
			foreach (Buff buff in this.buffs)
			{
				num |= buff.GetVisuals();
			}
			if ((num & 256) != 0 && this is UnitHealthy)
			{
				UnitHealthy unitHealthy = (UnitHealthy)this;
				if (unitHealthy.GetShieldRatio() <= 0.0)
				{
					num ^= 256;
				}
			}
			this.UpdateBuffTimeCounter(num, 1, ref this.buffTimeCounterAttackFast, dt);
			this.UpdateBuffTimeCounter(num, 2, ref this.buffTimeCounterAttackSlow, dt);
			this.UpdateBuffTimeCounter(num, 4, ref this.buffTimeCounterCritChance, dt);
			this.UpdateBuffTimeCounter(num, 8, ref this.buffTimeCounterDamageAdd, dt);
			this.UpdateBuffTimeCounter(num, 16, ref this.buffTimeCounterDamageDec, dt);
			this.UpdateBuffTimeCounter(num, 32, ref this.buffTimeCounterDefenseless, dt);
			this.UpdateBuffTimeCounter(num, 64, ref this.buffTimeCounterHeathRegen, dt);
			this.UpdateBuffTimeCounter(num, 128, ref this.buffTimeCounterImmunity, dt);
			this.UpdateBuffTimeCounter(num, 256, ref this.buffTimeCounterShield, dt);
			this.UpdateBuffTimeCounter(num, 512, ref this.buffTimeCounterStun, dt);
			this.UpdateBuffTimeCounter(num, 1024, ref this.buffTimeCounterMark, dt);
			this.UpdateBuffTimeCounter(num, 2048, ref this.buffTimeCounterMiss, dt);
			this.UpdateBuffTimeCounter(num, 4096, ref this.buffTimeCounterTrinket, dt);
			this.UpdateBuffTimeCounter(num, 8192, ref this.buffTimeCounterCritDamage, dt);
			this.UpdateBuffTimeCounter(num, 16384, ref this.buffTimeCounterDodgeChance, dt);
			this.UpdateBuffTimeCounter(num, 32768, ref this.buffTimeCounterReduceRevive, dt);
		}

		private void UpdateBuffTimeCounter(int vis, int bitMask, ref float timeCounter, float dt)
		{
			if ((vis & bitMask) != 0)
			{
				timeCounter = GameMath.GetMaxFloat(dt, timeCounter + dt);
			}
			else
			{
				timeCounter = GameMath.GetMinFloat(-dt, timeCounter - dt);
			}
		}

		protected double GetPermBuffDamageFactor()
		{
			BuffTotalUnitEffect buffTotalUnitEffect = new BuffTotalUnitEffect();
			buffTotalUnitEffect.Init();
			foreach (Buff buff in this.buffs)
			{
				if (buff.IsPermenant())
				{
					buff.ApplyEffects(buffTotalUnitEffect, 0f);
				}
			}
			return buffTotalUnitEffect.damageAddFactor * buffTotalUnitEffect.damageTEFactor * buffTotalUnitEffect.damageMulFactor;
		}

		public void Damage(UnitHealthy damaged, Damage damage)
		{
			this.world.DamageMain(this, damaged, damage);
		}

		public abstract void DamageAll(Damage damage);

		public void OnNewEnemies()
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnNewEnemies();
			}
		}

		public void OnNewWave()
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnNewWave();
			}
		}

		public void OnNewStage()
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnNewStage();
			}
		}

		public virtual void OnDeathSelf()
		{
			this.world.OnDeathAny(this);
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnDeathSelf();
			}
		}

		public void OnDeathAny(UnitHealthy dead)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnDeathAny(dead);
			}
		}

		public virtual void OnReviveSelf()
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnReviveSelf();
			}
		}

		public virtual void OnReviveAlly(UnitHealthy revived)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnReviveAlly(revived);
			}
		}

		public virtual void OnShieldBreakSelf()
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnShieldBreakSelf();
			}
		}

		public virtual void OnShieldBreakAlly(UnitHealthy shieldBroken)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnShieldBreakAlly(shieldBroken);
			}
		}

		public virtual void OnDeathAlly(UnitHealthy dead)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnDeathAlly(dead);
			}
		}

		public void OnOpponentDeath(UnitHealthy dead)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnOpponentDeath(dead);
			}
		}

		public void OnOpponentTakenDamage(Unit opponent, Unit attacker, Damage damage)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnOpponentTakenDamage(opponent, attacker, damage);
			}
		}

		public void OnPreTakeDamage(Unit damager, Damage damage)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnPreTakeDamage(damager, damage);
			}
		}

		public void OnTakenDamage(Unit damager, Damage damage)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnTakenDamage(damager, damage);
			}
			foreach (UnitHealthy unitHealthy in this.GetOpponents())
			{
				unitHealthy.OnOpponentTakenDamage(this, damager, damage);
			}
			if (this.GetId() == "BABU")
			{
				this.world.OnBabuGetHit();
			}
		}

		public void OnHealthLost(double ratio)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnHealthLost(ratio);
			}
		}

		public virtual void OnPostDamage(UnitHealthy damaged, Damage damage)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnPostDamage(damaged, damage);
			}
		}

		public virtual void OnKilled(UnitHealthy killed)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnKilled(killed);
			}
		}

		public void OnCastSpellSelf(Skill skill)
		{
			if (this.GetId() == "DEREK")
			{
				this.world.OnWendleCastSpell();
			}
			if (skill.data.dataBase is SkillDataBaseSelfDestruct)
			{
				this.world.OnBoomerBoom();
			}
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnCastSpellSelf(skill);
			}
		}

		public void OnCastSpellAlly(Unit ally, Skill skill)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnCastSpellAlly(ally, skill);
			}
		}

		public void OnOverheated()
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnOverheated();
			}
		}

		public void OnOverheatFinished()
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnOverheatFinished();
			}
		}

		public void OnPreDamage(UnitHealthy target, Damage damage)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnPreDamage(target, damage);
			}
		}

		public void OnPreProjectile(Projectile projectile)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnPreProjectile(projectile);
			}
		}

		public void OnDodged(Unit attacker, Damage damage)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnDodged(attacker, damage);
			}
		}

		public void OnMissed(UnitHealthy target, Damage damage)
		{
			if (this.GetId() == "GOBLIN")
			{
				this.world.OnGoblinMiss();
			}
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnMissed(target, damage);
			}
		}

		public void OnBlocked(Unit attacker, Damage damage, double damageBlockFactor)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnBlocked(attacker, damage, damageBlockFactor);
			}
		}

		public void OnPreThunderbolt(UnitHealthy target, Damage damage, bool isSecondary)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnPreThunderbolt(target, damage, isSecondary);
			}
		}

		public void OnAfterThunderbolt(UnitHealthy target, Damage damage, bool isSecondary)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnAfterThunderbolt(target, damage, isSecondary);
			}
		}

		public void OnPreLightning(UnitHealthy target, Damage damage)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnPreLightning(target, damage);
			}
		}

		public void OnAfterLightning(UnitHealthy target, Damage damage)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnAfterLightning(target, damage);
			}
		}

		public void OnAttackTargetChanged(UnitHealthy oldTarget, UnitHealthy newTarget)
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnAttackTargetChanged(oldTarget, newTarget);
			}
		}

		public void OnAfterIceShardRain()
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnAfterIceShardRain();
			}
		}

		public void OnIceManaFull()
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnIceManaFull();
			}
		}

		public void OnMeteorShower()
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.OnMeteorShower();
			}
		}

		public void AddVisualBuff(float dur, int visualBit)
		{
			this.AddBuff(new BuffData
			{
				dur = dur,
				visuals = visualBit
			}, 0, false);
		}

		public bool CanStunned()
		{
			return this.HasBuffOfType(typeof(BuffDataDefense));
		}

		public bool IsInvulnerable()
		{
			return this.HasBuffOfType(typeof(BuffDataInvulnerability));
		}

		public bool HasBuffStun()
		{
			return this.HasBuffOfType(typeof(BuffDataStun));
		}

		public bool IsSilenced()
		{
			return this.HasBuffOfType(typeof(BuffDataSilence));
		}

		public bool HasBuffTargetAll()
		{
			return this.HasBuffOfType(typeof(BuffDataTargetAllCounted));
		}

		public bool HasWeaponAntiHeatBuff()
		{
			return this.HasBuffOfType(typeof(BuffDataWeaponAntiHeat));
		}

		public int GetBuffsCountWithId(int buffId)
		{
			int num = 0;
			foreach (Buff buff in this.buffs)
			{
				if (buff.GetDataId() == buffId)
				{
					num++;
				}
			}
			return num;
		}

		public Buff GetBuffWithId(int buffId)
		{
			foreach (Buff buff in this.buffs)
			{
				if (buff.GetDataId() == buffId)
				{
					return buff;
				}
			}
			return null;
		}

		public virtual bool AddBuff(BuffData buffData, int genericCounter = 0, bool lateAdd = false)
		{
			if (buffData is BuffDataStun && this.IsInvulnerable())
			{
				return false;
			}
			Buff buff = new Buff(buffData, this, this.world, genericCounter);
			if (!buffData.isStackable)
			{
				for (int i = this.buffs.Count - 1; i >= 0; i--)
				{
					Buff buff2 = this.buffs[i];
					if (buff2.HasSameId(buffData))
					{
						this.buffs[i] = buff;
						return true;
					}
				}
			}
			if (lateAdd)
			{
				this.buffToAdd = buff;
			}
			else
			{
				this.buffs.Add(buff);
			}
			buffData.OnInit(buff);
			return true;
		}

		public void RemoveBuff(int id)
		{
			int num = this.buffs.Count - 1;
			while (num >= 0 && this.buffs[num].GetDataId() != id)
			{
				num--;
			}
			if (num >= 0)
			{
				if (num < this.buffs.Count - 1)
				{
					this.buffs[num] = this.buffs[this.buffs.Count - 1];
					this.buffs.RemoveAt(this.buffs.Count - 1);
				}
				else
				{
					this.buffs.RemoveAt(num);
				}
			}
		}

		public void RemoveBuff(BuffData buffData)
		{
			int num = this.buffs.Count - 1;
			while (num >= 0 && !this.buffs[num].HasSameId(buffData))
			{
				num--;
			}
			if (num >= 0)
			{
				if (num < this.buffs.Count - 1)
				{
					this.buffs[num] = this.buffs[this.buffs.Count - 1];
					this.buffs.RemoveAt(this.buffs.Count - 1);
				}
				else
				{
					this.buffs.RemoveAt(num);
				}
			}
		}

		public void ClearAllBuffs()
		{
			this.buffs.Clear();
		}

		public void RemoveBuffsOnDeath()
		{
			for (int i = this.buffs.Count - 1; i >= 0; i--)
			{
				if (!this.buffs[i].DontRemoveOnDeath())
				{
					this.buffs.RemoveAt(i);
				}
			}
		}

		public void ClearNonpermenantBuffs()
		{
			this.buffs.RemoveAll((Buff x) => !x.IsPermenant() && !x.DontRemoveOnDeath());
		}

		public void ClearPermenantBuffs(out Dictionary<int, int> oldGenericCounters, out Dictionary<int, float> oldGenericTimers, out Dictionary<int, bool> oldGenericFlags)
		{
			oldGenericCounters = new Dictionary<int, int>();
			oldGenericTimers = new Dictionary<int, float>();
			oldGenericFlags = new Dictionary<int, bool>();
			int num = this.buffs.Count;
			for (int i = num - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				if (buff.IsPermenant() && !buff.DoNotRemoveOnRefresh())
				{
					if (!oldGenericCounters.ContainsKey(buff.GetDataId()))
					{
						oldGenericCounters.Add(buff.GetDataId(), buff.GetGenericCounter());
					}
					if (!oldGenericTimers.ContainsKey(buff.GetDataId()))
					{
						oldGenericTimers.Add(buff.GetDataId(), buff.GetGenericTimer());
					}
					if (!oldGenericFlags.ContainsKey(buff.GetDataId()))
					{
						oldGenericFlags.Add(buff.GetDataId(), buff.GetGenericFlag());
					}
					this.buffs[i] = this.buffs[--num];
					this.buffs.RemoveAt(num);
				}
			}
		}

		private void UpdateBuffs(float dt)
		{
			int num = this.buffs.Count;
			for (int i = num - 1; i >= 0; i--)
			{
				Buff buff = this.buffs[i];
				buff.Update(dt);
				if (buff.HasEnded())
				{
					this.buffs[i] = this.buffs[--num];
					this.buffs.RemoveAt(num);
					buff.OnFinished();
				}
			}
		}

		public bool HasBuffOfType(Type buffType)
		{
			foreach (Buff buff in this.buffs)
			{
				if (buff.HasDataType(buffType))
				{
					return true;
				}
			}
			return false;
		}

		public bool HasBuffWithId(int id)
		{
			foreach (Buff buff in this.buffs)
			{
				if (buff.GetDataId() == id)
				{
					return true;
				}
			}
			return false;
		}

		public void ZeroBuffGenericCounter(Type buffType)
		{
			foreach (Buff buff in this.buffs)
			{
				if (buff.HasDataType(buffType))
				{
					buff.ZeroGenericCounter();
					break;
				}
			}
		}

		public void IncrementDurationOfBuff(Type buffType, float time)
		{
			foreach (Buff buff in this.buffs)
			{
				if (buff.HasDataType(buffType))
				{
					buff.IncreaseDuration(time);
					break;
				}
			}
		}

		public virtual void OnEventProjectile(Projectile.Type type, Projectile.TargetType targetType, float durFly, ProjectilePath path, Damage damage, List<BuffData> buffs, VisualEffect visualEffect, bool targetLocked = false, Vector3 targetPosition = default(Vector3), float damageTimeRatio = 1f)
		{
			UnitHealthy unitHealthy;
			if (targetType == Projectile.TargetType.ALL_ENEMIES)
			{
				unitHealthy = null;
			}
			else if (targetType == Projectile.TargetType.SINGLE_ENEMY)
			{
				unitHealthy = this.world.GetRandomAliveEnemy();
			}
			else if (targetType == Projectile.TargetType.SINGLE_ALLY_ANY)
			{
				unitHealthy = this.world.GetRandomAliveHeroExcluding(this);
				if (unitHealthy == null)
				{
					return;
				}
			}
			else if (targetType == Projectile.TargetType.SINGLE_ALLY_ANY_SELF)
			{
				unitHealthy = this.world.GetRandomAliveHeroExcluding(this);
				if (unitHealthy == null)
				{
					unitHealthy = (this as Hero);
				}
				if (unitHealthy == null)
				{
					return;
				}
			}
			else if (targetType == Projectile.TargetType.SINGLE_ALLY_MIN_HEALTH)
			{
				unitHealthy = this.world.GetAliveHeroWithMinHealthExcluding(this);
				if (unitHealthy == null)
				{
					return;
				}
			}
			else
			{
				if (targetType != Projectile.TargetType.NONE)
				{
					throw new NotImplementedException();
				}
				unitHealthy = null;
			}
			this.AddProjectile(new Projectile(this, type, targetType, unitHealthy, durFly, path, targetPosition)
			{
				targetLocked = targetLocked,
				damage = damage,
				buffs = buffs,
				visualEffect = visualEffect,
				damageMomentTimeRatio = damageTimeRatio
			});
		}

		public virtual void AddProjectile(Projectile newProjectile)
		{
			this.OnPreProjectile(newProjectile);
			if (!newProjectile.discarded)
			{
				this.world.AddProjectile(newProjectile);
			}
		}

		public Dictionary<string, string> GetSaveDataGeneric()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (Buff buff in this.buffs)
			{
				if (buff.IsToSave())
				{
					dictionary.Add(buff.GetSaveDataGenericKey(), buff.GetGenericCounter().ToString());
				}
			}
			return dictionary;
		}

		public void LoadSaveDataGeneric(Dictionary<string, string> data)
		{
			foreach (Buff buff in this.buffs)
			{
				if (buff.IsToSave())
				{
					buff.LoadSaveDataGeneric(data);
				}
			}
		}

		public World world;

		public Vector3 pos;

		public float inStateTimeCounter;

		public List<Buff> buffs;

		public BuffTotalUnitEffect buffTotalEffect;

		public UnitStatCache statCache;

		public float buffTimeCounterAttackFast;

		public float buffTimeCounterAttackSlow;

		public float buffTimeCounterCritChance;

		public float buffTimeCounterDamageAdd;

		public float buffTimeCounterDamageDec;

		public float buffTimeCounterDefenseless;

		public float buffTimeCounterHeathRegen;

		public float buffTimeCounterImmunity;

		public float buffTimeCounterShield;

		public float buffTimeCounterStun;

		public float buffTimeCounterMark;

		public float buffTimeCounterMiss;

		public float buffTimeCounterTrinket;

		public float buffTimeCounterCritDamage;

		public float buffTimeCounterDodgeChance;

		public float buffTimeCounterReduceRevive;

		public Buff buffToAdd;
	}
}
