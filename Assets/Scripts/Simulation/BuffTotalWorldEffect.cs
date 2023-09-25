using System;

namespace Simulation
{
	public class BuffTotalWorldEffect
	{
		public void Init()
		{
			this.upgradeCostFactor = 1.0;
			this.timeUpdateFactor = 1f;
			this.constantHeroReviveTime = 0f;
			this.enemyDamageFactor = 1.0;
			this.enemyAttackSpeedAdd = 0f;
			this.enemyAttackSpeedFactor = 1f;
			this.noGoldDrop = false;
			this.allUnitsDodgeChanceAdd = 0f;
			this.reviveTimeFactor = 1f;
			this.heroUltiCooldownMaxFactor = 1f;
			this.enemiesRegenAdd = 0.0;
			this.heroesRegenAdd = 0.0;
			this.heroHealthMaxFactorTE = 1.0;
			this.heroHealthMaxFactor = 1.0;
			this.goldFactor = 1.0;
			this.goldFactorTE = 1.0;
			this.goldChestFactor = 1.0;
			this.chestChanceAdd = 0f;
			this.heroDamageFactor = 1.0;
			this.heroDamageFactorTE = 1.0;
			this.heroDamageTakenFactor = 1.0;
			this.heroSkillCoolFactor = 1f;
			this.heroUltiCoolFactor = 1f;
			this.heroMissChance = 0f;
			this.heroHealFactor = 1.0;
			this.ringDamageAdd = 0.0;
			this.ringDamageFactor = 1.0;
			this.ringDamageEvolveFactor = 1.0;
			this.ringDamageTEFactor = 1.0;
			this.goldBoostFactor = 1.0;
			this.critChanceAdd = 0f;
			this.reviveSpeed = 1f;
			this.upgradeCostFactorTE = 1.0;
			this.damageNonCritFactor = 1.0;
			this.heroAttackSpeedAdd = 0f;
			this.heroAttackSpeedFactor = 1f;
			this.charmSelectionTimerFactor = 1f;
		}

		public double heroHealthMaxFactorTE;

		public double heroHealthMaxFactor;

		public double goldFactor;

		public double goldFactorTE;

		public double goldChestFactor;

		public float chestChanceAdd;

		public double heroDamageFactor;

		public double heroDamageFactorTE;

		public double heroDamageTakenFactor;

		public double heroHealFactor;

		public float heroSkillCoolFactor;

		public float heroUltiCoolFactor;

		public float heroUltiCooldownMaxFactor;

		public float heroMissChance;

		public double ringDamageAdd;

		public double ringDamageFactor;

		public double ringDamageEvolveFactor;

		public double ringDamageTEFactor;

		public double goldBoostFactor;

		public float critChanceAdd;

		public float reviveSpeed;

		public float reviveTimeFactor;

		public double upgradeCostFactorTE;

		public double damageNonCritFactor;

		public float allUnitsDodgeChanceAdd;

		public bool noGoldDrop;

		public float enemyAttackSpeedAdd;

		public float enemyAttackSpeedFactor;

		public double enemyDamageFactor;

		public double enemiesRegenAdd;

		public double heroesRegenAdd;

		public float constantHeroReviveTime;

		public float heroAttackSpeedAdd;

		public float heroAttackSpeedFactor;

		public float timeUpdateFactor;

		public double upgradeCostFactor;

		public float charmSelectionTimerFactor;
	}
}
