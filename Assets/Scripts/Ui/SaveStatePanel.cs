using System;
using SaveLoad;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class SaveStatePanel : MonoBehaviour
	{
		public void SetState(SaveData save)
		{
			this.qpText.text = GameMath.GetDoubleString((double)SaveLoadManager.GetTotalArtifactsLevel(save.artifacts, save.artifactList));
			this.gemText.text = GameMath.GetDoubleString(save.credits);
			this.maxStageText.text = save.worldsMaxStageReached["STANDARD"].ToString();
			this.playDurText.text = GameMath.GetTimeDatailedShortenedString(TimeSpan.FromSeconds((double)save.playerStatLifeTime));
		}

		public void SetState(Simulator sim)
		{
			this.qpText.text = sim.artifactsManager.TotalArtifactsLevel.ToString();
			this.gemText.text = sim.GetCredits().GetString();
			this.maxStageText.text = sim.GetStandardMaxStageReached().ToString();
			this.playDurText.text = GameMath.GetTimeDatailedShortenedString(TimeSpan.FromTicks(Main.localSaveDataLifetimeInTicks));
		}

		public Text qpText;

		public Text gemText;

		public Text maxStageText;

		public Text playDurText;

		public GameButton buttonSelect;
	}
}
