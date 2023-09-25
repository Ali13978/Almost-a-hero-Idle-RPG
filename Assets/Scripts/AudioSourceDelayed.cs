using System;

public class AudioSourceDelayed : AudioSourceTimed
{
	public override void Update(float dt)
	{
		if (this.hasStarted)
		{
			return;
		}
		this.time += dt;
		if ((this.audioClipPromise == null || this.audioClipPromise.IsReady()) && this.time >= this.timePlay)
		{
			this.hasStarted = true;
			if (this.audioClipPromise != null)
			{
				this.source.clip = this.audioClipPromise.Clip;
			}
			this.source.Play();
		}
		else if (this.audioClipPromise != null && !this.audioClipPromise.IsReady() && this.time >= this.timePlay)
		{
			base.CheckAudioPromiseMaxLoadTimePassed(dt);
		}
	}

	public float timePlay;
}
