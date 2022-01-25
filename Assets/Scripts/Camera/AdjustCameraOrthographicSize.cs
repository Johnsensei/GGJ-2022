using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class AdjustCameraOrthographicSize : MonoBehaviour
{
    void Start()
    {
        float orthoSize = 11f * Screen.height / Screen.width * 0.5f;
        GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = orthoSize;
    }    
}
