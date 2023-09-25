using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBuffDamageAdd : SpineAnim
	{
		public SpineAnimBuffDamageAdd() : base(SpineAnimBuffDamageAdd.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
