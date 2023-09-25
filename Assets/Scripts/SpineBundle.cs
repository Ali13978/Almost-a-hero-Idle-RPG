using System;
using Spine.Unity;
using UnityEngine;

[CreateAssetMenu(fileName = "SpineBundle")]
public class SpineBundle : ScriptableObject
{
	public GameObject prefab;

	public SkeletonDataAsset skeletonAsset;
}
