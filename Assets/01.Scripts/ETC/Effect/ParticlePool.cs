using System.Collections;
using UnityEngine;

public class ParticlePool : PoolableMono
{
    private ParticleSystem _particle;

    private void Awake() 
    {
        _particle = GetComponent<ParticleSystem>();
    }

    public override void Init()
    {
        _particle.Play();
        StartCoroutine(PushParticle());
    }

    private IEnumerator PushParticle()
    {
        yield return new WaitForSeconds(_particle.main.duration + .5f);
        PoolManager.Instance.Push(this);
    }
}
