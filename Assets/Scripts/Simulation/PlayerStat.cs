using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Static;

namespace Simulation
{
	public class PlayerStat
	{
		static PlayerStat()
		{
			PlayerStat.playerStats = new List<PlayerStat>();
			PlayerStat.AddPlayerStat(true, "STAT_MAX_STAGE", (Simulator sim) => GameMath.GetMaxInt(sim.maxStagePrestigedAt, sim.GetWorld(GameMode.STANDARD).GetMaxStageReached()).ToString(), 20);
			PlayerStat.AddPlayerStat(false, "STAT_PRESTIGES_COUNT", (Simulator sim) => GameMath.GetDoubleString((double)sim.numPrestiges), 10);
			PlayerStat.AddPlayerStat(false, "STAT_MAX_STAGE_PRESTIGED", (Simulator sim) => sim.maxStagePrestigedAt.ToString(), 130);
			PlayerStat.AddPlayerStat(false, "STAT_TAPS_COUNT", (Simulator sim) => GameMath.GetDoubleString((double)PlayerStats.numTotTap), 40);
			PlayerStat.AddPlayerStat(true, "STAT_ENEMIES_KILLED", (Simulator sim) => GameMath.GetDoubleString((double)PlayerStats.enemiesKilled), 170);
			PlayerStat.AddPlayerStat(true, "STAT_HEROES_SLAINED", (Simulator sim) => GameMath.GetDoubleString((double)PlayerStats.timeHeroesDied), 180);
			PlayerStat.AddPlayerStat(true, "STAT_GOBLIN_CHEST_DESTROYED", (Simulator sim) => GameMath.GetDoubleString((double)PlayerStats.goblinChestsDestroyedCount), 200);
			PlayerStat.AddPlayerStat(false, "STAT_ULTI_USED", (Simulator sim) => GameMath.GetDoubleString((double)PlayerStats.ultimatesUsedCount), 210);
			PlayerStat.AddPlayerStat(true, "STAT_SECONDARY_ABILITIES_USED", (Simulator sim) => GameMath.GetDoubleString((double)PlayerStats.secondaryAbilitiesCastedCount), 70);
			PlayerStat.AddPlayerStat(false, "STAT_OUTFITS_OWNED", (Simulator sim) => sim.GetBoughSkinsFromUnlockedHeroesCount().ToString(), 90, new OutfitsOwnedUnlockReq());
			PlayerStat.AddPlayerStat(true, "STAT_RUNES_OWNED", (Simulator sim) => sim.GetBoughtRunes().Count.ToString(), 120);
			PlayerStat.AddPlayerStat(false, "STAT_MERCHANT_ITEMS_USED", delegate(Simulator sim)
			{
				double num = 0.0;
				foreach (int num2 in PlayerStats.numUsedMerchantItems.Values)
				{
					num += (double)num2;
				}
				return GameMath.GetDoubleString(num);
			}, 50);
			PlayerStat.AddPlayerStat(false, "STAT_DRAGONS_CAUGHT", (Simulator sim) => GameMath.GetDoubleString((double)PlayerStats.numAdDragonCatch), 80);
			PlayerStat.AddPlayerStat(false, "STAT_SIDE_QUESTS_COMPLETED", (Simulator sim) => GameMath.GetDoubleString((double)PlayerStats.numTotalDailyCompleted), 30);
			PlayerStat.AddPlayerStat(false, "STAT_SIDE_QUESTS_SKIPPED", (Simulator sim) => GameMath.GetDoubleString((double)PlayerStats.numTotalDailySkip), 160);
			PlayerStat.AddPlayerStat(false, "STAT_FREE_CHESTS_OPENED", delegate(Simulator sim)
			{
				string name = sim.lootpacks[0].GetType().Name;
				return GameMath.GetDoubleString((double)((!sim.lootpacksOpenedCount.ContainsKey(name)) ? 0 : sim.lootpacksOpenedCount[name]));
			}, 60);
			PlayerStat.AddPlayerStat(false, "STAT_TRINKET_PACKS_OPENED", (Simulator sim) => GameMath.GetDoubleString((double)PlayerStats.numTrinketPacksOpened), 150);
			PlayerStat.AddPlayerStat(false, "STAT_CHARMS_PACK_OPENED", (Simulator sim) => GameMath.GetDoubleString((double)sim.numSmallCharmPacksOpened), 110);
			PlayerStat.AddPlayerStat(false, "STAT_CURSED_GATES_COMPLETED", (Simulator sim) => GameMath.GetDoubleString((double)sim.cursedGatesBeaten), 100);
			PlayerStat.AddPlayerStat(false, "STAT_MINES_COLLECTED", (Simulator sim) => GameMath.GetDoubleString((double)PlayerStats.minesCollectedCount), 190);
			PlayerStat.AddPlayerStat(true, "STAT_LIFETIME", (Simulator sim) => GameMath.GetTimeDatailedShortenedString(new TimeSpan(PlayerStats.lifeTimeInTicksInCurrentSaveFile)), 140);
			bool updateEveryFrame = true;
			string key = "STAT_LIFETIME_W_OFFLINE";
			if (PlayerStat._003C_003Ef__mg_0024cache0 == null)
			{
				PlayerStat._003C_003Ef__mg_0024cache0 = new Func<Simulator, string>(PlayerStat.GetPlayerLifetime);
			}
			PlayerStat.AddPlayerStat(updateEveryFrame, key, PlayerStat._003C_003Ef__mg_0024cache0, 220, new HasPlayerCreationTimeUnlockReq());
		}

