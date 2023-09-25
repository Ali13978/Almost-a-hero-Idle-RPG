using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ArtifactStone : AahMonoBehaviour
	{
		public void PlaySpineAnim(int level)
		{
			if (level + 1 != this.levelOld)
			{
				this.levelOld = level + 1;
				this.imageStone.sprite = this.spriteLevels[level];
				this.imageStone.SetNativeSize();
				this.imageIcon.rectTransform.SetAnchorPosY(ArtifactStone.IconPosPerLevel[level]);
			}
		}

		public void SetAlpha(float alpha)
		{
			this.canvasGroup.alpha = alpha;
		}

		public CanvasGroup canvasGroup;

		public RectTransform rectTransform;

		public Image imageStone;

		public Image imageIcon;

		public Sprite[] spriteLevels;

		private int levelOld = -1;

		public static readonly float[] IconPosPerLevel = new float[]
		{
			10f,
			10f,
			5f,
			0f,
			0f,
			0f,
			10f
		};
	}
}
