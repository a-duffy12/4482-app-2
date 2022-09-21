using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    public float flyTime = 5.0f;
    public bool playerDart = true;

    [Header("Audio")]
    public AudioClip shootAudio;
    public AudioClip goodAudio;
    public AudioClip badAudio;

    Rigidbody rb;
    AudioSource source;

    float shotTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        source = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        
        if (playerDart)
        {
            source.clip = shootAudio;
            source.Play();
        }

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
            enemy.lastItTime = Time.time;
            source.clip = goodAudio;
            source.Play();
        }
        else if (player != null && !Config.playerIt) // player is hit and enemy is it
        {
            Config.playerIt = true;
            source.clip = badAudio;
            source.Play();
        }

        Destroy(gameObject);
    }
}
