using UnityEngine;

[ExecuteInEditMode]
public class TargetParticles : MonoBehaviour
{
    private static ParticleSystem.Particle[] Particles = new ParticleSystem.Particle[1000];

    [SerializeField] private ParticleSystem _effect = default;
    [SerializeField] private float _delay = 0.1f;

    private Transform _target;
    private int _count;

    private void Awake()
    {
        if (_effect == null)
            enabled = false;
    }

    private void Update()
    {
        if (_effect.isPlaying == false || _target == null)
            return;

        _count = _effect.GetParticles(Particles);

        for (var i = 0; i < _count; i++)
        {
            var particle = Particles[i];

            if (particle.startLifetime - particle.remainingLifetime > _delay)
            {
                var v1 = _effect.transform.TransformPoint(particle.position);
                var v2 = _target.transform.position;

                var targetPosition = (v2 - v1) * (particle.remainingLifetime / (particle.startLifetime - _delay));
                particle.position = _effect.transform.InverseTransformPoint(v2 - targetPosition);
                Particles[i] = particle;
            }
        }

        _effect.SetParticles(Particles, _count);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}