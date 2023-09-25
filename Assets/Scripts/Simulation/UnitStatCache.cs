using System;
using UnityEngine;

namespace Simulation
{
	public class UnitStatCache
	{
		public void UpdateTotemIce(TotemDataIce data, BuffTotalUnitEffect buffUnitEffect, BuffTotalWorldEffect buffWorldEffect, UniversalTotalBonus universalBonus, ChallengeUpgradesTotal challengeUpgradesTotal)
		{
			this.UpdateTotem(data, buffUnitEffect, buffWorldEffect, universalBonus, challengeUpgradesTotal);
			this.totemIceManaMax = data.GetManaMax() * buffUnitEffect.totemIceManaMaxFactor;
			this.totemIceManaGatherSpeed = data.GetManaGatherSpeed() * buffUnitEffect.totemIceManaGatherSpeedFactor;
			this.totemIceManaUseSpeed = data.GetManaUseSpeed() * buffUnitEffect.totemIceManaUseSpeedFactor;
			this.totemIceShardReqMana = data.GetShardReqMana() * buffUnitEffect.totemIceShardReqManaFactor;
		}

		public void UpdateTotemLightning(TotemDataLightning data, BuffTotalUnitEffect buffUnitEffect, BuffTotalWorldEffect buffWorldEffect, UniversalTotalBonus universalBonus, ChallengeUpgradesTotal challengeUpgradesTotal)
		{
			this.UpdateTotem(data, buffUnitEffect, buffWorldEffect, universalBonus, challengeUpgradesTotal);
			this.totemChargeReq = data.GetChargeReq() + buffUnitEffect.totemChargeReqAdd;
		}

		public void UpdateTotemFire(TotemDataFire data, BuffTotalUnitEffect buffUnitEffect, BuffTotalWorldEffect buffWorldEffect, UniversalTotalBonus universalBonus, ChallengeUpgradesTotal challengeUpgradesTotal)
		{
			this.UpdateTotem(data, buffUnitEffect, buffWorldEffect, universalBonus, challengeUpgradesTotal);
			this.totemHeatMax = data.GetHeatMax() + buffUnitEffect.totemHeatMaxAdd;
			this.totemHeatPerFire = data.GetHeatPerFire() * buffUnitEffect.totemHeatFactor;
			this.totemCoolSpeed = data.GetCoolSpeed() * buffUnitEffect.totemCoolFactor;
			this.totemOverCoolSpeed = data.GetOverCoolSpeed() * buffUnitEffect.totemOverCoolFactor;
		}

		public void UpdateTotemEarth(TotemDataEarth data, BuffTotalUnitEffect buffUnitEffect, BuffTotalWorldEffect buffWorldEffect, UniversalTotalBonus universalBonus, ChallengeUpgradesTotal challengeUpgradesTotal)
		{
			this.UpdateTotem(data, buffUnitEffect, buffWorldEffect, universalBonus, challengeUpgradesTotal);
		}

		public void UpdateTotem(TotemData data, BuffTotalUnitEffect buffUnitEffect, BuffTotalWorldEffect buffWorldEffect, UniversalTotalBonus universalBonus, ChallengeUpgradesTotal challengeUpgradesTotal)
		{
			this.attackSpeed = (this.GetNormalAttackSpeed() + buffUnitEffect.attackSpeedAdd) * buffUnitEffect.attackSpeedFactor;
			this.damageNonAdded = data.damage * challengeUpgradesTotal.totemDamageFactor * buffUnitEffect.damageAddFactor * buffUnitEffect.damageMulFactor * buffWorldEffect.ringDamageFactor * buffWorldEffect.ringDamageEvolveFactor * buffWorldEffect.ringDamageTEFactor * universalBonus.gearDamageFactor * (universalBonus.damageFactor + (universalBonus.damageTotemFactor - 1.0)) * universalBonus.mineDamageFactor * buffWorldEffect.damageNonCritFactor;
			this.damageAdded = buffWorldEffect.ringDamageAdd * buffWorldEffect.ringDamageTEFactor * buffWorldEffect.ringDamageFactor;
			this.damage = this.damageNonAdded + this.damageAdded;
			this.damageAreaFactor = buffUnitEffect.damageAreaFactor;
			this.damageAreaRFactor = buffUnitEffect.damageAreaRFactor;
			this.critChance = data.critChance + buffUnitEffect.critChanceAdd + universalBonus.critChanceTotemAdd + buffWorldEffect.critChanceAdd;
			this.critFactor = (data.critFactor + buffUnitEffect.critFactorAdd + universalBonus.critFactorTotemAdd) / buffWorldEffect.damageNonCritFactor;
			this.upgradeCostFactor = universalBonus.costTotemUpgradeFactor * buffUnitEffect.upgradeCostFactor * buffWorldEffect.upgradeCostFactorTE;
		}

