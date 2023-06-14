using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : EnemyAbstract
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Rigidbody[] _allRigidbodies;
    [SerializeField] private ParticleSystem _blood;
    
    private NavMeshAgent _navMeshAgent;
    private CharController _charController;

    private int _damage = Constants.TEN;
    private bool _canDamage = true;
    private float _damageCooldown = Constants.TWO;
    
    private void Awake()
    {
        foreach (var rb in _allRigidbodies)
        {
            rb.isKinematic = true;
        }
    }

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _speed;
    }

    private void Update()
    {
        if (_charController != null)
        {
            _navMeshAgent.SetDestination(_charController.transform.position);
        }
        Damage();
    }

    public void Initialization(CharController charController)
    {
        _charController = charController;
    }
    
    void MakePhysical()
    {
        _anim.enabled = false;
        _navMeshAgent.velocity = Vector3.zero;
        _navMeshAgent.isStopped = true;
        foreach (var rb in _allRigidbodies)
        {
            rb.isKinematic = false;
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        ActivateBloodParticles();
        if (_health <= Constants.ZERO)
        {
            Die();
        }
    }
    
    public void ActivateBloodParticles()
    {
        _blood.Play();
    }

    private void Die()
    {
        MakePhysical();
        _charController.CharacterModel.EnemyKills();
        SoundManager.instance.PlayDeadSound();
        Destroy(gameObject, Constants.DESTRUCTION_AFTER_DEATH);
    }
    
    private void Damage()
    {
        if (_canDamage && _charController != null)
        {
            Vector3 direction = _charController.transform.position - transform.position;
            float distance = direction.magnitude;

            if (distance <= Constants.ONE)
            {
                _charController.CharacterModel.TakeDamage(_damage);
                _canDamage = false;
                StartCoroutine(DamageCooldownCoroutine());
            }
        }
    }

    private IEnumerator DamageCooldownCoroutine()
    {
        yield return new WaitForSeconds(_damageCooldown);
        _canDamage = true;
    }
}
