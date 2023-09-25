using System;
using System.Collections.Generic;
using Simulation;
using Ui;
using UnityEngine;

public class SoundManager : SoundArchieve
{
	public void ApplyEvent(SoundEvent e)
	{
		e.Apply(this);
	}

	public void CancelBy(string by)
	{
		int num = this.sourcesActive.Count;
		for (int i = num - 1; i >= 0; i--)
		{
			if (this.MatchBys(by, this.sourcesActive[i].by))
			{
				AudioSourceSimple audioSourceSimple = this.sourcesActive[i];
				audioSourceSimple.Cancel();
				this.sourcesPassive.Add(audioSourceSimple.source);
				this.sourcesActive[i] = this.sourcesActive[--num];
				this.sourcesActive.RemoveAt(num);
			}
		}
	}

	private bool MatchBys(string pattern, string text)
	{
		return text.StartsWith(pattern);
	}

	public void CancelSoundEffects()
	{
		List<AudioSourceSimple> list = new List<AudioSourceSimple>();
		foreach (AudioSourceSimple audioSourceSimple in this.sourcesActive)
		{
			if (audioSourceSimple.type != SoundType.MUSIC && audioSourceSimple.type != SoundType.MUSIC_BOSS && !audioSourceSimple.isVoice)
			{
				audioSourceSimple.Cancel();
				this.sourcesPassive.Add(audioSourceSimple.source);
			}
			else
			{
				list.Add(audioSourceSimple);
			}
		}
		this.sourcesActive = list;
	}

	public void CancelVoices()
	{
		List<AudioSourceSimple> list = new List<AudioSourceSimple>();
		foreach (AudioSourceSimple audioSourceSimple in this.sourcesActive)
		{
			if (audioSourceSimple.type != SoundType.MUSIC && audioSourceSimple.type != SoundType.MUSIC_BOSS && audioSourceSimple.isVoice)
			{
				audioSourceSimple.Cancel();
				this.sourcesPassive.Add(audioSourceSimple.source);
			}
			else
			{
				list.Add(audioSourceSimple);
			}
		}
		this.sourcesActive = list;
	}

	public void UpdateSound(float dt)
	{
		this.UpdateActives(dt);
		this.PassifyFinishedSources();
	}

	private void UpdateActives(float dt)
	{
		foreach (AudioSourceSimple audioSourceSimple in this.sourcesActive)
		{
			audioSourceSimple.Update(dt);
		}
	}

	private void PassifyFinishedSources()
	{
		int num = this.sourcesActive.Count;
		for (int i = num - 1; i >= 0; i--)
		{
			if (this.sourcesActive[i].HasEnded())
			{
				this.sourcesPassive.Add(this.sourcesActive[i].source);
				this.sourcesActive[i] = this.sourcesActive[--num];
				this.sourcesActive.RemoveAt(num);
			}
		}
	}

	private AudioSource GetAudioSource()
	{
		int num = this.sourcesPassive.Count;
		AudioSource audioSource;
		if (num > 0)
		{
			audioSource = this.sourcesPassive[--num];
			this.sourcesPassive.RemoveAt(num);
		}
		else
		{
			audioSource = base.gameObject.AddComponent<AudioSource>();
		}
		audioSource.playOnAwake = false;
		audioSource.loop = false;
		audioSource.volume = 1f;
		audioSource.pitch = 1f;
		return audioSource;
	}

	private void CancelVoiceSoundWithLowerPriority(string by, float priority)
	{
		AudioSourceSimple audioSourceSimple = null;
		int index = -1;
		int num = this.sourcesActive.Count;
		for (int i = num - 1; i >= 0; i--)
		{
			AudioSourceSimple audioSourceSimple2 = this.sourcesActive[i];
			if (audioSourceSimple2.isVoice && audioSourceSimple2.by == by)
			{
				audioSourceSimple = audioSourceSimple2;
				index = i;
				break;
			}
		}
		if (audioSourceSimple != null)
		{
			if (audioSourceSimple.priority > priority)
			{
				return;
			}
			audioSourceSimple.Cancel();
			this.sourcesActive[index] = this.sourcesActive[--num];
			this.sourcesActive.RemoveAt(num);
			this.sourcesPassive.Add(audioSourceSimple.source);
		}
	}

