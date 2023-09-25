using System;

namespace Simulation
{
	public class SkillEventHealAllAllies : SkillEvent
	{
		public override void Apply(Unit by)
		{
			foreach (UnitHealthy unitHealthy in by.GetAllies())
			{
				if (unitHealthy.IsAlive() && (unitHealthy != by || this.applySelf))
				{
					unitHealthy.Heal(this.healRatio);
					if (by.GetId() == "BABU")
					{
						by.world.OnBabuHealAlly();
					}
					unitHealthy.AddVisualBuff(1f, 64);
				}
			}
		}

		public override void Cancel(Unit by, float timePassedSinceActivation)
		{
		}

		public double healRatio;

		public bool applySelf;
	}
}
