using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseStampede : SkillActiveDataBase
	{
		public SkillDataBaseStampede()
		{
			this.nameKey = "SKILL_NAME_STAMPEDE";
			this.descKey = "SKILL_DESC_STAMPEDE";
			this.requiredHeroLevel = 1;
			this.maxLevel = 7;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			int num = 0;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerStrengthInNumbers)
				{
					num = SkillEnhancerStrengthInNumbers.GetAnimalsCount(skillEnhancer.level);
				}
			}
			data.dur = 3.067f;
			data.durInvulnurability = 3.067f;
			data.cooldownMax = 180f;
			data.events = new List<SkillEvent>();
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			skillEventBuffSelf.time = 1.5335f;
			skillEventBuffSelf.buff = new BuffData();
			skillEventBuffSelf.buff.dur = this.GetDur(level, num);
			skillEventBuffSelf.buff.events = new List<BuffEvent>();
			num += this.GetHitsCount(level);
			for (int i = 1; i <= num; i++)
			{
				skillEventBuffSelf.buff.events.Add(new BuffEventSpawnStampede
				{
					damageMul = this.GetDamageMultiplier(level),
					time = 0.7f * (float)i
				});
			}
			data.events.Add(skillEventBuffSelf);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetHitsCount(0).ToString()), AM.csa(GameMath.GetPercentString(this.GetDamageMultiplier(0), false))) + AM.GetCooldownText(180f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			int count = ((data as SkillActiveData).events[0] as SkillEventBuffSelf).buff.events.Count;
			return string.Format(LM.Get(this.descKey), AM.csa(count.ToString()), AM.csa(GameMath.GetPercentString(this.GetDamageMultiplier(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(2.0, false) + ")")) + AM.GetCooldownText(180f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			int count = ((data as SkillActiveData).events[0] as SkillEventBuffSelf).buff.events.Count;
			return string.Format(LM.Get(this.descKey), AM.csa(count.ToString()), AM.csa(GameMath.GetPercentString(this.GetDamageMultiplier(data.level), false))) + AM.GetCooldownText(180f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			bool flag = (by as Hero).IsUsingTempWeapon();
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.druidAutoSkills[(!flag) ? 0 : 1], 1f));
			if (flag)
			{
				base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voDruidStampedeBeast, 1f));
			}
			else
			{
				base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voDruidStampede, 1f));
			}
		}

		public float GetDur(int level, int hitsCountFromEnhancer)
		{
			return 1f * (float)(this.GetHitsCount(level) + hitsCountFromEnhancer);
		}

		public int GetHitsCount(int level)
		{
			return 3 + 0 * level;
		}

		public double GetDamageMultiplier(int level)
		{
			return 4.0 + 2.0 * (double)level;
		}

		public const float COOLDOWN = 180f;

		public const double DAMAGE_INIT = 4.0;

		public const double DAMAGE_PER_LEVEL = 2.0;

		public const int HITS_COUNT_INIT = 3;

		public const int HITS_COUNT_PER_LEVEL = 0;

		private const float DUR_ANIM = 3.067f;

		private const float DUR_ANIM_EACH_SPAWN = 1f;

		private const float DUR_BETWEEN_SPAWN = 0.7f;
	}
}
