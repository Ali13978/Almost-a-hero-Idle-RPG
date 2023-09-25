using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimTotemThunderbolt : SpineAnim
	{
		public SpineAnimTotemThunderbolt() : base(SpineAnimTotemThunderbolt.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
