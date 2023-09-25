using System;
using Render;
using Spine.Unity;
using UnityEngine;

namespace DynamicLoading
{
	[CreateAssetMenu(fileName = "WendleInGameAssetsBundle", menuName = "Bundles/Hero InGame Assets/Wendle")]
	public class WendleInGameAssetsBundle : HeroInGameAssetsBundle
	{
		public VariantSpriteRenderer ProjectileMagicBall;

		public GameObject ProjectileMeteor;

		public SkeletonDataAsset ProjectileMeteorAnimData;
	}
}
