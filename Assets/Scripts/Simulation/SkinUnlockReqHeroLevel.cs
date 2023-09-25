using System;
using Ui;

namespace Simulation
{
	public class SkinUnlockReqHeroLevel : SkinUnlockReq
	{
		public override string GetReqString(bool reveal)
		{
			if (reveal)
			{
				return string.Format(LM.Get("SKIN_UNLOCK_REQ_EVOLVE_RE"), AM.cs(SkinUnlockReqHeroLevel.LevelString(this.targetEvolveLevel), UiManager.colorHeroLevels[this.targetEvolveLevel - 1]));
			}
			return string.Format(LM.Get("SKIN_UNLOCK_REQ_EVOLVE_UN"), AM.cs(SkinUnlockReqHeroLevel.LevelString(this.targetEvolveLevel), UiManager.colorHeroLevels[this.targetEvolveLevel - 1]));
		}

		public override bool IsSatisfied(Simulator sim, SkinData skinData)
		{
			bool flag = sim.IsSkinBought(skinData);
			return skinData.belongsTo.evolveLevel >= this.targetEvolveLevel || flag;
		}

		public static string LevelString(int level)
		{
			if (level == 1)
			{
				return LM.Get("UI_LEVEL_COMMON");
			}
			if (level == 2)
			{
				return LM.Get("UI_LEVEL_UNCOMMON");
			}
			if (level == 3)
			{
				return LM.Get("UI_LEVEL_RARE");
			}
			if (level == 4)
			{
				return LM.Get("UI_LEVEL_EPIC");
			}
			if (level == 5)
			{
				return LM.Get("UI_LEVEL_LEGENDARY");
			}
			if (level == 6)
			{
				return LM.Get("UI_ARTIFACTS_MYTHICAL");
			}
			throw new NotImplementedException();
		}

		public int targetEvolveLevel;
	}
}
