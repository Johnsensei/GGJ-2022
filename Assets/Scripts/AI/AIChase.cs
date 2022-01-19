using UnityEngine;

public class AIChase : MonoBehaviour
{
    public float ChaseSpeed = 2;
    [SerializeField]
    private Transform ChaseTarget;

    void Update()
    {
        if (ChaseTarget == null)
            return;

        var chaseDirection = (ChaseTarget.position - transform.position).normalized;
        transform.position += chaseDirection * ChaseSpeed * Time.deltaTime;
    }

    public void BeginChasing(Transform chaseTarget)
    {
        if (chaseTarget == null)
            return;

        ChaseTarget = chaseTarget;
    }

    public void StopChasing()
    {
        ChaseTarget = null;
    }    

}
