using UnityEngine;

public static class GeneralExtensions
{
    public static bool Contains(this LayerMask layerMask, int layer)
    {
        return (layerMask.value & 1 << layer) > 0;
    }
}
