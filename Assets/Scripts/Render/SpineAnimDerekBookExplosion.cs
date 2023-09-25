using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimDerekBookExplosion : SpineAnim
	{
		public SpineAnimDerekBookExplosion() : base(SpineAnimDerekBookExplosion.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
