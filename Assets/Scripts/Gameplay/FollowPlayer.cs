using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject robot;
    public GameObject beast;
    Touch touch;
	Vector3 touchPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    //     if (Input.touchCount > 0) {
	// 		touch = Input.GetTouch (0);

	// 		if (touch.phase == TouchPhase.Began) {
	// 				touchPosition = Camera.main.ScreenToWorldPoint (touch.position);

    //                 if(touchPosition.x > 0 && touchPosition.x < 5 && touchPosition.y < 10 && touchPosition.y > -10){
    //                     touchPosition.z = 0;
    //                     transform.position = new Vector3(transform.position.x, beast.transform.position.y, transform.position.z);
    //                 }

    //                 if(touchPosition.x < 0 && touchPosition.x > -5 && touchPosition.y < 10 && touchPosition.y > -10){
    //                     touchPosition.z = 0;
    //                     transform.position = new Vector3(transform.position.x, robot.transform.position.y, transform.position.z);
    //                 }
    //         }

    // }
    transform.position = new Vector3(transform.position.x, robot.transform.position.y, transform.position.z);
    }
}
