using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    public float Speed = 1.2f;

    [SerializeField] float max_Range = 2.8f;

    float RandomPosition;

    public Rigidbody2D Rb2d;
    public GameObject Player;

    public static float distance;

    private void Awake() 
    {
        Rb2d = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() 
    {
        distance = Vector2.Distance(Player.transform.position, transform.position);   
        
        if (distance <= max_Range)
        {
            Move();
        }
        else
        {
            return;
        }

        // Facing Method
        Facing();
    }

    private void Move()
    { 
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime);
    }

    private void Facing()
    {
        if (Player.transform.position.x >= transform.position.x) // Facing Right
        {
            transform.localScale = new Vector3(0.4f, 0.4f, 0);
        }
        else if (Player.transform.position.x <= transform.position.x) // Facing Left
        {
            transform.localScale = new Vector3(-0.4f, 0.4f, 0);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, max_Range);
        Gizmos.color = Color.cyan;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.CompareTag("PlayerWeapon"))
        {
            Vector2 Diference = transform.position - other.transform.position;
            transform.position = new Vector2(transform.position.x + Diference.x, transform.position.y + Diference.y);

            Destroy(this);

            GetComponent<Animator>().SetBool("IsDeath", true);

            Destroy(this.gameObject, 1f);
        }
    }
}
