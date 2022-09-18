using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float fireRate = 4.0f;
    public float fireSpeed = 1500;
    public float lookDistance = 150;
    public float attackDistance = 50;

    [Header("GameObjects")]
    public GameObject dartPrefab;
    public Transform firePoint;

    Transform playerTransform;
    
    private float lastFireTime;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // audio source setup
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        if (distanceToPlayer <= lookDistance) // enemy can see player
        {
            transform.LookAt(playerTransform.position);
            firePoint.LookAt(new Vector3(playerTransform.position.x, playerTransform.position.y + 0.25f, playerTransform.position.z));

            if (distanceToPlayer <= attackDistance) // enemy can target player
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        if (!Config.playerIt && (Time.time > lastFireTime + (1/fireRate)))
        {
            GameObject dartObject = Instantiate(dartPrefab, firePoint.position, firePoint.rotation);

            Dart dart = dartObject.GetComponent<Dart>();
            dart.Shoot(firePoint.transform.forward, fireSpeed);

            lastFireTime = Time.time;
        }
    }
}
