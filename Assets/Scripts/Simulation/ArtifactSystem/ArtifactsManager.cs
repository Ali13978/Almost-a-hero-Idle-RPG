using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Static;

namespace Simulation.ArtifactSystem
{
	public class ArtifactsManager
	{
		public ArtifactsManager()
		{
			this.Artifacts = new List<Simulation.ArtifactSystem.Artifact>();
			this.MythicalArtifacts = new List<Simulation.Artifact>();
			this.OldArtifacts = new List<Simulation.Artifact>();
			this.cachedUniqueEffectsCopies = new Dictionary<int, int>();
			this.cachedUniqueEffectsStock = new Dictionary<int, int>();
			this.cachedAvailableUniqueEffects = new List<int>();
			this.TotalArtifactsLevel = 0;
			this.currentTotalArtifactLevelMilestoneIndex = 0;
			this.AvailableSlotsCount = 4;
			this.NumArtifactSlotsMythical = 0;
			this.UpdateUniqueEffectCaches();
		}

		public void LoadState(List<Artifact> artifacts, List<Simulation.Artifact> oldArtifacts, int availableSlotsCount)
		{
			if (availableSlotsCount > this.AvailableSlotsCount)
			{
				this.AvailableSlotsCount = availableSlotsCount;
			}
			if (oldArtifacts != null)
			{
				this.OldArtifacts = oldArtifacts;
				foreach (Simulation.Artifact artifact in oldArtifacts)
				{
					if (artifact.IsLegendaryPlus())
					{
						this.MythicalArtifacts.Add(artifact);
						this.TotalArtifactsLevel += artifact.GetLegendaryPlusRank() + 1;
					}
				}
			}
			if (artifacts != null)
			{
				this.Artifacts = artifacts;
				for (int i = this.Artifacts.Count - 1; i >= 0; i--)
				{
                    Simulation.ArtifactSystem.Artifact artifact2 = this.Artifacts[i];
					artifact2.CraftIndex = i;
					this.TotalArtifactsLevel += artifact2.Level;
					for (int j = artifact2.UniqueEffectsIds.Count - 1; j >= 0; j--)
					{
						this.CacheUniqueEffectCopiesFrom(artifact2, j);
					}
				}
				this.UpdateUniqueEffectCaches();
			}
			this.currentTotalArtifactLevelMilestoneIndex = this.GetCurrentTotalArtifactsLevelUnlockIndex();
			PlayerStats.OnTotalArtifactsLevelChanged(this.TotalArtifactsLevel);
		}

		public void UpdateCurrentTotalArtifactsLevelMilestone()
		{
			this.currentTotalArtifactLevelMilestoneIndex = this.GetCurrentTotalArtifactsLevelUnlockIndex();
		}

		public void ApplyEffects(UniversalTotalBonus universalTotalBonus)
		{
			int count = this.MythicalArtifacts.Count;
			for (int i = 0; i < count; i++)
			{
				this.MythicalArtifacts[i].PreApply(universalTotalBonus);
			}
			for (int j = 0; j < count; j++)
			{
				this.MythicalArtifacts[j].CalculateAdditionalBonuses(universalTotalBonus);
			}
			for (int k = 0; k < count; k++)
			{
				this.MythicalArtifacts[k].Apply(universalTotalBonus, this.TotalArtifactsLevel);
			}
			for (int l = 0; l < EffectIds.Unique.Length; l++)
			{
				int key = EffectIds.Unique[l];
				int level = 0;
				if (this.cachedUniqueEffectsCopies.TryGetValue(key, out level))
				{
					EffectsDatabase.Unique[key].Effect.Apply(universalTotalBonus, level);
				}
			}
			for (int m = this.Artifacts.Count - 1; m >= 0; m--)
			{
				EffectsDatabase.Common[this.Artifacts[m].CommonEffectId].Apply(universalTotalBonus, this.Artifacts[m].Level);
			}
			universalTotalBonus.healthHeroFactor *= universalTotalBonus.healthHeroFactorMuliplier;
		}

		public IEnumerator BuyAllUpgradesPossible(Simulator simulator, double buddget)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			double lastPurchaseCost = 0.0;
			do
			{
				if (stopwatch.ElapsedMilliseconds > 10L)
				{
					stopwatch.Stop();
					stopwatch.Reset();
					yield return null;
					stopwatch.Start();
				}
				buddget -= lastPurchaseCost;
				lastPurchaseCost = this.BuyCheapestThingPossibleAndReturnCost(simulator, buddget);
				this.UpdateCurrentTotalArtifactsLevelMilestone();
			}
			while (lastPurchaseCost >= 0.0);
			simulator.OnArtifactsChanged();
			simulator.isCataclysmSurviver = true;
			yield break;
		}

		public int GetTotalAmountOfArtifacts()
		{
			return this.MythicalArtifacts.Count + this.Artifacts.Count;
		}

