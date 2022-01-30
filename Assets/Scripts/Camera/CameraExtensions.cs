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

    public static Transform GetTarget(this Camera camera)
    {
        CinemachineBrain cinemachineBrain;
        if (camera != null && camera.TryGetComponent(out cinemachineBrain))
            return cinemachineBrain.ActiveVirtualCamera.Follow;

        return null;
    }
}
