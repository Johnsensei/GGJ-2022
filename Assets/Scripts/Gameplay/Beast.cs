using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Beast : MonoBehaviour
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

	public Robot robot;

	public Image healthBarImage;
	public float maxHealth;
	public float healthAmount;
	public float healthDecreaseAmount;

	public float forceAmount;
	public float recoveryTime;
	private bool recovery = false;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update () {

		if (recovery){
			return;
		}

		// healthBarImage.fillAmount = healthAmount / maxHealth;

		// Leave this isMoving block like this.
		// If it matches the bottom isMoving block then Beast won't stop moving.
        if (isMoving){
            currentDistanceToTouchPos = (touchPosition - transform.position).magnitude;
			robot.batteryAmount += 1f * Time.deltaTime;
			robot.batteryBarImage.fillAmount = robot.batteryAmount / robot.maxBattery;
        }
    
		if (Input.touchCount > 0) {
			touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) {
					previousDistanceToTouchPos = 0;
					currentDistanceToTouchPos = 0;
					touchPosition = Camera.main.ScreenToWorldPoint (touch.position);
                    
                    if(touchPosition.x > 0 && touchPosition.x < 5 && touchPosition.y < 10 && touchPosition.y > -10){
                        isMoving = true;
                        touchPosition.z = 0;
					    whereToMove = (touchPosition - transform.position).normalized;
					    rb.velocity = new Vector2 (whereToMove.x * moveSpeed, whereToMove.y * moveSpeed);
						if (CameraMovement.Singleton != null)
							CameraMovement.Singleton.Follow(transform);
                    }
				}
		}

		if (currentDistanceToTouchPos > previousDistanceToTouchPos) {
			isMoving = false;
			rb.velocity = Vector2.zero;
		}

        
        if (isMoving){
            previousDistanceToTouchPos = (touchPosition - transform.position).magnitude;

			if (robot.batteryAmount >= 100){
				robot.batteryAmount = 100;
				robot.batteryBarImage.fillAmount = robot.batteryAmount / robot.maxBattery;
			} else {
				robot.batteryAmount += 1f * Time.deltaTime;
				robot.batteryBarImage.fillAmount = robot.batteryAmount / robot.maxBattery;
				}
        }

		Animate();
    
        
	} // End of Update

	void Animate(){
		anim.SetFloat("AnimMoveX", whereToMove.x);
		anim.SetFloat("AnimMoveY", whereToMove.y);
		anim.SetBool("isMoving", isMoving);
	}

	void OnCollisionEnter2D(Collision2D other) {
		// Debug.Log("Collided with enemy.");
		if(other.gameObject.tag == "Enemy"){
			spriteRenderer.color = Color.red;
			healthAmount -= healthDecreaseAmount;
			healthBarImage.fillAmount = healthAmount / maxHealth;
			// TODO: How to increase the speed of the knockback without increasing the distance?
			rb.AddForce((transform.position - other.transform.position) * forceAmount);
			recovery = true;
			Invoke("Recover", recoveryTime);
		} else if (other.gameObject.tag == "Targeted"){
			Destroy(other.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Enemy"){
			healthAmount -= healthDecreaseAmount;
			healthBarImage.fillAmount = healthAmount / maxHealth;
		} else if (other.gameObject.tag == "Targeted"){
			Destroy(other.gameObject);
		}
	}

	void Recover(){
		recovery = false;
		rb.velocity = Vector2.zero;
		spriteRenderer.color = Color.white;
	}

}