		public bool HasUnlockedNewTotalArtifactsLevelMilestone()
		{
			return this.currentTotalArtifactLevelMilestoneIndex < this.GetCurrentTotalArtifactsLevelUnlockIndex();
		}

		public bool HasRechedTotalArtifactLevelMaxMilestone()
		{
			return this.currentTotalArtifactLevelMilestoneIndex + 1 >= EffectsDatabase.TotalArtifactLevelMilestones.Length;
		}

		public double GetCommonEffectTotalValue(int effectId, UniversalTotalBonus universalTotalBonus)
		{
			double num = 0.0;
			for (int i = this.Artifacts.Count - 1; i >= 0; i--)
			{
                Simulation.ArtifactSystem.Artifact artifact = this.Artifacts[i];
				if (artifact.CommonEffectId == effectId)
				{
					num += EffectsDatabase.Common[effectId].GetValue(artifact.Level, universalTotalBonus);
				}
			}
			return num;
		}

		public bool IsMultipleUpgradeUnlocked()
		{
			return this.TotalArtifactsLevel >= 50;
		}

		public int GetMaxLevelJump(Artifact artifact)
		{
			return GameMath.Clamp(200 + this.GetMaxLevelIncreaseByArtifacts() - artifact.Level, 0, int.MaxValue);
		}

		public bool IsLevelMaxed(Artifact artifact)
		{
			return artifact.Level >= 200 + this.GetMaxLevelIncreaseByArtifacts();
		}

		public int GetMaxLevelCap()
		{
			return 200 + this.GetMaxLevelIncreaseByArtifacts();
		}

		public bool IsMaxed(Artifact artifact)
		{
			return ArtifactsManager.LevelRequiredForNextRarity[ArtifactsManager.LevelRequiredForNextRarity.Length - 1] + this.GetMaxLevelIncreaseByArtifacts() <= artifact.Level;
		}

		public bool IsUniqueStatCountMaxed(Artifact artifact)
		{
			return artifact.UniqueEffectsIds.Count >= 4;
		}

		public bool ReachedMinLevelForNextRarity(Artifact artifact)
		{
			return this.GetRequiredLevelToEvolve(artifact, artifact.Rarity) <= artifact.Level;
		}

		public bool IsMaxRarity(Artifact artifact)
		{
			return artifact.Rarity >= ArtifactsManager.LevelRequiredForNextRarity.Length - 1;
		}

		public int GetRequiredLevelToEvolve(Artifact artifact, int rarity)
		{
			int artifactStartLevel = this.GetArtifactStartLevel(artifact.CraftIndex);
			int num = ArtifactsManager.LevelRequiredForNextRarity[rarity];
			if (artifact.CraftIndex != 0)
			{
				num += artifactStartLevel;
			}
			return num;
		}

		public string GetDescriptionOfEffectWithId(int effectId)
		{
			if (this.IsIdFromCommonEffect(effectId))
			{
				return EffectsDatabase.Common[effectId].GetDescription();
			}
			return EffectsDatabase.Unique[effectId].Effect.GetDescription();
		}

		public bool IsIdFromCommonEffect(int effectId)
		{
			return effectId <= 99;
		}

		public string GetCommonEffectValueOf(Artifact artifact, UniversalTotalBonus universalTotalBonus)
		{
			return EffectsDatabase.Common[artifact.CommonEffectId].GetValueString(artifact.Level, universalTotalBonus);
		}

		public string GetUniqueEffectBaseValue(int effectId, UniversalTotalBonus universalTotalBonus)
		{
			return EffectsDatabase.Unique[effectId].Effect.GetValueString(1, universalTotalBonus);
		}

		public string GetUniqueEffectBaseSignedValue(int effectId, UniversalTotalBonus universalTotalBonus)
		{
			return EffectsDatabase.Unique[effectId].Effect.GetSignedValue(1, universalTotalBonus);
		}

		public string GetUniqueEffectTotalValue(int effectId, UniversalTotalBonus universalTotalBonus)
		{
			return EffectsDatabase.Unique[effectId].Effect.GetValueString(this.GetCurrentCopiesOfUniqueEffect(effectId), universalTotalBonus);
		}

		public int GetMaxCopiesAllowedOfUniqueEffect(int effectId)
		{
			List<int> artifactTotalLevelToUnlockEachCopy = EffectsDatabase.Unique[effectId].ArtifactTotalLevelToUnlockEachCopy;
			int num = 0;
			for (int i = 0; i < artifactTotalLevelToUnlockEachCopy.Count; i++)
			{
				if (artifactTotalLevelToUnlockEachCopy[i] <= this.TotalArtifactsLevel)
				{
					num++;
				}
			}
			return num;
		}

		public int GetCurrentCopiesOfUniqueEffect(int effectId)
		{
			int result = 0;
			this.cachedUniqueEffectsCopies.TryGetValue(effectId, out result);
			return result;
		}

