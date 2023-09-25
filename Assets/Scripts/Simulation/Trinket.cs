using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class Trinket
	{
		static Trinket()
		{
			Trinket.commonEffects = new List<TrinketEffect>();
			Trinket.specialEffects = new List<TrinketEffect>();
			Trinket.secondaryEffects = new List<TrinketEffect>();
			List<TrinketEffect> list = new List<TrinketEffect>();
			list.Add(new TrinketEffectGuidance());
			list.Add(new TrinketEffectRage());
			list.Add(new TrinketEffectLightningShield());
			list.Add(new TrinketEffectFury());
			list.Add(new TrinketEffectSpree());
			list.Add(new TrinketEffectAcrobacy());
			list.Add(new TrinketEffectAnticipation());
			list.Add(new TrinketEffectCheatDeath());
			list.Add(new TrinketEffectParley());
			list.Add(new TrinketEffectAlacrity());
			list.Add(new TrinketEffectRestoration());
			list.Add(new TrinketEffectHatred());
			list.Add(new TrinketEffectHemorrhage());
			list.Add(new TrinketEffectPickPocket());
			list.Add(new TrinketEffectDeepPocket());
			list.Add(new TrinketEffectProtection());
			list.Add(new TrinketEffectLightning());
			list.Add(new TrinketEffectBolt());
			list.Add(new TrinketEffectUnharmed());
			list.Add(new TrinketEffectBackForRevenge());
			list.Add(new TrinketEffectBandage());
			list.Add(new TrinketEffectMeteor());
			list.Add(new TrinketEffectReviveHalf());
			list.Add(new TrinketEffectAngerStun());
			list.Add(new TrinketEffectShieldOnDeath());
			list.Add(new TrinketEffectHarmfulShield());
			list.Add(new TrinketEffectProtectedShield());
			list.Add(new TrinketEffectSkillShield());
			list.Add(new TrinketEffectRegeneration());
			list.Add(new TrinketEffectStun());
			list.Add(new TrinketEffectBlind());
			list.Add(new TrinketEffectCurse());
			list.Add(new TrinketEffectCritCrit());
			list.Add(new TrinketEffectPoweredSkill());
			list.Add(new TrinketEffectDirtyFighter());
			list.Add(new TrinketEffectProsper());
			list.Add(new TrinketEffectBlock());
			list.Add(new TrinketEffectAbilityShield());
			list.Add(new TrinketEffectGoldyStand());
			list.Add(new TrinketEffectOffenceDeffence());
			list.Add(new TrinketEffectDamageTotem());
			list.Add(new TrinketEffectHealthAll());
			list.Add(new TrinketEffectDamageGlobalHigh());
			list.Add(new TrinketEffectDamageHealthGlobal());
			list.Add(new TrinketEffectUpgradeCostAll());
			list.Add(new TrinketEffectHealth());
			list.Add(new TrinketEffectDamageHigh());
			list.Add(new TrinketEffectDamageHealth());
			list.Add(new TrinketEffectUpgradeCost());
			list.Add(new TrinketEffectGold());
			list.Add(new TrinketEffectCritChance());
			list.Add(new TrinketEffectAttackSpeed());
			list.Add(new TrinketEffectDamageConsecutive());
			list.Add(new TrinketEffectFountain());
			Trinket.allReqs = new List<TrinketUpgradeReq>();
			Trinket.allReqs.Add(new TrinketUpgradeReqKill());
			Trinket.allReqs.Add(new TrinketUpgradeReqCastSpell());
			Trinket.allReqs.Add(new TrinketUpgradeReqTakeDamage());
			Trinket.allReqs.Add(new TrinketUpgradeReqEpicBoss());
			Trinket.allReqs.Add(new TrinketUpgradeReqStage());
			Trinket.allReqs.Add(new TrinketUpgradeReqCritAttack());
			foreach (TrinketEffect trinketEffect in list)
			{
				if (trinketEffect.GetGroup() == TrinketEffectGroup.COMMON)
				{
					Trinket.commonEffects.Add(trinketEffect);
				}
				else if (trinketEffect.GetGroup() == TrinketEffectGroup.SPECIAL)
				{
					Trinket.specialEffects.Add(trinketEffect);
				}
				else
				{
					if (trinketEffect.GetGroup() != TrinketEffectGroup.SECONDARY)
					{
						throw new Exception();
					}
					Trinket.secondaryEffects.Add(trinketEffect);
				}
			}
			Trinket.AllEffectsCount = list.Count;
		}

		public Trinket(List<TrinketEffect> effects, TrinketUpgradeReq req)
		{
			this.effects = effects;
			this.req = req;
			this.OnLevelChanged();
		}

		public Trinket(List<TrinketEffect> effects)
		{
			this.effects = effects;
			this.req = null;
			this.OnLevelChanged();
		}

		public static int AllEffectsCount { get; private set; }

		public static TrinketEffect GetEffectWithDebugName(string name)
		{
			foreach (TrinketEffect trinketEffect in Trinket.commonEffects)
			{
				if (trinketEffect.GetDebugName() == name)
				{
					return trinketEffect;
				}
			}
			foreach (TrinketEffect trinketEffect2 in Trinket.secondaryEffects)
			{
				if (trinketEffect2.GetDebugName() == name)
				{
					return trinketEffect2;
				}
			}
			foreach (TrinketEffect trinketEffect3 in Trinket.specialEffects)
			{
				if (trinketEffect3.GetDebugName() == name)
				{
					return trinketEffect3;
				}
			}
			return null;
		}

		public TrinketEffect GetTrinketEffectToUpgrade()
		{
			int num = int.MaxValue;
			TrinketEffect result = null;
			int i = 0;
			int count = this.effects.Count;
			while (i < count)
			{
				if (this.effects[i].level < num && this.effects[i].level < this.effects[i].GetMaxLevel())
				{
					result = this.effects[i];
					num = this.effects[i].level;
				}
				i++;
			}
			return result;
		}

		public int GetTrinketEffectIndexToUpgrade()
		{
			TrinketEffect trinketEffectToUpgrade = this.GetTrinketEffectToUpgrade();
			return this.effects.IndexOf(trinketEffectToUpgrade);
		}

		public static TrinketUpgradeReq GetReqWithDebugName(string name)
		{
			foreach (TrinketUpgradeReq trinketUpgradeReq in Trinket.allReqs)
			{
				if (trinketUpgradeReq.GetDebugName() == name)
				{
					return trinketUpgradeReq;
				}
			}
			return null;
		}

		public string GetDesc()
		{
			string text = string.Empty;
			foreach (TrinketEffect trinketEffect in this.effects)
			{
				text = text + trinketEffect.GetDesc(false, -1) + "\n";
			}
			if (text.Length > 0)
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text;
		}

		public string GetDescFirst()
		{
			string text = string.Empty;
			foreach (TrinketEffect trinketEffect in this.effects)
			{
				text = text + trinketEffect.GetDesc(false, 0) + "\n";
			}
			if (text.Length > 0)
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text;
		}

		public string GetDescFirstWithoutColor()
		{
			string text = string.Empty;
			for (int i = 0; i < this.effects.Count; i++)
			{
				text += this.effects[i].GetDescFirstWithoutColor();
				if (i < this.effects.Count - 1)
				{
					text += "\n\n";
				}
			}
			return text;
		}

		public int GetTotalLevel()
		{
			int num = 0;
			foreach (TrinketEffect trinketEffect in this.effects)
			{
				num += trinketEffect.level;
			}
			return num;
		}

		public bool IsCapped()
		{
			int num = 0;
			int num2 = 0;
			foreach (TrinketEffect trinketEffect in this.effects)
			{
				num += trinketEffect.GetMaxLevel();
				num2 += trinketEffect.level;
			}
			return num2 >= num;
		}

		public void OnLevelChanged()
		{
		}

		public double GetUpgradeCost(int level)
		{
			return Trinket.UPGRADE_COSTS[level];
		}

		public double GetDestroyReward()
		{
			double num = 0.0;
			int i = 0;
			int totalLevel = this.GetTotalLevel();
			while (i < totalLevel)
			{
				num += this.GetUpgradeCost(i);
				i++;
			}
			num *= 0.9;
			return num + (double)this.effects.Count * 30.0;
		}

		public double GetDestroyCostCredits()
		{
			return (double)(10 + this.GetTotalLevel());
		}

		public double GetDisassembleCostCredits()
		{
			return (double)(10 + this.GetTotalLevel());
		}

		public double GetDestroyCostTokens()
		{
			return (double)(500 + 50 * this.GetTotalLevel());
		}

		public double GetDisassembleCostTokens()
		{
			return (double)(500 + 50 * this.GetTotalLevel());
		}

		public static Trinket GetGoodUpgradedTrinket()
		{
			List<TrinketEffect> list = new List<TrinketEffect>();
			list.Add(Trinket.GetRandomEffectFrom(Trinket.commonEffects, false));
			list.Add(Trinket.GetRandomEffectFrom(Trinket.secondaryEffects, false));
			list.Add(Trinket.GetRandomEffectFrom(Trinket.specialEffects, false));
			list[0].level = list[0].GetMaxLevel();
			list[1].level = list[1].GetMaxLevel();
			list[2].level = list[2].GetMaxLevel();
			TrinketUpgradeReq randomReqFrom = Trinket.GetRandomReqFrom(Trinket.allReqs);
			return new Trinket(list, randomReqFrom)
			{
				bodyColorIndex = GameMath.GetRandomInt(0, 6, GameMath.RandType.NoSeed),
				bodySpriteIndex = GameMath.GetRandomInt(0, 6, GameMath.RandType.NoSeed)
			};
		}

		public static Trinket GetRandom(int numTrinketsObtained)
		{
			List<TrinketEffect> list = new List<TrinketEffect>();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (numTrinketsObtained % 6 == 0)
			{
				flag3 = (flag = (flag2 = true));
			}
			else
			{
				float randomFloat = GameMath.GetRandomFloat(0f, 1f, GameMath.RandType.Trinket);
				if (randomFloat < 0.045f)
				{
					flag = true;
				}
				else if (randomFloat < 0.05f)
				{
					flag2 = true;
				}
				else if (randomFloat < 0.055f)
				{
					flag3 = true;
				}
				else if (randomFloat < 0.33f)
				{
					flag2 = (flag = true);
				}
				else if (randomFloat < 0.57f)
				{
					flag3 = (flag = true);
				}
				else if (randomFloat < 0.7f)
				{
					flag2 = (flag3 = true);
				}
				else
				{
					flag3 = (flag = (flag2 = true));
				}
			}
			if (flag)
			{
				TrinketEffect randomEffectFrom = Trinket.GetRandomEffectFrom(Trinket.commonEffects, false);
				if (randomEffectFrom != null)
				{
					list.Add(randomEffectFrom);
				}
			}
			if (flag2)
			{
				TrinketEffect randomEffectFrom2 = Trinket.GetRandomEffectFrom(Trinket.secondaryEffects, false);
				if (randomEffectFrom2 != null)
				{
					list.Add(randomEffectFrom2);
				}
			}
			if (flag3)
			{
				TrinketEffect randomEffectFrom3 = Trinket.GetRandomEffectFrom(Trinket.specialEffects, false);
				if (randomEffectFrom3 != null)
				{
					list.Add(randomEffectFrom3);
				}
			}
			TrinketUpgradeReq randomReqFrom = Trinket.GetRandomReqFrom(Trinket.allReqs);
			return new Trinket(list, randomReqFrom)
			{
				bodyColorIndex = GameMath.GetRandomInt(0, 6, GameMath.RandType.NoSeed),
				bodySpriteIndex = GameMath.GetRandomInt(0, 6, GameMath.RandType.NoSeed)
			};
		}

		private static TrinketEffect GetRandomEffectFrom(List<TrinketEffect> effects, bool deleteFromList)
		{
			float num = 0f;
			foreach (TrinketEffect trinketEffect in effects)
			{
				num += trinketEffect.GetChanceWeight();
			}
			if (num <= 0f)
			{
				return null;
			}
			float num2 = GameMath.GetRandomFloat(0f, num, GameMath.RandType.Trinket);
			for (int i = effects.Count - 1; i >= 0; i--)
			{
				TrinketEffect trinketEffect2 = effects[i];
				num2 -= trinketEffect2.GetChanceWeight();
				if (num2 <= 0f)
				{
					if (deleteFromList)
					{
						effects.Remove(trinketEffect2);
					}
					return trinketEffect2.GetCopy();
				}
			}
			int num3 = -1;
			for (int j = effects.Count - 1; j >= 0; j--)
			{
				TrinketEffect trinketEffect3 = effects[j];
				if (num3 < 0 || effects[num3].GetChanceWeight() < trinketEffect3.GetChanceWeight())
				{
					num3 = j;
				}
			}
			TrinketEffect trinketEffect4 = effects[num3];
			if (deleteFromList)
			{
				effects.Remove(trinketEffect4);
			}
			return trinketEffect4.GetCopy();
		}

		private static TrinketUpgradeReq GetRandomReqFrom(List<TrinketUpgradeReq> reqs)
		{
			float num = 0f;
			foreach (TrinketUpgradeReq trinketUpgradeReq in reqs)
			{
				num += trinketUpgradeReq.GetChanceWeight();
			}
			if (num <= 0f)
			{
				return null;
			}
			float num2 = GameMath.GetRandomFloat(0f, num, GameMath.RandType.Trinket);
			for (int i = reqs.Count - 1; i >= 0; i--)
			{
				TrinketUpgradeReq trinketUpgradeReq2 = reqs[i];
				num2 -= trinketUpgradeReq2.GetChanceWeight();
				if (num2 <= 0f)
				{
					return trinketUpgradeReq2.GetCopy();
				}
			}
			int num3 = -1;
			for (int j = reqs.Count - 1; j >= 0; j--)
			{
				TrinketUpgradeReq trinketUpgradeReq3 = reqs[j];
				if (num3 < 0 || reqs[num3].GetChanceWeight() < trinketUpgradeReq3.GetChanceWeight())
				{
					num3 = j;
				}
			}
			return reqs[num3].GetCopy();
		}

		public int GetRarity()
		{
			int num = 0;
			foreach (TrinketEffect trinketEffect in this.effects)
			{
				if (trinketEffect.GetRarity() > num)
				{
					num = trinketEffect.GetRarity();
				}
			}
			return num;
		}

		public Sprite GetRenderSprite()
		{
			foreach (TrinketEffect trinketEffect in this.effects)
			{
				if (trinketEffect.GetGroup() == TrinketEffectGroup.SPECIAL)
				{
					return trinketEffect.GetSprites()[0];
				}
			}
			return null;
		}

		public TrinketEffect GetEffectOfGroup(TrinketEffectGroup group)
		{
			return this.effects.Find((TrinketEffect effect) => effect.GetGroup() == group);
		}

		public List<TrinketEffect> effects;

		public TrinketUpgradeReq req;

		public int bodyColorIndex;

		public int bodySpriteIndex;

		public static List<TrinketUpgradeReq> allReqs;

		public static List<TrinketEffect> commonEffects;

		public static List<TrinketEffect> specialEffects;

		public static List<TrinketEffect> secondaryEffects;

		public static double[] CraftConstByEffect = new double[]
		{
			150.0,
			300.0,
			500.0
		};

		public const double DEFAULT_TRINKET_SCRAP_VALUE = 100.0;

		public const double TRINKET_SCRAP_PER_STAT = 30.0;

		public static double[] RARITY_VALUES = new double[]
		{
			20.0,
			40.0,
			60.0,
			80.0,
			100.0,
			150.0,
			200.0,
			250.0,
			300.0
		};

		public static double[] UPGRADE_COSTS = new double[]
		{
			25.0,
			25.0,
			50.0,
			50.0,
			75.0,
			75.0,
			100.0,
			100.0,
			125.0,
			125.0,
			150.0,
			150.0,
			175.0,
			175.0,
			200.0,
			200.0,
			225.0,
			225.0,
			250.0,
			250.0,
			275.0,
			275.0,
			300.0,
			300.0,
			325.0,
			325.0,
			350.0,
			350.0,
			375.0,
			375.0,
			400.0,
			400.0,
			425.0,
			425.0,
			450.0,
			450.0,
			475.0,
			475.0,
			500.0,
			500.0,
			525.0,
			525.0,
			550.0,
			550.0,
			575.0,
			575.0,
			600.0,
			600.0,
			625.0,
			625.0,
			650.0,
			650.0,
			675.0,
			675.0,
			700.0,
			700.0,
			725.0,
			725.0,
			750.0,
			750.0,
			775.0,
			775.0,
			800.0,
			800.0,
			825.0,
			825.0,
			850.0,
			850.0,
			875.0,
			875.0,
			900.0,
			900.0,
			925.0,
			925.0,
			950.0,
			950.0,
			975.0,
			975.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0,
			1000.0
		};

		private const int ThreeStatsTrinketFrequency = 6;

		public const int ColorCount = 6;

		public const int BodyCount = 6;
	}
}
