using UnityEngine;
using Pathfinding;

public class AIPatrol : MonoBehaviour
{
    public float PatrolSpeed = 1;
    public Transform[] PatrolPoints;

    [SerializeField]
    private bool IsPatrolling;
    private AIDestinationSetter destinationSetter;
    private AIPath pathfinder;

    bool isPatrollingForwards;
    int currentPatrolPoint;
    Vector3 currentDestination;

    private void Awake()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        pathfinder = GetComponent<AIPath>();
    }

    void Start()
    {
        if (PatrolPoints != null && PatrolPoints.Length > 0)
        {
            transform.position = PatrolPoints[0].position;
            currentDestination = PatrolPoints[0].position;
            currentPatrolPoint = 0;
            isPatrollingForwards = true;
        }

        if(IsPatrolling)        
            StartPatrolling();
        
    }

    void Update()
    {
        if (!IsPatrolling) 
            return;

        if ((currentDestination - transform.position).sqrMagnitude <= .05f)
            GoToNextDestination();
        else
            MoveTowardsDestination();
    }

    public void StartPatrolling()
    {
        IsPatrolling = true;
        pathfinder.maxSpeed = PatrolSpeed;
    }

    public void StopPatrolling()
    {
        IsPatrolling = false;
    }

    void GoToNextDestination()
    {
        if (isPatrollingForwards)
            GoToNextDestinationForwards();
        else
            GoToNextDestinationBackwards();
    }

    void GoToNextDestinationForwards()
    {
        if (currentPatrolPoint + 1 < PatrolPoints.Length)
            currentDestination = PatrolPoints[++currentPatrolPoint].position;
        else if (currentPatrolPoint - 1 >= 0)
        {
            isPatrollingForwards = false;
            currentDestination = PatrolPoints[--currentPatrolPoint].position;
        }
    }

    void GoToNextDestinationBackwards()
    {
        if (currentPatrolPoint - 1 >= 0)
            currentDestination = PatrolPoints[--currentPatrolPoint].position;
        else if (currentPatrolPoint + 1 < PatrolPoints.Length)
        {
            isPatrollingForwards = true;
            currentDestination = PatrolPoints[++currentPatrolPoint].position;
        }
    }

    void MoveTowardsDestination()
    {
        destinationSetter.target = PatrolPoints[currentPatrolPoint];
    }

}
