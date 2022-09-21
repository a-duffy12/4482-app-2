using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    [SerializeField] private float sensMod; // tweak this to get sens close to source values
    [SerializeField] private GameObject crosshair;

    float xRotation = 0f;

    private float xMouse;
    private float yMouse;

    void Start()
    {
        Cursor.visible = false;
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

    #region input

    public void MouseX(InputAction.CallbackContext con)
    {
        xMouse = con.ReadValue<float>();
    }

    public void MouseY(InputAction.CallbackContext con)
    {
        yMouse = con.ReadValue<float>();
    }

    public void ToggleCamera(InputAction.CallbackContext con)
    {
        if (con.performed)
        {
            if (Config.firstPerson)
            {   
                Config.firstPerson = false;
                firstPersonCamera.enabled = false;
                thirdPersonCamera.enabled = true;
                crosshair.SetActive(false);
            }
            else
            {
                Config.firstPerson = true;
                firstPersonCamera.enabled = true;
                thirdPersonCamera.enabled = false;
                crosshair.SetActive(true);
            }
        }
    }

    #endregion input
}
