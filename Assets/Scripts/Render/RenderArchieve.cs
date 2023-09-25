using System;
using Spine.Unity;
using UnityEngine;

namespace Render
{
	public class RenderArchieve : MonoBehaviour
	{
		public RenderArchieve()
		{
			RenderArchieve.inst = this;
		}

		public static RenderArchieve inst;

		public GameObject Bandit;

		public GameObject Wolf;

		public GameObject Spider;

		public GameObject Bat;

		public GameObject ElfSemiCorrupted;

		public GameObject ElfCorrupted;

		public GameObject DwarfSemiCorrupted;

		public GameObject DwarfCorrupted;

		public GameObject HumanCorrupted;

		public GameObject HumanSemiCorrupted;

		public GameObject Mangolies;

		public GameObject Chest;

		public GameObject Boss;

		public GameObject BossElf;

		public GameObject BossMangolies;

		public GameObject BossDwarf;

		public GameObject BossHuman;

		public GameObject BossWiseSnake;

		public GameObject Snake;

		public GameObject BossChristmas;

		public WiseSnakeInvulnerabilityVisualEffect wiseSnakeInvulnerabilityEffect;

		public GameObject Smoke;

		public GameObject DerekBookExplosion;

		public GameObject MagoliesProjectileExplosion;

		public GameObject SnakeProjectileExplosion;

		public GameObject TotemEarthImpact;

		public GameObject HitAnimation;

		public GameObject EnemyDeath;

		public GameObject TamFlare;

		public GameObject BuffAttackFast;

		public GameObject BuffAttackSlow;

		public GameObject BuffCritChance;

		public GameObject BuffDamageAdd;

		public GameObject BuffDamageDec;

		public GameObject BuffDeath;

		public GameObject BuffDefenseless;

		public GameObject BuffHealthRegen;

		public GameObject BuffImmunity;

		public GameObject BuffShield;

		public GameObject BuffStun;

		public GameObject BuffMiss;

		public GameObject BuffMark;

		public GameObject BuffCritDamage;

		public GameObject BuffDodgeChance;

		public GameObject BuffReduceRevive;

		public SmartProjectileRenderer daggerSmartProjectile;

		public SmartProjectileRenderer wendleBookProjectile;

		public SmartProjectileRenderer fireworksSmartProjectile;

		public SmartProjectileRenderer dynamiteProjectile;

		public SmartProjectileRenderer ProjectileChristmasOrnament;

		public GameObject ProjectileBoss;

		public GameObject ProjectileElfCorrupted;

		public GameObject ProjectileHumanCorrupted;

		public GameObject ProjectileGoblinBomb;

		public GameObject ProjectileFlute;

		public GameObject ProjectileSnake;

		public GameObject ProjectileWiseSnake;

		public VariantSpriteRenderer ProjectileAppleAid;

		public VariantSpriteRenderer ProjectileQuickStudy;

		public GameObject TotemLightning;

		public GameObject TotemFire;

		public GameObject TotemShard;

		public GameObject TotemThunderbolt;

		public GameObject TotemSmoke;

		public GameObject TotemEarth;

		public GameObject TotemEarthTap;

		public GameObject TotemEarthTapDisable;

		public GameObject TotemEarthStarfall;

		public GameObject ProjectileDwarfCorrupted;

		public GameObject ProjectileMagolies;

		public GameObject TotemIceManaGather;

		public GameObject Duck;

		public GameObject AdDragon;

		public SpriteRendererObject[] DropGolds;

		public SpriteRendererObject[] DropGoldTris;

		public SpriteRendererObject[] DropGoldSqr;

		public SpriteRendererObject[] DropMythstones;

		public SpriteRendererObject[] DropCredits;

		public SpriteRendererObject[] DropTokens;

		public SpriteRendererObject[] DropScraps;

		public SpriteRendererObject[] DropAeons;

		public SpriteRendererObject[] DropCandies;

		public SpriteRendererObject[] DropTrinketBoxes;

		public SpriteRendererObject DropPowerupCritChance;

