using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSystem : MonoBehaviour
{
    [Header("Stats")]
    public float fireRate = 1.0f;
    public float fireSpeed = 2000;

    [Header("GameObjects")]
    public GameObject dartPrefab;
    public Transform firePoint;

    private float lastFireTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region input functions

    public void Shoot(InputAction.CallbackContext con)
    {
        if (Config.playerIt && (Time.time > lastFireTime + (1/fireRate))) // can only shoot if player is it and are within fire rate cap
        {
            GameObject dartObject = Instantiate(dartPrefab, firePoint.position, firePoint.rotation);

            Dart dart = dartObject.GetComponent<Dart>();
            dart.Shoot(firePoint.transform.forward, fireSpeed);

            lastFireTime = Time.time;
        }
    }

    #endregion input functions
}
