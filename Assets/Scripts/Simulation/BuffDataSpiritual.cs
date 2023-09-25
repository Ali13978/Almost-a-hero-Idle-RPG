using System;

namespace Simulation
{
	public class BuffDataSpiritual : BuffData
	{
		public BuffDataSpiritual()
		{
			this.id = 164;
		}

		public override void OnCastSpellAlly(Buff buff, Unit ally, Skill skill)
		{
			(buff.GetBy() as TotemEarth).RechargeMeteorShower();
		}

		private bool spellCasted;
	}
}
