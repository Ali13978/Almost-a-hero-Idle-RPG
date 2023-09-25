using System;
using System.Collections.Generic;
using Simulation;

public static class StatCollector
{
	public static void LogDamage(Damage damage, UnitHealthy damaged, Unit damager)
	{
		if (damage.dontCountForDps)
		{
			return;
		}
		StatCollector.damageLogs.Add(new DamageLog
		{
			damage = damage,
			damaged = damaged,
			damager = damager
		});
	}

	public static void LogGold(double amount)
	{
		StatCollector.goldCollected += amount;
	}

	public static void ClearDamageLogs()
	{
		StatCollector.damageLogs.Clear();
	}

	public static void ClearGoldLogs()
	{
		StatCollector.goldCollected = 0.0;
	}

	public static List<DamageLog> damageLogs = new List<DamageLog>();

	public static double goldCollected;
}
