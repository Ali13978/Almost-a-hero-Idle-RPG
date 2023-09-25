using System;

namespace Simulation
{
	public class SkillEventShieldAll : SkillEvent
	{
		public override void Apply(Unit by)
		{
			BuffData buffData = new BuffData();
			buffData.dur = 3f;
			buffData.id = 272;
			buffData.visuals |= 256;
			foreach (UnitHealthy unitHealthy in by.GetAllies())
			{
				unitHealthy.GainShield(this.ratio, this.dur);
				unitHealthy.AddBuff(buffData, 0, false);
				if (unitHealthy.IsAlive())
				{
					by.world.OnSamShield();
				}
			}
		}

		public override void Cancel(Unit by, float timePassedSinceActivation)
		{
		}

		public double ratio;

		public float dur;
	}
}
