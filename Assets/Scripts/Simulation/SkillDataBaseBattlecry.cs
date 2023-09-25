using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseBattlecry : SkillActiveDataBase
	{
		public SkillDataBaseBattlecry()
		{
			this.nameKey = "SKILL_NAME_BATTLECRY";
			this.descKey = "SKILL_DESC_BATTLECRY";
			this.timeFadeInStart = 0.1f;
			this.timeFadeInEnd = 0.3f;
			this.timeFadeOutStart = 1.2f;
			this.timeFadeOutEnd = 1.4000001f;
			this.requiredHeroLevel = 1;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			int num = -1;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerBaseHeroism)
				{
					num = skillEnhancer.level;
				}
			}
			data.durInvulnurability = 3f;
			data.dur = 4.33333349f;
			data.cooldownMax = 90f;
			float dur = 15f;
			SkillEventBuffAllAllies skillEventBuffAllAllies = new SkillEventBuffAllAllies();
			SkillEventBuffAllAllies skillEventBuffAllAllies2 = new SkillEventBuffAllAllies();
			data.events = new List<SkillEvent>
			{
				skillEventBuffAllAllies,
				skillEventBuffAllAllies2
			};
			skillEventBuffAllAllies.time = 1.2f;
			skillEventBuffAllAllies2.time = 1.2f;
			BuffDataDamageAdd buffDataDamageAdd = new BuffDataDamageAdd();
			buffDataDamageAdd.id = 37;
			skillEventBuffAllAllies.buff = buffDataDamageAdd;
			buffDataDamageAdd.isPermenant = false;
			buffDataDamageAdd.isStackable = true;
			buffDataDamageAdd.dur = dur;
			buffDataDamageAdd.damageAdd = this.GetDamageAdd(level);
			buffDataDamageAdd.visuals |= 8;
			BuffDataAttackSpeed buffDataAttackSpeed = new BuffDataAttackSpeed();
			buffDataAttackSpeed.id = 5;
			skillEventBuffAllAllies2.buff = buffDataAttackSpeed;
			buffDataAttackSpeed.isPermenant = false;
			buffDataAttackSpeed.isStackable = true;
			buffDataAttackSpeed.dur = dur;
			buffDataAttackSpeed.attackSpeedAdd = this.GetSpeedAdd(level);
			buffDataAttackSpeed.visuals |= 1;
			if (num >= 0)
			{
				this.AddCrit(data, SkillEnhancerBaseHeroism.GetBonus(num), dur);
			}
		}

		private double GetDamageAdd(int level)
		{
			return (double)(0.4f + (float)level * 0.1f);
		}

		private float GetSpeedAdd(int level)
		{
			return 0.2f + (float)level * 0.05f;
		}

		private void AddCrit(SkillActiveData data, double amount, float dur)
		{
			SkillEventBuffAllAllies skillEventBuffAllAllies = new SkillEventBuffAllAllies();
			data.events.Add(skillEventBuffAllAllies);
			skillEventBuffAllAllies.time = 1.2f;
			BuffDataCritFactor buffDataCritFactor = new BuffDataCritFactor();
			buffDataCritFactor.id = 38;
			skillEventBuffAllAllies.buff = buffDataCritFactor;
			buffDataCritFactor.isPermenant = false;
			buffDataCritFactor.isStackable = true;
			buffDataCritFactor.dur = dur;
			buffDataCritFactor.critFactorAdd = amount;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageAdd(0), false)), AM.csa(GameMath.GetPercentString(this.GetSpeedAdd(0), false)), AM.csa(GameMath.GetTimeInSecondsString(15f))) + AM.GetCooldownText(90f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageAdd(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.1f, false) + ")"), AM.csa(GameMath.GetPercentString(this.GetSpeedAdd(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.05f, false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(15f))) + AM.GetCooldownText(90f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageAdd(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetSpeedAdd(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(15f))) + AM.GetCooldownText(90f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voJimSkillBattlecry, 1f));
			int randomInt = GameMath.GetRandomInt(0, SoundArchieve.inst.jimBattleCry.Length, GameMath.RandType.NoSeed);
			base.AddSoundEvent(world, by, new SoundDelayed(0.8f, SoundArchieve.inst.jimBattleCry[randomInt], 1f));
		}

		private const float COOLDOWN = 90f;

		private const float DURATION = 15f;

		private const float DUR_BEGIN = 1.2f;

		private const float DUR_END = 3.13333344f;

		private const float DAMAGE_ADD_BASE = 0.4f;

		private const float DAMAGE_ADD_LEVEL = 0.1f;

		private const float SPEED_ADD_BASE = 0.2f;

		private const float SPEED_ADD_LEVEL = 0.05f;
	}
}
