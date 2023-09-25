using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseTaunt : SkillActiveDataBase
	{
		public SkillDataBaseTaunt()
		{
			this.nameKey = "SKILL_NAME_TAUNT";
			this.descKey = "SKILL_DESC_TAUNT";
			this.requiredHeroLevel = 1;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.dur = 1.5f;
			data.durInvulnurability = 0f;
			data.cooldownMax = 90f;
			data.events = new List<SkillEvent>();
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf);
			skillEventBuffSelf.time = data.dur;
			BuffDataTaunt buffDataTaunt = new BuffDataTaunt();
			buffDataTaunt.id = 181;
			skillEventBuffSelf.buff = buffDataTaunt;
			buffDataTaunt.dur = this.GetTauntDur(level);
			buffDataTaunt.isStackable = true;
			buffDataTaunt.tauntAdd = BuffDataTaunt.TauntAgroThourPassive;
			buffDataTaunt.visuals |= 256;
			BuffEventShieldSelf buffEventShieldSelf = new BuffEventShieldSelf();
			buffDataTaunt.events = new List<BuffEvent>
			{
				buffEventShieldSelf
			};
			buffEventShieldSelf.time = 0f;
			buffEventShieldSelf.ratio = this.GetShieldAmount(level);
			buffEventShieldSelf.dur = 1000f;
		}

		public float GetTauntDur(int level)
		{
			return 20f + (float)level * 5f;
		}

		public double GetShieldAmount(int level)
		{
			return 0.4 + (double)level * 0.1;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetShieldAmount(0), false)), AM.csa(GameMath.GetTimeInSecondsString(20f))) + AM.GetCooldownText(90f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetShieldAmount(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.1, false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(this.GetTauntDur(data.level))) + AM.csl(" (+" + GameMath.GetTimeInSecondsString(5f) + ")")) + AM.GetCooldownText(90f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetShieldAmount(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(this.GetTauntDur(data.level)))) + AM.GetCooldownText(90f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundDelayed(0.4f, SoundArchieve.inst.thourAutoSkills[0], 1f));
			base.AddSoundEvent(world, by, new SoundDelayed(0.8f, SoundArchieve.inst.thourAutoSkills[0], 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voThourSkillAuto1, 1f));
		}

		private const float COOLDOWN = 90f;

		private const float TAUNT_DURATION = 20f;

		private const float TAUNT_DURATION_LEVEL = 5f;

		private const double SHIELD_AMOUNT_INITIAL = 0.4;

		private const double SHIELD_AMOUNT_LEVEL = 0.1;
	}
}
