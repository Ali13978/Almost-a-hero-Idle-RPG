using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimWarlockSwarm : SpineAnim
	{
		public SpineAnimWarlockSwarm() : base(SpineAnimWarlockSwarm.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation loop;

		public static Spine.Animation attack;

		public static Spine.Animation end;
	}
}
