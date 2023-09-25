using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseSlam : SkillActiveDataBase
	{
		public SkillDataBaseSlam()
		{
			this.nameKey = "SKILL_NAME_SLAM";
			this.descKey = "SKILL_DESC_SLAM";
			this.requiredHeroLevel = 1;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.dur = 1.73f;
			data.durInvulnurability = 0f;
			data.cooldownMax = 60f;
			float time = data.dur * 0.5f;
			data.events = new List<SkillEvent>();
			SkillEventDamageAll skillEventDamageAll = new SkillEventDamageAll();
			skillEventDamageAll.canCrit = true;
			skillEventDamageAll.damageType = DamageType.SKILL;
			data.events.Add(skillEventDamageAll);
			skillEventDamageAll.time = time;
			skillEventDamageAll.damageInDps = this.GetDamage(level);
			SkillEventStunAll skillEventStunAll = new SkillEventStunAll();
			data.events.Add(skillEventStunAll);
			skillEventStunAll.time = time;
			skillEventStunAll.effect = new BuffDataStun();
			skillEventStunAll.effect.id = 172;
			skillEventStunAll.effect.dur = this.GetStunDuration(level);
			skillEventStunAll.effect.visuals |= 512;
		}

		private float GetStunDuration(int level)
		{
			return 3f + (float)level * 1f;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(0), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetStunDuration(0)))) + AM.GetCooldownText(60f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(2.5, false) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetStunDuration(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(1f) + ")")) + AM.GetCooldownText(60f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetStunDuration(data.level)))) + AM.GetCooldownText(60f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.samAutoSkills[1], 1f));
			base.AddSoundEvent(world, by, new SoundDelayed(skillData.dur * 0.5f, SoundArchieve.inst.samAutoSkillBack, 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voSamSkillAuto2, 1f));
		}

		public double GetDamage(int level)
		{
			return 5.0 + 2.5 * (double)level;
		}

		private const float COOLDOWN = 60f;

		private const float STUN_DURATION = 3f;

		private const float LEVEL_STUN_DURATION = 1f;

		private const double INIT_DAMAGE = 5.0;

		private const double LEVEL_DAMAGE = 2.5;
	}
}
