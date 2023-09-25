using System;

namespace Simulation
{
	public class MerchantItemTrainingBook : MerchantItem
	{
		public override string GetId()
		{
			return "TRAINING_BOOK";
		}

		public override string GetTitleString()
		{
			return LM.Get("MERCHANT_ITEM_TRAININGBOOK");
		}

		public override string GetDescriptionString(Simulator sim, int count)
		{
			return string.Format(LM.Get("MERCHANT_ITEM_DESC_TRAININGBOOK"), 5 * count);
		}

		public override void Apply(Simulator sim)
		{
			base.Apply(sim);
			World activeWorld = sim.GetActiveWorld();
			foreach (Hero hero in activeWorld.heroes)
			{
				hero.IncrementNumUnspentSkillPoints(5);
			}
		}

		private const int NUM_SKILL_POINTS = 5;

		public static int BASE_COUNT = 1;

		public static int BASE_COST = 10;
	}
}
