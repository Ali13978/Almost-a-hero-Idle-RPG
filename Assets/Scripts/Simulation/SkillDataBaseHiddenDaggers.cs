using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseHiddenDaggers : SkillPassiveDataBase
	{
		public SkillDataBaseHiddenDaggers()
		{
			this.nameKey = "SKILL_NAME_HIDDEN_DAGGERS";
			this.descKey = "SKILL_DESC_HIDDEN_DAGGERS";
			this.descFullKey = "SKILL_DESC_FULL_HIDDEN_DAGGERS";
			this.requiredHeroLevel = 5;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataWeaponLoad buffDataWeaponLoad = new BuffDataWeaponLoad();
			buffDataWeaponLoad.id = 193;
			data.passiveBuff = buffDataWeaponLoad;
			buffDataWeaponLoad.isPermenant = true;
			buffDataWeaponLoad.weaponLoadAdd = this.GetAmmoBonus(level);
			buffDataWeaponLoad.damageAdd = this.GetDamageBonus(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(SkillDataBaseHiddenDaggers.BASE_AMMO.ToString()), AM.csl((SkillDataBaseHiddenDaggers.BASE_AMMO + this.GetAmmoBonus(0)).ToString()), AM.csa(GameMath.GetPercentString(this.GetDamageBonus(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa((SkillDataBaseHiddenDaggers.BASE_AMMO + this.GetAmmoBonus(data.level)).ToString()), AM.csl((SkillDataBaseHiddenDaggers.BASE_AMMO + this.GetAmmoBonus(data.level + 1)).ToString()), AM.csa(GameMath.GetPercentString(this.GetDamageBonus(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.1, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descFullKey), AM.csa((SkillDataBaseHiddenDaggers.BASE_AMMO + this.GetAmmoBonus(data.level)).ToString()), AM.csa(GameMath.GetPercentString(this.GetDamageBonus(data.level), false)));
		}

		public int GetAmmoBonus(int level)
		{
			return SkillDataBaseHiddenDaggers.INITIAL_AMMO_BONUS + level * SkillDataBaseHiddenDaggers.LEVEL_AMMO_BONUS;
		}

		public double GetDamageBonus(int level)
		{
			return 0.1 + (double)level * 0.1;
		}

		public static int BASE_AMMO = 6;

		public static int INITIAL_AMMO_BONUS = 1;

		public static int LEVEL_AMMO_BONUS = 1;

		private const double INITIAL_DAMAGE_BONUS = 0.1;

		private const double LEVEL_DAMAGE_BONUS = 0.1;
	}
}
