using System;
using UnityEngine;

namespace Simulation
{
	public class TrinketUpgradeReqDie : TrinketUpgradeReq
	{
		public TrinketUpgradeReqDie()
		{
			this.descKey = "TRINKET_REQ_DIE";
			this.buffData = new BuffDataTrinketDie();
			this.buffData.id = 213;
			this.buffData.isPermenant = true;
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override string GetDebugName()
		{
			return "Die";
		}

		public override int GetLevelReq(int level)
		{
			return 5 + Mathf.FloorToInt((float)level * 0.5f);
		}

		public override int GetBodySpriteIndex()
		{
			return 2;
		}
	}
}
