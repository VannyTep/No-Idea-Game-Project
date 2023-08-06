using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;

    public Rigidbody2D Rb2d;
    public Animator Animator;

    Vector2 _movement;
    Vector2 _movement_Animation;

    public float AttackRate = 3f;
    float _nextAttackTime = 0f;

    private void Update()
    {   
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        _movement.Normalize();

        Animator.SetFloat("Horizontal", _movement.x);
        Animator.SetFloat("Vertical", _movement.y);

        Animator.SetFloat("Idle_Horizontal", _movement_Animation.x);
        Animator.SetFloat("Idle_Vertical", _movement_Animation.y);

        Animator.SetFloat("Speed", _movement.sqrMagnitude);

        // Methods
        Facing();

        if (Time.time >= _nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine("Attack");
                _nextAttackTime = Time.time + 1f / AttackRate;
            }
        }
    }

    private void FixedUpdate() 
    {
        Rb2d.MovePosition(Rb2d.position + _movement * Speed * Time.fixedDeltaTime);
    }

    void Facing()
    {
        if (_movement.x == 1) // Facing Right
        {
            _movement_Animation.x = 1;
            _movement_Animation.y = 0;
        }

        if (_movement.x == -1) // Facing Left
        {
            _movement_Animation.x = -1;
            _movement_Animation.y = 0;
        }

        if (_movement.y == 1) // Facing Up
        {
            _movement_Animation.x = 0;
            _movement_Animation.y = 1;
        }

        if (_movement.y == -1) // Facing Down
        {
            _movement_Animation.x = 0;
            _movement_Animation.y = -1;
        }
    }

    IEnumerator Attack()
    {
        Animator.SetTrigger("IsAttack");

        Speed = 0;

        yield return new WaitForSeconds(.75f);

        Speed = 3;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag("Enemies"))
        {
            Vector2 Diference = transform.position - other.transform.position;
            transform.position = new Vector2(transform.position.x + Diference.x, transform.position.y + Diference.y);

            // Play hurt animation
            Animator.SetTrigger("IsHurt");

            // Take player health
            PlayerHealthController.instance.TakeDamage(0.5f);
        }
    }
}
