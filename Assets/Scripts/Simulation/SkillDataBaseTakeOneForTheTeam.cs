using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseTakeOneForTheTeam : SkillActiveDataBase
	{
		public SkillDataBaseTakeOneForTheTeam()
		{
			this.nameKey = "SKILL_NAME_TAKE_ONE_FOR_THE_TEAM";
			this.descKey = "SKILL_DESC_TAKE_ONE_FOR_THE_TEAM";
			this.requiredHeroLevel = 1;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			int num = -1;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerLongWinded)
				{
					num = skillEnhancer.level;
				}
			}
			data.durInvulnurability = 2f;
			data.cooldownMax = this.GetCooldown(level);
			data.events = new List<SkillEvent>();
			float num2 = this.GetDur(level);
			if (num >= 0)
			{
				num2 += SkillEnhancerLongWinded.GetIncreaseDuration(num);
			}
			data.dur = 2f + num2;
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			skillEventBuffSelf.time = 2f;
			skillEventBuffSelf.buff = new BuffDataHealAlliesWhenHit
			{
				dur = num2,
				healRatio = this.GetHealRatio(level),
				onDeathSound = new SoundSimple(SoundArchieve.inst.babuDeathInUlti, 1f),
				id = 322
			};
			skillEventBuffSelf.buff.events = new List<BuffEvent>
			{
				new BuffEventAction(num2 - 1.9f, BuffEventAction.ID_BABU_ULTI_FINISH)
			};
			skillEventBuffSelf.buff.tag = BuffTags.BABU_HEAL;
			data.events.Add(skillEventBuffSelf);
			SkillEventBuffSelf skillEventBuffSelf2 = new SkillEventBuffSelf();
			skillEventBuffSelf2.time = 2f;
			skillEventBuffSelf2.buff = new BuffDataTaunt
			{
				dur = num2,
				tauntAdd = BuffDataTaunt.TauntAgroBabuUlti,
				id = 331
			};
			data.events.Add(skillEventBuffSelf2);
			data.animEvents = new List<SkillAnimEvent>();
			SkillAnimEvent skillAnimEvent = new SkillAnimEvent();
			skillAnimEvent.time = 0f;
			skillAnimEvent.animIndex = 0;
			SkillAnimEvent skillAnimEvent2 = new SkillAnimEvent();
			skillAnimEvent2.time = 2f;
			skillAnimEvent2.animIndex = 1;
			SkillAnimEvent skillAnimEvent3 = new SkillAnimEvent();
			skillAnimEvent3.time = 2f + num2 - 1.9f;
			skillAnimEvent3.animIndex = 2;
			data.animEvents.Add(skillAnimEvent);
			data.animEvents.Add(skillAnimEvent2);
			data.animEvents.Add(skillAnimEvent3);
		}

		private float GetCooldown(int level)
		{
			return 200f - (float)level * 20f;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(this.GetDur(0))), AM.csa(GameMath.GetPercentString(this.GetHealRatio(0), false))) + AM.GetCooldownText(this.GetCooldown(0), -1f);
		}

		public override string GetDesc(SkillData data)
		{
			BuffData buff = ((data as SkillActiveData).events[0] as SkillEventBuffSelf).buff;
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(buff.dur)), AM.csa(GameMath.GetPercentString(this.GetHealRatio(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.0099999997764825821, false) + ")")) + AM.GetCooldownText(this.GetCooldown(data.level), 20f);
		}

		public override string GetDescFull(SkillData data)
		{
			BuffData buff = ((data as SkillActiveData).events[0] as SkillEventBuffSelf).buff;
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(buff.dur)), AM.csa(GameMath.GetPercentString(this.GetHealRatio(data.level), false))) + AM.GetCooldownText(this.GetCooldown(data.level), -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundBabuUlti(skillData, 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voBabuUlti, 1f));
		}

		public float GetDur(int level)
		{
			return 8f;
		}

		public double GetHealRatio(int level)
		{
			return 0.0099999997764825821 + 0.0099999997764825821 * (double)level;
		}

		public const float COOLDOWN_INIT = 200f;

		public const float COOLDOWN_LEVEL = 20f;

		public const float DUR_INIT = 8f;

		public const double HEAL_RATIO_INIT = 0.0099999997764825821;

		public const double HEAL_RATIO_PER_LEVEL = 0.0099999997764825821;

		private const float DUR_ANIM_START = 2f;

		private const float DUR_ANIM_END = 1.9f;

		private SoundEvent loopSoundEvent;
	}
}
