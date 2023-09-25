using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;

namespace DG.Tweening
{
	public sealed class Sequence : Tween
	{
		internal Sequence()
		{
			this.tweenType = TweenType.Sequence;
			this.Reset();
		}

		internal static Sequence DoPrepend(Sequence inSequence, Tween t)
		{
			if (t.loops == -1)
			{
				t.loops = 1;
			}
			float num = t.delay + t.duration * (float)t.loops;
			inSequence.duration += num;
			int count = inSequence._sequencedObjs.Count;
			for (int i = 0; i < count; i++)
			{
				ABSSequentiable abssequentiable = inSequence._sequencedObjs[i];
				abssequentiable.sequencedPosition += num;
				abssequentiable.sequencedEndPosition += num;
			}
			return Sequence.DoInsert(inSequence, t, 0f);
		}

		internal static Sequence DoInsert(Sequence inSequence, Tween t, float atPosition)
		{
			TweenManager.AddActiveTweenToSequence(t);
			atPosition += t.delay;
			inSequence.lastTweenInsertTime = atPosition;
			t.isSequenced = (t.creationLocked = true);
			t.sequenceParent = inSequence;
			if (t.loops == -1)
			{
				t.loops = 1;
			}
			float num = t.duration * (float)t.loops;
			t.autoKill = false;
			t.delay = (t.elapsedDelay = 0f);
			t.delayComplete = true;
			t.isSpeedBased = false;
			t.sequencedPosition = atPosition;
			t.sequencedEndPosition = atPosition + num;
			if (t.sequencedEndPosition > inSequence.duration)
			{
				inSequence.duration = t.sequencedEndPosition;
			}
			inSequence._sequencedObjs.Add(t);
			inSequence.sequencedTweens.Add(t);
			return inSequence;
		}

		internal static Sequence DoAppendInterval(Sequence inSequence, float interval)
		{
			inSequence.lastTweenInsertTime = inSequence.duration;
			inSequence.duration += interval;
			return inSequence;
		}

		internal static Sequence DoPrependInterval(Sequence inSequence, float interval)
		{
			inSequence.lastTweenInsertTime = 0f;
			inSequence.duration += interval;
			int count = inSequence._sequencedObjs.Count;
			for (int i = 0; i < count; i++)
			{
				ABSSequentiable abssequentiable = inSequence._sequencedObjs[i];
				abssequentiable.sequencedPosition += interval;
				abssequentiable.sequencedEndPosition += interval;
			}
			return inSequence;
		}

		internal static Sequence DoInsertCallback(Sequence inSequence, TweenCallback callback, float atPosition)
		{
			inSequence.lastTweenInsertTime = atPosition;
			SequenceCallback sequenceCallback = new SequenceCallback(atPosition, callback);
			ABSSequentiable abssequentiable = sequenceCallback;
			sequenceCallback.sequencedEndPosition = atPosition;
			abssequentiable.sequencedPosition = atPosition;
			inSequence._sequencedObjs.Add(sequenceCallback);
			if (inSequence.duration < atPosition)
			{
				inSequence.duration = atPosition;
			}
			return inSequence;
		}

		internal override void Reset()
		{
			base.Reset();
			this.sequencedTweens.Clear();
			this._sequencedObjs.Clear();
			this.lastTweenInsertTime = 0f;
		}

		internal override bool Validate()
		{
			int count = this.sequencedTweens.Count;
			for (int i = 0; i < count; i++)
			{
				if (!this.sequencedTweens[i].Validate())
				{
					return false;
				}
			}
			return true;
		}

		internal override bool Startup()
		{
			return Sequence.DoStartup(this);
		}

		internal override bool ApplyTween(float prevPosition, int prevCompletedLoops, int newCompletedSteps, bool useInversePosition, UpdateMode updateMode, UpdateNotice updateNotice)
		{
			return Sequence.DoApplyTween(this, prevPosition, prevCompletedLoops, newCompletedSteps, useInversePosition, updateMode);
		}

		internal static void Setup(Sequence s)
		{
			s.autoKill = DOTween.defaultAutoKill;
			s.isRecyclable = DOTween.defaultRecyclable;
			s.isPlaying = (DOTween.defaultAutoPlay == AutoPlay.All || DOTween.defaultAutoPlay == AutoPlay.AutoPlaySequences);
			s.loopType = DOTween.defaultLoopType;
			s.easeType = Ease.Linear;
			s.easeOvershootOrAmplitude = DOTween.defaultEaseOvershootOrAmplitude;
			s.easePeriod = DOTween.defaultEasePeriod;
		}

