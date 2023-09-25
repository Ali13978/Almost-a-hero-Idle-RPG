using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DG.Tweening;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

public static class Utility
{
	public static Color32 HexColor(string hex)
	{
		hex = hex.ToUpper();
		byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
		return new Color32(r, g, b, byte.MaxValue);
	}

	public static void SetSizeDeltaX(this RectTransform rectTransform, float x)
	{
		Vector2 sizeDelta = rectTransform.sizeDelta;
		sizeDelta.x = x;
		rectTransform.sizeDelta = sizeDelta;
	}

	public static void SetSizeDeltaY(this RectTransform rectTransform, float y)
	{
		Vector2 sizeDelta = rectTransform.sizeDelta;
		sizeDelta.y = y;
		rectTransform.sizeDelta = sizeDelta;
	}

	public static void SetAnchorPosX(this RectTransform rectTransform, float x)
	{
		Vector2 anchoredPosition = rectTransform.anchoredPosition;
		anchoredPosition.x = x;
		rectTransform.anchoredPosition = anchoredPosition;
	}

	public static void SetAnchorPosY(this RectTransform rectTransform, float y)
	{
		Vector2 anchoredPosition = rectTransform.anchoredPosition;
		anchoredPosition.y = y;
		rectTransform.anchoredPosition = anchoredPosition;
	}

	public static void SetRightDelta(this RectTransform rectTransform, float delta)
	{
		Vector2 offsetMax = rectTransform.offsetMax;
		offsetMax.x = -delta;
		rectTransform.offsetMax = offsetMax;
	}

	public static void SetLeftDelta(this RectTransform rectTransform, float delta)
	{
		Vector2 offsetMin = rectTransform.offsetMin;
		offsetMin.x = delta;
		rectTransform.offsetMin = offsetMin;
	}

	public static void SetTopDelta(this RectTransform rectTransform, float delta)
	{
		Vector2 offsetMax = rectTransform.offsetMax;
		offsetMax.y = -delta;
		rectTransform.offsetMax = offsetMax;
	}

	public static void SetBottomDelta(this RectTransform rectTransform, float delta)
	{
		Vector2 offsetMin = rectTransform.offsetMin;
		offsetMin.y = delta;
		rectTransform.offsetMin = offsetMin;
	}

	public static void SetImageAlpha(this Image image, float a)
	{
		Color color = image.color;
		color.a = a;
		image.color = color;
	}

	public static void SetAlpha(this Graphic image, float a)
	{
		Color color = image.color;
		color.a = a;
		image.color = color;
	}

	public static void SetRGB(this Graphic image, Color color)
	{
		Color color2 = image.color;
		color2.r = color.r;
		color2.g = color.g;
		color2.b = color.b;
		image.color = color2;
	}

	public static void SetImageLumminity(this Image image, float a)
	{
		Color color = image.color;
		color.r = a;
		color.g = a;
		color.b = a;
		image.color = color;
	}

	public static string UppercaseFirst(this string str)
	{
		if (string.IsNullOrEmpty(str))
		{
			return string.Empty;
		}
		char[] array = str.ToLower().ToCharArray();
		array[0] = char.ToUpper(array[0]);
		return new string(array);
	}

	public static string FirstLetterToUpperCaseOrConvertNullToEmptyString(this string s)
	{
		if (string.IsNullOrEmpty(s))
		{
			return string.Empty;
		}
		char[] array = s.ToCharArray();
		array[0] = char.ToUpper(array[0]);
		return new string(array);
	}

	public static float GetTextPreferredWidth(this Text text)
	{
		TextGenerationSettings generationSettings = text.GetGenerationSettings(text.rectTransform.rect.size);
		generationSettings.scaleFactor = 1f;
		generationSettings.fontSize = text.fontSize;
		generationSettings.resizeTextMaxSize = text.resizeTextMaxSize;
		generationSettings.resizeTextForBestFit = true;
		return text.cachedTextGenerator.GetPreferredWidth(text.text, generationSettings);
	}

	public static float GetTextPreferredHeight(this Text text)
	{
		TextGenerationSettings generationSettings = text.GetGenerationSettings(text.rectTransform.rect.size);
		generationSettings.scaleFactor = 1f;
		generationSettings.fontSize = text.fontSize;
		generationSettings.resizeTextMaxSize = text.resizeTextMaxSize;
		generationSettings.resizeTextForBestFit = false;
		return text.cachedTextGenerator.GetPreferredHeight(text.text, generationSettings);
	}

