using System;

namespace Simulation
{
	public class CharmEffectSweetMoves : CharmEffectData
	{
		public CharmEffectSweetMoves()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Defense,
				id = 204,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_SWEET_MOVES_NAME",
				conditionKey = "CHARM_CONDITION_PASS_WAVES",
				descKey = "CHARM_SWEET_MOVES_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffSweetMoves
			{
				enchantmentData = this,
				dodgeChanceIncrease = this.GetDodge(this.level),
				dur = this.GetDur(this.level),
				pic = 1f / (float)this.GetActivateThreshold()
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			string str2 = string.Empty;
			if (showUpgrade)
			{
				float num = this.GetDur(this.level + 1) - this.GetDur(this.level);
				if (num > 0f)
				{
					str = AM.cdu(" (+" + GameMath.GetTimeInSecondsString(num) + ")");
				}
				float num2 = this.GetDodge(this.level + 1) - this.GetDodge(this.level);
				if (num2 > 0f)
				{
					str2 = AM.cdu(" (+" + GameMath.GetPercentString(num2, false) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetDodge(this.level), false)) + str2, AM.cds(GameMath.GetTimeInSecondsString(this.GetDur(this.level))) + str);
		}

		public override string GetActivationDesc(bool showUpgrade)
		{
			return string.Format(LM.Get(base.conditionKey), AM.cas(this.GetActivateThreshold().ToString()));
		}

		public override string GetDesc()
		{
			return this.GetDesc(false);
		}

		public override string GetConditionDescription()
		{
			return this.GetActivationDesc(false);
		}

		public float GetDodge(int lev)
		{
			return 0.27f + 0.03f * (float)(lev - (lev + 1) / 5);
		}

		public float GetDur(int lev)
		{
			return 5f + 1f * (float)((lev + 1) / 5);
		}

		public int GetActivateThreshold()
		{
			return 3;
		}

		public override int GetNumPacksRequired()
		{
			return 10;
		}

		public const float BASE_DODGE_CHANCE = 0.27f;

		public const float PER_LEVEL_DODGE_CHANCE = 0.03f;

		public const float BASE_DUR = 5f;

		public const float PER_5_LEVEL_DUR = 1f;

		public const int BASE_ACTIVATE_THRESHOLD = 3;
	}
}
