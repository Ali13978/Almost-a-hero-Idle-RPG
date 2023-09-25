using System;
using Render;
using Spine.Unity;
using UnityEngine;

namespace DynamicLoading
{
	[CreateAssetMenu(fileName = "KindLennyInGameAssetsBundle", menuName = "Bundles/Hero InGame Assets/Kind Lenny")]
	public class KindLennyInGameAssetsBundle : HeroInGameAssetsBundle
	{
		public SmartProjectileRenderer Projectile;

		public VariantSpriteRenderer ProjectileAppleBombard;

		public GameObject GreenAppleExplossion;

		public SkeletonDataAsset GreenAppleExplosionAnimData;
	}
}