		public PlayerStat(bool updateEveryFrame, string key, Func<Simulator, string> valueGetter, int id)
		{
			this.isUpdatedEveryFrame = updateEveryFrame;
			this.nameKey = key;
			this.statGetter = valueGetter;
			this.id = id;
		}

		public PlayerStat(bool updateEveryFrame, string key, Func<Simulator, string> valueGetter, int id, StatUnlockReq unlockReq)
		{
			this.isUpdatedEveryFrame = updateEveryFrame;
			this.nameKey = key;
			this.statGetter = valueGetter;
			this.unlockReq = unlockReq;
			this.id = id;
		}

		public static List<PlayerStat> GetActiveStats(Simulator sim)
		{
			PlayerStat.activeStatsCache.Clear();
			foreach (PlayerStat playerStat in PlayerStat.playerStats)
			{
				if (playerStat.IsUnlocked(sim))
				{
					PlayerStat.activeStatsCache.Add(playerStat);
				}
			}
			return PlayerStat.activeStatsCache;
		}

		private static void AddPlayerStat(bool updateEveryFrame, string key, Func<Simulator, string> valueGetter, int id)
		{
			PlayerStat.playerStats.Add(new PlayerStat(updateEveryFrame, key, valueGetter, id));
		}

		private static void AddPlayerStat(bool updateEveryFrame, string key, Func<Simulator, string> valueGetter, int id, StatUnlockReq unlockReq)
		{
			PlayerStat.playerStats.Add(new PlayerStat(updateEveryFrame, key, valueGetter, id, unlockReq));
		}

		public bool IsUnlocked(Simulator sim)
		{
			return this.unlockReq == null || this.unlockReq.IsUnlocked(sim);
		}

		public string GetValue(Simulator sim)
		{
			return this.statGetter(sim);
		}

		private static string GetPlayerLifetime(Simulator sim)
		{
			DateTime d = new DateTime(PlayerStats.playfabCreationDate.Value);
			if (TrustedTime.IsReady())
			{
				return GameMath.GetTimeRecursive(TimeSpan.FromSeconds((TrustedTime.Get() - d).TotalSeconds));
			}
			return "UI_SHOP_CHEST_0_WAIT".Loc();
		}

		public static List<PlayerStat> playerStats;

		private static List<PlayerStat> activeStatsCache = new List<PlayerStat>();

		public const int PRESTIGES_COUNT = 10;

		public const int MAX_STAGE = 20;

		public const int SIDE_QUESTS_COMPLETED = 30;

		public const int TAPS_COUNT = 40;

		public const int MERCHANT_ITEMS_USED = 50;

		public const int FREE_CHESTS_OPENED = 60;

		public const int SECONDARY_ABILITIES_USED = 70;

		public const int DRAGONS_CAUGHT = 80;

		public const int OUTFITS_OWNED = 90;

		public const int CURSED_GATES_COMPLETED = 100;

		public const int CHARMS_PACK_OPENED = 110;

		public const int RUNES_OWNED = 120;

		public const int MAX_STAGE_PRESTIGED = 130;

		public const int LIFETIME = 140;

		public const int TRINKET_PACKS_OPENED = 150;

		public const int SIDE_QUESTS_SKIPPED = 160;

		public const int ENEMIES_KILLED = 170;

		public const int HEROES_SLAINED = 180;

		public const int MINES_COLLECTED = 190;

		public const int GOBLIN_CHEST_DESTROYED = 200;

		public const int ULTI_USED = 210;

		public const int LIFETIME_W_OFFLINE = 220;

		public readonly string nameKey;

		public readonly Func<Simulator, string> statGetter;

		public readonly bool isUpdatedEveryFrame;

		public readonly int id;

		public StatUnlockReq unlockReq;

		[CompilerGenerated]
		private static Func<Simulator, string> _003C_003Ef__mg_0024cache0;
	}
}
