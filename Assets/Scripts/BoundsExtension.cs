using UnityEngine;
public static class BoundsExtension
{
	public static bool ContainBounds(this Bounds bounds, Bounds target)
	{
		return bounds.Contains(target.min) && bounds.Contains(target.max);
	}

    public static bool can_add(this Bounds bounds, Bounds other, Vector3 add_pos){
        if(bounds.Intersects(other)){
            var vec = bounds.center - other.center;
            var angle = Vector3.Angle(vec, add_pos);
            return angle >= 89;
        }
        return true;
    }
}