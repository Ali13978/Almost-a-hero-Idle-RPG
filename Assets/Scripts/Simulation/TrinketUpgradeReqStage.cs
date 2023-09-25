using System;

namespace Simulation
{
	public class TrinketUpgradeReqStage : TrinketUpgradeReq
	{
		public TrinketUpgradeReqStage()
		{
			this.descKey = "TRINKET_REQ_STAGE";
			this.buffData = new BuffDataTrinketStage();
			this.buffData.id = 270;
			this.buffData.isPermenant = true;
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override string GetDebugName()
		{
			return "Stage";
		}

		public override int GetLevelReq(int level)
		{
			return 20 + level * 2;
		}

		public override int GetBodySpriteIndex()
		{
			return 4;
		}
	}
}