	public static float GetPreferredWidth(Text text, int maxSize)
	{
		TextGenerationSettings generationSettings = text.GetGenerationSettings(text.rectTransform.rect.size);
		generationSettings.scaleFactor = 1f;
		generationSettings.resizeTextMaxSize = maxSize;
		generationSettings.resizeTextForBestFit = true;
		return text.cachedTextGenerator.GetPreferredWidth(text.text, generationSettings);
	}

	public static float GetPreferredHeight(Text text, int minSize, int maxSize)
	{
		TextGenerationSettings generationSettings = text.GetGenerationSettings(text.rectTransform.rect.size);
		generationSettings.resizeTextMinSize = minSize;
		generationSettings.scaleFactor = 1f;
		generationSettings.resizeTextMaxSize = maxSize;
		generationSettings.resizeTextForBestFit = true;
		return text.cachedTextGenerator.GetPreferredHeight(text.text, generationSettings);
	}

	public static void OptimizeFontSizes(params Text[] texts)
	{
		int num = -2147483647;
		int num2 = -1;
		for (int i = 0; i < texts.Length; i++)
		{
			Text text = texts[i];
			int length = text.text.Length;
			if (length > num)
			{
				num = length;
				num2 = i;
			}
		}
		Text text2 = texts[num2];
		TextGenerationSettings generationSettings = text2.GetGenerationSettings(text2.rectTransform.rect.size);
		generationSettings.scaleFactor = 1f;
		generationSettings.fontSize = 36;
		generationSettings.resizeTextForBestFit = true;
		TextGenerator textGenerator = new TextGenerator();
		textGenerator.Populate(text2.text, generationSettings);
		int fontSizeUsedForBestFit = textGenerator.fontSizeUsedForBestFit;
		for (int j = 0; j < texts.Length; j++)
		{
			texts[j].fontSize = fontSizeUsedForBestFit;
		}
	}

	public static Color GetUltiColor(HeroDataBase.UltiCatagory ucat)
	{
		switch (ucat)
		{
		case HeroDataBase.UltiCatagory.GREEN:
			return new Color32(125, 199, 20, 155);
		case HeroDataBase.UltiCatagory.BLUE:
			return new Color32(0, 158, 234, 155);
		case HeroDataBase.UltiCatagory.ORANGE:
			return new Color32(byte.MaxValue, 137, 20, 155);
		case HeroDataBase.UltiCatagory.RED:
			return new Color32(249, 59, 18, 155);
		default:
			return Color.white;
		}
	}

	public static float NBit(int eightBitVal)
	{
		return (float)eightBitVal * 0.00390625f;
	}

	public static void Shuffle<T>(this List<T> list)
	{
		int i = list.Count;
		while (i > 1)
		{
			i--;
			int index = Utility.rng.Next(i + 1);
			T value = list[index];
			list[index] = list[i];
			list[i] = value;
		}
	}

	public static List<T> GetEnumList<T>()
	{
		return Enum.GetValues(typeof(T)).Cast<T>().ToList<T>();
	}

	public static void FillUiElementList<T>(T prefab, Transform parent, int targetCount, List<T> listToFill) where T : MonoBehaviour
	{
		int i = listToFill.Count;
		while (i < targetCount)
		{
			i++;
			T item = UnityEngine.Object.Instantiate<T>(prefab, parent);
			item.transform.localScale = Vector3.one;
			item.transform.localPosition = Vector3.zero;
			listToFill.Add(item);
		}
		while (i > targetCount)
		{
			i--;
			T t = listToFill[i];
			listToFill.RemoveAt(i);
			UnityEngine.Object.Destroy(t.gameObject);
		}
	}

