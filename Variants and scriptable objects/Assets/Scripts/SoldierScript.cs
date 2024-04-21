using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class SoldierScript : MonoBehaviour
{
    // refrences 
    [SerializeField] SoldierScriptableObject _soldierSO;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Animator _animator;
    [SerializeField] Transform _target;
    [SerializeField] GameObject _projectile;

    [Header("Ranges")]
    public float SightRange;
    public float AttackRange;

    [Header("Layer Masks")]
    [SerializeField] LayerMask _groundMask;
    [SerializeField] LayerMask _TargetMask;

    [Header("Mesh Renderer")]
    [SerializeField] MeshRenderer _targetA;
    [SerializeField] MeshRenderer _targetB;

    public int deathCount;


    private Vector3 _distanceToWalkPoint;
    private bool _goingToTA = true;

    private float _timeBetweenAttacks = 0.5f;
    private bool _attackAlready;

    public bool _playerIsInMySight = false;
    public bool _playerInAttackRange = false;
    private bool _isDead;

    public float _health;
    private float _speed;

    void Start()
    {
        _agent.destination = Vector3.zero;
        _health = _soldierSO._startingHealth;
        _speed = _soldierSO._baseSpeed;
        
        _agent.speed = _speed;
    }

    void Update()
    {
        enemeyState();

        if (_agent.velocity != Vector3.zero)
        {
            _animator.SetBool("Walking", true);
        }
        else _animator.SetBool("Walking", false);
    }

    private void enemeyState()
    {
        //Check for Sight and Attack Range
         _playerIsInMySight = Physics.CheckSphere(transform.position, SightRange, _TargetMask);
         _playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, _TargetMask);

        Debug.Log($"insight = {_playerIsInMySight}, Attack Range = {_playerInAttackRange}");
        if (_health <= 0 || _isDead == true)
        {
            Death();
        }
      
        if (!_playerIsInMySight && !_playerInAttackRange)
        {
            Patroling();
        }
        else if (_playerIsInMySight && !_playerInAttackRange)
        {
            ChasePlayer();
        }
        else if (_playerIsInMySight && _playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        Debug.Log("Patroling");
        if (_goingToTA)
        {
            _agent.SetDestination(_targetA.gameObject.transform.position);
            _distanceToWalkPoint = transform.position - _targetA.gameObject.transform.position;
        }
        else if (!_goingToTA)
        {
            _agent.SetDestination(_targetB.gameObject.transform.position);
            _distanceToWalkPoint = transform.position - _targetB.gameObject.transform.position;
        }

        if (_distanceToWalkPoint.magnitude < 2f)
        {
            _goingToTA = !_goingToTA;
        }
    }

    private void ChasePlayer()
    {
        Debug.Log("chasing");
        _agent.SetDestination(_target.position);
    }

    private void AttackPlayer()
    {
        Debug.Log("Attacking");
        _animator.SetBool("Shooting", true);
        _animator.SetBool("Walking", false);

        _agent.SetDestination(transform.position); transform.LookAt(_target);

        if (!_attackAlready)
        {
            Instantiate(_projectile, transform.position, transform.rotation);

            _attackAlready = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        _attackAlready = false;
    }

    private void OnDrawGizmos() // SHOULD BE REMOVED
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SightRange);
    }

    private void TakeDamage()
    {
        _health -= 25;
    }

    private void Death()
    {
        deathCount++;
        _health = _soldierSO._startingHealth;
        transform.position = _targetB.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            TakeDamage();
        }
    }
}
