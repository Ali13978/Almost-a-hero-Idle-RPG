using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseFastReloader : SkillPassiveDataBase
	{
		public SkillDataBaseFastReloader()
		{
			this.nameKey = "SKILL_NAME_FAST_RELOADER";
			this.descKey = "SKILL_DESC_FAST_RELOADER";
			this.descFullKey = "SKILL_DESC_FULL_FAST_RELOADER";
			this.requiredHeroLevel = 5;
			this.maxLevel = 2;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataReloadSpeed buffDataReloadSpeed = new BuffDataReloadSpeed();
			buffDataReloadSpeed.id = 147;
			data.passiveBuff = buffDataReloadSpeed;
			buffDataReloadSpeed.isPermenant = true;
			buffDataReloadSpeed.amount = this.GetReductionAmount(level);
			buffDataReloadSpeed.loadAdd = this.GetLoadAdd(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), new object[]
			{
				AM.csa(SkillDataBaseFastReloader.BASE_AMMO.ToString()),
				AM.csl((SkillDataBaseFastReloader.BASE_AMMO + this.GetLoadAdd(0)).ToString()),
				AM.csa(GameMath.GetDetailedNumberString(SkillDataBaseFastReloader.BASE_RELOAD)),
				AM.csl(GameMath.GetDetailedNumberString(SkillDataBaseFastReloader.BASE_RELOAD - this.GetReductionAmount(0)))
			});
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), new object[]
			{
				AM.csa((SkillDataBaseFastReloader.BASE_AMMO + this.GetLoadAdd(data.level)).ToString()),
				AM.csl((SkillDataBaseFastReloader.BASE_AMMO + this.GetLoadAdd(data.level + 1)).ToString()),
				AM.csa(GameMath.GetDetailedNumberString(SkillDataBaseFastReloader.BASE_RELOAD - this.GetReductionAmount(data.level))),
				AM.csl(GameMath.GetDetailedNumberString(SkillDataBaseFastReloader.BASE_RELOAD - this.GetReductionAmount(data.level + 1)))
			});
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descFullKey), AM.csa((SkillDataBaseFastReloader.BASE_AMMO + this.GetLoadAdd(data.level)).ToString()), AM.csa(GameMath.GetDetailedNumberString(SkillDataBaseFastReloader.BASE_RELOAD - this.GetReductionAmount(data.level))));
		}

		public float GetReductionAmount(int level)
		{
			return SkillDataBaseFastReloader.INITIAL_REDUCE + SkillDataBaseFastReloader.LEVEL_REDUCE * (float)level;
		}

		public int GetLoadAdd(int level)
		{
			return SkillDataBaseFastReloader.INITIAL_AMMO + SkillDataBaseFastReloader.LEVEL_AMMO * level;
		}

		public static float BASE_RELOAD = 3.5f;

		public static float INITIAL_REDUCE = 0.5f;

		public static float LEVEL_REDUCE = 0.25f;

		public static int BASE_AMMO = 4;

		public static int INITIAL_AMMO = 1;

		public static int LEVEL_AMMO = 1;
	}
}
