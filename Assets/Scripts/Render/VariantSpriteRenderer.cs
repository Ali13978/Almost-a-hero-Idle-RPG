using System;
using UnityEngine;

namespace Render
{
	public class VariantSpriteRenderer : MonoBehaviour
	{
		public void SetSkin(int index)
		{
			this.spriteRenderer.sprite = this.sprites[index];
		}

		public void SetSkinWithVariant(int index)
		{
			this.spriteRenderer.sprite = this.sprites[this.variantIndexes[index]];
		}

		public Sprite[] sprites;

		public SpriteRenderer spriteRenderer;

		public int[] variantIndexes;
	}
}