		public List<int> GetCopyCountsOfAllUniqueEffects()
		{
			List<int> list = new List<int>();
			foreach (KeyValuePair<int, EffectsDatabase.UniqueEffectInfo> keyValuePair in EffectsDatabase.Unique)
			{
				int num;
				if (this.cachedUniqueEffectsStock.TryGetValue(keyValuePair.Key, out num))
				{
					for (int i = 0; i < num; i++)
					{
						list.Add(keyValuePair.Key);
					}
				}
			}
			return list;
		}

		public int GetUniqueEffectsStockCount()
		{
			int num = 0;
			foreach (KeyValuePair<int, EffectsDatabase.UniqueEffectInfo> keyValuePair in EffectsDatabase.Unique)
			{
				int num2;
				if (this.cachedUniqueEffectsStock.TryGetValue(keyValuePair.Key, out num2))
				{
					num += num2;
				}
			}
			return num;
		}

		public List<KeyValuePair<int, int>> GetEffectUnlocksInfoForCurrentTotalLevelMilestone()
		{
			return this.GetEffectsUnlockInfoForTotalArtifactLevelMilestoneIndex(this.currentTotalArtifactLevelMilestoneIndex);
		}

		public List<KeyValuePair<int, int>> GetEffectUnlocksInfoForNextTotalLevelMilestone()
		{
			return (this.currentTotalArtifactLevelMilestoneIndex + 1 >= EffectsDatabase.TotalArtifactLevelMilestones.Length) ? null : this.GetEffectsUnlockInfoForTotalArtifactLevelMilestoneIndex(this.currentTotalArtifactLevelMilestoneIndex + 1);
		}

		public List<KeyValuePair<int, int>> GetInfoOfAllUniqueEffectsAvailableAtCurrentTotalLevelMilestone()
		{
			List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
			for (int i = 0; i < EffectIds.Unique.Length; i++)
			{
				int num = EffectIds.Unique[i];
				EffectsDatabase.UniqueEffectInfo uniqueEffectInfo = EffectsDatabase.Unique[num];
				int maxCopiesAllowedOfUniqueEffect = this.GetMaxCopiesAllowedOfUniqueEffect(num);
				if (maxCopiesAllowedOfUniqueEffect > 0)
				{
					list.Add(new KeyValuePair<int, int>(num, maxCopiesAllowedOfUniqueEffect));
				}
			}
			return list;
		}

		public int GetTotalArtifactsLevelOfCurrentMilestone()
		{
			return EffectsDatabase.TotalArtifactLevelMilestones[this.currentTotalArtifactLevelMilestoneIndex];
		}

		public int GetTotalArtifactsLevelOfNextMilestone()
		{
			return (this.currentTotalArtifactLevelMilestoneIndex + 1 >= EffectsDatabase.TotalArtifactLevelMilestones.Length) ? -1 : EffectsDatabase.TotalArtifactLevelMilestones[this.currentTotalArtifactLevelMilestoneIndex + 1];
		}

		public bool AreThereSlotsAvailableToPurchase()
		{
			return this.AvailableSlotsCount < 40;
		}

		public double GetSlotCost()
		{
			double num = 20.0 + 10.0 * (double)(this.AvailableSlotsCount - 4);
			return (num <= 370.0) ? num : 370.0;
		}

		public bool CanPurchaseASlot(Simulator simulator)
		{
			return this.AreThereSlotsAvailableToPurchase() && simulator.GetCurrency(CurrencyType.GEM).CanAfford(this.GetSlotCost());
		}

		public void TryToPurchaseASlot(Simulator simulator)
		{
			if (this.CanPurchaseASlot(simulator))
			{
				PlayfabManager.SendPlayerEvent(PlayfabEventId.ARTIFACT_SLOT_UNLOCKED, new Dictionary<string, object>
				{
					{
						"slot",
						this.AvailableSlotsCount + 1
					},
					{
						"spent_currency",
						CurrencyType.GEM
					},
					{
						"spent_amount",
						this.GetSlotCost()
					},
					{
						"num_crafted",
						this.Artifacts.Count
					},
					{
						"tal",
						this.TotalArtifactsLevel
					}
				}, null, null, true);
				simulator.GetCurrency(CurrencyType.GEM).Decrement(this.GetSlotCost());
				this.AvailableSlotsCount++;
			}
		}

		public bool AreThereEmptyArtifactSlots()
		{
			return this.Artifacts.Count < this.AvailableSlotsCount;
		}

		public List<int> GetAvailableEffects()
		{
			return this.cachedAvailableUniqueEffects;
		}

		public bool CanCraftAnArtifact(Simulator simulator)
		{
			return this.AreThereEmptyArtifactSlots() && this.CanAffordCraftingArtifact(simulator);
		}

		public bool CanAffordCraftingArtifact(Simulator simulator)
		{
			return simulator.GetCurrency(CurrencyType.MYTHSTONE).CanAfford(this.GetCraftCost());
		}

