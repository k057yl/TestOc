using UnityEngine;
using UnityEngine.AI;

public class EnemyController : EnemyAbstract
{
    private const float DESTRUCTION_AFTER_DEATH = 1.5f;
    
    [SerializeField] private Animator _anim;
    [SerializeField] private Rigidbody[] _allRigidbodies;
    [SerializeField] private ParticleSystem _blood;
    
    private NavMeshAgent _navMeshAgent;
    private GameObject _player;

    private void Awake()
    {
        for (int i = 0; i < _allRigidbodies.Length; i++)
        {
            _allRigidbodies[i].isKinematic = true;
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
        for (int i = 0; i < _allRigidbodies.Length; i++)
        {
            _allRigidbodies[i].isKinematic = false;
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        ActivateBloodParticles();
    }
    
    public void ActivateBloodParticles()
    {
        _blood.Play();
    }
}
