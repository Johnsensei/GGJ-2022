using UnityEngine;


[RequireComponent(typeof(AIChase))]

public class AIChaseWhenNear : MonoBehaviour
{
    public float SpotRadius = 1;
    public LayerMask ChaseTargetsMask;
    public bool DebugMode;

    AIChase chaseComponent;

    void Start()
    {
        chaseComponent = GetComponent<AIChase>();
    }

    void FixedUpdate()
    {        
        var spottedTarget = Physics2D.OverlapCircle(transform.position, SpotRadius, ChaseTargetsMask);
        if (spottedTarget != null)
        {
            chaseComponent.BeginChasing(spottedTarget.transform);
        }
    }

    private void OnDrawGizmos()
    {
        if (DebugMode)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, SpotRadius);
        }
    }
}
