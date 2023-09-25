using System;
using UnityEngine.UI;

namespace Ui
{
	public class PanelGlobalBonus : AahMonoBehaviour
	{
		public void SetDamageHero(double mult)
		{
			this.SetColumn(0, mult);
		}

		public void SetDamageRing(double mult)
		{
			this.SetColumn(1, mult);
		}

		public void SetGold(double mult)
		{
			this.SetColumn(2, mult);
		}

		public void SetHealth(double mult)
		{
			this.SetColumn(3, mult);
		}

		private void SetColumn(int index, double mult)
		{
			this.textPercents[index].text = "+" + GameMath.GetPercentString(mult, true);
		}

		public void InitStrings()
		{
			this.textTitles[0].text = LM.Get("UI_HUB_HERO_DMG");
			this.textTitles[1].text = LM.Get("UI_HUB_RING_DMG");
			this.textTitles[2].text = LM.Get("UI_HUB_GOLD");
			this.textTitles[3].text = LM.Get("UI_HUB_HERO_HP");
		}

		public Text[] textTitles;

		public Text[] textPercents;

		public Image[] imageBarArtifacts;

		public Image[] imageBarGears;

		public Image[] imageBarMiddles;

		public Image nonEffectingBonusIcon;

		public Image ornament;
	}
}
