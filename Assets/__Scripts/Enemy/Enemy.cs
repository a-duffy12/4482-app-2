using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float movementSpeed = 8f;
    public float fireRate = 2f;
    public float fireSpeed = 2000;
    public float itDelay = 1.5f;
    public float attackDistance = 25;
    public float minItRange = 5;
    public float maxNotItRange = 30;

    [Header("GameObjects")]
    public GameObject dartPrefab;
    public Transform firePoint;

    Transform playerTransform;
    
    private float lastFireTime;
    [HideInInspector] public float lastItTime;

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

        transform.LookAt(playerTransform.position);
        firePoint.LookAt(new Vector3(playerTransform.position.x, playerTransform.position.y + 0.25f, playerTransform.position.z));

        if (distanceToPlayer <= attackDistance) // enemy can target player
        {
            Shoot();
        }

        Move(distanceToPlayer);
    }

    void Shoot()
    {
        if (!Config.playerIt && (Time.time > lastFireTime + (1/fireRate)) && (Time.time > lastItTime + itDelay))
        {
            GameObject dartObject = Instantiate(dartPrefab, firePoint.position, firePoint.rotation);

            Dart dart = dartObject.GetComponent<Dart>();
            dart.Shoot(firePoint.transform.forward, fireSpeed);

            lastFireTime = Time.time;
        }
    }

    void Move(float distanceToPlayer)
    {
        if (Config.playerIt) // player is it
        {
            if (distanceToPlayer < maxNotItRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, -1 * movementSpeed * Time.deltaTime);
            }
        }
        else // enemy is it
        {
            if (distanceToPlayer > minItRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, movementSpeed * Time.deltaTime);
            }
        }
    }
}
