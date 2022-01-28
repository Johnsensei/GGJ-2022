using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleChild : MonoBehaviour
{
    
    public GameObject obstacleParent;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - obstacleParent.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            obstacleParent.transform.position.x + offset.x,
            obstacleParent.transform.position.y + offset.y,
            0);
    }
}
