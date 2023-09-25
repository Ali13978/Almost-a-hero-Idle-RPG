using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseSwiftMoves : SkillActiveDataBase
	{
		public SkillDataBaseSwiftMoves()
		{
			this.nameKey = "SKILL_NAME_SWIFT_MOVES";
			this.descKey = "SKILL_DESC_SWIFT_MOVES";
			this.requiredHeroLevel = 1;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.dur = 1f;
			data.durInvulnurability = 0f;
			data.cooldownMax = this.GetCooldown(level);
			data.events = new List<SkillEvent>();
			float duration = this.GetDuration(level);
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf);
			skillEventBuffSelf.time = data.dur;
			BuffDataSwiftMoves buffDataSwiftMoves = new BuffDataSwiftMoves();
			buffDataSwiftMoves.id = 179;
			skillEventBuffSelf.buff = buffDataSwiftMoves;
			buffDataSwiftMoves.dur = duration;
			buffDataSwiftMoves.dodgeChanceAdd = SkillDataBaseSwiftMoves.DODGE_CHANCE;
			buffDataSwiftMoves.critChanceAdd = SkillDataBaseSwiftMoves.CRIT_CHANCE;
			BuffEventZeroBuffGenericCounter buffEventZeroBuffGenericCounter = new BuffEventZeroBuffGenericCounter();
			buffDataSwiftMoves.events = new List<BuffEvent>
			{
				buffEventZeroBuffGenericCounter
			};
			buffDataSwiftMoves.visuals |= 16388;
			buffEventZeroBuffGenericCounter.buffType = typeof(BuffDataElegance);
			buffEventZeroBuffGenericCounter.time = buffDataSwiftMoves.dur;
		}

		private float GetCooldown(int level)
		{
			return SkillDataBaseSwiftMoves.COOLDOWN - (float)level * SkillDataBaseSwiftMoves.LEVEL_COOLDOWN;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(SkillDataBaseSwiftMoves.DODGE_CHANCE, false)), AM.csa(GameMath.GetPercentString(SkillDataBaseSwiftMoves.CRIT_CHANCE, false)), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(0)))) + AM.GetCooldownText(this.GetCooldown(0), -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(SkillDataBaseSwiftMoves.DODGE_CHANCE, false)), AM.csa(GameMath.GetPercentString(SkillDataBaseSwiftMoves.CRIT_CHANCE, false)), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(data.level))) + AM.csl(" (+" + GameMath.GetTimeInSecondsString(SkillDataBaseSwiftMoves.DURATION_LEVEL) + ")")) + AM.GetCooldownText(this.GetCooldown(data.level), SkillDataBaseSwiftMoves.LEVEL_COOLDOWN);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(SkillDataBaseSwiftMoves.DODGE_CHANCE, false)), AM.csa(GameMath.GetPercentString(SkillDataBaseSwiftMoves.CRIT_CHANCE, false)), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(data.level)))) + AM.GetCooldownText(this.GetCooldown(data.level), -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.liaAutoSkills[1], 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voLiaSkillAuto2, 1f));
		}

		public float GetDuration(int level)
		{
			return SkillDataBaseSwiftMoves.DURATION_INIT + (float)level * SkillDataBaseSwiftMoves.DURATION_LEVEL;
		}

		public static float COOLDOWN = 150f;

		public static float LEVEL_COOLDOWN = 10f;

		public static float DODGE_CHANCE = 0.8f;

		public static float CRIT_CHANCE = 0.3f;

		public static float DURATION_INIT = 8f;

		public static float DURATION_LEVEL = 1f;
	}
}
