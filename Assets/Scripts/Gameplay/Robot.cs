using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Robot : MonoBehaviour
{

   [SerializeField]
	float moveSpeed = 5f;

	Rigidbody2D rb;
	public SpriteRenderer spriteRenderer;
	public Color myColor;

	Touch touch;
	Vector3 touchPosition, whereToMove;
	bool isMoving = false;

	float previousDistanceToTouchPos, currentDistanceToTouchPos;

	public Animator anim;
	public bool levelCleared = false;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer>();
		myColor = GetComponent<SpriteRenderer>().color;
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

		if(levelCleared){
			// StartCoroutine(FadeTo(0f, 2f));
		}
    
        
	} // End of Update

	void Animate(){
		anim.SetFloat("AnimMoveX", whereToMove.x);
		anim.SetFloat("AnimMoveY", whereToMove.y);
		anim.SetBool("isMoving", isMoving);
	}

	IEnumerator FadeTo(float aValue, float aTime)
	{
		// Debug.Log("This got called.");
		
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, aValue, t));
			myColor = newColor;
			Debug.Log(newColor.a);
			yield return null;
		}
	}

}
