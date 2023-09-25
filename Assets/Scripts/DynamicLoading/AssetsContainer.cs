using System;
using UnityEngine;

namespace DynamicLoading
{
	[CreateAssetMenu(fileName = "AssetsContainer", menuName = "Bundles/Assets Container")]
	public class AssetsContainer : ScriptableObject
	{
		public Sprite[] sprites;
	}
}
