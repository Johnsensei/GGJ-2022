using UnityEngine;

public class AIMirror : MonoBehaviour
{
    public GameObject mirroredGameObject;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - mirroredGameObject.transform.position;
    }

    void Update()
    {
        if (mirroredGameObject == null)
        {
            Destroy(gameObject);
            return;
        }

        var mirrorPosition = mirroredGameObject.transform.position;
        transform.position = new Vector3(mirrorPosition.x + offset.x, mirrorPosition.y + offset.y, 0);
    }
}
