using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogManager
{
    static float logTimer = 0.3f;
    static float preTime;
    public static void Log(string text)
    {
        if(logTimer + preTime < Time.time)
        {
            Debug.Log(text);
            preTime = Time.time;
        }
    }
    public static void Log(float text)
    {
        if (logTimer + preTime < Time.time)
        {
            Debug.Log(text);
            preTime = Time.time;
        }
    }
}
