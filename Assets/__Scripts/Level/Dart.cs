using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    public float flyTime = 2.0f;

    Rigidbody rb;
    float shotTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        shotTime = Time.time;
    }

    void Update()
    {
        if (Time.time > shotTime + flyTime)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot(Vector3 direction, float force)
    {
        rb.AddForce(direction * force);
    }

    void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        PlayerSystem player = collision.collider.GetComponent<PlayerSystem>();

        if (enemy != null && Config.playerIt) // enemy is hit and player is it
        {
            Config.playerIt = false;
        }
        else if (player != null && !Config.playerIt) // player is hit and enemy is it
        {
            Config.playerIt = true;
        }

        Destroy(gameObject);
    }
}
