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

    [Header("Jumping")]
    [Range(0f, 100)] public static float gravity = -9.81f;
    [Range(0f, 1f)] public static float jumpWindow = 0.1f;
    [Range(0f, 20)] public static float jumpSpeed = 6.5f;
    [Range(0f, 20)] public static float airAcceleration = 16f;
    [Range(0f, 20)] public static float airFriction = 0.4f;
    
    [Header("Ground Movement")]
    [Range(0f, 20)] public static float sprintSpeed = 12f;
    //[Range(0f, 20)] public static float walkSpeed = 7f;
    [Range(0f, 20)] public static float acceleration = 14f;
    [Range(0f, 20)] public static float deceleration = 10f;
    [Range(0f, 20)] public static float friction = 6f;
}
