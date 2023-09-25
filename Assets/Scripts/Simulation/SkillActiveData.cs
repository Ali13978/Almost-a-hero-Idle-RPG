using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillActiveData : SkillData
	{
		public SkillActiveData(SkillActiveDataBase dataBase, List<SkillEnhancer> skillEnhancers, int level = 0)
		{
			this.dataBase = dataBase;
			this.level = level;
			dataBase.SetLevel(this, level, skillEnhancers);
		}

		public override void SetLevel(int level, List<SkillEnhancer> skillEnhancers)
		{
			this.level = level;
			this.dataBase.SetLevel(this, level, skillEnhancers);
		}

		protected override SkillTreeNode GetSkillDataBase()
		{
			return this.dataBase;
		}

		public void PlaySound(World world, Unit by)
		{
			this.dataBase.PlaySound(world, by, this);
		}

		public void StopSound(World world, Unit by)
		{
			this.dataBase.StopSound(world, by);
		}

		public SkillActiveDataBase dataBase;

		public bool isToggle;

		public float dur;

		public float durInvulnurability;

		public float cooldownMax;

		public List<SkillEvent> events;

		public List<SkillAnimEvent> animEvents;

		public SkillCondition liveCondition;

		public SkillCondition castCondition;
	}
}
