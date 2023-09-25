using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseEatApple : SkillActiveDataBase
	{
		public SkillDataBaseEatApple()
		{
			this.nameKey = "SKILL_NAME_EAT_AN_APPLE";
			this.descKey = "SKILL_DESC_EAT_AN_APPLE";
			this.requiredHeroLevel = 1;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			float num = 0f;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerBaseBigStomach)
				{
					num += (skillEnhancer.enhancerBase as SkillEnhancerBaseBigStomach).GetDuration(skillEnhancer.level);
				}
			}
			data.dur = 2f;
			data.durInvulnurability = 0f;
			data.cooldownMax = this.GetCooldown(level);
			BuffDataHealthRegen buffDataHealthRegen = new BuffDataHealthRegen();
			buffDataHealthRegen.id = 99;
			buffDataHealthRegen.isStackable = true;
			buffDataHealthRegen.dur = 12f + num;
			buffDataHealthRegen.healthRegenAdd = this.GetTotalHealthRestored(level) / (double)buffDataHealthRegen.dur;
			buffDataHealthRegen.visuals |= 68;
			buffDataHealthRegen.tag = BuffTags.LENNY_HEAL;
			BuffDataCritChance buffDataCritChance = new BuffDataCritChance();
			buffDataCritChance.id = 34;
			buffDataCritChance.isStackable = true;
			buffDataCritChance.dur = 12f + num;
			buffDataCritChance.critChanceAdd = 0.4f;
			data.events = new List<SkillEvent>();
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf);
			skillEventBuffSelf.time = data.dur * 0.5f;
			skillEventBuffSelf.buff = buffDataHealthRegen;
			SkillEventBuffSelf skillEventBuffSelf2 = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf2);
			skillEventBuffSelf2.time = skillEventBuffSelf.time;
			skillEventBuffSelf2.buff = buffDataCritChance;
			SkillEventProjectile skillEventProjectile = new SkillEventProjectile();
			data.events.Add(skillEventProjectile);
			skillEventProjectile.time = data.dur * 0.8f;
			skillEventProjectile.projectileType = Projectile.Type.APPLE_AID;
			skillEventProjectile.targetType = Projectile.TargetType.SINGLE_ALLY_MIN_HEALTH;
			skillEventProjectile.durFly = 0.5f;
			skillEventProjectile.path = new ProjectilePathBomb
			{
				heightAddMax = 0.3f
			};
			skillEventProjectile.damageInDps = 0.0;
			skillEventProjectile.damageInTeamDps = 0.0;
			skillEventProjectile.buffs = new List<BuffData>
			{
				buffDataHealthRegen,
				buffDataCritChance
			};
		}

		private float GetCooldown(int level)
		{
			return 130f - (float)level * 10f;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetTotalHealthRestored(0), false)), AM.csa(GameMath.GetPercentString(0.4f, false)), AM.csa(GameMath.GetTimeInSecondsString(12f))) + AM.GetCooldownText(this.GetCooldown(0), -1f);
		}

		public override string GetDesc(SkillData data)
		{
			float dur = (((data as SkillActiveData).events[0] as SkillEventBuffSelf).buff as BuffDataHealthRegen).dur;
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetTotalHealthRestored(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.14, false) + ")"), AM.csa(GameMath.GetPercentString(0.4f, false)), AM.csa(GameMath.GetTimeInSecondsString(dur))) + AM.GetCooldownText(this.GetCooldown(data.level), 10f);
		}

		public override string GetDescFull(SkillData data)
		{
			float dur = (((data as SkillActiveData).events[0] as SkillEventBuffSelf).buff as BuffDataHealthRegen).dur;
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetTotalHealthRestored(data.level), false)), AM.csa(GameMath.GetPercentString(0.4f, false)), AM.csa(GameMath.GetTimeInSecondsString(dur))) + AM.GetCooldownText(this.GetCooldown(data.level), -1f);
		}

		public double GetTotalHealthRestored(int level)
		{
			return 0.3 + (double)level * 0.14;
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundDelayed(0.7f, SoundArchieve.inst.lennyAutoSkills[0], 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voLennySkillAuto1, 1f));
		}

		private const float COOLDOWN = 130f;

		private const float LEVEL_COOLDOWN = 10f;

		private const float INITIAL_DURATION = 12f;

		private const float INITIAL_CRIT_CHANCE = 0.4f;

		private const double INITIAL_HEALTH_RESTORED = 0.3;

		private const double LEVEL_HEALTH_RESTORED = 0.14;
	}
}
