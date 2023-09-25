using System;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ServerRewardSkin : MonoBehaviour
	{
		public void Init(Simulator simulator, UiManager uiManager, int id)
		{
			SkinData skinData = simulator.GetSkinData(id);
			this.heroAvatar.sprite = uiManager.GetHeroAvatar(skinData);
		}

		[SerializeField]
		private Image heroAvatar;
	}
}
