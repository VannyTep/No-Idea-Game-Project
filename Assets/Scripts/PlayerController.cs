using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;

    public GameObject[] AttackPoint_Object;

    public Rigidbody2D Rb2d;
    public Animator Animator;

    Vector2 _movement;
    Vector2 _movement_Animation;

    public float AttackRate = 2f;
    float _nextAttackTime = 0f;

    bool IsAttack = false;

    private void Awake() 
    {
        
    }

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
        FacingAttack();

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

    void FacingAttack()
    {
        if (!IsAttack)
        {
            for (int i = 0; i < AttackPoint_Object.Length; i++)
            {
                AttackPoint_Object[i].GetComponent<BoxCollider2D>().enabled = false;
            }
            return;
        }

        if (_movement.x == 1 && IsAttack) // Facing Right
        {
            AttackPoint_Object[0].GetComponent<BoxCollider2D>().enabled = false; // Up
            AttackPoint_Object[1].GetComponent<BoxCollider2D>().enabled = false; // Down
            AttackPoint_Object[2].GetComponent<BoxCollider2D>().enabled = true; // Right
            AttackPoint_Object[3].GetComponent<BoxCollider2D>().enabled = false; // Left
        }

        if (_movement.x == -1 && IsAttack) // Facing Left
        {
            AttackPoint_Object[0].GetComponent<BoxCollider2D>().enabled = false; // Up
            AttackPoint_Object[1].GetComponent<BoxCollider2D>().enabled = false; // Down
            AttackPoint_Object[2].GetComponent<BoxCollider2D>().enabled = false; // Right
            AttackPoint_Object[3].GetComponent<BoxCollider2D>().enabled = true; // Left
        }

        if (_movement.y == 1 && IsAttack) // Facing Up
        {
            AttackPoint_Object[0].GetComponent<BoxCollider2D>().enabled = true; // Up
            AttackPoint_Object[1].GetComponent<BoxCollider2D>().enabled = false; // Down
            AttackPoint_Object[2].GetComponent<BoxCollider2D>().enabled = false; // Right
            AttackPoint_Object[3].GetComponent<BoxCollider2D>().enabled = false; // Left
        }

        if (_movement.y == -1 && IsAttack) // Facing Down
        {
            AttackPoint_Object[0].GetComponent<BoxCollider2D>().enabled = false; // Up
            AttackPoint_Object[1].GetComponent<BoxCollider2D>().enabled = true; // Down
            AttackPoint_Object[2].GetComponent<BoxCollider2D>().enabled = false; // Right
            AttackPoint_Object[3].GetComponent<BoxCollider2D>().enabled = false; // Left
        }
    }

    IEnumerator Attack()
    {
        Animator.SetTrigger("IsAttack");

        IsAttack = true;

        Speed = 0;

        yield return new WaitForSeconds(.8f);

        Speed = 3;

        IsAttack = false;
    }
}
