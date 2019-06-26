using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class UnitAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;

        private void FixedUpdate()
        {
            if (_agent.velocity.magnitude == 0)
            {
                _animator.SetTrigger("Idle");
            }
            else
            {
                _animator.SetTrigger("Move");
            }
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
}
