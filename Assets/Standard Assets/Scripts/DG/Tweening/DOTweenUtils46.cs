using System;
using UnityEngine;

namespace DG.Tweening
{
	public static class DOTweenUtils46
	{
		public static Vector2 SwitchToRectTransform(RectTransform from, RectTransform to)
		{
			Vector2 b = new Vector2(from.rect.width * 0.5f + from.rect.xMin, from.rect.height * 0.5f + from.rect.yMin);
			Vector2 vector = RectTransformUtility.WorldToScreenPoint(null, from.position);
			vector += b;
			Vector2 b2;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(to, vector, null, out b2);
			Vector2 b3 = new Vector2(to.rect.width * 0.5f + to.rect.xMin, to.rect.height * 0.5f + to.rect.yMin);
			return to.anchoredPosition + b2 - b3;
		}
	}
}
