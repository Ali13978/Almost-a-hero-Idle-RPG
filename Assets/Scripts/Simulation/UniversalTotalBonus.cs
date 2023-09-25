using System;

namespace Simulation
{
	public class UniversalTotalBonus
	{
		public void Init()
		{
			this.milestoneBonusFactor = 1.0;
			this.milestoneCostFactor = 1.0;
			this.gearDamageFactor = 1.0;
			this.gearHealthFactor = 1.0;
			this.gearGoldFactor = 1.0;
			this.goldBagCountAdd = 0;
			this.goldBagValueFactor = 1f;
			this.autoTapCountAdd = 0;
			this.autoTapDurationAdd = 0f;
			this.timeWarpCountAdd = 0;
			this.timeWarpSpeedFactor = 1f;
			this.timeWarpDurationAdd = 0f;
			this.shieldCountAdd = 0;
			this.shieldDurationAdd = 0f;
			this.horseshoeCountAdd = 0;
			this.horseshoeDurationAdd = 0f;
			this.horseshoeValueFactor = 1f;
			this.destructionCountAdd = 0;
			this.powerUpCountAdd = 0;
			this.refresherOrbCountAdd = 0;
			this.afterBossDurationFactor = 1f;
			this.healthHeroFactor = 1.0;
			this.healthHeroFactorWithOneDefender = 1.0;
			this.healthHeroFactorWithTwoDefenders = 1.0;
			this.healthHeroFactorWithSeveralDefenders = 1.0;
			this.healthHeroFactorMuliplier = 1.0;
			this.healthBossFactor = 1.0;
			this.healthEnemyFactor = 1.0;
			this.goldFactor = 1.0;
			this.goldFactorWithOneSupporter = 1.0;
			this.goldFactorWithTwoSupporters = 1.0;
			this.goldFactorWithSeveralSupporters = 1.0;
			this.goldBossFactor = 1.0;
			this.goldEnemyFactor = 1.0;
			this.goldChestFactor = 1.0;
			this.goldChestRaidFactor = 1.0;
			this.goldOfflineFactor = 1.0;
			this.chestChanceFactor = 1f;
			this.goldMultTenChanceAdd = 0f;
			this.damageFactor = 1.0;
			this.damageTotemFactor = 1.0;
			this.damageHeroFactor = 1.0;
			this.damageEnemyFactor = 1.0;
			this.damageBossFactor = 1.0;
			this.damageHeroSkillFactor = 1.0;
			this.damageHeroNonSkillFactor = 1.0;
			this.damageHeroFactorWithOneAttacker = 1.0;
			this.damageHeroFactorWithTwoAttackers = 1.0;
			this.damageHeroFactorWithSeveralAttackers = 1.0;
			this.critChanceTotemAdd = 0f;
			this.critFactorTotemAdd = 0.0;
			this.critChanceHeroAdd = 0f;
			this.critFactorHeroAdd = 0.0;
			this.costTotemUpgradeFactor = 1.0;
			this.costHeroUpgradeFactor = 1.0;
			this.reviveTimeFactor = 1f;
			this.ultiCoolDownMaxFactor = 1f;
			this.bossTimeAdd = 0f;
			this.waveSkipChanceAdd = 0f;
			this.heroLevelRequiredForSkillDecrease = 0;
			this.epicBossDropMythstonesAdd = 0;
			this.epicBossDropMythstonesFactor = 1.0;
			this.freePackCooldownFactor = 1f;
			this.dragonSpawnRateFactor = 1f;
			this.prestigeMythFactor = 1.0;
			this.ringUltraCritCd = 0f;
			this.ringUltraCritChance = 0f;
			this.ringUltraCritFactor = 1.0;
			this.healthHeroArtifactAdd = 0.0;
			this.fastEnemySpawnBelow = 0;
			this.treasureRaidChance = 0f;
			this.treasureRaidBonusRatio = 0.0;
			this.qpIncrease = 0.0;
			this.gearAddMultiplierFactor = 0.0;
			this.freeUpgradeChance = 0f;
			this.commonArtifactFactor = 1.0;
			this.commonArtifactDamageFactor = 1.0;
			this.commonArtifactHealthFactor = 1.0;
			this.idleBonusAfter = 0f;
			this.goldIdleFactor = 1.0;
			this.autoUpgradeMaxCost = 0.0;
			this.goldBagAdDragonFactor = 1.0;
			this.powerupNonCritDamageDropChance = 0f;
			this.powerupNonCritDamageDuration = 0f;
			this.powerupNonCritDamageFactorBonus = 1.0;
			this.powerupCooldownDropChance = 0f;
			this.powerupCooldownDuration = 0f;
			this.powerupCooldownUltiAdd = 0f;
			this.powerupReviveDropChance = 0f;
			this.powerupReviveDuration = 0f;
			this.powerupReviveHealthRegen = 0.0;
			this.powerupReviveSpeedAdd = 0f;
			this.mineGoldFactor = 1.0;
			this.mineHealthFactor = 1.0;
			this.mineDamageFactor = 1.0;
			this.extraDamageTakenFromHeroesFactor = 0.0;
			this.extraDamageTakenFromRingFactor = 0.0;
			this.bountyIncreasePerDamageTakenFromHero = 0.0;
			this.charmDamageFactor = 1.0;
			this.charmHealthFactor = 1.0;
			this.charmGoldFactor = 1.0;
			this.heroItemsInFreeChestAdd = 0;
			this.currencyInFreeChestFactor = 1.0;
			this.artifactUpgradeCostFactor = 1.0;
			this.stagesToJumpInAdventure = 0;
			this.stageSkipPair = null;
		}

