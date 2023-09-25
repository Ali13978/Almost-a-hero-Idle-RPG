using System;
using System.Collections;
using System.Collections.Generic;
using DynamicLoading;
using Simulation;
using Spine;
using Static;
using Ui;
using UnityEngine;
using UnityEngine.UI;

namespace Render
{
	public class RenderManager : RenderArchieve
	{
		public SpineAnim GetHeroSpineAnimForVictoryScreen(string id)
		{
			switch (id)
			{
			case "HORATIO":
			{
				SpineAnim instance = this.poolHoratio.GetInstance();
				instance.Apply(SpineAnimHoratio.animVictory, 0f, false);
				return instance;
			}
			case "THOUR":
			{
				SpineAnim instance = this.poolThour.GetInstance();
				instance.Apply(SpineAnimThour.animVictory, 0f, false);
				return instance;
			}
			case "IDA":
			{
				SpineAnim instance = this.poolIda.GetInstance();
				instance.Apply(SpineAnimIda.animVictory, 0f, false);
				return instance;
			}
			case "KIND_LENNY":
			{
				SpineAnim instance = this.poolKindLenny.GetInstance();
				instance.Apply(SpineAnimKindLenny.animVictory, 0f, false);
				return instance;
			}
			case "DEREK":
			{
				SpineAnim instance = this.poolDerek.GetInstance();
				instance.Apply(SpineAnimDerek.animVictory, 0f, false);
				return instance;
			}
			case "SHEELA":
			{
				SpineAnim instance = this.poolSheela.GetInstance();
				instance.Apply(SpineAnimSheela.animVictory, 0f, false);
				return instance;
			}
			case "BOMBERMAN":
			{
				SpineAnim instance = this.poolBomberman.GetInstance();
				instance.Apply(SpineAnimBomberman.animVictory, 0f, false);
				return instance;
			}
			case "SAM":
			{
				SpineAnim instance = this.poolSam.GetInstance();
				instance.Apply(SpineAnimSam.animVictory, 0f, false);
				return instance;
			}
			case "BLIND_ARCHER":
			{
				SpineAnim instance = this.poolBlindArcher.GetInstance();
				instance.Apply(SpineAnimBlindArcher.animVictory, 0f, false);
				return instance;
			}
			case "JIM":
			{
				SpineAnim instance = this.poolJim.GetInstance();
				instance.Apply(SpineAnimJim.animVictory, 0f, false);
				return instance;
			}
			case "TAM":
			{
				SpineAnim instance = this.poolTam.GetInstance();
				instance.Apply(SpineAnimTam.animVictory, 0f, false);
				return instance;
			}
			case "WARLOCK":
			{
				SpineAnim instance = this.poolWarlock.GetInstance();
				instance.Apply(SpineAnimWarlock.animVictory, 0f, false);
				return instance;
			}
			case "GOBLIN":
			{
				SpineAnim instance = this.poolGoblin.GetInstance();
				instance.Apply(SpineAnimGoblin.animVictory, 0f, false);
				return instance;
			}
			case "BABU":
			{
				SpineAnim instance = this.poolBabu.GetInstance();
				instance.Apply(SpineAnimBabu.animVictory, 0f, false);
				return instance;
			}
			case "DRUID":
			{
				SpineAnim instance = this.poolDruid.GetInstance();
				instance.Apply(SpineAnimDruid.animVictory, 0f, false);
				return instance;
			}
			}
			throw new NotImplementedException();
		}

		private void Start()
		{
			Transform transform = this._sceneRenderers.transform;
			Transform transform2 = this._uiSceneRenderers.transform;
			this.poolHoratio = new RenderPoolSpineAnimPostDestroy<SpineAnimHoratio>(transform);
			this.poolThour = new RenderPoolSpineAnimPostDestroy<SpineAnimThour>(transform);
			this.poolIda = new RenderPoolSpineAnimPostDestroy<SpineAnimIda>(transform);
			this.poolKindLenny = new RenderPoolSpineAnimPostDestroy<SpineAnimKindLenny>(transform);
			this.poolDerek = new RenderPoolSpineAnimPostDestroy<SpineAnimDerek>(transform);
			this.poolSheela = new RenderPoolSpineAnimPostDestroy<SpineAnimSheela>(transform);
			this.poolBomberman = new RenderPoolSpineAnimPostDestroy<SpineAnimBomberman>(transform);
			this.poolSam = new RenderPoolSpineAnimPostDestroy<SpineAnimSam>(transform);
			this.poolBlindArcher = new RenderPoolSpineAnimPostDestroy<SpineAnimBlindArcher>(transform);
			this.poolJim = new RenderPoolSpineAnimPostDestroy<SpineAnimJim>(transform);
			this.poolTam = new RenderPoolSpineAnimPostDestroy<SpineAnimTam>(transform);
			this.poolWarlock = new RenderPoolSpineAnimPostDestroy<SpineAnimWarlock>(transform);
			this.poolGoblin = new RenderPoolSpineAnimPostDestroy<SpineAnimGoblin>(transform);
			this.poolBabu = new RenderPoolSpineAnimPostDestroy<SpineAnimBabu>(transform);
			this.poolDruid = new RenderPoolSpineAnimPostDestroy<SpineAnimDruid>(transform);
			this.poolBandit = new RenderPoolSpineAnimPostDeactivate<SpineAnimBandit>(transform);
			this.poolWolf = new RenderPoolSpineAnimPostDeactivate<SpineAnimWolf>(transform);
			this.poolSpider = new RenderPoolSpineAnimPostDeactivate<SpineAnimSpider>(transform);
			this.poolBat = new RenderPoolSpineAnimPostDeactivate<SpineAnimBat>(transform);
			this.poolElfSemiCorrupted = new RenderPoolSpineAnimPostDeactivate<SpineAnimElfSemiCorrupted>(transform);
			this.poolElfCorrupted = new RenderPoolSpineAnimPostDeactivate<SpineAnimElfCorrupted>(transform);
			this.poolDwarfSemiCorrupted = new RenderPoolSpineAnimPostDeactivate<SpineAnimDwarfSemiCorrupted>(transform);
			this.poolDwarfCorrupted = new RenderPoolSpineAnimPostDeactivate<SpineAnimDwarfCorrupted>(transform);
			this.poolHumanCorrupted = new RenderPoolSpineAnimPostDeactivate<SpineAnimHumanCorrupted>(transform);
			this.poolHumanSemiCorrupted = new RenderPoolSpineAnimPostDeactivate<SpineAnimHumanSemiCorrupted>(transform);
			this.poolMagolies = new RenderPoolSpineAnimPostDeactivate<SpineAnimMagolies>(transform);
			this.poolChest = new RenderPoolSpineAnimPostDeactivate<SpineAnimChest>(transform);
			this.poolBoss = new RenderPoolSpineAnimPostDeactivate<SpineAnimBoss>(transform);
			this.poolBossElf = new RenderPoolSpineAnimPostDeactivate<SpineAnimBossElf>(transform);
			this.poolBossHuman = new RenderPoolSpineAnimPostDeactivate<SpineAnimBossHuman>(transform);
			this.poolBossMangolies = new RenderPoolSpineAnimPostDeactivate<SpineAnimBossMangolies>(transform);
			this.poolBossDwarf = new RenderPoolSpineAnimPostDeactivate<SpineAnimBossDwarf>(transform);
			this.poolBossWiseSnake = new RenderPoolSpineAnimPostDeactivate<SpineAnimBossWiseSnake>(transform);
			this.poolSnake = new RenderPoolSpineAnimPostDeactivate<SpineAnimSnake>(transform);
			this.poolBossChristmas = new RenderPoolSpineAnimPostDeactivate<SpineAnimBossChristmas>(transform);
			this.poolWiseSnakeInvulnerabilityEffect = new RenderPoolWiseSnakeInvulnerabilityEffectPostDeactivate(this.wiseSnakeInvulnerabilityEffect, transform);
			this.poolSmoke = new RenderPoolSpineAnimPostDeactivate<SpineAnimSmoke>(transform);
			this.poolBombermanExplosion = new RenderPoolSpineAnimPostDeactivate<SpineAnimBombermanExplosion>(transform);
			this.poolDerekBookExplosion = new RenderPoolSpineAnimPostDeactivate<SpineAnimDerekBookExplosion>(transform);
			this.poolMagoliesProjectileExplosion = new RenderPoolSpineAnimPostDeactivate<SpineAnimMagoliesProjectileExplosion>(transform);
			this.poolSnakeProjectileExplosion = new RenderPoolSpineAnimPostDeactivate<SpineAnimSnakeProjectileExplosion>(transform);
			this.poolGreenAppleExplosion = new RenderPoolSpineAnimPostDestroy<SpineAnimGreenAppleExplosion>(transform);
			this.poolSamBottleExplosion = new RenderPoolSpineAnimPostDestroy<SpineAnimSamBottleExplosion>(transform);
			this.poolHitAnimation = new RenderPoolSpineAnimPostDeactivate<SpineAnimHit>(transform);
			this.poolEnemyDeath = new RenderPoolSpineAnimPostDeactivate<SpineAnimEnemyDeath>(transform);
			this.poolTotemLightning = new RenderPoolSpineAnimPostDeactivate<SpineAnimTotemLightning>(transform);
			this.poolTotemThunderbolt = new RenderPoolSpineAnimPostDeactivate<SpineAnimTotemThunderbolt>(transform);
			this.poolTotemFire = new RenderPoolSpineAnimPostDeactivate<SpineAnimTotemFire>(transform);
			this.poolTotemSmoke = new RenderPoolSpineAnimPostDeactivate<SpineAnimTotemSmoke>(transform);
			this.poolBuffAttackFast = new RenderPoolSpineAnimPostDeactivate<SpineAnimBuffAttackFast>(transform);
			this.poolBuffAttackSlow = new RenderPoolSpineAnimPostDeactivate<SpineAnimBuffAttackSlow>(transform);
			this.poolBuffCritChance = new RenderPoolSpineAnimPostDeactivate<SpineAnimBuffCritChance>(transform);
			this.poolBuffDamageAdd = new RenderPoolSpineAnimPostDeactivate<SpineAnimBuffDamageAdd>(transform);
			this.poolBuffDamageDec = new RenderPoolSpineAnimPostDeactivate<SpineAnimBuffDamageDec>(transform);
			this.poolBuffDeath = new RenderPoolSpineAnimPostDeactivate<SpineAnimBuffDeath>(transform);
			this.poolBuffDefenseless = new RenderPoolSpineAnimPostDeactivate<SpineAnimBuffDefenseless>(transform);
			this.poolBuffHealthRegen = new RenderPoolSpineAnimPostDeactivate<SpineAnimBuffHealthRegen>(transform);
			this.poolBuffImmunity = new RenderPoolSpineAnimPostDeactivate<SpineAnimBuffImmunity>(transform);
			this.poolBuffShield = new RenderPoolSpineAnimPostDeactivate<SpineAnimBuffShield>(transform);
			this.poolBuffStun = new RenderPoolSpineAnimPostDeactivate<SpineAnimBuffStun>(transform);
			this.poolBuffMiss = new RenderPoolSpineAnimPostDeactivate<SpineAnimBuffMiss>(transform);
			this.poolBuffMark = new RenderPoolSpineAnimPostDeactivate<SpineAnimBuffMark>(transform);
			this.poolBuffCritDamage = new RenderPoolSpineAnimPostDeactivate<SpineAnimBuffCritDamage>(transform);
			this.poolBuffDodgeChance = new RenderPoolSpineAnimPostDeactivate<SpineAnimBuffDodgeChance>(transform);
			this.poolBuffReduceRevive = new RenderPoolSpineAnimPostDeactivate<SpineAnimBuffReduceRevive>(transform);
			this.poolIceManaGather = new RenderPoolSpineAnimPostDeactivate<SpineAnimIceManaGather>(transform);
			this.poolDuck = new RenderPoolSpineAnimPostDeactivate<SpineAnimDuck>(transform);
			this.poolAdDragon = new RenderPoolSpineAnimPostDeactivate<SpineAnimAdDragon>(transform);
			this.poolsDropGold = new RenderPoolSpriteObject[this.DropGolds.Length];
			for (int i = 0; i < this.DropGolds.Length; i++)
			{
				this.poolsDropGold[i] = new RenderPoolSpriteObjectPostDeactivate(this.DropGolds[i], transform);
			}
			this.poolsDropGoldTri = new RenderPoolSpriteObject[this.DropGoldTris.Length];
			for (int j = 0; j < this.DropGoldTris.Length; j++)
			{
				this.poolsDropGoldTri[j] = new RenderPoolSpriteObjectPostDeactivate(this.DropGoldTris[j], transform);
			}
			this.poolsDropGoldSqr = new RenderPoolSpriteObject[this.DropGoldSqr.Length];
			for (int k = 0; k < this.DropGoldSqr.Length; k++)
			{
				this.poolsDropGoldSqr[k] = new RenderPoolSpriteObjectPostDeactivate(this.DropGoldSqr[k], transform);
			}
			this.poolsDropMythtone = new RenderPoolSpriteObject[this.DropMythstones.Length];
			for (int l = 0; l < this.DropMythstones.Length; l++)
			{
				this.poolsDropMythtone[l] = new RenderPoolSpriteObjectPostDeactivate(this.DropMythstones[l], transform);
			}
			this.poolsDropCredit = new RenderPoolSpriteObject[this.DropCredits.Length];
			for (int m = 0; m < this.DropCredits.Length; m++)
			{
				this.poolsDropCredit[m] = new RenderPoolSpriteObjectPostDeactivate(this.DropCredits[m], transform);
			}
			this.poolsDropToken = new RenderPoolSpriteObject[this.DropTokens.Length];
			for (int n = 0; n < this.DropTokens.Length; n++)
			{
				this.poolsDropToken[n] = new RenderPoolSpriteObjectPostDeactivate(this.DropTokens[n], transform);
			}
			this.poolsDropScrap = new RenderPoolSpriteObject[this.DropScraps.Length];
			for (int num = 0; num < this.DropScraps.Length; num++)
			{
				this.poolsDropScrap[num] = new RenderPoolSpriteObjectPostDeactivate(this.DropScraps[num], transform);
			}
			this.poolsDropAeon = new RenderPoolSpriteObject[this.DropAeons.Length];
			for (int num2 = 0; num2 < this.DropAeons.Length; num2++)
			{
				this.poolsDropAeon[num2] = new RenderPoolSpriteObjectPostDeactivate(this.DropAeons[num2], transform);
			}
			this.poolsDropCandy = new RenderPoolSpriteObject[this.DropCandies.Length];
			for (int num3 = 0; num3 < this.DropCandies.Length; num3++)
			{
				this.poolsDropCandy[num3] = new RenderPoolSpriteObjectPostDeactivate(this.DropCandies[num3], transform);
			}
			this.poolsDropTrinketBox = new RenderPoolSpriteObject[this.DropTrinketBoxes.Length];
			for (int num4 = 0; num4 < this.DropTrinketBoxes.Length; num4++)
			{
				this.poolsDropTrinketBox[num4] = new RenderPoolSpriteObjectPostDeactivate(this.DropTrinketBoxes[num4], transform);
			}
			this.poolDropPowerupCritChance = new RenderPoolSpriteObjectPostDeactivate(this.DropPowerupCritChance, transform);
			this.poolDropPowerupCooldown = new RenderPoolSpriteObjectPostDeactivate(this.DropPowerupCooldown, transform);
			this.poolDropPowerupRevive = new RenderPoolSpriteObjectPostDeactivate(this.DropPowerupRevive, transform);
			this.poolBuffTrinketEffect = new RenderPoolSpriteRendererContainerPostDeactivate(transform);
			this.poolHealthBar = new RenderPoolGameObjectPostDeactivate(this.HealthBar, transform);
			this.poolTimeBar = new RenderPoolGameObjectPostDeactivate(this.TimeBar, transform);
			this.poolFloaterDamage = new RenderPoolTextPostDeactivate(this.FloaterDamage, this._floaterContainer.transform);
			this.poolProjectileBoss = new RenderPoolGameObjectPostDeactivate(this.ProjectileBoss, transform);
			this.poolProjectileAppleBombard = new RenderPoolVariantSpriteDestroy(null, transform);
			this.poolProjectileFlute = new RenderPoolGameObjectPostDeactivate(this.ProjectileFlute, transform);
			this.poolProjectileSamAxe = new RenderPoolGameObjectPostDestroy(null, transform);
			this.poolProjectileBlindArcher = new RenderPoolGameObjectPostDestroy(null, transform);
			this.poolProjectileElfCorrupted = new RenderPoolGameObjectPostDeactivate(this.ProjectileElfCorrupted, transform);
			this.poolProjectileHumanCorrupted = new RenderPoolGameObjectPostDeactivate(this.ProjectileHumanCorrupted, transform);
			this.poolProjectileGoblinBomb = new RenderPoolGameObjectPostDestroy(null, transform);
			this.projectilePools = new Dictionary<Projectile.Type, RenderPool<GameObject>>
			{
				{
					Projectile.Type.BOSS,
					this.poolProjectileBoss
				},
				{
					Projectile.Type.SAM_AXE,
					this.poolProjectileSamAxe
				},
				{
					Projectile.Type.BLIND_ARCHER_ATTACK,
					this.poolProjectileBlindArcher
				},
				{
					Projectile.Type.BLIND_ARCHER_AUTO,
					this.poolProjectileBlindArcher
				},
				{
					Projectile.Type.BLIND_ARCHER_ULTI,
					this.poolProjectileBlindArcher
				},
				{
					Projectile.Type.ELF_CORRUPTED,
					this.poolProjectileElfCorrupted
				},
				{
					Projectile.Type.HUMAN_CORRUPTED,
					this.poolProjectileHumanCorrupted
				},
				{
					Projectile.Type.GOBLIN_SMOKE_BOMB,
					this.poolProjectileGoblinBomb
				},
				{
					Projectile.Type.FLUTE,
					this.poolProjectileFlute
				}
			};
			this.poolProjectileAppleAid = new RenderPoolVariantSpriteDeactivate(this.ProjectileAppleAid, transform);
			this.poolProjectileQuickStudy = new RenderPoolVariantSpriteDeactivate(this.ProjectileQuickStudy, transform);
			this.poolProjectileBabuTeaCup = new RenderPoolVariantSpriteDeactivate(null, transform);
			this.poolProjectileDerekMagicBall = new RenderPoolVariantSpriteDestroy(null, transform);
			this.poolProjectileTotemShard = new RenderPoolSpineAnimPostDeactivate<SpineAnimProjectileTotemShard>(transform);
			this.poolProjectileDerekMeteor = new RenderPoolSpineAnimPostDestroy<SpineAnimProjectileDerekMeteor>(transform);
			this.poolProjectileDwarfCorrupted = new RenderPoolSpineAnimPostDeactivate<SpineAnimProjectileDwarfCorrupted>(transform);
			this.poolProjectileMagolies = new RenderPoolSpineAnimPostDeactivate<SpineAnimProjectileMagolies>(transform);
			this.poolProjectileSnake = new RenderPoolSpineAnimPostDeactivate<SpineAnimProjectileSnake>(transform);
			this.poolProjectileWiseSnake = new RenderPoolGameObjectPostDeactivate(this.ProjectileWiseSnake, transform);
			this.poolProjectileTotemEarth = new RenderPoolSpineAnimPostDeactivate<SpineAnimTotemEarth>(transform);
			this.poolProjectileTotemEarthImpact = new RenderPoolSpineAnimPostDeactivate<SpineAnimTotemEarthImpact>(transform);
			this.poolProjectileTotemEarthTap = new RenderPoolSpineAnimPostDeactivate<SpineAnimTotemEarthTap>(transform);
			this.poolProjectileTotemEarthTapDisable = new RenderPoolSpineAnimPostDeactivate<SpineAnimTotemEarthTapDisable>(transform);
			this.poolProjectileTotemEarthStarfall = new RenderPoolSpineAnimPostDeactivate<SpineAnimTotemEarthStarfall>(transform);
			this.poolProjectileTamFlare = new RenderPoolSpineAnimPostDeactivate<SpineAnimTamFlare>(transform);
			this.poolProjectileGoblinSmoke = new RenderPoolSpineAnimPostDestroy<SpineAnimGoblinSmoke>(transform);
			this.poolProjectileWarlockSwarm = new RenderPoolSpineAnimPostDestroy<SpineAnimWarlockSwarm>(transform);
			this.poolProjectileWarlockAttack = new RenderPoolSpineAnimPostDestroy<SpineAnimWarlockAttack>(transform);
			this.poolProjectileBombermanFirework = new RenderPoolSmartProjectilePostDestroy(this.fireworksSmartProjectile, transform);
			this.poolSmartProjectileSheela = new RenderPoolSmartProjectilePostDestroy(this.daggerSmartProjectile, transform);
			this.poolSmartProjectileWendle = new RenderPoolSmartProjectilePostDestroy(this.wendleBookProjectile, transform);
			this.poolSmartProjectileBoomer = new RenderPoolSmartProjectilePostDestroy(this.dynamiteProjectile, transform);
			this.poolSmartProjectileGoblin = new RenderPoolSmartProjectilePostDestroy(null, transform);
			this.poolSmartProjectileLenny = new RenderPoolSmartProjectilePostDestroy(null, transform);
			this.poolSmartProjectileHiltExcalibur = new RenderPoolSmartProjectilePostDestroy(null, transform);
			this.poolSmartProjectileSamBottle = new RenderPoolSmartProjectilePostDestroy(null, transform);
			this.poolSmartProjectileBabu = new RenderPoolSmartProjectilePostDestroy(null, transform);
			this.poolSmartProjectileOrnamentDrop = new RenderPoolSmartProjectilePostDeactivate(this.ProjectileChristmasOrnament, transform);
			this.poolDruidStampedeAnimal = new RenderPoolSpineAnimPostDestroy<SpineAnimDruidStampedeAnimal>(transform);
			this.poolDruidLarry = new RenderPoolSpineAnimPostDestroy<SpineAnimDruidLarry>(transform);
			this.poolDruidCurly = new RenderPoolSpineAnimPostDestroy<SpineAnimDruidCurly>(transform);
			this.poolDruidMoe = new RenderPoolSpineAnimPostDestroy<SpineAnimDruidMoe>(transform);
			this.supportAnimalsPools = new Dictionary<int, RenderPoolSpineAnim>
			{
				{
					0,
					this.poolDruidLarry
				},
				{
					1,
					this.poolDruidCurly
				},
				{
					2,
					this.poolDruidMoe
				}
			};
			this.smartProjectilePools = new Dictionary<Projectile.Type, RenderPoolSmartProjectile>
			{
				{
					Projectile.Type.APPLE,
					this.poolSmartProjectileLenny
				},
				{
					Projectile.Type.BOMBERMAN_DINAMIT,
					this.poolSmartProjectileBoomer
				},
				{
					Projectile.Type.BOMBERMAN_FIRE_CRACKER,
					this.poolSmartProjectileBoomer
				},
				{
					Projectile.Type.GOBLIN_SACK,
					this.poolSmartProjectileGoblin
				},
				{
					Projectile.Type.SAM_BOTTLE,
					this.poolSmartProjectileSamBottle
				}
			};
			bool quiet = false;
			SpineAnimBandit.Prefab = this.Bandit;
			SkeletonData skeletonData = this.animDataBandit.GetSkeletonData(quiet);
			SpineAnimBandit.animSpawnTranslate = skeletonData.FindAnimation("spawn");
			SpineAnimBandit.animSpawnDrop = skeletonData.FindAnimation("spawn_to_idle");
			SpineAnimBandit.animIdle = skeletonData.FindAnimation("idle_1");
			SpineAnimBandit.animsAttack = new Spine.Animation[]
			{
				skeletonData.FindAnimation("attack_1"),
				skeletonData.FindAnimation("attack_2"),
				skeletonData.FindAnimation("attack_3")
			};
			SpineAnimBandit.animDie = skeletonData.FindAnimation("death");
			SpineAnimWolf.Prefab = this.Wolf;
			SkeletonData skeletonData2 = this.animDataWolf.GetSkeletonData(quiet);
			SpineAnimWolf.animSpawnTranslate = skeletonData2.FindAnimation("spawn");
			SpineAnimWolf.animSpawnDrop = skeletonData2.FindAnimation("spawn_to_idle");
			SpineAnimWolf.animIdle = skeletonData2.FindAnimation("idle_1");
			SpineAnimWolf.animsAttack = new Spine.Animation[]
			{
				skeletonData2.FindAnimation("attack_1"),
				skeletonData2.FindAnimation("attack_2"),
				skeletonData2.FindAnimation("attack_3")
			};
			SpineAnimWolf.animDie = skeletonData2.FindAnimation("death");
			SpineAnimSpider.Prefab = this.Spider;
			SkeletonData skeletonData3 = this.animDataSpider.GetSkeletonData(quiet);
			SpineAnimSpider.animSpawnTranslate = skeletonData3.FindAnimation("spawn");
			SpineAnimSpider.animSpawnDrop = skeletonData3.FindAnimation("spawn to idle");
			SpineAnimSpider.animIdle = skeletonData3.FindAnimation("idle");
			SpineAnimSpider.animsAttack = new Spine.Animation[]
			{
				skeletonData3.FindAnimation("attack_1"),
				skeletonData3.FindAnimation("attack_2")
			};
			SpineAnimSpider.animDie = skeletonData3.FindAnimation("death");
			SpineAnimBat.Prefab = this.Bat;
			SkeletonData skeletonData4 = this.animDataBat.GetSkeletonData(quiet);
			SpineAnimBat.animSpawnDrop = skeletonData4.FindAnimation("spawn");
			SpineAnimBat.animIdle = skeletonData4.FindAnimation("idle");
			SpineAnimBat.animsAttack = new Spine.Animation[]
			{
				skeletonData4.FindAnimation("attack_1"),
				skeletonData4.FindAnimation("attack_2")
			};
			SpineAnimBat.animDie = skeletonData4.FindAnimation("death");
			SpineAnimElfSemiCorrupted.Prefab = this.ElfSemiCorrupted;
			SkeletonData skeletonData5 = this.animDataElfSemiCorrupted.GetSkeletonData(quiet);
			SpineAnimElfSemiCorrupted.animSpawnTranslate = skeletonData5.FindAnimation("spawn");
			SpineAnimElfSemiCorrupted.animSpawnDrop = skeletonData5.FindAnimation("spawn_to_idle");
			SpineAnimElfSemiCorrupted.animIdle = skeletonData5.FindAnimation("idle");
			SpineAnimElfSemiCorrupted.animsAttack = new Spine.Animation[]
			{
				skeletonData5.FindAnimation("attack_1"),
				skeletonData5.FindAnimation("attack_2")
			};
			SpineAnimElfSemiCorrupted.animDie = skeletonData5.FindAnimation("death");
			SpineAnimElfCorrupted.Prefab = this.ElfCorrupted;
			SkeletonData skeletonData6 = this.animDataElfCorrupted.GetSkeletonData(quiet);
			SpineAnimElfCorrupted.animSpawnTranslate = skeletonData6.FindAnimation("spawn");
			SpineAnimElfCorrupted.animSpawnDrop = skeletonData6.FindAnimation("spawn_to_idle");
			SpineAnimElfCorrupted.animIdle = skeletonData6.FindAnimation("idle");
			SpineAnimElfCorrupted.animsAttack = new Spine.Animation[]
			{
				skeletonData6.FindAnimation("attack_1")
			};
			SpineAnimElfCorrupted.animDie = skeletonData6.FindAnimation("death");
			SpineAnimDwarfSemiCorrupted.Prefab = this.DwarfSemiCorrupted;
			SkeletonData skeletonData7 = this.animDataDwarfSemiCorrupted.GetSkeletonData(quiet);
			SpineAnimDwarfSemiCorrupted.animSpawnTranslate = skeletonData7.FindAnimation("spawn");
			SpineAnimDwarfSemiCorrupted.animSpawnDrop = skeletonData7.FindAnimation("spawn_to_idle");
			SpineAnimDwarfSemiCorrupted.animIdle = skeletonData7.FindAnimation("idle_1");
			SpineAnimDwarfSemiCorrupted.animsAttack = new Spine.Animation[]
			{
				skeletonData7.FindAnimation("attack_1")
			};
			SpineAnimDwarfSemiCorrupted.animDie = skeletonData7.FindAnimation("death");
			SpineAnimDwarfCorrupted.Prefab = this.DwarfCorrupted;
			SkeletonData skeletonData8 = this.animDataDwarfCorrupted.GetSkeletonData(quiet);
			SpineAnimDwarfCorrupted.animSpawnTranslate = skeletonData8.FindAnimation("spawn");
			SpineAnimDwarfCorrupted.animSpawnDrop = skeletonData8.FindAnimation("spawn_to_idle");
			SpineAnimDwarfCorrupted.animIdle = skeletonData8.FindAnimation("idle_1");
			SpineAnimDwarfCorrupted.animsAttack = new Spine.Animation[]
			{
				skeletonData8.FindAnimation("attack_1"),
				skeletonData8.FindAnimation("attack_2")
			};
			SpineAnimDwarfCorrupted.animDie = skeletonData8.FindAnimation("death");
			SpineAnimHumanCorrupted.Prefab = this.HumanCorrupted;
			SkeletonData skeletonData9 = this.animDataHumanCorrupted.GetSkeletonData(quiet);
			SpineAnimHumanCorrupted.animSpawnTranslate = skeletonData9.FindAnimation("spawn");
			SpineAnimHumanCorrupted.animSpawnDrop = skeletonData9.FindAnimation("spawn_to_idle");
			SpineAnimHumanCorrupted.animIdle = skeletonData9.FindAnimation("idle");
			SpineAnimHumanCorrupted.animsAttack = new Spine.Animation[]
			{
				skeletonData9.FindAnimation("attack_1"),
				skeletonData9.FindAnimation("attack_2")
			};
			SpineAnimHumanCorrupted.animDie = skeletonData9.FindAnimation("death");
			SpineAnimHumanSemiCorrupted.Prefab = this.HumanSemiCorrupted;
			SkeletonData skeletonData10 = this.animDataHumanSemiCorrupted.GetSkeletonData(quiet);
			SpineAnimHumanSemiCorrupted.animSpawnTranslate = skeletonData10.FindAnimation("spawn");
			SpineAnimHumanSemiCorrupted.animSpawnDrop = skeletonData10.FindAnimation("spawn to idle");
			SpineAnimHumanSemiCorrupted.animIdle = skeletonData10.FindAnimation("idle");
			SpineAnimHumanSemiCorrupted.animsAttack = new Spine.Animation[]
			{
				skeletonData10.FindAnimation("attack_1"),
				skeletonData10.FindAnimation("attack_2")
			};
			SpineAnimHumanSemiCorrupted.animDie = skeletonData10.FindAnimation("death");
			SpineAnimMagolies.Prefab = this.Mangolies;
			SkeletonData skeletonData11 = this.animDataMagolies.GetSkeletonData(quiet);
			SpineAnimMagolies.animSpawnTranslate = skeletonData11.FindAnimation("spawn");
			SpineAnimMagolies.animSpawnDrop = skeletonData11.FindAnimation("spawn_to_idle");
			SpineAnimMagolies.animIdle = skeletonData11.FindAnimation("idle");
			SpineAnimMagolies.animsAttack = new Spine.Animation[]
			{
				skeletonData11.FindAnimation("attack_1"),
				skeletonData11.FindAnimation("attack_2")
			};
			SpineAnimMagolies.animsDie = new Spine.Animation[]
			{
				skeletonData11.FindAnimation("death_1"),
				skeletonData11.FindAnimation("death_2")
			};
			SpineAnimChest.Prefab = this.Chest;
			SkeletonData skeletonData12 = this.animDataChest.GetSkeletonData(quiet);
			SpineAnimChest.animSpawnTranslate = skeletonData12.FindAnimation("spawn");
			SpineAnimChest.animSpawnDrop = skeletonData12.FindAnimation("spawn_to_idle");
			SpineAnimChest.animIdle = skeletonData12.FindAnimation("idle");
			SpineAnimChest.animsHit = new Spine.Animation[]
			{
				skeletonData12.FindAnimation("hit_1"),
				skeletonData12.FindAnimation("hit_2")
			};
			SpineAnimChest.animDie = skeletonData12.FindAnimation("death");
			SpineAnimBoss.Prefab = this.Boss;
			SkeletonData skeletonData13 = this.animDataBoss.GetSkeletonData(quiet);
			SpineAnimBoss.animSpawn = skeletonData13.FindAnimation("spawn");
			SpineAnimBoss.animIdle = skeletonData13.FindAnimation("idle_1");
			SpineAnimBoss.animAttackProjectile = skeletonData13.FindAnimation("attack_1");
			SpineAnimBoss.animAttackMelee = skeletonData13.FindAnimation("attack_2");
			SpineAnimBoss.animDie = skeletonData13.FindAnimation("death");
			SpineAnimBoss.animLeave = skeletonData13.FindAnimation("leave");
			SpineAnimBossElf.Prefab = this.BossElf;
			SkeletonData skeletonData14 = this.animDataBossElf.GetSkeletonData(quiet);
			SpineAnimBossElf.animSpawn = skeletonData14.FindAnimation("spawn");
			SpineAnimBossElf.animIdle = skeletonData14.FindAnimation("idle_1");
			SpineAnimBossElf.animsAttack = new Spine.Animation[]
			{
				skeletonData14.FindAnimation("attack_1"),
				skeletonData14.FindAnimation("attack_2"),
				skeletonData14.FindAnimation("attack_3")
			};
			SpineAnimBossElf.animDie = skeletonData14.FindAnimation("death");
			SpineAnimBossElf.animLeave = skeletonData14.FindAnimation("leave");
			SpineAnimBossHuman.Prefab = this.BossHuman;
			SkeletonData skeletonData15 = this.animDataBossHuman.GetSkeletonData(quiet);
			SpineAnimBossHuman.animSpawn = skeletonData15.FindAnimation("spawn");
			SpineAnimBossHuman.animIdle = skeletonData15.FindAnimation("idle");
			SpineAnimBossHuman.animsAttack = new Spine.Animation[]
			{
				skeletonData15.FindAnimation("attack_1"),
				skeletonData15.FindAnimation("attack_2")
			};
			SpineAnimBossHuman.animDie = skeletonData15.FindAnimation("death");
			SpineAnimBossHuman.animLeave = skeletonData15.FindAnimation("leave");
			SpineAnimBossMangolies.Prefab = this.BossMangolies;
			SkeletonData skeletonData16 = this.animDataBossMangolies.GetSkeletonData(quiet);
			SpineAnimBossMangolies.animSpawn = skeletonData16.FindAnimation("spawn");
			SpineAnimBossMangolies.animIdle = skeletonData16.FindAnimation("idle");
			SpineAnimBossMangolies.animsAttack = new Spine.Animation[]
			{
				skeletonData16.FindAnimation("attack_1"),
				skeletonData16.FindAnimation("attack_2")
			};
			SpineAnimBossMangolies.animDie = skeletonData16.FindAnimation("death");
			SpineAnimBossMangolies.animLeave = skeletonData16.FindAnimation("leave");
			SpineAnimBossDwarf.Prefab = this.BossDwarf;
			SkeletonData skeletonData17 = this.animDataBossDwarf.GetSkeletonData(quiet);
			SpineAnimBossDwarf.animSpawn = skeletonData17.FindAnimation("spawn");
			SpineAnimBossDwarf.animIdle = skeletonData17.FindAnimation("idle");
			SpineAnimBossDwarf.animsAttack = new Spine.Animation[]
			{
				skeletonData17.FindAnimation("attack_1"),
				skeletonData17.FindAnimation("attack_2"),
				skeletonData17.FindAnimation("attack_3")
			};
			SpineAnimBossDwarf.animDie = skeletonData17.FindAnimation("death");
			SpineAnimBossDwarf.animLeave = skeletonData17.FindAnimation("leave");
			SpineAnimBossWiseSnake.Prefab = this.BossWiseSnake;
			SkeletonData skeletonData18 = this.animDataBossWiseSnake.GetSkeletonData(quiet);
			SpineAnimBossWiseSnake.animSpawn = skeletonData18.FindAnimation("appear");
			SpineAnimBossWiseSnake.animIdle = skeletonData18.FindAnimation("idle");
			SpineAnimBossWiseSnake.animsAttack = new Spine.Animation[]
			{
				skeletonData18.FindAnimation("attack1"),
				skeletonData18.FindAnimation("attack2")
			};
			SpineAnimBossWiseSnake.animDie = skeletonData18.FindAnimation("dead");
			SpineAnimBossWiseSnake.animHurt = skeletonData18.FindAnimation("hurt");
			SpineAnimBossWiseSnake.animSummon = skeletonData18.FindAnimation("summon");
			SpineAnimSnake.Prefab = this.Snake;
			SkeletonData skeletonData19 = this.animDataSnake.GetSkeletonData(quiet);
			SpineAnimSnake.animSpawn = skeletonData19.FindAnimation("appear");
			SpineAnimSnake.animIdle = skeletonData19.FindAnimation("idle");
			SpineAnimSnake.animsAttack = new Spine.Animation[]
			{
				skeletonData19.FindAnimation("attack1"),
				skeletonData19.FindAnimation("attack2")
			};
			SpineAnimSnake.animDie = skeletonData19.FindAnimation("dead");
			SpineAnimProjectileTotemShard.Prefab = this.TotemShard;
			SkeletonData skeletonData20 = this.animDataTotemShard.GetSkeletonData(quiet);
			SpineAnimProjectileTotemShard.anim = skeletonData20.FindAnimation("attack");
			SkeletonData skeletonData21 = this.animDataHiltExcalibur.GetSkeletonData(quiet);
			SpineAnimProjectileHiltExcalibur.anim = skeletonData21.FindAnimation("animation");
			SkeletonData skeletonData22 = this.fireworkProjectileAnimation.GetSkeletonData(false);
			SpineAnimProjectileBombermanFirework.anim = skeletonData22.FindAnimation("animation");
			SpineAnimWarlockSwarm.Prefab = this.projectileSwarm;
			SkeletonData skeletonData23 = this.projectileSwarmAnimData.GetSkeletonData(false);
			SpineAnimWarlockSwarm.attack = skeletonData23.FindAnimation("attack_1");
			SpineAnimWarlockSwarm.loop = skeletonData23.FindAnimation("run");
			SpineAnimWarlockSwarm.end = skeletonData23.FindAnimation("death");
			SpineAnimProjectileDwarfCorrupted.Prefab = this.ProjectileDwarfCorrupted;
			SkeletonData skeletonData24 = this.animDataProjectileDwarfCorrupted.GetSkeletonData(quiet);
			SpineAnimProjectileDwarfCorrupted.anim = skeletonData24.FindAnimation("idle");
			SpineAnimProjectileMagolies.Prefab = this.ProjectileMagolies;
			SkeletonData skeletonData25 = this.animDataProjectileMagolies.GetSkeletonData(quiet);
			SpineAnimProjectileMagolies.anim = skeletonData25.FindAnimation("loop");
			SpineAnimProjectileSnake.Prefab = this.ProjectileSnake;
			SkeletonData skeletonData26 = this.animDataProjectileSnake.GetSkeletonData(quiet);
			SpineAnimProjectileSnake.anim = skeletonData26.FindAnimation("projectile");
			SpineAnimHit.Prefab = this.HitAnimation;
			SkeletonData skeletonData27 = this.animDataHitAnimation.GetSkeletonData(quiet);
			SpineAnimHit.anim = skeletonData27.FindAnimation("hit");
			SpineAnimEnemyDeath.Prefab = this.EnemyDeath;
			SkeletonData skeletonData28 = this.animDataEnemyDeath.GetSkeletonData(quiet);
			SpineAnimEnemyDeath.anim = skeletonData28.FindAnimation("death");
			SpineAnimSmoke.Prefab = this.Smoke;
			SkeletonData skeletonData29 = this.animDataSmoke.GetSkeletonData(quiet);
			SpineAnimSmoke.anim = skeletonData29.FindAnimation("explosion_nugget_bag");
			SpineAnimTotemLightning.Prefab = this.TotemLightning;
			SkeletonData skeletonData30 = this.animDataTotemLightning.GetSkeletonData(quiet);
			SpineAnimTotemLightning.anim = skeletonData30.FindAnimation("attack_1");
			SpineAnimTotemThunderbolt.Prefab = this.TotemThunderbolt;
			SkeletonData skeletonData31 = this.animDataTotemThunderBolt.GetSkeletonData(quiet);
			SpineAnimTotemThunderbolt.anim = skeletonData31.FindAnimation("lightning_1");
			SpineAnimTotemFire.Prefab = this.TotemFire;
			SkeletonData skeletonData32 = this.animDataTotemFire.GetSkeletonData(quiet);
			SpineAnimTotemFire.animAttack = skeletonData32.FindAnimation("attack_1");
			SpineAnimTotemFire.animAttackClose = skeletonData32.FindAnimation("attack_close");
			SpineAnimTotemFire.animAttackNone = skeletonData32.FindAnimation("attack_no_enemis");
			SpineAnimTotemSmoke.Prefab = this.TotemSmoke;
			SkeletonData skeletonData33 = this.animDataTotemSmoke.GetSkeletonData(quiet);
			SpineAnimTotemSmoke.anim = skeletonData33.FindAnimation("disable_3");
			SpineAnimTotemEarth.Prefab = this.TotemEarth;
			SkeletonData skeletonData34 = this.animDataTotemEarth.GetSkeletonData(quiet);
			SpineAnimTotemEarth.anim = skeletonData34.FindAnimation("_idle");
			SpineAnimTotemEarthImpact.Prefab = this.TotemEarthImpact;
			SkeletonData skeletonData35 = this.animDataTotemEarthImpact.GetSkeletonData(quiet);
			SpineAnimTotemEarthImpact.anim = skeletonData35.FindAnimation("impact");
			SpineAnimTotemEarthTap.Prefab = this.TotemEarthTap;
			SkeletonData skeletonData36 = this.animDataTotemEarthTap.GetSkeletonData(quiet);
			SpineAnimTotemEarthTap.anim = skeletonData36.FindAnimation("attack_close");
			SpineAnimTotemEarthStarfall.Prefab = this.TotemEarthStarfall;
			SkeletonData skeletonData37 = this.animDataTotemEarthStarfall.GetSkeletonData(quiet);
			SpineAnimTotemEarthStarfall.anim = skeletonData37.FindAnimation("falling_01");
			SpineAnimTotemEarthStarfall.animImpact = skeletonData37.FindAnimation("impact");
			SpineAnimTotemEarthTapDisable.Prefab = this.TotemEarthTapDisable;
			SkeletonData skeletonData38 = this.animDataTotemEarthTapDisable.GetSkeletonData(quiet);
			SpineAnimTotemEarthTapDisable.anim = skeletonData38.FindAnimation("disable_3");
			SpineAnimTamFlare.Prefab = this.TamFlare;
			SkeletonData skeletonData39 = this.animDataTamFlare.GetSkeletonData(quiet);
			SpineAnimTamFlare.animLoop = skeletonData39.FindAnimation("flare_loop");
			SpineAnimBombermanExplosion.Prefab = this.Explossion;
			SkeletonData skeletonData40 = this.ExplossionAnimation.GetSkeletonData(false);
			SpineAnimBombermanExplosion.anim = skeletonData40.FindAnimation("animation");
			SkeletonData skeletonData41 = this.wendleBookProjectile.projectileSkeleton.SkeletonDataAsset.GetSkeletonData(false);
			SpineAnimProjectileDerekMeteor.animBook = skeletonData41.FindAnimation("animation");
			SpineAnimDerekBookExplosion.Prefab = this.DerekBookExplosion;
			SkeletonData skeletonData42 = this.animDataDerekBookExplosion.GetSkeletonData(quiet);
			SpineAnimDerekBookExplosion.anim = skeletonData42.FindAnimation("animation");
			SpineAnimMagoliesProjectileExplosion.Prefab = this.MagoliesProjectileExplosion;
			SpineAnimMagoliesProjectileExplosion.anim = skeletonData25.FindAnimation("explosion");
			SpineAnimSnakeProjectileExplosion.Prefab = this.SnakeProjectileExplosion;
			SpineAnimSnakeProjectileExplosion.anim = skeletonData26.FindAnimation("explosion");
			SpineAnimBuffAttackFast.Prefab = this.BuffAttackFast;
			SkeletonData skeletonData43 = this.animDataBuffAttackFast.GetSkeletonData(quiet);
			SpineAnimBuffAttackFast.anim = skeletonData43.FindAnimation("attack_speed");
			SpineAnimBuffAttackSlow.Prefab = this.BuffAttackSlow;
			SkeletonData skeletonData44 = this.animDataBuffAttackSlow.GetSkeletonData(quiet);
			SpineAnimBuffAttackSlow.start = skeletonData44.FindAnimation("start");
			SpineAnimBuffAttackSlow.loop = skeletonData44.FindAnimation("loop");
			SpineAnimBuffAttackSlow.end = skeletonData44.FindAnimation("end");
			SpineAnimBuffCritChance.Prefab = this.BuffCritChance;
			SkeletonData skeletonData45 = this.animDataBuffCritChance.GetSkeletonData(quiet);
			SpineAnimBuffCritChance.anim = skeletonData45.FindAnimation("animation");
			SpineAnimBuffDamageAdd.Prefab = this.BuffDamageAdd;
			SkeletonData skeletonData46 = this.animDataBuffDamageAdd.GetSkeletonData(quiet);
			SpineAnimBuffDamageAdd.anim = skeletonData46.FindAnimation("animation4");
			SpineAnimBuffDamageDec.Prefab = this.BuffDamageDec;
			SkeletonData skeletonData47 = this.animDataBuffDamageDec.GetSkeletonData(quiet);
			SpineAnimBuffDamageDec.anim = skeletonData47.FindAnimation("damage_reduction");
			SpineAnimBuffDeath.Prefab = this.BuffDeath;
			SkeletonData skeletonData48 = this.animDataBuffDeath.GetSkeletonData(quiet);
			SpineAnimBuffDeath.anim = skeletonData48.FindAnimation("animation");
			SpineAnimBuffDefenseless.Prefab = this.BuffDefenseless;
			SkeletonData skeletonData49 = this.animDataBuffDefenseless.GetSkeletonData(quiet);
			SpineAnimBuffDefenseless.start = skeletonData49.FindAnimation("start");
			SpineAnimBuffDefenseless.loop = skeletonData49.FindAnimation("loop");
			SpineAnimBuffDefenseless.end = skeletonData49.FindAnimation("end");
			SpineAnimBuffHealthRegen.Prefab = this.BuffHealthRegen;
			SkeletonData skeletonData50 = this.animDataBuffHealthRegen.GetSkeletonData(quiet);
			SpineAnimBuffHealthRegen.anim = skeletonData50.FindAnimation("animation");
			SpineAnimBuffImmunity.Prefab = this.BuffImmunity;
			SkeletonData skeletonData51 = this.animDataBuffImmunity.GetSkeletonData(quiet);
			SpineAnimBuffImmunity.anim = skeletonData51.FindAnimation("animation2");
			SpineAnimBuffShield.Prefab = this.BuffShield;
			SkeletonData skeletonData52 = this.animDataBuffShield.GetSkeletonData(quiet);
			SpineAnimBuffShield.anim = skeletonData52.FindAnimation("loop");
			SpineAnimBuffStun.Prefab = this.BuffStun;
			SkeletonData skeletonData53 = this.animDataBuffStun.GetSkeletonData(quiet);
			SpineAnimBuffStun.anim = skeletonData53.FindAnimation("animation3");
			SpineAnimBuffMiss.Prefab = this.BuffMiss;
			SkeletonData skeletonData54 = this.animDataBuffMiss.GetSkeletonData(quiet);
			SpineAnimBuffMiss.anim = skeletonData54.FindAnimation("animation");
			SpineAnimBuffMark.Prefab = this.BuffMark;
			SkeletonData skeletonData55 = this.animDataBuffMark.GetSkeletonData(quiet);
			SpineAnimBuffMark.anim = skeletonData55.FindAnimation("animation");
			SpineAnimBuffCritDamage.Prefab = this.BuffCritDamage;
			SkeletonData skeletonData56 = this.animDataBuffCritDamage.GetSkeletonData(quiet);
			SpineAnimBuffCritDamage.anim = skeletonData56.FindAnimation("animation");
			SpineAnimBuffDodgeChance.Prefab = this.BuffDodgeChance;
			SkeletonData skeletonData57 = this.animDataBuffDodgeChance.GetSkeletonData(quiet);
			SpineAnimBuffDodgeChance.anim = skeletonData57.FindAnimation("animation");
			SpineAnimBuffReduceRevive.Prefab = this.BuffReduceRevive;
			SkeletonData skeletonData58 = this.animDataBuffReduceRevive.GetSkeletonData(quiet);
			SpineAnimBuffReduceRevive.anim = skeletonData58.FindAnimation("animation");
			SpineAnimIceManaGather.Prefab = this.TotemIceManaGather;
			SkeletonData skeletonData59 = this.animDataTotemIceManaGather.GetSkeletonData(quiet);
			SpineAnimIceManaGather.anim = skeletonData59.FindAnimation("tap_ice");
			SpineAnimDuck.Prefab = this.Duck;
			SkeletonData skeletonData60 = this.animDataDuck.GetSkeletonData(quiet);
			SpineAnimDuck.anim = skeletonData60.FindAnimation("falling");
			SpineAnimAdDragon.Prefab = this.AdDragon;
			SkeletonData skeletonData61 = this.animDataAdDragon.GetSkeletonData(quiet);
			SpineAnimAdDragon.animIdle = skeletonData61.FindAnimation("idle");
			SpineAnimAdDragon.animActivate = skeletonData61.FindAnimation("activate");
			SpineAnimAdDragon.animEscape = skeletonData61.FindAnimation("escape");
			RenderManager.POS_GOLD_INV_TIMECHALLENGE = this._goldTimeChallengeFlyTarget.TransformPoint(this._goldTimeChallengeFlyTarget.anchoredPosition);
			RenderManager.POS_GOLD_INV_TIMECHALLENGE = this._scene.transform.InverseTransformPoint(RenderManager.POS_GOLD_INV_TIMECHALLENGE);
			RenderManager.POS_GOLD_INV_TIMECHALLENGE.z = 0f;
			RenderManager.POS_CURRENCY_DAILY_QUEST = this._dailyQuestFlyTarget.TransformPoint(this._dailyQuestFlyTarget.anchoredPosition);
			RenderManager.POS_CURRENCY_DAILY_QUEST = this._scene.transform.InverseTransformPoint(RenderManager.POS_CURRENCY_DAILY_QUEST);
			RenderManager.POS_CURRENCY_DAILY_QUEST.z = 0f;
			this.worldBackground.animations.Init();
			RenderManager.POS_CURRENCY_SHOP = this._shopFlyTarget.TransformPoint(this._shopFlyTarget.anchoredPosition);
			RenderManager.POS_CURRENCY_SHOP = this._scene.transform.InverseTransformPoint(RenderManager.POS_CURRENCY_SHOP);
			RenderManager.POS_CURRENCY_SHOP.z = 0f;
			this.buffEffectPositions = new Dictionary<string, Vector3>();
		}

