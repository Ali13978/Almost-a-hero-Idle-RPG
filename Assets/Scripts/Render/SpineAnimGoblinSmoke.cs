using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimGoblinSmoke : SpineAnim
	{
		public SpineAnimGoblinSmoke() : base(SpineAnimGoblinSmoke.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation start;

		public static Spine.Animation loop;

		public static Spine.Animation end;
	}
}
