using System;
using UnityEngine;

public class TimeWarpEffect : MonoBehaviour
{
	private void Start()
	{
		this.lines = new EffectLine[this.lineCount];
		for (int i = 0; i < this.lineCount; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.effectLinePrefab);
			gameObject.transform.SetParent(base.transform, false);
			this.lines[i] = gameObject.GetComponent<EffectLine>();
			this.lines[i].Initialize();
		}
	}

	private void Update()
	{
		for (int i = 0; i < this.lineCount; i++)
		{
			this.lines[i].Advance(Time.deltaTime);
		}
	}

	public GameObject effectLinePrefab;

	public int lineCount;

	private EffectLine[] lines;
}
