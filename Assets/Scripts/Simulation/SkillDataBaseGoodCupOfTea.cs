using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseGoodCupOfTea : SkillActiveDataBase
	{
		public SkillDataBaseGoodCupOfTea()
		{
			this.nameKey = "SKILL_NAME_GOOD_CUP_OF_TEA";
			this.descKey = "SKILL_DESC_GOOD_CUP_OF_TEA";
			this.requiredHeroLevel = 1;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.dur = 3.13333344f;
			data.durInvulnurability = 0f;
			data.cooldownMax = 120f;
			data.events = new List<SkillEvent>();
			BuffDataDodge item = new BuffDataDodge
			{
				dur = this.GetDur(level),
				dodgeChanceAdd = this.GetDodgeChance(level),
				id = 323,
				isStackable = true,
				visuals = 16384
			};
			BuffDataAttackSpeed item2 = new BuffDataAttackSpeed
			{
				dur = this.GetDur(level),
				attackSpeedAdd = this.GetAttackSpeed(level),
				id = 323,
				isStackable = true,
				visuals = 1
			};
			SkillEventProjectile skillEventProjectile = new SkillEventProjectile();
			data.events.Add(skillEventProjectile);
			skillEventProjectile.time = 2.1f;
			skillEventProjectile.projectileType = Projectile.Type.BABU_TEA_CUP;
			skillEventProjectile.targetType = Projectile.TargetType.SINGLE_ALLY_ANY;
			skillEventProjectile.durFly = 0.5f;
			skillEventProjectile.path = new ProjectilePathBomb
			{
				heightAddMax = 0.3f
			};
			skillEventProjectile.damageInDps = 0.0;
			skillEventProjectile.damageInTeamDps = 0.0;
			skillEventProjectile.buffs = new List<BuffData>
			{
				item,
				item2
			};
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDodgeChance(0), false)), AM.csa(GameMath.GetPercentString(this.GetAttackSpeed(0), false)), AM.csa(GameMath.GetTimeInSecondsString(this.GetDur(0)))) + AM.GetCooldownText(120f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDodgeChance(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.06f, false) + ")"), AM.csa(GameMath.GetPercentString(this.GetAttackSpeed(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.1f, false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(this.GetDur(data.level)))) + AM.GetCooldownText(120f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDodgeChance(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetAttackSpeed(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(this.GetDur(data.level)))) + AM.GetCooldownText(120f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.babuAutoSkills[0], 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voBabuGoodCupOfTea, 1f));
		}

		public float GetDur(int level)
		{
			return 20f;
		}

		public float GetDodgeChance(int level)
		{
			return 0.4f + 0.06f * (float)level;
		}

		public float GetAttackSpeed(int level)
		{
			return 0.5f + 0.1f * (float)level;
		}

		public const float COOLDOWN = 120f;

		public const float BUFF_DUR = 20f;

		public const float DODGE_RATIO_INIT = 0.4f;

		public const float DODGE_RATIO_PER_LEVEL = 0.06f;

		public const float ATTACK_SPEED_RATIO_INIT = 0.5f;

		public const float ATTACK_SPEED_RATIO_PER_LEVEL = 0.1f;

		public const float DUR_ANIM = 3.13333344f;

		public const float BUFF_TIME = 2.1f;
	}
}
