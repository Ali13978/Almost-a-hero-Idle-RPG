using System;
using System.Collections.Generic;

namespace Simulation
{
	public class Buff
	{
		public Buff(BuffData data, Unit by, World world, int genericCounter)
		{
			this.data = data;
			this.by = by;
			this.world = world;
			this.genericCounter = genericCounter;
			this.countsLife = (data.lifeCounter > 0);
			this.lifeCounter = data.lifeCounter;
			this.time = 0f;
			this.atEvent = 0;
		}

		public Unit GetBy()
		{
			return this.by;
		}

		public World GetWorld()
		{
			return this.world;
		}

		public int GetDataId()
		{
			return this.data.id;
		}

		public bool DontRemoveOnDeath()
		{
			return this.data.DontRemoveOnDeath();
		}

		public Type GetDataType()
		{
			return this.data.GetType();
		}

		public bool IsStun()
		{
			return this.data is BuffDataStun;
		}

		public bool HasDataType(Type dataType)
		{
			return this.data.GetType() == dataType;
		}

		public void DecreaseLifeCounter()
		{
			this.lifeCounter--;
		}

		public float GetLifeCounter()
		{
			return (float)this.lifeCounter;
		}

		public int GetGenericCounter()
		{
			return this.genericCounter;
		}

		public float GetGenericTimer()
		{
			return this.data.genericTimer;
		}

		public bool GetGenericFlag()
		{
			return this.data.genericFlag;
		}

		public void IncreaseGenericCounter()
		{
			this.genericCounter++;
		}

		public void SetGenericCounter(int genericCounter)
		{
			this.genericCounter = genericCounter;
		}

		public void DecreaseGenericCounter(int amount)
		{
			this.genericCounter -= amount;
		}

		public void ZeroGenericCounter()
		{
			this.genericCounter = 0;
		}

		public float GetTime()
		{
			return this.time;
		}

		public bool IsPermenant()
		{
			return this.data.isPermenant;
		}

		public bool DoNotRemoveOnRefresh()
		{
			return this.data.doNotRemoveOnRefresh;
		}

		public bool IsToSave()
		{
			return this.data.IsToSave();
		}

		public string GetSaveDataGenericKey()
		{
			return this.data.GetSaveDataGenericKey();
		}

		public void LoadSaveDataGeneric(Dictionary<string, string> genericData)
		{
			string saveDataGenericKey = this.data.GetSaveDataGenericKey();
			if (genericData.ContainsKey(saveDataGenericKey))
			{
				this.genericCounter = int.Parse(genericData[saveDataGenericKey]);
			}
		}

		public int GetVisuals()
		{
			return this.data.visuals;
		}

		public bool HasEnded()
		{
			return !this.IsPermenant() && (this.time > this.data.dur || (this.countsLife && this.lifeCounter <= 0));
		}

		public bool HasSameId(BuffData otherData)
		{
			return this.data.GetType() == otherData.GetType() && this.data.id == otherData.id;
		}

		public void ApplyEffects(BuffTotalUnitEffect totEffect, float dt)
		{
			this.data.ApplyStats(totEffect, this, dt);
		}

		public void ApplyEffects(BuffTotalWorldEffect totEffect)
		{
			this.data.ApplyWorldEffect(totEffect, this);
		}

		public void Update(float dt)
		{
			this.time += dt;
			List<BuffEvent> events = this.data.events;
			if (events != null)
			{
				while (this.atEvent < events.Count && events[this.atEvent].time <= this.time)
				{
					BuffEvent buffEvent = events[this.atEvent++];
					buffEvent.Apply(this.by, this.world);
				}
			}
		}

		public void OnActionBuffEventTriggered(BuffEventAction buffEventAction, Unit by)
		{
			this.data.OnActionBuffEventTriggered(this, by, buffEventAction);
		}

		public float GetTimeRemaining()
		{
			return this.data.dur - this.time;
		}

		public void ResetTime()
		{
			this.time = 0f;
		}

		public void IncreaseDuration(float amount)
		{
			this.time -= amount;
		}

		public void OnNewEnemies()
		{
			this.data.OnNewEnemies(this);
		}

		public void OnNewWave()
		{
			this.data.OnNewWave(this);
		}

		public void OnNewStage()
		{
			this.data.OnNewStage(this);
		}

		public void OnDeathSelf()
		{
			this.data.OnDeathSelf(this);
		}