		public void OnHoratioAssetsLoaded(HeroBundle heroAssets, SkeletonData readAnimDataHoratio)
		{
			SpineAnimHoratio.Prefab = heroAssets.prefab;
			SpineAnimHoratio.animIdle = readAnimDataHoratio.FindAnimation("idle_1");
			SpineAnimHoratio.animsAttack = new Spine.Animation[]
			{
				readAnimDataHoratio.FindAnimation("attack_1"),
				readAnimDataHoratio.FindAnimation("attack_2"),
				readAnimDataHoratio.FindAnimation("attack_3")
			};
			SpineAnimHoratio.animUltiStart = readAnimDataHoratio.FindAnimation("ultimate_start");
			SpineAnimHoratio.animUltiLoop = readAnimDataHoratio.FindAnimation("ultimate_loop");
			SpineAnimHoratio.animUltiEnd = readAnimDataHoratio.FindAnimation("ultimate_end");
			SpineAnimHoratio.animsAuto = new Spine.Animation[]
			{
				readAnimDataHoratio.FindAnimation("passive_1"),
				readAnimDataHoratio.FindAnimation("passive_2")
			};
			SpineAnimHoratio.animDeath = readAnimDataHoratio.FindAnimation("death");
			SpineAnimHoratio.animVictory = readAnimDataHoratio.FindAnimation("victoty_1");
		}

		private void OnHoratioInGameAssetsLoaded(HoratioInGameAssetsBundle heroaAssets)
		{
			this.poolSmartProjectileHiltExcalibur.ChangePrefab(heroaAssets.SkillExcaliburProjectile);
		}

		public void OnThourAssetsLoaded(HeroBundle heroAssets, SkeletonData readAnimDataThour)
		{
			SpineAnimThour.Prefab = heroAssets.prefab;
			SpineAnimThour.animsIdle = new Spine.Animation[]
			{
				readAnimDataThour.FindAnimation("idle_1"),
				readAnimDataThour.FindAnimation("ultimate_idle")
			};
			SpineAnimThour.animsAttack = new Spine.Animation[]
			{
				readAnimDataThour.FindAnimation("attack_1"),
				readAnimDataThour.FindAnimation("ultimate_attack_1"),
				readAnimDataThour.FindAnimation("ultimate_attack_2")
			};
			SpineAnimThour.animUltiStart = readAnimDataThour.FindAnimation("ultimate_start");
			SpineAnimThour.animUltiEnd = readAnimDataThour.FindAnimation("ultimate_end");
			SpineAnimThour.animsAuto = new Spine.Animation[]
			{
				readAnimDataThour.FindAnimation("passive_1"),
				readAnimDataThour.FindAnimation("passive_2")
			};
			SpineAnimThour.animDeath = readAnimDataThour.FindAnimation("death");
			SpineAnimThour.animVictory = readAnimDataThour.FindAnimation("victory_1");
		}

		public void OnKindLennyAssetsLoaded(HeroBundle heroAssets, SkeletonData readAnimDataKindLenny)
		{
			SpineAnimKindLenny.Prefab = heroAssets.prefab;
			SpineAnimKindLenny.animIdle = readAnimDataKindLenny.FindAnimation("idle_1");
			SpineAnimKindLenny.animAttack = readAnimDataKindLenny.FindAnimation("attack_1");
			SpineAnimKindLenny.animUltiStart = readAnimDataKindLenny.FindAnimation("ultimate_start");
			SpineAnimKindLenny.animUltiLoop = readAnimDataKindLenny.FindAnimation("ultimate_loop");
			SpineAnimKindLenny.animUltiEnd = readAnimDataKindLenny.FindAnimation("ultimate_end");
			SpineAnimKindLenny.animsAuto = new Spine.Animation[]
			{
				readAnimDataKindLenny.FindAnimation("passive_1"),
				readAnimDataKindLenny.FindAnimation("passive_2")
			};
			SpineAnimKindLenny.animDeath = readAnimDataKindLenny.FindAnimation("death");
			SpineAnimKindLenny.animReload = readAnimDataKindLenny.FindAnimation("reload");
			SpineAnimKindLenny.animVictory = readAnimDataKindLenny.FindAnimation("victory_1");
		}

		private void OnKindLennyInGameAssetsLoaded(KindLennyInGameAssetsBundle heroAssets)
		{
			this.poolSmartProjectileLenny.ChangePrefab(heroAssets.Projectile);
			this.poolProjectileAppleBombard.ChangePrefab(heroAssets.ProjectileAppleBombard);
			SpineAnimGreenAppleExplosion.Prefab = heroAssets.GreenAppleExplossion;
			SkeletonData skeletonData = heroAssets.GreenAppleExplosionAnimData.GetSkeletonData(false);
			SpineAnimGreenAppleExplosion.anim = skeletonData.FindAnimation("animation");
		}

		public void OnIdaAssetsLoaded(HeroBundle heroAssets, SkeletonData readAnimDataIda)
		{
			SpineAnimIda.Prefab = heroAssets.prefab;
			SpineAnimIda.animIdle = readAnimDataIda.FindAnimation("idle_1");
			SpineAnimIda.animsAttack = new Spine.Animation[]
			{
				readAnimDataIda.FindAnimation("attack_1"),
				readAnimDataIda.FindAnimation("attack_2"),
				readAnimDataIda.FindAnimation("attack_3")
			};
			SpineAnimIda.animBreath = readAnimDataIda.FindAnimation("breath");
			SpineAnimIda.animUltiStart = readAnimDataIda.FindAnimation("ultimate_start");
			SpineAnimIda.animUltiLoop = readAnimDataIda.FindAnimation("ultimate_loop");
			SpineAnimIda.animUltiEnd = readAnimDataIda.FindAnimation("ultimate_end");
			SpineAnimIda.animsAuto = new Spine.Animation[]
			{
				readAnimDataIda.FindAnimation("passive_1"),
				readAnimDataIda.FindAnimation("passive_2")
			};
			SpineAnimIda.animDeath = readAnimDataIda.FindAnimation("death");
			SpineAnimIda.animVictory = readAnimDataIda.FindAnimation("victory_1");
		}

		public void OnWendleAssetsLoaded(HeroBundle heroAssets, SkeletonData readAnimDataDerek)
		{
			SpineAnimDerek.Prefab = heroAssets.prefab;
			SpineAnimDerek.animIdle = readAnimDataDerek.FindAnimation("idle_1");
			SpineAnimDerek.animAttack = readAnimDataDerek.FindAnimation("attack_1");
			SpineAnimDerek.animUlti = readAnimDataDerek.FindAnimation("ultimate");
			SpineAnimDerek.animsAuto = new Spine.Animation[]
			{
				readAnimDataDerek.FindAnimation("passive_1"),
				readAnimDataDerek.FindAnimation("passive_2")
			};
			SpineAnimDerek.animDeath = readAnimDataDerek.FindAnimation("death");
			SpineAnimDerek.animVictory = readAnimDataDerek.FindAnimation("victory_1");
		}

		private void OnWendleInGameAssetsLoaded(WendleInGameAssetsBundle heroAssets)
		{
			this.poolProjectileDerekMagicBall.ChangePrefab(heroAssets.ProjectileMagicBall);
			SpineAnimProjectileDerekMeteor.Prefab = heroAssets.ProjectileMeteor;
			SkeletonData skeletonData = heroAssets.ProjectileMeteorAnimData.GetSkeletonData(false);
			SpineAnimProjectileDerekMeteor.anim = skeletonData.FindAnimation("falling");
		}

		public void OnSheelaAssetsLoaded(HeroBundle heroAssets, SkeletonData readAnimDataSheela)
		{
			SpineAnimSheela.Prefab = heroAssets.prefab;
			SpineAnimSheela.animIdle = readAnimDataSheela.FindAnimation("idle_1");
			SpineAnimSheela.animsAttack = new Spine.Animation[]
			{
				readAnimDataSheela.FindAnimation("attack_1"),
				readAnimDataSheela.FindAnimation("attack_2")
			};
			SpineAnimSheela.animUltiStart = readAnimDataSheela.FindAnimation("ultimate_start");
			SpineAnimSheela.animUltiEnd = readAnimDataSheela.FindAnimation("ultimate_end");
			SpineAnimSheela.animsAuto = new Spine.Animation[]
			{
				readAnimDataSheela.FindAnimation("passive_1"),
				readAnimDataSheela.FindAnimation("passive_2")
			};
			SpineAnimSheela.animDeath = readAnimDataSheela.FindAnimation("death");
			SpineAnimSheela.animReload = readAnimDataSheela.FindAnimation("reload");
			SpineAnimSheela.animIdleUlti = readAnimDataSheela.FindAnimation("ultimate_idle");
			SpineAnimSheela.animAttackUlti = readAnimDataSheela.FindAnimation("ultimate_attack");
			SpineAnimSheela.animVictory = readAnimDataSheela.FindAnimation("victory_1");
		}

		public void OnBombermanAssetsLoaded(HeroBundle heroAssets, SkeletonData readAnimDataBomberman)
		{
			SpineAnimBomberman.Prefab = heroAssets.prefab;
			SpineAnimBomberman.animIdle = readAnimDataBomberman.FindAnimation("idle_1");
			SpineAnimBomberman.animAttack = readAnimDataBomberman.FindAnimation("attack_1");
			SpineAnimBomberman.animUlti = readAnimDataBomberman.FindAnimation("ultimate");
			SpineAnimBomberman.animsAuto = new Spine.Animation[]
			{
				readAnimDataBomberman.FindAnimation("passive_1"),
				readAnimDataBomberman.FindAnimation("passive_2")
			};
			SpineAnimBomberman.animDeath = readAnimDataBomberman.FindAnimation("death");
			SpineAnimBomberman.animVictory = readAnimDataBomberman.FindAnimation("victory_1");
		}

		public void OnSamAssetsLoaded(HeroBundle heroAssets, SkeletonData readAnimDataSam)
		{
			SpineAnimSam.Prefab = heroAssets.prefab;
			SpineAnimSam.animsIdle = new Spine.Animation[]
			{
				readAnimDataSam.FindAnimation("idle_1"),
				readAnimDataSam.FindAnimation("ultimate_idle")
			};
			SpineAnimSam.animsAttack = new Spine.Animation[]
			{
				readAnimDataSam.FindAnimation("attack_1"),
				readAnimDataSam.FindAnimation("ultimate_attack")
			};
			SpineAnimSam.animUltiStart = readAnimDataSam.FindAnimation("ultimate_start");
			SpineAnimSam.animUltiEnd = readAnimDataSam.FindAnimation("ultimate_end");
			SpineAnimSam.animsAuto = new Spine.Animation[]
			{
				readAnimDataSam.FindAnimation("passive_1"),
				readAnimDataSam.FindAnimation("passive_2")
			};
			SpineAnimSam.animDeath = readAnimDataSam.FindAnimation("death");
			SpineAnimSam.animVictory = readAnimDataSam.FindAnimation("victory_1");
		}

		private void OnSamInGameAssetsLoaded(SamInGameAssetsBundle heroAssets)
		{
			this.poolSmartProjectileSamBottle.ChangePrefab(heroAssets.SmartProjectileBottle);
			this.poolProjectileSamAxe.ChangePrefab(heroAssets.ProjectileAxe);
			SpineAnimSamBottleExplosion.Prefab = heroAssets.BottleExplossion;
			SkeletonData skeletonData = heroAssets.BottleExplossionAnimData.GetSkeletonData(false);
			SpineAnimSamBottleExplosion.anim = skeletonData.FindAnimation("animation");
		}

		public void OnBlindArcherAssetsLoaded(HeroBundle heroAssets, SkeletonData readAnimDataBlindArcher)
		{
			SpineAnimBlindArcher.Prefab = heroAssets.prefab;
			SpineAnimBlindArcher.animIdle = readAnimDataBlindArcher.FindAnimation("idle_1");
			SpineAnimBlindArcher.animsAttack = new Spine.Animation[]
			{
				readAnimDataBlindArcher.FindAnimation("attack_1"),
				readAnimDataBlindArcher.FindAnimation("attack_2")
			};
			SpineAnimBlindArcher.animUltiStart = readAnimDataBlindArcher.FindAnimation("ultimate_start");
			SpineAnimBlindArcher.animUltiEnd = readAnimDataBlindArcher.FindAnimation("ultimate_end");
			SpineAnimBlindArcher.animsAuto = new Spine.Animation[]
			{
				readAnimDataBlindArcher.FindAnimation("passive_1"),
				readAnimDataBlindArcher.FindAnimation("passive_2")
			};
			SpineAnimBlindArcher.animDeath = readAnimDataBlindArcher.FindAnimation("death");
			SpineAnimBlindArcher.animIdleUlti = readAnimDataBlindArcher.FindAnimation("ultimate_idle");
			SpineAnimBlindArcher.animAttackUlti = readAnimDataBlindArcher.FindAnimation("ultimate_attack");
			SpineAnimBlindArcher.animVictory = readAnimDataBlindArcher.FindAnimation("victory_1");
		}

		public void OnBlindArcherInGameAssetsLoaded(BlindArcherInGameAssetsBundle heroAssets)
		{
			this.poolProjectileBlindArcher.ChangePrefab(heroAssets.Projectile);
		}

		public void OnJimAssetsLoaded(HeroBundle heroAssets, SkeletonData readAnimDataJim)
		{
			SpineAnimJim.Prefab = heroAssets.prefab;
			SpineAnimJim.animIdle = readAnimDataJim.FindAnimation("idle_1");
			SpineAnimJim.animsAttack = new Spine.Animation[]
			{
				readAnimDataJim.FindAnimation("attack_1"),
				readAnimDataJim.FindAnimation("attack_2")
			};
			SpineAnimJim.animUltiStart = readAnimDataJim.FindAnimation("ultimate_start");
			SpineAnimJim.animUltiLoop = readAnimDataJim.FindAnimation("ultimate_loop");
			SpineAnimJim.animUltiEnd = readAnimDataJim.FindAnimation("ultimate_end");
			SpineAnimJim.animsAuto = new Spine.Animation[]
			{
				readAnimDataJim.FindAnimation("passive_1"),
				readAnimDataJim.FindAnimation("passive_2")
			};
			SpineAnimJim.animDeath = readAnimDataJim.FindAnimation("death");
			SpineAnimJim.animVictory = readAnimDataJim.FindAnimation("victory_1");
		}

		public void OnTamAssetsLoaded(HeroBundle heroAssets, SkeletonData readAnimDataTam)
		{
			SpineAnimTam.Prefab = heroAssets.prefab;
			SpineAnimTam.animIdle = readAnimDataTam.FindAnimation("idle_1");
			SpineAnimTam.animsAttack = new Spine.Animation[]
			{
				readAnimDataTam.FindAnimation("attack_1"),
				readAnimDataTam.FindAnimation("attack_2")
			};
			SpineAnimTam.animUlti = readAnimDataTam.FindAnimation("ultimate");
			SpineAnimTam.animsAuto = new Spine.Animation[]
			{
				readAnimDataTam.FindAnimation("passive_2"),
				readAnimDataTam.FindAnimation("passive_3")
			};
			SpineAnimTam.animReload = readAnimDataTam.FindAnimation("reload");
			SpineAnimTam.animDeath = readAnimDataTam.FindAnimation("death");
			SpineAnimTam.animVictory = readAnimDataTam.FindAnimation("victory_1");
		}

