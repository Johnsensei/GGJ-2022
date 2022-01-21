using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleChild : MonoBehaviour
{
    
    public GameObject obstacleParent;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            obstacleParent.transform.position.x + 5,
            obstacleParent.transform.position.y,
            0);
    }
}
