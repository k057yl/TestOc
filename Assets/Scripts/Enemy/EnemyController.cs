using UnityEngine;
using UnityEngine.AI;

public class EnemyController : EnemyAbstract
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Rigidbody[] _allRigidbodies;
    [SerializeField] private ParticleSystem _blood;
    
    private NavMeshAgent _navMeshAgent;
    private GameObject _player;

    private bool _hasKilledBeenCalled = false;
    
    private void Awake()
    {
        for (int i = Constants.NULL; i < _allRigidbodies.Length; i++)
        {
            _allRigidbodies[i].isKinematic = true;
        }
    }

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = GameObjectManager.instance.allObject[Constants.NULL];

        _navMeshAgent.speed = Speed;
    }
    
    private void Update()
    {
        if (_player != null)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
        }

        if (Health <= Constants.NULL)
        {
            if (!_hasKilledBeenCalled)
            {
                MakePhisical();
                UIBar.OnKilled?.Invoke();
                _hasKilledBeenCalled = true;
            }
            Destroy(gameObject, Constants.DESTRUCTION_AFTER_DEATH);
        }
    }

    void MakePhisical()
    {
        _anim.enabled = false;
        _navMeshAgent.velocity = Vector3.zero;
        _navMeshAgent.isStopped = true;
        for (int i = Constants.NULL; i < _allRigidbodies.Length; i++)
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
