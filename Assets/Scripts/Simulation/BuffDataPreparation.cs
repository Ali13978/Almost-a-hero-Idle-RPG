using System;

namespace Simulation
{
	public class BuffDataPreparation : BuffData
	{
		public BuffDataPreparation(float duration, double damageTakenFactor)
		{
			this.damageTakenFactor = damageTakenFactor;
			this.effect = new BuffDataDefense();
			this.effect.id = 60;
			this.effect.damageTakenFactor = this.damageTakenFactor;
			this.effect.dur = duration;
			this.effect.lifeCounter = 1;
		}

		public override void OnNewEnemies(Buff buff)
		{
			if (buff.GetWorld().activeChallenge is ChallengeStandard)
			{
				if (ChallengeStandard.IsBossWave(buff.GetWorld().activeChallenge.GetTotWave()))
				{
					foreach (Hero hero in buff.GetWorld().heroes)
					{
						hero.AddBuff(this.effect, 0, false);
					}
					this.addedBuff = true;
				}
				else if (this.addedBuff)
				{
					foreach (Hero hero2 in buff.GetWorld().heroes)
					{
						foreach (Buff buff2 in hero2.buffs)
						{
							if (buff2.HasSameId(this.effect))
							{
								buff2.DecreaseLifeCounter();
								break;
							}
						}
					}
					this.addedBuff = false;
				}
			}
		}

		private double damageTakenFactor;

		private bool addedBuff;

		private BuffDataDefense effect;
	}
}
