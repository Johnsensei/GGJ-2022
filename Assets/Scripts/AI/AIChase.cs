using UnityEngine;
using Pathfinding;

public class AIChase : MonoBehaviour
{
    public float ChaseSpeed = 2;
    [SerializeField]
    private Transform chaseTarget;

    private AIDestinationSetter destinationSetter;

    private void Awake()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();   
    }

    public void BeginChasing(Transform chaseTarget)
    {
        if (chaseTarget == null)
            return;

        this.chaseTarget = chaseTarget;
        destinationSetter.target = chaseTarget;
    }

    public void StopChasing()
    {
        chaseTarget = null;
    }    

}
