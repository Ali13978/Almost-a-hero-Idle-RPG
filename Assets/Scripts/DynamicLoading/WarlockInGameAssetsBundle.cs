using System;
using Spine.Unity;
using UnityEngine;

namespace DynamicLoading
{
	[CreateAssetMenu(fileName = "WarlockInGameAssetsBundle", menuName = "Bundles/Hero InGame Assets/Warlock")]
	public class WarlockInGameAssetsBundle : HeroInGameAssetsBundle
	{
		public GameObject Projectile;

		public SkeletonDataAsset ProjectileAnimData;
	}
}