		public void OnWarlockAssetsLoaded(HeroBundle heroAssets, SkeletonData readAnimDataWarlock)
		{
			SpineAnimWarlock.Prefab = heroAssets.prefab;
			SpineAnimWarlock.animIdle = readAnimDataWarlock.FindAnimation("idle_1");
			SpineAnimWarlock.animsAttack = new Spine.Animation[]
			{
				readAnimDataWarlock.FindAnimation("attack_1"),
				readAnimDataWarlock.FindAnimation("attack_2"),
				readAnimDataWarlock.FindAnimation("attack_3"),
				readAnimDataWarlock.FindAnimation("attack_4")
			};
			SpineAnimWarlock.animUltiStart = readAnimDataWarlock.FindAnimation("ultimate_start");
			SpineAnimWarlock.animUltiEnd = readAnimDataWarlock.FindAnimation("ultimate_end");
			SpineAnimWarlock.animUltiAttacks = new Spine.Animation[]
			{
				readAnimDataWarlock.FindAnimation("ultimate_attack1"),
				readAnimDataWarlock.FindAnimation("ultimate_attack2")
			};
			SpineAnimWarlock.animsAuto = new Spine.Animation[]
			{
				readAnimDataWarlock.FindAnimation("passive_1"),
				readAnimDataWarlock.FindAnimation("passive_2")
			};
			SpineAnimWarlock.animDeath = readAnimDataWarlock.FindAnimation("death");
			SpineAnimWarlock.animVictory = readAnimDataWarlock.FindAnimation("victory_1");
		}

		public void OnBabuAssetsLoaded(HeroBundle heroAssets, SkeletonData readAnimDataBabu)
		{
			SpineAnimBabu.Prefab = heroAssets.prefab;
			SpineAnimBabu.animsIdle = new Spine.Animation[]
			{
				readAnimDataBabu.FindAnimation("idle_1")
			};
			SpineAnimBabu.animsAttack = new Spine.Animation[]
			{
				readAnimDataBabu.FindAnimation("attack_1"),
				readAnimDataBabu.FindAnimation("attack_2")
			};
			SpineAnimBabu.animUltiStart = readAnimDataBabu.FindAnimation("ultimate_start");
			SpineAnimBabu.animUltiLoop = readAnimDataBabu.FindAnimation("ultimate_idle");
			SpineAnimBabu.animUltiEnd = readAnimDataBabu.FindAnimation("ultimate_end");
			SpineAnimBabu.animsAuto = new Spine.Animation[]
			{
				readAnimDataBabu.FindAnimation("passive_1"),
				readAnimDataBabu.FindAnimation("passive_2")
			};
			SpineAnimBabu.animDeath = readAnimDataBabu.FindAnimation("death");
			SpineAnimBabu.animDeathUlti = readAnimDataBabu.FindAnimation("death_ultimate");
			SpineAnimBabu.animVictory = readAnimDataBabu.FindAnimation("victory_1");
		}

		private void OnBabuInGameAssetsLoaded(BabuInGameAssetsBundle heroAssets)
		{
			this.poolSmartProjectileBabu.ChangePrefab(heroAssets.Projectile);
			this.poolProjectileBabuTeaCup.ChangePrefab(heroAssets.ProjectileTeaCup);
			SkeletonData skeletonData = heroAssets.Projectile.projectileSkeleton.SkeletonDataAsset.GetSkeletonData(false);
			SpineAnimBabuProjectile.loop = skeletonData.FindAnimation("projectile");
			SpineAnimBabuProjectile.explode = skeletonData.FindAnimation("explosion");
		}

		private void OnWarlockInGameAssetsLoaded(WarlockInGameAssetsBundle heroAssets)
		{
			SpineAnimWarlockAttack.Prefab = heroAssets.Projectile;
			SkeletonData skeletonData = heroAssets.ProjectileAnimData.GetSkeletonData(false);
			SpineAnimWarlockAttack.anim = skeletonData.FindAnimation("animation");
		}

		public void OnGoblinAssetsLoaded(HeroBundle heroAssets, SkeletonData skeletonData)
		{
			SpineAnimGoblin.Prefab = heroAssets.prefab;
			SpineAnimGoblin.Init(skeletonData);
		}

		private void OnGoblinInGameAssetsLoaded(GoblinInGameAssetsBundle assets)
		{
			this.poolSmartProjectileGoblin.ChangePrefab(assets.ProjectilePrefab);
			this.poolProjectileGoblinBomb.ChangePrefab(assets.ProjectileSmokeBombPrefab);
			SpineAnimGoblinSmoke.Prefab = assets.SmokePrefab;
			SkeletonData skeletonData = assets.SmokeAnimation.GetSkeletonData(false);
			SpineAnimGoblinSmoke.start = skeletonData.FindAnimation("start");
			SpineAnimGoblinSmoke.loop = skeletonData.FindAnimation("loop");
			SpineAnimGoblinSmoke.end = skeletonData.FindAnimation("end");
		}

		public void OnDruidAssetsLoaded(HeroBundle heroAssets)
		{
			SpineAnimDruid.Prefab = heroAssets.prefab;
			SkeletonData skeletonData = heroAssets.animation.GetSkeletonData(false);
			SpineAnimDruid.animIdle = skeletonData.FindAnimation("idle_1");
			SpineAnimDruid.animAttacks = new Spine.Animation[]
			{
				skeletonData.FindAnimation("attack_1"),
				skeletonData.FindAnimation("attack_2"),
				skeletonData.FindAnimation("attack_3")
			};
			SpineAnimDruid.animUltiStart = skeletonData.FindAnimation("ultimate_start");
			SpineAnimDruid.animUltiRepeat = skeletonData.FindAnimation("ultimate_repeat");
			SpineAnimDruid.animUltiEnd = skeletonData.FindAnimation("ultimate_end");
			SpineAnimDruid.animsAuto = new Spine.Animation[]
			{
				skeletonData.FindAnimation("passive_1"),
				skeletonData.FindAnimation("passive_2")
			};
			SpineAnimDruid.animDeath = skeletonData.FindAnimation("death");
			SpineAnimDruid.animDeathTransformed = skeletonData.FindAnimation("death_detransform");
			SpineAnimDruid.animVictory = skeletonData.FindAnimation("victory_1");
		}

		public void OnDruidInGameAssetsLoaded(DruidInGameAssetsBundle assets)
		{
			SpineAnimDruidStampedeAnimal.Prefab = assets.StampedeAnimal;
			SkeletonData skeletonData = assets.StampedeAnimalAnimData.GetSkeletonData(false);
			SpineAnimDruidStampedeAnimal.run = skeletonData.FindAnimation("idle_1");
			SpineAnimDruidSupportAnimal.AnimDataPerAnimal[0].Prefab = assets.Larry;
			skeletonData = assets.LarryAnimData.GetSkeletonData(false);
			SpineAnimDruidSupportAnimal.AnimDataPerAnimal[0].run = skeletonData.FindAnimation("idle_1");
			SpineAnimDruidSupportAnimal.AnimDataPerAnimal[0].giveBuff = skeletonData.FindAnimation("ability_1");
			SpineAnimDruidSupportAnimal.AnimDataPerAnimal[1].Prefab = assets.Curly;
			skeletonData = assets.CurlyAnimData.GetSkeletonData(false);
			SpineAnimDruidSupportAnimal.AnimDataPerAnimal[1].run = skeletonData.FindAnimation("idle_1");
			SpineAnimDruidSupportAnimal.AnimDataPerAnimal[1].giveBuff = skeletonData.FindAnimation("ability_1");
			SpineAnimDruidSupportAnimal.AnimDataPerAnimal[2].Prefab = assets.Moe;
			skeletonData = assets.MoeAnimData.GetSkeletonData(false);
			SpineAnimDruidSupportAnimal.AnimDataPerAnimal[2].run = skeletonData.FindAnimation("idle_1");
			SpineAnimDruidSupportAnimal.AnimDataPerAnimal[2].giveBuff = skeletonData.FindAnimation("ability_1");
		}

		public void OnScreenSizeChanged(ScreenRes currentResolution)
		{
			this.SetCameraAndWorldSize(currentResolution);
		}

		private IEnumerator DoAfterOneFrame(Action job)
		{
			yield return null;
			job();
			yield break;
		}

		private void SetCameraAndWorldSize(ScreenRes currentResolution)
		{
			float num = (float)currentResolution.width / (float)currentResolution.height;
			float num2 = 0.5625f - num;
			if (num <= 0.5625f)
			{
				this.worldBackground.image.transform.parent.localScale = new Vector3(1f, GameMath.Lerp(1f, 1.1f, num2 / 0.0625f), 1f);
				this.worldBackground.image.transform.parent.localPosition = new Vector3(0f, GameMath.Lerp(0f, 0.16f, num2 / 0.0625f), 0f);
				this.mainCam.orthographicSize = GameMath.Lerp(1.33f, 1.6f, num2 / 0.0625f);
			}
			else
			{
				this.worldBackground.image.transform.parent.localScale = new Vector3(1f, 1f, 1f);
				this.mainCam.orthographicSize = 1.33f;
			}
		}

		public void LoadGameAssets(World world)
		{
			ChallengeRift challengeRift = world.activeChallenge as ChallengeRift;
			int skinIndex = (challengeRift == null || challengeRift.riftData.cursesSetup == null) ? 0 : 1;
			this.SetBg(world.activeChallenge.activeEnv.bg, skinIndex);
			foreach (Hero hero in world.heroes)
			{
				string heroId = hero.GetId();
				if (!this.heroInGameAssetsBundles.ContainsKey(heroId) && HeroIds.HeroInGameAssetsBundleByName.ContainsKey(heroId))
				{
					this.heroInGameAssetsBundles.Add(heroId, null);
					string bundleName = HeroIds.HeroInGameAssetsBundleByName[heroId];
					DynamicLoadManager.GetPermanentReferenceToBundle(bundleName, delegate
					{
						DynamicLoadManager.LoadAllAndGetAssetOfType<HeroInGameAssetsBundle>(bundleName, delegate(HeroInGameAssetsBundle assets)
						{
							this.heroInGameAssetsBundles[heroId] = assets;
							switch (heroId)
							{
							case "HORATIO":
								this.OnHoratioInGameAssetsLoaded(this.heroInGameAssetsBundles["HORATIO"] as HoratioInGameAssetsBundle);
								break;
							case "BLIND_ARCHER":
								this.OnBlindArcherInGameAssetsLoaded(this.heroInGameAssetsBundles["BLIND_ARCHER"] as BlindArcherInGameAssetsBundle);
								break;
							case "GOBLIN":
								this.OnGoblinInGameAssetsLoaded(this.heroInGameAssetsBundles["GOBLIN"] as GoblinInGameAssetsBundle);
								break;
							case "KIND_LENNY":
								this.OnKindLennyInGameAssetsLoaded(this.heroInGameAssetsBundles["KIND_LENNY"] as KindLennyInGameAssetsBundle);
								break;
							case "SAM":
								this.OnSamInGameAssetsLoaded(this.heroInGameAssetsBundles["SAM"] as SamInGameAssetsBundle);
								break;
							case "WARLOCK":
								this.OnWarlockInGameAssetsLoaded(this.heroInGameAssetsBundles["WARLOCK"] as WarlockInGameAssetsBundle);
								break;
							case "DEREK":
								this.OnWendleInGameAssetsLoaded(this.heroInGameAssetsBundles["DEREK"] as WendleInGameAssetsBundle);
								break;
							case "BABU":
								this.OnBabuInGameAssetsLoaded(this.heroInGameAssetsBundles["BABU"] as BabuInGameAssetsBundle);
								break;
							case "DRUID":
								this.OnDruidInGameAssetsLoaded(this.heroInGameAssetsBundles["DRUID"] as DruidInGameAssetsBundle);
								break;
							}
						}, true);
					}, false);
				}
			}
		}

		public bool AreAllAssetsLoaded(List<Hero> heroes)
		{
			if (this.worldBackground.skinIndex == -1 || this.worldBackground.loadingIndex != -1)
			{
				return false;
			}
			int num = 0;
			this.uniqueHeroes.Clear();
			foreach (Hero hero in heroes)
			{
				if (!this.uniqueHeroes.Contains(hero.GetId()))
				{
					this.uniqueHeroes.Add(hero.GetId());
				}
			}
			foreach (string key in this.uniqueHeroes)
			{
				if (HeroIds.HeroInGameAssetsBundleByName.ContainsKey(key))
				{
					num++;
				}
			}
			if (num != this.heroInGameAssetsBundles.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, HeroInGameAssetsBundle> keyValuePair in this.heroInGameAssetsBundles)
			{
				if (keyValuePair.Value == null)
				{
					return false;
				}
			}
			return true;
		}

		public void Render(Simulator sim, World world)
		{
			if (!Main.IgnorePendingAssetsLoaded && !this.AreAllAssetsLoaded(world.heroes))
			{
				return;
			}
			foreach (IRenderPool renderPool in AllRenderPools.all)
			{
				renderPool.OnPreFrame();
			}
			this.cachedBlackCurtainColor = Color.Lerp(RenderManager.BLACK_CURTAIN_MIN_COLOR, RenderManager.BLACK_CURTAIN_MAX_COLOR, world.blackCurtainRatio);
			this.RenderBackground(sim, world);
			RenderManager.POS_GOLD_INV_DEFAULT = this._goldFlyTarget.TransformPoint(this._goldFlyTarget.anchoredPosition + this._goldFlyTarget.pivot * this._goldFlyTarget.sizeDelta);
			RenderManager.POS_GOLD_INV_DEFAULT = this._scene.transform.InverseTransformPoint(RenderManager.POS_GOLD_INV_DEFAULT);
			RenderManager.POS_GOLD_INV_DEFAULT.z = 0f;
			RenderManager.POS_CURRENCY_OFFER_BUTTON = this._offerButtonFlyTarget.TransformPoint(this._offerButtonFlyTarget.anchoredPosition);
			RenderManager.POS_CURRENCY_OFFER_BUTTON = this._scene.transform.InverseTransformPoint(RenderManager.POS_CURRENCY_OFFER_BUTTON);
			RenderManager.POS_CURRENCY_OFFER_BUTTON.z = 0f;
			RenderManager.POS_CURRENCY_DAILY_QUEST = this._dailyQuestFlyTarget.TransformPoint(this._dailyQuestFlyTarget.anchoredPosition);
			RenderManager.POS_CURRENCY_DAILY_QUEST = this._scene.transform.InverseTransformPoint(RenderManager.POS_CURRENCY_DAILY_QUEST);
			RenderManager.POS_CURRENCY_DAILY_QUEST.z = 0f;
			foreach (Hero hero in world.heroes)
			{
				if (hero.canBeRendered)
				{
					this.Render(hero, sim);
				}
			}
			foreach (Enemy simEnemy in world.activeChallenge.enemies)
			{
				this.Render(simEnemy, world.activeChallenge);
			}
			foreach (SwarmDragon swarmDragon in world.swarmDragons)
			{
				this.Render(swarmDragon);
			}
			foreach (Stampede druidStampede in world.stampedes)
			{
				this.Render(druidStampede);
			}
			foreach (SupportAnimal druidSupportAnimal in world.supportAnimals)
			{
				this.Render(druidSupportAnimal);
			}
			foreach (TreasureDrop treasureDrop in world.treasureDrops)
			{
				this.Render(treasureDrop);
			}
			foreach (Projectile simProjectile in world.projectiles)
			{
				this.Render(simProjectile);
			}
			foreach (Drop simDrop in world.drops)
			{
				this.Render(simDrop, world.gameMode);
			}
			foreach (GlobalPastDamage simDamage in world.pastDamages)
			{
				this.Render(simDamage);
			}
			foreach (GlobalPastHeal simDamage2 in world.pastHeals)
			{
				this.Render(simDamage2);
			}
			foreach (VisualEffect simEffect in world.visualEffects)
			{
				this.Render(simEffect);
			}
			foreach (VisualLinedEffect simVis in world.visualLinedEffects)
			{
				this.Render(simVis);
			}
			this.RenderIceManaGather(world);
			this.RenderAdDragon(world);
			this.RenderCurrencyDragons(world);
			if (world.GetBlizzardTimeLeft() > 0f)
			{
				if (!this.blizzard.isPlaying)
				{
					this.blizzard.Play();
				}
                var temp = this.blizzard.main;

                temp.simulationSpeed = ((world.timeWarpTimeLeft <= 0f) ? 1f : world.timeWarpSpeed) * Cheats.timeScale;
			}
			else if (world.GetBlizzardTimeLeft() <= 0f && this.blizzard.isPlaying)
			{
				this.blizzard.Stop();
			}
			foreach (IRenderPool renderPool2 in AllRenderPools.all)
			{
				renderPool2.OnPostFrame();
			}
		}

		private void Render(SwarmDragon swarmDragon)
		{
			SpineAnim instance = this.poolProjectileWarlockSwarm.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(swarmDragon.pos);
			localPosition.z -= 0.003f;
			Spine.Animation animation = SpineAnimWarlockSwarm.loop;
			float num = swarmDragon.currentRotation * 180f / 3.14159274f;
			bool flag = false;
			if ((num < -90f && num > -270f) || (num > 90f && num < 270f))
			{
				num += 180f;
				flag = true;
			}
			Vector3 vector = new Vector3((float)((!flag) ? 1 : -1), 1f, 1f);
			instance.gameObject.transform.localPosition = localPosition;
			instance.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, num);
			float num2 = 0f;
			bool loop = true;
			Hero hero = swarmDragon.by as Hero;
			if (hero.GetId() == "WARLOCK")
			{
				instance.SetSkin(hero.GetEquippedSkinIndex() - 1);
			}
			else
			{
				instance.SetSkin(0);
			}
			if (swarmDragon.state == SwarmDragon.State.HIT)
			{
				num2 = SpineAnimWarlockSwarm.attack.duration * swarmDragon.stateTime * 3f;
				bool flag2 = num2 < 0.19f;
				animation = ((!flag2) ? SpineAnimWarlockSwarm.attack : SpineAnimWarlockSwarm.loop);
				loop = flag2;
			}
			else if (swarmDragon.state == SwarmDragon.State.INITIAL_KICK)
			{
				animation = SpineAnimWarlockSwarm.loop;
				num2 = animation.duration * swarmDragon.totalTime * 2f;
				instance.gameObject.transform.localScale = vector * Easing.BackEaseOut(GameMath.Clamp(swarmDragon.stateTime * 4f, 0f, 1f), 0f, 1f, 1f);
			}
			else if (swarmDragon.state == SwarmDragon.State.HEADING)
			{
				animation = SpineAnimWarlockSwarm.loop;
				num2 = animation.duration * swarmDragon.totalTime * 2f;
				instance.gameObject.transform.localScale = vector;
			}
			else if (swarmDragon.state == SwarmDragon.State.DISAPPEAR)
			{
				animation = SpineAnimWarlockSwarm.end;
				num2 = animation.duration * swarmDragon.stateTime * 2f;
			}
			if (swarmDragon.state != SwarmDragon.State.DEAD)
			{
				instance.Apply(animation, num2, loop);
			}
			instance.SetColor(this.cachedBlackCurtainColor);
		}

		private void Render(Stampede druidStampede)
		{
			if (druidStampede.state == Stampede.State.DEAD)
			{
				return;
			}
			for (int i = 0; i < Stampede.AnimalsOffsets.Length; i++)
			{
				SpineAnim instance = this.poolDruidStampedeAnimal.GetInstance();
				instance.gameObject.transform.localPosition = GameMath.ConvertToScreenSpace(druidStampede.pos + Stampede.AnimalsOffsets[i]);
				instance.Apply(SpineAnimDruidStampedeAnimal.run, druidStampede.totalTime + (float)i * 0.1f, true);
				instance.SetColor(this.cachedBlackCurtainColor);
			}
		}

		private void Render(SupportAnimal druidSupportAnimal)
		{
			if (druidSupportAnimal.state == SupportAnimal.State.DEAD)
			{
				return;
			}
			SpineAnim instance = this.supportAnimalsPools[(int)druidSupportAnimal.skin].GetInstance();
			instance.gameObject.transform.localPosition = GameMath.ConvertToScreenSpace(druidSupportAnimal.pos);
			instance.gameObject.transform.localEulerAngles = ((druidSupportAnimal.state != SupportAnimal.State.RETURNING) ? Vector3.zero : new Vector3(0f, 180f, 0f));
			float stateTime = druidSupportAnimal.stateTime;
			SpineAnimDruidSupportAnimal.AnimData animData = SpineAnimDruidSupportAnimal.AnimDataPerAnimal[(int)druidSupportAnimal.skin];
			bool loop;
			Spine.Animation anim;
			if (druidSupportAnimal.state == SupportAnimal.State.GIVING_BUFF)
			{
				loop = false;
				anim = animData.giveBuff;
			}
			else
			{
				loop = true;
				anim = animData.run;
			}
			instance.SetSkin((druidSupportAnimal.master == null) ? 0 : druidSupportAnimal.master.GetEquippedSkinIndex());
			instance.SetColor(this.cachedBlackCurtainColor);
			instance.Apply(anim, stateTime, loop);
		}

		private void Render(TreasureDrop treasureDrop)
		{
			SpineAnim instance = this.poolChest.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(treasureDrop.pos);
			instance.SetSkin(SpineAnimChest.GetSkinName(treasureDrop.world.currentSim));
			if (treasureDrop.state == TreasureDrop.State.EXPLODE)
			{
				instance.gameObject.SetActive(true);
				Spine.Animation animDie = SpineAnimChest.animDie;
				float time = animDie.duration * treasureDrop.stateTime / TreasureDrop.EXPLOSION_TIME;
				instance.Apply(animDie, time, false);
			}
			else if (treasureDrop.state == TreasureDrop.State.DROP)
			{
				instance.gameObject.SetActive(true);
				Spine.Animation animIdle = SpineAnimChest.animIdle;
				float stateTime = treasureDrop.stateTime;
				instance.Apply(animIdle, stateTime, true);
			}
			else
			{
				instance.gameObject.SetActive(false);
			}
			instance.gameObject.transform.localScale = Vector3.one * treasureDrop.scale;
			instance.gameObject.transform.localPosition = localPosition;
			instance.SetColor(this.cachedBlackCurtainColor);
		}

		public void RenderWhileUiCovering(Simulator sim, World world, bool renderBackground)
		{
			foreach (IRenderPool renderPool in AllRenderPools.all)
			{
				renderPool.OnPreFrame();
			}
			this.cachedBlackCurtainColor = Color.Lerp(RenderManager.BLACK_CURTAIN_MIN_COLOR, RenderManager.BLACK_CURTAIN_MAX_COLOR, world.blackCurtainRatio);
			if (renderBackground)
			{
				this.RenderBackground(sim, world);
			}
			else if (this.worldBackground.parent.activeSelf)
			{
				this.worldBackground.parent.SetActive(false);
			}
			foreach (Drop simDrop in world.drops)
			{
				this.Render(simDrop, world.gameMode);
			}
			if (!renderBackground)
			{
				this.blizzard.Stop();
				this.blizzard.Clear();
			}
			foreach (IRenderPool renderPool2 in AllRenderPools.all)
			{
				renderPool2.OnPostFrame();
			}
		}

		private void RenderAdDragon(World world)
		{
			if (world.adDragonState == World.AdDragonState.NONEXISTANCE)
			{
				return;
			}
			SpineAnim instance = this.poolAdDragon.GetInstance();
			instance.SetSkin(SpineAnimAdDragon.GetSkinName(world.currentSim, 0));
			Vector3 localPosition = GameMath.ConvertToScreenSpace(world.adDragonPos);
			localPosition.z = -0.2f;
			instance.gameObject.transform.localPosition = localPosition;
			instance.gameObject.transform.localScale = Vector3.one;
			instance.SetFlip(world.adDragonDir < 0f);
			if (world.adDragonState == World.AdDragonState.IDLE || world.adDragonState == World.AdDragonState.WAIT_UI)
			{
				Spine.Animation animIdle = SpineAnimAdDragon.animIdle;
				float time = world.adDragonTimeCounter + animIdle.duration - world.adDragonIdleDur % animIdle.duration;
				instance.Apply(animIdle, time, true);
			}
			else if (world.adDragonState == World.AdDragonState.ACTIVATE)
			{
				instance.Apply(SpineAnimAdDragon.animActivate, world.adDragonTimeCounter, false);
			}
			else
			{
				if (world.adDragonState != World.AdDragonState.ESCAPE)
				{
					throw new NotImplementedException();
				}
				instance.Apply(SpineAnimAdDragon.animEscape, world.adDragonTimeCounter, false);
			}
			instance.SetColor(this.cachedBlackCurtainColor);
		}

		private void RenderCurrencyDragons(World world)
		{
			for (int i = world.currencyDragons.Count - 1; i >= 0; i--)
			{
				CurrencyDragon currencyDragon = world.currencyDragons[i];
				SpineAnim instance = this.poolAdDragon.GetInstance();
				if (currencyDragon.visualVariation == 10)
				{
					instance.SetSkin(SpineAnimAdDragon.skins[5]);
				}
				else if (currencyDragon.visualVariation == 11)
				{
					instance.SetSkin(SpineAnimAdDragon.skins[2]);
				}
				else
				{
					instance.SetSkin(SpineAnimAdDragon.skins[1]);
				}
				Vector3 localPosition = GameMath.ConvertToScreenSpace(currencyDragon.pos);
				localPosition.z -= 1.2f;
				localPosition.y -= 0.05f;
				instance.gameObject.transform.localPosition = localPosition;
				instance.gameObject.transform.localScale = Vector3.one * 0.75f;
				instance.SetFlip(currencyDragon.direction < 0f);
				if (currencyDragon.state == CurrencyDragon.State.IDLE)
				{
					Spine.Animation animIdle = SpineAnimAdDragon.animIdle;
					float time = currencyDragon.stateTime * currencyDragon.speed * 3f;
					instance.Apply(animIdle, time, true);
				}
				else if (currencyDragon.state == CurrencyDragon.State.DROP)
				{
					instance.Apply(SpineAnimAdDragon.animActivate, currencyDragon.stateTime, false);
				}
				else if (currencyDragon.state == CurrencyDragon.State.EXIT)
				{
					instance.Apply(SpineAnimAdDragon.animEscape, currencyDragon.stateTime, false);
				}
				else if (currencyDragon.state != CurrencyDragon.State.ENTER)
				{
					throw new NotImplementedException(currencyDragon.state.ToString());
				}
				instance.SetColor(this.cachedBlackCurtainColor);
			}
		}

		private void RenderIceManaGather(World world)
		{
			if (world.totem == null)
			{
				return;
			}
			if (world.totem is TotemIce)
			{
				TotemIce totemIce = (TotemIce)world.totem;
				if (totemIce.timeManaGather < 0f)
				{
					float num = -totemIce.timeManaGather;
					if (num < 0.333333343f)
					{
						num += 2.26666665f;
						SpineAnim instance = this.poolIceManaGather.GetInstance();
						Spine.Animation anim = SpineAnimIceManaGather.anim;
						instance.Apply(anim, num, false);
						Vector3 localPosition = GameMath.ConvertToScreenSpace(totemIce.posManaGather);
						localPosition.z = -10f;
						instance.gameObject.transform.localPosition = localPosition;
						instance.SetColor(this.cachedBlackCurtainColor);
					}
				}
				else
				{
					SpineAnim instance2 = this.poolIceManaGather.GetInstance();
					Spine.Animation anim2 = SpineAnimIceManaGather.anim;
					float num2 = totemIce.timeManaGather;
					if (num2 > 1.16666663f)
					{
						num2 -= 1.16666663f;
						num2 %= 1.1f;
						num2 += 1.16666663f;
					}
					instance2.Apply(anim2, num2, false);
					Vector3 localPosition2 = GameMath.ConvertToScreenSpace(totemIce.posManaGather);
					localPosition2.z = -10f;
					instance2.gameObject.transform.localPosition = localPosition2;
					instance2.SetColor(this.cachedBlackCurtainColor);
				}
			}
		}

