using System;

namespace Simulation
{
	public class BuffDataTrinketCastSpell : BuffData
	{
		public override void OnCastSpellSelf(Buff buff, Skill skill)
		{
			Hero hero = buff.GetBy() as Hero;
			Trinket trinket = hero.trinket;
			trinket.req.IncreaseProgress(1);
		}
	}
}
