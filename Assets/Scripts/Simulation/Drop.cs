using System;
using DG.Tweening;
using Ui;
using UnityEngine;

namespace Simulation
{
	public abstract class Drop
	{
		public Drop()
		{
		}

		public void InitVel(float durNonExistence, Vector3 startPos, float endPosY, Vector3 velStart)
		{
			this.moveType = Drop.MoveType.VELOCITY;
			this.state = Drop.State.NON_EXISTENCE;
			this.stateTime = 0f;
			this.durNonExistence = durNonExistence;
			this.startPos = startPos;
			this.pos = startPos;
			this.endPosY = endPosY + GameMath.GetRandomFloat(-0.15f, 0.15f, GameMath.RandType.NoSeed);
			if (this.endPosY > 0.28f)
			{
				this.endPosY = 0.28f - GameMath.GetRandomFloat(0f, 0.15f, GameMath.RandType.NoSeed);
			}
			this.vel = velStart;
			this.variation = GameMath.GetRandomInt(0, int.MaxValue, GameMath.RandType.NoSeed);
			this.scale = GameMath.GetRandomFloat(0.85f, 1f, GameMath.RandType.NoSeed);
			this.bounced = 0;
			this.DEBUG_INITED = true;
		}

		public void InitReach(float durNonExistence, Vector3 startPos, Vector3 endPos, Vector3 invPos, float duration, Drop.EaseType easeType, float scaleFactor = 1f)
		{
			this.moveType = Drop.MoveType.REACH;
			this.state = Drop.State.NON_EXISTENCE;
			this.stateTime = 0f;
			this.durNonExistence = durNonExistence;
			this.pos = startPos;
			this.startPos = startPos;
			this.endPos = endPos;
			this.invPos = invPos;
			this.scaleDuration = duration;
			this.duration = duration;
			this.easeType = easeType;
			this.variation = GameMath.GetRandomInt(0, int.MaxValue, GameMath.RandType.NoSeed);
			this.scaleRandomness = GameMath.GetRandomFloat(0.85f, 1f, GameMath.RandType.NoSeed) * scaleFactor;
			this.scale = 0f;
			this.DEBUG_INITED = true;
		}

