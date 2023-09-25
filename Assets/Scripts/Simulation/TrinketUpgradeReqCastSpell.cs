using System;

namespace Simulation
{
	public class TrinketUpgradeReqCastSpell : TrinketUpgradeReq
	{
		public TrinketUpgradeReqCastSpell()
		{
			this.descKey = "TRINKET_REQ_CAST_SPELL";
			this.buffData = new BuffDataTrinketCastSpell();
			this.buffData.id = 212;
			this.buffData.isPermenant = true;
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override string GetDebugName()
		{
			return "Cast Spell";
		}

		public override int GetLevelReq(int level)
		{
			return 20 + level * 2;
		}

		public override int GetBodySpriteIndex()
		{
			return 1;
		}
	}
}
