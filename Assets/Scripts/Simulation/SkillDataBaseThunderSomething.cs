using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseThunderSomething : SkillActiveDataBase
	{
		public SkillDataBaseThunderSomething()
		{
			this.nameKey = "SKILL_NAME_THUNDER_SOMETHING";
			this.descKey = "SKILL_DESC_THUNDER_SOMETHING";
			this.requiredHeroLevel = 1;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			double num = 0.0;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerBaseTricks)
				{
					num += AM.LinearEquationDouble(SkillEnhancerBaseTricks.LEVEL_BONUS, (double)skillEnhancer.level, SkillEnhancerBaseTricks.INITIAL_BONUS);
				}
			}
			float num2 = 0f;
			foreach (SkillEnhancer skillEnhancer2 in enhancers)
			{
				if (skillEnhancer2.enhancerBase is SkillEnhancerBaseRapidThunder)
				{
					num2 += AM.LinearEquationFloat((float)skillEnhancer2.level, SkillEnhancerBaseRapidThunder.LEVEL_REDUCTION, SkillEnhancerBaseRapidThunder.INITIAL_REDUCTION);
				}
			}
			data.dur = 4.1f;
			data.durInvulnurability = 0f;
			data.cooldownMax = 180f * (1f - num2);
			data.events = new List<SkillEvent>();
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf);
			skillEventBuffSelf.time = 2.82f;
			BuffData buffData = new BuffData();
			skillEventBuffSelf.buff = buffData;
			buffData.dur = 1.6f;
			buffData.events = new List<BuffEvent>();
			BuffEventDamageAll buffEventDamageAll = new BuffEventDamageAll();
			buffData.events.Add(buffEventDamageAll);
			buffEventDamageAll.time = 0f;
			buffEventDamageAll.damageInDps = this.GetDamage(level);
			buffEventDamageAll.damageInDps *= 1.0 + num;
			buffEventDamageAll.damageType = DamageType.SKILL;
			BuffEventCurseAll buffEventCurseAll = new BuffEventCurseAll();
			buffData.events.Add(buffEventCurseAll);
			buffEventCurseAll.time = 0f;
			BuffDataDefense buffDataDefense = new BuffDataDefense();
			buffDataDefense.id = 59;
			buffEventCurseAll.effect = buffDataDefense;
			buffDataDefense.damageTakenFactor = 1.0 + (double)this.GetAmplify(level);
			buffDataDefense.dur = 8f;
			buffDataDefense.visuals = 32;
		}

		public override string GetDescZero()
		{
			double damage = this.GetDamage(0);
			float amplify = this.GetAmplify(0);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(damage, false)), AM.csa(GameMath.GetPercentString(amplify, false)), AM.csa(GameMath.GetTimeInMilliSecondsString(8f))) + AM.GetCooldownText(180f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			double damageInDps = (((data as SkillActiveData).events[0] as SkillEventBuffSelf).buff.events[0] as BuffEventDamageAll).damageInDps;
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(damageInDps, false)) + AM.csl(" (+" + GameMath.GetPercentString(5.0, false) + ")"), AM.csa(GameMath.GetPercentString(this.GetAmplify(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.05f, false) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(8f))) + AM.GetCooldownText((data as SkillActiveData).cooldownMax, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			double damageInDps = (((data as SkillActiveData).events[0] as SkillEventBuffSelf).buff.events[0] as BuffEventDamageAll).damageInDps;
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(damageInDps, false)), AM.csa(GameMath.GetPercentString(this.GetAmplify(data.level), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(8f))) + AM.GetCooldownText((data as SkillActiveData).cooldownMax, -1f);
		}

		public double GetDamage(int level)
		{
			return 15.0 + (double)level * 5.0;
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundDerekSkillAuto(1, skillData, 1f));
			base.AddSoundEvent(world, by, new SoundDelayed(skillData.events[0].time - 0.3f, SoundArchieve.inst.derekFireball, 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voDerekSkillAuto2, 1f));
		}

		public float GetAmplify(int level)
		{
			return 0.2f + (float)level * 0.05f;
		}

		private const float COOLDOWN = 180f;

		private const float DUR_TOT = 4.1f;

		private const float DUR_METEOR = 1.6f;

		private const double INITIAL_DAMAGE = 15.0;

		private const double LEVEL_DAMAGE = 5.0;

		private const float INIT_AMPLIFY = 0.2f;

		private const float LEVEL_AMPLIFY = 0.05f;

		private const float DUR_AMPLIFY = 8f;
	}
}
