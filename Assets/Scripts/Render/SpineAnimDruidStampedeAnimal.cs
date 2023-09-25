using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimDruidStampedeAnimal : SpineAnim
	{
		public SpineAnimDruidStampedeAnimal() : base(SpineAnimDruidStampedeAnimal.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation run;
	}
}
