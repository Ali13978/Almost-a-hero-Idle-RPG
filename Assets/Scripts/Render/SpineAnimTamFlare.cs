using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimTamFlare : SpineAnim
	{
		public SpineAnimTamFlare() : base(SpineAnimTamFlare.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation animLoop;
	}
}
