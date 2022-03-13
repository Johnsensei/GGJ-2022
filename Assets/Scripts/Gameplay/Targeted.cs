using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeted : MonoBehaviour
{
    Touch touch;
	Vector3 touchPosition;
    Rigidbody2D rb;
    public float forceAmount = 500f;
    
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D> (); 
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
    
    void OnCollisionEnter2D(Collision2D other){
        // This code does not knock the enemy back as expected.
        rb.AddForce((transform.position - other.transform.position) * forceAmount);
        // Debug.Log("Should bounce enemy.");
    }

}