	public void PlayReversed(SoundType soundType, string by, bool isVoice, float priority, float volume, AudioClip clip)
	{
		if (this.sourcesActive.Count >= 20)
		{
			return;
		}
		if (isVoice)
		{
			this.CancelVoiceSoundWithLowerPriority(by, priority);
		}
		AudioSourceSimple audioSourceSimple = new AudioSourceSimple();
		AudioSource audioSource = this.GetAudioSource();
		audioSource.clip = clip;
		audioSource.pitch = -1f;
		audioSource.time = clip.length;
		audioSource.Play();
		audioSourceSimple.type = soundType;
		audioSourceSimple.by = by;
		audioSourceSimple.isVoice = isVoice;
		audioSourceSimple.priority = priority;
		audioSourceSimple.individualVolumeFactor = volume;
		audioSourceSimple.source = audioSource;
		this.UpdateVolume(audioSourceSimple);
		this.sourcesActive.Add(audioSourceSimple);
	}

	public void PlaySimple(SoundType soundType, string by, bool isVoice, float priority, float volume, AudioClip clip, float time = 0f, float fadeOutTime = 3.40282347E+38f)
	{
		if (this.sourcesActive.Count >= 20)
		{
			return;
		}
		if (isVoice)
		{
			this.CancelVoiceSoundWithLowerPriority(by, priority);
		}
		AudioSourceSimple audioSourceSimple = new AudioSourceSimple();
		AudioSource audioSource = this.GetAudioSource();
		audioSource.time = time;
		audioSource.clip = clip;
		audioSource.Play();
		audioSourceSimple.timeFadeOut = fadeOutTime;
		audioSourceSimple.type = soundType;
		audioSourceSimple.by = by;
		audioSourceSimple.isVoice = isVoice;
		audioSourceSimple.priority = priority;
		audioSourceSimple.individualVolumeFactor = volume;
		audioSourceSimple.source = audioSource;
		this.UpdateVolume(audioSourceSimple);
		this.sourcesActive.Add(audioSourceSimple);
	}

	public void PlaySimple(SoundType soundType, string by, bool isVoice, float priority, float volume, AudioClipPromise audioClipPromise, float time = 0f, float fadeOutTime = 3.40282347E+38f)
	{
		if (this.sourcesActive.Count >= 20)
		{
			return;
		}
		if (isVoice)
		{
			this.CancelVoiceSoundWithLowerPriority(by, priority);
		}
		AudioSourceSimple audioSourceSimple = new AudioSourceSimple();
		AudioSource audioSource = this.GetAudioSource();
		AudioClip clip = audioClipPromise.Clip;
		if (clip != null)
		{
			audioSource.clip = clip;
			audioSource.time = time;
			audioSource.Play();
		}
		else
		{
			audioSourceSimple.audioClipPromise = audioClipPromise;
			audioSourceSimple.timeOffset = time;
		}
		audioSourceSimple.timeFadeOut = fadeOutTime;
		audioSourceSimple.type = soundType;
		audioSourceSimple.by = by;
		audioSourceSimple.isVoice = isVoice;
		audioSourceSimple.priority = priority;
		audioSourceSimple.individualVolumeFactor = volume;
		audioSourceSimple.source = audioSource;
		this.UpdateVolume(audioSourceSimple);
		this.sourcesActive.Add(audioSourceSimple);
	}

