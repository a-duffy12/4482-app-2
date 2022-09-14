using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    public Transform playerHead;
    [SerializeField] private float sensMod; // tweak this to get sens close to source values

    float xRotation = 0f;

    private float xMouse;
    private float yMouse;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;      
    }

    void Update()
    {
        if (Config.firstPerson)
        {
            Look(xMouse, yMouse);
        }
        else
        {
            LookThirdPerson(xMouse, yMouse);
        }
    }

    void Look(float x, float y)
    {
        float xLook = x * Config.sensitivity * sensMod * Time.deltaTime;
        float yLook = y * Config.sensitivity * sensMod * Time.deltaTime;

        xRotation -= yLook;
        xRotation = Mathf.Clamp(xRotation, -90f, 75f); // restrict up and down head movement

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // apply y movement as rotation to the head
        playerBody.Rotate(Vector3.up * xLook); // apply x movement as rotation to the body
    }

    void LookThirdPerson(float x, float y)
    {
        float xLook = x * Config.sensitivity * sensMod * Time.deltaTime;
        float yLook = y * Config.sensitivity * sensMod * Time.deltaTime;

        xRotation -= yLook;
        xRotation = Mathf.Clamp(xRotation, -60f, 45f); // restrict up and down head movement

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // apply y movement as rotation to the head
        playerBody.Rotate(Vector3.up * xLook); // apply x movement as rotation to the body
    }
}
