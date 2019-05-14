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
            _animator?.SetBool("MOVING", _agent?.velocity.magnitude != 0);
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