		public void UpdateHero(HeroData data, BuffTotalUnitEffect buffUnitEffect, BuffTotalWorldEffect buffWorldEffect, UniversalTotalBonus universalBonus, ChallengeUpgradesTotal challengeUpgradesTotal, int numAttackersInTeam, int numDefendersInTeam)
		{
			this.attackSpeed = (this.GetNormalAttackSpeed() + buffUnitEffect.attackSpeedAdd + buffWorldEffect.heroAttackSpeedAdd) * buffWorldEffect.heroAttackSpeedFactor * buffUnitEffect.attackSpeedFactor;
			this.damage = data.damage * challengeUpgradesTotal.heroDamageFactor * buffUnitEffect.damageAddFactor * buffUnitEffect.damageMulFactor * buffUnitEffect.damageTEFactor * buffWorldEffect.heroDamageFactor * buffWorldEffect.heroDamageFactorTE * universalBonus.gearDamageFactor * (universalBonus.damageFactor + (universalBonus.damageHeroFactor - 1.0)) * universalBonus.charmDamageFactor * universalBonus.mineDamageFactor * buffWorldEffect.damageNonCritFactor * universalBonus.damageHeroNonSkillFactor * universalBonus.GetDamageFactorForCurrentAttackersInTeam(numAttackersInTeam);
			this.healthMax = data.healthMax * challengeUpgradesTotal.healthFactor * buffUnitEffect.healthMaxFactor * buffUnitEffect.healthMaxFactorTE * universalBonus.gearHealthFactor * universalBonus.healthHeroFactor * universalBonus.charmHealthFactor * universalBonus.mineHealthFactor * buffWorldEffect.heroHealthMaxFactorTE * buffWorldEffect.heroHealthMaxFactor * universalBonus.GetHealthFactorForCurrentDefendersInTeam(numDefendersInTeam);
			this.upgradeCostFactor = universalBonus.costHeroUpgradeFactor * buffUnitEffect.upgradeCostFactor * buffUnitEffect.upgradeCostFactorTE * buffWorldEffect.upgradeCostFactor * buffWorldEffect.upgradeCostFactorTE;
			this.damageAreaFactor = buffUnitEffect.damageAreaFactor;
			this.damageAreaRFactor = buffUnitEffect.damageAreaRFactor;
			this.missChance = data.GetMissChance() * buffUnitEffect.missChanceFactor + buffUnitEffect.missChanceAdd + buffWorldEffect.heroMissChance;
			this.critChance = data.critChance + buffUnitEffect.critChanceAdd + universalBonus.critChanceHeroAdd + buffWorldEffect.critChanceAdd;
			this.critFactor = (data.critFactor + buffUnitEffect.critFactorAdd + universalBonus.critFactorHeroAdd) / buffWorldEffect.damageNonCritFactor;
			this.healthRegen = (data.healthRegen + buffUnitEffect.healthRegenAdd + buffWorldEffect.heroesRegenAdd) * buffWorldEffect.heroHealFactor;
			this.damageTakenFactor = data.damageTakenFactor * buffUnitEffect.damageTakenFactor * buffWorldEffect.heroDamageTakenFactor;
			this.dodgeChance = data.dodgeChance + buffUnitEffect.dodgeChanceAdd + buffWorldEffect.allUnitsDodgeChanceAdd;
			float num = data.durRevive;
			if (buffWorldEffect.constantHeroReviveTime > 0f)
			{
				num = buffWorldEffect.constantHeroReviveTime;
			}
			this.reviveDur = Mathf.Max(8f, num * buffUnitEffect.reviveDurFactor * universalBonus.reviveTimeFactor * buffWorldEffect.reviveTimeFactor);
			this.skillCoolFactor = buffUnitEffect.skillCoolFactor * buffWorldEffect.heroSkillCoolFactor;
			this.reloadSpeed = 1f + buffUnitEffect.reloadSpeedAdd;
			this.weaponLoadExtra = buffUnitEffect.weaponLoadAdd;
		}

