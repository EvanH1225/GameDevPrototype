// using System.Numerics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public int health = 4;
    private Transform player;
    private Rigidbody rb;

    public float knockbackForce = 5f;

    void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player == null) return;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        // Move towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Face the player
        transform.LookAt(player);

    }

    public void TakeDamage()
    {
        health -= 1;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    
    public void ApplyKnockback(Vector3 sourcePosition)
    {
        if (rb == null) return;

        Vector3 direction = (transform.position - sourcePosition).normalized;
        direction.y += 0.2f;

        rb.AddForce(direction * knockbackForce, ForceMode.Impulse);
    }
}
