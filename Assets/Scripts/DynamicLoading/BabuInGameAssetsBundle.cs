using System;
using Render;
using Spine.Unity;
using UnityEngine;

namespace DynamicLoading
{
	[CreateAssetMenu(fileName = "BabuInGameAssetsBundle", menuName = "Bundles/Hero InGame Assets/Babu")]
	public class BabuInGameAssetsBundle : HeroInGameAssetsBundle
	{
		public SmartProjectileRenderer Projectile;

		public GameObject ProjectileExplosion;

		public SkeletonDataAsset ProjectileExplosionAnimData;

		public VariantSpriteRenderer ProjectileTeaCup;
	}
}
