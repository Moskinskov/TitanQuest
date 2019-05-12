using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class UnitAnimation : MonoBehaviour
{
    [SerializeField, SyncVar] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponentInParent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        _animator.SetBool("MOVING", _agent.velocity.magnitude != 0);
    }

    private void Hit()
    {
    }

    private void FootR()
    {
    }

    private void FootL()
    {
    }
}