	public void PlayDelayedLoopedInfinite(SoundType soundType, string by, float priority, float volume, AudioClip clip, float time = 0f, float delay = 0f)
	{
		AudioSource audioSource = this.GetAudioSource();
		audioSource.loop = true;
		audioSource.time = 0f;
		audioSource.clip = clip;
		audioSource.PlayDelayed(delay);
		if (time != 0f)
		{
			audioSource.time = time;
		}
		AudioSourceSimple audioSourceSimple = new AudioSourceSimple();
		audioSourceSimple.type = soundType;
		audioSourceSimple.by = by;
		audioSourceSimple.isVoice = false;
		audioSourceSimple.priority = priority;
		audioSourceSimple.individualVolumeFactor = volume;
		audioSourceSimple.source = audioSource;
		this.UpdateVolume(audioSourceSimple);
		this.sourcesActive.Add(audioSourceSimple);
	}

	public void PlayDelayedLoopedInfinite(SoundType soundType, string by, float priority, float volume, AudioClipPromise promise, float time = 0f, float delay = 0f)
	{
		AudioSource audioSource = this.GetAudioSource();
		audioSource.loop = true;
		audioSource.time = 0f;
		AudioClip clip = promise.Clip;
		AudioSourceSimple audioSourceSimple;
		if (clip != null)
		{
			audioSourceSimple = new AudioSourceSimple();
			audioSource.clip = clip;
			audioSource.PlayDelayed(delay);
		}
		else
		{
			audioSourceSimple = new AudioSourceDelayed
			{
				timePlay = delay
			};
			audioSourceSimple.audioClipPromise = promise;
		}
		if (time != 0f)
		{
			audioSource.time = time;
		}
		audioSourceSimple.type = soundType;
		audioSourceSimple.by = by;
		audioSourceSimple.isVoice = false;
		audioSourceSimple.priority = priority;
		audioSourceSimple.individualVolumeFactor = volume;
		audioSourceSimple.source = audioSource;
		this.UpdateVolume(audioSourceSimple);
		this.sourcesActive.Add(audioSourceSimple);
	}

	public void PlayLoopedInfinite(SoundType soundType, string by, float priority, float volume, AudioClip clip, float time = 0f)
	{
		AudioSource audioSource = this.GetAudioSource();
		audioSource.loop = true;
		audioSource.clip = clip;
		audioSource.time = 0f;
		audioSource.Play();
		if (time != 0f)
		{
			audioSource.time = time;
		}
		AudioSourceSimple audioSourceSimple = new AudioSourceSimple();
		audioSourceSimple.type = soundType;
		audioSourceSimple.by = by;
		audioSourceSimple.isVoice = false;
		audioSourceSimple.priority = priority;
		audioSourceSimple.individualVolumeFactor = volume;
		audioSourceSimple.source = audioSource;
		audioSourceSimple.timeFadeOut = float.PositiveInfinity;
		this.UpdateVolume(audioSourceSimple);
		this.sourcesActive.Add(audioSourceSimple);
	}

	public void PlayLooped(SoundType soundType, string by, float priority, float volume, AudioClipPromise promise, float fadeInStart, float fadeInEnd, float fadeOutStart, float fadeOutEnd)
	{
		AudioSourceLoop audioSourceLoop = new AudioSourceLoop();
		AudioSource audioSource = this.GetAudioSource();
		AudioClip clip = promise.Clip;
		audioSource.volume = 0f;
		audioSource.time = 0f;
		audioSource.loop = true;
		if (clip != null)
		{
			audioSource.clip = clip;
		}
		else
		{
			audioSourceLoop.audioClipPromise = promise;
		}
		audioSourceLoop.type = soundType;
		audioSourceLoop.by = by;
		audioSourceLoop.isVoice = false;
		audioSourceLoop.priority = priority;
		audioSourceLoop.individualVolumeFactor = volume;
		audioSourceLoop.source = audioSource;
		audioSourceLoop.fadeInStart = fadeInStart;
		audioSourceLoop.fadeInEnd = fadeInEnd;
		audioSourceLoop.fadeOutStart = fadeOutStart;
		audioSourceLoop.fadeOutEnd = fadeOutEnd;
		this.UpdateVolume(audioSourceLoop);
		this.sourcesActive.Add(audioSourceLoop);
	}

