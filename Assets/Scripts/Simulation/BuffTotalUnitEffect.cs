using System;
using System.Collections.Generic;

namespace Simulation
{
	public class BuffTotalUnitEffect
	{
		public void Init()
		{
			this.attackSpeedAdd = 0f;
			this.attackSpeedFactor = 1f;
			this.damageAddFactor = 1.0;
			this.damageMulFactor = 1.0;
			this.damageTEFactor = 1.0;
			this.damageAreaFactor = 1.0;
			this.damageAreaRFactor = 1f;
			this.missChanceFactor = 1f;
			this.missChanceAdd = 0f;
			this.critChanceAdd = 0f;
			this.critFactorAdd = 0.0;
			this.healthMaxFactor = 1.0;
			this.healthMaxFactorTE = 1.0;
			this.healthRegenAdd = 0.0;
			this.damageTakenFactor = 1.0;
			this.dodgeChanceAdd = 0f;
			this.tauntAdd = 0f;
			this.reviveDurFactor = 1f;
			this.upgradeCostFactor = 1.0;
			this.upgradeCostFactorTE = 1.0;
			this.reloadSpeedAdd = 0f;
			this.weaponLoadAdd = 0;
			this.skillCoolFactor = 1f;
			this.ultiCoolFactor = 1f;
			this.dropGoldFactor = 1.0;
			this.totemChargeReqAdd = 0;
			this.totemHeatFactor = 1f;
			this.totemHeatMaxAdd = 0f;
			this.totemCoolFactor = 1f;
			this.totemOverCoolFactor = 1f;
			this.totemIceManaMaxFactor = 1f;
			this.totemIceManaGatherSpeedFactor = 1f;
			this.totemIceManaUseSpeedFactor = 1f;
			this.totemIceShardReqManaFactor = 1f;
			this.totemIceManaSpendSpeedFactor = 1f;
			this.heatMaxFactor = 1f;
			this.heatOvercoolFactor = 1f;
			this.totemHasShotAuto = false;
			this.reviveSpeedFactor = 1f;
			this.totemEarthMeteoriteAuto = false;
			this.totemEarthDurationAdd = 0f;
			this.totemEarthTapRecharge = 0f;
			this.totemEarthMeteoriteTap = false;
			this.maxCostReductionReached.Clear();
		}

		public float attackSpeedAdd;

		public float attackSpeedFactor;

		public double damageAddFactor;

		public double damageMulFactor;

		public double damageTEFactor;

		public double damageAreaFactor;

		public float damageAreaRFactor;

		public float missChanceFactor;

		public float missChanceAdd;

		public float critChanceAdd;

		public double critFactorAdd;

		public double healthMaxFactor;

		public double healthMaxFactorTE;

		public double healthRegenAdd;

		public double damageTakenFactor;

		public float dodgeChanceAdd;

		public float tauntAdd;

		public float reviveDurFactor;

		public double upgradeCostFactor;

		public double upgradeCostFactorTE;

		public float reloadSpeedAdd;

		public int weaponLoadAdd;

		public float skillCoolFactor;

		public float ultiCoolFactor;

		public double dropGoldFactor;

		public int totemChargeReqAdd;

		public float totemHeatFactor;

		public float totemHeatMaxAdd;

		public float totemCoolFactor;

		public float totemOverCoolFactor;

		public float totemIceManaMaxFactor;

		public float totemIceManaGatherSpeedFactor;

		public float totemIceManaUseSpeedFactor;

		public float totemIceShardReqManaFactor;

		public float heatMaxFactor;

		public float heatOvercoolFactor;

		public bool totemHasShotAuto;

		public float totemIceManaSpendSpeedFactor;

		public float reviveSpeedFactor;

		public bool totemEarthMeteoriteAuto;

		public float totemEarthDurationAdd;

		public float totemEarthTapRecharge;

		public bool totemEarthMeteoriteTap;

		public List<bool> maxCostReductionReached = new List<bool>();
	}
}
