using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelHubTotems : MonoBehaviour
	{
		public void InitStrings()
		{
		}

		public ButtonSelectTotem[] buttonTotems;

		public PanelHeroesRunes panelRunes;

		public ScrollRect ringScroll;

		internal int selectedTotem;
	}
}
