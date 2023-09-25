using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBuffStun : SpineAnim
	{
		public SpineAnimBuffStun() : base(SpineAnimBuffStun.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
