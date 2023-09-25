using System;
using System.Collections.Generic;
using Simulation;

namespace Static
{
	public static class TypeHelper
	{
		static TypeHelper()
		{
			TypeHelper.cachedTrinketEffects = new Dictionary<int, TrinketEffect>();
			for (int i = 1; i <= 67; i++)
			{
				TypeHelper.cachedTrinketEffects.Add(i, TypeHelper.ConvertTrinketEffect(i));
			}
		}

		public static TrinketEffect ConvertTrinketEffect(int index)
		{
			switch (index)
			{
			case 1:
				return new TrinketEffectDamageHigh();
			case 2:
				return new TrinketEffectBandage();
			case 3:
				return new TrinketEffectDamageGlobalHigh();
			case 4:
				return new TrinketEffectDamageTotem();
			case 5:
				return new TrinketEffectKillsHeal();
			case 6:
				return new TrinketEffectUpgradeCost();
			case 7:
				return new TrinketEffectDamageConsecutive();
			case 8:
				return new TrinketEffectLightning();
			case 9:
				return new TrinketEffectMeteor();
			case 10:
				return new TrinketEffectMissChance();
			case 11:
				return new TrinketEffectAttackSpeed();
			case 12:
				return new TrinketEffectExtraLoot();
			case 13:
				return new TrinketEffectRage();
			case 14:
				return new TrinketEffectReviveHalf();
			case 15:
				return new TrinketEffectStun();
			case 16:
				return new TrinketEffectDamageChest();
			case 17:
				return new TrinketEffectDefense();
			case 18:
				return new TrinketEffectGold();
			case 19:
				return new TrinketEffectHealth();
			case 20:
				return new TrinketEffectSkillReduceUpgradeCost();
			case 21:
				return new TrinketEffectUltiCooldown();
			case 22:
				return new TrinketEffectGuidance();
			case 23:
				return new TrinketEffectSkillLifeSteal();
			case 24:
				return new TrinketEffectLightningShield();
			case 25:
				return new TrinketEffectFury();
			case 26:
				return new TrinketEffectSpree();
			case 27:
				return new TrinketEffectAcrobacy();
			case 28:
				return new TrinketEffectAnticipation();
			case 29:
				return new TrinketEffectCheatDeath();
			case 30:
				return new TrinketEffectParley();
			case 31:
				return new TrinketEffectAlacrity();
			case 32:
				return new TrinketEffectRestoration();
			case 33:
				return new TrinketEffectHatred();
			case 34:
				return new TrinketEffectHemorrhage();
			case 35:
				return new TrinketEffectPickPocket();
			case 36:
				return new TrinketEffectDeepPocket();
			case 37:
				return new TrinketEffectProtection();
			case 38:
				return new TrinketEffectBolt();
			case 39:
				return new TrinketEffectUnharmed();
			case 40:
				return new TrinketEffectBackForRevenge();
			case 41:
				return new TrinketEffectHarmfulShield();
			case 42:
				return new TrinketEffectProtectedShield();
			case 43:
				return new TrinketEffectSkillShield();
			case 44:
				return new TrinketEffectRegeneration();
			case 45:
				return new TrinketEffectBlind();
			case 46:
				return new TrinketEffectCurse();
			case 47:
				return new TrinketEffectCritCrit();
			case 48:
				return new TrinketEffectPoweredSkill();
			case 49:
				return new TrinketEffectDirtyFighter();
			case 50:
				return new TrinketEffectProsper();
			case 51:
				return new TrinketEffectBlock();
			case 52:
				return new TrinketEffectDamageTotem();
			case 53:
				return new TrinketEffectHealthAll();
			case 54:
				return new TrinketEffectDamageHealthGlobal();
			case 55:
				return new TrinketEffectUpgradeCostAll();
			case 56:
				return new TrinketEffectDamageHealth();
			case 57:
				return new TrinketEffectCritChance();
			case 58:
				return new TrinketEffectAbilityShield();
			case 59:
				return new TrinketEffectDamageGlobalHigh();
			case 60:
				return new TrinketEffectDamageGlobalHigh();
			case 61:
				return new TrinketEffectDamageHigh();
			case 62:
				return new TrinketEffectDamageHigh();
			case 63:
				return new TrinketEffectFountain();
			case 64:
				return new TrinketEffectGoldyStand();
			case 65:
				return new TrinketEffectAngerStun();
			case 66:
				return new TrinketEffectShieldOnDeath();
			case 67:
				return new TrinketEffectOffenceDeffence();
			default:
				throw new Exception("Trinket effect does not exist");
			}
		}

