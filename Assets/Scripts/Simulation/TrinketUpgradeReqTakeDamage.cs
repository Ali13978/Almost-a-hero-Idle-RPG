using System;

namespace Simulation
{
	public class TrinketUpgradeReqTakeDamage : TrinketUpgradeReq
	{
		public TrinketUpgradeReqTakeDamage()
		{
			this.descKey = "TRINKET_REQ_TAKE_DAMAGE";
			this.buffData = new BuffDataTrinketTakeDamage();
			this.buffData.id = 215;
			this.buffData.isPermenant = true;
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override string GetDebugName()
		{
			return "Take Damage";
		}

		public override int GetLevelReq(int level)
		{
			return level * 6 + 60;
		}

		public override int GetBodySpriteIndex()
		{
			return 2;
		}
	}
}
