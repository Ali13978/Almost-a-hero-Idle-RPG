using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseDemonicSwarm : SkillActiveDataBase
	{
		public SkillDataBaseDemonicSwarm()
		{
			this.nameKey = "SKILL_NAME_DEMONIC_SWARM";
			this.descKey = "SKILL_DESC_DEMONIC_SWARM";
			this.timeFadeInStart = 0.1f;
			this.timeFadeInEnd = 0.3f;
			this.timeFadeOutStart = 0.8f;
			this.timeFadeOutEnd = 1f;
			this.requiredHeroLevel = 1;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			float num = 0f;
			float num2 = 0f;
			int num3 = 0;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerChakraBooster)
				{
					SkillEnhancerChakraBooster skillEnhancerChakraBooster = skillEnhancer.enhancerBase as SkillEnhancerChakraBooster;
					num3 = skillEnhancerChakraBooster.GetCount(skillEnhancer.level);
				}
				if (skillEnhancer.enhancerBase is SkillEnhancerWarmerSwarm)
				{
					SkillEnhancerWarmerSwarm skillEnhancerWarmerSwarm = skillEnhancer.enhancerBase as SkillEnhancerWarmerSwarm;
					num2 = skillEnhancerWarmerSwarm.GetRed(skillEnhancer.level);
				}
			}
			int num4 = 5 + num3;
			float num5 = 1f / (float)num4;
			float dur = 7.7691f;
			data.dur = 5.33f;
			data.durInvulnurability = 2.5f;
			this.durNonslowable = data.dur;
			this.durStayInFrontCurtain = 1f;
			data.cooldownMax = 60f;
			data.events = new List<SkillEvent>();
			SkillEventDamageSelf skillEventDamageSelf = new SkillEventDamageSelf();
			skillEventDamageSelf.damagePer = this.GetSelfDamage(level) - (double)num2;
			skillEventDamageSelf.time = 2.2386f;
			skillEventDamageSelf.damageType = DamageType.SKILL;
			data.events.Add(skillEventDamageSelf);
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf);
			skillEventBuffSelf.time = 0f;
			BuffData buffData = new BuffData();
			skillEventBuffSelf.buff = buffData;
			buffData.dur = dur;
			buffData.events = new List<BuffEvent>();
			if (num > 0f)
			{
				SkillEventBuffAllAllies skillEventBuffAllAllies = new SkillEventBuffAllAllies();
				skillEventBuffAllAllies.applySelf = false;
				skillEventBuffAllAllies.time = 0f;
				skillEventBuffAllAllies.buff = new BuffDataUltiReduction
				{
					reduction = num
				};
				data.events.Add(skillEventBuffAllAllies);
			}
			data.animEvents = new List<SkillAnimEvent>();
			for (int i = 0; i < num4; i++)
			{
				BuffEventSwarmDragon buffEventSwarmDragon = new BuffEventSwarmDragon();
				buffEventSwarmDragon.time = 1f + (float)i * num5;
				buffEventSwarmDragon.damageMul = this.GetDamage(level);
				buffData.events.Add(buffEventSwarmDragon);
			}
		}

		public override string GetDescZero()
		{
			double damage = this.GetDamage(0);
			double selfDamage = this.GetSelfDamage(0);
			int num = 5;
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(selfDamage, false)), AM.csa(num.ToString()), AM.csa(GameMath.GetPercentString(damage, false))) + AM.GetCooldownText(60f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			SkillActiveData skillActiveData = data as SkillActiveData;
			SkillEventDamageSelf skillEventDamageSelf = skillActiveData.events[0] as SkillEventDamageSelf;
			int count = (skillActiveData.events[1] as SkillEventBuffSelf).buff.events.Count;
			double damage = this.GetDamage(data.level);
			double number = this.GetDamage(data.level + 1) - this.GetDamage(data.level);
			double damagePer = skillEventDamageSelf.damagePer;
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(damagePer, false)), AM.csa(count.ToString()), AM.csa(GameMath.GetPercentString(damage, false)) + AM.csl(" (+" + GameMath.GetPercentString(number, false) + ")")) + AM.GetCooldownText(60f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			SkillActiveData skillActiveData = data as SkillActiveData;
			int count = (skillActiveData.events[1] as SkillEventBuffSelf).buff.events.Count;
			SkillEventDamageSelf skillEventDamageSelf = skillActiveData.events[0] as SkillEventDamageSelf;
			double damage = this.GetDamage(data.level);
			double damagePer = skillEventDamageSelf.damagePer;
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(damagePer, false)), AM.csa(count.ToString()), AM.csa(GameMath.GetPercentString(damage, false))) + AM.GetCooldownText(60f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.warlockAutoSkills[0], 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voWarlockSpawn, 1f));
		}

		public double GetDamage(int level)
		{
			return 6.0 + (double)level * 1.0;
		}

		public double GetSelfDamage(int level)
		{
			return 0.75 + (double)level * 0.0;
		}

		private const float COOLDOWN_BASE = 60f;

		private const int NUM_DEMONS = 5;

		private const double DAMAGE_INIT = 6.0;

		private const double DAMAGE_LEVEL = 1.0;

		public const double DAMAGE_SELF_INIT = 0.75;

		public const double DAMAGE_SELF_LEVEL = 0.0;

		private const float SKILL_DUR = 5.33f;

		private const float DUR_START = 2.2386f;

		private const float DUR_SUMMON = 1f;

		private const float DUR_END = 4.5305f;
	}
}