		public double GetDamageFactorForCurrentAttackersInTeam(int numAttackersInTeam)
		{
			if (numAttackersInTeam == 1)
			{
				return this.damageHeroFactorWithOneAttacker;
			}
			if (numAttackersInTeam == 2)
			{
				return this.damageHeroFactorWithTwoAttackers;
			}
			if (numAttackersInTeam >= 3)
			{
				return this.damageHeroFactorWithSeveralAttackers;
			}
			return 1.0;
		}

		public double GetHealthFactorForCurrentDefendersInTeam(int numDefendersInTeam)
		{
			if (numDefendersInTeam == 1)
			{
				return this.healthHeroFactorWithOneDefender;
			}
			if (numDefendersInTeam == 2)
			{
				return this.healthHeroFactorWithTwoDefenders;
			}
			if (numDefendersInTeam >= 3)
			{
				return this.healthHeroFactorWithSeveralDefenders;
			}
			return 1.0;
		}

		public double GetGoldFactorForCurrentSupportersInTeam(int numSupportersInTeam)
		{
			if (numSupportersInTeam == 1)
			{
				return this.goldFactorWithOneSupporter;
			}
			if (numSupportersInTeam == 2)
			{
				return this.goldFactorWithTwoSupporters;
			}
			if (numSupportersInTeam >= 3)
			{
				return this.goldFactorWithSeveralSupporters;
			}
			return 1.0;
		}

		public double healthHeroFactor;

		public double healthHeroFactorWithOneDefender;

		public double healthHeroFactorWithTwoDefenders;

		public double healthHeroFactorWithSeveralDefenders;

		public double healthHeroFactorMuliplier;

		public double healthBossFactor;

		public double goldFactor;

		public double goldFactorWithOneSupporter;

		public double goldFactorWithTwoSupporters;

		public double goldFactorWithSeveralSupporters;

		public double goldBossFactor;

		public double goldEnemyFactor;

		public double goldChestFactor;

		public double goldChestRaidFactor;

		public double goldOfflineFactor;

		public float chestChanceFactor;

		public float goldMultTenChanceAdd;

		public double damageFactor;

		public double damageTotemFactor;

		public double damageHeroFactor;

		public double damageHeroSkillFactor;

		public double damageHeroNonSkillFactor;

		public double damageHeroFactorWithOneAttacker;

		public double damageHeroFactorWithTwoAttackers;

		public double damageHeroFactorWithSeveralAttackers;

		public float critChanceTotemAdd;

		public double critFactorTotemAdd;

		public float critChanceHeroAdd;

		public double critFactorHeroAdd;

		public double costTotemUpgradeFactor;

		public double costHeroUpgradeFactor;

		public float reviveTimeFactor;

		public float ultiCoolDownMaxFactor;

		public float bossTimeAdd;

		public float autoTapDurationAdd;

		public float waveSkipChanceAdd;

		public int heroLevelRequiredForSkillDecrease;

		public int epicBossDropMythstonesAdd;

		public double epicBossDropMythstonesFactor;

		public double prestigeMythFactor;

		public float freePackCooldownFactor;

		public float dragonSpawnRateFactor;

		public int autoTapCountAdd;

		public int goldBagCountAdd;

		public int timeWarpCountAdd;

		public int powerUpCountAdd;

		public int refresherOrbCountAdd;

		public int horseshoeCountAdd;

		public int shieldCountAdd;

		public int destructionCountAdd;

		public float goldBagValueFactor;

		public float timeWarpSpeedFactor;

		public float timeWarpDurationAdd;

		public float horseshoeValueFactor;

		public float horseshoeDurationAdd;

		public float shieldDurationAdd;

		public float afterBossDurationFactor;

		public float ringUltraCritChance;

		public double ringUltraCritFactor;

		public float ringUltraCritCd;

		public double healthHeroArtifactAdd;

		public int fastEnemySpawnBelow;

		public float treasureRaidChance;

		public double treasureRaidBonusRatio;

		public double qpIncrease;

		public double healthEnemyFactor;

		public double damageEnemyFactor;

		public double extraDamageTakenFromHeroesFactor;

		public double extraDamageTakenFromRingFactor;

		public double damageBossFactor;

		public double gearAddMultiplierFactor;

		public float freeUpgradeChance;

		public double commonArtifactFactor;

		public double commonArtifactDamageFactor;

		public double commonArtifactHealthFactor;

		public float idleBonusAfter;

		public double goldIdleFactor;

		public double autoUpgradeMaxCost;

		public double goldBagAdDragonFactor;

		public float powerupNonCritDamageDropChance;

		public float powerupNonCritDamageDuration;

		public double powerupNonCritDamageFactorBonus;

		public float powerupCooldownDropChance;

		public float powerupCooldownDuration;

		public float powerupCooldownUltiAdd;

		public double powerupReviveHealthRegen;

		public float powerupReviveDropChance;

		public float powerupReviveDuration;

		public float powerupReviveSpeedAdd;

		public double mineHealthFactor;

		public double mineGoldFactor;

		public double mineDamageFactor;

		public double bountyIncreasePerDamageTakenFromHero;

		public double charmDamageFactor;

		public double charmHealthFactor;

		public double charmGoldFactor;

		public int heroItemsInFreeChestAdd;

		public double currencyInFreeChestFactor;

		public double artifactUpgradeCostFactor;

		public int stagesToJumpInAdventure;

		public LevelSkipPairs stageSkipPair;

		public double gearDamageFactor;

		public double gearHealthFactor;

		public double gearGoldFactor;

		public double milestoneBonusFactor;

		public double milestoneCostFactor;
	}
}
