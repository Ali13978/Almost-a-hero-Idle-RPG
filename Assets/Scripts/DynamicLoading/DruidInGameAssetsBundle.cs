using System;
using Spine.Unity;
using UnityEngine;

namespace DynamicLoading
{
	[CreateAssetMenu(fileName = "DruidInGameAssetsBundle", menuName = "Bundles/Hero InGame Assets/Druid")]
	public class DruidInGameAssetsBundle : HeroInGameAssetsBundle
	{
		public GameObject StampedeAnimal;

		public SkeletonDataAsset StampedeAnimalAnimData;

		public GameObject Larry;

		public SkeletonDataAsset LarryAnimData;

		public GameObject Curly;

		public SkeletonDataAsset CurlyAnimData;

		public GameObject Moe;

		public SkeletonDataAsset MoeAnimData;
	}
}
