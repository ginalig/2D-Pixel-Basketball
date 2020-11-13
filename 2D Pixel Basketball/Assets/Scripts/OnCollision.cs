using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollision : MonoBehaviour
{
    public UnityEvent OnTriggerEnter;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEnter.Invoke();
    }
}
