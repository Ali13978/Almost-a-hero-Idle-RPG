using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class EnemyFactory
	{
    static Dictionary<string, int> _003C_003Ef__switch_0024mapB;
		public static EnemyDataBase CreateEnemy(string name, bool isRift = false)
		{
			if (name != null)
			{
				if (EnemyFactory._003C_003Ef__switch_0024mapB == null)
				{
					EnemyFactory._003C_003Ef__switch_0024mapB = new Dictionary<string, int>(20)
					{
						{
							"SEMI CORRUPTED HUMAN",
							0
						},
						{
							"BANDIT",
							1
						},
						{
							"CORRUPTED HUMAN",
							2
						},
						{
							"BOSS HUMAN",
							3
						},
						{
							"BOSS",
							4
						},
						{
							"WOLF",
							5
						},
						{
							"CORRUPTED ELF",
							6
						},
						{
							"SEMI CORRUPTED ELF",
							7
						},
						{
							"BOSS ELF",
							8
						},
						{
							"SEMI CORRUPTED DWARF",
							9
						},
						{
							"CORRUPTED DWARF",
							10
						},
						{
							"BOSS DWARF",
							11
						},
						{
							"BAT",
							12
						},
						{
							"SPIDER",
							13
						},
						{
							"MANGOLIES",
							14
						},
						{
							"BOSS MANGOLIES",
							15
						},
						{
							"CHEST",
							16
						},
						{
							"BOSS WISE SNAKE",
							17
						},
						{
							"SNAKE",
							18
						},
						{
							"BOSS SNOWMAN",
							19
						}
					};
				}
				int num;
				if (EnemyFactory._003C_003Ef__switch_0024mapB.TryGetValue(name, out num))
				{
					EnemyDataBase enemyDataBase;
					switch (num)
					{
					case 0:
						enemyDataBase = EnemyFactory.CreateHumanSemiCorrupted();
						break;
					case 1:
						enemyDataBase = EnemyFactory.CreateRegularBandit();
						break;
					case 2:
						enemyDataBase = EnemyFactory.CreateHumanCorrupted();
						break;
					case 3:
						enemyDataBase = EnemyFactory.CreateBossHuman();
						break;
					case 4:
						enemyDataBase = EnemyFactory.CreateBossOrc();
						break;
					case 5:
						enemyDataBase = EnemyFactory.CreateRegularWolf();
						break;
					case 6:
						enemyDataBase = EnemyFactory.CreateElfCorrupted();
						break;
					case 7:
						enemyDataBase = EnemyFactory.CreateElfSemiCorrupted();
						break;
					case 8:
						enemyDataBase = EnemyFactory.CreateBossElf();
						break;
					case 9:
						enemyDataBase = EnemyFactory.CreateDwarfSemiCorrupted();
						break;
					case 10:
						enemyDataBase = EnemyFactory.CreateDwarfCorrupted();
						break;
					case 11:
						enemyDataBase = EnemyFactory.CreateBossDwarf();
						break;
					case 12:
						enemyDataBase = EnemyFactory.CreateRegularBat();
						break;
					case 13:
						enemyDataBase = EnemyFactory.CreateRegularSpider();
						break;
					case 14:
						enemyDataBase = EnemyFactory.CreateRegularMagolies();
						break;
					case 15:
						enemyDataBase = EnemyFactory.CreateEpicBossMagolis();
						break;
					case 16:
						enemyDataBase = EnemyFactory.CreateChest();
						break;
					case 17:
						enemyDataBase = EnemyFactory.CreateBossWiseSnake();
						break;
					case 18:
						enemyDataBase = EnemyFactory.CreateSnake();
						break;
					case 19:
						enemyDataBase = EnemyFactory.CreateBossSnowman();
						break;
					case 20:
						goto IL_266;
					default:
						goto IL_266;
					}
					if ((isRift && enemyDataBase.type == EnemyDataBase.Type.EPIC) || enemyDataBase.type == EnemyDataBase.Type.BOSS)
					{
						enemyDataBase.type = EnemyDataBase.Type.REGULAR;
						if (name == "BOSS MANGOLIES")
						{
							enemyDataBase.scale = 0.6f;
						}
						else if (name != "BOSS WISE SNAKE")
						{
							enemyDataBase.scale = 0.75f;
						}
					}
					return enemyDataBase;
				}
			}
			IL_266:
			throw new NotImplementedException();
		}

		public static EnemyDataBase CreateHumanSemiCorrupted()
		{
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "SEMI CORRUPTED HUMAN";
			enemyDataBase.type = EnemyDataBase.Type.REGULAR;
			enemyDataBase.deathEffectScale = 0.6f;
			enemyDataBase.healthMax = 40.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.damage = 4.0;
			enemyDataBase.durAttackActive = 1.16666663f;
			enemyDataBase.durAttackWait = 1.6f;
			enemyDataBase.timeDamage = 0.6f;
			enemyDataBase.durSpawnTranslate = 2f;
			enemyDataBase.durSpawnDrop = 0.5f;
			enemyDataBase.durCorpse = 1.06666672f;
			enemyDataBase.deathEffectTime = 0.8f;
			enemyDataBase.goldToDrop = 6.0;
			enemyDataBase.spawnWeight = 2f;
			enemyDataBase.spawnProb = 1f;
			enemyDataBase.height = 1.7f;
			enemyDataBase.projectileTargetOffset = new Vector3(-0.35f, 0.75f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.humanSemiCorruptedSpawns, 1f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.humanSemiCorruptedDeaths, 1f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.514285743f, new SoundVariedSimple(SoundArchieve.inst.humanSemiCorruptedDamages, 1f))
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateRegularBandit()
		{
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "BANDIT";
			enemyDataBase.type = EnemyDataBase.Type.REGULAR;
			enemyDataBase.deathEffectScale = 0.6f;
			enemyDataBase.healthMax = 20.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.damage = 1.6;
			enemyDataBase.durAttackActive = 0.8f;
			enemyDataBase.durAttackWait = 1.35f;
			enemyDataBase.timeDamage = 0.433333337f;
			enemyDataBase.durSpawnTranslate = 0.5f;
			enemyDataBase.durSpawnDrop = 0.35f;
			enemyDataBase.durSpawnDropTranslate = 0.25f;
			enemyDataBase.durCorpse = 0.9f;
			enemyDataBase.deathEffectTime = 0.333333343f;
			enemyDataBase.goldToDrop = 3.0;
			enemyDataBase.spawnWeight = 0.6f;
			enemyDataBase.spawnProb = 0.85f;
			enemyDataBase.height = 1.7f;
			enemyDataBase.projectileTargetOffset = new Vector3(-0.35f, 0.75f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.banditSpawns, 1f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.banditDeaths, 1f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0f, new SoundVariedSimple(SoundArchieve.inst.banditDamages, 1f))
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateHumanCorrupted()
		{
			Projectile projectile = new Projectile();
			projectile.type = Projectile.Type.HUMAN_CORRUPTED;
			projectile.targetType = Projectile.TargetType.SINGLE_ALLY_ANY;
			projectile.durFly = 0.8f;
			projectile.visualEffect = new VisualEffect(VisualEffect.Type.BOMBERMAN_DINAMIT, 0.2f);
			projectile.path = new ProjectilePathBomb
			{
				heightAddMax = 0.32f
			};
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "CORRUPTED HUMAN";
			enemyDataBase.type = EnemyDataBase.Type.REGULAR;
			enemyDataBase.deathEffectScale = 0.6f;
			enemyDataBase.soundImpact = new SoundVariedSimple(SoundArchieve.inst.bombermanAttackImpacts, 0.6f);
			enemyDataBase.healthMax = 75.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.damage = 3.5;
			enemyDataBase.projectile = projectile;
			enemyDataBase.durAttackActive = 1.33333337f;
			enemyDataBase.durAttackWait = 1.25f;
			enemyDataBase.timeDamage = 0.6666667f;
			enemyDataBase.durSpawnTranslate = 0.3f;
			enemyDataBase.durSpawnDrop = 0.9f;
			enemyDataBase.durCorpse = 1.2f;
			enemyDataBase.deathEffectTime = 0.933333337f;
			enemyDataBase.goldToDrop = 9.0;
			enemyDataBase.spawnWeight = 3.5f;
			enemyDataBase.spawnProb = 0.7f;
			enemyDataBase.height = 1.7f;
			enemyDataBase.projectileTargetOffset = new Vector3(-0.35f, 0.75f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.humanCorruptedSpawns, 1f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.humanCorruptedDeaths, 1f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.514285743f, new SoundVariedSimple(SoundArchieve.inst.humanCorruptedDamages, 1f))
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateBossHuman()
		{
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "BOSS HUMAN";
			enemyDataBase.type = EnemyDataBase.Type.BOSS;
			enemyDataBase.deathEffectScale = 1.5f;
			enemyDataBase.scaleBuffVisual = 1.5f;
			enemyDataBase.healthMax = 500.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.damage = 10.0;
			enemyDataBase.durAttackActive = 1.76666665f;
			enemyDataBase.durAttackWait = enemyDataBase.durAttackActive * 1.5f;
			enemyDataBase.timeDamage = 0.733333349f;
			enemyDataBase.durSpawnTranslate = 0f;
			enemyDataBase.durSpawnDrop = 0.966666639f;
			enemyDataBase.durCorpse = 1.2f;
			enemyDataBase.deathEffectTime = 0.933333337f;
			enemyDataBase.durLeave = 1.06666672f;
			enemyDataBase.goldToDrop = 60.0;
			enemyDataBase.numDropMin = 10;
			enemyDataBase.numDropMax = 10;
			enemyDataBase.durLoot = 0.8f;
			enemyDataBase.spawnWeight = 2.25f;
			enemyDataBase.height = 2.7f;
			enemyDataBase.projectileTargetOffset = new Vector3(0f, 1f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.banditBossSpawns, 1f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.banditBossDeaths, 1f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.3448276f, new SoundVariedSimple(SoundArchieve.inst.banditBossDamages, 1f))
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateEpicBossOrc()
		{
			Projectile projectile = new Projectile();
			projectile.type = Projectile.Type.BOSS;
			projectile.targetType = Projectile.TargetType.SINGLE_ALLY_ANY;
			projectile.durFly = 0.5f;
			projectile.path = new ProjectilePathBomb
			{
				heightAddMax = 0.2f
			};
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "BOSS";
			enemyDataBase.type = EnemyDataBase.Type.EPIC;
			enemyDataBase.deathEffectScale = 1.5f;
			enemyDataBase.scaleBuffVisual = 1.3f;
			enemyDataBase.healthMax = 600.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.damage = 12.0;
			enemyDataBase.projectile = projectile;
			enemyDataBase.durAttackActive = 1.86666667f;
			enemyDataBase.durAttackWait = enemyDataBase.durAttackActive * 1.5f;
			enemyDataBase.timeDamage = 0.533333361f;
			enemyDataBase.durSpawnTranslate = 0f;
			enemyDataBase.durSpawnDrop = 2.5f;
			enemyDataBase.durCorpse = 2.95f;
			enemyDataBase.deathEffectTime = 2.55f;
			enemyDataBase.durLeave = 0.7f;
			enemyDataBase.goldToDrop = 100.0;
			enemyDataBase.numDropMin = 20;
			enemyDataBase.numDropMax = 20;
			enemyDataBase.durLoot = 1.3f;
			enemyDataBase.spawnWeight = 1E+09f;
			enemyDataBase.height = 2.7f;
			enemyDataBase.projectileTargetOffset = new Vector3(0f, 1f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.orcBossSpawns, 1f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.orcBossDeaths, 1f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.303571433f, new SoundVariedSimple(SoundArchieve.inst.orcBossDamages, 1f))
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateRegularWolf()
		{
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "WOLF";
			enemyDataBase.type = EnemyDataBase.Type.REGULAR;
			enemyDataBase.deathEffectScale = 0.5f;
			enemyDataBase.scaleBuffVisual = 0.85f;
			enemyDataBase.healthMax = 20.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.damage = 1.2;
			enemyDataBase.durAttackActive = 1.16666663f;
			enemyDataBase.durAttackWait = 0.75f;
			enemyDataBase.timeDamage = 0.6f;
			enemyDataBase.durSpawnTranslate = 0.4f;
			enemyDataBase.durSpawnDrop = 0.6f;
			enemyDataBase.durCorpse = 1f;
			enemyDataBase.deathEffectTime = 0.733333349f;
			enemyDataBase.goldToDrop = 3.0;
			enemyDataBase.spawnWeight = 0.8f;
			enemyDataBase.spawnProb = 0.4f;
			enemyDataBase.height = 1.7f;
			enemyDataBase.projectileTargetOffset = new Vector3(-0.35f, 0.75f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.wolfSpawns, 0.5f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.wolfDeaths, 0.3f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.314285725f, new SoundVariedSimple(SoundArchieve.inst.wolfAttacks, 0.25f)),
				new TimedSound(0.514285743f, new SoundVariedSimple(SoundArchieve.inst.wolfDamages, 0.25f))
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateElfSemiCorrupted()
		{
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "SEMI CORRUPTED ELF";
			enemyDataBase.type = EnemyDataBase.Type.REGULAR;
			enemyDataBase.deathEffectScale = 0.6f;
			enemyDataBase.healthMax = 60.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.damage = 4.0;
			enemyDataBase.durAttackActive = 1.66666663f;
			enemyDataBase.durAttackWait = 2.7f;
			enemyDataBase.timeDamage = 0.966666639f;
			enemyDataBase.durSpawnTranslate = 0.6f;
			enemyDataBase.durSpawnDrop = 0.3f;
			enemyDataBase.durCorpse = 1.2f;
			enemyDataBase.deathEffectTime = 0.933333337f;
			enemyDataBase.goldToDrop = 8.0;
			enemyDataBase.spawnWeight = 2.25f;
			enemyDataBase.spawnProb = 1f;
			enemyDataBase.height = 1.7f;
			enemyDataBase.projectileTargetOffset = new Vector3(-0.35f, 0.75f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.elfHalfCorruptedSpawns, 1f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.elfHalfCorruptedDeaths, 1f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.314285725f, new SoundVariedSimple(SoundArchieve.inst.elfHalfCorruptedAttack, 1f)),
				new TimedSound(0.514285743f, new SoundVariedSimple(SoundArchieve.inst.elfHalfCorruptedDamages, 1f))
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateElfCorrupted()
		{
			Projectile projectile = new Projectile();
			projectile.type = Projectile.Type.ELF_CORRUPTED;
			projectile.targetType = Projectile.TargetType.SINGLE_ALLY_ANY;
			projectile.durFly = 0.7f;
			projectile.path = new ProjectilePathBomb
			{
				heightAddMax = 0.4f
			};
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "CORRUPTED ELF";
			enemyDataBase.type = EnemyDataBase.Type.REGULAR;
			enemyDataBase.deathEffectScale = 0.6f;
			enemyDataBase.healthMax = 60.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.damage = 6.0;
			enemyDataBase.projectile = projectile;
			enemyDataBase.durAttackActive = 1.2f;
			enemyDataBase.durAttackWait = 0.25f;
			enemyDataBase.timeDamage = 0.733333349f;
			enemyDataBase.durSpawnTranslate = 0.5f;
			enemyDataBase.durSpawnDropTranslate = 0f;
			enemyDataBase.durSpawnDrop = 0.6f;
			enemyDataBase.durCorpse = 1.26666665f;
			enemyDataBase.deathEffectTime = 1f;
			enemyDataBase.goldToDrop = 15.0;
			enemyDataBase.spawnWeight = 4f;
			enemyDataBase.spawnProb = 0.5f;
			enemyDataBase.height = 1.7f;
			enemyDataBase.projectileTargetOffset = new Vector3(-0.35f, 0.75f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.elfCorruptedSpawns, 1f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.elfCorruptedDeaths, 1f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.314285725f, new SoundVariedSimple(SoundArchieve.inst.elfCorruptedAttack, 1f)),
				new TimedSound(0.514285743f, new SoundVariedSimple(SoundArchieve.inst.elfCorruptedDamages, 1f))
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateBossElf()
		{
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "BOSS ELF";
			enemyDataBase.type = EnemyDataBase.Type.BOSS;
			enemyDataBase.deathEffectScale = 1.5f;
			enemyDataBase.scaleBuffVisual = 1.3f;
			enemyDataBase.healthMax = 450.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.damage = 12.0;
			enemyDataBase.durAttackActive = 1.93333328f;
			enemyDataBase.durAttackWait = 0.15f;
			enemyDataBase.timeDamage = 0.6666667f;
			enemyDataBase.durSpawnTranslate = 0f;
			enemyDataBase.durSpawnDrop = 1.33333337f;
			enemyDataBase.durCorpse = 1.5f;
			enemyDataBase.deathEffectTime = 1.23333335f;
			enemyDataBase.durLeave = 0.966666639f;
			enemyDataBase.goldToDrop = 60.0;
			enemyDataBase.numDropMin = 10;
			enemyDataBase.numDropMax = 10;
			enemyDataBase.durLoot = 0.8f;
			enemyDataBase.spawnWeight = 2.25f;
			enemyDataBase.height = 3.3f;
			enemyDataBase.projectileTargetOffset = new Vector3(0f, 1f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.elfBossSpawns, 1f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.elfBossDeaths, 1f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.3448276f, new SoundVariedSimple(SoundArchieve.inst.elfBossDamages, 1f))
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateDwarfSemiCorrupted()
		{
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "SEMI CORRUPTED DWARF";
			enemyDataBase.type = EnemyDataBase.Type.REGULAR;
			enemyDataBase.deathEffectScale = 0.7f;
			enemyDataBase.healthMax = 20.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.damage = 3.0;
			enemyDataBase.durAttackActive = 0.966666639f;
			enemyDataBase.durAttackWait = 2f;
			enemyDataBase.timeDamage = 0.333333343f;
			enemyDataBase.durSpawnTranslate = 0.7f;
			enemyDataBase.durSpawnDropTranslate = 0f;
			enemyDataBase.durSpawnDrop = 0.8f;
			enemyDataBase.durCorpse = 1.06666672f;
			enemyDataBase.deathEffectTime = 0.8333333f;
			enemyDataBase.goldToDrop = 5.0;
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.height = 1.7f;
			enemyDataBase.projectileTargetOffset = new Vector3(-0.35f, 0.75f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.dwarfSpawns, 0.4f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.dwarfDeaths, 0.4f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.514285743f, new SoundVariedSimple(SoundArchieve.inst.dwarfDamages, 0.4f))
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateDwarfCorrupted()
		{
			Projectile projectile = new Projectile();
			projectile.type = Projectile.Type.DWARF_CORRUPTED;
			projectile.targetType = Projectile.TargetType.SINGLE_ALLY_ANY;
			projectile.durFly = 0.7f;
			projectile.path = new ProjectilePathBomb
			{
				heightAddMax = 0.4f
			};
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "CORRUPTED DWARF";
			enemyDataBase.type = EnemyDataBase.Type.REGULAR;
			enemyDataBase.deathEffectScale = 0.7f;
			enemyDataBase.healthMax = 60.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.damage = 7.0;
			enemyDataBase.projectile = projectile;
			enemyDataBase.durAttackActive = 0.966666639f;
			enemyDataBase.durAttackWait = 2f;
			enemyDataBase.timeDamage = 0.333333343f;
			enemyDataBase.durSpawnTranslate = 0.5f;
			enemyDataBase.durSpawnDropTranslate = 0f;
			enemyDataBase.durSpawnDrop = 0.8f;
			enemyDataBase.durCorpse = 1.06666672f;
			enemyDataBase.deathEffectTime = 0.8333333f;
			enemyDataBase.goldToDrop = 12.0;
			enemyDataBase.spawnWeight = 2.25f;
			enemyDataBase.spawnProb = 0.5f;
			enemyDataBase.height = 1.7f;
			enemyDataBase.projectileTargetOffset = new Vector3(-0.35f, 0.75f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.banditSpawns, 0.4f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.dwarfDeaths, 0.4f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.514285743f, new SoundVariedSimple(SoundArchieve.inst.dwarfDamages, 0.4f))
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateBossDwarf()
		{
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "BOSS DWARF";
			enemyDataBase.type = EnemyDataBase.Type.BOSS;
			enemyDataBase.deathEffectScale = 1.5f;
			enemyDataBase.scaleBuffVisual = 1.6f;
			enemyDataBase.healthMax = 500.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.damage = 12.0;
			enemyDataBase.durAttackActive = 2f;
			enemyDataBase.durAttackWait = 3f;
			enemyDataBase.timeDamage = 0.933333337f;
			enemyDataBase.durSpawnTranslate = 0f;
			enemyDataBase.durSpawnDrop = 1.73333335f;
			enemyDataBase.durCorpse = 1.83333337f;
			enemyDataBase.deathEffectTime = 1.5333333f;
			enemyDataBase.durLeave = 0.9f;
			enemyDataBase.goldToDrop = 60.0;
			enemyDataBase.numDropMin = 10;
			enemyDataBase.numDropMax = 10;
			enemyDataBase.durLoot = 0.8f;
			enemyDataBase.spawnWeight = 2.25f;
			enemyDataBase.height = 3.3f;
			enemyDataBase.projectileTargetOffset = new Vector3(0f, 1f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.dwarfBossSpawns, 1f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.dwarfBossDeaths, 1f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.3448276f, new SoundVariedSimple(SoundArchieve.inst.dwarfBossDamages, 1f))
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateRegularBat()
		{
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "BAT";
			enemyDataBase.type = EnemyDataBase.Type.REGULAR;
			enemyDataBase.deathEffectScale = 0.4f;
			enemyDataBase.scaleBuffVisual = 0.8f;
			enemyDataBase.healthMax = 20.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.damage = 1.7;
			enemyDataBase.durAttackActive = 1.5f;
			enemyDataBase.durAttackWait = 1.2f;
			enemyDataBase.timeDamage = 0.466666669f;
			enemyDataBase.durSpawnTranslate = 0f;
			enemyDataBase.durSpawnDropTranslate = 0f;
			enemyDataBase.durSpawnDrop = 0.8f;
			enemyDataBase.durCorpse = 1.3f;
			enemyDataBase.deathEffectTime = 1.06666672f;
			enemyDataBase.goldToDrop = 5.0;
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.spawnProb = 0.7f;
			enemyDataBase.height = 1.7f;
			enemyDataBase.projectileTargetOffset = new Vector3(-0.35f, 0.75f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.batSpawns, 1f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.batDeaths, 1f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.514285743f, new SoundVariedSimple(SoundArchieve.inst.batDamages, 1f))
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateRegularSpider()
		{
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "SPIDER";
			enemyDataBase.type = EnemyDataBase.Type.REGULAR;
			enemyDataBase.deathEffectScale = 0.5f;
			enemyDataBase.scaleBuffVisual = 0.75f;
			enemyDataBase.healthMax = 25.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.damage = 2.2;
			enemyDataBase.durAttackActive = 1.56666672f;
			enemyDataBase.durAttackWait = 1f;
			enemyDataBase.timeDamage = 0.6333333f;
			enemyDataBase.durSpawnTranslate = 0.4f;
			enemyDataBase.durSpawnDrop = 0.3f;
			enemyDataBase.durCorpse = 1.1f;
			enemyDataBase.deathEffectTime = 0.8333333f;
			enemyDataBase.goldToDrop = 8.0;
			enemyDataBase.spawnWeight = 1.25f;
			enemyDataBase.spawnProb = 0.4f;
			enemyDataBase.height = 1.1f;
			enemyDataBase.projectileTargetOffset = new Vector3(-0.35f, 0.75f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.spiderSpawns, 1f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.spiderDeaths, 1f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.514285743f, new SoundVariedSimple(SoundArchieve.inst.spiderDamages, 1f))
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateRegularMagolies()
		{
			Projectile projectile = new Projectile();
			projectile.type = Projectile.Type.MANGOLIES;
			projectile.targetType = Projectile.TargetType.SINGLE_ALLY_ANY;
			projectile.durFly = 0.4f;
			projectile.visualEffect = new VisualEffect(VisualEffect.Type.MAGOLIES_PROJECTILE_EXPLOSION, 0.433333337f);
			projectile.path = new ProjectilePathBomb
			{
				heightAddMax = 0.05f
			};
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "MANGOLIES";
			enemyDataBase.type = EnemyDataBase.Type.REGULAR;
			enemyDataBase.deathEffectScale = 0.65f;
			enemyDataBase.healthMax = 45.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.projectile = projectile;
			enemyDataBase.damage = 7.5;
			enemyDataBase.durAttackActive = 1.93333328f;
			enemyDataBase.durAttackWait = 0.7f;
			enemyDataBase.timeDamage = 0.8333333f;
			enemyDataBase.durSpawnTranslate = 0.4f;
			enemyDataBase.durSpawnDrop = 0.533333361f;
			enemyDataBase.durCorpse = 1f;
			enemyDataBase.deathEffectTime = 0.6666667f;
			enemyDataBase.goldToDrop = 15.0;
			enemyDataBase.spawnWeight = 2.25f;
			enemyDataBase.spawnProb = 1f;
			enemyDataBase.height = 2.3f;
			enemyDataBase.projectileTargetOffset = new Vector3(-0.35f, 0.75f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.magoliesSpawns, 1f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.magoliesDeaths, 1f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.514285743f, new SoundVariedSimple(SoundArchieve.inst.magoliesDamages, 1f))
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateBossOrc()
		{
			Projectile projectile = new Projectile();
			projectile.type = Projectile.Type.BOSS;
			projectile.targetType = Projectile.TargetType.SINGLE_ALLY_ANY;
			projectile.durFly = 0.5f;
			projectile.path = new ProjectilePathBomb
			{
				heightAddMax = 0.2f
			};
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "BOSS";
			enemyDataBase.type = EnemyDataBase.Type.BOSS;
			enemyDataBase.deathEffectScale = 1.2f;
			enemyDataBase.scaleBuffVisual = 1.3f;
			enemyDataBase.healthMax = 420.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.damage = 12.0;
			enemyDataBase.projectile = projectile;
			enemyDataBase.durAttackActive = 1.5333333f;
			enemyDataBase.durAttackWait = 1f;
			enemyDataBase.timeDamage = 0.533333361f;
			enemyDataBase.durSpawnTranslate = 0f;
			enemyDataBase.durSpawnDrop = 1.5f;
			enemyDataBase.durCorpse = 1.9666667f;
			enemyDataBase.deathEffectTime = 1.7f;
			enemyDataBase.durLeave = 0.7f;
			enemyDataBase.goldToDrop = 60.0;
			enemyDataBase.numDropMin = 10;
			enemyDataBase.numDropMax = 10;
			enemyDataBase.durLoot = 0.8f;
			enemyDataBase.spawnWeight = 2.2f;
			enemyDataBase.height = 2.3f;
			enemyDataBase.projectileTargetOffset = new Vector3(0f, 1f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.orcBossSpawns, 1f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.orcBossDeaths, 1f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.303571433f, new SoundVariedSimple(SoundArchieve.inst.orcBossDamages, 1f))
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateEpicBossMagolis()
		{
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "BOSS MANGOLIES";
			enemyDataBase.type = EnemyDataBase.Type.EPIC;
			enemyDataBase.deathEffectScale = 2.25f;
			enemyDataBase.scaleBuffVisual = 1.4f;
			enemyDataBase.healthMax = 550.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.damage = 15.0;
			enemyDataBase.durAttackActive = 2.9f;
			enemyDataBase.durAttackWait = 0.8f;
			enemyDataBase.timeDamage = 0.8666667f;
			enemyDataBase.durSpawnTranslate = 0f;
			enemyDataBase.durSpawnDrop = 1.76666665f;
			enemyDataBase.durCorpse = 1.83333337f;
			enemyDataBase.deathEffectTime = 1.5333333f;
			enemyDataBase.durLeave = 0.8f;
			enemyDataBase.goldToDrop = 100.0;
			enemyDataBase.numDropMin = 20;
			enemyDataBase.numDropMax = 20;
			enemyDataBase.durLoot = 1.3f;
			enemyDataBase.spawnWeight = 9999f;
			enemyDataBase.height = 3.7f;
			enemyDataBase.scaleHealthBar = 2f;
			enemyDataBase.projectileTargetOffset = new Vector3(0f, 1f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.magoliesBossSpawns, 1f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.magoliesBossDeaths, 1f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.3448276f, new SoundVariedSimple(SoundArchieve.inst.magoliesBossDamages, 1f))
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateBossWiseSnake()
		{
			Projectile projectile = new Projectile();
			projectile.type = Projectile.Type.WISE_SNAKE;
			projectile.targetType = Projectile.TargetType.SINGLE_ALLY_ANY;
			projectile.durFly = 0.3f;
			projectile.visualEffect = new VisualEffect(VisualEffect.Type.HIT, 0.25f);
			ProjectilePathLinear path = new ProjectilePathLinear();
			projectile.path = path;
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "BOSS WISE SNAKE";
			enemyDataBase.type = EnemyDataBase.Type.BOSS;
			enemyDataBase.deathEffectScale = 2.25f;
			enemyDataBase.scaleBuffVisual = 1.4f;
			enemyDataBase.projectile = projectile;
			enemyDataBase.stunHereosWhenSpawn = true;
			enemyDataBase.stunDurationInSeconds = 4f;
			enemyDataBase.blindMissChance = 0.5f;
			enemyDataBase.spawnedMinion = EnemyFactory.CreateSnake();
			enemyDataBase.spawnedMinionsCount = 4;
			enemyDataBase.spawnMinionsAnimDuration = 2.9f;
			enemyDataBase.spawnMinionsTime = 0.2f;
			enemyDataBase.spawnMinionsIfAloneAfterSeconds = 15f;
			enemyDataBase.isInmuneWithMinions = true;
			enemyDataBase.secondsBetweenEachMinionSpawn = 0.5f;
			enemyDataBase.healthMax = 400.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.damage = 20.0;
			enemyDataBase.durAttackActive = 1f;
			enemyDataBase.durAttackWait = 0.8f;
			enemyDataBase.timeDamage = 0.45f;
			enemyDataBase.durSpawnTranslate = 0f;
			enemyDataBase.durSpawnDrop = 3f;
			enemyDataBase.durCorpse = 6f;
			enemyDataBase.deathEffectTime = 4.2f;
			enemyDataBase.durLeave = 0.8f;
			enemyDataBase.goldToDrop = 100.0;
			enemyDataBase.numDropMin = 20;
			enemyDataBase.numDropMax = 20;
			enemyDataBase.durLoot = 4.5f;
			enemyDataBase.spawnWeight = 9999f;
			enemyDataBase.height = 3f;
			enemyDataBase.scaleHealthBar = 2f;
			enemyDataBase.projectileTargetOffset = new Vector3(0f, 1f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundSimple(SoundArchieve.inst.wiseSnakeSpawn, 1f, float.MaxValue);
			enemyDataBase.soundDeath = new SoundDelayed(1f, SoundArchieve.inst.wiseSnakeDeath, 1f);
			enemyDataBase.soundHurt = new SoundDelayed(1f, SoundArchieve.inst.wiseSnakeHurt, 1f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.45f, new SoundVariedSimple(SoundArchieve.inst.wiseSnakeAttacks, 1f))
			};
			enemyDataBase.soundSummonMinions = new SoundSimple(SoundArchieve.inst.wiseSnakeSummonMinions, 1f, float.MaxValue);
			enemyDataBase.voices = new EnemyVoices
			{
				death = new EnemyVoices.VoiceData(SoundArchieve.inst.voWiseSnakeDeath, 1f),
				hurt = new EnemyVoices.VoiceData(SoundArchieve.inst.voWiseSnakeScape, 1f),
				spawn = new EnemyVoices.VoiceData(SoundArchieve.inst.voWiseSnakeSpawn, 1f),
				summonMinions = new EnemyVoices.VoiceData(SoundArchieve.inst.voWiseSnakeSummonMinions, 1f)
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateSnake()
		{
			Projectile projectile = new Projectile();
			projectile.type = Projectile.Type.SNAKE;
			projectile.targetType = Projectile.TargetType.SINGLE_ALLY_ANY;
			projectile.durFly = 0.3f;
			projectile.visualEffect = new VisualEffect(VisualEffect.Type.SNAKE_PROJECTILE_EXPLOSION, 0.933f);
			projectile.path = new ProjectilePathBomb
			{
				heightAddMax = 0.05f
			};
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "SNAKE";
			enemyDataBase.type = EnemyDataBase.Type.REGULAR;
			enemyDataBase.deathEffectScale = 0.75f;
			enemyDataBase.scaleBuffVisual = 0.75f;
			enemyDataBase.projectile = projectile;
			enemyDataBase.useProjectileOnNumHit = new bool[]
			{
				default(bool),
				true
			};
			enemyDataBase.healthMax = 55.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.damage = 3.0;
			enemyDataBase.durAttackActive = 1f;
			enemyDataBase.durAttackWait = 1f;
			enemyDataBase.timeDamage = 0.45f;
			enemyDataBase.durSpawnTranslate = 1f;
			enemyDataBase.durSpawnDrop = 2f;
			enemyDataBase.durCorpse = 1.1f;
			enemyDataBase.deathEffectTime = 1f;
			enemyDataBase.goldToDrop = 8.0;
			enemyDataBase.spawnWeight = 1.25f;
			enemyDataBase.spawnProb = 0.4f;
			enemyDataBase.height = 1.1f;
			enemyDataBase.projectileTargetOffset = new Vector3(-0.35f, 0.75f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.snakeSpawns, 1f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.snakeDeaths, 1f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.45f, new SoundVariedSimple(SoundArchieve.inst.snakeAttacks, 1f))
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateChest()
		{
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "CHEST";
			enemyDataBase.type = EnemyDataBase.Type.CHEST;
			enemyDataBase.scaleBuffVisual = 0.9f;
			enemyDataBase.healthMax = 40.0;
			enemyDataBase.healthRegen = -0.015;
			enemyDataBase.damage = -1.0;
			enemyDataBase.durAttackActive = float.PositiveInfinity;
			enemyDataBase.durAttackWait = float.PositiveInfinity;
			enemyDataBase.timeDamage = float.PositiveInfinity;
			enemyDataBase.durSpawnTranslate = 0.7f;
			enemyDataBase.durSpawnDrop = 0.8f;
			enemyDataBase.durCorpse = 0.333333343f;
			enemyDataBase.deathEffectTime = float.PositiveInfinity;
			enemyDataBase.goldToDrop = 35.0;
			enemyDataBase.numDropMin = 15;
			enemyDataBase.numDropMax = 15;
			enemyDataBase.durLoot = 0.3f;
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.height = 1.1f;
			enemyDataBase.projectileTargetOffset = new Vector3(-0.35f, 0.75f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.chestSpawns, 1f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.chestDeaths, 1f);
			enemyDataBase.soundsAttack = new List<TimedSound>();
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}

		public static EnemyDataBase CreateBossSnowman()
		{
			EnemyDataBase enemyDataBase = new EnemyDataBase();
			enemyDataBase.name = "BOSS SNOWMAN";
			enemyDataBase.type = EnemyDataBase.Type.BOSS;
			enemyDataBase.deathEffectScale = 1.5f;
			enemyDataBase.scaleBuffVisual = 1.5f;
			enemyDataBase.healthMax = 290.0;
			enemyDataBase.healthRegen = 0.0;
			enemyDataBase.damage = 7.0;
			enemyDataBase.durAttackActive = 1.9f;
			enemyDataBase.durAttackWait = enemyDataBase.durAttackActive * 1.5f;
			enemyDataBase.timeDamage = 0.733333349f;
			enemyDataBase.durSpawnTranslate = 0f;
			enemyDataBase.durSpawnDrop = 0.9f;
			enemyDataBase.durCorpse = 1.4f;
			enemyDataBase.deathEffectTime = 0.933333337f;
			enemyDataBase.durLeave = 0.663f;
			enemyDataBase.goldToDrop = 60.0;
			enemyDataBase.numDropMin = 10;
			enemyDataBase.numDropMax = 10;
			enemyDataBase.durLoot = 0.8f;
			enemyDataBase.spawnWeight = 2.25f;
			enemyDataBase.height = 2.7f;
			enemyDataBase.projectileTargetOffset = new Vector3(0f, 1f);
			enemyDataBase.projectileTargetRandomness = 0.1f;
			enemyDataBase.soundSpawn = new SoundVariedSimple(SoundArchieve.inst.banditBossSpawns, 1f);
			enemyDataBase.soundDeath = new SoundVariedSimple(SoundArchieve.inst.banditBossDeaths, 1f);
			enemyDataBase.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.3448276f, new SoundVariedSimple(SoundArchieve.inst.banditBossDamages, 1f))
			};
			AttachmentOffsets.Init(enemyDataBase);
			return enemyDataBase;
		}
	}
}
