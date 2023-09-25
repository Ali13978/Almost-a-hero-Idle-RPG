using System;

namespace Simulation
{
	public class TrinketUpgradeReqCritAttack : TrinketUpgradeReq
	{
		public TrinketUpgradeReqCritAttack()
		{
			this.descKey = "TRINKET_REQ_CRIT_ATTACK";
			this.buffData = new BuffDataTrinketCritAttack();
			this.buffData.id = 269;
			this.buffData.isPermenant = true;
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override string GetDebugName()
		{
			return "CritAttack";
		}

		public override int GetLevelReq(int level)
		{
			return 40 + level * 4;
		}

		public override int GetBodySpriteIndex()
		{
			return 5;
		}
	}
}
