using System;
using Spine;
using UnityEngine;

namespace Render
{
	public interface ISpineAnim
	{
		void Apply(Spine.Animation anim, float time, bool loop);

		GameObject gameObject { get; }

		void SetColor(Color color);

		void SetAlpha(float a);

		void SetSkin(string skinName);

		void SetSkin(int index);
	}
}
