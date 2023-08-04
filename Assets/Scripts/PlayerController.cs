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
    }

    private void FixedUpdate() 
    {
        Rb2d.MovePosition(Rb2d.position + _movement * Speed * Time.fixedDeltaTime);
    }

    void Facing()
    {
        if (_movement.x == 1)
        {
            _movement_Animation.x = 1;
            _movement_Animation.y = 0;
        }

        if (_movement.x == -1)
        {
            _movement_Animation.x = -1;
            _movement_Animation.y = 0;
        }

        if (_movement.y == 1)
        {
            _movement_Animation.x = 0;
            _movement_Animation.y = 1;
        }

        if (_movement.y == -1)
        {
            _movement_Animation.x = 0;
            _movement_Animation.y = -1;
        }
    }
}