	public static void FillUiElementList<T>(T prefab, RectTransform parent, int targetCount, List<T> listToFill, Utility.ElementStateChange<T> onSpawnCommand, Utility.ElementStateChange<T> onDestroyCommand) where T : MonoBehaviour
	{
		int i = listToFill.Count;
		while (i < targetCount)
		{
			i++;
			T t = UnityEngine.Object.Instantiate<T>(prefab, parent);
			t.transform.localScale = Vector3.one;
			t.transform.localPosition = Vector3.zero;
			listToFill.Add(t);
			if (onSpawnCommand != null)
			{
				onSpawnCommand(t, parent, i - 1);
			}
		}
		while (i > targetCount)
		{
			i--;
			T obj = listToFill[i];
			listToFill.RemoveAt(i);
			if (onDestroyCommand != null)
			{
				onDestroyCommand(obj, parent, i);
			}
			UnityEngine.Object.Destroy(obj.gameObject);
		}
	}

	public static void FillUiElementList<T>(T prefab, Transform parent, int targetCount, Queue<T> listToFill) where T : MonoBehaviour
	{
		int i = listToFill.Count;
		while (i < targetCount)
		{
			i++;
			T item = UnityEngine.Object.Instantiate<T>(prefab, parent);
			item.transform.localScale = Vector3.one;
			item.transform.localPosition = Vector3.zero;
			listToFill.Enqueue(item);
		}
		while (i > targetCount)
		{
			i--;
			T t = listToFill.Dequeue();
			UnityEngine.Object.Destroy(t.gameObject);
		}
	}

	public static void FillUiElementArray<T>(T prefab, Transform parent, int targetCount, T[] array, int startIndex = 0) where T : MonoBehaviour
	{
		for (int i = 0; i < targetCount; i++)
		{
			T t = UnityEngine.Object.Instantiate<T>(prefab, parent);
			t.transform.localScale = Vector3.one;
			t.transform.localPosition = Vector3.zero;
			array[i + startIndex] = t;
		}
	}

