using System;
using UnityEngine;

[Serializable]
public class GK_AchievementTemplate
{
	public float Progress
	{
		get
		{
			if (IOSNativeSettings.Instance.UsePPForAchievements)
			{
				return GameCenterManager.GetAchievementProgress(this.Id);
			}
			return this._progress;
		}
		set
		{
			this._progress = value;
		}
	}

	public bool IsOpen = true;

	public string Id = string.Empty;

	public string Title = "New Achievement";

	public string Description = string.Empty;

	public float _progress;

	public Texture2D Texture;
}
