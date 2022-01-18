using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestRobot : MonoBehaviour
{

   [SerializeField]
	float moveSpeed = 5f;

	Rigidbody2D rb;

	Touch touch;
	Vector3 touchPosition, whereToMove;
	bool isMoving = false;

	float previousDistanceToTouchPos, currentDistanceToTouchPos;

	public Animator anim;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	void Update () {

        if (isMoving){
            currentDistanceToTouchPos = (touchPosition - transform.position).magnitude;
        }
    
		if (Input.touchCount > 0) {
			touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) {
					previousDistanceToTouchPos = 0;
					currentDistanceToTouchPos = 0;
					touchPosition = Camera.main.ScreenToWorldPoint (touch.position);
                    
                    if(touchPosition.x < 0 && touchPosition.x > -5 && touchPosition.y < 10 && touchPosition.y > -10){
                        isMoving = true;
                        touchPosition.z = 0;
					    whereToMove = (touchPosition - transform.position).normalized;
					    rb.velocity = new Vector2 (whereToMove.x * moveSpeed, whereToMove.y * moveSpeed);
                    }
                    
					
				}
		}

		if (currentDistanceToTouchPos > previousDistanceToTouchPos) {
			isMoving = false;
			rb.velocity = Vector2.zero;
		}

        
        if (isMoving){
            previousDistanceToTouchPos = (touchPosition - transform.position).magnitude;
        }

		Animate();
    
        
	} // End of Update

	void Animate(){
		anim.SetFloat("AnimMoveX", whereToMove.x);
		anim.SetFloat("AnimMoveY", whereToMove.y);
		anim.SetBool("isMoving", isMoving);
	}

}
