using System;

namespace Simulation
{
	public class TrinketUpgradeReqKill : TrinketUpgradeReq
	{
		public TrinketUpgradeReqKill()
		{
			this.descKey = "TRINKET_REQ_KILL";
			this.buffData = new BuffDataTrinketKill();
			this.buffData.id = 214;
			this.buffData.isPermenant = true;
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override string GetDebugName()
		{
			return "Kill";
		}

		public override int GetLevelReq(int level)
		{
			return 30 + 3 * level;
		}

		public override int GetBodySpriteIndex()
		{
			return 0;
		}
	}
}
