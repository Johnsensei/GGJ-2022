using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Singleton;

    public Transform Target;
    public float FollowSpeed = 1;
    public float SwitchSpeed = 2;

    private Coroutine switchCoroutine;

    private void Awake()
    {
        if (Singleton != null)
        {
            if (Singleton != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        else
            Singleton = this;
    }

    public void Follow(Transform target)
    {
        if (target != Target)
            Switch(target);

        Target = target;
    }
 
    public void Switch(Transform target)
    {
        if (switchCoroutine != null)
            StopCoroutine(switchCoroutine);

        target = Target;

        if (target != null)
            switchCoroutine = StartCoroutine(SwitchCoroutine());
    }

    IEnumerator SwitchCoroutine()
    {
        var targetOffset = CalculateTargetOffset();
        while (targetOffset.sqrMagnitude > 0.01f)
        {
            MoveTowardsTarget(SwitchSpeed);
            yield return new WaitForEndOfFrame();
            targetOffset = CalculateTargetOffset();
        }
    }

    Vector3 CalculateTargetOffset()
    {
        if (Target == null)
            return transform.position;

        return Target.position - transform.position;
    }

    void MoveTowardsTarget(float speed)
    {
        var moveDirection = CalculateTargetOffset().normalized;
        transform.position += Vector3.up * moveDirection.y * speed * Time.deltaTime;
    }

    void Update()
    {
        if(Target != null)
            MoveTowardsTarget(FollowSpeed);
    }
}
