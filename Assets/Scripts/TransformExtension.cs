
using UnityEngine;
public static class TransformExtension{
    public static int get_sorting_order(this Transform transform, float yoffset = 0){
        return Mathf.RoundToInt((-transform.position.y - yoffset) * 100.0f);
    }
}