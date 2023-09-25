using System;
using UnityEngine;

public class AudioClipPromise
{
	public AudioClipPromise(string bundleId, string clipPath)
	{
		this.bundleId = bundleId;
		this.clipPath = clipPath;
		this.requestClipLoading = true;
		this.clip = null;
	}

	public AudioClipPromise(AudioClip clip)
	{
		this.requestClipLoading = false;
		this.bundleId = null;
		this.clipPath = null;
		this.clip = clip;
	}

	public AudioClipPromise()
	{
		this.requestClipLoading = false;
		this.bundleId = null;
		this.clipPath = null;
		this.clip = null;
	}

	public static AudioClipPromise[] BuildPromises(AudioClip[] clips)
	{
		AudioClipPromise[] array = new AudioClipPromise[clips.Length];
		for (int i = 0; i < clips.Length; i++)
		{
			array[i] = new AudioClipPromise(clips[i]);
		}
		return array;
	}

	public static AudioClipPromise[] BuildPromises(int count)
	{
		AudioClipPromise[] array = new AudioClipPromise[count];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new AudioClipPromise();
		}
		return array;
	}

	public static void UnloadAssets(AudioClipPromise[] promises)
	{
		foreach (AudioClipPromise audioClipPromise in promises)
		{
			audioClipPromise.UnloadAsset();
		}
	}

	public static AudioClipPromise[] BuildPromises(string bundleId, string[] paths)
	{
		AudioClipPromise[] array = new AudioClipPromise[paths.Length];
		for (int i = 0; i < paths.Length; i++)
		{
			array[i] = new AudioClipPromise(bundleId, paths[i]);
		}
		return array;
	}

	public AudioClip Clip
	{
		get
		{
			if (this.requestClipLoading)
			{
				this.requestClipLoading = false;
				this.soundLoader = SoundArchieve.inst.LoadSound(this.clipPath, this.bundleId, new Action<AudioClip>(this.OnClipLoaded));
			}
			return this.clip;
		}
		set
		{
			this.clip = value;
		}
	}

	public bool IsReady()
	{
		return this.clip != null;
	}

	private void OnClipLoaded(AudioClip clip)
	{
		this.clip = clip;
		this.soundLoader = null;
	}

	public void UnloadAsset()
	{
		this.clip = null;
		this.requestClipLoading = (this.bundleId != null);
		if (this.soundLoader != null)
		{
			SoundArchieve.inst.CancelSoundLoading(this.soundLoader);
			this.soundLoader = null;
		}
	}

	private AudioClip clip;

	private bool requestClipLoading;

	private string clipPath;

	private string bundleId;

	private SoundArchieve.SoundLoader soundLoader;
}
