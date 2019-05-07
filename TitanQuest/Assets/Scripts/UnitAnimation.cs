using UnityEngine;
using UnityEngine.AI;

public class UnitAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponentInParent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        _animator.SetBool("MOVING", _agent.hasPath);
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