		public void UpdateEnemyRegular(EnemyData data, BuffTotalUnitEffect buffUnitEffect, BuffTotalWorldEffect buffWorldEffect, UniversalTotalBonus universalBonus, int numSupportersInTeam)
		{
			this.UpdateEnemyBase(data, buffUnitEffect, buffWorldEffect, universalBonus);
			this.damage = data.damage * buffUnitEffect.damageAddFactor * buffUnitEffect.damageMulFactor * buffUnitEffect.damageTEFactor * buffWorldEffect.enemyDamageFactor * universalBonus.damageEnemyFactor;
			this.healthMax = data.healthMax * buffUnitEffect.healthMaxFactor * universalBonus.healthEnemyFactor;
			this.goldToDrop = data.goldToDrop * buffUnitEffect.dropGoldFactor * universalBonus.goldFactor * universalBonus.gearGoldFactor * universalBonus.charmGoldFactor * universalBonus.mineGoldFactor * universalBonus.goldEnemyFactor * universalBonus.GetGoldFactorForCurrentSupportersInTeam(numSupportersInTeam);
			this.missChance = data.GetMissChance() * buffUnitEffect.missChanceFactor + buffUnitEffect.missChanceAdd;
		}

		public void UpdateEnemyBoss(EnemyData data, BuffTotalUnitEffect buffUnitEffect, BuffTotalWorldEffect buffWorldEffect, UniversalTotalBonus universalBonus, int numSupportersInTeam)
		{
			this.UpdateEnemyBase(data, buffUnitEffect, buffWorldEffect, universalBonus);
			this.damage = data.damage * buffUnitEffect.damageAddFactor * buffUnitEffect.damageMulFactor * buffUnitEffect.damageTEFactor * universalBonus.damageBossFactor;
			this.healthMax = data.healthMax * buffUnitEffect.healthMaxFactor * universalBonus.healthBossFactor;
			this.goldToDrop = data.goldToDrop * buffUnitEffect.dropGoldFactor * universalBonus.gearGoldFactor * universalBonus.goldFactor * universalBonus.charmGoldFactor * universalBonus.mineGoldFactor * universalBonus.goldBossFactor * universalBonus.GetGoldFactorForCurrentSupportersInTeam(numSupportersInTeam);
			this.missChance = data.GetMissChance() * buffUnitEffect.missChanceFactor + buffUnitEffect.missChanceAdd;
		}

		private void UpdateEnemyBase(EnemyData data, BuffTotalUnitEffect buffUnitEffect, BuffTotalWorldEffect buffWorldEffect, UniversalTotalBonus universalBonus)
		{
			this.attackSpeed = (this.GetNormalAttackSpeed() + buffUnitEffect.attackSpeedAdd + buffWorldEffect.enemyAttackSpeedAdd) * buffUnitEffect.attackSpeedFactor * buffWorldEffect.enemyAttackSpeedFactor;
			this.damageAreaFactor = buffUnitEffect.damageAreaFactor;
			this.damageAreaRFactor = buffUnitEffect.damageAreaRFactor;
			this.critChance = data.critChance + buffUnitEffect.critChanceAdd;
			this.critFactor = data.critFactor + buffUnitEffect.critFactorAdd;
			this.healthRegen = data.healthRegen + buffUnitEffect.healthRegenAdd + buffWorldEffect.enemiesRegenAdd;
			this.damageTakenFactor = data.damageTakenFactor * buffUnitEffect.damageTakenFactor;
			this.dodgeChance = data.dodgeChance + buffUnitEffect.dodgeChanceAdd + buffWorldEffect.allUnitsDodgeChanceAdd;
			this.missChance = data.GetMissChance() * buffUnitEffect.missChanceFactor + buffUnitEffect.missChanceAdd;
			this.extraDamageTakenFromHeroesFactor = universalBonus.extraDamageTakenFromHeroesFactor;
			this.extraDamageTakenFromRingFactor = universalBonus.extraDamageTakenFromRingFactor;
		}

		private float GetNormalAttackSpeed()
		{
			return 1f;
		}

		public float attackSpeed;

		public double damage;

		public double damageAreaFactor;

		public float damageAreaRFactor;

		public float missChance;

		public float critChance;

		public double critFactor;

		public double healthMax;

		public double healthRegen;

		public double damageTakenFactor;

		public double extraDamageTakenFromHeroesFactor;

		public double extraDamageTakenFromRingFactor;

		public float dodgeChance;

		public double goldToDrop;

		public float reviveDur;

		public double upgradeCostFactor;

		public float skillCoolFactor;

		public float reloadSpeed;

		public int weaponLoadExtra;

		public int totemChargeReq;

		public float totemHeatMax;

		public float totemHeatPerFire;

		public float totemCoolSpeed;

		public float totemOverCoolSpeed;

		public float totemIceManaMax;

		public float totemIceManaGatherSpeed;

		public float totemIceManaUseSpeed;

		public float totemIceShardReqMana;

		public double damageAdded;

		public double damageNonAdded;
	}
}
