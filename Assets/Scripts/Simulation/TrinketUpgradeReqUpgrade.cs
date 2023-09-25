using System;

namespace Simulation
{
	public class TrinketUpgradeReqUpgrade : TrinketUpgradeReq
	{
		public TrinketUpgradeReqUpgrade()
		{
			this.descKey = "TRINKET_REQ_UPGRADE";
			this.buffData = new BuffDataTrinketUpgrade();
			this.buffData.id = 216;
			this.buffData.isPermenant = true;
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override string GetDebugName()
		{
			return "Upgrade";
		}

		public override int GetLevelReq(int level)
		{
			return 100 + level * 10;
		}

		public override int GetBodySpriteIndex()
		{
			return 2;
		}
	}
}
