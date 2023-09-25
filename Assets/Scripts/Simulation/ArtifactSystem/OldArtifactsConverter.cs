using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation.ArtifactSystem
{
	public static class OldArtifactsConverter
	{
		public static bool DoesPlayerNeedsConversion(Simulator simulator)
		{
			List<Simulation.Artifact> oldArtifacts = simulator.artifactsManager.OldArtifacts;
			if (oldArtifacts != null)
			{
				if (simulator.artifactsManager.OldArtifacts.Find((Simulation.Artifact a) => !a.IsLegendaryPlus()) != null)
				{
					return simulator.artifactsManager.Artifacts.Count == 0;
				}
			}
			return false;
		}

		public static double GetHalfConversionReward(Simulator simulator)
		{
			double num = 0.0;
			List<Simulation.Artifact> oldArtifacts = simulator.artifactsManager.OldArtifacts;
			int count = oldArtifacts.Count;
			if (count <= 0)
			{
				return 0.0;
			}
			for (int i = 0; i < count; i++)
			{
                Simulation.Artifact artifact = oldArtifacts[i];
				if (!artifact.IsLegendaryPlus())
				{
					num += artifact.GetRerollCost();
				}
			}
			num = GameMath.GetMaxDouble(30.0, num);
			if (simulator.maxStagePrestigedAt > 50)
			{
				double minDouble = GameMath.GetMinDouble(1.0, (double)simulator.maxStagePrestigedAt / 1500.0);
				double num2 = 500.0 * GameMath.PowDouble(minDouble, 5.0);
				num *= 1.0 + num2;
				UnityEngine.Debug.Log("End game distance: " + GameMath.GetDetailedNumberString(minDouble) + " - Mythstone amount multiplied by: " + GameMath.GetDetailedNumberString(num2));
			}
			return num * 0.5;
		}

		public static void Convert(Simulator simulator)
		{
			double num = OldArtifactsConverter.GetHalfConversionReward(simulator);
			simulator.GetMythstones().Increment(num);
			for (int i = 0; i < simulator.artifactsManager.AvailableSlotsCount; i++)
			{
				double craftCost = simulator.artifactsManager.GetCraftCost();
				if (num < craftCost)
				{
					break;
				}
				num -= craftCost;
				simulator.artifactsManager.TryCraftNewArtifact(simulator, false);
			}
			Main.instance.StartCoroutine(simulator.artifactsManager.BuyAllUpgradesPossible(simulator, num));
		}

		public const double MYTHSTONES_REWARD_MULTIPLIER = 500.0;
	}
}
