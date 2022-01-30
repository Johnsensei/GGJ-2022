using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCamera : MonoBehaviour
{
    public Image logo;
    RectTransform rectTransform;
    
    void Start()
    {
        rectTransform = logo.GetComponent<RectTransform>();

        Debug.Log(logo);
        Debug.Log(rectTransform);
        // Debug.Log(rectTransform.height);

        // float orthoSize = rectTransform.width * Screen.height / Screen.width * 0.5f;

        // Camera.main.orthographicSize = orthoSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
