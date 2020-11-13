using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public BasketballManager manager;
    public AudioSystem AudioSystem;

    public Rigidbody2D rb;
    public CircleCollider2D col;
    
    public Transform groundPosition;
    public Transform startPosition;

    public GameEvent CameraShakeEvent;

    public float randomizeSpawnPoint;
    
    public Vector3 pos => transform.position;
    
    void Start()
    {
        DeactivateRb();
    }

    void Update()
    {
        if (transform.position.y < groundPosition.position.y) Reset();
    }

    public void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
        AudioSystem.Play("Throw");
    }
    
    public void ActivateRb()
    {
        rb.isKinematic = false;
    }

    public void DeactivateRb()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
    }

    private void Reset()
    {
        transform.position = startPosition.position + new Vector3(Random.Range(-randomizeSpawnPoint, randomizeSpawnPoint),
            Random.Range(-randomizeSpawnPoint, randomizeSpawnPoint));
        DeactivateRb();
        manager.isAbleToDrag = true;

        if (!manager.isScored)
        {
            manager.LoseStreak();
        }

        manager.isScored = false;
        manager.Reset();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude > 10)
        {
            AudioSystem.Play("Collision");
            CameraShakeEvent.Raise();
        }
    }
}
