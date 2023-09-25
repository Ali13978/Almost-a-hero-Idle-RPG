using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBuffMark : SpineAnim
	{
		public SpineAnimBuffMark() : base(SpineAnimBuffMark.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
