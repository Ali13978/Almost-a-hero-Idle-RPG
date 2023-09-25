using System;
using UnityEngine;

namespace DynamicLoading
{
	[CreateAssetMenu(fileName = "BackgroundBundle", menuName = "Bundles/Background")]
	public class BackgroundBundle : ScriptableObject
	{
		public Sprite sprite;

		public BackgroundSpineAnim prefab;
	}
}
