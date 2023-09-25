using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseCommonAffinities : SkillActiveDataBase
	{
		public SkillDataBaseCommonAffinities()
		{
			this.nameKey = "SKILL_NAME_COMMON_AFFINITIES";
			this.descKey = "SKILL_DESC_COMMON_AFFINITIES";
			this.requiredHeroLevel = 0;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			int numDragons = this.GetNumDragons(level);
			data.dur = 4.33f;
			data.durInvulnurability = 4.33f;
			data.cooldownMax = 300f;
			data.events = new List<SkillEvent>();
			Dictionary<CurrencyType, double> dictionary = new Dictionary<CurrencyType, double>();
			dictionary.Add(CurrencyType.GOLD, this.GetGold(level));
			List<float> list = new List<float>();
			for (int i = 0; i < numDragons; i++)
			{
				list.Add(-0.12f + (float)i * 0.04f);
			}
			list.Shuffle<float>();
			for (int j = 0; j < numDragons; j++)
			{
				SkillEventCurrencyDragon item = new SkillEventCurrencyDragon
				{
					allowedCurrencyTypes = dictionary,
					time = 3f,
					initialX = (float)(-(float)j) * 0.2f,
					yOffset = list[j]
				};
				data.events.Add(item);
			}
		}

		public double GetGold(int level)
		{
			return 2.0 + (double)level * 0.0;
		}

		public override string GetDescZero()
		{
			int numDragons = this.GetNumDragons(0);
			return string.Format(LM.Get(this.descKey), AM.csa(numDragons.ToString())) + AM.GetCooldownText(300f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			int numDragons = this.GetNumDragons(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(numDragons.ToString()) + AM.csl(" (+" + 1.ToString() + ")")) + AM.GetCooldownText(300f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			int numDragons = this.GetNumDragons(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(numDragons.ToString())) + AM.GetCooldownText(300f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.goblinAutoSkills[1], 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voGoblinAffinities, 1f));
		}

		public int GetNumDragons(int level)
		{
			return 2 + level;
		}

		private const double INITIAL_GOLD_FACTOR = 2.0;

		private const double LEVEL_GOLD_FACTOR = 0.0;

		private const double TOKEN_FACTOR = 0.20000000298023224;

		private const float COOLDOWN = 300f;

		private const float FIRST_DRAGON_TIME = 3f;

		private const float GAP_BETWEEN_DRAGONS = 0.2f;

		private const int INITIAL_NUM_DRAGONS = 2;

		private const int LEVEL_NUM_DRAGONS = 1;
	}
}
