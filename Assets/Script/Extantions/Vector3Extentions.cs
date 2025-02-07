using UnityEngine;

public static class Vector3Extentions
{
    public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null) =>
         new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);

    public static Vector3 Add(this Vector3 vector, float? x = null, float? y = null, float? z = null) =>
        new Vector3(vector.x + (x ?? 0), vector.y + (y ?? 0), vector.z + (z ?? 0));

    public static Vector3 Reset(this Vector3 vector) => Vector3.zero;

    public static Vector2 ToVector2(this Vector3 vector) 
    {
        return new Vector2(vector.x, vector.z);
    }

}
