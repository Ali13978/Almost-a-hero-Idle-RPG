using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseJokesOnYou : SkillActiveDataBase
	{
		public SkillDataBaseJokesOnYou()
		{
			this.nameKey = "SKILL_NAME_JOKES_ON_YOU";
			this.descKey = "SKILL_DESC_JOKES_ON_YOU";
			this.requiredHeroLevel = 1;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.dur = 3.4f;
			data.durInvulnurability = 0f;
			data.cooldownMax = 60f;
			data.events = new List<SkillEvent>();
			SkillEventJokesOnYou skillEventJokesOnYou = new SkillEventJokesOnYou();
			data.events.Add(skillEventJokesOnYou);
			skillEventJokesOnYou.time = data.dur * 0.75f;
			skillEventJokesOnYou.projectileType = Projectile.Type.BOMBERMAN_FIRE_CRACKER;
			skillEventJokesOnYou.targetType = Projectile.TargetType.SINGLE_ALLY_ANY;
			skillEventJokesOnYou.damageType = DamageType.NONE;
			skillEventJokesOnYou.durFly = 0.8f;
			skillEventJokesOnYou.path = new ProjectilePathBomb
			{
				heightAddMax = 0.3f
			};
			skillEventJokesOnYou.damageInPer = 0.25;
			skillEventJokesOnYou.visualEffect = new VisualEffect(VisualEffect.Type.BOMBERMAN_DINAMIT, 0.5f);
			skillEventJokesOnYou.buffs = new List<BuffData>();
			BuffDataAttackSpeed buffDataAttackSpeed = new BuffDataAttackSpeed();
			buffDataAttackSpeed.id = 7;
			skillEventJokesOnYou.buffs.Add(buffDataAttackSpeed);
			buffDataAttackSpeed.visuals |= 1;
			buffDataAttackSpeed.dur = 20f;
			buffDataAttackSpeed.isStackable = true;
			buffDataAttackSpeed.attackSpeedAdd = this.GetSpeed((float)level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(0.25, false)), AM.csa(GameMath.GetPercentString(this.GetSpeed(0f), false)), AM.csa(GameMath.GetTimeInSecondsString(20f))) + AM.GetCooldownText(60f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(0.25, false)), AM.csa(GameMath.GetPercentString(this.GetSpeed((float)data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.1f, false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(20f))) + AM.GetCooldownText(60f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(0.25, false)), AM.csa(GameMath.GetPercentString(this.GetSpeed((float)data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(20f))) + AM.GetCooldownText(60f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundBombermanSkillAuto(1, skillData, 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voBombermanSkillAuto2, 1f));
		}

		public float GetSpeed(float level)
		{
			return 0.6f + level * 0.1f;
		}

		public const float COOLDOWN = 60f;

		public const double DAMAGE_PER = 0.25;

		public const float INIT_SPEED = 0.6f;

		public const float LEVEL_SPEED = 0.1f;

		public const float DURATION = 20f;
	}
}
