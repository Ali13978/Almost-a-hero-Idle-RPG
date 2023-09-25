using System;

namespace Simulation
{
	public class TrinketUpgradeReqAttack : TrinketUpgradeReq
	{
		public TrinketUpgradeReqAttack()
		{
			this.descKey = "TRINKET_REQ_ATTACK";
			this.buffData = new BuffDataTrinketAttack();
			this.buffData.id = 211;
			this.buffData.isPermenant = true;
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override string GetDebugName()
		{
			return "Attack";
		}

		public override int GetLevelReq(int level)
		{
			return 100 + level * 10;
		}

		public override int GetBodySpriteIndex()
		{
			return 0;
		}
	}
}
