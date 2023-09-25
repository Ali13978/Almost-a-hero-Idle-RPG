using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimTotemFire : SpineAnim
	{
		public SpineAnimTotemFire() : base(SpineAnimTotemFire.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation animAttack;

		public static Spine.Animation animAttackClose;

		public static Spine.Animation animAttackNone;
	}
}
