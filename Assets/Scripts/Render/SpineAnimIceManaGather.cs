using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimIceManaGather : SpineAnim
	{
		public SpineAnimIceManaGather() : base(SpineAnimIceManaGather.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
