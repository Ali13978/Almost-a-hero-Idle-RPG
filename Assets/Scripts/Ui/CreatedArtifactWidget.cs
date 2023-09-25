using System;
using Simulation;
using Simulation.ArtifactSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class CreatedArtifactWidget : MonoBehaviour
	{
		public void InitStrings()
		{
			this.textUniqueEffectHeader.text = "UI_UNIQUE_STAT".Loc();
		}

		public void SetArtifact(Simulation.ArtifactSystem.Artifact artifact, Simulator sim)
		{
			this.buttonArtifact.SetButton(ButtonArtifact.State.FULL, 0, 0);
			Simulation.ArtifactSystem.ArtifactEffect artifactEffect = EffectsDatabase.Common[artifact.CommonEffectId];
			EffectsDatabase.UniqueEffectInfo uniqueEffectInfo = EffectsDatabase.Unique[artifact.UniqueEffectsIds[0]];
			this.textCommonEffect.text = artifactEffect.GetDescriptionWithValue(artifact.Level, sim.GetUniversalBonusAll());
			this.textUniqueEffect.text = uniqueEffectInfo.Effect.GetDescriptionWithValue(artifact.Level, sim.GetUniversalBonusAll());
		}

		public ButtonArtifact buttonArtifact;

		public Text textCommonEffect;

		public Text textUniqueEffect;

		public Text textUniqueEffectHeader;
	}
}