		private void Render(VisualLinedEffect simVis)
		{
			bool flag = true;
			bool flag2 = true;
			SpineAnim instance;
			Spine.Animation animation;
			if (simVis.type == VisualLinedEffect.Type.TOTEM_LIGHTNING)
			{
				instance = this.poolTotemLightning.GetInstance();
				animation = SpineAnimTotemLightning.anim;
			}
			else
			{
				if (simVis.type != VisualLinedEffect.Type.TOTEM_FIRE)
				{
					throw new NotImplementedException();
				}
				instance = this.poolTotemFire.GetInstance();
				if (simVis.isWithoutEnemies)
				{
					animation = SpineAnimTotemFire.animAttackNone;
					flag = false;
					flag2 = false;
				}
				else if (GameMath.D2xy(simVis.posStart, simVis.posEnd) <= 0.05f)
				{
					animation = SpineAnimTotemFire.animAttackClose;
					flag = false;
					flag2 = false;
				}
				else
				{
					animation = SpineAnimTotemFire.animAttack;
				}
			}
			float time = animation.duration * simVis.time / simVis.dur;
			instance.Apply(animation, time, false);
			GameObject gameObject = instance.gameObject;
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simVis.posStart);
			localPosition.z -= 2f;
			gameObject.transform.localPosition = localPosition;
			Vector3 a = GameMath.ConvertToScreenSpace(simVis.posStart);
			Vector3 b = GameMath.ConvertToScreenSpace(simVis.posEnd + new Vector3(0f, 0.1f, 0f));
			if (flag)
			{
				float z = Mathf.Atan2(b.y - a.y, b.x - a.x) * 180f / 3.14159274f;
				gameObject.transform.localEulerAngles = new Vector3(0f, 0f, z);
			}
			if (flag2)
			{
				float x = GameMath.Dxy(a, b);
				gameObject.transform.localScale = new Vector3(x, 1f, 1f);
			}
		}

		private void Render(Hero simHero, Simulator sim)
		{
			float additionalScale = 1f;
			string id = simHero.GetId();
			SpineAnim spineAnim;
			if (id == "HORATIO")
			{
				spineAnim = this.RenderHoratio(simHero);
			}
			else if (id == "THOUR")
			{
				spineAnim = this.RenderThour(simHero);
			}
			else if (id == "IDA")
			{
				spineAnim = this.RenderIda(simHero);
			}
			else if (id == "KIND_LENNY")
			{
				spineAnim = this.RenderKindLenny(simHero);
			}
			else if (id == "DEREK")
			{
				spineAnim = this.RenderDerek(simHero);
			}
			else if (id == "SHEELA")
			{
				spineAnim = this.RenderSheela(simHero);
			}
			else if (id == "BOMBERMAN")
			{
				spineAnim = this.RenderBomberman(simHero);
			}
			else if (id == "SAM")
			{
				spineAnim = this.RenderSam(simHero);
			}
			else if (id == "BLIND_ARCHER")
			{
				spineAnim = this.RenderBlindArcher(simHero);
			}
			else if (id == "JIM")
			{
				spineAnim = this.RenderJim(simHero);
			}
			else if (id == "TAM")
			{
				spineAnim = this.RenderTam(simHero);
			}
			else if (id == "WARLOCK")
			{
				spineAnim = this.RenderWarlock(simHero);
			}
			else if (id == "GOBLIN")
			{
				spineAnim = this.RenderGoblin(simHero);
			}
			else if (id == "BABU")
			{
				spineAnim = this.RenderBabu(simHero);
			}
			else
			{
				if (!(id == "DRUID"))
				{
					throw new NotImplementedException();
				}
				spineAnim = this.RenderDruid(simHero, sim.GetNumHeroSkins(id));
				if (simHero.IsChangingWeaponToTemp())
				{
					float inStateTimeCounter = simHero.inStateTimeCounter;
					float num = inStateTimeCounter / 3.2f;
					if (num > 0.4f)
					{
						additionalScale = GameMath.Lerp(1f, 1.8f, (num - 0.4f) / 0.05f);
					}
				}
				else if (simHero.IsChangingWeaponToOrig())
				{
					float inStateTimeCounter2 = simHero.inStateTimeCounter;
					float num2 = inStateTimeCounter2 / SpineAnimDruid.animUltiEnd.duration;
					if (num2 > 0.6f)
					{
						additionalScale = GameMath.Lerp(1.8f, 1f, (num2 - 0.6f) / 0.04f);
					}
					else
					{
						additionalScale = 1.8f;
					}
				}
				else if (simHero.IsUsingTempWeapon())
				{
					additionalScale = 1.8f;
				}
			}
			spineAnim.gameObject.transform.localPosition = GameMath.ConvertToScreenSpace(simHero.pos);
			Vector2 rootBonePosition = spineAnim.GetRootBonePosition();
			if (this.buffEffectPositions.ContainsKey(id))
			{
				this.buffEffectPositions[id] = rootBonePosition;
			}
			else
			{
				this.buffEffectPositions.Add(id, rootBonePosition);
			}
			bool flag = simHero.IsInFrontBlackCurtain();
			Color color = (!simHero.IsDuplicate()) ? RenderManager.WHITE_COLOR : RenderManager.HERO_DUPLICATE_COLOR;
			if (!flag)
			{
				color *= Color.Lerp(RenderManager.BLACK_CURTAIN_MIN_COLOR, RenderManager.BLACK_CURTAIN_MAX_COLOR, sim.GetActiveWorld().blackCurtainRatio);
			}
			spineAnim.SetColor(color);
			this.PutShieldBar(simHero, flag);
			this.PutHealthBar(simHero, flag, 1f);
			this.PutTimeBar(simHero, simHero.GetAttackBarTimeRatio(), flag);
			this.RenderBuffs(simHero, 0f, sim.GetActiveWorld(), flag, additionalScale);
		}

		public void UnloadGameAssets()
		{
			foreach (string key in this.heroInGameAssetsBundles.Keys)
			{
				if (HeroIds.HeroInGameAssetsBundleByName.ContainsKey(key))
				{
					DynamicLoadManager.RemovePermanentReferenceToBundle(HeroIds.HeroInGameAssetsBundleByName[key]);
				}
			}
			this.heroInGameAssetsBundles.Clear();
			this.worldBackground.loadingIndex = -1;
			this.worldBackground.skinIndex = -1;
		}

		private SpineAnim RenderHoratio(Hero simHero)
		{
			SpineAnim instance = this.poolHoratio.GetInstance();
			instance.SetSkin(simHero.GetEquippedSkinIndex());
			if (simHero.IsDead())
			{
				Spine.Animation animDeath = SpineAnimHoratio.animDeath;
				float inStateTimeCounter = simHero.inStateTimeCounter;
				float reviveDuration = simHero.GetReviveDuration();
				float time;
				if (inStateTimeCounter < animDeath.duration / 2f)
				{
					time = inStateTimeCounter;
				}
				else if (reviveDuration < inStateTimeCounter + animDeath.duration / 2f)
				{
					time = animDeath.duration + inStateTimeCounter - reviveDuration;
				}
				else
				{
					time = animDeath.duration / 2f;
				}
				instance.Apply(animDeath, time, false);
			}
			else if (simHero.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimHoratio.animIdle;
				float inStateTimeCounter2 = simHero.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter2, true);
			}
			else if (simHero.IsAttacking())
			{
				int num = simHero.GetNumHits() % SpineAnimHoratio.animsAttack.Length;
				Spine.Animation animation = SpineAnimHoratio.animsAttack[num];
				float time2 = animation.duration * simHero.GetAttackAnimTimeRatio();
				instance.Apply(animation, time2, false);
			}
			else
			{
				if (!simHero.IsUsingSkill())
				{
					throw new NotImplementedException();
				}
				SkillActiveDataBase runningSkillDataBase = simHero.GetRunningSkillDataBase();
				Spine.Animation animation2;
				float time3;
				if (runningSkillDataBase is SkillDataBaseDeadlyTwirl)
				{
					int runningSkillAnimIndex = simHero.GetRunningSkillAnimIndex();
					if (runningSkillAnimIndex == 0)
					{
						animation2 = SpineAnimHoratio.animUltiStart;
					}
					else if (runningSkillAnimIndex == 1)
					{
						animation2 = SpineAnimHoratio.animUltiLoop;
					}
					else
					{
						if (runningSkillAnimIndex != 2)
						{
							throw new NotImplementedException();
						}
						animation2 = SpineAnimHoratio.animUltiEnd;
					}
					time3 = animation2.duration * simHero.GetRunningSkillCurAnimTimeRatio();
				}
				else if (runningSkillDataBase is SkillDataBaseReversedExcalibur)
				{
					animation2 = SpineAnimHoratio.animsAuto[0];
					time3 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
				}
				else
				{
					if (!(runningSkillDataBase is SkillDataBaseHeHasThePower))
					{
						throw new NotImplementedException();
					}
					animation2 = SpineAnimHoratio.animsAuto[1];
					time3 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
				}
				instance.Apply(animation2, time3, false);
			}
			return instance;
		}

		private SpineAnim RenderThour(Hero simHero)
		{
			SpineAnim instance = this.poolThour.GetInstance();
			instance.SetSkin(simHero.GetEquippedSkinIndex());
			bool flag = simHero.IsUsingTempWeapon();
			if (simHero.IsDead())
			{
				Spine.Animation animDeath = SpineAnimThour.animDeath;
				float inStateTimeCounter = simHero.inStateTimeCounter;
				float reviveDuration = simHero.GetReviveDuration();
				float time;
				if (inStateTimeCounter < animDeath.duration / 2f)
				{
					time = inStateTimeCounter;
				}
				else if (reviveDuration < inStateTimeCounter + animDeath.duration / 2f)
				{
					time = animDeath.duration + inStateTimeCounter - reviveDuration;
				}
				else
				{
					time = animDeath.duration / 2f;
				}
				instance.Apply(animDeath, time, false);
			}
			else if (simHero.IsIdle())
			{
				Spine.Animation anim = (!flag) ? SpineAnimThour.animsIdle[0] : SpineAnimThour.animsIdle[1];
				float inStateTimeCounter2 = simHero.inStateTimeCounter;
				instance.Apply(anim, inStateTimeCounter2, true);
			}
			else if (simHero.IsAttacking())
			{
				Spine.Animation animation;
				if (flag)
				{
					int num = 1 + simHero.GetNumHits() % 2;
					animation = SpineAnimThour.animsAttack[num];
				}
				else
				{
					animation = SpineAnimThour.animsAttack[0];
				}
				float time2 = animation.duration * simHero.GetAttackAnimTimeRatio();
				instance.Apply(animation, time2, false);
			}
			else if (simHero.IsChangingWeaponToTemp())
			{
				Spine.Animation animUltiStart = SpineAnimThour.animUltiStart;
				float time3 = animUltiStart.duration * (simHero.inStateTimeCounter / simHero.durWeaponChange);
				instance.Apply(animUltiStart, time3, false);
			}
			else if (simHero.IsChangingWeaponToOrig())
			{
				Spine.Animation animUltiEnd = SpineAnimThour.animUltiEnd;
				float time4 = animUltiEnd.duration * (simHero.inStateTimeCounter / simHero.durWeaponChange);
				instance.Apply(animUltiEnd, time4, false);
			}
			else
			{
				if (!simHero.IsUsingSkill())
				{
					throw new NotImplementedException();
				}
				SkillActiveDataBase runningSkillDataBase = simHero.GetRunningSkillDataBase();
				if (runningSkillDataBase is SkillDataBaseTaunt)
				{
					Spine.Animation animation2 = SpineAnimThour.animsAuto[0];
					float time5 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
					instance.Apply(animation2, time5, false);
				}
				else if (runningSkillDataBase is SkillDataBaseLunchTime)
				{
					Spine.Animation animation2 = SpineAnimThour.animsAuto[1];
					float time5 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
					instance.Apply(animation2, time5, false);
				}
				else if (!(runningSkillDataBase is SkillDataBaseAnger))
				{
					throw new NotImplementedException();
				}
			}
			return instance;
		}

		private SpineAnim RenderIda(Hero simHero)
		{
			SpineAnim instance = this.poolIda.GetInstance();
			instance.SetSkin(simHero.GetEquippedSkinIndex());
			if (simHero.IsDead())
			{
				Spine.Animation animDeath = SpineAnimIda.animDeath;
				float inStateTimeCounter = simHero.inStateTimeCounter;
				float reviveDuration = simHero.GetReviveDuration();
				float time;
				if (inStateTimeCounter < animDeath.duration / 2f)
				{
					time = inStateTimeCounter;
				}
				else if (reviveDuration < inStateTimeCounter + animDeath.duration / 2f)
				{
					time = animDeath.duration + inStateTimeCounter - reviveDuration;
				}
				else
				{
					time = animDeath.duration / 2f;
				}
				instance.Apply(animDeath, time, false);
			}
			else if (simHero.IsIdle())
			{
				if (simHero.IsOverheated())
				{
					Spine.Animation animBreath = SpineAnimIda.animBreath;
					float inStateTimeCounter2 = simHero.inStateTimeCounter;
					float overheatTimeLeft = simHero.GetOverheatTimeLeft();
					if (overheatTimeLeft < RenderManager.IdaAnimDurs[2])
					{
						float time2 = RenderManager.IdaAnimDurs[0] + RenderManager.IdaAnimDurs[1] + RenderManager.IdaAnimDurs[2] - overheatTimeLeft;
						instance.Apply(animBreath, time2, false);
					}
					else if (inStateTimeCounter2 < RenderManager.IdaAnimDurs[0])
					{
						float time3 = inStateTimeCounter2;
						instance.Apply(animBreath, time3, false);
					}
					else
					{
						float num = inStateTimeCounter2 + overheatTimeLeft;
						float num2 = num - RenderManager.IdaAnimDurs[0] - RenderManager.IdaAnimDurs[2];
						float num3 = Mathf.Round(num2 / RenderManager.IdaAnimDurs[1]) * RenderManager.IdaAnimDurs[1];
						if (num3 == 0f)
						{
							num3 = RenderManager.IdaAnimDurs[1];
						}
						float num4 = inStateTimeCounter2 - RenderManager.IdaAnimDurs[0];
						num4 *= num3 / num2;
						num4 %= RenderManager.IdaAnimDurs[1];
						num4 += RenderManager.IdaAnimDurs[0];
						instance.Apply(animBreath, num4, false);
					}
				}
				else
				{
					Spine.Animation animIdle = SpineAnimIda.animIdle;
					float inStateTimeCounter3 = simHero.inStateTimeCounter;
					instance.Apply(animIdle, inStateTimeCounter3, true);
				}
			}
			else if (simHero.IsAttacking())
			{
				int num5 = simHero.GetNumHits() % SpineAnimIda.animsAttack.Length;
				Spine.Animation animation = SpineAnimIda.animsAttack[num5];
				float time4 = animation.duration * simHero.GetAttackAnimTimeRatio();
				instance.Apply(animation, time4, false);
			}
			else
			{
				if (!simHero.IsUsingSkill())
				{
					throw new NotImplementedException();
				}
				SkillActiveDataBase runningSkillDataBase = simHero.GetRunningSkillDataBase();
				Spine.Animation animation2;
				float time5;
				if (runningSkillDataBase is SkillDataBaseShockWave)
				{
					int runningSkillAnimIndex = simHero.GetRunningSkillAnimIndex();
					if (runningSkillAnimIndex == 0)
					{
						animation2 = SpineAnimIda.animUltiStart;
					}
					else if (runningSkillAnimIndex == 1)
					{
						animation2 = SpineAnimIda.animUltiLoop;
					}
					else
					{
						if (runningSkillAnimIndex != 2)
						{
							throw new NotImplementedException();
						}
						animation2 = SpineAnimIda.animUltiEnd;
					}
					time5 = animation2.duration * simHero.GetRunningSkillCurAnimTimeRatio();
				}
				else if (runningSkillDataBase is SkillDataBaseFastCheerful)
				{
					animation2 = SpineAnimIda.animsAuto[0];
					time5 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
				}
				else
				{
					if (!(runningSkillDataBase is SkillDataBaseConcentration))
					{
						throw new NotImplementedException();
					}
					animation2 = SpineAnimIda.animsAuto[1];
					time5 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
				}
				instance.Apply(animation2, time5, false);
			}
			return instance;
		}

		private SpineAnim RenderKindLenny(Hero simHero)
		{
			SpineAnim instance = this.poolKindLenny.GetInstance();
			instance.SetSkin(simHero.GetEquippedSkinIndex());
			if (simHero.IsDead())
			{
				Spine.Animation animDeath = SpineAnimKindLenny.animDeath;
				float inStateTimeCounter = simHero.inStateTimeCounter;
				float reviveDuration = simHero.GetReviveDuration();
				float time;
				if (inStateTimeCounter < animDeath.duration / 2f)
				{
					time = inStateTimeCounter;
				}
				else if (reviveDuration < inStateTimeCounter + animDeath.duration / 2f)
				{
					time = animDeath.duration + inStateTimeCounter - reviveDuration;
				}
				else
				{
					time = animDeath.duration / 2f;
				}
				instance.Apply(animDeath, time, false);
			}
			else if (simHero.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimKindLenny.animIdle;
				float inStateTimeCounter2 = simHero.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter2, true);
			}
			else if (simHero.IsAttacking())
			{
				if (simHero.IsReloading())
				{
					Spine.Animation animReload = SpineAnimKindLenny.animReload;
					float time2 = animReload.duration * simHero.GetReloadTimeRatio();
					instance.Apply(animReload, time2, false);
				}
				else
				{
					Spine.Animation animAttack = SpineAnimKindLenny.animAttack;
					float time3 = animAttack.duration * simHero.GetAttackAnimTimeRatio();
					instance.Apply(animAttack, time3, false);
				}
			}
			else
			{
				if (!simHero.IsUsingSkill())
				{
					throw new NotImplementedException();
				}
				SkillActiveDataBase runningSkillDataBase = simHero.GetRunningSkillDataBase();
				Spine.Animation animation;
				float time4;
				if (runningSkillDataBase is SkillDataBaseMiniCannon)
				{
					int runningSkillAnimIndex = simHero.GetRunningSkillAnimIndex();
					if (runningSkillAnimIndex == 0)
					{
						animation = SpineAnimKindLenny.animUltiStart;
					}
					else if (runningSkillAnimIndex == 1)
					{
						animation = SpineAnimKindLenny.animUltiLoop;
					}
					else
					{
						if (runningSkillAnimIndex != 2)
						{
							throw new NotImplementedException();
						}
						animation = SpineAnimKindLenny.animUltiEnd;
					}
					time4 = animation.duration * simHero.GetRunningSkillCurAnimTimeRatio();
				}
				else if (runningSkillDataBase is SkillDataBaseEatApple)
				{
					animation = SpineAnimKindLenny.animsAuto[0];
					time4 = simHero.GetRunningSkillCurAnimTimeRatio() * animation.duration;
				}
				else
				{
					if (!(runningSkillDataBase is SkillDataBaseBombard))
					{
						throw new NotImplementedException();
					}
					animation = SpineAnimKindLenny.animsAuto[1];
					int runningSkillAnimIndex2 = simHero.GetRunningSkillAnimIndex();
					float runningSkillCurAnimTimeRatio = simHero.GetRunningSkillCurAnimTimeRatio();
					float[] animStartTimeRatios = new float[]
					{
						0f,
						0.72131145f,
						0.868852437f
					};
					float[] animDurTimeRatios = new float[]
					{
						0.72131145f,
						0.147540987f,
						0.131147534f
					};
					time4 = this.GetAnimTimeOfDivided(runningSkillAnimIndex2, runningSkillCurAnimTimeRatio, animStartTimeRatios, animDurTimeRatios, animation.duration);
				}
				instance.Apply(animation, time4, false);
			}
			return instance;
		}

		private SpineAnim RenderDerek(Hero simHero)
		{
			SpineAnim instance = this.poolDerek.GetInstance();
			instance.SetSkin(simHero.GetEquippedSkinIndex());
			if (simHero.IsDead())
			{
				Spine.Animation animDeath = SpineAnimDerek.animDeath;
				float inStateTimeCounter = simHero.inStateTimeCounter;
				float reviveDuration = simHero.GetReviveDuration();
				float time;
				if (inStateTimeCounter < animDeath.duration / 2f)
				{
					time = inStateTimeCounter;
				}
				else if (reviveDuration < inStateTimeCounter + animDeath.duration / 2f)
				{
					time = animDeath.duration + inStateTimeCounter - reviveDuration;
				}
				else
				{
					time = animDeath.duration / 2f;
				}
				instance.Apply(animDeath, time, false);
			}
			else if (simHero.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimDerek.animIdle;
				float inStateTimeCounter2 = simHero.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter2, true);
			}
			else if (simHero.IsAttacking())
			{
				Spine.Animation animAttack = SpineAnimDerek.animAttack;
				float time2 = animAttack.duration * simHero.GetAttackAnimTimeRatio();
				instance.Apply(animAttack, time2, false);
			}
			else
			{
				if (!simHero.IsUsingSkill())
				{
					throw new NotImplementedException();
				}
				SkillActiveDataBase runningSkillDataBase = simHero.GetRunningSkillDataBase();
				Spine.Animation animation;
				float num;
				if (runningSkillDataBase is SkillDataBaseLobMagic)
				{
					animation = SpineAnimDerek.animUlti;
					num = simHero.GetRunningSkillCurAnimTimeRatio() * animation.duration;
				}
				else if (runningSkillDataBase is SkillDataBaseOutOfControl)
				{
					animation = SpineAnimDerek.animsAuto[0];
					int runningSkillAnimIndex = simHero.GetRunningSkillAnimIndex();
					float runningSkillCurAnimTimeRatio = simHero.GetRunningSkillCurAnimTimeRatio();
					float[] animStartTimeRatios = new float[]
					{
						0f,
						0.5479452f,
						0.726027369f
					};
					float[] animDurTimeRatios = new float[]
					{
						0.5479452f,
						0.1780822f,
						0.2739726f
					};
					num = this.GetAnimTimeOfDivided(runningSkillAnimIndex, runningSkillCurAnimTimeRatio, animStartTimeRatios, animDurTimeRatios, animation.duration);
				}
				else
				{
					if (!(runningSkillDataBase is SkillDataBaseThunderSomething))
					{
						throw new NotImplementedException();
					}
					animation = SpineAnimDerek.animsAuto[1];
					num = simHero.GetRunningSkillCurAnimTimeRatio() * animation.duration;
					Spine.Animation anim = SpineAnimProjectileDerekMeteor.anim;
					if (num > animation.duration - anim.duration)
					{
						SpineAnim instance2 = this.poolProjectileDerekMeteor.GetInstance();
						Vector3 localPosition = GameMath.ConvertToScreenSpace(World.ENEMY_CENTER);
						instance2.gameObject.transform.localPosition = localPosition;
						float time3 = num - animation.duration + anim.duration;
						instance2.Apply(anim, time3, false);
					}
				}
				instance.Apply(animation, num, false);
			}
			return instance;
		}

		private SpineAnim RenderSheela(Hero simHero)
		{
			SpineAnim instance = this.poolSheela.GetInstance();
			instance.SetSkin(simHero.GetEquippedSkinIndex());
			bool flag = simHero.IsUsingTempWeapon();
			if (simHero.IsDead())
			{
				Spine.Animation animDeath = SpineAnimSheela.animDeath;
				float inStateTimeCounter = simHero.inStateTimeCounter;
				float reviveDuration = simHero.GetReviveDuration();
				float time;
				if (inStateTimeCounter < animDeath.duration / 2f)
				{
					time = inStateTimeCounter;
				}
				else if (reviveDuration < inStateTimeCounter + animDeath.duration / 2f)
				{
					time = animDeath.duration + inStateTimeCounter - reviveDuration;
				}
				else
				{
					time = animDeath.duration / 2f;
				}
				instance.Apply(animDeath, time, false);
			}
			else if (simHero.IsIdle())
			{
				Spine.Animation anim = (!flag) ? SpineAnimSheela.animIdle : SpineAnimSheela.animIdleUlti;
				float inStateTimeCounter2 = simHero.inStateTimeCounter;
				instance.Apply(anim, inStateTimeCounter2, true);
			}
			else if (simHero.IsAttacking())
			{
				if (simHero.IsReloading())
				{
					Spine.Animation animReload = SpineAnimSheela.animReload;
					float time2 = animReload.duration * simHero.GetReloadTimeRatio();
					instance.Apply(animReload, time2, false);
				}
				else if (flag)
				{
					Spine.Animation animAttackUlti = SpineAnimSheela.animAttackUlti;
					float time3 = animAttackUlti.duration * simHero.GetAttackAnimTimeRatio();
					instance.Apply(animAttackUlti, time3, false);
				}
				else
				{
					int num = simHero.GetNumHits() % SpineAnimSheela.animsAttack.Length;
					Spine.Animation animation = SpineAnimSheela.animsAttack[num];
					float time4 = animation.duration * simHero.GetAttackAnimTimeRatio();
					instance.Apply(animation, time4, false);
				}
			}
			else if (simHero.IsChangingWeaponToTemp())
			{
				Spine.Animation animUltiStart = SpineAnimSheela.animUltiStart;
				float time5 = animUltiStart.duration * (simHero.inStateTimeCounter / simHero.durWeaponChange);
				instance.Apply(animUltiStart, time5, false);
			}
			else if (simHero.IsChangingWeaponToOrig())
			{
				Spine.Animation animUltiEnd = SpineAnimSheela.animUltiEnd;
				float time6 = animUltiEnd.duration * (simHero.inStateTimeCounter / simHero.durWeaponChange);
				instance.Apply(animUltiEnd, time6, false);
			}
			else
			{
				if (!simHero.IsUsingSkill())
				{
					throw new NotImplementedException();
				}
				SkillActiveDataBase runningSkillDataBase = simHero.GetRunningSkillDataBase();
				Spine.Animation animation2 = null;
				float time7 = -1f;
				if (runningSkillDataBase is SkillDataBaseSliceDice)
				{
					animation2 = SpineAnimSheela.animsAuto[0];
					time7 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
				}
				else if (runningSkillDataBase is SkillDataBaseRunEmmetRun)
				{
					animation2 = SpineAnimSheela.animsAuto[1];
					time7 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
				}
				else if (!(runningSkillDataBase is SkillDataBaseMasterThief))
				{
					throw new NotImplementedException();
				}
				instance.Apply(animation2, time7, false);
			}
			return instance;
		}

		private SpineAnim RenderBomberman(Hero simHero)
		{
			SpineAnim instance = this.poolBomberman.GetInstance();
			instance.SetSkin(simHero.GetEquippedSkinIndex());
			if (simHero.IsDead())
			{
				Spine.Animation animDeath = SpineAnimBomberman.animDeath;
				float inStateTimeCounter = simHero.inStateTimeCounter;
				float reviveDuration = simHero.GetReviveDuration();
				float time;
				if (inStateTimeCounter < animDeath.duration / 2f)
				{
					time = inStateTimeCounter;
				}
				else if (reviveDuration < inStateTimeCounter + animDeath.duration / 2f)
				{
					time = animDeath.duration + inStateTimeCounter - reviveDuration;
				}
				else
				{
					time = animDeath.duration / 2f;
				}
				instance.Apply(animDeath, time, false);
			}
			else if (simHero.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimBomberman.animIdle;
				float inStateTimeCounter2 = simHero.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter2, true);
			}
			else if (simHero.IsAttacking())
			{
				Spine.Animation animAttack = SpineAnimBomberman.animAttack;
				float time2 = animAttack.duration * simHero.GetAttackAnimTimeRatio();
				instance.Apply(animAttack, time2, false);
			}
			else
			{
				if (!simHero.IsUsingSkill())
				{
					throw new NotImplementedException();
				}
				SkillActiveDataBase runningSkillDataBase = simHero.GetRunningSkillDataBase();
				Spine.Animation animation;
				float time3;
				if (runningSkillDataBase is SkillDataBaseSelfDestruct)
				{
					animation = SpineAnimBomberman.animUlti;
					time3 = simHero.GetRunningSkillCurAnimTimeRatio() * animation.duration;
				}
				else if (runningSkillDataBase is SkillDataBaseJokesOnYou)
				{
					animation = SpineAnimBomberman.animsAuto[0];
					time3 = simHero.GetRunningSkillCurAnimTimeRatio() * animation.duration;
				}
				else
				{
					if (!(runningSkillDataBase is SkillDataBaseFireworks))
					{
						throw new NotImplementedException();
					}
					animation = SpineAnimBomberman.animsAuto[1];
					time3 = simHero.GetRunningSkillCurAnimTimeRatio() * animation.duration;
				}
				instance.Apply(animation, time3, false);
			}
			return instance;
		}

		private SpineAnim RenderSam(Hero simHero)
		{
			SpineAnim instance = this.poolSam.GetInstance();
			instance.SetSkin(simHero.GetEquippedSkinIndex());
			bool flag = simHero.IsUsingTempWeapon();
			if (simHero.IsDead())
			{
				Spine.Animation animDeath = SpineAnimSam.animDeath;
				float inStateTimeCounter = simHero.inStateTimeCounter;
				float reviveDuration = simHero.GetReviveDuration();
				float time;
				if (inStateTimeCounter < animDeath.duration / 2f)
				{
					time = inStateTimeCounter;
				}
				else if (reviveDuration < inStateTimeCounter + animDeath.duration / 2f)
				{
					time = animDeath.duration + inStateTimeCounter - reviveDuration;
				}
				else
				{
					time = animDeath.duration / 2f;
				}
				instance.Apply(animDeath, time, false);
			}
			else if (simHero.IsIdle())
			{
				Spine.Animation anim = (!flag) ? SpineAnimSam.animsIdle[0] : SpineAnimSam.animsIdle[1];
				float inStateTimeCounter2 = simHero.inStateTimeCounter;
				instance.Apply(anim, inStateTimeCounter2, true);
			}
			else if (simHero.IsAttacking())
			{
				int num = (!flag) ? 0 : 1;
				Spine.Animation animation = SpineAnimSam.animsAttack[num];
				float time2 = animation.duration * simHero.GetAttackAnimTimeRatio();
				instance.Apply(animation, time2, false);
			}
			else if (simHero.IsChangingWeaponToTemp())
			{
				Spine.Animation animUltiStart = SpineAnimSam.animUltiStart;
				float time3 = animUltiStart.duration * (simHero.inStateTimeCounter / simHero.durWeaponChange);
				instance.Apply(animUltiStart, time3, false);
			}
			else if (simHero.IsChangingWeaponToOrig())
			{
				Spine.Animation animUltiEnd = SpineAnimSam.animUltiEnd;
				float time4 = animUltiEnd.duration * (simHero.inStateTimeCounter / simHero.durWeaponChange);
				instance.Apply(animUltiEnd, time4, false);
			}
			else
			{
				if (!simHero.IsUsingSkill())
				{
					throw new NotImplementedException();
				}
				SkillActiveDataBase runningSkillDataBase = simHero.GetRunningSkillDataBase();
				Spine.Animation animation2 = null;
				float time5 = -1f;
				if (runningSkillDataBase is SkillDataBaseShieldAll)
				{
					animation2 = SpineAnimSam.animsAuto[0];
					time5 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
				}
				else if (runningSkillDataBase is SkillDataBaseSlam)
				{
					animation2 = SpineAnimSam.animsAuto[1];
					time5 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
				}
				else if (!(runningSkillDataBase is SkillDataBaseRevenge))
				{
					throw new NotImplementedException();
				}
				instance.Apply(animation2, time5, false);
			}
			return instance;
		}

		private SpineAnim RenderBlindArcher(Hero simHero)
		{
			SpineAnim instance = this.poolBlindArcher.GetInstance();
			instance.SetSkin(simHero.GetEquippedSkinIndex());
			bool flag = simHero.IsUsingTempWeapon();
			if (simHero.IsDead())
			{
				Spine.Animation animDeath = SpineAnimBlindArcher.animDeath;
				float inStateTimeCounter = simHero.inStateTimeCounter;
				float reviveDuration = simHero.GetReviveDuration();
				float time;
				if (inStateTimeCounter < animDeath.duration / 2f)
				{
					time = inStateTimeCounter;
				}
				else if (reviveDuration < inStateTimeCounter + animDeath.duration / 2f)
				{
					time = animDeath.duration + inStateTimeCounter - reviveDuration;
				}
				else
				{
					time = animDeath.duration / 2f;
				}
				instance.Apply(animDeath, time, false);
			}
			else if (simHero.IsIdle())
			{
				Spine.Animation anim = (!flag) ? SpineAnimBlindArcher.animIdle : SpineAnimBlindArcher.animIdleUlti;
				float inStateTimeCounter2 = simHero.inStateTimeCounter;
				instance.Apply(anim, inStateTimeCounter2, true);
			}
			else if (simHero.IsAttacking())
			{
				if (flag)
				{
					Spine.Animation animAttackUlti = SpineAnimBlindArcher.animAttackUlti;
					float time2 = animAttackUlti.duration * simHero.GetAttackAnimTimeRatio();
					instance.Apply(animAttackUlti, time2, false);
				}
				else
				{
					int num = simHero.GetNumHits() % SpineAnimBlindArcher.animsAttack.Length;
					Spine.Animation animation = SpineAnimBlindArcher.animsAttack[num];
					float time3 = animation.duration * simHero.GetAttackAnimTimeRatio();
					instance.Apply(animation, time3, false);
				}
			}
			else if (simHero.IsChangingWeaponToTemp())
			{
				Spine.Animation animUltiStart = SpineAnimBlindArcher.animUltiStart;
				float time4 = animUltiStart.duration * (simHero.inStateTimeCounter / simHero.durWeaponChange);
				instance.Apply(animUltiStart, time4, false);
			}
			else if (simHero.IsChangingWeaponToOrig())
			{
				Spine.Animation animUltiEnd = SpineAnimBlindArcher.animUltiEnd;
				float time5 = animUltiEnd.duration * (simHero.inStateTimeCounter / simHero.durWeaponChange);
				instance.Apply(animUltiEnd, time5, false);
			}
			else
			{
				if (!simHero.IsUsingSkill())
				{
					throw new NotImplementedException();
				}
				SkillActiveDataBase runningSkillDataBase = simHero.GetRunningSkillDataBase();
				Spine.Animation animation2 = null;
				float time6 = -1f;
				if (runningSkillDataBase is SkillDataBaseSnipe)
				{
					animation2 = SpineAnimBlindArcher.animsAuto[0];
					time6 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
				}
				else if (runningSkillDataBase is SkillDataBaseSwiftMoves)
				{
					animation2 = SpineAnimBlindArcher.animsAuto[1];
					time6 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
				}
				else if (!(runningSkillDataBase is SkillDataBaseTargetPractice))
				{
					throw new NotImplementedException();
				}
				if (animation2 != null)
				{
					instance.Apply(animation2, time6, false);
				}
			}
			return instance;
		}

		private SpineAnim RenderJim(Hero simHero)
		{
			SpineAnim instance = this.poolJim.GetInstance();
			instance.SetSkin(simHero.GetEquippedSkinIndex());
			if (simHero.IsDead())
			{
				Spine.Animation animDeath = SpineAnimJim.animDeath;
				float inStateTimeCounter = simHero.inStateTimeCounter;
				float reviveDuration = simHero.GetReviveDuration();
				float time;
				if (inStateTimeCounter < animDeath.duration / 2f)
				{
					time = inStateTimeCounter;
				}
				else if (reviveDuration < inStateTimeCounter + animDeath.duration / 2f)
				{
					time = animDeath.duration + inStateTimeCounter - reviveDuration;
				}
				else
				{
					time = animDeath.duration / 2f;
				}
				instance.Apply(animDeath, time, false);
			}
			else if (simHero.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimJim.animIdle;
				float inStateTimeCounter2 = simHero.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter2, true);
			}
			else if (simHero.IsAttacking())
			{
				int num = simHero.GetNumHits() % SpineAnimJim.animsAttack.Length;
				Spine.Animation animation = SpineAnimJim.animsAttack[num];
				float time2 = animation.duration * simHero.GetAttackAnimTimeRatio();
				instance.Apply(animation, time2, false);
			}
			else
			{
				if (!simHero.IsUsingSkill())
				{
					throw new NotImplementedException();
				}
				SkillActiveDataBase runningSkillDataBase = simHero.GetRunningSkillDataBase();
				Spine.Animation animation2;
				float time3;
				if (runningSkillDataBase is SkillDataBaseBittersweet)
				{
					int runningSkillAnimIndex = simHero.GetRunningSkillAnimIndex();
					if (runningSkillAnimIndex == 0)
					{
						animation2 = SpineAnimJim.animUltiStart;
					}
					else if (runningSkillAnimIndex == 1)
					{
						animation2 = SpineAnimJim.animUltiLoop;
					}
					else
					{
						if (runningSkillAnimIndex != 2)
						{
							throw new NotImplementedException();
						}
						animation2 = SpineAnimJim.animUltiEnd;
					}
					time3 = animation2.duration * simHero.GetRunningSkillCurAnimTimeRatio();
				}
				else if (runningSkillDataBase is SkillDataBaseBattlecry)
				{
					animation2 = SpineAnimJim.animsAuto[0];
					time3 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
				}
				else
				{
					if (!(runningSkillDataBase is SkillDataBaseWeepingSong))
					{
						throw new NotImplementedException();
					}
					animation2 = SpineAnimJim.animsAuto[1];
					time3 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
				}
				instance.Apply(animation2, time3, false);
			}
			return instance;
		}

		private SpineAnim RenderTam(Hero simHero)
		{
			SpineAnim instance = this.poolTam.GetInstance();
			instance.SetSkin(simHero.GetEquippedSkinIndex());
			if (simHero.IsDead())
			{
				Spine.Animation animDeath = SpineAnimTam.animDeath;
				float inStateTimeCounter = simHero.inStateTimeCounter;
				float reviveDuration = simHero.GetReviveDuration();
				float time;
				if (inStateTimeCounter < animDeath.duration / 2f)
				{
					time = inStateTimeCounter;
				}
				else if (reviveDuration < inStateTimeCounter + animDeath.duration / 2f)
				{
					time = animDeath.duration + inStateTimeCounter - reviveDuration;
				}
				else
				{
					time = animDeath.duration / 2f;
				}
				instance.Apply(animDeath, time, false);
			}
			else if (simHero.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimTam.animIdle;
				float inStateTimeCounter2 = simHero.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter2, true);
			}
			else if (simHero.IsAttacking())
			{
				if (simHero.IsReloading())
				{
					Spine.Animation animReload = SpineAnimTam.animReload;
					float time2 = animReload.duration * simHero.GetReloadTimeRatio();
					instance.Apply(animReload, time2, false);
				}
				else
				{
					int num = simHero.GetNumHits() % SpineAnimTam.animsAttack.Length;
					Spine.Animation animation = SpineAnimTam.animsAttack[num];
					float time3 = animation.duration * simHero.GetAttackAnimTimeRatio();
					instance.Apply(animation, time3, false);
				}
			}
			else
			{
				if (!simHero.IsUsingSkill())
				{
					throw new NotImplementedException();
				}
				SkillActiveDataBase runningSkillDataBase = simHero.GetRunningSkillDataBase();
				Spine.Animation animation2;
				float time4;
				if (runningSkillDataBase is SkillDataBaseRoar)
				{
					animation2 = SpineAnimTam.animUlti;
					time4 = animation2.duration * simHero.GetRunningSkillCurAnimTimeRatio();
				}
				else if (runningSkillDataBase is SkillDataBaseCrowAttack)
				{
					animation2 = SpineAnimTam.animsAuto[0];
					time4 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
				}
				else
				{
					if (!(runningSkillDataBase is SkillDataBaseFlare))
					{
						throw new NotImplementedException();
					}
					animation2 = SpineAnimTam.animsAuto[1];
					time4 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
				}
				instance.Apply(animation2, time4, false);
			}
			return instance;
		}

		private SpineAnim RenderWarlock(Hero simHero)
		{
			SpineAnim instance = this.poolWarlock.GetInstance();
			instance.SetSkin(simHero.GetEquippedSkinIndex());
			if (simHero.IsDead())
			{
				Spine.Animation animDeath = SpineAnimWarlock.animDeath;
				float inStateTimeCounter = simHero.inStateTimeCounter;
				float reviveDuration = simHero.GetReviveDuration();
				float num = 1.5f;
				float time;
				if (inStateTimeCounter < animDeath.duration / num)
				{
					time = inStateTimeCounter;
				}
				else if (reviveDuration < inStateTimeCounter + animDeath.duration - animDeath.duration / num)
				{
					time = animDeath.duration + inStateTimeCounter - reviveDuration;
				}
				else
				{
					time = animDeath.duration / num;
				}
				instance.Apply(animDeath, time, false);
			}
			else if (simHero.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimWarlock.animIdle;
				float inStateTimeCounter2 = simHero.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter2, true);
			}
			else if (simHero.IsAttacking())
			{
				Spine.Animation animation = SpineAnimWarlock.animsAttack[simHero.GetNumHits() % SpineAnimWarlock.animsAttack.Length];
				float time2 = animation.duration * simHero.GetAttackAnimTimeRatio();
				instance.Apply(animation, time2, false);
			}
			else
			{
				if (!simHero.IsUsingSkill())
				{
					throw new NotImplementedException();
				}
				SkillActiveDataBase runningSkillDataBase = simHero.GetRunningSkillDataBase();
				Spine.Animation animation2;
				float time3;
				if (runningSkillDataBase is SkillDataBaseDarkRitual)
				{
					int runningSkillAnimIndex = simHero.GetRunningSkillAnimIndex();
					float runningSkillCurAnimTimeRatio;
					if (runningSkillAnimIndex == 0)
					{
						animation2 = SpineAnimWarlock.animUltiStart;
						runningSkillCurAnimTimeRatio = simHero.GetRunningSkillCurAnimTimeRatio();
					}
					else if (runningSkillAnimIndex == 1)
					{
						animation2 = SpineAnimWarlock.animUltiAttacks[0];
						runningSkillCurAnimTimeRatio = simHero.GetRunningSkillCurAnimTimeRatio();
					}
					else if (runningSkillAnimIndex == 3)
					{
						animation2 = SpineAnimWarlock.animUltiAttacks[1];
						runningSkillCurAnimTimeRatio = simHero.GetRunningSkillCurAnimTimeRatio();
					}
					else
					{
						if (runningSkillAnimIndex != 2)
						{
							throw new NotImplementedException();
						}
						animation2 = SpineAnimWarlock.animUltiEnd;
						runningSkillCurAnimTimeRatio = simHero.GetRunningSkillCurAnimTimeRatio();
					}
					time3 = runningSkillCurAnimTimeRatio * animation2.duration;
				}
				else if (runningSkillDataBase is SkillDataBaseDemonicSwarm)
				{
					animation2 = SpineAnimWarlock.animsAuto[0];
					time3 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
				}
				else
				{
					if (!(runningSkillDataBase is SkillDataBaseRegret))
					{
						throw new NotImplementedException();
					}
					animation2 = SpineAnimWarlock.animsAuto[1];
					time3 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
				}
				instance.Apply(animation2, time3, false);
			}
			return instance;
		}

		private SpineAnim RenderGoblin(Hero simHero)
		{
			SpineAnim instance = this.poolGoblin.GetInstance();
			instance.SetSkin(simHero.GetEquippedSkinIndex());
			if (simHero.IsDead())
			{
				Spine.Animation animDeath = SpineAnimGoblin.animDeath;
				float inStateTimeCounter = simHero.inStateTimeCounter;
				float reviveDuration = simHero.GetReviveDuration();
				float time;
				if (inStateTimeCounter < animDeath.duration / 2f)
				{
					time = inStateTimeCounter;
				}
				else if (reviveDuration < inStateTimeCounter + animDeath.duration / 2f)
				{
					time = animDeath.duration + inStateTimeCounter - reviveDuration;
				}
				else
				{
					time = animDeath.duration / 2f;
				}
				instance.Apply(animDeath, time, false);
			}
			else if (simHero.IsIdle())
			{
				int num = simHero.idleChanger % 5;
				if (num > 1)
				{
					num = 0;
				}
				Spine.Animation anim = SpineAnimGoblin.animsIdle[num];
				float inStateTimeCounter2 = simHero.inStateTimeCounter;
				instance.Apply(anim, inStateTimeCounter2, true);
			}
			else if (simHero.IsAttacking())
			{
				int num2 = simHero.GetNumHits() % 8;
				if (num2 > 1)
				{
					num2 = 0;
				}
				Spine.Animation animation = SpineAnimGoblin.animsAttack[num2];
				float time2 = animation.duration * simHero.GetAttackAnimTimeRatio();
				instance.Apply(animation, time2, false);
			}
			else
			{
				if (!simHero.IsUsingSkill())
				{
					throw new NotImplementedException();
				}
				SkillActiveDataBase runningSkillDataBase = simHero.GetRunningSkillDataBase();
				Spine.Animation animation2;
				float time3;
				if (runningSkillDataBase is SkillDataBaseGreedGrenade)
				{
					animation2 = SpineAnimGoblin.animUlti;
					time3 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
				}
				else if (runningSkillDataBase is SkillDataBaseNegotiate)
				{
					animation2 = SpineAnimGoblin.animsAuto[0];
					time3 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
				}
				else
				{
					if (!(runningSkillDataBase is SkillDataBaseCommonAffinities))
					{
						throw new NotImplementedException();
					}
					animation2 = SpineAnimGoblin.animsAuto[1];
					time3 = simHero.GetRunningSkillCurAnimTimeRatio() * animation2.duration;
				}
				instance.Apply(animation2, time3, false);
			}
			return instance;
		}

		private SpineAnim RenderBabu(Hero simHero)
		{
			SpineAnim instance = this.poolBabu.GetInstance();
			instance.SetSkin(simHero.GetEquippedSkinIndex());
			if (simHero.IsDead())
			{
				Skill castedSkillDuringDeath = simHero.castedSkillDuringDeath;
				Spine.Animation animation;
				float num;
				if (castedSkillDuringDeath != null && castedSkillDuringDeath.GetAnimIndex() == 1)
				{
					animation = SpineAnimBabu.animDeathUlti;
					num = 1.184f;
				}
				else
				{
					animation = SpineAnimBabu.animDeath;
					num = 1.16f;
				}
				float inStateTimeCounter = simHero.inStateTimeCounter;
				float reviveDuration = simHero.GetReviveDuration();
				float time;
				if (inStateTimeCounter < num)
				{
					time = inStateTimeCounter;
				}
				else if (reviveDuration < inStateTimeCounter + animation.duration - num)
				{
					time = animation.duration + inStateTimeCounter - reviveDuration;
				}
				else
				{
					time = num;
				}
				instance.Apply(animation, time, false);
			}
			else if (simHero.IsIdle())
			{
				Spine.Animation anim = SpineAnimBabu.animsIdle[0];
				float inStateTimeCounter2 = simHero.inStateTimeCounter;
				instance.Apply(anim, inStateTimeCounter2, true);
			}
			else if (simHero.IsAttacking())
			{
				WeaponSwitched weaponSwitched = simHero.GetWeapon() as WeaponSwitched;
				Spine.Animation animation2 = SpineAnimBabu.animsAttack[weaponSwitched.weaponIndex];
				float time2 = animation2.duration * simHero.GetAttackAnimTimeRatio();
				instance.Apply(animation2, time2, false);
			}
			else if (simHero.IsChangingWeaponToTemp())
			{
				Spine.Animation animUltiStart = SpineAnimBabu.animUltiStart;
				float time3 = animUltiStart.duration * (simHero.inStateTimeCounter / simHero.durWeaponChange);
				instance.Apply(animUltiStart, time3, false);
			}
			else if (simHero.IsChangingWeaponToOrig())
			{
				Spine.Animation animUltiEnd = SpineAnimBabu.animUltiEnd;
				float time4 = animUltiEnd.duration * (simHero.inStateTimeCounter / simHero.durWeaponChange);
				instance.Apply(animUltiEnd, time4, false);
			}
			else
			{
				if (!simHero.IsUsingSkill())
				{
					throw new NotImplementedException();
				}
				SkillActiveDataBase runningSkillDataBase = simHero.GetRunningSkillDataBase();
				Spine.Animation animation3 = null;
				if (runningSkillDataBase is SkillDataBaseGoodCupOfTea)
				{
					animation3 = SpineAnimBabu.animsAuto[0];
					float time5 = simHero.GetRunningSkillCurAnimTimeRatio() * animation3.duration;
					instance.Apply(animation3, time5, false);
				}
				else if (runningSkillDataBase is SkillDataBaseGotYourBack)
				{
					animation3 = SpineAnimBabu.animsAuto[1];
					float time5 = simHero.GetRunningSkillCurAnimTimeRatio() * animation3.duration;
					instance.Apply(animation3, time5, false);
				}
				else
				{
					if (!(runningSkillDataBase is SkillDataBaseTakeOneForTheTeam))
					{
						throw new NotImplementedException();
					}
					float num2 = 0f;
					bool loop = false;
					int runningSkillAnimIndex = simHero.GetRunningSkillAnimIndex();
					if (runningSkillAnimIndex == 0)
					{
						animation3 = SpineAnimBabu.animUltiStart;
						num2 = simHero.GetRunningSkillCurAnimTimeRatio();
					}
					else if (runningSkillAnimIndex == 1)
					{
						animation3 = SpineAnimBabu.animUltiLoop;
						float num3 = simHero.GetRunningSkillCurAnimDur() / animation3.duration;
						num2 = simHero.GetRunningSkillCurAnimTimeRatio() * num3;
						loop = true;
					}
					else if (runningSkillAnimIndex == 2)
					{
						animation3 = SpineAnimBabu.animUltiEnd;
						num2 = simHero.GetRunningSkillCurAnimTimeRatio();
					}
					float time5 = num2 * animation3.duration;
					instance.Apply(animation3, time5, loop);
				}
			}
			return instance;
		}

		private SpineAnim RenderDruid(Hero simHero, int numSkins)
		{
			SpineAnim instance = this.poolDruid.GetInstance();
			bool flag = (simHero.IsUsingTempWeapon() || simHero.IsChangingWeaponToOrig()) && !simHero.IsChangingWeaponToTemp() && simHero.IsAlive();
			int num = simHero.GetEquippedSkinIndex();
			if (flag)
			{
				num += numSkins;
			}
			instance.SetSkin(num);
			simHero.heightOffset = ((!flag) ? 0f : 0.1f);
			instance.gameObject.transform.localScale = ((!flag || simHero.IsChangingWeaponToOrig()) ? SpineAnimDruid.NormalStateScale : SpineAnimDruid.TransformedStateScale);
			if (simHero.IsDead())
			{
				Spine.Animation animation;
				float num2;
				if (simHero.IsUsingTempWeapon())
				{
					animation = SpineAnimDruid.animDeathTransformed;
					num2 = 1.184f;
				}
				else
				{
					animation = SpineAnimDruid.animDeath;
					num2 = 1.16f;
				}
				float inStateTimeCounter = simHero.inStateTimeCounter;
				float reviveDuration = simHero.GetReviveDuration();
				float time;
				if (inStateTimeCounter < num2)
				{
					time = inStateTimeCounter;
				}
				else if (reviveDuration < inStateTimeCounter + animation.duration - num2)
				{
					time = animation.duration + inStateTimeCounter - reviveDuration;
				}
				else
				{
					time = num2;
				}
				instance.Apply(animation, time, false);
			}
			else if (simHero.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimDruid.animIdle;
				float inStateTimeCounter2 = simHero.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter2, true);
			}
			else if (simHero.IsAttacking())
			{
				int numHits = simHero.GetNumHits();
				int num3;
				if ((numHits + 1) % 10 == 0)
				{
					num3 = SpineAnimDruid.animAttacks.Length - 1;
				}
				else
				{
					num3 = simHero.GetNumHits() % (SpineAnimDruid.animAttacks.Length - 1);
				}
				Spine.Animation animation2 = SpineAnimDruid.animAttacks[num3];
				float time2 = animation2.duration * simHero.GetAttackAnimTimeRatio();
				instance.Apply(animation2, time2, false);
			}
			else if (simHero.IsChangingWeaponToTemp())
			{
				Spine.Animation anim = (!simHero.GetWeapon().isRepeatedTempWeapon) ? SpineAnimDruid.animUltiStart : SpineAnimDruid.animUltiRepeat;
				float inStateTimeCounter3 = simHero.inStateTimeCounter;
				float num4 = inStateTimeCounter3 / 3.2f;
				if (num4 > 0.4f)
				{
					simHero.heightOffset = GameMath.Lerp(0f, 0.1f, (num4 - 0.4f) / 0.05f);
				}
				instance.Apply(anim, inStateTimeCounter3, false);
			}
			else if (simHero.IsChangingWeaponToOrig())
			{
				Spine.Animation animUltiEnd = SpineAnimDruid.animUltiEnd;
				float inStateTimeCounter4 = simHero.inStateTimeCounter;
				instance.Apply(animUltiEnd, inStateTimeCounter4, false);
			}
			else
			{
				if (!simHero.IsUsingSkill())
				{
					throw new NotImplementedException();
				}
				SkillActiveDataBase runningSkillDataBase = simHero.GetRunningSkillDataBase();
				if (runningSkillDataBase is SkillDataBaseStampede)
				{
					Spine.Animation animation3 = SpineAnimDruid.animsAuto[0];
					float time3 = simHero.GetRunningSkillCurAnimTimeRatio() * animation3.duration;
					instance.Apply(animation3, time3, false);
				}
				else if (runningSkillDataBase is SkillDataBaseLarry)
				{
					Spine.Animation animation3 = SpineAnimDruid.animsAuto[1];
					float time3 = simHero.GetRunningSkillCurAnimTimeRatio() * animation3.duration;
					instance.Apply(animation3, time3, false);
				}
				else if (!(runningSkillDataBase is SkillDataBaseBeastMode))
				{
					throw new NotImplementedException();
				}
			}
			return instance;
		}

		private float GetAnimTimeOfDivided(int animIndex, float animTimeRatio, float[] animStartTimeRatios, float[] animDurTimeRatios, float animDur)
		{
			float num = animTimeRatio * animDurTimeRatios[animIndex];
			num += animStartTimeRatios[animIndex];
			return num * animDur;
		}

		private void Render(Enemy simEnemy, Challenge challenge)
		{
			SpineAnim spineAnim;
			if (simEnemy.GetName() == "BANDIT")
			{
				spineAnim = this.RenderBandit(simEnemy);
			}
			else if (simEnemy.GetName() == "WOLF")
			{
				spineAnim = this.RenderWolf(simEnemy);
			}
			else if (simEnemy.GetName() == "SPIDER")
			{
				spineAnim = this.RenderSpider(simEnemy);
			}
			else if (simEnemy.GetName() == "BAT")
			{
				spineAnim = this.RenderBat(simEnemy);
			}
			else if (simEnemy.GetName() == "MANGOLIES")
			{
				spineAnim = this.RenderMagolies(simEnemy);
			}
			else if (simEnemy.GetName() == "SEMI CORRUPTED DWARF")
			{
				spineAnim = this.RenderDwarfSemiCorrupted(simEnemy);
			}
			else if (simEnemy.GetName() == "SEMI CORRUPTED ELF")
			{
				spineAnim = this.RenderElfSemiCorrupted(simEnemy);
			}
			else if (simEnemy.GetName() == "CORRUPTED ELF")
			{
				spineAnim = this.RenderElfCorrupted(simEnemy);
			}
			else if (simEnemy.GetName() == "CORRUPTED DWARF")
			{
				spineAnim = this.RenderDwarfCorrupted(simEnemy);
			}
			else if (simEnemy.GetName() == "CORRUPTED HUMAN")
			{
				spineAnim = this.RenderHumanCorrupted(simEnemy);
			}
			else if (simEnemy.GetName() == "SEMI CORRUPTED HUMAN")
			{
				spineAnim = this.RenderHumanSemiCorrupted(simEnemy);
			}
			else if (simEnemy.GetName() == "CHEST")
			{
				spineAnim = this.RenderChest(simEnemy);
			}
			else if (simEnemy.GetName() == "BOSS")
			{
				if (simEnemy.IsEpic())
				{
					spineAnim = this.RenderBoss(simEnemy, 1f);
				}
				else
				{
					spineAnim = this.RenderBoss(simEnemy, 0.75f);
				}
			}
			else if (simEnemy.GetName() == "BOSS ELF")
			{
				spineAnim = this.RenderBossElf(simEnemy);
			}
			else if (simEnemy.GetName() == "BOSS HUMAN")
			{
				spineAnim = this.RenderBossHuman(simEnemy);
			}
			else if (simEnemy.GetName() == "BOSS DWARF")
			{
				spineAnim = this.RenderBossDwarf(simEnemy);
			}
			else if (simEnemy.GetName() == "BOSS MANGOLIES")
			{
				spineAnim = this.RenderBossMangolies(simEnemy);
			}
			else if (simEnemy.GetName() == "BOSS WISE SNAKE")
			{
				spineAnim = this.RenderBossWiseSnake(simEnemy, challenge);
			}
			else if (simEnemy.GetName() == "SNAKE")
			{
				spineAnim = this.RenderSnake(simEnemy);
			}
			else
			{
				if (!(simEnemy.GetName() == "BOSS SNOWMAN"))
				{
					throw new NotImplementedException();
				}
				spineAnim = this.RenderBossSnowman(simEnemy);
			}
			spineAnim.SetColor(((!simEnemy.IsStunned()) ? RenderManager.WHITE_COLOR : RenderManager.ENEMY_STUNNED_COLOR) * this.cachedBlackCurtainColor);
			this.PutHealthBar(simEnemy, false, simEnemy.data.dataBase.scale);
			this.RenderBuffs(simEnemy, 0f, challenge.world, false, simEnemy.data.dataBase.scale);
		}

		private SpineAnim RenderBandit(Enemy simEnemy)
		{
			SpineAnim instance = this.poolBandit.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			int num = simEnemy.genericRandom % SpineAnimBandit.skinNames.Length;
			instance.SetSkin(SpineAnimBandit.skinNames[num]);
			if (simEnemy.IsDead())
			{
				Spine.Animation animDie = SpineAnimBandit.animDie;
				float time = animDie.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animDie, time, false);
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				Spine.Animation animSpawnTranslate = SpineAnimBandit.animSpawnTranslate;
				float inStateTimeCounter = simEnemy.inStateTimeCounter;
				instance.Apply(animSpawnTranslate, inStateTimeCounter, true);
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawnDrop = SpineAnimBandit.animSpawnDrop;
				float time2 = simEnemy.GetAnimTimeRatioSpawnDrop() * animSpawnDrop.duration;
				instance.Apply(animSpawnDrop, time2, false);
			}
			else if (simEnemy.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimBandit.animIdle;
				float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter2, true);
			}
			else if (simEnemy.IsAttacking())
			{
				int num2 = simEnemy.numHits % SpineAnimBandit.animsAttack.Length;
				Spine.Animation animation = SpineAnimBandit.animsAttack[num2];
				float time3 = animation.duration * simEnemy.timeCounterAttackActive / simEnemy.data.durAttackActive;
				instance.Apply(animation, time3, false);
			}
			else
			{
				if (!simEnemy.IsStunned())
				{
					throw new NotImplementedException();
				}
				Spine.Animation animIdle2 = SpineAnimBandit.animIdle;
				float inStateTimeCounter3 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle2, inStateTimeCounter3, true);
			}
			instance.gameObject.transform.localPosition = localPosition;
			return instance;
		}

		private SpineAnim RenderWolf(Enemy simEnemy)
		{
			SpineAnim instance = this.poolWolf.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			int num = simEnemy.genericRandom % SpineAnimWolf.skinNames.Length;
			instance.SetSkin(SpineAnimWolf.skinNames[num]);
			if (simEnemy.IsDead())
			{
				Spine.Animation animDie = SpineAnimWolf.animDie;
				float time = animDie.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animDie, time, false);
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				Spine.Animation animSpawnTranslate = SpineAnimWolf.animSpawnTranslate;
				float inStateTimeCounter = simEnemy.inStateTimeCounter;
				instance.Apply(animSpawnTranslate, inStateTimeCounter, true);
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawnDrop = SpineAnimWolf.animSpawnDrop;
				float time2 = simEnemy.GetAnimTimeRatioSpawnDrop() * animSpawnDrop.duration;
				instance.Apply(animSpawnDrop, time2, false);
			}
			else if (simEnemy.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimWolf.animIdle;
				float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter2, true);
			}
			else if (simEnemy.IsAttacking())
			{
				int num2 = simEnemy.numHits % SpineAnimWolf.animsAttack.Length;
				Spine.Animation animation = SpineAnimWolf.animsAttack[num2];
				float time3 = animation.duration * simEnemy.timeCounterAttackActive / simEnemy.data.durAttackActive;
				instance.Apply(animation, time3, false);
			}
			else
			{
				if (!simEnemy.IsStunned())
				{
					throw new NotImplementedException();
				}
				Spine.Animation animIdle2 = SpineAnimWolf.animIdle;
				float inStateTimeCounter3 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle2, inStateTimeCounter3, true);
			}
			instance.gameObject.transform.localPosition = localPosition;
			return instance;
		}

		private SpineAnim RenderSpider(Enemy simEnemy)
		{
			SpineAnim instance = this.poolSpider.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			int num = simEnemy.genericRandom % SpineAnimSpider.skinNames.Length;
			instance.SetSkin(SpineAnimSpider.skinNames[num]);
			if (simEnemy.IsDead())
			{
				Spine.Animation animDie = SpineAnimSpider.animDie;
				float time = animDie.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animDie, time, false);
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				Spine.Animation animSpawnTranslate = SpineAnimSpider.animSpawnTranslate;
				float inStateTimeCounter = simEnemy.inStateTimeCounter;
				instance.Apply(animSpawnTranslate, inStateTimeCounter, true);
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawnDrop = SpineAnimSpider.animSpawnDrop;
				float time2 = simEnemy.GetAnimTimeRatioSpawnDrop() * animSpawnDrop.duration;
				instance.Apply(animSpawnDrop, time2, false);
			}
			else if (simEnemy.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimSpider.animIdle;
				float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter2, true);
			}
			else if (simEnemy.IsAttacking())
			{
				int num2 = (simEnemy.numHits + simEnemy.genericRandom) % SpineAnimSpider.animsAttack.Length;
				Spine.Animation animation = SpineAnimSpider.animsAttack[num2];
				float time3 = animation.duration * simEnemy.timeCounterAttackActive / simEnemy.data.durAttackActive;
				instance.Apply(animation, time3, false);
			}
			else
			{
				if (!simEnemy.IsStunned())
				{
					throw new NotImplementedException();
				}
				Spine.Animation animIdle2 = SpineAnimSpider.animIdle;
				float inStateTimeCounter3 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle2, inStateTimeCounter3, true);
			}
			instance.gameObject.transform.localPosition = localPosition;
			return instance;
		}

		private SpineAnim RenderBat(Enemy simEnemy)
		{
			SpineAnim instance = this.poolBat.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			if (simEnemy.IsDead())
			{
				Spine.Animation animDie = SpineAnimBat.animDie;
				float time = animDie.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animDie, time, false);
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawnDrop = SpineAnimBat.animSpawnDrop;
				float time2 = animSpawnDrop.duration * simEnemy.GetAnimTimeRatioSpawnDrop();
				instance.Apply(animSpawnDrop, time2, false);
			}
			else if (simEnemy.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimBat.animIdle;
				float inStateTimeCounter = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter, true);
			}
			else if (simEnemy.IsAttacking())
			{
				int num = (simEnemy.numHits + simEnemy.genericRandom) % SpineAnimBat.animsAttack.Length;
				Spine.Animation animation = SpineAnimBat.animsAttack[num];
				float time3 = animation.duration * simEnemy.timeCounterAttackActive / simEnemy.data.durAttackActive;
				instance.Apply(animation, time3, false);
			}
			else
			{
				if (!simEnemy.IsStunned())
				{
					throw new NotImplementedException();
				}
				Spine.Animation animIdle2 = SpineAnimBat.animIdle;
				float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle2, inStateTimeCounter2, true);
			}
			instance.gameObject.transform.localPosition = localPosition;
			return instance;
		}

		private SpineAnim RenderMagolies(Enemy simEnemy)
		{
			SpineAnim instance = this.poolMagolies.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			int num = simEnemy.genericRandom % SpineAnimMagolies.skinNames.Length;
			instance.SetSkin(SpineAnimMagolies.skinNames[num]);
			if (simEnemy.IsDead())
			{
				int num2 = simEnemy.genericRandom % SpineAnimMagolies.animsDie.Length;
				Spine.Animation animation = SpineAnimMagolies.animsDie[num2];
				float time = animation.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animation, time, false);
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				Spine.Animation animSpawnTranslate = SpineAnimMagolies.animSpawnTranslate;
				float inStateTimeCounter = simEnemy.inStateTimeCounter;
				instance.Apply(animSpawnTranslate, inStateTimeCounter, true);
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawnDrop = SpineAnimMagolies.animSpawnDrop;
				float time2 = simEnemy.GetAnimTimeRatioSpawnDrop() * animSpawnDrop.duration;
				instance.Apply(animSpawnDrop, time2, false);
			}
			else if (simEnemy.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimMagolies.animIdle;
				float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter2, true);
			}
			else if (simEnemy.IsAttacking())
			{
				int num3 = (simEnemy.numHits + simEnemy.genericRandom) % SpineAnimMagolies.animsAttack.Length;
				Spine.Animation animation2 = SpineAnimMagolies.animsAttack[num3];
				float time3 = animation2.duration * simEnemy.timeCounterAttackActive / simEnemy.data.durAttackActive;
				instance.Apply(animation2, time3, false);
			}
			else
			{
				if (!simEnemy.IsStunned())
				{
					throw new NotImplementedException();
				}
				Spine.Animation animIdle2 = SpineAnimMagolies.animIdle;
				float inStateTimeCounter3 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle2, inStateTimeCounter3, true);
			}
			instance.gameObject.transform.localPosition = localPosition;
			return instance;
		}

		private SpineAnim RenderDwarfSemiCorrupted(Enemy simEnemy)
		{
			SpineAnim instance = this.poolDwarfSemiCorrupted.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			int num = simEnemy.genericRandom % SpineAnimDwarfSemiCorrupted.skinNames.Length;
			instance.SetSkin(SpineAnimDwarfSemiCorrupted.skinNames[num]);
			if (simEnemy.IsDead())
			{
				Spine.Animation animDie = SpineAnimDwarfSemiCorrupted.animDie;
				float time = animDie.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animDie, time, false);
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				Spine.Animation animSpawnTranslate = SpineAnimDwarfSemiCorrupted.animSpawnTranslate;
				float inStateTimeCounter = simEnemy.inStateTimeCounter;
				instance.Apply(animSpawnTranslate, inStateTimeCounter, true);
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawnDrop = SpineAnimDwarfSemiCorrupted.animSpawnDrop;
				float time2 = simEnemy.GetAnimTimeRatioSpawnDrop() * animSpawnDrop.duration;
				instance.Apply(animSpawnDrop, time2, false);
			}
			else if (simEnemy.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimDwarfSemiCorrupted.animIdle;
				float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter2, true);
			}
			else if (simEnemy.IsAttacking())
			{
				int num2 = simEnemy.numHits % SpineAnimDwarfSemiCorrupted.animsAttack.Length;
				Spine.Animation animation = SpineAnimDwarfSemiCorrupted.animsAttack[num2];
				float time3 = animation.duration * simEnemy.timeCounterAttackActive / simEnemy.data.durAttackActive;
				instance.Apply(animation, time3, false);
			}
			else
			{
				if (!simEnemy.IsStunned())
				{
					throw new NotImplementedException();
				}
				Spine.Animation animIdle2 = SpineAnimDwarfSemiCorrupted.animIdle;
				float inStateTimeCounter3 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle2, inStateTimeCounter3, true);
			}
			instance.gameObject.transform.localPosition = localPosition;
			return instance;
		}

		private SpineAnim RenderDwarfCorrupted(Enemy simEnemy)
		{
			SpineAnim instance = this.poolDwarfCorrupted.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			int num = simEnemy.genericRandom % SpineAnimDwarfCorrupted.skinNames.Length;
			instance.SetSkin(SpineAnimDwarfCorrupted.skinNames[num]);
			if (simEnemy.IsDead())
			{
				Spine.Animation animDie = SpineAnimDwarfCorrupted.animDie;
				float time = animDie.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animDie, time, false);
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				Spine.Animation animSpawnTranslate = SpineAnimDwarfCorrupted.animSpawnTranslate;
				float inStateTimeCounter = simEnemy.inStateTimeCounter;
				instance.Apply(animSpawnTranslate, inStateTimeCounter, true);
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawnDrop = SpineAnimDwarfCorrupted.animSpawnDrop;
				float time2 = simEnemy.GetAnimTimeRatioSpawnDrop() * animSpawnDrop.duration;
				instance.Apply(animSpawnDrop, time2, false);
			}
			else if (simEnemy.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimDwarfCorrupted.animIdle;
				float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter2, true);
			}
			else if (simEnemy.IsAttacking())
			{
				int num2 = simEnemy.numHits % SpineAnimDwarfCorrupted.animsAttack.Length;
				Spine.Animation animation = SpineAnimDwarfCorrupted.animsAttack[num2];
				float time3 = animation.duration * simEnemy.timeCounterAttackActive / simEnemy.data.durAttackActive;
				instance.Apply(animation, time3, false);
			}
			else
			{
				if (!simEnemy.IsStunned())
				{
					throw new NotImplementedException();
				}
				Spine.Animation animIdle2 = SpineAnimDwarfCorrupted.animIdle;
				float inStateTimeCounter3 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle2, inStateTimeCounter3, true);
			}
			instance.gameObject.transform.localPosition = localPosition;
			return instance;
		}

		private SpineAnim RenderHumanCorrupted(Enemy simEnemy)
		{
			SpineAnim instance = this.poolHumanCorrupted.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			int num = simEnemy.genericRandom % SpineAnimHumanCorrupted.skinNames.Length;
			instance.SetSkin(SpineAnimHumanCorrupted.skinNames[num]);
			if (simEnemy.IsDead())
			{
				Spine.Animation animDie = SpineAnimHumanCorrupted.animDie;
				float time = animDie.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animDie, time, false);
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				Spine.Animation animSpawnTranslate = SpineAnimHumanCorrupted.animSpawnTranslate;
				float inStateTimeCounter = simEnemy.inStateTimeCounter;
				instance.Apply(animSpawnTranslate, inStateTimeCounter, true);
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawnDrop = SpineAnimHumanCorrupted.animSpawnDrop;
				float time2 = simEnemy.GetAnimTimeRatioSpawnDrop() * animSpawnDrop.duration;
				instance.Apply(animSpawnDrop, time2, false);
			}
			else if (simEnemy.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimHumanCorrupted.animIdle;
				float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter2, true);
			}
			else if (simEnemy.IsAttacking())
			{
				int num2 = simEnemy.numHits % SpineAnimHumanCorrupted.animsAttack.Length;
				Spine.Animation animation = SpineAnimHumanCorrupted.animsAttack[num2];
				float time3 = animation.duration * simEnemy.timeCounterAttackActive / simEnemy.data.durAttackActive;
				instance.Apply(animation, time3, false);
			}
			else
			{
				if (!simEnemy.IsStunned())
				{
					throw new NotImplementedException();
				}
				Spine.Animation animIdle2 = SpineAnimHumanCorrupted.animIdle;
				float inStateTimeCounter3 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle2, inStateTimeCounter3, true);
			}
			instance.gameObject.transform.localPosition = localPosition;
			return instance;
		}

		private SpineAnim RenderHumanSemiCorrupted(Enemy simEnemy)
		{
			SpineAnim instance = this.poolHumanSemiCorrupted.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			int num = simEnemy.genericRandom % SpineAnimHumanSemiCorrupted.skinNames.Length;
			instance.SetSkin(SpineAnimHumanSemiCorrupted.skinNames[num]);
			if (simEnemy.IsDead())
			{
				Spine.Animation animDie = SpineAnimHumanSemiCorrupted.animDie;
				float time = animDie.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animDie, time, false);
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				Spine.Animation animSpawnTranslate = SpineAnimHumanSemiCorrupted.animSpawnTranslate;
				float inStateTimeCounter = simEnemy.inStateTimeCounter;
				instance.Apply(animSpawnTranslate, inStateTimeCounter, true);
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawnDrop = SpineAnimHumanSemiCorrupted.animSpawnDrop;
				float time2 = simEnemy.GetAnimTimeRatioSpawnDrop() * animSpawnDrop.duration;
				instance.Apply(animSpawnDrop, time2, false);
			}
			else if (simEnemy.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimHumanSemiCorrupted.animIdle;
				float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter2, true);
			}
			else if (simEnemy.IsAttacking())
			{
				int num2 = simEnemy.numHits % SpineAnimHumanSemiCorrupted.animsAttack.Length;
				Spine.Animation animation = SpineAnimHumanSemiCorrupted.animsAttack[num2];
				float time3 = animation.duration * simEnemy.timeCounterAttackActive / simEnemy.data.durAttackActive;
				instance.Apply(animation, time3, false);
			}
			else
			{
				if (!simEnemy.IsStunned())
				{
					throw new NotImplementedException();
				}
				Spine.Animation animIdle2 = SpineAnimHumanSemiCorrupted.animIdle;
				float inStateTimeCounter3 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle2, inStateTimeCounter3, true);
			}
			instance.gameObject.transform.localPosition = localPosition;
			return instance;
		}

		private SpineAnim RenderChest(Enemy simEnemy)
		{
			SpineAnim instance = this.poolChest.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			instance.SetSkin(SpineAnimChest.GetSkinName(simEnemy.world.currentSim));
			if (simEnemy.IsDead())
			{
				Spine.Animation animDie = SpineAnimChest.animDie;
				float time = animDie.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animDie, time, false);
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				Spine.Animation animSpawnTranslate = SpineAnimChest.animSpawnTranslate;
				float inStateTimeCounter = simEnemy.inStateTimeCounter;
				instance.Apply(animSpawnTranslate, inStateTimeCounter, true);
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawnDrop = SpineAnimChest.animSpawnDrop;
				float time2 = simEnemy.GetAnimTimeRatioSpawnDrop() * animSpawnDrop.duration;
				instance.Apply(animSpawnDrop, time2, false);
			}
			else
			{
				if (!simEnemy.IsIdle() && !simEnemy.IsAttacking() && !simEnemy.IsStunned())
				{
					throw new NotImplementedException();
				}
				if (simEnemy.lastDamage != null && simEnemy.lastDamage.time < 0.2f)
				{
					int num = simEnemy.pastDamages.Count % SpineAnimChest.animsHit.Length;
					Spine.Animation animation = SpineAnimChest.animsHit[num];
					float time3 = simEnemy.lastDamage.time;
					float time4 = animation.duration * time3 / 0.2f;
					instance.Apply(animation, time4, false);
				}
				else
				{
					Spine.Animation animIdle = SpineAnimChest.animIdle;
					float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
					instance.Apply(animIdle, inStateTimeCounter2, true);
				}
			}
			instance.gameObject.transform.localPosition = localPosition;
			return instance;
		}

		private SpineAnim RenderElfSemiCorrupted(Enemy simEnemy)
		{
			SpineAnim instance = this.poolElfSemiCorrupted.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			int num = simEnemy.genericRandom % SpineAnimElfSemiCorrupted.skinNames.Length;
			instance.SetSkin(SpineAnimElfSemiCorrupted.skinNames[num]);
			if (simEnemy.IsDead())
			{
				Spine.Animation animDie = SpineAnimElfSemiCorrupted.animDie;
				float time = animDie.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animDie, time, false);
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				Spine.Animation animSpawnTranslate = SpineAnimElfSemiCorrupted.animSpawnTranslate;
				float inStateTimeCounter = simEnemy.inStateTimeCounter;
				instance.Apply(animSpawnTranslate, inStateTimeCounter, true);
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawnDrop = SpineAnimElfSemiCorrupted.animSpawnDrop;
				float time2 = simEnemy.GetAnimTimeRatioSpawnDrop() * animSpawnDrop.duration;
				instance.Apply(animSpawnDrop, time2, false);
			}
			else if (simEnemy.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimElfSemiCorrupted.animIdle;
				float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter2, true);
			}
			else if (simEnemy.IsAttacking())
			{
				int num2 = simEnemy.numHits % SpineAnimElfSemiCorrupted.animsAttack.Length;
				Spine.Animation animation = SpineAnimElfSemiCorrupted.animsAttack[num2];
				float time3 = animation.duration * simEnemy.timeCounterAttackActive / simEnemy.data.durAttackActive;
				instance.Apply(animation, time3, false);
			}
			else
			{
				if (!simEnemy.IsStunned())
				{
					throw new NotImplementedException();
				}
				Spine.Animation animIdle2 = SpineAnimElfSemiCorrupted.animIdle;
				float inStateTimeCounter3 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle2, inStateTimeCounter3, true);
			}
			instance.gameObject.transform.localPosition = localPosition;
			return instance;
		}

		private SpineAnim RenderElfCorrupted(Enemy simEnemy)
		{
			SpineAnim instance = this.poolElfCorrupted.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			int num = simEnemy.genericRandom % SpineAnimElfCorrupted.skinNames.Length;
			instance.SetSkin(SpineAnimElfCorrupted.skinNames[num]);
			if (simEnemy.IsDead())
			{
				Spine.Animation animDie = SpineAnimElfCorrupted.animDie;
				float time = animDie.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animDie, time, false);
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				Spine.Animation animSpawnTranslate = SpineAnimElfCorrupted.animSpawnTranslate;
				float inStateTimeCounter = simEnemy.inStateTimeCounter;
				instance.Apply(animSpawnTranslate, inStateTimeCounter, true);
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawnDrop = SpineAnimElfCorrupted.animSpawnDrop;
				float time2 = simEnemy.GetAnimTimeRatioSpawnDrop() * animSpawnDrop.duration;
				instance.Apply(animSpawnDrop, time2, false);
			}
			else if (simEnemy.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimElfCorrupted.animIdle;
				float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter2, true);
			}
			else if (simEnemy.IsAttacking())
			{
				int num2 = simEnemy.numHits % SpineAnimElfCorrupted.animsAttack.Length;
				Spine.Animation animation = SpineAnimElfCorrupted.animsAttack[num2];
				float time3 = animation.duration * simEnemy.timeCounterAttackActive / simEnemy.data.durAttackActive;
				instance.Apply(animation, time3, false);
			}
			else
			{
				if (!simEnemy.IsStunned())
				{
					throw new NotImplementedException();
				}
				Spine.Animation animIdle2 = SpineAnimElfCorrupted.animIdle;
				float inStateTimeCounter3 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle2, inStateTimeCounter3, true);
			}
			instance.gameObject.transform.localPosition = localPosition;
			return instance;
		}

		private SpineAnim RenderBoss(Enemy simEnemy, float scale)
		{
			SpineAnim instance = this.poolBoss.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			if (simEnemy.IsDead())
			{
				Spine.Animation animDie = SpineAnimBoss.animDie;
				float time = animDie.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animDie, time, false);
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawn = SpineAnimBoss.animSpawn;
				float time2 = simEnemy.GetAnimTimeRatioSpawnDrop() * animSpawn.duration;
				instance.Apply(animSpawn, time2, false);
			}
			else if (simEnemy.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimBoss.animIdle;
				float inStateTimeCounter = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter, true);
			}
			else if (simEnemy.IsAttacking())
			{
				Spine.Animation animAttackProjectile = SpineAnimBoss.animAttackProjectile;
				float time3 = animAttackProjectile.duration * simEnemy.timeCounterAttackActive / simEnemy.data.durAttackActive;
				instance.Apply(animAttackProjectile, time3, false);
			}
			else if (simEnemy.IsStunned())
			{
				Spine.Animation animIdle2 = SpineAnimBoss.animIdle;
				float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle2, inStateTimeCounter2, true);
			}
			else
			{
				if (!simEnemy.IsLeaving())
				{
					throw new NotImplementedException();
				}
				Spine.Animation animLeave = SpineAnimBoss.animLeave;
				float time4 = animLeave.duration * simEnemy.inStateTimeCounter / simEnemy.data.durLeave;
				instance.Apply(animLeave, time4, false);
			}
			instance.gameObject.transform.localPosition = localPosition;
			instance.gameObject.transform.localScale = new Vector3(simEnemy.data.dataBase.scale * scale, simEnemy.data.dataBase.scale * scale, 1f);
			return instance;
		}

		private SpineAnim RenderBossElf(Enemy simEnemy)
		{
			SpineAnim instance = this.poolBossElf.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			if (simEnemy.IsDead())
			{
				Spine.Animation animDie = SpineAnimBossElf.animDie;
				float time = animDie.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animDie, time, false);
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawn = SpineAnimBossElf.animSpawn;
				float time2 = simEnemy.GetAnimTimeRatioSpawnDrop() * animSpawn.duration;
				instance.Apply(animSpawn, time2, false);
			}
			else if (simEnemy.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimBossElf.animIdle;
				float inStateTimeCounter = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter, true);
			}
			else if (simEnemy.IsAttacking())
			{
				int num = simEnemy.numHits % SpineAnimBossElf.animsAttack.Length;
				Spine.Animation animation = SpineAnimBossElf.animsAttack[num];
				float time3 = animation.duration * simEnemy.timeCounterAttackActive / simEnemy.data.durAttackActive;
				instance.Apply(animation, time3, false);
			}
			else if (simEnemy.IsStunned())
			{
				Spine.Animation animIdle2 = SpineAnimBossElf.animIdle;
				float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle2, inStateTimeCounter2, true);
			}
			else
			{
				if (!simEnemy.IsLeaving())
				{
					throw new NotImplementedException();
				}
				Spine.Animation animLeave = SpineAnimBossElf.animLeave;
				float time4 = animLeave.duration * simEnemy.inStateTimeCounter / simEnemy.data.durLeave;
				instance.Apply(animLeave, time4, false);
			}
			instance.gameObject.transform.localScale = new Vector3(simEnemy.data.dataBase.scale, simEnemy.data.dataBase.scale, 1f);
			instance.gameObject.transform.localPosition = localPosition;
			return instance;
		}

		private SpineAnim RenderBossHuman(Enemy simEnemy)
		{
			SpineAnim instance = this.poolBossHuman.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			instance.SetSkin(SpineAnimBossHuman.GetSkinName(simEnemy.world.currentSim));
			if (simEnemy.IsDead())
			{
				Spine.Animation animDie = SpineAnimBossHuman.animDie;
				float time = animDie.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animDie, time, false);
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawn = SpineAnimBossHuman.animSpawn;
				float time2 = simEnemy.GetAnimTimeRatioSpawnDrop() * animSpawn.duration;
				instance.Apply(animSpawn, time2, false);
			}
			else if (simEnemy.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimBossHuman.animIdle;
				float inStateTimeCounter = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter, true);
			}
			else if (simEnemy.IsAttacking())
			{
				int num = simEnemy.numHits % SpineAnimBossHuman.animsAttack.Length;
				Spine.Animation animation = SpineAnimBossHuman.animsAttack[num];
				float time3 = animation.duration * simEnemy.timeCounterAttackActive / simEnemy.data.durAttackActive;
				instance.Apply(animation, time3, false);
			}
			else if (simEnemy.IsStunned())
			{
				Spine.Animation animIdle2 = SpineAnimBossHuman.animIdle;
				float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle2, inStateTimeCounter2, true);
			}
			else
			{
				if (!simEnemy.IsLeaving())
				{
					throw new NotImplementedException();
				}
				Spine.Animation animLeave = SpineAnimBossHuman.animLeave;
				float time4 = animLeave.duration * simEnemy.inStateTimeCounter / simEnemy.data.durLeave;
				instance.Apply(animLeave, time4, false);
			}
			instance.gameObject.transform.localScale = new Vector3(simEnemy.data.dataBase.scale, simEnemy.data.dataBase.scale, 1f);
			instance.gameObject.transform.localPosition = localPosition;
			return instance;
		}

		private SpineAnim RenderBossDwarf(Enemy simEnemy)
		{
			SpineAnim instance = this.poolBossDwarf.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			if (simEnemy.IsDead())
			{
				Spine.Animation animDie = SpineAnimBossDwarf.animDie;
				float time = animDie.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animDie, time, false);
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawn = SpineAnimBossDwarf.animSpawn;
				float time2 = simEnemy.GetAnimTimeRatioSpawnDrop() * animSpawn.duration;
				instance.Apply(animSpawn, time2, false);
			}
			else if (simEnemy.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimBossDwarf.animIdle;
				float inStateTimeCounter = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter, true);
			}
			else if (simEnemy.IsAttacking())
			{
				int num = simEnemy.numHits % SpineAnimBossDwarf.animsAttack.Length;
				Spine.Animation animation = SpineAnimBossDwarf.animsAttack[num];
				float time3 = animation.duration * simEnemy.timeCounterAttackActive / simEnemy.data.durAttackActive;
				instance.Apply(animation, time3, false);
			}
			else if (simEnemy.IsStunned())
			{
				Spine.Animation animIdle2 = SpineAnimBossDwarf.animIdle;
				float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle2, inStateTimeCounter2, true);
			}
			else
			{
				if (!simEnemy.IsLeaving())
				{
					throw new NotImplementedException();
				}
				Spine.Animation animLeave = SpineAnimBossDwarf.animLeave;
				float time4 = animLeave.duration * simEnemy.inStateTimeCounter / simEnemy.data.durLeave;
				instance.Apply(animLeave, time4, false);
			}
			instance.gameObject.transform.localScale = new Vector3(simEnemy.data.dataBase.scale, simEnemy.data.dataBase.scale, 1f);
			instance.gameObject.transform.localPosition = localPosition;
			return instance;
		}

		private SpineAnim RenderBossMangolies(Enemy simEnemy)
		{
			SpineAnim instance = this.poolBossMangolies.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			if (simEnemy.IsDead())
			{
				Spine.Animation animDie = SpineAnimBossMangolies.animDie;
				float time = animDie.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animDie, time, false);
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawn = SpineAnimBossMangolies.animSpawn;
				float time2 = simEnemy.GetAnimTimeRatioSpawnDrop() * animSpawn.duration;
				instance.Apply(animSpawn, time2, false);
			}
			else if (simEnemy.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimBossMangolies.animIdle;
				float inStateTimeCounter = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter, true);
			}
			else if (simEnemy.IsAttacking())
			{
				int num = simEnemy.numHits % SpineAnimBossMangolies.animsAttack.Length;
				Spine.Animation animation = SpineAnimBossMangolies.animsAttack[num];
				float time3 = animation.duration * simEnemy.timeCounterAttackActive / simEnemy.data.durAttackActive;
				instance.Apply(animation, time3, false);
			}
			else if (simEnemy.IsStunned())
			{
				Spine.Animation animIdle2 = SpineAnimBossMangolies.animIdle;
				float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle2, inStateTimeCounter2, true);
			}
			else
			{
				if (!simEnemy.IsLeaving())
				{
					throw new NotImplementedException();
				}
				Spine.Animation animLeave = SpineAnimBossMangolies.animLeave;
				float time4 = animLeave.duration * simEnemy.inStateTimeCounter / simEnemy.data.durLeave;
				instance.Apply(animLeave, time4, false);
			}
			instance.gameObject.transform.localScale = new Vector3(simEnemy.data.dataBase.scale, simEnemy.data.dataBase.scale, 1f);
			instance.gameObject.transform.localPosition = localPosition;
			return instance;
		}

		private SpineAnim RenderBossWiseSnake(Enemy simEnemy, Challenge challenge)
		{
			SpineAnim instance = this.poolBossWiseSnake.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			if (simEnemy.IsDead())
			{
				ChallengeRift challengeRift = challenge as ChallengeRift;
				bool flag = challengeRift.totWave >= challengeRift.targetTotWaveNo;
				Spine.Animation animation = (!flag) ? SpineAnimBossWiseSnake.animHurt : SpineAnimBossWiseSnake.animDie;
				float time = animation.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animation, time, false);
				WiseSnakeInvulnerabilityVisualEffect instance2 = this.poolWiseSnakeInvulnerabilityEffect.GetInstance();
				if (simEnemy.inStateTimeCounter >= simEnemy.data.dataBase.deathEffectTime)
				{
					for (int i = 0; i < instance2.elements.Length; i++)
					{
						if (simEnemy.inStateTimeCounter - simEnemy.data.dataBase.deathEffectTime <= 1f)
						{
							if (!instance2.particleAnimationComponents[i].gameObject.activeSelf)
							{
								instance2.particleAnimationComponents[i].gameObject.SetActive(true);
							}
							instance2.particleAnimations[i].SetSkin("common");
							instance2.particleAnimations[i].Apply(SpineAnimEnemyDeath.anim, simEnemy.inStateTimeCounter - simEnemy.data.dataBase.deathEffectTime, false);
						}
						else if (instance2.particleAnimationComponents[i].gameObject.activeSelf)
						{
							instance2.particleAnimationComponents[i].gameObject.SetActive(false);
						}
						if (instance2.elements[i].enabled)
						{
							instance2.elements[i].enabled = false;
						}
					}
				}
				else
				{
					this.UpdateWiseSnakeVisualEffectSegments(simEnemy, instance2);
				}
				instance2.cachedTransform.Rotate(Vector3.forward, 10f * simEnemy.invulnerabilityVisualEffect.rotationTime);
				instance2.cachedTransform.localPosition = localPosition;
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawn = SpineAnimBossWiseSnake.animSpawn;
				float time2 = simEnemy.GetAnimTimeRatioSpawnDrop() * animSpawn.duration;
				instance.Apply(animSpawn, time2, false);
			}
			else
			{
				WiseSnakeInvulnerabilityVisualEffect instance3 = this.poolWiseSnakeInvulnerabilityEffect.GetInstance();
				this.UpdateWiseSnakeVisualEffectSegments(simEnemy, instance3);
				instance3.cachedTransform.Rotate(Vector3.forward, 10f * simEnemy.invulnerabilityVisualEffect.rotationTime);
				instance3.cachedTransform.localPosition = localPosition;
				if (simEnemy.IsIdle())
				{
					Spine.Animation animIdle = SpineAnimBossWiseSnake.animIdle;
					float inStateTimeCounter = simEnemy.inStateTimeCounter;
					instance.Apply(animIdle, inStateTimeCounter, true);
				}
				else if (simEnemy.IsAttacking())
				{
					int num = simEnemy.numHits % SpineAnimBossWiseSnake.animsAttack.Length;
					Spine.Animation animation2 = SpineAnimBossWiseSnake.animsAttack[num];
					float time3 = animation2.duration * simEnemy.timeCounterAttackActive / simEnemy.data.durAttackActive;
					instance.Apply(animation2, time3, false);
				}
				else if (simEnemy.IsStunned())
				{
					Spine.Animation animIdle2 = SpineAnimBossWiseSnake.animIdle;
					float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
					instance.Apply(animIdle2, inStateTimeCounter2, true);
				}
				else
				{
					if (!simEnemy.IsSpawningMinions())
					{
						throw new NotImplementedException();
					}
					Spine.Animation animSummon = SpineAnimBossWiseSnake.animSummon;
					float inStateTimeCounter3 = simEnemy.inStateTimeCounter;
					instance.Apply(animSummon, inStateTimeCounter3, true);
				}
			}
			instance.gameObject.transform.localScale = new Vector3(simEnemy.data.dataBase.scale, simEnemy.data.dataBase.scale, 1f);
			instance.gameObject.transform.localPosition = localPosition;
			return instance;
		}

		private void UpdateWiseSnakeVisualEffectSegments(Enemy simEnemy, WiseSnakeInvulnerabilityVisualEffect effect)
		{
			for (int i = 0; i < effect.elements.Length; i++)
			{
				float num = simEnemy.invulnerabilityVisualEffect.minionsDeathTime[i];
				effect.elements[i].enabled = (num >= 0f);
				if (num <= 1f)
				{
					effect.elements[i].color = WiseSnakeInvulnerabilityVisualEffect.ActiveColor * this.cachedBlackCurtainColor;
					effect.particleAnimationComponents[i].gameObject.SetActive(false);
				}
				else if (num >= 1.6f)
				{
					effect.particleAnimationComponents[i].gameObject.SetActive(false);
					effect.elements[i].color = WiseSnakeInvulnerabilityVisualEffect.DeactiveColor * this.cachedBlackCurtainColor;
				}
				else
				{
					effect.elements[i].color = Color.Lerp(WiseSnakeInvulnerabilityVisualEffect.ActiveColor, WiseSnakeInvulnerabilityVisualEffect.DeactiveColor, (num - 1f) / 0.6f) * this.cachedBlackCurtainColor;
					if (num >= 1f)
					{
						if (!effect.particleAnimationComponents[i].gameObject.activeSelf)
						{
							effect.particleAnimationComponents[i].gameObject.SetActive(true);
						}
						effect.particleAnimations[i].SetSkin("snakes");
						effect.particleAnimations[i].Apply(SpineAnimEnemyDeath.anim, num - 1f, false);
						effect.particleAnimations[i].SetColor(this.cachedBlackCurtainColor);
					}
				}
			}
		}

		private SpineAnim RenderSnake(Enemy simEnemy)
		{
			SpineAnim instance = this.poolSnake.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			int num = simEnemy.genericRandom % SpineAnimSnake.skinNames.Length;
			instance.SetSkin(SpineAnimSnake.skinNames[num]);
			if (simEnemy.IsDead())
			{
				Spine.Animation animDie = SpineAnimSnake.animDie;
				float time = animDie.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animDie, time, false);
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawn = SpineAnimSnake.animSpawn;
				float time2 = simEnemy.GetAnimTimeRatioSpawnDrop() * animSpawn.duration;
				instance.Apply(animSpawn, time2, false);
			}
			else if (simEnemy.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimSnake.animIdle;
				float inStateTimeCounter = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter, true);
			}
			else if (simEnemy.IsAttacking())
			{
				int num2 = simEnemy.numHits % SpineAnimSnake.animsAttack.Length;
				Spine.Animation animation = SpineAnimSnake.animsAttack[num2];
				float time3 = animation.duration * simEnemy.timeCounterAttackActive / simEnemy.data.durAttackActive;
				instance.Apply(animation, time3, false);
			}
			else
			{
				if (!simEnemy.IsStunned())
				{
					throw new NotImplementedException();
				}
				Spine.Animation animIdle2 = SpineAnimSnake.animIdle;
				float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle2, inStateTimeCounter2, true);
			}
			instance.gameObject.transform.localScale = new Vector3(simEnemy.data.dataBase.scale, simEnemy.data.dataBase.scale, 1f);
			instance.gameObject.transform.localPosition = localPosition;
			return instance;
		}

		private SpineAnim RenderBossSnowman(Enemy simEnemy)
		{
			SpineAnim instance = this.poolBossChristmas.GetInstance();
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEnemy.pos);
			if (simEnemy.IsDead())
			{
				Spine.Animation animDie = SpineAnimBossChristmas.animDie;
				float time = animDie.duration * simEnemy.inStateTimeCounter / simEnemy.GetDurCorpse();
				instance.Apply(animDie, time, false);
			}
			else if (simEnemy.IsSpawnNonexisting())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnTranslating())
			{
				localPosition.x += 3f;
			}
			else if (simEnemy.IsSpawnDropping())
			{
				Spine.Animation animSpawn = SpineAnimBossChristmas.animSpawn;
				float time2 = simEnemy.GetAnimTimeRatioSpawnDrop() * animSpawn.duration;
				instance.Apply(animSpawn, time2, false);
			}
			else if (simEnemy.IsIdle())
			{
				Spine.Animation animIdle = SpineAnimBossChristmas.animIdle;
				float inStateTimeCounter = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle, inStateTimeCounter, true);
			}
			else if (simEnemy.IsAttacking())
			{
				int num = simEnemy.numHits % SpineAnimBossChristmas.animsAttack.Length;
				Spine.Animation animation = SpineAnimBossChristmas.animsAttack[num];
				float time3 = animation.duration * simEnemy.timeCounterAttackActive / simEnemy.data.durAttackActive;
				instance.Apply(animation, time3, false);
			}
			else if (simEnemy.IsStunned())
			{
				Spine.Animation animIdle2 = SpineAnimBossChristmas.animIdle;
				float inStateTimeCounter2 = simEnemy.inStateTimeCounter;
				instance.Apply(animIdle2, inStateTimeCounter2, true);
			}
			else
			{
				if (!simEnemy.IsLeaving())
				{
					throw new NotImplementedException();
				}
				Spine.Animation animLeave = SpineAnimBossChristmas.animLeave;
				float time4 = animLeave.duration * simEnemy.inStateTimeCounter / simEnemy.data.durLeave;
				instance.Apply(animLeave, time4, false);
			}
			instance.gameObject.transform.localScale = new Vector3(simEnemy.data.dataBase.scale, simEnemy.data.dataBase.scale, 1f);
			instance.gameObject.transform.localPosition = localPosition;
			return instance;
		}

		private void Render(Projectile simProjectile)
		{
			if (this.projectilePools.ContainsKey(simProjectile.type))
			{
				RenderPool<GameObject> renderPool = this.projectilePools[simProjectile.type];
				GameObject instance = renderPool.GetInstance();
				Vector3 vector = GameMath.ConvertToScreenSpace(simProjectile.GetDir());
				float num = Mathf.Atan2(vector.y, vector.x) * 180f / 3.14159274f;
				Vector3 localPosition = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition.z += -0.1f;
				instance.transform.localPosition = localPosition;
				if (simProjectile.rotateSpeed > 0f)
				{
					num += simProjectile.timeOnAir * simProjectile.rotateSpeed;
				}
				instance.transform.localRotation = Quaternion.Euler(0f, 0f, num);
			}
			else if (this.smartProjectilePools.ContainsKey(simProjectile.type))
			{
				RenderPoolSmartProjectile renderPoolSmartProjectile = this.smartProjectilePools[simProjectile.type];
				SmartProjectileRenderer instance2 = renderPoolSmartProjectile.GetInstance();
				Vector3 localPosition2 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition2.z += -0.1f;
				instance2.gameObject.transform.localPosition = localPosition2;
				float z = simProjectile.GetFlyRatio() * 360f * instance2.spinSpeed;
				instance2.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, z);
				instance2.SetColor(this.cachedBlackCurtainColor);
				instance2.SetSkin(simProjectile.visualVariation);
			}
			else if (simProjectile.type == Projectile.Type.REVERSED_EXCALIBUR_MUD)
			{
				SmartProjectileRenderer instance3 = this.poolSmartProjectileHiltExcalibur.GetInstance();
				Vector3 localPosition3 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition3.z += -0.1f;
				instance3.gameObject.transform.localPosition = localPosition3;
				Spine.Animation anim = SpineAnimProjectileHiltExcalibur.anim;
				float z2 = simProjectile.GetFlyRatio() * 360f * instance3.spinSpeed;
				float time = simProjectile.GetFlyRatio() * anim.duration * 3f;
				instance3.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, z2);
				instance3.SetSkin(simProjectile.visualVariation);
				instance3.SetColor(this.cachedBlackCurtainColor);
				instance3.Apply(anim, time, true);
			}
			else if (simProjectile.type == Projectile.Type.BOMBERMAN_FIREWORK)
			{
				SmartProjectileRenderer instance4 = this.poolProjectileBombermanFirework.GetInstance();
				Vector3 localPosition4 = GameMath.ConvertToScreenSpace(simProjectile.GetPosEnd());
				localPosition4.z += -0.1f;
				instance4.gameObject.transform.localPosition = localPosition4;
				Spine.Animation anim2 = SpineAnimProjectileBombermanFirework.anim;
				float time2 = simProjectile.GetFlyRatio() * anim2.duration;
				instance4.SetSkin(simProjectile.visualVariation);
				instance4.SetColor(this.cachedBlackCurtainColor);
				instance4.Apply(anim2, time2, false);
			}
			else if (simProjectile.type == Projectile.Type.SHEELA)
			{
				Hero hero = simProjectile.by as Hero;
				int numHeroSkins = hero.world.currentSim.GetNumHeroSkins(hero.GetId());
				SmartProjectileRenderer instance5 = this.poolSmartProjectileSheela.GetInstance();
				Vector3 localPosition5 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition5.z += -0.1f;
				instance5.gameObject.transform.localPosition = localPosition5;
				float z3 = simProjectile.GetFlyRatio() * 360f * 10f;
				instance5.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, z3);
				if (hero.IsUsingTempWeapon())
				{
					instance5.SetSkin(simProjectile.visualVariation + numHeroSkins);
				}
				else
				{
					instance5.SetSkin(simProjectile.visualVariation);
				}
				instance5.SetColor(this.cachedBlackCurtainColor);
			}
			else if (simProjectile.type == Projectile.Type.CHARM_DAGGER)
			{
				SmartProjectileRenderer instance6 = this.poolSmartProjectileSheela.GetInstance();
				Vector3 localPosition6 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition6.z += -0.1f;
				instance6.gameObject.transform.localPosition = localPosition6;
				float z4 = simProjectile.GetFlyRatio() * 360f * 10f;
				instance6.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, z4);
				instance6.SetSkin(simProjectile.visualVariation);
				instance6.SetColor(this.cachedBlackCurtainColor);
			}
			else if (simProjectile.type == Projectile.Type.DWARF_CORRUPTED)
			{
				SpineAnim instance7 = this.poolProjectileDwarfCorrupted.GetInstance();
				Vector3 localPosition7 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition7.z += -0.1f;
				instance7.gameObject.transform.localPosition = localPosition7;
				float z5 = simProjectile.GetFlyRatio() * 360f * 3f;
				instance7.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, z5);
				Spine.Animation anim3 = SpineAnimProjectileDwarfCorrupted.anim;
				float time3 = 0f;
				int num2 = simProjectile.visualVariation % SpineAnimProjectileDwarfCorrupted.skinNames.Length;
				instance7.SetSkin(SpineAnimProjectileDwarfCorrupted.skinNames[num2]);
				instance7.SetColor(this.cachedBlackCurtainColor);
				instance7.Apply(anim3, time3, false);
			}
			else if (simProjectile.type == Projectile.Type.TOTEM_ICE_SHARD)
			{
				SpineAnim instance8 = this.poolProjectileTotemShard.GetInstance();
				Vector3 localPosition8 = GameMath.ConvertToScreenSpace(simProjectile.GetPosEnd());
				instance8.gameObject.transform.localPosition = localPosition8;
				Spine.Animation anim4 = SpineAnimProjectileTotemShard.anim;
				float time4 = simProjectile.GetFlyRatio() * anim4.duration;
				instance8.SetColor(this.cachedBlackCurtainColor);
				instance8.Apply(anim4, time4, false);
			}
			else if (simProjectile.type == Projectile.Type.MANGOLIES)
			{
				SpineAnim instance9 = this.poolProjectileMagolies.GetInstance();
				Vector3 localPosition9 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition9.z += -0.1f;
				instance9.gameObject.transform.localPosition = localPosition9;
				float z6 = simProjectile.GetFlyRatio() * 360f * 3f;
				instance9.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, z6);
				Spine.Animation anim5 = SpineAnimProjectileMagolies.anim;
				float time5 = 0f;
				instance9.SetColor(this.cachedBlackCurtainColor);
				instance9.Apply(anim5, time5, false);
			}
			else if (simProjectile.type == Projectile.Type.TOTEM_EARTH)
			{
				SpineAnim instance10 = this.poolProjectileTotemEarth.GetInstance();
				Vector3 localPosition10 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				instance10.gameObject.transform.localPosition = localPosition10;
				instance10.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				Spine.Animation anim6 = SpineAnimTotemEarth.anim;
				float time6 = simProjectile.GetFlyRatio() * anim6.duration;
				instance10.SetColor(this.cachedBlackCurtainColor);
				instance10.Apply(anim6, time6, true);
			}
			else if (simProjectile.type == Projectile.Type.TOTEM_EARTH_SMALL)
			{
				SpineAnim instance11 = this.poolProjectileTotemEarth.GetInstance();
				Vector3 localPosition11 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				instance11.gameObject.transform.localPosition = localPosition11;
				instance11.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
				Spine.Animation anim7 = SpineAnimTotemEarth.anim;
				float time7 = simProjectile.GetFlyRatio() * anim7.duration;
				instance11.SetColor(this.cachedBlackCurtainColor);
				instance11.Apply(anim7, time7, true);
			}
			else if (simProjectile.type == Projectile.Type.TOTEM_EARTH_STAR)
			{
				SpineAnim instance12 = this.poolProjectileTotemEarthStarfall.GetInstance();
				Vector3 localPosition12 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition12.z -= 0.1f;
				instance12.gameObject.transform.localRotation = Quaternion.identity;
				instance12.gameObject.transform.localPosition = localPosition12;
				Spine.Animation anim8 = SpineAnimTotemEarthStarfall.anim;
				float time8 = simProjectile.GetFlyRatio() * anim8.duration;
				instance12.SetColor(this.cachedBlackCurtainColor);
				instance12.Apply(anim8, time8, true);
			}
			else if (simProjectile.type == Projectile.Type.WARLOCK_SWARM)
			{
				SpineAnim instance13 = this.poolProjectileWarlockSwarm.GetInstance();
				Vector3 localPosition13 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition13.z += -0.1f;
				Spine.Animation loop = SpineAnimWarlockSwarm.loop;
				Vector3 vector2 = GameMath.ConvertToScreenSpace(simProjectile.GetDir());
				float z7 = Mathf.Atan2(vector2.y, vector2.x) * 180f / 3.14159274f;
				instance13.gameObject.transform.localPosition = localPosition13;
				instance13.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, z7);
				float time9 = loop.duration * simProjectile.GetFlyRatio() * 2f;
				instance13.SetColor(this.cachedBlackCurtainColor);
				instance13.Apply(loop, time9, true);
			}
			else if (simProjectile.type == Projectile.Type.WARLOCK_ATTACK)
			{
				SpineAnim instance14 = this.poolProjectileWarlockAttack.GetInstance();
				Vector3 localPosition14 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition14.z += -0.1f;
				Spine.Animation anim9 = SpineAnimWarlockAttack.anim;
				Hero hero2 = simProjectile.by as Hero;
				int numHeroSkins2 = hero2.world.currentSim.GetNumHeroSkins(hero2.GetId());
				int num3 = 0;
				if (simProjectile.projectileAlternativeIndex != -1)
				{
					num3 = simProjectile.projectileAlternativeIndex;
				}
				else if (hero2 != null)
				{
					int equippedSkinIndex = hero2.GetEquippedSkinIndex();
					hero2.GetNumHits();
					if ((hero2.GetNumHits() + 2) % 4 == 0)
					{
						num3 = equippedSkinIndex + numHeroSkins2;
					}
					else
					{
						num3 = equippedSkinIndex;
					}
				}
				instance14.SetSkin(num3 - 1);
				Vector3 vector3 = GameMath.ConvertToScreenSpace(simProjectile.GetDir());
				float z8 = Mathf.Atan2(vector3.y, vector3.x) * 180f / 3.14159274f;
				instance14.gameObject.transform.localPosition = localPosition14;
				instance14.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, z8);
				float time10 = anim9.duration * simProjectile.GetFlyRatio() * 2f;
				instance14.SetColor(this.cachedBlackCurtainColor);
				instance14.Apply(anim9, time10, true);
			}
			else if (simProjectile.type == Projectile.Type.APPLE_AID)
			{
				VariantSpriteRenderer instance15 = this.poolProjectileAppleAid.GetInstance();
				Vector3 vector4 = GameMath.ConvertToScreenSpace(simProjectile.GetDir());
				float z9 = Mathf.Atan2(vector4.y, vector4.x) * 180f / 3.14159274f;
				Vector3 localPosition15 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition15.z += -0.1f;
				instance15.transform.localPosition = localPosition15;
				instance15.transform.localRotation = Quaternion.Euler(0f, 0f, z9);
				instance15.SetSkinWithVariant(simProjectile.visualVariation - 1);
				instance15.spriteRenderer.color = this.cachedBlackCurtainColor;
			}
			else if (simProjectile.type == Projectile.Type.DEREK_BOOK)
			{
				SmartProjectileRenderer instance16 = this.poolSmartProjectileWendle.GetInstance();
				Vector3 vector5 = GameMath.ConvertToScreenSpace(simProjectile.GetDir());
				float z10 = Mathf.Atan2(vector5.y, vector5.x) * 180f / 3.14159274f;
				Vector3 localPosition16 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition16.z += -0.1f;
				instance16.transform.localPosition = localPosition16;
				instance16.transform.localRotation = Quaternion.Euler(0f, 0f, z10);
				instance16.SetSkin(simProjectile.visualVariation);
				float time11 = simProjectile.GetFlyRatio() * simProjectile.durFly;
				instance16.SetColor(this.cachedBlackCurtainColor);
				instance16.Apply(SpineAnimProjectileDerekMeteor.animBook, time11, true);
			}
			else if (simProjectile.type == Projectile.Type.QUICK_STUDY)
			{
				VariantSpriteRenderer instance17 = this.poolProjectileQuickStudy.GetInstance();
				Vector3 vector6 = GameMath.ConvertToScreenSpace(simProjectile.GetDir());
				float z11 = Mathf.Atan2(vector6.y, vector6.x) * 180f / 3.14159274f;
				Vector3 localPosition17 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition17.z += -0.1f;
				instance17.transform.localPosition = localPosition17;
				instance17.transform.localRotation = Quaternion.Euler(0f, 0f, z11);
				instance17.spriteRenderer.color = this.cachedBlackCurtainColor;
			}
			else if (simProjectile.type == Projectile.Type.DEREK_MAGIC_BALL)
			{
				VariantSpriteRenderer instance18 = this.poolProjectileDerekMagicBall.GetInstance();
				Vector3 vector7 = GameMath.ConvertToScreenSpace(simProjectile.GetDir());
				float z12 = Mathf.Atan2(vector7.y, vector7.x) * 180f / 3.14159274f;
				Vector3 localPosition18 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition18.z += -0.1f;
				instance18.transform.localPosition = localPosition18;
				instance18.transform.localRotation = Quaternion.Euler(0f, 0f, z12);
				if (simProjectile.visualVariation == 9)
				{
					instance18.SetSkin(1);
				}
				else
				{
					instance18.SetSkin(0);
				}
				instance18.spriteRenderer.color = this.cachedBlackCurtainColor;
			}
			else if (simProjectile.type == Projectile.Type.METEOR)
			{
				SpineAnim instance19 = this.poolProjectileDerekMeteor.GetInstance();
				Vector3 localPosition19 = GameMath.ConvertToScreenSpace(simProjectile.targetPosition);
				instance19.gameObject.transform.localPosition = localPosition19;
				instance19.gameObject.transform.localScale = Vector3.one * simProjectile.scale;
				Spine.Animation anim10 = SpineAnimProjectileDerekMeteor.anim;
				float time12 = simProjectile.GetFlyRatio() * anim10.duration;
				instance19.SetColor(this.cachedBlackCurtainColor);
				instance19.Apply(anim10, time12, false);
			}
			else if (simProjectile.type == Projectile.Type.SNAKE)
			{
				SpineAnim instance20 = this.poolProjectileSnake.GetInstance();
				Vector3 localPosition20 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition20.z += -0.1f;
				instance20.gameObject.transform.localPosition = localPosition20;
				instance20.gameObject.transform.right = -(simProjectile.target.pos - simProjectile.by.pos).normalized;
				Spine.Animation anim11 = SpineAnimProjectileSnake.anim;
				float time13 = simProjectile.GetFlyRatio() * simProjectile.durFly;
				instance20.SetColor(this.cachedBlackCurtainColor);
				instance20.Apply(anim11, time13, true);
			}
			else if (simProjectile.type == Projectile.Type.WISE_SNAKE)
			{
				GameObject instance21 = this.poolProjectileWiseSnake.GetInstance();
				Vector3 localPosition21 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition21.z += -0.1f;
				instance21.gameObject.transform.localPosition = localPosition21;
				instance21.gameObject.transform.right = -(simProjectile.target.pos - simProjectile.by.pos).normalized;
			}
			else if (simProjectile.type == Projectile.Type.BABU_SOUP)
			{
				SmartProjectileRenderer instance22 = this.poolSmartProjectileBabu.GetInstance();
				Vector3 localPosition22 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition22.z += -0.1f;
				Vector3 vector8 = GameMath.ConvertToScreenSpace(simProjectile.GetDir());
				float z13 = Mathf.Atan2(vector8.y, vector8.x) * 180f / 3.14159274f;
				instance22.gameObject.transform.localPosition = localPosition22;
				instance22.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, z13);
				instance22.SetSkin(simProjectile.visualVariation);
				Spine.Animation loop2 = SpineAnimBabuProjectile.loop;
				float time14 = simProjectile.GetFlyRatio() * simProjectile.durFly;
				instance22.SetColor(this.cachedBlackCurtainColor);
				instance22.Apply(loop2, time14, true);
			}
			else if (simProjectile.type == Projectile.Type.BABU_TEA_CUP)
			{
				VariantSpriteRenderer instance23 = this.poolProjectileBabuTeaCup.GetInstance();
				Vector3 localPosition23 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition23.z += -0.1f;
				Vector3 vector9 = GameMath.ConvertToScreenSpace(simProjectile.GetDir());
				float z14 = Mathf.Atan2(vector9.y, vector9.x) * 180f / 3.14159274f;
				instance23.gameObject.transform.localPosition = localPosition23;
				instance23.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, z14);
				instance23.SetSkinWithVariant(simProjectile.visualVariation);
				instance23.spriteRenderer.color = this.cachedBlackCurtainColor;
			}
			else if (simProjectile.type == Projectile.Type.CHRISTMAS_ORNAMENT)
			{
				SmartProjectileRenderer instance24 = this.poolSmartProjectileOrnamentDrop.GetInstance();
				Vector3 localPosition24 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition24.z += -0.1f;
				instance24.gameObject.transform.localPosition = localPosition24;
				instance24.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, simProjectile.rotateSpeed);
				instance24.SetSkin(simProjectile.visualVariation);
				Spine.Animation loop3 = SpineAnimProjectileOrnamentDrop.loop;
				float time15 = simProjectile.GetFlyRatio() * simProjectile.durFly;
				instance24.SetColor(this.cachedBlackCurtainColor);
				instance24.Apply(loop3, time15, true);
			}
			else
			{
				if (simProjectile.type != Projectile.Type.APPLE_BOMBARD)
				{
					throw new NotImplementedException();
				}
				VariantSpriteRenderer instance25 = this.poolProjectileAppleBombard.GetInstance();
				Vector3 localPosition25 = GameMath.ConvertToScreenSpace(simProjectile.GetPos());
				localPosition25.z += -0.1f;
				Vector3 vector10 = GameMath.ConvertToScreenSpace(simProjectile.GetDir());
				float z15 = Mathf.Atan2(vector10.y, vector10.x) * 180f / 3.14159274f;
				instance25.gameObject.transform.localPosition = localPosition25;
				instance25.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, z15);
				Hero hero3 = simProjectile.by as Hero;
				instance25.SetSkinWithVariant((hero3 == null) ? 0 : (hero3.GetEquippedSkinIndex() - 1));
				instance25.spriteRenderer.color = this.cachedBlackCurtainColor;
			}
		}

		private void Render(Drop simDrop, GameMode gameMode)
		{
			if (simDrop.state == Drop.State.NON_EXISTENCE || simDrop.state == Drop.State.COLLECTED)
			{
				return;
			}
			Vector3 vector = GameMath.ConvertToScreenSpace(simDrop.pos);
			if (simDrop.state == Drop.State.FLY_TO_INV)
			{
				vector.z += simDrop.zOffsetInventory;
			}
			SpriteRendererObject instance;
			if (simDrop is DropCurrency)
			{
				CurrencyType currencyType = (simDrop as DropCurrency).currencyType;
				if (currencyType == CurrencyType.GOLD)
				{
					RenderPoolSpriteObject[] array = (gameMode != GameMode.STANDARD) ? this.poolsDropGoldTri : this.poolsDropGold;
					RenderPoolSpriteObject[] array2;
					switch (gameMode)
					{
					case GameMode.STANDARD:
						array2 = this.poolsDropGold;
						goto IL_C6;
					case GameMode.CRUSADE:
						array2 = this.poolsDropGoldTri;
						goto IL_C6;
					case GameMode.RIFT:
						array2 = this.poolsDropGoldSqr;
						goto IL_C6;
					}
					array2 = this.poolsDropGold;
					IL_C6:
					instance = array2[simDrop.variation % array2.Length].GetInstance();
				}
				else if (currencyType == CurrencyType.MYTHSTONE)
				{
					instance = this.poolsDropMythtone[simDrop.variation % this.poolsDropMythtone.Length].GetInstance();
				}
				else if (currencyType == CurrencyType.GEM)
				{
					instance = this.poolsDropCredit[simDrop.variation % this.poolsDropCredit.Length].GetInstance();
				}
				else if (currencyType == CurrencyType.TOKEN)
				{
					instance = this.poolsDropToken[simDrop.variation % this.poolsDropToken.Length].GetInstance();
				}
				else if (currencyType == CurrencyType.SCRAP)
				{
					instance = this.poolsDropScrap[simDrop.variation % this.poolsDropScrap.Length].GetInstance();
				}
				else if (currencyType == CurrencyType.AEON)
				{
					instance = this.poolsDropAeon[simDrop.variation % this.poolsDropAeon.Length].GetInstance();
				}
				else if (currencyType == CurrencyType.CANDY)
				{
					instance = this.poolsDropCandy[simDrop.variation % this.poolsDropCandy.Length].GetInstance();
				}
				else
				{
					if (currencyType != CurrencyType.TRINKET_BOX)
					{
						throw new NotImplementedException();
					}
					instance = this.poolsDropTrinketBox[simDrop.variation % this.poolsDropTrinketBox.Length].GetInstance();
				}
			}
			else if (simDrop is DropPowerupNonCritDamage)
			{
				instance = this.poolDropPowerupCritChance.GetInstance();
				if (simDrop.state == Drop.State.FLY_TO_INV)
				{
					vector = GameMath.Lerp(vector, UiManager.POS_POWERUP_CRIT_CHANCE_FLY, simDrop.stateTime / 0.8f);
				}
			}
			else if (simDrop is DropPowerupCooldown)
			{
				instance = this.poolDropPowerupCooldown.GetInstance();
				if (simDrop.state == Drop.State.FLY_TO_INV)
				{
					vector = GameMath.Lerp(vector, UiManager.POS_POWERUP_COOLDOWN_FLY, simDrop.stateTime / 0.8f);
				}
			}
			else
			{
				if (!(simDrop is DropPowerupRevive))
				{
					throw new NotImplementedException();
				}
				instance = this.poolDropPowerupRevive.GetInstance();
				if (simDrop.state == Drop.State.FLY_TO_INV)
				{
					vector = GameMath.Lerp(vector, UiManager.POS_POWERUP_REVIVE_FLY, simDrop.stateTime / 0.8f);
				}
			}
			if (simDrop.uiState != UiState.NONE)
			{
				vector.z = -120f;
				instance.spriteRenderer.sortingOrder = 5;
				instance.transform.SetParent(this._uiSceneRenderers.transform);
			}
			else
			{
				instance.spriteRenderer.sortingOrder = 0;
			}
			instance.transform.localPosition = vector;
			instance.transform.localScale = Vector2.one * simDrop.scale;
			instance.transform.localEulerAngles = Vector3.forward * simDrop.rotation;
			instance.spriteRenderer.color = this.cachedBlackCurtainColor;
		}

		private float GetHackyJimBarLiftAmount(Unit simUnit)
		{
			if (simUnit.GetId() != "JIM")
			{
				return 0f;
			}
			Hero hero = (Hero)simUnit;
			if (!hero.IsUsingSkill())
			{
				return 0f;
			}
			if (!(hero.GetRunningSkillDataBase() is SkillDataBaseBittersweet))
			{
				return 0f;
			}
			float runningSkillTime = hero.GetRunningSkillTime();
			float activeSkillDuration = hero.GetActiveSkillDuration();
			float num = activeSkillDuration - runningSkillTime;
			if (runningSkillTime < 0.4f)
			{
				return 0f;
			}
			if (runningSkillTime < 0.55f)
			{
				return 0.12f * (runningSkillTime - 0.4f) / 0.15f;
			}
			if (num > 0.9f)
			{
				return 0.12f;
			}
			if (num > 0.75f)
			{
				return 0.12f * (num - 0.75f) / 0.149999976f;
			}
			return 0f;
		}

		private void PutHealthBar(UnitHealthy simUnit, bool isInFronOfBlackCurtain, float additionalScale = 1f)
		{
			if (!simUnit.IsAlive())
			{
				return;
			}
			GameObject instance = this.poolHealthBar.GetInstance();
			HealthBar component = instance.GetComponent<HealthBar>();
			component.SetScale((float)simUnit.GetHealthRatio());
			Vector3 vector = simUnit.pos;
			vector.y += simUnit.GetHeight() * additionalScale;
			vector.y += this.GetHackyJimBarLiftAmount(simUnit);
			vector = GameMath.ConvertToScreenSpace(vector);
			Hero hero = simUnit as Hero;
			if (hero != null)
			{
				vector.z = GameMath.ConvertToScreenSpaceZ(hero.pos.y) + 0.001f;
			}
			else
			{
				vector.z = -0.5f;
			}
			instance.transform.localPosition = vector;
			float x = simUnit.GetScaleHealthBar() * additionalScale;
			instance.transform.localScale = new Vector3(x, 1f, 1f);
			Color white = Color.white;
			PastDamage lastDamage = simUnit.lastDamage;
			if (lastDamage != null)
			{
				white.a = 0.85f - lastDamage.time * 4f;
			}
			else
			{
				white.a = 0f;
			}
			if (isInFronOfBlackCurtain)
			{
				component.SetColor(white, RenderManager.BLACK_CURTAIN_MIN_COLOR);
			}
			else
			{
				component.SetColor(white, this.cachedBlackCurtainColor);
			}
		}

		private void PutTimeBar(Unit simUnit, float timeScale, bool isInFrontOfBlackCurtain)
		{
			if (timeScale < 0f)
			{
				return;
			}
			if (simUnit is UnitHealthy && !((UnitHealthy)simUnit).IsAlive())
			{
				return;
			}
			GameObject instance = this.poolTimeBar.GetInstance();
			Scaler component = instance.GetComponent<Scaler>();
			component.SetScale(timeScale);
			component.scaled.transform.localPosition = new Vector3(-0.07375f, -0.003f, 0f);
			component.coloredBar.color = Scaler.colorTime;
			if (!isInFrontOfBlackCurtain)
			{
				component.coloredBar.color *= this.cachedBlackCurtainColor;
			}
			Vector3 vector = simUnit.pos;
			vector.y += simUnit.GetHeight();
			vector.y += -0.0125f;
			vector.y += this.GetHackyJimBarLiftAmount(simUnit);
			vector = GameMath.ConvertToScreenSpace(vector);
			Hero hero = simUnit as Hero;
			if (hero != null)
			{
				vector.z = GameMath.ConvertToScreenSpaceZ(hero.pos.y) + 0.002f;
			}
			else
			{
				vector.z = -0.4f;
			}
			instance.transform.localPosition = vector;
			float scaleHealthBar = simUnit.GetScaleHealthBar();
			instance.transform.localScale = new Vector3(scaleHealthBar, 1f, 1f);
		}

		private void PutShieldBar(UnitHealthy simUnit, bool isInFrontOfBlackCurtain)
		{
			if (simUnit.HasZeroShield())
			{
				return;
			}
			GameObject instance = this.poolTimeBar.GetInstance();
			Scaler component = instance.GetComponent<Scaler>();
			component.SetScale((float)simUnit.GetShieldRatio());
			component.coloredBar.color = Scaler.colorShield;
			if (!isInFrontOfBlackCurtain)
			{
				component.coloredBar.color *= this.cachedBlackCurtainColor;
			}
			component.scaled.transform.localPosition = new Vector3(-0.07375f, 0.003f, 0f);
			Vector3 vector = simUnit.pos;
			vector.y += simUnit.GetHeight();
			vector.y += this.GetHackyJimBarLiftAmount(simUnit);
			vector.y += 0.0125f;
			vector = GameMath.ConvertToScreenSpace(vector);
			Hero hero = simUnit as Hero;
			if (hero != null)
			{
				vector.z = GameMath.ConvertToScreenSpaceZ(hero.pos.y) + 0.0015f;
			}
			else
			{
				vector.z = -0.45f;
			}
			instance.transform.localPosition = vector;
		}

		private void RenderBuffs(UnitHealthy unit, float zOffset, World world, bool isInFrontOfBlackCurtain, float additionalScale = 1f)
		{
			float num = unit.GetScaleBuffVisual() * additionalScale;
			float num2;
			if (unit.IsAlive())
			{
				num2 = unit.buffTimeCounterAttackFast;
				if (num2 > -0.2f)
				{
					SpineAnim instance = this.poolBuffAttackFast.GetInstance();
					Vector3 localPosition;
					if (!this.buffEffectPositions.TryGetValue(unit.GetId(), out localPosition))
					{
						localPosition = GameMath.ConvertToScreenSpace(unit.pos);
					}
					localPosition.z = GameMath.ConvertToScreenSpaceZ(unit.pos.y) + 0.001f + zOffset;
					instance.gameObject.transform.localPosition = localPosition;
					float buffScale = this.GetBuffScale(num, num2, 0.3f, 0.2f);
					instance.gameObject.transform.localScale = new Vector3(buffScale, buffScale, 1f);
					Spine.Animation anim = SpineAnimBuffAttackFast.anim;
					instance.SetColor((!isInFrontOfBlackCurtain) ? this.cachedBlackCurtainColor : RenderManager.BLACK_CURTAIN_MIN_COLOR);
					instance.Apply(anim, num2 + 0.2f, true);
				}
				num2 = unit.buffTimeCounterAttackSlow;
				if (num2 > -SpineAnimBuffAttackSlow.DUR_FADE_OUT)
				{
					SpineAnim instance2 = this.poolBuffAttackSlow.GetInstance();
					Vector3 localPosition2;
					if (!this.buffEffectPositions.TryGetValue(unit.GetId(), out localPosition2))
					{
						localPosition2 = GameMath.ConvertToScreenSpace(unit.pos);
					}
					localPosition2.z = GameMath.ConvertToScreenSpaceZ(localPosition2.y) - 0.001f + zOffset;
					instance2.gameObject.transform.localPosition = localPosition2;
					instance2.gameObject.transform.localScale = new Vector3(num, num, 1f);
					instance2.SetColor((!isInFrontOfBlackCurtain) ? this.cachedBlackCurtainColor : RenderManager.BLACK_CURTAIN_MIN_COLOR);
					if (num2 > 0f && num2 < SpineAnimBuffAttackSlow.DUR_FADE_IN)
					{
						Spine.Animation start = SpineAnimBuffAttackSlow.start;
						instance2.Apply(start, num2, true);
					}
					else if (num2 < 0f)
					{
						Spine.Animation end = SpineAnimBuffAttackSlow.end;
						instance2.Apply(end, -num2, true);
					}
					else
					{
						Spine.Animation loop = SpineAnimBuffAttackSlow.loop;
						instance2.Apply(loop, num2, true);
					}
				}
				num2 = unit.buffTimeCounterCritChance;
				if (num2 > -0.2f)
				{
					SpineAnim instance3 = this.poolBuffCritChance.GetInstance();
					Vector3 localPosition3;
					if (!this.buffEffectPositions.TryGetValue(unit.GetId(), out localPosition3))
					{
						localPosition3 = GameMath.ConvertToScreenSpace(unit.pos);
					}
					localPosition3.y += unit.GetHeight() * 0.6f;
					localPosition3.z = GameMath.ConvertToScreenSpaceZ(unit.pos.y) - 0.001f + zOffset;
					instance3.gameObject.transform.localPosition = localPosition3;
					float buffScale2 = this.GetBuffScale(num, num2, 0.3f, 0.2f);
					instance3.gameObject.transform.localScale = new Vector3(buffScale2, buffScale2, 1f);
					instance3.SetColor(new Color(1f, 1f, 1f, 0.8f));
					Spine.Animation anim2 = SpineAnimBuffCritChance.anim;
					instance3.SetColor((!isInFrontOfBlackCurtain) ? this.cachedBlackCurtainColor : RenderManager.BLACK_CURTAIN_MIN_COLOR);
					instance3.Apply(anim2, num2 + 0.2f, true);
				}
				num2 = unit.buffTimeCounterDamageAdd;
				if (num2 > -0.2f)
				{
					SpineAnim instance4 = this.poolBuffDamageAdd.GetInstance();
					Vector3 localPosition4;
					if (!this.buffEffectPositions.TryGetValue(unit.GetId(), out localPosition4))
					{
						localPosition4 = GameMath.ConvertToScreenSpace(unit.pos);
					}
					localPosition4.z = GameMath.ConvertToScreenSpaceZ(unit.pos.y) - 0.001f + zOffset;
					instance4.gameObject.transform.localPosition = localPosition4;
					float buffScale3 = this.GetBuffScale(num, num2, 0.3f, 0.2f);
					instance4.gameObject.transform.localScale = new Vector3(buffScale3, buffScale3, 1f);
					Spine.Animation anim3 = SpineAnimBuffDamageAdd.anim;
					instance4.SetColor((!isInFrontOfBlackCurtain) ? this.cachedBlackCurtainColor : RenderManager.BLACK_CURTAIN_MIN_COLOR);
					instance4.Apply(anim3, num2 + 0.2f, true);
				}
				num2 = unit.buffTimeCounterDamageDec;
				if (num2 > -0.2f)
				{
					SpineAnim instance5 = this.poolBuffDamageDec.GetInstance();
					Vector3 localPosition5;
					if (!this.buffEffectPositions.TryGetValue(unit.GetId(), out localPosition5))
					{
						localPosition5 = GameMath.ConvertToScreenSpace(unit.pos);
					}
					localPosition5.z = GameMath.ConvertToScreenSpaceZ(unit.pos.y) - 0.001f + zOffset;
					instance5.gameObject.transform.localPosition = localPosition5;
					float buffScale4 = this.GetBuffScale(num, num2, 0.3f, 0.2f);
					instance5.gameObject.transform.localScale = new Vector3(buffScale4, buffScale4, 1f);
					Spine.Animation anim4 = SpineAnimBuffDamageDec.anim;
					instance5.SetColor((!isInFrontOfBlackCurtain) ? this.cachedBlackCurtainColor : RenderManager.BLACK_CURTAIN_MIN_COLOR);
					instance5.Apply(anim4, num2 + 0.2f, true);
				}
				num2 = unit.buffTimeCounterDefenseless;
				if (num2 > -SpineAnimBuffDefenseless.DUR_FADE_OUT)
				{
					SpineAnim instance6 = this.poolBuffDefenseless.GetInstance();
					Vector3 localPosition6;
					if (!this.buffEffectPositions.TryGetValue(unit.GetId(), out localPosition6))
					{
						localPosition6 = GameMath.ConvertToScreenSpace(unit.pos);
					}
					localPosition6.z = GameMath.ConvertToScreenSpaceZ(unit.pos.y) - 0.001f + zOffset;
					instance6.gameObject.transform.localPosition = localPosition6;
					instance6.gameObject.transform.localScale = new Vector3(num, num, 1f);
					instance6.SetColor((!isInFrontOfBlackCurtain) ? this.cachedBlackCurtainColor : RenderManager.BLACK_CURTAIN_MIN_COLOR);
					if (num2 > 0f && num2 < SpineAnimBuffDefenseless.DUR_FADE_IN)
					{
						Spine.Animation start2 = SpineAnimBuffDefenseless.start;
						instance6.Apply(start2, num2, true);
					}
					else if (num2 < 0f)
					{
						Spine.Animation end2 = SpineAnimBuffDefenseless.end;
						instance6.Apply(end2, -num2, true);
					}
					else
					{
						Spine.Animation loop2 = SpineAnimBuffDefenseless.loop;
						instance6.Apply(loop2, num2, true);
					}
				}
				num2 = unit.buffTimeCounterHeathRegen;
				if (num2 > -0.2f)
				{
					SpineAnim instance7 = this.poolBuffHealthRegen.GetInstance();
					Vector3 localPosition7;
					if (!this.buffEffectPositions.TryGetValue(unit.GetId(), out localPosition7))
					{
						localPosition7 = GameMath.ConvertToScreenSpace(unit.pos);
					}
					localPosition7.z = GameMath.ConvertToScreenSpaceZ(unit.pos.y) - 0.001f + zOffset;
					instance7.gameObject.transform.localPosition = localPosition7;
					float buffScale5 = this.GetBuffScale(num, num2, 0.3f, 0.2f);
					instance7.gameObject.transform.localScale = new Vector3(buffScale5, buffScale5, 1f);
					Spine.Animation anim5 = SpineAnimBuffHealthRegen.anim;
					instance7.SetColor((!isInFrontOfBlackCurtain) ? this.cachedBlackCurtainColor : RenderManager.BLACK_CURTAIN_MIN_COLOR);
					instance7.Apply(anim5, num2 + 0.2f, true);
				}
				num2 = unit.buffTimeCounterImmunity;
				if (num2 > -0.2f)
				{
					SpineAnim instance8 = this.poolBuffImmunity.GetInstance();
					Vector3 localPosition8;
					if (!this.buffEffectPositions.TryGetValue(unit.GetId(), out localPosition8))
					{
						localPosition8 = GameMath.ConvertToScreenSpace(unit.pos);
					}
					localPosition8.z = GameMath.ConvertToScreenSpaceZ(unit.pos.y) - 0.001f + zOffset;
					instance8.gameObject.transform.localPosition = localPosition8;
					float buffScale6 = this.GetBuffScale(num, num2, 0.3f, 0.2f);
					instance8.gameObject.transform.localScale = new Vector3(buffScale6, buffScale6, 1f);
					Spine.Animation anim6 = SpineAnimBuffImmunity.anim;
					instance8.SetColor((!isInFrontOfBlackCurtain) ? this.cachedBlackCurtainColor : RenderManager.BLACK_CURTAIN_MIN_COLOR);
					instance8.Apply(anim6, num2 + 0.2f, true);
				}
				num2 = unit.buffTimeCounterShield;
				if (num2 > -0.2f)
				{
					SpineAnim instance9 = this.poolBuffShield.GetInstance();
					Vector3 localPosition9;
					if (!this.buffEffectPositions.TryGetValue(unit.GetId(), out localPosition9))
					{
						localPosition9 = GameMath.ConvertToScreenSpace(unit.pos);
					}
					localPosition9.z = GameMath.ConvertToScreenSpaceZ(unit.pos.y) - 0.001f + zOffset;
					instance9.gameObject.transform.localPosition = localPosition9;
					float buffScale7 = this.GetBuffScale(num, num2, 0.3f, 0.2f);
					instance9.gameObject.transform.localScale = new Vector3(buffScale7, buffScale7, 1f);
					Spine.Animation anim7 = SpineAnimBuffShield.anim;
					instance9.SetColor((!isInFrontOfBlackCurtain) ? this.cachedBlackCurtainColor : RenderManager.BLACK_CURTAIN_MIN_COLOR);
					instance9.Apply(anim7, num2 + 0.2f, true);
				}
				num2 = unit.buffTimeCounterStun;
				if (unit.buffTimeCounterStun > -0.2f)
				{
					SpineAnim instance10 = this.poolBuffStun.GetInstance();
					Vector3 localPosition10;
					if (!this.buffEffectPositions.TryGetValue(unit.GetId(), out localPosition10))
					{
						localPosition10 = GameMath.ConvertToScreenSpace(unit.pos);
					}
					localPosition10.y += unit.GetHeight() * 0.85f;
					localPosition10.z = GameMath.ConvertToScreenSpaceZ(unit.pos.y) - 0.001f + zOffset;
					instance10.gameObject.transform.localPosition = localPosition10;
					float buffScale8 = this.GetBuffScale(num, num2, 0.3f, 0.2f);
					instance10.gameObject.transform.localScale = new Vector3(buffScale8, buffScale8, 1f);
					Spine.Animation anim8 = SpineAnimBuffStun.anim;
					instance10.SetColor((!isInFrontOfBlackCurtain) ? this.cachedBlackCurtainColor : RenderManager.BLACK_CURTAIN_MIN_COLOR);
					instance10.Apply(anim8, num2 + 0.2f, true);
				}
				num2 = unit.buffTimeCounterMiss;
				if (unit.buffTimeCounterMiss > -0.2f)
				{
					SpineAnim instance11 = this.poolBuffMiss.GetInstance();
					Vector3 localPosition11;
					if (!this.buffEffectPositions.TryGetValue(unit.GetId(), out localPosition11))
					{
						localPosition11 = GameMath.ConvertToScreenSpace(unit.pos);
					}
					localPosition11.y += unit.GetHeight() * 0.7f;
					localPosition11.z = GameMath.ConvertToScreenSpaceZ(unit.pos.y) - 0.001f + zOffset;
					instance11.gameObject.transform.localPosition = localPosition11;
					float buffScale9 = this.GetBuffScale(num, num2, 0.3f, 0.2f);
					instance11.gameObject.transform.localScale = new Vector3(buffScale9, buffScale9, 1f);
					Spine.Animation anim9 = SpineAnimBuffMiss.anim;
					instance11.SetColor((!isInFrontOfBlackCurtain) ? this.cachedBlackCurtainColor : RenderManager.BLACK_CURTAIN_MIN_COLOR);
					instance11.Apply(anim9, num2 + 0.2f, true);
				}
				num2 = unit.buffTimeCounterMark;
				if (unit.buffTimeCounterMark > -0.2f)
				{
					SpineAnim instance12 = this.poolBuffMark.GetInstance();
					Vector3 localPosition12;
					if (!this.buffEffectPositions.TryGetValue(unit.GetId(), out localPosition12))
					{
						localPosition12 = GameMath.ConvertToScreenSpace(unit.pos);
					}
					localPosition12.y += unit.GetHeight() * 0.85f;
					localPosition12.z = GameMath.ConvertToScreenSpaceZ(unit.pos.y) - 0.001f + zOffset;
					instance12.gameObject.transform.localPosition = localPosition12;
					float buffScale10 = this.GetBuffScale(num, num2, 0.3f, 0.2f);
					instance12.gameObject.transform.localScale = new Vector3(buffScale10, buffScale10, 1f);
					Spine.Animation anim10 = SpineAnimBuffMark.anim;
					instance12.SetColor((!isInFrontOfBlackCurtain) ? this.cachedBlackCurtainColor : RenderManager.BLACK_CURTAIN_MIN_COLOR);
					instance12.Apply(anim10, num2 + 0.2f, true);
				}
				num2 = unit.buffTimeCounterCritDamage;
				if (unit.buffTimeCounterCritDamage > -0.2f)
				{
					SpineAnim instance13 = this.poolBuffCritDamage.GetInstance();
					Vector3 localPosition13;
					if (!this.buffEffectPositions.TryGetValue(unit.GetId(), out localPosition13))
					{
						localPosition13 = GameMath.ConvertToScreenSpace(unit.pos);
					}
					localPosition13.y += unit.GetHeight() * 0.75f;
					localPosition13.z = GameMath.ConvertToScreenSpaceZ(unit.pos.y) - 0.001f + zOffset;
					instance13.gameObject.transform.localPosition = localPosition13;
					float buffScale11 = this.GetBuffScale(num, num2, 0.3f, 0.2f);
					instance13.gameObject.transform.localScale = new Vector3(buffScale11, buffScale11, 1f);
					Spine.Animation anim11 = SpineAnimBuffCritDamage.anim;
					instance13.SetColor((!isInFrontOfBlackCurtain) ? this.cachedBlackCurtainColor : RenderManager.BLACK_CURTAIN_MIN_COLOR);
					instance13.Apply(anim11, num2 + 0.2f, true);
				}
				num2 = unit.buffTimeCounterDodgeChance;
				if (unit.buffTimeCounterDodgeChance > -0.2f)
				{
					SpineAnim instance14 = this.poolBuffDodgeChance.GetInstance();
					Vector3 localPosition14;
					if (!this.buffEffectPositions.TryGetValue(unit.GetId(), out localPosition14))
					{
						localPosition14 = GameMath.ConvertToScreenSpace(unit.pos);
					}
					localPosition14.z -= 0.001f;
					localPosition14.z += zOffset;
					instance14.gameObject.transform.localPosition = localPosition14;
					float buffScale12 = this.GetBuffScale(num, num2, 0.3f, 0.2f);
					instance14.gameObject.transform.localScale = new Vector3(buffScale12, buffScale12, 1f);
					Spine.Animation anim12 = SpineAnimBuffDodgeChance.anim;
					instance14.SetColor((!isInFrontOfBlackCurtain) ? this.cachedBlackCurtainColor : RenderManager.BLACK_CURTAIN_MIN_COLOR);
					instance14.Apply(anim12, num2 + 0.2f, true);
				}
				num2 = unit.buffTimeCounterTrinket;
				if (unit.buffTimeCounterTrinket > -0.2f)
				{
					Hero hero = unit as Hero;
					Trinket trinket = hero.trinket;
					if (trinket != null)
					{
						SpriteRendererContainer instance15 = this.poolBuffTrinketEffect.GetInstance();
						float num3 = this.GetBuffScale(num, num2, 0.3f, 0.2f) * 0.2f;
						instance15.gameObject.transform.localScale = new Vector3(num3, num3, 1f);
						Vector3 localPosition15;
						if (!this.buffEffectPositions.TryGetValue(unit.GetId(), out localPosition15))
						{
							localPosition15 = GameMath.ConvertToScreenSpace(unit.pos);
						}
						localPosition15.y += unit.GetHeight() * 1.2f;
						localPosition15.z = GameMath.ConvertToScreenSpaceZ(unit.pos.y) - 0.001f + zOffset;
						instance15.gameObject.transform.localPosition = localPosition15;
						Sprite renderSprite = trinket.GetRenderSprite();
						instance15.spriteRenderer.color = ((!isInFrontOfBlackCurtain) ? this.cachedBlackCurtainColor : RenderManager.BLACK_CURTAIN_MIN_COLOR);
						instance15.spriteRenderer.sprite = renderSprite;
					}
				}
				return;
			}
			if (!(unit is Hero))
			{
				return;
			}
			if ((unit as Hero).IsReviving())
			{
				return;
			}
			SpineAnim instance16 = this.poolBuffDeath.GetInstance();
			Vector3 localPosition16;
			if (!this.buffEffectPositions.TryGetValue(unit.GetId(), out localPosition16))
			{
				localPosition16 = GameMath.ConvertToScreenSpace(unit.pos);
			}
			localPosition16.z = GameMath.ConvertToScreenSpaceZ(localPosition16.y);
			localPosition16.z -= 0.001f;
			instance16.gameObject.transform.localPosition = localPosition16;
			Spine.Animation anim13 = SpineAnimBuffDeath.anim;
			instance16.SetColor((!isInFrontOfBlackCurtain) ? this.cachedBlackCurtainColor : RenderManager.BLACK_CURTAIN_MIN_COLOR);
			instance16.Apply(anim13, unit.inStateTimeCounter, true);
			num2 = unit.buffTimeCounterReduceRevive;
			if (unit.buffTimeCounterReduceRevive > -0.2f)
			{
				SpineAnim instance17 = this.poolBuffReduceRevive.GetInstance();
				Vector3 localPosition17;
				if (!this.buffEffectPositions.TryGetValue(unit.GetId(), out localPosition17))
				{
					localPosition17 = GameMath.ConvertToScreenSpace(unit.pos);
				}
				localPosition17.y += unit.GetHeight() * 0.75f;
				localPosition17.z = GameMath.ConvertToScreenSpaceZ(unit.pos.y) - 0.001f + zOffset;
				instance17.gameObject.transform.localPosition = localPosition17;
				float buffScale13 = this.GetBuffScale(num, num2, 0.3f, 0.2f);
				instance17.gameObject.transform.localScale = new Vector3(buffScale13, buffScale13, 1f);
				Spine.Animation anim14 = SpineAnimBuffReduceRevive.anim;
				instance17.SetColor((!isInFrontOfBlackCurtain) ? this.cachedBlackCurtainColor : RenderManager.BLACK_CURTAIN_MIN_COLOR);
				instance17.Apply(anim14, num2 + 0.2f, true);
			}
		}

		private float GetBuffScale(float unitScale, float time, float durFadeIn, float durFadeOut)
		{
			float num = unitScale;
			if (time < 0f)
			{
				num *= (durFadeOut + time) / durFadeOut;
			}
			else if (time < durFadeIn)
			{
				num *= time / durFadeIn;
			}
			return num;
		}

		private void Render(GlobalPastDamage simDamage)
		{
			Vector3 vector = simDamage.pos;
			vector = GameMath.ConvertToScreenSpace(vector);
			vector = this._scene.transform.TransformPoint(vector);
			vector = this._floaterContainer.transform.InverseTransformPoint(vector);
			TextContainer instance = this.poolFloaterDamage.GetInstance();
			Text text = instance.text;
			text.text = GameMath.GetDoubleString(simDamage.damage.amount);
			instance.transform.localPosition = vector;
			instance.transform.localScale = new Vector3(simDamage.scale, simDamage.scale, 1f);
			Color color;
			if (simDamage.damage.isMirrored)
			{
				color = new Color32(155, 109, byte.MaxValue, byte.MaxValue);
				text.text = GameMath.GetDoubleString(simDamage.damage.amount);
				text.rectTransform.SetAsLastSibling();
				if (text.font != this.fontBitmapDamage)
				{
					text.font = this.fontBitmapDamage;
				}
			}
			else if (simDamage.damage.showAsPer)
			{
				color = new Color(1f, 0.3f, 0.1f);
				text.text = GameMath.GetPercentString(simDamage.damage.amount, false);
				text.rectTransform.SetAsLastSibling();
				if (text.font != this.fontBitmapDamage)
				{
					text.font = this.fontBitmapDamage;
				}
			}
			else if (simDamage.damage.isMissed)
			{
				color = new Color(0.9098039f, 0.2f, 0.09803922f);
				text.text = LM.Get("DAMAGE_MISS");
				if (text.font != this.fontNormal)
				{
					text.font = this.fontNormal;
				}
			}
			else if (simDamage.damage.isDodged)
			{
				color = new Color(0.698039234f, 0.847058833f, 1f);
				text.text = LM.Get("DAMAGE_DODGED");
				if (text.font != this.fontNormal)
				{
					text.font = this.fontNormal;
				}
			}
			else if (simDamage.highlight)
			{
				color = new Color(1f, 0.6509804f, 0f);
				text.text = GameMath.GetDoubleString(simDamage.damage.amount);
				text.rectTransform.SetAsLastSibling();
				if (text.font != this.fontBitmapDamage)
				{
					text.font = this.fontBitmapDamage;
				}
			}
			else if (simDamage.damage.isUltraCrit)
			{
				color = Color.magenta;
				text.text = GameMath.GetDoubleString(simDamage.damage.amount);
				if (text.font != this.fontBitmapDamage)
				{
					text.font = this.fontBitmapDamage;
				}
			}
			else if (simDamage.damage.isCrit)
			{
				color = new Color(0.968627453f, 0.9647059f, 0.3764706f);
				text.text = GameMath.GetDoubleString(simDamage.damage.amount);
				if (text.font != this.fontBitmapDamage)
				{
					text.font = this.fontBitmapDamage;
				}
			}
			else
			{
				color = Color.white;
				text.text = GameMath.GetDoubleString(simDamage.damage.amount);
				if (text.font != this.fontBitmapDamage)
				{
					text.font = this.fontBitmapDamage;
				}
			}
			color.a = simDamage.alpha;
			text.color = color * this.cachedBlackCurtainColor;
			instance.transform.localPosition = vector;
			instance.transform.localScale = new Vector3(simDamage.scale, simDamage.scale, 1f);
			color.a = simDamage.alpha;
			text.color = color;
		}

		private void Render(GlobalPastHeal simDamage)
		{
			Vector3 vector = simDamage.pos;
			vector = GameMath.ConvertToScreenSpace(vector);
			vector = this._scene.transform.TransformPoint(vector);
			vector = this._floaterContainer.transform.InverseTransformPoint(vector);
			TextContainer instance = this.poolFloaterDamage.GetInstance();
			Text text = instance.text;
			text.text = GameMath.GetDoubleString(simDamage.heal);
			instance.transform.localPosition = vector;
			instance.transform.localScale = new Vector3(simDamage.scale, simDamage.scale, 1f);
			Color color = new Color32(55, byte.MaxValue, 55, byte.MaxValue);
			if (simDamage.isPercent)
			{
				text.text = StringExtension.Concat("+", GameMath.GetPercentString(simDamage.heal, false));
			}
			else
			{
				text.text = StringExtension.Concat("+", GameMath.GetDoubleString(simDamage.heal));
			}
			text.rectTransform.SetAsLastSibling();
			if (text.font != this.fontBitmapDamage)
			{
				text.font = this.fontBitmapDamage;
			}
			color.a = simDamage.alpha;
			text.color = color;
			instance.transform.localPosition = vector;
			instance.transform.localScale = new Vector3(simDamage.scale, simDamage.scale, 1f);
			color.a = simDamage.alpha;
			text.color = color * this.cachedBlackCurtainColor;
		}

		private void Render(VisualEffect simEffect)
		{
			Vector3 localPosition = GameMath.ConvertToScreenSpace(simEffect.pos);
			localPosition.z -= 0.001f;
			bool flag = false;
			float num = simEffect.time;
			float num2 = simEffect.dur;
			ISpineAnim instance;
			Spine.Animation animation;
			if (simEffect.type == VisualEffect.Type.REVERSED_EXCALIBUR_MUD)
			{
				instance = this.poolSmoke.GetInstance();
				animation = SpineAnimSmoke.anim;
			}
			else if (simEffect.type == VisualEffect.Type.BOMBERMAN_DINAMIT)
			{
				instance = this.poolBombermanExplosion.GetInstance();
				instance.SetSkin(Mathf.Max(0, simEffect.skinIndex - 1));
				animation = SpineAnimBombermanExplosion.anim;
				localPosition.z -= 0.1f;
			}
			else if (simEffect.type == VisualEffect.Type.DEREK_BOOK)
			{
				instance = this.poolDerekBookExplosion.GetInstance();
				instance.SetSkin(0);
				animation = SpineAnimDerekBookExplosion.anim;
				localPosition.z -= 0.1f;
			}
			else if (simEffect.type == VisualEffect.Type.TOTEM_THUNDERBOLT)
			{
				instance = this.poolTotemThunderbolt.GetInstance();
				instance.SetColor(Color.white);
				animation = SpineAnimTotemThunderbolt.anim;
			}
			else if (simEffect.type == VisualEffect.Type.TOTEM_THUNDERBOLT_PURPLE)
			{
				instance = this.poolTotemThunderbolt.GetInstance();
				instance.SetColor(new Color(1f, 0.2f, 0.2f));
				animation = SpineAnimTotemThunderbolt.anim;
			}
			else if (simEffect.type == VisualEffect.Type.TOTEM_FIRE_SMOKE)
			{
				instance = this.poolTotemSmoke.GetInstance();
				animation = SpineAnimTotemSmoke.anim;
			}
			else if (simEffect.type == VisualEffect.Type.DUCK)
			{
				instance = this.poolDuck.GetInstance();
				animation = SpineAnimDuck.anim;
			}
			else if (simEffect.type == VisualEffect.Type.DUCK_CHARM)
			{
				instance = this.poolDuck.GetInstance();
				animation = SpineAnimDuck.anim;
			}
			else if (simEffect.type == VisualEffect.Type.MAGOLIES_PROJECTILE_EXPLOSION)
			{
				instance = this.poolMagoliesProjectileExplosion.GetInstance();
				animation = SpineAnimMagoliesProjectileExplosion.anim;
			}
			else if (simEffect.type == VisualEffect.Type.GREEN_APPLE_EXPLOSION)
			{
				instance = this.poolGreenAppleExplosion.GetInstance();
				instance.SetSkin(simEffect.skinIndex - 1);
				animation = SpineAnimGreenAppleExplosion.anim;
				localPosition.z -= 0.1f;
			}
			else if (simEffect.type == VisualEffect.Type.HIT)
			{
				instance = this.poolHitAnimation.GetInstance();
				animation = SpineAnimHit.anim;
			}
			else if (simEffect.type == VisualEffect.Type.ENEMY_DEATH)
			{
				instance = this.poolEnemyDeath.GetInstance();
				animation = SpineAnimEnemyDeath.anim;
				instance.SetSkin(SpineAnimEnemyDeath.skinNames[simEffect.skinIndex]);
			}
			else if (simEffect.type == VisualEffect.Type.SAM_BOTTLE_EXPLOSION)
			{
				instance = this.poolSamBottleExplosion.GetInstance();
				instance.SetSkin(simEffect.skinIndex - 1);
				animation = SpineAnimSamBottleExplosion.anim;
				localPosition.z -= 0.1f;
			}
			else if (simEffect.type == VisualEffect.Type.TOTEM_EARTH_IMPACT)
			{
				instance = this.poolProjectileTotemEarthImpact.GetInstance();
				animation = SpineAnimTotemEarthImpact.anim;
				localPosition.z += 0.1f;
			}
			else if (simEffect.type == VisualEffect.Type.TOTEM_EARTH_TAP)
			{
				instance = this.poolProjectileTotemEarthTap.GetInstance();
				animation = SpineAnimTotemEarthTap.anim;
				localPosition.z -= 0.1f;
			}
			else if (simEffect.type == VisualEffect.Type.TOTEM_EARTH_TAP_DISABLE)
			{
				instance = this.poolProjectileTotemEarthTapDisable.GetInstance();
				animation = SpineAnimTotemEarthTapDisable.anim;
				localPosition.z -= 0.1f;
			}
			else if (simEffect.type == VisualEffect.Type.TOTEM_EARTH_STAR_IMPACT)
			{
				instance = this.poolProjectileTotemEarthStarfall.GetInstance();
				animation = SpineAnimTotemEarthStarfall.animImpact;
				localPosition.z -= 0.1f;
			}
			else if (simEffect.type == VisualEffect.Type.TAM_FLARE)
			{
				instance = this.poolProjectileTamFlare.GetInstance();
				animation = SpineAnimTamFlare.animLoop;
				localPosition.z += 0.1f;
				flag = true;
			}
			else if (simEffect.type == VisualEffect.Type.GOBLIN_SMOKE)
			{
				instance = this.poolProjectileGoblinSmoke.GetInstance();
				float duration = SpineAnimGoblinSmoke.start.duration;
				float duration2 = SpineAnimGoblinSmoke.end.duration;
				if (simEffect.time <= duration)
				{
					animation = SpineAnimGoblinSmoke.start;
					num2 = duration;
				}
				else if (simEffect.dur - simEffect.time <= duration2)
				{
					animation = SpineAnimGoblinSmoke.end;
					num2 = simEffect.dur - duration2;
					num = duration2 - simEffect.dur + simEffect.time;
				}
				else
				{
					animation = SpineAnimGoblinSmoke.loop;
					num = simEffect.time - duration;
					num2 = SpineAnimGoblinSmoke.loop.duration;
				}
				instance.SetSkin(simEffect.skinIndex - 1);
				flag = true;
				simEffect.rot = 0f;
				localPosition.z -= 1f;
			}
			else if (simEffect.type == VisualEffect.Type.WARLOCK_SWARM)
			{
				instance = this.poolProjectileWarlockSwarm.GetInstance();
				animation = SpineAnimWarlockSwarm.end;
				localPosition.z -= 0.1f;
			}
			else if (simEffect.type == VisualEffect.Type.SNAKE_PROJECTILE_EXPLOSION)
			{
				instance = this.poolSnakeProjectileExplosion.GetInstance();
				animation = SpineAnimSnakeProjectileExplosion.anim;
				localPosition.z -= 0.1f;
			}
			else if (simEffect.type == VisualEffect.Type.BABU_SOUP)
			{
				instance = this.poolSmartProjectileBabu.GetInstance();
				animation = SpineAnimBabuProjectile.explode;
				localPosition.z -= 0.1f;
			}
			else
			{
				if (simEffect.type != VisualEffect.Type.ORNAMENT_DROP)
				{
					throw new NotImplementedException();
				}
				instance = this.poolSmartProjectileOrnamentDrop.GetInstance();
				instance.SetSkin(simEffect.skinIndex);
				animation = SpineAnimProjectileOrnamentDrop.explode;
				localPosition.z -= 0.1f;
			}
			float time = (!flag) ? (animation.duration * num / num2) : num;
			instance.SetColor(this.cachedBlackCurtainColor);
			instance.Apply(animation, time, flag);
			instance.gameObject.transform.localPosition = localPosition;
			instance.gameObject.transform.localScale = new Vector3(simEffect.scale * simEffect.dirX, simEffect.scale, 1f);
		}

		private void SetBg(Bg bg, int skinIndex)
		{
			if (bg == Bg.FOREST)
			{
				skinIndex++;
			}
			if ((Bg)RenderManager.BackgroundBundleNames.Length <= bg)
			{
				throw new NotImplementedException();
			}
			if (bg != (Bg)this.worldBackground.bundleIndex && this.worldBackground.loadingIndex == -1)
			{
				this.worldBackground.loadingIndex = (int)bg;
				this.worldBackground.skinIndex = skinIndex;
				DynamicLoadManager.GetPermanentReferenceToBundle(RenderManager.BackgroundBundleNames[(int)bg], new Action(this.OnBackgroundBundleLoaded), false);
			}
			else if (skinIndex != this.worldBackground.skinIndex && bg == (Bg)this.worldBackground.bundleIndex && this.worldBackground.loadingIndex == -1)
			{
				this.worldBackground.animations.SetSkin(skinIndex);
				this.worldBackground.skinIndex = skinIndex;
			}
		}

		private void OnBackgroundBundleLoaded()
		{
			if (this.worldBackground.loadingIndex >= 0)
			{
				DynamicLoadManager.LoadAllAndGetAssetOfType<BackgroundBundle>(RenderManager.BackgroundBundleNames[this.worldBackground.loadingIndex], new Action<BackgroundBundle>(this.OnBackgroundAssetsLoaded), true);
			}
		}

		private void OnBackgroundAssetsLoaded(BackgroundBundle backgroundBundle)
		{
			this.worldBackground.image.sprite = backgroundBundle.sprite;
			BackgroundSpineAnim animations = this.worldBackground.animations;
			if (this.worldBackground.bundleIndex != -1)
			{
				DynamicLoadManager.RemovePermanentReferenceToBundle(RenderManager.BackgroundBundleNames[this.worldBackground.bundleIndex]);
			}
			this.worldBackground.bundleIndex = this.worldBackground.loadingIndex;
			this.worldBackground.skinIndex = GameMath.GetMaxInt(0, this.worldBackground.skinIndex);
			this.worldBackground.loadingIndex = -1;
			if (animations != null)
			{
				UnityEngine.Object.Destroy(animations.gameObject);
			}
			if (backgroundBundle.prefab != null)
			{
				this.worldBackground.animations = UnityEngine.Object.Instantiate<BackgroundSpineAnim>(backgroundBundle.prefab, this.worldBackground.image.transform.parent);
				this.worldBackground.animations.gameObject.SetActive(true);
				this.worldBackground.animations.Init();
				this.worldBackground.skinAlreadySet = false;
			}
		}

		private void RenderBackground(Simulator simulator, World world)
		{
			if (!this.worldBackground.parent.activeSelf)
			{
				this.worldBackground.parent.SetActive(true);
			}
			if (!world.isTransitioning)
			{
				ChallengeRift challengeRift = simulator.GetActiveWorld().activeChallenge as ChallengeRift;
				int skinIndex = (challengeRift == null || challengeRift.riftData.cursesSetup == null) ? 0 : 1;
				this.SetBg(world.activeChallenge.activeEnv.bg, skinIndex);
				this.worldBackground.image.color = this.cachedBlackCurtainColor;
				if (this.worldBackground.animations != null && this.worldBackground.animations.gameObject.activeInHierarchy)
				{
					this.worldBackground.animations.Apply(simulator.GetActiveWorld().timeSinceStartup, true, this.cachedBlackCurtainColor);
					if (!this.worldBackground.skinAlreadySet)
					{
						this.worldBackground.animations.SetSkin(this.worldBackground.skinIndex);
						this.worldBackground.skinAlreadySet = true;
					}
				}
			}
		}

		public Camera mainCam;

		[Header("RenderManager")]
		[Space]
		private const float NON_EXISTANCE_OFFSET = 3f;

		private static readonly Color WHITE_COLOR = Color.white;

		private static readonly Color ENEMY_STUNNED_COLOR = Color.grey;

		private static readonly Color HERO_DUPLICATE_COLOR = new Color(1f, 1f, 1f, 0.25f);

		private static readonly Color BLACK_CURTAIN_MIN_COLOR = new Color(1f, 1f, 1f, 1f);

		private static readonly Color BLACK_CURTAIN_MAX_COLOR = new Color(0.33f, 0.33f, 0.33f, 1f);

		private Color cachedBlackCurtainColor;

		public static Vector3 POS_GOLD_INV_DEFAULT;

		public static Vector3 POS_GOLD_INV_TIMECHALLENGE;

		public static Vector3 POS_CURRENCY_DAILY_QUEST;

		public static Vector3 POS_CURRENCY_SHOP;

		public static Vector3 POS_CURRENCY_OFFER_BUTTON;

		[SerializeField]
		public GameObject _sceneRenderers;

		[SerializeField]
		public GameObject _uiSceneRenderers;

		[SerializeField]
		private GameObject _scene;

		[SerializeField]
		private GameObject _floaterContainer;

		public RenderManager.Background worldBackground;

		[SerializeField]
		private RectTransform _goldFlyTarget;

		[SerializeField]
		private RectTransform _goldTimeChallengeFlyTarget;

		[SerializeField]
		private RectTransform _currencySideFlyTarget;

		[SerializeField]
		private RectTransform _currencySideReachFlyScaleTarget;

		[SerializeField]
		private RectTransform _dailyQuestFlyTarget;

		[SerializeField]
		private RectTransform _shopFlyTarget;

		[SerializeField]
		private RectTransform _offerButtonFlyTarget;

		[SerializeField]
		private Font fontBitmapDamage;

		[SerializeField]
		private Font fontNormal;

		[SerializeField]
		private ParticleSystem blizzard;

		private RenderPoolGameObject poolProjectileReversedExcalibur;

		private RenderPoolGameObject poolProjectileBoss;

		private RenderPoolGameObject poolProjectileFlute;

		private RenderPoolSpineAnim poolProjectileSnake;

		private RenderPoolGameObject poolProjectileWiseSnake;

		private RenderPoolGameObject poolProjectileSamAxe;

		private RenderPoolGameObject poolProjectileBlindArcher;

		private RenderPoolGameObject poolProjectileElfCorrupted;

		private RenderPoolGameObject poolProjectileHumanCorrupted;

		private RenderPoolGameObject poolProjectileGoblinBomb;

		private RenderPoolVariantSprite poolProjectileAppleAid;

		private RenderPoolVariantSprite poolProjectileDerekMagicBall;

		private RenderPoolVariantSprite poolProjectileQuickStudy;

		private RenderPoolVariantSprite poolProjectileBabuTeaCup;

		private RenderPoolVariantSpriteDestroy poolProjectileAppleBombard;

		private Dictionary<Projectile.Type, RenderPool<GameObject>> projectilePools;

		private Dictionary<Projectile.Type, RenderPoolSmartProjectile> smartProjectilePools;

		private RenderPoolSpineAnim poolProjectileTotemShard;

		private RenderPoolSpineAnim poolProjectileDerekMeteor;

		private RenderPoolSpineAnim poolProjectileDwarfCorrupted;

		private RenderPoolSpineAnim poolProjectileMagolies;

		private RenderPoolSpineAnim poolProjectileTotemEarth;

		private RenderPoolSpineAnim poolProjectileTotemEarthImpact;

		private RenderPoolSpineAnim poolProjectileTotemEarthTap;

		private RenderPoolSpineAnim poolProjectileTotemEarthTapDisable;

		private RenderPoolSpineAnim poolProjectileTotemEarthStarfall;

		private RenderPoolSpineAnim poolProjectileTamFlare;

		private RenderPoolSpineAnim poolProjectileGoblinSmoke;

		private RenderPoolSpineAnim poolProjectileWarlockSwarm;

		private RenderPoolSpineAnim poolProjectileWarlockAttack;

		private RenderPoolSmartProjectile poolSmartProjectileSheela;

		private RenderPoolSmartProjectile poolSmartProjectileGoblin;

		private RenderPoolSmartProjectile poolSmartProjectileBoomer;

		private RenderPoolSmartProjectile poolSmartProjectileLenny;

		private RenderPoolSmartProjectile poolSmartProjectileHiltExcalibur;

		private RenderPoolSmartProjectile poolProjectileBombermanFirework;

		private RenderPoolSmartProjectile poolSmartProjectileSamBottle;

		private RenderPoolSmartProjectile poolSmartProjectileBabu;

		private RenderPoolSmartProjectile poolSmartProjectileWendle;

		private RenderPoolSmartProjectilePostDeactivate poolSmartProjectileOrnamentDrop;

		private RenderPoolSpineAnim poolDruidStampedeAnimal;

		private RenderPoolSpineAnim poolDruidLarry;

		private RenderPoolSpineAnim poolDruidCurly;

		private RenderPoolSpineAnim poolDruidMoe;

		private Dictionary<int, RenderPoolSpineAnim> supportAnimalsPools;

		private RenderPoolSpineAnim poolTotemLightning;

		private RenderPoolSpineAnim poolTotemThunderbolt;

		private RenderPoolSpineAnim poolTotemFire;

		private RenderPoolSpineAnim poolTotemSmoke;

		private RenderPoolSpineAnim poolHoratio;

		private RenderPoolSpineAnim poolThour;

		private RenderPoolSpineAnim poolIda;

		private RenderPoolSpineAnim poolKindLenny;

		private RenderPoolSpineAnim poolDerek;

		private RenderPoolSpineAnim poolSheela;

		private RenderPoolSpineAnim poolBomberman;

		private RenderPoolSpineAnim poolSam;

		private RenderPoolSpineAnim poolBlindArcher;

		private RenderPoolSpineAnim poolJim;

		private RenderPoolSpineAnim poolTam;

		private RenderPoolSpineAnim poolWarlock;

		private RenderPoolSpineAnim poolGoblin;

		private RenderPoolSpineAnim poolBabu;

		private RenderPoolSpineAnim poolDruid;

		private RenderPoolSpineAnim poolBandit;

		private RenderPoolSpineAnim poolWolf;

		private RenderPoolSpineAnim poolSpider;

		private RenderPoolSpineAnim poolBat;

		private RenderPoolSpineAnim poolElfSemiCorrupted;

		private RenderPoolSpineAnim poolElfCorrupted;

		private RenderPoolSpineAnim poolDwarfSemiCorrupted;

		private RenderPoolSpineAnim poolDwarfCorrupted;

		private RenderPoolSpineAnim poolHumanCorrupted;

		private RenderPoolSpineAnim poolHumanSemiCorrupted;

		private RenderPoolSpineAnim poolMagolies;

		private RenderPoolSpineAnim poolChest;

		private RenderPoolSpineAnim poolBoss;

		private RenderPoolSpineAnim poolBossElf;

		private RenderPoolSpineAnim poolBossHuman;

		private RenderPoolSpineAnim poolBossMangolies;

		private RenderPoolSpineAnim poolBossDwarf;

		private RenderPoolSpineAnim poolBossWiseSnake;

		private RenderPoolSpineAnim poolSnake;

		private RenderPoolSpineAnim poolBossChristmas;

		private RenderPoolWiseSnakeInvulnerabilityEffect poolWiseSnakeInvulnerabilityEffect;

		private RenderPoolSpineAnim poolSmoke;

		private RenderPoolSpineAnim poolBombermanExplosion;

		private RenderPoolSpineAnim poolDerekBookExplosion;

		private RenderPoolSpineAnim poolMagoliesProjectileExplosion;

		private RenderPoolSpineAnim poolSnakeProjectileExplosion;

		private RenderPoolSpineAnim poolGreenAppleExplosion;

		private RenderPoolSpineAnim poolSamBottleExplosion;

		private RenderPoolSpineAnim poolHitAnimation;

		private RenderPoolSpineAnim poolEnemyDeath;

		private RenderPoolSpineAnim poolBuffAttackFast;

		private RenderPoolSpineAnim poolBuffAttackSlow;

		private RenderPoolSpineAnim poolBuffCritChance;

		private RenderPoolSpineAnim poolBuffDamageAdd;

		private RenderPoolSpineAnim poolBuffDamageDec;

		private RenderPoolSpineAnim poolBuffDeath;

		private RenderPoolSpineAnim poolBuffDefenseless;

		private RenderPoolSpineAnim poolBuffHealthRegen;

		private RenderPoolSpineAnim poolBuffImmunity;

		private RenderPoolSpineAnim poolBuffShield;

		private RenderPoolSpineAnim poolBuffStun;

		private RenderPoolSpineAnim poolBuffMiss;

		private RenderPoolSpineAnim poolBuffMark;

		private RenderPoolSpineAnim poolBuffCritDamage;

		private RenderPoolSpineAnim poolBuffDodgeChance;

		private RenderPoolSpineAnim poolBuffReduceRevive;

		private RenderPoolSpineAnim poolIceManaGather;

		private RenderPoolSpineAnim poolDuck;

		private RenderPoolSpineAnim poolAdDragon;

		private RenderPoolSpriteObject[] poolsDropGold;

		private RenderPoolSpriteObject[] poolsDropGoldTri;

		private RenderPoolSpriteObject[] poolsDropGoldSqr;

		private RenderPoolSpriteObject[] poolsDropMythtone;

		private RenderPoolSpriteObject[] poolsDropCredit;

		private RenderPoolSpriteObject[] poolsDropToken;

		private RenderPoolSpriteObject[] poolsDropScrap;

		private RenderPoolSpriteObject[] poolsDropAeon;

		private RenderPoolSpriteObject[] poolsDropCandy;

		private RenderPoolSpriteObject[] poolsDropTrinketBox;

		private RenderPoolSpriteObject poolDropPowerupCritChance;

		private RenderPoolSpriteObject poolDropPowerupCooldown;

		private RenderPoolSpriteObject poolDropPowerupRevive;

		private RenderPoolSpriteRendererContainer poolBuffTrinketEffect;

		private RenderPoolGameObject poolHealthBar;

		private RenderPoolGameObject poolTimeBar;

		private RenderPoolText poolFloaterDamage;

		private Dictionary<string, Vector3> buffEffectPositions;

		private Dictionary<string, HeroInGameAssetsBundle> heroInGameAssetsBundles = new Dictionary<string, HeroInGameAssetsBundle>();

		private HashSet<string> uniqueHeroes = new HashSet<string>();

		private static readonly float[] IdaAnimDurs = new float[]
		{
			0.533333361f,
			1f,
			0.8666667f
		};

		public static readonly string[] BackgroundBundleNames = new string[]
		{
			"backgrounds/background-0",
			"backgrounds/background-1",
			"backgrounds/background-2",
			"backgrounds/background-3",
			"backgrounds/background-4",
			"backgrounds/background-5",
			"backgrounds/background-6",
			"backgrounds/background-7",
			"backgrounds/background-8",
			"backgrounds/background-9"
		};

		[Serializable]
		public class Background
		{
			public SpriteRenderer image;

			public BackgroundSpineAnim animations;

			public GameObject parent;

			[NonSerialized]
			public int bundleIndex = -1;

			[NonSerialized]
			public int loadingIndex = -1;

			[NonSerialized]
			public int skinIndex = -1;

			[NonSerialized]
			public bool skinAlreadySet;
		}
	}
}
