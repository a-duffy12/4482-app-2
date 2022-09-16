using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSystem : MonoBehaviour
{
    [Header("Stats")]
    public float fireRate = 2.0f;
    public float fireSpeed = 1500;

    [Header("GameObjects")]
    public GameObject dartPrefab;
    public Transform firePoint;

    Rigidbody rb;

    private float lastFireTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region input functions

    public void Shoot(InputAction.CallbackContext con)
    {
        if (Config.playerIt && (Time.time > lastFireTime + (1/fireRate))) // can only shoot if it and are within fire rate cap
        {
            GameObject dartObject = Instantiate(dartPrefab, firePoint.position, firePoint.rotation);

            Dart dart = dartObject.GetComponent<Dart>();
            dart.Shoot(firePoint.transform.forward, fireSpeed);

            lastFireTime = Time.time;
        }
    }

    #endregion input functions
}
