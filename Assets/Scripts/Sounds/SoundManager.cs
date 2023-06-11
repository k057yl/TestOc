using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{ 
    private AudioSource _audioSource;

    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private AudioClip _shotSound;
    [SerializeField] private AudioClip _deathSound;
    
    private bool _canPlayStepSound = true;
    private float _stepSoundInterval = 0.5f;
    private Coroutine StepSoundCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.enabled = true;
        }
    }

    public void PlayStepSound()
    {
        if (_canPlayStepSound)
        {
            _audioSource.PlayOneShot(_stepSound);
            _canPlayStepSound = false;
            StepSoundCoroutine = StartCoroutine(StepSoundIntervalCoroutine());
        }
    }
    
    private IEnumerator StepSoundIntervalCoroutine()
    {
        yield return new WaitForSeconds(_stepSoundInterval);
        _canPlayStepSound = true;
    }

    private void OnDestroy()
    {
        if (StepSoundCoroutine != null)
        {
            StopCoroutine(StepSoundCoroutine);
        }
    }
    
    public void PlayShotSound()
    {
        _audioSource.PlayOneShot(_shotSound);
    }
    
    public void PlayDeathSound()
    {
        _audioSource.PlayOneShot(_deathSound);
    }
}
