using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseTradeSecret : SkillPassiveDataBase
	{
		public SkillDataBaseTradeSecret()
		{
			this.nameKey = "SKILL_NAME_TRADE_SECRET";
			this.descKey = "SKILL_DESC_TRADE_SECRET";
			this.requiredHeroLevel = 2;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataAlliesDamageChest buffDataAlliesDamageChest = new BuffDataAlliesDamageChest();
			buffDataAlliesDamageChest.id = 286;
			data.passiveBuff = buffDataAlliesDamageChest;
			buffDataAlliesDamageChest.isPermenant = true;
			buffDataAlliesDamageChest.isStackable = false;
			buffDataAlliesDamageChest.effect = new BuffDataDamageChest();
			buffDataAlliesDamageChest.effect.isPermenant = true;
			buffDataAlliesDamageChest.effect.isStackable = false;
			buffDataAlliesDamageChest.effect.damageAdd = this.GetDamageFactor(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.25, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(data.level), false)));
		}

		public double GetDamageFactor(int level)
		{
			return 1.0 + (double)level * 0.25;
		}

		private const double INITIAL_DAMAGE_FACTOR = 1.0;

		private const double LEVEL_DAMAGE_FACTOR = 0.25;
	}
}
