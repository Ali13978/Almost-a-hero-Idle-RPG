using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class HeroFactory
	{
		private static float GD(float numFrames)
		{
			return numFrames / 30f;
		}

		public static HeroDataBase CreateHoratio(List<GearData> allGears)
		{
			HeroDataBase heroDataBase = new HeroDataBase();
			heroDataBase.id = "HORATIO";
			heroDataBase.nameKey = "HERO_NAME_HILT";
			heroDataBase.descKey = "HERO_DESC_HILT";
			heroDataBase.healthMax = HeroConstants.STARTING_HEALTH * 1.0;
			heroDataBase.healthRegen = 0.0;
			heroDataBase.damageTakenFactor = 1.0;
			heroDataBase.damage = HeroConstants.STARTING_DAMAGE * 2.0;
			heroDataBase.critChance = 0.03f;
			heroDataBase.critFactor = 2.0;
			heroDataBase.durRevive = 10f;
			heroDataBase.heroClass = HeroClass.ATTACKER;
			heroDataBase.scaleBuffVisual = 1f;
			heroDataBase.ultiCatagory = HeroDataBase.UltiCatagory.ORANGE;
			WeaponWood weaponWood = new WeaponWood();
			weaponWood.id = 104;
			heroDataBase.weapon = weaponWood;
			weaponWood.SetTiming(HeroFactory.GD(35f), HeroFactory.GD(16f), 0.7f, 0f);
			weaponWood.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0f, new SoundVariedSimple(SoundArchieve.inst.horatioAttacks, 0.25f)),
				new TimedSound(0.4f, new SoundVariedSimple(SoundArchieve.inst.horatioDamages, 0.5f))
			};
			SkillDataBaseDeadlyTwirl ulti = new SkillDataBaseDeadlyTwirl();
			SkillDataBaseHeHasThePower skillDataBaseHeHasThePower = new SkillDataBaseHeHasThePower();
			SkillDataBaseDodge skillDataBaseDodge = new SkillDataBaseDodge();
			SkillDataBaseSonOfForest skillDataBaseSonOfForest = new SkillDataBaseSonOfForest();
			SkillDataBaseParanoia skillDataBaseParanoia = new SkillDataBaseParanoia();
			SkillEnhancerBaseEverlastingSpin skillEnhancerBaseEverlastingSpin = new SkillEnhancerBaseEverlastingSpin();
			SkillDataBaseReversedExcalibur skillDataBaseReversedExcalibur = new SkillDataBaseReversedExcalibur();
			SkillDataBaseLuckyBoy skillDataBaseLuckyBoy = new SkillDataBaseLuckyBoy();
			SkillDataBaseSonOfWind skillDataBaseSonOfWind = new SkillDataBaseSonOfWind();
			SkillDataBaseFriendship skillDataBaseFriendship = new SkillDataBaseFriendship();
			SkillDataBaseSharpEdge skillDataBaseSharpEdge = new SkillDataBaseSharpEdge();
			heroDataBase.skillTree = new SkillTree(ulti, new SkillTreeNode[]
			{
				skillDataBaseHeHasThePower,
				skillDataBaseDodge,
				skillDataBaseSonOfForest,
				skillDataBaseParanoia,
				skillEnhancerBaseEverlastingSpin
			}, new SkillTreeNode[]
			{
				skillDataBaseReversedExcalibur,
				skillDataBaseLuckyBoy,
				skillDataBaseSonOfWind,
				skillDataBaseFriendship,
				skillDataBaseSharpEdge
			});
			GearData[] array = new GearData[]
			{
				new GearData
				{
					nameKey = "ITEM_NAME_SLAP_GLOVES",
					universalBonusType = GearData.UniversalBonusType.GOLD,
					skillToLevelUp = skillDataBaseSonOfWind
				},
				new GearData
				{
					nameKey = "ITEM_NAME_HARMBURGER",
					universalBonusType = GearData.UniversalBonusType.HEALTH,
					skillToLevelUp = skillDataBaseDodge
				},
				new GearData
				{
					nameKey = "ITEM_NAME_LETTER_OPENER",
					universalBonusType = GearData.UniversalBonusType.DAMAGE,
					skillToLevelUp = skillDataBaseLuckyBoy
				}
			};
			foreach (GearData gearData in array)
			{
				gearData.belongsTo = heroDataBase;
				allGears.Add(gearData);
			}
			heroDataBase.height = 1.6f;
			heroDataBase.projectileTargetOffset = new Vector3(0f, 0.5f);
			heroDataBase.projectileTargetRandomness = 0.1f;
			heroDataBase.soundDeath = new SoundSimple(SoundArchieve.inst.babuDeath, 1f);
			heroDataBase.soundRevive = new SoundSimple(SoundArchieve.inst.heroRevive, 1f);
			heroDataBase.soundVoDeath = new SoundVariedSimple(SoundArchieve.inst.voHoratioDeath, 1f);
			heroDataBase.soundVoRevive = new SoundVariedSimple(SoundArchieve.inst.voHoratioRevive, 1f);
			heroDataBase.soundVoSpawn = new SoundVariedSimple(SoundArchieve.inst.voHoratioSpawn, 1f);
			heroDataBase.soundVoLevelUp = new SoundVariedSimple(SoundArchieve.inst.voHoratioLevelUp, 1f);
			heroDataBase.soundVoItem = SoundArchieve.inst.voHoratioUpgradeItem;
			heroDataBase.soundVoWelcome = new SoundVariedSimple(SoundArchieve.inst.voHoratioWelcome, 1f);
			heroDataBase.soundVoEnvChange = new SoundVariedSimple(SoundArchieve.inst.voHoratioEnvChange, 1f);
			heroDataBase.soundVoSelected = SoundArchieve.inst.voHoratioSelected;
			heroDataBase.soundVoCheer = SoundArchieve.inst.voHoratioCheer;
			return heroDataBase;
		}

		public static HeroDataBase CreateThour(List<GearData> allGears)
		{
			HeroDataBase heroDataBase = new HeroDataBase();
			heroDataBase.id = "THOUR";
			heroDataBase.nameKey = "HERO_NAME_BELLYLARF";
			heroDataBase.descKey = "HERO_DESC_BELLYLARF";
			heroDataBase.healthMax = HeroConstants.STARTING_HEALTH * 2.0;
			heroDataBase.healthRegen = 0.0;
			heroDataBase.damageTakenFactor = 1.0;
			heroDataBase.damage = HeroConstants.STARTING_DAMAGE * 1.5;
			heroDataBase.critChance = 0.03f;
			heroDataBase.critFactor = 2.0;
			heroDataBase.durRevive = 10f;
			heroDataBase.heroClass = HeroClass.DEFENDER;
			heroDataBase.scaleBuffVisual = 1.25f;
			heroDataBase.ultiCatagory = HeroDataBase.UltiCatagory.BLUE;
			WeaponWood weaponWood = new WeaponWood();
			heroDataBase.weapon = weaponWood;
			weaponWood.id = 105;
			weaponWood.SetTiming(HeroFactory.GD(37f), HeroFactory.GD(19f), 0.9f, 0f);
			weaponWood.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.432432443f, new SoundVariedSimple(SoundArchieve.inst.thourAttacks, 1f))
			};
			SkillDataBaseAnger ulti = new SkillDataBaseAnger();
			SkillDataBaseTaunt skillDataBaseTaunt = new SkillDataBaseTaunt();
			SkillDataBaseBash skillDataBaseBash = new SkillDataBaseBash();
			SkillDataBaseResilience skillDataBaseResilience = new SkillDataBaseResilience();
			SkillDataBaseAngerManagement skillDataBaseAngerManagement = new SkillDataBaseAngerManagement();
			SkillDataBaseBigGuy skillDataBaseBigGuy = new SkillDataBaseBigGuy();
			SkillDataBaseLunchTime skillDataBaseLunchTime = new SkillDataBaseLunchTime();
			SkillDataBaseFullStomach skillDataBaseFullStomach = new SkillDataBaseFullStomach();
			SkillDataBaseToughness skillDataBaseToughness = new SkillDataBaseToughness();
			SkillEnhancerBaseFreshMeat skillEnhancerBaseFreshMeat = new SkillEnhancerBaseFreshMeat();
			SkillDataBaseRegeneration skillDataBaseRegeneration = new SkillDataBaseRegeneration();
			heroDataBase.skillTree = new SkillTree(ulti, new SkillTreeNode[]
			{
				skillDataBaseTaunt,
				skillDataBaseBash,
				skillDataBaseResilience,
				skillDataBaseAngerManagement,
				skillDataBaseBigGuy
			}, new SkillTreeNode[]
			{
				skillDataBaseLunchTime,
				skillDataBaseFullStomach,
				skillDataBaseToughness,
				skillEnhancerBaseFreshMeat,
				skillDataBaseRegeneration
			});
			GearData[] array = new GearData[]
			{
				new GearData
				{
					nameKey = "ITEM_NAME_SHINY_HELMET",
					universalBonusType = GearData.UniversalBonusType.GOLD,
					skillToLevelUp = skillDataBaseResilience
				},
				new GearData
				{
					nameKey = "ITEM_NAME_LEG_OF_BEEF",
					universalBonusType = GearData.UniversalBonusType.HEALTH,
					skillToLevelUp = skillDataBaseBash
				},
				new GearData
				{
					nameKey = "ITEM_NAME_MORNING_STAR",
					universalBonusType = GearData.UniversalBonusType.DAMAGE,
					skillToLevelUp = skillDataBaseFullStomach
				}
			};
			foreach (GearData gearData in array)
			{
				gearData.belongsTo = heroDataBase;
				allGears.Add(gearData);
			}
			heroDataBase.height = 2.05f;
			heroDataBase.projectileTargetOffset = new Vector3(0f, 0.65f);
			heroDataBase.projectileTargetRandomness = 0.1f;
			heroDataBase.soundDeath = new SoundSimple(SoundArchieve.inst.heroDeathCommon1, 1f);
			heroDataBase.soundRevive = new SoundSimple(SoundArchieve.inst.heroRevive, 1f);
			heroDataBase.soundVoDeath = new SoundVariedSimple(SoundArchieve.inst.voThourDeath, 1f);
			heroDataBase.soundVoRevive = new SoundVariedSimple(SoundArchieve.inst.voThourRevive, 1f);
			heroDataBase.soundVoSpawn = new SoundVariedSimple(SoundArchieve.inst.voThourSpawn, 1f);
			heroDataBase.soundVoLevelUp = new SoundVariedSimple(SoundArchieve.inst.voThourLevelUp, 1f);
			heroDataBase.soundVoItem = SoundArchieve.inst.voThourUpgradeItem;
			heroDataBase.soundVoWelcome = new SoundVariedSimple(SoundArchieve.inst.voThourWelcome, 1f);
			heroDataBase.soundVoEnvChange = new SoundVariedSimple(SoundArchieve.inst.voThourEnvChange, 1f);
			heroDataBase.soundVoSelected = SoundArchieve.inst.voThourSelected;
			heroDataBase.soundVoCheer = SoundArchieve.inst.voThourCheer;
			return heroDataBase;
		}

		public static HeroDataBase CreateIda(List<GearData> allGears)
		{
			HeroDataBase heroDataBase = new HeroDataBase();
			heroDataBase.id = "IDA";
			heroDataBase.nameKey = "HERO_NAME_VEXX";
			heroDataBase.descKey = "HERO_DESC_VEXX";
			heroDataBase.healthMax = HeroConstants.STARTING_HEALTH * 1.0;
			heroDataBase.healthRegen = 0.0;
			heroDataBase.damageTakenFactor = 1.0;
			heroDataBase.damage = HeroConstants.STARTING_DAMAGE * 3.1;
			heroDataBase.critChance = 0.03f;
			heroDataBase.critFactor = 2.0;
			heroDataBase.durRevive = 10f;
			heroDataBase.heroClass = HeroClass.ATTACKER;
			heroDataBase.ultiCatagory = HeroDataBase.UltiCatagory.BLUE;
			WeaponHeat weaponHeat = new WeaponHeat();
			weaponHeat.id = 100;
			heroDataBase.weapon = weaponHeat;
			weaponHeat.SetTiming(HeroFactory.GD(61f), HeroFactory.GD(33f), 0.3f);
			weaponHeat.heatMax = 100f;
			weaponHeat.heatPerDamage = 25f;
			weaponHeat.coolingSpeed = 5f;
			weaponHeat.overCoolingSpeed = 15f;
			weaponHeat.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.269841284f, new SoundVariedSimple(SoundArchieve.inst.idaAttacks, 1f)),
				new TimedSound(0.476190478f, new SoundVariedSimple(SoundArchieve.inst.idaDamages, 1f))
			};
			SkillDataBaseShockWave ulti = new SkillDataBaseShockWave();
			SkillDataBaseConcentration skillDataBaseConcentration = new SkillDataBaseConcentration();
			SkillDataBaseVillageGirl skillDataBaseVillageGirl = new SkillDataBaseVillageGirl();
			SkillDataBaseMadGirl skillDataBaseMadGirl = new SkillDataBaseMadGirl();
			SkillDataBaseChillDown skillDataBaseChillDown = new SkillDataBaseChillDown();
			SkillDataBaseCollectScraps skillDataBaseCollectScraps = new SkillDataBaseCollectScraps();
			SkillDataBaseFastCheerful skillDataBaseFastCheerful = new SkillDataBaseFastCheerful();
			SkillDataBaseHardTraining skillDataBaseHardTraining = new SkillDataBaseHardTraining();
			SkillDataBaseRecycle skillDataBaseRecycle = new SkillDataBaseRecycle();
			SkillDataBaseForge skillDataBaseForge = new SkillDataBaseForge();
			SkillEnhancerBaseEarthquake skillEnhancerBaseEarthquake = new SkillEnhancerBaseEarthquake();
			heroDataBase.skillTree = new SkillTree(ulti, new SkillTreeNode[]
			{
				skillDataBaseConcentration,
				skillDataBaseVillageGirl,
				skillDataBaseMadGirl,
				skillDataBaseChillDown,
				skillDataBaseCollectScraps
			}, new SkillTreeNode[]
			{
				skillDataBaseFastCheerful,
				skillDataBaseHardTraining,
				skillDataBaseRecycle,
				skillDataBaseForge,
				skillEnhancerBaseEarthquake
			});
			GearData[] array = new GearData[]
			{
				new GearData
				{
					nameKey = "ITEM_NAME_GOLDEN_CROWN",
					universalBonusType = GearData.UniversalBonusType.GOLD,
					skillToLevelUp = skillDataBaseVillageGirl
				},
				new GearData
				{
					nameKey = "ITEM_NAME_IRRESISTIBLE_CAKE",
					universalBonusType = GearData.UniversalBonusType.HEALTH,
					skillToLevelUp = skillDataBaseHardTraining
				},
				new GearData
				{
					nameKey = "ITEM_NAME_BELT_OF_MIGHT",
					universalBonusType = GearData.UniversalBonusType.DAMAGE,
					skillToLevelUp = skillDataBaseRecycle
				}
			};
			foreach (GearData gearData in array)
			{
				gearData.belongsTo = heroDataBase;
				allGears.Add(gearData);
			}
			heroDataBase.height = 1.8f;
			heroDataBase.projectileTargetOffset = new Vector3(0f, 0.5f);
			heroDataBase.projectileTargetRandomness = 0.1f;
			heroDataBase.soundDeath = new SoundSimple(SoundArchieve.inst.heroDeathCommon1, 1f);
			heroDataBase.soundRevive = new SoundSimple(SoundArchieve.inst.heroRevive, 1f);
			heroDataBase.soundVoDeath = new SoundVariedSimple(SoundArchieve.inst.voIdaDeath, 1f);
			heroDataBase.soundVoRevive = new SoundVariedSimple(SoundArchieve.inst.voIdaRevive, 1f);
			heroDataBase.soundVoSpawn = new SoundVariedSimple(SoundArchieve.inst.voIdaSpawn, 1f);
			heroDataBase.soundVoLevelUp = new SoundVariedSimple(SoundArchieve.inst.voIdaLevelUp, 1f);
			heroDataBase.soundVoItem = SoundArchieve.inst.voIdaUpgradeItem;
			heroDataBase.soundVoWelcome = new SoundVariedSimple(SoundArchieve.inst.voIdaWelcome, 1f);
			heroDataBase.soundVoEnvChange = new SoundVariedSimple(SoundArchieve.inst.voIdaEnvChange, 1f);
			heroDataBase.soundVoSelected = SoundArchieve.inst.voIdaSelected;
			heroDataBase.soundVoCheer = SoundArchieve.inst.voIdaCheer;
			return heroDataBase;
		}

		public static HeroDataBase CreateKindLenny(List<GearData> allGears)
		{
			HeroDataBase heroDataBase = new HeroDataBase();
			heroDataBase.id = "KIND_LENNY";
			heroDataBase.nameKey = "HERO_NAME_KIND_LENNY";
			heroDataBase.descKey = "HERO_DESC_KIND_LENNY";
			heroDataBase.healthMax = HeroConstants.STARTING_HEALTH * 1.5;
			heroDataBase.healthRegen = 0.0;
			heroDataBase.damageTakenFactor = 1.0;
			heroDataBase.damage = HeroConstants.STARTING_DAMAGE * 1.7999999523162842;
			heroDataBase.critChance = 0.03f;
			heroDataBase.critFactor = 2.0;
			heroDataBase.durRevive = 10f;
			heroDataBase.heroClass = HeroClass.ATTACKER;
			heroDataBase.scaleBuffVisual = 1.25f;
			heroDataBase.ultiCatagory = HeroDataBase.UltiCatagory.ORANGE;
			WeaponLoadedRanged weaponLoadedRanged = new WeaponLoadedRanged();
			heroDataBase.weapon = weaponLoadedRanged;
			weaponLoadedRanged.SetTiming(HeroFactory.GD(24f), HeroFactory.GD(11f), 0.25f);
			weaponLoadedRanged.id = 101;
			weaponLoadedRanged.loadMax = SkillDataBaseFastReloader.BASE_AMMO;
			weaponLoadedRanged.durReload = SkillDataBaseFastReloader.BASE_RELOAD;
			weaponLoadedRanged.projectileType = Projectile.Type.APPLE;
			weaponLoadedRanged.targetType = Projectile.TargetType.SINGLE_ENEMY;
			weaponLoadedRanged.durFly = 0.42f;
			weaponLoadedRanged.projectilePath = new ProjectilePathBomb
			{
				heightAddMax = 0.13f
			};
			weaponLoadedRanged.projectileImpactVis = new VisualEffect(VisualEffect.Type.GREEN_APPLE_EXPLOSION, 0.333f);
			weaponLoadedRanged.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.5f, new SoundVariedSimple(SoundArchieve.inst.lennyAttacks, 1f)),
				new TimedSound(0.7916667f, new SoundVariedSimple(SoundArchieve.inst.lennyDamages, 1f))
			};
			weaponLoadedRanged.soundReload = new SoundSimple(SoundArchieve.inst.lennyReload, 1f);
			SkillDataBaseMiniCannon ulti = new SkillDataBaseMiniCannon();
			SkillDataBaseBombard skillDataBaseBombard = new SkillDataBaseBombard();
			SkillDataBaseXxl skillDataBaseXxl = new SkillDataBaseXxl();
			SkillDataBaseRottenApple skillDataBaseRottenApple = new SkillDataBaseRottenApple();
			SkillEnhancerBaseConstitution skillEnhancerBaseConstitution = new SkillEnhancerBaseConstitution();
			SkillDataBaseWellFed skillDataBaseWellFed = new SkillDataBaseWellFed();
			SkillDataBaseEatApple skillDataBaseEatApple = new SkillDataBaseEatApple();
			SkillDataBaseSharpShooter skillDataBaseSharpShooter = new SkillDataBaseSharpShooter();
			SkillDataBaseFastReloader skillDataBaseFastReloader = new SkillDataBaseFastReloader();
			SkillEnhancerBaseBigStomach skillEnhancerBaseBigStomach = new SkillEnhancerBaseBigStomach();
			SkillDataBaseNicestKiller skillDataBaseNicestKiller = new SkillDataBaseNicestKiller();
			heroDataBase.skillTree = new SkillTree(ulti, new SkillTreeNode[]
			{
				skillDataBaseBombard,
				skillDataBaseXxl,
				skillDataBaseRottenApple,
				skillEnhancerBaseConstitution,
				skillDataBaseWellFed
			}, new SkillTreeNode[]
			{
				skillDataBaseEatApple,
				skillDataBaseSharpShooter,
				skillDataBaseFastReloader,
				skillEnhancerBaseBigStomach,
				skillDataBaseNicestKiller
			});
			GearData[] array = new GearData[]
			{
				new GearData
				{
					nameKey = "ITEM_NAME_GOLDEN_APPLE",
					universalBonusType = GearData.UniversalBonusType.GOLD,
					skillToLevelUp = skillEnhancerBaseConstitution
				},
				new GearData
				{
					nameKey = "ITEM_NAME_COOKED_CHICKEN",
					universalBonusType = GearData.UniversalBonusType.HEALTH,
					skillToLevelUp = skillDataBaseXxl
				},
				new GearData
				{
					nameKey = "ITEM_NAME_POCKET_CANNON",
					universalBonusType = GearData.UniversalBonusType.DAMAGE,
					skillToLevelUp = skillDataBaseSharpShooter
				}
			};
			foreach (GearData gearData in array)
			{
				gearData.belongsTo = heroDataBase;
				allGears.Add(gearData);
			}
			heroDataBase.height = 1.9f;
			heroDataBase.projectileTargetOffset = new Vector3(0f, 0.65f);
			heroDataBase.projectileTargetRandomness = 0.1f;
			heroDataBase.soundDeath = new SoundSimple(SoundArchieve.inst.lennyDeath, 1f);
			heroDataBase.soundRevive = new SoundSimple(SoundArchieve.inst.lennyRevive, 1f);
			heroDataBase.soundVoDeath = new SoundVariedSimple(SoundArchieve.inst.voLennyDeath, 1f);
			heroDataBase.soundVoRevive = new SoundVariedSimple(SoundArchieve.inst.voLennyRevive, 1f);
			heroDataBase.soundVoSpawn = new SoundVariedSimple(SoundArchieve.inst.voLennySpawn, 1f);
			heroDataBase.soundVoLevelUp = new SoundVariedSimple(SoundArchieve.inst.voLennyLevelUp, 1f);
			heroDataBase.soundVoItem = SoundArchieve.inst.voLennyUpgradeItem;
			heroDataBase.soundVoWelcome = new SoundVariedSimple(SoundArchieve.inst.voLennyWelcome, 1f);
			heroDataBase.soundVoEnvChange = new SoundVariedSimple(SoundArchieve.inst.voLennyEnvChange, 1f);
			heroDataBase.soundVoSelected = SoundArchieve.inst.voLennySelected;
			heroDataBase.soundVoCheer = SoundArchieve.inst.voLennyCheer;
			return heroDataBase;
		}

		public static HeroDataBase CreateDerek(List<GearData> allGears)
		{
			HeroDataBase heroDataBase = new HeroDataBase();
			heroDataBase.id = "DEREK";
			heroDataBase.nameKey = "HERO_NAME_WENDLE";
			heroDataBase.descKey = "HERO_DESC_WENDLE";
			heroDataBase.healthMax = HeroConstants.STARTING_HEALTH * 0.75;
			heroDataBase.healthRegen = 0.0;
			heroDataBase.damageTakenFactor = 1.0;
			heroDataBase.damage = HeroConstants.STARTING_DAMAGE * 2.25;
			heroDataBase.critChance = 0.03f;
			heroDataBase.critFactor = 2.0;
			heroDataBase.durRevive = 10f;
			heroDataBase.heroClass = HeroClass.ATTACKER;
			heroDataBase.ultiCatagory = HeroDataBase.UltiCatagory.RED;
			WeaponWoodRandomRanged weaponWoodRandomRanged = new WeaponWoodRandomRanged();
			weaponWoodRandomRanged.id = 111;
			heroDataBase.weapon = weaponWoodRandomRanged;
			weaponWoodRandomRanged.SetTiming(HeroFactory.GD(44f), HeroFactory.GD(19f), 0.5f);
			weaponWoodRandomRanged.projectileType = Projectile.Type.DEREK_MAGIC_BALL;
			weaponWoodRandomRanged.targetType = Projectile.TargetType.SINGLE_ENEMY;
			weaponWoodRandomRanged.durFly = 0.6f;
			weaponWoodRandomRanged.projectilePath = new ProjectilePathWiggly();
			weaponWoodRandomRanged.impactSound = new SoundVariedSimple(SoundArchieve.inst.derekAttackImpacts, 0.8f);
			weaponWoodRandomRanged.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.09090909f, new SoundVariedSimple(SoundArchieve.inst.derekAttacks, 1f)),
				new TimedSound(0.4090909f, new SoundVariedSimple(SoundArchieve.inst.derekDamages, 1f))
			};
			SkillDataBaseLobMagic ulti = new SkillDataBaseLobMagic();
			SkillDataBaseOutOfControl skillDataBaseOutOfControl = new SkillDataBaseOutOfControl();
			SkillEnhancerBaseTricks skillEnhancerBaseTricks = new SkillEnhancerBaseTricks();
			SkillDataBaseForgetful skillDataBaseForgetful = new SkillDataBaseForgetful();
			SkillDataBaseCraziness skillDataBaseCraziness = new SkillDataBaseCraziness();
			SkillDataBaseDoubleMissile skillDataBaseDoubleMissile = new SkillDataBaseDoubleMissile();
			SkillDataBaseThunderSomething skillDataBaseThunderSomething = new SkillDataBaseThunderSomething();
			SkillDataBaseSpiritTalk skillDataBaseSpiritTalk = new SkillDataBaseSpiritTalk();
			SkillDataBaseWisdom skillDataBaseWisdom = new SkillDataBaseWisdom();
			SkillDataBaseElderness skillDataBaseElderness = new SkillDataBaseElderness();
			SkillEnhancerBaseRapidThunder skillEnhancerBaseRapidThunder = new SkillEnhancerBaseRapidThunder();
			heroDataBase.skillTree = new SkillTree(ulti, new SkillTreeNode[]
			{
				skillDataBaseOutOfControl,
				skillEnhancerBaseTricks,
				skillDataBaseForgetful,
				skillDataBaseCraziness,
				skillDataBaseDoubleMissile
			}, new SkillTreeNode[]
			{
				skillDataBaseThunderSomething,
				skillDataBaseSpiritTalk,
				skillDataBaseWisdom,
				skillDataBaseElderness,
				skillEnhancerBaseRapidThunder
			});
			GearData[] array = new GearData[]
			{
				new GearData
				{
					nameKey = "ITEM_NAME_BOOK_OF_ECONOMICS",
					universalBonusType = GearData.UniversalBonusType.GOLD,
					skillToLevelUp = skillDataBaseSpiritTalk
				},
				new GearData
				{
					nameKey = "ITEM_NAME_ELIXIR_OF_VITALITY",
					universalBonusType = GearData.UniversalBonusType.HEALTH,
					skillToLevelUp = skillDataBaseCraziness
				},
				new GearData
				{
					nameKey = "ITEM_NAME_SCROLL_OF_DARKNESS",
					universalBonusType = GearData.UniversalBonusType.DAMAGE,
					skillToLevelUp = skillEnhancerBaseTricks
				}
			};
			foreach (GearData gearData in array)
			{
				gearData.belongsTo = heroDataBase;
				allGears.Add(gearData);
			}
			heroDataBase.height = 1.5f;
			heroDataBase.projectileTargetOffset = new Vector3(0f, 0.5f);
			heroDataBase.projectileTargetRandomness = 0.15f;
			heroDataBase.soundDeath = new SoundSimple(SoundArchieve.inst.heroDeathCommon2, 1f);
			heroDataBase.soundRevive = new SoundSimple(SoundArchieve.inst.heroRevive, 1f);
			heroDataBase.soundVoDeath = new SoundVariedSimple(SoundArchieve.inst.voDerekDeath, 1f);
			heroDataBase.soundVoRevive = new SoundVariedSimple(SoundArchieve.inst.voDerekRevive, 1f);
			heroDataBase.soundVoSpawn = new SoundVariedSimple(SoundArchieve.inst.voDerekSpawn, 1f);
			heroDataBase.soundVoLevelUp = new SoundVariedSimple(SoundArchieve.inst.voDerekLevelUp, 1f);
			heroDataBase.soundVoItem = SoundArchieve.inst.voDerekUpgradeItem;
			heroDataBase.soundVoWelcome = new SoundVariedSimple(SoundArchieve.inst.voDerekWelcome, 1f);
			heroDataBase.soundVoEnvChange = new SoundVariedSimple(SoundArchieve.inst.voDerekEnvChange, 1f);
			heroDataBase.soundVoSelected = SoundArchieve.inst.voDerekSelected;
			heroDataBase.soundVoCheer = SoundArchieve.inst.voDerekCheer;
			return heroDataBase;
		}

		public static HeroDataBase CreateSheela(List<GearData> allGears)
		{
			HeroDataBase heroDataBase = new HeroDataBase();
			heroDataBase.id = "SHEELA";
			heroDataBase.nameKey = "HERO_NAME_V";
			heroDataBase.descKey = "HERO_DESC_V";
			heroDataBase.healthMax = HeroConstants.STARTING_HEALTH * 1.0;
			heroDataBase.healthRegen = 0.0;
			heroDataBase.damageTakenFactor = 1.0;
			heroDataBase.damage = HeroConstants.STARTING_DAMAGE * 1.7999999523162842;
			heroDataBase.critChance = 0.03f;
			heroDataBase.critFactor = 2.0;
			heroDataBase.durRevive = 10f;
			heroDataBase.heroClass = HeroClass.SUPPORTER;
			heroDataBase.ultiCatagory = HeroDataBase.UltiCatagory.BLUE;
			WeaponLoadedRanged weaponLoadedRanged = new WeaponLoadedRanged();
			heroDataBase.weapon = weaponLoadedRanged;
			weaponLoadedRanged.SetTiming(HeroFactory.GD(29f), HeroFactory.GD(15f), 0.5f);
			weaponLoadedRanged.id = 102;
			weaponLoadedRanged.loadMax = SkillDataBaseHiddenDaggers.BASE_AMMO;
			weaponLoadedRanged.durReload = HeroFactory.GD(73f);
			weaponLoadedRanged.projectileType = Projectile.Type.SHEELA;
			weaponLoadedRanged.targetType = Projectile.TargetType.SINGLE_ENEMY;
			weaponLoadedRanged.durFly = 0.35f;
			weaponLoadedRanged.projectilePath = new ProjectilePathLinear();
			weaponLoadedRanged.soundReload = new SoundVariedSimple(SoundArchieve.inst.sheelaReloads, 1f);
			weaponLoadedRanged.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.37931034f, new SoundVariedSimple(SoundArchieve.inst.sheelaAttacks, 0.7f))
			};
			SkillDataBaseMasterThief ulti = new SkillDataBaseMasterThief();
			SkillDataBaseSliceDice skillDataBaseSliceDice = new SkillDataBaseSliceDice();
			SkillDataBasePoisonedDaggers skillDataBasePoisonedDaggers = new SkillDataBasePoisonedDaggers();
			SkillDataBaseHiddenDaggers skillDataBaseHiddenDaggers = new SkillDataBaseHiddenDaggers();
			SkillDataBaseCityThief skillDataBaseCityThief = new SkillDataBaseCityThief();
			SkillDataBaseWeakPoint skillDataBaseWeakPoint = new SkillDataBaseWeakPoint();
			SkillDataBaseRunEmmetRun skillDataBaseRunEmmetRun = new SkillDataBaseRunEmmetRun();
			SkillDataBaseTreasureHunter skillDataBaseTreasureHunter = new SkillDataBaseTreasureHunter();
			SkillEnhancerBaseSwiftEmmet skillEnhancerBaseSwiftEmmet = new SkillEnhancerBaseSwiftEmmet();
			SkillDataBaseEvasion skillDataBaseEvasion = new SkillDataBaseEvasion();
			SkillDataBaseHearthSeeker skillDataBaseHearthSeeker = new SkillDataBaseHearthSeeker();
			heroDataBase.skillTree = new SkillTree(ulti, new SkillTreeNode[]
			{
				skillDataBaseSliceDice,
				skillDataBasePoisonedDaggers,
				skillDataBaseHiddenDaggers,
				skillDataBaseCityThief,
				skillDataBaseWeakPoint
			}, new SkillTreeNode[]
			{
				skillDataBaseRunEmmetRun,
				skillDataBaseTreasureHunter,
				skillEnhancerBaseSwiftEmmet,
				skillDataBaseEvasion,
				skillDataBaseHearthSeeker
			});
			GearData[] array = new GearData[]
			{
				new GearData
				{
					nameKey = "ITEM_NAME_GOLD_SACK",
					universalBonusType = GearData.UniversalBonusType.GOLD,
					skillToLevelUp = skillDataBaseTreasureHunter
				},
				new GearData
				{
					nameKey = "ITEM_NAME_HEALTH_TRAP",
					universalBonusType = GearData.UniversalBonusType.HEALTH,
					skillToLevelUp = skillDataBaseWeakPoint
				},
				new GearData
				{
					nameKey = "ITEM_NAME_POISONED_DAGGER",
					universalBonusType = GearData.UniversalBonusType.DAMAGE,
					skillToLevelUp = skillDataBaseHiddenDaggers
				}
			};
			foreach (GearData gearData in array)
			{
				gearData.belongsTo = heroDataBase;
				allGears.Add(gearData);
			}
			heroDataBase.height = 1.7f;
			heroDataBase.projectileTargetOffset = new Vector3(0f, 0.5f);
			heroDataBase.projectileTargetRandomness = 0.15f;
			heroDataBase.soundDeath = new SoundSimple(SoundArchieve.inst.heroDeathCommon2, 1f);
			heroDataBase.soundRevive = new SoundSimple(SoundArchieve.inst.heroRevive, 1f);
			heroDataBase.soundVoDeath = new SoundVariedSimple(SoundArchieve.inst.voSheelaDeath, 1f);
			heroDataBase.soundVoRevive = new SoundVariedSimple(SoundArchieve.inst.voSheelaRevive, 1f);
			heroDataBase.soundVoSpawn = new SoundVariedSimple(SoundArchieve.inst.voSheelaSpawn, 1f);
			heroDataBase.soundVoLevelUp = new SoundVariedSimple(SoundArchieve.inst.voSheelaLevelUp, 1f);
			heroDataBase.soundVoItem = SoundArchieve.inst.voSheelaUpgradeItem;
			heroDataBase.soundVoWelcome = new SoundVariedSimple(SoundArchieve.inst.voSheelaWelcome, 1f);
			heroDataBase.soundVoEnvChange = new SoundVariedSimple(SoundArchieve.inst.voSheelaEnvChange, 1f);
			heroDataBase.soundVoSelected = SoundArchieve.inst.voSheelaSelected;
			heroDataBase.soundVoCheer = SoundArchieve.inst.voSheelaCheer;
			return heroDataBase;
		}

		public static HeroDataBase CreateBomberman(List<GearData> allGears)
		{
			HeroDataBase heroDataBase = new HeroDataBase();
			heroDataBase.id = "BOMBERMAN";
			heroDataBase.nameKey = "HERO_NAME_BOOMER_BADLAD";
			heroDataBase.descKey = "HERO_DESC_BOOMER_BADLAD";
			heroDataBase.healthMax = HeroConstants.STARTING_HEALTH * 0.800000011920929;
			heroDataBase.healthRegen = 0.0;
			heroDataBase.damageTakenFactor = 1.0;
			heroDataBase.damage = HeroConstants.STARTING_DAMAGE * 2.2000000476837158;
			heroDataBase.critChance = 0.03f;
			heroDataBase.critFactor = 2.0;
			heroDataBase.durRevive = 10f;
			heroDataBase.heroClass = HeroClass.ATTACKER;
			heroDataBase.ultiCatagory = HeroDataBase.UltiCatagory.RED;
			WeaponWoodRandomRanged weaponWoodRandomRanged = new WeaponWoodRandomRanged();
			heroDataBase.weapon = weaponWoodRandomRanged;
			weaponWoodRandomRanged.id = 110;
			weaponWoodRandomRanged.SetTiming(HeroFactory.GD(42f), HeroFactory.GD(27f), 0.9f);
			weaponWoodRandomRanged.projectileType = Projectile.Type.BOMBERMAN_DINAMIT;
			weaponWoodRandomRanged.targetType = Projectile.TargetType.SINGLE_ENEMY;
			weaponWoodRandomRanged.durFly = 0.4f;
			weaponWoodRandomRanged.projectilePath = new ProjectilePathBomb
			{
				heightAddMax = 0.2f
			};
			weaponWoodRandomRanged.areaDamageRatio = 0.5;
			weaponWoodRandomRanged.areaDamageRadius = 0.35f;
			weaponWoodRandomRanged.impactVisualEffect = new VisualEffect(VisualEffect.Type.BOMBERMAN_DINAMIT, 0.5f);
			weaponWoodRandomRanged.impactSound = new SoundVariedSimple(SoundArchieve.inst.bombermanAttackImpacts, 0.5f);
			weaponWoodRandomRanged.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.166666672f, new SoundVariedSimple(SoundArchieve.inst.bombermanAttacks, 0.65f))
			};
			SkillDataBaseSelfDestruct ulti = new SkillDataBaseSelfDestruct();
			SkillDataBaseFireworks skillDataBaseFireworks = new SkillDataBaseFireworks();
			SkillDataBaseConcussion skillDataBaseConcussion = new SkillDataBaseConcussion();
			SkillDataBaseStubbernness skillDataBaseStubbernness = new SkillDataBaseStubbernness();
			SkillEnhancerBaseMadness skillEnhancerBaseMadness = new SkillEnhancerBaseMadness();
			SkillDataBaseFuelThemUp skillDataBaseFuelThemUp = new SkillDataBaseFuelThemUp();
			SkillDataBaseJokesOnYou skillDataBaseJokesOnYou = new SkillDataBaseJokesOnYou();
			SkillDataBaseFragmentation skillDataBaseFragmentation = new SkillDataBaseFragmentation();
			SkillDataBaseWhatDoesNotKillYou skillDataBaseWhatDoesNotKillYou = new SkillDataBaseWhatDoesNotKillYou();
			SkillDataBaseCrackShot skillDataBaseCrackShot = new SkillDataBaseCrackShot();
			SkillDataBaseExplosiveShots skillDataBaseExplosiveShots = new SkillDataBaseExplosiveShots();
			heroDataBase.skillTree = new SkillTree(ulti, new SkillTreeNode[]
			{
				skillDataBaseFireworks,
				skillDataBaseConcussion,
				skillDataBaseStubbernness,
				skillEnhancerBaseMadness,
				skillDataBaseFuelThemUp
			}, new SkillTreeNode[]
			{
				skillDataBaseJokesOnYou,
				skillDataBaseFragmentation,
				skillDataBaseWhatDoesNotKillYou,
				skillDataBaseCrackShot,
				skillDataBaseExplosiveShots
			});
			GearData[] array = new GearData[]
			{
				new GearData
				{
					nameKey = "ITEM_NAME_PIGGY_BANK",
					universalBonusType = GearData.UniversalBonusType.GOLD,
					skillToLevelUp = skillDataBaseFragmentation
				},
				new GearData
				{
					nameKey = "ITEM_NAME_WILDFIRE_POTION",
					universalBonusType = GearData.UniversalBonusType.HEALTH,
					skillToLevelUp = skillDataBaseCrackShot
				},
				new GearData
				{
					nameKey = "ITEM_NAME_SHORT_FUSED_BOMB",
					universalBonusType = GearData.UniversalBonusType.DAMAGE,
					skillToLevelUp = skillDataBaseWhatDoesNotKillYou
				}
			};
			foreach (GearData gearData in array)
			{
				gearData.belongsTo = heroDataBase;
				allGears.Add(gearData);
			}
			heroDataBase.height = 1.8f;
			heroDataBase.projectileTargetOffset = new Vector3(0f, 0.45f);
			heroDataBase.projectileTargetRandomness = 0.12f;
			heroDataBase.soundDeath = new SoundSimple(SoundArchieve.inst.heroDeathCommon3, 1f);
			heroDataBase.soundRevive = new SoundSimple(SoundArchieve.inst.heroRevive, 1f);
			heroDataBase.soundVoDeath = new SoundVariedSimple(SoundArchieve.inst.voBombermanDeath, 1f);
			heroDataBase.soundVoRevive = new SoundVariedSimple(SoundArchieve.inst.voBombermanRevive, 1f);
			heroDataBase.soundVoSpawn = new SoundVariedSimple(SoundArchieve.inst.voBombermanSpawn, 1f);
			heroDataBase.soundVoLevelUp = new SoundVariedSimple(SoundArchieve.inst.voBombermanLevelUp, 1f);
			heroDataBase.soundVoItem = SoundArchieve.inst.voBombermanUpgradeItem;
			heroDataBase.soundVoWelcome = new SoundVariedSimple(SoundArchieve.inst.voBombermanWelcome, 1f);
			heroDataBase.soundVoEnvChange = new SoundVariedSimple(SoundArchieve.inst.voBombermanEnvChange, 1f);
			heroDataBase.soundVoSelected = SoundArchieve.inst.voBombermanSelected;
			heroDataBase.soundVoCheer = SoundArchieve.inst.voBombermanCheer;
			return heroDataBase;
		}

		public static HeroDataBase CreateSam(List<GearData> allGears)
		{
			HeroDataBase heroDataBase = new HeroDataBase();
			heroDataBase.id = "SAM";
			heroDataBase.nameKey = "HERO_NAME_SAM";
			heroDataBase.descKey = "HERO_DESC_SAM";
			heroDataBase.healthMax = HeroConstants.STARTING_HEALTH * 3.2000000476837158;
			heroDataBase.healthRegen = 0.0;
			heroDataBase.damageTakenFactor = 1.0;
			heroDataBase.damage = HeroConstants.STARTING_DAMAGE * 2.0999999046325684;
			heroDataBase.critChance = 0.03f;
			heroDataBase.critFactor = 2.0;
			heroDataBase.durRevive = 10f;
			heroDataBase.heroClass = HeroClass.DEFENDER;
			heroDataBase.ultiCatagory = HeroDataBase.UltiCatagory.BLUE;
			WeaponWoodRandomRanged weaponWoodRandomRanged = new WeaponWoodRandomRanged();
			weaponWoodRandomRanged.id = 112;
			heroDataBase.weapon = weaponWoodRandomRanged;
			weaponWoodRandomRanged.SetTiming(HeroFactory.GD(32f), HeroFactory.GD(16f), 0.9f);
			weaponWoodRandomRanged.impactVisualEffect = new VisualEffect(VisualEffect.Type.SAM_BOTTLE_EXPLOSION, 0.267f);
			weaponWoodRandomRanged.projectileType = Projectile.Type.SAM_BOTTLE;
			weaponWoodRandomRanged.targetType = Projectile.TargetType.SINGLE_ENEMY;
			weaponWoodRandomRanged.durFly = 0.3f;
			weaponWoodRandomRanged.projectilePath = new ProjectilePathBomb
			{
				heightAddMax = 0.1f
			};
			weaponWoodRandomRanged.impactSound = new SoundVariedSimple(SoundArchieve.inst.samAttackImpacts, 1f);
			weaponWoodRandomRanged.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.40625f, new SoundVariedSimple(SoundArchieve.inst.samAttacks, 0.5f))
			};
			SkillDataBaseRevenge ulti = new SkillDataBaseRevenge();
			SkillDataBaseShieldAll skillDataBaseShieldAll = new SkillDataBaseShieldAll();
			SkillDataBaseBlock skillDataBaseBlock = new SkillDataBaseBlock();
			SkillDataBaseRepel skillDataBaseRepel = new SkillDataBaseRepel();
			SkillDataBaseHoldYourGround skillDataBaseHoldYourGround = new SkillDataBaseHoldYourGround();
			SkillEnhancerMasterShielder skillEnhancerMasterShielder = new SkillEnhancerMasterShielder();
			SkillDataBaseSlam skillDataBaseSlam = new SkillDataBaseSlam();
			SkillDataBaseManOfHill skillDataBaseManOfHill = new SkillDataBaseManOfHill();
			SkillDataBasePunishment skillDataBasePunishment = new SkillDataBasePunishment();
			SkillDataBaseTranscendence skillDataBaseTranscendence = new SkillDataBaseTranscendence();
			SkillEnhancerLetThemCome skillEnhancerLetThemCome = new SkillEnhancerLetThemCome();
			heroDataBase.skillTree = new SkillTree(ulti, new SkillTreeNode[]
			{
				skillDataBaseShieldAll,
				skillDataBaseBlock,
				skillDataBaseRepel,
				skillDataBaseHoldYourGround,
				skillEnhancerMasterShielder
			}, new SkillTreeNode[]
			{
				skillDataBaseSlam,
				skillDataBaseManOfHill,
				skillDataBasePunishment,
				skillDataBaseTranscendence,
				skillEnhancerLetThemCome
			});
			GearData[] array = new GearData[]
			{
				new GearData
				{
					nameKey = "ITEM_NAME_GOLDEN_BRACER",
					universalBonusType = GearData.UniversalBonusType.GOLD,
					skillToLevelUp = skillDataBaseManOfHill
				},
				new GearData
				{
					nameKey = "ITEM_NAME_SPIT_POT",
					universalBonusType = GearData.UniversalBonusType.HEALTH,
					skillToLevelUp = skillDataBaseBlock
				},
				new GearData
				{
					nameKey = "ITEM_NAME_AXE_OF_REVENGE",
					universalBonusType = GearData.UniversalBonusType.DAMAGE,
					skillToLevelUp = skillDataBasePunishment
				}
			};
			foreach (GearData gearData in array)
			{
				gearData.belongsTo = heroDataBase;
				allGears.Add(gearData);
			}
			heroDataBase.height = 1.85f;
			heroDataBase.projectileTargetOffset = new Vector3(0f, 0.4f);
			heroDataBase.projectileTargetRandomness = 0.1f;
			heroDataBase.soundDeath = new SoundSimple(SoundArchieve.inst.samDeath, 1f);
			heroDataBase.soundRevive = new SoundSimple(SoundArchieve.inst.heroRevive, 1f);
			heroDataBase.soundVoDeath = new SoundVariedSimple(SoundArchieve.inst.voSamDeath, 1f);
			heroDataBase.soundVoRevive = new SoundVariedSimple(SoundArchieve.inst.voSamRevive, 1f);
			heroDataBase.soundVoSpawn = new SoundVariedSimple(SoundArchieve.inst.voSamSpawn, 1f);
			heroDataBase.soundVoLevelUp = new SoundVariedSimple(SoundArchieve.inst.voSamLevelUp, 1f);
			heroDataBase.soundVoItem = SoundArchieve.inst.voSamUpgradeItem;
			heroDataBase.soundVoWelcome = new SoundVariedSimple(SoundArchieve.inst.voSamWelcome, 1f);
			heroDataBase.soundVoEnvChange = new SoundVariedSimple(SoundArchieve.inst.voSamEnvChange, 1f);
			heroDataBase.soundVoSelected = SoundArchieve.inst.voSamSelected;
			heroDataBase.soundVoCheer = SoundArchieve.inst.voSamCheer;
			return heroDataBase;
		}

		public static HeroDataBase CreateBlindArcher(List<GearData> allGears)
		{
			HeroDataBase heroDataBase = new HeroDataBase();
			heroDataBase.id = "BLIND_ARCHER";
			heroDataBase.nameKey = "HERO_NAME_LIA";
			heroDataBase.descKey = "HERO_DESC_LIA";
			heroDataBase.healthMax = HeroConstants.STARTING_HEALTH * 0.699999988079071;
			heroDataBase.healthRegen = 0.0;
			heroDataBase.damageTakenFactor = 1.0;
			heroDataBase.damage = HeroConstants.STARTING_DAMAGE * 2.2000000476837158;
			heroDataBase.missChance = SkillDataBaseTargetPractice.BASE_MISS_CHANCE;
			heroDataBase.critChance = 0.03f;
			heroDataBase.critFactor = 2.0;
			heroDataBase.durRevive = 10f;
			heroDataBase.heroClass = HeroClass.ATTACKER;
			heroDataBase.ultiCatagory = HeroDataBase.UltiCatagory.ORANGE;
			WeaponWoodRandomRanged weaponWoodRandomRanged = new WeaponWoodRandomRanged();
			weaponWoodRandomRanged.id = 109;
			heroDataBase.weapon = weaponWoodRandomRanged;
			weaponWoodRandomRanged.SetTiming(HeroFactory.GD(37f), HeroFactory.GD(24f), 0.4f);
			weaponWoodRandomRanged.projectileType = Projectile.Type.BLIND_ARCHER_ATTACK;
			weaponWoodRandomRanged.targetType = Projectile.TargetType.SINGLE_ENEMY;
			weaponWoodRandomRanged.durFly = 0.45f;
			weaponWoodRandomRanged.projectilePath = new ProjectilePathBomb
			{
				heightAddMax = 0.35f
			};
			weaponWoodRandomRanged.impactSound = new SoundVariedSimple(SoundArchieve.inst.liaAttackImpacts, 1f);
			weaponWoodRandomRanged.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.40625f, new SoundVariedSimple(SoundArchieve.inst.liaAttacks, 1f))
			};
			SkillDataBaseTargetPractice ulti = new SkillDataBaseTargetPractice();
			SkillDataBaseSnipe skillDataBaseSnipe = new SkillDataBaseSnipe();
			SkillDataBaseFeedPoor skillDataBaseFeedPoor = new SkillDataBaseFeedPoor();
			SkillDataBaseBlindNotDeaf skillDataBaseBlindNotDeaf = new SkillDataBaseBlindNotDeaf();
			SkillDataBaseSurvivalist skillDataBaseSurvivalist = new SkillDataBaseSurvivalist();
			SkillDataBaseOneShot skillDataBaseOneShot = new SkillDataBaseOneShot();
			SkillDataBaseSwiftMoves skillDataBaseSwiftMoves = new SkillDataBaseSwiftMoves();
			SkillDataBaseAccuracy skillDataBaseAccuracy = new SkillDataBaseAccuracy();
			SkillDataBaseElegance skillDataBaseElegance = new SkillDataBaseElegance();
			SkillDataBaseTracker skillDataBaseTracker = new SkillDataBaseTracker();
			SkillDataBaseMultiShot skillDataBaseMultiShot = new SkillDataBaseMultiShot();
			heroDataBase.skillTree = new SkillTree(ulti, new SkillTreeNode[]
			{
				skillDataBaseSnipe,
				skillDataBaseFeedPoor,
				skillDataBaseBlindNotDeaf,
				skillDataBaseSurvivalist,
				skillDataBaseOneShot
			}, new SkillTreeNode[]
			{
				skillDataBaseSwiftMoves,
				skillDataBaseAccuracy,
				skillDataBaseElegance,
				skillDataBaseTracker,
				skillDataBaseMultiShot
			});
			GearData[] array = new GearData[]
			{
				new GearData
				{
					nameKey = "ITEM_NAME_GOLDEN_FRAME",
					universalBonusType = GearData.UniversalBonusType.GOLD,
					skillToLevelUp = skillDataBaseTracker
				},
				new GearData
				{
					nameKey = "ITEM_NAME_FISH_OIL",
					universalBonusType = GearData.UniversalBonusType.HEALTH,
					skillToLevelUp = skillDataBaseFeedPoor
				},
				new GearData
				{
					nameKey = "ITEM_NAME_TARGET_DUMMY",
					universalBonusType = GearData.UniversalBonusType.DAMAGE,
					skillToLevelUp = skillDataBaseBlindNotDeaf
				}
			};
			foreach (GearData gearData in array)
			{
				gearData.belongsTo = heroDataBase;
				allGears.Add(gearData);
			}
			heroDataBase.height = 1.7f;
			heroDataBase.projectileTargetOffset = new Vector3(0f, 0.5f);
			heroDataBase.projectileTargetRandomness = 0.15f;
			heroDataBase.soundDeath = new SoundSimple(SoundArchieve.inst.heroDeathCommon2, 1f);
			heroDataBase.soundRevive = new SoundSimple(SoundArchieve.inst.heroRevive, 1f);
			heroDataBase.soundVoDeath = new SoundVariedSimple(SoundArchieve.inst.voLiaDeath, 1f);
			heroDataBase.soundVoRevive = new SoundVariedSimple(SoundArchieve.inst.voLiaRevive, 1f);
			heroDataBase.soundVoSpawn = new SoundVariedSimple(SoundArchieve.inst.voLiaSpawn, 1f);
			heroDataBase.soundVoLevelUp = new SoundVariedSimple(SoundArchieve.inst.voLiaLevelUp, 1f);
			heroDataBase.soundVoItem = SoundArchieve.inst.voLiaUpgradeItem;
			heroDataBase.soundVoWelcome = new SoundVariedSimple(SoundArchieve.inst.voLiaWelcome, 1f);
			heroDataBase.soundVoEnvChange = new SoundVariedSimple(SoundArchieve.inst.voLiaEnvChange, 1f);
			heroDataBase.soundVoSelected = SoundArchieve.inst.voLiaSelected;
			heroDataBase.soundVoCheer = SoundArchieve.inst.voLiaCheer;
			return heroDataBase;
		}

		public static HeroDataBase CreateJim(List<GearData> allGears)
		{
			HeroDataBase heroDataBase = new HeroDataBase();
			heroDataBase.id = "JIM";
			heroDataBase.nameKey = "HERO_NAME_HANDSUM_JIM";
			heroDataBase.descKey = "HERO_DESC_HANDSUM_JIM";
			heroDataBase.healthMax = HeroConstants.STARTING_HEALTH * 0.800000011920929;
			heroDataBase.healthRegen = 0.0;
			heroDataBase.damageTakenFactor = 1.0;
			heroDataBase.damage = HeroConstants.STARTING_DAMAGE * 2.0999999046325684;
			heroDataBase.critChance = 0.03f;
			heroDataBase.critFactor = 2.0;
			heroDataBase.durRevive = 10f;
			heroDataBase.heroClass = HeroClass.SUPPORTER;
			heroDataBase.ultiCatagory = HeroDataBase.UltiCatagory.BLUE;
			WeaponWood weaponWood = new WeaponWood();
			weaponWood.id = 113;
			heroDataBase.weapon = weaponWood;
			weaponWood.SetTiming(HeroFactory.GD(35f), HeroFactory.GD(12f), 0.8f, 0f);
			weaponWood.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.1f, new SoundVariedSimple(SoundArchieve.inst.jimAttacks, 0.5f))
			};
			SkillDataBaseBittersweet ulti = new SkillDataBaseBittersweet();
			SkillDataBaseBattlecry skillDataBaseBattlecry = new SkillDataBaseBattlecry();
			SkillDataBaseLullaby skillDataBaseLullaby = new SkillDataBaseLullaby();
			SkillEnhancerBaseHeroism skillEnhancerBaseHeroism = new SkillEnhancerBaseHeroism();
			SkillDataBasePartyTime skillDataBasePartyTime = new SkillDataBasePartyTime();
			SkillDataBaseTogetherWeStand skillDataBaseTogetherWeStand = new SkillDataBaseTogetherWeStand();
			SkillDataBaseWeepingSong skillDataBaseWeepingSong = new SkillDataBaseWeepingSong();
			SkillEnhancerBaseDepression skillEnhancerBaseDepression = new SkillEnhancerBaseDepression();
			SkillEnhancerBaseNotSoFast skillEnhancerBaseNotSoFast = new SkillEnhancerBaseNotSoFast();
			SkillDataBasePrettyFace skillDataBasePrettyFace = new SkillDataBasePrettyFace();
			SkillDataBaseDividedWeFall skillDataBaseDividedWeFall = new SkillDataBaseDividedWeFall();
			heroDataBase.skillTree = new SkillTree(ulti, new SkillTreeNode[]
			{
				skillDataBaseBattlecry,
				skillDataBaseLullaby,
				skillEnhancerBaseHeroism,
				skillDataBasePartyTime,
				skillDataBaseTogetherWeStand
			}, new SkillTreeNode[]
			{
				skillDataBaseWeepingSong,
				skillEnhancerBaseDepression,
				skillEnhancerBaseNotSoFast,
				skillDataBasePrettyFace,
				skillDataBaseDividedWeFall
			});
			GearData[] array = new GearData[]
			{
				new GearData
				{
					nameKey = "ITEM_NAME_PANS_FLUTE",
					universalBonusType = GearData.UniversalBonusType.GOLD,
					skillToLevelUp = skillEnhancerBaseNotSoFast
				},
				new GearData
				{
					nameKey = "ITEM_NAME_JACKS_MASK",
					universalBonusType = GearData.UniversalBonusType.HEALTH,
					skillToLevelUp = skillDataBasePrettyFace
				},
				new GearData
				{
					nameKey = "ITEM_NAME_PEIXOTOS_GUITAR",
					universalBonusType = GearData.UniversalBonusType.DAMAGE,
					skillToLevelUp = skillEnhancerBaseHeroism
				}
			};
			foreach (GearData gearData in array)
			{
				gearData.belongsTo = heroDataBase;
				allGears.Add(gearData);
			}
			heroDataBase.height = 1.93f;
			heroDataBase.projectileTargetOffset = new Vector3(0f, 0.5f);
			heroDataBase.projectileTargetRandomness = 0.15f;
			heroDataBase.soundDeath = new SoundSimple(SoundArchieve.inst.heroDeathCommon2, 1f);
			heroDataBase.soundRevive = new SoundSimple(SoundArchieve.inst.heroRevive, 1f);
			heroDataBase.soundVoDeath = new SoundVariedSimple(SoundArchieve.inst.voJimDeath, 1f);
			heroDataBase.soundVoRevive = new SoundVariedSimple(SoundArchieve.inst.voJimRevive, 1f);
			heroDataBase.soundVoSpawn = new SoundVariedSimple(SoundArchieve.inst.voJimSpawn, 1f);
			heroDataBase.soundVoLevelUp = new SoundVariedSimple(SoundArchieve.inst.voJimLevelUp, 1f);
			heroDataBase.soundVoItem = SoundArchieve.inst.voJimUpgradeItem;
			heroDataBase.soundVoWelcome = new SoundVariedSimple(SoundArchieve.inst.voJimWelcome, 1f);
			heroDataBase.soundVoEnvChange = new SoundVariedSimple(SoundArchieve.inst.voJimEnvChange, 1f);
			heroDataBase.soundVoSelected = SoundArchieve.inst.voJimSelected;
			heroDataBase.soundVoCheer = SoundArchieve.inst.voJimCheer;
			return heroDataBase;
		}

		public static HeroDataBase CreateTam(List<GearData> allGears)
		{
			HeroDataBase heroDataBase = new HeroDataBase();
			heroDataBase.id = "TAM";
			heroDataBase.nameKey = "HERO_NAME_TAM";
			heroDataBase.descKey = "HERO_DESC_TAM";
			heroDataBase.healthMax = HeroConstants.STARTING_HEALTH * 1.1000000238418579;
			heroDataBase.healthRegen = 0.0;
			heroDataBase.damageTakenFactor = 1.0;
			heroDataBase.damage = HeroConstants.STARTING_DAMAGE * 2.0;
			heroDataBase.critChance = 0.03f;
			heroDataBase.critFactor = 2.0;
			heroDataBase.durRevive = 10f;
			heroDataBase.heroClass = HeroClass.SUPPORTER;
			heroDataBase.ultiCatagory = HeroDataBase.UltiCatagory.GREEN;
			WeaponLoaded weaponLoaded = new WeaponLoaded();
			weaponLoaded.id = 114;
			weaponLoaded.loadMax = 8;
			weaponLoaded.durReload = 2.8f;
			weaponLoaded.soundReload = new SoundSimple(SoundArchieve.inst.tamReload, 1f);
			heroDataBase.weapon = weaponLoaded;
			weaponLoaded.SetTiming(HeroFactory.GD(30f), new float[]
			{
				HeroFactory.GD(11f),
				HeroFactory.GD(19f)
			}, HeroFactory.GD(40f));
			weaponLoaded.soundsAttack = new List<TimedSound>();
			SkillDataBaseRoar ulti = new SkillDataBaseRoar();
			SkillDataBaseFlare skillDataBaseFlare = new SkillDataBaseFlare();
			SkillDataBaseMark skillDataBaseMark = new SkillDataBaseMark();
			SkillDataBasePreparation skillDataBasePreparation = new SkillDataBasePreparation();
			SkillDataBaseBandage skillDataBaseBandage = new SkillDataBaseBandage();
			SkillDataBaseFeignDeath skillDataBaseFeignDeath = new SkillDataBaseFeignDeath();
			SkillDataBaseCrowAttack skillDataBaseCrowAttack = new SkillDataBaseCrowAttack();
			SkillDataBaseFrenzy skillDataBaseFrenzy = new SkillDataBaseFrenzy();
			SkillEnhancerBaseInstincts skillEnhancerBaseInstincts = new SkillEnhancerBaseInstincts();
			SkillEnhancerBaseDeathFromAbove skillEnhancerBaseDeathFromAbove = new SkillEnhancerBaseDeathFromAbove();
			SkillDataBaseCallOfWild skillDataBaseCallOfWild = new SkillDataBaseCallOfWild();
			heroDataBase.skillTree = new SkillTree(ulti, new SkillTreeNode[]
			{
				skillDataBaseFlare,
				skillDataBaseMark,
				skillDataBasePreparation,
				skillDataBaseBandage,
				skillDataBaseFeignDeath
			}, new SkillTreeNode[]
			{
				skillDataBaseCrowAttack,
				skillDataBaseFrenzy,
				skillEnhancerBaseInstincts,
				skillEnhancerBaseDeathFromAbove,
				skillDataBaseCallOfWild
			});
			GearData[] array = new GearData[]
			{
				new GearData
				{
					nameKey = "ITEM_NAME_GOLDEN_FEATHER",
					universalBonusType = GearData.UniversalBonusType.GOLD,
					skillToLevelUp = skillEnhancerBaseInstincts
				},
				new GearData
				{
					nameKey = "ITEM_NAME_JUICY_MEAT",
					universalBonusType = GearData.UniversalBonusType.HEALTH,
					skillToLevelUp = skillDataBasePreparation
				},
				new GearData
				{
					nameKey = "ITEM_NAME_DEADLY_SHELLS",
					universalBonusType = GearData.UniversalBonusType.DAMAGE,
					skillToLevelUp = skillDataBaseFrenzy
				}
			};
			foreach (GearData gearData in array)
			{
				gearData.belongsTo = heroDataBase;
				allGears.Add(gearData);
			}
			heroDataBase.height = 1.91f;
			heroDataBase.projectileTargetOffset = new Vector3(0f, 0.5f);
			heroDataBase.projectileTargetRandomness = 0.15f;
			heroDataBase.soundDeath = new SoundSimple(SoundArchieve.inst.heroDeathCommon2, 1f);
			heroDataBase.soundRevive = new SoundSimple(SoundArchieve.inst.heroRevive, 1f);
			heroDataBase.soundVoDeath = new SoundVariedSimple(SoundArchieve.inst.voTamDeath, 1f);
			heroDataBase.soundVoRevive = new SoundVariedSimple(SoundArchieve.inst.voTamRevive, 1f);
			heroDataBase.soundVoSpawn = new SoundVariedSimple(SoundArchieve.inst.voTamSpawn, 1f);
			heroDataBase.soundVoLevelUp = new SoundVariedSimple(SoundArchieve.inst.voTamLevelUp, 1f);
			heroDataBase.soundVoItem = SoundArchieve.inst.voTamUpgradeItem;
			heroDataBase.soundVoWelcome = new SoundVariedSimple(SoundArchieve.inst.voTamWelcome, 1f);
			heroDataBase.soundVoEnvChange = new SoundVariedSimple(SoundArchieve.inst.voTamEnvChange, 1f);
			heroDataBase.soundVoSelected = SoundArchieve.inst.voTamSelected;
			heroDataBase.soundVoCheer = SoundArchieve.inst.voTamCheer;
			return heroDataBase;
		}

		public static HeroDataBase CreateWarlock(List<GearData> allGears)
		{
			HeroDataBase heroDataBase = new HeroDataBase();
			heroDataBase.id = "WARLOCK";
			heroDataBase.nameKey = "HERO_NAME_WARLOCK";
			heroDataBase.descKey = "HERO_DESC_WARLOCK";
			heroDataBase.healthMax = HeroConstants.STARTING_HEALTH * 0.800000011920929;
			heroDataBase.healthRegen = 0.0;
			heroDataBase.damageTakenFactor = 1.0;
			heroDataBase.damage = HeroConstants.STARTING_DAMAGE * 2.5;
			heroDataBase.critChance = 0.03f;
			heroDataBase.critFactor = 2.0;
			heroDataBase.durRevive = 10f;
			heroDataBase.heroClass = HeroClass.ATTACKER;
			heroDataBase.ultiCatagory = HeroDataBase.UltiCatagory.RED;
			WeaponWoodRanged weaponWoodRanged = new WeaponWoodRanged();
			Weapon weapon = weaponWoodRanged;
			int[] array = new int[4];
			array[2] = 1;
			weapon.projectileIndexPattern = array;
			weaponWoodRanged.id = 115;
			heroDataBase.weapon = weaponWoodRanged;
			weaponWoodRanged.SetTiming(HeroFactory.GD(35f), HeroFactory.GD(22f), 0.833f);
			weaponWoodRanged.projectileType = Projectile.Type.WARLOCK_ATTACK;
			weaponWoodRanged.targetType = Projectile.TargetType.SINGLE_ENEMY;
			weaponWoodRanged.durFly = 0.2f;
			ProjectilePathLinear projectilePath = new ProjectilePathLinear();
			weaponWoodRanged.projectilePath = projectilePath;
			weaponWoodRanged.impactSound = new SoundVariedSimple(SoundArchieve.inst.warlockAttackImpacts, 0.8f);
			weaponWoodRanged.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.09090909f, new SoundVariedSimple(SoundArchieve.inst.warlockAttacks, 1f))
			};
			SkillDataBaseDarkRitual ulti = new SkillDataBaseDarkRitual();
			SkillDataBaseDemonicSwarm skillDataBaseDemonicSwarm = new SkillDataBaseDemonicSwarm();
			SkillDataBaseSoulSacrifice skillDataBaseSoulSacrifice = new SkillDataBaseSoulSacrifice();
			SkillDataBaseTasteOfRevege skillDataBaseTasteOfRevege = new SkillDataBaseTasteOfRevege();
			SkillEnhancerChakraBooster skillEnhancerChakraBooster = new SkillEnhancerChakraBooster();
			SkillEnhancerWarmerSwarm skillEnhancerWarmerSwarm = new SkillEnhancerWarmerSwarm();
			SkillDataBaseRegret skillDataBaseRegret = new SkillDataBaseRegret();
			SkillDataBaseTerror skillDataBaseTerror = new SkillDataBaseTerror();
			SkillDataBaseStrikeMirror skillDataBaseStrikeMirror = new SkillDataBaseStrikeMirror();
			SkillDataBaseFeelsBetter skillDataBaseFeelsBetter = new SkillDataBaseFeelsBetter();
			SkillEnhancerMyPittyTeam skillEnhancerMyPittyTeam = new SkillEnhancerMyPittyTeam();
			heroDataBase.skillTree = new SkillTree(ulti, new SkillTreeNode[]
			{
				skillDataBaseDemonicSwarm,
				skillDataBaseSoulSacrifice,
				skillDataBaseTasteOfRevege,
				skillEnhancerChakraBooster,
				skillEnhancerWarmerSwarm
			}, new SkillTreeNode[]
			{
				skillDataBaseRegret,
				skillDataBaseTerror,
				skillDataBaseStrikeMirror,
				skillDataBaseFeelsBetter,
				skillEnhancerMyPittyTeam
			});
			GearData[] array2 = new GearData[]
			{
				new GearData
				{
					nameKey = "ITEM_NAME_GOLD_AMULET",
					universalBonusType = GearData.UniversalBonusType.GOLD,
					skillToLevelUp = skillDataBaseTasteOfRevege
				},
				new GearData
				{
					nameKey = "ITEM_NAME_FRIENDLY_SKULL",
					universalBonusType = GearData.UniversalBonusType.HEALTH,
					skillToLevelUp = skillDataBaseSoulSacrifice
				},
				new GearData
				{
					nameKey = "ITEM_NAME_VILE_OF_VILENESS",
					universalBonusType = GearData.UniversalBonusType.DAMAGE,
					skillToLevelUp = skillDataBaseStrikeMirror
				}
			};
			foreach (GearData gearData in array2)
			{
				gearData.belongsTo = heroDataBase;
				allGears.Add(gearData);
			}
			heroDataBase.height = 1.9f;
			heroDataBase.projectileTargetOffset = new Vector3(0f, 0.5f);
			heroDataBase.projectileTargetRandomness = 0.15f;
			heroDataBase.soundDeath = new SoundSimple(SoundArchieve.inst.warlockDeath, 1f);
			heroDataBase.soundRevive = new SoundSimple(SoundArchieve.inst.heroRevive, 1f);
			heroDataBase.soundVoDeath = new SoundVariedSimple(SoundArchieve.inst.voWarlockDeath, 1f);
			heroDataBase.soundVoRevive = new SoundVariedSimple(SoundArchieve.inst.voWarlockRevive, 1f);
			heroDataBase.soundVoSpawn = new SoundVariedSimple(SoundArchieve.inst.voWarlockSpawn, 1f);
			heroDataBase.soundVoLevelUp = new SoundVariedSimple(SoundArchieve.inst.voWarlockLevelUp, 1f);
			heroDataBase.soundVoItem = SoundArchieve.inst.voWarlockUpgradeItem;
			heroDataBase.soundVoWelcome = new SoundVariedSimple(SoundArchieve.inst.voWarlockWelcome, 1f);
			heroDataBase.soundVoEnvChange = new SoundVariedSimple(SoundArchieve.inst.voWarlockEnvChange, 1f);
			heroDataBase.soundVoSelected = SoundArchieve.inst.voWarlockSelected;
			heroDataBase.soundVoCheer = SoundArchieve.inst.voWarlockCheer;
			return heroDataBase;
		}

		public static HeroDataBase CreateGoblin(List<GearData> allGears)
		{
			HeroDataBase heroDataBase = new HeroDataBase();
			heroDataBase.id = "GOBLIN";
			heroDataBase.nameKey = "HERO_NAME_GOBLIN";
			heroDataBase.descKey = "HERO_DESC_GOBLIN";
			heroDataBase.healthMax = HeroConstants.STARTING_HEALTH * 0.800000011920929;
			heroDataBase.healthRegen = 0.0;
			heroDataBase.damageTakenFactor = 1.0;
			heroDataBase.damage = HeroConstants.STARTING_DAMAGE * 1.7999999523162842;
			heroDataBase.critChance = 0.03f;
			heroDataBase.critFactor = 2.0;
			heroDataBase.durRevive = 10f;
			heroDataBase.heroClass = HeroClass.SUPPORTER;
			heroDataBase.ultiCatagory = HeroDataBase.UltiCatagory.GREEN;
			WeaponWoodRandomRanged weaponWoodRandomRanged = new WeaponWoodRandomRanged();
			heroDataBase.weapon = weaponWoodRandomRanged;
			weaponWoodRandomRanged.id = 116;
			weaponWoodRandomRanged.SetTiming(HeroFactory.GD(48f), HeroFactory.GD(16f), HeroFactory.GD(27f));
			weaponWoodRandomRanged.projectileType = Projectile.Type.GOBLIN_SACK;
			weaponWoodRandomRanged.targetType = Projectile.TargetType.SINGLE_ENEMY;
			weaponWoodRandomRanged.durFly = 0.4f;
			weaponWoodRandomRanged.projectilePath = new ProjectilePathBomb
			{
				heightAddMax = 0.2f
			};
			weaponWoodRandomRanged.areaDamageRatio = 0.5;
			weaponWoodRandomRanged.areaDamageRadius = 0f;
			weaponWoodRandomRanged.impactSound = new SoundVariedSimple(SoundArchieve.inst.goblinAttackImpacts, 0.5f);
			weaponWoodRandomRanged.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.166666672f, new SoundVariedSimple(SoundArchieve.inst.goblinAttacks, 0.65f))
			};
			SkillDataBaseGreedGrenade ulti = new SkillDataBaseGreedGrenade();
			SkillDataBaseNegotiate skillDataBaseNegotiate = new SkillDataBaseNegotiate();
			SkillDataBaseFormerFriends skillDataBaseFormerFriends = new SkillDataBaseFormerFriends();
			SkillDataBaseEasyTargets skillDataBaseEasyTargets = new SkillDataBaseEasyTargets();
			SkillDataBaseConfusingPresence skillDataBaseConfusingPresence = new SkillDataBaseConfusingPresence();
			SkillEnhancerBaseDistraction skillEnhancerBaseDistraction = new SkillEnhancerBaseDistraction();
			SkillDataBaseCommonAffinities skillDataBaseCommonAffinities = new SkillDataBaseCommonAffinities();
			SkillDataBaseTradeSecret skillDataBaseTradeSecret = new SkillDataBaseTradeSecret();
			SkillDataBaseKeenNose skillDataBaseKeenNose = new SkillDataBaseKeenNose();
			SkillDataBaseTimidFriends skillDataBaseTimidFriends = new SkillDataBaseTimidFriends();
			SkillDataBaseLooseChange skillDataBaseLooseChange = new SkillDataBaseLooseChange();
			heroDataBase.skillTree = new SkillTree(ulti, new SkillTreeNode[]
			{
				skillDataBaseNegotiate,
				skillDataBaseFormerFriends,
				skillDataBaseEasyTargets,
				skillDataBaseConfusingPresence,
				skillEnhancerBaseDistraction
			}, new SkillTreeNode[]
			{
				skillDataBaseCommonAffinities,
				skillDataBaseTradeSecret,
				skillDataBaseKeenNose,
				skillDataBaseTimidFriends,
				skillDataBaseLooseChange
			});
			GearData[] array = new GearData[]
			{
				new GearData
				{
					nameKey = "ITEM_NAME_GOLDEN_KEY",
					universalBonusType = GearData.UniversalBonusType.GOLD,
					skillToLevelUp = skillDataBaseTradeSecret
				},
				new GearData
				{
					nameKey = "ITEM_NAME_SPARE_LEG",
					universalBonusType = GearData.UniversalBonusType.HEALTH,
					skillToLevelUp = skillDataBaseConfusingPresence
				},
				new GearData
				{
					nameKey = "ITEM_NAME_SPY_LENS",
					universalBonusType = GearData.UniversalBonusType.DAMAGE,
					skillToLevelUp = skillDataBaseFormerFriends
				}
			};
			foreach (GearData gearData in array)
			{
				gearData.belongsTo = heroDataBase;
				allGears.Add(gearData);
			}
			heroDataBase.height = 1.8f;
			heroDataBase.projectileTargetOffset = new Vector3(0f, 0.45f);
			heroDataBase.projectileTargetRandomness = 0.12f;
			heroDataBase.soundDeath = new SoundSimple(SoundArchieve.inst.heroDeathCommon3, 1f);
			heroDataBase.soundRevive = new SoundSimple(SoundArchieve.inst.heroRevive, 1f);
			heroDataBase.soundVoDeath = new SoundVariedSimple(SoundArchieve.inst.voGoblinDeath, 1f);
			heroDataBase.soundVoRevive = new SoundVariedSimple(SoundArchieve.inst.voGoblinRevive, 1f);
			heroDataBase.soundVoSpawn = new SoundVariedSimple(SoundArchieve.inst.voGoblinSpawn, 1f);
			heroDataBase.soundVoLevelUp = new SoundVariedSimple(SoundArchieve.inst.voGoblinLevelUp, 1f);
			heroDataBase.soundVoItem = SoundArchieve.inst.voGoblinUpgradeItem;
			heroDataBase.soundVoWelcome = new SoundVariedSimple(SoundArchieve.inst.voGoblinWelcome, 1f);
			heroDataBase.soundVoEnvChange = new SoundVariedSimple(SoundArchieve.inst.voGoblinEnvChange, 1f);
			heroDataBase.soundVoSelected = SoundArchieve.inst.voGoblinSelected;
			heroDataBase.soundVoCheer = SoundArchieve.inst.voGoblinCheer;
			return heroDataBase;
		}

		public static HeroDataBase CreateBabu(List<GearData> allGears)
		{
			HeroDataBase heroDataBase = new HeroDataBase();
			heroDataBase.id = "BABU";
			heroDataBase.nameKey = "HERO_NAME_BABU";
			heroDataBase.descKey = "HERO_DESC_BABU";
			heroDataBase.healthMax = HeroConstants.STARTING_HEALTH * 2.2000000476837158;
			heroDataBase.healthRegen = 0.0;
			heroDataBase.damageTakenFactor = 1.0;
			heroDataBase.damage = HeroConstants.STARTING_DAMAGE * 1.7999999523162842;
			heroDataBase.critChance = 0.03f;
			heroDataBase.critFactor = 2.0;
			heroDataBase.durRevive = 10f;
			heroDataBase.heroClass = HeroClass.DEFENDER;
			heroDataBase.ultiCatagory = HeroDataBase.UltiCatagory.ORANGE;
			WeaponWoodRanged weaponWoodRanged = new WeaponWoodRanged();
			weaponWoodRanged.projectileIndexPattern = new int[3];
			weaponWoodRanged.id = 119;
			weaponWoodRanged.SetTiming(HeroFactory.GD(30f), 0.41f, 0.833f);
			weaponWoodRanged.projectileType = Projectile.Type.BABU_SOUP;
			weaponWoodRanged.targetType = Projectile.TargetType.SINGLE_ENEMY;
			weaponWoodRanged.impactVisualEffect = new VisualEffect(VisualEffect.Type.BABU_SOUP, 0.933f);
			weaponWoodRanged.durFly = 0.6f;
			weaponWoodRanged.projectilePath = new ProjectilePathBomb
			{
				heightAddMax = 0.6f
			};
			weaponWoodRanged.impactSound = new SoundVariedSimple(SoundArchieve.inst.babuAttackImpacts, 0.8f);
			weaponWoodRanged.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0f, new SoundSimple(SoundArchieve.inst.babuAttacks[0], 1f))
			};
			WeaponWood weaponWood = new WeaponWood();
			weaponWood.id = 118;
			weaponWood.SetTiming(HeroFactory.GD(35f), HeroFactory.GD(16f), 1f, 0f);
			weaponWood.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.09090909f, new SoundSimple(SoundArchieve.inst.babuAttacks[1], 1f))
			};
			WeaponSwitched weaponSwitched = new WeaponSwitched();
			WeaponSwitched weaponSwitched2 = weaponSwitched;
			int[] array = new int[3];
			array[0] = 1;
			array[1] = 1;
			weaponSwitched2.switchPattern = array;
			weaponSwitched.weapon_1 = weaponWoodRanged;
			weaponSwitched.weapon_2 = weaponWood;
			weaponSwitched.id = 117;
			heroDataBase.weapon = weaponSwitched;
			SkillDataBaseTakeOneForTheTeam ulti = new SkillDataBaseTakeOneForTheTeam();
			SkillDataBaseGoodCupOfTea skillDataBaseGoodCupOfTea = new SkillDataBaseGoodCupOfTea();
			SkillDataBaseToughLove skillDataBaseToughLove = new SkillDataBaseToughLove();
			SkillDataBaseMoraleBooster skillDataBaseMoraleBooster = new SkillDataBaseMoraleBooster();
			SkillDataBaseParticipationTrophy skillDataBaseParticipationTrophy = new SkillDataBaseParticipationTrophy();
			SkillEnhancerLongWinded skillEnhancerLongWinded = new SkillEnhancerLongWinded();
			SkillDataBaseGotYourBack skillDataBaseGotYourBack = new SkillDataBaseGotYourBack();
			SkillDataBaseBrushItOff skillDataBaseBrushItOff = new SkillDataBaseBrushItOff();
			SkillDataBaseInnerWorth skillDataBaseInnerWorth = new SkillDataBaseInnerWorth();
			SkillDataBaseShareTheBurden skillDataBaseShareTheBurden = new SkillDataBaseShareTheBurden();
			SkillDataBaseExpertLoveLore skillDataBaseExpertLoveLore = new SkillDataBaseExpertLoveLore();
			heroDataBase.skillTree = new SkillTree(ulti, new SkillTreeNode[]
			{
				skillDataBaseGoodCupOfTea,
				skillDataBaseToughLove,
				skillDataBaseMoraleBooster,
				skillDataBaseParticipationTrophy,
				skillEnhancerLongWinded
			}, new SkillTreeNode[]
			{
				skillDataBaseGotYourBack,
				skillDataBaseBrushItOff,
				skillDataBaseInnerWorth,
				skillDataBaseShareTheBurden,
				skillDataBaseExpertLoveLore
			});
			GearData[] array2 = new GearData[]
			{
				new GearData
				{
					nameKey = "ITEM_NAME_FAVORITE_TEA_CUP",
					universalBonusType = GearData.UniversalBonusType.GOLD,
					skillToLevelUp = skillDataBaseInnerWorth
				},
				new GearData
				{
					nameKey = "ITEM_NAME_HERILOOM_SPICES",
					universalBonusType = GearData.UniversalBonusType.HEALTH,
					skillToLevelUp = skillDataBaseToughLove
				},
				new GearData
				{
					nameKey = "ITEM_NAME_SENSIBLE_SHOES",
					universalBonusType = GearData.UniversalBonusType.DAMAGE,
					skillToLevelUp = skillDataBaseMoraleBooster
				}
			};
			foreach (GearData gearData in array2)
			{
				gearData.belongsTo = heroDataBase;
				allGears.Add(gearData);
			}
			heroDataBase.height = 1.8f;
			heroDataBase.projectileTargetOffset = new Vector3(0f, 0.45f);
			heroDataBase.projectileTargetRandomness = 0.12f;
			heroDataBase.soundDeath = new SoundSimple(SoundArchieve.inst.heroDeathCommon3, 1f);
			heroDataBase.soundRevive = new SoundSimple(SoundArchieve.inst.heroRevive, 1f);
			heroDataBase.soundVoDeath = new SoundVariedSimple(SoundArchieve.inst.voBabuDeath, 1f);
			heroDataBase.soundVoRevive = new SoundVariedSimple(SoundArchieve.inst.voBabuRevive, 1f);
			heroDataBase.soundVoSpawn = new SoundVariedSimple(SoundArchieve.inst.voBabuSpawn, 1f);
			heroDataBase.soundVoLevelUp = new SoundVariedSimple(SoundArchieve.inst.voBabuLevelUp, 1f);
			heroDataBase.soundVoItem = SoundArchieve.inst.voBabuUpgradeItem;
			heroDataBase.soundVoWelcome = new SoundVariedSimple(SoundArchieve.inst.voBabuWelcome, 1f);
			heroDataBase.soundVoEnvChange = new SoundVariedSimple(SoundArchieve.inst.voBabuEnvChange, 1f);
			heroDataBase.soundVoSelected = SoundArchieve.inst.voBabuSelected;
			heroDataBase.soundVoCheer = SoundArchieve.inst.voBabuCheer;
			return heroDataBase;
		}

		public static HeroDataBase CreateDruid(List<GearData> allGears)
		{
			HeroDataBase heroDataBase = new HeroDataBase();
			heroDataBase.id = "DRUID";
			heroDataBase.nameKey = "HERO_NAME_RON";
			heroDataBase.descKey = "HERO_DESC_RON";
			heroDataBase.healthMax = HeroConstants.STARTING_HEALTH * 0.89999997615814209;
			heroDataBase.healthRegen = 0.0;
			heroDataBase.damageTakenFactor = 1.0;
			heroDataBase.damage = HeroConstants.STARTING_DAMAGE * 2.2999999523162842;
			heroDataBase.critChance = 0.03f;
			heroDataBase.critFactor = 2.0;
			heroDataBase.durRevive = 10f;
			heroDataBase.heroClass = HeroClass.ATTACKER;
			heroDataBase.ultiCatagory = HeroDataBase.UltiCatagory.GREEN;
			WeaponWood weaponWood = new WeaponWood();
			weaponWood.id = 120;
			heroDataBase.weapon = weaponWood;
			weaponWood.SetTiming(1.4f, 0.4f, 0.4f, 0f);
			weaponWood.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.166666672f, new SoundVariedMultiple(0.65f, new SoundVariedMultiple.SoundsInfo[]
				{
					new SoundVariedMultiple.SoundsInfo
					{
						expectedResult = 0,
						module = 1,
						sounds = new SoundVariedSimple(SoundArchieve.inst.druidPawsAttacks, 1f),
						variationOffset = 0
					},
					new SoundVariedMultiple.SoundsInfo
					{
						expectedResult = 0,
						module = 10,
						sounds = new SoundVariedSimple(SoundArchieve.inst.druidTailAttacks, 1f),
						variationOffset = 1
					}
				}))
			};
			SkillDataBaseBeastMode ulti = new SkillDataBaseBeastMode();
			SkillDataBaseStampede skillDataBaseStampede = new SkillDataBaseStampede();
			SkillEnhancerStrengthInNumbers skillEnhancerStrengthInNumbers = new SkillEnhancerStrengthInNumbers();
			SkillEnhancerMassivePaws skillEnhancerMassivePaws = new SkillEnhancerMassivePaws();
			SkillDataBaseGoForTheEyes skillDataBaseGoForTheEyes = new SkillDataBaseGoForTheEyes();
			SkillDataBaseImpulsive skillDataBaseImpulsive = new SkillDataBaseImpulsive();
			SkillDataBaseLarry skillDataBaseLarry = new SkillDataBaseLarry();
			SkillEnhancerHunterInstinct skillEnhancerHunterInstinct = new SkillEnhancerHunterInstinct();
			SkillEnhancerCurly skillEnhancerCurly = new SkillEnhancerCurly();
			SkillEnhancerMoe skillEnhancerMoe = new SkillEnhancerMoe();
			SkillEnhancerRageDriven skillEnhancerRageDriven = new SkillEnhancerRageDriven();
			heroDataBase.skillTree = new SkillTree(ulti, new SkillTreeNode[]
			{
				skillDataBaseStampede,
				skillEnhancerStrengthInNumbers,
				skillEnhancerMassivePaws,
				skillDataBaseGoForTheEyes,
				skillDataBaseImpulsive
			}, new SkillTreeNode[]
			{
				skillDataBaseLarry,
				skillEnhancerHunterInstinct,
				skillEnhancerCurly,
				skillEnhancerMoe,
				skillEnhancerRageDriven
			});
			GearData[] array = new GearData[]
			{
				new GearData
				{
					nameKey = "ITEM_NAME_JEWLED_CLAW",
					universalBonusType = GearData.UniversalBonusType.GOLD,
					skillToLevelUp = skillDataBaseImpulsive
				},
				new GearData
				{
					nameKey = "ITEM_NAME_CRITTER_KIBBLE",
					universalBonusType = GearData.UniversalBonusType.HEALTH,
					skillToLevelUp = skillEnhancerCurly
				},
				new GearData
				{
					nameKey = "ITEM_NAME_BARELY_USED_TOY",
					universalBonusType = GearData.UniversalBonusType.DAMAGE,
					skillToLevelUp = skillEnhancerHunterInstinct
				}
			};
			foreach (GearData gearData in array)
			{
				gearData.belongsTo = heroDataBase;
				allGears.Add(gearData);
			}
			heroDataBase.height = 1.8f;
			heroDataBase.projectileTargetOffset = new Vector3(0f, 0.45f);
			heroDataBase.projectileTargetRandomness = 0.12f;
			heroDataBase.soundDeath = new SoundSimple(SoundArchieve.inst.heroDeathCommon3, 1f);
			heroDataBase.soundRevive = new SoundSimple(SoundArchieve.inst.heroRevive, 1f);
			heroDataBase.soundVoDeath = new SoundVariedSimple(SoundArchieve.inst.voDruidDeath, 1f);
			heroDataBase.soundVoRevive = new SoundVariedSimple(SoundArchieve.inst.voDruidRevive, 1f);
			heroDataBase.soundVoSpawn = new SoundVariedSimple(SoundArchieve.inst.voDruidSpawn, 1f);
			heroDataBase.soundVoLevelUp = new SoundVariedSimple(SoundArchieve.inst.voDruidLevelUp, 1f);
			heroDataBase.soundVoItem = SoundArchieve.inst.voDruidUpgradeItem;
			heroDataBase.soundVoWelcome = new SoundVariedSimple(SoundArchieve.inst.voDruidWelcome, 1f);
			heroDataBase.soundVoEnvChange = new SoundVariedSimple(SoundArchieve.inst.voDruidEnvChange, 1f);
			heroDataBase.soundVoSelected = SoundArchieve.inst.voDruidSelected;
			heroDataBase.soundVoCheer = SoundArchieve.inst.voDruidCheer;
			return heroDataBase;
		}

		private const float ANIM_FPS = 30f;
	}
}
