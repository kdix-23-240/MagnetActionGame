using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LastCheckPointPos
{    
    private static Vector3 lastCheckPoint;
    private static Vector3 firstCheckPoint;

    public static void SetLastPoint(Vector3 vector3) 
    {
        lastCheckPoint = vector3;
    }

    public static void SetFirstPoint(Vector3 vector3) 
    {
        firstCheckPoint = vector3;
    }

    public static Vector3 GetLastPoint()
    {
        return lastCheckPoint;
    }

    public static Vector3 GetFirstPoint()
    {
        return firstCheckPoint;
    }
    
}
