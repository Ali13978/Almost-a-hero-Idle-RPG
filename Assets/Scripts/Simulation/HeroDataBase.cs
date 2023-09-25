using System;
using System.Collections.Generic;

namespace Simulation
{
	public class HeroDataBase : UnitHealthyDataBase
	{
		public HeroDataBase()
		{
			this.skillBranchesEverUnlocked = new int[2];
		}

		public bool CanEvolve(List<Gear> gears)
		{
			if (this.evolveLevel == 6)
			{
				return false;
			}
			int num = 0;
			int num2 = 6;
			foreach (Gear gear in gears)
			{
				if (gear.data.belongsTo == this)
				{
					num++;
					if (gear.level < num2)
					{
						num2 = gear.level;
					}
				}
			}
			return num == 3 && num2 >= this.evolveLevel;
		}

		public string GetId()
		{
			return this.id;
		}

		public const int NUM_SKILLS = 4;

		public static int RIFT_LEVEL_XP_REQ = 100;

		public static int[] LEVEL_XPS = new int[]
		{
			6,
			6,
			6,
			6,
			6,
			7,
			7,
			7,
			7,
			7,
			8,
			8,
			8,
			8,
			8,
			9,
			9,
			9,
			9,
			9,
			10,
			10,
			10,
			10,
			10,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			14,
			14,
			14,
			14,
			14,
			16,
			16,
			16,
			16,
			19,
			19,
			19,
			19,
			19,
			19,
			21,
			21,
			21,
			21,
			21,
			21,
			21,
			21,
			21,
			21,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			9999999,
			-1
		};

		public const int EVOLVE_LEVEL_MAX = 6;

		public static double[] EVOLVE_COSTS = new double[]
		{
			20.0,
			100.0,
			200.0,
			500.0,
			1000.0,
			3000.0,
			-1.0
		};

		public int evolveLevel;

		public int[] skillBranchesEverUnlocked;

		public SkinData equippedSkin;

		public bool randomSkinsEnabled;

		public HeroClass heroClass;

		public string id;

		public string nameKey;

		public string descKey;

		public float durRevive;

		public SkillTree skillTree;

		public Weapon weapon;

		public Sound soundDeath;

		public Sound soundRevive;

		public Sound soundVoDeath;

		public Sound soundVoRevive;

		public Sound soundVoSpawn;

		public Sound soundVoLevelUp;

		public AudioClipPromise[] soundVoItem;

		public Sound soundVoWelcome;

		public Sound soundVoEnvChange;

		public AudioClipPromise[] soundVoSelected;

		public AudioClipPromise[] soundVoCheer;

		public Trinket trinket;

		public float trinketEquipTimer;

		public const float TrinketEquipPeriod = 60f;

		public HeroDataBase.UltiCatagory ultiCatagory;

		public enum UltiCatagory
		{
			GREEN,
			BLUE,
			ORANGE,
			RED
		}
	}
}
