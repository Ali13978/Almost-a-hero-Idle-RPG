using System;

namespace SaveLoad
{
	[Serializable]
	public class WorldMerchantUseState
	{
		public float timeWarpTimeLeft;

		public float timeWarpSpeed;

		public float autoTapTimeLeft;

		public float powerUpTimeLeft;

		public double powerUpDamageFactorAdd;

		public float refresherOrbTimeLeft;

		public float refresherOrbSkillCoolFactor;

		public float goldBoostTimeLeft;

		public double goldBoostFactorAdd;

		public float shieldTimeLeft;

		public float catalystTimeLeft;

		public float catalystProgressPercentage;

		public int numCharmSelectionAdd;

		public bool pickRandomCharmsEnabled;

		public float blizzardTimeLeft;

		public float blizzardSlowFactor;

		public float hotCocoaTimeLeft;

		public float hotCocoaCooldownReductionFactor;

		public float hotCocoaDamageFactor;

		public float ornamentDropTimeLeft;

		public float ornamentDropTargetTaps;

		public float ornamentDropTeamDamageFactor;

		public float ornamentDropCurrentTime;

		public int ornamentDropProjectilesCount;
	}
}