		public SpriteRendererObject DropPowerupCooldown;

		public SpriteRendererObject DropPowerupRevive;

		public GameObject HealthBar;

		public GameObject TimeBar;

		public TextContainer FloaterDamage;

		public GameObject Explossion;

		public SkeletonDataAsset ExplossionAnimation;

		public SkeletonDataAsset fireworkProjectileAnimation;

		public GameObject projectileSwarm;

		public SkeletonDataAsset projectileSwarmAnimData;

		public SkeletonDataAsset animDataBandit;

		public SkeletonDataAsset animDataWolf;

		public SkeletonDataAsset animDataSpider;

		public SkeletonDataAsset animDataBat;

		public SkeletonDataAsset animDataElfSemiCorrupted;

		public SkeletonDataAsset animDataElfCorrupted;

		public SkeletonDataAsset animDataDwarfSemiCorrupted;

		public SkeletonDataAsset animDataDwarfCorrupted;

		public SkeletonDataAsset animDataHumanCorrupted;

		public SkeletonDataAsset animDataHumanSemiCorrupted;

		public SkeletonDataAsset animDataMagolies;

		public SkeletonDataAsset animDataChest;

		public SkeletonDataAsset animDataBoss;

		public SkeletonDataAsset animDataBossElf;

		public SkeletonDataAsset animDataBossMangolies;

		public SkeletonDataAsset animDataBossDwarf;

		public SkeletonDataAsset animDataBossHuman;

		public SkeletonDataAsset animDataBossWiseSnake;

		public SkeletonDataAsset animDataSnake;

		public SkeletonDataAsset animDataBossChristmas;

		public SkeletonDataAsset animDataTotemShard;

		public SkeletonDataAsset animDataTotemThunderBolt;

		public SkeletonDataAsset animDataTotemSmoke;

		public SkeletonDataAsset animDataTotemEarth;

		public SkeletonDataAsset animDataTotemEarthImpact;

		public SkeletonDataAsset animDataTotemEarthTap;

		public SkeletonDataAsset animDataTotemEarthTapDisable;

		public SkeletonDataAsset animDataTotemEarthStarfall;

		public SkeletonDataAsset animDataProjectileDwarfCorrupted;

		public SkeletonDataAsset animDataProjectileMagolies;

		public SkeletonDataAsset animDataProjectileSnake;

		public SkeletonDataAsset animDataTamFlare;

		public SkeletonDataAsset animDataHiltExcalibur;

		public SkeletonDataAsset animDataTotemLightning;

		public SkeletonDataAsset animDataTotemFire;

		public SkeletonDataAsset animDataSmoke;

		public SkeletonDataAsset animDataDerekBookExplosion;

		public SkeletonDataAsset animDataHitAnimation;

		public SkeletonDataAsset animDataEnemyDeath;

		public SkeletonDataAsset animDataTotemIceManaGather;

		public SkeletonDataAsset animDataDuck;

		public SkeletonDataAsset animDataAdDragon;

		public SkeletonDataAsset animDataBuffAttackFast;

		public SkeletonDataAsset animDataBuffAttackSlow;

		public SkeletonDataAsset animDataBuffCritChance;

		public SkeletonDataAsset animDataBuffDamageAdd;

		public SkeletonDataAsset animDataBuffDamageDec;

		public SkeletonDataAsset animDataBuffDeath;

		public SkeletonDataAsset animDataBuffDefenseless;

		public SkeletonDataAsset animDataBuffHealthRegen;

		public SkeletonDataAsset animDataBuffImmunity;

		public SkeletonDataAsset animDataBuffShield;

		public SkeletonDataAsset animDataBuffStun;

		public SkeletonDataAsset animDataBuffMiss;

		public SkeletonDataAsset animDataBuffMark;

		public SkeletonDataAsset animDataBuffCritDamage;

		public SkeletonDataAsset animDataBuffDodgeChance;

		public SkeletonDataAsset animDataBuffReduceRevive;
	}
}
