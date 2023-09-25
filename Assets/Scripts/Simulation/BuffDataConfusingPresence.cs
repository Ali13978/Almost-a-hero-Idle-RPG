using System;

namespace Simulation
{
	public class BuffDataConfusingPresence : BuffData
	{
		public override void OnNewWave(Buff buff)
		{
			if (!GameMath.GetProbabilityOutcome(this.chance, GameMath.RandType.NoSeed))
			{
				return;
			}
			this.effect = new BuffDataAttackSpeed();
			this.effect.isPermenant = false;
			this.effect.isStackable = true;
			this.effect.dur = this.duration;
			this.effect.attackSpeedAdd = this.slow * -1f;
			this.effect.visuals |= 2;
			foreach (Enemy enemy in buff.GetWorld().activeChallenge.enemies)
			{
				enemy.AddBuff(this.effect, 0, false);
			}
		}

		public float chance;

		public float slow;

		public float duration;

		private BuffDataAttackSpeed effect;
	}
}
