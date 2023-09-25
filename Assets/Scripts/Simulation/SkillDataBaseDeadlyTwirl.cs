using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class SkillDataBaseDeadlyTwirl : SkillActiveDataBase
	{
		public SkillDataBaseDeadlyTwirl()
		{
			this.nameKey = "SKILL_NAME_DEADLY_TWIRL";
			this.descKey = "SKILL_DESC_DEADLY_TWIRL";
			this.maxLevel = 5;
			this.requiredHeroLevel = 0;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			int num = 0;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerBaseEverlastingSpin)
				{
					num += (int)AM.LinearEquationFloat((float)skillEnhancer.level, (float)SkillEnhancerBaseEverlastingSpin.NUM_LEVEL, (float)SkillEnhancerBaseEverlastingSpin.NUM_INITIAL);
				}
			}
			int num2 = 8 + num;
			int num3 = Mathf.CeilToInt((float)num2 / 4f);
			this.timeFadeOutStart = 3f;
			this.timeFadeOutEnd = 3.19999981f;
			this.durNonslowable = 3.1f + (float)num3 * 0.8f + 0.533333361f;
			this.durStayInFrontCurtain = this.durNonslowable;
			data.dur = 3.1f + (float)num3 * 0.8f + 0.533333361f;
			data.durInvulnurability = 5f;
			data.cooldownMax = 160f;
			data.events = new List<SkillEvent>();
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf);
			skillEventBuffSelf.time = 0f;
			BuffData buffData = new BuffData();
			skillEventBuffSelf.buff = buffData;
			buffData.dur = 3.1f + (float)num3 * 0.8f + 0.533333361f;
			buffData.events = new List<BuffEvent>();
			for (int i = 0; i < num2; i++)
			{
				BuffEventDamageAll buffEventDamageAll = new BuffEventDamageAll();
				buffData.events.Add(buffEventDamageAll);
				buffEventDamageAll.time = 3.1f + (float)num3 * 0.8f * ((float)i * 1f / (float)num2);
				buffEventDamageAll.damageInDps = 4.0 + 1.0 * (double)level;
				buffEventDamageAll.damageType = DamageType.SKILL;
			}
			data.animEvents = new List<SkillAnimEvent>();
			for (int j = 0; j < num3; j++)
			{
				SkillAnimEvent skillAnimEvent = new SkillAnimEvent();
				data.animEvents.Add(skillAnimEvent);
				skillAnimEvent.animIndex = 1;
				skillAnimEvent.time = 3.1f + 0.8f * (float)j;
			}
			SkillAnimEvent skillAnimEvent2 = new SkillAnimEvent();
			data.animEvents.Add(skillAnimEvent2);
			skillAnimEvent2.animIndex = 2;
			skillAnimEvent2.time = 3.1f + 0.8f * (float)num3;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(8.ToString()), AM.csa(GameMath.GetPercentString(4.0, true))) + AM.GetCooldownText(160f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			SkillActiveData skillActiveData = (SkillActiveData)data;
			SkillEventBuffSelf skillEventBuffSelf = (SkillEventBuffSelf)skillActiveData.events[0];
			BuffData buff = skillEventBuffSelf.buff;
			BuffEventDamageAll buffEventDamageAll = (BuffEventDamageAll)buff.events[0];
			return string.Format(LM.Get(this.descKey), AM.csa(buff.events.Count.ToString()), AM.csa(GameMath.GetPercentString(buffEventDamageAll.damageInDps, true)) + AM.csl(" (+" + GameMath.GetPercentString(1.0, true) + ")")) + AM.GetCooldownText(160f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			SkillActiveData skillActiveData = (SkillActiveData)data;
			SkillEventBuffSelf skillEventBuffSelf = (SkillEventBuffSelf)skillActiveData.events[0];
			BuffData buff = skillEventBuffSelf.buff;
			BuffEventDamageAll buffEventDamageAll = (BuffEventDamageAll)buff.events[0];
			return string.Format(LM.Get(this.descKey), AM.csa(buff.events.Count.ToString()), AM.csa(GameMath.GetPercentString(buffEventDamageAll.damageInDps, true))) + AM.GetCooldownText(160f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundHoratioUlti(skillData, 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voHoratioUlti, 1f));
		}

		private const float COOLDOWN = 160f;

		private const float DUR_START = 3.1f;

		private const float DUR_ATTACK_ANIM = 0.8f;

		private const float DUR_END = 0.533333361f;

		private const double UPGRADE_DPS = 1.0;

		private const double DAMAGE_IN_DPS = 4.0;

		private const int NUM_ATTACKS = 8;
	}
}
