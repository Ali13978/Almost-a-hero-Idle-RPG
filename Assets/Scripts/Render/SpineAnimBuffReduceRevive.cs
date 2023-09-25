using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBuffReduceRevive : SpineAnim
	{
		public SpineAnimBuffReduceRevive() : base(SpineAnimBuffReduceRevive.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
