using UnityEngine;
using System.Collections;

public class MovableBlock : MonoBehaviour
{
    [Header("Functionality")]
    public float MoveSpeed = 1;
    public LayerMask BlockingMovementLayerMask;
    public int TilesTall = 1;
    public int TilesWide = 1;
    [Header("Particles")]
    public ParticleSystem MovementTrailParticles;
    public ParticleSystem MirrorMovementTrailParticles;

    Vector3? targetPosition;
    Coroutine movementCoroutine;
    Collider2D blockCollider;

    int _playerLayer;

    private void Awake()
    {
        blockCollider = GetComponent<Collider2D>();
        _playerLayer = LayerMask.NameToLayer("Robot");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer != _playerLayer)
            return;

        Push(CalculatePushDirection(collision));   
    }

    MoveDirection CalculatePushDirection(Collision2D collision)
    {
        var contactPoint = collision.contacts[0].point;
        var blockExtensions = blockCollider.bounds.extents;

        var rightEdge = transform.position + Vector3.right * blockExtensions.x;
        var leftEdge = transform.position + Vector3.left * blockExtensions.x;
        var topEdge = transform.position + Vector3.up * blockExtensions.y;
        var bottomEdge = transform.position + Vector3.down * blockExtensions.y;

        //Right Side
        if (contactPoint.x > transform.position.x)
        {
            //Upper Edge
            if (contactPoint.y > transform.position.y)
            {
                //Push to the left side
                if (Mathf.Abs(contactPoint.x - rightEdge.x) < Mathf.Abs(contactPoint.y - topEdge.y))
                    return MoveDirection.Left;
                //Move down
                else
                    return MoveDirection.Down;
            }
            //Lower Edge
            else
            {
                //Move to the left side
                if (Mathf.Abs(contactPoint.x - rightEdge.x) < Mathf.Abs(contactPoint.y - bottomEdge.y))                
                    return MoveDirection.Left;                
                //Move up
                else
                    return MoveDirection.Up;                
            }
        }
        //Left Side
        else
        {
            //Upper Edge
            if (contactPoint.y > transform.position.y)
            {
                //Push to the right side
                if (Mathf.Abs(contactPoint.x - leftEdge.x) < Mathf.Abs(contactPoint.y - topEdge.y))                
                    return MoveDirection.Right;
                //Move down
                else
                    return MoveDirection.Down;                
            }
            //Lower Edge
            else
            {
                //Push to the right side
                if (Mathf.Abs(contactPoint.x - leftEdge.x) < Mathf.Abs(contactPoint.y - bottomEdge.y))                
                    return MoveDirection.Right;                
                //Move up
                else                
                    return MoveDirection.Up;                
            }
        }
    }

    public void Push(MoveDirection direction)
    {
        if (!CanMove(direction))
            return;

        var translation = GetTranslationDirection(direction);

        if (targetPosition.HasValue)
            targetPosition += translation;
        else
            targetPosition = transform.position + translation;

        if (movementCoroutine != null)
            StopCoroutine(movementCoroutine);

        movementCoroutine = StartCoroutine(MovementCoroutine());
    }

    Vector3 GetTranslationDirection(MoveDirection direction)
    {
        var translation = Vector3.zero;

        switch (direction)
        {
            case MoveDirection.Up:
                translation = Vector3.up;
                break;
            case MoveDirection.Down:
                translation = Vector3.down;
                break;
            case MoveDirection.Left:
                translation = Vector3.left;
                break;
            case MoveDirection.Right:
                translation = Vector3.right;
                break;
        }

        return translation;
    }

    Vector3 GetRaycastOffset(MoveDirection direction, int amountOfRaycasts, int raycastIndex)
    {
        var raycastNumberHorizontalOffset = -blockCollider.bounds.extents.x + (2 * blockCollider.bounds.extents.x / amountOfRaycasts) * raycastIndex + blockCollider.bounds.extents.x / amountOfRaycasts;
        var raycastNumberVerticalOffset = -blockCollider.bounds.extents.y + (2 * blockCollider.bounds.extents.y / amountOfRaycasts) * raycastIndex + blockCollider.bounds.extents.y / amountOfRaycasts;

        switch (direction)
        {
            case MoveDirection.Up:
                return new Vector3(raycastNumberHorizontalOffset, blockCollider.bounds.extents.y, 0);
            case MoveDirection.Down:
                return new Vector3(raycastNumberHorizontalOffset, -blockCollider.bounds.extents.y, 0);
            case MoveDirection.Left:
                return new Vector3(-blockCollider.bounds.extents.x, raycastNumberVerticalOffset, 0);
            case MoveDirection.Right:
                return new Vector3(blockCollider.bounds.extents.x, raycastNumberVerticalOffset, 0);
        }

        return Vector3.zero;
    }

    bool CanMove(MoveDirection direction)
    {
        var numberOfRaycasts = CalculateNumberOfRaycasts(direction);

        for (int i = 0; i < numberOfRaycasts; i++)
        {
            var translationDirection = GetTranslationDirection(direction);
            var raycastOffset = GetRaycastOffset(direction, numberOfRaycasts, i) + translationDirection.normalized / 100;
            var translationDirectionRaycast = Physics2D.Raycast(transform.position + raycastOffset, translationDirection, .85f, BlockingMovementLayerMask);

            if (translationDirectionRaycast)
            {
                return false;
            }
        }        

        return true;
    }

    int CalculateNumberOfRaycasts(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.Up:
            case MoveDirection.Down:
                return TilesWide;
            case MoveDirection.Left:
            case MoveDirection.Right:
                return TilesTall;
        }

        return 1;
    }

    IEnumerator MovementCoroutine()
    {
        ShowParticles();

        var movementDirection = targetPosition.Value - transform.position;
        while (movementDirection.sqrMagnitude > 0.01f)
        {
            transform.position += movementDirection.normalized * MoveSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
            movementDirection = targetPosition.Value - transform.position;
        }

        transform.position = targetPosition.Value;
        HideParticles();
    }

    void ShowParticles()
    {
        MovementTrailParticles?.Play();
        MirrorMovementTrailParticles?.Play();
    }

    void HideParticles()
    {
        MovementTrailParticles?.Stop();
        MirrorMovementTrailParticles?.Stop();
    }
}

public enum MoveDirection
{
    Up,
    Down,
    Left,
    Right
}