		internal static bool DoStartup(Sequence s)
		{
			if (s.sequencedTweens.Count == 0 && s._sequencedObjs.Count == 0 && s.onComplete == null && s.onKill == null && s.onPause == null && s.onPlay == null && s.onRewind == null && s.onStart == null && s.onStepComplete == null && s.onUpdate == null)
			{
				return false;
			}
			s.startupDone = true;
			s.fullDuration = ((s.loops <= -1) ? float.PositiveInfinity : (s.duration * (float)s.loops));
			List<ABSSequentiable> sequencedObjs = s._sequencedObjs;
			if (Sequence._003C_003Ef__mg_0024cache0 == null)
			{
				Sequence._003C_003Ef__mg_0024cache0 = new Comparison<ABSSequentiable>(Sequence.SortSequencedObjs);
			}
			sequencedObjs.Sort(Sequence._003C_003Ef__mg_0024cache0);
			if (s.isRelative)
			{
				int count = s.sequencedTweens.Count;
				for (int i = 0; i < count; i++)
				{
					Tween tween = s.sequencedTweens[i];
					if (!s.isBlendable)
					{
						s.sequencedTweens[i].isRelative = true;
					}
				}
			}
			return true;
		}

		internal static bool DoApplyTween(Sequence s, float prevPosition, int prevCompletedLoops, int newCompletedSteps, bool useInversePosition, UpdateMode updateMode)
		{
			float num = prevPosition;
			float num2 = s.position;
			if (s.easeType != Ease.Linear)
			{
				num = s.duration * EaseManager.Evaluate(s.easeType, s.customEase, num, s.duration, s.easeOvershootOrAmplitude, s.easePeriod);
				num2 = s.duration * EaseManager.Evaluate(s.easeType, s.customEase, num2, s.duration, s.easeOvershootOrAmplitude, s.easePeriod);
			}
			float num3 = 0f;
			bool flag = s.loopType == LoopType.Yoyo && ((num >= s.duration) ? (prevCompletedLoops % 2 == 0) : (prevCompletedLoops % 2 != 0));
			if (s.isBackwards)
			{
				flag = !flag;
			}
			float num5;
			if (newCompletedSteps > 0)
			{
				int completedLoops = s.completedLoops;
				float position = s.position;
				int num4 = newCompletedSteps;
				int i = 0;
				num5 = num;
				if (updateMode == UpdateMode.Update)
				{
					while (i < num4)
					{
						if (i > 0)
						{
							num5 = num3;
						}
						else if (flag && !s.isBackwards)
						{
							num5 = s.duration - num5;
						}
						num3 = ((!flag) ? s.duration : 0f);
						if (Sequence.ApplyInternalCycle(s, num5, num3, updateMode, useInversePosition, flag, true))
						{
							return true;
						}
						i++;
						if (s.loopType == LoopType.Yoyo)
						{
							flag = !flag;
						}
					}
					if (completedLoops != s.completedLoops || Math.Abs(position - s.position) > 1.401298E-45f)
					{
						return !s.active;
					}
				}
				else
				{
					if (s.loopType == LoopType.Yoyo && newCompletedSteps % 2 != 0)
					{
						flag = !flag;
						num = s.duration - num;
					}
					newCompletedSteps = 0;
				}
			}
			if (newCompletedSteps == 1 && s.isComplete)
			{
				return false;
			}
			if (newCompletedSteps > 0 && !s.isComplete)
			{
				num5 = ((!useInversePosition) ? 0f : s.duration);
				if (s.loopType == LoopType.Restart && num3 > 0f)
				{
					Sequence.ApplyInternalCycle(s, s.duration, 0f, UpdateMode.Goto, false, false, false);
				}
			}
			else
			{
				num5 = ((!useInversePosition) ? num : (s.duration - num));
			}
			return Sequence.ApplyInternalCycle(s, num5, (!useInversePosition) ? num2 : (s.duration - num2), updateMode, useInversePosition, flag, false);
		}

