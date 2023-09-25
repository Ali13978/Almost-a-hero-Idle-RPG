using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseShockWave : SkillActiveDataBase
	{
		public SkillDataBaseShockWave()
		{
			this.nameKey = "SKILL_NAME_SHOCK_WAVE";
			this.descKey = "SKILL_DESC_SHOCK_WAVE";
			this.timeFadeInStart = 0.1f;
			this.timeFadeInEnd = 0.3f;
			this.timeFadeOutStart = 1.36666667f;
			this.timeFadeOutEnd = 1.56666672f;
			this.requiredHeroLevel = 0;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			int num = 0;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerBaseEarthquake)
				{
					num += AM.LinearEquationInteger(skillEnhancer.level, SkillEnhancerBaseEarthquake.INITIAL_NUM, SkillEnhancerBaseEarthquake.LEVEL_NUM);
				}
			}
			int num2 = 6 + num;
			data.dur = 1.36666667f + (float)num2 * 0.8f + 0.6666667f;
			data.durInvulnurability = 5f;
			this.durNonslowable = data.dur;
			this.durStayInFrontCurtain = data.dur;
			data.cooldownMax = this.GetCooldown(data.level);
			BuffData buffData = new BuffData();
			buffData.events = new List<BuffEvent>();
			buffData.dur = 0.8f;
			BuffEventDamageAll buffEventDamageAll = new BuffEventDamageAll();
			buffData.events.Add(buffEventDamageAll);
			buffEventDamageAll.time = buffData.dur / 2f;
			buffEventDamageAll.damageInDps = this.GetDamage(level);
			buffEventDamageAll.damageType = DamageType.SKILL;
			data.events = new List<SkillEvent>();
			for (int i = 0; i < num2; i++)
			{
				SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
				data.events.Add(skillEventBuffSelf);
				skillEventBuffSelf.time = 1.36666667f + (float)i * 0.8f;
				skillEventBuffSelf.buff = buffData;
			}
			data.animEvents = new List<SkillAnimEvent>();
			for (int j = 0; j < num2; j++)
			{
				SkillAnimEvent skillAnimEvent = new SkillAnimEvent();
				data.animEvents.Add(skillAnimEvent);
				skillAnimEvent.animIndex = 1;
				skillAnimEvent.time = data.events[j].time;
			}
			SkillAnimEvent skillAnimEvent2 = new SkillAnimEvent();
			data.animEvents.Add(skillAnimEvent2);
			skillAnimEvent2.animIndex = 2;
			skillAnimEvent2.time = data.events[num2 - 1].time + buffData.dur;
		}

		public override string GetDescZero()
		{
			int num = 6;
			double damage = this.GetDamage(0);
			return string.Format(LM.Get(this.descKey), AM.csa(num.ToString()), AM.csa(GameMath.GetPercentString(damage, false))) + AM.GetCooldownText(300f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			SkillActiveData skillActiveData = (SkillActiveData)data;
			int count = skillActiveData.events.Count;
			double damage = this.GetDamage(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(count.ToString()), AM.csa(GameMath.GetPercentString(damage, false)) + AM.csl(" (+" + GameMath.GetPercentString(2.5, false) + ")")) + AM.GetCooldownText(this.GetCooldown(data.level), 15f);
		}

		public override string GetDescFull(SkillData data)
		{
			SkillActiveData skillActiveData = (SkillActiveData)data;
			int count = skillActiveData.events.Count;
			double damage = this.GetDamage(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(count.ToString()), AM.csa(GameMath.GetPercentString(damage, false))) + AM.GetCooldownText(this.GetCooldown(data.level), -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundIdaUlti(skillData, 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voIdaUlti, 1f));
		}

		public double GetDamage(int level)
		{
			return 5.0 + (double)level * 2.5;
		}

		public float GetCooldown(int level)
		{
			return 300f - (float)level * 15f;
		}

		private const float COOLDOWN_BASE = 300f;

		private const float COOLDOWN_DEC = 15f;

		private const int NUM_ATTACKS_BASE = 6;

		private const double DAMAGE_INIT = 5.0;

		private const double DAMAGE_LEVEL = 2.5;

		private const float DUR_START = 1.36666667f;

		private const float DUR_ATTACK = 0.8f;

		private const float DUR_END = 0.6666667f;
	}
}
