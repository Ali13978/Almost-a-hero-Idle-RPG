using System;

public class AudioSourceLoop : AudioSourceTimed
{
	public override void Update(float dt)
	{
		if (this.audioClipPromise != null && !this.audioClipPromise.IsReady())
		{
			base.CheckAudioPromiseMaxLoadTimePassed(dt);
		}
		this.time += dt;
		if (!this.hasStarted && this.time >= this.fadeInStart)
		{
			this.hasStarted = true;
			if (this.audioClipPromise != null)
			{
				this.source.clip = this.audioClipPromise.Clip;
			}
			this.source.Play();
		}
		if (this.time >= this.fadeInStart)
		{
			if (this.time < this.fadeInEnd)
			{
				float volume = (this.time - this.fadeInStart) / (this.fadeInEnd - this.fadeInStart);
				this.source.volume = volume;
			}
			else if (this.time < this.fadeOutStart)
			{
				this.source.volume = 1f;
			}
			else if (this.time < this.fadeOutEnd)
			{
				float volume2 = (this.fadeOutEnd - this.time) / (this.fadeOutEnd - this.fadeOutStart);
				this.source.volume = volume2;
			}
			else
			{
				this.source.volume = 1f;
				this.source.loop = false;
				this.source.Stop();
			}
		}
	}

	public float fadeInStart;

	public float fadeInEnd;

	public float fadeOutStart;

	public float fadeOutEnd;
}
