using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseGreedGrenade : SkillActiveDataBase
	{
		public SkillDataBaseGreedGrenade()
		{
			this.nameKey = "SKILL_NAME_GREED_GRENADE";
			this.descKey = "SKILL_DESC_GREED_GRENADE";
			this.requiredHeroLevel = 0;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			float num = 0f;
			float num2 = 0f;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerBaseDistraction)
				{
					num += AM.LinearEquationFloat((float)skillEnhancer.level, SkillEnhancerBaseDistraction.LEVEL_DAMAGE_FACTOR, SkillEnhancerBaseDistraction.INITIAL_DAMAGE_FACTOR);
					num2 += AM.LinearEquationFloat((float)skillEnhancer.level, SkillEnhancerBaseDistraction.LEVEL_DURATION, SkillEnhancerBaseDistraction.INITIAL_DURATION);
				}
			}
			data.dur = 6.7f;
			data.durInvulnurability = 3f;
			data.cooldownMax = 105f;
			data.events = new List<SkillEvent>();
			SkillEventDamageAll skillEventDamageAll = new SkillEventDamageAll();
			skillEventDamageAll.canCrit = true;
			data.events.Add(skillEventDamageAll);
			skillEventDamageAll.time = data.dur * 0.35f;
			skillEventDamageAll.damageType = DamageType.SKILL;
			skillEventDamageAll.damageInDps = this.GetDamage(level);
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf);
			skillEventBuffSelf.time = data.dur * 0.35f;
			BuffData buffData = new BuffData();
			buffData.id = 287;
			skillEventBuffSelf.buff = buffData;
			buffData.dur = 0f;
			buffData.events = new List<BuffEvent>();
			BuffEventGreedGrenade buffEventGreedGrenade = new BuffEventGreedGrenade();
			buffData.events.Add(buffEventGreedGrenade);
			buffEventGreedGrenade.time = 0f;
			buffEventGreedGrenade.goldFactor = this.GetGold(level);
			if (num > 0f)
			{
				BuffEventCurseAll buffEventCurseAll = new BuffEventCurseAll();
				buffData.events.Add(buffEventCurseAll);
				buffEventCurseAll.time = 0f;
				BuffDataDefense buffDataDefense = new BuffDataDefense();
				buffDataDefense.id = 360;
				buffEventCurseAll.effect = buffDataDefense;
				buffDataDefense.damageTakenFactor = 1.0 + (double)num;
				buffDataDefense.dur = num2;
				buffDataDefense.visuals = 32;
			}
		}

		public override string GetDescZero()
		{
			double damage = this.GetDamage(0);
			double gold = this.GetGold(0);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(gold, false)), AM.csa(GameMath.GetPercentString(damage, false))) + AM.GetCooldownText(105f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			double damage = this.GetDamage(data.level);
			double gold = this.GetGold(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(gold, false)) + AM.csl(" (+" + GameMath.GetPercentString(0.5f, false) + ")"), AM.csa(GameMath.GetPercentString(damage, false))) + AM.GetCooldownText(105f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			double damage = this.GetDamage(data.level);
			double gold = this.GetGold(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(gold, false)), AM.csa(GameMath.GetPercentString(damage, false))) + AM.GetCooldownText(105f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.goblinUlti, 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voGoblinUlti, 1f));
		}

		public double GetDamage(int level)
		{
			return 10.0;
		}

		public double GetGold(int level)
		{
			return (double)(2f + 0.5f * (float)level);
		}

		private const float COOLDOWN = 105f;

		private const float GOLD_INITIAL = 2f;

		private const float GOLD_LEVEL = 0.5f;

		private const double DMG_INITIAL = 10.0;
	}
}