		public static int ConvertTrinketEffect(TrinketEffect effect)
		{
			if (effect is TrinketEffectDamage)
			{
				return 1;
			}
			if (effect is TrinketEffectBandage)
			{
				return 2;
			}
			if (effect is TrinketEffectDamageGlobal)
			{
				return 3;
			}
			if (effect is TrinketEffectDamageTotem)
			{
				return 4;
			}
			if (effect is TrinketEffectKillsHeal)
			{
				return 5;
			}
			if (effect is TrinketEffectUpgradeCost)
			{
				return 6;
			}
			if (effect is TrinketEffectDamageConsecutive)
			{
				return 7;
			}
			if (effect is TrinketEffectLightning)
			{
				return 8;
			}
			if (effect is TrinketEffectMeteor)
			{
				return 9;
			}
			if (effect is TrinketEffectMissChance)
			{
				return 10;
			}
			if (effect is TrinketEffectAttackSpeed)
			{
				return 11;
			}
			if (effect is TrinketEffectExtraLoot)
			{
				return 12;
			}
			if (effect is TrinketEffectRage)
			{
				return 13;
			}
			if (effect is TrinketEffectReviveHalf)
			{
				return 14;
			}
			if (effect is TrinketEffectStun)
			{
				return 15;
			}
			if (effect is TrinketEffectDamageChest)
			{
				return 16;
			}
			if (effect is TrinketEffectDefense)
			{
				return 17;
			}
			if (effect is TrinketEffectGold)
			{
				return 18;
			}
			if (effect is TrinketEffectHealth)
			{
				return 19;
			}
			if (effect is TrinketEffectSkillReduceUpgradeCost)
			{
				return 20;
			}
			if (effect is TrinketEffectUltiCooldown)
			{
				return 21;
			}
			if (effect is TrinketEffectGuidance)
			{
				return 22;
			}
			if (effect is TrinketEffectSkillLifeSteal)
			{
				return 23;
			}
			if (effect is TrinketEffectLightningShield)
			{
				return 24;
			}
			if (effect is TrinketEffectFury)
			{
				return 25;
			}
			if (effect is TrinketEffectSpree)
			{
				return 26;
			}
			if (effect is TrinketEffectAcrobacy)
			{
				return 27;
			}
			if (effect is TrinketEffectAnticipation)
			{
				return 28;
			}
			if (effect is TrinketEffectCheatDeath)
			{
				return 29;
			}
			if (effect is TrinketEffectParley)
			{
				return 30;
			}
			if (effect is TrinketEffectAlacrity)
			{
				return 31;
			}
			if (effect is TrinketEffectRestoration)
			{
				return 32;
			}
			if (effect is TrinketEffectHatred)
			{
				return 33;
			}
			if (effect is TrinketEffectHemorrhage)
			{
				return 34;
			}
			if (effect is TrinketEffectPickPocket)
			{
				return 35;
			}
			if (effect is TrinketEffectDeepPocket)
			{
				return 36;
			}
			if (effect is TrinketEffectProtection)
			{
				return 37;
			}
			if (effect is TrinketEffectBolt)
			{
				return 38;
			}
			if (effect is TrinketEffectUnharmed)
			{
				return 39;
			}
			if (effect is TrinketEffectBackForRevenge)
			{
				return 40;
			}
			if (effect is TrinketEffectHarmfulShield)
			{
				return 41;
			}
			if (effect is TrinketEffectProtectedShield)
			{
				return 42;
			}
			if (effect is TrinketEffectSkillShield)
			{
				return 43;
			}
			if (effect is TrinketEffectRegeneration)
			{
				return 44;
			}
			if (effect is TrinketEffectBlind)
			{
				return 45;
			}
			if (effect is TrinketEffectCurse)
			{
				return 46;
			}
			if (effect is TrinketEffectCritCrit)
			{
				return 47;
			}
			if (effect is TrinketEffectPoweredSkill)
			{
				return 48;
			}
			if (effect is TrinketEffectDirtyFighter)
			{
				return 49;
			}
			if (effect is TrinketEffectProsper)
			{
				return 50;
			}
			if (effect is TrinketEffectBlock)
			{
				return 51;
			}
			if (effect is TrinketEffectDamageTotemByHero)
			{
				return 52;
			}
			if (effect is TrinketEffectHealthAll)
			{
				return 53;
			}
			if (effect is TrinketEffectDamageHealthGlobal)
			{
				return 54;
			}
			if (effect is TrinketEffectUpgradeCostAll)
			{
				return 55;
			}
			if (effect is TrinketEffectDamageHealth)
			{
				return 56;
			}
			if (effect is TrinketEffectCritChance)
			{
				return 57;
			}
			if (effect is TrinketEffectAbilityShield)
			{
				return 58;
			}
			if (effect is TrinketEffectDamageGlobalLow)
			{
				return 59;
			}
			if (effect is TrinketEffectDamageGlobalHigh)
			{
				return 60;
			}
			if (effect is TrinketEffectDamageLow)
			{
				return 61;
			}
			if (effect is TrinketEffectDamageHigh)
			{
				return 62;
			}
			if (effect is TrinketEffectFountain)
			{
				return 63;
			}
			if (effect is TrinketEffectGoldyStand)
			{
				return 64;
			}
			if (effect is TrinketEffectAngerStun)
			{
				return 65;
			}
			if (effect is TrinketEffectShieldOnDeath)
			{
				return 66;
			}
			if (effect is TrinketEffectOffenceDeffence)
			{
				return 67;
			}
			throw new NotImplementedException();
		}

		public const int TrinketEffectCount = 67;

		public static Dictionary<int, TrinketEffect> cachedTrinketEffects;

		public static readonly List<int> TrinketEffectsOrder = new List<int>
		{
			36,
			33,
			34,
			32,
			28,
			2,
			25,
			40,
			39,
			13,
			27,
			26,
			31,
			66,
			29,
			30,
			24,
			35,
			65,
			14,
			9,
			22,
			38,
			37,
			8,
			15,
			45,
			46,
			48,
			67,
			51,
			47,
			43,
			64,
			49,
			50,
			44,
			58,
			41,
			42,
			23,
			7,
			54,
			59,
			3,
			60,
			53,
			57,
			56,
			63,
			4,
			11,
			61,
			1,
			62,
			18,
			52,
			19,
			55,
			6,
			5,
			10,
			12,
			16,
			17,
			20,
			21
		};
	}
}