		public double GetCraftCost()
		{
			return this.CRAFT_COSTS[this.Artifacts.Count];
		}

		public void TryCraftNewArtifact(Simulator simulator, bool sendPlayfabEvent)
		{
			if (this.CanCraftAnArtifact(simulator))
			{
				Artifact artifact = new Artifact
				{
					Level = this.GetArtifactStartLevel(this.Artifacts.Count),
					CommonEffectId = EffectIds.Common[this.Artifacts.Count % EffectIds.Common.Length],
					UniqueEffectsIds = new List<int>(),
					CraftIndex = this.Artifacts.Count
				};
				this.TotalArtifactsLevel += artifact.Level;
				this.UpdateUniqueEffectCaches();
				simulator.GetCurrency(CurrencyType.MYTHSTONE).Decrement(this.GetCraftCost());
				this.Artifacts.Add(artifact);
				simulator.OnRegularArtifactCrafted();
				PlayerStats.OnTotalArtifactsLevelChanged(this.TotalArtifactsLevel);
				if (sendPlayfabEvent)
				{
					PlayfabManager.SendPlayerEvent(PlayfabEventId.ARTIFACT_REGULAR_CRAFT, new Dictionary<string, object>
					{
						{
							"num_crafted",
							this.Artifacts.Count
						},
						{
							"num_slots_unlocked",
							this.AvailableSlotsCount
						},
						{
							"tal",
							this.TotalArtifactsLevel
						}
					}, null, null, true);
				}
			}
		}

		public bool CanUpgrade(Artifact artifact, Simulator simulator)
		{
			return !this.IsLevelMaxed(artifact) && simulator.GetCurrency(CurrencyType.MYTHSTONE).CanAfford(this.GetUpgradeCostOf(artifact, simulator.GetUniversalBonusAll()));
		}

		public bool CanUpgrade(Artifact artifact, int levelJump, Simulator simulator)
		{
			int maxLevelJump = this.GetMaxLevelJump(artifact);
			return !this.IsLevelMaxed(artifact) && maxLevelJump >= levelJump && simulator.GetCurrency(CurrencyType.MYTHSTONE).CanAfford(this.GetUpgradeCostOf(artifact, levelJump, simulator.GetUniversalBonusAll())) && levelJump > 0;
		}

		public double GetUpgradeCostOf(Artifact artifact, UniversalTotalBonus universalTotalBonus)
		{
			return Math.Ceiling(ArtifactsManager.GetBaseUpgareCostForLevel(artifact.Level) * universalTotalBonus.artifactUpgradeCostFactor);
		}

		public double GetUpgradeCostOfLevel(int level, UniversalTotalBonus universalTotalBonus)
		{
			return Math.Ceiling(ArtifactsManager.GetBaseUpgareCostForLevel(level) * universalTotalBonus.artifactUpgradeCostFactor);
		}

		public double GetUpgradeCostOf(Artifact artifact, int levelJump, UniversalTotalBonus universalTotalBonus)
		{
			double num = 0.0;
			int num2 = artifact.Level + levelJump;
			for (int i = artifact.Level; i < num2; i++)
			{
				num += ArtifactsManager.GetBaseUpgareCostForLevel(i);
			}
			return Math.Ceiling(num * universalTotalBonus.artifactUpgradeCostFactor);
		}

		public int GetLevelCountForPrice(Artifact artifact, double bid, UniversalTotalBonus universalTotalBonus)
		{
			int num = 0;
			int num2 = artifact.Level;
			double upgradeCostOfLevel = this.GetUpgradeCostOfLevel(num2, universalTotalBonus);
			while ((upgradeCostOfLevel <= bid || Cheats.allFree) && num2 < 200 + this.GetMaxLevelIncreaseByArtifacts())
			{
				upgradeCostOfLevel = this.GetUpgradeCostOfLevel(num2, universalTotalBonus);
				num++;
				num2++;
				bid -= upgradeCostOfLevel;
				upgradeCostOfLevel = this.GetUpgradeCostOfLevel(num2, universalTotalBonus);
			}
			return num;
		}

		private static double GetBaseUpgareCostForLevel(int level)
		{
			return 10.0 + 3.0 * GameMath.PowDouble(1.1, GameMath.PowDouble((double)level, 1.04));
		}

		public void TryToUpgrade(Artifact artifact, Simulator simulator)
		{
			if (this.CanUpgrade(artifact, simulator))
			{
				this.Upgrade(artifact, simulator);
				simulator.OnArtifactsChanged();
			}
		}

