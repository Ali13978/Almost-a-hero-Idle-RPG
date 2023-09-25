using System;
using UnityEngine;

public class AudioSourceSimple
{
	public virtual void Update(float dt)
	{
		if (this.audioClipPromise == null || this.hasStarted)
		{
			if (this.source != null && this.source.clip != null && this.source.time >= this.timeFadeOut)
			{
				this.source.volume *= 1f - (this.source.time - this.timeFadeOut) / (this.source.clip.length - this.timeFadeOut);
			}
			return;
		}
		if (this.audioClipPromise.IsReady())
		{
			this.hasStarted = true;
			this.source.clip = this.audioClipPromise.Clip;
			this.source.time = this.timeOffset;
			this.source.Play();
		}
		else
		{
			this.CheckAudioPromiseMaxLoadTimePassed(dt);
		}
	}

	public virtual bool HasEnded()
	{
		return (this.audioClipPromise == null || this.hasStarted) && !this.source.isPlaying;
	}

	public void Cancel()
	{
		this.source.Stop();
	}

	protected void CheckAudioPromiseMaxLoadTimePassed(float dt)
	{
		this.timeWaitingLoad += dt;
		if (this.timeWaitingLoad > 2f)
		{
			this.hasStarted = true;
		}
	}

	public AudioSource source;

	public SoundType type;

	public string by;

	public bool isVoice;

	public AudioClipPromise audioClipPromise;

	public float timeOffset;

	public float timeFadeOut;

	public float priority;

	public float individualVolumeFactor;

	protected bool hasStarted;

	protected float timeWaitingLoad;

	protected const float MaxWaitTimeForPromise = 2f;
}
