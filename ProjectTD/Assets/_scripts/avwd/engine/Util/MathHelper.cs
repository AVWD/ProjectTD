using System;
using UnityEngine;
using System.Collections;

public class MathHelper
{
    public static float GetAngle(float x, float y)
    {
        return Mathf.Atan2(y, x);
    }
    public static float GetAngle(Vector2 v)
    {
        return Mathf.Atan2(v.y, v.x);
    }
    public static float GetAngle(Vector3 v)
    {
        return Mathf.Atan2(v.y, v.x);
    }

    public static float ProgressAlongLine(Vector2 start, Vector2 end, Vector2 point)
    {
        Vector2 dif = end - start;
        Vector2 dif2 = point - start;

        float num1 = dif.x * dif.x + dif.y * dif.y;
        float num2 = dif2.x * dif.x + dif2.y * dif.y;
        return num2 / num1;
    }

}
