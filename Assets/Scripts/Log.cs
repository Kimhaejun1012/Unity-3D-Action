using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log
{
    static float logTimer = 0.3f;
    static float preTime;
    public static void L(string text)
    {
        if(logTimer + preTime < Time.time)
        {
            Debug.Log(text);
            preTime = Time.time;
        }
    }
    public static void L(float text)
    {
        if (logTimer + preTime < Time.time)
        {
            Debug.Log(text);
            preTime = Time.time;
        }
    }
    public static void L(Vector3 text)
    {
        if (logTimer + preTime < Time.time)
        {
            Debug.Log(text);
            preTime = Time.time;
        }
    }
}
