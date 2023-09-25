using System;

namespace Simulation
{
	public class BuffDataHealAlliesWhenHit : BuffData
	{
		public override void OnActionBuffEventTriggered(Buff buff, Unit by, BuffEventAction buffEventAction)
		{
			if (buffEventAction.id == BuffEventAction.ID_BABU_ULTI_FINISH)
			{
				this.HealAllies(buff);
			}
		}

		public override void OnDeathSelf(Buff buff)
		{
			buff.GetWorld().AddSoundEvent(new SoundEventCancelBy(buff.GetBy().GetId()));
			if (this.onDeathSound != null)
			{
				buff.GetWorld().AddSoundEvent(new SoundEventSound(SoundType.GAMEPLAY, buff.GetBy().GetId(), false, 0f, this.onDeathSound));
			}
			this.HealAllies(buff);
		}

		public override void OnTakenDamage(Buff buff, Unit attacker, Damage damage)
		{
			if (attacker == null || !(buff.GetBy() as UnitHealthy).IsAlly(attacker))
			{
				buff.IncreaseGenericCounter();
			}
		}

		private void HealAllies(Buff buff)
		{
			Unit by = buff.GetBy();
			double num = this.healRatio * (double)buff.GetGenericCounter();
			foreach (UnitHealthy unitHealthy in by.GetAllies())
			{
				if (unitHealthy != by && unitHealthy.IsAlive())
				{
					unitHealthy.Heal(num);
					if (by.GetId() == "BABU")
					{
						by.world.OnBabuHealAlly();
					}
					unitHealthy.AddVisualBuff(1f, 64);
				}
			}
		}

		public double healRatio;

		public Sound onDeathSound;
	}
}