	public void PlayLooped(SoundType soundType, string by, float priority, float volume, AudioClip clip, float fadeInStart, float fadeInEnd, float fadeOutStart, float fadeOutEnd)
	{
		AudioSource audioSource = this.GetAudioSource();
		audioSource.clip = clip;
		audioSource.time = 0f;
		audioSource.volume = 0f;
		audioSource.loop = true;
		AudioSourceLoop audioSourceLoop = new AudioSourceLoop();
		audioSourceLoop.type = soundType;
		audioSourceLoop.by = by;
		audioSourceLoop.isVoice = false;
		audioSourceLoop.priority = priority;
		audioSourceLoop.individualVolumeFactor = volume;
		audioSourceLoop.source = audioSource;
		audioSourceLoop.fadeInStart = fadeInStart;
		audioSourceLoop.fadeInEnd = fadeInEnd;
		audioSourceLoop.fadeOutStart = fadeOutStart;
		audioSourceLoop.fadeOutEnd = fadeOutEnd;
		this.UpdateVolume(audioSourceLoop);
		this.sourcesActive.Add(audioSourceLoop);
	}

	public void PlayDelayed(SoundType soundType, string by, float priority, float volume, AudioClipPromise promise, float time)
	{
		if (this.sourcesActive.Count >= 20)
		{
			return;
		}
		AudioSourceDelayed audioSourceDelayed = new AudioSourceDelayed();
		AudioClip clip = promise.Clip;
		AudioSource audioSource = this.GetAudioSource();
		audioSource.time = 0f;
		if (clip != null)
		{
			audioSource.clip = clip;
		}
		else
		{
			audioSourceDelayed.audioClipPromise = promise;
		}
		audioSourceDelayed.type = soundType;
		audioSourceDelayed.by = by;
		audioSourceDelayed.isVoice = false;
		audioSourceDelayed.priority = priority;
		audioSourceDelayed.individualVolumeFactor = volume;
		audioSourceDelayed.source = audioSource;
		audioSourceDelayed.timePlay = time;
		this.UpdateVolume(audioSourceDelayed);
		this.sourcesActive.Add(audioSourceDelayed);
	}

	public void PlayDelayed(SoundType soundType, string by, float priority, float volume, AudioClip clip, float time)
	{
		if (this.sourcesActive.Count >= 20)
		{
			return;
		}
		AudioSource audioSource = this.GetAudioSource();
		audioSource.clip = clip;
		audioSource.time = 0f;
		AudioSourceDelayed audioSourceDelayed = new AudioSourceDelayed();
		audioSourceDelayed.type = soundType;
		audioSourceDelayed.by = by;
		audioSourceDelayed.isVoice = false;
		audioSourceDelayed.priority = priority;
		audioSourceDelayed.individualVolumeFactor = volume;
		audioSourceDelayed.source = audioSource;
		audioSourceDelayed.timePlay = time;
		this.UpdateVolume(audioSourceDelayed);
		this.sourcesActive.Add(audioSourceDelayed);
	}

	public void PlayMusic(UiManager uiManager, Simulator sim)
	{
		this.CancelBy("music");
		this.CancelBy("musicBoss");
		if (uiManager.IsInHubMenus())
		{
			this.PlayLoopedInfinite(SoundType.MUSIC, "music", 0f, 1f, this.musicMenu, 0f);
		}
		else if (sim.GetCurrentGameMode() == GameMode.STANDARD)
		{
			if (TutorialManager.first >= TutorialManager.First.FIGHT_HERO)
			{
				this.PlayLoopedInfinite(SoundType.MUSIC, "music", 0f, 1f, this.musicStandard, 0f);
			}
			else
			{
				this.PlayLoopedInfinite(SoundType.MUSIC, "music", 0f, 1f, this.ambientForest, 0f);
			}
		}
		else if (sim.GetCurrentGameMode() == GameMode.CRUSADE)
		{
			this.PlayLoopedInfinite(SoundType.MUSIC, "music", 0f, 1f, this.musicTimeChallenge, 0f);
		}
		else if (sim.GetCurrentGameMode() == GameMode.RIFT)
		{
			this.PlayLoopedInfinite(SoundType.MUSIC, "music", 0f, 1f, this.musicTimeChallenge, 0f);
		}
	}