		public void TryToUpgrade(Artifact artifact, int levelJump, Simulator simulator)
		{
			if (this.CanUpgrade(artifact, levelJump, simulator))
			{
				double upgradeCostOf = this.GetUpgradeCostOf(artifact, levelJump, simulator.GetUniversalBonusAll());
				artifact.Level += levelJump;
				this.TotalArtifactsLevel += levelJump;
				simulator.GetCurrency(CurrencyType.MYTHSTONE).Decrement(upgradeCostOf);
				this.UpdateUniqueEffectCaches();
				simulator.OnArtifactsChanged();
				PlayerStats.OnTotalArtifactsLevelChanged(this.TotalArtifactsLevel);
			}
		}

		private void Upgrade(Artifact artifact, Simulator simulator)
		{
			double upgradeCostOf = this.GetUpgradeCostOf(artifact, simulator.GetUniversalBonusAll());
			artifact.Level++;
			this.TotalArtifactsLevel++;
			simulator.GetCurrency(CurrencyType.MYTHSTONE).Decrement(upgradeCostOf);
			this.UpdateUniqueEffectCaches();
			PlayerStats.OnTotalArtifactsLevelChanged(this.TotalArtifactsLevel);
		}

		public float GetEvolveProgress(Artifact artifact)
		{
			int requiredLevelToEvolve = this.GetRequiredLevelToEvolve(artifact, artifact.Rarity);
			int num = this.GetArtifactStartLevel(artifact);
			if (artifact.Rarity > 0)
			{
				num = this.GetRequiredLevelToEvolve(artifact, artifact.Rarity - 1);
			}
			float num2 = (float)(requiredLevelToEvolve - num);
			return (float)(artifact.Level - num) / num2;
		}

		public bool CanEvolve(Artifact artifact, Simulator simulator)
		{
			return !this.IsUniqueStatCountMaxed(artifact) && this.ReachedMinLevelForNextRarity(artifact) && this.ThereAreAvailableUniqueEffectsToObtain(-1) && this.CanAffordEvolve(artifact, simulator);
		}

		public bool CanAffordEvolve(Artifact artifact, Simulator simulator)
		{
			return simulator.GetCurrency(CurrencyType.MYTHSTONE).CanAfford(this.GetEvolveCostOf(artifact));
		}

		public double GetEvolveCostOf(Artifact artifact)
		{
			return 0.0;
		}

		public void TryToEvolve(Artifact artifact, Simulator simulator, bool sendPlayfabEvent)
		{
			if (this.CanEvolve(artifact, simulator))
			{
				this.Evolve(artifact, simulator);
				simulator.OnArtifactsChanged();
				if (sendPlayfabEvent)
				{
					PlayfabManager.SendPlayerEvent(PlayfabEventId.ARTIFACT_EVOLVE, new Dictionary<string, object>
					{
						{
							"slot",
							artifact.CraftIndex
						},
						{
							"rarity",
							artifact.Rarity
						}
					}, null, null, true);
				}
			}
		}

		private void Evolve(Artifact artifact, Simulator simulator)
		{
			simulator.GetCurrency(CurrencyType.MYTHSTONE).Decrement(this.GetEvolveCostOf(artifact));
			artifact.UniqueEffectsIds.Add(this.GetRandomUniqueEffect(-1));
			this.CacheUniqueEffectCopiesFrom(artifact, artifact.UniqueEffectsIds.Count - 1);
			this.UpdateUniqueEffectCaches();
		}

		public bool CanReroll(Artifact artifact, int effectIndex, Simulator simulator)
		{
			return effectIndex > -1 && effectIndex < artifact.Rarity && this.ThereAreAvailableUniqueEffectsToObtain(artifact.UniqueEffectsIds[effectIndex]) && this.CanAffordRerrolOf(artifact, effectIndex, simulator);
		}

		public bool CanRerollWithoutAffording(Artifact artifact, int effectIndex, Simulator simulator)
		{
			return effectIndex > -1 && effectIndex < artifact.Rarity && this.ThereAreAvailableUniqueEffectsToObtain(artifact.UniqueEffectsIds[effectIndex]);
		}

		public bool CanAffordRerrolOf(Artifact artifact, int effectIndex, Simulator simulator)
		{
			return simulator.GetCurrency(CurrencyType.MYTHSTONE).CanAfford(this.GetRerollCostOf(artifact, effectIndex));
		}

		public double GetRerollCostOf(Artifact artifact, int effectIndex)
		{
			int requiredLevelToEvolve = this.GetRequiredLevelToEvolve(artifact, effectIndex);
			double baseUpgareCostForLevel = ArtifactsManager.GetBaseUpgareCostForLevel(requiredLevelToEvolve);
			return 0.5 * baseUpgareCostForLevel;
		}

		public void TryToReroll(Artifact artifact, int effectIndex, Simulator simulator)
		{
			if (this.CanReroll(artifact, effectIndex, simulator))
			{
				int num = artifact.UniqueEffectsIds[effectIndex];
				Dictionary<int, int> dictionary;
				int key;
				(dictionary = this.cachedUniqueEffectsCopies)[key = num] = dictionary[key] - 1;
				artifact.UniqueEffectsIds[effectIndex] = this.GetRandomUniqueEffect(num);
				this.CacheUniqueEffectCopiesFrom(artifact, effectIndex);
				this.UpdateUniqueEffectCaches();
				simulator.GetCurrency(CurrencyType.MYTHSTONE).Decrement(this.GetRerollCostOf(artifact, effectIndex));
				simulator.OnArtifactsChanged();
			}
		}

