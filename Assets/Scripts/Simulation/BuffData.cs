using System;
using System.Collections.Generic;

namespace Simulation
{
	public class BuffData
	{
		public BuffData()
		{
			this.visuals = 0;
			this.isPermenant = false;
			this.dur = 0f;
			this.lifeCounter = 0;
			this.isStackable = false;
			this.id = -1;
		}

		public virtual bool IsToSave()
		{
			return false;
		}

		public virtual bool DontRemoveOnDeath()
		{
			return false;
		}

		public virtual string GetSaveDataGenericKey()
		{
			return string.Empty;
		}

		public virtual void OnInit(Buff buff)
		{
		}

		public virtual void OnFinished(Buff buff)
		{
		}

		public virtual void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
		}

		public virtual void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
		}

		public virtual void OnUseAbilty(Buff buff, Skill skill)
		{
		}

		public virtual void OnNewEnemies(Buff buff)
		{
		}

		public virtual void OnNewWave(Buff buff)
		{
		}

		public virtual void OnNewStage(Buff buff)
		{
		}

		public virtual void OnReviveAlly(Buff buff, UnitHealthy revived)
		{
		}

		public virtual void OnDeathSelf(Buff buff)
		{
		}

		public virtual void OnReviveSelf(Buff buff)
		{
		}

		public virtual void OnDeathAny(Buff buff, UnitHealthy dead)
		{
		}

		public virtual void OnDeathAlly(Buff buff, UnitHealthy dead)
		{
		}

		public virtual void OnOpponentTakenDamage(Buff buff, Unit opponent, Unit attacker, Damage damage)
		{
		}

		public virtual void OnOpponentDeath(Buff buff, UnitHealthy dead)
		{
		}

		public virtual void OnUpgradedSelf(Buff buff)
		{
		}

		public virtual void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
		}

		public virtual void OnPreProjectile(Buff buff, Projectile projectile)
		{
		}

		public virtual void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
		}

		public virtual void OnPreTakeDamage(Buff buff, Unit attacker, Damage damage)
		{
		}

		public virtual void OnTakenDamage(Buff buff, Unit attacker, Damage damage)
		{
		}

		public virtual void OnKilled(Buff buff, UnitHealthy killed)
		{
		}

		public virtual void OnAttackTargetChanged(Buff buff, UnitHealthy oldTarget, UnitHealthy newTarget)
		{
		}

		public virtual void OnDodged(Buff buff, Unit attacker, Damage damage)
		{
		}

		public virtual void OnMissed(Buff buff, UnitHealthy target, Damage damage)
		{
		}

		public virtual void OnHealthLost(Buff buff, double ratio)
		{
		}

		public virtual void OnActionBuffEventTriggered(Buff buff, Unit by, BuffEventAction buffEventAction)
		{
		}

		public virtual void OnBlocked(Buff buff, Unit attacker, Damage damage, double damageBlockFactor)
		{
		}

		public virtual void OnCastSpellSelf(Buff buff, Skill skill)
		{
		}

		public virtual void OnCastSpellAlly(Buff buff, Unit ally, Skill skill)
		{
		}

		public virtual void OnOverheated(Buff buff)
		{
		}

		public virtual void OnOverheatFinished(Buff buff)
		{
		}

		public virtual void OnPreThunderbolt(Buff buff, UnitHealthy target, Damage damage, bool isSecondary)
		{
		}

		public virtual void OnAfterThunderbolt(Buff buff, UnitHealthy target, Damage damage, bool isSecondary)
		{
		}

		public virtual void OnPreLightning(Buff buff, UnitHealthy target, Damage damage)
		{
		}

		public virtual void OnAfterLightning(Buff buff, UnitHealthy target, Damage damage)
		{
		}

		public virtual void OnAfterIceShardRain(Buff buff)
		{
		}

		public virtual void OnIceManaFull(Buff buff)
		{
		}

		public virtual void OnMeteorShower(Buff buff)
		{
		}

		public virtual void OnShieldBreakAlly(Buff buff, UnitHealthy shieldBroken)
		{
		}

		public virtual void OnShieldBreakSelf(Buff buff)
		{
		}

		public const int VISUAL_BIT_OFFSET_ATTACK_FAST = 1;

		public const int VISUAL_BIT_OFFSET_ATTACK_SLOW = 2;

		public const int VISUAL_BIT_OFFSET_CRIT_CHANCE = 4;

		public const int VISUAL_BIT_OFFSET_DAMAGE_ADD = 8;

		public const int VISUAL_BIT_OFFSET_DAMAGE_DEC = 16;

		public const int VISUAL_BIT_OFFSET_DEFENSELESS = 32;

		public const int VISUAL_BIT_OFFSET_HEALTH_REGEN = 64;

		public const int VISUAL_BIT_OFFSET_IMMUNITY = 128;

		public const int VISUAL_BIT_OFFSET_SHIELD = 256;

		public const int VISUAL_BIT_OFFSET_STUN = 512;

		public const int VISUAL_BIT_OFFSET_MARK = 1024;

		public const int VISUAL_BIT_OFFSET_MISS = 2048;

		public const int VISUAL_BIT_OFFSET_TRINKET = 4096;

		public const int VISUAL_BIT_OFFSET_CRIT_DAMAGE = 8192;

		public const int VISUAL_BIT_OFFSET_DODGE_CHANCE = 16384;

		public const int VISUAL_BIT_OFFSET_REDUCE_REVIVE = 32768;

		public int visuals;

		public int tag;

		public bool isPermenant;

		public bool doNotRemoveOnRefresh;

		public float dur;

		public int lifeCounter;

		public float genericTimer;

		public bool genericFlag;

		public bool isStackable;

		public List<BuffEvent> events;

		protected const string GENERIC_KEY_PREFIX = "BuffGeneric.";

		public int id;
	}
}