		private static bool ApplyInternalCycle(Sequence s, float fromPos, float toPos, UpdateMode updateMode, bool useInverse, bool prevPosIsInverse, bool multiCycleStep = false)
		{
			bool flag = toPos < fromPos;
			if (flag)
			{
				int num = s._sequencedObjs.Count - 1;
				for (int i = num; i > -1; i--)
				{
					if (!s.active)
					{
						return true;
					}
					ABSSequentiable abssequentiable = s._sequencedObjs[i];
					if (abssequentiable.sequencedEndPosition >= toPos && abssequentiable.sequencedPosition <= fromPos)
					{
						if (abssequentiable.tweenType == TweenType.Callback)
						{
							if (updateMode == UpdateMode.Update && prevPosIsInverse)
							{
								Tween.OnTweenCallback(abssequentiable.onStart);
							}
						}
						else
						{
							float num2 = toPos - abssequentiable.sequencedPosition;
							if (num2 < 0f)
							{
								num2 = 0f;
							}
							Tween tween = (Tween)abssequentiable;
							if (tween.startupDone)
							{
								tween.isBackwards = true;
								if (TweenManager.Goto(tween, num2, false, updateMode))
								{
									return true;
								}
								if (multiCycleStep && tween.tweenType == TweenType.Sequence)
								{
									if (s.position <= 0f && s.completedLoops == 0)
									{
										tween.position = 0f;
									}
									else
									{
										bool flag2 = s.completedLoops == 0 || (s.isBackwards && (s.completedLoops < s.loops || s.loops == -1));
										if (tween.isBackwards)
										{
											flag2 = !flag2;
										}
										if (useInverse)
										{
											flag2 = !flag2;
										}
										if (s.isBackwards && !useInverse && !prevPosIsInverse)
										{
											flag2 = !flag2;
										}
										tween.position = ((!flag2) ? tween.duration : 0f);
									}
								}
							}
						}
					}
				}
			}
			else
			{
				int count = s._sequencedObjs.Count;
				for (int j = 0; j < count; j++)
				{
					if (!s.active)
					{
						return true;
					}
					ABSSequentiable abssequentiable2 = s._sequencedObjs[j];
					if (abssequentiable2.sequencedPosition <= toPos && abssequentiable2.sequencedEndPosition >= fromPos)
					{
						if (abssequentiable2.tweenType == TweenType.Callback)
						{
							if (updateMode == UpdateMode.Update)
							{
								bool flag3 = (!s.isBackwards && !useInverse && !prevPosIsInverse) || (s.isBackwards && useInverse && !prevPosIsInverse);
								if (flag3)
								{
									Tween.OnTweenCallback(abssequentiable2.onStart);
								}
							}
						}
						else
						{
							float num3 = toPos - abssequentiable2.sequencedPosition;
							if (num3 < 0f)
							{
								num3 = 0f;
							}
							Tween tween2 = (Tween)abssequentiable2;
							if (toPos >= abssequentiable2.sequencedEndPosition)
							{
								if (!tween2.startupDone)
								{
									TweenManager.ForceInit(tween2, true);
								}
								if (num3 < tween2.fullDuration)
								{
									num3 = tween2.fullDuration;
								}
							}
							tween2.isBackwards = false;
							if (TweenManager.Goto(tween2, num3, false, updateMode))
							{
								return true;
							}
							if (multiCycleStep && tween2.tweenType == TweenType.Sequence)
							{
								if (s.position <= 0f && s.completedLoops == 0)
								{
									tween2.position = 0f;
								}
								else
								{
									bool flag4 = s.completedLoops == 0 || (!s.isBackwards && (s.completedLoops < s.loops || s.loops == -1));
									if (tween2.isBackwards)
									{
										flag4 = !flag4;
									}
									if (useInverse)
									{
										flag4 = !flag4;
									}
									if (s.isBackwards && !useInverse && !prevPosIsInverse)
									{
										flag4 = !flag4;
									}
									tween2.position = ((!flag4) ? tween2.duration : 0f);
								}
							}
						}
					}
				}
			}
			return false;
		}

		private static int SortSequencedObjs(ABSSequentiable a, ABSSequentiable b)
		{
			if (a.sequencedPosition > b.sequencedPosition)
			{
				return 1;
			}
			if (a.sequencedPosition < b.sequencedPosition)
			{
				return -1;
			}
			return 0;
		}

		internal readonly List<Tween> sequencedTweens = new List<Tween>();

		private readonly List<ABSSequentiable> _sequencedObjs = new List<ABSSequentiable>();

		internal float lastTweenInsertTime;

		[CompilerGenerated]
		private static Comparison<ABSSequentiable> _003C_003Ef__mg_0024cache0;
	}
}
