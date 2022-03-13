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


    } // End of Update.

    void OnMouseUpAsButton(){
            // Debug.Log("Enemy touched.");
            gameObject.tag = "Targeted";
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

}
