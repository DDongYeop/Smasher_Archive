using System.Collections;
using UnityEngine;

public class SoundEffect : PoolableMono
{
    private AudioSource _audioSource;

    private void Awake() 
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Init()
    {
    }

    public void SetSound(AudioClip clip)
    {
        StartCoroutine(PushSound());
    }

    private IEnumerator PushSound()
    {
        yield return new WaitForSeconds(_audioSource.clip.length + 0.5f);
        PoolManager.Instance.Push(this);
    }
}
