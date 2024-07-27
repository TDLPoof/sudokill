using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static float Length(Vector3 vec)
    {
        return Mathf.Sqrt(vec.x * vec.x + vec.y * vec.y);
    }

    public static Vector3 GetEndpoint(Vector3 start, float distance, float angle)
    {
        Vector3 straightDistance = new Vector3(distance, 0, 0);
        straightDistance.x = distance * Mathf.Cos(angle);
        straightDistance.y = distance * Mathf.Sin(angle);
        return start + straightDistance;
    }

    public static float DistanceSQ(Vector3 p1, Vector3 p2)
    {
        return Mathf.Pow(p1.x - p2.x, 2) + Mathf.Pow(p1.y - p2.y, 2);
    }
}
