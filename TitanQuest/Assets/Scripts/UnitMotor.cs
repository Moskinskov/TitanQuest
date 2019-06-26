using Assets.Scripts;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMotor : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (target != null)
        {
            if (agent.velocity.magnitude == 0)
                FaceTarget();
            agent.SetDestination(target.position);
        }
    }

    private void FaceTarget()
    {
        Vector3 tempDirection = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(tempDirection.x, 0, tempDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newFocus)
    {
        agent.stoppingDistance = newFocus.radius;
        target = newFocus.interactionTransform;
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0;
        agent.ResetPath();
        target = null;
    }
}
