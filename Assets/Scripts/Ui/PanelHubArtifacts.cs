using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelHubArtifacts : MonoBehaviour
	{
		public void InitStrings()
		{
			this.header.text = LM.Get("UI_TAB_ARTIFACTS");
		}

		public GameButton buttonBack;

		public Text header;

		public PanelArtifactScroller artifactScroller;

		public MenuShowCurrency menuShowCurrencyCredits;

		public MenuShowCurrency menuShowCurrencyMythstone;
	}
}
