using System;
using Render;
using UnityEngine;

namespace DynamicLoading
{
	[CreateAssetMenu(fileName = "HoratioInGameAssetsBundle", menuName = "Bundles/Hero InGame Assets/Horatio")]
	public class HoratioInGameAssetsBundle : HeroInGameAssetsBundle
	{
		public SmartProjectileRenderer SkillExcaliburProjectile;
	}
}
