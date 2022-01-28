using UnityEngine;
using Pathfinding;

public class AIChase : MonoBehaviour
{
    public float ChaseSpeed = 2;
    [SerializeField]
    private Transform chaseTarget;

    private AIDestinationSetter destinationSetter;
    private AIPath pathfinder;

    private void Awake()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        pathfinder = GetComponent<AIPath>();
    }

    public void BeginChasing(Transform chaseTarget)
    {
        if (chaseTarget == null)
            return;

        UpdatePatrolling(false);

        pathfinder.maxSpeed = ChaseSpeed;
        this.chaseTarget = chaseTarget;
        destinationSetter.target = chaseTarget;
    }

    public void StopChasing()
    {
        UpdatePatrolling(true);

        chaseTarget = null;
    }    

    void UpdatePatrolling(bool patrolling)
    {
        AIPatrol patrolComponent;
        if (TryGetComponent(out patrolComponent))
        {
            if (patrolling)
                patrolComponent.StartPatrolling();
            else
                patrolComponent.StopPatrolling();
        }
    }

}
