using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseGotYourBack : SkillActiveDataBase
	{
		public SkillDataBaseGotYourBack()
		{
			this.nameKey = "SKILL_NAME_GOT_YOUR_BACK";
			this.descKey = "SKILL_DESC_GOT_YOUR_BACK";
			this.requiredHeroLevel = 1;
			this.maxLevel = 8;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.dur = 3.5666666f;
			data.durInvulnurability = 0f;
			data.cooldownMax = 200f;
			data.events = new List<SkillEvent>();
			SkillEventBuffAllAllies skillEventBuffAllAllies = new SkillEventBuffAllAllies();
			skillEventBuffAllAllies.applySelf = true;
			skillEventBuffAllAllies.time = 1.83333337f;
			skillEventBuffAllAllies.buff = new BuffDataDefense
			{
				dur = this.GetDur(level),
				damageTakenFactor = (double)(1f - this.GetDamageReduction(level)),
				id = 324
			};
			data.events.Add(skillEventBuffAllAllies);
			SkillEventHealAllAllies skillEventHealAllAllies = new SkillEventHealAllAllies();
			skillEventHealAllAllies.applySelf = true;
			skillEventHealAllAllies.time = 1.83333337f;
			skillEventHealAllAllies.healRatio = (double)this.GetHealRatio(level);
			data.events.Add(skillEventHealAllAllies);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetHealRatio(0), false)), AM.csa(GameMath.GetPercentString(this.GetDamageReduction(0), false)), AM.csa(GameMath.GetTimeInSecondsString(this.GetDur(0)))) + AM.GetCooldownText(200f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetHealRatio(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.1f, false) + ")"), AM.csa(GameMath.GetPercentString(this.GetDamageReduction(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.05f, false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(this.GetDur(data.level)))) + AM.GetCooldownText(200f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetHealRatio(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetDamageReduction(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(this.GetDur(data.level)))) + AM.GetCooldownText(200f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.babuAutoSkills[1], 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voBabuGotYourBack, 1f));
		}

		public float GetDur(int level)
		{
			return 15f;
		}

		public float GetDamageReduction(int level)
		{
			return 0.2f + 0.05f * (float)level;
		}

		public float GetHealRatio(int level)
		{
			return 0.2f + 0.1f * (float)level;
		}

		public const float COOLDOWN = 200f;

		public const float BUFF_DUR = 15f;

		public const float HEAL_RATIO = 0.2f;

		public const float HEAL_RATIO_PER_LEVEL = 0.1f;

		public const float DMG_REDUCTION_RATIO_INIT = 0.2f;

		public const float DMG_REDUCTION_RATIO_PER_LEVEL = 0.05f;

		public const float DUR_ANIM = 3.5666666f;

		public const float BUFF_TIME = 1.83333337f;
	}
}
