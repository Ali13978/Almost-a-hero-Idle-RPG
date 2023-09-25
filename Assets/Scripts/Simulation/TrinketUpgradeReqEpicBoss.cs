using System;
using UnityEngine;

namespace Simulation
{
	public class TrinketUpgradeReqEpicBoss : TrinketUpgradeReq
	{
		public TrinketUpgradeReqEpicBoss()
		{
			this.descKey = "TRINKET_REQ_EPIC_BOSS";
			this.buffData = new BuffDataTrinketEpicBoss();
			this.buffData.id = 217;
			this.buffData.isPermenant = true;
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override string GetDebugName()
		{
			return "Kill Boss";
		}

		public override int GetLevelReq(int level)
		{
			return 3 + Mathf.FloorToInt((float)level / 3f);
		}

		public override int GetBodySpriteIndex()
		{
			return 3;
		}
	}
}
