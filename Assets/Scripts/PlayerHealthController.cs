using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{   
    public static PlayerHealthController instance;

    public float CurrentPlayerHealth;
    private float _maxPlayerHealth = 6;

    void Awake()
    {
        instance = this;
        CurrentPlayerHealth = _maxPlayerHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentPlayerHealth -= damage;

        if (CurrentPlayerHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Disable the Enemy AI Script
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemies");
        foreach (GameObject items in Enemies)
        {
            items.GetComponent<EnemyAIController>().enabled = false;
        }

        // Play death animation
        Animator animator = GetComponent<Animator>();
        animator.SetBool("IsDeath", true);

        // Disable player movement script
        GetComponent<PlayerController>().enabled = false;

        // Disable player box collider
        GetComponent<BoxCollider2D>().enabled = false;

        Debug.Log("Player has died");
    }
}
