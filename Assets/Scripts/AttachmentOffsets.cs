using System;
using System.Collections.Generic;
using Simulation;
using UnityEngine;

public static class AttachmentOffsets
{
	public static void Init()
	{
		List<Projectile.Type> list = new List<Projectile.Type>(AttachmentOffsets.PROJECTILE_STARTS.Keys);
		foreach (Projectile.Type type in list)
		{
			Dictionary<Projectile.Type, Vector3> projectile_STARTS;
			Projectile.Type key;
			(projectile_STARTS = AttachmentOffsets.PROJECTILE_STARTS)[key = type] = projectile_STARTS[key] * 0.2f;
		}
		List<string> list2 = new List<string>(AttachmentOffsets.DEATH_EFFECTS.Keys);
		foreach (string text in list2)
		{
			Dictionary<string, Vector3> death_EFFECTS;
			string key2;
			(death_EFFECTS = AttachmentOffsets.DEATH_EFFECTS)[key2 = text] = death_EFFECTS[key2] * 0.2f;
		}
	}

	public static void Init<T>(List<T> units) where T : UnitDataBase
	{
		foreach (T t in units)
		{
			AttachmentOffsets.Init(t);
		}
	}

	public static void Init(UnitDataBase unit)
	{
		unit.height *= 0.2f;
		if (unit is UnitHealthyDataBase)
		{
			UnitHealthyDataBase unitHealthyDataBase = unit as UnitHealthyDataBase;
			unitHealthyDataBase.projectileTargetOffset *= 0.2f;
			unitHealthyDataBase.projectileTargetRandomness *= 0.2f;
		}
	}

	public static Vector3 GetProjectileStart(Projectile.Type type)
	{
		return (!AttachmentOffsets.PROJECTILE_STARTS.ContainsKey(type)) ? new Vector3(0f, 0f, 0f) : AttachmentOffsets.PROJECTILE_STARTS[type];
	}

	public static Vector3 GetDeathEffect(string enemyName)
	{
		return AttachmentOffsets.DEATH_EFFECTS[enemyName];
	}

	private const float SCALE = 0.2f;

	private static Dictionary<Projectile.Type, Vector3> PROJECTILE_STARTS = new Dictionary<Projectile.Type, Vector3>
	{
		{
			Projectile.Type.REVERSED_EXCALIBUR_MUD,
			new Vector3(1.131f, 1.384f)
		},
		{
			Projectile.Type.BOSS,
			new Vector3(-1.6f, 2.2f)
		},
		{
			Projectile.Type.APPLE,
			new Vector3(1.44f, 0.63f)
		},
		{
			Projectile.Type.APPLE_BOMBARD,
			new Vector3(1.238f, 1.323f)
		},
		{
			Projectile.Type.APPLE_AID,
			new Vector3(1f, 3f)
		},
		{
			Projectile.Type.DEREK_MAGIC_BALL,
			new Vector3(0.9f, 0.7f)
		},
		{
			Projectile.Type.DEREK_BOOK,
			new Vector3(0.9f, 0.7f)
		},
		{
			Projectile.Type.BOMBERMAN_DINAMIT,
			new Vector3(1.73f, 1.736f)
		},
		{
			Projectile.Type.BOMBERMAN_FIRE_CRACKER,
			new Vector3(0.714f, 0.869f)
		},
		{
			Projectile.Type.BOMBERMAN_FIREWORK,
			Vector3.zero
		},
		{
			Projectile.Type.SHEELA,
			new Vector3(1.362f, 0.782f)
		},
		{
			Projectile.Type.SAM_BOTTLE,
			new Vector3(1.365f, 1.845f)
		},
		{
			Projectile.Type.SAM_AXE,
			new Vector3(1.032f, 1.851f)
		},
		{
			Projectile.Type.BLIND_ARCHER_ATTACK,
			new Vector3(0.43f, 1.69f)
		},
		{
			Projectile.Type.BLIND_ARCHER_AUTO,
			new Vector3(0.32f, 1.49f)
		},
		{
			Projectile.Type.BLIND_ARCHER_ULTI,
			new Vector3(0.27f, 1.11f)
		},
		{
			Projectile.Type.ELF_CORRUPTED,
			new Vector3(-0.44f, 1.36f)
		},
		{
			Projectile.Type.DWARF_CORRUPTED,
			new Vector3(-1.622f, 1.692f)
		},
		{
			Projectile.Type.HUMAN_CORRUPTED,
			new Vector3(-0.859f, 1.363f)
		},
		{
			Projectile.Type.MANGOLIES,
			new Vector3(-2.28f, 1.72f)
		},
		{
			Projectile.Type.GOBLIN_SACK,
			new Vector3(0.9f, 0.7f)
		},
		{
			Projectile.Type.GOBLIN_SMOKE_BOMB,
			new Vector3(0.56f, 0.28f)
		},
		{
			Projectile.Type.WARLOCK_ATTACK,
			new Vector3(1.3f, 0.8f)
		},
		{
			Projectile.Type.WARLOCK_SWARM,
			new Vector3(-1.2f, 1.2f)
		},
		{
			Projectile.Type.WARLOCK_REGRET,
			new Vector3(0.25f, 2.75f)
		},
		{
			Projectile.Type.SNAKE,
			new Vector3(-1.5f, 0.7f)
		},
		{
			Projectile.Type.WISE_SNAKE,
			new Vector3(-1.3f, 1.7f)
		},
		{
			Projectile.Type.BABU_SOUP,
			new Vector3(-0.8f, 2f)
		},
		{
			Projectile.Type.BABU_TEA_CUP,
			new Vector3(0.3f, 1.6f)
		}
	};

	private static Dictionary<string, Vector3> DEATH_EFFECTS = new Dictionary<string, Vector3>
	{
		{
			"BANDIT",
			new Vector3(0.202f, 0.014f)
		},
		{
			"WOLF",
			new Vector3(0f, 0.105f)
		},
		{
			"SPIDER",
			new Vector3(1.191f, 0.038f)
		},
		{
			"BAT",
			new Vector3(0.8f, -0.012f)
		},
		{
			"SEMI CORRUPTED ELF",
			new Vector3(0.785f, -0.021f)
		},
		{
			"CORRUPTED ELF",
			new Vector3(0.851f, -0.016f)
		},
		{
			"SEMI CORRUPTED DWARF",
			new Vector3(0.24f, 0f)
		},
		{
			"CORRUPTED DWARF",
			new Vector3(0.259f, -0.023f)
		},
		{
			"CORRUPTED HUMAN",
			new Vector3(0.538f, -0.025f)
		},
		{
			"SEMI CORRUPTED HUMAN",
			new Vector3(0.594f, 0.034f)
		},
		{
			"MANGOLIES",
			new Vector3(1.944f, 0f)
		},
		{
			"BOSS",
			new Vector3(-0.6f, 0.29f)
		},
		{
			"BOSS ELF",
			new Vector3(-1.537f, -0.173f)
		},
		{
			"BOSS DWARF",
			new Vector3(-1.295f, 0.063f)
		},
		{
			"BOSS HUMAN",
			new Vector3(-0.796f, -0.056f)
		},
		{
			"BOSS MANGOLIES",
			new Vector3(-1.67f, -0.2f)
		},
		{
			"BOSS WISE SNAKE",
			new Vector3(0f, -0.2f)
		},
		{
			"SNAKE",
			new Vector3(0.7f, -0.2f)
		},
		{
			"BOSS SNOWMAN",
			new Vector3(0.4f, -0.056f)
		}
	};
}