		public bool CanCraftMythicalArtifact(Simulator simulator)
		{
			return this.MythicalArtifacts.Count < Simulation.Artifact.GetLegendaryEffectsTotalCount() && this.HaveRoomToCraftMythicalArtifact() && this.CanAffordMythicalArtifactCraft(simulator);
		}

		public bool HaveRoomToCraftMythicalArtifact()
		{
			return this.MythicalArtifacts.Count < this.NumArtifactSlotsMythical;
		}

		public double GetMythicalArtifactCraftCost()
		{
			return ArtifactsManager.NEW_MYTHICAL_ARTIFACT_COSTS[this.MythicalArtifacts.Count];
		}

		public bool TryCraftNewMythicalArtifact(Simulator simulator)
		{
			if (this.CanCraftMythicalArtifact(simulator))
			{
				double mythicalArtifactCraftCost = this.GetMythicalArtifactCraftCost();
				simulator.GetMythstones().Decrement(mythicalArtifactCraftCost);
                Simulation.Artifact artifact = Simulation.Artifact.CreateMythical(mythicalArtifactCraftCost, this.MythicalArtifacts, this.MythicalArtifacts.Count);
				this.MythicalArtifacts.Add(artifact);
				this.TotalArtifactsLevel++;
				this.OldArtifacts.Add(artifact);
				simulator.OnMythicalArtifactCrafted();
				PlayerStats.OnTotalArtifactsLevelChanged(this.TotalArtifactsLevel);
				PlayfabManager.SendPlayerEvent(PlayfabEventId.ARTIFACT_MYTHICAL_CRAFT, new Dictionary<string, object>
				{
					{
						"name",
						artifact.GetNameEN()
					},
					{
						"num_crafted",
						this.MythicalArtifacts.Count
					},
					{
						"num_slots_unlocked",
						this.NumArtifactSlotsMythical
					},
					{
						"tal",
						this.TotalArtifactsLevel
					}
				}, null, null, true);
				return true;
			}
			return false;
		}

		public bool CanAffordArtifactUpgrade(Simulation.Artifact artifact, Simulator simulator)
		{
			double upgradeCost = artifact.GetUpgradeCost();
			return simulator.GetMythstones().CanAfford(upgradeCost);
		}

		public bool CanAffordArtifactUpgrade(Simulation.Artifact artifact, Simulator simulator, int levelJump)
		{
			double upgradeCost = artifact.GetUpgradeCost(levelJump);
			return simulator.GetMythstones().CanAfford(upgradeCost);
		}

		public bool CanUpgradeArtifact(Simulation.Artifact artifact)
		{
			return artifact.CanUpgrade();
		}

		public bool CanAffordMythicalArtifactCraft(Simulator simulator)
		{
			return simulator.GetMythstones().CanAfford(this.GetMythicalArtifactCraftCost());
		}

		public void TryUpgradeMythicalArtifact(int index, Simulator simulator, int jumpCount)
		{
			this.TryUpgradeMythicalArtifact(this.MythicalArtifacts[index], simulator, jumpCount);
		}

		public void TryUpgradeMythicalArtifact(Simulation.Artifact artifact, Simulator simulator, int jumpCount)
		{
			if (!this.CanUpgradeArtifact(artifact))
			{
				return;
			}
			if (!this.CanAffordArtifactUpgrade(artifact, simulator, jumpCount))
			{
				return;
			}
			double upgradeCost = artifact.GetUpgradeCost(jumpCount);
			simulator.GetMythstones().Decrement(upgradeCost);
			artifact.Upgrade(jumpCount);
			this.TotalArtifactsLevel += jumpCount;
			this.UpdateUniqueEffectCaches();
			simulator.OnArtifactsChanged();
			PlayerStats.OnTotalArtifactsLevelChanged(this.TotalArtifactsLevel);
		}

		public void TryEnableArtifact(Simulation.Artifact artifact, Simulator simulator)
		{
			artifact.TryEnable();
			simulator.OnArtifactsChanged();
		}

		public void TryDisableArtifact(Simulation.Artifact artifact, Simulator simulator)
		{
			artifact.TryDisable();
			simulator.OnArtifactsChanged();
		}

		private void CacheUniqueEffectCopiesFrom(Artifact artifact, int effectIndex)
		{
			int num = artifact.UniqueEffectsIds[effectIndex];
			if (this.cachedUniqueEffectsCopies.ContainsKey(num))
			{
				Dictionary<int, int> dictionary;
				int key;
				(dictionary = this.cachedUniqueEffectsCopies)[key = num] = dictionary[key] + 1;
			}
			else
			{
				this.cachedUniqueEffectsCopies.Add(num, 1);
			}
		}

