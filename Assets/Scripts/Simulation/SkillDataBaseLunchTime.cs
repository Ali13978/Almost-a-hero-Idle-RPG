using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseLunchTime : SkillActiveDataBase
	{
		public SkillDataBaseLunchTime()
		{
			this.nameKey = "SKILL_NAME_LUNCH_TIME";
			this.descKey = "SKILL_DESC_LUNCH_TIME";
			this.requiredHeroLevel = 1;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			float num = 0f;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerBaseFreshMeat)
				{
					num += (skillEnhancer.enhancerBase as SkillEnhancerBaseFreshMeat).GetRed(skillEnhancer.level);
				}
			}
			data.dur = 1.4f;
			data.durInvulnurability = 0f;
			data.cooldownMax = 150f - num;
			data.events = new List<SkillEvent>();
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf);
			skillEventBuffSelf.time = data.dur;
			BuffDataHealthRegen buffDataHealthRegen = new BuffDataHealthRegen();
			buffDataHealthRegen.id = 97;
			skillEventBuffSelf.buff = buffDataHealthRegen;
			buffDataHealthRegen.isStackable = true;
			buffDataHealthRegen.dur = 5f;
			buffDataHealthRegen.healthRegenAdd = 0.1;
			buffDataHealthRegen.visuals |= 64;
			SkillEventBuffSelf skillEventBuffSelf2 = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf2);
			skillEventBuffSelf2.time = data.dur;
			BuffDataDamageCounted buffDataDamageCounted = new BuffDataDamageCounted();
			buffDataDamageCounted.id = 46;
			skillEventBuffSelf2.buff = buffDataDamageCounted;
			buffDataDamageCounted.dur = float.MaxValue;
			buffDataDamageCounted.lifeCounter = 1;
			buffDataDamageCounted.damageAdd = AM.LinearEquationDouble((double)level, 2.5, 5.0);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetTotalHealthReg(0), false)), AM.csa(GameMath.GetPercentString(this.GetDamage(0), false))) + AM.GetCooldownText(150f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			float cooldownMax = (data as SkillActiveData).cooldownMax;
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetTotalHealthReg(0), false)), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(2.5, false) + ")")) + AM.GetCooldownText(cooldownMax, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			float cooldownMax = (data as SkillActiveData).cooldownMax;
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetTotalHealthReg(0), false)), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false))) + AM.GetCooldownText(cooldownMax, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundDelayed(0.3f, SoundArchieve.inst.thourAutoSkills[1], 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voThourSkillAuto2, 1f));
		}

		public float GetTotalHealthReg(int level)
		{
			return 0.5f;
		}

		public double GetDamage(int level)
		{
			return 5.0 + (double)level * 2.5;
		}

		private const float COOLDOWN = 150f;

		private const float DURATION = 5f;

		private const double HEALTH_REGEN = 0.1;

		private const double INITIAL_DAMAGE_ADD = 5.0;

		private const double LEVEL_DAMAGE_ADD = 2.5;
	}
}
