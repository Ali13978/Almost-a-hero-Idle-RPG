using System;
using UnityEngine;

public class EffectLine : MonoBehaviour
{
	public void Initialize()
	{
		this.destination = GameMath.GetRandomFloat(-1.5f, 1.5f, GameMath.RandType.NoSeed);
		base.transform.position = new Vector3(0f, GameMath.GetRandomFloat(-1.5f, 1.5f, GameMath.RandType.NoSeed), 0f);
		base.transform.localScale = new Vector3(2f, GameMath.GetRandomFloat(0.005f, 0.04f, GameMath.RandType.NoSeed), 1f);
		this.speed = GameMath.GetRandomFloat(this.maxSpeed * 0.25f, this.maxSpeed, GameMath.RandType.NoSeed);
		this.disappearTimer = GameMath.GetRandomFloat(0f, 0.5f, GameMath.RandType.NoSeed);
		this.isDisappeared = (GameMath.GetRandomFloat(0f, 1f, GameMath.RandType.NoSeed) < 0.5f);
		SpriteRenderer component = base.GetComponent<SpriteRenderer>();
		if (this.isDisappeared)
		{
			component.color = new Color(1f, 1f, 1f, 0f);
		}
		else
		{
			component.color = new Color(1f, 1f, 1f, 0.1f);
		}
	}

	public void Advance(float dt)
	{
		float num = this.TweenTo(base.transform.position.y, this.destination, this.maxSpeed * dt);
		base.transform.position = new Vector3(0f, num, 0f);
		if (num == this.destination)
		{
			this.destination = GameMath.GetRandomFloat(-1.5f, 1.5f, GameMath.RandType.NoSeed);
			this.speed = GameMath.GetRandomFloat(this.maxSpeed * 0.25f, this.maxSpeed, GameMath.RandType.NoSeed);
		}
		this.disappearTimer -= dt;
		if (this.disappearTimer < 0f)
		{
			this.disappearTimer = GameMath.GetRandomFloat(0.1f, 0.2f, GameMath.RandType.NoSeed);
			this.isDisappeared = !this.isDisappeared;
			SpriteRenderer component = base.GetComponent<SpriteRenderer>();
			if (this.isDisappeared)
			{
				component.color = new Color(1f, 1f, 1f, 0f);
			}
			else
			{
				component.color = new Color(1f, 1f, 1f, 0.1f);
			}
		}
	}

	public float TweenTo(float current, float destination, float amount)
	{
		float num = destination - current;
		if (amount > Mathf.Abs(num))
		{
			return destination;
		}
		if (num > 0f)
		{
			return current + amount;
		}
		return current - amount;
	}

	public float destination;

	public float maxSpeed;

	public float speed;

	public float scale;

	public float disappearTimer;

	public bool isDisappeared;
}
