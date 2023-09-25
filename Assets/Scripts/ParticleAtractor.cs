using System;
using UnityEngine;

public class ParticleAtractor : MonoBehaviour
{
	private void LateUpdate()
	{
		if (this.particleSystem == null)
		{
			return;
		}
		this.InitializeIfNeeded();
		int particles = this.particleSystem.GetParticles(this.m_Particles);
		Vector3 position = base.transform.position;
		for (int i = 0; i < this.m_Particles.Length; i++)
		{
			float num = this.m_Particles[i].remainingLifetime / this.m_Particles[i].startLifetime;
			Vector3 position2 = this.m_Particles[i].position;
			this.m_Particles[i].position = Vector3.Slerp(this.m_Particles[i].position, position, 1f - num * 1.2f);
		}
		this.particleSystem.SetParticles(this.m_Particles, particles);
	}

	private void InitializeIfNeeded()
	{
		this.particleEmitter = this.particleSystem.emission;
		if (this.m_Particles == null || this.m_Particles.Length < this.particleSystem.main.maxParticles)
		{
			this.m_Particles = new ParticleSystem.Particle[this.particleSystem.main.maxParticles];
		}
	}

	public void SetBrustCount(short count)
	{
		this.particleEmitter = this.particleSystem.emission;
		short num = 10;
		int cycleCount = (int)(count / num);
		if (count < num)
		{
			cycleCount = 1;
		}
		else
		{
			count = num;
		}
		ParticleSystem.Burst burst = new ParticleSystem.Burst(0f, count, count, cycleCount, 0.01f);
		this.particleEmitter.SetBursts(new ParticleSystem.Burst[]
		{
			burst
		});
	}

	public ParticleSystem particleSystem;

	public ParticleSystem.EmissionModule particleEmitter;

	private ParticleSystem.Particle[] m_Particles;

	public float power;
}
