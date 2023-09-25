using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseCrowAttack : SkillActiveDataBase
	{
		public SkillDataBaseCrowAttack()
		{
			this.nameKey = "SKILL_NAME_CROW_ATTACK";
			this.descKey = "SKILL_DESC_CROW_ATTACK";
			this.timeFadeInStart = 0.1f;
			this.timeFadeInEnd = 0.3f;
			this.timeFadeOutStart = 1f;
			this.timeFadeOutEnd = 1.2f;
			this.requiredHeroLevel = 1;
			this.maxLevel = 7;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			float blindDur = this.GetBlindDur(level);
			double num = 0.0;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerBaseDeathFromAbove)
				{
					num += AM.LinearEquationDouble((double)skillEnhancer.level, SkillEnhancerBaseDeathFromAbove.DAMAGE_LEVEL, SkillEnhancerBaseDeathFromAbove.DAMAGE_BASE);
				}
			}
			data.dur = 3.2f;
			data.cooldownMax = 150f;
			SkillEventBuffAllOpponents skillEventBuffAllOpponents = new SkillEventBuffAllOpponents();
			data.events = new List<SkillEvent>
			{
				skillEventBuffAllOpponents
			};
			skillEventBuffAllOpponents.time = 1.28000009f;
			BuffDataMissChanceAdd buffDataMissChanceAdd = new BuffDataMissChanceAdd();
			buffDataMissChanceAdd.id = 132;
			skillEventBuffAllOpponents.buff = buffDataMissChanceAdd;
			buffDataMissChanceAdd.isPermenant = false;
			buffDataMissChanceAdd.isStackable = false;
			buffDataMissChanceAdd.visuals |= 2048;
			buffDataMissChanceAdd.dur = blindDur;
			buffDataMissChanceAdd.missChanceAdd = this.GetMissChance(level);
			if (num > 0.0)
			{
				SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
				data.events.Add(skillEventBuffSelf);
				skillEventBuffSelf.time = 1.28000009f;
				BuffData buffData = new BuffData();
				skillEventBuffSelf.buff = buffData;
				buffData.dur = 0f;
				buffData.events = new List<BuffEvent>();
				BuffEventDamageAll buffEventDamageAll = new BuffEventDamageAll();
				buffData.events.Add(buffEventDamageAll);
				buffEventDamageAll.damageType = DamageType.SKILL;
				buffEventDamageAll.time = 0f;
				buffEventDamageAll.damageInDps = num;
			}
		}

		private float GetMissChance(int level)
		{
			return 0.6f + (float)level * 0.03f;
		}

		private float GetBlindDur(int level)
		{
			return 6f + 1f * (float)level;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetMissChance(0), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetBlindDur(0)))) + AM.GetCooldownText(150f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetMissChance(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.03f, false) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetBlindDur(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(1f) + ")")) + AM.GetCooldownText(150f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetMissChance(data.level), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetBlindDur(data.level)))) + AM.GetCooldownText(150f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voTamCrowAttack, 1f));
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.tamCrowAttack, 1f));
		}

		private const float MISS_CHANCE_BASE = 0.6f;

		private const float MISS_CHANCE_LEVEL = 0.03f;

		private const float ANIM_DUR = 3.2f;

		private const float SKILL_EVENT_TIME_RATIO = 0.4f;

		private const float BLIND_DUR_BASE = 6f;

		private const float BLIND_DUR_LEVEL = 1f;

		private const float COOLDOWN = 150f;
	}
}