		public void OnDeathAny(UnitHealthy dead)
		{
			this.data.OnDeathAny(this, dead);
		}

		public void OnShieldBreakSelf()
		{
			this.data.OnShieldBreakSelf(this);
		}

		public void OnShieldBreakAlly(UnitHealthy shieldBroken)
		{
			this.data.OnShieldBreakAlly(this, shieldBroken);
		}

		public void OnDeathAlly(UnitHealthy dead)
		{
			this.data.OnDeathAlly(this, dead);
		}

		public void OnReviveSelf()
		{
			this.data.OnReviveSelf(this);
		}

		public void OnReviveAlly(UnitHealthy revived)
		{
			this.data.OnReviveAlly(this, revived);
		}

		public void OnOpponentTakenDamage(Unit opponent, Unit attacker, Damage damage)
		{
			this.data.OnOpponentTakenDamage(this, opponent, attacker, damage);
		}

		public void OnOpponentDeath(UnitHealthy dead)
		{
			this.data.OnOpponentDeath(this, dead);
		}

		public void OnUpgradedSelf()
		{
			this.data.OnUpgradedSelf(this);
		}

		public void OnPreTakeDamage(Unit attacker, Damage damage)
		{
			this.data.OnPreTakeDamage(this, attacker, damage);
		}

		public void OnTakenDamage(Unit attacker, Damage damage)
		{
			this.data.OnTakenDamage(this, attacker, damage);
		}

		public void OnHealthLost(double ratio)
		{
			this.data.OnHealthLost(this, ratio);
		}

		public void OnPostDamage(UnitHealthy target, Damage damage)
		{
			this.data.OnPostDamage(this, target, damage);
		}

		public void OnKilled(UnitHealthy killed)
		{
			this.data.OnKilled(this, killed);
		}

		public void OnAttackTargetChanged(UnitHealthy oldTarget, UnitHealthy newTarget)
		{
			this.data.OnAttackTargetChanged(this, oldTarget, newTarget);
		}

		public void OnCastSpellSelf(Skill skill)
		{
			this.data.OnCastSpellSelf(this, skill);
		}

		public void OnCastSpellAlly(Unit ally, Skill skill)
		{
			this.data.OnCastSpellAlly(this, ally, skill);
		}

		public void OnOverheated()
		{
			this.data.OnOverheated(this);
		}

		public void OnOverheatFinished()
		{
			this.data.OnOverheatFinished(this);
		}

		public void OnPreDamage(UnitHealthy target, Damage damage)
		{
			this.data.OnPreDamage(this, target, damage);
		}

		public void OnPreProjectile(Projectile projectile)
		{
			this.data.OnPreProjectile(this, projectile);
		}

		public void OnDodged(Unit attacker, Damage damage)
		{
			this.data.OnDodged(this, attacker, damage);
		}

		public void OnMissed(UnitHealthy target, Damage damage)
		{
			this.data.OnMissed(this, target, damage);
		}

		public void OnBlocked(Unit attacker, Damage damage, double damageBlockFactor)
		{
			this.data.OnBlocked(this, attacker, damage, damageBlockFactor);
		}

		public void OnPreThunderbolt(UnitHealthy target, Damage damage, bool isSecondary)
		{
			this.data.OnPreThunderbolt(this, target, damage, isSecondary);
		}

		public void OnAfterThunderbolt(UnitHealthy target, Damage damage, bool isSecondary)
		{
			this.data.OnAfterThunderbolt(this, target, damage, isSecondary);
		}

		public void OnPreLightning(UnitHealthy target, Damage damage)
		{
			this.data.OnPreLightning(this, target, damage);
		}

		public void OnAfterLightning(UnitHealthy target, Damage damage)
		{
			this.data.OnAfterLightning(this, target, damage);
		}

		public void OnAfterIceShardRain()
		{
			this.data.OnAfterIceShardRain(this);
		}

		public void OnIceManaFull()
		{
			this.data.OnIceManaFull(this);
		}

		public void OnMeteorShower()
		{
			this.data.OnMeteorShower(this);
		}

		public void OnFinished()
		{
			this.data.OnFinished(this);
		}

		private BuffData data;

		private Unit by;

		private World world;

		private bool countsLife;

		private int lifeCounter;

		private int genericCounter;

		private float time;

		private int atEvent;
	}
}
