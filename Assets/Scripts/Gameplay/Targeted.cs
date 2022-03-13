using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeted : MonoBehaviour
{
    Touch touch;
	Vector3 touchPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        

        // if (Input.touchCount > 0) {
		// 	touch = Input.GetTouch (0);

		// 	if (touch.phase == TouchPhase.Ended) {
		// 			touchPosition = Camera.main.ScreenToWorldPoint (touch.position);
                    
        //             if(touchPosition == transform.position){
        //                 gameObject.tag = "Targeted";
        //             }
		// 	}
		// }

    } // End of Update.

    void OnMouseUpAsButton(){
            // Debug.Log("Enemy touched.");
            gameObject.tag = "Targeted";
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

}
