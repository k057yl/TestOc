using System.Collections;
using UnityEngine;

using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioClip _deadSound;
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioClip _takeItemSound;
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private AudioClip _jumpSound;
    
    private AudioSource _audioSource;
    
    private bool _canPlayStepSound = true;
    private float _stepSoundInterval = 0.5f;
    private Coroutine StepSoundCoroutine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayDeadSound()
    {
        _audioSource.PlayOneShot(_deadSound);
    }

    public void PlayShootSound()
    {
        _audioSource.PlayOneShot(_shootSound);
    }
    
    public void PlayItemSound()
    {
        _audioSource.PlayOneShot(_takeItemSound);
    }
    
    public void PlayDamageSound()
    {
        _audioSource.PlayOneShot(_damageSound);
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
    
    public void PlayJumpSound()
    {
        _audioSource.PlayOneShot(_jumpSound);
    }
}