using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBuffMiss : SpineAnim
	{
		public SpineAnimBuffMiss() : base(SpineAnimBuffMiss.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
