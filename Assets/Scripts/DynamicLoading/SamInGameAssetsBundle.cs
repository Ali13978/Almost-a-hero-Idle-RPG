using System;
using Render;
using Spine.Unity;
using UnityEngine;

namespace DynamicLoading
{
	[CreateAssetMenu(fileName = "SamInGameAssetsBundle", menuName = "Bundles/Hero InGame Assets/Sam")]
	public class SamInGameAssetsBundle : HeroInGameAssetsBundle
	{
		public GameObject ProjectileAxe;

		public SmartProjectileRenderer SmartProjectileBottle;

		public GameObject BottleExplossion;

		public SkeletonDataAsset BottleExplossionAnimData;
	}
}
