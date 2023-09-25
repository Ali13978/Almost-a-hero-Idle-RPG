using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseSliceDice : SkillActiveDataBase
	{
		public SkillDataBaseSliceDice()
		{
			this.nameKey = "SKILL_NAME_SLICE_AND_DICE";
			this.descKey = "SKILL_DESC_SLICE_AND_DICE";
			this.requiredHeroLevel = 1;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.dur = 1.8f;
			data.durInvulnurability = 0f;
			data.cooldownMax = 90f;
			data.events = new List<SkillEvent>();
			SkillEventDamageAll skillEventDamageAll = new SkillEventDamageAll();
			skillEventDamageAll.canCrit = true;
			data.events.Add(skillEventDamageAll);
			skillEventDamageAll.time = 0.6f;
			skillEventDamageAll.damageType = DamageType.SKILL;
			skillEventDamageAll.damageInDps = this.GetDamage(level);
			SkillEventStunAll skillEventStunAll = new SkillEventStunAll();
			data.events.Add(skillEventStunAll);
			skillEventStunAll.time = 0.6f;
			BuffDataStun buffDataStun = new BuffDataStun();
			buffDataStun.id = 170;
			buffDataStun.visuals |= 512;
			skillEventStunAll.effect = buffDataStun;
			buffDataStun.dur = this.GetStunDuration(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(0), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetStunDuration(0)))) + AM.GetCooldownText(90f, -1f);
		}

		private float GetStunDuration(int level)
		{
			return 2f + 0.5f * (float)level;
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(2.0, false) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetStunDuration(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(0.5f) + ")")) + AM.GetCooldownText(90f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetStunDuration(data.level)))) + AM.GetCooldownText(90f, -1f);
		}

		public double GetDamage(int level)
		{
			return 4.0 + (double)level * 2.0;
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.sheelaAutoSkills[0], 1f));
			base.AddSoundEvent(world, by, new SoundDelayed(0.6f, SoundArchieve.inst.sheelaAutoSkillBacks[0], 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voSheelaSkillAuto1, 1f));
		}

		private const float COOLDOWN = 90f;

		private const double INITIAL_DAMAGE = 4.0;

		private const double LEVEL_DAMAGE = 2.0;

		private const float STUN_DURATION = 2f;

		private const float LEVEL_STUN_DURATION = 0.5f;
	}
}
