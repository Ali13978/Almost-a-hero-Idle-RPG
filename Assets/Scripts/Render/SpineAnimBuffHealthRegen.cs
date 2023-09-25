using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBuffHealthRegen : SpineAnim
	{
		public SpineAnimBuffHealthRegen() : base(SpineAnimBuffHealthRegen.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
