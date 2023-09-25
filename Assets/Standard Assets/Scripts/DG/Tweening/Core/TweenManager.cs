using System;
using System.Collections.Generic;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Core
{
	public static class TweenManager
	{
		internal static TweenerCore<T1, T2, TPlugOptions> GetTweener<T1, T2, TPlugOptions>() where TPlugOptions : struct, IPlugOptions
		{
			TweenerCore<T1, T2, TPlugOptions> tweenerCore;
			if (TweenManager.totPooledTweeners > 0)
			{
				Type typeFromHandle = typeof(T1);
				Type typeFromHandle2 = typeof(T2);
				Type typeFromHandle3 = typeof(TPlugOptions);
				for (int i = TweenManager._maxPooledTweenerId; i > TweenManager._minPooledTweenerId - 1; i--)
				{
					Tween tween = TweenManager._pooledTweeners[i];
					if (tween != null && tween.typeofT1 == typeFromHandle && tween.typeofT2 == typeFromHandle2 && tween.typeofTPlugOptions == typeFromHandle3)
					{
						tweenerCore = (TweenerCore<T1, T2, TPlugOptions>)tween;
						TweenManager.AddActiveTween(tweenerCore);
						TweenManager._pooledTweeners[i] = null;
						if (TweenManager._maxPooledTweenerId != TweenManager._minPooledTweenerId)
						{
							if (i == TweenManager._maxPooledTweenerId)
							{
								TweenManager._maxPooledTweenerId--;
							}
							else if (i == TweenManager._minPooledTweenerId)
							{
								TweenManager._minPooledTweenerId++;
							}
						}
						TweenManager.totPooledTweeners--;
						return tweenerCore;
					}
				}
				if (TweenManager.totTweeners >= TweenManager.maxTweeners)
				{
					TweenManager._pooledTweeners[TweenManager._maxPooledTweenerId] = null;
					TweenManager._maxPooledTweenerId--;
					TweenManager.totPooledTweeners--;
					TweenManager.totTweeners--;
				}
			}
			else if (TweenManager.totTweeners >= TweenManager.maxTweeners - 1)
			{
				int num = TweenManager.maxTweeners;
				int num2 = TweenManager.maxSequences;
				TweenManager.IncreaseCapacities(TweenManager.CapacityIncreaseMode.TweenersOnly);
				if (Debugger.logPriority >= 1)
				{
					Debugger.LogWarning("Max Tweens reached: capacity has automatically been increased from #0 to #1. Use DOTween.SetTweensCapacity to set it manually at startup".Replace("#0", num + "/" + num2).Replace("#1", TweenManager.maxTweeners + "/" + TweenManager.maxSequences));
				}
			}
			tweenerCore = new TweenerCore<T1, T2, TPlugOptions>();
			TweenManager.totTweeners++;
			TweenManager.AddActiveTween(tweenerCore);
			return tweenerCore;
		}

		internal static Sequence GetSequence()
		{
			Sequence sequence;
			if (TweenManager.totPooledSequences > 0)
			{
				sequence = (Sequence)TweenManager._PooledSequences.Pop();
				TweenManager.AddActiveTween(sequence);
				TweenManager.totPooledSequences--;
				return sequence;
			}
			if (TweenManager.totSequences >= TweenManager.maxSequences - 1)
			{
				int num = TweenManager.maxTweeners;
				int num2 = TweenManager.maxSequences;
				TweenManager.IncreaseCapacities(TweenManager.CapacityIncreaseMode.SequencesOnly);
				if (Debugger.logPriority >= 1)
				{
					Debugger.LogWarning("Max Tweens reached: capacity has automatically been increased from #0 to #1. Use DOTween.SetTweensCapacity to set it manually at startup".Replace("#0", num + "/" + num2).Replace("#1", TweenManager.maxTweeners + "/" + TweenManager.maxSequences));
				}
			}
			sequence = new Sequence();
			TweenManager.totSequences++;
			TweenManager.AddActiveTween(sequence);
			return sequence;
		}

		internal static void SetUpdateType(Tween t, UpdateType updateType, bool isIndependentUpdate)
		{
			if (!t.active || t.updateType == updateType)
			{
				t.updateType = updateType;
				t.isIndependentUpdate = isIndependentUpdate;
				return;
			}
			if (t.updateType == UpdateType.Normal)
			{
				TweenManager.totActiveDefaultTweens--;
				TweenManager.hasActiveDefaultTweens = (TweenManager.totActiveDefaultTweens > 0);
			}
			else
			{
				UpdateType updateType2 = t.updateType;
				if (updateType2 != UpdateType.Fixed)
				{
					if (updateType2 != UpdateType.Late)
					{
						TweenManager.totActiveManualTweens--;
						TweenManager.hasActiveManualTweens = (TweenManager.totActiveManualTweens > 0);
					}
					else
					{
						TweenManager.totActiveLateTweens--;
						TweenManager.hasActiveLateTweens = (TweenManager.totActiveLateTweens > 0);
					}
				}
				else
				{
					TweenManager.totActiveFixedTweens--;
					TweenManager.hasActiveFixedTweens = (TweenManager.totActiveFixedTweens > 0);
				}
			}
			t.updateType = updateType;
			t.isIndependentUpdate = isIndependentUpdate;
			if (updateType == UpdateType.Normal)
			{
				TweenManager.totActiveDefaultTweens++;
				TweenManager.hasActiveDefaultTweens = true;
			}
			else if (updateType != UpdateType.Fixed)
			{
				if (updateType != UpdateType.Late)
				{
					TweenManager.totActiveManualTweens++;
					TweenManager.hasActiveManualTweens = true;
				}
				else
				{
					TweenManager.totActiveLateTweens++;
					TweenManager.hasActiveLateTweens = true;
				}
			}
			else
			{
				TweenManager.totActiveFixedTweens++;
				TweenManager.hasActiveFixedTweens = true;
			}
		}

		internal static void AddActiveTweenToSequence(Tween t)
		{
			TweenManager.RemoveActiveTween(t);
		}

		internal static int DespawnAll()
		{
			int result = TweenManager.totActiveTweens;
			for (int i = 0; i < TweenManager._maxActiveLookupId + 1; i++)
			{
				Tween tween = TweenManager._activeTweens[i];
				if (tween != null)
				{
					TweenManager.Despawn(tween, false);
				}
			}
			TweenManager.ClearTweenArray(TweenManager._activeTweens);
			TweenManager.hasActiveTweens = (TweenManager.hasActiveDefaultTweens = (TweenManager.hasActiveLateTweens = (TweenManager.hasActiveFixedTweens = (TweenManager.hasActiveManualTweens = false))));
			TweenManager.totActiveTweens = (TweenManager.totActiveDefaultTweens = (TweenManager.totActiveLateTweens = (TweenManager.totActiveFixedTweens = (TweenManager.totActiveManualTweens = 0))));
			TweenManager.totActiveTweeners = (TweenManager.totActiveSequences = 0);
			TweenManager._maxActiveLookupId = (TweenManager._reorganizeFromId = -1);
			TweenManager._requiresActiveReorganization = false;
			if (TweenManager.isUpdateLoop)
			{
				TweenManager._despawnAllCalledFromUpdateLoopCallback = true;
			}
			return result;
		}

		internal static void Despawn(Tween t, bool modifyActiveLists = true)
		{
			if (t.onKill != null)
			{
				Tween.OnTweenCallback(t.onKill);
			}
			if (modifyActiveLists)
			{
				TweenManager.RemoveActiveTween(t);
			}
			if (t.isRecyclable)
			{
				TweenType tweenType = t.tweenType;
				if (tweenType != TweenType.Sequence)
				{
					if (tweenType == TweenType.Tweener)
					{
						if (TweenManager._maxPooledTweenerId == -1)
						{
							TweenManager._maxPooledTweenerId = TweenManager.maxTweeners - 1;
							TweenManager._minPooledTweenerId = TweenManager.maxTweeners - 1;
						}
						if (TweenManager._maxPooledTweenerId < TweenManager.maxTweeners - 1)
						{
							TweenManager._pooledTweeners[TweenManager._maxPooledTweenerId + 1] = t;
							TweenManager._maxPooledTweenerId++;
							if (TweenManager._minPooledTweenerId > TweenManager._maxPooledTweenerId)
							{
								TweenManager._minPooledTweenerId = TweenManager._maxPooledTweenerId;
							}
						}
						else
						{
							for (int i = TweenManager._maxPooledTweenerId; i > -1; i--)
							{
								if (TweenManager._pooledTweeners[i] == null)
								{
									TweenManager._pooledTweeners[i] = t;
									if (i < TweenManager._minPooledTweenerId)
									{
										TweenManager._minPooledTweenerId = i;
									}
									if (TweenManager._maxPooledTweenerId < TweenManager._minPooledTweenerId)
									{
										TweenManager._maxPooledTweenerId = TweenManager._minPooledTweenerId;
									}
									break;
								}
							}
						}
						TweenManager.totPooledTweeners++;
					}
				}
				else
				{
					TweenManager._PooledSequences.Push(t);
					TweenManager.totPooledSequences++;
					Sequence sequence = (Sequence)t;
					int count = sequence.sequencedTweens.Count;
					for (int j = 0; j < count; j++)
					{
						TweenManager.Despawn(sequence.sequencedTweens[j], false);
					}
				}
			}
			else
			{
				TweenType tweenType2 = t.tweenType;
				if (tweenType2 != TweenType.Sequence)
				{
					if (tweenType2 == TweenType.Tweener)
					{
						TweenManager.totTweeners--;
					}
				}
				else
				{
					TweenManager.totSequences--;
					Sequence sequence2 = (Sequence)t;
					int count2 = sequence2.sequencedTweens.Count;
					for (int k = 0; k < count2; k++)
					{
						TweenManager.Despawn(sequence2.sequencedTweens[k], false);
					}
				}
			}
			t.active = false;
			t.Reset();
		}

		internal static void PurgeAll()
		{
			for (int i = 0; i < TweenManager.totActiveTweens; i++)
			{
				Tween tween = TweenManager._activeTweens[i];
				if (tween != null)
				{
					tween.active = false;
					if (tween.onKill != null)
					{
						Tween.OnTweenCallback(tween.onKill);
					}
				}
			}
			TweenManager.ClearTweenArray(TweenManager._activeTweens);
			TweenManager.hasActiveTweens = (TweenManager.hasActiveDefaultTweens = (TweenManager.hasActiveLateTweens = (TweenManager.hasActiveFixedTweens = (TweenManager.hasActiveManualTweens = false))));
			TweenManager.totActiveTweens = (TweenManager.totActiveDefaultTweens = (TweenManager.totActiveLateTweens = (TweenManager.totActiveFixedTweens = (TweenManager.totActiveManualTweens = 0))));
			TweenManager.totActiveTweeners = (TweenManager.totActiveSequences = 0);
			TweenManager._maxActiveLookupId = (TweenManager._reorganizeFromId = -1);
			TweenManager._requiresActiveReorganization = false;
			TweenManager.PurgePools();
			TweenManager.ResetCapacities();
			TweenManager.totTweeners = (TweenManager.totSequences = 0);
		}

		internal static void PurgePools()
		{
			TweenManager.totTweeners -= TweenManager.totPooledTweeners;
			TweenManager.totSequences -= TweenManager.totPooledSequences;
			TweenManager.ClearTweenArray(TweenManager._pooledTweeners);
			TweenManager._PooledSequences.Clear();
			TweenManager.totPooledTweeners = (TweenManager.totPooledSequences = 0);
			TweenManager._minPooledTweenerId = (TweenManager._maxPooledTweenerId = -1);
		}

		internal static void ResetCapacities()
		{
			TweenManager.SetCapacities(200, 50);
		}

		internal static void SetCapacities(int tweenersCapacity, int sequencesCapacity)
		{
			if (tweenersCapacity < sequencesCapacity)
			{
				tweenersCapacity = sequencesCapacity;
			}
			TweenManager.maxActive = tweenersCapacity + sequencesCapacity;
			TweenManager.maxTweeners = tweenersCapacity;
			TweenManager.maxSequences = sequencesCapacity;
			Array.Resize<Tween>(ref TweenManager._activeTweens, TweenManager.maxActive);
			Array.Resize<Tween>(ref TweenManager._pooledTweeners, tweenersCapacity);
			TweenManager._KillList.Capacity = TweenManager.maxActive;
		}

		internal static int Validate()
		{
			if (TweenManager._requiresActiveReorganization)
			{
				TweenManager.ReorganizeActiveTweens();
			}
			int num = 0;
			for (int i = 0; i < TweenManager._maxActiveLookupId + 1; i++)
			{
				Tween tween = TweenManager._activeTweens[i];
				if (!tween.Validate())
				{
					num++;
					TweenManager.MarkForKilling(tween);
				}
			}
			if (num > 0)
			{
				TweenManager.DespawnActiveTweens(TweenManager._KillList);
				TweenManager._KillList.Clear();
			}
			return num;
		}

		internal static void Update(UpdateType updateType, float deltaTime, float independentTime)
		{
			if (TweenManager._requiresActiveReorganization)
			{
				TweenManager.ReorganizeActiveTweens();
			}
			TweenManager.isUpdateLoop = true;
			bool flag = false;
			int num = TweenManager._maxActiveLookupId + 1;
			for (int i = 0; i < num; i++)
			{
				Tween tween = TweenManager._activeTweens[i];
				if (tween != null && tween.updateType == updateType)
				{
					if (!tween.active)
					{
						flag = true;
						TweenManager.MarkForKilling(tween);
					}
					else if (tween.isPlaying)
					{
						tween.creationLocked = true;
						float num2 = ((!tween.isIndependentUpdate) ? deltaTime : independentTime) * tween.timeScale;
						if (num2 > 0f)
						{
							if (!tween.delayComplete)
							{
								num2 = tween.UpdateDelay(tween.elapsedDelay + num2);
								if (num2 <= -1f)
								{
									flag = true;
									TweenManager.MarkForKilling(tween);
									goto IL_283;
								}
								if (num2 <= 0f)
								{
									goto IL_283;
								}
								if (tween.playedOnce && tween.onPlay != null)
								{
									Tween.OnTweenCallback(tween.onPlay);
								}
							}
							if (!tween.startupDone && !tween.Startup())
							{
								flag = true;
								TweenManager.MarkForKilling(tween);
							}
							else
							{
								float num3 = tween.position;
								bool flag2 = num3 >= tween.duration;
								int num4 = tween.completedLoops;
								if (tween.duration <= 0f)
								{
									num3 = 0f;
									num4 = ((tween.loops != -1) ? tween.loops : (tween.completedLoops + 1));
								}
								else
								{
									if (tween.isBackwards)
									{
										num3 -= num2;
										while (num3 < 0f && num4 > -1)
										{
											num3 += tween.duration;
											num4--;
										}
										if (num4 < 0 || (flag2 && num4 < 1))
										{
											num3 = 0f;
											num4 = ((!flag2) ? 0 : 1);
										}
									}
									else
									{
										num3 += num2;
										while (num3 >= tween.duration && (tween.loops == -1 || num4 < tween.loops))
										{
											num3 -= tween.duration;
											num4++;
										}
									}
									if (flag2)
									{
										num4--;
									}
									if (tween.loops != -1 && num4 >= tween.loops)
									{
										num3 = tween.duration;
									}
								}
								bool flag3 = Tween.DoGoto(tween, num3, num4, UpdateMode.Update);
								if (flag3)
								{
									flag = true;
									TweenManager.MarkForKilling(tween);
								}
							}
						}
					}
				}
				IL_283:;
			}
			if (flag)
			{
				if (TweenManager._despawnAllCalledFromUpdateLoopCallback)
				{
					TweenManager._despawnAllCalledFromUpdateLoopCallback = false;
				}
				else
				{
					TweenManager.DespawnActiveTweens(TweenManager._KillList);
				}
				TweenManager._KillList.Clear();
			}
			TweenManager.isUpdateLoop = false;
		}

		internal static int FilteredOperation(OperationType operationType, FilterType filterType, object id, bool optionalBool, float optionalFloat, object optionalObj = null, object[] optionalArray = null)
		{
			int num = 0;
			bool flag = false;
			int num2 = (optionalArray != null) ? optionalArray.Length : 0;
			bool flag2 = false;
			string b = null;
			bool flag3 = false;
			int num3 = 0;
			if (filterType == FilterType.TargetOrId || filterType == FilterType.TargetAndId)
			{
				if (id is string)
				{
					flag2 = true;
					b = (string)id;
				}
				else if (id is int)
				{
					flag3 = true;
					num3 = (int)id;
				}
			}
			for (int i = TweenManager._maxActiveLookupId; i > -1; i--)
			{
				Tween tween = TweenManager._activeTweens[i];
				if (tween != null && tween.active)
				{
					bool flag4 = false;
					switch (filterType)
					{
					case FilterType.All:
						flag4 = true;
						break;
					case FilterType.TargetOrId:
						if (flag2)
						{
							flag4 = (tween.stringId != null && tween.stringId == b);
						}
						else if (flag3)
						{
							flag4 = (tween.intId == num3);
						}
						else
						{
							flag4 = ((tween.id != null && id.Equals(tween.id)) || (tween.target != null && id.Equals(tween.target)));
						}
						break;
					case FilterType.TargetAndId:
						if (flag2)
						{
							flag4 = (tween.target != null && tween.stringId == b && optionalObj != null && optionalObj.Equals(tween.target));
						}
						else if (flag3)
						{
							flag4 = (tween.target != null && tween.intId == num3 && optionalObj != null && optionalObj.Equals(tween.target));
						}
						else
						{
							flag4 = (tween.id != null && tween.target != null && optionalObj != null && id.Equals(tween.id) && optionalObj.Equals(tween.target));
						}
						break;
					case FilterType.AllExceptTargetsOrIds:
						flag4 = true;
						for (int j = 0; j < num2; j++)
						{
							object obj = optionalArray[j];
							if (flag2 && tween.stringId == b)
							{
								flag4 = false;
								break;
							}
							if (flag3 && tween.intId == num3)
							{
								flag4 = false;
								break;
							}
							if ((tween.id != null && obj.Equals(tween.id)) || (tween.target != null && obj.Equals(tween.target)))
							{
								flag4 = false;
								break;
							}
						}
						break;
					}
					if (flag4)
					{
						switch (operationType)
						{
						case OperationType.Complete:
						{
							bool autoKill = tween.autoKill;
							if (TweenManager.Complete(tween, false, (optionalFloat <= 0f) ? UpdateMode.Goto : UpdateMode.Update))
							{
								num += (optionalBool ? ((!autoKill) ? 0 : 1) : 1);
								if (autoKill)
								{
									if (TweenManager.isUpdateLoop)
									{
										tween.active = false;
									}
									else
									{
										flag = true;
										TweenManager._KillList.Add(tween);
									}
								}
							}
							break;
						}
						case OperationType.Despawn:
							num++;
							tween.active = false;
							if (!TweenManager.isUpdateLoop)
							{
								TweenManager.Despawn(tween, false);
								flag = true;
								TweenManager._KillList.Add(tween);
							}
							break;
						case OperationType.Flip:
							if (TweenManager.Flip(tween))
							{
								num++;
							}
							break;
						case OperationType.Goto:
							TweenManager.Goto(tween, optionalFloat, optionalBool, UpdateMode.Goto);
							num++;
							break;
						case OperationType.Pause:
							if (TweenManager.Pause(tween))
							{
								num++;
							}
							break;
						case OperationType.Play:
							if (TweenManager.Play(tween))
							{
								num++;
							}
							break;
						case OperationType.PlayForward:
							if (TweenManager.PlayForward(tween))
							{
								num++;
							}
							break;
						case OperationType.PlayBackwards:
							if (TweenManager.PlayBackwards(tween))
							{
								num++;
							}
							break;
						case OperationType.Rewind:
							if (TweenManager.Rewind(tween, optionalBool))
							{
								num++;
							}
							break;
						case OperationType.SmoothRewind:
							if (TweenManager.SmoothRewind(tween))
							{
								num++;
							}
							break;
						case OperationType.Restart:
							if (TweenManager.Restart(tween, optionalBool, optionalFloat))
							{
								num++;
							}
							break;
						case OperationType.TogglePause:
							if (TweenManager.TogglePause(tween))
							{
								num++;
							}
							break;
						case OperationType.IsTweening:
							if ((!tween.isComplete || !tween.autoKill) && (!optionalBool || tween.isPlaying))
							{
								num++;
							}
							break;
						}
					}
				}
			}
			if (flag)
			{
				int num4 = TweenManager._KillList.Count - 1;
				for (int k = num4; k > -1; k--)
				{
					Tween tween2 = TweenManager._KillList[k];
					if (tween2.activeId != -1)
					{
						TweenManager.RemoveActiveTween(tween2);
					}
				}
				TweenManager._KillList.Clear();
			}
			return num;
		}

		internal static bool Complete(Tween t, bool modifyActiveLists = true, UpdateMode updateMode = UpdateMode.Goto)
		{
			if (t.loops == -1)
			{
				return false;
			}
			if (!t.isComplete)
			{
				Tween.DoGoto(t, t.duration, t.loops, updateMode);
				t.isPlaying = false;
				if (t.autoKill)
				{
					if (TweenManager.isUpdateLoop)
					{
						t.active = false;
					}
					else
					{
						TweenManager.Despawn(t, modifyActiveLists);
					}
				}
				return true;
			}
			return false;
		}

		internal static bool Flip(Tween t)
		{
			t.isBackwards = !t.isBackwards;
			return true;
		}

		internal static void ForceInit(Tween t, bool isSequenced = false)
		{
			if (t.startupDone)
			{
				return;
			}
			if (!t.Startup() && !isSequenced)
			{
				if (TweenManager.isUpdateLoop)
				{
					t.active = false;
				}
				else
				{
					TweenManager.RemoveActiveTween(t);
				}
			}
		}

		internal static bool Goto(Tween t, float to, bool andPlay = false, UpdateMode updateMode = UpdateMode.Goto)
		{
			bool isPlaying = t.isPlaying;
			t.isPlaying = andPlay;
			t.delayComplete = true;
			t.elapsedDelay = t.delay;
			int num = Mathf.FloorToInt(to / t.duration);
			float num2 = to % t.duration;
			if (t.loops != -1 && num >= t.loops)
			{
				num = t.loops;
				num2 = t.duration;
			}
			else if (num2 >= t.duration)
			{
				num2 = 0f;
			}
			bool flag = Tween.DoGoto(t, num2, num, updateMode);
			if (!andPlay && isPlaying && !flag && t.onPause != null)
			{
				Tween.OnTweenCallback(t.onPause);
			}
			return flag;
		}

		internal static bool Pause(Tween t)
		{
			if (t.isPlaying)
			{
				t.isPlaying = false;
				if (t.onPause != null)
				{
					Tween.OnTweenCallback(t.onPause);
				}
				return true;
			}
			return false;
		}

		internal static bool Play(Tween t)
		{
			if (!t.isPlaying && ((!t.isBackwards && !t.isComplete) || (t.isBackwards && (t.completedLoops > 0 || t.position > 0f))))
			{
				t.isPlaying = true;
				if (t.playedOnce && t.delayComplete && t.onPlay != null)
				{
					Tween.OnTweenCallback(t.onPlay);
				}
				return true;
			}
			return false;
		}

		internal static bool PlayBackwards(Tween t)
		{
			if (!t.isBackwards)
			{
				t.isBackwards = true;
				TweenManager.Play(t);
				return true;
			}
			return TweenManager.Play(t);
		}

		internal static bool PlayForward(Tween t)
		{
			if (t.isBackwards)
			{
				t.isBackwards = false;
				TweenManager.Play(t);
				return true;
			}
			return TweenManager.Play(t);
		}

		internal static bool Restart(Tween t, bool includeDelay = true, float changeDelayTo = -1f)
		{
			bool flag = !t.isPlaying;
			t.isBackwards = false;
			if (changeDelayTo >= 0f)
			{
				t.delay = changeDelayTo;
			}
			TweenManager.Rewind(t, includeDelay);
			t.isPlaying = true;
			if (flag && t.playedOnce && t.delayComplete && t.onPlay != null)
			{
				Tween.OnTweenCallback(t.onPlay);
			}
			return true;
		}

		internal static bool Rewind(Tween t, bool includeDelay = true)
		{
			bool isPlaying = t.isPlaying;
			t.isPlaying = false;
			bool result = false;
			if (t.delay > 0f)
			{
				if (includeDelay)
				{
					result = (t.delay > 0f && t.elapsedDelay > 0f);
					t.elapsedDelay = 0f;
					t.delayComplete = false;
				}
				else
				{
					result = (t.elapsedDelay < t.delay);
					t.elapsedDelay = t.delay;
					t.delayComplete = true;
				}
			}
			if (t.position > 0f || t.completedLoops > 0 || !t.startupDone)
			{
				result = true;
				if (!Tween.DoGoto(t, 0f, 0, UpdateMode.Goto) && isPlaying && t.onPause != null)
				{
					Tween.OnTweenCallback(t.onPause);
				}
			}
			return result;
		}

		internal static bool SmoothRewind(Tween t)
		{
			bool result = false;
			if (t.delay > 0f)
			{
				result = (t.elapsedDelay < t.delay);
				t.elapsedDelay = t.delay;
				t.delayComplete = true;
			}
			if (t.position > 0f || t.completedLoops > 0 || !t.startupDone)
			{
				result = true;
				if (t.loopType == LoopType.Incremental)
				{
					t.PlayBackwards();
				}
				else
				{
					t.Goto(t.ElapsedDirectionalPercentage() * t.duration, false);
					t.PlayBackwards();
				}
			}
			else
			{
				t.isPlaying = false;
			}
			return result;
		}

		internal static bool TogglePause(Tween t)
		{
			if (t.isPlaying)
			{
				return TweenManager.Pause(t);
			}
			return TweenManager.Play(t);
		}

		public static int TotalPooledTweens()
		{
			return TweenManager.totPooledTweeners + TweenManager.totPooledSequences;
		}

		public static int TotalPlayingTweens()
		{
			if (!TweenManager.hasActiveTweens)
			{
				return 0;
			}
			if (TweenManager._requiresActiveReorganization)
			{
				TweenManager.ReorganizeActiveTweens();
			}
			int num = 0;
			for (int i = 0; i < TweenManager._maxActiveLookupId + 1; i++)
			{
				Tween tween = TweenManager._activeTweens[i];
				if (tween != null && tween.isPlaying)
				{
					num++;
				}
			}
			return num;
		}

		internal static List<Tween> GetActiveTweens(bool playing)
		{
			if (TweenManager._requiresActiveReorganization)
			{
				TweenManager.ReorganizeActiveTweens();
			}
			if (TweenManager.totActiveTweens <= 0)
			{
				return null;
			}
			int num = TweenManager.totActiveTweens;
			List<Tween> list = new List<Tween>(num);
			for (int i = 0; i < num; i++)
			{
				Tween tween = TweenManager._activeTweens[i];
				if (tween.isPlaying == playing)
				{
					list.Add(tween);
				}
			}
			if (list.Count > 0)
			{
				return list;
			}
			return null;
		}

		internal static List<Tween> GetTweensById(object id, bool playingOnly)
		{
			if (TweenManager._requiresActiveReorganization)
			{
				TweenManager.ReorganizeActiveTweens();
			}
			if (TweenManager.totActiveTweens <= 0)
			{
				return null;
			}
			int num = TweenManager.totActiveTweens;
			List<Tween> list = new List<Tween>(num);
			for (int i = 0; i < num; i++)
			{
				Tween tween = TweenManager._activeTweens[i];
				if (tween != null && object.Equals(id, tween.id))
				{
					if (!playingOnly || tween.isPlaying)
					{
						list.Add(tween);
					}
				}
			}
			if (list.Count > 0)
			{
				return list;
			}
			return null;
		}

		internal static List<Tween> GetTweensByTarget(object target, bool playingOnly)
		{
			if (TweenManager._requiresActiveReorganization)
			{
				TweenManager.ReorganizeActiveTweens();
			}
			if (TweenManager.totActiveTweens <= 0)
			{
				return null;
			}
			int num = TweenManager.totActiveTweens;
			List<Tween> list = new List<Tween>(num);
			for (int i = 0; i < num; i++)
			{
				Tween tween = TweenManager._activeTweens[i];
				if (tween.target == target)
				{
					if (!playingOnly || tween.isPlaying)
					{
						list.Add(tween);
					}
				}
			}
			if (list.Count > 0)
			{
				return list;
			}
			return null;
		}

		private static void MarkForKilling(Tween t)
		{
			t.active = false;
			TweenManager._KillList.Add(t);
		}

		private static void AddActiveTween(Tween t)
		{
			if (TweenManager._requiresActiveReorganization)
			{
				TweenManager.ReorganizeActiveTweens();
			}
			t.active = true;
			t.updateType = DOTween.defaultUpdateType;
			t.isIndependentUpdate = DOTween.defaultTimeScaleIndependent;
			t.activeId = (TweenManager._maxActiveLookupId = TweenManager.totActiveTweens);
			TweenManager._activeTweens[TweenManager.totActiveTweens] = t;
			if (t.updateType == UpdateType.Normal)
			{
				TweenManager.totActiveDefaultTweens++;
				TweenManager.hasActiveDefaultTweens = true;
			}
			else
			{
				UpdateType updateType = t.updateType;
				if (updateType != UpdateType.Fixed)
				{
					if (updateType != UpdateType.Late)
					{
						TweenManager.totActiveManualTweens++;
						TweenManager.hasActiveManualTweens = true;
					}
					else
					{
						TweenManager.totActiveLateTweens++;
						TweenManager.hasActiveLateTweens = true;
					}
				}
				else
				{
					TweenManager.totActiveFixedTweens++;
					TweenManager.hasActiveFixedTweens = true;
				}
			}
			TweenManager.totActiveTweens++;
			if (t.tweenType == TweenType.Tweener)
			{
				TweenManager.totActiveTweeners++;
			}
			else
			{
				TweenManager.totActiveSequences++;
			}
			TweenManager.hasActiveTweens = true;
		}

		private static void ReorganizeActiveTweens()
		{
			if (TweenManager.totActiveTweens <= 0)
			{
				TweenManager._maxActiveLookupId = -1;
				TweenManager._requiresActiveReorganization = false;
				TweenManager._reorganizeFromId = -1;
				return;
			}
			if (TweenManager._reorganizeFromId == TweenManager._maxActiveLookupId)
			{
				TweenManager._maxActiveLookupId--;
				TweenManager._requiresActiveReorganization = false;
				TweenManager._reorganizeFromId = -1;
				return;
			}
			int num = 1;
			int num2 = TweenManager._maxActiveLookupId + 1;
			TweenManager._maxActiveLookupId = TweenManager._reorganizeFromId - 1;
			for (int i = TweenManager._reorganizeFromId + 1; i < num2; i++)
			{
				Tween tween = TweenManager._activeTweens[i];
				if (tween == null)
				{
					num++;
				}
				else
				{
					tween.activeId = (TweenManager._maxActiveLookupId = i - num);
					TweenManager._activeTweens[i - num] = tween;
					TweenManager._activeTweens[i] = null;
				}
			}
			TweenManager._requiresActiveReorganization = false;
			TweenManager._reorganizeFromId = -1;
		}

		private static void DespawnActiveTweens(List<Tween> tweens)
		{
			int num = tweens.Count - 1;
			for (int i = num; i > -1; i--)
			{
				TweenManager.Despawn(tweens[i], true);
			}
		}

		private static void RemoveActiveTween(Tween t)
		{
			int activeId = t.activeId;
			t.activeId = -1;
			TweenManager._requiresActiveReorganization = true;
			if (TweenManager._reorganizeFromId == -1 || TweenManager._reorganizeFromId > activeId)
			{
				TweenManager._reorganizeFromId = activeId;
			}
			TweenManager._activeTweens[activeId] = null;
			if (t.updateType == UpdateType.Normal)
			{
				if (TweenManager.totActiveDefaultTweens > 0)
				{
					TweenManager.totActiveDefaultTweens--;
					TweenManager.hasActiveDefaultTweens = (TweenManager.totActiveDefaultTweens > 0);
				}
				else
				{
					Debugger.LogRemoveActiveTweenError("totActiveDefaultTweens");
				}
			}
			else
			{
				UpdateType updateType = t.updateType;
				if (updateType != UpdateType.Fixed)
				{
					if (updateType != UpdateType.Late)
					{
						if (TweenManager.totActiveManualTweens > 0)
						{
							TweenManager.totActiveManualTweens--;
							TweenManager.hasActiveManualTweens = (TweenManager.totActiveManualTweens > 0);
						}
						else
						{
							Debugger.LogRemoveActiveTweenError("totActiveManualTweens");
						}
					}
					else if (TweenManager.totActiveLateTweens > 0)
					{
						TweenManager.totActiveLateTweens--;
						TweenManager.hasActiveLateTweens = (TweenManager.totActiveLateTweens > 0);
					}
					else
					{
						Debugger.LogRemoveActiveTweenError("totActiveLateTweens");
					}
				}
				else if (TweenManager.totActiveFixedTweens > 0)
				{
					TweenManager.totActiveFixedTweens--;
					TweenManager.hasActiveFixedTweens = (TweenManager.totActiveFixedTweens > 0);
				}
				else
				{
					Debugger.LogRemoveActiveTweenError("totActiveFixedTweens");
				}
			}
			TweenManager.totActiveTweens--;
			TweenManager.hasActiveTweens = (TweenManager.totActiveTweens > 0);
			if (t.tweenType == TweenType.Tweener)
			{
				TweenManager.totActiveTweeners--;
			}
			else
			{
				TweenManager.totActiveSequences--;
			}
			if (TweenManager.totActiveTweens < 0)
			{
				TweenManager.totActiveTweens = 0;
				Debugger.LogRemoveActiveTweenError("totActiveTweens");
			}
			if (TweenManager.totActiveTweeners < 0)
			{
				TweenManager.totActiveTweeners = 0;
				Debugger.LogRemoveActiveTweenError("totActiveTweeners");
			}
			if (TweenManager.totActiveSequences < 0)
			{
				TweenManager.totActiveSequences = 0;
				Debugger.LogRemoveActiveTweenError("totActiveSequences");
			}
		}

		private static void ClearTweenArray(Tween[] tweens)
		{
			int num = tweens.Length;
			for (int i = 0; i < num; i++)
			{
				tweens[i] = null;
			}
		}

		private static void IncreaseCapacities(TweenManager.CapacityIncreaseMode increaseMode)
		{
			int num = 0;
			int num2 = Mathf.Max((int)((float)TweenManager.maxTweeners * 1.5f), 200);
			int num3 = Mathf.Max((int)((float)TweenManager.maxSequences * 1.5f), 50);
			if (increaseMode != TweenManager.CapacityIncreaseMode.TweenersOnly)
			{
				if (increaseMode != TweenManager.CapacityIncreaseMode.SequencesOnly)
				{
					num += num2;
					TweenManager.maxTweeners += num2;
					TweenManager.maxSequences += num3;
					Array.Resize<Tween>(ref TweenManager._pooledTweeners, TweenManager.maxTweeners);
				}
				else
				{
					num += num3;
					TweenManager.maxSequences += num3;
				}
			}
			else
			{
				num += num2;
				TweenManager.maxTweeners += num2;
				Array.Resize<Tween>(ref TweenManager._pooledTweeners, TweenManager.maxTweeners);
			}
			TweenManager.maxActive = TweenManager.maxTweeners + TweenManager.maxSequences;
			Array.Resize<Tween>(ref TweenManager._activeTweens, TweenManager.maxActive);
			if (num > 0)
			{
				TweenManager._KillList.Capacity += num;
			}
		}

		private const int _DefaultMaxTweeners = 200;

		private const int _DefaultMaxSequences = 50;

		private const string _MaxTweensReached = "Max Tweens reached: capacity has automatically been increased from #0 to #1. Use DOTween.SetTweensCapacity to set it manually at startup";

		public static int maxActive = 250;

		public static int maxTweeners = 200;

		public static int maxSequences = 50;

		public static bool hasActiveTweens;

		public static bool hasActiveDefaultTweens;

		public static bool hasActiveLateTweens;

		public static bool hasActiveFixedTweens;

		public static bool hasActiveManualTweens;

		public static int totActiveTweens;

		public static int totActiveDefaultTweens;

		public static int totActiveLateTweens;

		public static int totActiveFixedTweens;

		public static int totActiveManualTweens;

		public static int totActiveTweeners;

		public static int totActiveSequences;

		public static int totPooledTweeners;

		public static int totPooledSequences;

		public static int totTweeners;

		public static int totSequences;

		public static bool isUpdateLoop;

		public static Tween[] _activeTweens = new Tween[250];

		private static Tween[] _pooledTweeners = new Tween[200];

		private static readonly Stack<Tween> _PooledSequences = new Stack<Tween>();

		private static readonly List<Tween> _KillList = new List<Tween>(250);

		private static int _maxActiveLookupId = -1;

		private static bool _requiresActiveReorganization;

		private static int _reorganizeFromId = -1;

		private static int _minPooledTweenerId = -1;

		private static int _maxPooledTweenerId = -1;

		private static bool _despawnAllCalledFromUpdateLoopCallback;

		internal enum CapacityIncreaseMode
		{
			TweenersAndSequences,
			TweenersOnly,
			SequencesOnly
		}
	}
}
