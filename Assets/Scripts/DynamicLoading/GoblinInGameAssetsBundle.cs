using System;
using Render;
using Spine.Unity;
using UnityEngine;

namespace DynamicLoading
{
	[CreateAssetMenu(fileName = "GoblinInGameAssetsBundle", menuName = "Bundles/Hero InGame Assets/Goblin")]
	public class GoblinInGameAssetsBundle : HeroInGameAssetsBundle
	{
		public SmartProjectileRenderer ProjectilePrefab;

		public GameObject ProjectileSmokeBombPrefab;

		public GameObject SmokePrefab;

		public SkeletonDataAsset SmokeAnimation;
	}
}
