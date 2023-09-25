using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseFireworks : SkillActiveDataBase
	{
		public SkillDataBaseFireworks()
		{
			this.nameKey = "SKILL_NAME_FIREWORKS";
			this.descKey = "SKILL_DESC_FIREWORKS";
			this.requiredHeroLevel = 1;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			int num = 0;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerBaseMadness)
				{
					num += AM.LinearEquationInteger(skillEnhancer.level, SkillEnhancerBaseMadness.LEVEL_NUM_FIRES, SkillEnhancerBaseMadness.INITIAL_NUM_FIRES);
				}
			}
			int num2 = 3 + num;
			data.dur = 4.2f;
			data.durInvulnurability = 0f;
			data.cooldownMax = 120f;
			data.events = new List<SkillEvent>();
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf);
			skillEventBuffSelf.time = 2.5f;
			BuffData buffData = new BuffData
			{
				doNotRemoveOnRefresh = true
			};
			skillEventBuffSelf.buff = buffData;
			skillEventBuffSelf.buff.isPermenant = true;
			buffData.dur = 5f - skillEventBuffSelf.time + (float)num2 * 1f;
			buffData.events = new List<BuffEvent>();
			for (int i = 0; i < num2; i++)
			{
				BuffEventProjectile buffEventProjectile = new BuffEventProjectile();
				buffEventProjectile.canCrit = true;
				buffData.events.Add(buffEventProjectile);
				buffEventProjectile.time = 5f - skillEventBuffSelf.time + (float)i * 1f;
				buffEventProjectile.projectileType = Projectile.Type.BOMBERMAN_FIREWORK;
				buffEventProjectile.targetType = Projectile.TargetType.ALL_ENEMIES;
				buffEventProjectile.durFly = 1f;
				buffEventProjectile.damageTimeRatio = 0.65f;
				buffEventProjectile.path = new ProjectilePathFromAbove
				{
					speedVertical = 5f
				};
				buffEventProjectile.damageInTeamDps = this.GetDamage(level);
				buffEventProjectile.damageType = DamageType.SKILL;
			}
		}

		public override string GetDescZero()
		{
			double damage = this.GetDamage(0);
			int num = 3;
			return string.Format(LM.Get(this.descKey), AM.csa(num.ToString()), AM.csa(GameMath.GetPercentString(damage, false))) + AM.GetCooldownText(120f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			double damage = this.GetDamage(data.level);
			SkillActiveData skillActiveData = (SkillActiveData)data;
			SkillEventBuffSelf skillEventBuffSelf = (SkillEventBuffSelf)skillActiveData.events[0];
			int count = skillEventBuffSelf.buff.events.Count;
			return string.Format(LM.Get(this.descKey), AM.csa(count.ToString()), AM.csa(GameMath.GetPercentString(damage, false)) + AM.csl(" (+" + GameMath.GetPercentString(0.25, false) + ")")) + AM.GetCooldownText(120f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			double damage = this.GetDamage(data.level);
			SkillActiveData skillActiveData = (SkillActiveData)data;
			SkillEventBuffSelf skillEventBuffSelf = (SkillEventBuffSelf)skillActiveData.events[0];
			int count = skillEventBuffSelf.buff.events.Count;
			return string.Format(LM.Get(this.descKey), AM.csa(count.ToString()), AM.csa(GameMath.GetPercentString(damage, false))) + AM.GetCooldownText(120f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundBombermanSkillAuto(0, skillData, 0.65f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voBombermanSkillAuto1, 1f));
		}

		public double GetDamage(int level)
		{
			return 0.75 + 0.25 * (double)level;
		}

		private const float COOLDOWN = 120f;

		private const float TIME_FIRST_FIRE = 5f;

		private const float TIME_BETWEEN_FIRES = 1f;

		private const int INITIAL_NUM_FIRES = 3;

		private const double INITIAL_DAMAGE = 0.75;

		private const double LEVEL_DAMAGE = 0.25;
	}
}
