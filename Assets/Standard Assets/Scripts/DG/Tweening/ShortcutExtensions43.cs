using System;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
	public static class ShortcutExtensions43
	{
		public static Sequence DOGradientColor(this Material target, Gradient gradient, float duration)
		{
			Sequence sequence = DOTween.Sequence();
			GradientColorKey[] colorKeys = gradient.colorKeys;
			int num = colorKeys.Length;
			for (int i = 0; i < num; i++)
			{
				GradientColorKey gradientColorKey = colorKeys[i];
				if (i == 0 && gradientColorKey.time <= 0f)
				{
					target.color = gradientColorKey.color;
				}
				else
				{
					float duration2 = (i != num - 1) ? (duration * ((i != 0) ? (gradientColorKey.time - colorKeys[i - 1].time) : gradientColorKey.time)) : (duration - sequence.Duration(false));
					sequence.Append(target.DOColor(gradientColorKey.color, duration2).SetEase(Ease.Linear));
				}
			}
			return sequence;
		}

		public static Sequence DOGradientColor(this Material target, Gradient gradient, string property, float duration)
		{
			Sequence sequence = DOTween.Sequence();
			GradientColorKey[] colorKeys = gradient.colorKeys;
			int num = colorKeys.Length;
			for (int i = 0; i < num; i++)
			{
				GradientColorKey gradientColorKey = colorKeys[i];
				if (i == 0 && gradientColorKey.time <= 0f)
				{
					target.color = gradientColorKey.color;
				}
				else
				{
					float duration2 = (i != num - 1) ? (duration * ((i != 0) ? (gradientColorKey.time - colorKeys[i - 1].time) : gradientColorKey.time)) : (duration - sequence.Duration(false));
					sequence.Append(target.DOColor(gradientColorKey.color, property, duration2).SetEase(Ease.Linear));
				}
			}
			return sequence;
		}

		public static Tweener DOColor(this SpriteRenderer target, Color endValue, float duration)
		{
			return DOTween.To(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOFade(this SpriteRenderer target, float endValue, float duration)
		{
			return DOTween.ToAlpha(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Sequence DOGradientColor(this SpriteRenderer target, Gradient gradient, float duration)
		{
			Sequence sequence = DOTween.Sequence();
			GradientColorKey[] colorKeys = gradient.colorKeys;
			int num = colorKeys.Length;
			for (int i = 0; i < num; i++)
			{
				GradientColorKey gradientColorKey = colorKeys[i];
				if (i == 0 && gradientColorKey.time <= 0f)
				{
					target.color = gradientColorKey.color;
				}
				else
				{
					float duration2 = (i != num - 1) ? (duration * ((i != 0) ? (gradientColorKey.time - colorKeys[i - 1].time) : gradientColorKey.time)) : (duration - sequence.Duration(false));
					sequence.Append(target.DOColor(gradientColorKey.color, duration2).SetEase(Ease.Linear));
				}
			}
			return sequence;
		}

		public static Tweener DOMove(this Rigidbody2D target, Vector2 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector2>(target.MovePosition), endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOMoveX(this Rigidbody2D target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector2>(target.MovePosition), new Vector2(endValue, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetTarget(target);
		}

		public static Tweener DOMoveY(this Rigidbody2D target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector2>(target.MovePosition), new Vector2(0f, endValue), duration).SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
		}

		public static Tweener DORotate(this Rigidbody2D target, float endValue, float duration)
		{
			return DOTween.To(() => target.rotation, new DOSetter<float>(target.MoveRotation), endValue, duration).SetTarget(target);
		}

		public static Sequence DOJump(this Rigidbody2D target, Vector2 endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
		{
			if (numJumps < 1)
			{
				numJumps = 1;
			}
			float startPosY = target.position.y;
			float offsetY = -1f;
			bool offsetYSet = false;
			Sequence s = DOTween.Sequence();
			s.Append(DOTween.To(() => target.position, delegate(Vector2 x)
			{
				target.position = x;
			}, new Vector2(endValue.x, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear).OnUpdate(delegate
			{
				if (!offsetYSet)
				{
					offsetYSet = true;
					offsetY = ((!s.isRelative) ? (endValue.y - startPosY) : endValue.y);
				}
				Vector2 position = target.position;
				position.y += DOVirtual.EasedValue(0f, offsetY, s.ElapsedDirectionalPercentage(), Ease.OutQuad);
				target.position = position;
			})).Join(DOTween.To(() => target.position, delegate(Vector2 x)
			{
				target.position = x;
			}, new Vector2(0f, jumpPower), duration / (float)(numJumps * 2)).SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad).SetLoops(numJumps * 2, LoopType.Yoyo).SetRelative<Tweener>()).SetTarget(target).SetEase(DOTween.defaultEaseType);
			return s;
		}

		public static Tweener DOBlendableColor(this SpriteRenderer target, Color endValue, float duration)
		{
			endValue -= target.color;
			Color to = new Color(0f, 0f, 0f, 0f);
			return DOTween.To(() => to, delegate(Color x)
			{
				Color b = x - to;
				to = x;
				target.color += b;
			}, endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget(target);
		}
	}
}
