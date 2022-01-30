using UnityEngine;
using Pathfinding;

public class AIAnimate : MonoBehaviour
{
    public Animator animator;
    public Animator otherAnimator;
    AIPath pathfinder;

    void Awake()
    {
        pathfinder = GetComponent<AIPath>();
    }

    void Update()
    {
        var velocity = pathfinder.desiredVelocity;
        if (velocity.sqrMagnitude >= .5f)
        {
            animator.SetFloat("AnimMoveX", velocity.x);
            animator.SetFloat("AnimMoveY", velocity.y);
            otherAnimator.SetFloat("AnimMoveX", velocity.x);
            otherAnimator.SetFloat("AnimMoveY", velocity.y);
        }
    }
}
