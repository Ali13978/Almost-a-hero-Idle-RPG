using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseLobMagic : SkillActiveDataBase
	{
		public SkillDataBaseLobMagic()
		{
			this.nameKey = "SKILL_NAME_LOB_THE_MAGIC";
			this.descKey = "SKILL_DESC_LOB_THE_MAGIC";
			this.requiredHeroLevel = 0;
			this.maxLevel = 4;
			this.timeFadeInStart = 0.5f;
			this.timeFadeInEnd = 0.7f;
			this.timeFadeOutStart = 2.5333333f;
			this.timeFadeOutEnd = 2.73333335f;
			this.durNonslowable = 2.5333333f;
			this.durStayInFrontCurtain = 2.5333333f;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			double num = 0.0;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerBaseTricks)
				{
					num += AM.LinearEquationDouble(SkillEnhancerBaseTricks.LEVEL_BONUS, (double)skillEnhancer.level, SkillEnhancerBaseTricks.INITIAL_BONUS);
				}
			}
			data.dur = 3.83333325f;
			data.durInvulnurability = data.dur;
			data.cooldownMax = 210f;
			data.events = new List<SkillEvent>();
			SkillEventProjectile skillEventProjectile = new SkillEventProjectile();
			data.events.Add(skillEventProjectile);
			skillEventProjectile.damageType = DamageType.SKILL;
			skillEventProjectile.canCrit = true;
			skillEventProjectile.time = 2.5333333f;
			skillEventProjectile.projectileType = Projectile.Type.DEREK_BOOK;
			skillEventProjectile.targetType = Projectile.TargetType.ALL_ENEMIES;
			skillEventProjectile.durFly = 0.32f;
			skillEventProjectile.path = new ProjectilePathLinear();
			skillEventProjectile.damageInTeamDps = this.GetDamage(level) * (1.0 + num);
			skillEventProjectile.visualEffect = new VisualEffect(VisualEffect.Type.DEREK_BOOK, 0.5f);
			BuffDataStun buffDataStun = new BuffDataStun();
			buffDataStun.id = 169;
			buffDataStun.visuals |= 512;
			skillEventProjectile.buffs = new List<BuffData>
			{
				buffDataStun
			};
			buffDataStun.dur = 5f;
		}

		public override string GetDescZero()
		{
			double damage = this.GetDamage(0);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(damage, false)), AM.csa(GameMath.GetTimeInSecondsString(5f))) + AM.GetCooldownText(210f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			double damageInTeamDps = ((data as SkillActiveData).events[0] as SkillEventProjectile).damageInTeamDps;
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(damageInTeamDps, false)) + AM.csl(" (+" + GameMath.GetPercentString(2.0, false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(5f))) + AM.GetCooldownText(210f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			double damageInTeamDps = ((data as SkillActiveData).events[0] as SkillEventProjectile).damageInTeamDps;
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(damageInTeamDps, false)), AM.csa(GameMath.GetTimeInSecondsString(5f))) + AM.GetCooldownText(210f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.derekUlti, 1f));
			SkillEventProjectile skillEventProjectile = (SkillEventProjectile)skillData.events[0];
			base.AddSoundEvent(world, by, new SoundDelayed(skillEventProjectile.time, SoundArchieve.inst.derekThrowBook, 1f));
			base.AddSoundEvent(world, by, new SoundDelayed(skillEventProjectile.time + skillEventProjectile.durFly + 0.3f, SoundArchieve.inst.derekBookExplosion, 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voDerekUlti, 1f));
		}

		public double GetDamage(int level)
		{
			return 4.0 + (double)level * 2.0;
		}

		private const float COOLDOWN = 210f;

		private const double DAMAGE_INIT = 4.0;

		private const double DAMAGE_LEVEL = 2.0;

		private const float STUN_DURATION = 5f;
	}
}