		private void UpdateUniqueEffectCaches()
		{
			int num = this.currentTotalArtifactLevelMilestoneIndex;
			if (num < EffectsDatabase.TotalArtifactLevelMilestones.Length && EffectsDatabase.TotalArtifactLevelMilestones[num] <= this.TotalArtifactsLevel)
			{
				this.cachedAvailableUniqueEffects.Clear();
				this.cachedUniqueEffectsStock.Clear();
				this.totalUniqueEffectStock = 0;
				this.ownedUniqueEffectStock = 0;
				for (int i = 0; i < EffectIds.Unique.Length; i++)
				{
					int num2 = EffectIds.Unique[i];
					int maxCopiesAllowedOfUniqueEffect = this.GetMaxCopiesAllowedOfUniqueEffect(num2);
					int currentCopiesOfUniqueEffect = this.GetCurrentCopiesOfUniqueEffect(num2);
					this.totalUniqueEffectStock += maxCopiesAllowedOfUniqueEffect;
					this.ownedUniqueEffectStock += currentCopiesOfUniqueEffect;
					int num3 = maxCopiesAllowedOfUniqueEffect - currentCopiesOfUniqueEffect;
					this.cachedUniqueEffectsStock.Add(num2, num3);
					if (num3 > 0)
					{
						this.cachedAvailableUniqueEffects.Add(num2);
					}
				}
			}
		}

		private int GetRandomUniqueEffect(int avoidEffectId = -1)
		{
			if (avoidEffectId != -1)
			{
				this.cachedAvailableUniqueEffects.Remove(avoidEffectId);
				int randomListElement = this.cachedAvailableUniqueEffects.GetRandomListElement(GameMath.RandType.Artifact);
				this.cachedAvailableUniqueEffects.Add(avoidEffectId);
				return randomListElement;
			}
			return this.cachedAvailableUniqueEffects.GetRandomListElement(GameMath.RandType.Artifact);
		}

		public bool ThereAreAvailableUniqueEffectsToObtain(int ignoreEffectId = -1)
		{
			return this.cachedAvailableUniqueEffects.Count > ((ignoreEffectId == -1 || !this.cachedAvailableUniqueEffects.Contains(ignoreEffectId)) ? 0 : 1);
		}

		private int GetCurrentTotalArtifactsLevelUnlockIndex()
		{
			int num = 0;
			while (num < EffectsDatabase.TotalArtifactLevelMilestones.Length && EffectsDatabase.TotalArtifactLevelMilestones[num] <= this.TotalArtifactsLevel)
			{
				num++;
			}
			return num - 1;
		}

		private List<KeyValuePair<int, int>> GetEffectsUnlockInfoForTotalArtifactLevelMilestoneIndex(int totalArtifactsLevelMilestoneIndex)
		{
			List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
			int element = EffectsDatabase.TotalArtifactLevelMilestones[totalArtifactsLevelMilestoneIndex];
			for (int i = 0; i < EffectIds.Unique.Length; i++)
			{
				int key = EffectIds.Unique[i];
				EffectsDatabase.UniqueEffectInfo uniqueEffectInfo = EffectsDatabase.Unique[key];
				int num = uniqueEffectInfo.ArtifactTotalLevelToUnlockEachCopy.NumAppearencesOf(element);
				if (num > 0)
				{
					list.Add(new KeyValuePair<int, int>(key, num));
				}
			}
			return list;
		}

		private int GetArtifactStartLevel(Artifact artifact)
		{
			return this.GetArtifactStartLevel(artifact.CraftIndex);
		}

		private int GetArtifactStartLevel(int craftIndex)
		{
			if (craftIndex == 0)
			{
				return 1;
			}
			return craftIndex * 5;
		}

		private double BuyCheapestThingPossibleAndReturnCost(Simulator simulator, double budget)
		{
			int num = -1;
			bool flag = false;
			double num2 = double.PositiveInfinity;
			List<Artifact> artifacts = this.Artifacts;
			int i = 0;
			int count = artifacts.Count;
			while (i < count)
			{
				Artifact artifact = artifacts[i];
				if (!this.IsUniqueStatCountMaxed(artifact) && this.ReachedMinLevelForNextRarity(artifact) && this.ThereAreAvailableUniqueEffectsToObtain(-1))
				{
					double evolveCostOf = this.GetEvolveCostOf(artifact);
					if (evolveCostOf <= budget && (evolveCostOf < num2 || (evolveCostOf == num2 && !flag)))
					{
						num = i;
						flag = true;
						num2 = this.GetEvolveCostOf(artifact);
					}
				}
				else if (!this.IsLevelMaxed(artifact))
				{
					double upgradeCostOf = this.GetUpgradeCostOf(artifact, simulator.GetUniversalBonusAll());
					if (upgradeCostOf < budget && upgradeCostOf < num2)
					{
						num = i;
						flag = false;
						num2 = this.GetUpgradeCostOf(artifact, simulator.GetUniversalBonusAll());
					}
				}
				i++;
			}
			if (num != -1)
			{
				if (flag)
				{
					this.Evolve(artifacts[num], simulator);
				}
				else
				{
					this.Upgrade(artifacts[num], simulator);
				}
				return num2;
			}
			return -1.0;
		}

