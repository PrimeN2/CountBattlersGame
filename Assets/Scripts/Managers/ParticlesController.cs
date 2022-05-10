using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _particles;

    public void PauseParticles()
    {
        foreach (var particle in _particles)
            particle.Pause();
    }

    public void ContinueParticles()
    {
        foreach (var particle in _particles)
            particle.Play();
    }

    public void RemoveParticles()
    {
        foreach (var particle in _particles)
        {
            particle.Clear();
            particle.Pause();
        }
    }

}
