using System;

namespace Simulation.ArtifactSystem
{
	public static class EffectIds
	{
		public const int GOLD_EARNINGS = 1;

		public const int RING_DAMAGE = 2;

		public const int HERO_DAMAGE = 3;

		public const int HERO_HEALTH = 4;

		public const int MAX_COMMON_EFFECT_ID = 99;

		public const int HERO_UPGRADE_COST = 100;

		public const int HERO_CRIT_CHANCE = 101;

		public const int HERO_CRIT_DAMAGE = 102;

		public const int HERO_SKILL_DAMAGE = 103;

		public const int HERO_NON_SKILL_DAMAGE = 104;

		public const int LEVEL_REQ_FOR_SKILLS = 105;

		public const int HERO_REVIVE_TIME = 106;

		public const int HERO_ULTIMATE_COOLDOWN = 107;

		public const int RING_UPGRADE_COST = 108;

		public const int RING_CRIT_CHANCE = 109;

		public const int RING_CRIT_DAMAGE = 110;

		public const int BOSS_TIME = 111;

		public const int BOSS_HEALTH = 112;

		public const int BOSS_DAMAGE = 113;

		public const int BOSS_GOLD = 114;

		public const int NON_BOSS_HEALTH = 115;

		public const int NON_BOSS_DAMAGE = 116;

		public const int NON_BOSS_GOLD = 117;

		public const int FAST_ENEMY_SPAWN = 118;

		public const int DRAGON_SPAWN_RATE = 119;

		public const int EPIC_BOSS_MYTHSTONES_DROP = 120;

		public const int FREE_CHEST_EXTRA_ITEM = 121;

		public const int FREE_CHEST_CURRENCY = 122;

		public const int GOLD_FROM_TREASURE_CHESTS = 123;

		public const int TREASURE_CHEST_CHANCE = 124;

		public const int GOLD_FROM_OFFLINE_EARNINGS = 125;

		public const int CHANCE_TO_SKIP_NON_BOSS_WAVE = 126;

		public const int PRESTIGE_MYTHSTONES = 127;

		public const int HORSESHOE_COUNT = 128;

		public const int HORSESHOE_DURATION = 129;

		public const int DESTRUCTION_COUNT = 130;

		public const int TIME_WARP_COUNT = 131;

		public const int TIME_WARP_SPEED = 132;

		public const int TIME_WARP_DURATION = 133;

		public const int AUTO_TAP_COUNT = 134;

		public const int AUTO_TAP_DURATION = 135;

		public const int GOLD_BAG_COUNT = 136;

		public const int GOLD_BAG_VALUE = 137;

		public const int SHIELD_COUNT = 138;

		public const int SHIELD_DURATION = 139;

		public const int T1_ADVENTURE_STAGE_JUMP = 140;

		public const int T2_ADVENTURE_STAGE_JUMP = 141;

		public const int ARTIFACT_UPGRADE_COST = 142;

		public const int MILESTONE_BONUS = 143;

		public const int MILESTONE_UPGRADE_COST = 144;

		public const int HORSESHOE_VALUE = 145;

		public const int FREE_CHEST_COOLDOWN = 146;

		public static readonly int[] Common = new int[]
		{
			1,
			2,
			3,
			4
		};

		public static readonly int[] Unique = new int[]
		{
			100,
			101,
			102,
			103,
			104,
			105,
			106,
			107,
			108,
			109,
			110,
			111,
			112,
			113,
			114,
			115,
			116,
			117,
			119,
			120,
			146,
			121,
			122,
			123,
			124,
			125,
			126,
			127,
			128,
			129,
			145,
			130,
			131,
			132,
			133,
			134,
			135,
			136,
			137,
			138,
			139,
			142,
			143,
			144
		};
	}
}
