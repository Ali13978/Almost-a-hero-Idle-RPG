using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
	private void Start()
	{
		this.loadAsync.onClick.AddListener(delegate()
		{
			Cheats.stopwatch.Start();
			this.asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
		});
		this.loadNormal.onClick.AddListener(delegate()
		{
			Cheats.stopwatch.Start();
			SceneManager.LoadScene(1);
		});
	}

	private void Update()
	{
		if (this.asyncLoad == null)
		{
			return;
		}
		this.slider.value = this.asyncLoad.progress;
		this.timePassed.text = (Cheats.stopwatch.Elapsed.TotalMilliseconds / 1000.0).ToString();
		if (this.asyncLoad.isDone)
		{
			this.asyncLoad = null;
		}
	}

	public Text timePassed;

	public Button loadAsync;

	public Button loadNormal;

	public Slider slider;

	public AsyncOperation asyncLoad;
}
