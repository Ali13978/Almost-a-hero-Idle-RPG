using System;
using System.Collections.Generic;

namespace Simulation
{
	public class CharmBuffLucrativeLightning : CharmBuff
	{
		protected override bool TryActivating()
		{
			this.timer = this.intervalBetweenAttacks;
			return true;
		}

		public override void Update(float dt)
		{
			base.Update(dt);
			if (this.state == EnchantmentBuffState.ACTIVE && this.stateTime < this.durActive)
			{
				this.timer += dt;
				if (this.timer > this.intervalBetweenAttacks)
				{
					double heroTeamDps = this.world.GetHeroTeamDps();
					List<Enemy> list = this.world.activeChallenge.enemies.FindAll((Enemy e) => e.IsAttackable());
					if (list.Count > 0)
					{
						Enemy randomListElement = list.GetRandomListElement<Enemy>();
						Damage damage = new Damage(this.damageMul * heroTeamDps, false, false, false, false);
						this.world.DamageMain(null, randomListElement, damage);
						VisualEffect visualEffect = new VisualEffect(VisualEffect.Type.TOTEM_THUNDERBOLT, 0.5f);
						visualEffect.pos = randomListElement.pos;
						this.world.visualEffects.Add(visualEffect);
						BuffDataDropGold buffData = new BuffDataDropGold
						{
							dropGoldFactorAdd = this.goldIncrease,
							isPermenant = true,
							isStackable = true,
							id = 315,
							dur = float.MaxValue
						};
						randomListElement.AddBuff(buffData, 0, false);
						this.world.AddSoundEvent(new SoundEventSound(SoundType.GAMEPLAY, "charm", false, 0f, new SoundSimple(SoundArchieve.inst.thunderbolts[0], 1f, float.MaxValue)));
					}
					this.timer = 0f;
				}
			}
		}

		public override void OnWeaponUsed(Hero hero)
		{
			this.AddProgress(this.pic);
		}

		public double damageMul;

		public double goldIncrease;

		private float intervalBetweenAttacks = 0.5f;

		private float timer;
	}
}
