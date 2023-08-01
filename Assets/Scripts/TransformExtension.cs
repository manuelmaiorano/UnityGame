
using UnityEngine;
public static class TransformExtension{
    public static int get_sorting_order(this Transform transform, float yoffet = 0){
        return -(int)((transform.position.y + yoffet) * 100);
    }
}