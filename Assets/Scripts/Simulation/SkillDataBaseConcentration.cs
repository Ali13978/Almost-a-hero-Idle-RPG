using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseConcentration : SkillActiveDataBase
	{
		public SkillDataBaseConcentration()
		{
			this.nameKey = "SKILL_NAME_CONCENTRATION";
			this.descKey = "SKILL_DESC_CONCENTRATION";
			this.requiredHeroLevel = 1;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.dur = 5.2f;
			data.durInvulnurability = 0f;
			data.cooldownMax = 150f;
			data.events = new List<SkillEvent>();
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf);
			skillEventBuffSelf.time = 4.33333349f;
			BuffData buffData = new BuffData();
			skillEventBuffSelf.buff = buffData;
			buffData.dur = 0f;
			BuffEventDamageAll buffEventDamageAll = new BuffEventDamageAll();
			buffEventDamageAll.damageType = DamageType.SKILL;
			buffData.events = new List<BuffEvent>();
			buffData.events.Add(buffEventDamageAll);
			buffEventDamageAll.time = 0f;
			buffEventDamageAll.damageInTeamDps = this.GetDamageInDPS(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageInDPS(0), false))) + AM.GetCooldownText(150f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageInDPS(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(1.5, false) + ")")) + AM.GetCooldownText(150f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageInDPS(data.level), false))) + AM.GetCooldownText(150f, -1f);
		}

		public double GetDamageInDPS(int level)
		{
			return 3.0 + (double)level * 1.5;
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.idaAutoSkills[1], 1f));
			base.AddSoundEvent(world, by, new SoundDelayed(4f, SoundArchieve.inst.idaHitGround, 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voIdaSkillAuto2, 1f));
		}

		private const float COOLDOWN = 150f;

		private const double INITIAL_DAMAGE = 3.0;

		private const double LEVEL_DAMAGE = 1.5;
	}
}