	private float GetVolume(SoundEventSound e)
	{
		float volume = e.sound.GetVolume();
		return volume * this.GetGroupVolumeFactor(e.soundType);
	}

	private float GetVolume(AudioSourceSimple activeSource)
	{
		float individualVolumeFactor = activeSource.individualVolumeFactor;
		return individualVolumeFactor * this.GetGroupVolumeFactor(activeSource.type);
	}

	private float GetGroupVolumeFactor(SoundType type)
	{
		if (type == SoundType.UI)
		{
			return this.volumeUi;
		}
		if (type == SoundType.MUSIC)
		{
			return this.volumeMusic;
		}
		if (type == SoundType.MUSIC_BOSS)
		{
			return this.volumeMusicBoss;
		}
		return this.volumeGameplay;
	}

	public void SetVolumeGameplay(float volumeGameplay)
	{
		this.volumeGameplay = volumeGameplay;
		this.UpdateAllActiveVolumes();
	}

	public void SetVolumeUi(float volumeUi)
	{
		this.volumeUi = volumeUi;
		this.UpdateAllActiveVolumes();
	}

	public void SetVolumeMusic(float volumeMusic)
	{
		if (this.volumeMusic != volumeMusic)
		{
			this.volumeMusic = volumeMusic;
			this.UpdateAllActiveVolumes();
		}
	}

	public void SetVolumeMusicBoss(float volumeMusicBoss)
	{
		if (this.volumeMusicBoss != volumeMusicBoss)
		{
			if (this.volumeMusicBoss == 0f)
			{
				this.PlayLoopedInfinite(SoundType.MUSIC_BOSS, "musicBoss", 0f, 1f, this.musicStandardBoss, 0f);
			}
			else if (volumeMusicBoss == 0f)
			{
				this.CancelBy("musicBoss");
			}
			this.volumeMusicBoss = volumeMusicBoss;
			this.UpdateAllActiveVolumes();
		}
	}

	public void SetGroupVolumes(float volumeGameplay, float volumeUi)
	{
		this.volumeGameplay = volumeGameplay;
		this.volumeUi = volumeUi;
		this.UpdateAllActiveVolumes();
	}

	private void UpdateAllActiveVolumes()
	{
		foreach (AudioSourceSimple activeSource in this.sourcesActive)
		{
			this.UpdateVolume(activeSource);
		}
	}

	private void UpdateVolume(AudioSourceSimple activeSource)
	{
		activeSource.source.volume = this.GetVolume(activeSource);
	}

	private float GetMusicTime()
	{
		foreach (AudioSourceSimple audioSourceSimple in this.sourcesActive)
		{
			if (audioSourceSimple.type == SoundType.MUSIC)
			{
				return audioSourceSimple.source.time;
			}
		}
		return 0f;
	}

	[NonSerialized]
	public bool muteSounds;

	[NonSerialized]
	public bool muteMusic;

	[NonSerialized]
	public bool muteVoices;

	private float volumeGameplay = 1f;

	private float volumeUi = 1f;

	private float volumeMusic = 1f;

	private float volumeMusicBoss;

	private List<AudioSource> sourcesPassive = new List<AudioSource>();

	private List<AudioSourceSimple> sourcesActive = new List<AudioSourceSimple>();

	public const float MusicVolumeMultiplierDuringBoss = 0.25f;

	private const int MAX_ACTIVE_SOURCES_ALLOWED = 20;
}