		private int GetMaxLevelIncreaseByArtifacts()
		{
            Simulation.Artifact artifact = this.MythicalArtifacts.Find((Simulation.Artifact a) => a.effects[0] is MythicalArtifactPerfectQuasi);
			return (artifact != null) ? ((int)artifact.effects[0].GetAmount()) : 0;
		}

		public static int BASE_LEVEL_CAP = 200;

		public const CurrencyType SLOT_COST_TYPE = CurrencyType.GEM;

		public const CurrencyType CRAFT_COST_TYPE = CurrencyType.MYTHSTONE;

		public const CurrencyType UPGRADE_COST_TYPE = CurrencyType.MYTHSTONE;

		public const CurrencyType EVOLVE_COST_TYPE = CurrencyType.MYTHSTONE;

		public const CurrencyType REROLL_COST_TYPE = CurrencyType.MYTHSTONE;

		public const int MAX_ARTIFACT_SLOTS = 40;

		private const int MaxArtifactLevel = 200;

		public static readonly int[] LevelRequiredForNextRarity = new int[]
		{
			5,
			30,
			50,
			100
		};

		public List<Simulation.ArtifactSystem.Artifact> Artifacts;

		public List<Simulation.Artifact> MythicalArtifacts;

		public List<Simulation.Artifact> OldArtifacts;

		public int AvailableSlotsCount;

		public int NumArtifactSlotsMythical;

		public int TotalArtifactsLevel;

		private Dictionary<int, int> cachedUniqueEffectsCopies;

		private Dictionary<int, int> cachedUniqueEffectsStock;

		private List<int> cachedAvailableUniqueEffects;

		private int currentTotalArtifactLevelMilestoneIndex;

		public int totalUniqueEffectStock;

		public int ownedUniqueEffectStock;

		private const double SLOT_COST_BASE = 20.0;

		private const double SLOT_COST_MAX = 370.0;

		private const double SLOT_COST_PER_EACH_UNLOCKED = 10.0;

		private const double UPGRADE_COST_BASE = 10.0;

		private const double UPGRADE_COST_MUL = 3.0;

		private const double UPGRADE_COST_EXP_LEVEL = 1.04;

		private const double UPGRADE_COST_EXP = 1.1;

		private const double REROLL_COST_MOD = 0.5;

		private const int MIN_TAL_TO_ALLOW_MULTIPLE_UPGRADE = 50;

		private const int INITIAL_NUM_ARTIFACT_SLOTS = 4;

		private double[] CRAFT_COSTS = new double[]
		{
			1.0,
			40.0,
			80.0,
			200.0,
			400.0,
			800.0,
			2000.0,
			4000.0,
			8000.0,
			20000.0,
			40000.0,
			80000.0,
			200000.0,
			400000.0,
			800000.0,
			1000000.0,
			2000000.0,
			4000000.0,
			8000000.0,
			20000000.0,
			40000000.0,
			80000000.0,
			100000000.0,
			200000000.0,
			400000000.0,
			800000000.0,
			1000000000.0,
			2000000000.0,
			4000000000.0,
			8000000000.0,
			10000000000.0,
			30000000000.0,
			60000000000.0,
			90000000000.0,
			200000000000.0,
			500000000000.0,
			800000000000.0,
			1000000000000.0,
			4000000000000.0,
			7000000000000.0,
			30000000000000.0,
			80000000000000.0,
			200000000000000.0,
			400000000000000.0,
			800000000000000.0,
			2E+15,
			4E+15,
			8E+15,
			2E+16,
			4E+16,
			8E+16,
			2E+17,
			4E+17,
			8E+17,
			2E+18,
			4E+18,
			8E+18,
			2E+19,
			4E+19,
			8E+19,
			2E+20,
			4E+20,
			8E+20
		};

		public static double[] NEW_MYTHICAL_ARTIFACT_COSTS = new double[]
		{
			100000.0,
			1000000.0,
			4000000.0,
			20000000.0,
			80000000.0,
			300000000.0,
			1000000000.0,
			4000000000.0,
			20000000000.0,
			80000000000.0,
			300000000000.0,
			600000000000.0,
			2000000000000.0,
			8000000000000.0,
			20000000000000.0,
			80000000000000.0,
			300000000000000.0,
			900000000000000.0,
			4E+15,
			8E+15,
			2E+16,
			5E+16,
			1E+17,
			5E+17,
			-1.0
		};
	}
}
