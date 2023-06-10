using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : EnemyAbstract
{
    private const float DESTRUCTION_AFTER_DEATH = 5f;
    
    public Animator _anim;
    public Rigidbody[] AllRigidbodies;
    public ParticleSystem Blood;
    
    private NavMeshAgent _navMeshAgent;
    private GameObject _player;

    private void Awake()
    {
        for (int i = 0; i < AllRigidbodies.Length; i++)
        {
            AllRigidbodies[i].isKinematic = true;
        }
    }

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = GameObjectManager.instance.allObject[0];

        _navMeshAgent.speed = Speed;
    }
    
    private void Update()
    {
        if (_player != null)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
        }

        if (Health <= 0)
        {
            MakePhisical();
            Destroy(gameObject, DESTRUCTION_AFTER_DEATH);
        }
    }

    void MakePhisical()
    {
        _anim.enabled = false;
        _navMeshAgent.velocity = Vector3.zero;
        _navMeshAgent.isStopped = true;
        for (int i = 0; i < AllRigidbodies.Length; i++)
        {
            AllRigidbodies[i].isKinematic = false;
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        ActivateBloodParticles();
    }
    
    public void ActivateBloodParticles()
    {
        Blood.Play();
    }
}
