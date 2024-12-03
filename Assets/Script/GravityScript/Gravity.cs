using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity
{

    private MagnetManager magnetManager;

    public Vector3 AddUpForce()  // 上向きの重力にするための力を返してる
    {
        return new Vector3(0.0f, 7.5f + 5.5f, 0.0f);
        //return new Vector3(0.0f, 7.5f + 600.0f, 0.0f);//MagnetRun(重力:-300)
        //return new Vector3(0.0f, 7.5f + 40.0f, 0.0f);//MagnetUp
    }

    public Vector3 AddLeftForce()
    {
        return new Vector3(-10.5f+2.5f, 0.0f, 0.0f);
    }

    public Vector3 AddRightForce()
    {
        return new Vector3(10.5f+2.5f, 0.0f, 0.0f);
    }

    public Vector3 AntiGravity()
    {
        return new Vector3(0.0f, 7.5f, 0.0f);
    }
}
