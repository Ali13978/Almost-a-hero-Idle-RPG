using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseRunEmmetRun : SkillActiveDataBase
	{
		public SkillDataBaseRunEmmetRun()
		{
			this.nameKey = "SKILL_NAME_RUN_EMMET_RUN";
			this.descKey = "SKILL_DESC_RUN_EMMET_RUN";
			this.requiredHeroLevel = 1;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			float num = 0f;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerBaseSwiftEmmet)
				{
					num += AM.LinearEquationFloat((float)skillEnhancer.level, SkillEnhancerBaseSwiftEmmet.LEVEL_RED, SkillEnhancerBaseSwiftEmmet.INITIAL_RED);
				}
			}
			data.dur = 4f;
			data.durInvulnurability = 0f;
			data.cooldownMax = 150f - num;
			data.events = new List<SkillEvent>();
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf);
			skillEventBuffSelf.time = 2.66666675f;
			BuffData buffData = new BuffData();
			skillEventBuffSelf.buff = buffData;
			buffData.dur = 0f;
			buffData.events = new List<BuffEvent>();
			BuffEventStealGold buffEventStealGold = new BuffEventStealGold();
			buffData.events.Add(buffEventStealGold);
			buffEventStealGold.time = 0f;
			buffEventStealGold.goldFactor = this.GetGold(level);
		}

		public double GetGold(int level)
		{
			return 2.0 + (double)level * 1.0;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetGold(0), false))) + AM.GetCooldownText(150f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetGold(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(1.0, false) + ")")) + AM.GetCooldownText((data as SkillActiveData).cooldownMax, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetGold(data.level), false))) + AM.GetCooldownText((data as SkillActiveData).cooldownMax, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.sheelaAutoSkills[1], 1f));
			base.AddSoundEvent(world, by, new SoundDelayed(1.2f, SoundArchieve.inst.sheelaAutoSkillBacks[1], 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voSheelaSkillAuto2, 1f));
		}

		private const float COOLDOWN = 150f;

		private const double INITIAL_GOLD = 2.0;

		private const double LEVEL_GOLD = 1.0;
	}
}
