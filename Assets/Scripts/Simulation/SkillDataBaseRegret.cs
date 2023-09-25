using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseRegret : SkillActiveDataBase
	{
		public SkillDataBaseRegret()
		{
			this.nameKey = "SKILL_NAME_REGRET";
			this.descKey = "SKILL_DESC_REGRET";
			this.timeFadeInStart = 0.1f;
			this.timeFadeInEnd = 0.2f;
			this.timeFadeOutStart = 0.9f;
			this.timeFadeOutEnd = 1f;
			this.requiredHeroLevel = 1;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.dur = 3f;
			data.durInvulnurability = 0f;
			this.durStayInFrontCurtain = 1f;
			data.cooldownMax = this.GetCooldown(level);
			BuffDataDamageAdd buffDataDamageAdd = new BuffDataDamageAdd();
			buffDataDamageAdd.id = 273;
			buffDataDamageAdd.isStackable = false;
			buffDataDamageAdd.dur = 20f;
			buffDataDamageAdd.damageAdd = this.GetDamage(level);
			buffDataDamageAdd.visuals |= 8;
			BuffDataHealthRegret buffDataHealthRegret = new BuffDataHealthRegret();
			buffDataHealthRegret.lifeCounter = 1;
			buffDataHealthRegret.damagePer = 0.5;
			buffDataHealthRegret.healDur = 20f;
			buffDataHealthRegret.dur = 0.2f;
			buffDataHealthRegret.id = 274;
			data.events = new List<SkillEvent>();
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			skillEventBuffSelf.time = data.dur * 0.5f;
			skillEventBuffSelf.buff = buffDataDamageAdd;
			data.events.Add(skillEventBuffSelf);
			SkillEventBuffRandomAlly skillEventBuffRandomAlly = new SkillEventBuffRandomAlly();
			skillEventBuffRandomAlly.time = data.dur * 0.5f;
			skillEventBuffRandomAlly.buffs = new List<BuffData>
			{
				buffDataHealthRegret
			};
			data.events.Add(skillEventBuffRandomAlly);
		}

		private double GetDamage(int level)
		{
			return (double)(0.8f + (float)level * 0.2f);
		}

		private float GetCooldown(int level)
		{
			return 75f - (float)level * 5f;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(0.5f, false)), AM.csa(GameMath.GetPercentString(this.GetDamage(0), false)), AM.csa(GameMath.GetTimeInSecondsString(20f))) + AM.GetCooldownText(this.GetCooldown(0), -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(0.5f, false)), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.2f, false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(20f))) + AM.GetCooldownText(this.GetCooldown(data.level), 5f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(0.5f, false)), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(20f))) + AM.GetCooldownText(this.GetCooldown(data.level), -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.warlockAutoSkills[1], 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voWarlockRegret, 1f));
		}

		private const float COOLDOWN = 75f;

		private const float LEVEL_COOLDOWN = 5f;

		private const float INITIAL_DURATION = 20f;

		private const float INITIAL_DAMAGE_BONUS = 0.8f;

		private const float LEVEL_DAMAGE_BONUS = 0.2f;

		private const float INITIAL_ALLY_DAMAGE = 0.5f;
	}
}
