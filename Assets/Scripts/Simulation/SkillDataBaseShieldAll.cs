using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseShieldAll : SkillActiveDataBase
	{
		public SkillDataBaseShieldAll()
		{
			this.nameKey = "SKILL_NAME_SHIELDEM_ALL";
			this.descKey = "SKILL_DESC_SHIELDEM_ALL";
			this.requiredHeroLevel = 1;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			float num = 0f;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerMasterShielder)
				{
					num += AM.LinearEquationFloat((float)skillEnhancer.level, SkillEnhancerMasterShielder.LEVEL_RED, SkillEnhancerMasterShielder.INIT_RED);
				}
			}
			data.dur = 1.6f;
			data.durInvulnurability = 0f;
			data.cooldownMax = 120f - num;
			data.events = new List<SkillEvent>();
			SkillEventShieldAll skillEventShieldAll = new SkillEventShieldAll();
			data.events.Add(skillEventShieldAll);
			skillEventShieldAll.time = data.dur * 0.5f;
			skillEventShieldAll.ratio = this.GetShield(level);
			skillEventShieldAll.dur = 1000f;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetShield(0), false))) + AM.GetCooldownText(120f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetShield(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.07, false) + ")")) + AM.GetCooldownText((data as SkillActiveData).cooldownMax, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetShield(data.level), false))) + AM.GetCooldownText((data as SkillActiveData).cooldownMax, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.samAutoSkills[0], 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voSamSkillAuto1, 1f));
		}

		public double GetShield(int level)
		{
			return 0.2 + (double)level * 0.07;
		}

		private const float COOLDOWN = 120f;

		private const double INIT_SHIELD = 0.2;

		private const double LEVEL_SHIELD = 0.07;
	}
}
