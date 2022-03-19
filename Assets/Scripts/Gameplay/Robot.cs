using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Robot : MonoBehaviour
{

   [SerializeField]
	float moveSpeed;

	Rigidbody2D rb;
	public SpriteRenderer spriteRenderer;
	public ParticleSystem goalTeleportEffect;

	Touch touch;
	Vector3 touchPosition, whereToMove;
	bool isMoving = false;

	float previousDistanceToTouchPos, currentDistanceToTouchPos;

	public Animator anim;

	public Beast beast;
	public Image batteryBarImage;
	public float maxBattery;
	public float batteryAmount;
	public float batteryDecreaseAmount;

	private bool recovery = false;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update () {

		if(recovery){
			return;
		}

		if(batteryAmount <= 0){
			isMoving = false;
			rb.velocity = Vector2.zero;
			spriteRenderer.color = new Color(0.3f, 0.3f, 0.3f, 1f);
		} else {
			spriteRenderer.color = Color.white;
		}

        if (isMoving){
            currentDistanceToTouchPos = (touchPosition - transform.position).magnitude;
			ReduceBattery();
        }
    
		if (Input.touchCount > 0) {
			touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) {
					previousDistanceToTouchPos = 0;
					currentDistanceToTouchPos = 0;
					touchPosition = Camera.main.ScreenToWorldPoint (touch.position);
                    
                    if(touchPosition.x < 0 &&
						touchPosition.x > -5 &&
						touchPosition.y < 10 &&
						touchPosition.y > -10 &&
						batteryAmount > 0){
							isMoving = true;
							touchPosition.z = 0;
							whereToMove = (touchPosition - transform.position).normalized;
							rb.velocity = new Vector2 (whereToMove.x * moveSpeed, whereToMove.y * moveSpeed);

							Camera.main.UpdateTarget(transform);							
					}
			}
		}

		if (currentDistanceToTouchPos > previousDistanceToTouchPos) {
			isMoving = false;
			rb.velocity = Vector2.zero;
		}

        
        if (isMoving){
            previousDistanceToTouchPos = (touchPosition - transform.position).magnitude;
			ReduceBattery();
			SoundManager.PlayRobotMovement();
        }
		else
        {
			SoundManager.StopPlayingRobotSound();
		}

		Animate();
           
	} // End of Update

	void Animate(){
		anim.SetFloat("AnimMoveX", whereToMove.x);
		anim.SetFloat("AnimMoveY", whereToMove.y);
		anim.SetBool("isMoving", isMoving);
	}

	void ReduceBattery(){
		if (batteryAmount <= 0){
			// isMoving = false;
			batteryAmount = 0;
			batteryBarImage.fillAmount = batteryAmount / maxBattery;
			SoundManager.PlayRobotNoBattery();
		} else {
			batteryAmount -= batteryDecreaseAmount * Time.deltaTime;
			batteryBarImage.fillAmount = batteryAmount / maxBattery;
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		// Debug.Log("Collided with enemy.");
		if(other.gameObject.tag == "Enemy"){
			spriteRenderer.color = Color.black;
			batteryAmount -= batteryDecreaseAmount;
			batteryBarImage.fillAmount = batteryAmount / maxBattery;
			rb.AddForce((transform.position - other.transform.position) * beast.forceAmount);
			recovery = true;
			// SoundManager.PlayBeastGrowlSound();  // Play a sound if there is one for robot.
			Invoke("Recover", 0.1f);
		}
	}

	void Recover(){
		recovery = false;
		rb.velocity = Vector2.zero;
		spriteRenderer.color = Color.white;
	}

}
