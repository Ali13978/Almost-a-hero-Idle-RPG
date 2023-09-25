using System;
using UnityEngine;

namespace DynamicLoading
{
	[CreateAssetMenu(fileName = "BlindArcherInGameAssetsBundle", menuName = "Bundles/Hero InGame Assets/Blind Archer")]
	public class BlindArcherInGameAssetsBundle : HeroInGameAssetsBundle
	{
		public GameObject Projectile;
	}
}