		public void Update(float dt)
		{
			if (!this.DEBUG_INITED)
			{
				throw new Exception("Not Inited");
			}
			if (this.uiState != UiState.NONE)
			{
				dt = Time.deltaTime;
			}
			this.stateTime += dt;
			if (this.state == Drop.State.NON_EXISTENCE)
			{
				if (this.stateTime >= this.durNonExistence)
				{
					this.state = Drop.State.AIR;
					this.stateTime = 0f;
				}
			}
			else if (this.state == Drop.State.AIR)
			{
				if (this.moveType == Drop.MoveType.VELOCITY)
				{
					this.vel.y = this.vel.y - 9f * dt;
					this.pos += this.vel * dt;
					if (this.pos.y < this.endPosY)
					{
						this.pos.y = this.endPosY;
						this.vel.x = this.vel.x * 0.7f;
						this.vel.y = Mathf.Min(2.5f, -this.vel.y * 0.35f);
						this.bounced++;
						if (this.bounced > 2 || Mathf.Abs(this.vel.y) < 0.07f)
						{
							this.state = Drop.State.GROUND;
							this.stateTime = 0f;
							this.bounced = 0;
						}
					}
				}
				else
				{
					if (this.moveType != Drop.MoveType.REACH)
					{
						throw new NotImplementedException();
					}
					if (this.easeType == Drop.EaseType.LINEAR)
					{
						this.scale = Easing.Linear(Mathf.Min(this.stateTime, this.scaleDuration), 0f, this.scaleRandomness, this.scaleDuration);
						this.pos.x = Easing.Linear(this.stateTime, this.startPos.x, this.endPos.x - this.startPos.x, this.duration);
						this.pos.y = Easing.Linear(this.stateTime, this.startPos.y, this.endPos.y - this.startPos.y, this.duration);
					}
					else if (this.easeType == Drop.EaseType.EASE_OUT)
					{
						this.scale = Easing.CubicEaseOut(Mathf.Min(this.stateTime, this.scaleDuration), 0f, this.scaleRandomness, this.scaleDuration);
						this.pos.x = Easing.CubicEaseOut(this.stateTime, this.startPos.x, this.endPos.x - this.startPos.x, this.duration);
						this.pos.y = Easing.CubicEaseOut(this.stateTime, this.startPos.y, this.endPos.y - this.startPos.y, this.duration);
					}
					else if (this.easeType == Drop.EaseType.EASE_IN_OUT)
					{
						this.scale = Easing.CubicEaseInOut(Mathf.Min(this.stateTime, this.scaleDuration), 0f, this.scaleRandomness, this.scaleDuration);
						this.pos.x = Easing.CubicEaseInOut(this.stateTime, this.startPos.x, this.endPos.x - this.startPos.x, this.duration);
						this.pos.y = Easing.CubicEaseInOut(this.stateTime, this.startPos.y, this.endPos.y - this.startPos.y, this.duration);
					}
					if (this.stateTime > this.duration)
					{
						this.state = Drop.State.GROUND;
						this.stateTime = 0f;
					}
				}
			}
			else if (this.state == Drop.State.GROUND)
			{
				this.stateTime += dt;
				if (this.stateTime > this.durStayOnGround)
				{
					this.state = Drop.State.FLY_TO_INV;
					this.stateTime = 0f;
					this.startPos = this.pos;
					if (this.pos.x > this.invPos.x)
					{
						this.targetRotation = -this.targetRotation;
					}
				}
			}
			else if (this.state == Drop.State.FLY_TO_INV)
			{
				this.stateTime += dt;
				float z = this.pos.z;
				if (this.easeType == Drop.EaseType.LINEAR)
				{
					this.pos.x = Easing.Linear(this.stateTime, this.startPos.x, this.invPos.x - this.startPos.x, this.durFlyToInv);
					this.pos.y = Easing.Linear(this.stateTime, this.startPos.y, this.invPos.y - this.startPos.y, this.durFlyToInv);
					this.pos.z = this.invPos.z;
				}
				else if (this.easeType == Drop.EaseType.EASE_OUT)
				{
					this.pos.x = Easing.CubicEaseOut(this.stateTime, this.startPos.x, this.invPos.x - this.startPos.x, this.durFlyToInv);
					this.pos.y = Easing.CubicEaseOut(this.stateTime, this.startPos.y, this.invPos.y - this.startPos.y, this.durFlyToInv);
					this.pos.z = this.invPos.z;
				}
				else if (this.easeType == Drop.EaseType.EASE_IN_OUT)
				{
					this.pos.x = Easing.CubicEaseInOut(this.stateTime, this.startPos.x, this.invPos.x - this.startPos.x, this.durFlyToInv);
					this.pos.y = Easing.CubicEaseInOut(this.stateTime, this.startPos.y, this.invPos.y - this.startPos.y, this.durFlyToInv);
					this.pos.z = this.invPos.z;
				}
				if (this.pos.z == 0f)
				{
					this.pos.z = z;
				}
				this.rotation = this.targetRotation * (this.stateTime / this.durFlyToInv);
				if (this.scaleDurationBeforeReach > 0f && this.durFlyToInv - this.stateTime <= this.scaleDurationBeforeReach + 0.2f)
				{
					this.scale = this.scaleRandomness * (GameMath.GetMaxFloat(this.durFlyToInv - this.stateTime - 0.2f, 0f) / this.scaleDurationBeforeReach);
				}
				if (this.durFlyToInv - this.stateTime <= 0.2f && this.targetToScaleOnReach != null && !this.targetScaleAnimTriggered)
				{
					this.targetScaleAnimTriggered = true;
					DOTween.Sequence().Append(this.targetToScaleOnReach.DOScale(1.3f, 0.025f)).Append(this.targetToScaleOnReach.DOScale(1f, 0.025f).SetEase(Ease.OutBack)).Play<Sequence>();
				}
				if (this.stateTime > this.durFlyToInv)
				{
					this.state = Drop.State.COLLECTED;
					this.stateTime = 0f;
				}
			}
			else if (this.state == Drop.State.COLLECTED)
			{
			}
		}

		public bool IsTimeToCollect()
		{
			return this.state == Drop.State.COLLECTED;
		}

		public bool IsFlyingToInv()
		{
			return this.state == Drop.State.FLY_TO_INV;
		}

		public float GetFlyTimeRatio()
		{
			return this.stateTime / this.GetDurFly();
		}

		public float GetDurFly()
		{
			return 0.8f;
		}

		public abstract void Apply(World world);

		public void CollectManually()
		{
			this.state = Drop.State.FLY_TO_INV;
			this.stateTime = 0f;
			this.startPos = this.pos;
		}

		public const float GRAVITY = 9f;

		public const float DUR_STAY_ON_GROUND = 3f;

		public const float DUR_FLY_TO_INV = 0.8f;

		public Drop.State state;

		public Drop.MoveType moveType;

		public Drop.EaseType easeType;

		public float durNonExistence;

		public float duration;

		public float scaleDuration;

		public Vector3 startPos;

		public Vector3 endPos;

		public Vector3 invPos;

		public Vector3 pos;

		public float rotation;

		public Vector3 vel;

		public bool showSideCurrency = true;

		public float endPosY;

		public float stateTime;

		public float durStayOnGround = 3f;

		public float durFlyToInv = 0.8f;

		public int bounced;

		public int variation;

		public float scale;

		public float scaleRandomness;

		private bool DEBUG_INITED;

		public UiState uiState;

		public float zOffsetInventory;

		public Transform targetToScaleOnReach;

		public float scaleDurationBeforeReach;

		private bool targetScaleAnimTriggered;

		protected const float MAX_ROTATION = 80f;

		protected float targetRotation;

		public enum MoveType
		{
			VELOCITY,
			REACH
		}

		public enum EaseType
		{
			LINEAR,
			EASE_OUT,
			EASE_IN_OUT
		}

		public enum State
		{
			NON_EXISTENCE,
			AIR,
			GROUND,
			FLY_TO_INV,
			COLLECTED
		}
	}
}
