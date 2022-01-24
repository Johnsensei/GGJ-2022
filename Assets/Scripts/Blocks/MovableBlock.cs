using UnityEngine;
using System.Collections;

public class MovableBlock : MonoBehaviour
{
    public float MoveSpeed = 1;

    Vector3? targetPosition;
    Coroutine movementCoroutine;
       
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        var pushDirection = transform.position - collision.gameObject.transform.position;

        //Horizontal Push
        if(Mathf.Abs(pushDirection.x) >= Mathf.Abs(pushDirection.y))
        {
            Push(pushDirection.x > 0 ? MoveDirection.Right : MoveDirection.Left);
        }
        //Vertical Push
        else
        {
            Push(pushDirection.y > 0 ? MoveDirection.Up : MoveDirection.Down);
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

    bool CanMove(MoveDirection direction)
    {
        var translationDirection = GetTranslationDirection(direction);
        var translationDirectionRaycast = Physics2D.Raycast(transform.position + translationDirection / 2 + translationDirection.normalized / 100, translationDirection, .85f);
        if (translationDirectionRaycast)
        {
            return false;
        }

        return true;
    }

    IEnumerator MovementCoroutine()
    {
        var movementDirection = targetPosition.Value - transform.position;
        while (movementDirection.sqrMagnitude > 0.01f)
        {
            transform.position += movementDirection.normalized * MoveSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
            movementDirection = targetPosition.Value - transform.position;
        }

        transform.position = targetPosition.Value;
    }
}

public enum MoveDirection
{
    Up,
    Down,
    Left,
    Right
}
