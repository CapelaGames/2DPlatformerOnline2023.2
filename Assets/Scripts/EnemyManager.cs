using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int Damage = 20;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
             Health playerHealth = other.collider.GetComponent<Health>();
             if (playerHealth != null)
             { 
                 playerHealth.Damage(Damage);
             }
        }
    }
}
