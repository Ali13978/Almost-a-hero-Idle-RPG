using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseHeHasThePower : SkillActiveDataBase
	{
		public SkillDataBaseHeHasThePower()
		{
			this.nameKey = "SKILL_NAME_I_HAVE_THE_POWER";
			this.descKey = "SKILL_DESC_I_HAVE_THE_POWER";
			this.requiredHeroLevel = 1;
			this.maxLevel = 2;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.dur = 2.4f;
			data.durInvulnurability = 0f;
			data.cooldownMax = 150f;
			data.events = new List<SkillEvent>();
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf);
			skillEventBuffSelf.time = data.dur / 2f;
			BuffDataDamageCounted buffDataDamageCounted = new BuffDataDamageCounted();
			buffDataDamageCounted.id = 45;
			skillEventBuffSelf.buff = buffDataDamageCounted;
			buffDataDamageCounted.dur = float.PositiveInfinity;
			buffDataDamageCounted.lifeCounter = this.GetNumTimes(level);
			buffDataDamageCounted.damageAdd = this.GetDamage(level);
			buffDataDamageCounted.visuals |= 8;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(3.0, false)), AM.csa(5.ToString())) + AM.GetCooldownText(150f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(1.0, false) + ")"), AM.csa(this.GetNumTimes(data.level).ToString()) + AM.csl(" (+" + 2.ToString() + ")")) + AM.GetCooldownText(150f, -1f);
		}

		private double GetDamage(int level)
		{
			return 3.0 + (double)level * 1.0;
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)), AM.csa(this.GetNumTimes(data.level).ToString())) + AM.GetCooldownText(150f, -1f);
		}

		public int GetNumTimes(int level)
		{
			return 5 + 2 * level;
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.horatioHasThePower, 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voHoratioSkillAuto2, 1f));
		}

		private const float COOLDOWN = 150f;

		private const double DMG_INITIAL = 3.0;

		private const double DMG_LEVEL = 1.0;

		private const int NUM_TIMES = 5;

		private const int NUM_TIMES_ADD = 2;
	}
}
