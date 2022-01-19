using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject robot;
    public GameObject beast;
    public SpriteRenderer wallBottom;
    

    void Start()
    {
        // Debug.Log(Screen.width);
        float orthoSize = wallBottom.bounds.size.x * Screen.height / Screen.width * 0.5f;

        Camera.main.orthographicSize = orthoSize;
    }

    void Update()
    {

        transform.position = new Vector3(transform.position.x, robot.transform.position.y, transform.position.z);

    }
}
