using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class SoldierScript : MonoBehaviour
{
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

    private Vector3 DistanceToWalkPoint;
    private bool _goingToTA = true;

    private float TimeBetweenAttacks = 0.5f;
    private bool AttackAlready;

    public bool _playerIsInMySight = false;
    public bool _playerInAttackRange = false;

    void Start()
    {
        _agent.destination = Vector3.zero;
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
        //_playerIsInMySight = Physics.CheckSphere(transform.position, SightRange, _TargetMask);
       // _playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, _TargetMask);

        Debug.Log($"insight = {_playerIsInMySight}, Attack Range = {_playerInAttackRange}");
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
            DistanceToWalkPoint = transform.position - _targetA.gameObject.transform.position;
        }
        else if (!_goingToTA)
        {
            _agent.SetDestination(_targetB.gameObject.transform.position);
            DistanceToWalkPoint = transform.position - _targetB.gameObject.transform.position;
        }

        if (DistanceToWalkPoint.magnitude < 2f)
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

        if (!AttackAlready)
        {
            Instantiate(_projectile, transform.position, transform.rotation);

            AttackAlready = true;
            Invoke(nameof(ResetAttack), TimeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        AttackAlready = false;
    }

    private void DestroySpitter()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos() // SHOULD BE REMOVED
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SightRange);
    }
}