	public static void SuffleList<T>(IList<T> list)
	{
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			int randomInt = GameMath.GetRandomInt(0, count, GameMath.RandType.NoSeed);
			T value = list[randomInt];
			list[randomInt] = list[i];
			list[i] = value;
		}
	}

	public static T GetLastItem<T>(this IList<T> list)
	{
		return list[list.Count - 1];
	}

	public static T PopLastItem<T>(this IList<T> list)
	{
		T result = list[list.Count - 1];
		list.RemoveAt(list.Count - 1);
		return result;
	}

	public static T PopRandomItem<T>(this IList<T> list, GameMath.RandType randType = GameMath.RandType.NoSeed)
	{
		int randomInt = GameMath.GetRandomInt(0, list.Count, randType);
		T result = list[randomInt];
		list.RemoveAt(randomInt);
		return result;
	}

	public static void SetScale(this Transform transform, float scale)
	{
		transform.localScale = new Vector3(scale, scale, scale);
	}

	public static void SetScale(this Transform transform, float x, float y)
	{
		transform.localScale = new Vector3(x, y, transform.localScale.z);
	}

	public static void SetScaleY(this Transform transform, float scale)
	{
		transform.localScale = new Vector3(transform.localScale.x, scale, transform.localScale.z);
	}

	public static void SetScaleX(this Transform transform, float scale)
	{
		transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
	}

	public static void SetPosX(this Transform transform, float pos)
	{
		transform.position = new Vector3(pos, transform.position.y, transform.position.z);
	}

	public static void SetPosY(this Transform transform, float pos)
	{
		transform.position = new Vector3(transform.position.x, pos, transform.position.z);
	}

	public static void SetAngleZ(this Transform transform, float angle)
	{
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
	}

	public static void BubbleSort<T>(this List<T> list, Comparison<T> comparer)
	{
		int count = list.Count;
		for (int i = 1; i < count; i++)
		{
			for (int j = 0; j < count - i; j++)
			{
				if (comparer(list[j], list[j + 1]) > 0)
				{
					T value = list[j];
					list[j] = list[j + 1];
					list[j + 1] = value;
				}
			}
		}
	}

	public static void BubbleSort<T>(this List<T> list, IComparer<T> comparer)
	{
		int count = list.Count;
		for (int i = 1; i < count; i++)
		{
			for (int j = 0; j < count - i; j++)
			{
				if (comparer.Compare(list[j], list[j + 1]) > 0)
				{
					T value = list[j];
					list[j] = list[j + 1];
					list[j + 1] = value;
				}
			}
		}
	}

	public static Sequence DOColorWithShot(this Graphic graphic, Color targetColor, Color shotColor, float duration, float mixPivot = 0.5f)
	{
		Color endValue = Color.Lerp(graphic.color, shotColor, 0.65f);
		float num = duration * mixPivot;
		float duration2 = duration - num;
		return DOTween.Sequence().Append(graphic.DOColor(endValue, num)).Append(graphic.DOColor(targetColor, duration2)).Play<Sequence>();
	}

	public static Sequence DOColorWithShot(this Outline graphic, Color targetColor, Color shotColor, float duration, float mixPivot = 0.5f)
	{
		Color endValue = Color.Lerp(graphic.effectColor, shotColor, 0.65f);
		float num = duration * mixPivot;
		float duration2 = duration - num;
		return DOTween.Sequence().Append(graphic.DOColor(endValue, num)).Append(graphic.DOColor(targetColor, duration2)).Play<Sequence>();
	}

	public static Tweener DoTopDelta(this RectTransform target, float delta, float duration, bool snapping = false)
	{
		Vector2 offsetMax = target.offsetMax;
		offsetMax.y = -delta;
		return DOTween.To(() => target.offsetMax, delegate(Vector2 x)
		{
			target.offsetMax = x;
		}, offsetMax, duration).SetOptions(snapping).SetTarget(target);
	}

	public static Tweener DoBottomDelta(this RectTransform target, float delta, float duration, bool snapping = false)
	{
		Vector2 offsetMin = target.offsetMin;
		offsetMin.y = delta;
		return DOTween.To(() => target.offsetMin, delegate(Vector2 x)
		{
			target.offsetMin = x;
		}, offsetMin, duration).SetOptions(snapping).SetTarget(target);
	}

	public static Tweener DOSizeDeltaX(this RectTransform target, float endValue, float duration, bool snapping = false)
	{
		Vector2 sizeDelta = target.sizeDelta;
		sizeDelta.x = endValue;
		return DOTween.To(() => target.sizeDelta, delegate(Vector2 x)
		{
			target.sizeDelta = x;
		}, sizeDelta, duration).SetOptions(snapping).SetTarget(target);
	}

	public static Tweener DOSizeDeltaY(this RectTransform target, float endValue, float duration, bool snapping = false)
	{
		Vector2 sizeDelta = target.sizeDelta;
		sizeDelta.y = endValue;
		return DOTween.To(() => target.sizeDelta, delegate(Vector2 x)
		{
			target.sizeDelta = x;
		}, sizeDelta, duration).SetOptions(snapping).SetTarget(target);
	}

	public static void RemoveFastAt<T>(this IList<T> collection, int index)
	{
		int index2 = collection.Count - 1;
		T value = collection[index2];
		collection[index] = value;
		collection.RemoveAt(index2);
	}

	public static void RemoveFast<T>(this IList<T> collection, T item)
	{
		int num = collection.IndexOf(item);
		if (num > 0)
		{
			int index = collection.Count - 1;
			collection[num] = collection[index];
			collection.RemoveAt(index);
		}
	}

	public static void DoPopupAnimation(this RectTransform rectTransform)
	{
	}

	public static void ShuffleWeighted<T>(this IList<T> weightedCollection) where T : IProvideWeight
	{
		int count = weightedCollection.Count;
		List<float> list = (from w in weightedCollection
		select w.GetWeight()).ToList<float>();
		for (int i = 0; i < count; i++)
		{
			int num = GameMath.GetRouletteOutcome(list, GameMath.RandType.NoSeed) + i;
			T item = weightedCollection[num];
			weightedCollection.RemoveAt(num);
			weightedCollection.Insert(i, item);
			list.RemoveAt(num - i);
		}
	}

	public static Tweener TimerTween(float duration)
	{
		float f = 0f;
		return DOTween.To(() => f, delegate(float x)
		{
			f = x;
		}, 3f, duration);
	}

	public static string Loc(this string key)
	{
		return LM.Get(key);
	}

	public static string LocFormat(this string key, object arg0)
	{
		return string.Format(LM.Get(key), arg0);
	}

	public static string LocFormat(this string key, object arg0, object arg1)
	{
		return string.Format(LM.Get(key), arg0, arg1);
	}

	private static System.Random rng = new System.Random();

	public delegate void ElementStateChange<T>(T obj, RectTransform parent, int index);
}
