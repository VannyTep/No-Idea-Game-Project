using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemies"))
        {
            TakeHealth(other);
        }
    }

    void TakeHealth(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
    }
}
