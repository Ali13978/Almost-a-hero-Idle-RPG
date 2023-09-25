using System;

namespace Simulation
{
	public class RiftUtility
	{
		public static RiftDifficulty GetDifficulty(Simulator sim, World riftWorld, ChallengeRift rift, UniversalTotalBonus universalBonus)
		{
			double powerComparison = RiftUtility.GetPowerComparison(sim, riftWorld, rift, universalBonus);
			if (powerComparison > 600.0)
			{
				return RiftDifficulty.TRIVIAL;
			}
			if (powerComparison > 25.0)
			{
				return RiftDifficulty.EASY;
			}
			if (powerComparison > 1.0)
			{
				return RiftDifficulty.MEDIUM;
			}
			if (powerComparison > 0.08)
			{
				return RiftDifficulty.HARD;
			}
			if (powerComparison > 0.002)
			{
				return RiftDifficulty.INSANE;
			}
			return RiftDifficulty.IMPOSSIBLE;
		}

		public static string GetColoredDifficultyName(RiftDifficulty diff)
		{
			switch (diff)
			{
			case RiftDifficulty.TRIVIAL:
				return "<color=#82878FFF>(" + LM.Get("RIFT_DIFFICULTY_TRIVIAL") + ")</color>";
			case RiftDifficulty.EASY:
				return "<color=#8FAE47FF>(" + LM.Get("RIFT_DIFFICULTY_EASY") + ")</color>";
			case RiftDifficulty.MEDIUM:
				return "<color=#E09028FF>(" + LM.Get("RIFT_DIFFICULTY_MEDIUM") + ")</color>";
			case RiftDifficulty.HARD:
				return "<color=#BC5700FF>(" + LM.Get("RIFT_DIFFICULTY_HARD") + ")</color>";
			case RiftDifficulty.INSANE:
				return "<color=#C63C31FF>(" + LM.Get("RIFT_DIFFICULTY_INSANE") + ")</color>";
			case RiftDifficulty.IMPOSSIBLE:
				return "<color=#9E1713FF>(" + LM.Get("RIFT_DIFFICULTY_IMPOSSIBLE") + ")</color>";
			default:
				return "???";
			}
		}

		public static double GetPowerComparison(Simulator sim, World riftWorld, ChallengeRift rift, UniversalTotalBonus universalBonus)
		{
			int p = riftWorld.heroLevelJumps[0][rift.heroStartLevel];
			int averageEvolveLevel = RiftUtility.GetAverageEvolveLevel(sim);
			double num = 15.0;
			double num2 = num * GameMath.PowInt(1.3, p) * GameMath.PowInt(1.5, averageEvolveLevel) * (universalBonus.damageFactor + (universalBonus.damageHeroFactor - 1.0)) * universalBonus.gearDamageFactor * universalBonus.charmDamageFactor * universalBonus.mineDamageFactor;
			double num3 = 3.0;
			double num4 = num3 * UnitMath.GetDamageForPowerGog(rift.baseEnemyPower);
			return num2 / num4;
		}

		private static int GetAverageEvolveLevel(Simulator sim)
		{
			float num = 0f;
			foreach (HeroDataBase heroDataBase in sim.GetAllHeroes())
			{
				if (heroDataBase.evolveLevel > 1)
				{
					num += (float)heroDataBase.evolveLevel;
				}
				else
				{
					num += 1f;
				}
			}
			return (int)(num / (float)sim.GetAllHeroes().Count);
		}
	}
}
