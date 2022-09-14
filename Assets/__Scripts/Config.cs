using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Config
{
    [Header("Settings")]
    [Range(0.1f, 10)] public static float sensitivity = 1f;
    [Range(60, 110)] public static float fieldOfView = 60f;
    public static bool firstPerson = true;
}
