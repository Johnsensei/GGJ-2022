using UnityEngine;
using Cinemachine;

public static class CameraExtensions 
{
    public static void UpdateTarget(this Camera camera, Transform target)
    {
        CinemachineBrain cinemachineBrain;
        if (camera != null && camera.TryGetComponent(out cinemachineBrain))
            cinemachineBrain.ActiveVirtualCamera.Follow = target;
    }
}
