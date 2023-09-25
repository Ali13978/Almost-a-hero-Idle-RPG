using System;
using Ui;

namespace Simulation
{
	public class MerchantItemOrnamentDrop : MerchantItem
	{
		public MerchantItemOrnamentDrop()
		{
			this.level = 0;
		}

		public override string GetId()
		{
			return "ORNAMENT_DROP";
		}

		public override string GetTitleString()
		{
			return LM.Get("MERCHANT_ITEM_ORNAMENT_DROP_NAME");
		}

		public override string GetDescriptionString(Simulator sim, int count)
		{
			return string.Format(LM.Get("MERCHANT_ITEM_ORNAMENT_DROP_DESC"), new object[]
			{
				AM.csm(GameMath.GetTimeInSecondsString(1f)),
				AM.csm(10.ToString()),
				AM.csm(GameMath.GetPercentString(2.5f, false)),
				AM.csm(GameMath.GetTimeDetailedString((double)(600f * (float)count), true))
			});
		}

		public override void Apply(Simulator sim)
		{
			base.Apply(sim);
			sim.GetActiveWorld().StartOrnamentDrop(600f, 2.5f, 1f, 10);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.merchantItemPowerUp, 1f));
		}

		public float GetDuration(Simulator sim)
		{
			return 600f + sim.GetActiveWorld().universalBonus.autoTapDurationAdd;
		}

		public const float DUR = 600f;

		public const float TIME_TARGET = 1f;

		public const int PROJECTILES_COUNT = 10;

		public const float TEAM_DAMAGE_FACTOR = 2.5f;
	}
}
