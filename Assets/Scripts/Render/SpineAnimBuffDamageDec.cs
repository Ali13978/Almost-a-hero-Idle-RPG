using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBuffDamageDec : SpineAnim
	{
		public SpineAnimBuffDamageDec() : base(SpineAnimBuffDamageDec.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
