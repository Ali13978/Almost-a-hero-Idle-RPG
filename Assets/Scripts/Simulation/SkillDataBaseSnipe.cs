using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseSnipe : SkillActiveDataBase
	{
		public SkillDataBaseSnipe()
		{
			this.nameKey = "SKILL_NAME_SNIPE";
			this.descKey = "SKILL_DESC_SNIPE";
			this.requiredHeroLevel = 1;
			this.maxLevel = 8;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.dur = 2.9f;
			data.durInvulnurability = data.dur;
			data.cooldownMax = SkillDataBaseSnipe.COOLDOWN;
			data.events = new List<SkillEvent>();
			SkillEventProjectile skillEventProjectile = new SkillEventProjectile();
			skillEventProjectile.damageType = DamageType.SKILL;
			data.events.Add(skillEventProjectile);
			skillEventProjectile.time = 2.36666656f;
			skillEventProjectile.canCrit = true;
			skillEventProjectile.projectileType = Projectile.Type.BLIND_ARCHER_AUTO;
			skillEventProjectile.targetType = Projectile.TargetType.SINGLE_ENEMY;
			skillEventProjectile.durFly = 0.32f;
			skillEventProjectile.path = new ProjectilePathBomb
			{
				heightAddMax = 0.15f
			};
			skillEventProjectile.damageInDps = this.GetDamage(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(0), false))) + AM.GetCooldownText(SkillDataBaseSnipe.COOLDOWN, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillDataBaseSnipe.DAMAGE_LEVEL, false) + ")")) + AM.GetCooldownText(SkillDataBaseSnipe.COOLDOWN, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false))) + AM.GetCooldownText(SkillDataBaseSnipe.COOLDOWN, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.liaAutoSkills[0], 1f));
			base.AddSoundEvent(world, by, new SoundDelayed(skillData.events[0].time, SoundArchieve.inst.liaUltiAttacks[GameMath.GetRandomInt(0, SoundArchieve.inst.liaUltiAttacks.Length, GameMath.RandType.NoSeed)], 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voLiaSkillAuto1, 1f));
		}

		public double GetDamage(int level)
		{
			return SkillDataBaseSnipe.DAMAGE_INIT + SkillDataBaseSnipe.DAMAGE_LEVEL * (double)level;
		}

		public static float COOLDOWN = 80f;

		public static double DAMAGE_INIT = 6.0;

		public static double DAMAGE_LEVEL = 3.0;
	}
}
